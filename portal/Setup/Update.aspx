<%@ Page language="c#" Codebehind="Update.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.Setup.Update" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Setup</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="msdefault.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" marginwidth="0" marginheight="0">
		<form id="Setup" method="post" runat="server">
			<TABLE id="Table1" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<td align="right" bgColor="#101010" height="92"><IMG height="110" src="logo_big.gif" width="300"></td>
				</TR>
				<TR>
					<td vAlign="middle" align="center" bgColor="#bababa"><asp:panel id="InfoPanel" runat="server" Visible="true">
							<TABLE class="Normal" id="infopaneltable" cellSpacing="1" cellPadding="1" width="450" border="0">
								<TR>
									<TD><B><U>Portal Setup/Update</U></B>
										<P>
										<P>Welcome to the portal update page. This page is shown automatically&nbsp;at 
											first setup and when an update to the database is needed.<BR>
											You can protect unauthorized access to this page by setting user name and 
											password in /Setup/web.config.
										</P>
									</TD>
								</TR>
							</TABLE>
						</asp:panel><asp:panel id="AuthenticationPanel" runat="server" Visible="false">
							<TABLE class="Normal" id="authenticatetable" cellSpacing="1" cellPadding="1" width="450"
								border="0">
								<TR>
									<TD><B><U>Portal Setup/Update Authentication</U></B>
										<P>
										<P>Welcome to the portal update page.This page is shown automatically at first 
											setup and when an update to the database is needed.<BR>
											Please enter your special "Portal Update" username and password.&nbsp;<BR>
											Password is case sensitive so "Secret" and "SecrET" are different.<BR>
											If you see this message your system administrator has decided to protect the 
											update process, please ask him/her for more details.<BR>
											You'll then be able to see if there are any updates available and apply 
											them.&nbsp;<BR>
											&nbsp;&nbsp;
										</P>
										<P>
											<asp:Label id="loginError" runat="server" Visible="False" CssClass="Error">I'm sorry but the username / password you entered was not correct. Please try again or contact the portal administrator.</asp:Label></P>
										<P><B>Username:</B>&nbsp;
											<asp:TextBox id="updateUsername" runat="server"></asp:TextBox><BR>
											<B>Password: </B>&nbsp;
											<asp:TextBox id="updatePassword" runat="server" TextMode="Password"></asp:TextBox></P>
										<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											<asp:Button id="authenticateUser" runat="server" Text="Login" CausesValidation="False"></asp:Button></P>
									</TD>
								</TR>
							</TABLE>
						</asp:panel><asp:panel id="UpdatePanel" runat="server" Visible="False">
							<TABLE class="Normal" id="Table2" cellSpacing="1" cellPadding="1" width="450" border="0">
								<TR id="dbNoUpdate" runat="server">
									<TD>
										<P>Your database is up-to-date.</P>
										<P align="right">
											<asp:Button id="Button1" runat="server" Text="Finish" Width="75px"></asp:Button></P>
									</TD>
								</TR>
								<TR id="dbNeedsUpdate" runat="server">
									<TD>
										<P>Rainbow has detected that Database Version does not match Code Version.
										</P>
										<P align="center">
											<asp:Label id="lblVersion" runat="server" Font-Bold="True" CssClass="Normal"></asp:Label></P>
										<P>Update will now apply these patches to your database:</P>
										<P align="center"></P>
										<DIV style="OVERFLOW: auto; HEIGHT: 200px">
											<asp:DataList id="dlScripts" runat="server">
												<ItemTemplate>
													<asp:Label id=Label2 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem") %>' CssClass="Normal" Font-Bold="True">
													</asp:Label>
												</ItemTemplate>
											</asp:DataList></DIV>
										&nbsp;
										<P></P>
										<P align="left">Please BACKUP your data before proceeding.</P>
										<P align="right">
											<asp:Button id="UpdateDatabaseCommand" runat="server" Text="Next >" Width="75px"></asp:Button></P>
									</TD>
								</TR>
								<TR id="Status" runat="server" visible="false">
									<TD>
										<P>
											<asp:Label id="lblStatus" runat="server">Database successfully updated!</asp:Label>&nbsp;</P>
										<P align="right">
											<asp:Button id="FinishButton" runat="server" Text="Finish" Width="75px"></asp:Button></P>
									</TD>
								</TR>
								<TR id="dbUpdateResult" runat="server" visible="false">
									<TD>
										<P>
											<asp:Label id="lblError" runat="server" CssClass="Error"></asp:Label></P>
										<DIV style="OVERFLOW: auto; HEIGHT: 200px">
											<P>
												<asp:DataList id="dlErrors" runat="server">
													<ItemTemplate>
														<asp:Label id="ErrorLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem") %>' Font-Bold="True" Font-Size="8">
														</asp:Label>
													</ItemTemplate>
												</asp:DataList></P>
											<P>
												<asp:DataList id="dlMessages" runat="server">
													<ItemTemplate>
														<asp:Label id="MessageLabel" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem") %>' Font-Bold="True" Font-Size="8">
														</asp:Label>
													</ItemTemplate>
												</asp:DataList></P>
										</DIV>
									</TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</TR>
				<tr>
					<td style="BACKGROUND: #101010" height="10">
						<TABLE cellSpacing="5" cellPadding="1" width="100%" border="0">
							<TR>
								<td class="Normal" style="COLOR: white">&nbsp;<STRONG>Rainbow Portal - (c) 2004, 2005</STRONG></td>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
