using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow.UI;
using Rainbow.Configuration;
using Rainbow.UI.WebControls;

namespace Rainbow.DesktopModules
{
	/// <summary>
	///		Default user control placed at the bottom of each administrative page.
	/// </summary>
	public abstract class DesktopFooter : UserControl
	{
        /// <summary>
        /// Placeholder for current control
        /// </summary>
        protected System.Web.UI.WebControls.PlaceHolder LayoutPlaceHolder;

		private void DesktopFooter_Load(object sender, System.EventArgs e)
		{
            string LayoutBasePage = "DesktopFooter.ascx";
			
            // Obtain PortalSettings from Current Context
            PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
			
			string footerPage = Rainbow.Settings.Path.WebPathCombine(portalSettings.PortalLayoutPath, LayoutBasePage);
			if(System.IO.File.Exists(Server.MapPath(footerPage)))
				LayoutPlaceHolder.Controls.Add(Page.LoadControl(footerPage));
//			try
//			{
//				//LayoutPlaceHolder.Controls.Add(Page.LoadControl(portalSettings.PortalLayoutPath + LayoutBasePage));
//				LayoutPlaceHolder.Controls.Add(Page.LoadControl(portalSettings.PortalLayoutPath + LayoutBasePage));
//			}
//			catch
//			{
//				//No footer available
//			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.Load += new System.EventHandler(this.DesktopFooter_Load);

        }
		#endregion
	}
}
