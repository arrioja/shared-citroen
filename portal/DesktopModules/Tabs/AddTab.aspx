<%@ Register TagPrefix="uc1" TagName="ADGroupMember" Src="~/DesktopModules/Admin/ADGroupMember.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="cnf" Namespace="Rainbow.Configuration" Assembly="Rainbow" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Page language="c#" CodeBehind="AddTab.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.Admin.AddTab" %>

<HTML>
	<HEAD runat="server">
	</HEAD>
	<body runat="server">
		<form runat="server">
			<div class="rb_AlternateLayoutDiv">
				<table class="rb_AlternateLayoutTable">
					<tr vAlign="top">
						<td class="rb_AlternatePortalHeader" vAlign="top"><portal:banner id="Banner1" runat="server" ShowTabs="false"></portal:banner></td></tr>
					<tr>
						<td><br>
							<table cellSpacing="0" cellPadding="4" width="98%">
								<tr vAlign="top">
									<td width="150">&nbsp; </td>
									<td width="*">
										<table cellSpacing="1" cellPadding="2" border="0">
											<tr>
												<td colSpan="4">
													<table cellSpacing="0" cellPadding="0" width="100%">
														<tr>
															<td class="Head" align="left"><tra:literal id="tab_name" runat="server" Text="Add New Tab" TextKey="AM_TABNAME"></tra:literal></td></tr>
														<tr>
															<td>
																<hr noShade SIZE="1">
															</td></tr></table></td></tr>
											<tr>
												<td class="Normal" width="100"><tra:literal id="tab_name1" runat="server" Text="Tab Name" TextKey="AM_TABNAME1"></tra:literal></td>
												<td colSpan="3"><asp:textbox id="tabName" runat="server" cssclass="NormalTextBox" width="300" maxlength="47"></asp:textbox></td></tr>
											<tr>
												<td class="Normal" noWrap><tra:literal id="roles_auth" runat="server" Text="Authorized Roles" TextKey="AM_ROLESAUTH"></tra:literal></td>
												<td colSpan="3"><asp:checkboxlist id="authRoles" runat="server" width="300" RepeatColumns="2" CssClass="Normal"></asp:checkboxlist><uc1:adgroupmember id="memRoles" runat="server" visible="false"></uc1:adgroupmember></td></tr>
											<TR>
												<td class="Normal" noWrap><tra:literal id="tab_parent" runat="server" Text="Parent Tab" TextKey="TAB_PARENT"></tra:literal></td>
												<td colSpan="3"><asp:dropdownlist id="parentTab" runat="server" CssClass="NormalTextBox" Width="300px" DataTextField="TabName" DataValueField="TabID"></asp:dropdownlist><tra:label id="lblErrorNotAllowed" runat="server" TextKey="ERROR_NOT_ALLOWED_PARENT" CssClass="Error" EnableViewState="False" Visible="False">Not allowed to choose that parent</tra:label></td></TR>
											<tr>
												<td>&nbsp; </td>
												<td colSpan="3">
													<hr noShade SIZE="1">
												</td></tr>
											<tr>
												<td class="Normal" noWrap><tra:literal id="show_mobile" runat="server" Text="Show to mobile users" TextKey="AM_SHOWMOBILE"></tra:literal></td>
												<td colSpan="3"><asp:checkbox id="showMobile" runat="server" CssClass="Normal"></asp:checkbox></td></tr>
											<tr>
												<td class="Normal" noWrap><tra:literal id="mobiletab" runat="server" Text="Mobile Tab Name" TextKey="AM_MOBILETAB"></tra:literal></td>
												<td colSpan="3"><asp:textbox id="mobileTabName" runat="server" cssclass="NormalTextBox" width="300" MaxLength="50"></asp:textbox></td></tr>
											<tr>
												<td colSpan="4">
													<hr noShade SIZE="1">
												</td></tr>
											<tr>
												<td class="Normal" noWrap><tra:literal id="lbl_jump_to_tab" runat="server" Text="Jump to this tab?" TextKey="AM_JUMPTOTAB"></tra:literal></td>
												<td colSpan="3"><asp:checkbox id="cb_JumpToTab" runat="server" CssClass="Normal"></asp:checkbox></td></tr>
											<tr>
												<td colSpan="4">
													<hr noShade SIZE="1">
												</td></tr>
											<tr>
												<td class="Error" align="center" colSpan="4"><tra:literal id="msgError" runat="server" Text="You do not have the appropriate permissions to delete or move this module" TextKey="AM_NO_RIGHTS"></tra:literal></td></tr>
											<tr>
												<td colSpan="4">
													<hr noShade SIZE="1">
													<cnf:settingstable id="EditTable" runat="server"></cnf:settingstable></td></tr>
											<tr>
												<td colSpan="4"><tra:linkbutton class="CommandButton" id="saveButton" runat="server" Text="Save Changes" TextKey="SAVE_CHANGES">Save Tab</tra:linkbutton>&nbsp;
													<tra:linkbutton class="CommandButton" id="cancelButton" runat="server" Text="Cancel" TextKey="CANCEL"></tra:linkbutton></td></tr></table></td></tr></table></td></tr>
					<tr>
						<td class="rb_AlternatePortalFooter"><foot:footer id="Footer" runat="server"></foot:footer></td></tr></table></div></form>
	</body>
</HTML>
