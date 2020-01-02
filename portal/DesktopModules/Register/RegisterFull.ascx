<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Control language="c#" Inherits="Rainbow.DesktopModules.RegisterFull" CodeBehind="RegisterFull.ascx.cs" AutoEventWireup="false" %>
<asp:Panel Runat="server" ID="FullProfileInformation">
	<TABLE class="Normal" cellSpacing="4" cellPadding="0" width="100%" border="0">
		<TR>
			<TD class="Head" align="left" colSpan="3">
				<tra:Label id="PageTitleLabel" runat="server" Text="Profile Information" TextKey="PROFILE_INFO"></tra:Label>
				<HR noShade SIZE="1">
			</TD>
		</TR>
		<tr>
		<td class="Normal" colspan="3">
			<asp:label id="Message" runat="server" cssclass="NormalRed" forecolor="Red"></asp:label></td>
		</tr>
		<TR>
			<TD class="Subhead" noWrap>
				<tra:Label id="NameLabel" runat="server" Text="Name" TextKey="NAME" Width="83px" Height="22"></tra:Label></TD>
			<TD class="Normal">&nbsp;
				<asp:TextBox id="NameField" runat="server" Width="350px" size="25"></asp:TextBox></TD>
			<TD class="Normal">
				<tra:RequiredFieldValidator id="RequiredName" runat="server" Text="'Name' must not be left blank" TextKey="INSERT_NAME"
					Display="Dynamic" ControlToValidate="NameField" ErrorMessage="'Name' must not be left blank">'Name' must not be left blank</tra:RequiredFieldValidator></TD>
		</TR>
		<TR>
			<TD class="Subhead" noWrap>
				<tra:Label id="CompanyLabel" runat="server" Text="Company" TextKey="COMPANY" EnableViewState="False"></tra:Label></TD>
			<TD class="Normal">&nbsp;
				<asp:TextBox id="CompanyField" runat="server" cssclass="NormalTextBox" width="350px" Columns="28"
					maxlength="50"></asp:TextBox></TD>
			<TD></TD>
		</TR>
		<TR>
			<TD class="Subhead" noWrap>
				<tra:Label id="AddressLabel" runat="server" Text="Address" TextKey="ADDRESS" EnableViewState="False"></tra:Label></TD>
			<TD class="Normal">&nbsp;
				<asp:TextBox id="AddressField" runat="server" cssclass="NormalTextBox" width="350px" Columns="28"
					maxlength="50"></asp:TextBox></TD>
			<TD>&nbsp;</TD>
		</TR>
		<TR>
			<TD class="Subhead" noWrap>
				<tra:Label id="CityLabel" runat="server" Text="City" TextKey="CITY" EnableViewState="False"></tra:Label></TD>
			<TD class="Normal">&nbsp;
				<asp:TextBox id="CityField" runat="server" cssclass="NormalTextBox" width="350px" Columns="28"
					maxlength="50"></asp:TextBox></TD>
			<TD>&nbsp;</TD>
		</TR>
		<TR>
			<TD class="Subhead" noWrap>
				<tra:Label id="ZipLabel" runat="server" Text="Postal Code/Zip" TextKey="ZIP" EnableViewState="False"></tra:Label></TD>
			<TD class="Normal">&nbsp;
				<asp:TextBox id="ZipField" runat="server" cssclass="NormalTextBox" width="60px" Columns="28"
					maxlength="10"></asp:TextBox></TD>
			<TD>&nbsp;</TD>
		</TR>
		<TR>
			<TD class="Subhead" noWrap>
				<tra:Label id="CountryLabel" runat="server" Text="Country" TextKey="COUNTRY"></tra:Label></TD>
			<TD class="Normal">&nbsp;
				<asp:DropDownList id="CountryField" runat="server" Width="350px" AutoPostBack="True" DataValueField="Name"
					DataTextField="DisplayName"></asp:DropDownList></TD>
			<TD>&nbsp;</TD>
		</TR>
		<TR id="StateRow" runat="server">
			<TD class="Subhead" noWrap>
				<tra:Label id="StateLabel" runat="server" Text="Province/State" TextKey="PROV_STATE"></tra:Label></TD>
			<TD class="Normal">&nbsp;
				<asp:DropDownList id="StateField" runat="server" Width="170px" DataValueField="CountryCode" DataTextField="DisplayName"></asp:DropDownList>
				<tra:Label id="InLabel" runat="server" Text="in" TextKey="IN"></tra:Label>&nbsp;
				<asp:Label id="ThisCountryLabel" runat="server" Font-Italic="True" Font-Bold="True"></asp:Label></TD>
			<TD>&nbsp;</TD>
		</TR>
		<TR>
			<TD class="Subhead" noWrap>
				<tra:Label id="PhoneLabel" runat="server" Text="Telephone" TextKey="PHONE" EnableViewState="False"></tra:Label></TD>
			<TD class="Normal">&nbsp;
				<asp:TextBox id="PhoneField" runat="server" cssclass="NormalTextBox" width="350px" Columns="28"
					maxlength="50"></asp:TextBox></TD>
			<TD>&nbsp;</TD>
		</TR>
		<TR>
			<TD class="Subhead" noWrap>
				<tra:Label id="FaxLabel" runat="server" Text="Fax" TextKey="FAX" EnableViewState="False"></tra:Label></TD>
			<TD class="Normal">&nbsp;
				<asp:TextBox id="FaxField" runat="server" cssclass="NormalTextBox" width="350px" Columns="28"
					maxlength="50"></asp:TextBox></TD>
			<TD>&nbsp;</TD>
		</TR>
		<TR id="PIvaRow" runat="server">
			<TD class="Subhead" noWrap>
				<tra:Label id="PIvaLabel" runat="server" Text="Company Number" TextKey="PIVA" EnableViewState="False"></tra:Label></TD>
			<TD class="Normal">&nbsp;
				<asp:TextBox id="PIvaField" runat="server" cssclass="NormalTextBox" width="200px" Columns="28"
					maxlength="11"></asp:TextBox></TD>
			<TD>&nbsp;</TD>
		</TR>
		<TR id="CFiscaleRow" runat="server">
			<TD class="Subhead" noWrap>
				<tra:Label id="CFiscaleLabel" runat="server" Text="Fiscal Code" TextKey="CFISCALE" EnableViewState="False"></tra:Label></TD>
			<TD class="Normal">&nbsp;
				<asp:TextBox id="CFiscaleField" runat="server" cssclass="NormalTextBox" width="200px" Columns="28"
					maxlength="16"></asp:TextBox></TD>
			<TD>&nbsp;</TD>
		</TR>
		<TR>
			<TD class="Subhead" noWrap>
				<tra:Label id="SendNewsletterLabel" runat="server" Text="Send Newsletter" TextKey="SEND_NEWSLETTER"
					EnableViewState="False"></tra:Label></TD>
			<TD class="Normal">&nbsp;
				<asp:CheckBox id="SendNewsletter" runat="server"></asp:CheckBox></TD>
			<TD>&nbsp;</TD>
		</TR>
	</TABLE>
