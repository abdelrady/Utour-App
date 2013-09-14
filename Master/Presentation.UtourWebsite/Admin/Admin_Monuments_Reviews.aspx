<%@ Page Title="" Language="C#" MasterPageFile="NewAdminMasterPage.master" AutoEventWireup="true" CodeFile="Admin_Monuments_Reviews.aspx.cs" Inherits="Admin_Admin_Monuments_Reviews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="width:100%;">
        <tr>
            <td>

    <asp:GridView ID="GridView_Monuments_Reviews" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" 
                    DataSourceID="SqlDataSource1" Width="367px">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ID" />
            <asp:TemplateField HeaderText="Tourist_ID" SortExpression="Tourist_ID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Tourist_ID") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Tourist_ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="hotSpotID" SortExpression="hotSpotID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("hotSpotID") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("hotSpotID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Review" HeaderText="Review" 
                SortExpression="Review" />
        </Columns>
    </asp:GridView>

            </td>
        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:UTOURDBConnectionString %>" 
                    DeleteCommand="DELETE FROM [Monuments_Reviews] WHERE [ID] = @ID" 
                    InsertCommand="INSERT INTO [Monuments_Reviews] ([Tourist_ID], [hotSpotID], [Review]) VALUES (@Tourist_ID, @hotSpotID, @Review)" 
                    SelectCommand="SELECT * FROM [Monuments_Reviews]" 
                    UpdateCommand="UPDATE [Monuments_Reviews] SET [Tourist_ID] = @Tourist_ID, [hotSpotID] = @hotSpotID, [Review] = @Review WHERE [ID] = @ID">
                    <DeleteParameters>
                        <asp:Parameter Name="ID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Tourist_ID" Type="Int32" />
                        <asp:Parameter Name="hotSpotID" Type="String" />
                        <asp:Parameter Name="Review" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Tourist_ID" Type="Int32" />
                        <asp:Parameter Name="hotSpotID" Type="String" />
                        <asp:Parameter Name="Review" Type="String" />
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

    <asp:DetailsView ID="DetailsView_Monuments_Reviews" runat="server" Height="50px" 
                    Width="367px" AutoGenerateRows="False" DataKeyNames="ID" 
                    DataSourceID="SqlDataSource1">
        <Fields>
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="Tourist_ID" HeaderText="Tourist_ID" 
                SortExpression="Tourist_ID" />
            <asp:BoundField DataField="hotSpotID" HeaderText="hotSpotID" 
                SortExpression="hotSpotID" />
            <asp:BoundField DataField="Review" HeaderText="Review" 
                SortExpression="Review" />
            <asp:CommandField ShowInsertButton="True" />
        </Fields>
    </asp:DetailsView>

            </td>
        </tr>
    </table>

</asp:Content>

