<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginWebUserControl.ascx.cs" Inherits="UserControl_WebUserControlNew" %>

<%--<table style="width: 30%; float: right; position:relative; top:-50px;right:120px;" >
    <tr>
        <td>
           <asp:Label id="Label1" style="height: 21px; width: 122px;" runat="server">User Name:</asp:Label>
			<asp:TextBox id="edEmailAddress" runat="server" Width="140px" Height="22px"></asp:TextBox>
        </td>
        <td style="margin-left:0px; padding-left:0px;">
           <asp:Label id="Label2" style="height: 21px; width: 122px;" runat="server">Password:</asp:Label>
			<asp:TextBox id="TextBox2" runat="server"  Width="140px" Height="22px" ></asp:TextBox>
        </td>
        <td style="text-align:left">
        <asp:Label id="Label3" style="height: 21px; width: 122px; margin-left:0px; padding-left:0px;" runat="server" Visible="false" > test only  </asp:Label>
         <asp:Button id="btnLogIn" runat="server" Text="Log In" Width="69px" Height="22px"></asp:Button>
        </td>
    </tr>
</table>--%>

<div style="width: 44%; float: right; position:relative; top:-50px;right:120px;">

<div id=123A style="float: right; text-align: left;">
<div style="float:left ;width:150px">
<asp:Label id="Label4" style="height: 21px; width: 122px;" runat="server">User Name:</asp:Label>
</div>

<div style="float:left ; width:215px">
<asp:Label id="Label2" style="height: 21px; width: 122px;" runat="server">Password:</asp:Label>
</div>
</div>

<div id=123B style="float: right; text-align: right; width: 368px;">
<asp:TextBox id="txtUserName" runat="server" Width="140px" Height="22px"></asp:TextBox>
<asp:TextBox id="txtUserPassword" runat="server"  Width="140px" Height="22px" ></asp:TextBox>
<asp:Button id="btnLogin" runat="server" Text="Log In" Width="69px" Height="31px" 
        onclick="btnLogin_Click"></asp:Button>
</div>

<div style="float: right; text-align: left;">
<div style="float:left ;width:150px">
<asp:CheckBox ID="CheckBox1" runat="server" Text="Remember Me" />
</div>
<div style="float:left ;width:215px">
<p><a href="#">Forgot your password?</a></p>
</div>
<br />
    <asp:Label ID="lblErrorMessage" runat="server" Visible="False" ForeColor="Red" 
        Font-Bold="True" Font-Size="Large"></asp:Label>

</div>
    
</div>
<%--style="position:absolute;top:250px;left:1000px;--%>
<%--style="position:absolute;width:50px;height:120px;top:250px;left:1000px;--%>