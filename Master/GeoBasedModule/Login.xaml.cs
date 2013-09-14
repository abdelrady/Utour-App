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
using GeoBasedModule.LoginRegRevRateMgmtServiceProxy;
using Microsoft.Phone.Controls;
using LogInCompletedEventArgs = GeoBasedModule.LoginRegRevRateMgmtServiceProxy.LogInCompletedEventArgs;
using Service1Client = GeoBasedModule.LoginRegRevRateMgmtServiceProxy.LoginRegRevRateMgmtServiceClient;
using J4ni.Controls.LoginRegRevRateMgmtServiceProxy;
using GeoBasedModule;

namespace PhoneApp1
{
    public partial class Login : PhoneApplicationPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            GeoBasedModule.LoginRegRevRateMgmtServiceProxy.LoginRegRevRateMgmtServiceClient client = new Service1Client();
            if (GetData() != null)
            {
                client.LogInAsync(GetData());
                client.LogInCompleted+=new EventHandler<LogInCompletedEventArgs>(client_LogInCompleted);
            }
            else
            {
                MessageBox.Show("invalid username/password");
            }

        }
        public void client_LogInCompleted(object sender , LogInCompletedEventArgs e)
        {
            
     
            if (e.Result.isSucceeded == true)
            {

                MessageBox.Show("users is registered");
                var temp = App.Current as App;  
                temp.userName = txt_UserName.Text;
                //temp.user = new UserAuthInfo();
                temp.user = e.userAuthInfo;
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("users is not registered");
            }
            
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/registration.xaml", UriKind.Relative)); 
        }
        public UserAuthInfo GetData()
        {
            UserAuthInfo userInfo = new UserAuthInfo();
            if(txt_UserName.Text!="")
            {
                userInfo.UserName = txt_UserName.Text;
            }
            if(txt_PassWord.Password !="")
            {
                userInfo.Password = txt_PassWord.Password;
            }
            userInfo.UserType = UserTypes.Tourist;
            if (txt_UserName.Text != "" & txt_PassWord.Password != "")
            return userInfo;
            else
            {
                return null;
            }
        }
    }
}