﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DummyClient.TCPDecipher {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ConnectionToken", Namespace="http://schemas.datacontract.org/2004/07/Data.Interop")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(object[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(DummyClient.TCPDecipher.InputValidationFaultContract))]
    public partial class ConnectionToken : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string AppTokenField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private object[] DataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string InfosField;
        
        private string OperationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool StatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserTokenField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string AppToken {
            get {
                return this.AppTokenField;
            }
            set {
                if ((object.ReferenceEquals(this.AppTokenField, value) != true)) {
                    this.AppTokenField = value;
                    this.RaisePropertyChanged("AppToken");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public object[] Data {
            get {
                return this.DataField;
            }
            set {
                if ((object.ReferenceEquals(this.DataField, value) != true)) {
                    this.DataField = value;
                    this.RaisePropertyChanged("Data");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Infos {
            get {
                return this.InfosField;
            }
            set {
                if ((object.ReferenceEquals(this.InfosField, value) != true)) {
                    this.InfosField = value;
                    this.RaisePropertyChanged("Infos");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string Operation {
            get {
                return this.OperationField;
            }
            set {
                if ((object.ReferenceEquals(this.OperationField, value) != true)) {
                    this.OperationField = value;
                    this.RaisePropertyChanged("Operation");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Status {
            get {
                return this.StatusField;
            }
            set {
                if ((this.StatusField.Equals(value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserToken {
            get {
                return this.UserTokenField;
            }
            set {
                if ((object.ReferenceEquals(this.UserTokenField, value) != true)) {
                    this.UserTokenField = value;
                    this.RaisePropertyChanged("UserToken");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="InputValidationFaultContract", Namespace="http://schemas.datacontract.org/2004/07/Security.Validation")]
    [System.SerializableAttribute()]
    public partial class InputValidationFaultContract : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Error {
            get {
                return this.ErrorField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorField, value) != true)) {
                    this.ErrorField = value;
                    this.RaisePropertyChanged("Error");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TCPDecipher.IOperationDispatcher")]
    public interface IOperationDispatcher {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOperationDispatcher/Dispatch", ReplyAction="http://tempuri.org/IOperationDispatcher/DispatchResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(DummyClient.TCPDecipher.InputValidationFaultContract), Action="http://tempuri.org/IOperationDispatcher/DispatchInputValidationFaultContractFault" +
            "", Name="InputValidationFaultContract", Namespace="http://schemas.datacontract.org/2004/07/Security.Validation")]
        DummyClient.TCPDecipher.ConnectionToken Dispatch(DummyClient.TCPDecipher.ConnectionToken ct);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOperationDispatcher/Dispatch", ReplyAction="http://tempuri.org/IOperationDispatcher/DispatchResponse")]
        System.Threading.Tasks.Task<DummyClient.TCPDecipher.ConnectionToken> DispatchAsync(DummyClient.TCPDecipher.ConnectionToken ct);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOperationDispatcherChannel : DummyClient.TCPDecipher.IOperationDispatcher, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OperationDispatcherClient : System.ServiceModel.ClientBase<DummyClient.TCPDecipher.IOperationDispatcher>, DummyClient.TCPDecipher.IOperationDispatcher {
        
        public OperationDispatcherClient() {
        }
        
        public OperationDispatcherClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OperationDispatcherClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OperationDispatcherClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OperationDispatcherClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public DummyClient.TCPDecipher.ConnectionToken Dispatch(DummyClient.TCPDecipher.ConnectionToken ct) {
            return base.Channel.Dispatch(ct);
        }
        
        public System.Threading.Tasks.Task<DummyClient.TCPDecipher.ConnectionToken> DispatchAsync(DummyClient.TCPDecipher.ConnectionToken ct) {
            return base.Channel.DispatchAsync(ct);
        }
    }
}
