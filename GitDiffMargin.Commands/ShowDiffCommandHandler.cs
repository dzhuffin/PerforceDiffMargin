namespace PerforceDiffMargin.Commands
{
    using System.ComponentModel.Composition;
    using Microsoft.VisualStudio.Commanding;
    using Microsoft.VisualStudio.Utilities;

    [Export(typeof(ICommandHandler))]
    [ContentType("text")]
    [Name(nameof(ShowDiffCommandHandler))]
    internal class ShowDiffCommandHandler : GitDiffMarginCommandHandler<ShowDiffCommandArgs>
    {
        public ShowDiffCommandHandler()
            : base(PerforceDiffMarginCommand.ShowDiff)
        {
        }

        public override string DisplayName => "Show Diff";
    }
}
