<%@ Page Language="c#" AutoEventWireup="false" Inherits="PeterBlum.DateTextBoxWebForms.PopupCalendar" clientTarget="Uplevel" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<HTML>
  <HEAD>
      <title>Date</title>
      <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
      <meta content="C#" name="CODE_LANGUAGE">
      <meta content="JavaScript" name="vs_defaultClientScript">
      <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
  </HEAD>
   <body leftmargin="0" topmargin="0" onblur="javascript:self.focus()">
      <form runat="server" ID="Form1">
         <table width="100%" height="100%" cellpadding=0 cellspacing=0 >
            <tr>
               <td align="middle" valign="center" >
                  <asp:Calendar id="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" OnDayRender="Calendar1_DayRender" showtitle="true" DayNameFormat="FirstTwoLetters" SelectionMode="Day" BackColor="#ffffff" FirstDayOfWeek="Monday" BorderColor="#000000" ForeColor="#00000" Height="60" Width="150" Font-Names="Verdana,Arial" Font-Size="8pt">
                     <NextPrevStyle ForeColor="White" BackColor="Navy"></NextPrevStyle>
                     <TitleStyle ForeColor="White" BackColor="Navy"></TitleStyle>
                     <OtherMonthDayStyle ForeColor="Silver"></OtherMonthDayStyle>
                  </asp:Calendar>
               </td>
            </tr>
         </table>
      </form>
   </body>
</HTML>
