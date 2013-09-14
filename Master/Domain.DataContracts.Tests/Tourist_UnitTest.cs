using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.DataContracts.Tests
{
    [TestClass]
    public class Tourist_UnitTest
    {
        [TestMethod]
        public void Tourist_Entity_TestMethod1()
        {
            Tourist tourist = new Tourist()
                                  {
                                      UserName = "test",
                                      Password = "test",
                                      Email = "abdelrady@gmail.com",
                                      First_Name = "asd",
                                      Gender = true,
                                      Last_Name="asd",
                                      Nationality="Eg",
                                      Preferred_Language = "en-us"
                                  };
            var isValid = tourist.IsValid;
            var error = tourist.Error;
            Assert.AreEqual(true , isValid);

        }
    }
}
