<%@ Page language="c#" Codebehind="ApproveModuleContent.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.ApproveModuleContent" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DesktopPortalBanner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="uc1" TagName="EmailForm" Src="EmailForm.ascx" %>
<html>
	<head runat="server">
		<meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<body leftmargin="0" bottommargin="0" rightmargin="0" topmargin="0">
		<form id="RequestModuleContentApproval" method="post" runat="server">
			<div class="rb_AlternateLayoutDiv">
			<table class="rb_AlternateLayoutTable">
			<tr>
			<td class="rb_AlternatePortalHeader" valign="top"><uc1:DesktopPortalBanner id="SiteHeader" runat="server"></uc1:DesktopPortalBanner></td>
			</tr>
				<tr>
				<td nowrap align=center width="100%"><uc1:emailform id="emailForm" runat="server"></uc1:emailform></td>
				</tr>
				<tr>
				<td nowrap align="center">
					<tra:LinkButton TextKey="SWI_APPROVESENDMAIL" Text="Approve &amp; send mail" id="btnApproveAndSendMail" runat="server" CssClass="CommandButton"></tra:LinkButton>
					&nbsp;
					<tra:LinkButton TextKey="SWI_APPROVE" Text="Approve" id="btnApprove" runat="server" CssClass="CommandButton"></tra:LinkButton>
					&nbsp;
					<tra:LinkButton TextKey="CANCEL" Text="Cancel" id="cancelButton" runat="server" CssClass="CommandButton"></tra:LinkButton>
				</td>
				</tr>
				<tr>
				<td class="rb_AlternatePortalFooter"><foot:Footer id="Footer" runat="server"></foot:Footer></td>
				</tr>
			</table>
			</div>
		</form>
	</body>
</html>
