using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ITI.Common.HotSpotsInfo.CommonCotracts
{
    [DataContract]
    public class OperationResult
    {
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public bool OperationStatus { get; set; }
    }
}
