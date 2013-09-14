<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Login.ascx.cs" Inherits="UserControl_Login" %>
<script language="javascript" type="text/javascript">
// <![CDATA[

// ]]>
</script>

<table width="205" cellpadding=5 cellspacing=0>
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
             <td><b><asp:textbox  id="UserName"   size=14 runat=server /> </td><td>
      <asp:Label id="UserMark" Text="*" style="color:Red;font:12pt verdana, arial" Visible=false runat=server/></td>
           </tr>
           <tr>
             <td><font face="Arial" size="-1">Password: </td>
             <td><input id="Password" type="password" size=14 runat=server></td><td><asp:Label id="PasswordMark" Text="*" style="color:Red;font:12pt verdana, arial" Visible=false runat=server/></td>
           </tr>
           <tr>
             <td></td>
             <td><input id="Submit1" type="submit"  value="Sign In" runat=server onclick="" /></td>
           </tr>
              
           <tr>
             <td colspan=3 align=center>
                
       <asp:Label id="Message" style="color:Red;font:8pt verdana, arial" Visible=false runat=server/>
                   
                
             </td>
           </tr>
         </table>

      </td>
   </tr>
</table> 