<%@ Page Language="c#" CodeBehind="SurveyEdit.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.SurveyEdit" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>

<html>
	<head runat="server"></head>
	<body runat="server">
		<form id="form1" method="post" runat="server">
			<div class="rb_AlternateLayoutDiv">
				<table class="rb_AlternateLayoutTable">
					<tr vAlign="top">
						<td class="rb_AlternatePortalHeader"><portal:banner id="SiteHeader" runat="server"></portal:banner></td>
					</tr>
					<tr>
						<td><br>
							<table cellSpacing="0" cellPadding="4" width="98%" border="0">
								<tr vAlign="top">
									<td width="150">&nbsp;</td>
									<td width="*">
										<table cellSpacing="0" cellPadding="0" width="500">
											<tr>
												<td class="Head" align="left"><tra:label id="lblTitle" runat="server" Cssclass="Head" TextKey="SURVEY_DETAILS" Text="Survey Details"></tra:label></td>
											</tr>
											<tr>
												<td colSpan="2">
													<hr noShade SIZE="1">
												</td>
											</tr>
										</table>
										<table cellSpacing="0" cellPadding="0" width="500" border="0">
											<tr vAlign="top">
												<td class="SubHead"><tra:label id="label11" runat="server" TextKey="SURVEY_TITLE" Text="Survey:" Font-Bold="True" CssClass="Title" width="100px"></tra:label></td>
												<td colSpan="2"><asp:label id="lblDescSurvey" runat="server" Height="48px" CssClass="Title" Width="100%"></asp:label></td>
											</tr>
											<tr>
												<td colSpan="3"><tra:linkbutton id="addBtn" onclick="AddBtn_Click" runat="server" TextKey="SURVEY_ADDNEWQUESTION" Text="Add New Question" cssclass="CommandButton"></tra:linkbutton></td>
											</tr>
											<tr>
												<td colSpan="3"><br>
												</td>
											</tr>
											<tr vAlign="top">
												<td class="SubHead"><tra:label id="lblNewQuestion" runat="server" Cssclass="Normal" TextKey="SURVEY_NEWQUESTION" Text="New Question:" Height="60px" Visible="False"></tra:label></td>
												<td><asp:textbox id="txtNewQuestion" runat="server" Height="48px" CssClass="NormalTextBox" Visible="False" width="100%"></asp:textbox></td>
												<td class="Normal">&nbsp;<tra:requiredfieldvalidator id="ReqQuestion" runat="server" TextKey="SURVEY_QUESTION_ERR" CssClass="Normal" Width="266px" Visible="False" ControlToValidate="txtNewQuestion" ErrorMessage="Please, insert the question."></tra:requiredfieldvalidator></td>
											</tr>
											<tr>
												<td class="SubHead"><tra:label id="lblOptionType" runat="server" Cssclass="Normal" TextKey="SURVEY_OPTIONTYPE" Text="Option Type:" Height="27px" Width="100px" Visible="False"></tra:label></td>
												<td colSpan="2"><asp:radiobutton id="RdBtnCheck" runat="server" Text="Checkboxes" Height="33px" CssClass="Normal" Width="100px" Visible="False" GroupName="Type"></asp:radiobutton><asp:radiobutton id="RdBtnRadio" runat="server" Text="Radio buttons" Height="33px" CssClass="Normal" Visible="False" GroupName="Type" Checked="True"></asp:radiobutton></td>
											</tr>
											<tr>
												<td height="25" colspan="3"><asp:linkbutton id="AddQuestionBtn" onclick="AddQuestionBtn_Click" runat="server" CssClass="CommandButton" Visible="False"></asp:linkbutton>
													<tra:linkbutton id="btnCancel" onclick="btnCancel_Click" runat="server" TextKey="CANCEL" Text="Cancel" CssClass="CommandButton" Visible="False"></tra:linkbutton></td>
											</tr>
											<tr>
												<td colSpan="3"><br>
												</td>
											</tr>
											<tr valign="top">
												<td class="SubHead">
													<tra:label id="lblQuestion" runat="server" width="100px" TextKey="SURVEY_QUESTIONS" Text="Questions:" CssClass="Normal" /></td>
												<td><asp:listbox id="QuestionList" runat="server" Height="106px" CssClass="NormalTextBox" width="400px" rows="5" /></td>
												<td>
													<table>
														<tr>
															<td><tra:imagebutton id="upBtn" onclick="UpDown_Click" runat="server" TextKey="MOVEUP" Text="Move up" CommandName="up"/></td>
														</tr>
														<tr>
															<td><tra:imagebutton id="downBtn" onclick="UpDown_Click" runat="server" TextKey="MOVEDOWN" Text="Move down" CommandName="down"/></td>
														</tr>
														<tr>
															<td><tra:imagebutton id="editBtn" onclick="EditBtn_Click" runat="server" TextKey="EDITBTN" Text="Edit button"/></td>
														</tr>
														<tr>
															<td><tra:imagebutton id="deleteBtn" onclick="DeleteBtn_Click" runat="server" TextKey="DELETEBTN" Text="Delete button"/></td>
														</tr>
													</table>
												</td>
											</tr>
											<tr>
												<td colspan="3">
													<tra:linkbutton id="btnBackSurvey" onclick="btnBackSurvey_Click" runat="server" TextKey="RETURN" Text="Return" CssClass="CommandButton"></tra:linkbutton>
												</td>
											</tr>
										</table>
										<p>
											<hr width="500" noShade SIZE="1">
											<span class="Normal"><tra:literal id="CreatedLabel" runat="server" TextKey="CREATED_BY" Text="Created by"></tra:literal>&nbsp; <asp:label id="CreatedBy" runat="server"></asp:label>&nbsp; 
											<tra:literal id="OnLabel" runat="server" TextKey="ON" Text="on"></tra:literal>&nbsp; <asp:label id="CreatedDate" runat="server"></asp:label></span>
									</td>
								</tr>
							</table>
						</td>
					</tr>
                    <tr> 
						<td class="rb_AlternatePortalFooter"> 
							<foot:Footer id="Footer" runat="server"></foot:Footer> 
                        </td> 
                    </tr> 
				</table>
			</div>
		</form>
	</body>
</html>
