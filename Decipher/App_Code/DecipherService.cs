using Data;
using Data.Interop;
using Decipher.Persistence;
using Encryption.Symmetric.Basic;
using MessageSaverReference;
using RemoteJmsBridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

//using InternalMsmq;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DecipherService" in code, svc and config file together.
[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
public class DecipherService : IOperationDispatcher{
    // Fichier du client, non partagé
    #region 
    private static Boolean PerformOperation = true;
    private static int _FAILED_ATTEMPTS = 0;
    private static bool MSMQ_SWITCHED = false;
    private static Object SharedLock = new object();
    #endregion

    #region Common Data
    private static EmergencyMessageSaveClient   EMSC;
    private static MessagingEndpointClient RemoteBridge;
    private static Dictionary<String, String[]> FileDecryption;
    internal static readonly Dictionary<String, Boolean> StopOrder = new Dictionary<string, bool>();
    internal static readonly Dictionary<String, Dictionary<String,Int32>> DecipheringProcess = new Dictionary<string, Dictionary<String, Int32>>(); 
    #endregion
    
    static DecipherService()
    {
        FileDecryption = new Dictionary<string, string[]>();
        NetTcpBinding ntb = new NetTcpBinding();
        ntb.Security.Mode = SecurityMode.Message;

        ntb.Security.Message = new MessageSecurityOverTcp() { AlgorithmSuite = new Basic256SecurityAlgorithmSuite(), ClientCredentialType = MessageCredentialType.Windows };
        EMSC = new EmergencyMessageSaveClient(ntb, new EndpointAddress("net.tcp://pc-axel:20200/EmergencyMessageSave/EmergencyMessageSave.svc"));

        EMSC.Open();

        try
        {
            //RemoteBridge = null;
            RemoteBridge = new MessagingEndpointClient();
            RemoteBridge.Open();
        }
        catch
        {
            RemoteBridge = null;
        }
    }

    /// <summary>
    /// "Cran d'arrêt" de la fonction de déchiffrement
    /// </summary>
    /// <param name="output"></param>
    public static void StopDeciphering(string deciphered, string filename, string key, string email, Double TrustIndex){
        if (StopOrder.ContainsKey(filename)){
            if (FileDecryption.ContainsKey(filename))
                FileDecryption.Remove(filename);
            if (TrustIndex == null) TrustIndex = 0;
            FileDecryption.Add(filename, new String[5] { deciphered, email, filename, key, TrustIndex.ToString() });
            StopOrder[filename] = true;
        }
    }

    /// <summary>
    /// Transfert d'information vers la JMS
    /// </summary>
    /// <param name="deciphered"></param>
    /// <param name="resetFailures"></param>
    public void TransfertToJMS(String filename, String deciphered, String Key, bool resetFailures = false){
        if (resetFailures) _FAILED_ATTEMPTS = 0;    // Remet à 0 ou non les échecs
        bool failed = true;
        
        //String json = String.Format("{message:\" {0} \"}", deciphered);
        while (failed && _FAILED_ATTEMPTS <= 8){  // 
            try {
                RemoteBridge.messageOperation(Encoding.UTF8.GetString(Encoding.Default.GetBytes(deciphered)), Key, filename);
                failed = false;
            }
            catch {
                failed = true;
                _FAILED_ATTEMPTS++;
            }
        }

        MSMQ_SWITCHED = failed;         // Si échec de l'opération, on passe en mode MSMQ
        if (failed) InaccessibleJMS();      
        else {                          // Remise à 0 de la MSMQ
            AccessibleJMS();
            _FAILED_ATTEMPTS = 0;
        }
    }

    /// <summary>
    /// Si la JMS est inaccessible
    /// On stocke en local
    /// </summary>
    private void InaccessibleJMS(){
        MessageRecovery.CloseServiceHost();
        EMSC.Register();
    }

    /// <summary>
    /// Si la JMS est accessible
    /// On va récupérer les informations stockées en local
    /// puis les envoyer à la JMS
    /// </summary>
    private void AccessibleJMS(){
        EMSC.Unregister();
        MessageRecovery.InitiateServicehost();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="content"></param>
    private void ErrorCase(String content){
        EMSC.ForwardFailedMessage(content);
    }


    /// <summary>
    /// On déchiffre le fichier
    /// </summary>
    /// <returns></returns>
    private String DecipherFile(ClientFile File){
       // String Deciphered;

        try {

            // Si le bridge est indisponible, on switch en local
            if (RemoteBridge == null){
                InaccessibleJMS();
                MSMQ_SWITCHED = true;
            }

            if (File.Algorithm.Contains("XOR"))
                XorDecryption(File, true);
            else
                return String.Format("The Algorithm {0} is not available", File.Algorithm);

            while (!StopOrder[File.FileName]) ;
            
        }
        catch (Exception e) {
            return "A problem occured during the deciphering process : " + e.InnerException;
        }

        try {
            if (FileDecryption.ContainsKey(File.FileName))
            {
                // { deciphered, email, filename, key });
                String deciphered = FileDecryption[File.FileName][0],
                        email = FileDecryption[File.FileName][1],
                        filename = FileDecryption[File.FileName][2],
                        key = FileDecryption[File.FileName][3],
                        trust = FileDecryption[File.FileName][4];

                /*
                Task.Factory.StartNew(() =>{
                    try
                    {*/
                EmailSender.SendEmail(email: File.ContactEmail, TargetIdentity:email, content: deciphered, Filename: filename, TrustIndex: trust +"%", Key: key);
                    /*} catch(Exception e)
                    {
                        //System.IO.File.AppendAllText(@"C:\inetpub\ProjectLogs\test2.txt", e.Message);
                    }
                });*/
                return deciphered + "____" + key + "____" + filename + "____" + email;

            }
            else
                return "No solution has been validated.";
        }
        catch
        {
            return "Could not parse the results";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    public String UpdateFile(ConnectionToken ct){

        try
        {
            String[] TokenData = ct.Data.Cast<String>().ToArray();
          
            ClientFile File = new ClientFile(){
                FileName = TokenData[0],
                CipheredFileContent = TokenData[1],
                ContactEmail = TokenData[2],
                KeyBeginning = TokenData[3] == null ? "" : TokenData[3].Trim(),
                KeySize = Int32.Parse(TokenData[4]),
                Algorithm = TokenData[5],
                KeyEnding = TokenData[6] == null ? "" : TokenData[6].Trim()
            };

            if(!StopOrder.ContainsKey(File.FileName))
                StopOrder.Add(File.FileName, false);

            if(!DecipheringProcess.ContainsKey(File.FileName))
                DecipheringProcess.Add(File.FileName, new Dictionary<string, int>() { { "CalculatedIterations", 0 }, { "CurrentIteration", 0 }, {"Percentage",0 } });

            String deciphered = DecipherFile(File);

            if (DecipheringProcess.ContainsKey(File.FileName))
                DecipheringProcess.Remove(File.FileName);


            if (StopOrder.ContainsKey(File.FileName))
                StopOrder.Remove(File.FileName);

            return deciphered;
        }
        catch(Exception e){



            return String.Format("An error occured while parsing the data {0}", e.Message);
        }
    }

    /// <summary>
    /// Le dispatcher post-dispatch de l'application
    /// Appel la fonction concernée après vérification du token de connection
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    [OperationBehavior(Impersonation = ImpersonationOption.NotAllowed)]
    public ConnectionToken Dispatch(ConnectionToken ct)
    {
        ConnectionToken output = ct;
        try {

            try {
                // To avoid
                // Invocation through Reflection
                // ((typeof(DecipherService).GetMethod(ct.Operation).ReturnType))
                var MethodToInvoke = typeof(DecipherService).GetMethod(ct.Operation);   // On cherche l'information
                if (!MethodToInvoke.IsPublic || MethodToInvoke.IsConstructor ||MethodToInvoke.IsAbstract || MethodToInvoke.IsStatic) // Si la fonction existe mais n'est pas référencée
                    throw new FaultException("Error 403 - Forbidden");

                if (MethodToInvoke.ReturnType == typeof(void))  // Si retour vide
                    throw new FaultException("The function to invoke cannot be void");

                output.Data = new Object[1] { MethodToInvoke.Invoke(this, new Object[1]{ ct }) };   // On retourne l'élément voulu
                
            } catch (Exception e) {
                //if (/*e is ArgumentNullException || e is TargetInvocationException || */e is NullReferenceException)
                //    throw new FaultException("Invocation exception, function " + output.OpName + " does not exist");
                output.Data = new String[1] { String.Format("An error occured: {1} - {0}.", e.Message, e.GetType()) };  // Information user-friendly

            }
            
        }
        catch (Exception e){
            output.Data = new String[2] { "Internal Server Error, Error 500.", e.Message }; // Si autre type d'erreur
            //if (wse.Database.Connection.State.Equals(ConnectionState.Open))
            //    wse.Database.Connection.Close();
        }

        try {
            if (DecipheringProcess.ContainsKey((String)ct.Data[0]))
                DecipheringProcess.Remove((String)ct.Data[0]);

            StopOrder.Remove((String)ct.Data[0]);
        } catch {}

        return output;

    }


    /// <summary>
    /// Déchiffrement du Xor
    /// </summary>
    /// <param name="File"></param>
    /// <param name="FixedSize"></param>
    public void XorDecryption(ClientFile File, bool FixedSize = true){

        // Temporary solution
        String EncryptionCharacters;
        if (File.Algorithm.Contains("alphabetic"))
        {
            if (File.Algorithm.Contains("lower")) EncryptionCharacters = "abcdefghijklmnopqrstuvwxyz";
            else if (File.Algorithm.Contains("upper")) EncryptionCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            else EncryptionCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        }
        else if (File.Algorithm.Contains("alphanumeric"))
        {
            if (File.Algorithm.Contains("lower")) EncryptionCharacters = "abcdefghijklmnopqrstuvwxyz1234567890";
            else if (File.Algorithm.Contains("upper")) EncryptionCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            else EncryptionCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        }
        else
            throw new InvalidOperationException("The provided algorithm does not exist.");

        //
        XorEncryptionExtended extendedDecryption = new XorEncryptionExtended(){
            CipheredText = File.CipheredFileContent,
            Characters = EncryptionCharacters,
            KeyBeginning = File.KeyBeginning,
            MinimumLength = (FixedSize ? File.KeySize - (File.KeyBeginning.Length + File.KeyEnding.Length) : (File.KeyBeginning.Length + File.KeyEnding.Length)),
            MaximumLength = File.KeySize -( File.KeyBeginning.Length + File.KeyEnding.Length )
        };


        Int32 Total = 0;
        lock (SharedLock){
            Total  = DecipheringProcess[File.FileName]["CalculatedIterations"] = (Int32)extendedDecryption.TotalPossibilities();
        }

        int CurrentIteration = 1;

        foreach (string result in extendedDecryption){
            if (StopOrder[File.FileName]) break;

            
            String Deciphered = extendedDecryption.Decipher(result);

            if (!MSMQ_SWITCHED) // First attempt
                TransfertToJMS(File.FileName, Deciphered, result);

            if (MSMQ_SWITCHED) // Then recheck
                ErrorCase(File.FileName + "____" + Deciphered + "____" + result);

            lock (SharedLock)
            {
                DecipheringProcess[File.FileName]["CurrentIteration"] = CurrentIteration++;
                DecipheringProcess[File.FileName]["Percentage"] = (CurrentIteration / Total) * 100;
            }
        }
    }

    // Changer l'emplacement
    public void XorDecryption(ClientFile File, Double MinRange = -1, Double MaxRange = -1) {
        if (MaxRange == -1) MaxRange = XorEncryption.DMaximumLength;
        if (MinRange == -1) MinRange = XorEncryption.DMinimumLength;

        XorEncryption Xor = new XorEncryption() { CipheredText = File.CipheredFileContent };
        foreach (var key in XorEncryption.GenerateKeys(26, MaxRange, MinRange)) {
            if (StopOrder[File.FileName]) break;

            String Deciphered = Xor.Decipher(key);

            if (!MSMQ_SWITCHED) // First attempt
                TransfertToJMS(File.FileName, Deciphered, key);

            if (MSMQ_SWITCHED) // Then recheck
                ErrorCase(File.FileName + "____" + Deciphered + "____" + key);

        }


        #region Reworks

        /*
        XorEncryption Xor = new XorEncryption() { CipheredText = File.CipheredFileContent };
        var ParallelForEachResult = Parallel.ForEach(
            XorEncryption.GenerateKeys(26, XorEncryption.MaximumLength, XorEncryption.MinimumLength),
            new ParallelOptions { MaxDegreeOfParallelism = 6},
            (CurrentKey,State) => {
                ClientFile tempFile = File;
                if (StopOrder[tempFile.FileName])
                    State.Break();

                String Deciphered = Xor.Decipher(CurrentKey);

                if (!MSMQ_SWITCHED) // First attempt
                    TransfertToJMS(tempFile.FileName, Deciphered, CurrentKey);

                if (MSMQ_SWITCHED) // Then recheck
                    ErrorCase(tempFile.FileName + "____" + Deciphered + "____" + CurrentKey);
            }
        );
        */
        #endregion

    }

}
