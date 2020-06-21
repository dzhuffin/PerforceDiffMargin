namespace GitDiffMargin.Commands
{
    using Microsoft.VisualStudio.Text;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Text.Editor.Commanding;

    internal class ChangelistComboCommandArgs : EditorCommandArgs
    {
        public ChangelistComboCommandArgs(ITextView textView, ITextBuffer subjectBuffer)
            : base(textView, subjectBuffer)
        {
        }
    }
}
