using Application.Contracts;
using Application.Impl;
using DistributedServices.Contracts;
using ITI.Common.Utilities.Diagnostics.Trace;
using ITI.Nlayerd.Infrastructure.Data.UTour.Context;
using ITI.Nlayerd.Infrastructure.Data.UTour.Repositories;

namespace DistributedServices.UTourService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LoginManagmentService" in code, svc and config file together.
    public class LoginManagmentService : ILoginManagmentContract
    {
        private Application.Contracts.ILoginManagementService _loginManagementService;

        public LoginManagmentService(): this(new LoginManagementService(new AdminUsersRepository(new UTOURDBEntities(),new TraceManager() ),new TouristRepository(new UTOURDBEntities(),new TraceManager() ) ))
        {
            
        }
        public LoginManagmentService(ILoginManagementService loginManagementService)
        {
            _loginManagementService = loginManagementService;
        }
        
        public LogInResult AuthenticateUser(ref Domain.DataContracts.DTOs.UserAuthInfo userAuthInfo)
        {
            return _loginManagementService.AuthenticateUser(userAuthInfo);
        }
    }
}
