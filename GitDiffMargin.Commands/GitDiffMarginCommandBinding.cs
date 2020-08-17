namespace PerforceDiffMargin.Commands
{
    using System.ComponentModel.Composition;
    using Microsoft.VisualStudio.Editor.Commanding;

    internal class GitDiffMarginCommandBinding
    {
#pragma warning disable CS0649 // Field 'fieldName' is never assigned to, and will always have its default value null

        [Export]
        [CommandBinding(GitDiffMarginCommandHandler.GitDiffMarginCommandSet, (uint)PerforceDiffMarginCommand.PreviousChange, typeof(PreviousChangeCommandArgs))]
        internal CommandBindingDefinition PreviousChangeCommandBinding;

        [Export]
        [CommandBinding(GitDiffMarginCommandHandler.GitDiffMarginCommandSet, (uint)PerforceDiffMarginCommand.NextChange, typeof(NextChangeCommandArgs))]
        internal CommandBindingDefinition NextChangeCommandBinding;

        [Export]
        [CommandBinding(GitDiffMarginCommandHandler.GitDiffMarginCommandSet, (uint)PerforceDiffMarginCommand.RollbackChange, typeof(RollbackChangeCommandArgs))]
        internal CommandBindingDefinition RollbackChangeCommandBinding;

        [Export]
        [CommandBinding(GitDiffMarginCommandHandler.GitDiffMarginCommandSet, (uint)PerforceDiffMarginCommand.CopyOldText, typeof(CopyOldTextCommandArgs))]
        internal CommandBindingDefinition CopyOldTextCommandBinding;

        [Export]
        [CommandBinding(GitDiffMarginCommandHandler.GitDiffMarginCommandSet, (uint)PerforceDiffMarginCommand.ShowPopup, typeof(ShowPopupCommandArgs))]
        internal CommandBindingDefinition ShowPopupCommandBinding;

#pragma warning restore CS0649 // Field 'fieldName' is never assigned to, and will always have its default value null
    }
}
