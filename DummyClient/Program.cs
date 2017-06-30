using DummyClient.NetTcpRef;
using DummyClient.RemoteJms2;
using DummyClient.TCPDecipher;
using DummyClient.TokenRemoteReference;
using Microsoft.Office.Interop.Word;
using Novacode;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DummyClient
{
    class Program
    {


        public static readonly List<String> FrenchDictionary = new List<string>();
        static void Main(string[] args)
        {
            /*
            foreach (var word in File.ReadAllLines(@"C:\Users\macie\Documents\PrositsA4\Projets\DaD\liste_francais\liste_francais.txt", Encoding.UTF8))
                FrenchDictionary.Add(word);
            */
            /*
            TokenGenerationServiceClient trc = new TokenGenerationServiceClient();
            trc.ClientCredentials.UserName.UserName = "DefaultAuthorizedUser";
            trc.ClientCredentials.UserName.Password = "defaultpassword";
            String tok;
            Console.WriteLine(tok = trc.GenerateToken());
            */

            try


            {
                //string filename = @"C:\Users\macie\Documents\PrositsA4\Projets\DaD\Template\TemplateRapportConfianceTemp.docx";
                /*
                using (DocX document = DocX.Load(filename))
                {
                    document.ReplaceText("%SenderEmail%", "Daemontoolz62@gmail.com");
                    document.ReplaceText("%ProvidedEmail%", "axel.maciejewski@viacesi.fr");
                    document.ReplaceText("%TestedFile%", @"C:\Users\macie\Documents\PrositsA4\Projets\DaD\ToDecrypt\A4DemoEncrypted.txt");
                    document.ReplaceText("%DecipheredText%", "Les framboises sont perchées sur le tabouret de mon grand-père. kiwipgm@gmail.com");
                    document.ReplaceText("%CipheringKey%", "aaaaah");
                    document.ReplaceText("%TrustIndex%", "60%");

                    document.SaveAs(@"C:\Users\macie\Documents\PrositsA4\Projets\DaD\Template\TemplateRapportConfianceTempNew.docx");
                }
                */
                /*

                Dictionary<String, String> changes = new Dictionary<string, string>();
                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application() { Visible = false };
                var Document = word.Documents.Open(filename, Visible:false, ReadOnly : false);
                Document.Activate();

                changes.Add("%SenderEmail%", "Daemontoolz62@gmail.com");
                changes.Add("%ProvidedEmail%", "axel.maciejewski@viacesi.fr");
                changes.Add("%TestedFile%", @"C:\Users\macie\Documents\PrositsA4\Projets\DaD\ToDecrypt\A4DemoEncrypted.txt");
                changes.Add("%DecipheredText%", "Les framboises sont perchées sur le tabouret de mon grand-père. kiwipgm@gmail.com");
                changes.Add("%CipheringKey%", "aaaaah");
                changes.Add("%TrustIndex%", "60%");

                foreach(var key in changes.Keys)
                    word.Selection.Find.Execute(Replace: WdReplace.wdReplaceAll, FindText: key, ReplaceWith: changes[key]);

                word.Selection.Find.Execute(Replace: WdReplace.wdReplaceAll, FindText: "%SenderEmail%", ReplaceWith: "Daemontoolz62@gmail.com");
                word.Selection.Find.Execute(Replace: WdReplace.wdReplaceAll, FindText: "%ProvidedEmail%", ReplaceWith: "axel.maciejewski@viacesi.fr");
                word.Selection.Find.Execute(Replace: WdReplace.wdReplaceAll, FindText: "%TestedFile%", ReplaceWith: @"C:\Users\macie\Documents\PrositsA4\Projets\DaD\ToDecrypt\A4DemoEncrypted.txt");
                word.Selection.Find.Execute(Replace: WdReplace.wdReplaceAll, FindText: "%DecipheredText%", ReplaceWith: "Les framboises sont perchées sur le tabouret de mon grand-père. kiwipgm@gmail.com");
                word.Selection.Find.Execute(Replace: WdReplace.wdReplaceAll, FindText: "%CipheringKey%", ReplaceWith: "aaaaah");
                word.Selection.Find.Execute(Replace: WdReplace.wdReplaceAll, FindText: "%TrustIndex%", ReplaceWith: "60%");

                
                Document.ExportAsFixedFormat(
                            filename.Replace(".docx", ".pdf"),
                            Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF,
                            OptimizeFor: Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForOnScreen,
                            BitmapMissingFonts: true, DocStructureTags: false);

                foreach (var key in changes.Keys)
                    word.Selection.Find.Execute(Replace: WdReplace.wdReplaceAll, FindText: changes[key], ReplaceWith: key );

                Document.Save();
                Document.Close();
                */
                
                XorEncryption Xor = new XorEncryption() { CipheredText = File.ReadAllText(@"C:\Users\macie\Documents\PrositsA4\Projets\DaD\ToDecrypt\Decipher\P1FDecrypted.txt", Encoding.UTF8) };
                //File.WriteAllText(@"C:\Users\macie\Documents\PrositsA4\Projets\DaD\ToDecrypt\Decipher\P1F.txt", Xor.CipheredText);

                String encrypted = Xor.Decipher("fjuiop");
                File.WriteAllText(@"C:\Users\macie\Documents\PrositsA4\Projets\DaD\ToDecrypt\P1FEncrypted.txt", encrypted);

                Decrypt(File.ReadAllText(@"C:\Users\macie\Documents\PrositsA4\Projets\DaD\ToDecrypt\P1FEncrypted.txt", Encoding.UTF8), "fjuiop");
                

                //File.AppendAllText(@"C:\inetpub\ProjectLogs\DecipherTreatmentLog.txt", obj + Environment.NewLine);
                /*
                TokenValidationServiceClient tvc = new TokenValidationServiceClient();
                Console.WriteLine(tvc.CheckToken(tok, true));

                OperationDispatcherClient odc;
        
                odc = new OperationDispatcherClient("NetTcpBinding_IOperationDispatcher");

                ConnectionToken ct = new ConnectionToken() {
                    UserToken = tok,
                    Operation = "UpdateFile",
                    AppToken = "78de44f-880a5dccb-DS",
                    Data = new Object[2] { "MyFile.txt","MySTRIIIIIING" } };
               */
                /*
                 Stopwatch t = new Stopwatch();
                 t.Start();
                 HyperThreadedDecryption(File.ReadAllText(@"C:\Users\macie\Documents\PrositsA4\Projets\DaD\ToDecrypt\P1A.txt", Encoding.UTF8));
                 t.Stop();
                 TimeSpan ts = t.Elapsed;

                 Console.WriteLine(String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10));
                 */
                //XorEncryption Xor = new XorEncryption() { CipheredText = File.ReadAllText(@"C:\Users\macie\Documents\PrositsA4\Projets\DaD\ToDecrypt\P1E.txt", Encoding.UTF8) };

                //Console.WriteLine(Xor.Decipher("genjms"));
                /*
                MessagingEndpointClient mec = new MessagingEndpointClient();
                mec.Open();
                int limit = 1000000;
                XorEncryption Xor = new XorEncryption() { CipheredText = File.ReadAllText(@"C:\Users\macie\Documents\PrositsA4\Projets\DaD\ToDecrypt\P1E.txt", Encoding.UTF8) };
                foreach (var key in XorEncryption.GenerateKeys(26, XorEncryption.MaximumLength, XorEncryption.MinimumLength))
                {
                    if (limit-- <= 0) break;
                    //{ message,key,nameFile}
                    String Deciphered = Xor.Decipher(key);
                    mec.messageOperation(Deciphered, key, @"C:\Users\macie\Documents\PrositsA4\Projets\DaD\ToDecrypt\P1E.txt");
                }
                mec.Close();
                */
                //ServiceStopper.JmsBridgeClient jbc = new ServiceStopper.JmsBridgeClient("BasicHttpBinding_IJmsBridge");
                /*
                //var Runner = Task.Run(() => {
                    var operation = odc.Dispatch(ct);

                    //TCP(ct);
                    foreach (var obj in operation.Data)
                        Console.WriteLine(obj.ToString());
                //});
                
                //Thread.Sleep(8000);
                */
                //jbc.StopDeciphering("DecipheredString", @"C:\Users\macie\Documents\PrositsA4\Projets\DaD\ToDecrypt\P1A.txt", "MySTRIIIIIING", "aaaccc");
                //jbc.StopDeciphering("DecipheredString", @"C:\Users\macie\Documents\PrositsA4\Projets\DaD\ToDecrypt\P1B.txt", "MySTRIIIIIING", "bbbbbb");

                //Runner.Wait();
                //odc.Close();
                /*
                XorEncryptionEvolved xee = new XorEncryptionEvolved() { MinimumLength = 5, MaximumLength = 5, Characters = "abcdefghijklmnopqrstuvwxyz1234567890" };
                foreach (string result in xee){
                    Console.WriteLine(result);
                }
                */
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
            }

            /*
            NetTcpBinding ntb = new NetTcpBinding();
            ntb.Security.Mode = SecurityMode.Message;

            ntb.Security.Message = new MessageSecurityOverTcp() { AlgorithmSuite = new Basic256SecurityAlgorithmSuite(), ClientCredentialType = MessageCredentialType.Windows };

            
            EmergencyMessageSaveClient emsc = new EmergencyMessageSaveClient(ntb, new EndpointAddress("net.tcp://pc-axel:20200/EmergencyMessageSave/EmergencyMessageSave.svc"));
            emsc.Open();
            Console.WriteLine(emsc.State);
            Console.ReadKey();
            emsc.Register();
            
            emsc.ForwardFailedMessageOffline("Deciphered message 3");
            
            Console.ReadKey();
            emsc.Unregister();
            emsc.Close();
            */
            Console.ReadKey();
        }
        
        private static void DecryptNew(String content)
        {

            XorEncryption Xor = new XorEncryption() { CipheredText = content };
            Parallel.ForEach(
                XorEncryption.GenerateKeys(26, XorEncryption.MaximumLength, XorEncryption.MinimumLength),
                new ParallelOptions { MaxDegreeOfParallelism = 10 },
                (CurrentKey, State) =>
                {
                    //if (StopOrder[File.FileName]) State.Break();
                    Xor.Decipher(CurrentKey);
                    //Console.WriteLine(Task.CurrentId + "," + CurrentKey + " : " + Xor.Decipher(CurrentKey));
                    //System.IO.File.AppendAllText(@"C:\inetpub\ProjectLogs\Output_" + Task.CurrentId + ".txt", CurrentKey + "  " + Deciphered + "" + Environment.NewLine);
                }
            );
        }



        private static void Decrypt(String content, String awaitedKey = "")
        {
            
            XorEncryption Xor = new XorEncryption() { CipheredText = content };
            foreach (var key in XorEncryption.GenerateKeys(26, XorEncryption.MaximumLength, XorEncryption.MinimumLength)){
                String dec = Xor.Decipher(key);
                Console.WriteLine(dec);
                if(key == awaitedKey){
                    Console.WriteLine(key + " " + awaitedKey + " " + content + " / " + dec);
                    break;
                }
            }
        }
        public class XorEncryption
        {

            public static Double MaximumLength = 6, MinimumLength = 5.01203793;

            public String CipheredText { get; set; }

            public XorEncryption() { }

            // 97 - 122 includes
            /// <summary>
            /// Décrypter
            /// </summary>
            /// <param name="Key"></param>
            /// <returns></returns>
            public String Decipher(String Key)
            {
                String Deciphered = String.Empty;
                for (int i = 0; i < CipheredText.Length; ++i)
                    Deciphered += (char)(CipheredText[i] ^ Key[i % Key.Length]);
                return Deciphered;
            }

            public static IEnumerable<String> GenerateKeys(int range, Double Limit, Double Floor = 0)
            {
                int count = 0;
                for (var i = Limit; i > Floor; i--)
                {
                    count += (int)Math.Pow(range, i);
                }

                for (var i = Math.Pow(range, Floor); i <= Math.Pow(range, Limit); i++)
                {
                    yield return ToBase26((int)i);
                }

            }

            private static string ToBase26(int myNumber)
            {
                var array = new LinkedList<int>();

                while (myNumber > 26)
                {
                    int value = myNumber % 26;

                    if (value == 0)
                    {
                        myNumber = myNumber / 26 - 1;
                        array.AddFirst(26);
                    }
                    else
                    {
                        myNumber /= 26;
                        array.AddFirst(value);
                    }
                }

                if (myNumber > 0)
                {
                    array.AddFirst(myNumber);
                }

                return new string(array.Select(s => (char)('A' + s - 1)).ToArray()).ToLower();
            }
        }


         public class XorEncryptionEvolved{
            private StringBuilder sb = new StringBuilder();
            //the string we want to permutate 
            public string Characters { get; set; }
            public ulong Length { get; private set; }

            public int MaximumLength { get; set; }
            public int MinimumLength { get; set; }


            public System.Collections.IEnumerator GetEnumerator()
            {
                Length = (ulong)this.Characters.Length;
                for (double x = MinimumLength; x <= MaximumLength; x++){
                    ulong total = (ulong)Math.Pow((double)Characters.Length, (double)x);
                    ulong counter = 0;
                    while (counter < total){
                        string a = Factoradic(counter, x - 1);
                        yield return a;
                        counter++;
                    }
                }
            }

            private string Factoradic(ulong l, double power){
                sb.Length = 0;
                while (power >= 0){
                    sb = sb.Append(this.Characters[(int)(l % Length)]);
                    l /= Length;
                    power--;
                }
                return sb.ToString();
            }

        }

      

    }
}
