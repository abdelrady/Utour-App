<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="MapWithClickablePushpins.aspx.cs" Inherits="Samples_SimpleMapWithBubble" %>
<%@ Register Src="~/GoogleMapForASPNet.ascx" TagName="GoogleMapForASPNet" TagPrefix="uc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
    
    <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
      <table>
      <tr>
        <td style="width:200px; height:200px">
        <asp:DetailsView ID="MonumentDetailsView" runat="server" 
        AutoGenerateRows="False" CellPadding="4" DataKeyNames="HotSpotID" 
                                                            ForeColor="#333333" 
        GridLines="None" Width="675px" 
                onpageindexchanging="MonumentDetailsView_PageIndexChanging">
                                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                            <CommandRowStyle BackColor="#D1DDF1" Font-Bold="True" />
                                                            <RowStyle BackColor="#EFF3FB" />
                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                            <Fields>
                                                                <asp:BoundField DataField="Title" HeaderText="Title" />
                                                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                                                <asp:BoundField DataField="BriefHistory" HeaderText="Brief History" />
                                                                <asp:ImageField DataImageUrlField="ImageUrl" NullImageUrl="~/nullImage.jpg" ItemStyle-Width="400px">
                                                                </asp:ImageField>
                                                                <asp:HyperLinkField HeaderText="See More Photos" Text="See More Photos" DataNavigateUrlFields="PhotosLink" />
                                                                <asp:HyperLinkField HeaderText="See More Videos" Text="See More Videos" DataNavigateUrlFields="VideosLink" />
                                                                <asp:HyperLinkField HeaderText="Listen To Audio" Text="Listen To Audio" DataNavigateUrlFields="AudioLink" />
                                                                <asp:HyperLinkField HeaderText="See Monument Reviews" Text="See Monument Reviews" DataNavigateUrlFields="ReviewsLink" />
                                                            </Fields>
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <FieldHeaderStyle BackColor="#DEE8F5" Font-Bold="True" />
                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                        </asp:DetailsView>
        </td>
        <td>
     <asp:ScriptManager ID="ScriptManager2" runat="server">
        </asp:ScriptManager>

    <h3><a href="Default.aspx">Back</a></h3>
    <h3>Google Map with clickable pushpins.</h3>
    Click on a pushpin to see it's description.
    <div>
        <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet2" runat="server" />
    </div>
                <%--<iframe width="425" height="350" frameborder="0" scrolling="no" marginheight="0" marginwidth="0"
             src="https://maps.google.com/maps?f=q&amp;source=s_q&amp;hl=ar&amp;q=smart+village+egypt&amp;aq=&amp;sll=30.069103,31.020445&amp;sspn=0.005302,0.009645&amp;ie=UTF8&amp;t=h&amp;st=111962455527842713054&amp;rq=1&amp;ev=p&amp;split=1&amp;radius=0.35&amp;hq=smart+village+egypt&amp;hnear=&amp;ll=37.0625,-95.677068&amp;spn=0.005302,0.009645&amp;output=embed"></iframe><br /><small><a href="https://maps.google.com/maps?f=q&amp;source=embed&amp;hl=ar&amp;q=smart+village+egypt&amp;aq=&amp;sll=30.069103,31.020445&amp;sspn=0.005302,0.009645&amp;ie=UTF8&amp;t=h&amp;st=111962455527842713054&amp;rq=1&amp;ev=p&amp;split=1&amp;radius=0.35&amp;hq=smart+village+egypt&amp;hnear=&amp;ll=37.0625,-95.677068&amp;spn=0.005302,0.009645" style="color:#0000FF;text-align:left">عرض خريطة بحجم أكبر</a></small>--%>

        </td>
      </tr>
    </table>


    <h3><a href="Default.aspx">Back</a></h3>
    <h3>Google Map with clickable pushpins.</h3>
    Click on a pushpin to see it's description.
    <div>
        <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet1" runat="server" />
    </div>
        <h3><a href="Default.aspx">Return to Samples Index</a></h3>
        </asp:Content>