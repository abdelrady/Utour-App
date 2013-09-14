using System;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Device.Location;
using ITI.Common.Entities;
using Newtonsoft.Json;
using System.IO;
using GeoBasedModule.PhotoUploadProxy;
using Sample1;
using System.Collections.Generic;
using System.Windows.Threading;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using Facebook;
using System.Windows.Media.Imaging;

namespace GeoBasedModule
{

    public class UTourClient
    {
        public event EventHandler<DataReceivedEventArgs> MonumentsDataReceived;
        public event EventHandler<UploadedPhotoEventArgs> CapturedPhotoUploaded;

        public void getPointsOfInterest(LayerQueryParam info)
        {
            // -- http://localhost:7935/HotSpotsMgmtService2.svc/RetrieveHotSpots?lang=en&countryCode=NL&lon=31.0177597&userId=6f85d06929d160a7c8a3cc1ab4b54b87db99f74b&developerId=4441&developerHash=26ec094e19db2c4a82ebafa200ea2a5e87a7d671&version=4.0&radius=2500&timestamp=1286357071952&lat=30.0732721&layerName=layaroffice&accuracy=100
         
            WebClient client = new WebClient(); 
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            string request = string.Format("http://localhost:54131/HotSpotsMgmtService2.svc/RetrieveHotSpots?" +
                                                "lang={0}&countryCode={1}&lon={2}&userId={3}&developerId=4441&developerHash=26ec094e19db2c4a82ebafa200ea2a5e87a7d671&version=4.0&radius={4}&timestamp=1286357071952&lat={5}&layerName=layaroffice&accuracy={6}", 
                                                info.lang, info.countryCode, info.lon, info.userId, info.radius, info.lat,info.accuracy);
            client.DownloadStringAsync(new Uri(request, UriKind.Absolute));
        }

        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            LayerInfo layer = new LayerInfo();
            layer = JsonConvert.DeserializeObject<LayerInfo>(e.Result);

            if (MonumentsDataReceived != null)
                MonumentsDataReceived(this, new DataReceivedEventArgs { POIs = layer.hotspots });
        }


         public byte[] ReadImageBytes(Stream imageStream)
        {
            byte[] imageBytes = new byte[imageStream.Length];
            imageStream.Read(imageBytes, 0, imageBytes.Length);
            return imageBytes;
        }

         public void UploadCapturedPhoto(string UserName, string hotSpotID,byte[] imageBytes) 
         {
           
             PhotoUploadClient uploadClient = new PhotoUploadClient();
             uploadClient.UploadPhotoCompleted += new EventHandler<UploadPhotoCompletedEventArgs>(uploadClient_UploadPhotoCompleted);
             uploadClient.UploadPhotoAsync(UserName, hotSpotID, imageBytes);
         }
         Dictionary<string, object> parameters = new Dictionary<string, object>();
         private FacebookAccess file = LoadSetting("FacebookAccess");
         GeoBasedModule.App app = App.Current as App;
         public static FacebookAccess LoadSetting(string fileName)
         {
             using (var store = IsolatedStorageFile.GetUserStoreForApplication())
             {
                 if (!store.FileExists(fileName))
                     return default(FacebookAccess);
                 try
                 {
                     using (var stream = store.OpenFile(fileName, FileMode.Open, FileAccess.Read))
                     {
                         var serializer = new DataContractSerializer(typeof(object));
                         return (FacebookAccess)serializer.ReadObject(stream);
                     }
                 }
                 catch (Exception e)
                 {
                     Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(e.Message));
                     return default(FacebookAccess);
                 }
             }
         }
         public void SharePhoto(byte[] imageBytes ,string AccessToken, string UserID, string message)
         {

             /*
              *  MemoryStream stream = new MemoryStream((Byte[])value);
            WriteableBitmap bmp = new WriteableBitmap(200, 200);
            bmp.LoadJpeg(stream);
              */

             var fb = new FacebookClient(app.AccessToken);
             fb.PostCompleted += (o, args) =>
             {
                 if (args.Error != null)
                 {
                     if (args.Error.Message.StartsWith("(OAuthException - #190)"))
                     {
                       
                             MessageBox.Show(
                                 "Token has expired please log in againg to get new token");
                     }
                     else
                     {
                        
                             //MessageBox.Show(args.Error.Message, "error", MessageBoxButton.OK);
                         string t = args.Error.Message;
                     }
                     return;
                 }
                 else
                 {
                     Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show("Posted"));

                 }
             };
             var fbUpl = new Facebook.FacebookMediaObject
             {
                 FileName = "TempPhoto.jpg",
                 ContentType = "image/jpg"

             };
             //fbUpl.SetValue(imageBytes);
             
                 Deployment.Current.Dispatcher.BeginInvoke(() =>
                                                               {
                                                                   using (MemoryStream ms = new MemoryStream(imageBytes))
             {
                                                                   WriteableBitmap LoadedPhoto =
                                                                       new WriteableBitmap((int) app.width,
                                                                                           (int) app.height);
                                                                   /*LoadedPhoto.SaveJpeg(ms, (int) app.width,
                                                                                        (int) app.height, 0, 95);*/
                                                                       LoadedPhoto.LoadJpeg(ms);
                                                                   ms.Seek(0, 0);
                                                                   byte[] data2 = new byte[ms.Length];
                                                                   data2 = imageBytes;
                                                                   ms.Read(data2, 0, data2.Length);
                                                                   ms.Close();
                                                                   fbUpl.SetValue(data2);
             }
                                                                   parameters["message"] = message;
                                                                   parameters["file"] = fbUpl;
                                                                   fb.PostAsync("me/photos", parameters);
                                                               });
                 //WriteableBitmap LoadedPhoto = new WriteableBitmap((int)app.width,(int)app.height);
                 //LoadedPhoto.SaveJpeg(ms, (int)app.width, (int)app.height , 0, 95);
                 //ms.Seek(0, 0);
                 //byte[] data2= new byte[ms.Length];
                 //ms.Read(data2, 0, data2.Length);
                 //ms.Close();
                 //fbUpl.SetValue(data2);
             
             //parameters["message"] = message;
             //parameters["file"] = fbUpl;
             //fb.PostAsync("me/photos", parameters);
         }

         
      public   void uploadClient_UploadPhotoCompleted(object sender, UploadPhotoCompletedEventArgs e)
         {
             if (CapturedPhotoUploaded != null)
                 CapturedPhotoUploaded(this, new UploadedPhotoEventArgs { result = e.Result.OperationStatus});
         }

         public HotSpots findNearestPOI(GeoCoordinate userLocation, HotSpots[] onScreen) 
         {
             double minDistance = double.MaxValue;
             double current;
             HotSpots nearestSpot = new HotSpots();

             foreach (HotSpots spot in onScreen) {
                 current = MesuringDistanceAlgorithms.GetDistanceBetweenPoints(userLocation.Latitude, userLocation.Longitude,
                     double.Parse(spot.anchor.geolocation.lat), double.Parse(spot.anchor.geolocation.lon)) ;
                 
                 if (current < minDistance)
                     nearestSpot = spot;
                 
                 minDistance = Math.Min(current, minDistance);
             }
             return nearestSpot;
         }

    }
}
