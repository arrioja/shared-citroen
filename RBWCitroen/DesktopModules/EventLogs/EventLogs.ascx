<%@ Control language="c#" Inherits="Rainbow.DesktopModules.EventLogs" CodeBehind="EventLogs.ascx.cs" AutoEventWireup="false" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Import Namespace="System.Diagnostics" %>
Machine :
<asp:textbox id="MachineName" runat="server" AutoPostBack="True" OnTextChanged="MachineName_Change" Columns="10" CssClass="NormalTextBox">.</asp:textbox>
&nbsp;&nbsp;&nbsp; Log :
<asp:dropdownlist id="LogName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LogName_Change" CssClass="NormalTextBox"></asp:dropdownlist>
&nbsp;&nbsp;&nbsp; Source :
<asp:dropdownlist id="LogSource" runat="server" AutoPostBack="True" OnSelectedIndexChanged="LogSource_Change" CssClass="NormalTextBox"></asp:dropdownlist>
<br>
<asp:label id="Message" runat="server" CssClass="NormalRed"></asp:label><br>
<asp:datagrid id="LogGrid" runat="server" PageSize="100" OnSortCommand="LogGrid_Sort" AllowSorting="True" AutoGenerateColumns="False" OnPageIndexChanged="LogGrid_Change" PagerStyle-PrevPageText="Prev" PagerStyle-NextPageText="Next" PagerStyle-HorizontalAlign="Right" PagerStyle-Mode="NumericPages" AllowPaging="True">
	<Columns>
		<asp:TemplateColumn SortExpression="EntryType" HeaderText="Type">
			<ItemTemplate>
				<asp:Image id=Image1 runat="server" ImageUrl='<%#GetEntryTypeImage((EventLogEntryType) DataBinder.Eval(Container.DataItem, "EntryType"))%>'>
				</asp:Image>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="TimeGenerated" SortExpression="TimeGenerated" HeaderText="Date"></asp:BoundColumn>
		<asp:BoundColumn DataField="Source" SortExpression="Source" HeaderText="Source"></asp:BoundColumn>
		<asp:BoundColumn DataField="EventID" SortExpression="EventID" HeaderText="Event ID"></asp:BoundColumn>
		<asp:BoundColumn DataField="Message" SortExpression="Message" HeaderText="Message"></asp:BoundColumn>
	</Columns>
	<PagerStyle NextPageText="Next" PrevPageText="Prev" HorizontalAlign="Right" Position="TopAndBottom" Mode="NumericPages"></PagerStyle>
</asp:datagrid>
