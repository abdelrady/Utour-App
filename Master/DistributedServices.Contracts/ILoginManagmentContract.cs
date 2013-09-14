using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Domain.DataContracts.DTOs;

namespace DistributedServices.Contracts
{
    [ServiceContract]
    public interface ILoginManagmentContract
    {
            [OperationContract]
        LogInResult AuthenticateUser(ref UserAuthInfo userAuthInfo);

    }
}
