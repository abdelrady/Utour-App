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
    [KnownType(typeof(city))]
    [KnownType(typeof(Monument_Ratings))]
    [KnownType(typeof(Monuments_Photos))]
    [KnownType(typeof(Monuments_Reviews))]
    [KnownType(typeof(Monuments_Videos))]
    [KnownType(typeof(UploadedPhoto))]
    public partial class layerhotspot: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public string Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'Id' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        private string _id;
    
        [DataMember]
        public string footnote
        {
            get { return _footnote; }
            set
            {
                if (_footnote != value)
                {
                    _footnote = value;
                    OnPropertyChanged("footnote");
                }
            }
        }
        private string _footnote;
    
        [DataMember]
        public string title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged("title");
                }
            }
        }
        private string _title;
    
        [DataMember]
        public Nullable<decimal> lat
        {
            get { return _lat; }
            set
            {
                if (_lat != value)
                {
                    _lat = value;
                    OnPropertyChanged("lat");
                }
            }
        }
        private Nullable<decimal> _lat;
    
        [DataMember]
        public Nullable<decimal> lon
        {
            get { return _lon; }
            set
            {
                if (_lon != value)
                {
                    _lon = value;
                    OnPropertyChanged("lon");
                }
            }
        }
        private Nullable<decimal> _lon;
    
        [DataMember]
        public string imageurl
        {
            get { return _imageurl; }
            set
            {
                if (_imageurl != value)
                {
                    _imageurl = value;
                    OnPropertyChanged("imageurl");
                }
            }
        }
        private string _imageurl;
    
        [DataMember]
        public string description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("description");
                }
            }
        }
        private string _description;
    
        [DataMember]
        public string biwStyle
        {
            get { return _biwStyle; }
            set
            {
                if (_biwStyle != value)
                {
                    _biwStyle = value;
                    OnPropertyChanged("biwStyle");
                }
            }
        }
        private string _biwStyle;
    
        [DataMember]
        public Nullable<int> alt
        {
            get { return _alt; }
            set
            {
                if (_alt != value)
                {
                    _alt = value;
                    OnPropertyChanged("alt");
                }
            }
        }
        private Nullable<int> _alt;
    
        [DataMember]
        public Nullable<byte> doNotIndex
        {
            get { return _doNotIndex; }
            set
            {
                if (_doNotIndex != value)
                {
                    _doNotIndex = value;
                    OnPropertyChanged("doNotIndex");
                }
            }
        }
        private Nullable<byte> _doNotIndex;
    
        [DataMember]
        public Nullable<byte> showSmallBiw
        {
            get { return _showSmallBiw; }
            set
            {
                if (_showSmallBiw != value)
                {
                    _showSmallBiw = value;
                    OnPropertyChanged("showSmallBiw");
                }
            }
        }
        private Nullable<byte> _showSmallBiw;
    
        [DataMember]
        public Nullable<byte> showBiwOnClick
        {
            get { return _showBiwOnClick; }
            set
            {
                if (_showBiwOnClick != value)
                {
                    _showBiwOnClick = value;
                    OnPropertyChanged("showBiwOnClick");
                }
            }
        }
        private Nullable<byte> _showBiwOnClick;
    
        [DataMember]
        public string poiType
        {
            get { return _poiType; }
            set
            {
                if (_poiType != value)
                {
                    _poiType = value;
                    OnPropertyChanged("poiType");
                }
            }
        }
        private string _poiType;
    
        [DataMember]
        public string Monument_Audio
        {
            get { return _monument_Audio; }
            set
            {
                if (_monument_Audio != value)
                {
                    _monument_Audio = value;
                    OnPropertyChanged("Monument_Audio");
                }
            }
        }
        private string _monument_Audio;
    
        [DataMember]
        public string Long_Description
        {
            get { return _long_Description; }
            set
            {
                if (_long_Description != value)
                {
                    _long_Description = value;
                    OnPropertyChanged("Long_Description");
                }
            }
        }
        private string _long_Description;
    
        [DataMember]
        public Nullable<int> CityID
        {
            get { return _cityID; }
            set
            {
                if (_cityID != value)
                {
                    ChangeTracker.RecordOriginalValue("CityID", _cityID);
                    if (!IsDeserializing)
                    {
                        if (city != null && city.CityID != value)
                        {
                            city = null;
                        }
                    }
                    _cityID = value;
                    OnPropertyChanged("CityID");
                }
            }
        }
        private Nullable<int> _cityID;

        #endregion

        #region Navigation Properties
    
        [DataMember]
        public city city
        {
            get { return _city; }
            set
            {
                if (!ReferenceEquals(_city, value))
                {
                    var previousValue = _city;
                    _city = value;
                    Fixupcity(previousValue);
                    OnNavigationPropertyChanged("city");
                }
            }
        }
        private city _city;
    
        [DataMember]
        public TrackableCollection<Monument_Ratings> Monument_Ratings
        {
            get
            {
                if (_monument_Ratings == null)
                {
                    _monument_Ratings = new TrackableCollection<Monument_Ratings>();
                    _monument_Ratings.CollectionChanged += FixupMonument_Ratings;
                }
                return _monument_Ratings;
            }
            set
            {
                if (!ReferenceEquals(_monument_Ratings, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_monument_Ratings != null)
                    {
                        _monument_Ratings.CollectionChanged -= FixupMonument_Ratings;
                    }
                    _monument_Ratings = value;
                    if (_monument_Ratings != null)
                    {
                        _monument_Ratings.CollectionChanged += FixupMonument_Ratings;
                    }
                    OnNavigationPropertyChanged("Monument_Ratings");
                }
            }
        }
        private TrackableCollection<Monument_Ratings> _monument_Ratings;
    
        [DataMember]
        public TrackableCollection<Monuments_Photos> Monuments_Photos
        {
            get
            {
                if (_monuments_Photos == null)
                {
                    _monuments_Photos = new TrackableCollection<Monuments_Photos>();
                    _monuments_Photos.CollectionChanged += FixupMonuments_Photos;
                }
                return _monuments_Photos;
            }
            set
            {
                if (!ReferenceEquals(_monuments_Photos, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_monuments_Photos != null)
                    {
                        _monuments_Photos.CollectionChanged -= FixupMonuments_Photos;
                    }
                    _monuments_Photos = value;
                    if (_monuments_Photos != null)
                    {
                        _monuments_Photos.CollectionChanged += FixupMonuments_Photos;
                    }
                    OnNavigationPropertyChanged("Monuments_Photos");
                }
            }
        }
        private TrackableCollection<Monuments_Photos> _monuments_Photos;
    
        [DataMember]
        public TrackableCollection<Monuments_Reviews> Monuments_Reviews
        {
            get
            {
                if (_monuments_Reviews == null)
                {
                    _monuments_Reviews = new TrackableCollection<Monuments_Reviews>();
                    _monuments_Reviews.CollectionChanged += FixupMonuments_Reviews;
                }
                return _monuments_Reviews;
            }
            set
            {
                if (!ReferenceEquals(_monuments_Reviews, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_monuments_Reviews != null)
                    {
                        _monuments_Reviews.CollectionChanged -= FixupMonuments_Reviews;
                    }
                    _monuments_Reviews = value;
                    if (_monuments_Reviews != null)
                    {
                        _monuments_Reviews.CollectionChanged += FixupMonuments_Reviews;
                    }
                    OnNavigationPropertyChanged("Monuments_Reviews");
                }
            }
        }
        private TrackableCollection<Monuments_Reviews> _monuments_Reviews;
    
        [DataMember]
        public TrackableCollection<Monuments_Videos> Monuments_Videos
        {
            get
            {
                if (_monuments_Videos == null)
                {
                    _monuments_Videos = new TrackableCollection<Monuments_Videos>();
                    _monuments_Videos.CollectionChanged += FixupMonuments_Videos;
                }
                return _monuments_Videos;
            }
            set
            {
                if (!ReferenceEquals(_monuments_Videos, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_monuments_Videos != null)
                    {
                        _monuments_Videos.CollectionChanged -= FixupMonuments_Videos;
                    }
                    _monuments_Videos = value;
                    if (_monuments_Videos != null)
                    {
                        _monuments_Videos.CollectionChanged += FixupMonuments_Videos;
                    }
                    OnNavigationPropertyChanged("Monuments_Videos");
                }
            }
        }
        private TrackableCollection<Monuments_Videos> _monuments_Videos;
    
        [DataMember]
        public TrackableCollection<UploadedPhoto> UploadedPhotos
        {
            get
            {
                if (_uploadedPhotos == null)
                {
                    _uploadedPhotos = new TrackableCollection<UploadedPhoto>();
                    _uploadedPhotos.CollectionChanged += FixupUploadedPhotos;
                }
                return _uploadedPhotos;
            }
            set
            {
                if (!ReferenceEquals(_uploadedPhotos, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_uploadedPhotos != null)
                    {
                        _uploadedPhotos.CollectionChanged -= FixupUploadedPhotos;
                    }
                    _uploadedPhotos = value;
                    if (_uploadedPhotos != null)
                    {
                        _uploadedPhotos.CollectionChanged += FixupUploadedPhotos;
                    }
                    OnNavigationPropertyChanged("UploadedPhotos");
                }
            }
        }
        private TrackableCollection<UploadedPhoto> _uploadedPhotos;

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
            city = null;
            Monument_Ratings.Clear();
            Monuments_Photos.Clear();
            Monuments_Reviews.Clear();
            Monuments_Videos.Clear();
            UploadedPhotos.Clear();
        }

        #endregion

        #region Association Fixup
    
        private void Fixupcity(city previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.layerhotspots.Contains(this))
            {
                previousValue.layerhotspots.Remove(this);
            }
    
            if (city != null)
            {
                if (!city.layerhotspots.Contains(this))
                {
                    city.layerhotspots.Add(this);
                }
    
                CityID = city.CityID;
            }
            else if (!skipKeys)
            {
                CityID = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("city")
                    && (ChangeTracker.OriginalValues["city"] == city))
                {
                    ChangeTracker.OriginalValues.Remove("city");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("city", previousValue);
                }
                if (city != null && !city.ChangeTracker.ChangeTrackingEnabled)
                {
                    city.StartTracking();
                }
            }
        }
    
        private void FixupMonument_Ratings(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (Monument_Ratings item in e.NewItems)
                {
                    item.layerhotspot = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("Monument_Ratings", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Monument_Ratings item in e.OldItems)
                {
                    if (ReferenceEquals(item.layerhotspot, this))
                    {
                        item.layerhotspot = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("Monument_Ratings", item);
                    }
                }
            }
        }
    
        private void FixupMonuments_Photos(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (Monuments_Photos item in e.NewItems)
                {
                    item.layerhotspot = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("Monuments_Photos", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Monuments_Photos item in e.OldItems)
                {
                    if (ReferenceEquals(item.layerhotspot, this))
                    {
                        item.layerhotspot = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("Monuments_Photos", item);
                    }
                }
            }
        }
    
        private void FixupMonuments_Reviews(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (Monuments_Reviews item in e.NewItems)
                {
                    item.layerhotspot = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("Monuments_Reviews", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Monuments_Reviews item in e.OldItems)
                {
                    if (ReferenceEquals(item.layerhotspot, this))
                    {
                        item.layerhotspot = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("Monuments_Reviews", item);
                    }
                }
            }
        }
    
        private void FixupMonuments_Videos(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (Monuments_Videos item in e.NewItems)
                {
                    item.layerhotspot = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("Monuments_Videos", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Monuments_Videos item in e.OldItems)
                {
                    if (ReferenceEquals(item.layerhotspot, this))
                    {
                        item.layerhotspot = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("Monuments_Videos", item);
                    }
                }
            }
        }
    
        private void FixupUploadedPhotos(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (UploadedPhoto item in e.NewItems)
                {
                    item.layerhotspot = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("UploadedPhotos", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (UploadedPhoto item in e.OldItems)
                {
                    if (ReferenceEquals(item.layerhotspot, this))
                    {
                        item.layerhotspot = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("UploadedPhotos", item);
                    }
                }
            }
        }

        #endregion

    }
}