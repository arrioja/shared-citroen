<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Control language="c#" Inherits="Rainbow.DesktopModules.SendThoughts" CodeBehind="SendThoughts.ascx.cs" AutoEventWireup="false" %>
<table width="98%" cellspacing="0" cellpadding="4" border="0">
	<tr valign="top">
		<td align="left">
			<asp:label id="Label1" border="0" runat="server" />
			<br>
			<asp:label id="Label2" border="0" runat="server" />
			<br>
			&nbsp;
		</td>
	</tr>
</table>
<br>
<asp:panel id="EditPanel" Visible="true" runat="server">
	<DIV align="center">
		<TABLE cellSpacing="0" cellPadding="4" width="600" border="0">
			<TR vAlign="top">
				<TD class="SubHead" width="200">
					<tra:Literal id="Literal1" runat="server" Text="Your EMail Address:" TextKey="SENDTHTS_YREMAIL"></tra:Literal></TD>
				<TD width="*">
					<asp:TextBox id="txtEMail" runat="server" maxlength="100" columns="40" width="450" cssclass="NormalTextBox"></asp:TextBox>
					<DIV class="SubHead">
						<tra:RegularExpressionValidator id="validEMailRegExp" runat="server" TextKey="SENDTHTS_EMAILVALID" ValidationExpression="[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+" ErrorMessage="Please enter a valid email address." Display="Dynamic" ControlToValidate="txtEMail"></tra:RegularExpressionValidator>
						<tra:RequiredFieldValidator id="rfvEMail" runat="server" TextKey="SENDTHTS_EMAILBLANK" ErrorMessage="'EMail' must not be left blank." Display="Dynamic" ControlToValidate="txtEMail"></tra:RequiredFieldValidator></DIV>
				</TD>
			</TR>
			<TR vAlign="top">
				<TD class="SubHead" width="200">
					<tra:Literal id="Literal2" runat="server" Text="Your Name: (optional)" TextKey="SENDTHTS_YRNAME"></tra:Literal></TD>
				</TD>
				<TD width="*">
					<asp:TextBox id="txtName" runat="server" maxlength="100" columns="40" width="450" cssclass="NormalTextBox"></asp:TextBox></TD>
			</TR>
			<TR vAlign="top">
				<TD class="SubHead">
					<tra:Literal id="Literal3" runat="server" Text="Subject: (optional)" TextKey="SENDTHTS_SUBJECT"></tra:Literal></TD>
				</TD>
				<TD width="*">
					<asp:TextBox id="txtSubject" runat="server" maxlength="100" columns="40" width="450" cssclass="NormalTextBox"></asp:TextBox></TD>
			</TR>
			<TR vAlign="top">
				<TD class="SubHead">
					<tra:Literal id="Literal4" runat="server" Text="Message Body:" TextKey="SENDTHTS_BODY"></tra:Literal></TD>
				<TD width="*">
					<asp:TextBox id="txtBody" runat="server" columns="59" width="450" Rows="15" TextMode="Multiline" CssClass="NormalTextBox"></asp:TextBox>
					<DIV class="SubHead">
						<tra:RequiredFieldValidator id="rfvMessageBody" runat="server" TextKey="SENDTHTS_BODY_ERR" ControlToValidate="txtBody" display="Dynamic" errormessage="Please enter message text."></tra:RequiredFieldValidator></DIV>
				</TD>
			</TR>
			<TR vAlign="top">
				<TD>&nbsp;
				</TD>
				<TD>
					<tra:LinkButton class="CommandButton" id="SendBtn" onclick="SendBtn_Click" runat="server" Text="Send" Textkey="SENDTHTS_SEND"></tra:LinkButton>&nbsp;
					<tra:LinkButton class="CommandButton" id="ClearBtn" onclick="ClearBtn_Click" runat="server" Text="Clear" Textkey="SENDTHTS_CLEAR" CausesValidation="False"></tra:LinkButton>&nbsp;
				</TD>
			</TR>
		</TABLE>
	</DIV>
</asp:panel>
