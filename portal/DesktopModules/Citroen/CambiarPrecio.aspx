<%@ Page language="c#" Codebehind="CambiarPrecio.aspx.cs" AutoEventWireup="false" Inherits="CITROEN.CambiarPrecio" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>CambiarPrecio</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="CambiarPrecio" method="post" runat="server">
			<DIV id="DIV1" style="WIDTH: 700px; HEIGHT: 100px" align="center" runat="server" ms_positioning="FlowLayout">
				<P><asp:label id="Label1" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Cambiar Precio PR</asp:label></P>
				<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="300" border="0">
					<TR>
						<TD><asp:label id="Label3" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Referencia:</asp:label></TD>
						<TD><asp:textbox id="txtReferencia" runat="server" Font-Size="Smaller" Font-Names="Rockwell" ReadOnly="True"></asp:textbox></TD>
					</TR>
					<TR>
						<TD><asp:label id="Label5" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Descripcion:</asp:label></TD>
						<TD><asp:textbox id="txtDescripcion" runat="server" Font-Size="Smaller" Font-Names="Rockwell" ReadOnly="True"></asp:textbox></TD>
					</TR>
					<TR>
						<TD><asp:label id="Label7" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Disponibilidad:</asp:label></TD>
						<TD><asp:textbox id="txtDisp" runat="server" Font-Size="Smaller" Font-Names="Rockwell"></asp:textbox></TD>
					</TR>
					<TR>
						<TD><asp:label id="Label2" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Precio Anterior:</asp:label></TD>
						<TD><asp:textbox id="txtPrecio" runat="server" Font-Size="Smaller" Font-Names="Rockwell" ReadOnly="True"></asp:textbox></TD>
					</TR>
					<TR>
						<TD><asp:label id="Label4" runat="server" Font-Bold="True" ForeColor="DimGray" Font-Size="Smaller" Font-Names="Rockwell">Precio Actual:</asp:label></TD>
						<TD><asp:textbox id="txtPrecioa" runat="server" Font-Size="Smaller" Font-Names="Rockwell"></asp:textbox></TD>
					</TR>
				</TABLE>
				<P><asp:button id="Button1" runat="server" Font-Size="Smaller" Font-Names="Rockwell" Text="Guardar"></asp:button></P>
			</DIV>
			<asp:label id="Label6" runat="server" Visible="False"></asp:label></form>
	</body>
</HTML>
