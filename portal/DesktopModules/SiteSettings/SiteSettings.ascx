<%@ Control Inherits="Rainbow.DesktopModules.SiteSettings" CodeBehind="SiteSettings.ascx.cs" Language="c#" AutoEventWireup="false" targetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cnf" Namespace="Rainbow.Configuration" Assembly="Rainbow" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<table border="0">
	<tr>
		<td width="140" class="Subhead" vAlign="top">
			<tra:Literal TextKey="SITESETTINGS_SITE_TITLE" id="site_title" Text="Site Title" runat="server"></tra:Literal>
		</td>
		<td colspan="2" class="NormalTextBox">
			<asp:Textbox id="siteName" runat="server" width="240" CssClass="NormalTextBox"></asp:Textbox>
		</td>
	</tr>
	<TR>
		<td class="Subhead" vAlign="top" width="140">
			<tra:Literal TextKey="SITESETTINGS_SITE_PATH" Text="Site Path" id="site_path" runat="server"></tra:Literal>
		</td>
		<td class="Normal" colSpan="2">
			<asp:Label id="sitePath" width="240" runat="server" />
		</td>
	</TR>
</table>
<cnf:SettingsTable id="EditTable" runat="server"></cnf:SettingsTable>
<table border="0">
	<tr>
		<td>
			<tra:LinkButton id="updateButton" class="CommandButton" TextKey="APPLY" Text="Apply Changes" runat="server" />
		</td>
	</tr>
</table>
