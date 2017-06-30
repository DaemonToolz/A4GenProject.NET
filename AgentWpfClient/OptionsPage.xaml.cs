using AgentWpfClient.Properties;
using AgentWpfClient.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AgentWpfClient
{
    /// <summary>
    /// Interaction logic for OptionsPage.xaml
    /// </summary>
    public partial class OptionsPage : Page{
        private static MainWindow Owner;
        private static Settings DefaultProperties = Properties.Settings.Default;

        internal static OverallConfigs UserConfiguration = new OverallConfigs();
        public OptionsPage()
        {

            UpdateCredentials.UserCredentials.DefaultUserName = DefaultProperties.DefaultUserName;
            UpdateCredentials.UserCredentials.DefaultPasswordProtected = DefaultProperties.DefaultPassword;

            UserConfiguration.AutogenerateToken = DefaultProperties.AutomaticTokenGeneration;
            UserConfiguration.DefaultSearchPath = DefaultProperties.DefaultSearchPath;
            UserConfiguration.UseAppSecuritySettings = DefaultProperties.UseDefaultSecuritySettings;
            UserConfiguration.UseAppSettings = DefaultProperties.UseDefaultSettings;
            UserConfiguration.DefaultEmail = DefaultProperties.DefaultEmail;


            InitializeComponent();
            Owner = (MainWindow)App.Current.MainWindow;

            SecurityDefaultCheckbox.IsChecked = DefaultProperties.UseDefaultSecuritySettings;

            if (!(SecurityDefaultCheckbox.IsChecked ?? false))
                SecuritySettings.IsEnabled = false;

            SecurityDefaultCheckbox.Click += (sender, e) => {
                if (sender is CheckBox translated){
                    DefaultProperties.UseDefaultSecuritySettings = translated.IsChecked ?? false;
                    DefaultProperties.Save();

                    SecuritySettings.IsEnabled = translated.IsChecked ?? false;
                    UserConfiguration.UseAppSecuritySettings = DefaultProperties.UseDefaultSecuritySettings;
                }
            };

            DefaultPathTextBox.Text = DefaultProperties.DefaultSearchPath.Equals("") || DefaultProperties.DefaultSearchPath == null ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : DefaultProperties.DefaultSearchPath;


            DefaultUsernameTextbox.Text = (DefaultProperties.DefaultUserName ?? "");
            TokenOnStart.IsChecked = DefaultProperties.AutomaticTokenGeneration;

            DefaultCheckbox.IsChecked = DefaultProperties.UseDefaultSettings;
            DefaultEmailTextBox.Text = DefaultProperties.DefaultEmail;


            if (!(DefaultCheckbox.IsChecked ?? false))
                GlobalSettings.IsEnabled = false;

            DefaultCheckbox.Click += (sender, e) =>
            {
                if (sender is CheckBox translated)
                {
                    UserConfiguration.UseAppSettings = GlobalSettings.IsEnabled =  DefaultProperties.UseDefaultSettings = translated.IsChecked ?? false;
                    DefaultProperties.Save();
                    
                    UserConfiguration.DefaultSearchPath = DefaultProperties.DefaultSearchPath;
                }
            };

            TokenOnStart.Click += (sender, e) =>{
                if (sender is CheckBox translated){
                    UserConfiguration.AutogenerateToken =  DefaultProperties.AutomaticTokenGeneration = translated.IsChecked ?? false;
                    DefaultProperties.Save();
                }
            };

            DefaultPasswordPassBox.Password = DefaultProperties.DefaultPassword;


        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Owner.NavigationFrame.Visibility = Visibility.Hidden;
        }

        private void DefaultPasswordPassBox_PasswordChanged(object sender, RoutedEventArgs e){
            if (DefaultPasswordPassBox.Password.Equals(DefaultProperties.DefaultPassword))
                return;
  

        }

        private void DefaultUsernameTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DefaultPathTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SaveChangesBtn_Click(object sender, RoutedEventArgs e){
            if (!DefaultProperties.DefaultPassword.Equals(DefaultPasswordPassBox.Password)){
                DefaultPasswordPassBox.Password = UpdateCredentials.UserCredentials.DefaultPasswordProtected =  DefaultProperties.DefaultPassword = PasswordProtection.Protect(DefaultPasswordPassBox.Password);
                //UpdateCredentials.UserCredentials.DefaultPasswordProtected = DefaultProperties.DefaultPassword;
            }

            if (!DefaultProperties.DefaultUserName.Equals(DefaultUsernameTextbox.Text))
            {
                UpdateCredentials.UserCredentials.DefaultUserName =  DefaultProperties.DefaultUserName = DefaultUsernameTextbox.Text;
                //UpdateCredentials.UserCredentials.DefaultUserName = DefaultProperties.DefaultUserName;
            }
            if (!DefaultProperties.DefaultSearchPath.Equals(DefaultPathTextBox.Text))
            {
                UserConfiguration.DefaultSearchPath =  DefaultProperties.DefaultSearchPath = DefaultPathTextBox.Text;
                //UserConfiguration.DefaultSearchPath = DefaultProperties.DefaultSearchPath;
            }

            if (!DefaultProperties.DefaultEmail.Equals(DefaultEmailTextBox.Text))
            {
                UserConfiguration.DefaultEmail = DefaultProperties.DefaultEmail = DefaultEmailTextBox.Text;
                MessageBox.Show(DefaultProperties.DefaultEmail);
            }


            DefaultProperties.Save();
        }

        private void AddEncryption_Click(object sender, RoutedEventArgs e){
            if (!Resources.Contains(EncryptionName.Text))
                Resources.Add(EncryptionName.Text.Replace(" ", ""), EncryptionName.Text);
        }
    }
}
