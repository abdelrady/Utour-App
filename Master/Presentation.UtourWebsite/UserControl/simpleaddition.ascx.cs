using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usercontrol_simpleaddition : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int x = int.Parse(TextBox1.Text);
        int y = int.Parse(TextBox1.Text);

        lblresult.Text = (x + y).ToString();
    }
}