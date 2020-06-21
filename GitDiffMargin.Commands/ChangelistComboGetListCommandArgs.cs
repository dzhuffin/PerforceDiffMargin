namespace GitDiffMargin.Commands
{
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Text.Editor.Commanding;

    internal class ChangelistComboGetListCommandArgs : EditorCommandArgs
    {
        public ChangelistComboGetListCommandArgs(ITextView textView, ITextBuffer subjectBuffer)
            : base(textView, subjectBuffer)
        {
        }
    }
}
