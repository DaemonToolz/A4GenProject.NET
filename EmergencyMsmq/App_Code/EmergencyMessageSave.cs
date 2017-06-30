using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Data.Interop;
using System.Threading;
using System.ServiceModel.MsmqIntegration;
using System.Messaging;
using System.IO;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmergencyMessageSave" in code, svc and config file together.
public class EmergencyMessageSave : IEmergencyMessageSave{

    #region Common Queue
    private static readonly String MsmqEndpointAddress = @".\private$\msmq.gen.decipher.recovery";
    private static MessageQueue CommonQueue;

    private static long ClientCount = 0;
    #endregion

    /// <summary>
    /// On forward les messages échoués vers la queue
    /// </summary>
    /// <param name="deciphered"></param>
    [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
    public void ForwardFailedMessage(String deciphered)
    {
        if (CommonQueue == null)
        {
            //Console.WriteLine("No Associated Queue.");
            return;
        }
        try
        {
            //File.WriteAllText(@"C:\inetpub\ProjectLogs\Log_3.slog", deciphered + " " + CommonQueue.AccessMode.ToString() + ":" + CommonQueue.QueueName);
            CommonQueue.Send(deciphered);
           
        } catch(Exception e)
        {
            //File.WriteAllText(@"C:\inetpub\ProjectLogs\Log_4.slog", e.Message);
        }
    }

    /// <summary>
    /// Mode offline - deprecated
    /// </summary>
    /// <param name="deciphered"></param>
    public void ForwardFailedMessageOffline(String deciphered)
    {
        if (CommonQueue == null)
        {
            //Console.WriteLine("No Associated Queue.");
            return;
        }
        try
        {
            //File.WriteAllText(@"C:\inetpub\ProjectLogs\Log_3.slog", deciphered + " " + CommonQueue.AccessMode.ToString() + ":" + CommonQueue.QueueName);
            CommonQueue.Send(deciphered);

        }
        catch (Exception e)
        {
            //File.WriteAllText(@"C:\inetpub\ProjectLogs\Log_4.slog", e.Message);
        }
    }

    /// <summary>
    /// Enregistrement du nouveau client
    /// </summary>
    public void Register()
    {
        try
        {
            if (ClientCount == 0)
            {
                
                if (!MessageQueue.Exists(MsmqEndpointAddress))
                {
                    CommonQueue = MessageQueue.Create(MsmqEndpointAddress);
                }
                else
                {
                    CommonQueue = new MessageQueue(MsmqEndpointAddress);
                    CommonQueue.MessageReadPropertyFilter.Priority = true;
                }
            }
            ClientCount = (Interlocked.Increment(ref ClientCount));
        }
        catch(Exception e)
        {
            //File.WriteAllText(@"C:\inetpub\ProjectLogs\Log_1.slog", e.Message);
            Unregister();
        }
    }

    /// <summary>
    /// Désenregistrement
    /// </summary>
    public void Unregister()
    {
        try
        {
            ClientCount = (Interlocked.Decrement(ref ClientCount));
            if (ClientCount == 0)
            {
                CommonQueue.Close();
            }
        }
        catch (Exception e)
        {
            //File.WriteAllText(@"C:\inetpub\ProjectLogs\Log_2.slog", e.Message);

        }
    }

    /// <summary>
    /// Nettoyage de la queue
    /// </summary>
    public void ClearQueue(){
        if (CommonQueue != null)
            CommonQueue.Purge();
    }
}
