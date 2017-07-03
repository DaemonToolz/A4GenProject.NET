using AgentWpfClient.Security;
using AgentWpfClient.TokenManagementService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace AgentWpfClient
{
    /// <summary>
    /// Interaction logic for UpdateCredentials.xaml
    /// </summary>
    public partial class UpdateCredentials : Page, IDisposable
    {
        private static MainWindow Owner;
        public TokenGenerationServiceClient TokenGenerator;
        internal static Credentials UserCredentials = new Credentials() { DefaultUserName = "", DefaultPasswordProtected = "" };

        public UpdateCredentials()
        {


            InitializeComponent();
            Owner = (MainWindow)App.Current.MainWindow;

            // NOT TO DO
            //UserName.Text = "DefaultAuthorizedUser";
            //PasswordBox.Password = "defaultpassword";


        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Owner.NavigationFrame.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Valider les données d'authentification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidateBtn_Click(object sender, RoutedEventArgs e){

            Task.Factory.StartNew((Action)(() => {
                Dispatcher.Invoke((Action)(() => {
                    SkipBtn.Visibility = Visibility.Hidden;
                    ResultsLabel.Content = "Récupération des informations";
                    WaitingSpinner.Visibility = Visibility.Visible;
                }));
                try
                {
                    Dispatcher.Invoke((Action)(() =>
                    {

                        int DispatcherFailedAttempts = 0;
                        while (DispatcherFailedAttempts++ < 5){
                            try
                            {
                                if (TokenGenerator != null)
                                    TokenGenerator.Close();
                                TokenGenerator = new TokenGenerationServiceClient();
                                TokenGenerator.ClientCredentials.UserName.UserName = UserName.Text;
                                TokenGenerator.ClientCredentials.UserName.Password = !PasswordBox.Password.Equals(UserCredentials.DefaultPasswordProtected) ? PasswordBox.Password : PasswordProtection.Unprotect(PasswordBox.Password);
                                TokenGenerator.Open();
                                break;
                            }
                            catch
                            {
                                
                            }
                        }
                        if (DispatcherFailedAttempts == 5)
                            throw new Exception("Impossible de se connecter au services des tokens");
                    }));

                    int TaskFailedAttempts = 0;
                    while (TaskFailedAttempts++ < 5)
                    {
                        try
                        {
                            UserCredentials.GeneratedToken = TokenGenerator.GenerateToken();
                        }
                        catch
                        {

                        }
                    }
                    if (TaskFailedAttempts == 5)
                        throw new Exception("Impossible d'utiliser le service des tokens, veuillez réessayer plus tard");

                    Dispatcher.Invoke((Action)(() =>
                    {
                        TokenGenerator.Close();

                        ResultsLabel.Content = "Connexion réussie";
                        ServeurResponse.Text = UserCredentials.GeneratedToken;

                        SkipBtn.Visibility = Visibility.Visible;
                    }));
                }
                catch (FaultException fe)
                {
                    Dispatcher.Invoke((Action)(() =>
                    {
                        ServeurResponse.Text = fe.Message;

                        ResultsLabel.Content = String.Format("Échec car {0}", fe.Message);
                        TokenGenerator.Close();
                    }));
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke((Action)(() =>
                    {
                        ServeurResponse.Text = ex.Message + Environment.NewLine + (ex.InnerException != null ? ex.InnerException.Message : "");
                        ResultsLabel.Content = String.Format("Échec");
                        TokenGenerator.Close();
                    }));
                }
                finally
                {
                    Dispatcher.Invoke((Action)(() => {
                        
                        WaitingSpinner.Visibility = Visibility.Hidden;
                        Owner.CheckStatus(true);
                    }));
                   
                }
            }));
        }

        public void InvokeValidation(){
            if(UserCredentials.GeneratedToken == null)
                ValidateBtn_Click(ValidateBtn, new System.Windows.RoutedEventArgs());
        }

        private void SkipBtn_Click(object sender, RoutedEventArgs e){
            Owner.UpdateAllData();
            Owner.NavigationFrame.Navigate(Owner.NavigationPanel["DecipherData"]);
            Owner.NavigationFrame.Visibility = Visibility.Visible;
        }

        public void Dispose()
        {
            if (TokenGenerator != null)
                TokenGenerator.Abort();
        }
    }
}
