<%@ Page language="c#" Codebehind="ConsultarPedidoS.aspx.cs" AutoEventWireup="false" Inherits="CITROEN.ConsultarEstado" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ConsultarEstado</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="ConsultarEstado" method="post" runat="server">
			<DIV id="DIV1" style="WIDTH: 700px; HEIGHT: 100px" align="center" runat="server" ms_positioning="FlowLayout">
				<P align="center">
					<asp:label id="Label8" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Ingrese su Clave de Acceso</asp:label></P>
				<P align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:Label id="Label1" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Login:</asp:Label>
					<asp:TextBox id="txtLogin" runat="server" Width="130px"></asp:TextBox></P>
				<P align="center">
					<asp:Label id="Label2" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Password:</asp:Label>&nbsp;
					<asp:TextBox id="txtPassword" runat="server" Width="130px"></asp:TextBox></P>
				<P align="center">
					<asp:Button id="Button1" runat="server" Font-Names="Rockwell" Text="Aceptar" Font-Size="Smaller" Width="60px"></asp:Button></P>
			</DIV>
			<DIV id="DIV2" style="WIDTH: 700px; HEIGHT: 40px" runat="server" ms_positioning="FlowLayout" align="center">
				<P align="center">
					<asp:label id="Label4" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Lista de Solicitudes</asp:label></P>
				<P>
					<asp:table id="Table1" runat="server" ForeColor="SlateGray" Font-Size="Smaller" Font-Names="Rockwell" BackColor="White" BorderStyle="Solid" BorderColor="#DE0A01" CellPadding="3" CellSpacing="1" BorderWidth="2px" GridLines="Both" Width="482px" Height="22px">
						<asp:TableRow Font-Names="Rockwell">
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="N&#250;mero Pedido"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Fecha"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Estado General"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Fecha Est. de Entrega"></asp:TableCell>
						</asp:TableRow>
					</asp:table></P>
			</DIV>
			<DIV id="criterio" style="WIDTH: 700px; HEIGHT: 121px" align="center" runat="server" ms_positioning="FlowLayout">
				<P>
					<asp:label id="Label3" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Seleccione el Criterio de Búsqueda del PR</asp:label></P>
				<P>
					<asp:RadioButton id="numero" runat="server" Font-Size="Smaller" Font-Names="Rockwell" Text="Número" GroupName="busqueda" AutoPostBack="True" Checked="True"></asp:RadioButton>&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:RadioButton id="todos" runat="server" Font-Size="Smaller" Font-Names="Rockwell" Text="Todos" GroupName="busqueda" AutoPostBack="True"></asp:RadioButton>&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:RadioButton id="fecha" runat="server" Font-Size="Smaller" Font-Names="Rockwell" Text="Fecha" GroupName="busqueda" AutoPostBack="True"></asp:RadioButton></P>
				<P>
					<asp:TextBox id="txtBuscar" runat="server" Font-Names="Rockwell"></asp:TextBox>
					<asp:Calendar id="Calendar2" runat="server" Width="200px" Height="180px" CellPadding="4" Visible="False" SelectedDate="2004-11-03" VisibleDate="2004-11-12" ForeColor="Black" Font-Size="8pt" Font-Names="Verdana" BorderColor="#999999" BackColor="White" DayNameFormat="FirstLetter" FirstDayOfWeek="Sunday">
						<TodayDayStyle ForeColor="Black" BackColor="#CCCCCC"></TodayDayStyle>
						<SelectorStyle BackColor="#CCCCCC"></SelectorStyle>
						<NextPrevStyle VerticalAlign="Bottom"></NextPrevStyle>
						<DayHeaderStyle Font-Size="7pt" Font-Bold="True" BackColor="#CCCCCC"></DayHeaderStyle>
						<SelectedDayStyle Font-Bold="True" ForeColor="White" BackColor="#666666"></SelectedDayStyle>
						<TitleStyle Font-Bold="True" BorderColor="Black" BackColor="#999999"></TitleStyle>
						<WeekendDayStyle BackColor="#FFFFCC"></WeekendDayStyle>
						<OtherMonthDayStyle ForeColor="#808080"></OtherMonthDayStyle>
					</asp:Calendar></P>
				<P align="center">
					<asp:Button id="buscarPedido" runat="server" Font-Size="Smaller" Font-Names="Rockwell" Text="Buscar" Width="60px"></asp:Button></P>
			</DIV>
			<P align="center">
				<asp:Label id="lblError" runat="server" Font-Bold="True" ForeColor="Red" Font-Size="Smaller" Font-Names="Rockwell" Visible="False"></asp:Label>
				<asp:Label id="lblNombreS" runat="server" Visible="False">Label</asp:Label>
				<asp:Label id="Label15" runat="server" Visible="False">Label</asp:Label></P>
		</form>
	</body>
</HTML>
