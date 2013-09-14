<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" Inherits="AdminHomePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/default.css" rel="stylesheet" type="text/css" />
</head>
<body style="background:url(images/img01.gif) repeat-z;">
    <form id="form1" runat="server">
  <div id="UpperBorder"></div>
<div id="logo">
	<div style="width:29%; float:left; position:relative; top:-30px;">
        <img src="images/UTourLogo_04.png" style="height: 104px; width: 243px" /></div>
	<!--<h1><a href="#">Capsicum</a></h1>-->
	<div style="width:70%; float:right"><h2><a href="http://www.freecsstemplates.org/">Be your own guide</a></h2></div>
</div>

<div id="content">
    <div id="main" style="height:400px; width:900px; float:right">
    <div style="position:relative; left:-200px; top:100px" >
    <table cellpadding=5 cellspacing=0 
            style="position:relative; left:500px; top: 0px; width: 234px; float:left ">
   <tr>
      <td align="left" height="25" style="border-color:black;border-style:solid; border-width:1; font-size:20px">
         <font face="Arial" color="Black"><b>Login</b></font>
      </td>
   </tr>
   <tr>
      <td align="center" height="25" style="border-color:black;border-style:solid; border-top:0;border-width:1">

         <table width="100%" style="height: 133px">
           <tr>
             <td><font face="Arial"/>UserName:</td>
             <td><b><asp:TextBox ID="txtUserName" runat="server" Height="28px"></asp:TextBox></b></td>
             <td><asp:Label id="UserMark" Text="*" style="color:Red;font:12pt verdana, arial" Visible="false" runat="server"/>
             </td>
            </tr>
           <tr>
             <td><font face="Arial"/>Password: </td>
             <td>
                 <asp:TextBox ID="txtPassword" runat="server" Height="28px" TextMode="Password"></asp:TextBox></td>
                 <td>
             <asp:Label id="PasswordMark" Text="*" style="color:Red;font:12pt verdana, arial" Visible="false" runat="server"/>
             </td>
           </tr>
           <tr>
             <td></td>
             <td>
                 <asp:Button ID="btnLogin" runat="server" Text="Log In" 
                     onclick="btnLogin_Click" Height="31px" Width="57px" /></td>
           </tr>
           <tr>
             <td colspan="3" align="center">
                
       <asp:Label id="lblMessage" style="color:Red;font:12pt verdana, arial" Visible="false" runat="server"/>  
             </td>
           </tr>
         </table>
      </td>
   </tr>
</table>
</div>
		
	</div>
	<%--<div id="extra" style="width:900px; float:right"></div>--%>
</div> 


<div id="footer">
	<p style="font-size:15px" id="legal">Copyright &copy; 2012 UTour All Rights Reserved. Designed by UTour Team.</p>
	<p style="font-size:15px" id="links"><a href="#">Privacy Policy</a> | <a href="#">Terms of Use</a></p>
</div>
<%--<div align="center">This template  downloaded form <a href='http://all-free-download.com/free-website-templates/'>free website templates</a></div>--%>
<div id="FootBorder"></div>
    </form>
</body>
</html>
