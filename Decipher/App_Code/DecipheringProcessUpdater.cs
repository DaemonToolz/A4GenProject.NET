using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Timers;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DecipheringProcessUpdater" in code, svc and config file together.
[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerSession)]
public class DecipheringProcessUpdater : IDecipheringProcessUpdater{

    private IUpdateDecipherProgress Callback;
    private System.Timers.Timer Timer;
    private String FileName;

    
    void OnTimerElapsed(object sender, ElapsedEventArgs e){

        if (!DecipherService.DecipheringProcess.ContainsKey(FileName))
            Callback.Progress(String.Format("Le fichier demandé n'est pas en cours de déchiffrement", FileName), 0, 0, 0);
        else
            //while (!DecipherService.StopOrder[FileName])
            {
                try
                {
                    Callback.Progress(FileName,
                        DecipherService.DecipheringProcess[FileName]["CurrentIteration"],
                        DecipherService.DecipheringProcess[FileName]["CalculatedIterations"], 
                        DecipherService.DecipheringProcess[FileName]["Percentage"]);
                }
                catch
                {
                    Callback.Progress("Impossible de récupérer la progression", 0, 0, 0);
                }
            }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="FileName"></param>
    public void ShowProgress(string FileName){
        this.FileName = FileName;
        Callback = OperationContext.Current.GetCallbackChannel<IUpdateDecipherProgress>();
        
        Timer = new  System.Timers.Timer(100);
        Timer.Elapsed += OnTimerElapsed;
        Timer.Enabled = true;
        Timer.Start();
    }

    public void Unregister(string FileName){
        Timer.Stop();
        Timer = null;
        
    }
}
