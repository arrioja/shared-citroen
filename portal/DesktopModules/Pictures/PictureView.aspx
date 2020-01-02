<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Page language="c#" Codebehind="PictureView.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.PictureView" %>

<HTML>
  <HEAD runat="server" />
	<body runat="server">
		<form id="PictureView" method="post" runat="server">
			<div class="rb_DefaultLayoutDiv">
		<table class="rb_DefaultLayoutTable">
			<tr valign="top">
				<td class="rb_DefaultPortalHeader" valign="top">
					<portal:Banner id="SiteHeader" runat="server" />
				</td>
			</tr>
			<tr>
				<td>
					<tra:label id=lblError visible="false" textkey="PICTURES_FAILED_TO_LOAD" text="Failed to load templates. Revise your settings" runat="server" Font-Bold="True" ForeColor="Red"></tra:label>
					<asp:PlaceHolder id="Picture" runat="server"></asp:PlaceHolder>
				</td>
			</tr>
			<tr>
				<td class="rb_DefaultPortalFooter"><foot:Footer id="Footer" runat="server"></foot:Footer></td>
			</tr>
		</table>
		</div>
		</form>
	</body>
</HTML>
