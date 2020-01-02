<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Page Language="c#" CodeBehind="HtmlEdit.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.HtmlEdit" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Import Namespace="Esperantus" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form runat="server" enctype="multipart/form-data">
			<div class="rb_AlternateLayoutDiv">
				<table class="rb_AlternateLayoutTable">
					<tr vAlign="top">
						<td class="rb_AlternatePortalHeader" valign="top"><portal:banner id="SiteHeader" runat="server"></portal:banner></td>
					</tr>
					<tr>
						<td><br>
							<table cellSpacing="0" cellPadding="4" width="98%" border="0">
								<tr vAlign="top">
									<td width="100">&nbsp;
									</td>
									<td width="*">
										<table cellSpacing="0" cellPadding="0" width="550">
											<tr>
												<td class="Head" align="left">
													<tra:Literal id="Literal1" runat="server" TextKey="HTML_EDITOR" Text="HTML Editor"></tra:Literal>
												</td>
											</tr>
											<tr>
												<td colSpan="2">
													<hr noshade size="1">
												</td>
											</tr>
										</table>
										<table cellSpacing="0" cellPadding="0" width="720">
											<TR>
												<td class="SubHead">
													<P>
														<tra:Literal id="Literal2" runat="server" TextKey="HTML_DESKTOP_CONTENT" Text="Desktop HTML Content"></tra:Literal><FONT face="宋体">:</FONT>
														<BR>
														<span class="normal">
														<asp:placeholder id="PlaceHolderHTMLEditor" runat="server"></asp:placeholder>
														</span>
													</P>
												</td>
											</TR>
											<TR ID="MobileRow" runat="server">
												<td class="SubHead"><FONT face="宋体">&nbsp;</FONT>
													<P><BR>
														<tra:Literal id="Literal3" runat="server" TextKey="HTML_MOBILE_SUMMARY" Text="Mobile Summary"></tra:Literal><FONT face="宋体">:</FONT>
														<br>
														<asp:textbox id="MobileSummary" runat="server" textmode="multiline" rows="3" width="650" columns="75" CssClass="NormalTextBox"></asp:textbox><BR>
														<tra:Literal id="Literal4" runat="server" TextKey="HTML_MOBILE_DETAILS" Text="Mobile Details"></tra:Literal><FONT face="宋体">:</FONT>
														<br>
														<asp:textbox id="MobileDetails" runat="server" textmode="multiline" rows="5" width="650" columns="75" CssClass="NormalTextBox"></asp:textbox></P>
												</td>
											</TR>
										</table>
										<p><asp:placeholder id="PlaceHolderButtons" runat="server"></asp:placeholder></p>
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
