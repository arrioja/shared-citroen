<%@ Control language="c#" Inherits="Rainbow.DesktopModules.EnhancedLinks" CodeBehind="EnhancedLinks.ascx.cs" AutoEventWireup="false" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>

<table cellSpacing=0 cellPadding=4 border=0>
	<tr><td><asp:DropDownList id="cboLinks" Visible="False" Runat="server" CssClass="NormalTextBox" AutoPostBack="True"></asp:DropDownList></td></tr>
</table>
<asp:Panel id="results" Visible="False" Runat="server" />