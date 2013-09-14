<%@ Page Title="" Language="C#" MasterPageFile="NewAdminMasterPage.master" AutoEventWireup="true" CodeFile="Admin_UploadedPhotos.aspx.cs" Inherits="Admin_Admin_UploadedPhotos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table style="width:100%;">
        <tr>
            <td>

    <asp:GridView ID="GridView_UploadedPhotos" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" 
                    DataSourceID="SqlDataSource1" Width="366px">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                SortExpression="ID" />
            <asp:BoundField DataField="TouristID" HeaderText="TouristID" 
                SortExpression="TouristID" />
            <asp:BoundField DataField="hotspotID" HeaderText="hotspotID" 
                SortExpression="hotspotID" />
            <asp:BoundField DataField="ImageUrl" HeaderText="ImageUrl" 
                SortExpression="ImageUrl" />
        </Columns>
    </asp:GridView>

            </td>
        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:UTOURDBConnectionString %>" 
                    SelectCommand="SELECT * FROM [UploadedPhotos]" 
                    DeleteCommand="DELETE FROM [UploadedPhotos] WHERE [ID] = @ID" 
                    InsertCommand="INSERT INTO [UploadedPhotos] ([ID], [TouristID], [hotspotID], [ImageUrl]) VALUES (@ID, @TouristID, @hotspotID, @ImageUrl)" 
                    UpdateCommand="UPDATE [UploadedPhotos] SET [TouristID] = @TouristID, [hotspotID] = @hotspotID, [ImageUrl] = @ImageUrl WHERE [ID] = @ID">
                    <DeleteParameters>
                        <asp:Parameter Name="ID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="ID" Type="Int32" />
                        <asp:Parameter Name="TouristID" Type="Int32" />
                        <asp:Parameter Name="hotspotID" Type="String" />
                        <asp:Parameter Name="ImageUrl" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="TouristID" Type="Int32" />
                        <asp:Parameter Name="hotspotID" Type="String" />
                        <asp:Parameter Name="ImageUrl" Type="String" />
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

    <asp:DetailsView ID="DetailsView_UploadedPhotos" runat="server" Height="50px" Width="366px" 
                    AutoGenerateRows="False" DataKeyNames="ID" DataSourceID="SqlDataSource1">
        <Fields>
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                SortExpression="ID" />
            <asp:BoundField DataField="TouristID" HeaderText="TouristID" 
                SortExpression="TouristID" />
            <asp:BoundField DataField="hotspotID" HeaderText="hotspotID" 
                SortExpression="hotspotID" />
            <asp:BoundField DataField="ImageUrl" HeaderText="ImageUrl" 
                SortExpression="ImageUrl" />
            <asp:CommandField ShowInsertButton="True" />
        </Fields>
    </asp:DetailsView>

            &nbsp;&nbsp;

            </td>
        </tr>
    </table>
</asp:Content>

