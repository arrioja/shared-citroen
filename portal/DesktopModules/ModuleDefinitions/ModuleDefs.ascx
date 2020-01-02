<%@ Control Inherits="Rainbow.DesktopModules.ModuleDefs" CodeBehind="ModuleDefs.ascx.cs" Language="c#" AutoEventWireup="false" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>

<div class=settings-table>
	<fieldset class=SettingsTableGroup>
		<legend class=SubSubHead>
			<tra:literal id=userTitle runat="server" Text="User Modules" TextKey="MODULE_DEFS_USER"></tra:Literal>
		</legend>
		<table class=SettingsTableGroup width="100%" border=0>
			<tr>
				<td>
					<asp:datalist class=SettingsTableGroup id=userModules runat="server" DataKeyField="ModuleDefID">
						<ItemTemplate>
							<asp:Label id=Label1 runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FriendlyName") %>' CssClass="Normal">
							</asp:Label>
						</ItemTemplate>
					</asp:datalist>
				</td>
			</tr>
		</table>
	</fieldset>
	<fieldset class=SettingsTableGroup>
		<legend class=SubSubHead>
			<tra:literal id="adminTitle" runat="server" Text="Admin Modules" TextKey="MODULE_DEFS_ADMIN"></tra:Literal>
		</legend>
		<table class=SettingsTableGroup width="100%" border=0>
			<tr>
				<td>
					<asp:datalist id=adminModules runat="server" DataKeyField="ModuleDefID">
						<ItemTemplate>
							<asp:Label id="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FriendlyName") %>' CssClass="Normal">
							</asp:Label>
						</ItemTemplate>
					</asp:datalist>
				</td>
			</tr>
		</table>
	</fieldset>
</div>
