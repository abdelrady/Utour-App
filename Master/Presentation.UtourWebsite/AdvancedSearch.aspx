<%@ Page Title="" Language="C#" MasterPageFile="~/NewMasterPage.master" AutoEventWireup="true" CodeFile="AdvancedSearch.aspx.cs" Inherits="AdvancedSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 20px;
            width: 191px;
        }
        .style2
        {
            height: 20px;
            width: 655px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table style="width:80%;">
                    <tr>
                        <td>
                            Keyword&nbsp;
                            <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                        </td>
                        <td>
                Select Category&nbsp;
                <asp:DropDownList ID="dropCategory" runat="server" Height="22px" Width="149px">
                    <asp:ListItem Value="">All</asp:ListItem>
                    <asp:ListItem Value="Isl_">Islamic</asp:ListItem>
                    <asp:ListItem Value="Anc_">Ancient</asp:ListItem>
                    <asp:ListItem Value="Mod_">Modern</asp:ListItem>
                    <asp:ListItem Value="Cop_">Coptic</asp:ListItem>
                    <asp:ListItem Value="Jew_">Jewish</asp:ListItem>
                </asp:DropDownList>
                        </td>
                        <td>
                Select City&nbsp;
                <asp:DropDownList ID="dropCity" runat="server" Height="20px" Width="148px">
                    <asp:ListItem Value="">All</asp:ListItem>
                    <asp:ListItem>Cairo</asp:ListItem>
                    <asp:ListItem>Alexandria</asp:ListItem>
                    <asp:ListItem>Aswan</asp:ListItem>
                    <asp:ListItem>Luxor</asp:ListItem>
                </asp:DropDownList>
                        </td>
                        <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="63px" 
                    Height="27px" onclick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>               
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>               
    </table>

    <table>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style1">

                <br />
                <%--<asp:DataList ID="hotSpotsDataList" runat="server" DataKeyField="hotSpotID" 
                    RepeatColumns="5" cellpadding="4" BackColor="#CCCCCC" 
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" 
                    GridLines="Both" CellSpacing="2" ForeColor="Black">
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <ItemStyle BackColor="White" />
                    <itemtemplate>
                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' Width="200" Height="200" />
                    <br />
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "viewMonumentDetails.aspx?hotSpotID="+Eval("hotSpotID") %>'
                    Text='<%# Eval("Title") %>' Font-Bold="True" ForeColor="#660033"></asp:HyperLink>
                    </itemtemplate>
                    <SelectedItemStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                </asp:DataList>--%>
                


            </td>
            
            <td>
            
            <asp:DataList ID="hotSpotsDataList" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                  <ItemTemplate>
                             <table id="Table1" cellpadding="1" cellspacing="1" visible ="true">
                                   <tr>
                                            <td>
                                            <p align="left">
                                               <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' Width="200px" Height="200px" border="1px"/>   
                                            </p>
                                            </td>
                                            <%--<td width="200px">
                                            <p align="left">
                                                      <asp:Label ID="lblEmpName" runat = "server"< BR > CssClass="LabelStyle" Text='
                                                            <%# DataBinder.Eval(Container.DataItem, "EmpName")%>'> 
                                                      </asp:Label>
                                            </p>
                                            </td>--%>
                                            <td valign="top">
                                            <p align="left" style="width:150px; margin-left:4px" >
                                               <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "viewMonumentDetails.aspx?hotSpotID="+Eval("hotSpotID") %>'
                                               Text='<%# Eval("Title") %>' Font-Bold="True" ForeColor="#660033"></asp:HyperLink>      
                                             </p>
                                             </td>
                                    </tr>
                                   <%--<tr>
                                   <hr style="color:Black; width:70px;"/>
                                   </tr> --%>
                            </table>
                       </ItemTemplate>
</asp:DataList>
            </td>


        </tr>
    </table>
    
    <center><asp:Label ID="lblPages" runat="server" Text="" CssClass="Test"></asp:Label></center>

    <asp:Panel ID="pnlPager" runat="server" Height="20px" Width="190px">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lnkPrev" runat="server" CommandName="Previous" 
            OnClick="Pager_Click" Text="&lt;&lt; Previous"></asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

    <asp:LinkButton ID="lnkNext" runat="server" CommandName = "Next" 
        Text = "Next >>"  OnClick = "Pager_Click"></asp:LinkButton>
</asp:Panel>
</asp:Content>

