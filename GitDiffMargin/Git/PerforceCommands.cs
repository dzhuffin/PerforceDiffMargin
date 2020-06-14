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
    //[Export(typeof(IGitCommands))]
    public class PerforceCommands : IGitCommands
    {
        private readonly SVsServiceProvider _serviceProvider;
        private readonly Server _server;
        private readonly Repository _repository;
        private readonly Connection _connection;
        private readonly string _perforceRoot;
        private readonly bool _connected;
        private readonly int _currentChangelistId;
        private readonly Changelist _currentChangelist;

        // TODO: which actions are supported???
        private static readonly IList<FileAction> _supportedFileActions = new List<FileAction>() {
            FileAction.Edit,
            FileAction.EditFrom
        };

        // P4USER, P4PORT and P4CLIENT should be set. Connection.GetP4EnvironmentVar can be used

        [ImportingConstructor]
        public PerforceCommands(SVsServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _server = new Server(new ServerAddress(""));
            _repository = new Repository(_server);
            _connection = _repository.Connection;

            _connection.UserName = "";
            _connection.Client = new Client();
            _connection.Client.Name = "";
            _connection.Connect(null);


            _perforceRoot = _repository.GetClientMetadata().Root;
            if (_perforceRoot == null || !Directory.Exists(_perforceRoot))
            {
                _connected = false;
                return;
            }

            var currentChangelistId = 451155; // TODO: move to taskbar/property
            _currentChangelist = _repository.GetChangelist(currentChangelistId);

            _connected = true;
        }

        public IEnumerable<HunkRangeInfo> GetGitDiffFor(ITextDocument textDocument, string originalPath, ITextSnapshot snapshot)
        {
            if (!IsGitRepository(textDocument.FilePath, null))
                yield break;

            // Not pending chagelists are already submitted and no changes should be shown
            if (!_currentChangelist.Pending)
                yield break;


            var depotPath = GetPerforcePath(textDocument.FilePath);

            var changelistFileMetaData = _currentChangelist.Files.FirstOrDefault(metaData => metaData.DepotPath.Path == depotPath);

            // file is not found in the current changelist
            if (changelistFileMetaData == null)
                yield break;

            // Unsupported actions check. For example in case new file is added diff can't be shown.
            if (!_supportedFileActions.Contains(changelistFileMetaData.Action))
                yield break;

            // get diff using p4 diff
            GetDepotFileDiffsCmdOptions opts = new GetDepotFileDiffsCmdOptions(GetDepotFileDiffsCmdFlags.Unified, 0, 0, "", "", "");
            IList<FileSpec> fsl = new List<FileSpec>();
            FileSpec fs = new FileSpec(new DepotPath(depotPath));
            fsl.Add(fs);
            IList<FileDiff> target = _repository.GetFileDiffs(fsl, opts);

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