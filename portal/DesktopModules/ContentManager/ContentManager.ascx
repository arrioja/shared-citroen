<%@ Control Language="c#" Inherits="Rainbow.DesktopModules.ContentManager" codebehind="ContentManager.ascx.cs" autoeventwireup="false" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<table width="90%" align="center" border="0">
	<tbody>
		<tr>
			<td width="43%">
				<span class="ItemTitle"><center>ModuleTypes</center></span>
			</td>
			<td width="16%">&nbsp;</td>
			<td width="43%">&nbsp;</td>
		</tr>
		<tr>
			<td colspan="3" align="left">
				<asp:DropDownList id="ModuleTypes" width="264px" runat="server" AutoPostBack="true" />
			</td>
		</tr>
	</tbody>
</table>
<table id="MultiPortalTable" width="90%" align="center" border="0" runat="server">
	<tr><td colspan="3"><hr></td></tr>
	<tr>
		<td width="43%">
			<center>
				<asp:label id="SourcePortalLabel" class="ItemTitle" Text="Source Portal" runat="server" />
			</center>
		</td>
		<td width="16%">&nbsp;</td>
		<td width="43%">
			<center>
				<asp:label id="DestinationPortalLabel" class="ItemTitle" Text="Destination Portal" runat="server" />
			</center>
		</td>
	</tr>
	<tr>
		<td width="43%">
			<asp:DropDownList id="SourcePortal" width="100%" runat="server" AutoPostBack="true" />
		</td>
		<td width="16%">&nbsp;</td>
		<td width="43%">
			<asp:DropDownList id="DestinationPortal" width="100%" runat="server" AutoPostBack="true" />
		</td>
	</tr>
	<tr><td colspan="3"><hr></td></tr>
</table>
<table width="90%" align="center" border="0">
	<tr>
		<td width="43%">
			<span class="ItemTitle"><center>Source Module</center></span>
		</td>
		<td width="16%">&nbsp;</td>
		<td width="43%">
			<span class="ItemTitle"><center>Destination Module</center></span>
		</td>
	</tr>
	<tr>
		<td width="43%">
			<asp:DropDownList id="SourceInstance" width="100%" runat="server" AutoPostBack="true" />
		</td>
		<td width="16%">&nbsp;</td>
		<td align="right" width="43%">
			<asp:DropDownList id="DestinationInstance" width="100%" runat="server" AutoPostBack="true" />
		</td>
	</tr>
	<tr>
		<td colspan="3"><hr></td>
	</tr>
</table>
<table width="90%" align="center" border="0">
	<tr>
		<td width="43%">
			<span class="ItemTitle"><center>Source</center></span>
		</td>
		<td width="16%">&nbsp;</td>
		<td width="43%">
			<span class="ItemTitle"><center>Destination</center></span>
		</td>
	</tr>
	<tr>
		<td width="43%" valign="top">
			<asp:ListBox id="SourceListBox" scroll="true" Width="100%" rows="15" runat="server" />
		</td>
		<td width="16%" valign="top">

			<tra:LinkButton id="DeleteLeft_Btn" cssclass="CommandButton" width="100%" TextKey="DeleteLeft" Text="<- Delete" runat="server" />
			<tra:LinkButton id="MoveLeft_Btn" cssclass="CommandButton" width="100%" TextKey="MoveItem" Text="<- Move" runat="server" />
			<tra:LinkButton id="MoveRight_Btn" cssclass="CommandButton" width="100%" TextKey="MoveItem" Text="Move ->" runat="server" />
			<tra:LinkButton id="Copyright_Btn" cssclass="CommandButton" width="100%" TextKey="CopyItem" Text="Copy ->" runat="server" />
			<tra:LinkButton id="CopyAll_Btn" cssclass="CommandButton" width="100%" TextKey="CopyAll" Text=" Copy All ->" runat="server" />
			<tra:LinkButton id="DeleteRight_Btn" cssclass="CommandButton" width="100%" TextKey="DeleteRight" Text="Delete->" runat="server" />
		</td>
		<td width="43%" valign="top">
			<asp:ListBox id="DestListBox" scroll="true" Width="100%" rows="15" runat="server" />
		</td>
	</tr>
</table>
