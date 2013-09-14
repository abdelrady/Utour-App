<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TestWebUserControl.ascx.cs" Inherits="UserControl_TestWebUserControl" %>

<div id="content">
	<div id="sidebar">
				<div id="login" class="boxed">
			<h3 class="title">User Account</h3>
			<div class="content">
				<form id="form1" method="post" action="#">
					<fieldset>
					<legend>Sign-In</legend>

					<label for="inputtext1">Client ID:</label>
					<input id="inputtext1" type="text" name="inputtext1" value="" />
					<label for="inputtext2">Password:</label>
					<input id="inputtext2" type="password" name="inputtext2" value="" />
					<input id="inputsubmit1" type="submit" name="inputsubmit1" value="Sign In" onclick="return inputsubmit1_onclick()" />
					<p><a href="#">Forgot your password?</a></p>
					</fieldset>
				</form>
			</div>
		</div>
			</div>
	<div id="extra"></div>
</div>