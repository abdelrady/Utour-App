using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain.DataContracts;
using ITI.Common.Utilities.Diagnostics.Trace;
using ITI.Nlayerd.Infrastructure.Data.UTour.Context;
using ITI.Nlayerd.Infrastructure.Data.UTour.Repositories;

public partial class ViewUploadedPhotos : System.Web.UI.Page
{
    private UTOURDBEntities ctx = new UTOURDBEntities();
    private TraceManager traceManager = new TraceManager();
    private string userName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Session["PreviousPage"] = "ViewUploadedPhotos.aspx";
            Response.Redirect("Login.aspx");
        }
        userName = Session["UserName"].ToString();
        GetDataByKeyword();
    }
    private void GetDataByKeyword()
    {
        var touristRep = new TouristRepository(ctx, traceManager);
        var firstOrDefault =
            touristRep.GetFilteredElements(tourist => tourist.UserName == userName).FirstOrDefault
                ();
        if (firstOrDefault != null)
        {
            var userID =
                firstOrDefault.ID;
            var uploadedPhotosRep = new UploadedPhotoRepository(ctx, traceManager);
            var photos = uploadedPhotosRep.LoadWith(photo => photo.layerhotspot)
                .Where(photo => photo.TouristID == userID)
                .Select(
                    photo =>
                    new {ImageUrl = photo.ImageUrl, Title = photo.layerhotspot.title, hotSpotID = photo.hotspotID});

            var hotSpotsList = photos.ToList();
            hotSpotsDataList.DataSource = hotSpotsList;
            hotSpotsDataList.DataBind();
        }
    }
}