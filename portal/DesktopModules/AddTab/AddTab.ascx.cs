using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Rainbow.Configuration;
using Esperantus;

namespace Rainbow.DesktopModules.AddModule
{
	/// <summary>
	/// This module has been built by John Mandia (www.whitelightsolutions.com)
	/// It allows administrators to give permission to selected roles to add modules to pages
	/// </summary>
	public class AddTab : PortalModuleControl
	{
		#region Controls
		protected Esperantus.WebControls.Literal tabParentLabel;
		protected Esperantus.WebControls.Literal tabVisibleLabel;
		protected System.Web.UI.WebControls.DropDownList PermissionDropDown;
		protected Esperantus.WebControls.Literal tabTitleLabel;
		protected System.Web.UI.WebControls.TextBox TabTitleTextBox;
		protected Esperantus.WebControls.LinkButton AddTabButton;
		protected Esperantus.WebControls.Label lblErrorNotAllowed;
		protected System.Web.UI.WebControls.DropDownList parentTabDropDown;
		protected Esperantus.WebControls.Literal lbl_ShowMobile;
		protected System.Web.UI.WebControls.CheckBox cb_ShowMobile;
		protected Esperantus.WebControls.Literal lbl_MobileTabName;
		protected System.Web.UI.WebControls.TextBox tb_MobileTabName;
		protected Esperantus.WebControls.Literal Literal1;
		protected System.Web.UI.WebControls.RadioButtonList rbl_JumpToTab;
		protected Esperantus.WebControls.Literal moduleError;
		#endregion

		#region Page Load
		/// <summary>
		/// The Page_Load event handler on this User Control is used to
		/// load all the modules that are currently on this tab
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e)
		{
			// If first visit to the page, update all entries
			if (Page.IsPostBack == false) 
			{
				BindData();
				TabTitleTextBox.Text = Esperantus.Localize.GetString("TAB_NAME", "New Tab Name");
			}
		}
		#endregion
	
		#region Methods
		/// <summary>
		/// The BindData helper method is used to update the tab's
		/// layout panes with the current configuration information
		/// </summary>
		private void BindData() 
		{
			// Populate the "ParentTab" Data
			TabsDB t = new TabsDB();
			SqlDataReader dr = t.GetTabsParent(portalSettings.PortalID, TabID);
			parentTabDropDown.DataSource = dr;
			parentTabDropDown.DataBind();
			dr.Close(); //by Manu, fixed bug 807858

			//Preselects current tab as parent
// Comment out old code for Grischa Brockhaus 
//			int currentTab = this.portalSettings.ActiveTab.TabID;
//			if (parentTabDropDown.Items.FindByValue(parentTabDropDown.ToString()) != null)
//				parentTabDropDown.Items.FindByValue(parentTabDropDown.ToString()).Selected = true;

			// Changes for Grischa Brockhaus copied by Mike Stone 7/1/2005
			int currentTab = this.portalSettings.ActiveTab.TabID; 
			if (parentTabDropDown.Items.FindByValue(currentTab .ToString()) != null) 
				parentTabDropDown.Items.FindByValue(currentTab .ToString()).Selected = true; 

			// Translate
			if (parentTabDropDown.Items.FindByText(" ROOT_LEVEL") != null)
				parentTabDropDown.Items.FindByText(" ROOT_LEVEL").Text = Esperantus.Localize.GetString("ROOT_LEVEL", "Root Level", parentTabDropDown);
		}
		#endregion

		#region Events

