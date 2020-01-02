<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Control language="c#" Inherits="Rainbow.DesktopModules.Discussion" CodeBehind="Discussion.ascx.cs" AutoEventWireup="false" %>
<%-- list of threads, URL must have a Parent="parent from dataabse" --%>
<asp:datalist OnItemDataBound="OnItemDataBound" id="TopLevelList" EnableViewState="true" runat="server" DataKeyField="ItemID" ItemStyle-Cssclass="Normal" width="98%">
	<HeaderTemplate>
		<tr>
			<TD width="50%">
				<tra:Label id="Label14" runat="server" TextKey="DISCUSION_TITLE" CssClass="NormalBold">Titulo</tra:Label></TD>
			<TD width="25%" align="right">&nbsp;
				<tra:Label id="Label13" runat="server" TextKey="DISCUSION_AUTHOR" CssClass="NormalBold">Autor</tra:Label></TD>
			<TD width="25%" align="right">
				<tra:Label id="Label12" runat="server" TextKey="DISCUSSION_LAST_UPDATE" CssClass="NormalBold">Ultima Actualizacion</tra:Label></TD>
		</tr>
		<tr>
			<td colspan="3"><hr>
			</td>
		</tr>
	</HeaderTemplate>
	<SelectedItemTemplate>
		<TR bgcolor="WhiteSmoke">
			<TD colspan="3">
				<tra:ImageButton id="btnCollapse" runat="server" ImageUrl='<%# getLocalImage("minus.gif") %>' CommandName="CollapseThread"></tra:ImageButton>
				<asp:Label id=Label1 Runat="server" Tooltip="Number of replys to this topic" Text='<%# DataBinder.Eval(Container.DataItem, "ChildCount") %>' CssClass="Normal">
				</asp:Label>/
				<asp:Label id=Label2 Runat="server" Tooltip="Number of times this topic has been viewed" Text='<%# DataBinder.Eval(Container.DataItem, "ViewCount") %>' CssClass="Normal">
				</asp:Label>&nbsp;
				<asp:LinkButton id=LinkButton2 runat="server" CommandName="CollapseThread" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>' CssClass="ItemTitle">
				</asp:LinkButton><%-- add the property 'Target="_new"' to the following hyperlink to have edits occur up in a new browser window --%>
				<tra:HyperLink id=HyperLink2 runat="server" ImageUrl="<%# GetReplyImage() %>" Text="Reply to this message_" TextKey="DS_REPLYTHISMSG" Visible="True" NavigateUrl='<%# FormatUrlEditItem((int)DataBinder.Eval(Container.DataItem, "ItemID"), "REPLY") %>'>
				</tra:HyperLink>
				<tra:HyperLink id=HyperLink1 runat="server" ImageUrl='<%# GetEditImage((string)DataBinder.Eval(Container.DataItem,"CreatedByUser")) %>' Text="Edit this message" TextKey="EDIT" Visible="True" NavigateUrl='<%# FormatUrlEditItem((int)DataBinder.Eval(Container.DataItem, "ItemID"), "EDIT") %>'>
				</tra:HyperLink>
				<tra:ImageButton id=deleteBtn runat="server" ImageUrl='<%# GetDeleteImage((int)DataBinder.Eval(Container.DataItem, "ItemID"),(string)DataBinder.Eval(Container.DataItem,"CreatedByUser")) %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ItemID") %>' CommandName="delete" TextKey="DELETE_THIS_ITEM">
				</tra:ImageButton>
				<tra:Label id="Label11" runat="server" Text="Posted by" CssClass="Normal" TextKey="POSTED_BY"></tra:Label>
				<tra:Label id=Label10 runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CreatedByUser") %>' CssClass="NormalDim">
				</tra:Label>,
				<tra:Label id="Label9" runat="server" Text="posted on" CssClass="Normal" TextKey="POSTED_DATE"></tra:Label>
				<tra:Label id=Label8 runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CreatedDate", "{0:g}") %>' CssClass="NormalDim">
				</tra:Label><BR>
				<tra:Label id=Label7 runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Body") %>' CssClass="NormalDim">
				</tra:Label>
				<P></P>
				<%-- expanded responses to main thread --%>
				<asp:DataList id=DetailList runat="server" OnItemDataBound="OnItemDataBound" datasource="<%# GetThreadMessages() %>" OnItemCommand="TopLevelListOrDetailList_Select">
					<ItemTemplate>
						<%# DataBinder.Eval(Container.DataItem, "BlockQuoteStart") %>
						<tra:Label CssClass="ItemTitle" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>' runat="server" />
						<tra:HyperLink ImageUrl='<%# GetReplyImage() %>' TextKey="DS_REPLYTHISMSG" Text='Reply to this message_' NavigateUrl='<%# FormatUrlEditItem((int)DataBinder.Eval(Container.DataItem, "ItemID"), "REPLY") %>' runat="server" Visible=True ID="Hyperlink1"/>
						<tra:HyperLink ImageUrl='<%# GetEditImage((string)DataBinder.Eval(Container.DataItem,"CreatedByUser")) %>' TextKey="EDIT" Text="Edit this message" NavigateUrl='<%# FormatUrlEditItem((int)DataBinder.Eval(Container.DataItem, "ItemID"), "EDIT") %>' runat="server" Visible=True ID="Hyperlink2"/>
						<tra:ImageButton id="deleteBtnExpanded" ImageUrl='<%# GetDeleteImage((int)DataBinder.Eval(Container.DataItem, "ItemID"),(string)DataBinder.Eval(Container.DataItem,"CreatedByUser")) %>' TextKey="DELETE_THIS_ITEM" CommandName="delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ItemID") %>' runat="server" />
						<tra:Label CssClass="Normal" TextKey="POSTED_BY" Text="Posted by" runat="server" />
						<tra:Label CssClass="Normal" Text='<%# DataBinder.Eval(Container.DataItem,"CreatedByUser") %>' runat="server" />
						,
						<tra:Label CssClass="Normal" TextKey="POSTED_DATE" Text="posted on" runat="server" />
						<tra:Label CssClass="Normal" Text='<%# DataBinder.Eval(Container.DataItem,"CreatedDate", "{0:g}") %>' runat="server" />
						<br>
						<tra:Label CssClass="Normal" Text='<%# DataBinder.Eval(Container.DataItem,"Body") %>' runat="server" />
						<%# DataBinder.Eval(Container.DataItem, "BlockQuoteEnd") %>
					</ItemTemplate>
				</asp:DataList></TD>
		</TR>
	</SelectedItemTemplate>
	<ItemStyle CssClass="Normal"></ItemStyle>
	<ItemTemplate>
		<TR>
			<TD width="60%">
				<tra:ImageButton id=btnSelect runat="server" ToolTip="Expand the thread of this topic inside this browser page" ImageUrl='<%# NodeImage((int)DataBinder.Eval(Container.DataItem, "ChildCount")) %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ItemID") %>' CommandName="ExpandThread">
				</tra:ImageButton>
				<tra:ImageButton id=btnNewWindow runat="server" ToolTip="Open the thread of this topic in a new browser page" ImageUrl='<%# getLocalImage("new_window.gif") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ItemID") %>' CommandName="ShowThreadNewWindow" Runat="server">
				</tra:ImageButton>
				<asp:Label id=Label6 Runat="server" Tooltip="Number of replys to this topic" Text='<%# DataBinder.Eval(Container.DataItem, "ChildCount") %>' CssClass="Normal">
				</asp:Label>/
				<asp:Label id=Label5 Runat="server" Tooltip="Number of times this topic has been viewed" Text='<%# DataBinder.Eval(Container.DataItem, "ViewCount") %>' CssClass="Normal">
				</asp:Label>&nbsp;
				<asp:LinkButton id=LinkButton1 runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ItemID") %>' CommandName="ExpandThread" Text='<%# DataBinder.Eval(Container.DataItem, "Title") %>' CssClass="ItemTitle">
				</asp:LinkButton></TD>
			<TD width="20%" align="right">
				<asp:Label id=Label4 Runat="server" Tooltip="Author of this post" Text='<%# DataBinder.Eval(Container.DataItem,"CreatedByUser") %>' CssClass="Normal">
				</asp:Label>&nbsp;
			</TD>
			<TD width="20%" align="right">
				<asp:Label id=Label3 Runat="server" Tooltip="Date of most recent reply" Text='<%# DataBinder.Eval(Container.DataItem,"DateofLastReply", "{0:g}") %>' CssClass="Normal">
				</asp:Label></TD>
		</TR>
	</ItemTemplate>
</asp:datalist>
