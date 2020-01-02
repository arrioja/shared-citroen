<%@ Page language="c#" Codebehind="UploadFlash.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.UploadFlash" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<html>
	<head runat="server"></head>
	<body runat="server">
<FORM id="Form" encType="multipart/form-data" runat="server">
	<asp:panel id="uploadpanel" runat="server">
		<TABLE height="50" width="100%">
			<TR>
				<TD class="HeadBg"><TRA:Label id="Label5" runat="server" Text="Flash Gallery" TextKey="FLASH_GALLERY" CssClass="SiteTitle"></TRA:Label>:</TD></TR>
			<TR>
				<TD><asp:label id="gallerymessage" runat="server" CssClass="Message"></asp:label></TD></TR></TABLE><asp:TABLE id="flashTable" style="OVERFLOW: scroll" runat="server" Height="300" Width="100%" EnableViewState="True"></asp:TABLE>
		<HR>
		<TRA:Label class="Head" id="Label1" runat="server" Text="Upload Flash File" TextKey="FLASH_UPLOAD_TITLE"></TRA:Label>
		<TABLE>
			<TR>
				<TD class="SubHead"><TRA:Label id="Label2" runat="server" Text="1. Select the image" TextKey="FLASH_SELECT_FILE"></TRA:Label>:</TD>
				<TD class="SubHead"><asp:label id="uploadlabel" runat="server"></asp:label></TD>
				<TD><INPUT id="uploadfile" type="file" name="uploadfile" runat="server" CssClass="CommandButton"></TD></TR>
			<TR>
				<TD colSpan="3"><asp:label id="uploadmessage" runat="server" CssClass="Message"></asp:label></TD></TR>
			<TR>
				<TD class="SubHead"><TRA:Label id="Label3" runat="server" Text="2. Upload the (*.swf)File" TextKey="FLASH_UPLOAD_FILE"></TRA:Label>:</TD>
				<TD></TD>
				<TD><tra:Button id="uploadButton" runat="server" Text="Upload" TextKey="UPLOAD_FILE" CssClass="CommandButton"></tra:Button></TD></TR></TABLE>
	</asp:panel>
</FORM>
	</body>
</html>