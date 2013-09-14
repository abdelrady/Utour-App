<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/NewMasterPage.master" CodeFile="viewMonumentDetails.aspx.cs" Inherits="Samples_SimpleMapWithBubble" %>

<%@ Register Assembly="Spaanjaars.Toolkit" Namespace="Spaanjaars.Toolkit" TagPrefix="isp" %>
<%@ Register Src="~/GoogleMapForASPNet.ascx" TagName="GoogleMapForASPNet" TagPrefix="uc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
    
    <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <table style="width:100%">
      <tr align="center">
        <td>
        <asp:DetailsView ID="MonumentDetailsView" runat="server" 
        AutoGenerateRows="False" CellPadding="4" DataKeyNames="HotSpotID" 
                                                            ForeColor="#333333" 
        GridLines="None" Width="675px" 
                onpageindexchanging="MonumentDetailsView_PageIndexChanging">
                                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                            <CommandRowStyle BackColor="#D1DDF1" Font-Bold="True" />
                                                            <RowStyle BackColor="#FFFFFF" />
                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                            <Fields>
                                                                <asp:BoundField DataField="Title" HeaderText="" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="18px"  ItemStyle-CssClass="Test1" />
                                                                 <asp:ImageField DataImageUrlField="ImageUrl"  NullImageUrl="~/nullImage.jpg" ItemStyle-CssClass="Test1" ControlStyle-Width="600px" ControlStyle-Height="400px">
                                                                </asp:ImageField>
                                                                <asp:BoundField DataField="Description" HeaderText="" ItemStyle-CssClass="Test1" />
                                                                <asp:BoundField DataField="BriefHistory" HeaderText="" ItemStyle-CssClass="Test1" />
                                                                <asp:HyperLinkField HeaderText="" Text="See More Photos" DataNavigateUrlFields="PhotosLink" ItemStyle-CssClass="Test1" ItemStyle-Font-Underline="true"/>
                                                                <asp:HyperLinkField HeaderText="" Text="See More Videos" DataNavigateUrlFields="VideosLink" ItemStyle-CssClass="Test1" ItemStyle-Font-Underline="true"/>
                                                                <asp:HyperLinkField HeaderText="" Text="Listen To Audio" DataNavigateUrlFields="AudioLink" ItemStyle-CssClass="Test1" ItemStyle-Font-Underline="true"/>
                                                                <asp:HyperLinkField HeaderText="" Text="See Monument Reviews" DataNavigateUrlFields="ReviewsLink" ItemStyle-CssClass="Test1" ItemStyle-Font-Underline="true"/>
                                                            </Fields>
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <FieldHeaderStyle BackColor="#FFFFFF" Font-Bold="True" />
                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                        </asp:DetailsView>
        </td>
      </tr>
      
      <tr align="center">
        <td>
          <asp:ScriptManager ID="ScriptManager2" runat="server">
          </asp:ScriptManager>
<%--
    <h3><a href="Default.aspx">Back</a></h3>
    <h3>Google Map with clickable pushpins.</h3>
    Click on a pushpin to see it's description.--%>
    <div>
        <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet2" runat="server" />
    </div>
                <%--<iframe width="425" height="350" frameborder="0" scrolling="no" marginheight="0" marginwidth="0"
             src="https://maps.google.com/maps?f=q&amp;source=s_q&amp;hl=ar&amp;q=smart+village+egypt&amp;aq=&amp;sll=30.069103,31.020445&amp;sspn=0.005302,0.009645&amp;ie=UTF8&amp;t=h&amp;st=111962455527842713054&amp;rq=1&amp;ev=p&amp;split=1&amp;radius=0.35&amp;hq=smart+village+egypt&amp;hnear=&amp;ll=37.0625,-95.677068&amp;spn=0.005302,0.009645&amp;output=embed"></iframe><br /><small><a href="https://maps.google.com/maps?f=q&amp;source=embed&amp;hl=ar&amp;q=smart+village+egypt&amp;aq=&amp;sll=30.069103,31.020445&amp;sspn=0.005302,0.009645&amp;ie=UTF8&amp;t=h&amp;st=111962455527842713054&amp;rq=1&amp;ev=p&amp;split=1&amp;radius=0.35&amp;hq=smart+village+egypt&amp;hnear=&amp;ll=37.0625,-95.677068&amp;spn=0.005302,0.009645" style="color:#0000FF;text-align:left">عرض خريطة بحجم أكبر</a></small>--%>

         <%--  <h3><a href="Default.aspx">Back</a></h3>
         <h3>Google Map with clickable pushpins.</h3>
         Click on a pushpin to see it's description.--%>
          <%--<div  style="width:10px; height:10px; float:right; position:relative; top:-400px">
           <uc1:GoogleMapForASPNet ID="GoogleMapForASPNet1" runat="server" />
          </div>--%>
        </td>
      </tr>
    </table>
        <%--<h3><a href="Default.aspx">Return to Samples Index</a></h3>--%>
        <div style="position:relative; top:-550px; left:550px; height: 57px; width: 253px;">
            <isp:ContentRating ID="hotSpotContentRating"
            runat="server" LegendText="rates: {0} avg: {1}"/>
        </div>
        </asp:Content>