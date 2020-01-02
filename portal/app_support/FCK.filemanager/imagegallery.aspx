<%@ Page language="c#" CodeBehind="imagegallery.aspx.cs" AutoEventWireup="false" Inherits="Rainbow.DesktopModules.FCK.filemanager.browse.imagegallery" %>
<!doctype html public "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Insert Image</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<META HTTP-EQUIV="Expires" CONTENT="0">
		<style>

BODY { BORDER-RIGHT: 0px; PADDING-RIGHT: 0px; BORDER-TOP: 0px; PADDING-LEFT: 0px; BACKGROUND: #ffffff; PADDING-BOTTOM: 0px; MARGIN: 0px; OVERFLOW: hidden; BORDER-LEFT: 0px; WIDTH: 100%; PADDING-TOP: 0px; BORDER-BOTTOM: 0px }

BODY { FONT-SIZE: 10pt; COLOR: #000000; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif }

TR { FONT-SIZE: 10pt; COLOR: #000000; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif }

TD { FONT-SIZE: 10pt; COLOR: #000000; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif }

DIV.imagespacer { FLOAT: left; MARGIN: 5px; FONT: 10pt verdana; OVERFLOW: hidden; WIDTH: 120px; HEIGHT: 126px; TEXT-ALIGN: center }

DIV.imageholder { BORDER-RIGHT: #cccccc 1px solid; PADDING-RIGHT: 0px; BORDER-TOP: #cccccc 1px solid; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: #cccccc 1px solid; WIDTH: 100px; PADDING-TOP: 0px; BORDER-BOTTOM: #cccccc 1px solid; HEIGHT: 100px }

DIV.titleholder { FONT-SIZE: 8pt; OVERFLOW: hidden; WIDTH: 100px; FONT-FAMILY: ms sans serif, arial; WHITE-SPACE: nowrap; TEXT-OVERFLOW: ellipsis }

</style>
		<script language="javascript">
lastDiv = null;
function divClick(theDiv,filename) {
	if (lastDiv) {
		lastDiv.style.border = "1 solid #CCCCCC";
	}
	lastDiv = theDiv;
	theDiv.style.border = "2 solid #316AC5";
	
	document.getElementById("FileToDelete").value = filename;

}
function gotoFolder(rootfolder,newfolder) {
	window.navigate("imagegallery.aspx?frame=1&rif=" + rootfolder + "&cif=" + newfolder);
}		
function returnImage(imagename,width,height) {
	var arr = new Array();
	arr["filename"] = imagename;  
	arr["width"] = width;  
	arr["height"] = height;			 
	window.parent.returnValue = arr;
	window.parent.setImage(imagename) ;
	window.parent.close();	
}		
		</script>
</HEAD>
	<body>
		<FORM encType="multipart/form-data" runat="server">
			<asp:panel id="MainPage" runat="server" visible="false">
<TABLE height="100%" cellSpacing=0 cellPadding=0 width="100%" border=0>
  <TR>
    <TD>
      <DIV id=galleryarea style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 100%">
<asp:label id=gallerymessage runat="server"></asp:label>
<asp:panel id=GalleryPanel runat="server"></asp:panel></DIV></TD></TR>
<asp:Panel id=UploadPanel runat="server">
  <TR>
    <TD height=16>
      <TABLE>
        <TR>
          <TD vAlign=top><INPUT id=UploadFile style="WIDTH: 300px" type=file 
            name=UploadFile runat="server"></TD>
          <TD vAlign=top>
<asp:button id=UploadImage onclick=UploadImage_OnClick runat="server" Text="Upload"></asp:button></TD>
          <TD vAlign=top>
<asp:button id=DeleteImage onclick=DeleteImage_OnClick runat="server" Text="Delete"></asp:button></TD>
          <TD vAlign=middle></TD>
        <TR>
          <TD colSpan=3>
<asp:RegularExpressionValidator id=FileValidator runat="server" display="dynamic" ControlToValidate="UploadFile"></asp:RegularExpressionValidator>
<asp:literal id=ResultsMessage runat="server"></asp:literal></TD></TR></TABLE><INPUT 
      id=FileToDelete type=hidden runat="server"> <INPUT id=RootImagesFolder 
      type=hidden value=images runat="server"> <INPUT id=CurrentImagesFolder 
      type=hidden value=images runat="server"> 
</TD></TR></asp:Panel></TABLE>
			</asp:panel>
			<asp:panel id="iframePanel" runat="server">
				<iframe style="BORDER-RIGHT:0px;BORDER-TOP:0px;BORDER-LEFT:0px;WIDTH:100%;BORDER-BOTTOM:0px;HEIGHT:100%" border=0 frameborder=0 src="imagegallery.aspx?frame=1&amp;<%=Request.QueryString%>">
				</iframe>
			</asp:panel>
		</FORM>
	</body>
</HTML>