using AgentWpfClient.Security;
using AgentWpfClient.TokenManagementService;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace AgentWpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window{

        private DispatcherTimer _timer;
        private TimeSpan _limitedTime;

        private bool HasbeenUpdated = false;
        public readonly Dictionary<String, Page> NavigationPanel = new Dictionary<string, Page>();

        private void CatSelect_MouseDown(object sender, MouseButtonEventArgs e){
            //

            UpdateAllData();

            //

            NavigationFrame.Navigate(NavigationPanel["ChangeCredential"]);
            NavigationFrame.Visibility = Visibility.Visible;
        }

        internal void UpdateAllData(){

            if (OptionsPage.UserConfiguration.UseAppSecuritySettings)
            {
                UpdateCredentials uc = (UpdateCredentials)NavigationPanel["ChangeCredential"];
                uc.UserName.Text = UpdateCredentials.UserCredentials.DefaultUserName;
                uc.PasswordBox.Password = UpdateCredentials.UserCredentials.DefaultPasswordProtected;

                if ( OptionsPage.UserConfiguration.AutogenerateToken)
                    uc.InvokeValidation();
            }

            DecipheringPag dp = (DecipheringPag)NavigationPanel["DecipherData"];
            if (OptionsPage.UserConfiguration.UseAppSettings)
            {
                // DecipheringPag dp = (DecipheringPag)NavigationPanel["DecipherData"];
                dp.EmailTextBox.Text = (OptionsPage.UserConfiguration.DefaultEmail);
            }

        }

        private void CategorySelection_MouseDown(object sender, MouseButtonEventArgs e){

            UpdateAllData();

            NavigationFrame.Navigate(NavigationPanel["DecipherData"]);
            NavigationFrame.Visibility = Visibility.Visible;
        }

        public void CheckStatus(bool ForceUpdate = false){
            if (UpdateCredentials.UserCredentials.GeneratedToken == null)
            {
                Instruction.Content = "Veuillez vérifier vos accréditations";
                SendCiphered.IsEnabled = false;
                ChangeCred.IsEnabled = true;
            }
            else
            {
                if (ForceUpdate || !HasbeenUpdated){
                    StartCountdown(ForceUpdate);
                }
                SendCiphered.IsEnabled = true;
            }

        }

        private void StartCountdown(bool ForceStop = false){
            if (ForceStop)
            {
                if(_timer != null)
                    _timer.Stop();
                _timer = null;
            }
            _limitedTime = TimeSpan.FromMinutes(5);
            HasbeenUpdated = true;
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate {
                Instruction.Content = String.Format("Vous avez {0} minutes afin de vous authentifier sur le serveur", _limitedTime.ToString("c"));
                if (_limitedTime <= TimeSpan.Zero)
                {
                    _timer.Stop();
                    Instruction.Content = "Veuillez vérifier vos accréditations";
                    NavigationFrame.Visibility = Visibility.Hidden;
                    UpdateCredentials.UserCredentials.GeneratedToken = null;
                    CheckStatus();
                }

                _limitedTime = _limitedTime.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }

        public MainWindow()
        {
            InitializeComponent();

            NavigationPanel.Add("Options", new OptionsPage());
            
            NavigationPanel.Add("ChangeCredential", new UpdateCredentials());
            

            NavigationPanel.Add("DecipherData", new DecipheringPag());
            Thread.Sleep(50);
            UpdateAllData();
            CheckStatus();
        }

        private void MenuBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e){
            try{
                ((UpdateCredentials)NavigationPanel["ChangeCredential"]).Dispose();
            }
            catch{

            }
            try
            {
                ((DecipheringPag)NavigationPanel["DecipherData"]).Dispose();
            }
            catch{

            }
            this.Close();
        }

        private void Options_MouseDown(object sender, MouseButtonEventArgs e)
        {

            NavigationFrame.Navigate(NavigationPanel["Options"]);
            NavigationFrame.Visibility = Visibility.Visible;
        }
    }
}
