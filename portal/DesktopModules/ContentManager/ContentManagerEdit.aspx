<%@ Page Language="c#" codebehind="ContentManagerEdit.aspx.cs" autoeventwireup="false" Inherits="Rainbow.DesktopModules.ContentManagerEdit" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form runat="server">
			<div class="rb_AlternateLayoutDiv">
				<table class="rb_AlternateLayoutTable">
					<tr vAlign="top">
						<td class="rb_AlternatePortalHeader" valign="top"><portal:banner id="SiteHeader" runat="server"></portal:banner></td>
					</tr>
					<tr>
						<td><br>
							<table cellSpacing="0" cellPadding="4" width="98%" border="0">
								<tr vAlign="top">
									<td width="150">&nbsp;
									</td>
									<td width="*">
										<table cellSpacing="0" cellPadding="0" width="500">
											<tr>
												<td class="Head" align="left"><tra:label id="Label1" runat="server" Text="Content Manager 3rd Party Module-Support Installer"
														TextKey="CONTENT_MGR_TITLE"></tra:label></td>
											</tr>
											<tr>
												<td colSpan="2">
													<hr noShade SIZE="1">
												</td>
											</tr>
										</table>
										<table cellSpacing="0" cellPadding="3" width="750" border="0" runat="server" id="tableInstaller">
											<TR>
												<TD class="SubHead" noWrap width="106"><tra:label id="Label2" runat="server" Text="Friendly Name" TextKey="INSTALLER_FILE">Installer file</tra:label>:</TD>
												<TD width="6"></TD>
												<TD><asp:textbox id="InstallerFileName" runat="server" maxlength="150" Columns="30" width="390" cssclass="NormalTextBox"></asp:textbox></TD>
												<TD width="10"></TD>
												<TD class="Normal" width="250">
													<tra:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" TextKey="ERROR_ENTER_A_FILE_NAME" Display="Dynamic"
														ErrorMessage="Enter an Installer Name" ControlToValidate="InstallerFileName" CssClass="Error"></tra:requiredfieldvalidator>
												</TD>
											</TR>
										</table>
										<p><tra:linkbutton class="CommandButton" id="updateButton" runat="server" Text="Update"></tra:linkbutton>&nbsp;
											<tra:linkbutton class="CommandButton" id="cancelButton" runat="server" Text="Cancel" CausesValidation="False"></tra:linkbutton>&nbsp;
											<tra:linkbutton class="CommandButton" id="deleteButton" runat="server" Text="Delete this module type"
												CausesValidation="False"></tra:linkbutton></p>
										<P>
											<asp:Label id="lblErrorDetail" runat="server" cssclass="error"></asp:Label></P>
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
