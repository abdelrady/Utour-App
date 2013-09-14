using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Services.Test.HotSpotsMgmtService.ServiceReference;
using Services.Test.LoginRegRevRate.ServiceReference;
using Services.Test.UploadPhotoServiceReference;
using System.IO;

namespace Services.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //UploadPhotoTest();

            //AuthenticateUserTest();
            //PostReviewTest();
            do
            {
                RetrieveHotSpotsTest();
            } while ((Console.ReadLine()) != "Q");
        }

        private static void UploadPhotoTest()
        {
            byte[] imageData =
                File.ReadAllBytes(
                    @"C:\Drive_D\ITI_Cources\GP\Implementation\SourceControlSolution\ITI_NLayered\Presentation.UtourWebsite\images\home.png");

            UploadPhotoServiceReference.PhotoUploadClient client = new PhotoUploadClient();
            var opResult = client.UploadPhoto(new CapturedPicture()
                                                  {FileName = "", ImageBytes = imageData}, "rana", "isl_15");

            Console.WriteLine("Status: " + opResult.OperationStatus + " , Message : " + opResult.ErrorMessage);
        }

        private static void PostReviewTest()
        {
            LoginRegRevRateMgmtServiceClient client =
                new LoginRegRevRateMgmtServiceClient();
            var result = client.PostReview(new ReviewInfo() { UserName = "test", Hotspotid = "geo_1", Userreview = "kowais." });

            Console.WriteLine("Operation Result: " + result.OperationStatus + " , Error: " + result.ErrorMessage);

        }
        static void RetrieveHotSpotsTest()
        {
            var startTime = DateTime.Now;
            var client =
                new HotSpotsManagmentContractClient();

            var hotSpots = client.RetrieveHotSpots("en", "eg", "31.0177597", "4", "", "", "6.2", "500", "", "30.0732721", "test", "100");

            foreach (var hotSpot in hotSpots.hotspots)
            {
                Console.WriteLine(hotSpot.id);
            }
            var secondTime = DateTime.Now;
            Console.WriteLine(secondTime - startTime);
        }
        static void AuthenticateUserTest()
        {
            /*
            var LoginClient = 
                new LoginManagmentContractClient();

            Console.WriteLine(
                LoginClient.AuthenticateUser(new UserAuthInfo()
                                                 {UserName = "rana", Password = "rana", UserType = UserTypes.Admin}));
            */
        }
    }
}
