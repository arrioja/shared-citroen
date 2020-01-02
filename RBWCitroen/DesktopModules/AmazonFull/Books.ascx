<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Control Language="c#" Inherits="AmazonFull.Books" codebehind="Books.ascx.cs" autoeventwireup="false" %>
<asp:DataList id="myDataList" runat="server" EnableViewState="false" Width="98%" CellPadding="4">
	<ItemStyle VerticalAlign="Top"></ItemStyle>
	<ItemTemplate>
		<TABLE>
			<TR>
				<TD width="<%# GetTdWidthPercentage(Settings["Columns"].ToString()) %>" vAlign=top>
					<tra:HyperLink id=BooksEditLink runat="server" Visible="<%# IsEditable %>" NavigateUrl='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/AmazonFull/BooksEdit.aspx","ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID") + "&amp;mid=" + ModuleID )%>' 
						ImageUrl='<%# CurrentTheme.GetImage("Buttons_Edit", "Edit.gif").ImageUrl %>' Text="Edit" TextKey="EDIT" /><BR>
					<A href='http://www.amazon.com/exec/obidos/ISBN=<%# DataBinder.Eval(Container.DataItem,"ISBN") %>/<%# Settings["Promotion Code"].ToString() %>'>
						<IMG src='http://images.amazon.com/images/P/<%# DataBinder.Eval(Container.DataItem,"ISBN") %>.01.MZZZZZZZ.jpg' width='<%=Settings["Width"].ToString()%>' border=0>
					</A>
					<BR>
					<%# GetWebServiceDetails(Settings["Amazon Dev Token"].ToString(),DataBinder.Eval(Container.DataItem,"ISBN").ToString(),Settings["Show Details"].ToString(),Settings["Promotion Code"].ToString()) %>
					<%# DataBinder.Eval(Container.DataItem,"Caption") %>
				</TD>
			</TR>
		</TABLE>
		<BR>
	</ItemTemplate>
</asp:DataList>
