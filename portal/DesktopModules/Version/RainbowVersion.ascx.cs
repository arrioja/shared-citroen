using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.IO;
using Esperantus;
using Rainbow.Security;
using Rainbow.Configuration;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.Admin;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// Rainbow Version
	/// </summary>
	public class RainbowVersion : PortalModuleControl
	{
		protected System.Web.UI.WebControls.Label currentLanguage;
		protected System.Web.UI.WebControls.Label currentUILanguage;
		protected System.Web.UI.WebControls.Label VersionLabel;

		private void RainbowVersion_Load(object sender, System.EventArgs e)
		{
			VersionLabel.Text = PortalSettings.ProductVersion;
			currentLanguage.Text = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
			currentUILanguage.Text = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
		}
	
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{72C6F60A-50C4-4f20-8F89-3E8A27820557}");
			}
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Raises Init event
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
			this.Load += new System.EventHandler(this.RainbowVersion_Load);

		}
		#endregion
	}
}