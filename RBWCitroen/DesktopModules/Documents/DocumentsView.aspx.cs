using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules 
{
	[History("jviladiu@portalServices.net", "2004/07/02", "Corrections for correct operation")]
	public class DocumentsView : Rainbow.UI.ViewItemPage
	{
		/// <summary>
		/// The Page_Load event handler on this Page is used to
		/// obtain obtain the contents of a document from the 
		/// Documents table, construct an HTTP Response of the
		/// correct type for the document, and then stream the 
		/// document contents to the response.  It uses the 
		/// Rainbow.DocumentDB() data component to encapsulate 
		/// the data access functionality.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e) 
		{
			if (ItemID != -1) 
			{
				// Obtain Document Data from Documents table
				DocumentDB documents = new DocumentDB();
            
				// Change by Geert.Audenaert@Syntegra.Com
				// Date: 7/2/2003
				WorkFlowVersion version = Request.QueryString["wversion"] == "Staging" ? WorkFlowVersion.Staging : WorkFlowVersion.Production;
				// End Change Geert.Audenaert@Syntegra.Com
            
				// Change by Geert.Audenaert@Syntegra.Com
				// Date: 7/2/2003
				// Original:
				// SqlDataReader dBContent = documents.GetDocumentContent(ItemID);
				SqlDataReader dBContent = documents.GetDocumentContent(ItemID, version);
				// End Change Geert.Audenaert@Syntegra.Com

				try
				{
					dBContent.Read();

					// Serve up the file by name
					// jviladiu@portalServices.net. FIX: FileName does not exist
					// Response.AppendHeader("content-disposition", "filename=" + (string)dBContent["FileName"]);          
					Response.AppendHeader("content-disposition", "filename=" + (string)dBContent["FileNameUrl"]);          
            
					// set the content type for the Response to that of the 
					// document to display.  For example. "application/msword"
					// Change for translate extension-files to exact contentType
					// Response.ContentType = (string) dBContent["ContentType"];
					Response.ContentType = giveMeContentType((string) dBContent["ContentType"]);
            
					// output the actual document contents to the response output stream
					Response.OutputStream.Write((byte[]) dBContent["Content"], 0, (int) dBContent["ContentSize"]);
				}
				finally
				{
					dBContent.Close(); //by Manu, fixed bug 807858
				}

				// end the response
				Response.End();
			}
		}

		/// <summary>
		/// Set the module guids with free access to this page
		/// </summary>
		protected override ArrayList AllowedModules
		{
			get
			{
				ArrayList al = new ArrayList();
				al.Add ("F9645B82-CB45-4C4C-BB2D-72FA42FE2B75");
				return al;
			}
		}

		/// <summary>
		/// Return the contentType for a especific extension
		/// </summary>
		/// <param name="extension">file extension</param>
		/// <returns>contentType</returns>
		private string giveMeContentType (string extension) 
		{
			switch (extension) 
			{
				case "pdf": return "application/pdf";
				case "doc": return "application/msword";
				case "rtf": return "application/rtf";
				case "xls": return "application/vnd.ms-excel";
				case "zip": 
				case "rar": return "application/zip";
				case "txt": return "text/plain";
				case "ppt": return "application/vnd.ms-powerpoint";
				case "gif": return "image/gif";
				case "jpg":
				case "jpeg": return "image/jpeg";
				case "bmp": return "image/bmp";
				case "png": return "image/png";
				case "swf": return "application/x-shockwave-flash";
				case "fla": 
				case "exe": 
				case "com":
				case "mdb": return "application/octet-stream";
				case "dll": return "application/octet-stream";
				case "config":
				case "ini":
				case "sys": 
				case "bat": 
				case "c":
				case "h":
				case "bas":
				case "cs":
				case "vb": return "text/plain";
				case "mp3": return "audio/mpeg3";
				case "css": return "text/css";
				default: return "application/octet-stream";
			}
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Raises the Init event.
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() 
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

	}

}
