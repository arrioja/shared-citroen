<%@ Page language="c#" CodeBehind="QuizPage.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.QuizPage" %>
<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Register TagPrefix="portal" TagName="Banner" Src="~/Design/DesktopLayouts/DesktopPortalBanner.ascx" %>
<%@ Register TagPrefix="foot" TagName="Footer" Src="~/Design/DesktopLayouts/DesktopFooter.ascx" %>
<html>
	<head runat="server"></head>
	<body runat="server">
		<form runat="server">
		<div class="rb_DefaultLayoutDiv">
			<table class="rb_DefaultLayoutTable">
				<tr valign="top">
					<td class="rb_DefaultPortalHeader">
						<portal:Banner id="SiteHeader" runat="server" />
					</td>
				</tr>
			<tr>
				<td align="center"><br>
					<h2>Quiz:&nbsp;<asp:label id="lblQuiz" runat="server" /></h2>
					<span id="QuizScreen" runat="server">
							<table width="50%" border="0" cellpadding="2" cellspacing="0">
								<tr>
									<td align="center">
										<b><asp:label id="lblQuestion" runat="server" /></b><br>
										<asp:radiobuttonlist id="rblAnswer" RepeatDirection="vertical" TextAlign="right" RepeatLayout="table" runat="server" /><br>
										<asp:requiredfieldvalidator ControlToValidate="rblAnswer" ErrorMessage="Please pick an answer!" runat="server" ID="Requiredfieldvalidator1" /><br>
										<asp:button id="btnSubmit" class="CommandButton" text=" Next >> " onClick="btnSubmit_Click" runat="server" />
									</td>
								</tr>
								<tr>
									<td align="center">
										<b>
											<br>Total questions: 
											<asp:label id="lblTotalQuestion" runat="server" />
											&nbsp;&nbsp;&nbsp;
											Time spent: <asp:label id="lblTimeSpent" runat="server" />
										</b>
									</td>
								</tr>
							</table>
					</span>
					<span id="ResultScreen" runat="server">
						<asp:label id="lblResult" runat="server" />
					</span>
				</td>
			</tr>
			<tr>
					<td class="rb_DefaultPortalFooter"><foot:Footer id="Footer" runat="server"></foot:Footer></td>
					</tr>
			</table>
		</div>
		</form>
	</body>
</html>
