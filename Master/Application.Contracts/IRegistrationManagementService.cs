using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.DataContracts;

namespace Application.Contracts
{
    public interface IRegistrationManagementService
    {
        bool ValidateUser(Tourist tourist);
        bool RegisterUser(Tourist tourist , out string errorMessage);

        bool ValidateAdmin(Admin_Users admin);
        bool RegisterAdmin(Admin_Users admin, out string errorMessage);
    }

}
