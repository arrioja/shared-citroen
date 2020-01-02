<%@ Page Language="c#" CodeBehind="EnhancedLinksEdit.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.EnhancedLinksEdit" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Register TagPrefix="upl" Namespace="Rainbow.UI.WebControls" Assembly="Rainbow" %>
<html>
	<head runat="server"></head>
	<body runat="server">
<script language=JavaScript>
<!--
function newWindow(file,window) {
    msgWindow=open(file,window,'resizable=yes,width=400,height=400,scrollbars=yes');
    if (msgWindow.opener == null) msgWindow.opener = self;
}
-->
		</script>

<form id=Form1 runat="server">
<div class=rb_AlternateLayoutDiv>
<table class=rb_AlternateLayoutTable>
  <tr vAlign=top>
    <td class=rb_AlternatePortalHeader vAlign=top><portal:banner id=SiteHeader runat="server"></portal:banner></td></tr>
  <tr>
    <td><br>
      <table cellSpacing=0 cellPadding=4 width="98%" border=0 
      >
        <tr vAlign=top>
          <td width=150>&nbsp;</td>
          <td width=*>
            <table cellSpacing=0 cellPadding=0 width=500>
              <tr>
                <td class=Head align=left width=266 
                  ><tra:literal id=Literal1 
                   runat="server" Text="Link detail" 
                  TextKey="LINKDETAILS"></tra:literal></td></tr></table>
            <table cellSpacing=0 cellPadding=0 width=750 border=0 
            >
              <tr>
                <td class=SubHead width=100><tra:literal 
                  id=Literal10 runat="server" 
                  Text="Link Group?" TextKey="LINKGROUP"></tra:literal></td>
                <td><asp:checkbox id=IsGroup runat="server" ToolTip="Separator for group links" AutoPostBack="True"></asp:checkbox></td></tr>
              <tr>
                <td class=SubHead width=100><tra:literal 
                  id=Literal11 runat="server" 
                  Text="Icon File" TextKey="ICONFILE"></tra:literal>:</td>
                <td><upl:uploaddialogtextbox id=Src runat="server" maxlength="150" Columns="30" width="390" CssClass="NormalTextBox"></upl:uploaddialogtextbox></td></tr>
              <tr>
                <td class=SubHead width=100><tra:literal 
                  id=Literal2 runat="server" Text="Title" 
                  TextKey="TITLE"></tra:literal>:</td>
                <td><asp:textbox id=TitleField runat="server" maxlength="150" Columns="30" width="390" CssClass="NormalTextBox"></asp:textbox></td>
                <td class=Normal width=250><tra:requiredfieldvalidator id=Req1 runat="server" TextKey="ERROR_VALID_TITLE" ControlToValidate="TitleField" ErrorMessage="You Must Enter a Valid Title" Display="Static"></tra:requiredfieldvalidator></td></tr>
              <tr>
                <td class=SubHead><tra:literal 
                  id=UrlFieldLabel runat="server" 
                  Text="Url" TextKey="URL"></tra:literal></td>
                <td><asp:textbox id=UrlField runat="server" maxlength="150" Columns="30" width="390" CssClass="NormalTextBox"></asp:textbox><asp:literal 
                  id=oldUrl runat="server" Text=string.Empty 
                  Visible="False"></asp:literal></td>
                <td class=Normal><tra:requiredfieldvalidator id=Req2 runat="server" TextKey="ERROR_VALID_URL" ControlToValidate="UrlField" ErrorMessage="You Must Enter a Valid URL" Display="Dynamic"></tra:requiredfieldvalidator></td></tr>
              <tr>
                <td class=SubHead><tra:literal 
                  id=MobileUrlFieldLabel runat="server" 
                  Text="Mobile Url" TextKey="MOBILEURL"></tra:literal></td>
                <td><asp:textbox id=MobileUrlField runat="server" maxlength="150" Columns="30" width="390" CssClass="NormalTextBox"></asp:textbox></td>
                <td>&nbsp;</td></tr>
              <tr>
                <td class=SubHead><tra:literal 
                  id=Literal5 runat="server" 
                  Text="Description" TextKey="DESCRIPTION"></tra:literal>:</td>
                <td><asp:textbox id=DescriptionField runat="server" maxlength="150" Columns="30" width="390" CssClass="NormalTextBox"></asp:textbox></td>
                <td>&nbsp;</td></tr>
              <tr>
                <td class=SubHead><tra:literal 
                  id=TargetFieldLabel runat="server" 
                  Text="Target" TextKey="TARGET"></tra:literal></td>
                <td><asp:dropdownlist id=TargetField runat="server" Width="390px"></asp:dropdownlist></td>
                <td>&nbsp;</td></tr>
              <tr>
                <td class=SubHead><tra:literal 
                  id=Literal7 runat="server" 
                  Text="View Order" TextKey="VIEWORDER"></tra:literal>:</td>
                <td><asp:textbox id=ViewOrderField runat="server" maxlength="3" Columns="30" width="390" CssClass="NormalTextBox"></asp:textbox></td>
                <td class=Normal><tra:requiredfieldvalidator id=RequiredViewOrder runat="server" TextKey="ERROR_VALID_VIEWORDER" ControlToValidate="ViewOrderField" ErrorMessage="You Must Enter a Valid View Order" Display="Static"></tra:requiredfieldvalidator><tra:comparevalidator id=VerifyViewOrder runat="server" TextKey="ERROR_VALID_VIEWORDER" ControlToValidate="ViewOrderField" ErrorMessage="You Must Enter a Valid View Order" Display="Static" Type="Integer" Operator="DataTypeCheck"></tra:comparevalidator></td></tr></table>
            <p>
            <P>
            <P><asp:linkbutton id=updateButton runat="server" Text="Update" Cssclass="CommandButton"></asp:linkbutton>&nbsp; 
<asp:linkbutton id=cancelButton runat="server" Text="Cancel" Cssclass="CommandButton" CausesValidation="False"></asp:linkbutton>&nbsp; 
<asp:linkbutton id=deleteButton runat="server" Text="Delete this item" Cssclass="CommandButton" CausesValidation="False"></asp:linkbutton>&nbsp; 
            <hr width=500 noShade SIZE=1>
            <span class=Normal><tra:literal id=Literal8 
             runat="server" Text="Created by" 
            TextKey="CREATEDBY"></tra:literal>&nbsp; <asp:label id=CreatedBy runat="server"></asp:label>&nbsp; 
            <tra:literal id=Literal9 runat="server" 
            Text="on" TextKey="ON"></tra:literal>&nbsp; <asp:label id=CreatedDate runat="server"></asp:label><br 
            ></span></td></tr></table></td></tr>
  <tr>
    <td class=rb_AlternatePortalFooter><foot:footer id=Footer runat="server"></foot:footer></td></tr></table></div></form>
	</body>
</html>
