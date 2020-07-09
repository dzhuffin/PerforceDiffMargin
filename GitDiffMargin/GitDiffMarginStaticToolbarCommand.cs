namespace GitDiffMargin
{
    using System.Runtime.InteropServices;

    [Guid(GitDiffMarginCommandHandler.GitDiffMarginStaticToolbarCommandSet)]
    public enum GitDiffMarginStaticToolbarCommand
    {
        Refresh = 0,
        Disconnect = 1,
        Settings = 2,

        GitDiffStaticToolbar = 100,
        GitDiffStaticToolbarGroup = 150,
    }
}