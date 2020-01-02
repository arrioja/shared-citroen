using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
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
    /// New portal wizard
    /// </summary>
	[History("Mario Endara","2004/10/14","Now can create a Portal based on other Portal (Roles, Tabs & Modules)")]
    public class AddNewPortal : Rainbow.UI.AddItemPage
    {
        /// <summary>
        /// 
        /// </summary>
        protected System.Web.UI.WebControls.DropDownList SolutionsList;
        /// <summary>
        /// 
        /// </summary>
		protected System.Web.UI.WebControls.TextBox AliasField;
        /// <summary>
        /// 
        /// </summary>
		protected System.Web.UI.WebControls.TextBox TitleField;
        /// <summary>
        /// 
        /// </summary>
		protected System.Web.UI.WebControls.TextBox PathField;
        /// <summary>
        /// 
        /// </summary>
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredTitle;
        /// <summary>
        /// 
        /// </summary>
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredAlias;
        /// <summary>
        /// 
        /// </summary>
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredSitepath;
        /// <summary>
        /// 
        /// </summary>
		protected System.Web.UI.WebControls.Label ErrorMessage;
		protected Esperantus.WebControls.Literal Literal1;
		protected Esperantus.WebControls.Literal Literal2;
		protected Esperantus.WebControls.Literal Literal4;
		protected Esperantus.WebControls.Literal Literal3;
		protected Esperantus.WebControls.CheckBox chkUseTemplate;
        /// <summary>
        /// 
        /// </summary>
		protected Rainbow.Configuration.SettingsTable EditTable;

		private struct moduleTemplate
		{
			public int id;
			public Guid GuidID;
		}

		private struct tabTemplate
		{
			public int oldID;
			public int newID;
		}

        private void Page_Load(object sender, System.EventArgs e)
        {
            // Verify that the current user has access to access this page
			// Removed by Mario Endara <mario@softworks.com.uy> (2004/11/04)
//            if (PortalSecurity.IsInRoles("Admins") == false) 
//                PortalSecurity.AccessDeniedEdit();

            // If this is the first visit to the page, populate the site data
            if (Page.IsPostBack == false) 
            {
				// Bind the Portals to the SolutionsList
				SolutionsList.DataSource = GetPortals();
                SolutionsList.DataBind();

				//Preselect default Portal
				if (SolutionsList.Items.FindByValue("Default") != null)
					SolutionsList.Items.FindByValue("Default").Selected = true;
            }

			if (chkUseTemplate.Checked == false)
			{
				// Don't use a template portal, so show the EditTable
				// Remove the cache that can be setted by the new Portal, to get a "clean" PortalBaseSetting
				Rainbow.Settings.Cache.CurrentCache.Remove(Rainbow.Settings.Cache.Key.PortalBaseSettings());
				EditTable.DataSource = new SortedList(PortalSettings.GetPortalBaseSettings(null));
				EditTable.DataBind();
				EditTable.Visible = true;
				SolutionsList.Enabled = false;
			}
			else
			{
				EditTable.Visible = false;
				SolutionsList.Enabled = true;
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
				//Get Solutions
				PortalsDB portals = new PortalsDB();

				try
				{
					PathField.Text = PathField.Text.Replace("/", string.Empty);
					PathField.Text = PathField.Text.Replace("\\", string.Empty);
					PathField.Text = PathField.Text.Replace(".", string.Empty);
					if (chkUseTemplate.Checked == false)
					{
						// Create portal the "old" way
						int NewPortalID = portals.CreatePortal(Convert.ToInt32(SolutionsList.SelectedItem.Value), AliasField.Text, TitleField.Text, PathField.Text);
					
						// Update custom settings in the database
						EditTable.ObjectID = NewPortalID;
						EditTable.UpdateControls();
					}
					else
					{
						//Create portal based on the selected portal
						int NewPortalID = CreatePortal(Convert.ToInt32(SolutionsList.SelectedItem.Value), 
							SolutionsList.SelectedItem.Text, AliasField.Text, TitleField.Text, PathField.Text);
					}

					// Redirect back to calling page
					RedirectBackToReferringPage();

				}
				catch(Exception ex)
				{
					Rainbow.Configuration.ErrorHandler.HandleException(Esperantus.Localize.GetString("NEW_PORTAL_ERROR", "There was an error on creating the portal", this), ex);

					ErrorMessage.Visible = true;
					ErrorMessage.Text = Esperantus.Localize.GetString("NEW_PORTAL_ERROR", "There was an error on creating the portal", this) + "<br>";
					//ErrorMessage.Text += ex.Message;
				}
			}
        }

		private void EditTable_UpdateControl(object sender, Rainbow.Configuration.SettingsTableEventArgs e)
		{
            SettingsTable edt = (SettingsTable) sender;
			PortalSettings.UpdatePortalSetting(edt.ObjectID, e.CurrentItem.EditControl.ID, e.CurrentItem.Value);        
		}

		private SqlConnection GetConnection()
		{
			// Watch if there's a Template's database in the config.sys
			// else, use the same database as the portal
			string portalSqlConnectionID = "PortalTemplatesConnectionString";
			string strSqlConnection;

			if(ConfigurationSettings.AppSettings[portalSqlConnectionID] != null)
				strSqlConnection = ConfigurationSettings.AppSettings[portalSqlConnectionID];
			else
				strSqlConnection = ConfigurationSettings.AppSettings["ConnectionString"];

			return (new SqlConnection(strSqlConnection));
		}

		private DataSet GetPortals()
		{
			// Create Instance of Connection and Command Object
			SqlConnection myConnection = GetConnection();
			string selectSQL = "SELECT PortalID, PortalAlias from rb_Portals WHERE PortalID >= 0";
			SqlDataAdapter myCommand = new SqlDataAdapter(selectSQL, myConnection);

			// Create and Fill the DataSet
			DataSet myDataSet = new DataSet();
			try
			{
				myCommand.Fill(myDataSet);
			}
			finally
			{
				myCommand.Dispose(); 
				myConnection.Close();
				myConnection.Dispose();
			}
			// Return the dataset
			return myDataSet;

		}

		private int CreatePortal(int templateID, string templateAlias, string portalAlias, string portalName, string portalPath) 
		{
			int newPortalID;

			PortalsDB portals = new PortalsDB();
			TabsDB tabs = new TabsDB();
			ModulesDB modules = new ModulesDB();
			UsersDB users = new UsersDB();

			// create an Array to stores modules ID and GUID for finding them later
			ArrayList templateModules = new ArrayList();
			moduleTemplate module;
			// create an Array to stores tabs ID for finding them later
			ArrayList templateTabs = new ArrayList();
			tabTemplate tab;
			
			// Create a new portal
			newPortalID = portals.AddPortal(portalAlias, portalName, portalPath);

			// Open the connection to the PortalTemplates Database
			SqlConnection myConnection = GetConnection();
			SqlConnection my2ndConnection = GetConnection();
			SqlConnection my3rdConnection = GetConnection();
			myConnection.Open();
			my2ndConnection.Open();
			my3rdConnection.Open();

			// get module definitions and save them in the new portal
			SqlDataReader myReader = GetTemplateModuleDefinitions(templateID, myConnection);

			// Always call Read before accessing data.
			while (myReader.Read()) 
			{
				module.id = (int) myReader["ModuleDefID"];
				module.GuidID = GetGeneralModuleDefinitionByName(myReader["FriendlyName"].ToString(), my2ndConnection);
				try 
				{
					// save module definitions in the new portal
					modules.UpdateModuleDefinitions(module.GuidID, newPortalID, true);
					// Save the modules into a list for finding them later
					templateModules.Add(module);
				}
				catch
				{
					// tried to add a Module thas doesn´t exists in this implementation of the portal
				}
			}

			myReader.Close(); 

			if (!PortalSettings.UseSingleUserBase)
			{

				int roleID;
				int userID;
				int adminRoleID = 0;

				// get roles and save them in the new portal
				myReader = GetPortalRoles(templateID, myConnection);

				// Always call Read before accessing data.
				while (myReader.Read()) 
				{
					roleID = users.AddRole(newPortalID, myReader["RoleName"].ToString());
					if (myReader["RoleName"].ToString() == "Admins")
						adminRoleID = roleID;
				}

				myReader.Close();

				// Create the "admin" User for the new portal
				string AdminEmail = "admin@rainbowportal.net";
				userID = users.AddUser("admin", AdminEmail, "admin", newPortalID);

				// Create a new row in a many to many table (userroles)
				// giving the "admins" role to the "admin" user
				users.AddUserRole(adminRoleID, userID);

			}

			// Get all the Tabs in the Template Portal, store IDs in a list for finding them later
			// and create the Tabs in the new Portal
			myReader = GetTabsByPortal(templateID, myConnection);

			// Always call Read before accessing data.
			while (myReader.Read()) 
			{
				// Save the tabs into a list for finding them later
				tab.oldID = (int) myReader["TabID"];
				tab.newID = tabs.AddTab(newPortalID, myReader["TabName"].ToString(), Int32.Parse(myReader["TabOrder"].ToString()));
				templateTabs.Add(tab);
			}
			myReader.Close(); 

			// now I have to get them again to set up the ParentID for each Tab
			myReader = GetTabsByPortal(templateID, myConnection);

			// Always call Read before accessing data.
			while (myReader.Read()) 
			{
				// Find the news TabID and ParentTabID
				System.Collections.IEnumerator myEnumerator = templateTabs.GetEnumerator();
				int newTabID = -1;
				int newParentTabID = -1;

				while ( myEnumerator.MoveNext() && (newTabID == -1 || newParentTabID == -1))
				{
					tab = (tabTemplate) myEnumerator.Current;
					if (tab.oldID == (int) myReader["TabID"])
						newTabID = tab.newID;
					if (tab.oldID == Int32.Parse("0" + myReader["ParentTabID"]))
						newParentTabID = tab.newID;
				}

				if (newParentTabID == -1)
					newParentTabID = 0;

				// Update the Tab in the new portal
				tabs.UpdateTab(newPortalID, newTabID, newParentTabID, myReader["TabName"].ToString(), 
					Int32.Parse(myReader["TabOrder"].ToString()), myReader["AuthorizedRoles"].ToString(),
					myReader["MobileTabName"].ToString(), (bool)myReader["ShowMobile"]);

				// Finally use GetPortalSettings to access each Tab and its Modules in the Template Portal
				// and create them in the new Portal
				SqlDataReader result;

				try
				{

					result = GetTabModules(Int32.Parse(myReader["TabID"].ToString()), my2ndConnection);
				
					object myValue;

					while(result.Read()) 
					{
						ModuleSettings m = new ModuleSettings();
						m.ModuleID = (int) result["ModuleID"];
						m.ModuleDefID = (int) result["ModuleDefID"];
						m.TabID = newTabID;
						m.PaneName = (string) result["PaneName"];
						m.ModuleTitle = (string) result["ModuleTitle"];
					
						myValue = result["AuthorizedEditRoles"];
						m.AuthorizedEditRoles = ! Convert.IsDBNull(myValue) ? (string)myValue : string.Empty;

						myValue = result["AuthorizedViewRoles"];
						m.AuthorizedViewRoles = ! Convert.IsDBNull(myValue) ? (string)myValue : string.Empty;

						myValue = result["AuthorizedAddRoles"];
						m.AuthorizedAddRoles = ! Convert.IsDBNull(myValue) ? (string)myValue : string.Empty;

						myValue = result["AuthorizedDeleteRoles"];
						m.AuthorizedDeleteRoles = ! Convert.IsDBNull(myValue) ? (string)myValue : string.Empty;

						myValue = result["AuthorizedPropertiesRoles"];
						m.AuthorizedPropertiesRoles = ! Convert.IsDBNull(myValue) ? (string)myValue : string.Empty;

						myValue = result["AuthorizedMoveModuleRoles"];
						m.AuthorizedMoveModuleRoles = ! Convert.IsDBNull(myValue) ? (string)myValue : string.Empty;
				
						myValue = result["AuthorizedDeleteModuleRoles"];
						m.AuthorizedDeleteModuleRoles = ! Convert.IsDBNull(myValue) ? (string)myValue : string.Empty;

						myValue = result["AuthorizedPublishingRoles"];
						m.AuthorizedPublishingRoles = ! Convert.IsDBNull(myValue) ? (string)myValue : string.Empty;

						myValue = result["SupportWorkflow"];
						m.SupportWorkflow = ! Convert.IsDBNull(myValue) ? (bool)myValue : false;

						myValue = result["AuthorizedApproveRoles"];
						m.AuthorizedApproveRoles = ! Convert.IsDBNull(myValue) ? (string)myValue : string.Empty;

						myValue = result["WorkflowState"];
						m.WorkflowStatus = ! Convert.IsDBNull(myValue) ? (WorkflowState)(0 + (byte)myValue) : WorkflowState.Original;

						try {myValue = result["SupportCollapsable"];}
						catch{myValue = DBNull.Value;}
						m.SupportCollapsable = DBNull.Value != myValue ? (bool)myValue : false;

						try {myValue = result["ShowEveryWhere"];}
						catch{myValue = DBNull.Value;}
						m.ShowEveryWhere = DBNull.Value != myValue ? (bool)myValue : false;

						m.CacheTime = int.Parse(result["CacheTime"].ToString());
						m.ModuleOrder = int.Parse(result["ModuleOrder"].ToString());

						myValue = result["ShowMobile"];
						m.ShowMobile = ! Convert.IsDBNull(myValue) ? (bool)myValue : false;

						// Find the new ModuleDefID assigned to the module in the new portal
						myEnumerator = templateModules.GetEnumerator();
						int newModuleDefID = 0;

						while ( myEnumerator.MoveNext() && newModuleDefID == 0 )
						{
							module = (moduleTemplate)myEnumerator.Current;
							if (module.id == m.ModuleDefID)
								newModuleDefID = modules.GetModuleDefinitionByGuid(newPortalID, module.GuidID);
						}

						if (newModuleDefID > 0)
						{
							// add the module to the new tab
							int newModuleID = modules.AddModule(newTabID, m.ModuleOrder, m.PaneName, m.ModuleTitle, 
								newModuleDefID,	m.CacheTime, m.AuthorizedEditRoles, m.AuthorizedViewRoles,
								m.AuthorizedAddRoles, m.AuthorizedDeleteRoles, m.AuthorizedPropertiesRoles,
								m.AuthorizedMoveModuleRoles, m.AuthorizedDeleteModuleRoles,
								m.ShowMobile, m.AuthorizedPublishingRoles, m.SupportWorkflow,
								m.ShowEveryWhere, m.SupportCollapsable);
							// At the end, get all ModuleSettings and save them in the new module
							SqlDataReader dr = GetModuleSettings(m.ModuleID, my3rdConnection);

							while (dr.Read()) 
							{
								ModuleSettings.UpdateModuleSetting(newModuleID, dr["SettingName"].ToString(), dr["SettingValue"].ToString());
							}
							dr.Close(); 
						}
					}

					result.Close(); 

				}
				catch
				{
					// Error? ignore Tab ...
				}

			}
			myReader.Close(); 
			
			// Set the CustomSettings of the New Portal based in the Template Portal
			myReader = GetPortalCustomSettings(templateID, myConnection);

			// Always call Read before accessing data.
			while (myReader.Read()) 
			{
				PortalSettings.UpdatePortalSetting(newPortalID, myReader["SettingName"].ToString(), myReader["SettingValue"].ToString());
			}

			myReader.Close();

			// close the conections
			myConnection.Close();
			myConnection.Dispose();
			my2ndConnection.Close();
			my2ndConnection.Dispose();
			my3rdConnection.Close();
			my3rdConnection.Dispose();

			// Create paths
			portals.CreatePortalPath(portalPath);

			return newPortalID;
		}

		private SqlDataReader GetTemplateModuleDefinitions(int templateID, SqlConnection myConnection )
		{
			SqlCommand myCommand = new SqlCommand("rb_GetCurrentModuleDefinitions", myConnection);

			// Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure;

			// Add Parameters to SPROC
			SqlParameter parameterPortalID = new SqlParameter("@PortalID", SqlDbType.Int, 4);
			parameterPortalID.Value = templateID;
			myCommand.Parameters.Add(parameterPortalID);

			// execute the command
			SqlDataReader dr = myCommand.ExecuteReader();

			// Return the datareader
			return dr;
		}

		private SqlDataReader GetPortalRoles(int templateID, SqlConnection myConnection )
		{
			// Create Instance of Command Object
			SqlCommand myCommand = new SqlCommand("rb_GetPortalRoles", myConnection);

			// Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure;

			// Add Parameters to SPROC
			SqlParameter parameterPortalID = new SqlParameter("@PortalID", SqlDbType.Int, 4);
			parameterPortalID.Value = templateID;
			myCommand.Parameters.Add(parameterPortalID);

			// execute the command
			SqlDataReader dr = myCommand.ExecuteReader();

			// Return the datareader
			return dr;
		}

		private SqlDataReader GetPortalCustomSettings(int templateID, SqlConnection myConnection )
		{
			// Create Instance of Command Object
			SqlCommand myCommand = new SqlCommand("rb_GetPortalCustomSettings", myConnection);

			// Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure;

			// Add Parameters to SPROC
			SqlParameter parameterPortalID = new SqlParameter("@PortalID", SqlDbType.Int, 4);
			parameterPortalID.Value = templateID;
			myCommand.Parameters.Add(parameterPortalID);

			// execute the command
			SqlDataReader dr = myCommand.ExecuteReader();

			// Return the datareader
			return dr;
		}

		private Guid GetGeneralModuleDefinitionByName(string moduleName, SqlConnection myConnection)
		{
			// Instance of Command Object
			SqlCommand myCommand = new SqlCommand("rb_GetGeneralModuleDefinitionByName", myConnection);

			// Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure;

			// Add Parameters to SPROC
			SqlParameter parameterFriendlyName = new SqlParameter("@FriendlyName", SqlDbType.NVarChar, 128);
			parameterFriendlyName.Value = moduleName;
			myCommand.Parameters.Add(parameterFriendlyName);

			SqlParameter parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.UniqueIdentifier);
			parameterModuleID.Direction = ParameterDirection.Output;
			myCommand.Parameters.Add(parameterModuleID);

			// Execute the command
			myCommand.ExecuteNonQuery();

			if (parameterModuleID.Value != null && parameterModuleID.Value.ToString().Length != 0)
			{
				try
				{
					return new Guid(parameterModuleID.Value.ToString());
				}
				catch(Exception ex)
				{
					Rainbow.Configuration.ErrorHandler.HandleException("'" + parameterModuleID.Value.ToString() + "' seems not a valid GUID.", ex);
					throw;
				}
			}
			else
			{
				Rainbow.Configuration.ErrorHandler.HandleException("Null GUID!.", new ArgumentException("Null GUID!", "GUID"));
			}
			throw new ArgumentException("Invalid GUID", "GUID");
		}

		private SqlDataReader GetTabModules(int TabID, SqlConnection myConnection)
		{
			string selectSQL = "select ModuleID, ModuleDefID, ModuleOrder, PaneName, ModuleTitle, " +
								"AuthorizedEditRoles, AuthorizedViewRoles, AuthorizedAddRoles, " +
								"AuthorizedDeleteRoles, AuthorizedPropertiesRoles, CacheTime, "+
								"ShowMobile, AuthorizedPublishingRoles, SupportWorkflow, "+
								"AuthorizedApproveRoles, WorkflowState, SupportCollapsable, "+
								"ShowEveryWhere, AuthorizedMoveModuleRoles, AuthorizedDeleteModuleRoles "+
								"from rb_Modules where TabID=" + TabID.ToString();
			SqlCommand myCommand = new SqlCommand(selectSQL, myConnection);

			// Mark the Command as a SPROC
			myCommand.CommandType = CommandType.Text;

			// execute the command
			SqlDataReader dr = myCommand.ExecuteReader();

			// Return the datareader
			return dr;
		}

		private SqlDataReader GetModuleSettings(int moduleID, SqlConnection myConnection)
		{
			SqlCommand myCommand = new SqlCommand("rb_GetModuleSettings", myConnection);

			// Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure;

			// Add Parameters to SPROC
			SqlParameter parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
			parameterModuleID.Value = moduleID;
			myCommand.Parameters.Add(parameterModuleID);

			// Execute the command
			SqlDataReader dr = myCommand.ExecuteReader();

			return dr;

		}

		private SqlDataReader GetTabsByPortal(int templateID, SqlConnection myConnection)
		{
			SqlCommand myCommand = new SqlCommand("rb_GetTabsByPortal", myConnection);

			// Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure;

			// Add Parameters to SPROC
			SqlParameter parameterPortalID = new SqlParameter("@PortalID", SqlDbType.Int, 4);
			parameterPortalID.Value = templateID;
			myCommand.Parameters.Add(parameterPortalID);

			// Execute the command
			SqlDataReader dr = myCommand.ExecuteReader();

			return dr;

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
			RequiredAlias.ErrorMessage = Esperantus.Localize.GetString("VALID_FIELD");
			RequiredSitepath.ErrorMessage = Esperantus.Localize.GetString("VALID_FIELD");
		
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

