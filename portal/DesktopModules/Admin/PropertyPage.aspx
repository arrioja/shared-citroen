<%@ Page language="c#" Codebehind="PropertyPage.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.PagePropertyPage" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Rainbow.Configuration" Assembly="Rainbow" %>
<%@ Import Namespace="Esperantus" %>

<HTML>
	<head runat="server" />
	<body runat="server">
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
							<table align="center" cellspacing="0" cellpadding="4" border="0">
								<tr valign="top">
									<td>
										<table width="600" cellspacing="0" cellpadding="0">
											<tr>
												<td align="left" class="Head">
													<tra:Literal id="Literal1" runat="server" TextKey="MODULESETTINGS_SETTINGS" Text="Module settings"></tra:Literal>
												</td>
											</tr>
											<tr>
												<td colspan="2">
													<hr noshade size="1">
													<cc1:SettingsTable ID="EditTable" runat="server"></cc1:SettingsTable>
												</td>
											</tr>
										</table>
										<p>
											<asp:placeholder id="PlaceHolderButtons" runat="server"></asp:placeholder>&nbsp;
										</p>
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
