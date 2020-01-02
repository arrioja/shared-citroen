<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Control language="c#" Inherits="Rainbow.DesktopModules.SigninLink" CodeBehind="SigninLink.ascx.cs" AutoEventWireup="false" %>
<table class="Normal" cellspacing="1" cellpadding="1" align="center" width="100%">
	<tr>
		<td>
			<tra:LinkButton id="SignInBtn" runat="server" CssClass="CommandButton" EnableViewState="False" TextKey="SIGNIN" Text="Sign In" CausesValidation="False">
		Sign In</tra:LinkButton>
		</td>
	</tr>
	<tr>
		<td>
			<tra:LinkButton id="RegisterBtn" runat="server" CssClass="CommandButton" EnableViewState="False" TextKey="REGISTER" Text="Register" CausesValidation="False">
		Register</tra:LinkButton>
		</td>
	</tr>
</table>
