<%@ Control language="c#" Inherits="Rainbow.DesktopModules.RainbowVersion" CodeBehind="RainbowVersion.ascx.cs" AutoEventWireup="false" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<P>
	<tra:Label id="RainbowVersion" TextKey="RAINBOW_RUNNING_VERSION" Text="The running rainbow version is" runat="server" CssClass="Normal" EnableViewstate="false" />
	<asp:Label id="VersionLabel" CssClass="Normal" runat="server" EnableViewState="False"></asp:Label>
</P>
<P>
	<asp:Label id="currentUILanguage" CssClass="Normal" runat="server" EnableViewState="False"></asp:Label>/
	<asp:Label id="currentLanguage" CssClass="Normal" runat="server" EnableViewState="False"></asp:Label>
</P>