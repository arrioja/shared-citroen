<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Control language="c#" Inherits="Rainbow.DesktopModules.GoogleSearch" CodeBehind="GoogleSearch.ascx.cs" AutoEventWireup="false" %>
<p>
<asp:datagrid id="DataGrid1" runat="server" 
	Width="100%" CellPadding="3" 
	HeaderStyle-CssClass="Grid_Header"
	ItemStyle-CssClass="Grid_Item"
	AlternatingItemStyle-CssClass="Grid_AlternatingItem"
/>
</p>
<p>
	<asp:textbox id="txtSearchString" runat="server" Width="200px" CssClass="NormalTextBox"></asp:textbox>
	<tra:Label id="Label1" runat="server" Text="Start" TextKey="GOOGLE_START" CssClass="Normal"></tra:Label>
	<asp:textbox id="TextBox2" runat="server" Width="40px" CssClass="NormalTextBox">1</asp:textbox>
	<tra:Button id="Search" runat="server" Text="Search" TextKey="GOOGLE_SEARCH"></tra:Button>
	<asp:label id="lblHits" runat="server" Width="100px" CssClass="Normal"></asp:label>
</p>
