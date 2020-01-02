<%@ Control language="c#" Inherits="Rainbow.DesktopModules.MapQuest" AutoEventWireup="false" CodeBehind="MapQuest.ascx.cs" targetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>


<asp:label id=lblLocation CssClass="NormalBold" Runat="server"></asp:label><br><asp:label id=lblAddress CssClass="Normal" Runat="server"></asp:label><br>
<table align=center>
  <tr>
    <td><asp:hyperlink id=hypMap Runat="server"></asp:hyperlink></TD>
    <td><asp:literal id=Literal1 
      runat="server" text="Zoom"></asp:literal><br><asp:radiobuttonlist id=RadioButtonList1 runat="server" AutoPostBack="True">
				<asp:ListItem Value="1">1</asp:ListItem>
				<asp:ListItem Value="2">2</asp:ListItem>
				<asp:ListItem Value="3">3</asp:ListItem>
				<asp:ListItem Value="4">4</asp:ListItem>
				<asp:ListItem Value="5">5</asp:ListItem>
				<asp:ListItem Value="6">6</asp:ListItem>
				<asp:ListItem Selected="true" Value="7">7</asp:ListItem>
				<asp:ListItem Value="8">8</asp:ListItem>
				<asp:ListItem Value="9">9</asp:ListItem>
				<asp:ListItem Value="10">10</asp:ListItem>
			</asp:radiobuttonlist></TD></TR></TABLE>
	
<!-- WILL ONLY WORK IN US!!
<br> 
<asp:HyperLink ID="hypDirections" Runat="server" CssClass="CommandButton"></asp:HyperLink> 
<br> 
-->
