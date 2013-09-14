using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Facebook;
using Microsoft.Phone.Controls;
using System.Collections;

namespace Sample1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            FacebookAccess file = LoadSetting("FacebookAccess");
        }
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        private FacebookAccess file = LoadSetting("FacebookAccess");
        Sample1.App app = App.Current as App;

        private void btn_Share_Click(object sender, RoutedEventArgs e)
        {
            
            
            SharePhoto(app.AccessToken,app.UserID, txt_TextToShare.Text);
           

        }

        private void SharePhoto(string  AccessToken,string UserID, string message)
        {
            
            var fb = new FacebookClient(app.AccessToken);
            fb.PostCompleted += (o, args) =>
                                    {
                                        if (args.Error != null)
                                        {
                                            if (args.Error.Message.StartsWith("(OAuthException - #190)"))
                                            {
                                                Dispatcher.BeginInvoke(
                                                    () =>
                                                    MessageBox.Show(
                                                        "Token has expired please log in againg to get new token"));
                                            }
                                            else
                                            {
                                                Dispatcher.BeginInvoke(
                                                    () =>
                                                    MessageBox.Show(args.Error.Message, "error", MessageBoxButton.OK));
                                            }
                                            return;
                                        }
                                        else
                                        {
                                            Dispatcher.BeginInvoke(() => MessageBox.Show("Posted"));
                                        }
                                    };
            var fbUpl = new Facebook.FacebookMediaObject
            {
                FileName = "TempPhoto.jpg",
                ContentType = "image/jpg"

            };
            using (MemoryStream ms = new MemoryStream())
            {
                WriteableBitmap LoadedPhoto = new WriteableBitmap(image1, null);
                LoadedPhoto.SaveJpeg(ms, (int)image1.Width, (int)image1.Height, 0, 95);
                ms.Seek(0, 0);
                byte[] data = new byte[ms.Length];
                ms.Read(data, 0, data.Length);
                ms.Close();
                fbUpl.SetValue(data);
            }
            parameters["message"] = message;
            parameters["file"] = fbUpl;
            fb.PostAsync("me/photos", parameters);
        }

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
    }
}