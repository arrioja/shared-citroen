<%@ Page language="c#" CodeBehind="EditPortal.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.AdminAll.EditPortal" targetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Rainbow.Configuration" Assembly="Rainbow" %>

<HTML>
  <HEAD runat="server" />
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
											<td class="Head" align="left" colSpan="3"><tra:Literal TextKey="EDIT_PORTAL" Text="Edit Portal" runat=server id=Literal1 /></td>
										</tr>
										<tr>
											<td colSpan="2">
												<hr noshade size="1">
											</td>
											<td></td>
										</tr>
										<TR>
											<td class="SubHead" width="140"><tra:Literal TextKey="AM_PORTALID" Text="Portal ID" runat=server id=Literal2 /></td>
											<td class="Normal"><asp:Label id="PortalIDField" runat="server" /></td>
											<td class="Normal"></td>
										</TR>
										<TR>
											<td class="SubHead" width="140"><tra:Literal TextKey="AM_SITETITLE" Text="Portal Title" runat=server id=Literal3 /></td>
											<td class="Normal">
												<asp:Textbox id="TitleField" runat="server" width="350" CssClass="NormalTextBox"></asp:Textbox></td>
											<td class="Normal">
												<asp:RequiredFieldValidator id="RequiredTitle" runat="server" ErrorMessage="Required Field" ControlToValidate="TitleField"></asp:RequiredFieldValidator></td>
										</TR>
										<TR>
											<td class="SubHead" width="140"><tra:Literal TextKey="AM_PORTALALIAS" Text="Portal Alias" runat=server id=Literal4 /></td>
											<td class="Normal"><asp:Label id="AliasField" runat="server"></asp:Label></td>
											<td class="Normal"></td>
										</TR>
										<TR>
											<td class="SubHead" width="140"><tra:Literal TextKey="AM_SITEPATH" Text="Site Path" runat=server id=Literal5 /></td>
											<td class="Normal"><asp:Label id="PathField" runat="server"></asp:Label></td>
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
</HTML>
