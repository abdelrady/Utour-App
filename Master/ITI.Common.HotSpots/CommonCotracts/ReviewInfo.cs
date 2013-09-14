using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DistributedServices.Contracts
{
    [DataContract]
    public class ReviewInfo
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Hotspotid { get; set; }

        [DataMember]
        public string Userreview { get; set; }
    }
}
