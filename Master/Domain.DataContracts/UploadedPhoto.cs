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
    [KnownType(typeof(Tourist))]
    [KnownType(typeof(layerhotspot))]
    public partial class UploadedPhoto: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int TouristID
        {
            get { return _touristID; }
            set
            {
                if (_touristID != value)
                {
                    ChangeTracker.RecordOriginalValue("TouristID", _touristID);
                    if (!IsDeserializing)
                    {
                        if (Tourist != null && Tourist.ID != value)
                        {
                            Tourist = null;
                        }
                    }
                    _touristID = value;
                    OnPropertyChanged("TouristID");
                }
            }
        }
        private int _touristID;
    
        [DataMember]
        public string hotspotID
        {
            get { return _hotspotID; }
            set
            {
                if (_hotspotID != value)
                {
                    ChangeTracker.RecordOriginalValue("hotspotID", _hotspotID);
                    if (!IsDeserializing)
                    {
                        if (layerhotspot != null && layerhotspot.Id != value)
                        {
                            layerhotspot = null;
                        }
                    }
                    _hotspotID = value;
                    OnPropertyChanged("hotspotID");
                }
            }
        }
        private string _hotspotID;
    
        [DataMember]
        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                if (_imageUrl != value)
                {
                    _imageUrl = value;
                    OnPropertyChanged("ImageUrl");
                }
            }
        }
        private string _imageUrl;
    
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
        public Tourist Tourist
        {
            get { return _tourist; }
            set
            {
                if (!ReferenceEquals(_tourist, value))
                {
                    var previousValue = _tourist;
                    _tourist = value;
                    FixupTourist(previousValue);
                    OnNavigationPropertyChanged("Tourist");
                }
            }
        }
        private Tourist _tourist;
    
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
            Tourist = null;
            layerhotspot = null;
        }

        #endregion

        #region Association Fixup
    
        private void FixupTourist(Tourist previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.UploadedPhotos.Contains(this))
            {
                previousValue.UploadedPhotos.Remove(this);
            }
    
            if (Tourist != null)
            {
                if (!Tourist.UploadedPhotos.Contains(this))
                {
                    Tourist.UploadedPhotos.Add(this);
                }
    
                TouristID = Tourist.ID;
            }
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Tourist")
                    && (ChangeTracker.OriginalValues["Tourist"] == Tourist))
                {
                    ChangeTracker.OriginalValues.Remove("Tourist");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Tourist", previousValue);
                }
                if (Tourist != null && !Tourist.ChangeTracker.ChangeTrackingEnabled)
                {
                    Tourist.StartTracking();
                }
            }
        }
    
        private void Fixuplayerhotspot(layerhotspot previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.UploadedPhotos.Contains(this))
            {
                previousValue.UploadedPhotos.Remove(this);
            }
    
            if (layerhotspot != null)
            {
                if (!layerhotspot.UploadedPhotos.Contains(this))
                {
                    layerhotspot.UploadedPhotos.Add(this);
                }
    
                hotspotID = layerhotspot.Id;
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