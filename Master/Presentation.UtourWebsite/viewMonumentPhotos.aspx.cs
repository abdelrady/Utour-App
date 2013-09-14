using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITI.Common.Utilities.Diagnostics.Trace;
using ITI.Nlayerd.Infrastructure.Data.UTour.Context;
using ITI.Nlayerd.Infrastructure.Data.UTour.Repositories;

public partial class Photos : System.Web.UI.Page
{
    private UTOURDBEntities ctx = new UTOURDBEntities();
    private TraceManager traceManager = new TraceManager();
    private string hotSpotID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if(Session["UserName"] ==null)
        //    Response.Redirect("Login.aspx");

        hotSpotID = Request.QueryString["hotSpotID"];
        GetMonumentPhotos();

    }

    private void GetMonumentPhotos()
    {
        var hotSpotsPhotosRepository = new MonumentPhotosRepository(ctx, traceManager);

        var hotspots = hotSpotsPhotosRepository
            .GetFilteredElements(photos => photos.hostSpotID==hotSpotID)
            .Select(photos => new
            {
                hotSpotID = photos.hostSpotID,
                ImageUrl = photos.ImageUrl,
                Description = photos.Description
            });
        if (hotspots.Any())
        {
            hotSpotsDataList.DataSource = hotspots.ToList();
            hotSpotsDataList.DataBind();
        }
        else
        {
            var originalhotspot = new LayerHotSpotsRepository(ctx,traceManager)
            .GetFilteredElements(layerhotspot => layerhotspot.Id == hotSpotID)
            .Select(hotSpot => new
            {
                hotSpotID = hotSpot.Id,
                ImageUrl = hotSpot.imageurl,
                Description = hotSpot.description
            });

            hotSpotsDataList.DataSource = originalhotspot.ToList();
            hotSpotsDataList.DataBind();
        }



        //Response.Write(hotspots.Count());
    }
    protected void Pager_Click(object sender, EventArgs e)
    {

    }
}