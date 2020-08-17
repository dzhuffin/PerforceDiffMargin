namespace PerforceDiffMargin
{
    using System.Runtime.InteropServices;

    [Guid(GitDiffMarginCommandHandler.GitDiffMarginStaticToolbarCommandSet)]
    public enum PerforceDiffMarginStaticToolbarCommand
    {
        Refresh = 0,
        Disconnect = 1,
        Settings = 2,

        PerforceDiffStaticToolbar = 100,
        PerforceDiffStaticToolbarGroup = 150,
    }
}