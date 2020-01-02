<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Page language="c#" CodeBehind="UsersManage.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.UsersManage" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>

<HTML>
	<head runat="server" />
	<body runat="server">
		<form runat="server">
			<div class="rb_AlternateLayoutDiv">
				<table class="rb_AlternateLayoutTable">
					<tr vAlign="top">
						<td colSpan="2" class="rb_AlternatePortalHeader"><portal:banner id="Banner1" runat="server" ShowTabs="false"></portal:banner></td>
					</tr>
					<tr>
						<td>
							<table align="center" cellSpacing="0" cellPadding="4" border="0">
								<tr vAlign="top" height="*">
									<td colSpan="2">
										<table cellSpacing="0" cellPadding="0" width="100%">
											<tr>
												<td align="left">
													<span class="Head" id="title" runat="server">
														<tra:Label id="Label2" runat="server" TextKey="USER_MANAGE">Manage User</tra:Label>
													</span>
												</td>
											</tr>
											<tr>
												<td>
													<hr noshade size="1">
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<TR>
									<td class="Normal" colSpan="2">
										<!-- Start Register control -->
										<asp:PlaceHolder id="register" runat="server"></asp:PlaceHolder>
										<!-- End Register control -->
									</td>
								</TR>
								<tr>
									<td colSpan="3">
										<P>&nbsp;</P>
									</td>
								</tr>
								<tr>
									<td colSpan="2"><asp:dropdownlist id="allRoles" runat="server" DataValueField="RoleID" DataTextField="RoleName"></asp:dropdownlist>&nbsp;<tra:linkbutton TextKey="ADDUSER" id="addExisting" runat="server" cssclass="CommandButton" Text="Add user to this role"></tra:linkbutton>
									</td>
								</tr>
								<tr vAlign="top">
									<td>&nbsp;
									</td>
									<td><asp:datalist id="userRoles" runat="server" DataKeyField="RoleID" RepeatColumns="2">
											<ItemStyle Width="225" />
											<ItemTemplate>
												&#160;&#160;
												<tra:ImageButton ImageUrl='<%# CurrentTheme.GetImage("Buttons_Delete", "Delete.gif").ImageUrl %>' CommandName="delete" AlternateText='DELUSER' runat="server" ID="deleteBtn" />
												<asp:Label Text='<%# DataBinder.Eval(Container.DataItem, "RoleName") %>' cssclass="Normal" runat="server" ID="Label1" />
											</ItemTemplate>
										</asp:datalist></td>
								</tr>
								<tr>
									<td colSpan="2">
										<hr noshade size="1">
										<asp:Label id="ErrorLabel" runat="server" CssClass="Error" Visible="False"></asp:Label>
									</td>
								</tr>
								<tr>
									<td colSpan="2"><tra:linkbutton TextKey="SAVEUSER" class="CommandButton" id="saveBtn" runat="server" Text="Save User Changes"></tra:linkbutton></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td colspan="2" class="rb_AlternatePortalFooter"><foot:Footer id="Footer" runat="server"></foot:Footer></td>
					</tr>
				</table>
			</div>
		</form>
	</body>
</HTML>
