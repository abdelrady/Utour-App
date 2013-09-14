<%@ Page Title="" Language="C#" MasterPageFile="NewAdminMasterPage.master" AutoEventWireup="true" CodeFile="Admin_Monuments_Videos.aspx.cs" Inherits="Admin_Admin_Monuments_Videos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 119px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="width:100%;">
        <tr>
            <td class="style1">

    <asp:GridView ID="GridView_Monuments_Videos" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ID" 
                    DataSourceID="SqlDataSource1" Width="470px">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                SortExpression="ID" HeaderStyle-Width="30%"/>
            <asp:TemplateField HeaderText="hostSpotID" SortExpression="hostSpotID">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server" 
                        DataSourceID="SqlDataSource_videos_edit" DataTextField="title" 
                        DataValueField="Id" SelectedValue='<%# Bind("hostSpotID") %>'>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource_videos_edit" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:UTOURDBConnectionString %>" 
                        SelectCommand="SELECT [Id], [title] FROM [Layerhotspots]">
                    </asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("hostSpotID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="video" HeaderText="video" 
                SortExpression="video"/>
            <asp:BoundField DataField="Description" HeaderText="Description" 
                SortExpression="Description" />
            <asp:BoundField DataField="VideoLength" HeaderText="VideoLength" 
                SortExpression="VideoLength" />
        </Columns>
    </asp:GridView>

            </td>
        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:UTOURDBConnectionString %>" 
                    DeleteCommand="DELETE FROM [Monuments_Videos] WHERE [ID] = @ID" 
                    InsertCommand="INSERT INTO [Monuments_Videos] ([ID], [hostSpotID], [video], [Description], [VideoLength]) VALUES (@ID, @hostSpotID, @video, @Description, @VideoLength)" 
                    SelectCommand="SELECT * FROM [Monuments_Videos]" 
                    UpdateCommand="UPDATE [Monuments_Videos] SET [hostSpotID] = @hostSpotID, [video] = @video, [Description] = @Description, [VideoLength] = @VideoLength WHERE [ID] = @ID">
                    <DeleteParameters>
                        <asp:Parameter Name="ID" Type="Int32" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="ID" Type="Int32" />
                        <asp:Parameter Name="hostSpotID" Type="String" />
                        <asp:Parameter Name="video" Type="String" />
                        <asp:Parameter Name="Description" Type="String" />
                        <asp:Parameter Name="VideoLength" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="hostSpotID" Type="String" />
                        <asp:Parameter Name="video" Type="String" />
                        <asp:Parameter Name="Description" Type="String" />
                        <asp:Parameter Name="VideoLength" Type="String" />
                        <asp:Parameter Name="ID" Type="Int32" />
                    </UpdateParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <tr>
            <td>

    <asp:DetailsView ID="DetailsView_Monuments_Videos" runat="server" Height="50px" 
                    Width="470px" AutoGenerateRows="False" DataKeyNames="ID" 
                    DataSourceID="SqlDataSource1">
        <Fields>
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                SortExpression="ID" />
            <asp:TemplateField HeaderText="hostSpotID" SortExpression="hostSpotID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("hostSpotID") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:DropDownList ID="DropDownList2" runat="server" 
                        DataSourceID="SqlDataSource_videos_new" DataTextField="title" 
                        DataValueField="Id" SelectedValue='<%# Bind("hostSpotID") %>'>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource_videos_new" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:UTOURDBConnectionString %>" 
                        SelectCommand="SELECT [Id], [title] FROM [Layerhotspots]">
                    </asp:SqlDataSource>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("hostSpotID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="video" HeaderText="video" SortExpression="video" />
            <asp:BoundField DataField="Description" HeaderText="Description" 
                SortExpression="Description" />
            <asp:BoundField DataField="VideoLength" HeaderText="VideoLength" 
                SortExpression="VideoLength" />
            <asp:CommandField ShowInsertButton="True" />
        </Fields>
    </asp:DetailsView>

            </td>
        </tr>
    </table>
</asp:Content>

