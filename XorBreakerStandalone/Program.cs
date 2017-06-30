using Encryption.Symmetric.Basic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XorBreakerStandalone{
    class Program{
        static void Main(string[] args){
            /*
            Console.WriteLine("Entrez le numéro de phase ainsi que la lettre du fichier (A -> G)");
            String FileName = "P"+Console.ReadLine()+".txt";
            */
            Console.WriteLine("Entrez le début de la clé ");
            String KeyBeginning = Console.ReadLine();

            Console.WriteLine("Entrez la taille de la clé");
            Int32 KeySize = (Int32.Parse(Console.ReadLine()));

            Console.WriteLine("Entrez le type de chiffrement (alpha ou mix)");
            String CipheringType = Console.ReadLine();

            Console.WriteLine("Encodage (UTF8, ASCII, DEFAULT)");
            String EncodingString = Console.ReadLine();


            Console.WriteLine("Nombre de match minimum afin de générer une sortie fichier");
            Int32 MinimumMatches = Int32.Parse(Console.ReadLine());

            Encoding EncodingVar;
            if (EncodingString.Contains("UTF8"))
                EncodingVar = Encoding.UTF8;
            if (EncodingString.Contains("ASCII"))
                EncodingVar = Encoding.ASCII;
            else EncodingVar = Encoding.Default;

            /*
            XorEncryptionExtended extendedDecryption = new XorEncryptionExtended()
            {
                CipheredText = File.ReadAllText(FileName, EncodingVar),
                Characters = CipheringType.Contains("alpha") ? "abcdefghijklmnopqrstuvwxyz" : "abcdefghijklmnopqrstuvwxyz1234567890",
                KeyBeginning = KeyBeginning,
                MinimumLength = (KeySize - KeyBeginning.Length),
                MaximumLength = KeySize - KeyBeginning.Length
            };
            */
            String AvailableFiles = "BK";
            foreach (var file in AvailableFiles)
            {
                XorEncryptionExtended extendedDecryption = new XorEncryptionExtended()
                {
                    CipheredText = File.ReadAllText("P2"+file+"B.txt", EncodingVar),
                    Characters = CipheringType.Contains("alpha") ? "abcdefghijklmnopqrstuvwxyz" : "abcdefghijklmnopqrstuvwxyz1234567890",
                    KeyBeginning = KeyBeginning,
                    MinimumLength = (KeySize - KeyBeginning.Length),
                    MaximumLength = KeySize - KeyBeginning.Length
                };

                HashSet<String> FrenchDictionary = new HashSet<string>();
                foreach (var line in File.ReadAllLines("liste_francais.txt"))
                    FrenchDictionary.Add(line);

                int matches = 0;
                foreach (var key in XorEncryptionExtended.GenerateKeys(extendedDecryption))
                {
                    String deciphered = extendedDecryption.Decipher(key + "67");
                    matches = 0;
                    foreach (var HashedWord in FrenchDictionary)
                        if (deciphered.Contains(HashedWord))
                            matches++;

                    if (matches >= MinimumMatches)
                        File.AppendAllText(@"DecryptionOutputP2" + file + "B.txt", matches + " - " + key + "67" + " " + deciphered + Environment.NewLine + Environment.NewLine);

                }
            }

    
        }
    }
}
