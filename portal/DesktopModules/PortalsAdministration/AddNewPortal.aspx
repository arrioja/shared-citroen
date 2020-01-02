<%@ Page language="c#" CodeBehind="AddNewPortal.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.AdminAll.AddNewPortal" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Rainbow.Configuration" Assembly="Rainbow" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form runat="server">
			<div class="rb_AlternateLayoutDiv">
			<table class="rb_AlternateLayoutTable">
				<tr vAlign="top">
					<td class="rb_AlternatePortalHeader" valign="top"><portal:banner id="Banner1" runat="server" ShowTabs="false"></portal:banner></td>
				</tr>
				<tr>
					<td><br>
						<table cellSpacing="0" cellPadding="4" width="98%">
							<tr vAlign="top">
								<td width="150">&nbsp;
								</td>
								<td width="*">
									<table cellSpacing="1" cellPadding="2" border="0">
										<tr>
											<td class="Head" align="left" colSpan="3"><tra:Literal TextKey="ADD_PORTAL" Text="Add new portal" runat="server" id="Literal1" /></td>
										</tr>
										<tr>
											<td colSpan="2">
												<hr noshade size="1">
											</td>
											<td></td>
										</tr>
										<TR>
											<td class="SubHead" width="140"><tra:Literal TextKey="AM_SITETITLE" Text="Site title" runat="server" id="Literal2" /></td>
											<td class="Normal">
												<asp:Textbox id="TitleField" runat="server" width="350" CssClass="NormalTextBox"></asp:Textbox></td>
											<td class="Normal">
												<asp:RequiredFieldValidator id="RequiredTitle" runat="server" ErrorMessage="Required Field" ControlToValidate="TitleField"></asp:RequiredFieldValidator></td>
										</TR>
										<TR>
											<td class="SubHead" width="140"><tra:Literal TextKey="AM_PORTALALIAS" Text="Portal Alias" runat="server" id="Literal3" /></td>
											<td class="Normal"><asp:textbox id="AliasField" runat="server" width="350" CssClass="NormalTextBox"></asp:textbox></td>
											<td class="Normal">
												<asp:RequiredFieldValidator id="RequiredAlias" runat="server" ErrorMessage="Required Field" ControlToValidate="AliasField"></asp:RequiredFieldValidator></td>
										</TR>
										<TR>
											<td class="SubHead" width="140"><tra:Literal TextKey="AM_SITEPATH" Text="Site Path" runat="server" id="Literal4" /></td>
											<td class="Normal"><asp:textbox id="PathField" runat="server" width="350" CssClass="NormalTextBox"></asp:textbox></td>
											<td class="Normal">
												<asp:RequiredFieldValidator id="RequiredSitepath" runat="server" ErrorMessage="Required Field" ControlToValidate="PathField"></asp:RequiredFieldValidator></td>
										</TR>
										<TR>
												<td class="SubHead" width="140">
													<tra:CheckBox id="chkUseTemplate" runat="server" Text="Use Template?" TextKey="AM_USETEMPLATE"
														AutoPostBack="True"></tra:CheckBox></td>
												<td class="Normal"><asp:dropdownlist id="SolutionsList" runat="server" Width="350px" DataValueField="PortalID" DataTextField="PortalAlias"
														CssClass="NormalTextBox"></asp:dropdownlist></td>
											<td class="Normal"></td>
										</TR>
									</table>
									<cc1:SettingsTable id="EditTable" runat="server"></cc1:SettingsTable>
									<P>
										<asp:linkbutton class="CommandButton" id="updateButton" runat="server"></asp:linkbutton></P>
									<P class="Normal">
										<asp:Label id="ErrorMessage" cssclass="Error" runat="server" Visible="false"></asp:Label></P>
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
