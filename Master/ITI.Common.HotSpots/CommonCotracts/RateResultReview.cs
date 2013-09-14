using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ITI.Common.HotSpotsInfo.CommonCotracts
{
    [DataContract]
    public class RateResultReview
    {
        [DataMember]
        public double Rate;
        [DataMember]
        public string errorMessage;
    }
}
