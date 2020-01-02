<%@ Control language="c#" Inherits="Rainbow.DesktopModules.Contacts" CodeBehind="Contacts.ascx.cs" AutoEventWireup="false" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<asp:datagrid id="myDataGrid" runat="server" Border="0" width="100%" AutoGenerateColumns="False"
	EnableViewState="False" AllowSorting="True">
	<Columns>
		<asp:TemplateColumn>
			<ItemTemplate>
				<tra:HyperLink TextKey="EDIT" Text="Edit" ImageUrl='<%# CurrentTheme.GetImage("Buttons_Edit", "Edit.gif").ImageUrl %>'
					NavigateUrl='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Contacts/ContactsEdit.aspx",TabID,"ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID") + "&mid=" + ModuleID)%>' Visible='<%# IsEditable %>' runat="server" />
			</ItemTemplate>
		</asp:TemplateColumn>
		<tra:TemplateColumn HeaderText="Name" SortExpression="Name" TextKey="CONTACTS_NAME">
			<HeaderStyle CssClass="NormalBold"></HeaderStyle>
			<ItemStyle CssClass="Normal"></ItemStyle>
			<ItemTemplate>
				<asp:Literal Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>' runat="server">
				</asp:Literal>
			</ItemTemplate>
		</tra:TemplateColumn>
		<tra:TemplateColumn HeaderText="Role" SortExpression="Role" TextKey="CONTACTS_ROLE">
			<HeaderStyle CssClass="NormalBold"></HeaderStyle>
			<ItemStyle CssClass="Normal"></ItemStyle>
			<ItemTemplate>
				<asp:Literal Text='<%# DataBinder.Eval(Container, "DataItem.Role") %>' runat="server">
				</asp:Literal>
			</ItemTemplate>
		</tra:TemplateColumn>
		<tra:TemplateColumn HeaderText="Email" SortExpression="Email" TextKey="CONTACTS_EMAIL">
			<HeaderStyle CssClass="NormalBold"></HeaderStyle>
			<ItemStyle Wrap="False"></ItemStyle>
			<ItemTemplate>
				<tra:HyperLink id="HyperLinkView" runat="server" NavigateUrl='<%# "mailto:" + DataBinder.Eval(Container.DataItem,"Email")%>' Text='<%# DataBinder.Eval(Container, "DataItem.Email") %>' CssClass="Normal">
				</tra:HyperLink>
			</ItemTemplate>
		</tra:TemplateColumn>
		<tra:TemplateColumn HeaderText="Office" SortExpression="Contact1" TextKey="CONTACTS_CONTACT1">
			<HeaderStyle CssClass="NormalBold"></HeaderStyle>
			<ItemStyle CssClass="Normal"></ItemStyle>
			<ItemTemplate>
				<asp:Literal Text='<%# DataBinder.Eval(Container, "DataItem.Contact1") %>' runat="server">
				</asp:Literal>
			</ItemTemplate>
		</tra:TemplateColumn>
		<tra:TemplateColumn HeaderText="Mobile" SortExpression="Contact2" TextKey="CONTACTS_CONTACT2">
			<HeaderStyle CssClass="NormalBold"></HeaderStyle>
			<ItemStyle Wrap="False" CssClass="Normal"></ItemStyle>
			<ItemTemplate>
				<asp:Literal Text='<%# DataBinder.Eval(Container, "DataItem.Contact2") %>' runat="server">
				</asp:Literal>
			</ItemTemplate>
		</tra:TemplateColumn>
		<tra:TemplateColumn HeaderText="Fax" SortExpression="Fax" TextKey="CONTACTS_FAX">
			<HeaderStyle CssClass="NormalBold"></HeaderStyle>
			<ItemStyle Wrap="False" CssClass="Normal"></ItemStyle>
			<ItemTemplate>
				<asp:Literal Text='<%# DataBinder.Eval(Container, "DataItem.Fax") %>' runat="server">
				</asp:Literal>
			</ItemTemplate>
		</tra:TemplateColumn>
		<tra:TemplateColumn HeaderText="Address" SortExpression="Address" TextKey="CONTACTS_ADDRESS">
			<HeaderStyle CssClass="NormalBold"></HeaderStyle>
			<ItemStyle CssClass="Normal"></ItemStyle>
			<ItemTemplate>
				<asp:Literal Text='<%# DataBinder.Eval(Container, "DataItem.Address") %>' runat="server">
				</asp:Literal>
			</ItemTemplate>
		</tra:TemplateColumn>
	</Columns>
</asp:datagrid>
