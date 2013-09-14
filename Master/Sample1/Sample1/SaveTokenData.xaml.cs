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
using System.Windows.Shapes;
using Facebook;
using Microsoft.Phone.Controls;

namespace Sample1
{
    public partial class SaveTokenData : PhoneApplicationPage
    {
        //user_about_me is used for access basic information about the user, 
        //publish_stream permission is used to enables your app to post content, comments,
        //and likes to a user’s stream and to the streams of the user’s friends.and also to upload photo on user time line
        private const string ExtendedPermissions = "user_about_me,publish_stream"; 
        private readonly FacebookClient _fb  = new FacebookClient();
        Dictionary<string,object> parameters = new Dictionary<string, object>();
       
        public SaveTokenData()
        {
            InitializeComponent();
            
        }

        private void Browser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            FacebookOAuthResult oauthResult;
            if(!_fb.TryParseOAuthCallbackUrl(e.Uri,out oauthResult))
            {
                return;
            }
           if(oauthResult.IsSuccess)
           {
               var accessToken = oauthResult.AccessToken;
               LoginSucceded(accessToken);
           }
           else
           {
               MessageBox.Show(oauthResult.ErrorDescription);
           }
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
                                       var result = (IDictionary<string, object>) e.GetResultData();
                                       var id = (string) result["id"];
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


                                       Dispatcher.BeginInvoke(
                                           () => NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative)));
                                   };
            
            fb.GetAsync("me?fields=id");
        }

        private void SaveSetting<T>(string fileName, T dataToSave)
        {
            using(var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    using (var stream = store.CreateFile(fileName))
                    {
                        var serializer = new DataContractSerializer(typeof (T));
                        serializer.WriteObject(stream,dataToSave);
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
       

        //private void DeleteSettings<T>(string fileName)
        //{
        //    using (var store = IsolatedStorageFile.GetUserStoreForApplication())
        //    {
        //        try
        //        {
        //            store.DeleteFile(fileName);
        //        }
        //        catch (Exception e)
        //        {
        //            MessageBox.Show(e.Message);
        //            return;
        //            throw;
        //        }
        //    }
        //}
        private void Browser_Loaded(object sender, RoutedEventArgs e)
        {
            parameters["client_id"] = FacebookSettings.AppID;
            parameters["redirect_uri"] = "https://www.facebook.com/connect/login_success.html";
            parameters["response_type"] = "token";
            parameters["display"] = "page";
            parameters["scope"] = ExtendedPermissions;
            BrowserControl.Navigate(_fb.GetLoginUrl(parameters));
        }
    }
}