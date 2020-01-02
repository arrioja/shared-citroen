<%@ Page language="c#" Codebehind="ConsultarPedidoC.aspx.cs" AutoEventWireup="false" Inherits="CITROEN.ConsultarPedidoC" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ConsultarPedidoC</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="ConsultarPedidoC" method="post" runat="server">
			<DIV id="principal" style="WIDTH: 700px; HEIGHT: 308px" align="center" runat="server"
				ms_positioning="FlowLayout">
				<P><asp:label id="Label8" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller"
						Font-Names="Rockwell">Seleccione el Criterio de Búsqueda</asp:label></P>
				<P><asp:radiobutton id="RadioButton1" runat="server" Font-Size="Smaller" Font-Names="Rockwell" AutoPostBack="True"
						Text="Nro. Pedido" GroupName="busqueda" Visible="False"></asp:radiobutton>&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:radiobutton id="RadioButton2" runat="server" Font-Size="Smaller" Font-Names="Rockwell" AutoPostBack="True"
						Text="Concesionario" GroupName="busqueda"></asp:radiobutton>&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:radiobutton id="RadioButton3" runat="server" Font-Size="Smaller" Font-Names="Rockwell" AutoPostBack="True"
						Text="Fecha" GroupName="busqueda"></asp:radiobutton></P>
				<P><asp:textbox id="txtBuscar" runat="server" Font-Names="Rockwell" Visible="False"></asp:textbox><asp:dropdownlist id="dropSucursal" runat="server" Font-Size="Smaller" Font-Names="Rockwell" AutoPostBack="True"
						Visible="False" Width="176px"></asp:dropdownlist><asp:calendar id="Calendar2" runat="server" ForeColor="Black" Font-Size="8pt" Font-Names="Verdana"
						Visible="False" Width="200px" DayNameFormat="FirstLetter" FirstDayOfWeek="Sunday" BackColor="White" BorderColor="#999999" SelectedDate="2004-11-03"
						VisibleDate="2004-11-12" CellPadding="4" Height="180px">
						<TodayDayStyle ForeColor="Black" BackColor="#CCCCCC"></TodayDayStyle>
						<SelectorStyle BackColor="#CCCCCC"></SelectorStyle>
						<NextPrevStyle VerticalAlign="Bottom"></NextPrevStyle>
						<DayHeaderStyle Font-Size="7pt" Font-Bold="True" BackColor="#CCCCCC"></DayHeaderStyle>
						<SelectedDayStyle Font-Bold="True" ForeColor="White" BackColor="#666666"></SelectedDayStyle>
						<TitleStyle Font-Bold="True" BorderColor="Black" BackColor="#999999"></TitleStyle>
						<WeekendDayStyle BackColor="#FFFFCC"></WeekendDayStyle>
						<OtherMonthDayStyle ForeColor="#808080"></OtherMonthDayStyle>
					</asp:calendar></P>
				<P align="center"><asp:button id="Button1" runat="server" Font-Size="Smaller" Font-Names="Rockwell" Text="Buscar"
						Visible="False" Width="142px"></asp:button></P>
			</DIV>
			<DIV id="sucursal" style="WIDTH: 700px; HEIGHT: 100px" align="center" runat="server"
				ms_positioning="FlowLayout">
				<P></P>
				<P><asp:label id="Label10" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller"
						Font-Names="Rockwell">Lista de Solicitudes</asp:label></P>
				<P><asp:table id="Table3" runat="server" ForeColor="Navy" Font-Size="Smaller" Font-Names="Rockwell"
						Width="612px" BackColor="White" BorderColor="#DE0A01" CellPadding="3" Height="22px" GridLines="Both"
						BorderWidth="2px" CellSpacing="0">
						<asp:TableRow>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="N&#250;mero Pedido"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Fecha"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Estado General"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Fecha Est. de Entrega"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Solicitante"></asp:TableCell>
						</asp:TableRow>
					</asp:table></P>
			</DIV>
			<DIV id="pedido" style="WIDTH: 700px; HEIGHT: 100px" align="center" runat="server" ms_positioning="FlowLayout">
				<P><asp:label id="Label1" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller"
						Font-Names="Rockwell">Datos del PR</asp:label></P>
				<P>
					<TABLE id="Table2" style="WIDTH: 623px; HEIGHT: 88px" cellSpacing="1" cellPadding="1" width="623"
						bgColor="#ffffff" border="0" runat="server">
						<TR>
							<TD style="WIDTH: 82px"><asp:label id="Label2" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller"
									Font-Names="Rockwell">Número:</asp:label></TD>
							<TD style="WIDTH: 221px"><asp:textbox id="txtNumero" runat="server" Font-Size="Smaller" Font-Names="Rockwell" ReadOnly="True"></asp:textbox></TD>
							<TD><asp:label id="Label3" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller"
									Font-Names="Rockwell">Tipo:</asp:label></TD>
							<TD><asp:textbox id="txtTipo" runat="server" Font-Size="Smaller" Font-Names="Rockwell" ReadOnly="True"></asp:textbox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 82px"><asp:label id="Label4" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller"
									Font-Names="Rockwell">Fecha:</asp:label></TD>
							<TD style="WIDTH: 221px"><asp:textbox id="txtFecha" runat="server" Font-Size="Smaller" Font-Names="Rockwell" ReadOnly="True"></asp:textbox></TD>
							<TD><asp:label id="Label5" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller"
									Font-Names="Rockwell">Fecha Est. de Entrega:</asp:label></TD>
							<TD><asp:textbox id="txtFechae" runat="server" Font-Size="Smaller" Font-Names="Rockwell"></asp:textbox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 82px"><asp:label id="Label6" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller"
									Font-Names="Rockwell">Estado:</asp:label></TD>
							<TD style="WIDTH: 221px"><asp:dropdownlist id="dropEstado" runat="server" Font-Size="Smaller" Font-Names="Rockwell" Width="156px">
									<asp:ListItem Value="En Proceso">En Proceso</asp:ListItem>
									<asp:ListItem Value="Procesado">Procesado</asp:ListItem>
									<asp:ListItem Value="En Proc. de Nacionalizaci&#243;n">En Proc. de Nacionalizaci&#243;n</asp:ListItem>
									<asp:ListItem Value="Despachado">Despachado</asp:ListItem>
									<asp:ListItem Value="En Transito">En Transito</asp:ListItem>
									<asp:ListItem Value="Rechazado">Rechazado</asp:ListItem>
								</asp:dropdownlist></TD>
							<TD><asp:label id="Label7" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller"
									Font-Names="Rockwell">Observación:</asp:label></TD>
							<TD><asp:textbox id="txtObservacion" runat="server" Font-Size="Smaller" Font-Names="Rockwell" ReadOnly="True"></asp:textbox></TD>
						</TR>
					</TABLE>
				</P>
				<P><asp:label id="Label9" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller"
						Font-Names="Rockwell">Lista de Productos:</asp:label></P>
				<P><asp:table id="Table1" runat="server" ForeColor="Navy" Font-Size="Smaller" Font-Names="Rockwell"
						Width="531px" BackColor="White" BorderColor="#DE0A01" CellPadding="3" Height="22px" GridLines="Both"
						BorderWidth="2px" CellSpacing="0">
						<asp:TableRow Font-Names="Rockwell">
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Modelo"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Cantidad"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Color"></asp:TableCell>
						</asp:TableRow>
					</asp:table></P>
				<P><asp:table id="Table4" runat="server" ForeColor="Navy" Font-Size="Smaller" Font-Names="Rockwell"
						Width="592px" BackColor="White" BorderColor="#DE0A01" CellPadding="3" Height="22px" GridLines="Both"
						BorderWidth="2px" CellSpacing="0">
						<asp:TableRow Font-Names="Rockwell">
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Referencia"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Solicitado"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Disponible"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Precio Unitario"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Monto total"></asp:TableCell>
						</asp:TableRow>
					</asp:table></P>
				<P><asp:label id="Label14" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller"
						Font-Names="Rockwell">Datos del Deposito:</asp:label></P>
				<P><asp:table id="Table5" runat="server" ForeColor="Navy" Font-Size="Smaller" Font-Names="Rockwell"
						Visible="False" Width="482px" BackColor="White" BorderColor="#DE0A01" CellPadding="3"
						Height="22px" GridLines="Both" BorderWidth="2px" CellSpacing="0">
						<asp:TableRow Font-Names="Rockwell">
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Nro. Deposito"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Banco"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Fecha"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Cantidad"></asp:TableCell>
						</asp:TableRow>
					</asp:table></P>
				<P><asp:button id="Button2" runat="server" Font-Names="Rockwell" Text="Guardar"></asp:button></P>
			</DIV>
			<P align="center"><asp:label id="lblError" runat="server" Font-Bold="True" ForeColor="Red" Font-Size="Smaller"
					Font-Names="Rockwell" Visible="False"></asp:label><asp:label id="lblMensaje" runat="server" Font-Bold="True" ForeColor="SteelBlue" Font-Size="Smaller"
					Font-Names="Rockwell" Visible="False"></asp:label><asp:label id="Label11" runat="server" Visible="False"></asp:label><asp:label id="Label12" runat="server" Visible="False"></asp:label><asp:label id="Label13" runat="server" Visible="False"></asp:label></P>
			<P align="center"><asp:label id="Label15" runat="server" Visible="False">Fecha seleccionada en formato dd/mm/aaaa</asp:label><asp:label id="Label16" runat="server" Visible="False">Fecha Actual en formato dd/mm/aaaa</asp:label></P>
		</form>
	</body>
</HTML>