		/// <summary>
		/// The AddTabButton_Click server event handler 
		/// on this page is used to add a new portal module 
		/// into the tab
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddTabButton_Click(Object sender, EventArgs e) 
		{
			if(Page.IsValid)
			{
				// Hide error message in case there was a previous error.
				moduleError.Visible = false;

				// This allows the user to pick what type of people can view the module being added.
				// If Authorised Roles is selected from the dropdown then every role that has view permission for the
				// Add Role module will be added to the view permissions of the module being added.
				string viewPermissionRoles = PermissionDropDown.SelectedValue.ToString();
				if(viewPermissionRoles == "Authorised Roles")
				{
					viewPermissionRoles = Rainbow.Security.PortalSecurity.GetViewPermissions(this.ModuleID);
				}

				try
				{
					// New tabs go to the end of the list
					TabItem t = new TabItem();
					t.Name = TabTitleTextBox.Text;
					t.ID = -1;
					t.Order = 990000;
					
					// Get Parent Tab Id Convert only once used many times
					int parentTabID = int.Parse(this.parentTabDropDown.SelectedValue);


					// write tab to database
					TabsDB tabs = new TabsDB();
					//t.ID = tabs.AddTab(portalSettings.PortalID, t.Name, viewPermissionRoles, t.Order);
					
					// Changed to use new method in TabsDB.cs now all parms are posible 
					// By Mike Stone (mstone@kaskaskia.edu) - 30/12/2004
					t.ID = tabs.AddTab(portalSettings.PortalID, parentTabID, t.Name, t.Order, viewPermissionRoles, cb_ShowMobile.Checked , tb_MobileTabName.Text);
																																
					//TODO.. the only way to update a parent id is throught update :S
					// Changed to AddTab method now supports the parm
					// Mike Stone - 30/12/2004
					//tabs.UpdateTab(portalSettings.PortalID, t.ID, parentTabID, t.Name, t.Order, viewPermissionRoles, t.Name, false);

					//Invalidate cache
					// Changed to access form directly 
					// mike stone - 30/12/2004
					//   Cache.Remove(Rainbow.Settings.Cache.Key.TabSettings(parentTabID));
					// Copied to here 29/12/2004 by Mike Stone
					Rainbow.Settings.Cache.CurrentCache.RemoveAll("_TabNavigationSettings_");
					System.Diagnostics.Debug.WriteLine("************* Remove " + Rainbow.Settings.Cache.Key.TabSettings(parentTabID));

					//Jump to Page option
					string returnTab = string.Empty;
					if (rbl_JumpToTab.SelectedValue.ToString() == "Yes") 
					{
						// Redirect to New Page/Tab - Mike Stone 30/12/2004
						returnTab = Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopDefault.aspx", t.ID, "SelectedTabID=" + t.ID.ToString());
					}
					else
					{
						// Do NOT Redirect to New Form - Mike Stone 30/12/2004
						// I guess every .aspx page needs to have a module tied to it. 
						// or you will get an error about edit access denied. 
						returnTab = Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopDefault.aspx", int.Parse(Request.QueryString["tabID"]), "SelectedTabID=" + t.ID.ToString());
					}
					Response.Redirect(returnTab);
				}
				catch(Exception ex)
				{
					moduleError.Visible = true;
					//Helpers.LogHelper.Logger.Log(Rainbow.Configuration.LogLevel.Error, "There was an error with the Add Tab Module while trying to add a new tab.",ex);
					Rainbow.Configuration.ErrorHandler.HandleException("There was an error with the Add Tab Module while trying to add a new tab.",ex);
					return;
				}
				// Reload page to pick up changes
				Response.Redirect(Request.RawUrl,false);
			}
		}
		#endregion

		#region General Implementation
		/// <summary>
		/// Gets the GUID for this module.
		/// </summary>
		/// <value></value>
		public override Guid GuidID
		{
			get
			{
				return new Guid("{A1E37A0F-4EE9-4b83-9482-43466FC21E08}");
			}
		}

		/// <summary>
		/// Marks This Module To Be An Admin Module
		/// </summary>
		public override bool AdminModule
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// Public constructor. Sets base settings for module.
		/// </summary>
		public AddTab()
		{    
		}
		#endregion

		#region Web Form Designer generated code
        /// <summary>
        /// Raises OnInitEvent
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			InitializeComponent();

			// Call base init procedure
			base.OnInit(e);
		}

		/// <summary>
		/// Initializes the component.
		/// </summary>
		private void InitializeComponent()
		{
			this.AddTabButton.Click += new System.EventHandler(this.AddTabButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
