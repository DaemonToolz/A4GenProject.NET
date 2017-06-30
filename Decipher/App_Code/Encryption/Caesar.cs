using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Encryption.Symmetric.Basic {
    /// <summary>
    /// Summary description for Caesar
    /// </summary>
    public class Caesar : IBasicDecryption{
        public String CipheredText { get; set; }

        public string InitialKey { get; set; }

        public Caesar() {

        }

        /// <summary>
        /// Décrypter
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public String Decipher(String Key) {
            String Deciphered = String.Empty;
            char InitialShift;
            for (int i = 0; i < CipheredText.Length; ++i) {
                InitialShift = char.IsUpper(CipheredText[i]) ? 'A' : 'a';
                Deciphered += (char)((((CipheredText[i] + Key[0]) - InitialShift) % 26) + InitialShift);
            }
            return Deciphered;
        }

    }
}