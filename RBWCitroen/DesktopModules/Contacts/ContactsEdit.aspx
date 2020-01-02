<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Page Language="c#" CodeBehind="ContactsEdit.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.ContactsEdit" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
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
									<td>
										<table width="100%" cellspacing="0" cellpadding="0" border="0">
											<tr>
												<td align="left" class="Head">
													<tra:Literal TextKey="CONTACTS_DETAILS" Text="Conctact details" id="Literal1" runat="server" />
												</td>
											</tr>
											<tr>
												<td colspan="2">
													<hr noshade size="1">
												</td>
											</tr>
										</table>
										<table width="750" cellspacing="0" cellpadding="0" border="0">
											<tr valign="top">
												<td class="SubHead">
													<tra:Literal TextKey="CONTACTS_NAME" Text="Name" id="Literal2" runat="server" />:
												</td>
												<td rowspan="7">&nbsp;
												</td>
												<td align="left">
													<asp:TextBox id="NameField" cssclass="NormalTextBox" width="390" Columns="30" maxlength="50"
														runat="server" />
												</td>
												<td width="25" rowspan="7">&nbsp;
												</td>
												<td class="Normal" width="250">
													<tra:RequiredFieldValidator TextKey="ERROR_VALID_NAME" ErrorMessage="Please enter a vaild name" Display="Dynamic"
														runat="server" ControlToValidate="NameField" id="RequiredName" />
												</td>
											</tr>
											<tr valign="top">
												<td class="SubHead">
													<tra:Literal TextKey="CONTACTS_ROLE" Text="Role" id="Literal3" runat="server" />:
												</td>
												<td>
													<asp:TextBox id="RoleField" cssclass="NormalTextBox" width="390" Columns="30" maxlength="100"
														runat="server" />
												</td>
											</tr>
											<tr valign="top">
												<td class="SubHead">
													<tra:Literal TextKey="CONTACTS_EMAIL" Text="Email" id="Literal4" runat="server" />:
												</td>
												<td>
													<asp:TextBox id="EmailField" cssclass="NormalTextBox" width="390" Columns="30" maxlength="100"
														runat="server" />
												</td>
												<td class="Normal" width="250">
													<tra:RegularExpressionValidator id="ValidEmail" runat="server" TextKey="VALID_EMAIL" ControlToValidate="EmailField"
														Display="Dynamic" ErrorMessage="You must use a valid email address" ValidationExpression="[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+"></tra:RegularExpressionValidator>
												</td>
											</tr>
											<tr valign="top">
												<td class="SubHead">
													<tra:Literal TextKey="CONTACTS_CONTACT1" Text="Office" id="Literal5" runat="server" />:
												</td>
												<td>
													<asp:TextBox id="Contact1Field" cssclass="NormalTextBox" width="390" Columns="30" maxlength="250"
														runat="server" />
												</td>
											</tr>
											<tr valign="top">
												<td class="SubHead">
													<tra:Literal TextKey="CONTACTS_CONTACT2" Text="Mobile" id="Literal6" runat="server" />:
												</td>
												<td>
													<asp:TextBox id="Contact2Field" cssclass="NormalTextBox" width="390" Columns="30" maxlength="250"
														runat="server" />
												</td>
											</tr>
											<tr valign="top">
												<td class="SubHead">
													<tra:Literal TextKey="CONTACTS_FAX" Text="Fax" id="Literal7" runat="server" />:
												</td>
												<td>
													<asp:TextBox id="FaxField" cssclass="NormalTextBox" width="390" Columns="30" maxlength="250"
														runat="server" />
												</td>
											</tr>
											<tr valign="top">
												<td class="SubHead">
													<tra:Literal TextKey="CONTACTS_ADDRESS" Text="Address" id="Literal8" runat="server" />:
												</td>
												<td>
													<asp:TextBox id="AddressField" cssclass="NormalTextBox" width="390" Columns="30" maxlength="250"
														runat="server" />
												</td>
											</tr>
										</table>
										<p>
											<asp:LinkButton id="updateButton" Text="UPDATE" runat="server" CssClass="CommandButton" />
											&nbsp;
											<asp:LinkButton id="cancelButton" Text="CANCEL" CausesValidation="False" runat="server" CssClass="CommandButton" />
											&nbsp;
											<asp:LinkButton id="deleteButton" Text="Delete this item" CausesValidation="False" runat="server"
												CssClass="CommandButton" />
											<hr noshade size="1">
											<span class="Normal">
											<tra:Literal id="CreatedLabel" runat="server" Text="Created by" TextKey="CREATED_BY"></tra:Literal>&nbsp; 
											<asp:label id="CreatedBy" runat="server"></asp:label>&nbsp; 
											<tra:Literal id="OnLabel" runat="server" Text="on" TextKey="ON"></tra:Literal>&nbsp; 
											<asp:label id="CreatedDate" runat="server"></asp:label>
											</span>
										<P></P>
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
