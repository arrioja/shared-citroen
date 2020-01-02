<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Control Inherits="Rainbow.DesktopModules.Tabs" CodeBehind="Tabs.ascx.cs" Language="c#" AutoEventWireup="false" %>
<table cellpadding="5" cellspacing="0">
	<TR>
		<td colspan="3" class="SubHead"><tra:Label TextKey="AM_TABS" Text="Tabs" id="lblHead" runat="server"></tra:Label>
		</td>
	</TR>
	<tr valign="top">
		<td>
			<asp:ListBox id="tabList" width=400 DataSource="<%# portalTabs %>" DataTextField="Name" DataValueField="ID" rows=20 runat="server" CssClass="NormalTextBox"/>
		</td>
		<td>
			&nbsp;
		</td>
		<td>
			<table>
				<tr>
					<td>
						<tra:ImageButton id="upBtn" TextKey="MOVE_UP" Text="Move up" CommandName="up" runat="server" />
					</td>
				</tr>
				<tr>
					<td>
						<tra:ImageButton id="downBtn" TextKey="MOVE_DOWN" Text="Move down" CommandName="down" runat="server" />
					</td>
				</tr>
				<tr>
					<td>
						<tra:ImageButton id="EditBtn" TextKey="EDITBTN" Text="Edit button" runat="server" />
					</td>
				</tr>
				<tr>
					<td>
						<tra:ImageButton id="DeleteBtn" TextKey="DELETEBTN" Text="Delete button" runat="server" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<TR>
		<td colSpan="3">
			<tra:LinkButton id="addBtn" cssclass="CommandButton" TextKey="ADDTAB" Text="Add New Tab" runat="server" />
		</td>
	</TR>
</table>
