<%@ Page Title="" Language="C#" MasterPageFile="NewAdminMasterPage.master" AutoEventWireup="true" CodeFile="Admin_Monuments_Photos.aspx.cs" Inherits="Admin_Admin_Monuments_Photos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="width:100%;">
        <tr>
            <td>

    <asp:GridView ID="GridView_Monuments_Photos" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" 
                    DataSourceID="SqlDataSource1" style="width: 377px">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ID" />
            <asp:TemplateField HeaderText="hostSpotID" SortExpression="hostSpotID">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList3" runat="server" 
                        DataSourceID="SqlDataSource_photos_edit" DataTextField="title" 
                        DataValueField="Id">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource_photos_edit" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:UTOURDBConnectionString %>" 
                        SelectCommand="SELECT [title], [Id] FROM [Layerhotspots]">
                    </asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("hostSpotID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ImageUrl" HeaderText="ImageUrl" 
                SortExpression="ImageUrl" />
            <asp:BoundField DataField="Description" HeaderText="Description" 
                SortExpression="Description" />
        </Columns>
    </asp:GridView>

            </td>
        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:UTOURDBConnectionString %>" 
                    DeleteCommand="DELETE FROM [Monuments_Photos] WHERE [ID] = @ID" 
                    InsertCommand="INSERT INTO [Monuments_Photos] ([hostSpotID], [ImageUrl], [Description]) VALUES (@hostSpotID, @ImageUrl, @Description)" 
                    SelectCommand="SELECT * FROM [Monuments_Photos]" 
                    UpdateCommand="UPDATE [Monuments_Photos] SET [hostSpotID] = @hostSpotID, [ImageUrl] = @ImageUrl, [Description] = @Description WHERE [ID] = @ID">
                    <DeleteParameters>
                        <asp:Parameter Name="ID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="hostSpotID" Type="String" />
                        <asp:Parameter Name="ImageUrl" Type="String" />
                        <asp:Parameter Name="Description" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="hostSpotID" Type="String" />
                        <asp:Parameter Name="ImageUrl" Type="String" />
                        <asp:Parameter Name="Description" Type="String" />
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

    <asp:DetailsView ID="DetailsView_Monuments_Photos" runat="server" Height="50px" 
                    Width="377px" AutoGenerateRows="False" DataKeyNames="ID" 
                    DataSourceID="SqlDataSource1">
        <Fields>
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ID" />
            <asp:TemplateField HeaderText="hostSpotID" SortExpression="hostSpotID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("hostSpotID") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:DropDownList ID="DropDownList2" runat="server" 
                        DataSourceID="SqlDataSource_New_Photos" DataTextField="title" 
                        DataValueField="Id" SelectedValue='<%# Bind("hostSpotID") %>'>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource_New_Photos" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:UTOURDBConnectionString %>" 
                        SelectCommand="SELECT [Id], [title] FROM [Layerhotspots]">
                    </asp:SqlDataSource>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("hostSpotID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ImageUrl" HeaderText="ImageUrl" 
                SortExpression="ImageUrl" />
            <asp:BoundField DataField="Description" HeaderText="Description" 
                SortExpression="Description" />
            <asp:CommandField ShowInsertButton="True" />
        </Fields>
    </asp:DetailsView>

            </td>
        </tr>
    </table>
</asp:Content>

