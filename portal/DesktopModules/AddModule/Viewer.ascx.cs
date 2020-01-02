using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Rainbow.Configuration;
using Esperantus;
using Rainbow.Security;
using Rainbow;

namespace Rainbow.DesktopModules.AddModule
{
	/// <summary>
	/// This module has been built by John Mandia (www.whitelightsolutions.com)
	/// It allows administrators to give permission to selected roles to add modules to pages
	/// </summary>
	public class Viewer : PortalModuleControl
	{
		#region Controls
		protected System.Web.UI.WebControls.DropDownList moduleType;
		protected Esperantus.WebControls.Literal addmodule;
		protected System.Web.UI.WebControls.TextBox moduleTitle;
		protected Esperantus.WebControls.LinkButton AddModuleBtn;
		protected System.Web.UI.WebControls.DropDownList paneLocation;
		protected Esperantus.WebControls.Literal moduleNameLabel;
		protected Esperantus.WebControls.Literal moduleTitleLabel;
		protected Esperantus.WebControls.Literal moduleLocationLabel;
		protected Esperantus.WebControls.Literal moduleVisibleLabel;
		protected System.Web.UI.WebControls.DropDownList viewPermissions;
		protected Esperantus.WebControls.Literal moduleError;
		protected System.Web.UI.WebControls.HyperLink AddModuleHelp;
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
				SetHelpPath();
				SetModuleName();
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
			
			// Populate the "Add Module" Data
			ModulesDB m = new ModulesDB();

			SqlDataReader drCurrentModuleDefinitions = m.GetCurrentModuleDefinitions(portalSettings.PortalID);
			
			try
			{
//				if(this.ArePropertiesEditable)
//				{
//					while(drCurrentModuleDefinitions.Read())
//					{
//						moduleType.Items.Add(new ListItem(drCurrentModuleDefinitions["FriendlyName"].ToString(),drCurrentModuleDefinitions["ModuleDefID"].ToString() + "|" + GetHelpPath(drCurrentModuleDefinitions["DesktopSrc"].ToString())));
//					}
//				}
//				else
//				{
					while(drCurrentModuleDefinitions.Read())
					{
						// Added by Mario Endara <mario@softworks.com.uy> 2004/11/04
						// only users members of the "Amins" role can add Admin modules to a Tab
						if( PortalSecurity.IsInRoles("Admins") == true || 
							!(bool.Parse(drCurrentModuleDefinitions["Admin"].ToString())))
						{
							moduleType.Items.Add(new ListItem(drCurrentModuleDefinitions["FriendlyName"].ToString(),drCurrentModuleDefinitions["ModuleDefID"].ToString() + "|" + GetHelpPath(drCurrentModuleDefinitions["DesktopSrc"].ToString())));
						}
					}
//				}
			}
			finally
			{
				drCurrentModuleDefinitions.Close();
			}
		}


		/// <summary>
		/// Gets the folder help path.
		/// </summary>
		/// <param name="desktopSrc">Desktop SRC.</param>
		/// <returns>The name of the help folder in the correct format</returns>
		private string GetHelpPath(string desktopSrc)
		{
			string helpPath = desktopSrc.Replace(".","_").ToString();
			return "Rainbow/" + helpPath;
		}

		private void SetModuleName()
		{
			//by Manu, set title like module name
			if (moduleType.SelectedItem != null)
				moduleTitle.Text = moduleType.SelectedItem.Text;
		}

