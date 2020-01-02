<%@ Register TagPrefix="tra" Namespace="Esperantus.WebControls" Assembly="Esperantus" %>
<%@ Control language="c#" Inherits="Rainbow.DesktopModules.FAQs" CodeBehind="FAQs.ascx.cs" AutoEventWireup="false" %>
<asp:datalist ID="myDataList" runat="server">
	<SelectedItemStyle></SelectedItemStyle>
	<SelectedItemTemplate>
		<tra:HyperLink ID=HyperlinkSelected TextKey="EDIT" Text="Edit" runat="server"
			NavigateUrl='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/FAQs/FAQsEdit.aspx","ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID") + "&mID=" + ModuleID) %>'
			Visible="<%# IsEditable %>"
			ImageUrl='<%# CurrentTheme.GetImage("Buttons_Edit", "Edit.gif").ImageUrl %>' />
		<SPAN class="normalBold">
		<tra:Literal textkey="FAQ_Q" text="Q" ID="Literal1" runat="server"></tra:Literal>:&nbsp;</SPAN>
		<asp:LinkButton ID=LinkbuttonSelected runat="server" CommandName="select" Text='<%# DataBinder.Eval(Container.DataItem, "Question") %>' title='<%# DataBinder.Eval(Container.DataItem, "CreatedDate") %>'>
		</asp:LinkButton><BR>
		</asp:Image>
		<SPAN class="normalBold">
			<tra:Literal textkey="FAQ_A" text="A" ID="Literal2" runat="server"></tra:Literal>:&nbsp;
		</SPAN>
		<asp:Label ID=LabelSelected runat="server" CssClass="normal" Text='<%# DataBinder.Eval(Container.DataItem, "Answer") %>'>:&nbsp;</SPAN>

		</asp:Label>
	</SelectedItemTemplate>
	<ItemTemplate>
		<tra:HyperLink ID=HyperlinkItem TextKey="EDIT" Text="Edit" runat="server"
			NavigateUrl='<%# Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/FAQs/FAQsEdit.aspx","ItemID=" + DataBinder.Eval(Container.DataItem,"ItemID") + "&mID=" + ModuleID)%>' 
			Visible="<%# IsEditable %>" 
			ImageUrl='<%# CurrentTheme.GetImage("Buttons_Edit", "Edit.gif").ImageUrl %>' />
		<SPAN class="normalBold"><tra:Literal textkey="FAQ_Q" text="Q" ID="Literal3" runat="server"></tra:Literal>:&nbsp;</SPAN>
		<asp:LinkButton ID=LinkbuttonItem runat="server" CommandName="select" Text='<%# DataBinder.Eval(Container.DataItem, "Question") %>' title='<%# DataBinder.Eval(Container.DataItem, "CreatedDate") %>'>
		</asp:LinkButton>
	</ItemTemplate>
</asp:datalist>
