<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Control language="c#" Inherits="Rainbow.DesktopModules.Monitoring" CodeBehind="Monitoring.ascx.cs" AutoEventWireup="false" %>
<asp:Panel ID="MonitoringPanel" Visible="true" Runat="server">
	<table cellSpacing="2" cellPadding="2" width="100%" align="center" border="0">
		<tr vAlign="top">
			<td class="SubHead" vAlign="top" align="left" width="100" height="19">Report Type
			</td>
			<td class="SubHead" vAlign="top" align="left" height="19" width="170"><asp:dropdownlist id="cboReportType" Runat="server" CssClass="NormalTextBox">
					<asp:ListItem Value="Detailed Site Log" Selected="True">Detailed Site Log</asp:ListItem>
					<asp:ListItem Value="Page Popularity">Page Popularity</asp:ListItem>
					<asp:ListItem Value="Most Active Users">Most Active Users</asp:ListItem>
					<asp:ListItem Value="Page Views By Day">Page Views By Day</asp:ListItem>
					<asp:ListItem Value="Page Views By Browser Type">Page Views By Browser Type</asp:ListItem>
				</asp:dropdownlist></td>
			<TD class="SubHead" vAlign="top" align="left" height="19" rowSpan="5"><asp:checkbox id="CheckBoxPageRequests" Checked="True" runat="server"></asp:checkbox><asp:label id="Label1" runat="server">Include page requests</asp:label><br>
				<asp:checkbox id="CheckBoxLogons" Checked="True" runat="server"></asp:checkbox><asp:label id="Label3" runat="server">Include logins</asp:label><br>
				<asp:checkbox id="CheckBoxLogouts" Checked="True" runat="server"></asp:checkbox><asp:label id="Label2" runat="server">Include logouts</asp:label><br>
				<asp:checkbox id="CheckBoxIncludeMonitorPage" runat="server"></asp:checkbox><asp:label id="Label4" runat="server">Include Monitor Page</asp:label><br>
				<asp:checkbox id="CheckBoxIncludeAdminUser" runat="server"></asp:checkbox><asp:label id="Label5" runat="server">Include admin user</asp:label><br>
				<asp:checkbox id="CheckBoxIncludeMyIPAddress" runat="server"></asp:checkbox><asp:label id="Label6" runat="server">Include my IP address</asp:label></TD>
		</tr>
		<tr vAlign="top">
			<td class="SubHead"><tra:literal id="Literal2" runat="server" TextKey="START_DATE" Text="Start date"></tra:literal>:
			</td>
			<td><asp:textbox id="txtStartDate" runat="server" CssClass="NormalTextBox"></asp:textbox></td>
			<TD></TD>
		</tr>
		<tr vAlign="top">
			<td></td>
			<td><tra:requiredfieldvalidator id="RequiredStartDate" runat="server" TextKey="ERROR_VALID_STARTDATE" Display="Dynamic"
					ErrorMessage="You Must Enter a Valid Start Date" ControlToValidate="txtStartDate"></tra:requiredfieldvalidator>
				<tra:comparevalidator id="VerifyStartDate" runat="server" TextKey="ERROR_VALID_STARTDATE" Display="Dynamic"
					ErrorMessage="You Must Enter a Valid Start Date" ControlToValidate="txtStartDate" Operator="DataTypeCheck"
					Type="Date"></tra:comparevalidator></td>
			<td></td>
		</tr>
		<tr vAlign="top">
			<td class="SubHead"><tra:literal id="Literal3" runat="server" TextKey="END_DATE" Text="End date"></tra:literal>:
			</td>
			<td><asp:textbox id="txtEndDate" runat="server" CssClass="NormalTextBox"></asp:textbox></td>
			<TD></TD>
		</tr>
		<tr vAlign="top">
			<td></td>
			<td><tra:requiredfieldvalidator id="RequiredEndDate" runat="server" TextKey="ERROR_VALID_ENDDATE" Display="Dynamic"
					ErrorMessage="You Must Enter a Valid End Date" ControlToValidate="txtEndDate"></tra:requiredfieldvalidator>
				<tra:comparevalidator id="VerifyExpireDate" runat="server" TextKey="ERROR_VALID_ENDDATE" Display="Dynamic"
					ErrorMessage="You Must Enter a Valid End Date" ControlToValidate="txtEndDate" Operator="DataTypeCheck"
					Type="Date"></tra:comparevalidator></td>
			<td></td>
		</tr>
		<tr vAlign="top">
			<td></td>
			<td class="NormalBold" vAlign="top" align="left"></td>
			<TD class="NormalBold" vAlign="top" align="left">
				<asp:linkbutton id="cmdDisplay" runat="server" Text="Display" cssclass="CommandButton"></asp:linkbutton>&nbsp;&nbsp;
			</TD>
		</tr>
	</table>
	<br>
	<asp:image id="ChartImage" Runat="server" Visible="False"></asp:image>
	<asp:Label id="LabelNoData" runat="server" Visible="False">No data available</asp:Label>
	<asp:datagrid id="myDataGrid" runat="server" OnSortCommand="SortTasks" AllowSorting="True" EnableViewState="False"
		AutoGenerateColumns="True" CssClass="normal" Border="0" CellPadding="3" PageSize="1">
		<PagerStyle Mode="NumericPages"></PagerStyle>
	</asp:datagrid>
</asp:Panel>
<asp:Label Id="ErrorLabel" Runat="server" CssClass="Error" Visible="false"></asp:Label>
