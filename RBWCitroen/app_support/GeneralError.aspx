<%@ Import Namespace="Esperantus" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Page CodeBehind="GeneralError.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Rainbow.Error.GeneralErrorPage" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form runat="server">
			<div class="rb_DefaultLayoutDiv">
		<table class="rb_DefaultLayoutTable">
				<TBODY>
					<tr valign="top">
						<td class="rb_DefaultPortalHeader" valign="top">
							<portal:Banner runat="server" id="Banner1" />
						</td>
					</tr>
					<tr>
						<td valign="top">
							<center>
								<br>
								<table width="500" border="0">
									<TBODY>
										<tr>
											<td class="Normal">
												<P>
													<br>
													&nbsp;</P>
												<P>
													<tra:Label CssClass="Head" TextKey="GENERAL_ERROR" runat="server" id="Label1">Section currently under Maintenance</tra:Label></P>
												<P>
													<hr noshade size="1">
												</P>
												<P>
													<tra:Label CssClass="Normal" TextKey="GENERAL_ERROR_MESSAGE" runat="server" id="Label2">The area you requested is currently undergoing some maintenance.<br />We will have it back up as soon as our upgrades / fixes are complete.</tra:Label></P>
												<P>
													<tra:Label id="Label3" runat="server" TextKey="APOLOGISE" CssClass="Normal">We apologise for the inconvenience.</tra:Label>
													<br>
													<br>
													<tra:HyperLink id="ReturnHome" TextKey="RETURN_HOME" runat="server">Return Home</tra:HyperLink></P>
											</td>
										</tr>
									</TBODY>
								</table>
							</center>
						</td>
					</tr>
					<tr>
						<td class="rb_DefaultPortalFooter"><foot:Footer id="Footer" runat="server"></foot:Footer></td>
					</tr>
				</TBODY>
			</table>
			</div>
		</form>
	</body>
</html>
