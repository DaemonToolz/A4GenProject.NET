using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

[ServiceContract(SessionMode = SessionMode.NotAllowed, ProtectionLevel = System.Net.Security.ProtectionLevel.None)]
public interface ITokenValidationService
{


    [OperationContract(IsOneWay = false)]
    Boolean CheckToken(string input, bool forceExisting);
}
