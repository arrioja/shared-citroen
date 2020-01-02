<%@ Page language="c#" Codebehind="AddEditPage.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.AddEditPage" %>
<%@ Register TagPrefix="cc1" Namespace="Rainbow.Configuration" Assembly="Rainbow" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Import Namespace="Esperantus" %>
<HTML>
	<HEAD id="htmlHead" runat="server"></HEAD>
	<body leftmargin="0" bottommargin="0" rightmargin="0" topmargin="0" marginheight="0" marginwidth="0">
		<form runat="server" ID="Form1">
			<div class="rb_AlternateLayoutDiv">
				<table class="rb_AlternateLayoutTable">
					<tr valign="top">
						<td class="rb_AlternatePortalHeader" valign="top">
							<portal:Banner id="SiteHeader" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							<br>
							<table width="98%" cellspacing="0" cellpadding="4" border="0">
								<tr valign="top">
									<td width="150">
										&nbsp;
									</td>
									<td width="*">
										<table width="500" cellspacing="0" cellpadding="0">
											<tr>
												<td align="left" class="Head">
													<tra:Literal id="Literal1" runat="server" TextKey="ADDEDITITEMPAGE_TITLE" Text="Add/Edit Item"></tra:Literal>
												</td>
											</tr>
											<tr>
												<td colspan="2">
													<hr noshade size="1">
													<asp:placeholder id="AddEditControlPlaceHolder" runat="server"></asp:placeholder>
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="rb_AlternatePortalFooter"><foot:Footer id="Footer" runat="server"></foot:Footer></td>
					</tr>
				</table>
			</div>
		</form>
	</body>
</HTML>
