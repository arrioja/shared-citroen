<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ADGroupMember.ascx.cs" Inherits="Rainbow.Admin.ADGroupMember" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<table>
	<tr>
		<td vAlign="top">
			<tra:ImageButton id="btnShowHide" runat="server" ImageUrl='<%# CurrentTheme.GetImage("Buttons_Max", "Max.gif").ImageUrl %>'></tra:ImageButton>
		</td>
		<td>
			<asp:panel id="pnlShowHide" Runat="server" Visible="False">
				<TABLE>
					<TR>
						<td align="left">
							<asp:dropdownlist id="ddlDomain" runat="server" AutoPostBack="True"></asp:dropdownlist></td>
						<td align="right">&#160;
							<tra:LinkButton id="lnkShowMembers" runat="server" TextKey="AD_SHOW_USERS" Text="Show users"></tra:LinkButton>&#160;
							<tra:Linkbutton id="lnkRefresh" runat="server" TextKey="REFRESH" Text="Refresh"></tra:Linkbutton>
							<INPUT id="hidShowMembers" type="hidden" value="0" runat="server">
						</td>
					</TR>
				</TABLE>
				<TABLE>
					<TR>
						<td>
							<asp:datagrid id=dgMembers runat="server" AllowPaging="True" DataSource="<%# dvMembers %>" AutoGenerateColumns="False" Width="100%" ShowHeader="False">
								<Columns>
									<asp:TemplateColumn ItemStyle-Width="20px">
										<ItemTemplate>
											<tra:Image Runat=server BorderWidth=0 ImageUrl='<%# "~/images/" + DataBinder.Eval(Container, "DataItem.AccountType") + ".gif" %>' Width=16 Height=16 ID="Image1"/>
											<input runat="server" value='<%# DataBinder.Eval(Container, "DataItem.AccountName") %>' type="hidden" ID="Hidden1" NAME="Hidden1"/>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Name">
										<ItemTemplate>
											<asp:LinkButton runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DisplayName") %>' CommandName="Select" ID="Linkbutton1">
											</asp:LinkButton>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
								<PagerStyle Mode="NumericPages"></PagerStyle>
							</asp:datagrid></td>
						<td>
							<asp:textbox id="txtMembers" runat="server" Rows="15" TextMode="MultiLine" Columns="40"></asp:textbox></td>
					</TR>
				</TABLE>
			</asp:panel>
		</td>
	</tr>
</table>
