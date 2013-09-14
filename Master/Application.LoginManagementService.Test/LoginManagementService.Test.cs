using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Domain.DataContracts;
using Domain.DataContracts.DTOs;
using ITI.Common.Utilities.Diagnostics.Trace;
using ITI.Nlayerd.Infrastructure.Data.UTour.Context;
using ITI.Nlayerd.Infrastructure.Data.UTour.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.LoginManagementService.Test
{
    [TestClass]
    public class LoginManagementServiceTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var ctx = new UTOURDBEntities();
            var oLoginManagementService = new Impl.LoginManagementService(new AdminUsersRepository(ctx, new TraceManager()),
                new TouristRepository(ctx,new TraceManager() ));
            var tourist =  new UserAuthInfo()
                                                             {
                                                                 UserName = "rana",
                                                                 Password = "rana",
                                                                 UserType = UserTypes.Admin
                                                             };
            var isAuthenticated =
                oLoginManagementService.AuthenticateUser(tourist); 
            Assert.AreEqual(true , isAuthenticated.isSucceeded);

        }
    }
}
