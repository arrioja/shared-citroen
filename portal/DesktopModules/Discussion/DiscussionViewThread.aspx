<%@ Page language="c#" Inherits="Rainbow.DesktopModules.DiscussionViewThread" CodeBehind="DiscussionViewThread.aspx.cs" AutoEventWireup="false" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<html>
  <head runat="server"></head>
	<body runat="server">
		<form name="form1" runat="server">
			<%-- needed to get site header to render correctly --%>
			<div class="rb_DefaultLayoutDiv">
				<table class="rb_DefaultLayoutTable">
					<tr vAlign="top">
						<td class="rb_DefaultPortalHeader" vAlign="top" colSpan="2"><portal:banner id="Banner1" runat="server" ShowTabs="false"></portal:banner></td>
					</tr>
					<tr vAlign="top">
						<td width="5%">&nbsp;</td>
						<td width="95%"><br>
							<%-- DataList of all the threads in this topic --%>
							<asp:datalist id="ThreadList" runat="server" ItemStyle-Cssclass="Normal" DataKeyField="ItemID"
								EnableViewState="true" OnItemCommand="ThreadList_Select" OnItemDataBound="OnItemDataBound">
<HeaderTemplate>
									<tra:Label CssClass="ItemTitle" Text="Displaying all threads for this topic" TextKey="DS_DISPLAY_THREADS" Runat="server" />
									<hr>
									<br>
								
</HeaderTemplate>

<FooterTemplate>
									<hr>
									<tra:LinkButton CssClass="Normal" Text="Return to list of threads" TextKey="DS_RETURN_LIST" CommandName="return_to_discussion_list" Runat="server" />
									<!-- Known bug:--this link doesn't work corectly if you edit an item on this page.--><br>
									<br>
								
</FooterTemplate>

<ItemStyle CssClass="Normal">
</ItemStyle>

<ItemTemplate>
<%# DataBinder.Eval(Container.DataItem, "BlockQuoteStart") %>
<tra:Label id=Label4 CssClass="ItemTitle" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>'></tra:Label>
<tra:HyperLink id=HyperLink2 runat="server" Text="Reply to this message_" Visible="True" NavigateUrl='<%# FormatUrlEditItem((int)DataBinder.Eval(Container.DataItem, "ItemID"), "REPLY") %>' TextKey="DS_REPLYTHISMSG" ImageUrl="<%# GetReplyImage() %>"></tra:HyperLink>
<tra:HyperLink id=HyperLink1 runat="server" Text="Edit this message" Visible="True" NavigateUrl='<%# FormatUrlEditItem((int)DataBinder.Eval(Container.DataItem, "ItemID"), "EDIT") %>' TextKey="EDIT" ImageUrl='<%# GetEditImage((string)DataBinder.Eval(Container.DataItem,"CreatedByUser")) %>'></tra:HyperLink>
<tra:ImageButton id=deleteBtn runat="server" TextKey="DELETE_THIS_ITEM" ImageUrl='<%# GetDeleteImage((int)DataBinder.Eval(Container.DataItem, "ItemID"),(string)DataBinder.Eval(Container.DataItem,"CreatedByUser")) %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ItemID") %>' CommandName="delete"></tra:ImageButton>
<asp:Panel CssClass="NormalDim">
				<tra:Label id="Label11" runat="server" Text="Posted by" CssClass="Normal" TextKey="POSTED_BY"></tra:Label>
				<tra:Label id=Label10 runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CreatedByUser") %>' CssClass="NormalDim">
				</tra:Label>,
				<tra:Label id="Label9" runat="server" Text="posted on" CssClass="Normal" TextKey="POSTED_DATE"></tra:Label>
				<tra:Label id=Label8 runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CreatedDate", "{0:g}") %>' CssClass="NormalDim">
				</tra:Label><BR>
<tra:Label id=Label3 CssClass="Normal" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Body") %>'></tra:Label><%# DataBinder.Eval(Container.DataItem, "BlockQuoteEnd") %>
</ItemTemplate>
							</asp:datalist></td>
					</tr>
					<tr>
						<td class="rb_DefaultPortalFooter" colSpan="2"><foot:footer id="Footer" runat="server"></foot:footer></td>
					</tr>
				</table>
			</div>
		</form>
	</body>
</html>
