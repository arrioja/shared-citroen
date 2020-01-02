<%@ Control language="c#" Inherits="Rainbow.DesktopModules.LDAPUserProfile" CodeBehind="LDAPUserProfile.ascx.cs" AutoEventWireup="false" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<TABLE class="Normal" id="Table1" cellSpacing="4" cellPadding="0" width="100%" border="0">
	<TR>
		<TD class="Head" align="left" colSpan="3">
			<tra:Label id="PageTitleLabel" TextKey="PROFILE_INFO" Text="Profile Information" runat="server"
				EnableViewState="False">Profile Information</tra:Label>
			<HR noShade SIZE="1">
		</TD>
	</TR>
	<tr>
		<td class="subhead" align="left" colspan="3">
			<tra:label id="ErrorMessage" textkey="PROFILE_INFO_ERROR" text="Error: Profile Information - Could not be retrieved" runat="server"
				enableviewstate="False" visible="false"></tra:label>
		</td>
	</tr>
	<TR id="UserIDRow" runat="server" visible="false">
		<TD class="subhead" noWrap width="164">
			<tra:Label id="UseridLabel" TextKey="USERID" Text="Name" runat="server" Width="83px" Height="22"
				EnableViewState="False">UserID</tra:Label></TD>
		<TD class="normal">&nbsp;
			<asp:TextBox id="UseridField" runat="server" Width="350px" size="25"></asp:TextBox></TD>
		<TD class="normal"></TD>
	</TR>
	<TR>
		<TD class="subhead" noWrap width="164">
			<tra:Label id="NameLabel" TextKey="NAME" Text="Name" runat="server" Width="83px" Height="22"
				EnableViewState="False">Name</tra:Label></TD>
		<TD class="normal">&nbsp;
			<asp:TextBox id="NameField" runat="server" Width="350px" size="25"></asp:TextBox></TD>
		<TD class="normal"></TD>
	</TR>
	<TR>
		<TD class="subhead" noWrap width="164">
			<tra:Label id="EmailLabel" TextKey="EMAIL" Text="Company" runat="server" EnableViewState="False">Email</tra:Label></TD>
		<TD class="normal">&nbsp;
			<asp:TextBox id="EmailField" runat="server" maxlength="50" Columns="28" width="350px" cssclass="NormalTextBox"></asp:TextBox></TD>
		<TD></TD>
	</TR>
	<TR>
		<TD class="subhead" noWrap width="164">
			<tra:Label id="DepartmentLabel" TextKey="DEPARTMENT" Text="Address" runat="server" EnableViewState="False">Department</tra:Label></TD>
		<TD class="normal">&nbsp;
			<asp:TextBox id="DepartmentField" runat="server" maxlength="50" Columns="28" width="350px" cssclass="NormalTextBox"></asp:TextBox></TD>
		<TD>&nbsp;</TD>
	</TR>
	<TR>
		<TD class="subhead" noWrap width="164">
			<tra:Label id="MembershipLabel" runat="server" Text="Address" TextKey="MEMBERSHIP" EnableViewState="False">Membership</tra:Label></TD>
		<TD class="normal">&nbsp;
			<asp:ListBox id="MembershipListBox" runat="server" Width="350px"></asp:ListBox></TD>
		<TD>&nbsp;</TD>
	</TR>
</TABLE>
