<%@ Register TagPrefix="cc1" Namespace="Rainbow.UI.WebControls" Assembly="Rainbow" %>
<%@ Control language="c#" Inherits="Rainbow.DesktopModules.XmlModule" CodeBehind="XmlModule.ascx.cs" AutoEventWireup="false" %>
<%@ Import Namespace="Esperantus" %>
<cc1:DesktopModuleTitle PropertiesText="PROPERTIES" PropertiesUrl="~/DesktopModules/Admin/PropertyPage.aspx" runat="server" ID="ModuleTitle" />
<span class="Normal">
	<asp:xml id="xml1" runat="server" />
</span>
