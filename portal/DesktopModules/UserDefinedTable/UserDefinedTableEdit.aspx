<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Page Language="c#" CodeBehind="UserDefinedTableEdit.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.UserDefinedTableEdit" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form encType="multipart/form-data" runat="server" ID="Form1">
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
									<td width="100">&nbsp;
									</td>
									<td width="*">
										<table width="600" cellspacing="0" cellpadding="0">
											<tr>
												<td align="left" class="Head">
													<tra:Label TextKey="USERTABLE_EDITROW" Text="Edit Table Row" id="EditTableRow" runat="Server" />
												</td>
											</tr>
											<tr>
												<td colspan="2">
													<hr noshade size="1">
												</td>
											</tr>
										</table>
										<asp:Table ID="tblFields" Runat="server"></asp:Table>
										<asp:Label ID="lblMessage" Runat="server" CssClass="NormalRed"></asp:Label>
										<hr noshade size="1" width="600">
										<p>
											<tra:LinkButton id="updateButton" Text="Update" runat="server" class="CommandButton" />
											&nbsp;
											<tra:LinkButton id="cancelButton" Text="Cancel" CausesValidation="False" runat="server" class="CommandButton" />
											&nbsp;
											<tra:LinkButton id="deleteButton" Text="Delete this item" CausesValidation="False" runat="server"
												class="CommandButton" />
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
</html>
