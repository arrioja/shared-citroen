<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Control language="c#" Inherits="Rainbow.Admin.Blacklist" CodeBehind="Blacklist.ascx.cs" AutoEventWireup="false" %>
<asp:datagrid id="myDataGrid" runat="server" Border="0" width="100%" AutoGenerateColumns="False" EnableViewState="False" AllowSorting="True" OnSortCommand="SortList">
	<Columns>
		<tra:BoundColumn DataField="Name" SortExpression="Name" TextKey="BLACKLIST_NAME" HeaderText="Name">
			<HeaderStyle cssclass="NormalBold"></HeaderStyle>
			<ItemStyle cssclass="Normal"></ItemStyle>
		</tra:BoundColumn>
		<tra:BoundColumn DataField="Email" SortExpression="Email" TextKey="BLACKLIST_EMAIL" HeaderText="Email">
			<HeaderStyle cssclass="NormalBold"></HeaderStyle>
			<ItemStyle cssclass="Normal"></ItemStyle>
		</tra:BoundColumn>
		<tra:BoundColumn DataField="SendNewsletter" SortExpression="SendNewsletter" TextKey="BLACKLIST_SENDNEWSLETTER" HeaderText="Send Newsletter">
			<HeaderStyle cssclass="NormalBold"></HeaderStyle>
			<ItemStyle cssclass="Normal"></ItemStyle>
		</tra:BoundColumn>
		<tra:BoundColumn DataField="Reason" SortExpression="Reason" TextKey="BLACKLIST_REASON" HeaderText="Reason">
			<HeaderStyle cssclass="NormalBold"></HeaderStyle>
			<ItemStyle cssclass="Normal"></ItemStyle>
		</tra:BoundColumn>
		<tra:BoundColumn DataField="Date" SortExpression="Date" TextKey="BLACKLIST_DATE" DataFormatString="{0:d}" HeaderText="Date">
			<HeaderStyle cssclass="NormalBold"></HeaderStyle>
			<ItemStyle cssclass="Normal"></ItemStyle>
		</tra:BoundColumn>
		<tra:BoundColumn DataField="LastSend" SortExpression="LastSend" TextKey="BLACKLIST_LASTSEND" HeaderText="Last Send" DataFormatString="{0:d}">
			<HeaderStyle cssclass="NormalBold"></HeaderStyle>
			<ItemStyle cssclass="Normal"></ItemStyle>
		</tra:BoundColumn>
	</Columns>
</asp:datagrid>
