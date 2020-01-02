using System;
//added 7/27/2003 by Joe Audette to read proxy setting from the web.config
using System.Configuration;
//end addition
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Net;

using Rainbow.UI;
using Rainbow.UI.DataTypes;
using Rainbow.UI.WebControls;
using Rainbow.Configuration;


namespace Rainbow.DesktopModules 
{

	/// <summary>
	/// XmlFeed Module
	/// </summary>
	public class XmlFeed : PortalModuleControl 
	{
		protected System.Web.UI.WebControls.Xml xml1;

		/// <summary>
		/// The Page_Load event handler on this User Control obtains
		/// an xml document and xsl/t transform file location.
		/// It then sets these properties on an &lt;asp:Xml&gt; server control.
		/// 
 		/// Patch 11/11/2003 by Manu: Errors are logged.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			string xmlsrc = string.Empty;
			string xmlsrcType = Settings["XML Type"].ToString();
			if (xmlsrcType == "File")
				xmlsrc = Settings["XML File"].ToString();
			else
				xmlsrc = Settings["XML URL"].ToString();

            
			string xslsrc = string.Empty;
			string xslsrcType = Settings["XSL Type"].ToString();
			if (xslsrcType == "File")
				xslsrc = Settings["XSL File"].ToString();
			else
				xslsrc = Settings["XSL Predefined"].ToString();


			//Timeout
			int timeout = int.Parse(Settings["Timeout"].ToString());
			
			if ((xmlsrc != null) && (xmlsrc != string.Empty))
			{

				if (xmlsrcType == "File")
				{
					PortalUrlDataType pathXml = new PortalUrlDataType();
					pathXml.Value = xmlsrc;
					xmlsrc = pathXml.FullPath;

					if (File.Exists(Server.MapPath(xmlsrc)))
						xml1.DocumentSource = xmlsrc;
					else
						Controls.Add(new LiteralControl("<br><div class='error'>File " + xmlsrc + " not found.<br></div>"));
				}
				else 
				{
					try
					{
						Rainbow.Helpers.LogHelper.Log.Warn("XMLFeed - This should not done more than once in 30 minutes: '" + xmlsrc + "'");

						// handle on the remote ressource
						HttpWebRequest wr = (HttpWebRequest) WebRequest.Create(xmlsrc);

						if(ConfigurationSettings.AppSettings.Get("UseProxyServerForServerWebRequests") == "true" )
							wr.Proxy = PortalSettings.GetProxy();
						
						// set the HTTP properties
						wr.Timeout = timeout * 1000; // milliseconds to seconds
						// Read the response
						WebResponse resp = wr.GetResponse();
						// Stream read the response
						Stream stream = resp.GetResponseStream();
						// Read XML data from the stream
						XmlTextReader reader = new XmlTextReader(stream);
						// ignore the DTD
						reader.XmlResolver = null;
						// Create a new document object
						XmlDocument doc = new XmlDocument();
						// Create the content of the XML Document from the XML data stream
						doc.Load(reader);
						// the XML control to hold the generated XML document
						xml1.Document = doc;
					}
					catch(Exception ex)
					{
						// connectivity issues
						Controls.Add(new LiteralControl("<br><div class='error'>Error loading: " + xmlsrc + ".<br>" + ex.Message + "</div>"));
						Rainbow.Configuration.ErrorHandler.HandleException("Error loading: " + xmlsrc + ".", ex);
					}
				}
			}

			if (xslsrcType == "File")
			{
				PortalUrlDataType pathXsl = new PortalUrlDataType();
				pathXsl.Value = xslsrc;
				xslsrc = pathXsl.FullPath;
			}
			else
			{
				if(ConfigurationSettings.AppSettings.Get("XMLFeedXSLFolder") != null )
				{
					
					if(ConfigurationSettings.AppSettings.Get("XMLFeedXSLFolder").ToString().Length > 0 )
						xslsrc = ConfigurationSettings.AppSettings.Get("XMLFeedXSLFolder").ToString() + xslsrc;
					else
						xslsrc = "~/DesktopModules/XmlFeed/" + xslsrc;
				}
				else
				{
					xslsrc = "~/DesktopModules/XmlFeed/" + xslsrc;
				}

				if(!xslsrc.EndsWith(".xslt"))
					xslsrc += ".xslt";
			}

