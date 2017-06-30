using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Decipher.Persistence
{
    /// <summary>
    /// Représente le fichier client
    /// </summary>
    public class ClientFile {
        /// <summary>
        /// Nom du fichier
        /// </summary>
        public String FileName { get; internal set; }
        /// <summary>
        /// Contenu (chiffré par défaut)
        /// </summary>
        public String CipheredFileContent { get; internal set; }

        public String Algorithm { get; internal set; }
        public String ContactEmail { get; internal set; }
        public Int32 KeySize { get; internal set; }
        public String KeyBeginning { get; internal set; }
        public String KeyEnding { get; internal set; }

    }
}