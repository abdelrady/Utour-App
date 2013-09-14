using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Domain.DataContracts;
using ITI.Common.Utilities.Diagnostics.Trace;
using ITI.Nlayerd.Infrastructure.Data.UTour.Context;
using ITI.Nlayerd.Infrastructure.Data.UTour.Repositories;

public partial class viewMonumentReviews : System.Web.UI.Page
{
    private UTOURDBEntities ctx = new UTOURDBEntities();
    private TraceManager traceManager = new TraceManager();
    private string hotSpotID = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Image1.ImageUrl = Session["HotSpotImageUrl"].ToString();
        hotSpotID = Request.QueryString["hotSpotID"];
        GetMonumentReviews();
    }

    private void GetMonumentReviews()
    {
        var monumentReviewsRepository = new MonumentReviewsRepository(ctx, traceManager);
        //var touristRepository = new TouristRepository(ctx, traceManager);

        var hotspotsReview = monumentReviewsRepository.LoadWith(reviews => reviews.Tourist).Where(reviews => 
                                 reviews.hotSpotID == hotSpotID)
            .Select(h => new
            {
                HotSpotID = h.hotSpotID,
                ImageUrl = "images/tourist-icon.gif",
                UserName = h.Tourist.UserName,
                Review = h.Review
            });
       
        //MessageBox.Show(hotspotsReview.Count().ToString(CultureInfo.InvariantCulture));
        
        //MonumentDetailsView.DataSource = hotspotsReview.ToList();
        //MonumentDetailsView.DataBind();
        ////////////GridView1.AutoGenerateColumns = false;
        ////////////GridView1.DataSource = hotspotsReview.ToList();
        ////////////GridView1.DataBind();

       //GridView1.AutoGenerateColumns = false;
        hotSpotsDataList.DataSource = hotspotsReview.ToList();
        hotSpotsDataList.DataBind();

       // GridView1.Columns[1].Visible = false;
    }

    protected void MonumentDetailsView_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
    {

    }
}