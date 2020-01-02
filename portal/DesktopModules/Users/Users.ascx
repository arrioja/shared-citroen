<%@ Control Inherits="Rainbow.DesktopModules.Users" CodeBehind="Users.ascx.cs" Language="c#" AutoEventWireup="false" %>
<%@ Register TagPrefix="cc1" Namespace="Rainbow.UI.WebControls" Assembly="Rainbow" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<table cellSpacing="0" cellPadding="2" border="0">
	<tr vAlign="top">
		<td class="Normal"><asp:placeholder id="UserDomain" Visible="False" runat="server"><tra:Literal id="DomainMessage1" runat="server" Text="Domain users do not need to be registered to access portal content that is available to 'All Users'" TextKey="USER_MESSAGE_DOMAIN1"></tra:Literal><BR><tra:Literal id="DomainMessage2" runat="server" Text="Administrators may add domain users to specific roles using the Security Roles function above." TextKey="USER_MESSAGE_DOMAIN2"></tra:Literal><BR><tra:Literal id="DomainMessage3" runat="server" Text="This section permits Administrators to manage users and their security roles directly." TextKey="USER_MESSAGE_DOMAIN3"></tra:Literal><BR>
			</asp:placeholder><asp:placeholder id="UserForm" Visible="False" runat="server"><tra:Literal id="FormsMessage1" runat="server" Text="Users must be registered to view secure content." TextKey="USER_MESSAGE1"></tra:Literal><BR><tra:Literal id="FormsMessage2" runat="server" Text="Users may add themselves using the Register form, and Administrators may add users to specific roles using the Security Roles function above." TextKey="USER_MESSAGE2"></tra:Literal><BR><tra:Literal id="FormsMessage3" runat="server" Text="This section permits Administrators to manage users and their security roles directly." TextKey="USER_MESSAGE3"></tra:Literal><BR>
			</asp:placeholder></td>
	</tr>
	<tr vAlign="top">
		<td class="Normal"><tra:label id="RegisteredLabel" Text="Registered users" TextKey="REGISTERED_USERS" Runat="server"></tra:label>&nbsp;
			<asp:dropdownlist id="allUsers" runat="server" DataValueField="UserID" DataTextField="Email" CssClass="NormalTextBox"></asp:dropdownlist>&nbsp;
			<tra:imagebutton id="EditBtn" runat="server" AlternateText="Edit this user" CommandName="edit"></tra:imagebutton>
			<tra:imagebutton id="DeleteBtn" runat="server" AlternateText="Delete this user"></tra:imagebutton>&nbsp;
		</td>
	</tr>
</table>
