<%@ Page Title="" Language="C#" MasterPageFile="NewAdminMasterPage.master" AutoEventWireup="true" CodeFile="Admin_Tourists.aspx.cs" Inherits="Admin_Admin_Tourists" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="width:100%;">
        <tr>
            <td>

    <asp:GridView ID="GridView_Tourists" runat="server" AllowPaging="True" AllowSorting="True" 
                    AutoGenerateColumns="False" DataKeyNames="ID" 
                    DataSourceID="SqlDataSource1" Width="703px">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="UserName" HeaderText="UserName" 
                SortExpression="UserName" />
            <asp:BoundField DataField="Password" HeaderText="Password" 
                SortExpression="Password" HeaderStyle-Width="10%"/>
            <asp:BoundField DataField="First Name" HeaderText="First Name" 
                SortExpression="First Name" />
            <asp:BoundField DataField="Last Name" HeaderText="Last Name" 
                SortExpression="Last Name" />
            <asp:CheckBoxField DataField="Gender" HeaderText="Gender" 
                SortExpression="Gender" />
            <asp:BoundField DataField="Nationality" HeaderText="Nationality" 
                SortExpression="Nationality" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Preferred Language" HeaderText="Preferred Language" 
                SortExpression="Preferred Language" />
        </Columns>
    </asp:GridView>

            </td>
        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:UTOURDBConnectionString %>" 
                    DeleteCommand="DELETE FROM [Tourists] WHERE [ID] = @ID" 
                    InsertCommand="INSERT INTO [Tourists] ([UserName], [Password], [First Name], [Last Name], [Gender], [Nationality], [Email], [Preferred Language]) VALUES (@UserName, @Password, @First_Name, @Last_Name, @Gender, @Nationality, @Email, @Preferred_Language)" 
                    SelectCommand="SELECT * FROM [Tourists]" 
                    UpdateCommand="UPDATE [Tourists] SET [UserName] = @UserName, [Password] = @Password, [First Name] = @First_Name, [Last Name] = @Last_Name, [Gender] = @Gender, [Nationality] = @Nationality, [Email] = @Email, [Preferred Language] = @Preferred_Language WHERE [ID] = @ID">
                    <DeleteParameters>
                        <asp:Parameter Name="ID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="UserName" Type="String" />
                        <asp:Parameter Name="Password" Type="String" />
                        <asp:Parameter Name="First_Name" Type="String" />
                        <asp:Parameter Name="Last_Name" Type="String" />
                        <asp:Parameter Name="Gender" Type="Boolean" />
                        <asp:Parameter Name="Nationality" Type="String" />
                        <asp:Parameter Name="Email" Type="String" />
                        <asp:Parameter Name="Preferred_Language" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="UserName" Type="String" />
                        <asp:Parameter Name="Password" Type="String" />
                        <asp:Parameter Name="First_Name" Type="String" />
                        <asp:Parameter Name="Last_Name" Type="String" />
                        <asp:Parameter Name="Gender" Type="Boolean" />
                        <asp:Parameter Name="Nationality" Type="String" />
                        <asp:Parameter Name="Email" Type="String" />
                        <asp:Parameter Name="Preferred_Language" Type="String" />
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

    <asp:DetailsView ID="DetailsView_Tourists" runat="server" Height="50px"  Width="403px"
                    AutoGenerateRows="False" DataKeyNames="ID" DataSourceID="SqlDataSource1">
        <Fields>
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="UserName" HeaderText="UserName" 
                SortExpression="UserName" />
            <asp:BoundField DataField="Password" HeaderText="Password" 
                SortExpression="Password" />
            <asp:BoundField DataField="First Name" HeaderText="First Name" 
                SortExpression="First Name" />
            <asp:BoundField DataField="Last Name" HeaderText="Last Name" 
                SortExpression="Last Name" />
            <asp:CheckBoxField DataField="Gender" HeaderText="Gender" 
                SortExpression="Gender" />
            <asp:BoundField DataField="Nationality" HeaderText="Nationality" 
                SortExpression="Nationality" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Preferred Language" HeaderText="Preferred Language" 
                SortExpression="Preferred Language" />
            <asp:CommandField ShowInsertButton="True" />
        </Fields>
    </asp:DetailsView>

            </td>
        </tr>
    </table> 
</asp:Content>

