<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Page Language="c#" CodeBehind="MilestonesEdit.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.Milestones.MilestonesEdit" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form runat="server" enctype="multipart/form-data">
			<div class="rb_AlternateLayoutDiv">
				<TABLE class="rb_AlternateLayoutTable">
					<TR vAlign="top">
						<td class="rb_AlternatePortalHeader" valign="top"><portal:banner id="SiteHeader" runat="server"></portal:banner></td>
					</TR>
					<TR>
						<td><br>
							<table width="98%" border="0" celspacing="0" celpadding="4">
								<tr vAlign="top">
									<td width="150">&nbsp;</td>
									<td width="*">
										<table cellSpacing="0" cellPadding="0" width="520">
											<tr>
												<td class="Head" align="left"><tra:Literal TextKey="MILESTONES_DETAIL" Text="Milestones Details" runat="server" id="Literal1"></tra:Literal></td>
											</tr>
											<TR>
												<td colSpan="2">
													<hr noshade size="1">
												</td>
											</TR>
										</table>
										<table cellSpacing="0" cellPadding="0" width="750">
											<tr vAlign="top">
												<td class="Subhead" width="100" noWrap><tra:Literal TextKey="MILESTONES_TITLE" Text="Title" runat="server" id="Literal3"></tra:Literal>:
												</td>
												<td rowSpan="5">&nbsp;
												</td>
												<td><asp:textbox id="TitleField" runat="server" cssclass="NormalTextBox" Columns="30" MaxLength="100" Width="390"></asp:textbox></td>
												<td width="1" rowSpan="5">&nbsp;
												</td>
												<td class="Normal" width="250"><tra:requiredfieldvalidator id="Req1" runat="server" Display="Dynamic" TextKey="ERROR_VALID_MILESTONE_TITLE" ErrorMessage="You Must Enter a Valid Title" ControlToValidate="TitleField"></tra:requiredfieldvalidator></td>
											</tr>
											<tr vAlign="top">
												<td class="SubHead" noWrap><tra:Literal TextKey="MILESTONES_COMPLETION_DATE" Text="Estimated Completion Date" runat="server" id="Literal2"></tra:Literal>:</td>
												<td width="25">
													<P>
														<asp:textbox id="EstCompleteDate" runat="server" Columns="8" Width="100" CssClass="NormalTextBox"></asp:textbox>
													</P>
												</td>
												<td class="Normal" width="250"><tra:requiredfieldvalidator id="Req2" runat="server" Display="Dynamic" TextKey="ERROR_COMPLETION_DATE" ErrorMessage="Enter The Estimated Completion Date" ControlToValidate="EstCompleteDate"></tra:requiredfieldvalidator><tra:comparevalidator id="VerifyCompleteDate" runat="server" Display="Dynamic" TextKey="ERROR_VALID_COMPLETION_DATE" ErrorMessage="You must enter a valid date." ControlToValidate="EstCompleteDate" Type="Date" Operator="DataTypeCheck"></tra:comparevalidator></td>
											</tr>
											<tr vAlign="top">
												<td class="Subhead" vAlign="top" width="100" noWrap><tra:Literal TextKey="MILESTONES_STATUS" Text="Status of Milestone" runat="server" id="Literal4"></tra:Literal>:</td>
												<td><asp:textbox id="StatusBox" runat="server" Columns="44" Width="387px" CssClass="NormalTextBox" Height="101px" TextMode="MultiLine" Rows="6"></asp:textbox></td>
												<td rowSpan="5"><tra:requiredfieldvalidator id="Req3" Display="Dynamic" CssClass="Error" TextKey="ERROR_VALID_MILESTONE_STATUS" ErrorMessage="Enter The Status of this Milestone" ControlToValidate="StatusBox" Runat="server"></tra:requiredfieldvalidator></td>
												<td class="Normal"></td>
											</tr>
										</table>
										<p>
											<asp:linkbutton id="updateButton" CssClass="CommandButton" Runat="server" Text="Update"></asp:linkbutton>
											&nbsp;
											<asp:linkbutton id="cancelButton" CssClass="CommandButton" Runat="server" Text="Cancel" CausesValidation="False"></asp:linkbutton>
											&nbsp;
											<asp:linkbutton id="deleteButton" CssClass="CommandButton" Runat="server" Text="Delete this Item" CausesValidation="False"></asp:linkbutton>
											<hr width="520" noshade SIZE="1">
											<span class="Normal">
<tra:Literal id="CreatedLabel" runat="server" Text="Created by" TextKey="CREATED_BY"></tra:Literal>&nbsp; 
<asp:label id="CreatedBy" Runat="server"></asp:label>
<tra:Literal id="OnLabel" runat="server" Text="on" TextKey="ON"></tra:Literal>&nbsp; 
<asp:label id="CreatedDate" Runat="server"></asp:label>
            <br>
            </span>
									</td>
								</tr>
							</table>
						</td>
					</TR>
					<tr>
						<td class="rb_AlternatePortalFooter"><foot:Footer id="Footer" runat="server"></foot:Footer></td>
					</tr>
				</TABLE>
			</div>
		</form>
	</body>
</html>
