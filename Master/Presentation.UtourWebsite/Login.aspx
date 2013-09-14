<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<%@ Register src="UserControl/LoginWebUserControl.ascx" tagname="WebUserControlNew" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

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

