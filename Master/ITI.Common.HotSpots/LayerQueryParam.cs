using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ITI.Common.HotSpotsInfo
{
    [DataContract]
    public class LayerQueryParam
    {
        [DataMember]
        public string lang;
        [DataMember]
        public string countryCode;
        [DataMember]
        public string lon;
        [DataMember]
        public string userId;
        [DataMember]
        public string developerId;
        [DataMember]
        public string developerHash;
        [DataMember]
        public string version;
        [DataMember]
        public string radius;
        [DataMember]
        public string timestamp;
        [DataMember]
        public string lat;
        [DataMember]
        public string layerName;
        [DataMember]
        public string accuracy;

        //public LayerQueryParam(string lang, string countryCode, string lon, string userId, string developerId, string developerHash, string version, string radius, string timestamp, string lat, string layerName, string accuracy)
        //{
        //    this.lang = lang;
        //    this.countryCode = countryCode;
        //    this.lon = lon;
        //    this.userId = userId;
        //    this.developerId = developerId;
        //    this.developerHash = developerHash;
        //    this.version = version;
        //    this.radius = radius;
        //    this.timestamp = timestamp;
        //    this.lat = lat;
        //    this.layerName = layerName;
        //    this.accuracy = accuracy;
        //}
    }
}
