using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow.Configuration;
using Rainbow.UI.WebControls;
using Esperantus;

namespace Rainbow
{
    /// <summary>
    /// Module print page
    /// </summary>
    public class PrintPage : Rainbow.UI.ViewItemPage
    {
		protected System.Web.UI.WebControls.PlaceHolder PrintPlaceHolder;

		private void Page_Load(object sender, System.EventArgs e)
		{
			foreach (ModuleSettings module in portalSettings.ActiveTab.Modules)
			{
				if (this.Request.Params["ModID"] != null && module.ModuleID == int.Parse(this.Request.Params["ModID"]))
				{
					// create an instance of the module
					PortalModuleControl myPortalModule = (PortalModuleControl) LoadControl(Rainbow.Settings.Path.ApplicationRoot + "/" + module.DesktopSrc);
					myPortalModule.PortalID = portalSettings.PortalID;                                  
					myPortalModule.ModuleConfiguration = module;

					// add the module to the placeholder
					PrintPlaceHolder.Controls.Add(myPortalModule);

					break;
				}
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