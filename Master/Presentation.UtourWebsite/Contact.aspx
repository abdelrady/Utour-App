<%@ Page Title="" Language="C#" MasterPageFile="~/NewMasterPage.master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact_Us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 148px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<div>
<div class="mainbar">
        <div class="article">
          <div class="clr"></div>
        </div>
        <div class="article">
          <h2><span>Send us</span> mail</h2>
          <div class="clr"></div>
          <form action="#" method="post" id="sendemail">
            <ol>
              <li>
                <asp:label id="lblname" Runat="server">Name (required)</asp:label>
                <asp:textbox id="txtname" Runat="server"></asp:textbox>
              </li>
              <li>
                <asp:label id="lblemail" Runat="server">Email Address (required)</asp:label>
                <asp:textbox id="txtemail" Runat="server"></asp:textbox>
              </li>
              <li>
                <asp:label id="lblmessage" Runat="server">Your Message</asp:label>
                <asp:textbox id="txtmessage" Runat="server" TextMode="MultiLine" MaxLength="400" 
                      Height="60px" Width="349px"></asp:textbox>
              </li>
              <li>
                <input type="image" name="imageField" id="imageField" src="images/submit.gif" class="send" onclick="return imageField_onclick()" />
                <div class="clr"></div>
                <asp:button id="btnSendmail" Runat="server" Text="Send Mail" OnClick="btnSendmail_Click"></asp:button>
			    <asp:button id="btnReset" Runat="server" Text="Reset" OnClick="btnReset_Click"></asp:button>
              </li>
              <li>
                <asp:label id="lblStatus" Runat="server" EnableViewState="False"></asp:label>
              </li>
            </ol>
          </form>
        </div>
      </div>
</div>--%>

    <div style="position:relative; left:400px; top: 0px; width: 391px;">
        <h3 style="position:relative; left:-15px; top: 0px; width: 399px;">Please enter the following requested information below to send us your comments.</h3>
			<table align="center">
				<tr>
					<td class="style1"><span style="font-family:Verdana; font-size:12px; font-weight:bold;color:Brown;">Your Name:</span></td>
					<td>&nbsp;</td>
				</tr>
                <tr>
					<td colspan="2"><asp:textbox id="txtName" Width="317px" Runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td class="style1">&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td class="style1"><span style="font-family:Verdana; font-size:12px; font-weight:bold;color:Brown;">Your Email Address:</span></td>
					<td>&nbsp;</td>
				</tr>
                <tr>
					<td colspan="2"><asp:textbox id="txtEmail" Width="316px" Runat="server"></asp:textbox></td>
				</tr>
				<tr>
					<td colspan="2" >&nbsp;</td>
				</tr>
				<tr>
					<td colspan="2" ><span style="font-family:Verdana; font-size:12px; font-weight:bold; color:Brown;">Your Comment:</span></td>
				</tr>
				<tr>
					<td align="center" colspan="2" Width="317px"><asp:textbox id="txtMessage" Width="100%" Runat="server" Height="99" TextMode="MultiLine" MaxLength="400"></asp:textbox></td>
				</tr>
				<tr>
					<td colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td align="center" class="style1"><asp:button id="btnSendmail" Runat="server" Text="Send Mail" 
                            OnClick="btnSendmail_Click" Height="31px" Width="103px"></asp:button></td>
					<td align="center"><asp:button id="btnReset" Runat="server" Text="Reset" 
                            OnClick="btnReset_Click" Height="33px" Width="63px"></asp:button></td>
				</tr>
                <tr>
					<td colspan="2">&nbsp;</td>
				</tr>
				<tr>
					<td colspan="2"><asp:label id="lblStatus" Runat="server" EnableViewState="False"></asp:label></td>
				</tr>
			</table>
    </div>

</asp:Content>

