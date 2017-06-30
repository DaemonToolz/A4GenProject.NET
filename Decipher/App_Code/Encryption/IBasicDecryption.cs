using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Encryption.Symmetric.Basic
{

    /// <summary>
    /// Summary description for IBasicDecryption
    /// </summary>
    /// 

    
    public interface IBasicDecryption {
        String CipheredText { get; set; }

        String Decipher(String key);
    }
}