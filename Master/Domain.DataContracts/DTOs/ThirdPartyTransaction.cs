using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Domain.DataContracts.DTOs
{
    [DataContract]
    public class ThirdPartyTransaction
    {
        [DataMember]
        public long PAN { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
        //[DataMember]
        //public TransactionType TransType { get; set; }
        [DataMember]
        public string Result { get; set; }
    }
}
