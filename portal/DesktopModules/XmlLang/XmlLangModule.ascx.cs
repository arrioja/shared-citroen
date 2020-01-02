using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Esperantus;
using Rainbow.Configuration;
using Rainbow.Helpers;

namespace Rainbow.DesktopModules 
{

    /// <summary>
    /// XML Language Module v1.1 - based (loosely) on the original XML module with added
    /// support for content language selection via the PortalContentLanguage
    /// property in PortalSettings. By Jes1111
    /// Now supports "Print this..." and "Email this..." buttons
    /// </summary>
    public class XmlLangModule : PortalModuleControl 
    {
        protected System.Web.UI.WebControls.PlaceHolder ContentHolder;

        /// <summary>
        /// The Page_Load event handler on this User Control uses
        /// the Portal configuration system to obtain an xml document
        /// and xsl/t transform file location.  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e) 
        {
			XslTransform xs;
			XPathDocument xd;
			XsltArgumentList xa = new XsltArgumentList();
			XslHelper xh = new XslHelper();
			StringBuilder sb = new StringBuilder();
			TextWriter tw = new StringWriter(sb);
			PortalUrlDataType pt;
            
            pt = new PortalUrlDataType();
            pt.Value = Settings["XMLsrc"].ToString();
            string xmlsrc = Server.MapPath(pt.FullPath);
			pt = new PortalUrlDataType();
			pt.Value = Settings["XSLsrc"].ToString();
			string xslsrc = Server.MapPath(pt.FullPath);

			if (   (xmlsrc != null) && (xmlsrc != string.Empty)
				&& (xslsrc != null) && (xslsrc != string.Empty)
				&& File.Exists(xmlsrc)
				&& File.Exists(xslsrc) ) 
			{
				xd = new XPathDocument(xmlsrc);
				xs = new XslTransform();
				xs.Load(xslsrc);
				xa.AddParam("Lang",string.Empty,this.portalSettings.PortalContentLanguage.Name.ToLower());
				xa.AddExtensionObject("urn:rainbow",xh);
#if FW11
				xs.Transform(xd, xa, tw, new XmlUrlResolver());
#else
				xs.Transform(xd, xa, tw);
#endif
				this.Content = sb.ToString();
				this.ContentHolder.Controls.Add(new LiteralControl(this.Content.ToString()));

				this.ModuleConfiguration.CacheDependency.Add(xslsrc);
				this.ModuleConfiguration.CacheDependency.Add(xmlsrc);
			}
        }


        /// <summary>
        /// Constructor
        /// </summary>
        public XmlLangModule()
        {
			SettingItem XMLsrc = new SettingItem(new PortalUrlDataType());
			XMLsrc.Required = true;
			XMLsrc.Order = 1;
			this._baseSettings.Add("XMLsrc", XMLsrc);

			SettingItem XSLsrc = new SettingItem(new PortalUrlDataType());
			XSLsrc.Required = true;
			XSLsrc.Order = 2;
			this._baseSettings.Add("XSLsrc", XSLsrc);

			this.SupportsWorkflow = false;
			this.SupportsBack = false;
        }

        public override Guid GuidID 
        {
            get
            {
                return new Guid("{E16DD121-267E-4268-A497-BDA6314E21A5}");
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
