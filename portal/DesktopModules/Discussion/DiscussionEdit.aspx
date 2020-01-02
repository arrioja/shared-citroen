<%@ Page language="c#" Inherits="Rainbow.DesktopModules.DiscussionEdit" CodeBehind="DiscussionEdit.aspx.cs" AutoEventWireup="false" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form name="form1" runat="server">
			<div class="rb_DefaultLayoutDiv">
				<table class="rb_DefaultLayoutTable">
					<%-- <table cellSpacing="0" cellPadding="0" width="100%" border="0"> --%>
					<tr vAlign="top">
						<td colSpan="2" class="rb_DefaultPortalHeader" valign="top">
							<portal:banner id="Banner1" runat="server" ShowTabs="false"></portal:banner>
						</td>
					</tr>
					<tr vAlign="top">
						<td width="10%">&nbsp;
						</td>
						<td>
							<br>
							<table cellSpacing="0" cellPadding="0" width="600">
								<tr>
									<td align="left">
										<tra:Label class="Head" TextKey="DS_NEWTHREAD" Text="Add a new thread" id="DiscussionEditInstructions" runat="Server" />
										<%-- <asp:Label class="Head" TextKey="DS_NEWTHREAD" Text="Add a new thread" id="DiscussionEditInstructions" runat="Server" /> --%>
									</td>
									<td align="right">
									</td>
								</tr>
								<tr>
									<td colSpan="2">
										<P>
											<hr noshade size="1">
										<P></P>
										<P>&nbsp;</P>
									</td>
								</tr>
							</table>
							<asp:panel id="EditPanel" runat="server" Visible="true">
								<TABLE cellSpacing="0" cellPadding="4" width="600" border="0">
									<TR vAlign="top">
										<TD class="SubHead" width="150">
											<tra:Label id="title_label" runat="Server" Text="Subject" TextKey="SUBJECT"></tra:Label></TD>
										<TD rowSpan="4">&nbsp;
										</TD>
										<TD width="*">
											<asp:TextBox id="TitleField" runat="server" cssclass="NormalTextBox" width="500" columns="40" maxlength="100"></asp:TextBox><%-- translation needed --%>
											<asp:RequiredFieldValidator id="Requiredfieldvalidator1" runat="server" ErrorMessage="Please enter a summary of your reply." Display="Dynamic" ControlToValidate="TitleField"></asp:RequiredFieldValidator></TD>
									</TR>
									<TR vAlign="top">
										<TD class="SubHead">
											<tra:Label id="body_label" runat="Server" Text="Body" TextKey="DS_BODY"></tra:Label></TD>
										<TD width="*">
											<asp:TextBox id="BodyField" runat="server" width="500" columns="59" Rows="15" TextMode="Multiline" CssClass="NormalTextBox"></asp:TextBox></TD>
									</TR>
									<TR vAlign="top">
										<TD>&nbsp;
										</TD>
										<TD>
											<asp:LinkButton class="CommandButton" id="submitButton" runat="server" Text="Submit"></asp:LinkButton>&nbsp;
											<asp:LinkButton class="CommandButton" id="cancelButton" runat="server" Text="Cancel" CausesValidation="False"></asp:LinkButton>&nbsp;
										</TD>
									</TR>
									<TR vAlign="top">
										<TD class="SubHead"></TD>
										<TD>&nbsp;
										</TD>
									</TR>
								</TABLE>
							</asp:panel>
							<table cellSpacing="0" cellPadding="4" width="600" border="0">
								<ItemTemplate>
									<TBODY>
										<tr vAlign="top">
											<td align="left">
												<asp:Panel id="OriginalMessagePanel" runat="server">
													<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
														<TR>
															<TD colSpan="2">
																<tra:Label class="SubHead" id="Label1" runat="server" Text="Original Message" TextKey="DS_ORIGINALMSG"></tra:Label><BR>
																<BR>
															</TD>
														</TR>
														<TR>
															<TD>
																<tra:Label class="SubHead" id="Label2" runat="server" Text="Subject" TextKey="SUBJECT"></tra:Label></TD>
															<TD>&nbsp;
																<asp:label id="Title" runat="server"></asp:label></TD>
														</TR>
														<TR>
															<TD>
																<tra:Label class="SubHead" id="Label3" runat="server" Text="Author" TextKey="AUTHOR"></tra:Label></TD>
															<TD>&nbsp;
																<asp:label id="CreatedByUser" runat="server"></asp:label></TD>
														</TR>
														<TR>
															<TD>
																<tra:Label class="SubHead" id="Label4" runat="server" Text="Created Date" TextKey="DATE"></tra:Label></TD>
															<TD>&nbsp;
																<asp:label id="CreatedDate" runat="server"></asp:label></TD>
														</TR>
													</TABLE>
													<BR>
													<asp:label id="Body" runat="server"></asp:label>
													<P></P>
												</asp:Panel>
											</td>
										</tr>
								</ItemTemplate></table>
						</td>
					</tr>
					<tr>
						<td colspan="2" class="rb_DefaultPortalFooter"><foot:Footer id="Footer" runat="server"></foot:Footer></td>
					</tr>
					</TBODY></table>
			</div>
		</form>
	</body>
</html>
