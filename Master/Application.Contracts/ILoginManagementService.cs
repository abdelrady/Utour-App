using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.DataContracts;
using Domain.DataContracts.DTOs;
using DistributedServices.Contracts;

namespace Application.Contracts
{
    public interface ILoginManagementService
    {
        bool ValidateInput(UserAuthInfo userAuthInfo);
        LogInResult AuthenticateUser(UserAuthInfo userAuthInfo);
    }
}
