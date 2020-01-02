<%@ Control language="c#" Inherits="Rainbow.DesktopModules.DatabaseTool" CodeBehind="DatabaseTool.ascx.cs" AutoEventWireup="false" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<asp:label id="lblConnectedError" runat="server" ForeColor="Red" />
<asp:Panel id="panConnected" runat="server">
<tra:Label id="Label1" runat="server" TextKey="DBTOOL_OWNER">Owner</tra:Label>&nbsp; 
<asp:TextBox id="tbUserName" runat="server" CssClass="NormalTextBox" Width="60px">dbo</asp:TextBox>&nbsp; 
<asp:DropDownList id="ddObjectSelectList" runat="server" CssClass="NormalTextBox" AutoPostBack="true">
	<asp:ListItem Value="U" Selected="True">User table</asp:ListItem>
	<asp:ListItem Value="P">Stored procedure</asp:ListItem>
	<asp:ListItem Value="C">CHECK constraint</asp:ListItem>
	<asp:ListItem Value="D">Default or DEFAULT constraint</asp:ListItem>
	<asp:ListItem Value="F">FOREIGN KEY constraint</asp:ListItem>
	<asp:ListItem Value="L">Log</asp:ListItem>
	<asp:ListItem Value="FN">Scalar function</asp:ListItem>
	<asp:ListItem Value="IF">Inlined table-function</asp:ListItem>
	<asp:ListItem Value="PK">PRIMARY KEY constraint (type is K)</asp:ListItem>
	<asp:ListItem Value="RF">Replication filter stored procedure </asp:ListItem>
	<asp:ListItem Value="S">System table</asp:ListItem>
	<asp:ListItem Value="TF">Table function</asp:ListItem>
	<asp:ListItem Value="TR">Trigger</asp:ListItem>
	<asp:ListItem Value="UQ">UNIQUE constraint (type is K)</asp:ListItem>
	<asp:ListItem Value="V">View</asp:ListItem>
	<asp:ListItem Value="X">Extended stored procedure</asp:ListItem>
	</asp:DropDownList><BR>
<asp:listbox id="lbObjects" runat="server" CssClass="NormalTextBox" Height="100px"></asp:listbox><BR>
<tra:Button id="btnGetObjectProps" runat="server" TextKey="DBTOOL_GETPROPERTIES" Text="Properties"></tra:Button>
<tra:Button id="btnGetObjectInfo" runat="server" TextKey="DBTOOL_GETINFO" Text="Info"></tra:Button>
<tra:Button id="btnGetObjectInfoExtended" runat="server" TextKey="DBTOOL_GETINFOEXTENDED" Text="InfoExtended"></tra:Button>
<tra:Button id="btnGetObjectData" runat="server" TextKey="DBTOOL_GETDATA" Text="Data"></tra:Button><BR>
<asp:Panel id="panQueryBox" runat="server">
		<BR>
		<asp:textbox id="txtQueryBox" runat="server" CssClass="NormalTextBox" Width="100%" Height="150px"
			TextMode="MultiLine"></asp:textbox>
		<BR>
		<tra:Button id="btnQueryExecute" runat="server" TextKey="DBTOOL_QUERYEXECUTE" Text="Execute Query"></tra:Button>
		<asp:label id="lblRes" ForeColor="Red" runat="server"></asp:label>
		<BR>
	</asp:Panel><BR>
<asp:datagrid id="DataGrid1" runat="server" Width="100%" Height="90px" BorderWidth="1px" BorderColor="#697898"
		HorizontalAlign="Center" BackColor="White" PageSize="20" Font-Size="Smaller" CellPadding="3">
	<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="#697898"></HeaderStyle>
	<AlternatingItemStyle BackColor="#F0F0F0"></AlternatingItemStyle>
		<ItemStyle HorizontalAlign="Left" BorderWidth="2px" ForeColor="#000066" BorderColor="White"
			Width="80%"></ItemStyle>
	</asp:datagrid>
</asp:Panel>
