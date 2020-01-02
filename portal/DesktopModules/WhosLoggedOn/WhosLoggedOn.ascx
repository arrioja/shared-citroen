<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Control language="c#" Inherits="Rainbow.DesktopModules.WhosLoggedOn" CodeBehind="WhosLoggedOn.ascx.cs" AutoEventWireup="false" %>
<tra:literal id="Literal1" runat="server" TextKey="WHOS_LOGGED_ON_1" Text="There are currently:"></tra:literal><br />
<b>
	<asp:Label id="LabelAnonUsersCount" runat="server">xx</asp:Label>
</b>
<tra:literal id="Literal2" runat="server" TextKey="WHOS_LOGGED_ON_2" Text="anonymous users online"></tra:literal><br />
<b>
	<asp:Label id="LabelRegUsersOnlineCount" runat="server">xx</asp:Label>
</b>
<tra:literal id="Literal3" runat="server" TextKey="WHOS_LOGGED_ON_3" Text=" registered users online: "></tra:literal>
<b><asp:Label id="LabelRegUserNames" runat="server">users list</asp:Label></b>
