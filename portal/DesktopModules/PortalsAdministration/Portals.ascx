<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Register TagPrefix="cc1" Namespace="Rainbow.UI.WebControls" Assembly="Rainbow" %>
<%@ Control Inherits="Rainbow.DesktopModules.Portals" CodeBehind="Portals.ascx.cs" Language="c#" AutoEventWireup="false" %>
<P>
	<table cellpadding="2" cellspacing="0" border="0" align="center">
		<tr valign="top">
			<td>
				<table cellpadding="0" cellspacing="0" border="0">
					<tr valign="top">
						<td>
							<asp:ListBox id="portalList" width=200 DataSource="<%# portals %>" DataTextField="Name" DataValueField="ID" rows=8 runat="server" CssClass="NormalTextBox" />
						</td>
						<td>
							&nbsp;
						</td>
						<td>
							<table>
								<tr>
									<td>
										<tra:ImageButton id="EditBtn" TextKey="EDIT_PORTAL" AlternateText="Edit selected portal" runat="server" />
									</td>
								</tr>
								<tr>
									<td>
										<tra:ImageButton id="DeleteBtn" TextKey="DELETE_PORTAL" AlternateText="Delete selected portal" runat="server" />
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</P>
