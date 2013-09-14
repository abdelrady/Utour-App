using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Domain.DataContracts.DTOs
{
    //public class UserAuthInfo
    //{
    //    public string  UserName { get; set; }
    //    public string Password { get; set; }
    //    public UserTypes UserType { get; set; }
    //}
    [DataContract]
    public class UserAuthInfo
    {
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public UserTypes UserType { get; set; }
        [DataMember]
        public string Nationality { get; set; }
        [DataMember]
        public string Preferred_Language { get; set; }
        [DataMember]
        public int ID { get; set; }
    }
}
