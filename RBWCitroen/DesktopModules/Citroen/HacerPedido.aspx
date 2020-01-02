<%@ Page language="c#" Codebehind="HacerPedido.aspx.cs" AutoEventWireup="false" Inherits="CITROEN.HacerPedido" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			&nbsp;
			<DIV id="DIV1" style="WIDTH: 700px; HEIGHT: 40px" align="center" runat="server" ms_positioning="FlowLayout">
				<P>
					<asp:label id="Label8" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Seleccione el Producto a Solicitar</asp:label></P>
				<P>
					<asp:RadioButton id="RadioButton2" runat="server" Font-Size="Smaller" Font-Names="Rockwell" Text="Vehiculo" AutoPostBack="True" GroupName="operacion"></asp:RadioButton>&nbsp;
					<asp:RadioButton id="RadioButton3" runat="server" Font-Size="Smaller" Font-Names="Rockwell" Text="Repuesto" AutoPostBack="True" GroupName="operacion"></asp:RadioButton></P>
			</DIV>
			<DIV id="Div4" style="WIDTH: 700px; HEIGHT: 100px" align="center" runat="server" ms_positioning="FlowLayout">
				<P>
					<asp:label id="Label17" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Ingrese su Clave de Acceso</asp:label></P>
				<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:Label id="Label16" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Login:</asp:Label>
					<asp:TextBox id="txtLogin" runat="server"></asp:TextBox></P>
				<P>
					<asp:Label id="Label15" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Password:</asp:Label>
					<asp:TextBox id="txtPassword" runat="server"></asp:TextBox></P>
				<P>
					<asp:Button id="Button4" runat="server" Font-Names="Rockwell" Text="Buscar"></asp:Button></P>
				<P>
					<asp:Label id="lblError2" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="Red" Font-Bold="True"></asp:Label></P>
			</DIV>
			<DIV id="DIV3" style="WIDTH: 700px; HEIGHT: 185px" runat="server" ms_positioning="FlowLayout">
				<P align="center">
					<asp:Label id="Label9" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Datos del PR:</asp:Label></P>
				<P align="center">
					<TABLE id="Table3" style="WIDTH: 483px; HEIGHT: 55px" cellSpacing="1" cellPadding="1" width="483" border="0">
						<TR>
							<TD>
								<asp:Label id="Label1" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Número:</asp:Label></TD>
							<TD>
								<asp:TextBox id="txtNumero" runat="server" ReadOnly="True"></asp:TextBox></TD>
							<TD>
								<asp:Label id="Label2" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Tipo:</asp:Label></TD>
							<TD>
								<asp:DropDownList id="dropTipo" runat="server" Width="152px">
									<asp:ListItem Value="Stock">Stock</asp:ListItem>
									<asp:ListItem Value="Urgente">Urgente</asp:ListItem>
								</asp:DropDownList></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="Label7" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Observacion:</asp:Label></TD>
							<TD>
								<asp:TextBox id="txtObservacion" runat="server"></asp:TextBox></TD>
							<TD>
								<asp:Label id="Label14" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Fecha:</asp:Label></TD>
							<TD>
								<asp:TextBox id="txtFecha" runat="server" Enabled="False"></asp:TextBox></TD>
						</TR>
					</TABLE>
				</P>
				<P align="center">
					<asp:Label id="Label10" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Datos del Repuesto:</asp:Label></P>
				<P align="center">
					<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="300" align="center" border="0">
						<TR>
							<TD>
								<asp:Label id="Label3" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Referencia:</asp:Label></TD>
							<TD style="WIDTH: 169px">
								<asp:TextBox id="txtReferencia" runat="server"></asp:TextBox></TD>
							<TD>
								<asp:Label id="Label5" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Descripcion:</asp:Label></TD>
							<TD>
								<asp:TextBox id="txtDescripcion" runat="server"></asp:TextBox></TD>
						</TR>
						<TR>
							<TD>
								<asp:Label id="Label6" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Cantidad:</asp:Label></TD>
							<TD style="WIDTH: 169px">
								<asp:TextBox id="txtCantidad" runat="server"></asp:TextBox></TD>
							<TD></TD>
							<TD></TD>
						</TR>
					</TABLE>
				</P>
				<P align="center">
					<asp:Button id="Button1" runat="server" Font-Size="Smaller" Font-Names="Rockwell" Text="Agregar"></asp:Button>
					<asp:Button id="Button2" runat="server" Font-Size="Smaller" Font-Names="Rockwell" Text="Quitar" Width="61px"></asp:Button></P>
				<P align="center">
					<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="300" border="0">
						<TR>
							<TD>
								<asp:Label id="Label11" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Referencia:</asp:Label></TD>
							<TD>
								<asp:Label id="Label12" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Descripcion:</asp:Label></TD>
							<TD>
								<asp:Label id="Label13" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Cantidad:</asp:Label></TD>
						</TR>
						<TR>
							<TD>
								<asp:ListBox id="listReferencia" runat="server" AutoPostBack="True" Height="100px" Width="145px"></asp:ListBox></TD>
							<TD>
								<asp:ListBox id="listDescripcion" runat="server" AutoPostBack="True" Height="100px" Width="160px"></asp:ListBox></TD>
							<TD>
								<P align="center">
									<asp:ListBox id="listCantidad" runat="server" AutoPostBack="True" Height="100px" Width="59px"></asp:ListBox></P>
							</TD>
						</TR>
					</TABLE>
				</P>
				<P align="center">
					<asp:Label id="lblError" runat="server" Font-Bold="True" ForeColor="Red" Font-Size="Smaller" Font-Names="Rockwell"></asp:Label></P>
				<P align="center">
					<asp:Button id="Button3" runat="server" Font-Size="Smaller" Font-Names="Rockwell" Text="Guardar"></asp:Button></P>
			</DIV>
			<P>&nbsp;</P>
			<P align="center">
				<asp:Label id="Label18" runat="server" Visible="False"></asp:Label>
				<asp:Label id="lblMensaje" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DodgerBlue" Font-Bold="True"></asp:Label></P>
		</form>
	</body>
</HTML>
