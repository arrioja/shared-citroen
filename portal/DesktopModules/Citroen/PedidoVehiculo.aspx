<%@ Page language="c#" Codebehind="PedidoVehiculo.aspx.cs" AutoEventWireup="false" Inherits="CITROEN.PedidoVehiculo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PedidoVehiculo</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="PedidoVehiculo" method="post" runat="server">
			<P><asp:label id="Label1" runat="server" Visible="False">Label</asp:label><asp:label id="Label29" runat="server" Visible="False">29</asp:label></P>
			<DIV id="Div3" style="WIDTH: 700px; HEIGHT: 100px" align="center" runat="server" ms_positioning="FlowLayout">
				<P><asp:label id="Label27" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Ingrese su Clave de Acceso</asp:label></P>
				<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:label id="Label26" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Login:</asp:label><asp:textbox id="txtLogin" runat="server"></asp:textbox></P>
				<P><asp:label id="Label25" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Password:</asp:label><asp:textbox id="txtPassword" runat="server"></asp:textbox></P>
				<P><asp:button id="Button5" runat="server" Font-Names="Rockwell" Text="Buscar"></asp:button></P>
				<P><asp:label id="lblError2" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="Red" Font-Bold="True"></asp:label></P>
			</DIV>
			<DIV id="Div5" style="WIDTH: 700px; HEIGHT: 100px" align="center" runat="server" ms_positioning="FlowLayout"><asp:label id="Label28" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Listado de  Vehiculos</asp:label>
				<HR width="100%" color="#ff0033" SIZE="1">
				<asp:table id="Table6" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Width="405px" BorderStyle="Solid" BorderColor="#DE0A01" CellPadding="3" CellSpacing="1" BorderWidth="2px" GridLines="Both">
					<asp:TableRow>
						<asp:TableCell Font-Bold="True" Text="Modelos de vehiculos disponibles"></asp:TableCell>
					</asp:TableRow>
				</asp:table></DIV>
			<DIV id="DIV4" style="WIDTH: 700px; HEIGHT: 348px" align="center" runat="server" ms_positioning="FlowLayout">
				<DIV id="DIV1" style="WIDTH: 724px; POSITION: relative; HEIGHT: 298px" align="center" runat="server" ms_positioning="GridLayout">
					<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 0px; WIDTH: 300px; POSITION: absolute; TOP: 0px; HEIGHT: 284px" cellSpacing="1" cellPadding="1" width="300" align="left" border="1">
						<TR>
							<TD bgColor="#de0a01"><asp:label id="Label11" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="White" Font-Bold="True">Modelo</asp:label></TD>
						</TR>
						<TR>
							<TD bgColor="#d3d3d3"><asp:label id="Label14" runat="server" Font-Names="Rockwell" Font-Size="Smaller">Label</asp:label></TD>
						</TR>
						<TR>
							<TD bgColor="#de0a01"><asp:label id="Label2" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="White" Font-Bold="True">Motor</asp:label></TD>
						</TR>
						<TR>
							<TD style="HEIGHT: 19px" bgColor="lightgrey"><asp:label id="Label3" runat="server" Font-Names="Rockwell" Font-Size="Smaller">Label</asp:label></TD>
						</TR>
						<TR>
							<TD bgColor="#de0a01"><asp:label id="Label4" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="White" Font-Bold="True">Frenos</asp:label></TD>
						</TR>
						<TR>
							<TD bgColor="lightgrey"><asp:label id="Label5" runat="server" Font-Names="Rockwell" Font-Size="Smaller">Label</asp:label></TD>
						</TR>
						<TR>
							<TD bgColor="#de0a01"><asp:label id="Label6" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="White" Font-Bold="True">Dirección</asp:label></TD>
						</TR>
						<TR>
							<TD bgColor="lightgrey"><asp:label id="Label7" runat="server" Font-Names="Rockwell" Font-Size="Smaller">Label</asp:label></TD>
						</TR>
						<TR>
							<TD bgColor="#de0a01"><asp:label id="Label8" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="White" Font-Bold="True">Caja de Velocidades</asp:label></TD>
						</TR>
						<TR>
							<TD bgColor="lightgrey"><asp:label id="Label9" runat="server" Font-Names="Rockwell" Font-Size="Smaller">Label</asp:label></TD>
						</TR>
						<TR>
							<TD bgColor="#de0a01"><asp:label id="Label10" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="White" Font-Bold="True">Equipamento</asp:label></TD>
						</TR>
						<TR>
							<TD bgColor="lightgrey"><asp:table id="Table2" runat="server"></asp:table></TD>
						</TR>
						<TR>
							<TD bgColor="#de0a01">
								<P align="center"><asp:button id="Button2" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Text="Hacer Pedido"></asp:button></P>
							</TD>
						</TR>
					</TABLE>
					<asp:image id="Image1" style="Z-INDEX: 102; LEFT: 304px; POSITION: absolute; TOP: 0px" runat="server" Width="413px" Height="285px"></asp:image></DIV>
				<P>
					<asp:Label id="lblError3" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="Red" Font-Bold="True"></asp:Label></P>
			</DIV>
			<DIV id="DIV2" style="WIDTH: 700px; HEIGHT: 100px" align="center" runat="server" ms_positioning="FlowLayout">
				<P><asp:label id="Label20" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Datos del Pedido</asp:label></P>
				<P align="center">
					<TABLE id="Table4" style="WIDTH: 597px; HEIGHT: 55px" cellSpacing="1" cellPadding="1" width="597" border="0">
						<TR>
							<TD style="WIDTH: 103px; HEIGHT: 26px"><asp:label id="Label24" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Número:</asp:label></TD>
							<TD style="WIDTH: 218px; HEIGHT: 26px"><asp:textbox id="txtNumero" runat="server" ReadOnly="True"></asp:textbox></TD>
							<TD style="WIDTH: 72px; HEIGHT: 26px"><asp:label id="Label23" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Tipo:</asp:label></TD>
							<TD style="HEIGHT: 26px"><asp:dropdownlist id="dropTipo" runat="server" Width="152px" Enabled="False">
									<asp:ListItem Value="Stock">Stock</asp:ListItem>
									<asp:ListItem Value="Urgente">Urgente</asp:ListItem>
								</asp:dropdownlist></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 103px"><asp:label id="Label22" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Observacion:</asp:label></TD>
							<TD style="WIDTH: 218px"><asp:textbox id="txtObservacion" runat="server"></asp:textbox></TD>
							<TD style="WIDTH: 72px"><asp:label id="Label21" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Fecha:</asp:label></TD>
							<TD><asp:textbox id="txtFecha" runat="server" Enabled="False"></asp:textbox></TD>
						</TR>
					</TABLE>
				<P>
					<TABLE id="Table3" style="WIDTH: 474px; HEIGHT: 15px" cellSpacing="1" cellPadding="1" width="474" border="0">
						<TR>
							<TD style="WIDTH: 144px"><asp:label id="Label15" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Colores Disponibles:</asp:label></TD>
							<TD style="WIDTH: 80px">
								<P><asp:dropdownlist id="dropColor" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Width="176px" AutoPostBack="True"></asp:dropdownlist></P>
							</TD>
							<TD style="WIDTH: 130px"></TD>
							<TD></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 144px"><asp:label id="Label16" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True" Width="148px">Cantidad Disponible:</asp:label></TD>
							<TD style="WIDTH: 80px"><asp:textbox id="txtCantidad" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Width="79px"></asp:textbox></TD>
							<TD style="WIDTH: 130px"><asp:label id="Label17" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True" Width="132px">Cantidad a Solicitar</asp:label></TD>
							<TD><asp:textbox id="txtSolicita" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Width="79px"></asp:textbox></TD>
						</TR>
					</TABLE>
				</P>
				<P><asp:button id="Button1" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Text="Agregar"></asp:button><asp:button id="Button4" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Text="Quitar" Width="61px"></asp:button></P>
				<P>
					<TABLE id="Table5" style="WIDTH: 218px; HEIGHT: 115px" cellSpacing="0" cellPadding="0" width="218" border="0">
						<TR>
							<TD><asp:label id="Label19" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Color:</asp:label></TD>
							<TD><asp:label id="Label18" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Cantidad:</asp:label></TD>
						</TR>
						<TR>
							<TD><asp:listbox id="listColor" runat="server" Font-Names="Rockwell" Width="160px" Height="100px" AutoPostBack="True"></asp:listbox></TD>
							<TD>
								<P align="center"><asp:listbox id="listCantidad" runat="server" Font-Names="Rockwell" Width="59px" Height="100px" AutoPostBack="True"></asp:listbox></P>
							</TD>
						</TR>
					</TABLE>
					<asp:listbox id="listSerial" runat="server" Visible="False" Font-Names="Rockwell"></asp:listbox></P>
				<P align="center"><asp:label id="lblError" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="Red" Font-Bold="True"></asp:label></P>
				<P align="center"><asp:button id="Button3" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Text="Guardar"></asp:button></P>
			</DIV>
		</form>
	</body>
</HTML>
