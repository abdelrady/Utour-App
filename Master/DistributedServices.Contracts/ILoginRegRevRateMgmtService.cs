using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Domain.DataContracts.DTOs;
using ITI.Common.HotSpotsInfo.CommonCotracts;
using WcfService1;


namespace DistributedServices.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ILoginRegRevRateMgmtService
    {
        [OperationContract]
        string RegisterUser(UserInfo xList);
        //[OperationContract]
        //bool LogIn(UserAuthInfo userAuthInfo);
        [OperationContract]
        LogInResult LogIn(ref UserAuthInfo userAuthInfo);
        [OperationContract]
        RateResultReview Rate(RateInfo rateInfo);

        [OperationContract()]
        OperationResult PostReview(ReviewInfo reviewInfo);

    }


    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
