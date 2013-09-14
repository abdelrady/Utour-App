using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Xna.Framework;
using System.Device.Location;


namespace GeoBasedModule
{
   
        public static class ARHelper
        {
            public static double CalculateBearing(GeoCoordinate Venue, GeoCoordinate MyPosition)
            {
                ARHelper.DegreeToRadian(MyPosition.Latitude - Venue.Latitude);

                double num1 = ARHelper.DegreeToRadian(MyPosition.Longitude - Venue.Longitude);
                double num2 = ARHelper.DegreeToRadian(Venue.Latitude);
                double num3 = ARHelper.DegreeToRadian(MyPosition.Latitude);
                double angle = Math.Atan2(Math.Sin(num1) * Math.Cos(num3), Math.Cos(num2)
                    * Math.Sin(num3) - Math.Sin(num2) * Math.Cos(num3) * Math.Cos(num1));

                return ARHelper.RadianToDegree(angle) + 180.0;
            }

            public static Vector3 AngleToVector(double inAngle, double inRadius)
            {
                double num = ARHelper.DegreeToRadian(inAngle - 90.0);
                return new Vector3((float)Math.Round(inRadius * Math.Cos(num)), 0.0f, (float)Math.Round(inRadius * Math.Sin(num)));
            }

            public static double DegreeToRadian(double angle)
            {
                return 3.14159265358979 * angle / 180.0;
            }

            public static double RadianToDegree(double angle)
            {
                return angle * 57.2957795130823;
            }
        }
    
}
