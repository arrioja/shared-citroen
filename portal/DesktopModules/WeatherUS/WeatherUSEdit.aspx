<%@ Page Language="c#" CodeBehind="WeatherUSEdit.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.WeatherUSEdit" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form runat="server">
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
								<td width="*">
									<table cellSpacing="0" cellPadding="0" width="500">
										<tr>
											<td class="Head" align="left">United States Weather information&nbsp;(<A href="http://www.wx.com" target="_blank">www.wx.com</A>)<BR>
											</td>
										</tr>
										<tr>
											<td class="SubHead" align="left"><a href="http://www.wx.com/link/index.cfm?link=instruct" target="_blank">Please 
													have a look at: WX.COM Custom Link Pages Terms &amp; Conditions</a></td>
										</tr>
										<tr>
											<td colSpan="2">
												<hr noShade SIZE="1">
											</td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="500">
										<tr vAlign="top">
											<td class="SubHead" width="100">Zip code:
											</td>
											<td rowSpan="3">&nbsp;
											</td>
											<td class="Normal"><asp:textbox id="Zip" runat="server" Columns="30" width="390" cssclass="NormalTextBox"></asp:textbox>
												<div class="SubHead"><asp:requiredfieldvalidator id="rfvZip" runat="server" ErrorMessage="'Zip' must not be left blank." Display="Dynamic"
														ControlToValidate="Zip"></asp:requiredfieldvalidator></div>
											</td>
										</tr>
										<tr vAlign="top">
											<td class="SubHead" width="100">Option:
											</td>
											<td class="Normal">
												<asp:RadioButtonList id="Option" runat="server" RepeatDirection="Horizontal" Width="250px">
													<asp:ListItem Value="0" Selected="True">current conditions</asp:ListItem>
													<asp:ListItem Value="1">local radar</asp:ListItem>
												</asp:RadioButtonList>
											</td>
										</tr>
									</table>
									<p>&nbsp;
										<asp:placeholder id="PlaceHolderButtons" runat="server"></asp:placeholder></p>
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
