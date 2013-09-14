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
using System.Data;
using System.Data.SqlClient;

public partial class ViewUploadedPhotos2 : System.Web.UI.Page
{
    private UTOURDBEntities ctx = new UTOURDBEntities();
    private TraceManager traceManager = new TraceManager();
    private string userName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] == null)
        {
            Session["PreviousPage"] = "ViewUploadedPhotos.aspx";
            Response.Redirect("InvalidLogin.aspx");
        }
        userName = Session["UserName"].ToString();
        GetDataByKeyword();

        if (!IsPostBack)
        {
            this.Prepare_Pager(TotalRows);
        }
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

    private int CurrentPage = 1;
    private int ItemsPerPage = 4;
   
    int TotalRows = 0;
    
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
        int TotalRows = this.BindList(PageNo);
        int PageCount = this.CalculateTotalPages(TotalRows);
        ViewState["CurrentPage"] = PageNo;
        if (PageNo == 1)
        {
            lnkPrev.Enabled = false;
        }
        else
        {
            lnkPrev.Enabled = true;
        }
        if (PageNo == PageCount)
        {
            lnkNext.Enabled = false;
        }
        else
        {
            lnkNext.Enabled = true;
        }
    }

    private int BindList(int PageNo)
    {
        int TotalRows = 0;
        DataTable dt = new DataTable();
        String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        SqlConnection con = new SqlConnection(strConnString);
        SqlDataAdapter sda = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand("spx_Pager");
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@PageNo", SqlDbType.Int).Value = PageNo;
        cmd.Parameters.Add("@ItemsPerPage", SqlDbType.Int).Value = ItemsPerPage;
        cmd.Parameters.Add("@TotalRows", SqlDbType.Int).Direction = ParameterDirection.Output;
        cmd.Connection = con;
        try
        {
            con.Open();
            sda.SelectCommand = cmd;
            sda.Fill(dt);
            hotSpotsDataList.DataSource = dt;
            hotSpotsDataList.DataBind();
            TotalRows = Convert.ToInt32(cmd.Parameters["@TotalRows"].Value);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            con.Close();
            sda.Dispose();
            con.Dispose();
        }
        return TotalRows;
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