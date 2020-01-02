<%@ Control language="c#" Inherits="Rainbow.DesktopModules.SigninLdap" CodeBehind="SigninLdap.ascx.cs" AutoEventWireup="false" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<table class="Normal" cellSpacing="1" cellPadding="1" align="center">
	<tr>
		<td noWrap><tra:literal id="EmailLabel" runat="server" Text="EMail" TextKey="EMAIL" EnableViewState="False"></tra:literal>:
		</td>
	</tr>
	<tr>
		<td noWrap><asp:textbox id="email" runat="server" columns="24" width="130" cssclass="NormalTextBox"></asp:textbox></td>
	</tr>
	<tr>
		<td noWrap><tra:literal id="PasswordLabel" runat="server" Text="Password" TextKey="PASSWORD" EnableViewState="False"></tra:literal>:
		</td>
	</tr>
	<tr>
		<td noWrap><asp:textbox id="password" runat="server" columns="24" width="130" cssclass="NormalTextBox" textmode="password"></asp:textbox></td>
	</tr>
	<tr id="TRGroup" runat="server" Visible="False">
		<td noWrap><tra:literal id="Group" runat="server" Text="Group" TextKey="GROUP" EnableViewState="False"></tra:literal>:
		</td>
	</tr>
	<tr id="TRGroupList" runat="server" Visible="False">
		<td noWrap height="17">
			<asp:DropDownList id="GroupList" runat="server"></asp:DropDownList></td>
	</tr>
	<tr>
		<td noWrap>
			<tra:checkbox id="RememberCheckBox" runat="server" Text="Remember Login" TextKey="REMEMBER_LOGIN"
				EnableViewState="False"></tra:checkbox>
			<tra:CheckBox id="LDAPCheckBox" Text="LDAP" TextKey="LDAP" runat="server" AutoPostBack="True"></tra:CheckBox>
		</td>
	</tr>
	<tr>
		<td noWrap align="right"><tra:button id="LoginBtn" runat="server" Text="Sign in" TextKey="SIGNIN" EnableViewState="False"
				CssClass="CommandButton"></tra:button></td>
	</tr>
	<tr>
		<td noWrap align="right"><tra:linkbutton id="RegisterBtn" runat="server" Text="Register" TextKey="REGISTER" EnableViewState="False"
				CssClass="CommandButton"></tra:linkbutton></td>
	</tr>
	<tr>
		<td noWrap align="right"><tra:linkbutton id="SendPasswordBtn" runat="server" Text="Forgotten Password?" TextKey="SIGNIN_SEND_PWD"
				EnableViewState="False" CssClass="CommandButton"></tra:linkbutton></td>
	</tr>
	<tr>
		<td class="NormalRed"><tra:label id="Message" runat="server"></tra:label></td>
	</tr>
</table>
