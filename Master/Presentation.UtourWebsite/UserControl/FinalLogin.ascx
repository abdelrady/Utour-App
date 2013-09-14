<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FinalLogin.ascx.cs" Inherits="UserControl_FinalLogin" %>

<style type="text/css">
    #login {        height: 321px;
        width: 900px;
    }

#login form {
}

#login fieldset {
	border-style: none;
        border-color: inherit;
        border-width: medium;
        width: 209px;
    }

#login legend {
	display: none;
}

#login label {
	font-size: x-small;
	font-weight: bold;
}

#login input {
	margin-bottom: 5px;
	padding: 2px 5px;
	border: 1px solid #B43939;
	font-family: Verdana, Arial, Helvetica, sans-serif;
}

#inputtext1, #inputtext2 {
	color: #B43939;
}

#inputsubmit1 {
	background: #B43939;
	color: #FFFFFF;
}
     
</style>

<div id="login" class="boxed">
			<h2 class="title">Client Account</h2>
			<div class="content">
				<form id="form1" method="post" action="#">
					<fieldset>
					<legend>Sign-In</legend>
					<asp:label runat="server" Text="Client ID:"></asp:label>
					<asp:TextBox id="txtUserName" runat="server" name="inputtext1" value=""></asp:TextBox>
					<asp:label runat="server" Text="Password:"></asp:label>
                    <asp:TextBox id="txtPassword" runat="server" name="inputtext2" value=""></asp:TextBox>
                    <asp:Button ID="inputsubmit1" runat="server" Text="Sign In" 
                            onclick="inputsubmit1_Click" />
					    <br />
					<asp:CheckBox ID="CheckBox1" runat="server" Text="Remember Me" />
                        <br />
                        <a href="#">Forgot your password?</a>
                        <asp:Label ID="lblMessage" runat="server" Visible="False" ForeColor="Red" 
        Font-Bold="True" Font-Size="Large"></asp:Label>

					</fieldset>
				</form>
			</div>
</div>