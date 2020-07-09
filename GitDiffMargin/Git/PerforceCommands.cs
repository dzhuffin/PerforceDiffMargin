using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Perforce.P4;

namespace GitDiffMargin.Git
{
    public class PerforceCommands : IGitCommands
    {
        private static PerforceCommands instance = null;

        private readonly IServiceProvider _serviceProvider;
        private Server _server;
        private Repository _repository;
        private Connection _connection;
        private string _perforceRoot;
        private bool _connected;
        private string _last_error;

        public static PerforceCommands getInstance(IServiceProvider serviceProvider = null)
        {
            if (instance == null)
                instance = new PerforceCommands(serviceProvider);
            return instance;
        }

        // P4USER, P4PORT and P4CLIENT should be set. Connection.GetP4EnvironmentVar can be used

        private PerforceCommands(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            RefreshConnection();
        }

        public void RefreshConnection()
        {
            Disconnect();
            if (_server == null)
            {
                _server = new Server(new ServerAddress(""));
            }

            if (_repository == null)
            {
                _repository = new Repository(_server);
            }
            _connection = _repository.Connection;

            _connection.UserName = "";
            _connection.Client = new Client();
            _connection.Client.Name = "";
            _connection.Connect(null);

            _perforceRoot = _repository.GetClientMetadata().Root;

            var error_msg = String.Format("Can't establish Perforce connection. P4USER, P4PORT and P4CLIENT perforce environment varialbes should be set. Login should be done. Currently thier values are: \nP4USER={0} \nP4PORT={1} \nP4CLIENT={2}",
                _connection.GetP4EnvironmentVar("P4USER"), _connection.GetP4EnvironmentVar("P4PORT"), _connection.GetP4EnvironmentVar("P4CLIENT"));

            if (!_connection.connectionEstablished() || _connection.GetActiveTicket() == null)
            {
                // TODO: close connection?
                Disconnect();
                _last_error = error_msg;
                return;
            }

            if (_perforceRoot == null || !Directory.Exists(_perforceRoot))
            {
                Disconnect();
                _last_error = error_msg + " \nError: \nWorkspace root is unset or doesn't exist";
                return;
            }

            // check user:
            // check ticket exists and valid. 
            var cmd = new P4Command(_connection, "login", true, new string[] { "-s" }); // p4 login -s get status of the ticket
            try
            {
                // P4Exception will be thrown in case user is not logged in
                cmd.Run();
            }
            catch (P4Exception ex)
            {
                // TODO: close connection?
                Disconnect();
                _last_error = error_msg + " \nError: \n" + ex.CmdLine;
                return;
            }
            // this ticket should belong to current user
            var output = cmd.TextOutput;
            if (cmd.TaggedOutput[0]["User"] != _connection.UserName)
            {
                // TODO: close connection?
                Disconnect();
                _last_error = error_msg + " \nError: \nP4USER variable don't correspond to logged in user.";
                return;
            }

            _connected = true;
            _last_error = "";
        }

        public void Disconnect()
        {
            _connected = false;
            if (_connection != null)
            {
                _connection.Disconnect();
            }
        }

        public bool Connected
        {
            get
            {
                return _connected;
            }
        }

        public IEnumerable<HunkRangeInfo> GetGitDiffFor(ITextDocument textDocument, string originalPath, ITextSnapshot snapshot)
        {
            if (!IsGitRepository(textDocument.FilePath, null))
                yield break;

            var depotPath = GetPerforcePath(textDocument.FilePath);

            IList<FileDiff> target = null;
            try
            {
                // get diff using p4 diff
                GetDepotFileDiffsCmdOptions opts = new GetDepotFileDiffsCmdOptions(GetDepotFileDiffsCmdFlags.Unified, 0, 0, "", "", "");
                IList<FileSpec> fsl = new List<FileSpec>();
                FileSpec fs = new FileSpec(new DepotPath(depotPath));
                fsl.Add(fs);
                target = _repository.GetFileDiffs(fsl, opts);
            }
            catch (P4Exception ex)
            {
                Disconnect();
                yield break;
            }

            // TODO: implement comparison with yellow changes not "file on disk" vs "file in depot" but "file in RAM in VS" vs "file in depot"
            //var content = GetCompleteContent(textDocument, snapshot);
            //if (content == null) yield break;

            // TODO: after debugging remove the second and the third arguments from c'tor and next code, are they useless?
            var gitDiffParser = new GitDiffParser(target[0].Diff, 0, false);
            var hunkRangeInfos = gitDiffParser.Parse();

            foreach (var hunkRangeInfo in hunkRangeInfos)
            {
                yield return hunkRangeInfo;
            }
        }

        // Probably will be required to get content
        private static byte[] GetCompleteContent(ITextDocument textDocument, ITextSnapshot snapshot)
        {
            var currentText = snapshot.GetText();

            var content = textDocument.Encoding.GetBytes(currentText);

            var preamble = textDocument.Encoding.GetPreamble();
            if (preamble.Length == 0) return content;

            var completeContent = new byte[preamble.Length + content.Length];
            Buffer.BlockCopy(preamble, 0, completeContent, 0, preamble.Length);
            Buffer.BlockCopy(content, 0, completeContent, preamble.Length, content.Length);

            return completeContent;
        }

        public void StartExternalDiff(ITextDocument textDocument, string originalPath)
        {
            throw new NotImplementedException();
        }

        public bool TryGetOriginalPath(string path, out string originalPath)
        {
            // TODO: after debugging remove originalPath from all places
            originalPath = null;
            return true;
        }

        public bool IsGitRepository(string path, string originalPath)
        {
            return _connected && IsFileUnderPerforceRoot(path);
        }

        public string GetConnectionError()
        {
            return _last_error;
        }

        public string GetP4EnvironmentVar(string varName)
        {
            return _connection == null ? null : _connection.GetP4EnvironmentVar(varName);
        }

        private bool IsFileUnderPerforceRoot(string absolutePath)
        {
            return absolutePath.StartsWith(_perforceRoot, StringComparison.OrdinalIgnoreCase);
        }

        private string GetPerforcePath(string absolutePath)
        {
            // TODO: implement 2 following checks as asserts
            if (!IsGitRepository(absolutePath, null))
            {
                return null;
            }

            if (!IsFileUnderPerforceRoot(absolutePath))
            {
                return null;
            }

            string perforcePath = absolutePath.Substring(_perforceRoot.Length, absolutePath.Length - _perforceRoot.Length);
            perforcePath = perforcePath.Replace('\\', '/');
            perforcePath = perforcePath.Insert(0, "/");

            return perforcePath;
        }
    }
}