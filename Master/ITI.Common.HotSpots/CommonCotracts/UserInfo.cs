using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WcfService1
{
    [DataContract]
    public class UserInfo
    {
        [DataMember]
        string uname;
        [DataMember]
        string pwd;
        [DataMember]
        string email;
        // int mno;
        [DataMember]
        string firstName;
        [DataMember]
        string lastName;
        [DataMember]
        string nationality;
        [DataMember]
        string gender;
        [DataMember]
        string prefferedlanguage;
        public string USERNAME
        {
            get { return uname; }
            set { uname = value; }
        }

        public string PASSWORD
        {
            get { return pwd; }
            set { pwd = value; }
        }
        public string EMAILADDRESS
        {
            get { return email; }
            set { email = value; }
        }

        //public int MOBILE
        //{
        //    get { return mno; }
        //    set { mno = value; }
        //}

        public string FIRSTNAME
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LASTNAME
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public string NATIONALITY
        {
            get { return nationality; }
            set { nationality = value; }
        }
        public string GENDER
        {
            get { return gender; }
            set { gender = value; }
        }
        public string PREFFEREDLANGUAGE { get { return prefferedlanguage; } set { prefferedlanguage = value; } }
    }
}
