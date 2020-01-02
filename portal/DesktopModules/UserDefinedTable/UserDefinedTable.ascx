<%@ Control language="c#" Inherits="Rainbow.DesktopModules.UserDefinedTable" CodeBehind="UserDefinedTable.ascx.cs" AutoEventWireup="false" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<asp:placeholder id="PlaceHolderOutput" runat="server"></asp:placeholder>
<br>
<tra:LinkButton ID="cmdManage" Runat="server" onclick="cmdManage_Click" CssClass="CommandButton" Text="Manage Table" TextKey="USERTABLE_MANAGETABLE" Visible="False"></tra:LinkButton>
