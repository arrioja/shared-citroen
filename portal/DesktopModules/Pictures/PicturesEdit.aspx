<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Page language="c#" Codebehind="PicturesEdit.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.PicturesEdit" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Import Namespace="Esperantus" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form encType="multipart/form-data" runat="server" ID="Form1">
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
									<table cellSpacing="0" cellPadding="0" width="500">
										<tr>
											<td class="Head" align="left"><asp:label id="PageTitleLabel" runat="server" Height="22"></asp:label></td>
										</tr>
										<tr>
											<td colSpan="2">
												<hr noshade SIZE="1">
											</td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="726" border="0">
										<tr>
											<td width="200" class="SubHead">
												<tra:Literal TextKey="PICTURES_DISPLAY_ORDER" Text="Display Order" runat="server" ID="Literal1"></tra:Literal>
											</td>
											<td>
												<asp:TextBox id="DisplayOrder" runat="server" MaxLength="10" Width="100px" CssClass="NormalTextBox"></asp:TextBox>
											</td>
											<td class="Normal" width="266">
											</td>
										</tr>
										<tr>
											<td width="200" class="SubHead">
												<tra:Literal TextKey="PICTURES_FLIP" Text="Flip" runat="server" ID="Literal2"></tra:Literal>
											</td>
											<td>
												<asp:DropDownList ID="selFlip" Width="100px" Runat="server" CssClass="NormalTextBox">
													<asp:ListItem Value="None" Selected="True">None</asp:ListItem>
													<asp:ListItem Value="X">X</asp:ListItem>
													<asp:ListItem Value="Y">Y</asp:ListItem>
													<asp:ListItem Value="XY">XY</asp:ListItem>
												</asp:DropDownList>
											</td>
											<td class="Normal" width="266">
											</td>
										</tr>
										<tr>
											<td width="200" class="SubHead">
												<tra:Literal TextKey="PICTURES_ROTATE" Text="Rotate" runat="server" ID="Literal3"></tra:Literal>
											</td>
											<td>
												<asp:DropDownList ID="selRotate" Runat="server" Width="100px" CssClass="NormalTextBox">
													<asp:ListItem Value="None" Selected="True">None</asp:ListItem>
													<asp:ListItem Value="90">90</asp:ListItem>
													<asp:ListItem Value="180">180</asp:ListItem>
													<asp:ListItem Value="270">270</asp:ListItem>
												</asp:DropDownList>
											</td>
											<td class="Normal" width="266">
											</td>
										</tr>
										<tr>
											<td width="200" class="SubHead">
												<tra:Literal TextKey="PICTURES_CAPTION" Text="Caption" runat="server" ID="Literal4"></tra:Literal>
											</td>
											<td>
												<asp:TextBox id="Caption" runat="server" MaxLength="255" Width="401px" CssClass="NormalTextBox"></asp:TextBox>
											</td>
											<td class="Normal" width="266">
											</td>
										</tr>
										<tr>
											<td width="200" class="SubHead">
												<tra:Literal TextKey="PICTURES_KEYWORDS" Text="Keywords" runat="server" ID="Literal5"></tra:Literal>
											</td>
											<td>
												<asp:TextBox id="Keywords" runat="server" MaxLength="255" Width="401px" CssClass="NormalTextBox"></asp:TextBox>
											</td>
											<td class="Normal" width="266">
											</td>
										</tr>
										<tr>
											<td width="200" class="SubHead">
												<tra:Literal TextKey="PICTURES_SHORT_DESCRIPTION" Text="Short Description" runat="server" ID="Literal6"></tra:Literal>
											</td>
											<td>
												<asp:TextBox id="ShortDescription" runat="server" Height="120px" MaxLength="255" TextMode="MultiLine" Width="401px" CssClass="NormalTextBox"></asp:TextBox>
											</td>
											<td class="Normal" width="266">
											</td>
										</tr>
										<tr>
											<td width="200" class="SubHead">
												<tra:Literal TextKey="PICTURES_LONG_DESCRIPTION" Text="Long Description" runat="server" ID="Literal7"></tra:Literal>
											</td>
											<td>
												<asp:TextBox id="LongDescription" runat="server" Height="120px" MaxLength="255" TextMode="MultiLine" Width="401px" CssClass="NormalTextBox"></asp:TextBox>
											</td>
											<td class="Normal" width="266">
											</td>
										</tr>
										<tr>
											<td width="200" class="SubHead">
												<tra:Literal TextKey="PICTURES_FILE" Text="File" runat="server" ID="Literal8"></tra:Literal>
											</td>
											<td>
												<input type="file" id="flPicture" runat="server" NAME="flPicture" CssClass="NormalTextBox">
											</td>
											<td class="Normal" width="266">
											</td>
										</tr>
										<tr>
											<td width="200" class="SubHead">
												<tra:Literal TextKey="PICTURES_INCLUDE_EXIF" Text="Include EXIF" runat="server" ID="Literal9"></tra:Literal>
											</td>
											<td>
												<asp:CheckBox ID="chkIncludeExif" Runat="server" Checked="True" />
											</td>
											<td class="Normal" width="266">
											</td>
										</tr>
										<tr>
											<td width="200" class="SubHead">
												<tra:Literal TextKey="PICTURES_BULK_LOAD" Text="Bulk load from server directory" runat="server" ID="BulkDirLiteral"></tra:Literal>
											</td>
											<td>
												<asp:TextBox id="BulkDir" runat="server" MaxLength="255" Width="401px" CssClass="NormalTextBox"/>
											</td>
											<td class="Normal" width="266">
											</td>
										</tr>
									</table>
									<p>
										<asp:LinkButton id="updateButton" runat="server" CssClass="CommandButton">Update</asp:LinkButton>
										&nbsp;&nbsp;
										<asp:LinkButton id="cancelButton" runat="server" CssClass="CommandButton" CausesValidation="False">Cancel</asp:LinkButton>
										&nbsp;&nbsp;
										<asp:LinkButton id="deleteButton" runat="server" CssClass="CommandButton" CausesValidation="False">Delete this item</asp:LinkButton>
										<br>
										<hr noshade size="1" width="500">
									<P>
										<asp:Label id="Message" runat="server" ForeColor="Red" CssClass="NormalRed"></asp:Label>
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