			if ((xslsrc != null) && (xslsrc != string.Empty))
			{
				if (File.Exists(Server.MapPath(xslsrc)))
					xml1.TransformSource = xslsrc;
				else
					Controls.Add(new LiteralControl("<br><div class='error'>File " + xslsrc + " not found.<br></div>"));
			}
		}

        
		/// <summary>
		/// Contsructor
		/// </summary>
		public XmlFeed()
		{
			SettingItem XMLsrcType = new SettingItem(new ListDataType("URL;File"));
			XMLsrcType.Required = true;
			XMLsrcType.Value = "URL";
			XMLsrcType.Order = 1;
			this._baseSettings.Add("XML Type", XMLsrcType);

			SettingItem XMLsrcUrl = new SettingItem(new UrlDataType());
			XMLsrcUrl.Required = false;
			XMLsrcUrl.Order = 2;
			this._baseSettings.Add("XML URL", XMLsrcUrl);

			SettingItem XMLsrcFile = new SettingItem(new PortalUrlDataType());
			XMLsrcFile.Required = false;
			XMLsrcFile.Order = 3;
			this._baseSettings.Add("XML File",  XMLsrcFile);

			SettingItem XSLsrcType = new SettingItem(new ListDataType("Predefined;File"));
			XSLsrcType.Required = true;
			XSLsrcType.Value = "Predefined";
			XSLsrcType.Order = 4;
			this._baseSettings.Add("XSL Type", XSLsrcType);

			
			ListDataType xsltFileList = new ListDataType(this.GetXSLListForFeedTransformations());
			SettingItem XSLsrcPredefined = new SettingItem(xsltFileList);
			XSLsrcPredefined.Required = true;
			XSLsrcPredefined.Value = "RSS91";
			XSLsrcPredefined.Order = 5;
			this._baseSettings.Add("XSL Predefined", XSLsrcPredefined);

			SettingItem XSLsrcFile = new SettingItem(new PortalUrlDataType());
			XSLsrcFile.Required = false;
			XSLsrcFile.Order = 6;
			this._baseSettings.Add("XSL File", XSLsrcFile);
		
			SettingItem Timeout = new SettingItem(new IntegerDataType());
			Timeout.Required = true;
			Timeout.Order = 7;
			Timeout.Value = "15";
			this._baseSettings.Add("Timeout", Timeout);
		}

		
		/// <summary>
		/// Author:		Joe Audette
		/// Added:		7/31/2003
		/// Allows you to add stylesheets for new feed formats without recompiling.
		/// Any xslt stylesheets placed in the folder specified in the web.config willshow up
		/// in the dropdown list
		/// 
		/// Patch 11/11/2003 by Manu: Errors are logged.
		/// </summary>
		/// <returns>FileInfo[]</returns>
		public  FileInfo[] GetXSLListForFeedTransformations()
		{
			string xsltPath = string.Empty;
			
			if (ConfigurationSettings.AppSettings["XMLFeedXSLFolder"] != null && ConfigurationSettings.AppSettings["XMLFeedXSLFolder"].Length > 0)
				xsltPath = ConfigurationSettings.AppSettings["XMLFeedXSLFolder"];
			else
			{
				//this will default to the xmlfeed folder where the .xslt files are located by default
				xsltPath = System.Web.HttpContext.Current.Server.MapPath(this.TemplateSourceDirectory);
			}

			try
			{
				if(Directory.Exists(xsltPath))
				{
					DirectoryInfo dir = new DirectoryInfo(xsltPath);
					return dir.GetFiles("*.xslt");
				}
				else
				{
					Rainbow.Helpers.LogHelper.Log.Warn("Default XSLT location not found: '" + xsltPath + "'");
				}
			}
			catch(Exception ex)
			{	
				Rainbow.Configuration.ErrorHandler.HandleException("XSLT location not found: " + xsltPath, ex);
			}
			return null;
		}

		public override Guid GuidID 
		{
			get
			{
				return new Guid("{2502DB18-B580-4F90-8CB4-C15E6E531020}");
			}
		}

		#region Web Form Designer generated code
		/// <summary>
		/// On init
		/// </summary>
		/// <param name="e"></param>
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
