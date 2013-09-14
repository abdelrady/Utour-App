<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginUserControl.ascx.cs" Inherits="UserControl_LoginUserControl" %>


			<h2 class="title">Client Account</h2>
			<div class="content">
				<form id="form2" method="post" action="#">
					<fieldset>
					<legend>Sign-In</legend>
					<asp:label ID="Label1" runat="server" Text="Client ID:"></asp:label>
					<asp:TextBox id="txtUserName" runat="server" name="inputtext1" value=""></asp:TextBox>
					<asp:label ID="Label2" runat="server" Text="Password:"></asp:label>
                    <asp:TextBox id="txtPassword" runat="server" name="inputtext2" value="" 
                            TextMode="Password"></asp:TextBox>
                    &nbsp;<asp:Button ID="inputsubmit1" runat="server" Text="Sign In"  
                            BackColor="#C7894C" ForeColor="#FFFFFF" onclick="inputsubmit1_Click"/>
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <%--<asp:CheckBox ID="CheckBox1" Text="Remember me" ForeColor= "#C7894C" runat="server"/>--%>
                        <br />
                        <a href="#" style="color:#C7894C;" >Forgot your password?</a>
                        <asp:Label ID="lblMessage" runat="server" Visible="False" ForeColor="Red" 
        Font-Bold="True" Font-Size="Large"></asp:Label>

					</fieldset>
				</form>
			</div>
