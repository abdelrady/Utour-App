//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using ITI.Common.Utilities.Domain.Core.Entities;

namespace Domain.DataContracts
{
    [DataContract(IsReference = true)]
    public partial class webpages_Membership: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int UserId
        {
            get { return _userId; }
            set
            {
                if (_userId != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'UserId' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _userId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }
        private int _userId;
    
        [DataMember]
        public Nullable<System.DateTime> CreateDate
        {
            get { return _createDate; }
            set
            {
                if (_createDate != value)
                {
                    _createDate = value;
                    OnPropertyChanged("CreateDate");
                }
            }
        }
        private Nullable<System.DateTime> _createDate;
    
        [DataMember]
        public string ConfirmationToken
        {
            get { return _confirmationToken; }
            set
            {
                if (_confirmationToken != value)
                {
                    _confirmationToken = value;
                    OnPropertyChanged("ConfirmationToken");
                }
            }
        }
        private string _confirmationToken;
    
        [DataMember]
        public Nullable<bool> IsConfirmed
        {
            get { return _isConfirmed; }
            set
            {
                if (_isConfirmed != value)
                {
                    _isConfirmed = value;
                    OnPropertyChanged("IsConfirmed");
                }
            }
        }
        private Nullable<bool> _isConfirmed;
    
        [DataMember]
        public Nullable<System.DateTime> LastPasswordFailureDate
        {
            get { return _lastPasswordFailureDate; }
            set
            {
                if (_lastPasswordFailureDate != value)
                {
                    _lastPasswordFailureDate = value;
                    OnPropertyChanged("LastPasswordFailureDate");
                }
            }
        }
        private Nullable<System.DateTime> _lastPasswordFailureDate;
    
        [DataMember]
        public int PasswordFailuresSinceLastSuccess
        {
            get { return _passwordFailuresSinceLastSuccess; }
            set
            {
                if (_passwordFailuresSinceLastSuccess != value)
                {
                    _passwordFailuresSinceLastSuccess = value;
                    OnPropertyChanged("PasswordFailuresSinceLastSuccess");
                }
            }
        }
        private int _passwordFailuresSinceLastSuccess;
    
        [DataMember]
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }
        private string _password;
    
        [DataMember]
        public Nullable<System.DateTime> PasswordChangedDate
        {
            get { return _passwordChangedDate; }
            set
            {
                if (_passwordChangedDate != value)
                {
                    _passwordChangedDate = value;
                    OnPropertyChanged("PasswordChangedDate");
                }
            }
        }
        private Nullable<System.DateTime> _passwordChangedDate;
    
        [DataMember]
        public string PasswordSalt
        {
            get { return _passwordSalt; }
            set
            {
                if (_passwordSalt != value)
                {
                    _passwordSalt = value;
                    OnPropertyChanged("PasswordSalt");
                }
            }
        }
        private string _passwordSalt;
    
        [DataMember]
        public string PasswordVerificationToken
        {
            get { return _passwordVerificationToken; }
            set
            {
                if (_passwordVerificationToken != value)
                {
                    _passwordVerificationToken = value;
                    OnPropertyChanged("PasswordVerificationToken");
                }
            }
        }
        private string _passwordVerificationToken;
    
        [DataMember]
        public Nullable<System.DateTime> PasswordVerificationTokenExpirationDate
        {
            get { return _passwordVerificationTokenExpirationDate; }
            set
            {
                if (_passwordVerificationTokenExpirationDate != value)
                {
                    _passwordVerificationTokenExpirationDate = value;
                    OnPropertyChanged("PasswordVerificationTokenExpirationDate");
                }
            }
        }
        private Nullable<System.DateTime> _passwordVerificationTokenExpirationDate;

        #endregion

        #region ChangeTracking
    
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (ChangeTracker.State != ObjectState.Added && ChangeTracker.State != ObjectState.Deleted)
            {
                ChangeTracker.State = ObjectState.Modified;
            }
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        protected virtual void OnNavigationPropertyChanged(String propertyName)
        {
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged{ add { _propertyChanged += value; } remove { _propertyChanged -= value; } }
        private event PropertyChangedEventHandler _propertyChanged;
        private ObjectChangeTracker _changeTracker;
    
        [DataMember]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if (_changeTracker == null)
                {
                    _changeTracker = new ObjectChangeTracker();
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
                return _changeTracker;
            }
            set
            {
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging -= HandleObjectStateChanging;
                }
                _changeTracker = value;
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
            }
        }
    
        private void HandleObjectStateChanging(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                ClearNavigationProperties();
            }
        }
    
        protected bool IsDeserializing { get; private set; }
    
        [OnDeserializing]
        public void OnDeserializingMethod(StreamingContext context)
        {
            IsDeserializing = true;
        }
    
        [OnDeserialized]
        public void OnDeserializedMethod(StreamingContext context)
        {
            IsDeserializing = false;
            ChangeTracker.ChangeTrackingEnabled = true;
        }
    
        protected virtual void ClearNavigationProperties()
        {
        }

        #endregion

    }
}
