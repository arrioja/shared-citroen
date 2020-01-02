<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Page Language="c#" CodeBehind="AnnouncementsEdit.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.AnnouncementsEdit" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form runat="server" enctype="multipart/form-data">
		<div class="rb_AlternateLayoutDiv">
			<table class="rb_AlternateLayoutTable">
				<tr vAlign="top">
					<td class="rb_AlternatePortalHeader" valign="top">
						<portal:banner id="SiteHeader" runat="server"></portal:banner>
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
										<table width="100%" cellspacing="0" cellpadding="0">
											<tr>
												<td align="left" class="Head">
													<tra:Literal TextKey="ANNOUNCES_DETAILS" Text="Announce details" id="Literal1" runat="server" />&nbsp;
												</td>
											</tr>
											<tr>
												<td colspan="2">
													<hr noshade size="1">
												</td>
											</tr>
										</table>
										<table width="850" cellspacing="0" cellpadding="0">
											<tr valign="top">
												<td width="100" class="SubHead">
													<tra:Literal TextKey="ANNOUNCES_TITLE" Text="Announce Title" id="Literal2" runat="server" />:
												</td>
												<td rowspan="5">&nbsp;
												</td>
												<td>
													<asp:TextBox id="TitleField" cssclass="NormalTextBox" width="390" Columns="30" maxlength="100"
														runat="server" />
												</td>
												<td width="25" rowspan="5">&nbsp;
												</td>
												<td class="Normal" width="250">
													<tra:RequiredFieldValidator TextKey="ERROR_VALID_TITLE" ErrorMessage="Please insert a valid title" id="RequiredTitle"
														Display="Dynamic" ControlToValidate="TitleField" runat="server" />
												</td>
											</tr>
											<tr valign="top">
												<td class="SubHead">
													<tra:Literal TextKey="ANNOUNCES_READ_MORE_LINK" Text="Read More Link" id="Literal3" runat="server" />:
												</td>
												<td>
													<asp:TextBox id="MoreLinkField" cssclass="NormalTextBox" width="390" Columns="30" maxlength="100"
														runat="server" />
												</td>
											</tr>
											<tr valign="top">
												<td class="SubHead" nowrap>
													<tra:Literal TextKey="ANNOUNCES_READ_MORE_MOBILE" Text="Read More Mobile Link" id="Literal4"
														runat="server" />:
												</td>
												<td>
													<asp:TextBox id="MobileMoreField" cssclass="NormalTextBox" width="390" Columns="30" maxlength="100"
														runat="server" />
												</td>
											</tr>
											<tr valign="top">
												<td class="SubHead">
													<tra:Literal TextKey="ANNOUNCES_DESCRIPTION" Text="Read Descriptions" id="Literal5" runat="server" />:
												</td>
												<td>
													<asp:placeholder id="PlaceHolderHTMLEditor" runat="server"></asp:placeholder>
												</td>
												<td class="Normal">
												</td>
											</tr>
											<tr valign="top">
												<td class="SubHead">
													<tra:Literal TextKey="ANNOUNCES_EXPIRES" Text="Expires on" id="Literal6" runat="server" />:
												</td>
												<td>
													<asp:TextBox id="ExpireField" cssclass="NormalTextBox" width="100" Columns="8" runat="server" />
												</td>
												<td class="Normal">
													<tra:RequiredFieldValidator TextKey="ERROR_VALID_EXPIRE_DATE" ErrorMessage="Please insert a valid expire date"
														Display="Dynamic" id="RequiredExpireDate" runat="server" ControlToValidate="ExpireField" />
													<tra:CompareValidator TextKey="ERROR_VALID_EXPIRE_DATE" ErrorMessage="Please insert a valid expire date"
														Display="Dynamic" id="VerifyExpireDate" runat="server" Operator="DataTypeCheck" ControlToValidate="ExpireField"
														Type="Date" />
												</td>
											</tr>
										</table>
										<p>
											<asp:placeholder id="PlaceHolderButtons" runat="server"></asp:placeholder>
										</p>
										<hr noshade size="1">
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
