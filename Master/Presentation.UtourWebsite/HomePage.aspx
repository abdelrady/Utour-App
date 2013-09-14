<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="HomePage" %>

<%@ Register src="UserControl/Login.ascx" tagname="Login" tagprefix="uc1" %>

<%@ Register src="UserControl/WeatherUserControl.ascx" tagname="WeatherUserControl" tagprefix="uc2" %>

<%@ Register src="UserControl/simpleaddition.ascx" tagname="simpleaddition" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="style.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <uc1:Login ID="Login1" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        </asp:UpdatePanel>
        <br />
                <uc2:WeatherUserControl ID="WeatherUserControl1" runat="server" />

    
    </div>
    </form>
</body>
</html>
