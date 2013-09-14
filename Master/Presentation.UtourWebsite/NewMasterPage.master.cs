using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITI.Nlayerd.Infrastructure.Data.UTour.Context;
using ITI.Common.Utilities.Diagnostics.Trace;
using ITI.Nlayerd.Infrastructure.Data.UTour.Repositories;


public partial class NewMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UploadedPhototsUserControl1.Visible = (Session["UserName"] != null);
        LoginUserControl1.Visible = (Session["UserName"] == null);

        if (Session["UserName"] != null)
        {
            UserNameWelcomeMsgLink.Text = "Welcome " + Session["UserName"].ToString();
            LinkButton1.Text = "Log out";
        }
        else
        {
            UserNameWelcomeMsgLink.Text = string.Empty;
            LinkButton1.Text= string.Empty;
        }
    }

    //private UTOURDBEntities ctx = new UTOURDBEntities();
    //private TraceManager traceManager = new TraceManager();

    //private string CatCriteria = null;
    //private string KeywordCriteria = string.Empty;
    //private string cityCriteria = null;

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Session["cityCriteria"] = dropCity.SelectedValue;
        Session["CatCriteria"] = dropCategory.SelectedValue;
        Session["KeywordCriteria"] = txtKeyword.Text;

        Response.Redirect("AdvancedSearch.aspx");
    //    GetDataByKeyword();
    //    /*if (txtKeyword.Text != string.Empty)
    //    {
    //        var hotSpotsRepository = new LayerHotSpotsRepository(ctx, traceManager);
    //        modify the || to be && 
    //        var hotspots = hotSpotsRepository
    //            .GetFilteredElements(layerhotspot =>
    //                                 (txtKeyword.Text == string.Empty || layerhotspot.title.Contains(txtKeyword.Text)) &&
    //                                  (dropCategory.SelectedIndex > 0 && layerhotspot.Id.StartsWith(dropCategory.SelectedValue)))
    //            .Select(h => new {ImageUrl = h.imageurl, Title = h.title, hotSpotID = h.Id});

    //        var hotSpotsList = hotspots.ToList();
    //        hotSpotsDataList.DataSource = hotSpotsList;
    //        hotSpotsDataList.DataBind();
    //    }*/
    }

    //private void GetDataByKeyword()
    //{
    //    CatCriteria = dropCategory.SelectedValue;
    //    cityCriteria = dropCity.SelectedValue;
    //    KeywordCriteria = txtKeyword.Text;

    //    var hotSpotsRepository = new LayerHotSpotsRepository(ctx, traceManager);
    //    var hotspots = hotSpotsRepository
    //        .GetFilteredElements(layerhotspot =>
    //                             (KeywordCriteria == string.Empty || layerhotspot.title.Contains(KeywordCriteria)) &&
    //                             (CatCriteria == string.Empty || layerhotspot.Id.StartsWith(CatCriteria)) &&
    //                             cityCriteria == string.Empty || layerhotspot.City.CityName == cityCriteria)
    //        .Select(h => new { ImageUrl = h.imageurl, Title = h.title, hotSpotID = h.Id });

    //    var hotSpotsList = hotspots.ToList();
    //    hotSpotsDataList.DataSource = hotSpotsList;
    //    hotSpotsDataList.DataBind();
    //}
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["UserName"] = null;
        Response.Redirect("Home.aspx");
    }
}
