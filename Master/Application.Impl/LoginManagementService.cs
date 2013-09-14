using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Contracts;
using Domain.DataContracts;
using Domain.Contracts;
using Domain.DataContracts.DTOs;
using ITI.Common.Utilities.Exceptions;
using ITI.Common.Utilities.General;
using DistributedServices.Contracts;

namespace Application.Impl
{
    public class LoginManagementService : ILoginManagementService
    {
        #region private members

        private IAdminUsersRepository _adminUsersRepository;
        private ITouristRepository _touristRepository;

        #endregion

        public LoginManagementService(IAdminUsersRepository adminUsersRepository, ITouristRepository touristRepository)
        {
            if (adminUsersRepository != (IAdminUsersRepository)null)
                _adminUsersRepository = adminUsersRepository;
            else throw new ArgumentException(message: "Non Valid Argument Passed", paramName: "adminUsersRepository");
            if (touristRepository != (ITouristRepository)null)
                _touristRepository = touristRepository;
            else throw new ArgumentException(message: "Non Valid Argument Passed", paramName: "touristRepository");
        }

        /// <summary>
        /// do some validation on input data 
        /// to be caution of injection attacks
        /// </summary>
        /// <param name="UserName">user name</param>
        /// <param name="Password">password</param>
        /// <returns></returns>
        public bool ValidateInput(UserAuthInfo userAuthInfo)
        {

            return (!string.IsNullOrEmpty(userAuthInfo.UserName) && !string.IsNullOrWhiteSpace(userAuthInfo.UserName))
                   && (!string.IsNullOrEmpty(userAuthInfo.Password) && !string.IsNullOrWhiteSpace(userAuthInfo.Password));
        }

        public LogInResult AuthenticateUser(UserAuthInfo userAuthInfo)
        {
            if (ValidateInput(userAuthInfo))
            {
                var userHash = userAuthInfo.Password.GetMD5Hash();
                var userName = userAuthInfo.UserName;
                switch (userAuthInfo.UserType)
                {
                    case UserTypes.Tourist:
                        var touristLst = _touristRepository.GetFilteredElements(
                            tourist => tourist.UserName == userName && userHash == tourist.Password);
                        if (touristLst.Any())
                        {
                            var touristInfo = touristLst.First();
                            userAuthInfo.ID = touristInfo.ID;
                            userAuthInfo.Preferred_Language = touristInfo.Preferred_Language.Trim();
                            userAuthInfo.Nationality = touristInfo.Nationality.Trim();
                            return new LogInResult { isSucceeded = true, errorMessage = "" };
                        }
                        else return  new LogInResult { isSucceeded = false, errorMessage = "" };
                        break;
                    case UserTypes.Admin:
                        return new LogInResult
                        {
                            isSucceeded = _adminUsersRepository.GetFilteredElements(
                                user => user.UserName == userName && userHash == user.Password).Any(),
                            errorMessage = ""
                        };
                            
                            
                    default:
                        throw new InvalidUserTypeException(
                            "Invalid User Type , UserType Must be either Admin or Tourist.");
                }
            }
            else
            {
                throw new NotValidUserInfoException("Invalid User Name , Or Password Data.");
            }
        }
    }
}