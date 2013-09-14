using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Devices;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Matrix = Microsoft.Xna.Framework.Matrix;
using System.Device.Location;
using ITI.Common.Entities;
using Microsoft.Phone.Tasks;
using System.Threading;
using Microsoft.Phone.BackgroundAudio;
using System.Windows.Media.Imaging;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;
using J4ni.Controls.LoginRegRevRateMgmtServiceProxy;
using System.IO;
using Facebook;
using Sample1;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;

namespace GeoBasedModule
{
    public partial class AugmentedRealityPage : PhoneApplicationPage
    {
        #region --Global variables
        PhotoCamera camera;
        Motion motion;
        Viewport viewPort;
        Matrix Projection;
        Matrix View;
        List<Vector3> Points;
        List<UIElement> POIs;
        List<HotSpots> hotSpots;
        double Radius = 100;
        GeoCoordinateWatcher Watcher;
        GeoCoordinate userLocation;
        UTourClient tourGuide;
        HotSpots tappedPOI;
        private readonly CameraCaptureTask _cameraTask;
        UserAuthInfo currentUser;
        private const string ExtendedPermissions = "user_about_me,publish_stream";
        private readonly FacebookClient _fb = new FacebookClient();
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        
        #endregion

        public AugmentedRealityPage()
        {
            InitializeComponent();
            Points = new List<Vector3>();
            POIs = new List<UIElement>();
            _cameraTask = new CameraCaptureTask();
            tourGuide = new UTourClient();
            currentUser = (App.Current as App).user;
            
            //-- test get points of interest and display it to screen
            userLocation = new GeoCoordinate(30.0709602000, 31.0211742000);
            tourGuide.MonumentsDataReceived += new EventHandler<DataReceivedEventArgs>(tourGuide_MonumentsDataReceived);
            tourGuide.getPointsOfInterest(new LayerQueryParam
                {
                    lang = currentUser.Preferred_Language ,
                    countryCode = currentUser.Nationality,
                    lon = userLocation.Longitude.ToString(),
                    userId = currentUser.ID.ToString(),
                    developerId = "4441",
                    developerHash = "26ec094e19db2c4a82ebafa200ea2a5e87a7d671",
                    radius = Radius.ToString(),
                    timestamp = "1286357071952",
                    lat = userLocation.Latitude.ToString(),
                    layerName = "layaroffice",
                    accuracy = "100"
                });
            
        }
        
        public void InitializeViewPort()
        {
            viewPort = new Viewport(0, 0, (int)this.ActualWidth, (int)this.ActualHeight);
            float Aspect = viewPort.AspectRatio;
            Projection = Matrix.CreatePerspectiveFieldOfView(3.14f, Aspect, 1, 100);
            View = Matrix.CreateLookAt(Vector3.UnitZ, Vector3.Zero, Vector3.UnitX);
            // client.getPointsOfInterest(new GeoCoordinate { Longitude = 30.071125, Latitude = 31.0213887 });
        }

        /// <summary>
        /// (1)
        /// The first event to be fired when the application starts, it is used to intialize the camera, and intialize
        /// the motion class to update readings from the mobile sensors, it also intialize the watcher object in 
        /// order to read the user's current location using the GPS
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

            camera = new PhotoCamera();
            ViewFinderBrush.SetSource(camera);
            
            if (Motion.IsSupported)
            {
                motion = new Motion();
                motion.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20);
                motion.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<MotionReading>>(motion_CurrentValueChanged);
                motion.Start();

                Watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
                if (Watcher.Permission == GeoPositionPermission.Granted)
                {
                    Watcher.MovementThreshold = 20;
                    Watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(Watcher_PositionChanged);
                    Watcher.Start();
                }
                else
                {
                    MessageBox.Show("You haven't Grant the device a permission to get your location");
                }
            }
            else
            {
                MessageBox.Show("Your Device Doesn't Support Motion API");
            }

