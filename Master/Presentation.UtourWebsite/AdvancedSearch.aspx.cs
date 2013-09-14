using System;
using System.Linq;
using ITI.Common.Utilities.Diagnostics.Trace;
using ITI.Nlayerd.Infrastructure.Data.UTour.Context;
using ITI.Nlayerd.Infrastructure.Data.UTour.Repositories;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using System.Web.UI;

public partial class AdvancedSearch : System.Web.UI.Page
{
    private UTOURDBEntities ctx = new UTOURDBEntities();
    private TraceManager traceManager = new TraceManager();

    private string CatCriteria = null;
    private string KeywordCriteria = string.Empty;
    private string cityCriteria = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["cityCriteria"] != null)
        {
            dropCity.SelectedValue = Session["cityCriteria"].ToString();
            Session["cityCriteria"] = null;
        }
        if (Session["CatCriteria"] != null)
        {
            dropCategory.SelectedValue = Session["CatCriteria"].ToString();
            Session["CatCriteria"] = null;
        }
        if(Session["KeywordCriteria"] != null) 
        {
            txtKeyword.Text = Session["KeywordCriteria"].ToString();
            Session["KeywordCriteria"] = null;
        }

        if (Request.UrlReferrer != null)
        {
            //MessageBox.Show(Request.UrlReferrer.ToString());
            GetDataByKeyword(0);
        }

        if (ViewState["CurrentPage"] != null)
        {
            this.CurrentPage = Convert.ToInt32(ViewState["CurrentPage"]);
            //MessageBox.Show(CurrentPage.ToString());
        }
        //if (!IsPostBack)
        //{
        //    ViewState["CurrentPage"] = 1;
        //}
        DisableLinkButton(lnkPrev);
        EnableLinkButton(lnkNext);
    }

    private int GetDataByKeyword(int index)
    {
        CatCriteria = dropCategory.SelectedValue;
        cityCriteria = dropCity.SelectedValue;
        KeywordCriteria = txtKeyword.Text;
        //if (KeywordCriteria == string.Empty) MessageBox.Show("KeywordCriteria == string.Empty ");
        //if (CatCriteria == string.Empty) MessageBox.Show("CatCriteria == string.Empty ");
        //if (cityCriteria == string.Empty) MessageBox.Show("cityCriteria == string.Empty ");

        var hotSpotsRepository = new LayerHotSpotsRepository(ctx, traceManager);
        var hotspots = hotSpotsRepository
            .GetFilteredElements(layerhotspot =>
                                 (KeywordCriteria == string.Empty || layerhotspot.title.Contains(KeywordCriteria)) &&
                                 (CatCriteria == string.Empty || layerhotspot.Id.StartsWith(CatCriteria)) &&
                                 (cityCriteria == string.Empty || layerhotspot.city.CityName == cityCriteria))
            .Select(h => new {ImageUrl = h.imageurl, Title = h.title, hotSpotID = h.Id});
        
        var TotalRows = hotspots.Count();
        var hotSpotsList = hotspots.Skip(index * ItemsPerPage).Take(ItemsPerPage).ToList();
        hotSpotsDataList.DataSource = hotSpotsList;
        hotSpotsDataList.DataBind();
        //MessageBox.Show(TotalRows.ToString());
        lblPages.Text = "" +
            (index+1).ToString() + " Of " + CalculateTotalPages(TotalRows);
        return TotalRows;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetDataByKeyword(0);
        /*if (txtKeyword.Text != string.Empty)
        {
            var hotSpotsRepository = new LayerHotSpotsRepository(ctx, traceManager);
            //modify the || to be && 
            var hotspots = hotSpotsRepository
                .GetFilteredElements(layerhotspot =>
                                     (txtKeyword.Text == string.Empty || layerhotspot.title.Contains(txtKeyword.Text)) &&
                                      (dropCategory.SelectedIndex > 0 && layerhotspot.Id.StartsWith(dropCategory.SelectedValue)))
                .Select(h => new {ImageUrl = h.imageurl, Title = h.title, hotSpotID = h.Id});

            var hotSpotsList = hotspots.ToList();
            hotSpotsDataList.DataSource = hotSpotsList;
            hotSpotsDataList.DataBind();
        }*/
    }
    private int CurrentPage = 1;
    private int ItemsPerPage = 9;
    protected void Pager_Click(object sender, EventArgs e)
    {
        LinkButton lnkPager = (LinkButton)sender;
        int PageNo = 1;
        switch (lnkPager.CommandName)
        {
            case "Previous":
                PageNo = this.CurrentPage - 1;
                break;
            case "Next":
                PageNo = this.CurrentPage + 1;
                break;
        }
        int TotalRows = this.GetDataByKeyword(PageNo-1);
        int PageCount = this.CalculateTotalPages(TotalRows);
        ViewState["CurrentPage"] = PageNo;
        if (PageNo == 1)
        {
            //lnkPrev.Enabled = false;
            DisableLinkButton(lnkPrev);
        }
        else
        {
            lnkPrev.Enabled = true;
            EnableLinkButton(lnkPrev);
        }
        if (PageNo == PageCount)
        {
            //lnkNext.Enabled = false;
            DisableLinkButton(lnkNext);
        }
        else
        {
            lnkNext.Enabled = true;
            EnableLinkButton(lnkNext);
        }
    }

    public static void DisableLinkButton(LinkButton linkButton)
    {
        linkButton.Attributes.Remove("href");
        linkButton.Attributes.CssStyle[HtmlTextWriterStyle.Color] = "gray";
        linkButton.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "default";
        if (linkButton.Enabled != false)
        {
            linkButton.Enabled = false;
        }

        if (linkButton.OnClientClick != null)
        {
            linkButton.OnClientClick = null;
        }
    }

    public static void EnableLinkButton(LinkButton linkButton)
    {
        //linkButton.Attributes.Remove("href");
        linkButton.Attributes.CssStyle[HtmlTextWriterStyle.Color] = "blue";
        linkButton.Attributes.CssStyle[HtmlTextWriterStyle.Cursor] = "pointer";
        if (linkButton.Enabled != false)
        {
            linkButton.Enabled = true;
        }

        //if (linkButton.OnClientClick != null)
        //{
        //    linkButton.OnClientClick = null;
        //}
    }

    private void Prepare_Pager(int TotalRows)
    {
        int intPageCount = this.CalculateTotalPages(TotalRows);
        if (intPageCount > 1 && this.CurrentPage < intPageCount)
        {
            this.lnkNext.Enabled = true;
        }
        if (this.CurrentPage != 1)
        {
            this.lnkPrev.Enabled = true;
        }
        else
        {
            this.lnkPrev.Enabled = false;
        }
    }

    private int CalculateTotalPages(int intTotalRows)
    {
        int intPageCount = 1;
        double dblPageCount = (double)(Convert.ToDecimal(intTotalRows) / Convert.ToDecimal(this.ItemsPerPage));
        intPageCount = Convert.ToInt32(Math.Ceiling(dblPageCount));
        return intPageCount;
    }

}