<%@ Control language="c#" Inherits="Rainbow.DesktopModules.Links" CodeBehind="Links.ascx.cs" AutoEventWireup="false" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<asp:datalist id="myDataList" runat="server" Width="100%" CellPadding="2">
	<ItemTemplate>
		<span class="Normal">
			<tra:HyperLink id="editLink" TextKey="<%# linkTextKey %>" AlternateText="<%# linkAlternateText %>" ImageUrl="<%# linkImage %>" NavigateUrl='<%# GetLinkUrl(DataBinder.Eval(Container.DataItem,"ItemID"),DataBinder.Eval(Container.DataItem,"Url")) %>' runat="server" />
			<asp:HyperLink Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>' NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"Url") %>' ToolTip='<%# DataBinder.Eval(Container.DataItem,"Description") %>' Target='<%# DataBinder.Eval(Container.DataItem,"Target") %>' runat="server" />
		</span>
		<br>
	</ItemTemplate>
</asp:datalist>
