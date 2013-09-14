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
using System.ComponentModel;
using J4ni.Controls.LoginRegRevRateMgmtServiceProxy;
using J4ni.Controls.LoginRegRevRateMgmtServiceProxy;
namespace SR.MaskDemo
{
    public partial class RatingControl : UserControl, INotifyPropertyChanged
    {
        readonly Random _r = new Random();

        private double _value;
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged("Value");
                RaisePropertyChanged("ValueFormatted");
            }
        }
        public void FillStars(double val)
        {
            Brush yellow = new SolidColorBrush(Colors.Yellow);
            if (val <= 1)
                star.Fill = yellow;
            if (val > 1 & val <= 2)
            {
                star.Fill = yellow;
                star2.Fill = yellow;
            }
            if (val > 2 & val <= 3)
            {
                star.Fill = yellow;
                star2.Fill = yellow;
                star3.Fill = yellow;
            }
            if (val > 3 & val <= 4)
            {
                star.Fill = yellow;
                star2.Fill = yellow;
                star3.Fill = yellow;
                star4.Fill = yellow;
            }
            if (val > 4 & val <= 5)
            {
                star.Fill = yellow;
                star2.Fill = yellow;
                star3.Fill = yellow;
                star4.Fill = yellow;
                star5.Fill = yellow;
            }
        }
        public string ValueFormatted { get { return "Rating: " + string.Format("{0:0.##}", _value); } }

        public RatingControl()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Value = double.Parse(string.Format("{0:f2}", AverageRate)); 
          
        }
        public RatingControl(string averageRate)
        {
            InitializeComponent();
            this.DataContext = this;
            this.AverageRate = averageRate;
            this.Value = double.Parse(averageRate);
            FillStars(double.Parse(averageRate));
        }

        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        LoginRegRevRateMgmtServiceClient client = new LoginRegRevRateMgmtServiceClient();
        public float Rate;

        public RateInfo GetData()
        {
            //var temp = App.Current as App;
            RateInfo rateInfo = new RateInfo(); 
            rateInfo.HOTSPOTID = HotSpotID;
            rateInfo.USERNAME = UserName;
            rateInfo.RATE = double.Parse(string.Format("{0:f2}", Rate));
            
            return rateInfo;
        }
        public RateInfo GetAverage()
        {
            //var temp = App.Current as App;
            RateInfo rateInfo = new RateInfo();
            rateInfo.HOTSPOTID = HotSpotID;
            rateInfo.USERNAME = UserName;
            rateInfo.RATE = double.Parse(string.Format("{0:f2}", AverageRate));

            return rateInfo;
        }

        public void client_RateCompleted(object sender,J4ni.Controls.LoginRegRevRateMgmtServiceProxy.RateCompletedEventArgs e)
        {
            if (e.Result.errorMessage == "")
            {
                MessageBox.Show("Rate completed successfully your rate is" + e.Result.Rate);
                border.IsHitTestVisible = true;
                border2.IsHitTestVisible = true;
                border3.IsHitTestVisible = true;
                border4.IsHitTestVisible = true;
                border5.IsHitTestVisible = true;
            }
            else
            {
                Brush black = new SolidColorBrush(Colors.Black);
                MessageBox.Show("Something went wrong please check the error message" + e.Result.errorMessage + UserName);
                this.Value = 0;
                border.IsHitTestVisible = true;
                border2.IsHitTestVisible = true;
                border3.IsHitTestVisible = true;
                border4.IsHitTestVisible = true;
                border5.IsHitTestVisible = true;
                star.Fill = black;
                star2.Fill = black;
                star3.Fill = black;
                star4.Fill = black;
                star5.Fill = black;

            }
        }
        private void BtnRandomClick(object sender, RoutedEventArgs e)
        {
           
            this.Value = _r.NextDouble() * 5;
        }

        private void border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Brush b = new SolidColorBrush(Colors.Yellow);
            //modifiedColor.
            if (sender.GetHashCode() == border.GetHashCode())
            {
                if (border4.IsHitTestVisible == true & border3.IsHitTestVisible == true &
                    border2.IsHitTestVisible == true & border5.IsHitTestVisible == true)
                {
                   
                    
                    this.Value = e.GetPosition(border).X / border5.Width;//((((border5.Width) - e.GetPosition(border).X)/(border5.Width*1))*1);
                    
                    star.Fill = b;
                    Rate = (float) Convert.ToDecimal(this.Value);
                    border.IsHitTestVisible = false;
                    border2.IsHitTestVisible = false;
                    border3.IsHitTestVisible = false;
                    border4.IsHitTestVisible = false;
                    border5.IsHitTestVisible = false;
                }
            }
            if (sender.GetHashCode() == border2.GetHashCode())
            {
                if (border5.IsHitTestVisible == true & border3.IsHitTestVisible == true & border4.IsHitTestVisible == true & border.IsHitTestVisible == true)
                {
                    this.Value = ((((border5.Width) - e.GetPosition(border).X) / (border5.Width * 1)) * -1)+1;//((e.GetPosition(star5).X/(star5.Width*1))*-1) + 1;
                    star.Fill = b;
                    star2.Fill = b;
                    Rate = (float)Convert.ToDecimal(this.Value);
                    border5.IsHitTestVisible = false;
                    border3.IsHitTestVisible = false;
                    border2.IsHitTestVisible = false;
                    border.IsHitTestVisible = false;
                    border4.IsHitTestVisible = false;
                }
            }
            if (sender.GetHashCode() == border3.GetHashCode())
            {
                if (border5.IsHitTestVisible == true & border4.IsHitTestVisible == true && border2.IsHitTestVisible == true & border.IsHitTestVisible == true)
                {
                    this.Value = ((((border5.Width) - e.GetPosition(border).X) / (border5.Width * 1)) * -1)+1;//((e.GetPosition(star5).X/(star5.Width*1))*-1)+1;
                    star.Fill = b;
                    star2.Fill = b;
                    star3.Fill = b;
                    Rate = (float)Convert.ToDecimal(this.Value);
                    if (this.Value > 3)
                    {
                        this.Value = 3;
                        Rate = (float)Convert.ToDecimal(this.Value);
                    }
                    border.IsHitTestVisible = false;
                    border2.IsHitTestVisible = false;
                    border3.IsHitTestVisible = false;
                    border4.IsHitTestVisible = false;
                    border5.IsHitTestVisible = false;
                }
            }
            if (sender.GetHashCode() == border4.GetHashCode())
            {
                if (border.IsHitTestVisible == true & border3.IsHitTestVisible == true && border2.IsHitTestVisible == true && border5.IsHitTestVisible == true)
                {
                    this.Value = ((((border5.Width) - e.GetPosition(border).X) / (border5.Width * 1)) * -1)+1;//((e.GetPosition(star5).X/(star5.Width*1))*-1);
                    star.Fill = b;
                    star2.Fill = b;
                    star3.Fill = b;
                    star4.Fill = b;
                    Rate = (float)Convert.ToDecimal(this.Value);
                    if (this.Value > 4)
                    {
                        this.Value = 4;
                        Rate = (float)Convert.ToDecimal(this.Value);
                    }
                    border.IsHitTestVisible = false;
                    border2.IsHitTestVisible = false;
                    border3.IsHitTestVisible = false;
                    border4.IsHitTestVisible = false;
                    border5.IsHitTestVisible = false;
                }

            }
            if (sender.GetHashCode() == border5.GetHashCode())
            {
                if (border2.IsHitTestVisible == true & border3.IsHitTestVisible == true & border4.IsHitTestVisible == true & border.IsHitTestVisible == true)
                {
                    if ((((((border5.Width) - e.GetPosition(border).X) / (border5.Width * 1)) * -1) + 1)>5)
                    {
                        this.Value = 5;
                        Rate = (float)Convert.ToDecimal(this.Value);
                        star.Fill = b;
                        star2.Fill = b;
                        star3.Fill = b;
                        star4.Fill = b;
                        star5.Fill = b;
                    }
                    else
                    {
                        this.Value = ((((border5.Width) - e.GetPosition(border).X) / (border5.Width * 1)) * -1) + 1;//((e.GetPosition(star5).X/(star5.Width*1))*-1);
                        Rate = (float)Convert.ToDecimal(this.Value);
                        star.Fill = b;
                        star2.Fill = b;
                        star3.Fill = b;
                        star4.Fill = b;
                        star5.Fill = b;
                    }
                    
                    

                    border.IsHitTestVisible = false;
                    border2.IsHitTestVisible = false;
                    border3.IsHitTestVisible = false;
                    border4.IsHitTestVisible = false;
                    border5.IsHitTestVisible = false;
                }

            }
            
            client.RateAsync(GetData());
            client.RateCompleted += new EventHandler<RateCompletedEventArgs>(client_RateCompleted);
        }






        private void border_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender.GetHashCode() == border.GetHashCode())
            {
                if (((((border5.Width) - e.GetPosition(border5).X) / (border5.Width * 1)) * 1) > 5)
                    this.Value = 5;
                else
                {
                    this.Value = ((((border5.Width) - e.GetPosition(border5).X) / (border5.Width * 1)) * 1);
                    //((e.GetPosition(star5).X/(star5.Width*1))*-1);
                }
            }
            if (sender.GetHashCode() == border2.GetHashCode())
            {
                this.Value = ((((border5.Width) - e.GetPosition(border5).X) / (border5.Width * 1)) * 1);
                if (this.Value > 4)
                    this.Value = 4;
            }
            if (sender.GetHashCode() == border3.GetHashCode())
            {
                this.Value = ((((border5.Width) - e.GetPosition(border5).X) / (border5.Width * 1)) * 1);
                if (this.Value > 3)
                    this.Value = 3;
            }
            if (sender.GetHashCode() == border4.GetHashCode())
            {
                this.Value = ((((border5.Width) - e.GetPosition(border5).X) / (border5.Width * 1)) * 1);
            }
            if (sender.GetHashCode() == border5.GetHashCode())
            {
                this.Value = ((((border5.Width) - e.GetPosition(border5).X) / (border5.Width * 1)) * 1);
            }

        }



        public string UserName { get; set; }
        public string HotSpotID { get; set; }
        public string AverageRate
        {
            get;
            set;

            //set
            //{

            //    if (PropertyChanged != null)
            //        PropertyChanged(this, new PropertyChangedEventArgs(propName));
            //}
            //get { return AverageRate; }
            //set { value = 0.ToString(); }
        }
    }
}
