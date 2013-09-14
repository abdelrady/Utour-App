using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Application.Impl;
using Domain.DataContracts;
using ITI.Common.Utilities.Diagnostics.Trace;
using ITI.Nlayerd.Infrastructure.Data.UTour.Context;
using ITI.Nlayerd.Infrastructure.Data.UTour.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.ManagementServices.Test
{
    [TestClass]
    public class RegistrationMgmtSvcTest
    {
        [TestMethod]
        public void AdminUser_Register_TestMethod1()
        {
            string errorMessage;

            var ctx = new UTOURDBEntities();
            var regMgmtSvc = new RegistrationManagementService(
                new AdminUsersRepository(ctx, new TraceManager()), new TouristRepository(ctx, new TraceManager()));

            var isRegistered = regMgmtSvc.RegisterAdmin(new Admin_Users()
                                         {
                                             UserName = "ahmed",
                                             Password = "ahmed",
                                         }
                                     , errorMessage: out errorMessage);
            //Assert.AreEqual(true , isRegistered);
            Assert.AreEqual(string.Empty , errorMessage);

        }
        [TestMethod]
        public void TouristUser_Register_TestMethod1()
        {
            string errorMessage;

            var ctx = new UTOURDBEntities();
            var regMgmtSvc = new RegistrationManagementService(
                new AdminUsersRepository(ctx, new TraceManager()), new TouristRepository(ctx, new TraceManager()));
            var isRegistered = regMgmtSvc.RegisterUser(
                new Tourist()
                    {
                        UserName = "Rana",
                        Password = "rana",
                        Email = "Rana@gmail.com",
                        First_Name = "Rana",
                        Gender = true,
                        Last_Name = "Amr",
                        Nationality = "Eg",
                        Preferred_Language = "ar-eg"
                    }, errorMessage: out errorMessage);

            Assert.AreEqual(string.Empty, errorMessage);
            Assert.AreEqual(true, isRegistered);
        }
    }

}
