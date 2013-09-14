using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ITI.Common.Utilities.Diagnostics.Trace;
using ITI.Nlayerd.Infrastructure.Data.UTour.Context;
using ITI.Nlayerd.Infrastructure.Data.UTour.Repositories;
using System.Windows.Forms;

public partial class Samples_SimpleMapWithBubble : System.Web.UI.Page
{
    private UTOURDBEntities ctx = new UTOURDBEntities();
    private TraceManager traceManager = new TraceManager();
    private string hotSpotID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        //SetMapPos();
        hotSpotID = Request.QueryString["hotSpotID"];
        GetMonumentInfo();
        GetMonumentRate();
        SetMapPositions();
    }

    private void GetMonumentRate()
    {
        var hotSpotRateRepository = new MonumentRatingRepository(ctx, traceManager);
        var arr = hotSpotRateRepository.GetFilteredElements(ratings => ratings.hotSpotID == hotSpotID).Select(ratings => ratings.Rate != null ? Convert.ToInt16(Math.Round(ratings.Rate.Value)) : 0);
        //MessageBox.Show(arr.Count().ToString());
        hotSpotContentRating.ItemId = Guid.NewGuid();
        hotSpotContentRating.DataSource = arr.Any()?arr.ToArray():new int[]{0};
        hotSpotContentRating.DataBind();
    }
    private void SetMapPositions()
    {
        //You must specify Google Map API Key for this component. You can obtain this key from http://code.google.com/apis/maps/signup.html
        //For samples to run properly, set GoogleAPIKey in Web.Config file.
        if (!IsPostBack)
        {
        //    GoogleMapForASPNet2.GoogleMapObject.APIKey = "AIzaSyDU1yfzzxDhkrCs-MnFbMG4aBmpmOebl7Q";
        }

        //GoogleMapForASPNet2.GoogleMapObject.APIVersion = "3";

        //Specify width and height for map. You can specify either in pixels or in percentage relative to it's container.
        //GoogleMapForASPNet2.GoogleMapObject.Width = "800px"; // You can also specify percentage(e.g. 80%) here
        //GoogleMapForASPNet2.GoogleMapObject.Height = "600px";

        //Specify initial Zoom level.
        GoogleMapForASPNet2.GoogleMapObject.ZoomLevel = 14;

        //Specify Center Point for map. Map will be centered on this point.
        GoogleMapForASPNet2.GoogleMapObject.CenterPoint = new GooglePoint("1", lat, lon);

        //Add pushpins for map. 
        //This should be done with initialization of GooglePoint class. 
        //ID is to identify a pushpin. It must be unique for each pin. Type is string.
        //Other properties latitude and longitude.
        GooglePoint GP1 = new GooglePoint { ID = "1", Latitude = lat, Longitude = lon, InfoHTML = title };
        GoogleMapForASPNet2.GoogleMapObject.Points.Add(GP1);

        //GooglePoint GP2 = new GooglePoint();
        //GP2.ID = "2";
        //GP2.Latitude = 43.66619;
        //GP2.Longitude = -79.44268;
        //GP2.InfoHTML = "This is point 2";
        //GoogleMapForASPNet2.GoogleMapObject.Points.Add(GP2);


        //GooglePoint GP3 = new GooglePoint();
        //GP3.ID = "3";
        //GP3.Latitude = 43.67689;
        //GP3.Longitude = -79.43270;
        //GP3.InfoHTML = "This is point 3";
        //GoogleMapForASPNet2.GoogleMapObject.Points.Add(GP3);

    }

    private double lon = 0.0;
    private double lat = 0.0;
    private string title;

    private void GetMonumentInfo()
    {
        var hotSpotsRepository = new LayerHotSpotsRepository(ctx, traceManager);
        var hotspots = hotSpotsRepository
            .GetFilteredElements(layerhotspot =>
                                 layerhotspot.Id == hotSpotID)
            .Select(h => new
            {
                ImageUrl = h.imageurl,
                Title = h.title,
                HotSpotID = h.Id,
                BriefHistory = h.Long_Description,
                Description = h.description,
                PhotosLink = "ViewMonumentPhotos.aspx?hotSpotID=" + hotSpotID,
                VideosLink = "ViewMonumentVideos.aspx?hotSpotID=" + hotSpotID,
                AudioLink = h.Monument_Audio,
                ReviewsLink = "viewMonumentReviews.aspx?hotSpotID=" + hotSpotID,
                Lat = h.lat,
                Lon = h.lon
            });

        MonumentDetailsView.DataSource = hotspots.ToList();
        MonumentDetailsView.DataBind();

        Session["HotSpotImageUrl"] = hotspots.ToList()[0].ImageUrl;

        lat = Convert.ToDouble(hotspots.ToList()[0].Lat.Value);
        lon = Convert.ToDouble(hotspots.ToList()[0].Lon.Value);
        title = hotspots.ToList()[0].Title;

        //Response.Write(Session["HotSpotImageUrl"].ToString());
    }
    protected void MonumentDetailsView_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
    {
    }

//    private void SetMapPos()
//    {
////You must specify Google Map API Key for this component. You can obtain this key from http://code.google.com/apis/maps/signup.html
//        //For samples to run properly, set GoogleAPIKey in Web.Config file.
//        GoogleMapForASPNet2.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];

//        //Specify width and height for map. You can specify either in pixels or in percentage relative to it's container.
//        GoogleMapForASPNet2.GoogleMapObject.Width = "800px"; // You can also specify percentage(e.g. 80%) here
//        GoogleMapForASPNet2.GoogleMapObject.Height = "600px";

//        //Specify initial Zoom level.
//        GoogleMapForASPNet2.GoogleMapObject.ZoomLevel = 14;

//        //Specify Center Point for map. Map will be centered on this point.
//        GoogleMapForASPNet2.GoogleMapObject.CenterPoint = new GooglePoint("1", 43.66619, -79.44268);

//        //Add pushpins for map. 
//        //This should be done with intialization of GooglePoint class. 
//        //ID is to identify a pushpin. It must be unique for each pin. Type is string.
//        //Other properties latitude and longitude.
//        GooglePoint GP1 = new GooglePoint();
//        GP1.ID = "1";
//        GP1.Latitude = 43.65669;
//        GP1.Longitude = -79.44268;
//        GP1.InfoHTML = "This is point 1";
//        GoogleMapForASPNet2.GoogleMapObject.Points.Add(GP1);

//        GooglePoint GP2 = new GooglePoint();
//        GP2.ID = "2";
//        GP2.Latitude = 43.66619;
//        GP2.Longitude = -79.44268;
//        GP2.InfoHTML = "This is point 2";
//        GoogleMapForASPNet2.GoogleMapObject.Points.Add(GP2);


//        GooglePoint GP3 = new GooglePoint();
//        GP3.ID = "3";
//        GP3.Latitude = 43.67689;
//        GP3.Longitude = -79.43270;
//        GP3.InfoHTML = "This is point 3";
//        GoogleMapForASPNet2.GoogleMapObject.Points.Add(GP3);
//    }
}
