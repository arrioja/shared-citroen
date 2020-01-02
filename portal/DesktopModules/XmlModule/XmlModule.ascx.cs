using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Esperantus;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules 
{

    /// <summary>
    /// Xml Module
    /// </summary>
    public class XmlModule : PortalModuleControl 
    {
        protected System.Web.UI.WebControls.Xml xml1;

        /// <summary>
        /// The Page_Load event handler on this User Control uses
        /// the Portal configuration system to obtain an xml document
        /// and xsl/t transform file location.  It then sets these
        /// properties on an &lt;asp:Xml&gt; server control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e) 
        {
            PortalUrlDataType pt;
            
            pt = new PortalUrlDataType();
            pt.Value = Settings["XMLsrc"].ToString();
            string xmlsrc = pt.FullPath;

            if ((xmlsrc != null) && (xmlsrc != string.Empty)) 
            {
                if  (System.IO.File.Exists(Server.MapPath(xmlsrc))) 
                {
                    xml1.DocumentSource = xmlsrc;
					// Change - 28/Feb/2003 - Jeremy Esland
					// Builds cache dependency files list
					this.ModuleConfiguration.CacheDependency.Add(Server.MapPath(xmlsrc));
                }
                else 
                {
                    Controls.Add(new LiteralControl("<br>" + "<span class='Error'>" + Esperantus.Localize.GetString("FILE_NOT_FOUND").Replace("%1%", xmlsrc) + "<br>"));
                }
            }

            pt = new PortalUrlDataType();
            pt.Value = Settings["XSLsrc"].ToString();
            string xslsrc = pt.FullPath;

            if ((xslsrc != null) && (xslsrc != string.Empty)) 
            {
                if  (System.IO.File.Exists(Server.MapPath(xslsrc))) 
                {
                    xml1.TransformSource = xslsrc;
					// Change - 28/Feb/2003 - Jeremy Esland
					// Builds cache dependency files list
					this.ModuleConfiguration.CacheDependency.Add(Server.MapPath(xslsrc));
                }
                else 
                {
                    Controls.Add(new LiteralControl("<br>" + "<span class='Error'>" + Esperantus.Localize.GetString("FILE_NOT_FOUND").Replace("%1%", xslsrc) + "<br>"));
                }
            }
        }

        /// <summary>
        /// Contsructor
        /// </summary>
        public XmlModule()
        {
			SettingItem XMLsrc = new SettingItem(new PortalUrlDataType());
			XMLsrc.Required = true;
			XMLsrc.Order = 1;
			this._baseSettings.Add("XMLsrc", XMLsrc);

			SettingItem XSLsrc = new SettingItem(new PortalUrlDataType());
			XSLsrc.Required = true;
			XSLsrc.Order = 2;
			this._baseSettings.Add("XSLsrc", XSLsrc);
        }

        public override Guid GuidID 
        {
            get
            {
                return new Guid("{BE224332-03DE-42B7-B127-AE1F1BD0FADC}");
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
