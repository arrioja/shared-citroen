<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Control language="c#" Inherits="Rainbow.DesktopModules.Tasks" CodeBehind="Tasks.ascx.cs" AutoEventWireup="false" %>
<asp:datagrid id="myDataGrid" runat="server" BorderWidth="0" width="100%" AutoGenerateColumns="False"
	EnableViewState="False" AllowSorting="True" OnSortCommand="SortTasks">
	<Columns>
		<asp:TemplateColumn>
			<ItemTemplate>
				<tra:HyperLink id="HyperLinkEdit" TextKey="EDIT" Text="Edit" runat="server" Visible="<%# IsEditable %>" NavigateUrl='<%# "~/DesktopModules/Tasks/TasksEdit.aspx?ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID") + "&amp;mid=" + ModuleID %>' 
					ImageUrl='<%# CurrentTheme.GetImage("Buttons_Edit", "Edit.gif").ImageUrl %>'></tra:HyperLink>
			</ItemTemplate>
		</asp:TemplateColumn>
		<tra:BoundColumn TextKey="TASK_DUEDATE" DataField="DueDate" SortExpression="DueDate" HeaderText="Due Date"
			DataFormatString="{0:d}">
			<HeaderStyle CssClass="NormalBold"></HeaderStyle>
			<ItemStyle CssClass="Normal"></ItemStyle>
		</tra:BoundColumn>
		<tra:TemplateColumn HeaderText="Title" SortExpression="Title" TextKey="TASK_TITLE">
			<HeaderStyle CssClass="NormalBold"></HeaderStyle>
			<ItemTemplate>
				<tra:HyperLink id="HyperLinkView" runat="server" NavigateUrl='<%# "~/DesktopModules/Tasks/TasksView.aspx?ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID") + "&amp;mid=" + ModuleID %>' Text='<%# DataBinder.Eval(Container, "DataItem.Title") %>' CssClass="Normal">
				</tra:HyperLink>
			</ItemTemplate>
		</tra:TemplateColumn>
		<tra:TemplateColumn HeaderText="Status" SortExpression="Status" TextKey="TASK_STATUS">
			<HeaderStyle CssClass="NormalBold"></HeaderStyle>
			<ItemStyle CssClass="Normal"></ItemStyle>
			<ItemTemplate>
				<tra:Literal TextKey='<%# "TASK_STATE_"+DataBinder.Eval(Container, "DataItem.Status") %>' Text='' runat="server">
				</tra:Literal>
			</ItemTemplate>
		</tra:TemplateColumn>
		<tra:TemplateColumn HeaderText="Priority" SortExpression="Priority" TextKey="TASK_PRIORITY">
			<HeaderStyle CssClass="NormalBold"></HeaderStyle>
			<ItemStyle CssClass="Normal"></ItemStyle>
			<ItemTemplate>
				<tra:Literal TextKey='<%# "TASK_PRIORITY_"+DataBinder.Eval(Container, "DataItem.Priority") %>' Text='' runat="server">
				</tra:Literal>
			</ItemTemplate>
		</tra:TemplateColumn>
		<tra:BoundColumn TextKey="TASK_ASSIGNEDTO" DataField="AssignedTo" SortExpression="AssignedTo" HeaderText="Assigned To">
			<HeaderStyle CssClass="NormalBold"></HeaderStyle>
			<ItemStyle CssClass="Normal"></ItemStyle>
		</tra:BoundColumn>
		<tra:TemplateColumn HeaderText="% Complete" SortExpression="PercentComplete" TextKey="TASK_COMPLETION">
			<HeaderStyle CssClass="NormalBold"></HeaderStyle>
			<ItemStyle CssClass="Normal"></ItemStyle>
			<ItemTemplate>
				<table class="Normal" cellspacing="0" cellpadding="0" width="100%" bordercolor='black'
					border="1">
					<tr>
						<%# PerCent(DataBinder.Eval(Container, "DataItem.PercentComplete")) %>
					</tr>
				</table>
			</ItemTemplate>
		</tra:TemplateColumn>
		<tra:BoundColumn TextKey="DOCUMENT_LAST_UPDATED" DataField="ModifiedDate" SortExpression="ModifiedDate"
			HeaderText="Last Updated" DataFormatString="{0:d}">
			<HeaderStyle CssClass="NormalBold"></HeaderStyle>
			<ItemStyle CssClass="Normal"></ItemStyle>
		</tra:BoundColumn>
	</Columns>
</asp:datagrid>
