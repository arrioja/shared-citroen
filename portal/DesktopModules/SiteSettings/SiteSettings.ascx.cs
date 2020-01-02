using System;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow.Design;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.Security;
using Rainbow.Configuration;
using Esperantus;

namespace Rainbow.DesktopModules
{
    public class SiteSettings : PortalModuleControl 
    {
		protected Rainbow.Configuration.SettingsTable EditTable;
		protected System.Web.UI.WebControls.TextBox siteName;
		protected Esperantus.WebControls.Literal site_title;
		protected Esperantus.WebControls.Literal site_path;
		protected System.Web.UI.WebControls.Label sitePath;

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

        /// <summary>
        /// The Page_Load server event handler on this user control is used
        /// to populate the current site settings from the config system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e) 
        {
            // If this is the first visit to the page, populate the site data
            if (Page.IsPostBack == false) 
            {
				//We flush cache for enable correct localization of items
				PortalSettings.FlushBaseSettingsCache(portalSettings.PortalPath);

                siteName.Text = portalSettings.PortalName;
                sitePath.Text = portalSettings.PortalPath;
			}
			EditTable.DataSource = new SortedList(portalSettings.CustomSettings);
			EditTable.DataBind();
        }
		        
        /// <summary>
        /// Is used to update the Site Settings within the Portal Config System
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUpdate(EventArgs e) 
        {
			// Flush the cache for recovery the changes. jviladiu@portalServices.net (30/07/2004)
			PortalSettings.FlushBaseSettingsCache(portalSettings.PortalPath);
			//Call base
			base.OnUpdate(e);

            // Only Update if Input Data is Valid
            if (Page.IsValid == true) 
            {
                //Update main settings and Tab info in the database
                new PortalsDB().UpdatePortalInfo(portalSettings.PortalID, siteName.Text, sitePath.Text, false);

                // Update custom settings in the database
                EditTable.UpdateControls();

                // Redirect to this site to refresh
                Response.Redirect(Request.RawUrl);
            }
        }

        private void EditTable_UpdateControl(object sender, Rainbow.Configuration.SettingsTableEventArgs e)
        {
            PortalSettings.UpdatePortalSetting(portalSettings.PortalID, e.CurrentItem.EditControl.ID, e.CurrentItem.Value);        
        }

		public override Guid GuidID 
		{
			get
			{
				return new Guid("{EBBB01B1-FBB5-4E79-8FC4-59BCA1D0554E}");
			}
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Raises OnInit Event
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

        private void InitializeComponent() 
        {
			this.EditTable.UpdateControl += new Rainbow.Configuration.UpdateControlEventHandler(this.EditTable_UpdateControl);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
    }
}