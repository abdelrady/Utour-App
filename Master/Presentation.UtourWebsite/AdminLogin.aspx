<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" Inherits="AdminLogin" %>

<%@ Register src="UserControl/LoginWebUserControl.ascx" tagname="WebUserControlNew" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

<%--<div style="width: 44%; float: right; position:relative; top:-40px;right:120px;">

<div id=123A style="float: right; text-align: left;">
<div style="float:left ;width:150px">
<asp:Label id="Label4" style="height: 21px; width: 122px;" runat="server">User Name:</asp:Label>
</div>

<div style="float:left ; width:215px">
<asp:Label id="Label2" style="height: 21px; width: 122px;" runat="server">Password:</asp:Label>
</div>
</div>

<div id=123B style="float: right; text-align: right; width: 368px;">
<asp:TextBox id="TextBox1" runat="server" Width="140px" Height="22px"></asp:TextBox>
<asp:TextBox id="TextBox2" runat="server"  Width="140px" Height="22px" ></asp:TextBox>
<asp:Button id="Button1" runat="server" Text="Log In" Width="69px" Height="31px"></asp:Button>
</div>

<div style="float: right; text-align: left;">
<div style="float:left ;width:150px">
<asp:CheckBox ID="CheckBox1" runat="server" Text="Remember Me" />
</div>
<div style="float:left ;width:215px">
<p><a href="#">Forgot your password?</a></p>
</div>
</div>
    
</div>--%>
    
    <table width="205" cellpadding=5 cellspacing=0 style="position:relative; left:500px">
   <tr>
      <td align="left" height="25" style="border-color:black;border-style:solid; border-width:1;">
         <font face="Arial" color="Black"><b>Login</b></font>
      </td>
   </tr>
   <tr>
      <td align="center" height="25" style="border-color:black;border-style:solid; border-top:0;border-width:1">

         <table width="100%">
           <tr>
             <td><font face="Arial" size="-1">UserName: </td>
             <td><b><asp:TextBox ID="txtUserName" runat="server"></asp:TextBox></td><td>
              <asp:Label id="UserMark" Text="*" style="color:Red;font:12pt verdana, arial" Visible="false" runat="server"/></td>
           </tr>
           <tr>
             <td><font face="Arial" size="-1">Password: </td>
             <td>
                 <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox></td><td>
             <asp:Label id="PasswordMark" Text="*" style="color:Red;font:12pt verdana, arial" Visible="false" runat="server"/></td>
           </tr>
           <tr>
             <td></td>
             <td>
                 <asp:Button ID="btnLogin" runat="server" Text="Log In" 
                     onclick="btnLogin_Click" /></td>
           </tr>
              
           <tr>
             <td colspan=3 align=center>
                
       <asp:Label id="lblMessage" style="color:Red;font:8pt verdana, arial" Visible=false runat=server/>
                   
                
             </td>
           </tr>
         </table>

      </td>
   </tr>
</table>
</asp:Content>

