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
    [KnownType(typeof(layerhotspot))]
    public partial class Monuments_Videos: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public string hostSpotID
        {
            get { return _hostSpotID; }
            set
            {
                if (_hostSpotID != value)
                {
                    ChangeTracker.RecordOriginalValue("hostSpotID", _hostSpotID);
                    if (!IsDeserializing)
                    {
                        if (layerhotspot != null && layerhotspot.Id != value)
                        {
                            layerhotspot = null;
                        }
                    }
                    _hostSpotID = value;
                    OnPropertyChanged("hostSpotID");
                }
            }
        }
        private string _hostSpotID;
    
        [DataMember]
        public string video
        {
            get { return _video; }
            set
            {
                if (_video != value)
                {
                    _video = value;
                    OnPropertyChanged("video");
                }
            }
        }
        private string _video;
    
        [DataMember]
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        private string _description;
    
        [DataMember]
        public string VideoLength
        {
            get { return _videoLength; }
            set
            {
                if (_videoLength != value)
                {
                    _videoLength = value;
                    OnPropertyChanged("VideoLength");
                }
            }
        }
        private string _videoLength;
    
        [DataMember]
        public int ID
        {
            get { return _iD; }
            set
            {
                if (_iD != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'ID' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _iD = value;
                    OnPropertyChanged("ID");
                }
            }
        }
        private int _iD;

        #endregion

        #region Navigation Properties
    
        [DataMember]
        public layerhotspot layerhotspot
        {
            get { return _layerhotspot; }
            set
            {
                if (!ReferenceEquals(_layerhotspot, value))
                {
                    var previousValue = _layerhotspot;
                    _layerhotspot = value;
                    Fixuplayerhotspot(previousValue);
                    OnNavigationPropertyChanged("layerhotspot");
                }
            }
        }
        private layerhotspot _layerhotspot;

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
            layerhotspot = null;
        }

        #endregion

        #region Association Fixup
    
        private void Fixuplayerhotspot(layerhotspot previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.Monuments_Videos.Contains(this))
            {
                previousValue.Monuments_Videos.Remove(this);
            }
    
            if (layerhotspot != null)
            {
                if (!layerhotspot.Monuments_Videos.Contains(this))
                {
                    layerhotspot.Monuments_Videos.Add(this);
                }
    
                hostSpotID = layerhotspot.Id;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("layerhotspot")
                    && (ChangeTracker.OriginalValues["layerhotspot"] == layerhotspot))
                {
                    ChangeTracker.OriginalValues.Remove("layerhotspot");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("layerhotspot", previousValue);
                }
                if (layerhotspot != null && !layerhotspot.ChangeTracker.ChangeTrackingEnabled)
                {
                    layerhotspot.StartTracking();
                }
            }
        }

        #endregion

    }
}