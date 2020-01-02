<%@ Page language="c#" ValidateRequest=false Inherits="Rainbow.Ftb.Imagegallery" CodeBehind="ftb.imagegallery.aspx.cs" AutoEventWireup="false" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>

<html>
<head>
	<title>Image Gallery</title>
</head>
<body>

    <form id="Form1" runat="server" enctype="multipart/form-data">  
    
		<FTB:ImageGallery id="ImageGallery1" runat="Server" />
		
	</form>

</body>
</html>
