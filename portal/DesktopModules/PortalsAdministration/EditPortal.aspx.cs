using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Rainbow.Design;
using Rainbow.Security;
using Rainbow.Configuration;
using Esperantus;

namespace Rainbow.AdminAll
{
    /// <summary>
    /// EditPortal
    /// </summary>
    public class EditPortal : Rainbow.UI.EditItemPage
    {
        /// <summary>
        /// 
        /// </summary>
        protected System.Web.UI.WebControls.Label PortalIDField;
        /// <summary>
        /// 
        /// </summary>
        protected System.Web.UI.WebControls.Label AliasField;
        /// <summary>
        /// 
        /// </summary>
		protected System.Web.UI.WebControls.TextBox TitleField;
        /// <summary>
        /// 
        /// </summary>
        protected System.Web.UI.WebControls.Label PathField;
        /// <summary>
        /// 
        /// </summary>
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredTitle;
        /// <summary>
        /// 
        /// </summary>
		protected System.Web.UI.WebControls.Label ErrorMessage;
        /// <summary>
        /// 
        /// </summary>
		protected Rainbow.Configuration.SettingsTable EditTable;
		protected Esperantus.WebControls.Literal Literal1;
		protected Esperantus.WebControls.Literal Literal2;
		protected Esperantus.WebControls.Literal Literal3;
		protected Esperantus.WebControls.Literal Literal4;
		protected Esperantus.WebControls.Literal Literal5;

        int currentPortalID = -1;

        private void Page_Load(object sender, System.EventArgs e)
        {
            // Get portalID from querystring
            if (Request.Params["portalID"] != null)
            {
                currentPortalID = Int32.Parse(Request.Params["portalID"]);
            } 

            if(currentPortalID != -1)
            {
				// Remove cache for reload settings
				if (!Page.IsPostBack) 
					Rainbow.Settings.Cache.CurrentCache.Remove (Rainbow.Settings.Cache.Key.PortalSettings());
				
				// Obtain PortalSettings of this Portal
	            PortalSettings currentPortalSettings = new PortalSettings(currentPortalID);

                // If this is the first visit to the page, populate the site data
                if (!Page.IsPostBack) 
                {
                    PortalIDField.Text = currentPortalID.ToString();
                    TitleField.Text = currentPortalSettings.PortalName;
                    AliasField.Text = currentPortalSettings.PortalAlias;
                    PathField.Text = currentPortalSettings.PortalPath;
				}
				EditTable.DataSource = new SortedList(PortalSettings.GetPortalCustomSettings(currentPortalSettings.PortalID, PortalSettings.GetPortalBaseSettings(null)));
 				EditTable.DataBind();
				EditTable.ObjectID = currentPortalID;
           }
        }
 
		/// <summary>
		/// Set the module guids with free access to this page
		/// </summary>
		protected override ArrayList AllowedModules
		{
			get
			{
				ArrayList al = new ArrayList();
				al.Add ("366C247D-4CFB-451D-A7AE-649C83B05841");
				return al;
			}
		}

        /// <summary>
        /// OnUpdate
        /// </summary>
        protected override void OnUpdate(EventArgs e) 
        {
			base.OnUpdate(e);

			if(Page.IsValid)
			{
				//Update main settings and Tab info in the database
				new PortalsDB().UpdatePortalInfo(currentPortalID, TitleField.Text, PathField.Text, false);
				
				// Update custom settings in the database
				EditTable.ObjectID = currentPortalID;
				EditTable.UpdateControls();

				// Remove cache for reload settings before redirect
				Rainbow.Settings.Cache.CurrentCache.Remove (Rainbow.Settings.Cache.Key.PortalSettings());
				// Redirect back to calling page
				RedirectBackToReferringPage();
			}
        }

		private void EditTable_UpdateControl(object sender, Rainbow.Configuration.SettingsTableEventArgs e)
		{
            SettingsTable edt = (SettingsTable) sender;
			PortalSettings.UpdatePortalSetting(edt.ObjectID, e.CurrentItem.EditControl.ID, e.CurrentItem.Value);        
		}

		#region Web Form Designer generated code
        /// <summary>
        /// Raises the Init event.
        /// </summary>
        /// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();

			//Translations
			RequiredTitle.ErrorMessage = Esperantus.Localize.GetString("VALID_FIELD");
		
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() 
		{    
			this.EditTable.UpdateControl += new Rainbow.Configuration.UpdateControlEventHandler(this.EditTable_UpdateControl);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

    }
}

