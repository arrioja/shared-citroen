<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Page Language="c#" CodeBehind="BlogEdit.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.BlogEdit" %>
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
						<td><br>
							<table cellSpacing="0" cellPadding="4" width="98%" border="0">
								<tr vAlign="top">
									<td width="100">&nbsp;
									</td>
									<td width="*">
										<table cellSpacing="0" cellPadding="0" width="750">
											<tr>
												<td class="Head" align="left">
													<tra:Literal runat="server" Text="Blog Entry" TextKey="BLOG_ENTRY" id="Literal1"></tra:Literal>
												</td>
											</tr>
											<tr>
												<td colSpan="2">
													<hr noshade size="1">
												</td>
											</tr>
										</table>
										<table class="Normal" cellSpacing="4" cellPadding="0" width="726" border="0">
											<tr vAlign="top">
												<td class="SubHead" noWrap>
													<tra:Literal TextKey="START_DATE" Text="Start date" runat="server" ID="Literal2" />:
												</td>
												<td>&nbsp;
													<asp:textbox id="StartField" runat="server" maxlength="150" Columns="28" width="353" cssclass="NormalTextBox"></asp:textbox>
												</td>
												<td width="25" rowSpan="5"></td>
												<td class="Normal" width="250">
													<tra:requiredfieldvalidator TextKey="ERROR_VALID_STARTDATE" id="RequiredStartDate" runat="server" Display="Dynamic"
														ErrorMessage="You Must Enter a Valid Start Date" ControlToValidate="StartField"></tra:requiredfieldvalidator>
													
												</td>
											</tr>
											<tr vAlign="top">
												<td class="SubHead" noWrap>
													<tra:Literal TextKey="TITLE" Text="Title" runat="server" ID="Literal4" />:
												</td>
												<td>&nbsp;
													<asp:textbox id="TitleField" runat="server" maxlength="150" Columns="28" width="353" cssclass="NormalTextBox"></asp:textbox></td>
												<td class="Normal" width="250"><tra:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="You Must Enter a Valid Title"
														ControlToValidate="TitleField" TextKey="ERROR_VALID_TITLE"></tra:requiredfieldvalidator></td>
											</tr>
											<tr vAlign="top">
												<td class="SubHead" noWrap>
													<tra:Literal TextKey="EXCERPT" Text="Excerpt" runat="server" ID="Literal6" />:
												</td>
												<td>&nbsp;
													<asp:textbox id="ExcerptField" runat="server" maxlength="5000" Columns="28" width="353" cssclass="NormalTextBox"
														TextMode="MultiLine" Rows="4"></asp:textbox></td>
												<td></td>
											</tr>
											<tr vAlign="top">
												<td class="SubHead" noWrap>
													<tra:Literal TextKey="DESCRIPTION" Text="Description" runat="server" ID="Literal7" />:
												</td>
												<td colSpan="3">&nbsp;
													<asp:placeholder id="PlaceHolderHTMLEditor" runat="server"></asp:placeholder>
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
