using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ApplicationReferencesUtil
/// </summary>
namespace Security.Validation
{
    /// <summary>
    /// Utilitaire
    /// </summary>
    public static class ApplicationReferencesUtil
    {
        /// <summary>
        /// Map les tokens de chaque services / applications 
        /// Peut évoluer et donner des conditions d'accès en fonction du Token spécifié
        /// </summary>
        internal static readonly Dictionary<Type, String> ServiceTokens = new Dictionary<Type, string>();
        static ApplicationReferencesUtil(){
            ServiceTokens.Add(typeof(DecipherService), "78de44f-880a5dccb-DS");
        }

    }
}