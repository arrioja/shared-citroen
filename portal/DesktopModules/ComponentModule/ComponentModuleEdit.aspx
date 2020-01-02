<%@ Page Language="c#" CodeBehind="ComponentModuleEdit.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.ComponentModuleEdit" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<html>
	<head runat="server"></head>
    <body runat="server">
        <form runat="server">
			<div class="rb_AlternateLayoutDiv">
            <table class="rb_AlternateLayoutTable">
                <tr valign="top">
                    <td class="rb_AlternatePortalHeader"  valign="top">
                        <portal:Banner id="SiteHeader" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <br>
                        <table width="98%" cellspacing="0" cellpadding="4" border="0">
                            <tr valign="top">
                                <td width="100">&nbsp;
                                    
                                </td>
                                <td width="*">
                                    <table width="600" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="left" class="Head">
												Details
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <hr noshade size="1">
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="750" cellspacing="0" cellpadding="0">
                                        <tr valign="top">
                                            <td width="100" class="SubHead">
												Title
                                            </td>
                                            <td rowspan="4">&nbsp;
                                                
                                            </td>
                                            <td>
                                                <asp:TextBox id="TitleField" cssclass="NormalTextBox" width="490" Columns="30" maxlength="150" runat="server" />
                                            </td>
                                            <td width="25" rowspan="4">&nbsp;
                                                
                                            </td>
                                            <td class="Normal" width="250">
                                                <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="TitleField" id="RequiredTitle" />
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td class="SubHead">
												Component
                                            </td>
                                            <td>
                                                <asp:TextBox id="ComponentField" TextMode="Multiline" width="490" Columns="44" Rows="10" runat="server" />
                                            </td>
                                            <td class="Normal">
                                                <asp:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="ComponentField" id="RequiredComponent" />
                                            </td>
                                        </tr>
                                    </table>
                                    <p>
										<tra:LinkButton id="updateButton" Text="UPDATE" runat="server" class="CommandButton" />
										&nbsp;
										<tra:LinkButton id="cancelButton" Text="CANCEL" CausesValidation="False" runat="server" class="CommandButton" />
									</p>
									<hr noshade size="1" width="600">
									<span class="Normal">
										<tra:Literal TextKey="CREATED_BY" Text="Created by" id="CreatedLabel" runat="server"></tra:Literal>&nbsp;
										<asp:label id="CreatedBy" runat="server"></asp:label>&nbsp;
										<tra:Literal TextKey="ON" Text="on" id="OnLabel" runat="server"></tra:Literal>&nbsp;
										<asp:label id="CreatedDate" runat="server"></asp:label>
									</span>
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
