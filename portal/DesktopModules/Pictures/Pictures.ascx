<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Register TagPrefix="cc2" Namespace="Rainbow.UI.WebControls" Assembly="Rainbow" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Pictures.ascx.cs" Inherits="Rainbow.DesktopModules.Pictures" %>
<tra:label id=lblError visible="false" textkey="PICTURES_FAILED_TO_LOAD" text="Failed to load templates. Revise your settings" runat="server" Font-Bold="True" ForeColor="Red"></tra:label>
<asp:datalist id="dlPictures" runat="server" ItemStyle-Width="1%"></asp:datalist>
<div align="center">
	<cc2:Paging id="pgPictures" runat="server" />
</div>
