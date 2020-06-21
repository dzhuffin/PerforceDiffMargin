namespace GitDiffMargin
{
    using System.Runtime.InteropServices;

    [Guid(GitDiffMarginCommandHandler.GitDiffMarginStaticToolbarCommandSet)]
    public enum GitDiffMarginStaticToolbarCommand
    {
        ChangelistCombo = 0,
        ChangelistComboGetList = 1,

        GitDiffStaticToolbar = 100,
        GitDiffStaticToolbarGroup = 150,
    }
}