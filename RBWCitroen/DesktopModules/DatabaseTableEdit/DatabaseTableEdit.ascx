<%@ Register TagPrefix="TAN" Namespace="TripleASP.SiteAdmin.TableEditorControl" Assembly="TripleASP.TableEditor" %>
<%@ Control Inherits="Rainbow.DesktopModules.DatabaseTableEdit" CodeBehind="DatabaseTableEdit.ascx.cs" Language="c#" AutoEventWireup="false" %>
<asp:label id="lblConnectedError" runat="server" ForeColor="Red" />
<asp:Panel id="panConnected" runat="server">
	<TABLE cellSpacing="0" border="0">
		<TR>
			<TD>
				<asp:Literal id="Message" runat="server"></asp:Literal></TD>
		</TR>
		<TR>
			<TD>
			<P>
					<asp:DropDownList id="tablelist" runat="server" AutoPostBack="true" CssClass="NormalTextBox"></asp:DropDownList></P>
			<P>
					<TAN:TABLEEDITOR class="Normal" id="tableeditor" runat="server" Width="100%"></TAN:TABLEEDITOR></P>
			</TD>
		</TR>
	</TABLE>
</asp:Panel>