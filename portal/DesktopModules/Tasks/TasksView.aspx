<%@ Page Language="c#" codebehind="TasksView.aspx.cs" autoeventwireup="false" Inherits="Rainbow.DesktopModules.TaskView" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form runat="server">
			<div class="rb_DefaultLayoutDiv">
				<table class="rb_DefaultLayoutTable">
					<tbody>
						<tr valign="top">
							<td class="rb_DefaultPortalHeader" valign="top">
								<portal:Banner id="SiteHeader" runat="server" />
							</td>
						</tr>
						<tr>
							<td>
								<br>
								<table cellspacing="0" cellpadding="4" width="98%" border="0">
									<tbody>
										<tr valign="top">
											<td width="50">
											</td>
											<td>
												<table cellspacing="0" cellpadding="4" width="732">
													<tbody>
														<tr>
															<td class="Head" align="left" width="120" colspan="2">
																<tra:Literal id="Literal1" runat="server" TextKey="TASK_DETAIL" Text="Task Detail" />
															</td>
															<td align="right">
																<%= EditLink%>
															</td>
														</tr>
														<tr>
															<td colspan="3">
																<hr noshade size="1">
															</td>
														</tr>
													</tbody>
													<tbody>
														<tr>
															<td class="SubHead" valign="top" nowrap width="170">
																<tra:Literal id="Literal2" runat="server" TextKey="TASK_TITLE" Text="Title" />:
															</td>
															<td class="Normal" valign="top" colspan="2">
																<asp:Label id="TitleField" runat="server"></asp:Label>
															</td>
														</tr>
														<tr>
															<td class="SubHead" valign="top" nowrap width="170">
																<tra:Literal id="Literal3" runat="server" TextKey="TASK_DESCRIPTION" Text="Description" />:
															</td>
															<td valign="top" colspan="2">
																<asp:Label id="longdesc" runat="server">No Description Available</asp:Label>&nbsp;
															</td>
														</tr>
														<tr>
															<td class="SubHead" valign="top" nowrap width="170">
																<tra:Literal id="Literal4" runat="server" TextKey="TASK_COMPLETION" Text="% Complete" />:
															</td>
															<td class="Normal" valign="top" colspan="2">
																<asp:Label id="PercentCompleteField" runat="server"></asp:Label>
															</td>
														</tr>
														<tr>
															<td class="SubHead" valign="top" nowrap width="170">
																<tra:Literal id="Literal5" runat="server" TextKey="TASK_STATUS" Text="Status" />:
															</td>
															<td class="Normal" valign="top" colspan="2">
																<asp:Label id="StatusField" runat="server"></asp:Label>
															</td>
														</tr>
														<tr>
															<td class="SubHead" valign="top" nowrap width="170">
																<tra:Literal id="Literal6" runat="server" TextKey="TASK_PRIORITY" Text="Priority" />:
															</td>
															<td class="Normal" valign="top" colspan="2">
																<asp:Label id="PriorityField" runat="server"></asp:Label>
															</td>
														</tr>
														<tr>
															<td class="SubHead" valign="top" nowrap width="170">
																<tra:Literal id="Literal7" runat="server" TextKey="TASK_ASSIGNEDTO" Text="Assigned To" />:
															</td>
															<td class="Normal" valign="top" colspan="2">
																<asp:Label id="AssignedField" runat="server"></asp:Label>
															</td>
														</tr>
														<tr>
															<td class="SubHead" valign="top" nowrap width="170">
																<tra:Literal id="Literal8" runat="server" TextKey="TASK_STARTDATE" Text="Start Date" />:
															</td>
															<td class="Normal" valign="top" colspan="2">
																<asp:Label id="StartField" runat="server"></asp:Label>
															</td>
														</tr>
														<tr>
															<td class="SubHead" valign="top" nowrap width="170">
																<tra:Literal id="Literal9" runat="server" TextKey="TASK_DUEDATE" Text="Due Date" />
															:
															<td class="Normal" valign="top" colspan="2">
																<asp:Label id="DueField" runat="server"></asp:Label>
															</td>
														</tr>
													</tbody>
												</table>
												<p>
													<tra:LinkButton id="cancelButton" Text="Cancel" CausesValidation="False" runat="server" class="CommandButton" />
													<br>
													<hr noshade size="1" width="600">
													<span class="Normal">
														<tra:Literal TextKey="CREATED_BY" Text="Created by" id="CreatedLabel" runat="server" />
														<asp:label id="CreatedBy" runat="server" />
														<tra:Literal TextKey="ON" Text="on" id="OnLabel" runat="server" />
														<asp:label id="CreatedDate" runat="server" />
														<br>
														<tra:Literal TextKey="MODIFIED_BY" Text="Modified by" id="ModifiedLabel" runat="server" />
														<asp:label id="ModifiedBy" runat="server" />
														<tra:Literal TextKey="ON" Text="on" id="ModifiedOnLabel" runat="server" />
														<asp:label id="ModifiedDate" runat="server" />
													</span>
												<P></P>
											</td>
										</tr>
									</tbody>
								</table>
							</td>
						</tr>
						<tr>
							<td class="rb_DefaultPortalFooter"><foot:Footer id="Footer" runat="server"></foot:Footer></td>
						</tr>
					</tbody>
				</table>
			</div>
		</form>
	</body>
</html>
