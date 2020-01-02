<%@ Register TagPrefix="cc1" Namespace="Rainbow.UI.WebControls" Assembly="Rainbow" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="EmailForm.ascx.cs" Inherits="Rainbow.DesktopModules.EmailForm" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table width="100%">
	<tr>
		<td align="right" width="20%"><span class="Normal"><tra:Literal TextKey="EMF_TO" Text="To" runat="server" id="Literal1" />:</span></td>
		<td align="left">
			<asp:TextBox id="txtTo" runat="server" Width="80%"></asp:TextBox></td>
	</tr>
	<tr>
		<td align="right" width="20%"><span class="Normal"><tra:Literal TextKey="EMF_CC" Text="Cc" runat="server" id="Literal2" />:</span></td>
		<td align="left">
			<asp:TextBox id="txtCc" runat="server" Width="80%"></asp:TextBox></td>
	</tr>
	<tr>
		<td align="right" width="20%string.Empty><span class="Normal"><tra:Literal TextKey="EMF_BCC" Text="Bcc" runat="server" id="Literal3" />:</span></td>
		<td align="left">
			<asp:TextBox id="txtBcc" runat="server" Width="80%"></asp:TextBox></td>
	</tr>
	<tr>
		<td align="right" width="20%"><span class="Normal"><tra:Literal TextKey="EMF_SUBJECT" Text="Subject" runat="server" id="Literal4" /></span>:</td>
		<td align="left">
			<asp:TextBox id="txtSubject" runat="server" Width="80%"></asp:TextBox></td>
	</tr>
	<tr>
		<td colspan="2" align=center>
			<asp:placeholder id="PlaceHolderHTMLEditor" runat="server"></asp:placeholder>
		</td>
	<TR>
		<td colSpan="2" align=center>
			<asp:Label id="lblEmailAddressesNotOk" runat="server" ForeColor="Red" Visible="False" CssClass="Normal"></asp:Label></td>
	</TR>
</table>
