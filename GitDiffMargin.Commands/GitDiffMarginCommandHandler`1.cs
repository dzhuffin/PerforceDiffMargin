namespace PerforceDiffMargin.Commands
{
    using System;
    using Microsoft.VisualStudio.OLE.Interop;
    using Microsoft.VisualStudio.Text.Editor.Commanding;

    internal abstract class GitDiffMarginCommandHandler<T> : ShimCommandHandler<T>
        where T : EditorCommandArgs
    {
        protected GitDiffMarginCommandHandler(PerforceDiffMarginCommand commandId)
            : base(new Guid(GitDiffMarginCommandHandler.PerforceDiffMarginCommandSet), (uint)commandId)
        {
        }

        protected override IOleCommandTarget GetCommandTarget(T args)
            => args.TextView.Properties.GetProperty<GitDiffMarginCommandHandler>(typeof(GitDiffMarginCommandHandler));
    }
}
