<%@ Page language="c#" CodeBehind="TabLayout.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.Admin.TabLayout" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Register TagPrefix="cnf" Namespace="Rainbow.Configuration" Assembly="Rainbow" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ADGroupMember" Src="~/DesktopModules/Admin/ADGroupMember.ascx" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form runat="server">
			<div class="rb_AlternateLayoutDiv">
				<table class="rb_AlternateLayoutTable">
					<tr vAlign="top">
						<td class="rb_AlternatePortalHeader" vAlign="top"><portal:banner id="Banner1" runat="server" ShowTabs="false"></portal:banner></td>
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
												<td colSpan="4">
													<table cellSpacing="0" cellPadding="0" width="100%">
														<tr>
															<td class="Head" align="left"><tra:literal id="tab_name" runat="server" Text="Tab Layouts" TextKey="AM_TABNAME"></tra:literal></td>
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
												<td class="Normal" width="100"><tra:literal id="tab_name1" runat="server" Text="Tab Name" TextKey="AM_TABNAME1"></tra:literal></td>
												<td colSpan="3"><asp:textbox id="tabName" runat="server" cssclass="NormalTextBox" width="300" maxlength="50"></asp:textbox></td>
											</tr>
											<tr>
												<td class="Normal" noWrap>
													<tra:Literal TextKey="AM_ROLESAUTH" Text="Authorized Roles" id="roles_auth" runat="server"></tra:Literal>
												</td>
												<td colSpan="3">
													<asp:checkboxlist id="authRoles" runat="server" width="300" RepeatColumns="2" CssClass="Normal"></asp:checkboxlist>
													<uc1:ADGroupMember id="memRoles" runat="server" visible="false"></uc1:ADGroupMember>
												</td>
											</tr>
											<TR>
												<td class="Normal" noWrap><tra:literal id="tab_parent" runat="server" Text="Parent Tab" TextKey="TAB_PARENT"></tra:literal></td>
												<td colSpan="3"><asp:dropdownlist id="parentTab" runat="server" CssClass="NormalTextBox" Width="300px" DataTextField="TabName"
														DataValueField="TabID"></asp:dropdownlist><tra:label id="lblErrorNotAllowed" runat="server" TextKey="ERROR_NOT_ALLOWED_PARENT" CssClass="Error"
														EnableViewState="False" Visible="False">Not allowed to choose that parent</tra:label></td>
											</TR>
											<tr>
												<td>&nbsp;
												</td>
												<td colSpan="3">
													<hr noShade SIZE="1">
												</td>
											</tr>
											<tr>
												<td class="Normal" noWrap><tra:Literal TextKey="AM_SHOWMOBILE" Text="Show to mobile users" id="show_mobile" runat="server"></tra:Literal></td>
												<td colSpan="3"><asp:checkbox id="showMobile" runat="server" CssClass="Normal"></asp:checkbox></td>
											</tr>
											<tr>
												<td class="Normal" noWrap><tra:literal id="mobiletab" runat="server" Text="Mobile Tab Name" TextKey="AM_MOBILETAB"></tra:literal></td>
												<td colSpan="3"><asp:textbox id="mobileTabName" runat="server" cssclass="NormalTextBox" width="300"></asp:textbox></td>
											</tr>
											<tr>
												<td colSpan="4">
													<hr noShade SIZE="1">
												</td>
											</tr>
											<tr>
												<td class="Normal"><tra:literal id="addmodule" runat="server" Text="Add module" TextKey="AM_ADDMODULE"></tra:literal></td>
												<td class="Normal"><tra:literal id="module_type" runat="server" Text="Module type" TextKey="AM_MODULETYPE"></tra:literal></td>
												<td colSpan="2"><asp:dropdownlist id="moduleType" runat="server" CssClass="NormalTextBox" DataTextField="FriendlyName"
														DataValueField="ModuleDefID"></asp:dropdownlist></td>
											</tr>
											<tr>
												<td></td>
												<td class="Normal"><tra:literal id="moduleLocationLabel" runat="server" Text="Module Location:" TextKey="AM_MODULELOCATION"></tra:literal></td>
												<td vAlign="top" colSpan="2"><asp:dropdownlist id="paneLocation" runat="server">
														<asp:ListItem Value="LeftPane">Left Column</asp:ListItem>
														<asp:ListItem Value="ContentPane" Selected="True">Center Column</asp:ListItem>
														<asp:ListItem Value="RightPane">Right Column</asp:ListItem>
													</asp:dropdownlist></td>
											</tr>
											<tr>
												<td></td>
												<td vAlign="top" class="Normal"><tra:literal id="moduleVisibleLabel" runat="server" Text="Module Visible To:" TextKey="AM_MODULEVISIBLETO"></tra:literal></td>
												<td vAlign="top" colSpan="2"><asp:dropdownlist id="viewPermissions" runat="server">
														<asp:ListItem Value="All Users;" Selected="True">All Users</asp:ListItem>
														<asp:ListItem Value="Authenticated Users;">Authenticated Users</asp:ListItem>
														<asp:ListItem Value="Unauthenticated Users;">Unauthenticated Users</asp:ListItem>
														<asp:ListItem Value="Admins;">Admins Role</asp:ListItem>
													</asp:dropdownlist></td>
											</tr>
											<tr>
												<td>&nbsp;
												</td>
												<td class="Normal"><tra:literal id="module_name" runat="server" Text="Module Name" TextKey="AM_MODULENAME"></tra:literal></td>
												<td colSpan="2"><asp:textbox id="moduleTitle" runat="server" Text="New Module Name" cssclass="NormalTextBox"
														width="250" EnableViewState="false"></asp:textbox></td>
											</tr>
											<tr>
												<td>&nbsp;
												</td>
												<td colSpan="3"><tra:linkbutton id="AddModuleBtn" runat="server" Text="Add to 'Organize Modules' Below" TextKey="AM_ADDMODULEBELOW"
														cssclass="CommandButton"></tra:linkbutton></td>
											</tr>
											<tr>
												<td>&nbsp;
												</td>
												<td colSpan="3">
													<hr noShade SIZE="1">
												</td>
											</tr>
											<tr vAlign="top">
												<td class="Normal"><tra:literal id="organizemodule" runat="server" Text="Organize Module" TextKey="AM_ORGANIZEMODULE"></tra:literal></td>
												<td width="120">
													<table cellSpacing="0" cellPadding="2" width="100%" border="0">
														<tr>
															<td class="NormalBold"><tra:literal id="LeftPanel" runat="server" Text="Left Pane" TextKey="AM_LEFTPANEL"></tra:literal></td>
														</tr>
														<tr vAlign="top">
															<td>
																<table cellSpacing="2" cellPadding="0" border="0">
																	<tr vAlign="top">
																		<td rowSpan="2"><asp:listbox id=leftPane runat="server" width="110" CssClass="NormalTextBox" DataTextField="Title" DataValueField="ID" rows="8" DataSource="<%# leftList %>">
																			</asp:listbox></td>
																		<td vAlign="top" noWrap><tra:imagebutton id="LeftUpBtn" runat="server" Text="Move Up" TextKey="MOVEUP" CommandArgument="leftPane"
																				CommandName="up" ></tra:imagebutton><br>
																			<tra:imagebutton id="LeftRightBtn" runat="server" Text="Move Right" TextKey="MOVERIGHT" CommandName="right"
																				targetpane="contentPane" sourcepane="leftPane"></tra:imagebutton><br>
																			<tra:imagebutton id="LeftDownBtn" runat="server" Text="Move Down" TextKey="MOVEDOWN" CommandArgument="leftPane"
																				CommandName="down"></tra:imagebutton>&nbsp;&nbsp;
																		</td>
																	</tr>
																	<tr>
																		<td vAlign="bottom" noWrap><tra:imagebutton id="LeftEditBtn" runat="server" Text="Edit" TextKey="EDIT" CommandArgument="leftPane"
																				CommandName="edit"></tra:imagebutton><br><br>
																			<tra:imagebutton id="LeftDeleteBtn" runat="server" Text="Delete" TextKey="DELETE" CommandArgument="leftPane"
																				CommandName="delete"></tra:imagebutton></td>
																	</tr>
																</table>
															</td>
														</tr>
													</table>
												</td>
												<td width="*">
													<table cellSpacing="0" cellPadding="2" width="100%" border="0">
														<tr>
															<td class="NormalBold">&nbsp;
																<tra:literal id="contentpanel" runat="server" Text="Content Pane" TextKey="AM_CENTERPANEL"></tra:literal></td>
														</tr>
														<tr>
															<td align="center">
																<table cellSpacing="2" cellPadding="0" border="0">
																	<tr vAlign="top">
																		<td rowSpan="2"><asp:listbox id=contentPane runat="server" width="170" CssClass="NormalTextBox" DataTextField="Title" DataValueField="ID" rows="8" DataSource="<%# contentList %>">
																			</asp:listbox></td>
																		<td vAlign="top" noWrap><tra:imagebutton id="ContentUpBtn" runat="server" Text="Move Up" TextKey="MOVEUP" CommandArgument="contentPane"
																				CommandName="up"></tra:imagebutton><br>
																			<tra:imagebutton id="ContentLeftBtn" runat="server" Text="Move Left" TextKey="MOVELEFT" 
																				targetpane="leftPane" sourcepane="contentPane"></tra:imagebutton><br>
																			<tra:imagebutton id="ContentRightBtn" runat="server" Text="Move Right" TextKey="MOVERIGHT" 
																				targetpane="rightPane" sourcepane="contentPane"></tra:imagebutton><br>
																			<tra:imagebutton id="ContentDownBtn" runat="server" Text="Move Down" TextKey="MOVEDOWN" CommandArgument="contentPane"
																				CommandName="down" ></tra:imagebutton>&nbsp;&nbsp;
																		</td>
																	</tr>
																	<tr>
																		<td vAlign="bottom" noWrap><tra:imagebutton id="ContentEditBtn" runat="server" Text="Edit" TextKey="EDIT" CommandArgument="contentPane"
																				CommandName="edit"></tra:imagebutton><br><br>
																			<tra:imagebutton id="ContentDeleteBtn" runat="server" Text="Delete" TextKey="DELETE" CommandArgument="contentPane"
																				CommandName="delete"></tra:imagebutton></td>
																	</tr>
																</table>
															</td>
														</tr>
													</table>
												</td>
												<td width="120">
													<table cellSpacing="0" cellPadding="2" width="100%" border="0">
														<tr>
															<td class="NormalBold">&nbsp;
																<tra:literal id="rightpanel" runat="server" Text="Right Pane" TextKey="AM_RIGHTPANEL"></tra:literal></td>
														</tr>
														<tr>
															<td>
																<table cellSpacing="2" cellPadding="0" border="0">
																	<tr vAlign="top">
																		<td rowSpan="2"><asp:listbox id=rightPane runat="server" width="110" CssClass="NormalTextBox" DataTextField="Title" DataValueField="ID" rows="8" DataSource="<%# rightList %>">
																			</asp:listbox></td>
																		<td vAlign="top" noWrap><tra:imagebutton id="RightUpBtn" runat="server" Text="Move Up" TextKey="MOVEUP" CommandArgument="rightPane"
																				CommandName="up"></tra:imagebutton><br>
																			<tra:imagebutton id="RightLeftBtn" runat="server" Text="Move Left" TextKey="MOVELEFT"
																				targetpane="contentPane" sourcepane="rightPane"></tra:imagebutton><br>
																			<tra:imagebutton id="RightDownBtn" runat="server" Text="Move Down" TextKey="MOVEDOWN" CommandArgument="rightPane"
																				CommandName="down"></tra:imagebutton></td>
																	</tr>
																	<tr>
																		<td vAlign="bottom" noWrap><tra:imagebutton id="RightEditBtn" runat="server" Text="Edit" TextKey="EDIT" CommandArgument="rightPane"
																				CommandName="edit"></tra:imagebutton><br><br>
																			<tra:imagebutton id="RightDeleteBtn" runat="server" Text="Delete" TextKey="DELETE" CommandArgument="rightPane"
																				CommandName="delete"></tra:imagebutton></td>
																	</tr>
																</table>
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<tr>
												<td class="Error" align="center" colSpan="4">
													<tra:literal id="msgError" runat="server" Text="You do not have the appropriate permissions to delete or move this module"
														TextKey="AM_NO_RIGHTS"></tra:literal>
												</td>
											</tr>
											<tr>
												<td colSpan="4">
													<hr noShade SIZE="1">
													<cnf:settingstable id="EditTable" runat="server"></cnf:settingstable></td>
											</tr>
											<tr>
												<td colSpan="4"><tra:linkbutton class="CommandButton" id="updateButton" runat="server" Text="Apply Changes" TextKey="APPLY_CHANGES"></tra:linkbutton>&nbsp;
													<tra:linkbutton class="CommandButton" id="cancelButton" runat="server" Text="Cancel" TextKey="CANCEL"></tra:linkbutton>
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