            base.OnNavigatedTo(e);
        }
        
        void Watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (Watcher.Status == GeoPositionStatus.Ready)
            {
                Watcher.Stop();

                userLocation = e.Position.Location;
                tourGuide.MonumentsDataReceived += new EventHandler<DataReceivedEventArgs>(tourGuide_MonumentsDataReceived);
              
                //retrieve surrounding monuments relative to the user's location
                tourGuide.getPointsOfInterest(new LayerQueryParam
                {
                    lang = currentUser.Preferred_Language,
                    countryCode = currentUser.Nationality,
                    lon = userLocation.Longitude.ToString(),
                    userId = currentUser.ID.ToString(),
                    developerId = "4441",
                    developerHash = "26ec094e19db2c4a82ebafa200ea2a5e87a7d671",
                    radius = Radius.ToString(),
                    timestamp = "1286357071952",
                    lat = userLocation.Latitude.ToString(),
                    layerName = "layaroffice",
                    accuracy = "100"
                });

            }
        }

        //add UI element for each point of interest
        void tourGuide_MonumentsDataReceived(object sender, DataReceivedEventArgs e)
        {
           
            hotSpots = e.POIs.ToList();
            //intialize again to match the hotspots retrieved
            POIs = new List<UIElement>();
            Points = new List<Vector3>();

            foreach (var hotSpot in e.POIs)
            {
                GeoCoordinate hotSpotLocation = new GeoCoordinate();
                hotSpotLocation.Longitude = double.Parse(hotSpot.anchor.geolocation.lon);
                hotSpotLocation.Latitude = double.Parse(hotSpot.anchor.geolocation.lat);

                double bearing = Math.Round(ARHelper.CalculateBearing(userLocation, hotSpotLocation), 0);
                addLabel(ARHelper.AngleToVector(bearing, Radius), hotSpot.text.title);
            }

            POIs[0].RenderTransform = new TranslateTransform { X = 0, Y =-10 };
            POIs[0].Visibility = Visibility.Visible;
            POIs[1].RenderTransform = new TranslateTransform { X =250, Y = 50 };
            POIs[1].Visibility = Visibility.Visible;
            tourGuide.MonumentsDataReceived -= tourGuide_MonumentsDataReceived;
        }

        void motion_CurrentValueChanged(object sender, SensorReadingEventArgs<MotionReading> e)
        {
            Dispatcher.BeginInvoke(() => CurrentValueChanged(e.SensorReading));
        }

        void CurrentValueChanged(MotionReading reading)
        {
            if (viewPort.Width == 0)
                InitializeViewPort();


            Matrix RotationMatrix = Matrix.CreateFromYawPitchRoll(-3.059f, -1.53f, 0.369f/*0f,1.57f, 1.57f*/);
            Matrix attitude = Matrix.CreateRotationX(MathHelper.PiOver2) * RotationMatrix;//reading.Attitude.RotationMatrix;


            for (int i = 0; i < Points.Count; i++)
            {
                Matrix world = Matrix.CreateWorld(Points[i], Vector3.UnitZ, Vector3.UnitX);
                Vector3 Projected = viewPort.Project(Vector3.Zero, Projection, View, world * attitude);

                if (Projected.Z > 1 || Projected.Z < 0)
                {
                    POIs[i].Visibility = Visibility.Collapsed;
                }
                else
                {
                    POIs[i].Visibility = Visibility.Visible;
                    TranslateTransform tt = new TranslateTransform();
                    tt.X = Projected.X - (POIs[i].RenderSize.Width / 2);
                    tt.Y = Projected.Y - (POIs[i].RenderSize.Height / 2);
                    POIs[i].RenderTransform = tt;
                }
            }
        }

        void addLabel(Vector3 Position, string name)
        {
            Border border = new Border();
            border.Style = App.Current.Resources["BorderStyle"] as Style;
            TextBlock displayText = new TextBlock();
            displayText.Text = name;
          //  displayText.Tap += new EventHandler<GestureEventArgs>(displayText_Tap);
            
            border.Tap += new EventHandler<GestureEventArgs>(displayText_Tap);
            displayText.Style = App.Current.Resources["textBlockStyle"] as Style;
            border.Visibility = Visibility.Collapsed;
            border.Child = displayText;
            LayoutRoot.Children.Add(border);
            Points.Add(Position);
            POIs.Add(border);
        }

        void displayText_Tap(object sender, GestureEventArgs e)
       
        {
            ActionList.SelectionChanged -= ActionList_SelectionChanged;

            if (BackgroundAudioPlayer.Instance.CanPause)
                BackgroundAudioPlayer.Instance.Pause();

            //ratingControl1.Visibility = Visibility.Visible;
            Border block = sender as Border;
            int index = POIs.IndexOf(block);
            tappedPOI = hotSpots.ElementAt(index);
            ActionList.ItemsSource = tappedPOI.actions;
            MonumentDesc.DataContext = tappedPOI.text;
            var temp = App.Current as App;
            temp.currentTappedPOI = tappedPOI;
           /* ratingControl1.HotSpotID = tappedPOI.id;
            ratingControl1.UserName = temp.userName;
            ratingControl1.AverageRate = tappedPOI.text.rate;*/
            SR.MaskDemo.RatingControl ratingControl2 = new SR.MaskDemo.RatingControl(tappedPOI.text.rate);
            ratingControl2.HotSpotID = tappedPOI.id;
            ratingControl2.UserName = temp.userName;
            //ratingControl2.AverageRate = tappedPOI.text.rate;
            ratingControl2.Width =double.Parse("410");
            //ratingControl2.SetValue(MarginProperty, "0,0,5,0" as object);
            ratingControl2.Margin = new Thickness(0, 0, 0, 0);
            ratingControl2.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            MonumentDesc.Children.Add(ratingControl2);
            ActionList.SelectionChanged += ActionList_SelectionChanged;
        }

        private void Rectangle_Tap(object sender, GestureEventArgs e)
        {
            if (BackgroundAudioPlayer.Instance.CanPause)
                BackgroundAudioPlayer.Instance.Pause();

            _cameraTask.Completed += new EventHandler<PhotoResult>(_cameraTask_Completed);
            _cameraTask.Show();
        }

        void _cameraTask_Completed(object sender, PhotoResult e)
        {

            SharePhotoPopup.IsOpen = true;
            app.CapturedPhoto = tourGuide.ReadImageBytes(e.ChosenPhoto);            
        }

        void tourGuide_CapturedPhotoUploaded(object sender, EventArgs e)
        {
            MessageBox.Show("Photo uploaded on UTour successfully");
            tourGuide.CapturedPhotoUploaded -= tourGuide_CapturedPhotoUploaded;
        }

        private void ActionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BackgroundAudioPlayer.Instance.CanPause)
                BackgroundAudioPlayer.Instance.Pause();

            Actions action = ActionList.SelectedValue as Actions;
            WebBrowserTask task = new WebBrowserTask();
            
            switch (action.label)
            {
                case "More Info":
                    DescriptionPopup.IsOpen = true;
                    DescriptionPopup.DataContext = tappedPOI.text;
                    break;

                case "Listen To Audio":
                    AudioTrack audioTrack = new AudioTrack(new Uri("http://localhost/ganna.mp3"/*action.uri*/), "", "", "", null, "", EnabledPlayerControls.Pause);
                    BackgroundAudioPlayer.Instance.Track = audioTrack;
                    break;

                case "See More Photos":
                    task.Uri = new Uri(action.uri);
                    task.Show();
                    break;

                case "See More Videos":
                    task.Uri = new Uri(action.uri);
                    task.Show();
                    break;

                case "Post Review":
                    this.NavigationService.Navigate(new Uri("/PostReview.xaml", UriKind.Relative));  
                    break; 

                default:
                    break;
            }
        }

        private void PopUpButton_Click(object sender, RoutedEventArgs e)
        {
            DescriptionPopup.IsOpen = false;
        }
       
        GeoBasedModule.App app = App.Current as App;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetHashCode() == FaceBookButton.GetHashCode())
            {
                app.comment = txt_TextToShare.Text;
                app.width = 200;
                app.height = 200;
                NavigationService.Navigate(new Uri("/FacebookLoginPage.xaml", UriKind.Relative));
                
                MessageBox.Show("Photo uploaded on Facebook successfully");
            }
            else
            {
                List<HotSpots> onScreen = new List<HotSpots>();

                for (int i = 0; i < POIs.Count; i++)
                {
                    if (POIs[i].Visibility == Visibility.Visible)
                        onScreen.Add(hotSpots[i]);
                }
                string nearestID = tourGuide.findNearestPOI(userLocation, onScreen.ToArray()).id;
                tourGuide.CapturedPhotoUploaded += new EventHandler<UploadedPhotoEventArgs>(tourGuide_CapturedPhotoUploaded);
                tourGuide.UploadCapturedPhoto(currentUser.UserName, nearestID, app.CapturedPhoto);

            }

            SharePhotoPopup.IsOpen = false;
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            SharePhotoPopup.IsOpen = false;
        }

        private void BrowserControl_Loaded(object sender, RoutedEventArgs e)
        {
            parameters["client_id"] = FacebookSettings.AppID;
            parameters["redirect_uri"] = "https://www.facebook.com/connect/login_success.html";
            parameters["response_type"] = "token";
            parameters["display"] = "page";
            parameters["scope"] = ExtendedPermissions;
        }
     
        private void LoginSucceded(string accessToken)
        {
            var fb = new FacebookClient(accessToken);
            fb.GetCompleted += (o, e) =>
            {
                if (e.Error != null)
                {
                    Dispatcher.BeginInvoke(() => MessageBox.Show(e.Error.Message));
                    return;
                }
                var result = (IDictionary<string, object>)e.GetResultData();
                var id = (string)result["id"];
                //DeleteSettings<FacebookAccess>("FacebookAccess");
                FacebookAccess facebookAccess = new FacebookAccess();
                facebookAccess.UserId = id;
                facebookAccess.AccessToken = accessToken;
                var app = App.Current as App;
                app.AccessToken = accessToken;
                app.UserID = id;
                SaveSetting<FacebookAccess>("FacebookAccess", new FacebookAccess

                {

                    AccessToken = accessToken,

                    UserId = id

                });


                /*Dispatcher.BeginInvoke(
                    () => NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative)));*/
                
                tourGuide.SharePhoto(app.CapturedPhoto, app.AccessToken, app.UserID, txt_TextToShare.Text);
            };

            fb.GetAsync("me?fields=id");
        }

        private void SaveSetting<T>(string fileName, T dataToSave)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    using (var stream = store.CreateFile(fileName))
                    {
                        var serializer = new DataContractSerializer(typeof(T));
                        serializer.WriteObject(stream, dataToSave);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return;
                    throw;
                }
            }
        }
    }
}