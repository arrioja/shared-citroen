using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Rainbow.Configuration;
using Rainbow.Helpers;


namespace Rainbow.DesktopModules
{
	/// <summary>
	/// WebPart module - Digital Dashboard WebPart Wrapper
	/// Written by: damacco, damacco@hotmail.com
	/// Moved into Rainbow by Jakob Hansen, hansen3000@hotmail.com
	/// </summary>
	public class WebPartModule : PortalModuleControl
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl InnerFrame;
		protected System.Web.UI.WebControls.Label ContentPane;

		private void Page_Load(object sender, System.EventArgs e)
		{
			string ModuleIDent = this.ModuleID.ToString();
			string WebPartFile = Settings["WebPartFile"].ToString();
			if ((WebPartFile == null) || (WebPartFile == string.Empty))
			{
				ContentPane.Text = "<font color=red><b>WebPart file setting is missing!</b></font>";
				ContentPane.Visible = true;
				return;
			}
		 
			string filename = Server.MapPath(WebPartFile);
			WebPart partData = WebPartParser.Load(filename);
		 
			if (partData == null)
				return;
			
			if (partData.RequiresIsolation == 1)
			{
				InnerFrame.Attributes["src"] = partData.ContentLink;
				InnerFrame.Attributes["width"] = partData.Width;
				InnerFrame.Attributes["height"] = partData.Height;
				InnerFrame.Visible = true;
				return;
			}

			string content = ObtainWebPartContent(partData);
			if (content != null)
			{
				ContentPane.Text = UpdateContentWithTokens(content);
				ContentPane.Visible = true;
			}
		}

		
		string FetchNetworkContent(string url) 
		{
			WebRequest netRequest = WebRequest.Create(url);
			WebResponse netResponse = netRequest.GetResponse();
 
			try 
			{
				Stream receiveStream = netResponse.GetResponseStream();
				byte [] read = new Byte[512];
				string content = string.Empty;
				int bytes = 0;
 
				try
				{
					do 
					{
						bytes = receiveStream.Read(read, 0, 512);
						content += System.Text.Encoding.ASCII.GetString(read, 0, bytes);
					} while (bytes > 0);
				}
				finally
				{
					receiveStream.Close();
				}
 				return content;
			}
			catch
			{
				return null;
			}
		}

		
		string ObtainWebPartContent(WebPart partData) 
		{

			string content = null;

			if ((partData.ContentLink != null) && (partData.ContentLink.Length > 0))
				content = FetchNetworkContent(partData.ContentLink);
			if (content == null) 
				content = partData.Content;

			if ((partData.ContentType == 1) || (partData.ContentType == 2)) 
				return "<font color=red><b>Unsupported Web Part Content	Format</b></font>";

			if (partData.ContentType == 3) 
			{
				XmlDocument document = new XmlDocument();
				document.LoadXml(content);
 
				string xslContent = null;
				if ((partData.XSLLink != null) && (partData.XSLLink.Length > 0))
					xslContent = FetchNetworkContent(partData.XSLLink);
 
				if (xslContent == null)
					xslContent = partData.XSL;

 
				XslTransform transform = new XslTransform();
				StringWriter output = new StringWriter();
#if FW11
				transform.Load(new XmlTextReader(new StringReader(xslContent)), new XmlUrlResolver(), new System.Security.Policy.Evidence());
				transform.Transform(document, null, output, new XmlUrlResolver());
#else
				transform.Load(new XmlTextReader(new StringReader(xslContent)));
				transform.Transform(document, null, output);
#endif
				content = output.ToString();
			}

			return content;
		}


		string UpdateContentWithTokens(string content) 
		{

			int startIndex = 0;

			while ((startIndex = content.IndexOf("_WPQ_", startIndex)) != -1) 
			{
				content = content.Substring(0, startIndex) +
					this.ModuleID.ToString() + content.Substring(startIndex+5);
				startIndex+=5;
			}
			return content;
		}

		
		public WebPartModule() 
		{
			SettingItem setWebPartFile = new SettingItem(new StringDataType());
			setWebPartFile.Required = true;
			setWebPartFile.Value = "_Rainbow/WebParts/sales.dwp";
			setWebPartFile.Order = 1;
			this._baseSettings.Add("WebPartFile", setWebPartFile);
		}


		public override Guid GuidID 
		{
			get
			{
				return new Guid("{2502DB18-B580-4F90-8CB4-C15E6E531009}");
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);


		}
		#endregion
	}
}
