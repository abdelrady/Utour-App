using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITI.Common.Utilities.Diagnostics.Trace;
using ITI.Nlayerd.Infrastructure.Data.UTour.Context;
using ITI.Nlayerd.Infrastructure.Data.UTour.Repositories;

public partial class UserControl_SearchUserControl : System.Web.UI.UserControl
{
    

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdvancedSearch.aspx?keyword="+txtKeyword.Text);
        
    }
}