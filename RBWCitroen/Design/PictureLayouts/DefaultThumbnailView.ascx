<%@ Control Language="c#" AutoEventWireup="false" Inherits="Rainbow.Design.PictureItem" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<table width="100%" height="100%">
	<tr>
		<td valign="top" width="1%">
			<a href='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Pictures/PictureView.aspx","ItemID=" + GetMetadata("ItemID") + "&mid=" + GetMetadata("ModuleID") + "&wversion=" + GetMetadata("WVersion"))%>' runat="server">
				<img border='1' width='<%#GetMetadata("ThumbnailWidth")%>' height='<%#GetMetadata("ThumbnailHeight")%>' alt='<%#GetMetadata("Caption")%>' src='<%# GetMetadata("AlbumPath") + "/" + GetMetadata("ThumbnailFilename")%>' />
			</a>
		</td>
		<td align="left" valign="middle" class="Normal">
			<%#GetMetadata("ShortDescription")%>
			<tra:HyperLink id="editLink" ImageUrl='<%# GetCurrentImageFromTheme("Buttons_Edit", "Edit.gif") %>' NavigateUrl='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Pictures/PicturesEdit.aspx","ItemID=" + GetMetadata("ItemID") + "&mid=" + GetMetadata("ModuleID"))%>' Visible='<%# GetMetadata("IsEditable") == "True"%>' runat="server" />
		</td>
	</tr>
	<tr>
		<td valign="top" align="center" class="Normal">
			[
			<%#GetMetadata("ModifiedWidth") + " x " + GetMetadata("ModifiedHeight")%>
			]<br>
			<%#GetMetadata("ModifiedFileSize") %>
			bytes
		</td>
	</tr>
</table>
