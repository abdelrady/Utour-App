using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Application.Impl;
using ITI.Common.HotSpotsInfo;
using ITI.Common.Utilities.Diagnostics.Trace;
using ITI.Nlayerd.Infrastructure.Data.UTour.Context;
using ITI.Nlayerd.Infrastructure.Data.UTour.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.HotSpotsMgmtSvc.Test
{
    [TestClass]
    public class HotSpotsMgmtSvcTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            UTOURDBEntities ctx = new UTOURDBEntities();

            HotSpotsManagementService hotSpotsManagementService = new HotSpotsManagementService(
                new LayerHotSpotsRepository(ctx, new TraceManager()), new MonumentPhotosRepository(ctx, new TraceManager()),
                new MonumentRatingRepository(ctx, new TraceManager()), new MonumentReviewsRepository(ctx, new TraceManager()),
                new MonumentVideosRepository(ctx, new TraceManager()));
            var hotSpots = hotSpotsManagementService.RetrieveSurroundingHotSpots(new LayerQueryParam()
            {
                lat = "30.0473430000",
                lon = "31.2336260000",
                accuracy = 10.ToString(),
                radius = 500.ToString(),
            }).hotspots;

            var result = hotSpots.Count();

            Assert.AreEqual(4, result);

        }
    }
}
