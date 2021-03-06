﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DummyClient.ServiceStopper {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceStopper.IJmsBridge")]
    public interface IJmsBridge {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJmsBridge/StopDeciphering")]
        void StopDeciphering(string output, string filename, string content, string result);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJmsBridge/StopDeciphering")]
        System.Threading.Tasks.Task StopDecipheringAsync(string output, string filename, string content, string result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IJmsBridgeChannel : DummyClient.ServiceStopper.IJmsBridge, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class JmsBridgeClient : System.ServiceModel.ClientBase<DummyClient.ServiceStopper.IJmsBridge>, DummyClient.ServiceStopper.IJmsBridge {
        
        public JmsBridgeClient() {
        }
        
        public JmsBridgeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public JmsBridgeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public JmsBridgeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public JmsBridgeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void StopDeciphering(string output, string filename, string content, string result) {
            base.Channel.StopDeciphering(output, filename, content, result);
        }
        
        public System.Threading.Tasks.Task StopDecipheringAsync(string output, string filename, string content, string result) {
            return base.Channel.StopDecipheringAsync(output, filename, content, result);
        }
    }
}