		/// <summary>
		/// Sets the help path. This method checks to see whether the currently selected module has a
		/// help file associated with it. If it does then it shows the help icon. If it doesn't then it
		/// hides it.
		/// </summary>
		private void SetHelpPath()
		{
			string folderName = moduleType.SelectedValue.ToString();
			int start = folderName.IndexOf("|");
			folderName = folderName.Substring(start + 1);
			int fileNameStart = folderName.LastIndexOf("/");
			string fileName = folderName.Substring(fileNameStart + 1);
			string completePath = folderName + "/" + fileName; 
	

			if(System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(Rainbow.Settings.Path.ApplicationRoot + "/rb_documentation/" + completePath + ".xml")))
			{
				AddModuleHelp.Visible = true;
				string javaScript = "HelpWindow=window.open('"
					+ Rainbow.Settings.Path.ApplicationRoot + "/rb_documentation/Viewer.aspx?loc=" + folderName + "&src=" + fileName
					+ "','HelpWindow','toolbar=no,location=no,directories=no,status=no,menubar=yes,scrollbars=yes,resizable=yes,width=640,height=480,left=15,top=15'); return false;";
				AddModuleHelp.Attributes.Add("onclick", javaScript);
				AddModuleHelp.Attributes.Add("style", "cursor: hand;");
				AddModuleHelp.NavigateUrl = string.Empty;
				AddModuleHelp.ImageUrl = this.CurrentTheme.GetImage("Buttons_Help", "Help.gif").ImageUrl;
				AddModuleHelp.ToolTip = moduleType.SelectedItem.Text.ToString() + " Help";
			}
			else
			{
				AddModuleHelp.Visible = false;
			}
		}
		#endregion

		#region Events
		/// <summary>
		/// Each time the module selection is changed it checks to see if that particular module has a help file.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void moduleType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SetHelpPath();
			SetModuleName();
		}

		/// <summary>
		/// The AddModule_Click server event handler 
		/// on this page is used to add a new portal module 
		/// into the tab
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddModule_Click(Object sender, EventArgs e) 
		{
			if(this.TabID !=0)
			{
				// All new modules go to the end of the contentpane
				string selectedModule = moduleType.SelectedItem.Value.ToString();
				int start = selectedModule.IndexOf("|");
				int moduleID = Convert.ToInt32(selectedModule.Substring(0,start).Trim());
			
				// Hide error message in case there was a previous error.
				moduleError.Visible = false;

				// This allows the user to pick what type of people can view the module being added.
				// If Authorised Roles is selected from the dropdown then every role that has view permission for the
				// Add Role module will be added to the view permissions of the module being added.
				string viewPermissionRoles = viewPermissions.SelectedValue.ToString();
				if(viewPermissionRoles == "Authorised Roles")
				{
					viewPermissionRoles = Rainbow.Security.PortalSecurity.GetViewPermissions(this.ModuleID);
				}

				try
				{
					ModuleItem m = new ModuleItem();
					m.Title = moduleTitle.Text;
					m.ModuleDefID = moduleID;
					m.Order = 999;
        			
					// save to database
					ModulesDB _mod = new ModulesDB(); 
					m.ID = _mod.AddModule(TabID, m.Order, paneLocation.SelectedValue.ToString(), m.Title, m.ModuleDefID, 0,Rainbow.Security.PortalSecurity.GetEditPermissions(this.ModuleID), viewPermissionRoles,Rainbow.Security.PortalSecurity.GetAddPermissions(this.ModuleID), Rainbow.Security.PortalSecurity.GetDeletePermissions(this.ModuleID),Rainbow.Security.PortalSecurity.GetPropertiesPermissions(this.ModuleID), Rainbow.Security.PortalSecurity.GetMoveModulePermissions(this.ModuleID), Rainbow.Security.PortalSecurity.GetDeleteModulePermissions(this.ModuleID), false,Rainbow.Security.PortalSecurity.GetPublishPermissions(this.ModuleID), false,false,false);
				}
				catch(Exception ex)
				{
					moduleError.Visible = true;
					Rainbow.Configuration.ErrorHandler.HandleException("There was an error with the Add Module Module while trying to add a new module.",ex);
				}
				finally
				{
					if(moduleError.Visible == false)
					{
						// Reload page to pick up changes
						Response.Redirect(Request.RawUrl,false);
					}
				}
			}
			else
			{
					moduleError.TextKey = "ADDMODULE_HOMEPAGEERROR";
					moduleError.Text = "You are currently on the homepage using the default virtual ID (The default ID is set when no specific page is selected. e.g. www.yourdomain.com. Please select your homepage from the Navigation menu e.g. 'Home' so that you can add a module against the page's actual ID.";
					moduleError.Visible = true;
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
				return new Guid("{350CED6F-6739-43f3-8BF1-1D95187CA0BF}");
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
		public Viewer()
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

			// Create a new Title the control
//			ModuleTitle = new DesktopModuleTitle();
			// Set here title properties
			// Add title ad the very beginning of
			// the control's controls collection
//			Controls.AddAt(0, ModuleTitle);

			// Call base init procedure
			base.OnInit(e);
		}

		/// <summary>
		/// Initializes the component.
		/// </summary>
		private void InitializeComponent()
		{
			this.moduleType.SelectedIndexChanged += new System.EventHandler(this.moduleType_SelectedIndexChanged);
			this.AddModuleBtn.Click += new System.EventHandler(this.AddModule_Click);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
