<%@ Page Language="c#" codebehind="FAQsEdit.aspx.cs" autoeventwireup="false" Inherits="Rainbow.DesktopModules.FAQsEdit" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form runat="server">
			<div class="rb_AlternateLayoutDiv">
			<table class="rb_AlternateLayoutTable">
				<tr valign="top">
					<td class="rb_AlternatePortalHeader" valign="top">
						<portal:Banner ID="SiteHeader" runat="server" />
					</td>
				</tr>
				<tr>
					<td>
						<br>
						<table width="98%" cellspacing="0" cellpadding="4" border="0">
							<tr valign="top">
								<td width="150">&nbsp;
									
								</td>
								<td width="*">
									<table width="500" cellspacing="0" cellpadding="0">
										<tr>
											<td align="left" class="Head">
											<tra:Literal textkey="FAQ_DETAILS" text="FAQ Details" ID="Literal1" runat="server"></tra:Literal>
											</td>
										</tr>
										<tr>
											<td colspan="2">
												<hr noshade size="1">
											</td>
										</tr>
									</table>
									<table width="749" cellspacing="0" cellpadding="0" border="0" height="244">
										<tr>
											<td width="100" class="SubHead">
												<tra:Literal textkey="FAQ_QUESTION" text="Question" ID="Literal2" runat="server"></tra:Literal>
											</td>
											<td>
												<asp:TextBox id="Question" runat="server" TextMode="MultiLine" Height="60px" Width="401px" MaxLength="500" CssClass="NormalTextBox"></asp:TextBox>
											</td>
											<td class="Normal" width="266">
												<tra:RequiredFieldValidator textkey="FAQ_QUESTION_ERR" ID="RequiredFieldValidatorQuestion" runat="server" ErrorMessage="Please enter a question!" ControlToValidate="Question"></tra:RequiredFieldValidator>
											</td>
										</tr>
										<tr>
											<td width="100" class="SubHead">
												<tra:Literal textkey="FAQ_ANSWER" text="Answer" id="Literal3" runat="server"></tra:Literal>
											</td>
											<td>
											    <asp:placeholder id="PlaceHolderHTMLEditor" runat="server"></asp:placeholder>
											</td>
											<td class="Normal" width="266">
												&nbsp;
											</td>
										</tr>
									</table>
									<p>
										<tra:LinkButton id="updateButton" Text="Update" runat="server" class="CommandButton" />
										&nbsp;
										<tra:LinkButton id="cancelButton" Text="Cancel" CausesValidation="False" runat="server" class="CommandButton" />
										&nbsp;
										<tra:LinkButton id="deleteButton" Text="Delete this item" CausesValidation="False" runat="server" class="CommandButton" />
										<br>
										<hr noshade size="1" width="500">
										<span class="Normal">
											<tra:Literal TextKey="CREATED_BY" Text="Created by" id="CreatedLabel" runat="server"></tra:Literal>&nbsp;
											<asp:label id="CreatedBy" runat="server"></asp:label>&nbsp;
											<tra:Literal TextKey="ON" Text="on" id="OnLabel" runat="server"></tra:Literal>&nbsp;
											<asp:label id="CreatedDate" runat="server"></asp:label>
										</span>
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
