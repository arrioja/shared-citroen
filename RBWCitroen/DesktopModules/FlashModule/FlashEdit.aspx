<%@ Page Language="c#" CodeBehind="FlashEdit.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.FlashEdit" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<html>
	<head runat="server"></head>
	<body runat="server">
<form runat="server" id="Form1">
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
					<table width="100%" cellspacing="0" cellpadding="4" border="0" class="EditInterface">
						<tr valign="top">
							<td width="150">&nbsp;
							</td>
							<td width="*">
								<table width="500" cellspacing="0" cellpadding="0">
									<tr>
										<td align="left" class="Head">
											<TRA:LITERAL id="Literal2" runat="server" Text="Flash Settings" TextKey="FLASH_SETTINGS"></TRA:LITERAL>
										</td>
									</tr>
									<tr>
										<td colspan="2">
											<hr noshade size="1">
										</td>
									</tr>
								</table>
								<table width="500" cellspacing="0" cellpadding="0">
									<tr valign="top">
										<td width="100" class="SubHead">
											<TRA:LITERAL id="Literal1" runat="server" Text="Swf-File Path" TextKey="FLASH_PATH"></TRA:LITERAL>
										</td>
										<td rowspan="3" width="251">&nbsp;
										</td>
										<td class="Normal">
											<asp:TextBox id="Src" cssclass="NormalTextBox" width="390" Columns="30" runat="server"></asp:TextBox>
										</td>
									</tr>
									<tr valign="top">
										<td class="SubHead">
											<TRA:LITERAL id="Literal3" runat="server" Text="Width" TextKey="WIDTH"></TRA:LITERAL>
										</td>
										<td>
											<asp:TextBox id="Width" cssclass="NormalTextBox" width="390" Columns="30" runat="server" />
										</td>
									</tr>
									<tr valign="top">
										<td class="SubHead">
											<TRA:LITERAL id="Literal4" runat="server" Text="Height" TextKey="HEIGHT"></TRA:LITERAL>
										</td>
										<td>
											<asp:TextBox id="Height" cssclass="NormalTextBox" width="390" Columns="30" runat="server" />
										</td>
									</tr>
									<TR>
										<td width="100" class="SubHead">
											<TRA:LITERAL id="Literal5" runat="server" Text="Background Color" TextKey="FLASH_BACKGROUNDCOLOR"></TRA:LITERAL>
											(rrggbb)
										</td>
										<td rowspan="3" width="251">&nbsp;
										</td>
										<td class="Normal">
											<asp:TextBox id="BackgroundCol" cssclass="NormalTextBox" width="390" Columns="30" runat="server"></asp:TextBox>
										</td>
									</TR>
								</table>
								<p>
									<TRA:LinkButton id="updateButton" Text="Update" TextKey="UPDATE" runat="server" class="CommandButton">Update</TRA:LinkButton>
									&nbsp;
									<TRA:LinkButton id="cancelButton" Text="Cancel" TextKey="CANCEL" CausesValidation="False" runat="server" class="CommandButton">Cancel</TRA:LinkButton>&nbsp;
									<tra:hyperlink id="showGalleryButton" runat="server" CssClass="CommandButton" Text="Show Gallery" TextKey="SHOW_FLASH_GALLERY">Show Gallery</tra:hyperlink>
								</p>
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