
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Encryption.Symmetric.Basic
{
    /// <summary>
    /// Summary description for XorEncryptionEvolved
    /// </summary>
    [Serializable]
    public class XorEncryptionExtended : ICloneable
    {
        private StringBuilder sb = new StringBuilder();
        //the string we want to permutate 
        public string Characters { get; set; }
        public ulong Length { get; private set; }
        public String CipheredText { get; set; }
        public Int32 MinimumLength { get; set; }
        public Int32 MaximumLength { get; set; }
        public String KeyBeginning { get; set; }

        public System.Collections.IEnumerator GetEnumerator()
        {
            if (KeyBeginning.Equals(MaximumLength))
            {
                yield return KeyBeginning;
            }
            else{
                Length = (ulong)this.Characters.Length;
                for (double x = MinimumLength; x <= MaximumLength; x++)
                {
                    ulong total = (ulong)Math.Pow((double)Characters.Length, (double)x);
                    ulong counter = 0;
                    while (counter < total)
                    {
                        string a = (KeyBeginning != null ? KeyBeginning : "") + Factoradic(counter, x - 1);
                        yield return a;
                        counter++;
                    }
                }
            }
        }

        public static IEnumerable<String> GenerateKeys(XorEncryptionExtended xee)
        {
            XorEncryptionExtended DeepCopy = new XorEncryptionExtended()
            {
                CipheredText = xee.CipheredText,
                Characters = xee.Characters,
                KeyBeginning = xee.KeyBeginning,
                MaximumLength = xee.MaximumLength,
                MinimumLength = xee.MinimumLength
            };

            foreach (String key in DeepCopy)
                yield return key;
        }

        private string Factoradic(ulong l, double power)
        {
            sb.Length = 0;
            while (power >= 0)
            {
                sb = sb.Append(this.Characters[(int)(l % Length)]);
                l /= Length;
                power--;
            }
            return sb.ToString();
        }

        public object Clone(){
            return this.MemberwiseClone();
        }

        public String Decipher(String Key)
        {
            String Deciphered = String.Empty;
            for (int i = 0; i < CipheredText.Length; ++i)
                Deciphered += (char)(CipheredText[i] ^ Key[i % Key.Length]);
            return Deciphered;
        }
    }
}