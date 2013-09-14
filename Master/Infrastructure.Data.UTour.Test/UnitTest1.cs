using System;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using ITI.Common.HotSpotsInfo;
using ITI.Common.Utilities.Diagnostics.Trace;
using ITI.Nlayerd.Infrastructure.Data.UTour.Context;
using ITI.Nlayerd.Infrastructure.Data.UTour.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Infrastructure.Data.UTour.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LayerHotSpot_HotSpot_Repository_TestMethod()
        {
            var repository = new LayerHotSpotsRepository(new UTOURDBEntities(), new TraceManager());
            var hotspotsCount = repository.GetFilteredElements(layerhotspot => layerhotspot.poiType == "geo").Count();
            Assert.AreEqual(4, hotspotsCount);
        }
        //GetDistanceBetweenPoints

        [TestMethod]
        public void LayerHotSpot_HotSpot_Repository_TestMethod2()
        {
            var repository = new LayerHotSpotsRepository(new UTOURDBEntities(), new TraceManager());
            var hotspotsCount = repository.GetFilteredElements(layerhotspot => layerhotspot.poiType == "geo").Count(layerhotspot => MesuringDistanceAlgorithms.
                                                    GetDistanceBetweenPoints(
                                                        Convert.ToDouble("30.0473430000"),
                                                        Convert.ToDouble("31.2336260000"),
                                                        Convert.ToDouble((decimal)layerhotspot.lat.Value),
                                                        Convert.ToDouble((decimal)layerhotspot.lon.Value))
                                                <= (Convert.ToDouble("500")));
            Assert.AreEqual(4, hotspotsCount);
        }


        [TestMethod]
        public void LayerHotSpot_HotSpot_Repository_TestMethod3()
        {
            using (var repository = new LayerHotSpotsRepository(new UTOURDBEntities(), new TraceManager()))
            {
                var hotspots = repository.GetFilteredElements(
                    layerhotspot => layerhotspot.poiType == "geo" && layerhotspot.Id.StartsWith("geo"))
                    .Select(layerhotspot => new
                    {
                        ID = layerhotspot.Id,
                        Distance = MesuringDistanceAlgorithms.
                    GetDistanceBetweenPoints(
                        Convert.ToDouble("30.0732721000"),
                        Convert.ToDouble("31.0177597000"),
                        Convert.ToDouble((decimal)layerhotspot.lat.Value),
                        Convert.ToDouble((decimal)layerhotspot.lon.Value))
                    });
                var lst = hotspots.ToList();
                Assert.AreEqual(4, lst.Count);
            }
        }

        [TestMethod]
        public void LayerHotSpot_HotSpot_Repository_SP_TestMethod()
        {
            using (var repository = new LayerHotSpotsRepository(new UTOURDBEntities(), new TraceManager()))
            {
                var hotspots =
                    repository.UnitOfWork.ExecuteQuery<Domain.DataContracts.layerhotspot>(
                        "exec GetHotSpotsWithinDistance @p1,@p2,@p3",
                        new SqlParameter { ParameterName = "p1", Value = 30.0732721000D },
                        new SqlParameter { ParameterName = "p2", Value = 31.0177597000D },
                        new SqlParameter { ParameterName = "p3", Value = 500.0D }
                        );
                var lst = hotspots.ToList();
                Assert.AreEqual(4, lst.Count);
            }
        }

        [TestMethod]
        public void LayerHotSpot_AdminUsersRepository_TestMethod()
        {
            var repository = new AdminUsersRepository(new UTOURDBEntities(), new TraceManager());
            var hotspotsCount = repository.GetAll().Count();
            Assert.AreEqual(4, hotspotsCount);
        }

        [TestMethod]
        public void LayerHotSpot_MonumentPhotosRepository_TestMethod()
        {
            var repository = new MonumentPhotosRepository(new UTOURDBEntities(), new TraceManager());
            var hotspotsCount = repository.GetAll().Count();
            Assert.AreEqual(4, hotspotsCount);
        }

        [TestMethod]
        public void LayerHotSpot_MonumentRatingRepository_TestMethod()
        {
            var repository = new MonumentRatingRepository(new UTOURDBEntities(), new TraceManager());
            var hotspotsCount = repository.GetAll().Count();
            Assert.AreEqual(4, hotspotsCount);
        }


        [TestMethod]
        public void LayerHotSpot_MonumentReviewsRepository_TestMethod()
        {
            var repository = new MonumentReviewsRepository(new UTOURDBEntities(), new TraceManager());
            var hotspotsCount = repository.GetAll().Count();
            Assert.AreEqual(4, hotspotsCount);
        }


        [TestMethod]
        public void LayerHotSpot_MonumentVideosRepository_TestMethod()
        {
            var repository = new MonumentVideosRepository(new UTOURDBEntities(), new TraceManager());
            var hotspotsCount = repository.GetAll().Count();
            Assert.AreEqual(4, hotspotsCount);
        }


        [TestMethod]
        public void LayerHotSpot_TouristRepository_TestMethod()
        {
            var repository = new TouristRepository(new UTOURDBEntities(), new TraceManager());
            var hotspotsCount = repository.GetAll().Count();
            Assert.AreEqual(4, hotspotsCount);
        }

        [TestMethod]
        public void LayerHotSpot_UploadedPhotoRepository_TestMethod()
        {
            var repository = new UploadedPhotoRepository(new UTOURDBEntities(), new TraceManager());
            var hotspotsCount = repository.GetAll().Count();
            Assert.AreEqual(4, hotspotsCount);
        }

    }
}