</asp:Panel>
<table class="Normal" cellspacing="4" cellpadding="0" width="100%" border="0">
	<tr>
		<td class="Head" colspan="3">
			<br>
			<tra:Label id="AccountLabel" TextKey="ACCOUNT_INFO" Text="Account Information" runat="server"></tra:Label>
			<hr noshade size="1">
		</td>
	</tr>
	<TR id="UserIDRow" runat="server" visible="false">
		<TD class="Subhead" noWrap>
			<tra:Label id="UseridLabel" runat="server" Text="Name" TextKey="USERID" Width="83px" Height="22">UserID</tra:Label></TD>
		<TD class="Normal">&nbsp;
			<asp:TextBox id="UseridField" runat="server" Width="350px" size="25">0</asp:TextBox></TD>
		<TD class="Normal">
			<tra:CompareValidator id="CheckID" runat="server" TextKey="ERROR_VALID_ID" Operator="DataTypeCheck" Type="Integer"
				Display="Dynamic" ControlToValidate="UseridField" ErrorMessage="ID must be an integer"></tra:CompareValidator></TD>
	</TR>
	<tr>
		<td class="Subhead" nowrap>
			<tra:Label id="EmailLabel" TextKey="EMAIL" Text="Email Address" runat="server"></tra:Label></td>
		<td class="Normal">&nbsp;
			<asp:TextBox id="EmailField" runat="server" Width="350px" size="25"></asp:TextBox></td>
		<td class="Normal">
			<tra:RegularExpressionValidator id="ValidEmail" runat="server" ControlToValidate="EmailField" ErrorMessage="You must use a valid email address"
				ValidationExpression="[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+" Display="Dynamic" TextKey="VALID_EMAIL"></tra:RegularExpressionValidator>
			<tra:RequiredFieldValidator id="RequiredEmail" TextKey="INSERT_EMAIL" Text="'Email' must not be left blank"
				runat="server" ControlToValidate="EmailField" ErrorMessage="'Email' must not be left blank" Display="Dynamic">'Email' must not be left blank</tra:RequiredFieldValidator></td>
	</tr>
	<tr id="EditPasswordRow" runat="server" visible="false">
		<td class="Subhead" nowrap></td>
		<td class="Normal">&nbsp;
			<tra:Label id="Label1" TextKey="CHANGE_PASSWORD" Text="Password" runat="server">Change password only if you want modify it:</tra:Label></td>
		<td class="Normal"></td>
	</tr>
	<tr>
		<td class="Subhead" nowrap>
			<tra:Label id="PasswordLabel" TextKey="PASSWORD" Text="Password" runat="server"></tra:Label></td>
		<td class="Normal">&nbsp;
			<asp:TextBox id="PasswordField" runat="server" Width="350px" size="25" TextMode="Password"></asp:TextBox></td>
		<td class="Normal">
			<tra:RequiredFieldValidator id="RequiredPassword" TextKey="INSERT_PASSWORD" Text="'Password' must not be left blank"
				runat="server" ControlToValidate="PasswordField" ErrorMessage="'Password' must not be left blank"></tra:RequiredFieldValidator></td>
	</tr>
	<tr>
		<td class="Subhead" nowrap>
			<tra:Label id="ConfirmPasswordLabel" TextKey="CONFIRM" Text="Confirm Password" runat="server"></tra:Label></td>
		<td class="Normal">&nbsp;
			<asp:TextBox id="ConfirmPasswordField" runat="server" Width="350px" size="25" TextMode="Password"></asp:TextBox></td>
		<td class="Normal">
			<tra:RequiredFieldValidator id="RequiredConfirm" TextKey="INSERT_CONFIRM" runat="server" ControlToValidate="ConfirmPasswordField"
				Display="Dynamic" ErrorMessage="'Confirm' must not be left blank"></tra:RequiredFieldValidator>
			<tra:CompareValidator id="ComparePasswords" runat="server" TextKey="PASSWORD_DO_NOT_MATCH" Display="Dynamic"
				ErrorMessage="Password fields do not match." ControlToValidate="ConfirmPasswordField" ControlToCompare="PasswordField"></tra:CompareValidator>
		</td>
	</tr>
	<tr id="ConditionsRow" runat="server">
		<td class="Subhead" nowrap valign="top">
			<tra:Label id="ConditionsLabel" runat="server" Text="Terms of Service" TextKey="CONDITIONS"></tra:Label>&nbsp;&nbsp;<BR>
			&nbsp;</td>
		<td class="Normal">&nbsp;
			<asp:TextBox id="FieldConditions" onfocus="this.blur()" runat="server" Width="350px" size="25"
				EnableViewState="False" Columns="40" TextMode="MultiLine" ReadOnly="True" Rows="8" Cssclass="Normal"></asp:TextBox><BR>
			&nbsp;
			<tra:CheckBox id="Accept" runat="server" Text="I Accept Terms and Conditions" TextKey="ACCEPT_TERMS"></tra:CheckBox></td>
		<td vAlign="top">
			<tra:CustomValidator id="CheckTermsValidator" runat="server" ErrorMessage="Must accept terms before proceed"
				TextKey="ACCEPT_TERMS"></tra:CustomValidator></td>
	</tr>
	<tr>
		<td class="Normal" colspan="3">
			<tra:LinkButton class="CommandButton" id="RegisterBtn" TextKey="REGISTER_NOW" Text="Register and Sign In Now"
				runat="server"></tra:LinkButton>&nbsp;
			<tra:LinkButton class="CommandButton" id="SaveChangesBtn" runat="server" Text="Register and Sign In Now"
				TextKey="SAVE_CHANGES">Save Changes</tra:LinkButton>&nbsp;
			<tra:LinkButton class="CommandButton" id="cancelButton" TextKey="CANCEL" runat="server" CausesValidation="False"></tra:LinkButton></td>
	</tr>
</table>
