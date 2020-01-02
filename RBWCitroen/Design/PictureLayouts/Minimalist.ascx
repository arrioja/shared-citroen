<%@ Control Language="c#" AutoEventWireup="false" Inherits="Rainbow.Design.PictureItem" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<a href='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Pictures/PictureView.aspx?ItemID=" + GetMetadata("ItemID") + "&amp;mid=" + GetMetadata("ModuleID") + "&amp;wversion=" + GetMetadata("WVersion"))%>' runat="server">
	<img border='0' width='<%#GetMetadata("ThumbnailWidth")%>' height='<%#GetMetadata("ThumbnailHeight")%>' alt='<%#GetMetadata("Caption")%>' src='<%# GetMetadata("AlbumPath") + "/" + GetMetadata("ThumbnailFilename")%>'>
</a>
<asp:HyperLink id="editLink" ImageUrl='<%# GetCurrentImageFromTheme("Buttons_Edit", "Edit.gif") %>' NavigateUrl='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Pictures/PicturesEdit.aspx","ItemID=" + GetMetadata("ItemID") + "&amp;mid=" + GetMetadata("ModuleID"))%>' Visible='<%# GetMetadata("IsEditable") == "True"%>' runat="server" />
