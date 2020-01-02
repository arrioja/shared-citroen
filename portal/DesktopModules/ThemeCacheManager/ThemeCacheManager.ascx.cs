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
using Rainbow.Design;

namespace Rainbow.DesktopModules
{
	public class ThemeCacheManager : PortalModuleControl
	{
		protected System.Web.UI.WebControls.Button Button2;
		protected Esperantus.WebControls.Button ClearThemeButton;
		protected Esperantus.WebControls.Button ClearLayoutButton;
		protected Esperantus.WebControls.Literal Literal1;
		protected Esperantus.WebControls.Literal Literal2;
		protected Esperantus.WebControls.Literal msgLayout;
		protected Esperantus.WebControls.Literal msgTheme;
		protected System.Web.UI.WebControls.Button Button1;

		/// <summary>
		/// Admin Module
		/// </summary>
		public override bool AdminModule
		{
			get
			{
				return true;
			}
		}
	
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{48358161-D002-4d70-896A-49CF62D5110B}");
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
			this.ClearLayoutButton.Click += new System.EventHandler(this.ClearLayoutButton_Click);
			this.ClearThemeButton.Click += new System.EventHandler(this.ClearThemeButton_Click);

		}
		#endregion

		private void ClearThemeButton_Click(object sender, System.EventArgs e)
		{
			ThemeManager themeManager = new ThemeManager(portalSettings.PortalPath);
			themeManager.ClearCacheList();
			msgTheme.Visible = true;
		}

		private void ClearLayoutButton_Click(object sender, System.EventArgs e)
		{
			LayoutManager layoutManager = new LayoutManager(portalSettings.PortalPath);
			layoutManager.ClearCacheList();
			msgLayout.Visible = true;
		}
	}
}