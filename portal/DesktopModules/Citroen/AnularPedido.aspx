<%@ Page language="c#" Codebehind="AnularPedido.aspx.cs" AutoEventWireup="false" Inherits="CITROEN.AnularPedido" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>AnularPedido</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="AnularPedido" method="post" runat="server">
			<P></P>
			<P align="center">
				<asp:label id="Label8" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Ingrese el Número de Pedido a Eliminar</asp:label>
			<P align="center">
				<asp:TextBox id="txtBuscar" runat="server" Width="130px"></asp:TextBox></P>
			<P align="center">
				<asp:Button id="Button2" runat="server" Text="Buscar" Font-Names="Rockwell" Font-Size="Smaller"></asp:Button></P>
			<P align="center">
				<asp:Label id="lblError" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="Red" Font-Bold="True"></asp:Label></P>
			<DIV id="anular" style="WIDTH: 700px; HEIGHT: 100px" align="center" runat="server" ms_positioning="FlowLayout">
				<P align="left">
					<asp:label id="Label6" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Datos Principales de Pedido a Eliminar</asp:label></P>
				<P align="left">
					<asp:Label id="Label9" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Número de Pedido:</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:TextBox id="TextBox1" runat="server"></asp:TextBox></P>
				<P align="left">
					<asp:Label id="lblTipo" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Tipo de Pedido:</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:TextBox id="TextBox2" runat="server"></asp:TextBox></P>
				<P align="left">
					<asp:Label id="Label4" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Estado:</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:TextBox id="TextBox3" runat="server"></asp:TextBox></P>
				<P align="left">
					<asp:Label id="Label3" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Fecha:</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:TextBox id="TextBox4" runat="server"></asp:TextBox></P>
				<P align="center">
					<asp:Button id="Button1" runat="server" Text="Eliminar" Font-Names="Rockwell" Font-Size="Smaller"></asp:Button></P>
			</DIV>
		</form>
	</body>
</HTML>
