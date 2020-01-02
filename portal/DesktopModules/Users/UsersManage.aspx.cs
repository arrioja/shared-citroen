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
using Rainbow.Security;
using Esperantus;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// User manager
	/// </summary>
	[History("gman3001","2004/10/06","Add GetCurrentProfileControl method to properly obtain a custom register control as specified by the 'Register Module ID' setting.")]
	public class UsersManage : Rainbow.UI.EditItemPage
	{
		/// <summary>
		/// 
		/// </summary>
		protected System.Web.UI.WebControls.DropDownList allRoles;
		protected Esperantus.WebControls.LinkButton addExisting;
		/// <summary>
		/// 
		/// </summary>
		protected System.Web.UI.WebControls.DataList userRoles;
		/// <summary>
		/// 
		/// </summary>
		protected Esperantus.WebControls.LinkButton saveBtn;
		/// <summary>
		/// 
		/// </summary>
		protected System.Web.UI.HtmlControls.HtmlGenericControl title;

		int    userID   = -1;
		string userName = string.Empty;
		//        int tabIndex = 0;
		protected Esperantus.WebControls.Literal name;
		protected System.Web.UI.WebControls.PlaceHolder register;
		protected Esperantus.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label ErrorLabel;
		protected IEditUserProfile EditControl;

		/// <summary>
		/// The Page_Load server event handler on this page is used
		/// to populate the role information for the page.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Verify that the current user has access to access this page
			// Removed by Mario Endara <mario@softworks.com.uy> (2004/11/04)
//			if (PortalSecurity.IsInRoles("Admins") == false)
//				PortalSecurity.AccessDeniedEdit();

			//Code no longer needed here, gman3001 10/06/2004
			/*string RegisterPage;

			//Select the actual register page
			if (portalSettings.CustomSettings["SITESETTINGS_REGISTER_TYPE"] != null &&
					portalSettings.CustomSettings["SITESETTINGS_REGISTER_TYPE"].ToString() != "register.aspx" )
				RegisterPage = portalSettings.CustomSettings["SITESETTINGS_REGISTER_TYPE"].ToString();
			else
				RegisterPage = "register.aspx";
			*/

			// Calculate userid
			if (Request.Params["userid"] != null) 
			{
				userID = Int32.Parse(Request.Params["userid"]);
			}
			if (Request.Params["username"] != null) 
			{
				userName = (string)Request.Params["username"];
			}

			
			//Control myControl = this.LoadControl("../DesktopModules/Register/" + RegisterPage);
			//Control myControl = this.LoadControl(Rainbow.Settings.Path.WebPathCombine(Rainbow.Settings.Path.ApplicationRoot, "DesktopModules/Register", RegisterPage));
			// Line Added by gman3001 10/06/2004, to support proper loading of a register module specified by 'Register Module ID' setting in the Portal Settings admin page
			Control myControl = GetCurrentProfileControl();
			
			EditControl = ((IEditUserProfile) myControl);
			//EditControl.RedirectPage = HttpUrlBuilder.BuildUrl("~/Admin/UsersManage.aspx", TabID, "username=" + userName + AllowEditUserID);
			register.Controls.Add(myControl);

			// If this is the first visit to the page, bind the role data to the datalist
			if (Page.IsPostBack == false)
			{
				// new user?
				if (userName == string.Empty)
				{
					try
					{
						UsersDB users = new UsersDB();

						// make a unique new user record
						int uid = -1;
						int i = 0;

						Exception lastException = null;
						while (uid == -1 && i < 99) //Avoid infinite loop
						{
							string friendlyName = "New User created " + DateTime.Now.ToString();
							userName = "NewUserEmail" + i.ToString() + "@yoursite.com";
							try
							{
								uid = users.AddUser(friendlyName, userName, string.Empty, portalSettings.PortalID);
							}
							catch(Exception ex)
							{
								uid = -1;
								lastException = ex;
							}
							i++;
						}
						if (uid == -1)
							throw new Exception("New user creation failed after " + i.ToString() + " retries.", lastException);

						// redirect to this page with the corrected querystring args
						Response.Redirect(HttpUrlBuilder.BuildUrl("~/DesktopModules/Users/UsersManage.aspx", TabID, "mID=" + ModuleID + "&userID=" + uid + "&username=" + userName));
					}
					catch(Exception ex)
					{
						Rainbow.Configuration.ErrorHandler.HandleException("Error creating new user", ex);
						ErrorLabel.Text = ex.Message; 
						ErrorLabel.Visible = true; 
					}
				}

				BindData();
			}
		}

		// Method Added by gman3001 10/06/2004, to support proper loading of a register module specified by 'Register Module ID' setting in the Portal Settings admin page
		private Control GetCurrentProfileControl()
		{
			//default
			string RegisterPage = "register.aspx";
			RainbowPrincipal user = HttpContext.Current.User as RainbowPrincipal;
			// Use proper User Management screen for LDAP Authentication
			// Not sure about this LDAP User Management form restriction, but for now I'll keep it in: gman3001 10/06/2004
			if (user != null && user.Identity.AuthenticationType == "LDAP")
			{
				RegisterPage = "LDAPUserProfile.ascx";
				PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
				Control myControl = this.LoadControl(Rainbow.Settings.Path.WebPathCombine(Rainbow.Settings.Path.ApplicationRoot, "DesktopModules/LDAPUserProfile", RegisterPage));
				
				Rainbow.UI.WebControls.PortalModuleControl p = ((Rainbow.UI.WebControls.PortalModuleControl) myControl);
				p.ModuleID = int.Parse(portalSettings.CustomSettings["SITESETTINGS_REGISTER_MODULEID"].ToString());

				return ((Control) p);
			}
			// Obtain PortalSettings from Current Context
			else if (HttpContext.Current != null)
			{
				PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];

				//Select the actual register page
				if (portalSettings.CustomSettings["SITESETTINGS_REGISTER_TYPE"] != null &&
					portalSettings.CustomSettings["SITESETTINGS_REGISTER_TYPE"].ToString() != "register.aspx" )
				{
					RegisterPage = portalSettings.CustomSettings["SITESETTINGS_REGISTER_TYPE"].ToString();
				}
				
				int moduleID = int.Parse(portalSettings.CustomSettings["SITESETTINGS_REGISTER_MODULEID"].ToString());
				string moduleDesktopSrc = string.Empty;
				if (moduleID > 0)
					moduleDesktopSrc = ModuleSettings.GetModuleDesktopSrc(moduleID);
				if (moduleDesktopSrc.Length == 0)
					moduleDesktopSrc = Rainbow.Settings.Path.WebPathCombine(Rainbow.Settings.Path.ApplicationRoot, "DesktopModules/Register", RegisterPage);
				Control myControl = this.LoadControl(moduleDesktopSrc);
				
				Rainbow.UI.WebControls.PortalModuleControl p = ((Rainbow.UI.WebControls.PortalModuleControl) myControl);

				// changed by Mario Endara <mario@softworks.com.uy> (2004/11/05)
				// if there's no custom register module, take actual ModuleID, else take the custom ModuleID
				if (moduleID == 0) 
				{
					p.ModuleID = ModuleID;
					((SettingItem) p.Settings["MODULESETTINGS_SHOW_TITLE"]).Value = "false";
				}
				else
					p.ModuleID = moduleID;

				return ((Control) p);
			}

			return (null);
		}

		/// <summary>
		/// Set the module guids with free access to this page
		/// </summary>
		protected override ArrayList AllowedModules
		{
			get
			{
				ArrayList al = new ArrayList();
				al.Add ("B6A48596-9047-4564-8555-61E3B31D7272");
				return al;
			}
		}

