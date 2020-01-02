<%@ Control language="c#" Inherits="Rainbow.DesktopModules.SigninCool" CodeBehind="SigninCool.ascx.cs" AutoEventWireup="false" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" align="center" border="0">
	<TR>
		<TD>
			<TABLE class="Normal" cellSpacing="1" cellPadding="3" width="150" align="center">
				<TR>
					<TD noWrap>
						<tra:Literal id="EmailLabel" runat="server" Text="Email" TextKey="EMAIL" LabelForControl="email"></tra:Literal>:
					</TD>
					<TD noWrap>
						<tra:Literal id="PasswordLabel" runat="server" Text="Password" TextKey="PASSWORD" LabelForControl="password"></tra:Literal>:</TD>
				</TR>
				<TR>
					<TD noWrap>
						<asp:TextBox id="email" runat="server" columns="24" width="130" cssclass="NormalTextBox"></asp:TextBox></TD>
					<TD noWrap>
						<asp:TextBox id="password" runat="server" columns="24" width="130" cssclass="NormalTextBox" textmode="password"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD noWrap>
						<tra:checkbox id="RememberCheckBox" runat="server" Text="Remember Login" TextKey="REMEMBER_LOGIN"></tra:checkbox></TD>
					<TD noWrap>
						<P align="right">
							<tra:Button id="LoginBtn" runat="server" Text="Sign in" TextKey="SIGNIN" CssClass="CommandButton" EnableViewState="False"></tra:Button></P>
					</TD>
				</TR>
				<TR>
					<TD noWrap>
						<tra:LinkButton id="SendPasswordBtn" runat="server" Text="Send me password" TextKey="SIGNIN_SEND_PWD" CssClass="CommandButton" EnableViewState="False"></tra:LinkButton></TD>
					<TD noWrap>
						<P align="right">
							<tra:LinkButton id="RegisterBtn" runat="server" TextKey="REGISTER" CssClass="CommandButton" EnableViewState="False">
		Register</tra:LinkButton></P>
					</TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<tra:Label id="Message" runat="server" CssClass="Error"></tra:Label></TD>
				</TR>
			</TABLE>
		</TD>
		<TD>&nbsp;</TD>
		<TD>
			<asp:PlaceHolder id="CoolTextPlaceholder" runat="server"></asp:PlaceHolder></TD>
	</TR>
</TABLE>
