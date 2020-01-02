<%@ Page language="c#" Codebehind="BlacklistEdit.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.Admin.BlacklistEdit" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form runat="server">
			<div class="rb_AlternateLayoutDiv">
				<table class="rb_AlternateLayoutTable">
					<tr valign="top">
						<td class="rb_AlternatePortalHeader" valign="top">
							<portal:Banner id="SiteHeader" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							<br>
							<table width="98%" cellspacing="0" cellpadding="4" border="0">
								<TBODY>
									<tr valign="top">
										<td width="50">&nbsp;
										</td>
										<td width="*">
											<table cellSpacing="0" cellPadding="0" width="100%">
												<tr>
													<td class="Head" align="left"><tra:Literal TextKey="BLACKLIST" Text="Blacklist" runat="server" id="Literal1" /></td>
												</tr>
												<tr>
													<td colSpan="2">
														<hr noshade size="1">
													</td>
												</tr>
											</table>
											<asp:Repeater id="repListItem" runat="server">
												<HeaderTemplate>
													<table width="100%">
														<tr>
															<td colspan="5"></td>
															<td>
																<input type="button" value=<%= Esperantus.Localize.GetString("BLACKLIST_ALL") %> onclick="AllCheckboxCheck(0,true);">&#160; 
																<input type="button" value=<%= Esperantus.Localize.GetString("BLACKLIST_NONE") %> onclick="AllCheckboxCheck(0,false);">
															</td>
														</tr>
														<tr>
															<th align="left">
																<tra:Literal TextKey="BLACKLIST_NAME" Text="Name" runat="server" /></th>
															<th align="left">
																<tra:Literal TextKey="BLACKLIST_EMAIL" runat="server" /></th>
															<th align="left">
																<tra:Literal TextKey="BLACKLIST_SENDNEWSLETTER" Text="Send Newsletter" runat="server" /></th>
															<th align="left">
																<tra:Literal TextKey="BLACKLIST_DATE" runat="server" /></th>
															<th align="left">
																<tra:Literal TextKey="BLACKLIST_REASON" runat="server" /></th>
															<th align="left">
																<tra:Literal TextKey="BLACKLISTED" runat="server" /></th>
														</tr>
												</HeaderTemplate>
												<ItemTemplate>
													<tr>
														<td><%# DataBinder.Eval(Container.DataItem, "Name") %>
															<asp:label runat="server" id="lblEMail" visible="False" text='<%# DataBinder.Eval(Container.DataItem, "EMail") %>'>
															</asp:label>
														</td>
														<td><%# DataBinder.Eval(Container.DataItem, "Email") %></td>
														<td><%# DataBinder.Eval(Container.DataItem, "SendNewsletter") %></td>
														<td><%# GetDate(Container.DataItem) %></td>
														<td><%# DataBinder.Eval(Container.DataItem, "Reason") %></td>
														<td><asp:checkbox runat="server" id="chkSelect" checked='<%# GetBlacklisted(Container.DataItem) %>' enableviewstate="False"></asp:checkbox></td>
													</tr>
												</ItemTemplate>
												<FooterTemplate>
							</table>
							</FooterTemplate> </asp:Repeater>
							<p>
								<asp:LinkButton id="updateButton" Text="Update" runat="server" Cssclass="CommandButton" />
								&nbsp;
								<asp:LinkButton id="cancelButton" Text="Cancel" CausesValidation="False" runat="server" Cssclass="CommandButton" />
							</p>
						</td>
					</tr>
				</table>
				</TD></TR>
				<tr>
					<td class="rb_AlternatePortalFooter"><foot:Footer id="Footer" runat="server"></foot:Footer></td>
				</tr>
				</TBODY></TABLE></div>
		</form>
	</body>
</html>