//		string AllowEditUserID
//		{
//			get
//			{
//				return (Request.Params["AllowEditUserID"] != null) ? "&AllowEditUserID=true" : string.Empty;
//			}	
//		}

		/// <summary>
		/// The Save_Click server event handler on this page is used
		/// to save the current security settings to the configuration system
		/// </summary>
		/// <param name="Sender"></param>
		/// <param name="e"></param>
		private void Save_Click(Object Sender, EventArgs e)
		{
			// Persists user data
			EditControl.SaveUserData();

			// Navigate back to admin page
			Response.Redirect(Rainbow.HttpUrlBuilder.BuildUrl(TabID));
		}

		/// <summary>
		/// The AddRole_Click server event handler is used to add
		/// the user to this security role.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddRole_Click(Object sender, EventArgs e)
		{
			int roleID;

			//get user id from dropdownlist of existing users
			roleID = Int32.Parse(allRoles.SelectedItem.Value);

			// Add a new userRole to the database
			UsersDB users = new UsersDB();
			users.AddUserRole(roleID, userID);

			// Rebind list
			BindData();
		}

		/// <summary>
		/// The UserRoles_ItemCommand server event handler on this page
		/// is used to handle deleting the user from roles
		/// from the userRoles asp:datalist control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UserRoles_ItemCommand(object sender, DataListCommandEventArgs e)
		{
			UsersDB users = new UsersDB();
			int roleID = (int) userRoles.DataKeys[e.Item.ItemIndex];

			// update database
			users.DeleteUserRole(roleID, userID);

			// Ensure that item is not editable
			userRoles.EditItemIndex = -1;

			// Repopulate list
			BindData();
		}

		/// <summary>
		/// The BindData helper method is used to bind the list of
		/// security roles for this portal to an asp:datalist server control
		/// </summary>
		private void BindData()
		{
			// Bind the Email and Password
			UsersDB users = new UsersDB();
//			SqlDataReader dr = users.GetSingleUser(userName, portalSettings.PortalID);

			// bind users in role to DataList
			SqlDataReader drUsers = users.GetRolesByUser(userName, portalSettings.PortalID);
			userRoles.DataSource = drUsers;
			userRoles.DataBind();
			drUsers.Close(); //by Manu, fixed bug 807858

			// bind all portal roles to dropdownlist
			SqlDataReader drRoles = users.GetPortalRoles(portalSettings.PortalID);
			allRoles.DataSource = drRoles;
			allRoles.DataBind();
			drRoles.Close(); //by Manu, fixed bug 807858
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
			this.addExisting.Click += new System.EventHandler(this.AddRole_Click);
			this.userRoles.ItemCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.UserRoles_ItemCommand);
			this.saveBtn.Click += new System.EventHandler(this.Save_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
        #endregion

	}
}
