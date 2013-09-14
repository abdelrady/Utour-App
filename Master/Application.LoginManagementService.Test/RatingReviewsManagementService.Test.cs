using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Application.Contracts;
using ITI.Common.Utilities.Diagnostics.Trace;
using ITI.Nlayerd.Infrastructure.Data.UTour.Context;
using ITI.Nlayerd.Infrastructure.Data.UTour.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application.ManagementServices.Test
{
    [TestClass]
    public class RatingReviewsManagementService
    {
        [TestMethod]
        public void PostReview_Test()
        {
            IRatingReviewsManagementService oRatingReviewsManagementService =
                new Impl.RatingReviewsManagementService(new MonumentRatingRepository(new UTOURDBEntities(), new TraceManager()), new MonumentReviewsRepository(new UTOURDBEntities(), new TraceManager()),
                                                        new TouristRepository(new UTOURDBEntities(), new TraceManager()));
            string errorMessage;

            oRatingReviewsManagementService.PostReview("Huda" , "geo_3" , "ya a7med daah test" , out errorMessage);
            Assert.AreEqual(string.Empty , errorMessage);


        }

        [TestMethod]
        public void PostRate_Test()
        {
            IRatingReviewsManagementService oRatingReviewsManagementService =
                new Impl.RatingReviewsManagementService(new MonumentRatingRepository(new UTOURDBEntities(), new TraceManager()), new MonumentReviewsRepository(new UTOURDBEntities(), new TraceManager()),
                                                        new TouristRepository(new UTOURDBEntities(), new TraceManager()));
            string errorMessage;

            oRatingReviewsManagementService.PostRate("Huda", "geo_3", 9, out errorMessage);
            Assert.AreEqual(string.Empty, errorMessage);


        }
    }
}
