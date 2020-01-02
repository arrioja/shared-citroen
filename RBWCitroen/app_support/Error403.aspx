<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Page CodeBehind="Error403.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Rainbow.Error.ErrorPage403" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Import Namespace="Esperantus" %>
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
													<br>
													<br>
													<tra:Label CssClass="Head" TextKey="ERROR_403" runat="server" id="Label1">Access / Edit rights have been denied</tra:Label>
													<br>
													<hr noshade size="1">
												</P>
												<P>
													<br>
													<tra:Label id="Label2" runat="server" TextKey="ERROR_403_MESSAGE" CssClass="Normal" DESIGNTIMEDRAGDROP="61">The area you requested has been denied because you do not have sufficient access or edit rights.</tra:Label></P>
												<P>
													<br>
													<tra:Label CssClass="Normal" TextKey="APOLOGISE" runat="server" id="Label3">We apologise for the inconvenience.</tra:Label></P>
												<P>
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
