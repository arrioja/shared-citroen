<%@ Import Namespace="Esperantus" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Page Language="c#" CodeBehind="EnhancedHtmlEdit.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.EnhancedHtmlEdit" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>

<HTML>
  <HEAD runat="server">
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema>
  </HEAD>
<body runat="server">
<form id=Form1 encType="multipart/form-data" runat="server">
<div class=rb_AlternateLayoutDiv>
<table align=center class=rb_AlternateLayoutTable border='0' cellpadding='0' cellspacing='0'>
  <tr vAlign=top>
    <td class=rb_AlternatePortalHeader vAlign=top><portal:banner id=SiteHeader runat="server"></portal:banner></td></tr>
  <tr>
    <td><asp:panel id=pnlSelectPage 
      Runat="server">
      <TABLE align=center border='0' cellpadding='0' cellspacing='0'>
        <TR>
          <TD>
            <TABLE border='0' cellpadding='0' cellspacing='0'>
              <TR>
                <TD class=Head><tra:literal id=Literal6 runat="server" Text="Language" TextKey="ENHANCEDHTML_LANGUAGE"></tra:literal></TD>
                <TD class=Head><LABEL for="<%= lstPages.ClientID %>"><tra:label id=Label1 runat="server" Text="Page" TextKey="ENHANCEDHTML_PAGE"></tra:label></LABEL></TD></TR>
              <TR>
                <TD vAlign=top align=center><asp:listbox id=lstLanguages runat="server" width="250px" Rows="20" CssClass="Normal" AutoPostBack="True"></asp:listbox></TD>
                <TD vAlign=top align=center><asp:listbox id=lstPages runat="server" width="300px" Rows="20" CssClass="Normal"></asp:listbox></TD></TR></TABLE></TD></TR>
        <TR>
          <TD noWrap align=center><tra:button id=cmdNewPage Runat="server" Text="New Page" TextKey="ENHANCEDHTML_NEWPAGE" CssClass="CommandButton" CausesValidation="False" Width="95"></tra:button>&nbsp; 
<tra:button id=cmdEditPage Runat="server" Text="Edit Page" TextKey="ENHANCEDHTML_EDITPAGE" CssClass="CommandButton" CausesValidation="False" Width="95" Enabled="False"></tra:button>&nbsp; 
<tra:button id=cmdDeletePage Runat="server" Text="Delete Page" TextKey="ENHANCEDHTML_DELETEPAGE" CssClass="CommandButton" CausesValidation="False" Width="95" Enabled="False"></tra:button>&nbsp; 
<tra:button id=cmdReturn Runat="server" Text="Return" TextKey="ENHANCEDHTML_RETURN" CssClass="CommandButton" CausesValidation="False" Width="95"></tra:button>&nbsp; 
          </TD></TR></TABLE></asp:panel><asp:panel id=pnlEditPage 
       Runat="server" Visible="False">
      <TABLE align=center border='0' cellpadding='0' cellspacing='0'>
        <TR>
          <TD>
            <TABLE border='0' cellpadding='0' cellspacing='0'>
              <TR>
                <TD class=SubHead align=left width=90><LABEL 
                  for="<%= txtPageName.ClientID %>"><tra:Literal id=Literal3 runat="server" Text="Page Name" TextKey="ENHANCEDHTML_PAGENAME"></tra:Literal></LABEL></TD>
                <TD align=left><asp:TextBox id=txtPageName runat="server" width="500px" CssClass="NormalTextBox" textmode="SingleLine"></asp:TextBox></TD></TR></TABLE></TD></TR>
        <TR>
          <TD>
            <TABLE border='0' cellpadding='0' cellspacing='0'>
              <TR>
                <TD class=SubHead width=90><LABEL 
                  for="<%= txtViewOrder.ClientID %>"><tra:Literal id=Literal4 runat="server" Text="View Order" TextKey="ENHANCEDHTML_VIEWORDER"></tra:Literal></LABEL></TD>
                <TD width=180><asp:TextBox id=txtViewOrder runat="server" width="100" CssClass="NormalTextBox" textmode="SingleLine"></asp:TextBox></TD>
                <TD class=SubHead align=right><tra:Literal id=Literal5 runat="server" Text="Language" TextKey="ENHANCEDHTML_LANGUAGE"></tra:Literal></TD>
                <TD class=Normal>&nbsp; <asp:DropDownList id=listLanguages runat="server"></asp:DropDownList></TD></TR>
              <TR>
                <TD class=SubHead><tra:Literal id=Literal1 runat="server" Text="Content from" TextKey="ENHANCEDHTML_CONTENT_FROM"></tra:Literal></TD>
                <TD colSpan=2><asp:RadioButtonList id=kindOfContent runat="server" AutoPostBack="True">
<asp:ListItem Value="Editor" Selected="True">Html Editor</asp:ListItem>
<asp:ListItem Value="Module">Module</asp:ListItem>
</asp:RadioButtonList></TD></TR></TABLE></TD></TR>
        <TR>
          <TD>
            <TABLE border='0' cellpadding='0' cellspacing='0'>
              <TR>
                <TD class=SubHead><tra:Literal id=Literal2 runat="server" Text="Desktop HTML Content" TextKey="HTML_DESKTOP_CONTENT"></tra:Literal></TD></TR>
              <TR>
                <TD class=SubHead><asp:placeholder id=PlaceHolderHTMLEditor runat="server"></asp:placeholder><asp:DropDownList id=listModules runat="server" visible="false"></asp:DropDownList><asp:DropDownList id=listAllModules runat="server" visible="false"></asp:DropDownList></TD></TR></TABLE></TD></TR>
        <TR>
          <TD>
            <TABLE align=center border='0' cellpadding='0' cellspacing='0'>
              <TR>
                <TD noWrap align=center><tra:Button id=cmdPageUpdate Runat="server" Text="Update" TextKey="ENHANCEDHTML_UPDATE" CssClass="CommandButton" CausesValidation="False"></tra:Button>&nbsp; 
<tra:Button id=cmdPageCancel Runat="server" Text="Cancel" TextKey="ENHANCEDHTML_CANCEL" CssClass="CommandButton" CausesValidation="False"></tra:Button></TD></TR></TABLE></TD></TR></TABLE></asp:panel></td></tr>
  <tr>
    <td class=rb_AlternatePortalFooter><foot:footer id=Footer runat="server"></foot:footer></td></tr></table></div></form>
	</body>
</HTML>
