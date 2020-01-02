<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Control language="c#" Inherits="Rainbow.DesktopModules.Documents" CodeBehind="Documents.ascx.cs" AutoEventWireup="false" %>
<asp:datagrid id="myDataGrid" BorderWidth="0px" width="100%" AutoGenerateColumns="false" EnableViewState="false"
	runat="server" AllowSorting="True">
	<Columns>
		<asp:TemplateColumn>
			<ItemTemplate>
				<tra:HyperLink TextKey="EDIT" Text="Edit" id="editLink" ImageUrl='<%# CurrentTheme.GetImage("Buttons_Edit", "Edit.gif").ImageUrl %>'
					NavigateUrl='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Documents/DocumentsEdit.aspx","ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID")  + "&mid=" + ModuleID) %>' runat="server" />
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn>
			<ItemTemplate>
				<tra:HyperLink TextKey="CONTENTTYPE" Text="Content Type" id="contentType" ImageUrl='<%# "~/DesktopModules/Documents/types/" + DataBinder.Eval(Container.DataItem,"contentType")%>' NavigateUrl='<%# GetBrowsePath(DataBinder.Eval(Container.DataItem,"FileNameUrl").ToString(), DataBinder.Eval(Container.DataItem,"ContentSize"), (int) DataBinder.Eval(Container.DataItem,"ItemID")) %>' Target="_new" runat="server" />
			</ItemTemplate>
		</asp:TemplateColumn>
		<tra:TemplateColumn SortExpression="FileFriendlyName" TextKey="TITLE" HeaderText="Title" HeaderStyle-CssClass="NormalBold">
			<ItemTemplate>
				<asp:HyperLink id="docLink" Text='<%# DataBinder.Eval(Container.DataItem,"FileFriendlyName") %>' NavigateUrl='<%# GetBrowsePath(DataBinder.Eval(Container.DataItem,"FileNameUrl").ToString(), DataBinder.Eval(Container.DataItem,"ContentSize"), (int) DataBinder.Eval(Container.DataItem,"ItemID")) %>' CssClass="Normal" Target="_new" runat="server" />
			</ItemTemplate>
		</tra:TemplateColumn>
		<tra:BoundColumn SortExpression="CreatedByUser" TextKey="DOCUMENT_OWNER" HeaderText="Owner" DataField="CreatedByUser"
			ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
		<tra:BoundColumn SortExpression="Category" TextKey="DOCUMENT_AREA" HeaderText="Area" DataField="Category"
			ItemStyle-Wrap="false" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
		<tra:BoundColumn SortExpression="CreatedDate" TextKey="DOCUMENT_LAST_UPDATED" HeaderText="Last Updated"
			DataField="CreatedDate" DataFormatString="{0:d}" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" />
	</Columns>
</asp:datagrid>
