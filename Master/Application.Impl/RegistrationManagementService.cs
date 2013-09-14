using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Application.Contracts;
using Domain.Contracts;
using Domain.DataContracts;
using ITI.Common.Utilities.General;

namespace Application.Impl
{
    public class RegistrationManagementService : IRegistrationManagementService
    {

        #region private members

        private IAdminUsersRepository _adminUsersRepository;
        private ITouristRepository _touristRepository;

        #endregion

        public RegistrationManagementService(IAdminUsersRepository adminUsersRepository, ITouristRepository touristRepository)
        {
            if (adminUsersRepository != (IAdminUsersRepository)null)
                _adminUsersRepository = adminUsersRepository;
            else throw new ArgumentException(message: "Non Valid Argument Passed", paramName: "adminUsersRepository");
            if (touristRepository != (ITouristRepository)null)
                _touristRepository = touristRepository;
            else throw new ArgumentException(message: "Non Valid Argument Passed", paramName: "touristRepository");
        }

        public bool ValidateUser(Tourist tourist)
        {
            return tourist.IsValid;
        }

        public bool RegisterUser(Tourist tourist, out string errorMessage)
        {
            //check to see if this is a valid admin object
            if (!ValidateUser(tourist))
            {
                errorMessage = tourist.Error;
                return false;
            }

            //check to see if this user is already in DB
            if (_touristRepository.GetFilteredElements(tourist1 => tourist1.UserName == tourist.UserName).Any())
            {
                errorMessage = "Sorry, User Name Is Already Existed.";
                return false;
            }

            //modify the plain text password to md5 version to store in DB
            tourist.Password = tourist.Password.GetMD5Hash();


            //add the object and save into database
            _touristRepository.Add(tourist);
            _touristRepository.UnitOfWork.Commit();
            errorMessage = string.Empty;
            return true;
        }

        public bool ValidateAdmin(Admin_Users admin)
        {
            return admin.IsValid;
        }

        public bool RegisterAdmin(Admin_Users admin, out string errorMessage)
        {
            //check to see if this is a valid admin object
            if (!ValidateAdmin(admin))
            {
                errorMessage = admin.Error;
                return false;
            }

            //check to see if this user is already in DB
            if (_adminUsersRepository.GetFilteredElements(users => users.UserName == admin.UserName).Any())
            {
                errorMessage = "Sorry, User Name Is Already Existed.";
                return false;
            }

            //modify the plain text password to md5 version to store in DB
            admin.Password = admin.Password.GetMD5Hash();

            //add the object and save into database
            _adminUsersRepository.Add(admin);
            _adminUsersRepository.UnitOfWork.Commit();
            errorMessage = string.Empty;
            return true;
        }
    }
}
