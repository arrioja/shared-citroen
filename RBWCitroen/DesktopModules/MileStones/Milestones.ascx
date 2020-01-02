<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Milestones.ascx.cs" Inherits="Rainbow.DesktopModules.Milestones.Milestones" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<asp:DataGrid id="myDataGrid" HeaderStyle-CssClass="Normal" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="Normal" AutoGenerateColumns="false" CellPadding="5" BorderWidth="0" Width="100%" EnableViewState="true" runat="server">
	<Columns>
		<asp:TemplateColumn>
			<ItemTemplate>
				<tra:HyperLink id="editLink" TextKey="EDIT" Text="Edit"
					ImageUrl='<%# CurrentTheme.GetImage("Buttons_Edit", "Edit.gif").ImageUrl %>'
					NavigateUrl='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/MileStones/MilestonesEdit.aspx", "ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID") + "&Mid=" + ModuleID) %>' 
					Visible="<%# IsEditable %>" runat="server" />
			</ItemTemplate>
		</asp:TemplateColumn>
		<tra:BoundColumn DataField="Title" TextKey="MILESTONE_TITLE" HeaderText="Title" runat="server" />
		<tra:BoundColumn DataField="EstCompleteDate" TextKey="MILESTONE_COMPL_DATE" HeaderText="Compl. Date" runat="server" DataFormatString="{0:d}" />
		<tra:BoundColumn DataField="Status" TextKey="MILESTONE_STATUS" HeaderText="Status" runat="server" />
	</Columns>
</asp:DataGrid>