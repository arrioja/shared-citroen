<%@ Control language="c#" Inherits="Rainbow.DesktopModules.Blog" CodeBehind="Blog.ascx.cs" AutoEventWireup="false" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<table class="rb_DefaultLayoutTable">
	<tr class="Normal">
		<td vAlign="top" width="150">
			<b>
				<tra:Literal id="SyndicationLabel" runat="server" Text="Syndication" TextKey="BLOG_SYNDICATION"></tra:Literal></b><br>
			<A id="lnkRSS" runat="server" href="/rainbow/DesktopModules/Blog/rss.aspx"><IMG id="imgRSS" runat="server" src="/rainbow/DesktopModules/Blog/xml.gif" border="0"></A>
			<br>
			<br>
			<b>
				<tra:Literal id="StatisticsLabel" runat="server" Text="Statistics" TextKey="BLOG_STATISTICS"></tra:Literal></b><br>
			<asp:Label ID="lblEntryCount" Runat="server"></asp:Label>
			<br>
			<asp:Label ID="lblCommentCount" Runat="server"></asp:Label><br>
			<br>
			<b>
				<tra:Literal id="ArchivesLabel" runat="server" Text="Archives" TextKey="BLOG_ARCHIVES"></tra:Literal></b><br>
			<asp:datalist id="dlArchive" Width="100%" EnableViewState="False" runat="server">
				<ItemTemplate>
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
			<asp:datalist id="myDataList" Width="100%" CellPadding="4" runat="server" EnableViewState="False">
				<ItemTemplate>
					<DIV class="Normal">
						<DIV>
							<div class="BlogTitle">
								<tra:HyperLink TextKey="EDIT" Text="Edit" id="editLink" ImageUrl='<%# CurrentTheme.GetImage("Buttons_Edit", "Edit.gif").ImageUrl %>'
									NavigateUrl='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Blog/BlogEdit.aspx",TabID,"ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID") + "&amp;mid=" + ModuleID )%>' Visible="<%# IsEditable %>" runat="server" />
								<asp:HyperLink id=Title runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>' Visible='<%# (bool) (DataBinder.Eval(Container.DataItem, "Description").ToString().Length != 0) %>' CssClass="BlogTitle" NavigateUrl='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Blog/BlogView.aspx",TabID,"ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID") + "&amp;mid=" + ModuleID )%>'>
								</asp:HyperLink>&#160;
							</div>
							<br />
							<asp:Label id="Description" runat="server" CssClass="Normal">
								<%# Server.HtmlDecode((String) DataBinder.Eval(Container.DataItem,"Description")) %>
							</asp:Label>
							<br />
							<br />
							<div class="BlogFooter">
								<asp:HyperLink id="Hyperlink1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"StartDate", "{0:dddd MMMM d yyyy hh:mm tt}") %>' Visible='<%# (bool) (DataBinder.Eval(Container.DataItem, "Description").ToString().Length != 0) %>' NavigateUrl='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Blog/BlogView.aspx",TabID,"ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID") + "&amp;mid=" + ModuleID )%>'>
								</asp:HyperLink>&#160;|
								<asp:HyperLink id="Hyperlink2" runat="server" Text='<%# Feedback + DataBinder.Eval(Container.DataItem,"CommentCount") + ")" %>' Visible='True' NavigateUrl='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Blog/BlogView.aspx",TabID,"ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID") + "&amp;mid=" + ModuleID )%>'>
								</asp:HyperLink>&#160;
							</div>
						</DIV>
						<DIV class="NormalItalic" style="MARGIN-TOP: 6px; margin-botton: 6px">
						</DIV>
					</DIV>
					<hr />
				</ItemTemplate>
			</asp:datalist>
		</td>
	</tr>
	<tr>
		<td colspan="2" align="center">
			<asp:Label ID="lblCopyright" Runat="server" CssClass="Normal"></asp:Label>
		</td>
	</tr>
</table>
