using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PerforceDiffMargin.Git;
using Microsoft.VisualStudio.PlatformUI;

namespace PerforceDiffMargin.View
{
    /// <summary>
    /// Interaction logic for SettingsDialog.xaml
    /// </summary>
    public partial class SettingsDialog : DialogWindow
    {
        string _initialPort;
        string _initialClient;
        string _initialUser;

        public SettingsDialog()
        {
            InitializeComponent();

            var commands = PerforceCommands.GetInstance();

            // TODO: do the following on open dialog not in c'tor
            _initialPort = PortTextBox.Text = commands.GetP4EnvironmentVar("P4PORT");
            if (_initialPort != null)
            {
                _initialClient = ClientTextBox.Text = commands.GetP4EnvironmentVar("P4CLIENT");
                _initialUser = UserTextBox.Text = commands.GetP4EnvironmentVar("P4USER");
            }
        }

        private void SetError(string error)
        {
            ResultLabel.Foreground = Brushes.Red;
            ResultLabel.Content = error.Trim();
        }

        private void SetInfo(string info)
        {
            ResultLabel.Foreground = Brushes.Black;
            ResultLabel.Content = info.Trim();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            ResultLabel.Content = "";

            var commands = PerforceCommands.GetInstance();

            if (_initialPort != PortTextBox.Text)
            {
                if (String.IsNullOrEmpty(PortTextBox.Text))
                {
                    SetError("Please, set the address");
                    return;
                }
                bool init_res = commands.SetNewPort(PortTextBox.Text);
                if (!init_res)
                {
                    SetError("An error occured: " + PerforceCommands.GetInstance().GetConnectionError());
                    return;
                    // can't connect to Perforce because Port is not set
                }

                commands.SetP4EnvironmentVar("P4PORT", PortTextBox.Text);
            }

            if (_initialClient != ClientTextBox.Text)
                commands.SetP4EnvironmentVar("P4CLIENT", ClientTextBox.Text);

            if (_initialUser != UserTextBox.Text)
                commands.SetP4EnvironmentVar("P4USER", UserTextBox.Text);

            string password = PasswordTextBox.Password;
            if (!String.IsNullOrEmpty(password))
            {
                if (commands.Login(password))
                {
                    var state = commands.RefreshConnection(out string msg);
                    if (state == PerforceCommands.ConnectionState.Success)
                    {
                        SetInfo("Logged in successfully");
                    }
                    else
                    {
                        SetError("An error occured: " + PerforceCommands.GetInstance().GetConnectionError());
                    }
                }
                else
                {
                    SetError("An error occured: " + PerforceCommands.GetInstance().GetConnectionError());
                }
            }
            else
            {
                SetError("Please, enter the password");
            }
        }
    }
}
