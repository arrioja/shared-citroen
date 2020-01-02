using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow;
using Rainbow.Admin;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// Summary description for Property Page
	/// </summary>
	[History("Jes1111", "2003/03/04", "Cache flushing now handled by inherited page")]
	public class PagePropertyPage : Rainbow.UI.PropertyPage
	{
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolderButtons;
        protected System.Web.UI.WebControls.Panel EditPanel;
        protected Esperantus.WebControls.HyperLink adminPropertiesButton;
		protected Esperantus.WebControls.Literal Literal1;
		protected System.Web.UI.WebControls.PlaceHolder AddEditControl;
		protected Rainbow.Configuration.SettingsTable EditTable;
        protected Esperantus.WebControls.LinkButton saveAndCloseButton;

		#region Web Form Designer generated code
        /// <summary>
        /// On init
        /// </summary>
        /// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			//Controls must be created here
			updateButton = new Esperantus.WebControls.LinkButton();
			updateButton.CssClass = "CommandButton";
			PlaceHolderButtons.Controls.Add(updateButton);


			PlaceHolderButtons.Controls.Add(new LiteralControl("&#160;"));
			saveAndCloseButton = new Esperantus.WebControls.LinkButton();
			saveAndCloseButton.TextKey = "SAVE_AND_CLOSE";
			saveAndCloseButton.Text = "Save and close";
			saveAndCloseButton.CssClass = "CommandButton";
			PlaceHolderButtons.Controls.Add(saveAndCloseButton);
			this.saveAndCloseButton.Click += new System.EventHandler(this.saveAndCloseButton_Click);

			// Removed by Mario Endara <mario@softworks.com.uy> (2004/11/04)
//			if (Rainbow.Security.PortalSecurity.IsInRoles("Admins"))
//			{
				adminPropertiesButton = new Esperantus.WebControls.HyperLink();
				adminPropertiesButton.TextKey = "MODULESETTINGS_BASE_SETTINGS";
				adminPropertiesButton.Text = "Edit base settings";
				adminPropertiesButton.CssClass = "CommandButton";
				adminPropertiesButton.NavigateUrl = HttpUrlBuilder.BuildUrl("~/DesktopModules/Admin/ModuleSettings.aspx", TabID, ModuleID);
				
				PlaceHolderButtons.Controls.Add(new LiteralControl("&#160;"));
				PlaceHolderButtons.Controls.Add(adminPropertiesButton);
//			}

			PlaceHolderButtons.Controls.Add(new LiteralControl("&#160;"));

			cancelButton = new Esperantus.WebControls.LinkButton();
			cancelButton.CssClass = "CommandButton";
			PlaceHolderButtons.Controls.Add(cancelButton);

			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.EditTable.UpdateControl += new Rainbow.Configuration.UpdateControlEventHandler(this.EditTable_UpdateControl);
			this.Load += new System.EventHandler(this.PagePropertyPage_Load);

		}
		#endregion

        private void PagePropertyPage_Load(object sender, System.EventArgs e)
        {
			//We reset cache before dispay page to ensure dropdown shows actual data
			//by Pekka Ylenius
			Rainbow.Settings.Cache.CurrentCache.Remove(Rainbow.Settings.Cache.Key.ModuleSettings(ModuleID));
            EditTable.DataSource = new SortedList(moduleSettings);
            EditTable.DataBind();
        }

		private void saveAndCloseButton_Click(object sender, System.EventArgs e)
		{
			OnUpdate(e);
			if (Page.IsValid == true) 
				Response.Redirect(HttpUrlBuilder.BuildUrl("~/Default.aspx", TabID));
        }    
   
		/// <summary>
		/// Persists the changes to database
		/// </summary>
        protected override void OnUpdate(EventArgs e) 
        {
			base.OnUpdate(e);

            // Only Update if Input Data is Valid
            if (Page.IsValid == true) 
            {
                // Update settings in the database
                EditTable.UpdateControls();
            }
        }

        protected override void OnCancel(EventArgs e) 
        {
			Response.Redirect(HttpUrlBuilder.BuildUrl("~/Default.aspx", TabID));
		}

        private void EditTable_UpdateControl(object sender, Rainbow.Configuration.SettingsTableEventArgs e)
        {
            ModuleSettings.UpdateModuleSetting(ModuleID, e.CurrentItem.EditControl.ID, e.CurrentItem.Value);
        }
	}
}