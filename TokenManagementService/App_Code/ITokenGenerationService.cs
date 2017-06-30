using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

[ServiceContract(SessionMode = SessionMode.NotAllowed, ProtectionLevel = System.Net.Security.ProtectionLevel.None)]
public interface ITokenGenerationService
{
    [OperationContract(IsOneWay = false)]
    //[WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "token/generate")]
    String GenerateToken();
}
