<%@ Control Inherits="Rainbow.DesktopModules.SecurityCheck" CodeBehind="SecurityCheck.ascx.cs" Language="c#" AutoEventWireup="false" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<table cellpadding="2" cellspacing="0" border="0">
	<tr>
		<td>
			<tra:Literal runat="server" id="label_description" TextKey="ROLES_DESCRIPTION" Text="Roles"></tra:Literal>
			<asp:DropDownList id="ddlRoles" runat="server" DataTextField="RoleName" DataValueField="RoleID"></asp:DropDownList>
			<tra:Button id="btnSearch" Text="Search" TextKey="SECURITYCHECK_SEARCH" runat="server"></tra:Button>
			<tra:CheckBox id="chkAdmin" Text="Only Admin Modules" TextKey="SECURITYCHECK_ONLYADMIN" runat="server"></tra:CheckBox>
		</td>
	</tr>
	<tr>
		<td>
			<asp:DataGrid id="dgModules" runat="server" DataKeyField="ModuleID" AllowPaging="True" AllowSorting="True"
				AutoGenerateColumns="False">
				<FooterStyle CssClass="Grid_Footer"></FooterStyle>
				<AlternatingItemStyle CssClass="Grid_AlternatingItem"></AlternatingItemStyle>
				<ItemStyle CssClass="Grid_Item"></ItemStyle>
				<HeaderStyle CssClass="Grid_Header"></HeaderStyle>
				<Columns>
					<tra:TemplateColumn SortExpression="TabName" HeaderText="Tab" TextKey="SECURITYCHECK_HEADERTAB">
						<ItemTemplate>
							<asp:Label id=Label2 Text='<%# DataBinder.Eval(Container, "DataItem.TabName") %>' runat="server">
							</asp:Label>
						</ItemTemplate>
					</tra:TemplateColumn>
					<tra:TemplateColumn SortExpression="ModuleTitle" HeaderText="Name" TextKey="SECURITYCHECK_HEADERNAME">
						<ItemTemplate>
							<asp:Label id=Label1 Text='<%# DataBinder.Eval(Container, "DataItem.ModuleTitle") %>' runat="server">
							</asp:Label>
						</ItemTemplate>
					</tra:TemplateColumn>
					<tra:TemplateColumn SortExpression="FriendlyName" HeaderText="Module Type" TextKey="SECURITYCHECK_HEADERTYPE">
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FriendlyName") %>'>
							</asp:Label>
						</ItemTemplate>
					</tra:TemplateColumn>
					<tra:TemplateColumn SortExpression="IsAdmin" HeaderText="Admin" TextKey="SECURITYCHECK_HEADERADMIN">
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IsAdmin") %>'>
							</asp:Label>
						</ItemTemplate>
					</tra:TemplateColumn>
					<tra:TemplateColumn SortExpression="CanView" HeaderText="View" TextKey="SECURITYCHECK_HEADERVIEW">
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CanView") %>'>
							</asp:Label>
						</ItemTemplate>
					</tra:TemplateColumn>
					<tra:TemplateColumn SortExpression="CanAdd" HeaderText="Add" TextKey="SECURITYCHECK_HEADERADD">
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CanAdd") %>'>
							</asp:Label>
						</ItemTemplate>
					</tra:TemplateColumn>
					<tra:TemplateColumn SortExpression="CanEdit" HeaderText="Edit" TextKey="SECURITYCHECK_HEADEREDIT">
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CanEdit") %>'>
							</asp:Label>
						</ItemTemplate>
					</tra:TemplateColumn>
					<tra:TemplateColumn SortExpression="CanDelete" HeaderText="Delete" TextKey="SECURITYCHECK_HEADERDELETE">
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CanDelete") %>'>
							</asp:Label>
						</ItemTemplate>
					</tra:TemplateColumn>
					<tra:TemplateColumn SortExpression="CanProperties" HeaderText="Properties" TextKey="SECURITYCHECK_HEADERPROPERTIES">
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CanProperties") %>'>
							</asp:Label>
						</ItemTemplate>
					</tra:TemplateColumn>
					<tra:TemplateColumn SortExpression="CanMoveModule" HeaderText="Move" TextKey="SECURITYCHECK_HEADERMOVE">
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CanMove") %>'>
							</asp:Label>
						</ItemTemplate>
					</tra:TemplateColumn>
					<tra:TemplateColumn SortExpression="CanDeleteModule" HeaderText="Delete Mod." TextKey="SECURITYCHECK_HEADERDELMOD">
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CanDeleteModule") %>'>
							</asp:Label>
						</ItemTemplate>
					</tra:TemplateColumn>
				</Columns>
				<PagerStyle Mode="NumericPages"></PagerStyle>
			</asp:DataGrid>
		</td>
	</tr>
	<tr>
		<td>
		</td>
	</tr>
</table>
