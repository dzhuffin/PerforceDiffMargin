using System;
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
        private readonly bool _connected;

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

            _connected = true;
        }

        public IEnumerable<HunkRangeInfo> GetGitDiffFor(ITextDocument textDocument, string originalPath, ITextSnapshot snapshot)
        {
            throw new NotImplementedException();
        }

        public void StartExternalDiff(ITextDocument textDocument, string originalPath)
        {
            throw new NotImplementedException();
        }

        public bool TryGetOriginalPath(string path, out string originalPath)
        {
            throw new NotImplementedException();
        }

        public bool IsGitRepository(string path, string originalPath)
        {
            return _connected;
        }
    }
}