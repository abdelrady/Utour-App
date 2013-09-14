<%@ Control Language="C#" AutoEventWireup="true" CodeFile="simpleaddition.ascx.cs" Inherits="usercontrol_simpleaddition" %>
<table style="width: 100%;">
    <tr>
        <td>
            Number1:</td>
        <td>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Number2:</td>
        <td>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblresult" runat="server"></asp:Label>
        </td>
        <td>
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
        </td>
    </tr>
</table>

