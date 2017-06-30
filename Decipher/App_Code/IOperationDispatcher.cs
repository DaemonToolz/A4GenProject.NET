using Data.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Dispatcher;
using Security;
using Security.Validation;
// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDecipherService" in both code and config file together.
[ServiceContract]
public interface IOperationDispatcher
{
    [OperationContract(IsOneWay = false)]
    [FaultContract(typeof(InputValidationFaultContract))]
    [AttributeInspector]

    ConnectionToken Dispatch(ConnectionToken ct);

    
}
