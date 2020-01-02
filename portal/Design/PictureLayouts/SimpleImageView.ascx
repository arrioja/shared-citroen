<%@ Control Language="c#" AutoEventWireup="false" Inherits="Rainbow.Design.PictureItem" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table width="90%" cellspacing="5" cellpadding="0" border="0" runat="server" align="center">
	<tr>
		<td align="center">
			<img width='<%#GetMetadata("ModifiedWidth")%>' height='<%#GetMetadata("ModifiedHeight")%>' src='<%#GetMetadata("AlbumPath") + "/" + GetMetadata("ModifiedFilename")%>' alt='<%#GetMetadata("LongDescription")%>' border="1">
		</td>
	</tr>
	<tr>
		<td align="center">
			<span class="Normal">
				<%#GetMetadata("LongDescription")%>
			</span>
		</td>
	</tr>
</table>
