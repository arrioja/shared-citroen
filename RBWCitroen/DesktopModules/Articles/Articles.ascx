<%@ Control Language="c#" Inherits="Rainbow.DesktopModules.Articles" codebehind="Articles.ascx.cs" autoeventwireup="false" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<asp:datalist id="myDataList" runat="server" CellPadding="4" Width="100%">
	<ItemTemplate>
		<DIV class="Normal">
			<DIV>
				<tra:HyperLink TextKey="EDIT" Text="Edit" id="editLink" ImageUrl='<%# CurrentTheme.GetImage("Buttons_Edit", "Edit.gif").ImageUrl %>' NavigateUrl='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Articles/ArticlesEdit.aspx",TabID,"ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID") + "&amp;mid=" + ModuleID )%>' Visible="<%# IsEditable %>" runat="server" />
				<asp:Label id=StartDate Visible="<%# ShowDate %>" runat="server" CssClass="ItemDate" Text='<%# DataBinder.Eval(Container.DataItem,"StartDate", "{0:d}") %>'/>
				<asp:Label id=Separator Visible="<%# ShowDate %>" runat="server">&#160;-&#160;</asp:Label>
				<asp:HyperLink id=Title runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>' Visible='<%# (bool) (DataBinder.Eval(Container.DataItem, "Description").ToString().Length != 0) %>' NavigateUrl='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Articles/ArticlesView.aspx","ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID") + "&amp;mid=" + ModuleID )%>'>
				</asp:HyperLink>&#160;

				<asp:Label id=SubTitle Text='<%# "(" + DataBinder.Eval(Container.DataItem,"SubTitle") + ")" %>' Visible='<%# ((string)DataBinder.Eval(Container.DataItem,"Subtitle")).Length > 0 %>' runat="server"/>

			</DIV>
			<DIV class="NormalItalic" style="MARGIN-TOP: 6px; margin-botton: 6px"><%# DataBinder.Eval(Container.DataItem,"Abstract").ToString().Replace("\n","<br>") %></DIV>
		</DIV>
	</ItemTemplate>
</asp:datalist>