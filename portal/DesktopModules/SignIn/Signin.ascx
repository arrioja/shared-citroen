<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Control language="c#" Inherits="Rainbow.DesktopModules.Signin" CodeBehind="Signin.ascx.cs" AutoEventWireup="false" %>
<table cellSpacing="1" cellPadding="1" width="150" align="center">
	<tr>		<td noWrap><span class="Normal"><tra:literal id="EmailLabel" runat="server" Text="Email" TextKey="EMAIL" LabelForControl="email"></tra:literal></span>
		</td>
	</tr>
	<tr>
		<td noWrap><asp:textbox id="email" runat="server" columns="24" width="130" cssclass="NormalTextBox"></asp:textbox></td>
	</tr>
	<tr>
		<td noWrap><span class="Normal"><tra:literal id="PasswordLabel" runat="server" Text="Password" TextKey="PASSWORD" LabelForControl="password"></tra:literal></span>
		</td>
	</tr>
	<tr>
		<td noWrap><asp:textbox id="password" runat="server" columns="24" width="130" cssclass="NormalTextBox" textmode="password"></asp:textbox></td>
	</tr>
	<tr>
		<td noWrap><tra:checkbox id="RememberCheckBox" runat="server" Text="Remember Login" TextKey="REMEMBER_LOGIN"
				CssClass="Normal"></tra:checkbox></td>
	</tr>

	<tr>		<td noWrap align="right"><tra:button id="LoginBtn" runat="server" Text="Sign in" TextKey="SIGNIN" EnableViewState="False"
				CssClass="CommandButton"></tra:button></td>
	</tr>
	<tr>		<td noWrap align="right"><tra:linkbutton id="RegisterBtn" runat="server" Text="Register" TextKey="REGISTER" EnableViewState="False"
				CssClass="CommandButton"></tra:linkbutton></td>
	</tr>
	<tr>
		<td noWrap align="right"><tra:linkbutton id="SendPasswordBtn" runat="server" Text="Forgotten Password?" TextKey="SIGNIN_SEND_PWD"
				EnableViewState="False" CssClass="CommandButton"></tra:linkbutton></td>
	</tr>
	<tr>
		<td><tra:label id="Message" runat="server" CssClass="Error"></tra:label></td>
	</tr>
</table>
