using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDecipheringProcessUpdater" in both code and config file together.

[ServiceContract(CallbackContract = typeof(IUpdateDecipherProgress), SessionMode = SessionMode.Required)]
public interface IDecipheringProcessUpdater{
    
    [OperationContract(/*IsInitiating = true, IsTerminating =false, */IsOneWay = true)]
    void ShowProgress(String FileName);
    /*

    [OperationContract(IsInitiating = false, IsTerminating = true, IsOneWay = true)]
    void Unregister(String FileName);
    */
}


public interface IUpdateDecipherProgress{
    [OperationContract(IsOneWay = true)]
    void Progress(String FileName, Decimal Rounds, Decimal TotalPossibilities, Decimal Percentage);
}