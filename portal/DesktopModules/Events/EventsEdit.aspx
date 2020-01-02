<%@ Page Language="c#" CodeBehind="EventsEdit.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.EventsEdit" %>
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
						<td class="rb_AlternatePortalHeader" valign="top">
							<portal:Banner id="SiteHeader" runat="server" />
						</td>
					</tr>
					<tr>
						<td>
							<br>
							<table width="98%" cellspacing="0" cellpadding="4" border="0">
								<tr valign="top">
									<td width="150">&nbsp;
									</td>
									<td width="*">
										<table width="100%" cellspacing="0" cellpadding="0">
											<tr>
												<td align="left" class="Head">
													<tra:Label runat="server" TextKey="EVENTS_DETAILS" Text="Event detail" ID="Label5" NAME="Label1"></tra:Label>:
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
													<tra:Label runat="server" TextKey="EVENTS_TITLE" Text="Title" ID="Label1" NAME="Label1"></tra:Label>:
												</td>
												<td rowspan="7">&nbsp;
												</td>
												<td>
													<asp:TextBox id="TitleField" cssclass="NormalTextBox" width="390" Columns="30" maxlength="150" runat="server" />
												</td>
												<td width="25" rowspan="7">&nbsp;</td>
												<td class="Normal" width="250">
													<tra:RequiredFieldValidator Display="Dynamic" runat="server" ControlToValidate="TitleField" id="RequiredTitle" TextKey="ERROR_VALID_TITLE" ErrorMessage="Please insert a valid Title" />
												</td>
											</tr>
											<tr valign="top">
												<td class="SubHead">
													<tra:Label runat="server" TextKey="EVENTS_DESCRIPTION" Text="Description" ID="Label2" NAME="Label2"></tra:Label>:
												</td>
												<td>
													<asp:placeholder id="PlaceHolderHTMLEditor" runat="server"></asp:placeholder>
												</td>
												<td class="Normal">&nbsp;
												</td>
											</tr>
											<tr valign="top">
												<td class="SubHead">
													<tra:Label runat="server" TextKey="EVENTS_WHEREWHEN" Text="Where" ID="Label3" NAME="Label2"></tra:Label>:
												</td>
												<td>
													<asp:TextBox id="WhereWhenField" cssclass="NormalTextBox" width="390" Columns="30" maxlength="150" runat="server" />
												</td>
												<td class="Normal">
													<tra:RequiredFieldValidator TextKey="ERROR_VALID_WHEREWHEN" ErrorMessage="Please insert a valid Where When field" Display="Dynamic" runat="server" ControlToValidate="WhereWhenField" id="RequiredWhereWhen" />
												</td>
											</tr>
											<tr valign="top">
												<td class="SubHead">
													<tra:Label runat="server" TextKey="EVENTS_STARTDATE" Text="When" ID="Label7" NAME="Label7"></tra:Label>:
												</td>
												<td>
													<asp:TextBox id="StartDate" cssclass="NormalTextBox" width="100" Columns="8" runat="server" />
												</td>
												<td class="Normal">
													<tra:RequiredFieldValidator Display="Dynamic" TextKey="ERROR_VALID_STARTDATE" ErrorMessage="Please insert a valid date" id="RequiredFieldValidator1" runat="server" ControlToValidate="StartDate" />
													<tra:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" TextKey="ERROR_VALID_STARTDATE" ErrorMessage="Please insert a valid date" ControlToValidate="StartDate" Display="Dynamic"></tra:RequiredFieldValidator>
												</td>
											</tr>
											<tr valign="top">
												<td class="SubHead">
													<tra:Label runat="server" TextKey="EVENTS_STARTTIME" Text="Time" ID="Label8" NAME="Label8"></tra:Label>:
												</td>
												<td>
													<P>
														<table cellpadding="0" cellspacing="0" border="0">
															<tr>
																<td><asp:radiobuttonlist id="AllDay" runat="server" AutoPostBack="True" CssClass="Normal">
																		<asp:ListItem Value="1" Selected="True">All Day Event</asp:ListItem>
																		<asp:ListItem Value="0">Start at </asp:ListItem>
																	</asp:radiobuttonlist></td>
																<td vAlign="bottom"><asp:dropdownlist id="StartHour" runat="server" CssClass="NormalTextBox" Enabled="False">
																		<asp:ListItem Value="1" Selected="True">1</asp:ListItem>
																		<asp:ListItem Value="2">2</asp:ListItem>
																		<asp:ListItem Value="3">3</asp:ListItem>
																		<asp:ListItem Value="4">4</asp:ListItem>
																		<asp:ListItem Value="5">5</asp:ListItem>
																		<asp:ListItem Value="6">6</asp:ListItem>
																		<asp:ListItem Value="7">7</asp:ListItem>
																		<asp:ListItem Value="8">8</asp:ListItem>
																		<asp:ListItem Value="9">9</asp:ListItem>
																		<asp:ListItem Value="10">10</asp:ListItem>
																		<asp:ListItem Value="11">11</asp:ListItem>
																		<asp:ListItem Value="12">12</asp:ListItem>
																	</asp:dropdownlist>&nbsp;<b>:</b>
																	<asp:dropdownlist id="StartMinute" runat="server" CssClass="NormalTextBox" Enabled="False">
																		<asp:ListItem Value="00" Selected="True">00</asp:ListItem>
																		<asp:ListItem Value="05">05</asp:ListItem>
																		<asp:ListItem Value="10">10</asp:ListItem>
																		<asp:ListItem Value="15">15</asp:ListItem>
																		<asp:ListItem Value="20">20</asp:ListItem>
																		<asp:ListItem Value="25">25</asp:ListItem>
																		<asp:ListItem Value="30">30</asp:ListItem>
																		<asp:ListItem Value="35">35</asp:ListItem>
																		<asp:ListItem Value="40">40</asp:ListItem>
																		<asp:ListItem Value="45">45</asp:ListItem>
																		<asp:ListItem Value="50">50</asp:ListItem>
																		<asp:ListItem Value="55">55</asp:ListItem>
																	</asp:dropdownlist>&nbsp;
																	<asp:dropdownlist id="StartAMPM" runat="server" CssClass="NormalTextBox" Enabled="False">
																		<asp:ListItem Value="AM" Selected="True">AM</asp:ListItem>
																		<asp:ListItem Value="PM">PM</asp:ListItem>
																	</asp:dropdownlist>&nbsp;&nbsp;
																</td>
															</tr>
														</table>
													</P>
												</td>
												<td class="Normal"></td>
											</tr>
											<tr valign="top">
												<td class="SubHead">
													<tra:Label runat="server" TextKey="EVENTS_EXPIRES" Text="Expires" ID="Label4" NAME="Label2"></tra:Label>:
												</td>
												<td>
													<asp:TextBox id="ExpireField" cssclass="NormalTextBox" width="100" Columns="8" runat="server" /><br>
												</td>
												<td class="Normal">
													<tra:RequiredFieldValidator id="RequiredExpireDate" runat="server" TextKey="ERROR_VALID_EXPIRE_DATE" ErrorMessage="Please insert a valid date" ControlToValidate="ExpireField" Display="Dynamic" DESIGNTIMEDRAGDROP="73"></tra:RequiredFieldValidator>
													<tra:CompareValidator Display="Dynamic" TextKey="ERROR_VALID_EXPIRE_DATE" ErrorMessage="Please insert a valid date" id="VerifyExpireDate" runat="server" Operator="DataTypeCheck" ControlToValidate="ExpireField" Type="Date" />
												</td>
											</tr>
										</table>
										<p>
											<tra:LinkButton id="updateButton" Text="Update" runat="server" class="CommandButton" />
											&nbsp;
											<tra:LinkButton id="cancelButton" Text="Cancel" CausesValidation="False" runat="server" class="CommandButton" />
											&nbsp;
											<tra:LinkButton id="deleteButton" Text="Delete this item" CausesValidation="False" runat="server" class="CommandButton" />
										</p>
										<hr noshade size="1">
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
