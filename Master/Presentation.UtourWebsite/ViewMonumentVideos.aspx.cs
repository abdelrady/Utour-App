using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITI.Common.Utilities.Diagnostics.Trace;
using ITI.Nlayerd.Infrastructure.Data.UTour.Context;
using ITI.Nlayerd.Infrastructure.Data.UTour.Repositories;

public partial class ViewMonumentVideos : System.Web.UI.Page
{
    private UTOURDBEntities ctx = new UTOURDBEntities();
    private TraceManager traceManager = new TraceManager();
    private string hotSpotID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        hotSpotID = Request.QueryString["hotSpotID"];
        GetMonumentVideo();
    }

    private void GetMonumentVideo()
    {
        var hotSpotsVideosRepository = new MonumentVideosRepository(ctx, traceManager);
        var videos = hotSpotsVideosRepository
            .GetFilteredElements(monumentsVideos => monumentsVideos.hostSpotID == hotSpotID)
            .Select(monumentsVideos => monumentsVideos);

        var firstOrDefault = videos.FirstOrDefault();
        hotSpotFlashVideo.VideoURL = firstOrDefault != null ? firstOrDefault.video : "http://abdelrady.110mb.com/Utour/Karnak_and_Luxor_temple_of_ancient_egypt.flv";
        hotSpotFlashVideo.AutoPlay = true;

        hotSpotFlashVideo.ShowControlPanel = true;
        
    }
}