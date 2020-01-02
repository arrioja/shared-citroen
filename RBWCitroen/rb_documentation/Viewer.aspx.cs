using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using Rainbow.Configuration;
using Rainbow.Helpers;
using Esperantus;


namespace Rainbow.Documentation
{
	public class Viewer : Rainbow.UI.Page
	{
		private const string cacheKeyRoot = "rb_help_transformer";
		private const string defaultXslt = "xsl/viewer.xslt";
		private const string defaultLocation = "Rainbow/";
		private const string defaultSource = "Rainbow";
		private const string sourceExtension = ".xml";

		protected System.Web.UI.WebControls.PlaceHolder ContentHolder;
		protected System.Web.UI.WebControls.Xml myXml;
       	
		private string ie7Script = string.Empty;
		public string Ie7Script
		{
			get
			{
				if ( ie7Script == string.Empty )
				{
					if ( ConfigurationSettings.AppSettings["Ie7Script"] != null && ConfigurationSettings.AppSettings["Ie7Script"].ToString() != string.Empty )
					{
						ie7Script = ConfigurationSettings.AppSettings["Ie7Script"].ToString();
					}
				}
				return ie7Script;
			}
			set{ie7Script = value;}
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
			// set current directory so paths are relative to this page location
			Environment.CurrentDirectory = this.Server.MapPath(this.TemplateSourceDirectory);

			// set localized Page Title
			this.TabTitle = Esperantus.Localize.GetString("TAB_TITLE_RAINBOW_HELP","Rainbow Help",this);
			// add the Help css
			this.ClearCssFileList();
			this.RegisterCssFile("help","css/help.css");
			this.RegisterCssFile("menu","css/mainmenu.css");

			if ( !this.IsAdditionalMetaElementRegistered("ie7") )
			{
				string _ie7 = string.Empty;
				string _ie7Part = string.Empty;

				foreach ( string _script in Ie7Script.Split(new char[]{';'}) )
				{
					_ie7Part = Rainbow.Settings.Path.WebPathCombine(Rainbow.Settings.Path.ApplicationRoot,_script);
					_ie7Part = string.Format("<!--[if lt IE 7]><script src=\"{0}\" type=\"text/javascript\"></script><![endif]-->",_ie7Part);
					_ie7 += _ie7Part + "\n";
				}
				this.RegisterAdditionalMetaElement("ie7",_ie7);
			}


			string loc = string.Empty;
			string src = string.Empty;
			string xslt = string.Empty;
			CultureInfo lang = this.portalSettings.PortalContentLanguage;
			CultureInfo defaultLang = this.portalSettings.PortalContentLanguage;
			CultureInfo fallbackLang = new CultureInfo(ConfigurationSettings.AppSettings["DefaultLanguage"]);
			XslTransform xt; 

			// grab the QueryString
			NameValueCollection qs = Request.QueryString;
			
			// Read Location
			if ( qs["loc"] != null && qs["loc"] != string.Empty )
				loc = qs["loc"]; 
			else
				loc = defaultLocation; 

			// Read Source
			if ( qs["src"] != null && qs["src"] != string.Empty )
				src = qs["src"];
			else
				src = defaultSource;

			// Read Culture
			if ( qs["lang"] != null && qs["lang"] != string.Empty )
			{
				try{lang = new CultureInfo(qs["lang"],false);}
				catch{}
			}

			// Read XSLT Stylesheet
			if ( qs["xslt"] != null 
				&& qs["xslt"] != string.Empty 
				&& File.Exists(qs["xslt"])
				)
				xslt = qs["xslt"]; 
			else
				xslt = defaultXslt; 
		
			// create language sequence
			ArrayList langSequence = new ArrayList(7);
			langSequence.Add(lang);
			if ( !lang.Equals(defaultLang) )
				langSequence.Add(defaultLang);
			if ( !defaultLang.Equals(fallbackLang) )
				langSequence.Add(fallbackLang);
			langSequence.Add(new CultureInfo(string.Empty));

			// create file sequence
			ArrayList fileSequence = new ArrayList(2);
			fileSequence.Add(Rainbow.Settings.Path.WebPathCombine(loc, src));
			fileSequence.Add(Rainbow.Settings.Path.WebPathCombine(defaultLocation, defaultSource));

			string filePath = string.Empty;
			bool found = false;
			bool asRequested = true;
			string languageReturned = string.Empty;
			
			// find a file
			foreach ( string _file in fileSequence )
			{
				foreach ( CultureInfo _language in langSequence )
				{
					filePath = string.Concat(_file,".",_language.Name,sourceExtension);
					filePath = filePath.Replace("..",".");
					if ( File.Exists(filePath) )
					{
						languageReturned = _language.Name;
						found = true;
						break;
					}
					if (_language.TwoLetterISOLanguageName.ToLower().Equals("en"))
						filePath = string.Concat(_file, sourceExtension);
					else
						filePath = string.Concat(_file,".",_language.TwoLetterISOLanguageName,sourceExtension);
					filePath = filePath.Replace("..",".");
					if ( File.Exists(filePath) )
					{
						languageReturned = _language.TwoLetterISOLanguageName;
						found = true;
						break;
					}
				}
				if ( found )
					break;
				else
					asRequested = false;
			}

			// if we found something to display
			if ( found )
			{
				// get the Transformer
				string transformerCacheKey = string.Concat(cacheKeyRoot,"_",xslt);
				if ( Context.Cache[transformerCacheKey] == null )
				{
					try
					{
						xt = new XslTransform();
						xslt = this.Server.MapPath(xslt);
						XmlUrlResolver xr = new XmlUrlResolver();
						xt.Load(xslt,xr);
						Context.Cache.Insert(transformerCacheKey, xt, new CacheDependency(xslt));
					}		
					catch(Exception ex)
					{
						Helpers.LogHelper.Logger.Log(Rainbow.Configuration.LogLevel.Error, "Failed in Help Transformer load - message was: " + ex.Message);
						throw new Exception("Failed in Help Transformer load - message was: " + ex.Message);
					}
				}
				else
				{
					xt = (XslTransform)Context.Cache[transformerCacheKey];
				}

				// create the ArgList
				XsltArgumentList xa = new XsltArgumentList();
				XslHelper xh = new XslHelper();
				xa.AddExtensionObject("urn:rainbow",xh);
				xa.AddParam("LanguageRequested",string.Empty,lang.Name);
				xa.AddParam("LanguageReturned",string.Empty,languageReturned);
				xa.AddParam("AsRequested",string.Empty,asRequested.ToString());
				//string rootFolder = Rainbow.Settings.Path.ApplicationRoot + "/rb_documentation/";
				//xa.AddParam("Location",string.Empty,rootFolder + loc);
				xa.AddParam("Location",string.Empty,loc);
				xa.AddParam("Title",string.Empty,this.TabTitle);
				string tocFile = string.Concat(loc.Substring(0,loc.IndexOf("/")),"/map.",languageReturned,sourceExtension);
				tocFile = tocFile.Replace("..",".");
				tocFile = string.Concat("../",tocFile);
				xa.AddParam("TOCfile",string.Empty,tocFile);
				xa.AddParam("Viewer",string.Empty,this.Request.Url.AbsolutePath);
				xa.AddParam("myRoot",string.Empty,loc.Substring(0,loc.IndexOf("/")));

				// load up the Xml control
				myXml.DocumentSource = filePath;
				myXml.Transform = xt;
				myXml.TransformArgumentList = xa;
			}
			else
			{
				using (Esperantus.WebControls.Literal errorMsg = new Esperantus.WebControls.Literal())
				{
					errorMsg.TextKey = "HELP_VIEWER_ERROR";
					errorMsg.Text = "Sorry - no help available";
					using (HtmlGenericControl container = new HtmlGenericControl("div"))
					{
						container.Attributes.Add("style","padding:3em;font-size:1.2em;text-align:center");
						container.Controls.Add(errorMsg);
						this.ContentHolder.Controls.Add(container);
					}
				}
			}
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Raises OnInitEvent
		/// </summary>
		/// <param name="e"></param>
		protected override void OnInit(EventArgs e)
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
