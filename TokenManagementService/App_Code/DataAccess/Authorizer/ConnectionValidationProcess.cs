using Authentication.CustomAuthorizer;
using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IO;
using System.ServiceModel;

namespace Authentication {
    /// <summary>
    /// Summary description for ConnectionValidationProcess
    /// Intecepteur, Identifiant Mot de passe / password custom
    /// </summary>

    internal class ConnectionValidationProcess : UserNamePasswordValidator
    {

        static ConnectionValidationProcess()
        {

        }

        /// <summary>
        /// Valide ou non la connection
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public override void Validate(string userName, string password){
            try {
                if (!UserAccessModel.UserExists(userName, password))
                    throw new FaultException("Either the user or the password is invalid");
            } catch(FaultException fe) {
                throw fe;
            } catch (Exception e) {
                //File.AppendAllText(@"C:\inetpub\ValidationLogger.txt", e.Message);
                throw e;
            }
        }
    }
}

