<%@ Page language="c#" Codebehind="IngresarVehiculo.aspx.cs" AutoEventWireup="false" Inherits="CITROEN.WebForm1" %>
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
			<DIV id="principal" style="WIDTH: 718px; HEIGHT: 71px" align="center" runat="server" ms_positioning="FlowLayout">
				<P align="center">&nbsp;
					<asp:label id="Label8" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True">Seleccione la Operación que Desea Realizar:</asp:label></P>
				<P align="center">
					<asp:radiobutton id="Radiobutton4" runat="server" Text="Nuevo" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="Black" AutoPostBack="True" GroupName="operacion"></asp:radiobutton>&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:radiobutton id="Radiobutton3" runat="server" Text="Modificar" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="Black" AutoPostBack="True" GroupName="operacion"></asp:radiobutton>&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:radiobutton id="Radiobutton5" runat="server" Text="Eliminar" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="Black" AutoPostBack="True" GroupName="operacion"></asp:radiobutton></P>
			</DIV>
			<DIV id="actualizar" style="WIDTH: 717px; HEIGHT: 101px" align="left" runat="server" ms_positioning="FlowLayout">
				<P align="center">
					<asp:label id="lblActualizar" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True" Width="485px"></asp:label></P>
				<P align="center">&nbsp;
					<asp:label id="Label7" runat="server" Font-Names="Rockwell" Font-Size="Smaller">Modelo:</asp:label>&nbsp;
					<asp:textbox id="txtBuscarlo" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Visible="False"></asp:textbox>
					<asp:dropdownlist id="dropmodelo" runat="server" Font-Size="Smaller" Font-Names="Rockwell" AutoPostBack="True" Width="176px"></asp:dropdownlist></P>
				<P align="center">
					<asp:button id="Button5" runat="server" Text="Buscar" Font-Names="Rockwell" Font-Size="Smaller" Width="51px"></asp:button></P>
			</DIV>
			<P align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:label id="lblError" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="Red" Font-Bold="True"></asp:label></P>
			<P></P>
			<DIV id="todo" style="WIDTH: 719px; HEIGHT: 327px" align="center" runat="server" ms_positioning="FlowLayout">
				<P align="left">
					<asp:label id="Label1" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True" Width="116px">Datos del Modelo:</asp:label></P>
				<DIV id="DIV1" style="WIDTH: 696px; HEIGHT: 128px" align="center" runat="server" ms_positioning="FlowLayout">
					<TABLE id="Table1" style="WIDTH: 683px; HEIGHT: 109px" cellSpacing="1" cellPadding="1" width="683" align="center" border="0" runat="server">
						<TR>
							<TD style="WIDTH: 73px">
								<asp:label id="lblModelo" runat="server" Font-Names="Rockwell" Font-Size="Smaller">Nombre:</asp:label></TD>
							<TD style="WIDTH: 239px">
								<asp:textbox id="txtModelo" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Width="210px" MaxLength="10"></asp:textbox></TD>
							<TD style="WIDTH: 137px">
								<asp:label id="lblDireccion" runat="server" Font-Names="Rockwell" Font-Size="Smaller">Dirección:</asp:label></TD>
							<TD>
								<asp:textbox id="txtDireccion" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Width="210px" MaxLength="10"></asp:textbox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 73px; HEIGHT: 26px">
								<asp:label id="lblMotor" runat="server" Font-Names="Rockwell" Font-Size="Smaller">Motor:</asp:label></TD>
							<TD style="WIDTH: 239px; HEIGHT: 26px">
								<asp:textbox id="txtMotor" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Width="210px" MaxLength="20"></asp:textbox></TD>
							<TD style="WIDTH: 137px; HEIGHT: 26px">
								<asp:label id="lblCaja" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Width="113px">Caja de Velocidad:</asp:label></TD>
							<TD style="HEIGHT: 26px">
								<asp:textbox id="txtCaja" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Width="210px" MaxLength="10"></asp:textbox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 73px; HEIGHT: 25px">
								<asp:label id="lblFrenos" runat="server" Font-Names="Rockwell" Font-Size="Smaller">Frenos:</asp:label></TD>
							<TD style="WIDTH: 239px; HEIGHT: 25px">
								<asp:textbox id="txtFrenos" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Width="210px" MaxLength="200"></asp:textbox></TD>
							<TD style="WIDTH: 137px; HEIGHT: 25px">
								<asp:label id="lblCosto" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Visible="False">Costo:</asp:label></TD>
							<TD style="HEIGHT: 25px">
								<asp:textbox id="txtCosto" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Width="210px" MaxLength="200" Visible="False">0</asp:textbox></TD>
						</TR>
						<TR>
							<TD style="WIDTH: 73px; HEIGHT: 25px">
								<asp:label id="lblFoto" runat="server" Font-Names="Rockwell" Font-Size="Smaller">Foto:</asp:label></TD>
							<TD style="WIDTH: 239px; HEIGHT: 25px">
								<asp:textbox id="txtFoto" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Width="210px"></asp:textbox></TD>
							<TD style="WIDTH: 137px; HEIGHT: 25px"></TD>
							<TD style="HEIGHT: 25px"></TD>
						</TR>
					</TABLE>
				</DIV>
				<P align="left">
					<asp:label id="Label6" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True" Width="89px">Equipamiento:</asp:label></P>
				<DIV id="DIV3" style="WIDTH: 697px; HEIGHT: 119px" align="center" runat="server" ms_positioning="FlowLayout">
					<TABLE id="Table3" style="WIDTH: 581px; HEIGHT: 28px" cellSpacing="1" cellPadding="1" width="581" align="left" border="0">
						<TR>
							<TD style="WIDTH: 130px">
								<asp:label id="lblEquipamiento" runat="server" Font-Names="Rockwell" Font-Size="Smaller">Descripción:</asp:label></TD>
							<TD>
								<P>
									<asp:textbox id="txtEquipamiento" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Width="374px"></asp:textbox>
									<asp:Button id="Button1" runat="server" Text="Agregar" Font-Names="Rockwell" Font-Size="XX-Small" Width="51px"></asp:Button></P>
							</TD>
						</TR>
					</TABLE>
					<P align="right">&nbsp;</P>
					<P align="right">
						<TABLE id="Table2" style="WIDTH: 620px; HEIGHT: 60px" cellSpacing="1" cellPadding="1" width="620" border="0">
							<TR>
								<TD style="WIDTH: 53px"></TD>
								<TD>
									<asp:ListBox id="listEquipamiento" runat="server" AutoPostBack="True" Width="377px"></asp:ListBox>
									<asp:Button id="btnQuitar" runat="server" Text="Quitar" Font-Names="Rockwell" Font-Size="XX-Small" Width="51px"></asp:Button></TD>
							</TR>
						</TABLE>
					</P>
				</DIV>
				<P align="left">
					<asp:label id="lblTit2" runat="server" Font-Names="Rockwell" Font-Size="Smaller" ForeColor="DimGray" Font-Bold="True" Width="124px">Datos del Vehículo:</asp:label></P>
				<DIV id="DIV2" style="WIDTH: 696px; HEIGHT: 120px" align="justify" runat="server" ms_positioning="FlowLayout">
					<P align="left">
						<TABLE id="Table4" style="WIDTH: 690px; HEIGHT: 55px" cellSpacing="1" cellPadding="1" width="690" border="0" runat="server">
							<TR>
								<TD style="WIDTH: 100px; HEIGHT: 26px">
									<asp:label id="lblSerialC" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Width="102px">Serial Carroceria:</asp:label></TD>
								<TD style="HEIGHT: 26px">
									<asp:textbox id="txtSerialC" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Width="210px"></asp:textbox></TD>
								<TD style="HEIGHT: 26px">
									<asp:label id="lblSerialM" runat="server" Font-Names="Rockwell" Font-Size="Smaller">Serial Motor:</asp:label></TD>
								<TD style="HEIGHT: 26px">
									<asp:textbox id="txtSerialM" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Width="210px"></asp:textbox></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100px; HEIGHT: 26px">
									<asp:label id="lblColor" runat="server" Font-Names="Rockwell" Font-Size="Smaller">Color:</asp:label></TD>
								<TD style="HEIGHT: 26px">
									<asp:textbox id="txtColor" runat="server" Font-Names="Rockwell" Font-Size="Smaller" Width="210px" MaxLength="50"></asp:textbox></TD>
								<TD style="HEIGHT: 26px">
									<asp:Button id="Button2" runat="server" Text="Agregar" Font-Names="Rockwell" Font-Size="XX-Small" Width="51px"></asp:Button></TD>
								<TD style="HEIGHT: 26px"></TD>
							</TR>
						</TABLE>
					</P>
					<TABLE id="Table5" style="WIDTH: 579px; HEIGHT: 154px" cellSpacing="0" cellPadding="0" width="579" align="center" border="0">
						<TR>
							<TD style="WIDTH: 48px"></TD>
							<TD style="WIDTH: 27px">
								<asp:ListBox id="listSerialC" runat="server" AutoPostBack="True" Width="150px" Height="135px"></asp:ListBox></TD>
							<TD style="WIDTH: 115px">
								<asp:ListBox id="listSerialM" runat="server" AutoPostBack="True" Width="150px" Height="135px"></asp:ListBox></TD>
							<TD style="WIDTH: 1px"></TD>
							<TD style="WIDTH: 119px">
								<asp:ListBox id="listColor" runat="server" AutoPostBack="True" Width="150px" Height="135px"></asp:ListBox></TD>
							<TD>
								<P>&nbsp;</P>
								<P>&nbsp;</P>
								<P>&nbsp;</P>
								<P>
									<asp:Button id="Button3" runat="server" Text="Quitar" Font-Names="Rockwell" Font-Size="XX-Small" Width="51px"></asp:Button></P>
							</TD>
						</TR>
						<TR>
							<TD style="WIDTH: 48px; HEIGHT: 13px"></TD>
							<TD style="WIDTH: 27px; HEIGHT: 13px"></TD>
							<TD style="WIDTH: 115px; HEIGHT: 13px"></TD>
						</TR>
					</TABLE>
				</DIV>
				<P align="center">
					<asp:button id="Button4" runat="server" Text="Guardar Nuevo" Font-Names="Rockwell" Font-Size="Smaller" Width="144px"></asp:button>
					<asp:Button id="Button7" runat="server" Text="Guardar Cambios"></asp:Button>
					<asp:Button id="Button6" runat="server" Text="Eliminar"></asp:Button></P>
			</DIV>
		</form>
	</body>
</HTML>
