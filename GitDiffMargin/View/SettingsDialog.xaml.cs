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
using PerforceDiffMargin.Perforce;
using Microsoft.VisualStudio.PlatformUI;
using Perforce.P4;

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

            try
            {
                _initialPort = PortTextBox.Text = commands.GetP4EnvironmentVar("P4PORT");
            }
            catch (P4Exception)
            {
                // optimization - it's useless to get client and user without port
                return;
            }

            try
            {
                _initialClient = ClientTextBox.Text = commands.GetP4EnvironmentVar("P4CLIENT");
            }
            catch (P4Exception)
            {
            }

            try
            {
                _initialUser = UserTextBox.Text = commands.GetP4EnvironmentVar("P4USER");
            }
            catch (P4Exception)
            {
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
                try
                {
                    commands.SetNewPort(PortTextBox.Text);
                    commands.SetP4EnvironmentVar("P4PORT", PortTextBox.Text);
                }
                catch (P4Exception ex)
                {
                    SetError("An error occured: " + ex.Message);
                    return;
                }
            }

            if (_initialClient != ClientTextBox.Text)
            {
                try
                {
                    commands.SetP4EnvironmentVar("P4CLIENT", ClientTextBox.Text);
                }
                catch (P4Exception)
                {
                }
            }

            if (_initialUser != UserTextBox.Text)
            {
                try
                {
                    commands.SetP4EnvironmentVar("P4USER", UserTextBox.Text);
                }
                catch (P4Exception)
                {
                }
            }

            string password = PasswordTextBox.Password;
            if (!String.IsNullOrEmpty(password))
            {
                try
                {
                    commands.Login(password);
                    commands.RefreshConnection();
                    SetInfo("Logged in successfully");
                }
                catch (P4Exception ex)
                {
                    SetError("An error occured: " + ex.Message);
                }
            }
            else
            {
                SetError("Please, enter the password");
            }
        }
    }
}
