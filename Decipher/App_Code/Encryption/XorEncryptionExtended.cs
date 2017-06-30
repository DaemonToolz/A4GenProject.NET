using Nuclex.Support.Cloning;
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
    public class XorEncryptionExtended : XorEncryption, ICloneable
    {
        private StringBuilder sb = new StringBuilder();
        //the string we want to permutate 
        public string Characters { get; set; }
        public ulong Length { get; private set; }

        public Int32 MinimumLength { get; set; }
        public Int32 MaximumLength { get; set; }
        public String KeyBeginning { get; set; }
        public String KeyEnding { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            if (((KeyBeginning != null? KeyBeginning.Length : 0) + (KeyEnding != null ? KeyEnding.Length : 0)).Equals(MaximumLength))
            {
                yield return (KeyBeginning ?? "")+(KeyEnding ?? "") ;
            }
            else{
                Length = (ulong)this.Characters.Length;
                for (double x = MinimumLength; x <= MaximumLength; x++)
                {
                    ulong total = (ulong)Math.Pow((double)Characters.Length, (double)x);
                    ulong counter = 0;
                    while (counter < total)
                    {
                        string a = (KeyBeginning ?? "") + Factoradic(counter, x - 1) + (KeyEnding ?? "");
                        yield return a;
                        counter++;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ulong TotalPossibilities()
        {
            ulong total = 0;
            for (double x = MinimumLength; x <= MaximumLength; x++)
                total  = (ulong)Math.Pow((double)Characters.Length, (double)x);
            return total;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xee"></param>
        /// <returns></returns>
        public static IEnumerable<String> GenerateKeys(XorEncryptionExtended xee)
        {
            XorEncryptionExtended DeepCopy = ExpressionTreeCloner.DeepFieldClone(xee);
            foreach (String key in DeepCopy)
                yield return key;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        /// <param name="power"></param>
        /// <returns></returns>
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
    }
}