using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DistributedServices.Contracts
{
    [DataContract]
  public  class RateInfo
    {
        private string userName;
        private string hotSpotId;
        private double rate;
        [DataMember]
        public string USERNAME { get; set; }
        [DataMember]
        public string HOTSPOTID { get; set; }
        [DataMember]
        public double RATE { get; set; }
    }
}
