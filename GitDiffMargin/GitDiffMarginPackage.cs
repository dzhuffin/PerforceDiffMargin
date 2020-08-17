using System.ComponentModel.Design;
using Microsoft.VisualStudio.Text.Editor;

namespace PerforceDiffMargin
{
    using System;
    using System.Runtime.InteropServices;
    using PerforceDiffMargin.Core;
    using PerforceDiffMargin.Git;
    using Microsoft.VisualStudio.Shell;
    using System.Windows;
    using Microsoft.VisualStudio.Shell.Interop;
    using PerforceDiffMargin.View;
    using Microsoft.Internal.VisualStudio.PlatformUI;

    [PackageRegistration(UseManagedResourcesOnly = true)]
    [Guid("F82C1EF6-3B52-425E-BC28-4934E6073A32")]

    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]

    public class GitDiffMarginPackage : Package
    {
        protected override void Initialize()
        {
            base.Initialize();
            // Add our command handlers for menu (commands must be declared in the .vsct file)
            OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                CommandID refreshCommandID = new CommandID(new Guid(GitDiffMarginCommandHandler.GitDiffMarginStaticToolbarCommandSet), (int)PerforceDiffMarginStaticToolbarCommand.Refresh);
                OleMenuCommand refreshCommand = new OleMenuCommand(new EventHandler(OnRefresh), refreshCommandID);
                mcs.AddCommand(refreshCommand);

                CommandID disconnectCommandID = new CommandID(new Guid(GitDiffMarginCommandHandler.GitDiffMarginStaticToolbarCommandSet), (int)PerforceDiffMarginStaticToolbarCommand.Disconnect);
                OleMenuCommand disconnectCommand = new OleMenuCommand(new EventHandler(OnDisconnect), disconnectCommandID);
                mcs.AddCommand(disconnectCommand);

                CommandID settingsCommandID = new CommandID(new Guid(GitDiffMarginCommandHandler.GitDiffMarginStaticToolbarCommandSet), (int)PerforceDiffMarginStaticToolbarCommand.Settings);
                OleMenuCommand settingsCommand = new OleMenuCommand(new EventHandler(OnSettings), settingsCommandID);
                mcs.AddCommand(settingsCommand);
            }

            // Initialize PerforceCommands
            PerforceCommands.GetInstance(this);
        }

        private void OnSettings(object sender, EventArgs e)
        {
            IVsUIShell uiShell = (IVsUIShell)GetService(typeof(SVsUIShell));
            var myDialog = new SettingsDialog();

            myDialog.HasMinimizeButton = false;
            myDialog.HasMaximizeButton = true;
            myDialog.Title = "Perforce Connection Settings";
            myDialog.ShowModal();
        }

        private void OnRefresh(object sender, EventArgs e)
        {
            var status = PerforceCommands.GetInstance().RefreshConnection(out string message_text);

            MessageBox.Show(message_text, "Perforce Connection");
        }

        private void OnDisconnect(object sender, EventArgs e)
        {
            // TODO: add try-catch?
            PerforceCommands.GetInstance().Disconnect();
            MessageBox.Show("Disconnected", "Perforce Connection");
        }

    }
}
