using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ITI.Common.Entities
{
    [DataContract]
    public class CapturedPicture
    {
        [DataMember]
        public byte[] ImageBytes
        {
            get;
            set;
        }

        [DataMember]
        public string FileName
        {
            get;
            set;
        }
    }
}
