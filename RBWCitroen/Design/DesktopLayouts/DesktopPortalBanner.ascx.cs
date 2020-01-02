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
    /// Default user control placed on top of each administrative page
    /// </summary>
    [History("john", "2003/03/15", "Some mods")]
    [History("manu", "2002/11/18", "Testing attributes")]
	[History("Jes1111", "2003/03/09", "Retrieve ShowTabs attribute and pass into new portalSettings.ShowTabs property")]
    public abstract class DesktopPortalBanner : UserControl
    {
        /// <summary>
        /// Placeholder for current control
        /// </summary>
        protected System.Web.UI.WebControls.PlaceHolder LayoutPlaceHolder;

		// jes1111 
		public bool ShowTabs = true;

        #region Web Form Designer generated code
        /// <summary>
        /// Raises OnInitEvent
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();

            base.OnInit(e);
        }
        private void InitializeComponent() 
        {
			this.Load += new System.EventHandler(this.DesktopPortalBanner_Load);

		}
		#endregion

		[History("bja@reedtek.com", "2003/05/09", "Validate the control being brought in")]
        private void DesktopPortalBanner_Load(object sender, System.EventArgs e)
        {
            
            string LayoutBasePage = "DesktopPortalBanner.ascx";
			
			// Obtain PortalSettings from Current Context
            PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
			
			// jes1111 
			portalSettings.ShowTabs = ShowTabs;

			// [START] file path -- bja@reedtek.com
			//
			// Validate that the layout file is present. I have found
			// that sometimes they go away in different releases. So let's check
			//string filepath = portalSettings.PortalLayoutPath + LayoutBasePage;
			string filepath = Rainbow.Settings.Path.WebPathCombine(portalSettings.PortalLayoutPath, LayoutBasePage);

			// does it exsists
			if (System.IO.File.Exists(Server.MapPath(filepath)))
				LayoutPlaceHolder.Controls.Add(Page.LoadControl(filepath));
			else 
			{
				// create an exception
				System.Exception ex = new System.Exception("Portal cannot find layout ('" + filepath + "')");
				// go log/handle it
				ErrorHandler.HandleException(ex);
			}
			// [END] file path -- bja@reedtek.com
        }
    }
}
