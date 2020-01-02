<%@ Register TagPrefix="rb" Namespace="Rainbow.UI.WebControls" Assembly="Rainbow" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="FlashModule.ascx.cs" Inherits="Rainbow.DesktopModules.FlashModule" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<rb:FlashMovie runat="server" id="FlashMovie1" ShowMenu="False" FlashOutputType="FlashOnly" AutoLoop="False" AutoPlay="True" AllowFlashAutoInstall="False" WindowMode="Opaque" MovieHeight="100" MovieWidth="340" MovieQuality="High" HtmlAlignment="none" MovieBGColor="#000000" MovieScale="ShowAll" />
<asp:Label id="ErrorLabel" runat="server" CssClass="Error" Visible="False"></asp:Label>
