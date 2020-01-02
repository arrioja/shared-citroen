<%@ Page Language="c#" CodeBehind="WeatherDEEdit.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.WeatherDEEdit" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
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
									<table width="500" cellspacing="0" cellpadding="0">
										<tr>
											<td align="left" class="Head" height="18">
												German Weather information obtained from '
												<asp:HyperLink id="HyperLink" runat="server" NavigateUrl="http://wetter.com" Target="_blank">Wetter.com</asp:HyperLink>'
											</td>
										</tr>
										<tr>
											<td colspan="2">
												<hr noshade size="1">
											</td>
										</tr>
									</table>
									<table width="500" cellspacing="0" cellpadding="0">
										<tr valign="top">
											<td width="100" class="SubHead">
												Zip code:
											</td>
											<td rowspan="3">&nbsp;
												
											</td>
											<td class="Normal">
												<asp:TextBox id="WeatherZip" cssclass="NormalTextBox" width="390" Columns="30" runat="server" />
												<div class="SubHead">
													<asp:RequiredFieldValidator runat="server" id="rfvWeatherZip" ControlToValidate="WeatherZip" Display="Dynamic"
														ErrorMessage="'Zip' must not be left blank." />
												</div>
											</td>
										</tr>
									</table>
									<table width="500" cellspacing="0" cellpadding="0">
										<tr valign="top">
											<td width="100" class="SubHead">
												City index:
											</td>
											<td rowspan="3">&nbsp;
												
											</td>
											<td class="Normal">
												<asp:TextBox id="WeatherCityIndex" cssclass="NormalTextBox" width="390" Columns="30" runat="server">
												0</asp:TextBox>
												<div class="SubHead">
													<asp:RequiredFieldValidator id="rfvWeatherCityIndex" runat="server" ErrorMessage="'CityIndex' must be a numer."
														Display="Dynamic" ControlToValidate="WeatherCityIndex"></asp:RequiredFieldValidator>
												</div>
											</td>
										</tr>
									</table>
									<table width="500" cellspacing="0" cellpadding="0">
										<tr valign="top">
											<td width="100" class="SubHead">
												Setting:
											</td>
											<td rowspan="3">&nbsp;
												
											</td>
											<td class="Normal">
												<div class="SubHead">
													<asp:RadioButtonList id="WeatherSetting" runat="server" Width="390px" RepeatDirection="Horizontal">
														<asp:ListItem Value="0" Selected="True">Today</asp:ListItem>
														<asp:ListItem Value="1">Forecast</asp:ListItem>
													</asp:RadioButtonList>
												</div>
											</td>
										</tr>
									</table>
									<table width="500" cellspacing="0" cellpadding="0">
										<tr valign="top">
											<td width="100" class="SubHead">
												Design:
											</td>
											<td rowspan="3">&nbsp;
												
											</td>
											<td class="Normal">
												<div class="SubHead">
													<asp:RadioButtonList id="WeatherDesign" runat="server" Width="390px">
														<asp:ListItem Value="1" Selected="True">Size: 150 X 90 px
																		&lt;br&gt;&#160;&#160;&#160;&#160;&#160;&#160;transparent backgroung
																		&lt;br&gt;&#160;&#160;&#160;&#160;&#160;&#160;black font
																		&lt;br&gt;&#160;&#160;&#160;&#160;&#160;&#160;Format: PNG
													</asp:ListItem>
														<asp:ListItem Value="1b">Size: 150 X 90 px
																		&lt;br&gt;&#160;&#160;&#160;&#160;&#160;&#160;transparent backgroung
																		&lt;br&gt;&#160;&#160;&#160;&#160;&#160;&#160;white font
																		&lt;br&gt;&#160;&#160;&#160;&#160;&#160;&#160;Format: PNG
													</asp:ListItem>
														<asp:ListItem Value="1c">Size: 130 X 113 px
																		&lt;br&gt;&#160;&#160;&#160;&#160;&#160;&#160;transparent backgroung
																		&lt;br&gt;&#160;&#160;&#160;&#160;&#160;&#160;black font
																		&lt;br&gt;&#160;&#160;&#160;&#160;&#160;&#160;Format: PNG
													</asp:ListItem>
														<asp:ListItem Value="2">Size: 130 X 113 px
																		&lt;br&gt;&#160;&#160;&#160;&#160;&#160;&#160;orange backgroung
																		&lt;br&gt;&#160;&#160;&#160;&#160;&#160;&#160;black font
																		&lt;br&gt;&#160;&#160;&#160;&#160;&#160;&#160;Format: PNG
													</asp:ListItem>
														<asp:ListItem Value="2b">Size: 150 X 94 px
																		&lt;br&gt;&#160;&#160;&#160;&#160;&#160;&#160;orange backgroung
																		&lt;br&gt;&#160;&#160;&#160;&#160;&#160;&#160;black font
																		&lt;br&gt;&#160;&#160;&#160;&#160;&#160;&#160;Format: PNG
													</asp:ListItem>
														<asp:ListItem Value="3">Size: 150 X 90 px
																		&lt;br&gt;&#160;&#160;&#160;&#160;&#160;&#160;blue backgroung
																		&lt;br&gt;&#160;&#160;&#160;&#160;&#160;&#160;black font
																		&lt;br&gt;&#160;&#160;&#160;&#160;&#160;&#160;Format: PNG
													</asp:ListItem>
													</asp:RadioButtonList>
												</div>
											</td>
										</tr>
									</table>
									<p>
										<asp:placeholder id="PlaceHolderButtons" runat="server"></asp:placeholder>
									</p>
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
