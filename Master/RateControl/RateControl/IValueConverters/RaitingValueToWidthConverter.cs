using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SR.MaskDemo.IValueConverters
{
    public class RaitingValueToWidthConverter : IValueConverter
    {
        //The max Width of all stars.
        private const double Max = 170;
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var rate = (double)value;
            //return 100;
             return ((((rate) * Max) / 5) - Max) * -1;
            //return ((((rate) * 1) / 5) - 1) * -1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
