﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Services.Test.LoginRegRevRate.ServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserInfo", Namespace="http://schemas.datacontract.org/2004/07/WcfService1")]
    [System.SerializableAttribute()]
    public partial class UserInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string emailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string firstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string genderField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string lastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string nationalityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string prefferedlanguageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string pwdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string unameField;
        
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
        public string email {
            get {
                return this.emailField;
            }
            set {
                if ((object.ReferenceEquals(this.emailField, value) != true)) {
                    this.emailField = value;
                    this.RaisePropertyChanged("email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string firstName {
            get {
                return this.firstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.firstNameField, value) != true)) {
                    this.firstNameField = value;
                    this.RaisePropertyChanged("firstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string gender {
            get {
                return this.genderField;
            }
            set {
                if ((object.ReferenceEquals(this.genderField, value) != true)) {
                    this.genderField = value;
                    this.RaisePropertyChanged("gender");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string lastName {
            get {
                return this.lastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.lastNameField, value) != true)) {
                    this.lastNameField = value;
                    this.RaisePropertyChanged("lastName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string nationality {
            get {
                return this.nationalityField;
            }
            set {
                if ((object.ReferenceEquals(this.nationalityField, value) != true)) {
                    this.nationalityField = value;
                    this.RaisePropertyChanged("nationality");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string prefferedlanguage {
            get {
                return this.prefferedlanguageField;
            }
            set {
                if ((object.ReferenceEquals(this.prefferedlanguageField, value) != true)) {
                    this.prefferedlanguageField = value;
                    this.RaisePropertyChanged("prefferedlanguage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string pwd {
            get {
                return this.pwdField;
            }
            set {
                if ((object.ReferenceEquals(this.pwdField, value) != true)) {
                    this.pwdField = value;
                    this.RaisePropertyChanged("pwd");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string uname {
            get {
                return this.unameField;
            }
            set {
                if ((object.ReferenceEquals(this.unameField, value) != true)) {
                    this.unameField = value;
                    this.RaisePropertyChanged("uname");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="UserAuthInfo", Namespace="http://schemas.datacontract.org/2004/07/Domain.DataContracts.DTOs")]
    [System.SerializableAttribute()]
    public partial class UserAuthInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Services.Test.LoginRegRevRate.ServiceReference.UserTypes UserTypeField;
        
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
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Services.Test.LoginRegRevRate.ServiceReference.UserTypes UserType {
            get {
                return this.UserTypeField;
            }
            set {
                if ((this.UserTypeField.Equals(value) != true)) {
                    this.UserTypeField = value;
                    this.RaisePropertyChanged("UserType");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserTypes", Namespace="http://schemas.datacontract.org/2004/07/Domain.DataContracts")]
    public enum UserTypes : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Tourist = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Admin = 1,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RateInfo", Namespace="http://schemas.datacontract.org/2004/07/DistributedServices.Contracts")]
    [System.SerializableAttribute()]
    public partial class RateInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string HOTSPOTIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RATEField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string USERNAMEField;
        
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
        public string HOTSPOTID {
            get {
                return this.HOTSPOTIDField;
            }
            set {
                if ((object.ReferenceEquals(this.HOTSPOTIDField, value) != true)) {
                    this.HOTSPOTIDField = value;
                    this.RaisePropertyChanged("HOTSPOTID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RATE {
            get {
                return this.RATEField;
            }
            set {
                if ((this.RATEField.Equals(value) != true)) {
                    this.RATEField = value;
                    this.RaisePropertyChanged("RATE");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string USERNAME {
            get {
                return this.USERNAMEField;
            }
            set {
                if ((object.ReferenceEquals(this.USERNAMEField, value) != true)) {
                    this.USERNAMEField = value;
                    this.RaisePropertyChanged("USERNAME");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="ReviewInfo", Namespace="http://schemas.datacontract.org/2004/07/DistributedServices.Contracts")]
    [System.SerializableAttribute()]
    public partial class ReviewInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string HotspotidField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserreviewField;
        
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
        public string Hotspotid {
            get {
                return this.HotspotidField;
            }
            set {
                if ((object.ReferenceEquals(this.HotspotidField, value) != true)) {
                    this.HotspotidField = value;
                    this.RaisePropertyChanged("Hotspotid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Userreview {
            get {
                return this.UserreviewField;
            }
            set {
                if ((object.ReferenceEquals(this.UserreviewField, value) != true)) {
                    this.UserreviewField = value;
                    this.RaisePropertyChanged("Userreview");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="OperationResult", Namespace="http://schemas.datacontract.org/2004/07/ITI.Common.HotSpotsInfo.CommonCotracts")]
    [System.SerializableAttribute()]
    public partial class OperationResult : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorMessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool OperationStatusField;
        
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
        public string ErrorMessage {
            get {
                return this.ErrorMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorMessageField, value) != true)) {
                    this.ErrorMessageField = value;
                    this.RaisePropertyChanged("ErrorMessage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool OperationStatus {
            get {
                return this.OperationStatusField;
            }
            set {
                if ((this.OperationStatusField.Equals(value) != true)) {
                    this.OperationStatusField = value;
                    this.RaisePropertyChanged("OperationStatus");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="LoginRegRevRate.ServiceReference.ILoginRegRevRateMgmtService")]
    public interface ILoginRegRevRateMgmtService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginRegRevRateMgmtService/RegisterUser", ReplyAction="http://tempuri.org/ILoginRegRevRateMgmtService/RegisterUserResponse")]
        string RegisterUser(Services.Test.LoginRegRevRate.ServiceReference.UserInfo xList);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginRegRevRateMgmtService/LogIn", ReplyAction="http://tempuri.org/ILoginRegRevRateMgmtService/LogInResponse")]
        bool LogIn(Services.Test.LoginRegRevRate.ServiceReference.UserAuthInfo userAuthInfo);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILoginRegRevRateMgmtService/Rate")]
        void Rate(Services.Test.LoginRegRevRate.ServiceReference.RateInfo rateInfo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginRegRevRateMgmtService/PostReview", ReplyAction="http://tempuri.org/ILoginRegRevRateMgmtService/PostReviewResponse")]
        Services.Test.LoginRegRevRate.ServiceReference.OperationResult PostReview(Services.Test.LoginRegRevRate.ServiceReference.ReviewInfo reviewInfo);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILoginRegRevRateMgmtServiceChannel : Services.Test.LoginRegRevRate.ServiceReference.ILoginRegRevRateMgmtService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LoginRegRevRateMgmtServiceClient : System.ServiceModel.ClientBase<Services.Test.LoginRegRevRate.ServiceReference.ILoginRegRevRateMgmtService>, Services.Test.LoginRegRevRate.ServiceReference.ILoginRegRevRateMgmtService {
        
        public LoginRegRevRateMgmtServiceClient() {
        }
        
        public LoginRegRevRateMgmtServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LoginRegRevRateMgmtServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LoginRegRevRateMgmtServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LoginRegRevRateMgmtServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string RegisterUser(Services.Test.LoginRegRevRate.ServiceReference.UserInfo xList) {
            return base.Channel.RegisterUser(xList);
        }
        
        public bool LogIn(Services.Test.LoginRegRevRate.ServiceReference.UserAuthInfo userAuthInfo) {
            return base.Channel.LogIn(userAuthInfo);
        }
        
        public void Rate(Services.Test.LoginRegRevRate.ServiceReference.RateInfo rateInfo) {
            base.Channel.Rate(rateInfo);
        }
        
        public Services.Test.LoginRegRevRate.ServiceReference.OperationResult PostReview(Services.Test.LoginRegRevRate.ServiceReference.ReviewInfo reviewInfo) {
            return base.Channel.PostReview(reviewInfo);
        }
    }
}