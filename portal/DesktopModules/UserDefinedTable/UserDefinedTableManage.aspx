<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Page Language="c#" CodeBehind="UserDefinedTableManage.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.UserDefinedTableManage" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form runat="server" ID="Form1">
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
								<tr valign="top">
									<td width="100">&nbsp;</td>
									<td width="*">
										<table width="500" cellspacing="0" cellpadding="0">
											<tr>
												<td align="left" class="Head">
													<tra:Label TextKey="USERTABLE_MANAGETABLE" Text="Manage Table" id="ManageTableLabel" runat="Server" />
												</td>
											</tr>
											<tr>
												<td colspan="2">
													<hr noshade size="1">
												</td>
											</tr>
										</table>
										<asp:datagrid id="grdFields" runat="server" CssClass="Normal" OnCancelCommand="grdFields_CancelEdit"
											OnUpdateCommand="grdFields_Update" OnEditCommand="grdFields_Edit" OnDeleteCommand="grdFields_Delete"
											OnItemCommand="grdFields_Move" AutoGenerateColumns="False" CellSpacing="0" CellPadding="2"
											Border="0" DataKeyField="UserDefinedFieldID">
											<Columns>
												<asp:TemplateColumn>
													<ItemTemplate>
														<tra:imagebutton id="cmdEditUserDefinedField" runat="server" causesvalidation="false" commandname="Edit" ImageUrl='<%# CurrentTheme.GetImage("Buttons_Edit", "Edit.gif").ImageUrl %>' AlternateText="Edit" TextKey="EDIT"></tra:imagebutton>
														<tra:imagebutton id="cmdDeleteUserDefinedField" runat="server" causesvalidation="false" commandname="Delete" ImageUrl='<%# CurrentTheme.GetImage("Buttons_Delete", "Delete.gif").ImageUrl %>' AlternateText="Delete" TextKey="DELETE"></tra:imagebutton>
													</ItemTemplate>
													<EditItemTemplate>
														<tra:imagebutton id="cmdSaveUserDefinedField" runat="server" causesvalidation="false" commandname="Update" ImageUrl='<%# CurrentTheme.GetImage("Buttons_Save", "Save.gif").ImageUrl %>' AlternateText="Save" TextKey="SAVE"></tra:imagebutton>
														<tra:imagebutton id="cmdCancelUserDefinedField" runat="server" causesvalidation="false" commandname="Cancel" ImageUrl='<%# CurrentTheme.GetImage("Buttons_Delete", "Delete.gif").ImageUrl %>' AlternateText="Cancel" TextKey="CANCEL"></tra:imagebutton>
													</EditItemTemplate>
												</asp:TemplateColumn>
												<tra:TemplateColumn HeaderText="Visible" TextKey="USERTABLE_VISIBLE" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
													<ItemTemplate>
														<asp:Image runat="server" ImageUrl='<%# IfVisible(Container.DataItem, "Checked", "Unchecked") %>' ID="Image2"/>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:CheckBox runat="server" id="Checkbox2" Checked='True' />
													</EditItemTemplate>
												</tra:TemplateColumn>
												<tra:TemplateColumn HeaderText="Title" TextKey="USERTABLE_TITLE" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
													<ItemTemplate>
														<asp:label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FieldTitle") %>' ID="Label1"/>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox runat="server" id="txtFieldTitle" Columns="30" MaxLength="50" Text='<%# DataBinder.Eval(Container.DataItem, "FieldTitle") %>' CssClass="NormalTextBox" />
													</EditItemTemplate>
												</tra:TemplateColumn>
												<tra:TemplateColumn HeaderText="Type" TextKey="USERTABLE_TYPE" ItemStyle-CssClass="Normal" HeaderStyle-Cssclass="NormalBold">
													<ItemTemplate>
														<asp:label runat="server" Text='<%# GetFieldTypeName(DataBinder.Eval(Container.DataItem, "FieldType").ToString()) %>' ID="Label2"/>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:DropDownList ID="cboFieldType" Runat="server" CssClass="NormalTextBox" SelectedIndex='<%# GetFieldTypeIndex(DataBinder.Eval(Container.DataItem, "FieldType").ToString()) %>' DataSource="<%# GetTableTypes() %>" DataTextField="TypeText" DataValueField="TypeValue">
														</asp:DropDownList>
													</EditItemTemplate>
												</tra:TemplateColumn>
												<asp:TemplateColumn>
													<ItemTemplate>
														<tra:imagebutton id="cmdMoveUserDefinedFieldUp" runat="server" causesvalidation="false" commandname="Item" CommandArgument="Up" ImageUrl='<%# CurrentTheme.GetImage("Buttons_Up", "Up.gif").ImageUrl %>' AlternateText="Move Field Up" TextKey="USERTABLE_MOVEFIELDUP"></tra:imagebutton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<ItemTemplate>
														<tra:imagebutton id="cmdMoveUserDefinedFieldDown" runat="server" causesvalidation="false" commandname="Item" CommandArgument="Down" ImageUrl='<%# CurrentTheme.GetImage("Buttons_Down", "Down.gif").ImageUrl %>' AlternateText="Move Field Down" TextKey="USERTABLE_MOVEFIELDDOWN"></tra:imagebutton>
													</ItemTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid>
										<hr noshade size="1" width="500">
										<p>
											<tra:LinkButton id="cmdAddField" Text="Add New Field" TextKey="USERTABLE_NEWFIELD" CausesValidation="False"
												runat="server" onclick="cmdAddField_Click" class="CommandButton" />&nbsp;
											<tra:LinkButton id="cmdCancel" Text="Back" TextKey="USERTABLE_BACK" CausesValidation="False" runat="server"
												onclick="cmdCancel_Click" class="CommandButton" />
										</p>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td class="rb_AlternatePortalFooter"><foot:Footer id="Footer" runat="server"></foot:Footer></td>
					</tr>
				</table>
			</div>
		</form>
	</body>
</html>
