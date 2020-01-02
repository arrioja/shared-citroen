<%@ Control language="c#" Inherits="Rainbow.DesktopModules.ServiceItemList" CodeBehind="ServiceItemList.ascx.cs" AutoEventWireup="false" %>
<p>
	<asp:datagrid id="DataGrid1" runat="server" 
		Width="100%" CellPadding="3" 
		AutoGenerateColumns="true"
		HeaderStyle-CssClass="Grid_Header"
		ItemStyle-CssClass="Grid_Item"
		AlternatingItemStyle-CssClass="Grid_AlternatingItem"
	/>
</p>
<div class='error'>
	<asp:label id="lblStatus" runat="server"></asp:label>
</div>
