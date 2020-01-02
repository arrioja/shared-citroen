<%@ Control language="c#" Inherits="Rainbow.DesktopModules.ThemeCacheManager" CodeBehind="ThemeCacheManager.ascx.cs" AutoEventWireup="false" targetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<table>
	<tr>
		<td colspan="2">
			<H3>
				<tra:Literal TextKey='TCM_DESCRIPTION' Text='Use this module to reload the Themes/Layouts list from disk and avoid &#13;&#10;&#9;application restart.'
					runat="server" ID="Literal1" />
			</H3>
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<H4 class="error">
				<tra:Literal TextKey='TCM_WARNING' Text='Warning: Windows cache Hard Disk request. Sometimes when it is made a call to the operating system the changes made are not reflected. Even if cache is regenerated immediately it may require a couple of browser refresh to reflect changes in the Themes and Layout list.'
					runat="server" ID="Literal2" />
			</H4>
		</td>
	</tr>
	<tr valign="middle">
		<td align="center">
			<tra:Button id="ClearLayoutButton" TextKey="TCM_CLEARLAYOUT" Text="Clear Layout cache" runat="server" CssClass="CommandButton"></tra:Button>
		</td>
		<td align="center">
			<tra:Button id="ClearThemeButton" TextKey="TCM_CLEARTHEME" Text="Clear Theme cache" runat="server" CssClass="CommandButton"></tra:Button>
		</td>
	</tr>
	<tr>
		<td align="center" valign=bottom>
			<h2>
				<tra:Literal TextKey='TCM_MSGLAYOUT' Text='Layout cache Reset !!' visible="false" runat="server" ID="msgLayout" />
			</h2>
		</td>
		<td align="center" valign=bottom>
			<h2>
				<tra:Literal TextKey='TCM_MSGTHEME' Text='Theme cache Reset !!' visible="false" runat="server" ID="msgTheme" />
			</h2>
		</td>
	</tr>
</table>
