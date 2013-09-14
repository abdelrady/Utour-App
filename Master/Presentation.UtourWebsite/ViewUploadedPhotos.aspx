<%@ Page Language="C#" MasterPageFile="~/NewMasterPage.master"  AutoEventWireup="true" CodeFile="ViewUploadedPhotos.aspx.cs" Inherits="ViewUploadedPhotos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="jquery.js" type="text/javascript"></script>
<script src="Final.js" type="text/javascript" />
       
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<%--			<asp:DataList ID="hotSpotsDataList" runat="server" DataKeyField="hotSpotID" 
					RepeatColumns="6" cellpadding="15" BackColor="White" 
					BorderColor="black" BorderStyle="None" BorderWidth="1px" 
					GridLines="Both">
					<FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
					<HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
					<ItemStyle BackColor="White" ForeColor="#330099" />
					<itemtemplate>
					<asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' Width="200" Height="200" />
					<br />
					<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "viewPhotoDetails.aspx?hotSpotID="+Eval("hotSpotID") %>'
					Text='<%# "At "+ Eval("Title") %>' Font-Bold="True" ForeColor="#660033"></asp:HyperLink>
					</itemtemplate>
					<SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
				</asp:DataList>--%>

         
            <asp:DataList ID="hotSpotsDataList" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                  <ItemTemplate>
                             <table id="Table1" cellpadding="1" cellspacing="1" visible ="true" style="float:left" >
                                   <tr>
                                            <td>
                                            <div id="main2">
                                            <%--<div  style="width:200px; height:200px">--%>
                                               <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' Width="170px" Height="170px" border="1px"/>   
                                            <%--</div>--%>
                                            </div>
                                            </td>
                                            <td valign="top">
                                           <%-- <p align="left" style="width:150px; margin-left:4px" >--%>
                                               <div>
                                               <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "viewMonumentDetails.aspx?hotSpotID="+Eval("hotSpotID") %>'
                                               Text='<%# Eval("Title") %>' Font-Bold="True" ForeColor="#660033"></asp:HyperLink>      
                                               </div>
                                            <%-- </p>--%>
                                             </td>
                                    </tr>
                            </table>
                       </ItemTemplate>
            </asp:DataList>	


</asp:Content>