<%@ Page language="c#" Codebehind="DetallePedido.aspx.cs" AutoEventWireup="false" Inherits="CITROEN.DetallePedido" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DetallePedido</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="DetallePedido" method="post" runat="server">
			<P align="center">&nbsp;</P>
			<DIV id="Div1" style="WIDTH: 702px; HEIGHT: 16px" align="center" runat="server" ms_positioning="FlowLayout">
				<P align="center">
					<asp:label id="lblMensaje" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True"></asp:label></P>
			</DIV>
			<P align="center"></P>
			<DIV id="detalle" style="WIDTH: 702px; HEIGHT: 17px" align="center" runat="server" ms_positioning="FlowLayout">
				<P align="center"><asp:table id="Table1" runat="server" ForeColor="Navy" Font-Size="Smaller" Font-Names="Rockwell" Height="22px" Width="545px" GridLines="Both" BorderWidth="2px" CellSpacing="0" CellPadding="3" BorderColor="#DE0A01" BackColor="White">
						<asp:TableRow Font-Names="Rockwell">
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Modelo"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Cantidad"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Color"></asp:TableCell>
						</asp:TableRow>
					</asp:table></P>
			</DIV>
			<asp:label id="lblNro" runat="server" Visible="False">Label</asp:label>
			<DIV id="respuesto" style="WIDTH: 702px; HEIGHT: 56px" align="center" runat="server" ms_positioning="FlowLayout">
				<P align="center">
					<asp:table id="Table2" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="Navy" BackColor="White" BorderColor="#DE0A01" CellPadding="3" CellSpacing="0" BorderWidth="2px" GridLines="Both" Width="609px" Height="22px">
						<asp:TableRow Font-Names="Rockwell">
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Referencia"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Descripci&#243;n"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Solicitado"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Disponible"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Precio Unitario"></asp:TableCell>
							<asp:TableCell Font-Names="Rockwell" Font-Bold="True" Text="Monto total"></asp:TableCell>
						</asp:TableRow>
					</asp:table></P>
				<P align="center">
					<asp:Button id="Button3" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Width="121px" Text="Confirmar Deposito"></asp:Button>
					<asp:Button id="Button2" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Width="60px" Text="Anular"></asp:Button></P>
			</DIV>
			<P align="center">
				<asp:label id="lblMensaje1" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="MidnightBlue" Font-Bold="True"></asp:label></P>
			<DIV id="deposito" style="WIDTH: 700px; HEIGHT: 106px" align="center" runat="server" ms_positioning="FlowLayout">
				<P align="center">
					<asp:label id="Label8" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Datos del Deposito</asp:label></P>
				<P align="left">
					<TABLE id="Table3" style="WIDTH: 686px; HEIGHT: 55px" cellSpacing="1" cellPadding="1" width="686" border="0" runat="server">
						<TR>
							<TD style="WIDTH: 116px; HEIGHT: 26px">
								<asp:Label id="Label1" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Número:</asp:Label></TD>
							<TD style="WIDTH: 293px; HEIGHT: 26px">
								<asp:TextBox id="txtNumeroD" runat="server" Width="230px"></asp:TextBox></TD>
							<TD style="WIDTH: 154px; HEIGHT: 26px">
								<asp:Label id="Label2" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True" Width="124px">Nombre del Banco:</asp:Label></TD>
							<TD style="HEIGHT: 26px">
								<asp:TextBox id="txtNombreB" runat="server" Width="230px"></asp:TextBox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 116px">
								<asp:Label id="Label4" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Fecha:</asp:Label></TD>
							<TD style="WIDTH: 293px">
								<asp:TextBox id="txtFecha" runat="server" Width="186px" Enabled="False"></asp:TextBox></TD>
							<TD style="WIDTH: 154px">
								<P>
									<asp:Label id="Label3" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Monto:</asp:Label></P>
							</TD>
							<TD>
								<P align="left">
									<asp:TextBox id="txtMonto" runat="server" Width="230px"></asp:TextBox></P>
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 116px" align="middle" colSpan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								<asp:Calendar id="Calendar2" runat="server" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" BackColor="White" BorderColor="#999999" CellPadding="4" Width="200px" Height="180px" SelectedDate="2004-11-03" VisibleDate="2004-11-12" DayNameFormat="FirstLetter" FirstDayOfWeek="Sunday">
									<TodayDayStyle ForeColor="Black" BackColor="#CCCCCC"></TodayDayStyle>
									<SelectorStyle BackColor="#CCCCCC"></SelectorStyle>
									<NextPrevStyle VerticalAlign="Bottom"></NextPrevStyle>
									<DayHeaderStyle Font-Size="7pt" Font-Bold="True" BackColor="#CCCCCC"></DayHeaderStyle>
									<SelectedDayStyle Font-Bold="True" ForeColor="White" BackColor="#666666"></SelectedDayStyle>
									<TitleStyle Font-Bold="True" BorderColor="Black" BackColor="#999999"></TitleStyle>
									<WeekendDayStyle BackColor="#FFFFCC"></WeekendDayStyle>
									<OtherMonthDayStyle ForeColor="#808080"></OtherMonthDayStyle>
								</asp:Calendar></TD>
							<TD style="WIDTH: 154px" align="middle" colSpan="2"></TD>
						</TR>
					</TABLE>
				</P>
				<asp:Button id="Button1" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Width="60px" Text="Aceptar"></asp:Button></DIV>
		</form>
	</body>
</HTML>
