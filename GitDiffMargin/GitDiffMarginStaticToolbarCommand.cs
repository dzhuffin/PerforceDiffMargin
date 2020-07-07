namespace GitDiffMargin
{
    using System.Runtime.InteropServices;

    [Guid(GitDiffMarginCommandHandler.GitDiffMarginStaticToolbarCommandSet)]
    public enum GitDiffMarginStaticToolbarCommand
    {
        Refresh = 0,
        Disconnect = 1,
        SetP4Port = 2,
        SetUser = 3,
        SetWorkspace = 4,

        GitDiffStaticToolbar = 100,
        GitDiffStaticToolbarGroup = 150,
    }
}