using Data.Interop;
using Security.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;
using System.Web;
using TokenManager;

namespace Security{
    /// <summary>
    /// Inspecte les attributs fournis à la fonction
    /// </summary>
    public class AttributeInspector : Attribute, IParameterInspector, IOperationBehavior
    {
        /// <summary>
        /// Référence vers le service des tokens
        /// </summary>
        private static TokenValidationServiceClient TokenValidator;

        private static Dictionary<String, String> ConnectedClients = new Dictionary<string, string>();

        static AttributeInspector()
        {

            // Avoid in production
            // Better use a REAL certificate
            TokenValidator = new TokenValidationServiceClient(); // Nouveau client
            TokenValidator.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None; // Évite les problèmes de négociations en cas de certificats self-signed
        }
        public AttributeInspector()
        {

        }

        /// <summary>
        /// Non utilisé
        /// </summary>
        /// <param name="operationDescription"></param>
        /// <param name="bindingParameters"></param>
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {

        }

        /// <summary>
        /// Non utilisé
        /// </summary>
        /// <param name="operationDescription"></param>
        /// <param name="clientOperation"></param>
        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {

        }

        /// <summary>
        /// Permet d'ajouter au dispatcher des opérations (e.g. géré par le serveur) uun vérificateur
        /// 
        /// </summary>
        /// <param name="operationDescription"> Description de l'opération appelée </param>
        /// <param name="dispatchOperation"> Opération à invoquer </param>
        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.ParameterInspectors.Add(this);
        }

        /// <summary>
        /// Validation 
        /// </summary>
        /// <param name="operationDescription"></param>
        public void Validate(OperationDescription operationDescription)
        {

        }


        /// <summary>
        /// Appelé après l'appel de la fonctipn
        /// </summary>
        /// <param name="operationName"> Nom de l'opération invoquée </param>
        /// <param name="outputs"> Sorties </param>
        /// <param name="returnValue"> Variables de retour</param>
        /// <param name="correlationState"> </param>
        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            try {
                ConnectionToken translated = (ConnectionToken)outputs[0];
                ConnectedClients.Remove(translated.UserToken);
            }
            catch {

            }
        }

        /// <summary>
        /// Appelé avant l'invocation
        /// </summary>
        /// <param name="operationName"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public object BeforeCall(string operationName, object[] inputs)
        {

           
            //Trace.Write("Interceptor called");
            // Si aucune entrée
            if (inputs == null || inputs.Length == 0)
                throw new FaultException<InputValidationFaultContract>(new InputValidationFaultContract() { Error = "Inputs are required" });

            // Si plusieurs inputs
            if (inputs.Count() > 1)
                throw new FaultException<InputValidationFaultContract>(new InputValidationFaultContract() { Error = "Only one input is authorized" });

            // Si entrée nulle
            if (inputs[0] == null)
                throw new FaultException<InputValidationFaultContract>(new InputValidationFaultContract() { Error = "Null inputs are not authorized" });

            // Récupération du token d'application
            ConnectionToken translated = (ConnectionToken)inputs[0];

            // Si token invalid
            if (!TokenValidator.CheckToken(translated.UserToken, true))
                throw new FaultException<InputValidationFaultContract>(new InputValidationFaultContract() { Error = "Invalid user token" });

            Type ResearchedOperation = null; // On va rechercher l'opération invoquée
            try {
                // On recherche l'opération invoquée
                ResearchedOperation = ApplicationReferencesUtil.ServiceTokens.Keys.First(obj =>
                {
                    try { obj.GetMethod(operationName); return true; }
                    catch { return false; }      // Means no operation with that name
                });
            }
            catch
            {
                // Exception si l'opération n'est pas trouvée
                throw new FaultException<InputValidationFaultContract>(new InputValidationFaultContract() { Error = "No services store that function" });
            }

            //if (ApplicationReferencesUtil.ServiceTokens.ContainsValue(translated.AppToken)) {
            // Si le token n'est pas valide
            if (!ApplicationReferencesUtil.ServiceTokens[ResearchedOperation].Equals(translated.AppToken)) {
                throw new FaultException<InputValidationFaultContract>(new InputValidationFaultContract() { Error = "The Application Tokens do not match" });
            }
            //}
            //else
            //    throw new FaultException<InputValidationFaultContract>(new InputValidationFaultContract() { Error = "The token does not exist" });

            ConnectedClients.Add(translated.UserToken, translated.Operation);
            return null;
        }
    }
}
