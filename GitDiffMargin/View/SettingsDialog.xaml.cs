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
using GitDiffMargin.Git;
using Microsoft.VisualStudio.PlatformUI;

namespace GitDiffMargin.View
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
            _initialClient = ClientTextBox.Text = commands.GetP4EnvironmentVar("P4CLIENT");
            _initialUser = UserTextBox.Text = commands.GetP4EnvironmentVar("P4USER");
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            ResultLabel.Content = "";

            var commands = PerforceCommands.GetInstance();

            if (_initialUser != PortTextBox.Text)
                commands.SetP4EnvironmentVar("P4PORT", PortTextBox.Text);

            if (_initialUser != ClientTextBox.Text)
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
                        ResultLabel.Foreground = Brushes.Black;
                        ResultLabel.Content = "Logged in successfully";
                    }
                    else
                    {
                        ResultLabel.Foreground = Brushes.Red;
                        ResultLabel.Content = "An error occured: " + PerforceCommands.GetInstance().GetConnectionError();
                    }
                }
                else
                {
                    ResultLabel.Foreground = Brushes.Red;
                    ResultLabel.Content = "An error occured: " + PerforceCommands.GetInstance().GetConnectionError();
                }
            }
            else
            {
                ResultLabel.Foreground = Brushes.Red;
                ResultLabel.Content = "Please, enter the password";
            }
        }
    }
}
