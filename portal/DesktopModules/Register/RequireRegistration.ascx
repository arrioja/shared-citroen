<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Control language="c#" Inherits="Rainbow.Admin.RequireRegistration" CodeBehind="RequireRegistration.ascx.cs" AutoEventWireup="false" %>
<tra:Label id="LabelRegister" CssClass="Normal" runat="server" Text="To use this module you have to register." TextKey="REGISTER_REQUIRED"></tra:Label><BR>
<tra:Label id="LabelAlreadyAccount" CssClass="Normal" runat="server" Text="If you already have an account." TextKey="ALREADY_REGISTERED1"></tra:Label>&#160;
<tra:HyperLink id="SignInHyperLink" runat="server" Text="Log in" TextKey="SIGNIN"></tra:HyperLink>.<BR>
<tra:Label id="LabelRegisterNow" CssClass="Normal" runat="server" Text="Register and Sign In Now." TextKey="REGISTER_NOW"></tra:Label>
<tra:HyperLink id="RegisterHyperlink" runat="server" Text="Register" TextKey="REGISTER"></tra:HyperLink>.
