using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "IMessageRecovery" in code, svc and config file together.
[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.PerCall)]
public class MessageRecovery : IMessageRecovery{
    // Service lié directement
    private DecipherService LinkedService = new DecipherService();

    /// <summary>
    /// On s'occupe des messages stockés
    /// </summary>
    /// <param name="msg"></param>
    public void ProcessMessage(MsmqMessage<string> msg){
        if (_shost != null)
        {
            try
            {
                String[] TranslatedMessage = msg.Body.Split(new string[] { "____" }, StringSplitOptions.None);
                LinkedService.TransfertToJMS(TranslatedMessage[0], TranslatedMessage[1], TranslatedMessage[2], true);
            }
            catch
            {

            }
        }
    }

    /// <summary>
    /// Initialisation du self-host
    /// </summary>
    public static void InitiateServicehost(){
        try
        {
            if(_shost != null)
                _shost = new ServiceHost(typeof(String));
            _shost.Open();
        }
        catch
        {
            _shost = null;
        }
    }

    /// <summary>
    /// Fermeture du service
    /// </summary>
    public static void CloseServiceHost(){
        if(_shost != null)
            _shost.Close();
    }

    private static ServiceHost _shost;

    static MessageRecovery(){
       
    }


}
