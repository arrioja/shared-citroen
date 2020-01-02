<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Control language="c#" Inherits="Rainbow.DesktopModules.SendNewsletter" CodeBehind="Newsletter.ascx.cs" AutoEventWireup="false" targetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table width="100%">
	<tr>
		<td colspan="5"><asp:label id="lblMessage" runat="server" CssClass="Normal" /></td>
	</tr>
	<tr>
		<td>
	<asp:panel id="UsersPanel" Visible="true" runat="server" HorizontalAlign="Center">
				<asp:DataList id="DataList1" runat="server" HorizontalAlign="center" cssclass="Normal" BorderColor="black"
					CellPadding="7" Font-Name="Verdana" Font-Size="8pt" HeaderStyle-BackColor="silver" AlternatingItemStyle-BackColor="Gainsboro">
			<HeaderTemplate>
				<%= Titulo %>
			</HeaderTemplate>
			<ItemTemplate>
				<%# DataBinder.Eval(Container.DataItem,"StringValue") %>
			</ItemTemplate>
		</asp:DataList>
	</asp:panel>
		</td>
	</tr>
	<tr>
		<td>
			<asp:panel id="EditPanel" Visible="true" runat="server" HorizontalAlign="Center">
	<TABLE cellSpacing="0" cellPadding="4" width="600" border="0">
			<TR vAlign="top">
				<TD class="SubHead" noWrap width="200">
					<tra:Literal id="Literal1" runat="server" Text="Sender email address" TextKey="NEWSLETTER_SENDER_EMAIL_ADDRESS"></tra:Literal>:
				</TD>
				<TD width="*">
							<asp:TextBox id="txtEMail" runat="server" cssclass="NormalTextBox" maxlength="100" columns="40"
								width="450"></asp:TextBox>
							<tra:RegularExpressionValidator id="validEmailRegExp" runat="server" cssclass="SubHead" Display="Dynamic" errormessage="Please enter a valid email address."
								TextKEY="ERROR_VALID_EMAIL" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
								ControlToValidate="txtEMail"></tra:RegularExpressionValidator></TD>
			</TR>
			<TR vAlign="top">
				<TD class="SubHead" noWrap width="200">
					<tra:Literal id="Literal2" runat="server" Text="Your Name (optional)" TextKey="NEWSLETTER_SENDER_OPTIONAL_NAME"></tra:Literal>:
				</TD>
				<TD width="*">
							<asp:TextBox id="txtName" runat="server" cssclass="NormalTextBox" maxlength="100" columns="40"
								width="450"></asp:TextBox></TD>
			</TR>
			<TR vAlign="top">
				<TD class="SubHead" noWrap>
							<tra:Literal id="Literal3" runat="server" Text="Subject" TextKey="NEWSLETTER_SUBJECT"></tra:Literal>:
						</TD>
				<TD width="*">
							<asp:TextBox id="txtSubject" runat="server" cssclass="NormalTextBox" maxlength="100" columns="40"
								width="450"></asp:TextBox></TD>
			</TR>
			<TR vAlign="top">
				<TD class="SubHead" noWrap>
					<tra:Literal id="Literal4" runat="server" Text="Message Body" TextKey="NEWSLETTER_MESSAGE_BODY"></tra:Literal>:
				</TD>
				<TD width="*">
							<asp:TextBox id="txtBody" CssClass="NormalTextBox" runat="server" columns="59" width="450" Rows="15"
								TextMode="Multiline"></asp:TextBox>
							<tra:RequiredFieldValidator id="validEmailRequired" runat="server" cssclass="SubHead" TextKey="ERROR_VALID_MESSAGE_TEXT"
								errormessage="Please enter message text." ControlToValidate="txtBody" display="Dynamic"></tra:RequiredFieldValidator></TD>
			</TR>
			<TR>
				<TD noWrap></TD>
				<TD>
					<tra:CheckBox id="HtmlMode" runat="server" cssclass="Normal" Text="HtmlMode" TextKey="NEWSLETTER_HTML_MODE"></tra:CheckBox>&nbsp;
							<tra:CheckBox id="InsertBreakLines" runat="server" cssclass="Normal" Text="Insert break lines"
								TextKey="NEWSLETTER_INSERT_BREAK_LINES"></tra:CheckBox>&nbsp;
				</TD>
			</TR>
			<TR>
				<TD noWrap></TD>
				<TD class="Normal">
					{NAME} =
							<tra:Literal id="Literal5" runat="server" Text="UserName" TextKey="NEWSLETTER_USERNAME"></tra:Literal><BR>
					{PASSWORD} =
							<tra:Literal id="Literal6" runat="server" Text="Password" TextKey="NEWSLETTER_PASSWORD"></tra:Literal><BR>
					{EMAIL} =
							<tra:Literal id="Literal7" runat="server" Text="UserEmail" TextKey="NEWSLETTER_USEREMAIL"></tra:Literal><BR>
					{LOGINURL} =
							<tra:Literal id="Literal8" runat="server" Text="A direct url that can be used to logon automatically"
								TextKey="NEWSLETTER_DIRECTURL"></tra:Literal></TD>
			</TR>
			<TR vAlign="top">
						<TD noWrap>&nbsp;</TD>
				<TD>
							<tra:LinkButton id="previewButton" runat="server" cssclass="CommandButton" Text="Preview" TextKey="PREVIEW"
								EnableViewState="False"></tra:LinkButton>&nbsp;
							<tra:LinkButton id="cancelButton" runat="server" cssclass="CommandButton" Text="Cancel" TextKey="CANCEL"
								EnableViewState="False" CausesValidation="False"></tra:LinkButton></TD>
			</TR>
	</TABLE>
			</asp:panel>
		</td>
	</tr>
	<tr>
		<td>
			<asp:panel id="PrewiewPanel" runat="server" Visible="true" HorizontalAlign="Center">
	<TABLE cellSpacing="0" cellPadding="4" width="600" border="0">
		<TR vAlign="top">
			<TD class="Normal">
				<tra:Label id="Label1" runat="server" cssclass="SubHead" Text="From" TextKey="FROM"></tra:Label>:
				<asp:Label id="lblFrom" runat="server"></asp:Label></TD>
		</TR>
		<TR vAlign="top">
			<TD class="Normal">
				<tra:Label id="Label2" runat="server" cssclass="SubHead" Text="to" TextKey="TO"></tra:Label>:
				<asp:Label id="lblTo" runat="server"></asp:Label></TD>
		</TR>
		<TR vAlign="top">
			<TD class="Normal">
				<tra:Label id="Label3" runat="server" cssclass="SubHead" Text="Subject" TextKey="SUBJECT"></tra:Label>
				<asp:Label id="lblSubject" runat="server"></asp:Label></TD>
		</TR>
		<TR vAlign="top">
			<TD class="Normal">
				<asp:Label id="lblBody" runat="server"></asp:Label></TD>
		</TR>
		<TR vAlign="top">
			<TD class="Normal">
							<tra:LinkButton id="submitButton" runat="server" cssclass="CommandButton" Text="Submit" TextKey="SUBMIT"
								EnableViewState="False"></tra:LinkButton>&nbsp;
							<tra:LinkButton id="cancelButton2" runat="server" cssclass="CommandButton" Text="Cancel" TextKey="CANCEL"
								EnableViewState="False" CausesValidation="False"></tra:LinkButton></TD>
		</TR>
	</TABLE>
			</asp:panel>
		</td>
	</tr>
	<tr>
		<td>
			<table width="98%" cellspacing="0" cellpadding="4" border="0">
	<tr>
		<td valign="top" align="left">
			<asp:Label cssclass="Normal" id="CreatedDate" runat="server" />
		</td>
	</tr>
			</table>
		</td>
	</tr>
</table>
