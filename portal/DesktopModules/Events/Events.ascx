<%@ Control language="c#" Inherits="Rainbow.DesktopModules.Events" CodeBehind="Events.ascx.cs" AutoEventWireup="false" %>
<asp:panel id="CalendarPanel" runat="server" visible="false">
<asp:TextBox id="txtDisplayMonth" runat="server" Enabled="False" ReadOnly="True" Visible="False"></asp:TextBox>
<asp:TextBox id="txtDisplayYear" runat="server" Enabled="False" ReadOnly="True" Visible="False"></asp:TextBox>
<table width="100%" border="0" cellpadding="3" cellspacing="0">
	<tr>
		<td nowrap align="left">
			<asp:LinkButton id="PreviousMonth" runat="server" CssClass="EventCalendar">&lt&lt</asp:LinkButton></td>
			<!-- EventCalendarTitle -->
		<td nowrap align="middle"><asp:label CssClass="ItemTitle" id="lblDisplayDate" runat="server" EnableViewState="False"></asp:label></td>
		<td nowrap align="right">
			<asp:LinkButton id="NextMonth" runat="server" CssClass="EventCalendar" align="right"> &gt&gt</asp:LinkButton></td>
	</tr>
	<tr>
		<td colspan="3">
			<asp:label id="lblCalendar" runat="server" EnableViewState="False"></asp:label>
		</td>
	</tr>
</table>
</asp:panel>
<asp:DataList id="myDataList" CellPadding="4" Width="98%" EnableViewState="false" runat="server">
	<ItemTemplate>
		<span class="ItemTitle">
			<asp:HyperLink id="editLink" ImageUrl='<%# CurrentTheme.GetImage("Buttons_Edit", "Edit.gif").ImageUrl %>'
				NavigateUrl='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Events/EventsEdit.aspx","ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID") + "&mid=" + ModuleID) %>' Visible="<%# IsEditable %>" runat="server" />
			<asp:Label Text='<%# DataBinder.Eval(Container.DataItem,"Title") %>' runat="server" />
		</span>
		<br>
		<span class="Normal">
			<i>
				<%# DataBinder.Eval(Container.DataItem,"WhereWhen") %>
			</i>
		</span>
		<br>
		<span class="Normal">
			<%# DataBinder.Eval(Container.DataItem,"Description") %>
		</span>
		<br>
	</ItemTemplate>
</asp:DataList>
