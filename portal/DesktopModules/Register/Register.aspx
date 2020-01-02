<%@ Page language="c#" CodeBehind="Register.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.Admin.Register" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form runat="server">
			<div class="rb_DefaultLayoutDiv">
				<table class="rb_DefaultLayoutTable">
					<tr valign="top">
						<td height="10" class="rb_DefaultPortalHeader">
							<portal:Banner ShowTabs="false" runat="server" id="Banner1" />
						</td>
					</tr>
					<tr>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td align="middle">
							<table cellspacing="0" cellpadding="0" width="726" border="0">
								<tr>
									<td>
										<!-- Start Register control -->
										<asp:PlaceHolder id="register" runat="server"></asp:PlaceHolder>
										<!-- End Register control -->
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="rb_DefaultPortalFooter"><foot:Footer id="Footer" runat="server"></foot:Footer></td>
					</tr>
				</table>
			</div>
		</form>
	</body>
</html>
