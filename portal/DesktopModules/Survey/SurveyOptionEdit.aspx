<%@ Page Language="c#" CodeBehind="SurveyOptionEdit.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.SurveyOptionEdit" %>
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
												<td class="Head" align="left"><tra:label id="lblTitle" runat="server" Text="Survey Details - Options" TextKey="SURVEY_DETAILSOPTIONS" Cssclass="Head"></tra:label></td>
											</tr>
											<tr>
												<td colSpan="2">
													<hr noShade SIZE="1">
												</td>
											</tr>
										</table>
										<table cellSpacing="0" cellPadding="0" width="500" border="0">
											<tr vAlign="top">
												<td class="SubHead"><tra:label id="label11" runat="server" CssClass="Title" Font-Bold="True" Text="Question:" width="100px" TextKey="SURVEY_QUESTION"></tra:label></td>
												<td colSpan="2"><asp:label id="lblQuestion" runat="server" Height="48px" CssClass="Title" Width="100%"></asp:label></td>
											</tr>
											<tr>
												<td class="SubHead"><tra:label id="Label1" runat="server" Width="100px" Text="Option Type:" TextKey="SURVEY_OPTIONTYPE" Cssclass="Normal"></tra:label></td>
												<td colSpan="2"><tra:label id="lblTypeOption" runat="server" CssClass="Normal" Width="100px"></tra:label></td>
											</tr>
											<tr>
												<td colSpan="3"><br><tra:linkbutton id="AddOptBtn" onclick="AddOptBtn_Click" runat="server" cssclass="CommandButton" Text="Add new option" TextKey="SURVEY_ADDNEWOPTION"></tra:linkbutton></td>
											</tr>
											<tr>
												<td colSpan="3"><br>
												</td>
											</tr>
											<tr vAlign="top">
												<td class="SubHead"><tra:label id="lblNewOption" runat="server" Height="60px" Text="New option:" Visible="False" TextKey="SURVEY_NEWOPTION" Cssclass="Normal"></tra:label></td>
												<td><asp:textbox id="TxtNewOption" runat="server" Height="48px" CssClass="NormalTextBox" Visible="False" width="100%"></asp:textbox></td>
												<td class="Normal">&nbsp;<tra:requiredfieldvalidator id="ReqNewOpt" runat="server" CssClass="Normal" Width="266px" Visible="False" ControlToValidate="TxtNewOption" ErrorMessage="Please, insert the option." TextKey="SURVEY_OPTION_ERR"></tra:requiredfieldvalidator></td>
											</tr>
											<tr>
												<td colSpan="3" height="25"><asp:linkbutton id="AddOptionBtn" onclick="AddOptionBtn_Click" runat="server" CssClass="CommandButton" Visible="False"></asp:linkbutton><tra:linkbutton id="CancelOptBtn" onclick="CancelOptBtn_Click" runat="server" CssClass="CommandButton" Text="Cancel" Visible="False" TextKey="CANCEL"></tra:linkbutton></td>
											</tr>
											<tr>
												<td colSpan="3"><br>
												</td>
											</tr>
											<tr vAlign="top">
												<td class="SubHead"><tra:label id="lblOption" runat="server" CssClass="Normal" Text="Options:" width="100px" TextKey="SURVEY_OPTIONS"></tra:label></td>
												<td><asp:listbox id="OptionList" runat="server" Height="106px" CssClass="NormalTextBox" width="400px" rows="5"></asp:listbox></td>
												<td>
													<table>
														<tr>
															<td><tra:imagebutton id="upBtn" onclick="UpDown_Click" runat="server" Text="Move up" CommandName="up" TextKey="MOVEUP"></tra:imagebutton></td>
														</tr>
														<tr>
															<td><tra:imagebutton id="downBtn" onclick="UpDown_Click" runat="server" Text="Move down" CommandName="down" TextKey="MOVEDOWN"></tra:imagebutton></td>
														</tr>
														<tr>
															<td><tra:imagebutton id="deleteBtn" onclick="deleteBtn_Click" runat="server" Text="Delete button" TextKey="DELETEBTN"></tra:imagebutton></td>
														</tr>
													</table>
												</td>
											</tr>
											<tr>
												<td colSpan="3"><tra:linkbutton id="btnReturnCancel" onclick="btnReturnCancel_Click" runat="server" CssClass="CommandButton" Text="Return" TextKey="RETURN"></tra:linkbutton></td>
											</tr>
										</table>
										<p>
											<hr width="500" noShade SIZE="1">
											<span class="Normal"><tra:literal id="CreatedLabel" runat="server" Text="Created by" TextKey="CREATED_BY"></tra:literal>&nbsp; <asp:label id="CreatedBy" runat="server"></asp:label>&nbsp; 
            <tra:literal id="OnLabel" runat="server" Text="on" TextKey="ON"></tra:literal>&nbsp; <asp:label id="CreatedDate" runat="server"></asp:label></span>
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
