<%@ Control Inherits="Rainbow.DesktopModules.ModuleDefsAll" CodeBehind="ModuleDefsAll.ascx.cs" Language="c#" AutoEventWireup="false" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<asp:datalist id="defsList" runat="server" DataKeyField="GeneralModDefID">
	<ItemTemplate>
		&#160;
		<tra:ImageButton id="ImageButton1" runat="server" TextKey="EDIT_THIS_ITEM" AlternateText="Edit this item" ImageUrl='<%# CurrentTheme.GetImage("Buttons_Edit", "Edit.gif").ImageUrl %>' />&#160;
		<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FriendlyName") %>' CssClass="Normal">
		</asp:Label>
	</ItemTemplate>
</asp:datalist>