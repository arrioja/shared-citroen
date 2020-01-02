<%@ Page language="c#" Codebehind="Usuarios.aspx.cs" AutoEventWireup="false" Inherits="WebApplication1.Usuarios" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Usuarios</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Usuarios" method="post" runat="server">
			<DIV id="DIV1" style="WIDTH: 700px; HEIGHT: 40px" align="center" runat="server" ms_positioning="FlowLayout">
				<P>
					<asp:label id="Label8" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Seleccione la Operación que Desea Realizar:</asp:label></P>
				<P>
					<asp:RadioButton id="RadioButton1" runat="server" Text="Nuevo" GroupName="operacion" AutoPostBack="True" Font-Size="Smaller" Font-Names="Rockwell"></asp:RadioButton>&nbsp;
					<asp:RadioButton id="RadioButton2" runat="server" Text="Modificar" GroupName="operacion" AutoPostBack="True" Font-Size="Smaller" Font-Names="Rockwell"></asp:RadioButton>&nbsp;
					<asp:RadioButton id="RadioButton3" runat="server" Text="Eliminar" GroupName="operacion" AutoPostBack="True" Font-Size="Smaller" Font-Names="Rockwell"></asp:RadioButton></P>
			</DIV>
			<DIV id="DIV2" style="WIDTH: 700px; HEIGHT: 80px" runat="server" ms_positioning="FlowLayout">
				<P align="center">
					<asp:label id="lblActualizar" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True" Width="485px"></asp:label></P>
				<P align="center">
					<asp:Label id="Label2" runat="server" Font-Size="Smaller" Font-Names="Rockwell">Usuario:</asp:Label>
					<asp:TextBox id="txtUsuario" runat="server"></asp:TextBox></P>
				<P align="center">
					<asp:Button id="Button1" runat="server" Text="Buscar" Font-Size="Smaller" Font-Names="Rockwell"></asp:Button></P>
			</DIV>
			<DIV id="DIV3" style="WIDTH: 700px; HEIGHT: 171px" align="center" runat="server" ms_positioning="FlowLayout">
				<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="300" align="center" border="0">
					<TR>
						<TD>
							<asp:Label id="Label1" runat="server" Font-Size="Smaller" Font-Names="Rockwell">Sucursal:</asp:Label></TD>
						<TD>
							<asp:TextBox id="txtSucursal" runat="server" AutoPostBack="True" Width="175px" Visible="False"></asp:TextBox>
							<asp:dropdownlist id="dropSucursal" runat="server" Font-Size="Smaller" Font-Names="Rockwell" AutoPostBack="True" Width="176px"></asp:dropdownlist></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label6" runat="server" Font-Size="Smaller" Font-Names="Rockwell">Direccion:</asp:Label></TD>
						<TD>
							<asp:TextBox id="txtDireccion" runat="server" Width="174px"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label3" runat="server" Font-Size="Smaller" Font-Names="Rockwell">Login:</asp:Label></TD>
						<TD>
							<asp:TextBox id="txtLogin" runat="server" Width="174px"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label4" runat="server" Font-Size="Smaller" Font-Names="Rockwell">Password:</asp:Label></TD>
						<TD>
							<asp:TextBox id="txtPassword" runat="server" Width="174px"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD>
							<asp:Label id="Label5" runat="server" Width="134px" Font-Size="Smaller" Font-Names="Rockwell">Confirmar Password:</asp:Label></TD>
						<TD>
							<asp:TextBox id="txtCpassword" runat="server" Width="174px"></asp:TextBox></TD>
					</TR>
				</TABLE>
				<asp:Button id="Button2" runat="server" Text="Guardar" Visible="False"></asp:Button>
				<asp:Button id="Button3" runat="server" Font-Size="Smaller" Font-Names="Rockwell" Text="Guardar Cambios" Visible="False"></asp:Button>
				<P align="center">
					<asp:Button id="Button4" runat="server" Font-Size="Smaller" Font-Names="Rockwell" Text="Eliminar" Visible="False"></asp:Button></P>
			</DIV>
			<P align="center">&nbsp;</P>
			<P align="center">
				<asp:Label id="lblError" runat="server" Font-Bold="True" ForeColor="Red" Font-Size="Smaller" Font-Names="Rockwell" Visible="False"></asp:Label>
				<asp:Label id="lblMensaje" runat="server" Font-Bold="True" ForeColor="SteelBlue" Font-Size="Smaller" Font-Names="Rockwell" Visible="False"></asp:Label></P>
		</form>
	</body>
</HTML>
