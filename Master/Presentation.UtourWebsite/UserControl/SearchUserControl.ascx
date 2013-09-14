<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchUserControl.ascx.cs" Inherits="UserControl_SearchUserControl" %>

<div>

<div id="b123" >
<asp:Label id="Label4" style="height: 21px; width: 122px;" runat="server">Search</asp:Label>
<asp:TextBox id="txtKeyword" runat="server" Width="140px" Height="22px"></asp:TextBox>
<asp:Button id="btnSearch" runat="server" Text="Search" Width="69px" Height="31px" 
        onclick="btnSearch_Click"></asp:Button>
</div>

<div>
<a href="AdvancedSearch.aspx">Advanced Search</a>
</div>
    
</div>