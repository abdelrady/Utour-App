using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using ITI.Common.Utilities.Diagnostics.Trace;
using ITI.Nlayerd.Infrastructure.Data.UTour.Context;
using ITI.Nlayerd.Infrastructure.Data.UTour.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Application.ManagementServices.Test
{
    [TestClass]
    public class UploadPhotosManagementService
    {
        [TestMethod]
        public void TestMethod1()
        {
            var ctx = new UTOURDBEntities();
            var traceManager = new TraceManager();
            string str = null;

            var uploadPhotosManagementService =
                new Impl.UploadPhotosManagementService(new TouristRepository(ctx, traceManager),
                                                       new UploadedPhotoRepository(ctx, traceManager));
            var result = uploadPhotosManagementService.SavePhoto("rana", "isl_15",
                File.ReadAllBytes(@"C:\Drive_D\ITI_Cources\GP\Implementation\SourceControlSolution\ITI_NLayered\Presentation.UtourWebsite\images\home.png"), out str);

            Assert.AreEqual(true , result);
        }
    }
}
