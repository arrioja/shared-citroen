<%@ Register TagPrefix="rb" Namespace="Rainbow.UI.WebControls" Assembly="Rainbow" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Control language="c#" Inherits="Rainbow.DesktopModules.RoleAssignment" CodeBehind="RoleAssignment.ascx.cs" AutoEventWireup="false" %>
<p>
	<tra:literal id="title" runat="server" Text="Welcome to role administration. Please use this tool to select a single user or multiple users and either add them to a role or remove them." TextKey="TI_ROLEADMINISTRATIONTITLE"></tra:literal>
</p>
<p>
	<asp:dropdownlist id="MemberType" runat="server" AutoPostBack="True">
		<asp:ListItem Value="0" Selected="True">Users Not In Role</asp:ListItem>
		<asp:ListItem Value="1">Users In Role</asp:ListItem>
	</asp:dropdownlist>
	<asp:dropdownlist id="DataTypeSelection" runat="server">
		<asp:ListItem Value="Email" Selected="True">Return User Email Addresses</asp:ListItem>
		<asp:ListItem Value="Name">Return User Names</asp:ListItem>
	</asp:dropdownlist><asp:dropdownlist id="RoleSorter" runat="server" AutoPostBack="True">
	</asp:dropdownlist>
</p>
<P>
	<tra:linkbutton id="CriteriaSelection" runat="server" Text="Select Sorting Criteria" cssclass="CommandButton" TextKey="TI_ROLEADMINISTRATIONADDTOROLE"></tra:linkbutton>&nbsp;</P>
<P>
	<tra:literal id="RoleSelectionAddLabel" runat="server" Text="Selected User(s) Will Be Added To:" Visible="False" TextKey="TI_ROLEADMINISTRATIONSELECTIONADDLABEL"></tra:literal>
	&nbsp;<tra:literal id="RoleSelectionRemoveLabel" runat="server" Text="Selected User(s) Will Be Removed From:" Visible="False" TextKey="TI_ROLEADMINISTRATIONSELECTIONREMOVELABEL"></tra:literal>&nbsp; <BR><BR>
	&nbsp;<STRONG><tra:literal id="RoleName" runat="server" Visible="False" TextKey="TI_ROLEADMINISTRATIONSELECTIONROLENAME" Text="Not In Any Role - Please select a role from the drop-down list above"></tra:literal></STRONG> </P>
<div id="warning" class="Error">&nbsp;<tra:literal id="Warning" TextKey="TI_ROLEADMINISTRATIONSELECTIONWARNING" Text="Please Select One or More Users Before Submitting" runat="server" Visible="False"></tra:literal></div><br>
<tra:linkbutton id="AddToRole" runat="server" Text="Add Selected User(s) To Role Selected" Visible="False" cssclass="CommandButton" TextKey="TI_ROLEADMINISTRATIONADDTOROLE">Add Selected User(s) To Role Selected</tra:linkbutton>
<tra:linkbutton id="RemoveFromRole" runat="server" Text="Remove Selected User(s) From Role Selected" Visible="False" cssclass="CommandButton" TextKey="TI_ROLEADMINISTRATIONREMOVEFROMROLE">Remove Selected User(s) From Role Selected</tra:linkbutton>
<P></P>
<p>&nbsp;<asp:listbox id="UserList" runat="server" Rows="15" SelectionMode="Multiple" Visible="False"></asp:listbox></p>
