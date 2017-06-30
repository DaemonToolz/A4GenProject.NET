using Data.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmergencyMessageSave" in both code and config file together.
[ServiceContract]
public interface IEmergencyMessageSave{
    [OperationContract(IsOneWay = true)]
    void ForwardFailedMessage(String deciphered);

    [OperationContract(IsOneWay = true)]

    void ForwardFailedMessageOffline(String deciphered);

    [OperationContract(IsOneWay = true)]
    void Register();

    [OperationContract(IsOneWay = true)]
    void Unregister();

    [OperationContract(IsOneWay = true)]
    void ClearQueue();
}
