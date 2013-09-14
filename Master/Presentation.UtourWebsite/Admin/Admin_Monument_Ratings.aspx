<%@ Page Title="" Language="C#" MasterPageFile="NewAdminMasterPage.master" AutoEventWireup="true" CodeFile="Admin_Monument_Ratings.aspx.cs" Inherits="Admin_Admin_Monument_Ratings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td>

    <asp:GridView ID="GridView_Monument_Ratings" runat="server" DataSourceID="SqlDataSource1" 
                    AutoGenerateColumns="False" DataKeyNames="ID" AllowPaging="True" 
                    AllowSorting="True" HeaderStyle-Width="100px" Width="400px">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ID" />
            <asp:TemplateField HeaderText="Tourist_ID" SortExpression="Tourist_ID">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" 
                        DataSourceID="SqlDataSource1" DataTextField="ID" DataValueField="ID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:UTOURDBConnectionString %>" 
                        SelectCommand="SELECT [ID] FROM [Tourists]"></asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Tourist_ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="hotSpotID" SortExpression="hotSpotID">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList2" runat="server" 
                        DataSourceID="SqlDataSource2" DataTextField="title" DataValueField="Id">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:UTOURDBConnectionString %>" 
                        SelectCommand="SELECT [Id], [title] FROM [Layerhotspots]">
                    </asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("hotSpotID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Rate" HeaderText="Rate" 
                SortExpression="Rate" />
        </Columns>
    </asp:GridView>

            </td>
        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:UTOURDBConnectionString %>" 
                    DeleteCommand="DELETE FROM [Monument_Ratings] WHERE [ID] = @ID" 
                    InsertCommand="INSERT INTO [Monument_Ratings] ([Tourist_ID], [hotSpotID], [Rate]) VALUES (@Tourist_ID, @hotSpotID, @Rate)" 
                    SelectCommand="SELECT * FROM [Monument_Ratings]" 
                    UpdateCommand="UPDATE [Monument_Ratings] SET [Tourist_ID] = @Tourist_ID, [hotSpotID] = @hotSpotID, [Rate] = @Rate WHERE [ID] = @ID">
                    <DeleteParameters>
                        <asp:Parameter Name="ID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Tourist_ID" Type="Int32" />
                        <asp:Parameter Name="hotSpotID" Type="String" />
                        <asp:Parameter Name="Rate" Type="Double" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Tourist_ID" Type="Int32" />
                        <asp:Parameter Name="hotSpotID" Type="String" />
                        <asp:Parameter Name="Rate" Type="Double" />
                        <asp:Parameter Name="ID" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>

    <asp:DetailsView ID="DetailsView_Monument_Ratings" runat="server" Height="50px" 
                    Width="400px" DataSourceID="SqlDataSource1" AutoGenerateRows="False" 
                    DataKeyNames="ID">
        <Fields>
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="Tourist_ID" HeaderText="Tourist_ID" 
                SortExpression="Tourist_ID" />
            <asp:BoundField DataField="hotSpotID" HeaderText="hotSpotID" 
                SortExpression="hotSpotID" />
            <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
            <asp:CommandField ShowInsertButton="True" />
        </Fields>
    </asp:DetailsView>

            </td>
        </tr>
    </table>
</asp:Content>

