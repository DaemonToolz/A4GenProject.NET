﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DummyClient.TokenRemoteReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TokenRemoteReference.ITokenGenerationService", SessionMode=System.ServiceModel.SessionMode.NotAllowed)]
    public interface ITokenGenerationService {
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.None, Action="http://tempuri.org/ITokenGenerationService/GenerateToken", ReplyAction="http://tempuri.org/ITokenGenerationService/GenerateTokenResponse")]
        string GenerateToken();
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.None, Action="http://tempuri.org/ITokenGenerationService/GenerateToken", ReplyAction="http://tempuri.org/ITokenGenerationService/GenerateTokenResponse")]
        System.Threading.Tasks.Task<string> GenerateTokenAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITokenGenerationServiceChannel : DummyClient.TokenRemoteReference.ITokenGenerationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TokenGenerationServiceClient : System.ServiceModel.ClientBase<DummyClient.TokenRemoteReference.ITokenGenerationService>, DummyClient.TokenRemoteReference.ITokenGenerationService {
        
        public TokenGenerationServiceClient() {
        }
        
        public TokenGenerationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TokenGenerationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TokenGenerationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TokenGenerationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GenerateToken() {
            return base.Channel.GenerateToken();
        }
        
        public System.Threading.Tasks.Task<string> GenerateTokenAsync() {
            return base.Channel.GenerateTokenAsync();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TokenRemoteReference.ITokenValidationService", SessionMode=System.ServiceModel.SessionMode.NotAllowed)]
    public interface ITokenValidationService {
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.None, Action="http://tempuri.org/ITokenValidationService/CheckToken", ReplyAction="http://tempuri.org/ITokenValidationService/CheckTokenResponse")]
        bool CheckToken(string input, bool forceExisting);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.None, Action="http://tempuri.org/ITokenValidationService/CheckToken", ReplyAction="http://tempuri.org/ITokenValidationService/CheckTokenResponse")]
        System.Threading.Tasks.Task<bool> CheckTokenAsync(string input, bool forceExisting);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITokenValidationServiceChannel : DummyClient.TokenRemoteReference.ITokenValidationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TokenValidationServiceClient : System.ServiceModel.ClientBase<DummyClient.TokenRemoteReference.ITokenValidationService>, DummyClient.TokenRemoteReference.ITokenValidationService {
        
        public TokenValidationServiceClient() {
        }
        
        public TokenValidationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TokenValidationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TokenValidationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TokenValidationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool CheckToken(string input, bool forceExisting) {
            return base.Channel.CheckToken(input, forceExisting);
        }
        
        public System.Threading.Tasks.Task<bool> CheckTokenAsync(string input, bool forceExisting) {
            return base.Channel.CheckTokenAsync(input, forceExisting);
        }
    }
}
