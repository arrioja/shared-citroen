<%@ Page Language="c#" codebehind="ArticlesView.aspx.cs" autoeventwireup="false" Inherits="Rainbow.DesktopModules.ArticlesView" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form id="ArticlesView" method="post" runat="server">
			<div class="rb_DefaultLayoutDiv">
			<table class="rb_DefaultLayoutTable">
				<tr vAlign="top">
				<td colspan="2" class="rb_DefaultPortalHeader" valign="top">
					<portal:banner id="SiteHeader" runat="server"></portal:banner></td>
				</tr>
				<tr vAlign="top">
					<td width="10%">
						&nbsp;
					</td>
					<td>
						<br>
						<table cellSpacing="0" cellPadding="0" width="600">
							<tr>
								<td class="Normal" colSpan="2">
									<P>
											<asp:Label id="Title" runat="server" CssClass="ItemTitle">&nbsp;</asp:Label>&nbsp;
											<asp:Label id="Subtitle" runat="server" CssClass="ItemTitle">&nbsp;</asp:Label>&nbsp;
											<asp:Label id="StartDate" runat="server" CssClass="ItemDate">&nbsp;</asp:Label>
									</P>
									<P>
											<asp:Label id="Description" runat="server" CssClass="Normal">&nbsp;</asp:Label>
									</P>
									<hr noshade size="1">
										<P><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                          <td><a href="javascript:history.go(-1)" class="Normal"><< <tra:Literal id="goback" runat="server" Text="Back" TextKey="BACK"></tra:Literal></a></td>
                                            <td align="right" class="Normal"><tra:Literal id="CreatedLabel" runat="server" Text="Created by" TextKey="CREATED_BY"></tra:Literal>&nbsp;
										<asp:label id="CreatedBy" runat="server"></asp:label>&nbsp;
										<tra:Literal id="OnLabel" runat="server" Text="on" TextKey="ON"></tra:Literal>&nbsp;
											<asp:label id="CreatedDate" runat="server"></asp:label></td>
                                        </tr>
                                        </table>
											
									<P>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			<tr>
				<td colspan="2" class="rb_DefaultPortalFooter"><foot:Footer id="Footer" runat="server"></foot:Footer></td>
			</tr>
		</table>
		</div>
		</form>
	</body>
</html>
