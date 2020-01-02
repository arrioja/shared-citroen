<%@ Register TagPrefix="cc1" Namespace="Rainbow.UI.WebControls" Assembly="Rainbow" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Control language="c#" Inherits="Rainbow.DesktopModules.PortalSearch" CodeBehind="PortalSearch.ascx.cs" AutoEventWireup="false" %>
<P>
	<asp:datagrid id="DataGrid1" runat="server" 
		Width="100%" CellPadding="3" 
		AutoGenerateColumns="False"
		HeaderStyle-CssClass="Grid_Header"
		ItemStyle-CssClass="Grid_Item"
		AlternatingItemStyle-CssClass="Grid_AlternatingItem"
	/>
</P>
<P>
	<asp:textbox id="txtSearchString" runat="server" Width="150px" CssClass="NormalTextBox"></asp:textbox>
	<tra:button runat="server" id="btnSearch" TextKey="PORTALSEARCH_SEARCH" Text="Search" />
	<asp:label id="lblHits" runat="server" Width="50px" Font-Names="Tahoma"></asp:label>
	&nbsp;<tra:literal TextKey="PORTALSEARCH_MODULE" Text="Module" runat="server" id="lblModule" />&nbsp;
	<asp:DropDownList id="ddSearchModule" runat="server" CssClass="NormalTextBox"></asp:DropDownList>
	<tra:literal id="lblTopic" runat="server" Text="Topic" TextKey="PORTALSEARCH_TOPIC"></tra:literal>
	<asp:DropDownList id="ddTopics" runat="server" CssClass="NormalTextBox"></asp:DropDownList>
	&nbsp;<tra:literal TextKey="PORTALSEARCH_FIELD" Text="Field" runat="server" ID="lblField" />&nbsp;
	<asp:DropDownList id="ddSearchField" runat="server" CssClass="NormalTextBox"></asp:DropDownList></P>
<asp:Label id="lblError" runat="server" Visible="false"></asp:Label>
