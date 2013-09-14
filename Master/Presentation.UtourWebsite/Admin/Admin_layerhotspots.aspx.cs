using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Admin_layerhotspots : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView_layerhotspots.Columns[6].ItemStyle.Width = Unit.Pixel(50);

    }
}