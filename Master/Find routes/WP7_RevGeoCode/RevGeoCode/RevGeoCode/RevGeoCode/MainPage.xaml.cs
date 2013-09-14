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
using Microsoft.Phone.Controls.Maps;
using Microsoft.Phone.Shell;
using System.Diagnostics;
using System.Windows.Controls.Primitives;
using System.Device.Location;
using System.Collections.ObjectModel;
using System.ServiceModel;

using RevGeoCode.BingMapGeoCodeService;

namespace RevGeoCode
{
    public partial class MainPage : PhoneApplicationPage
    {
        bool draggingNow = false;

        Pushpin oneMarker = null;
        GeocodeServiceClient geocodeService = null;

        String ApplicationId = "Change-Me";
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Debug.WriteLine("We are started"); //Check to see if the "Redirect all Output Window text to the Immediate Window" is checked under Tools -> Options -> Debugging -> General.  

            map1.MouseLeftButtonUp += new MouseButtonEventHandler(map_MouseLeftButtonUp);
            map1.MouseMove += new MouseEventHandler(map_MouseMove);
            map1.MapPan += map_MousePan;

            oneMarker = new Pushpin();
            oneMarker.Content = "RevGeoCode";
            oneMarker.Location = map1.Center;
            oneMarker.MouseLeftButtonUp += new MouseButtonEventHandler(oneMarker_MouseLeftButtonUp);
            oneMarker.MouseLeftButtonDown += new MouseButtonEventHandler(oneMarker_MouseLeftButtonDown);
            // moving handled with map mouse move events, also needs to disable panning
            map1.Children.Add(oneMarker);

            geocodeService = new GeocodeServiceClient("BasicHttpBinding_IGeocodeService");
            geocodeService.ReverseGeocodeCompleted += new EventHandler<ReverseGeocodeCompletedEventArgs>(geocodeService_ReverseGeocodeCompleted);
        }

        void map_MousePan(object sender, MapDragEventArgs e)
        {
            Debug.WriteLine("map_MousePan");

            if (oneMarker != null && draggingNow)
            {
                e.Handled = true;
            }
        }

        void map_MouseMove(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("map_MouseMove");

            if (oneMarker != null && draggingNow)
            {
                oneMarker.Location = map1.ViewportPointToLocation(e.GetPosition(map1));
            }
        }

        void map_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("map_MouseLeftButtonUp");
            draggingNow = false;
        }

        void oneMarker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("oneMarker_MouseLeftButtonDown");
            if (oneMarker != null)
            {
                draggingNow = true;
            }
        }

        void oneMarker_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("oneMarker_MouseLeftButtonUp");
            draggingNow = false;
        }

        private void MenuItem_GeoCode_Click(object sender, EventArgs e)
        {
            if ((oneMarker != null) && (geocodeService != null))
            {
                ReverseGeocodeRequest reverseGeocodeRequest = new ReverseGeocodeRequest();

                // Set the credentials using a valid Bing Maps key
                reverseGeocodeRequest.Credentials = new Credentials();
                reverseGeocodeRequest.Credentials.ApplicationId = ApplicationId;

                reverseGeocodeRequest.Location = oneMarker.Location;

                geocodeService.ReverseGeocodeAsync(reverseGeocodeRequest);
            }
        }

        private void geocodeService_ReverseGeocodeCompleted(object sender, ReverseGeocodeCompletedEventArgs e)
        {
            // The result is a GeocodeResponse object
            GeocodeResponse geocodeResponse = e.Result;

            Debug.WriteLine("ResponseSummary.StatusCode: " + geocodeResponse.ResponseSummary.StatusCode);
            Debug.WriteLine("ResponseSummary.FaultReason: " + geocodeResponse.ResponseSummary.FaultReason);

            if (geocodeResponse.Results.Count > 0)
            {
                oneMarker.Content = geocodeResponse.Results[0].DisplayName;

                for (int i = 0; i < geocodeResponse.Results.Count; i++)
                {
                    Debug.WriteLine("Result no.: " + i);
                    Debug.WriteLine("DisplayName: " + geocodeResponse.Results[i].DisplayName);
                    Debug.WriteLine("Address.AddressLine: " + geocodeResponse.Results[i].Address.AddressLine);
                    Debug.WriteLine("Address.AdminDistrict: " + geocodeResponse.Results[i].Address.AdminDistrict);
                    Debug.WriteLine("Address.CountryRegion: " + geocodeResponse.Results[i].Address.CountryRegion);
                    Debug.WriteLine("Address.District: " + geocodeResponse.Results[i].Address.District);
                    Debug.WriteLine("Address.FormattedAddress: " + geocodeResponse.Results[i].Address.FormattedAddress);
                    Debug.WriteLine("Address.Locality: " + geocodeResponse.Results[i].Address.Locality);
                    Debug.WriteLine("Address.PostalCode: " + geocodeResponse.Results[i].Address.PostalCode);
                    Debug.WriteLine("Address.PostalTown: " + geocodeResponse.Results[i].Address.PostalTown);
                    Debug.WriteLine("Confidence: " + geocodeResponse.Results[i].Confidence.ToString());
                    Debug.WriteLine("EntityType: " + geocodeResponse.Results[i].EntityType.ToString());
                }
            }
            else
            {
                oneMarker.Content = "No result, fault: " + geocodeResponse.ResponseSummary.FaultReason;
            }
        }
    }
}