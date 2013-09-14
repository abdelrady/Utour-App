<%@ Page Title="" Language="C#" MasterPageFile="~/NewMasterPage.master" AutoEventWireup="true" CodeFile="viewMonumentReviews.aspx.cs" Inherits="viewMonumentReviews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style4
        {
            width: 207px;
        }
        .style5
        {
            width: 278px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
 <table>
 <tr>
 <td class="style5">
  <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' 
         Width="278px" Height="263px" />
 </td>

 <td class="style4">
     &nbsp;</td>

 <td>
 <%--<asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" 
        GridLines="None" Height="297px" style="margin-left: 84px" Width="550px" 
         ShowHeader="False">
    <AlternatingRowStyle BackColor="White" />
     <Columns>
         <asp:ImageField DataImageUrlField="ImageUrl" ItemStyle-Height="90px" ItemStyle-Width="90px">
         </asp:ImageField>
         <asp:BoundField DataField="UserName" />
         <asp:BoundField DataField="Review" />
     </Columns>
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="white" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#F5F7FB" />
    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
    <SortedDescendingCellStyle BackColor="#E9EBEF" />
    <SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>--%>
 </td>
 </tr>
    </table>
   
	
	<%--<asp:DetailsView ID="MonumentDetailsView" runat="server" 
		AutoGenerateRows="False" CellPadding="4" DataKeyNames="HotSpotID" 
															ForeColor="#333333" 
		GridLines="None" Height="50px"
															Width="400px" AllowPaging="True" 
		onpageindexchanging="MonumentDetailsView_PageIndexChanging">
															<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
															<CommandRowStyle BackColor="#D1DDF1" Font-Bold="True" />
															<RowStyle BackColor="#EFF3FB" />
															<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
															<Fields>
																<asp:BoundField DataField="UserName" HeaderText="User Name" />
																<asp:BoundField DataField="Review" HeaderText="User Comment" />
															</Fields>
															<EditRowStyle BackColor="#2461BF" />
															<FieldHeaderStyle BackColor="#DEE8F5" Font-Bold="True" />
															<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
															<AlternatingRowStyle BackColor="White" />
														</asp:DetailsView>--%>

<asp:DataList ID="hotSpotsDataList" runat="server" DataKeyField="hotSpotID" CssClass="ViewMR"  
           RepeatColumns="1" cellspacing="1" cellpadding="0" Width="600px">
           <FooterStyle BackColor="#CCCCCC" />
           <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
           <ItemStyle BackColor="White" />
        <itemtemplate>
                                            <div>
                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' Width="70" Height="70" />
                                                <br />
                                                <asp:HyperLink ID="HyperLink2" runat="server" Text='<%# Eval("UserName") %>' Font-Bold="True" ForeColor="#660033">
                                                </asp:HyperLink>
                                             </div>
                                            <div align="left" style="position:relative; top:-60px; left:100px; width:700px">           
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Review") %>' CssClass="ViewMRLabel" ></asp:Label>       
                                             </div>
                                    <hr style="width:800px; color:#44576e"/> 
        </itemtemplate>
           <SelectedItemStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
    </asp:DataList>	
</asp:Content>


