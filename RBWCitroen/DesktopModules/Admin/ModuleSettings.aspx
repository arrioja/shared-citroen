<%@ Page CodeBehind="ModuleSettings.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Rainbow.Admin.ModuleSettingsPage" %>
<%@ Register TagPrefix="mem" TagName="ADGroupMember" Src="ADGroupMember.ascx" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>

<HTML>
	<head runat="server"/>
	<body runat="server">
		<form id="Form1" runat="server">
			<div class="rb_AlternateLayoutDiv">
				<table class="rb_AlternateLayoutTable">
					<tr vAlign="top">
						<td class="rb_AlternatePortalHeader" vAlign="top"><portal:banner id="Banner1" runat="server" ShowTabs="false"></portal:banner></td>
					</tr>
					<tr>
						<td><br>
							<table cellSpacing="0" cellPadding="4" width="98%" border="0">
								<tr vAlign="top">
									<td width="150">&nbsp;
									</td>
									<td width="*">
										<table cellSpacing="1" cellPadding="2" border="0">
											<tr>
												<td colSpan="4" height=20>
													<table cellSpacing="0" cellPadding="0" width="100%">
														<tr>
															<td class="Head" align="left"><tra:literal id="Literal1" runat="server" TextKey="MODULESETTINGS_BASE_SETTINGS" Text="Module base settings"></tra:literal></td>
														</tr>
														<tr>
															<td>
																<hr noShade SIZE="1">
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<tr>
												<td class="SubHead" width="200" height=50><tra:literal id="Literal2" runat="server" TextKey="MODULESETTINGS_MODULE_TYPE" Text="Module type"></tra:literal>:
												</td>
												<td colSpan="3" height=38>&nbsp;<asp:label id="moduleType" runat="server" width="300" cssclass="NormalBold"></asp:label>
												</td>
											</tr>
											<tr>
												<td class="SubHead" width="200"><tra:literal id="Literal18" runat="server" TextKey="MODULESETTINGS_MODULE_NAME" Text="Module name"></tra:literal>:
												</td>
												<td colSpan="3">&nbsp;<asp:textbox id="moduleTitle" runat="server" width="300" cssclass="NormalTextBox"></asp:textbox>
												</td>
											</tr>
											<tr>
												<td class="SubHead" width="200"><tra:literal id="Literal3" runat="server" TextKey="MODULESETTINGS_CACHE_TIMEOUT" Text="Cache Timeout"></tra:literal>:
												</td>
												<td colSpan="3">&nbsp;<asp:textbox id="cacheTime" runat="server" width="100" cssclass="NormalTextBox"></asp:textbox>
												</td>
											</tr>
											<TR>
												<td class="SubHead" width="200"><tra:literal id="Literal13" runat="server" TextKey="MODULESETTINGS_MOVE_TO_TAB" Text="Move to tab"></tra:literal>:</td>
												<td colSpan="3">&nbsp;<asp:dropdownlist id=tabDropDownList runat="server" DataSource="<%# portalTabs %>" DataTextField="Name" DataValueField="ID" CssClass="NormalTextBox" Width="300px">
													</asp:dropdownlist></td>
											</TR>
											<tr>
												<td>&nbsp;
												</td>
												<td colSpan="3">
													<hr noShade SIZE="1">
												</td>
											</tr>
											<tr>
												<td class="SubHead" vAlign="top" width="200"><tra:literal id="Literal5" runat="server" TextKey="MODULESETTINGS_ROLE_VIEW" Text="Roles that can view"></tra:literal>:
												</td>
												<TD colSpan="3" noWrap>
													<asp:checkboxlist id="authViewRoles" runat="server" width="300" RepeatColumns="2" CssClass="Normal"
														cellpadding="0" cellspacing="0"></asp:checkboxlist>
													<mem:adgroupmember id="memViewRoles" runat="server" visible="false"></mem:adgroupmember></TD>
											</tr>
											<TR>
												<TD Class="SubHead" vAlign="top" width="200">
													<tra:literal id="Literal4" runat="server" TextKey="MODULESETTINGS_ROLES_EDIT" Text="Roles that can edit"></tra:literal>:</TD>
												<TD colSpan="3" noWrap>
													<asp:checkboxlist id="authEditRoles" runat="server" width="300" RepeatColumns="2" CssClass="Normal"
														cellpadding="0" cellspacing="0"></asp:checkboxlist>
													<mem:adgroupmember id="memEditRoles" runat="server" visible="false"></mem:adgroupmember></TD>
											</TR>
											<TR>
												<TD Class="SubHead" vAlign="top" width="200">
													<tra:literal id="Literal6" runat="server" TextKey="MODULESETTINGS_ROLES_ADD" Text="Roles that can add"></tra:literal>:
												</TD>
												<TD colSpan="3" noWrap>
													<asp:checkboxlist id="authAddRoles" runat="server" width="300" RepeatColumns="2" CssClass="Normal"
														cellpadding="0" cellspacing="0"></asp:checkboxlist>
													<mem:adgroupmember id="memAddRoles" runat="server" visible="false"></mem:adgroupmember></TD>
											</TR>
											<TR>
												<TD Class="SubHead" vAlign="top" width="200">
													<tra:literal id="Literal7" runat="server" Text="Roles that can delete" TextKey="MODULESETTINGS_ROLES_DELETE"></tra:literal>:
												</TD>
												<td colSpan="3" noWrap>
													<asp:checkboxlist id="authDeleteRoles" runat="server" width="300" RepeatColumns="2" CssClass="Normal"
														cellspacing="0" cellpadding="0"></asp:checkboxlist>
													<mem:adgroupmember id="memDeleteRoles" runat="server" visible="false"></mem:adgroupmember></td>
											</TR>
											<TR>
												<td class="SubHead" vAlign="top" width="200">
													<tra:literal id="Literal8" runat="server" TextKey="MODULESETTINGS_ROLES_EDIT_COLLECTION" Text="Roles that can edit properties"></tra:literal>:
												</td>
												<td colSpan="3" noWrap>
													<asp:checkboxlist id="authPropertiesRoles" runat="server" width="300" RepeatColumns="2" CssClass="Normal"
														cellspacing="0" cellpadding="0"></asp:checkboxlist>
													<mem:adgroupmember id="memPropertiesRoles" runat="server" visible="false"></mem:adgroupmember></td>
											</TR>
											<TR>
												<td class="SubHead" vAlign="top" width="200">
													<tra:literal id="Literal16" runat="server" TextKey="MODULESETTINGS_ROLES_MOVE_MODULES" Text="Roles that can move modules"></tra:literal>:
												</td>
												<td noWrap colSpan="3">
													<asp:checkboxlist id="authMoveModuleRoles" runat="server" width="300" RepeatColumns="2" CssClass="Normal"
														cellspacing="0" cellpadding="0"></asp:checkboxlist>
													<mem:adgroupmember id="memMoveModuleRoles" runat="server" visible="false"></mem:adgroupmember></td>
											</TR>
											<TR>
												<td class="SubHead" vAlign="top" width="200">
													<tra:literal id="Literal17" runat="server" TextKey="MODULESETTINGS_ROLES_DELETE_MODULES" Text="Roles that can delete modules"></tra:literal>:
												</td>
												<td noWrap colSpan="3">
													<asp:checkboxlist id="authDeleteModuleRoles" runat="server" width="300" RepeatColumns="2" CssClass="Normal"
													cellspacing="0" cellpadding="0"></asp:checkboxlist>
													<mem:adgroupmember id="memDeleteModuleRoles" runat="server" visible="false"></mem:adgroupmember></td>
											</TR>
											<tr>
												<td>&nbsp;
												</td>
												<td colSpan="3">
													<hr noShade SIZE="1">
												</td>
											</tr>
											<TR>
												<td class="SubHead" vAlign="top" width="200">
													<tra:literal id="Literal9" runat="server" TextKey="MODULESETTINGS_SUPPORT_WORKFLOW" Text="Enable workflow"></tra:literal>:
												</td>
												<td colSpan="3"><asp:checkbox id="enableWorkflowSupport" Runat="server" AutoPostBack="True"></asp:checkbox></td>
											</TR>
											<TR>
												<td class="SubHead" vAlign="top" width="200"><tra:literal id="Literal10" runat="server" TextKey="MODULESETTINGS_ROLES_APPROVING" Text="Approve roles"></tra:literal>:
												</td>
												<td colSpan="3" noWrap><asp:checkboxlist id="authApproveRoles" runat="server" width="300" RepeatColumns="2" CssClass="Normal"
													cellspacing="0" cellpadding="0"></asp:checkboxlist>
												<mem:adgroupmember id="memApproveRoles" runat="server" visible="false"></mem:adgroupmember></td>
											</TR>
											<TR>
												<td class="SubHead" vAlign="top" width="200"><tra:literal id="Literal11" runat="server" TextKey="MODULESETTINGS_ROLES_PUBLISHING" Text="Publishing roles"></tra:literal>:
												</td>
												<td colSpan="3" noWrap><asp:checkboxlist id="authPublishingRoles" runat="server" width="300" RepeatColumns="2" CssClass="Normal"
													cellspacing="0" cellpadding="0"></asp:checkboxlist>
												<mem:adgroupmember id="memPublishingRoles" runat="server" visible="false"></mem:adgroupmember></td>
											</TR>
											<tr>
												<td>&nbsp;
												</td>
												<td colSpan="3">
													<hr noShade SIZE="1">
												</td>
											</tr>
											<tr>
												<td class="SubHead" noWrap width="200"><tra:literal id="Literal12" runat="server" TextKey="SHOWMOBILE" Text="Show to mobile users"></tra:literal>:
												</td>
												<td colSpan="3"><asp:checkbox id="ShowMobile" runat="server" CssClass="Normal"></asp:checkbox></td>
											</tr>
											<tr>
												<td>&nbsp;
												</td>
												<td colSpan="3">
													<hr noShade SIZE="1">
												</td>
											</tr>
											<tr>
												<td class="SubHead" noWrap width="200"><tra:literal id="Literal14" runat="server" TextKey="MODULESETTINGS_SHOW_EVERYWHERE" Text="Show on every page?"></tra:literal>:
												</td>
												<td colSpan="3"><asp:checkbox id="showEveryWhere" runat="server" CssClass="Normal"></asp:checkbox></td>
											</tr>
											<tr>
												<td class="SubHead" noWrap width="200"><tra:literal id="Literal15" runat="server" text="Can collapse window?" textkey="MODULESETTINGS_SHOW_COLLAPSABLE"></tra:literal>:
												</td>
												<td colspan="3"><asp:checkbox id="allowCollapsable" runat="server" CssClass="Normal"></asp:checkbox></td>
											</tr>
											<tr>
												<td colSpan="4">
													<hr noShade SIZE="1">
												</td>
											</tr>
											<tr>
												<td colSpan="4">
													<p><asp:placeholder id="PlaceHolderButtons" runat="server"></asp:placeholder>&nbsp;
													</p>
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
