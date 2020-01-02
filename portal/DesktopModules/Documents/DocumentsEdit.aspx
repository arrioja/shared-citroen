<%@ Page Language="c#" CodeBehind="DocumentsEdit.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.DocumentsEdit" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form encType="multipart/form-data" runat="server">
			<div class="rb_AlternateLayoutDiv">
				<table class="rb_AlternateLayoutTable">
					<tr vAlign="top">
						<td class="rb_AlternatePortalHeader" valign="top"><portal:banner id="SiteHeader" runat="server"></portal:banner></td>
					</tr>
					<tr>
						<td><br>
							<table cellSpacing="0" cellPadding="4" width="98%" border="0">
								<tr vAlign="top">
									<td width="150">&nbsp;
									</td>
									<td>
										<table cellSpacing="0" cellPadding="0" width="500">
											<tr>
												<td class="Head" align="left"><tra:label id="PageTitleLabel" runat="server" Height="22"></tra:label></td>
											</tr>
											<tr>
												<td colSpan="2">
													<hr noshade size="1">
												</td>
											</tr>
										</table>
										<table cellSpacing="0" cellPadding="0" width="726" border="0">
											<tr vAlign="top">
												<td class="SubHead" width="100"><tra:label id="FileNameLabel" runat="server" Height="22" TextKey="FILE_NAME">File name</tra:label></td>
												<td>&nbsp;
												</td>
												<td><asp:textbox id="NameField" runat="server" maxlength="150" Columns="28" width="353" cssclass="NormalTextBox"></asp:textbox></td>
												<td width="25" rowSpan="6">&nbsp;
												</td>
												<td class="Normal" width="250"><tra:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="NameField" ErrorMessage="You Must Enter a Valid Name"
														Display="Static" CssClass="Error" TextKey="ERROR_VALID_NAME"></tra:requiredfieldvalidator></td>
											</tr>
											<tr vAlign="top">
												<td class="SubHead"><tra:label id="CategoryLabel" runat="server" Height="22" TextKey="FILE_CATEGORY">Category</tra:label></td>
												<td>&nbsp;
												</td>
												<td><asp:textbox id="CategoryField" runat="server" maxlength="50" Columns="28" width="353" cssclass="NormalTextBox"></asp:textbox></td>
											</tr>
											<tr>
												<td>&nbsp;
												</td>
												<td colSpan="2">
													<hr width="100%" noshade SIZE="1">
												</td>
											</tr>
											<tr vAlign="top">
												<td class="SubHead" width="100"><tra:label id="UrlLabel" runat="server" Height="22" TextKey="URL">Url</tra:label></td>
												<td>&nbsp;
												</td>
												<td><asp:textbox id="PathField" runat="server" maxlength="250" Columns="28" width="353" cssclass="NormalTextBox"></asp:textbox></td>
											</tr>
											<tr>
												<td class="SubHead"><tra:label id="OrLabel" runat="server" Height="22" TextKey="OR">or</tra:label></td>
												<td colSpan="2">&nbsp;
													<br>
												</td>
											</tr>
											<tr vAlign="top">
												<td class="SubHead" vAlign="middle" noWrap><tra:label id="UploadLabel" runat="server" Height="22" TextKey="FILE_UPLOAD">Upload File</tra:label>&nbsp;
												</td>
												<td>&nbsp;
												</td>
												<td>
													<input id="FileUpload" style="WIDTH: 353px; FONT-FAMILY: verdana" type="file" name="FileUpload"
														runat="server" width="300">
												</td>
											</tr>
										</table>
										<p><tra:linkbutton class="CommandButton" id="updateButton" runat="server" Text="Update"></tra:linkbutton>&nbsp;
											<tra:linkbutton class="CommandButton" id="cancelButton" runat="server" Text="Cancel" CausesValidation="False"></tra:linkbutton>&nbsp;
											<tra:linkbutton class="CommandButton" id="deleteButton" runat="server" Text="Delete this item" CausesValidation="False"></tra:linkbutton>
											<hr noshade size="1">
											<span class="Normal">
											<tra:Literal TextKey="CREATED_BY" Text="Created by" id="CreatedLabel" runat="server"></tra:Literal>&nbsp;
											<asp:label id="CreatedBy" runat="server"></asp:label>&nbsp;
											<tra:Literal TextKey="ON" Text="on" id="OnLabel" runat="server"></tra:Literal>&nbsp;
											<asp:label id="CreatedDate" runat="server"></asp:label>
										</span>
										<P>
											<tra:Label id="Message" runat="server" ForeColor="Red" CssClass="NormalRed"></tra:Label>
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
