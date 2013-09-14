<%@ Page Title="" Language="C#" MasterPageFile="NewAdminMasterPage.master" AutoEventWireup="true" CodeFile="Admin_layerhotspots.aspx.cs" Inherits="Admin_Admin_layerhotspots" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style4
        {
            height: 121px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table style="width:100%;">
        <tr>
            <td class="style4">

    <%--<asp:GridView ID="GridView_layerhotspots" runat="server" DataSourceID="SqlDataSource1" 
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
                    DataKeyNames="Id" Width="200px">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" 
                SortExpression="Id" />
            <asp:BoundField DataField="footnote" HeaderText="footnote" 
                SortExpression="footnote" />
            <asp:BoundField DataField="title" HeaderText="title" SortExpression="title" />
            <asp:BoundField DataField="lat" HeaderText="lat" SortExpression="lat" />
            <asp:BoundField DataField="lon" HeaderText="lon" SortExpression="lon" />
            <asp:BoundField DataField="imageurl" HeaderText="imageurl" 
                SortExpression="imageurl" ItemStyle-Wrap="true" ControlStyle-Width="20px" ItemStyle-Width="20px" />
            <asp:BoundField DataField="description" HeaderText="description" 
                SortExpression="description" />
            <asp:BoundField DataField="biwStyle" HeaderText="biwStyle" 
                SortExpression="biwStyle" />
            <asp:BoundField DataField="alt" HeaderText="alt" SortExpression="alt" />
            <asp:BoundField DataField="doNotIndex" HeaderText="doNotIndex" 
                SortExpression="doNotIndex" />
            <asp:BoundField DataField="showSmallBiw" HeaderText="showSmallBiw" 
                SortExpression="showSmallBiw" />
            <asp:BoundField DataField="showBiwOnClick" HeaderText="showBiwOnClick" 
                SortExpression="showBiwOnClick" />
            <asp:BoundField DataField="poiType" HeaderText="poiType" 
                SortExpression="poiType" />
            <asp:BoundField DataField="Monument_Audio" HeaderText="Monument_Audio" 
                SortExpression="Monument_Audio" />
            <asp:BoundField DataField="Long_Description" HeaderText="Long_Description" 
                SortExpression="Long_Description" />
            <asp:TemplateField HeaderText="CityID" SortExpression="CityID">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" 
                        DataSourceID="SqlDataSource1" DataTextField="CityID" DataValueField="CityID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:UTOURDBConnectionString %>" 
                        SelectCommand="SELECT [CityID] FROM [City]"></asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("CityID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>--%>

    <asp:GridView ID="GridView_layerhotspots" runat="server" DataSourceID="SqlDataSource1" 
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
                    DataKeyNames="Id" Width="200px">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" 
                SortExpression="Id" />
            <asp:BoundField DataField="title" HeaderText="title" SortExpression="title" />
            <asp:BoundField DataField="lat" HeaderText="lat" SortExpression="lat" />
            <asp:BoundField DataField="lon" HeaderText="lon" SortExpression="lon" />
            <asp:BoundField DataField="imageurl" HeaderText="imageurl" 
                SortExpression="imageurl" ItemStyle-Wrap="true" ControlStyle-Width="20px" ItemStyle-Width="20px" />
            <asp:BoundField DataField="description" HeaderText="description" 
                SortExpression="description" />
            
            <asp:BoundField DataField="Monument_Audio" HeaderText="Monument_Audio" 
                SortExpression="Monument_Audio" />
            <asp:BoundField DataField="Long_Description" HeaderText="Long_Description" 
                SortExpression="Long_Description" />
            <asp:TemplateField HeaderText="CityID" SortExpression="CityID">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" 
                        DataSourceID="SqlDataSource1" DataTextField="CityID" DataValueField="CityID">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:UTOURDBConnectionString %>" 
                        SelectCommand="SELECT [CityID] FROM [City]"></asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("CityID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:UTOURDBConnectionString %>" 
                    DeleteCommand="DELETE FROM [Layerhotspots] WHERE [Id] = @Id" 
                    InsertCommand="INSERT INTO [Layerhotspots] ([Id], [footnote], [title], [lat], [lon], [imageurl], [description], [biwStyle], [alt], [doNotIndex], [showSmallBiw], [showBiwOnClick], [poiType], [Monument_Audio], [Long_Description], [CityID]) VALUES (@Id, @footnote, @title, @lat, @lon, @imageurl, @description, @biwStyle, @alt, @doNotIndex, @showSmallBiw, @showBiwOnClick, @poiType, @Monument_Audio, @Long_Description, @CityID)" 
                    SelectCommand="SELECT * FROM [Layerhotspots]" 
                    UpdateCommand="UPDATE [Layerhotspots] SET [footnote] = @footnote, [title] = @title, [lat] = @lat, [lon] = @lon, [imageurl] = @imageurl, [description] = @description, [biwStyle] = @biwStyle, [alt] = @alt, [doNotIndex] = @doNotIndex, [showSmallBiw] = @showSmallBiw, [showBiwOnClick] = @showBiwOnClick, [poiType] = @poiType, [Monument_Audio] = @Monument_Audio, [Long_Description] = @Long_Description, [CityID] = @CityID WHERE [Id] = @Id">
                    <DeleteParameters>
                        <asp:Parameter Name="Id" Type="String" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Id" Type="String" />
                        <asp:Parameter Name="footnote" Type="String" />
                        <asp:Parameter Name="title" Type="String" />
                        <asp:Parameter Name="lat" Type="Decimal" />
                        <asp:Parameter Name="lon" Type="Decimal" />
                        <asp:Parameter Name="imageurl" Type="String" />
                        <asp:Parameter Name="description" Type="String" />
                        <asp:Parameter Name="biwStyle" Type="String" />
                        <asp:Parameter Name="alt" Type="Int32" />
                        <asp:Parameter Name="doNotIndex" Type="Byte" />
                        <asp:Parameter Name="showSmallBiw" Type="Byte" />
                        <asp:Parameter Name="showBiwOnClick" Type="Byte" />
                        <asp:Parameter Name="poiType" Type="String" />
                        <asp:Parameter Name="Monument_Audio" Type="String" />
                        <asp:Parameter Name="Long_Description" Type="String" />
                        <asp:Parameter Name="CityID" Type="Int32" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="footnote" Type="String" />
                        <asp:Parameter Name="title" Type="String" />
                        <asp:Parameter Name="lat" Type="Decimal" />
                        <asp:Parameter Name="lon" Type="Decimal" />
                        <asp:Parameter Name="imageurl" Type="String" />
                        <asp:Parameter Name="description" Type="String" />
                        <asp:Parameter Name="biwStyle" Type="String" />
                        <asp:Parameter Name="alt" Type="Int32" />
                        <asp:Parameter Name="doNotIndex" Type="Byte" />
                        <asp:Parameter Name="showSmallBiw" Type="Byte" />
                        <asp:Parameter Name="showBiwOnClick" Type="Byte" />
                        <asp:Parameter Name="poiType" Type="String" />
                        <asp:Parameter Name="Monument_Audio" Type="String" />
                        <asp:Parameter Name="Long_Description" Type="String" />
                        <asp:Parameter Name="CityID" Type="Int32" />
                        <asp:Parameter Name="Id" Type="String" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>

    <asp:DetailsView ID="DetailsView_layerhotspots" runat="server" Height="50px" Width="125px" 
                    DataSourceID="SqlDataSource1" AutoGenerateRows="False" DataKeyNames="Id">
        <Fields>
            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" 
                SortExpression="Id" />
            <asp:BoundField DataField="footnote" HeaderText="footnote" 
                SortExpression="footnote" />
            <asp:BoundField DataField="title" HeaderText="title" SortExpression="title" />
            <asp:BoundField DataField="lat" HeaderText="lat" SortExpression="lat" />
            <asp:BoundField DataField="lon" HeaderText="lon" SortExpression="lon" />
            <asp:BoundField DataField="imageurl" HeaderText="imageurl" 
                SortExpression="imageurl" />
            <asp:BoundField DataField="description" HeaderText="description" 
                SortExpression="description" />
            <asp:BoundField DataField="biwStyle" HeaderText="biwStyle" 
                SortExpression="biwStyle" />
            <asp:BoundField DataField="alt" HeaderText="alt" SortExpression="alt" />
            <asp:BoundField DataField="doNotIndex" HeaderText="doNotIndex" 
                SortExpression="doNotIndex" />
            <asp:BoundField DataField="showSmallBiw" HeaderText="showSmallBiw" 
                SortExpression="showSmallBiw" />
            <asp:BoundField DataField="showBiwOnClick" HeaderText="showBiwOnClick" 
                SortExpression="showBiwOnClick" />
            <asp:BoundField DataField="poiType" HeaderText="poiType" 
                SortExpression="poiType" />
            <asp:BoundField DataField="Monument_Audio" HeaderText="Monument_Audio" 
                SortExpression="Monument_Audio" />
            <asp:BoundField DataField="Long_Description" HeaderText="Long_Description" 
                SortExpression="Long_Description" />
            <asp:BoundField DataField="CityID" HeaderText="CityID" 
                SortExpression="CityID" />
            <asp:CommandField ShowInsertButton="True" />
        </Fields>
    </asp:DetailsView>

            </td>
        </tr>
    </table>

</asp:Content>

