using AgentWpfClient.DecipheringProgressService;
using AgentWpfClient.DecipherService;
using AgentWpfClient.Integrity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
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

namespace AgentWpfClient{
    /// <summary>
    /// Interaction logic for DecipheringPag.xaml
    /// </summary>
    public partial class DecipheringPag : Page, IDisposable, IDecipheringProcessUpdaterCallback{
        private ClientFile File = new ClientFile();
        private ServiceResponse Response = new ServiceResponse();

        private static MainWindow Owner;
        public OperationDispatcherClient OperationDispatcher;

        public DecipheringProcessUpdaterClient Updater;
        //private static UpdateCredentials CredentialUpdatePage;

        internal List<String> CipheringTypes = new List<string>();

        private static readonly Dictionary<String, Dictionary<String, decimal>> _Values = new Dictionary<string, Dictionary<string, decimal>>();
        public static Dictionary<String, Dictionary<String, decimal>> Values{
            get { return _Values; }

        }


        public DecipheringPag()
        {


            CipheringTypes.Add("XOR lower alphabetic");
            CipheringTypes.Add("XOR lower alphanumeric");

            Updater = new DecipheringProcessUpdaterClient(new InstanceContext(this));

            InitializeComponent();
            CipheringTypeCombobox.ItemsSource = CipheringTypes;
            WaitingSpinner.Visibility = Visibility.Hidden;
            WarningMessage.Visibility = Visibility.Hidden;
            Owner = (MainWindow)App.Current.MainWindow;
            //CredentialUpdatePage = (UpdateCredentials)Owner.NavigationPanel["ChangeCredential"];
            OperationDispatcher = new OperationDispatcherClient("NetTcpBinding_IOperationDispatcher");
            OperationDispatcher.Open();
            Updater.Open();
            
        }

        /// <summary>
        /// Permet de charger le fichier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileLoaderButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            openFileDialog.InitialDirectory = OptionsPage.UserConfiguration.UseAppSettings ? OptionsPage.UserConfiguration.DefaultSearchPath : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == true)
                FileContentViewer.Text = File.CipheredFileContent = System.IO.File.ReadAllText(File.FileName = openFileDialog.FileName);

            if (File.FileName != null){
                DocumentTitleLabel.Text = File.FileName;
                SendFileBtn.IsEnabled = true;
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Owner.NavigationFrame.Visibility = Visibility.Hidden;
        }


        /// <summary>
        /// Envoyer le fichier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendFileBtn_Click(object sender, RoutedEventArgs e)
        {
            // TODO
            // Use a validator
            if (EmailTextBox.Text.Trim().Equals("") || !EmailTextBox.Text.Contains("@")){
                WarningMessage.Visibility = Visibility.Visible;
                return;
            }

            // TODO
            // Use a validator
            if (!Int32.TryParse(KeySizeTextBox.Text, out int TryParseResult)){
                // Prepare error message here
                return;
            }

            GenerateTextFile.IsEnabled = false;

            File.ContactEmail = EmailTextBox.Text;
            File.KeyBeginning = KeyBeginTextBox.Text;
            File.KeySize = KeySizeTextBox.Text;
            File.Algorithm = CipheringTypeCombobox.Text;
            File.KeyEnding = KeyEndTextBox.Text;
            WarningMessage.Visibility = Visibility.Hidden;


            ConnectionToken ct = new ConnectionToken()
            {
                UserToken = UpdateCredentials.UserCredentials.GeneratedToken,//MainWindow.UserToken,
                Operation = "UpdateFile",
                AppToken = "78de44f-880a5dccb-DS",
                Data = new Object[7] { File.FileName, File.CipheredFileContent, File.ContactEmail, File.KeyBeginning, File.KeySize, File.Algorithm, File.KeyEnding }
            };

            Task.Factory.StartNew((Action)(()=>{
                Dispatcher.Invoke((Action)(() => {
                    SendFileBtn.IsEnabled = false;
                    Owner.ChangeCred.IsEnabled = false;
                    DecryptStatus.Content = "En cours";
                    WaitingSpinner.Visibility = Visibility.Visible;
                }));


                var operation = OperationDispatcher.Dispatch(ct);

                Dispatcher.Invoke((Action)(() => {
                    Owner.ChangeCred.IsEnabled = true;
                    WaitingSpinner.Visibility = Visibility.Hidden;
                    DecryptStatus.Content = "Terminé";
                    DocumentTitleLabel.Text = "";

                    if (((String)operation.Data[0]).Contains("____")){
                        String[] Results = ((String)operation.Data[0]).Split(new String[1] { "____" }, StringSplitOptions.None);
                        Response.DecipheredText = Results[0];
                        Response.FoundEmail = (Results.Length < 4 ? "Unknown" : Results[3]);
                        Response.FoundKey = Results[1];

                        String output = String.Format("Le texte déchiffré est {0} {1} La clé utilisée est {2} {1} L'email récupéré est {3}", Response.DecipheredText, Environment.NewLine, Response.FoundKey, Response.FoundEmail);


                        FileContentViewer.Text = output;

                        GenerateTextFile.IsEnabled = true;
                    } else {
                        FileContentViewer.Text = (String)operation.Data[0];
                    }

                 }));
            }));

            Updater.ShowProgress(File.FileName);

        }

        public void Dispose(){
            if (OperationDispatcher != null)
                OperationDispatcher.Abort();

            if (Updater != null)
                Updater.Abort();
            
        }

        /// <summary>
        /// Callback function
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="Rounds"></param>
        /// <param name="TotalPossibilities"></param>
        /// <param name="Percentage"></param>
        public void Progress(string FileName, decimal Rounds, decimal TotalPossibilities, decimal Percentage){
            Dispatcher.Invoke((Action)(() =>
            {
                if (FileName.Equals(File.FileName) && Rounds != TotalPossibilities)
                    DecryptStatus.Content = "Déchiffrement démarré";
                else if (FileName.Equals(File.FileName) && Rounds == TotalPossibilities)
                    DecryptStatus.Content = "Validation du déchiffrement en cours";
                else
                    DecryptStatus.Content = FileName;
                DecipheringRounds.Content = Rounds;
                DecipheringETA.Content = TotalPossibilities;
                DecipheringPercentage.Content = Percentage + "%";

            }));
        }

        private void GenerateTextFile_Click(object sender, RoutedEventArgs e){
            try
            {
                System.IO.File.WriteAllLines(String.Format(@"./Deciphering_{0}.txt", DateTime.Now.ToString("yyyyMMddHHmmss")), new String[]{
                    String.Format("File: {0}", File.FileName),
                    String.Format("To: {0}", File.ContactEmail),
                    String.Format("Algorithm: {0}", File.Algorithm),
                    String.Format("Key Size: {0}", File.KeySize),
                    "------------ ORIGINAL INPUT ------------------------------",
                    String.Format("Ciphered Text: {0}", File.CipheredFileContent),
                    "------------ SERVICE RESPONSE ----------------------------",
                    String.Format("Found Key: {0}", Response.FoundKey),
                    String.Format("Found Email: {0}", Response.FoundEmail),
                    String.Format("Deciphered Text: {0}", Response.DecipheredText),
                    "------------ END RESPONSE --------------------------------",
                });
            } catch(Exception ex)
            {
                FileContentViewer.Text = Response.DecipheredText + Environment.NewLine + String.Format("Impossible de générer le fichier car {0}", ex.Message);
            }
            GenerateTextFile.IsEnabled = false;

        }
    }
}
