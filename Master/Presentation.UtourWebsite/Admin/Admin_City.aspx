<%@ Page Title="" Language="C#" MasterPageFile="NewAdminMasterPage.master" AutoEventWireup="true" CodeFile="Admin_City.aspx.cs" Inherits="Admin_Admin_City" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td>

    <asp:GridView ID="GridView_City" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="CityID" DataSourceID="SqlDataSource1" AllowPaging="True" 
                    Height="213px" Width="254px"  
                    onselectedindexchanged="GridView_City_SelectedIndexChanged" HorizontalAlign="Center">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="CityName" HeaderText="CityName" 
                SortExpression="CityName" HeaderStyle-Width="40%"/>
            <asp:BoundField DataField="CityID" HeaderText="CityID" InsertVisible="False" 
                ReadOnly="True" SortExpression="CityID" HeaderStyle-Width="30%"/>
        </Columns>
    </asp:GridView>

            </td>
        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:UTOURDBConnectionString %>" 
                    DeleteCommand="DELETE FROM [City] WHERE [CityID] = @CityID" 
                    InsertCommand="INSERT INTO [City] ([CityName]) VALUES (@CityName)" 
                    SelectCommand="SELECT * FROM [City]" 
                    UpdateCommand="UPDATE [City] SET [CityName] = @CityName WHERE [CityID] = @CityID">
                    <DeleteParameters>
                        <asp:Parameter Name="CityID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="CityName" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="CityName" Type="String" />
                        <asp:Parameter Name="CityID" Type="Int32" />
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

    <asp:DetailsView ID="DetailsView_City" runat="server"  HorizontalAlign="Center" Height="50px"  Width="254px"
                    AutoGenerateRows="False" DataKeyNames="CityID" DataSourceID="SqlDataSource1">
        <Fields>
            <asp:BoundField DataField="CityName" HeaderText="CityName" 
                SortExpression="CityName" />
            <asp:BoundField DataField="CityID" HeaderText="CityID" InsertVisible="False" 
                ReadOnly="True" SortExpression="CityID" />
            <asp:CommandField ShowInsertButton="True" />
        </Fields>
    </asp:DetailsView>

            </td>
        </tr>
    </table>
</asp:Content>

