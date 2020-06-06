using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;

namespace GitDiffMargin.Git
{
    //[Export(typeof(IGitCommands))]
    public class PerforceCommands : IGitCommands
    {
        private readonly SVsServiceProvider _serviceProvider;

        [ImportingConstructor]
        public PerforceCommands(SVsServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public string GetGitRepository(string path, string originalPath)
        {
            throw new NotImplementedException();
        }
    }
}