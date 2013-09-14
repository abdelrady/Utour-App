using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
//using J4ni.Controls.ServiceReference1;
using Microsoft.Phone.Controls;
using PostReviewCompletedEventArgs = GeoBasedModule.LoginRegRevRateMgmtServiceProxy.PostReviewCompletedEventArgs;
using Service1Client = GeoBasedModule.LoginRegRevRateMgmtServiceProxy.LoginRegRevRateMgmtServiceClient;
using J4ni.Controls.LoginRegRevRateMgmtServiceProxy;
using GeoBasedModule;

namespace PhoneApp1
{
    public partial class PostReview : PhoneApplicationPage
    {
        public PostReview()
        {
            InitializeComponent();
            var temp = App.Current as App;
           // ratingControl1.UserName = temp.userName;
        }
            
        private void btn_Review_Click(object sender, RoutedEventArgs e)
        {
            GeoBasedModule.LoginRegRevRateMgmtServiceProxy.LoginRegRevRateMgmtServiceClient client = new Service1Client();
            if(GetData()!=null)
            {
                client.PostReviewAsync(GetData());
                client.PostReviewCompleted+=new EventHandler<PostReviewCompletedEventArgs>(client_PostReviewCompleted);
            }
        }
        public void client_PostReviewCompleted(object sender, PostReviewCompletedEventArgs e)
        {
            if (e.Result.ErrorMessage == "")
            {
                MessageBox.Show("review is posted sucessfully");
                this.NavigationService.Navigate(new Uri("/AugmentedRealityPage.xaml",UriKind.Relative)); 
            }
            else
            {
                MessageBox.Show("Something went wrong " + e.Result.ErrorMessage);
            }
        }
        public ReviewInfo GetData() 
        {
            ReviewInfo reviewInfo = new ReviewInfo();
            if(txt_Review.Text!="")
            {
                var temp = App.Current as App;
                reviewInfo.UserName = temp.userName;
                reviewInfo.Userreview = txt_Review.Text;
                reviewInfo.Hotspotid = temp.currentTappedPOI.id;
                return reviewInfo;
            }
            else
            {
                return null;
            }
            
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            var temp = App.Current as App;
       //     txt_UserName.Text = "Welcome "+ temp.userName;
        }
    }
}