<%@ Page Language="c#" codebehind="TasksEdit.aspx.cs" autoeventwireup="false" Inherits="Rainbow.DesktopModules.TasksEdit" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="Date" Namespace="PeterBlum.DateTextBoxControls" Assembly="DateTextBoxControls" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form id="form1" runat="server">
			<div class="rb_AlternateLayoutDiv">
				<table class="rb_AlternateLayoutTable">
					<tbody>
						<tr vAlign="top">
							<td class="rb_AlternatePortalHeader" valign="top"><portal:banner id="SiteHeader" runat="server"></portal:banner></td>
						</tr>
						<tr>
							<td><br>
								<table cellSpacing="0" cellPadding="4" width="98%" border="0">
									<tbody>
										<tr vAlign="top">
											<td width="100"></td>
											<td width="*">
												<table cellSpacing="0" cellPadding="0" width="500">
													<tbody>
														<tr>
															<td class="Head" align="left"><tra:literal id="Literal1" runat="server" Text="Task Detail" TextKey="TASK_DETAIL" /></td>
														</tr>
														<tr>
															<td colSpan="2">
																<hr noshade size="1" width="600">
															</td>
														</tr>
													</tbody>
												</table>
												<table cellSpacing="0" cellPadding="0" width="750">
													<tbody>
														<tr vAlign="top">
															<td class="SubHead" width="179"><tra:literal id="Literal2" runat="server" Text="Title" TextKey="TASK_TITLE" />:
															</td>
															<td rowSpan="8" width="5"></td>
															<td width="391"><asp:textbox id="TitleField" runat="server" maxlength="150" Columns="30" width="390" cssclass="NormalTextBox"></asp:textbox></td>
															<td width="25" rowSpan="8"></td>
															<td class="Normal" width="218"><tra:requiredfieldvalidator id="RequiredTitle" runat="server" TextKey="TASK_TITLE_ERROR" ErrorMessage="You must enter a valid title" ControlToValidate="TitleField" Display="Dynamic"></tra:requiredfieldvalidator></td>
														</tr>
														<tr vAlign="top">
															<td class="SubHead" width="179"><tra:literal id="Literal3" runat="server" Text="Description" TextKey="TASK_DESCRIPTION" />:
															</td>
															<td width="391">
																<asp:placeholder id="DescriptionField" runat="server"></asp:placeholder>
															</td>
															<td class="Normal" width="218"></td>
														</tr>
														<tr vAlign="top">
															<td class="SubHead" width="179"><tra:literal id="Literal4" runat="server" Text="% Complete" TextKey="TASK_COMPLETION" />:
															</td>
															<td width="391"><asp:textbox id="PercentCompleteField" runat="server" maxlength="150" Columns="30" width="50px" cssclass="NormalTextBox">0</asp:textbox>&nbsp;%</td>
															<td class="Normal" width="218"><tra:rangevalidator id="PercentValidator" runat="server" TextKey="TASK_COMPLETION_ERROR" ErrorMessage="Must be an integer from 0 to 100" ControlToValidate="PercentCompleteField" MinimumValue="0" MaximumValue="100" Type="Integer"></tra:rangevalidator></td>
														<TR vAlign="top">
															<TD class="SubHead" width="179"><tra:literal id="Literal5" runat="server" Text="Status" TextKey="TASK_STATUS" />:</TD>
															<TD width="391"><asp:dropdownlist id="StatusField" runat="server" Width="120px" CssClass="NormalTextBox"></asp:dropdownlist></TD>
														</TR>
														<TR vAlign="top">
															<TD class="SubHead" width="179"><tra:literal id="Literal6" runat="server" Text="Priority" TextKey="TASK_PRIORITY" />:</TD>
															<TD width="391"><asp:dropdownlist id="PriorityField" runat="server" Width="120px" CssClass="NormalTextBox"></asp:dropdownlist></TD>
															<TD width="218"></TD>
														</TR>
														<TR vAlign="top">
															<TD class="SubHead" width="179"><tra:literal id="Literal7" runat="server" Text="Assigned To" TextKey="TASK_ASSIGNEDTO" />:
															</TD>
															<TD width="391"><asp:textbox id="AssignedField" runat="server" cssclass="NormalTextBox" Width="197px"></asp:textbox></TD>
															<TD width="218"></TD>
														</TR>


														<TR vAlign="top">
															<TD class="SubHead" width="179">
																<tra:literal id="Literal8" runat="server" Text="Start Date" TextKey="TASK_STARTDATE" />:
															</TD>

															<TD><Date:DateTextBox id="StartField" runat="server" xStartRangeControlID="StartField" xPopupWidth="150" DESIGNTIMEDRAGDROP="292" xEndRangeControlID="DueField"></Date:DateTextBox></TD>
															<TD>
																<tra:RequiredFieldValidator id="RequiredEndDate" CssClass="Normal" runat="server" ControlToValidate="StartField" Display="Dynamic" TextKey="TASK_STARTDATE_ERROR" ErrorMessage="You Must Enter a Valid Start Date"></tra:RequiredFieldValidator>
																<Date:DateTextBoxValidator id="VerifyEndDate" runat="server" controltovalidate="StartField" TextKey="TASK_STARTDATE_ERROR" ErrorMessage="You Must Enter a Valid Start Date"></Date:DateTextBoxValidator>
															</TD>
														</TR>

														<TR vAlign="top">
															<TD class="SubHead"><tra:literal id="Literal9" runat="server" Text="Due Date" TextKey="TASK_DUEDATE" />:
															</TD>
															<TD width="391">
																<Date:DateTextBox id="DueField" runat="server" xPopupWidth="150" xStartRangeControlID="StartField" xEndRangeControlID="DueField"></Date:DateTextBox></TD>
															<TD class="Normal">
																<tra:RequiredFieldValidator id="Requiredfieldvalidator1" CssClass="Normal" runat="server" ControlToValidate="DueField" Display="Dynamic" TextKey="TASK_DUEDATE_ERROR" ErrorMessage="You Must Enter a Valid Due Date"></tra:RequiredFieldValidator>
																<Date:DateTextBoxValidator id="Datetextboxvalidator1" runat="server" controltovalidate="DueField" TextKey="TASK_DUEDATE_ERROR" ErrorMessage="You Must Enter a Valid Due Date"></Date:DateTextBoxValidator>
															</TD>
														</TR>
													</tbody>
												</table>
												<p>
													<tra:LinkButton id="updateButton" Text="Update" runat="server" class="CommandButton" />
													&nbsp;
													<tra:LinkButton id="cancelButton" Text="Cancel" CausesValidation="False" runat="server" class="CommandButton" />
													&nbsp;
													<tra:LinkButton id="deleteButton" Text="Delete this item" CausesValidation="False" runat="server" class="CommandButton" />
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
												<asp:Literal id="PickDateCalendarScript" runat="server"></asp:Literal>
											</td>
										</tr>
									</tbody>
								</table>
							</td>
						</tr>
						<tr>
							<td class="rb_AlternatePortalFooter"></td>
						</tr>
					</tbody>
				</table>
			</div>
		</form>
	</body>
</html>
