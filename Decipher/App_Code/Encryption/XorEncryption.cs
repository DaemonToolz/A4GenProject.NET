using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Encryption.Symmetric.Basic
{
    /// <summary>
    /// Summary description for Caesar
    /// </summary>
    /// 
    [Serializable]
    [Obsolete]
    public class XorEncryption : IBasicDecryption
    {

        public static Double DMaximumLength = 6, DMinimumLength = 5.01203793;
        
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

            while (myNumber > 26) {
                int value = myNumber % 26;

                if (value == 0) {
                    myNumber = myNumber / 26 - 1;
                    array.AddFirst(26);
                }
                else {
                    myNumber /= 26;
                    array.AddFirst(value);
                }
            }

            if (myNumber > 0){
                array.AddFirst(myNumber);
            }

            return new string(array.Select(s => (char)('A' + s - 1)).ToArray()).ToLower();
        }
    }

}