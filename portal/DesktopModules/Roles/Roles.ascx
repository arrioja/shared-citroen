<%@ Control Inherits="Rainbow.DesktopModules.Roles" CodeBehind="Roles.ascx.cs" Language="c#" AutoEventWireup="false" targetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<table cellpadding="2" cellspacing="0" border="0">
	<tr>
		<td class="Normal">
			<tra:Literal runat="server" id="label_description" TextKey="ROLES_DESCRIPTION" Text="Roles"></tra:Literal>
		</td>
	</tr>
	<tr>
		<td>
			<asp:DataList id="rolesList" DataKeyField="RoleID" runat="server">
				<ItemTemplate>
					<table cellspacing="3">
						<tr>
							<td width=30>
					<tra:ImageButton id="ImageButton2" runat="server" ImageUrl='<%# CurrentTheme.GetImage("Buttons_Edit", "Edit.gif").ImageUrl %>' CommandName="edit" TextKey="EDIT_THIS_ITEM" AlternateText="Edit this item"></tra:ImageButton>
							</td>
							<td width=30>
								<tra:ImageButton id="ImageButton1" CausesValidation="false" runat="server" ImageUrl='<%# CurrentTheme.GetImage("Buttons_Delete", "Delete.gif").ImageUrl %>' CommandName="delete" TextKey="DELETE_THIS_ITEM" AlternateText="Delete this item"></tra:ImageButton>
							</td>
							<td>
								<asp:HyperLink id="Name" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RoleName") %>' cssclass="Normal">
								</asp:HyperLink>
							</td>
						</tr>
					</table>
				</ItemTemplate>
				<EditItemTemplate>
					<table cellspacing="3">
						<tr>
							<td>
					<asp:Textbox id=roleName runat="server" width="200" Text='<%# DataBinder.Eval(Container.DataItem, "RoleName") %>' cssclass="NormalTextBox">
								</asp:Textbox>
							</td>
							<td>
					<tra:LinkButton id="LinkButton2" cssclass="CommandButton" runat="server" TextKey="APPLY" CommandName="apply">Apply</tra:LinkButton>
							</td>
							<td>
								<tra:LinkButton id="LinkButton1" cssclass="CommandButton" runat="server" TextKey="ROLE_CHANGE_MEMBERS" CommandName="members">Change Role Members</tra:LinkButton>
							</td>
						</tr>
					</table>
				</EditItemTemplate>
			</asp:DataList>
		</td>
	</tr>
	<tr>
		<td>
			<tra:LinkButton cssclass="CommandButton" TextKey="AM_ADDROLE" Text="Add New Role" runat="server"
				id="AddRoleBtn"></tra:LinkButton>
		</td>
	</tr>
	<tr>
		<td class="Error">
			<tra:Literal runat="server" id="labelError" visible="false" TextKey="ROLE_DELETE_FAILED" Text="Failed to delete the role you selected. Please ensure no users are making use of this role."></tra:Literal>
		</td>
	</tr>
</table>
