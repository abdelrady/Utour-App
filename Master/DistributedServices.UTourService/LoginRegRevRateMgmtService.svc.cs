using System;
using System.ServiceModel;
using Application.Contracts;
using Application.Impl;
using DistributedServices.UTourService.InstanceProvider;
using Domain.DataContracts;
using Domain.DataContracts.DTOs;
using ITI.Common.HotSpotsInfo.CommonCotracts;
using ITI.Common.Utilities.Diagnostics.Trace;
using ITI.Nlayerd.Infrastructure.Data.UTour.Context;
using ITI.Nlayerd.Infrastructure.Data.UTour.Repositories;
using DistributedServices.Contracts;
using WcfService1;
using System.Runtime.Serialization;

namespace DistributedServices.UTourService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    [UnityInstanceProviderServiceBehavior]
    [ServiceBehavior]
    public class LoginRegRevRateMgmtService : ILoginRegRevRateMgmtService
    {
        private readonly IRegistrationManagementService _regMgmtSvc;
        private readonly ILoginManagementService _oLoginManagementService;
        private readonly IRatingReviewsManagementService _oRatingReviewsManagementService;

        public LoginRegRevRateMgmtService(IRegistrationManagementService regMgmtSvc, ILoginManagementService oLoginManagementService, IRatingReviewsManagementService oRatingReviewsManagementService)
        {
            this._regMgmtSvc = regMgmtSvc;
            this._oLoginManagementService = oLoginManagementService;
            this._oRatingReviewsManagementService = oRatingReviewsManagementService;
        }

        public string RegisterUser(UserInfo xList)
        {
            string errorMessage;
            bool gender = xList.GENDER != "male";
            
            var isRegistered = _regMgmtSvc.RegisterUser(
                new Tourist()
                {
                    UserName = xList.USERNAME,
                    Password = xList.PASSWORD,
                    Email = xList.EMAILADDRESS,
                    First_Name = xList.FIRSTNAME,
                    Gender = gender,
                    Last_Name = xList.LASTNAME,
                    Nationality = xList.NATIONALITY,
                    Preferred_Language = xList.PREFFEREDLANGUAGE
                }, errorMessage: out errorMessage);
            return xList.USERNAME;
        }
        
       public LogInResult LogIn(ref UserAuthInfo userAuthInfo)
        {
            var isAuthenticated =
                _oLoginManagementService.AuthenticateUser(userAuthInfo);
           var logInResult = new LogInResult {isSucceeded = isAuthenticated.isSucceeded};
           return logInResult;
        }

       public OperationResult PostReview(ReviewInfo reviewInfo)
        {
            string errorMessage;
            bool isValid = _oRatingReviewsManagementService.PostReview(reviewInfo.UserName, reviewInfo.Hotspotid, reviewInfo.Userreview, out errorMessage);
            return new OperationResult(){ErrorMessage = errorMessage , OperationStatus = isValid};
        }

       public RateResultReview Rate(RateInfo rateInfo)
       {
           string errorMessage;
           _oRatingReviewsManagementService.PostRate(rateInfo.USERNAME, rateInfo.HOTSPOTID, rateInfo.RATE,
                                                    out errorMessage);
           var rateResultReview = new RateResultReview {Rate = rateInfo.RATE, errorMessage = errorMessage};
           return rateResultReview;
       }
    }


}
