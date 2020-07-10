using System.ComponentModel.Design;
using Microsoft.VisualStudio.Text.Editor;

namespace GitDiffMargin
{
    using System;
    using System.Runtime.InteropServices;
    using GitDiffMargin.Core;
    using GitDiffMargin.Git;
    using Microsoft.VisualStudio.Shell;
    using System.Windows;
    using Microsoft.VisualStudio.Shell.Interop;
    using GitDiffMargin.View;
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
                CommandID refreshCommandID = new CommandID(new Guid(GitDiffMarginCommandHandler.GitDiffMarginStaticToolbarCommandSet), (int)GitDiffMarginStaticToolbarCommand.Refresh);
                OleMenuCommand refreshCommand = new OleMenuCommand(new EventHandler(OnRefresh), refreshCommandID);
                mcs.AddCommand(refreshCommand);

                CommandID disconnectCommandID = new CommandID(new Guid(GitDiffMarginCommandHandler.GitDiffMarginStaticToolbarCommandSet), (int)GitDiffMarginStaticToolbarCommand.Disconnect);
                OleMenuCommand disconnectCommand = new OleMenuCommand(new EventHandler(OnDisconnect), disconnectCommandID);
                mcs.AddCommand(disconnectCommand);

                CommandID settingsCommandID = new CommandID(new Guid(GitDiffMarginCommandHandler.GitDiffMarginStaticToolbarCommandSet), (int)GitDiffMarginStaticToolbarCommand.Settings);
                OleMenuCommand settingsCommand = new OleMenuCommand(new EventHandler(OnSettings), settingsCommandID);
                mcs.AddCommand(settingsCommand);
            }

            // Initialize PerforceCommands
            PerforceCommands.getInstance(this);
        }

        private void OnSettings(object sender, EventArgs e)
        {
            IVsUIShell uiShell = (IVsUIShell)GetService(typeof(SVsUIShell));
            var myDialog = new SettingsDialog();

            myDialog.HasMinimizeButton = false;
            myDialog.HasMaximizeButton = true;
            myDialog.ShowModal();
        }

        private void OnRefresh(object sender, EventArgs e)
        {
            PerforceCommands.getInstance().RefreshConnection();

            string message_text = "";
            if (!PerforceCommands.getInstance().Connected)
            {
                string error_msg = PerforceCommands.getInstance().GetConnectionError();
                message_text = String.IsNullOrEmpty(error_msg) ? "Unknown error. Perforce connection is not established" : error_msg;
            }
            else
            {
                message_text = "Successfully connected!";
            }
            MessageBox.Show(message_text);

            // Code can be useful get current text view:
            //var textManager = (IVsTextManager)GetService(typeof(SVsTextManager));
            ////var editor = (IVsEditorAdaptersFactoryService)GetService(typeof(IVsEditorAdaptersFactoryService));
            //if (textManager != null)
            //{
            //    textManager.GetActiveView(1, null, out IVsTextView textViewCurrent);

            //    IWpfTextView view = null;
            //    IVsUserData userData = textViewCurrent as IVsUserData;

            //    if (null != userData)
            //    {
            //        IWpfTextViewHost viewHost;
            //        object holder;
            //        Guid guidViewHost = DefGuidList.guidIWpfTextViewHost;
            //        userData.GetData(ref guidViewHost, out holder);
            //        viewHost = (IWpfTextViewHost)holder;
            //        view = viewHost.TextView;
            //    }

            //}
            //IVsTextManager txtMgr = (IVsTextManager)GetService(typeof(SVsTextManager));
            //IVsTextView vTextView = null;
            //int mustHaveFocus = 1;
            //txtMgr.GetActiveView(mustHaveFocus, null, out vTextView);
            //vTextView.
            //            MarginCore marginCore;
            //if (vTextView.Properties.TryGetProperty(typeof(MarginCore), out marginCore))
            //{
            //    return marginCore;
            //}
        }

        private void OnDisconnect(object sender, EventArgs e)
        {
            // TODO: add try-catch?
            PerforceCommands.getInstance().Disconnect();
            MessageBox.Show("Disconnected");
        }

    }
}
