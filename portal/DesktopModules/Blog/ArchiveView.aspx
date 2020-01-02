<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Page language="c#" Codebehind="ArchiveView.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.ArchiveView" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form id="ArchiveView" method="post" runat="server">
			<div class="rb_DefaultLayoutDiv">
				<table class="rb_DefaultLayoutTable">
					<tr vAlign="top">
						<td colspan="2" class="rb_DefaultPortalHeader" valign="top">
							<portal:banner id="SiteHeader" runat="server"></portal:banner></td>
					</tr>
					<tr vAlign="top">
						<td width="15%">
							&nbsp;<b><tra:Literal id="SyndicationLabel" runat="server" Text="Syndication" TextKey="BLOG_SYNDICATION"></tra:Literal></b><br>
							&nbsp;<A id="lnkRSS" runat="server" href="/rainbow/DesktopModules/Blog/rss.aspx"><IMG id="imgRSS" runat="server" src="/rainbow/DesktopModules/Blog/xml.gif" border="0"></A>
							<br>
							<br>
							&nbsp;<b><tra:Literal id="StatisticsLabel" runat="server" Text="Statistics" TextKey="BLOG_STATISTICS"></tra:Literal></b><br>
							&nbsp;<asp:Label ID="lblEntryCount" Runat="server"></asp:Label>
							<br>
							&nbsp;<asp:Label ID="lblCommentCount" Runat="server"></asp:Label>
							<br>
							<br>
							&nbsp;<b><tra:Literal id="ArchivesLabel" runat="server" Text="Archives" TextKey="BLOG_ARCHIVES"></tra:Literal></b><br>
							<asp:datalist id="dlArchive" Width="100%" runat="server">
								<ItemTemplate>
									&nbsp;
									<asp:HyperLink id="Hyperlink4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MonthName") 
																+ ", " +  DataBinder.Eval(Container.DataItem,"Year")
																+ " (" + DataBinder.Eval(Container.DataItem,"Count") + ")"%>' Visible='True' CssClass="BlogArchiveLink" NavigateUrl='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Blog/ArchiveView.aspx",TabID,
																"&month=" + DataBinder.Eval(Container.DataItem,"Month") 
																+ "&year=" + DataBinder.Eval(Container.DataItem,"Year")
																+ "&mid=" + ModuleID )%>'>
									</asp:HyperLink>
								</ItemTemplate>
							</asp:datalist>
						</td>
						<td>
							<asp:Label id="lblHeader" Visible="True" runat="server" CssClass="BlogTitle" Text='' />
							<br>
							<br>
							<asp:datalist id="myDataList" runat="server" CellPadding="4" EnableViewState="False" Width="100%">
								<ItemTemplate>
									<DIV class="Normal">
										<DIV>
											<asp:HyperLink id=Title runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>' Visible="True" NavigateUrl='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Blog/BlogView.aspx",TabID,"ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID") + "&amp;mid=" + ModuleID )%>'>
											</asp:HyperLink>&nbsp;
											<BR>
											<tra:Label id="Label1" runat="server" TextKey="BLOG_POSTED">posted</tra:Label>
											<asp:HyperLink id=Hyperlink1 runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"StartDate", "{0:dddd MMMM d yyyy hh:mm tt}") %>' Visible="True" NavigateUrl='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Blog/BlogView.aspx",TabID,"ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID") + "&amp;mid=" + ModuleID )%>'>
											</asp:HyperLink>&nbsp;|
											<asp:HyperLink id=Hyperlink2 runat="server" Text='<%# Feedback + DataBinder.Eval(Container.DataItem,"CommentCount") + ")" %>' Visible="True" NavigateUrl='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Blog/BlogView.aspx",TabID,"ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID") + "&amp;mid=" + ModuleID )%>'>
											</asp:HyperLink>&nbsp;
										</DIV>
									</DIV>
								</ItemTemplate>
							</asp:datalist>
						</td>
					</tr>
					<tr>
						<td colspan="2" align="center">
							<asp:Label ID="lblCopyright" Runat="server" CssClass="Normal"></asp:Label>
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
