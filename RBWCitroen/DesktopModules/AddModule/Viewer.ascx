<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Control language="c#" Inherits="Rainbow.DesktopModules.AddModule.Viewer" CodeBehind="Viewer.ascx.cs" AutoEventWireup="false" %>
<table align="center" border="0">
	<tr>
		<td vAlign="top"><tra:literal id="moduleNameLabel" runat="server" Text="Module Name:" TextKey="AM_MODULETYPE"></tra:literal></td>
		<td vAlign="top"><asp:dropdownlist id="moduleType" runat="server" AutoPostBack="True" CssClass="NormalTextBox" DataValueField="ModuleDefID" DataTextField="FriendlyName"></asp:dropdownlist>&nbsp;
			<asp:hyperlink id="AddModuleHelp" runat="server"></asp:hyperlink></td></tr>
	<tr>
		<td vAlign="top"><tra:Literal id="moduleLocationLabel" runat="server" Text="Module Location:" TextKey="AM_MODULELOCATION"></tra:Literal></td>
		<td valign="top">
			<asp:DropDownList id="paneLocation" runat="server">
				<asp:ListItem Value="LeftPane">Left Column</asp:ListItem>
				<asp:ListItem Value="ContentPane" Selected="True">Center Column</asp:ListItem>
				<asp:ListItem Value="RightPane">Right Column</asp:ListItem>
			</asp:DropDownList>
			</td>
			</tr>
	<tr>
		<td valign="top"><tra:Literal id="moduleVisibleLabel" runat="server" Text="Module Visible To:" TextKey="AM_MODULEVISIBLETO"></tra:Literal><BR></td>
		<td valign="top">
			<asp:DropDownList id="viewPermissions" runat="server">
				<asp:ListItem Value="All Users;" Selected="True">All Users</asp:ListItem>
				<asp:ListItem Value="Authenticated Users;">Authenticated Users</asp:ListItem>
				<asp:ListItem Value="Unauthenticated Users;">Unauthenticated Users</asp:ListItem>
				<asp:ListItem Value="Authorised Roles">Authorised Roles</asp:ListItem>
			</asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td valign="top"><tra:Literal TextKey="AM_MODULENAME" Text="Module Title:" id="moduleTitleLabel" runat="server"></tra:Literal></td>
		<td valign="top"><asp:textbox id="moduleTitle" runat="server" width="150px" cssclass="NormalTextBox" EnableViewState="false" Text="New Module Name">New Module Title</asp:textbox>
		<tra:linkbutton TextKey="AM_ADDMODULETOTAB" cssclass="CommandButton" id="AddModuleBtn" runat="server" Text="Add to 'Organize Modules' Below">Add this Module to the page</tra:linkbutton></td>
	</tr></table>
<div align="center" class="Error"><tra:Literal id="moduleError" runat="server" Visible="False" Text="There was an error adding this module. It has been logged." TextKey="AM_MODULEADDERROR" EnableViewState="False"></tra:Literal></div>
