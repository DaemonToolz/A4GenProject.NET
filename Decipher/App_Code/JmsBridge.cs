using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "JmsBridge" in code, svc and config file together.
[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
public class JmsBridge : IJmsBridge{

    public void StopDeciphering(string deciphered, string filename, string key, string email, Double TrustIndex){

        DecipherService.StopDeciphering(deciphered, filename, key, email, TrustIndex);
    }

}
