<%@ Page language="c#" CodeBehind="Logon.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.Admin.LogonPage" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form runat="server" ID="Form1">
			<div class="rb_DefaultLayoutDiv">
				<table class="rb_DefaultLayoutTable">
					<tr valign="top">
						<td height="10" class="rb_DefaultPortalHeader"><portal:Banner ShowTabs="false" runat="server" id="Banner1" /></td>
					</tr>
					<tr>
						<td align="center" height="*">
							<table width="170" cellspacing="0" cellpadding="0" border="0">
								<tr>
									<td><asp:PlaceHolder id="signIn" runat="server"></asp:PlaceHolder></td>
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