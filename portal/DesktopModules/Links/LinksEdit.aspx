<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Page Language="c#" CodeBehind="LinksEdit.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.LinksEdit" %>
<html>
<head runat="server"></head>
	<body runat="server">
		<form runat="server">
		<div class="rb_AlternateLayoutDiv">
			<table class="rb_AlternateLayoutTable">
				<tr valign="top">
					<td class="rb_AlternatePortalHeader" valign="top">
						<portal:Banner id="SiteHeader" runat="server" />
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
												<tra:Literal runat="server" TextKey="LINKDETAILS" Text="Link detail" id="Literal1" />
											</td>
										</tr>
										<tr>
											<td colspan="2">
												<hr noshade size="1">
											</td>
										</tr>
									</table>
									<table width="750" cellspacing="0" cellpadding="0" border="0">
										<tr>
											<td width="100" class="SubHead">
												<tra:Literal runat="server" TextKey="TITLE" Text="Title" id="Literal2" />:
											</td>
											<td rowspan="6" height="295">&nbsp;
												
											</td>
											<td>
												<asp:TextBox id="TitleField" CssClass="NormalTextBox" width="390" Columns="30" maxlength="150" runat="server" />
											</td>
											<td width="25" rowspan="6" height="295">&nbsp;
												
											</td>
											<td class="Normal" width="250">
												<tra:RequiredFieldValidator id="Req1" Display="Static" TextKey="ERROR_VALID_TITLE" ErrorMessage="You Must Enter a Valid Title" ControlToValidate="TitleField" runat="server" />
											</td>
										</tr>
										<tr>
											<td class="SubHead">
												<tra:Literal runat="server" TextKey="URL" Text="Url" id="Literal3" />:
											</td>
											<td>
												<asp:TextBox id="UrlField" CssClass="NormalTextBox" width="390" Columns="30" maxlength="150" runat="server" />
											</td>
											<td class="Normal">
												<tra:RequiredFieldValidator id="Req2" Display="Dynamic" runat="server" TextKey="ERROR_VALID_URL" ErrorMessage="You Must Enter a Valid URL" ControlToValidate="UrlField" />
											</td>
										</tr>
										<tr>
											<td class="SubHead">
												<tra:Literal runat="server" TextKey="MOBILEURL" Text="Mobile Url" id="Literal4" />:
											</td>
											<td>
												<asp:TextBox id="MobileUrlField" CssClass="NormalTextBox" width="390" Columns="30" maxlength="150" runat="server" />
											</td>
											<td>&nbsp;
												
											</td>
										</tr>
										<tr>
											<td class="SubHead">
												<tra:Literal runat="server" TextKey="DESCRIPTION" Text="Description" id="Literal5" />:
											</td>
											<td>
												<asp:TextBox id="DescriptionField" CssClass="NormalTextBox" width="390" Columns="30" maxlength="150" runat="server" />
											</td>
											<td>&nbsp;
												
											</td>
										</tr>
										<tr>
											<td class="SubHead">
												<tra:Literal runat="server" TextKey="TARGET" Text="Target" id="Literal6" />:
											</td>
											<td>
												<asp:DropDownList id="TargetField" runat="server" Width="390px" CssClass="NormalTextBox"></asp:DropDownList>
											</td>
											<td>&nbsp;
												
											</td>
										</tr>
										<tr>
											<td class="SubHead">
												<tra:Literal runat="server" TextKey="VIEWORDER" Text="View Order" id="Literal7" />:
											</td>
											<td>
												<asp:TextBox id="ViewOrderField" CssClass="NormalTextBox" width="390" Columns="30" maxlength="3" runat="server" />
											</td>
											<td class="Normal">
												<tra:RequiredFieldValidator Display="Static" TextKey="ERROR_VALID_VIEWORDER" id="RequiredViewOrder" runat="server" ControlToValidate="ViewOrderField" ErrorMessage="You Must Enter a Valid View Order" />
												<tra:CompareValidator Display="Static" TextKey="ERROR_VALID_VIEWORDER" id="VerifyViewOrder" runat="server" Operator="DataTypeCheck" ControlToValidate="ViewOrderField" Type="Integer" ErrorMessage="You Must Enter a Valid View Order" />
											</td>
										</tr>
									</table>
									<p>
										<asp:LinkButton id="updateButton" Text="Update" runat="server" Cssclass="CommandButton" />
										&nbsp;
										<asp:LinkButton id="cancelButton" Text="Cancel" CausesValidation="False" runat="server" Cssclass="CommandButton" />
										&nbsp;
										<asp:LinkButton id="deleteButton" Text="Delete this item" CausesValidation="False" runat="server" Cssclass="CommandButton" />
										<hr noshade size="1" width="500">
										<span class="Normal">
											<tra:Literal runat="server" TextKey="CREATEDBY" Text="Created by" id="Literal8" />
											&nbsp;
											<asp:label id="CreatedBy" runat="server" />
											&nbsp;
											<tra:Literal runat="server" TextKey="ON" Text="on" id="Literal9" />
											&nbsp;
											<asp:label id="CreatedDate" runat="server" />
											<br>
										</span>
									<P>
									</P>
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
