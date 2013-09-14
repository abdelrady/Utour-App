using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using J4ni.Controls.LoginRegRevRateMgmtServiceProxy;
using Microsoft.Phone.Controls;
using ValidationControl;
using RegisterCompletedEventArgs = GeoBasedModule.LoginRegRevRateMgmtServiceProxy.RegisterUserCompletedEventArgs;
using Service1Client = GeoBasedModule.LoginRegRevRateMgmtServiceProxy.LoginRegRevRateMgmtServiceClient;

namespace PhoneApp1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        private String[] Country = {
                                       "Viet Nam", "Japan",
                                       "China", "USA",
                                       "Poland", "Brazil",
                                       "Singapore", "Philipin", "Malaysia"
                                   };

        public String[] PreferedLanguage = {"English", "French", "Arabic",    "Chines", "Japaniese",
                                       "Spanish", "Portugese"
                                       };

        public string email_pattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
                           [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
            + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
                           [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
        public MainPage()
        {
            InitializeComponent();
            this.lpkCountry.ItemsSource = Country;
            this.lpkLanguage.ItemsSource = PreferedLanguage;
            txtUserName.Text = "";
            txt_Password.Password = "";
            txt_Email.Text = "";
            txt_FirstName.Text = "";
            txt_LastName.Text = "";
            txt_Email.Text = "";
           
            this.txt_Email.ValidationRule = new Regex_Val_Rule(email_pattern);
        }




        public void client_RegisterCompleted(object sender, RegisterCompletedEventArgs e)
      {
            if(e.Result=="")
          MessageBox.Show("data send successfully " + e.Result);
            else
            {
                MessageBox.Show("data send successfully " + e.Result);
            }
      }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            GeoBasedModule.LoginRegRevRateMgmtServiceProxy.LoginRegRevRateMgmtServiceClient client = new Service1Client();
            if (GetData() != null)
            {
                client.RegisterUserAsync(GetData());
                client.RegisterUserCompleted+=new EventHandler<RegisterCompletedEventArgs>(client_RegisterCompleted);
           //     client.Get_additionCompleted +=
             //       new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_Get_additionCompleted);
            }

        }

        public UserInfo GetData()
        {
            UserInfo ui = new UserInfo();
            if (txtUserName.Text != "")
            {
                ui.uname = txtUserName.Text;
            }
            if (txt_Password.Password != "")
            {
                ui.pwd = txt_Password.Password;
            }
            if (txt_Email.Text != "" && Regex.IsMatch(txt_Email.Text,email_pattern))
            {
                ui.email = txt_Email.Text;
            }
            if (txt_FirstName.Text != "")
            {
                ui.firstName = txt_FirstName.Text;
            }
            if (txt_LastName.Text != "")
            {
                ui.lastName = txt_LastName.Text;
            }
            if (lpkCountry.SelectedItem.ToString() != "")
            {
                ui.nationality = lpkCountry.SelectedItem.ToString();
            }
            if (rd_female.IsChecked == true || rd_male.IsChecked == true)
            {
                if (rd_male.IsChecked == true)
                    ui.gender = rd_male.Content.ToString();
                else
                {
                    ui.gender = rd_female.Content.ToString();
                }
            }
            if(lpkLanguage.SelectedItem.ToString()!="")
            {
                ui.prefferedlanguage = lpkLanguage.SelectedItem.ToString();
            }
            if (txtUserName.Text != "" & txt_Password.Password != "" & txt_Email.Text != "" & txt_FirstName.Text != "" &
                txt_LastName.Text != "" & lpkCountry.SelectedItem.ToString() != "" &
                (rd_female.IsChecked == true || rd_male.IsChecked == true)&lpkLanguage.SelectedItem.ToString()!="")
            {
                
                return ui;
            }
            else
            {
                MessageBox.Show("please fill all required field and then try again");
            }
            return null;

        }
    }
}