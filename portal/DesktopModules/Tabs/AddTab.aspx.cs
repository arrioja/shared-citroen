using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.Security;
using Rainbow.Configuration;
using Esperantus;

namespace Rainbow.Admin
{
	public class AddTab : Rainbow.UI.EditItemPage
	{
		protected ArrayList leftList;
		protected ArrayList contentList;
		protected Esperantus.WebControls.Literal tab_name;
		protected Esperantus.WebControls.Literal tab_name1;
		protected System.Web.UI.WebControls.TextBox tabName;
		protected Esperantus.WebControls.Literal roles_auth;
		protected System.Web.UI.WebControls.CheckBoxList authRoles;
		protected Esperantus.WebControls.Literal tab_parent;
		protected System.Web.UI.WebControls.DropDownList parentTab;
		protected Esperantus.WebControls.Literal show_mobile;
		protected System.Web.UI.WebControls.CheckBox showMobile;
		protected Esperantus.WebControls.Literal mobiletab;
		protected System.Web.UI.WebControls.TextBox mobileTabName;
		protected Esperantus.WebControls.Literal organizemodule;

		protected Rainbow.Configuration.SettingsTable EditTable;
		protected Esperantus.WebControls.Label lblErrorNotAllowed;
		protected ArrayList rightList;
		protected Esperantus.WebControls.Literal msgError;
		protected Esperantus.WebControls.Literal addmodule;
		protected Esperantus.WebControls.Literal module_type;
		protected System.Web.UI.WebControls.DropDownList moduleType;
		protected Esperantus.WebControls.Literal lbl_jump_to_tab;
		protected System.Web.UI.WebControls.CheckBox cb_JumpToTab;
		protected Esperantus.WebControls.LinkButton saveButton;
		protected ADGroupMember memRoles;

		#region Page_Load
		/// <summary>
		/// The Page_Load server event handler on this page is used
		/// to populate a tab's layout settings on the page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e)
		{
			// If first visit to the page, update all entries
			if (!Page.IsPostBack) 
			{
				msgError.Visible = false;
    			BindData();
			}
		}
		#endregion

		#region Events
		/// <summary>
		/// The cancelButton_Click is used to return to the 
		/// previous page if present
		/// Created by Mike Stone 30/12/2004
		/// </summary>
		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			string returnTab = Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopDefault.aspx", int.Parse(Request.QueryString["returntabid"]));
			Response.Redirect(returnTab);
		}
		
		
		/// <summary>
		/// The SaveButton_Click is used to commit the tab/page 
		/// information from the form to the database.
		/// Created by Mike Stone 29/12/2004
		/// </summary>
		private void SaveButton_Click(object sender, System.EventArgs e)
		{
			
			//Only Save if Input Data is Valid
			int NewTabID = 0;
			string returnTab;

			if (Page.IsValid == true) 
			{
				try
				{
					NewTabID = SaveTabData();
			
					// Flush all tab navigation cache keys. Very important for recovery the changes
					// made in all languages and not get a error if user change the tab parent.
					// jviladiu@portalServices.net (05/10/2004)
					// Copied to here 29/12/2004 by Mike Stone
					Rainbow.Settings.Cache.CurrentCache.RemoveAll("_TabNavigationSettings_");
			
					//Jump to Page option
					if (cb_JumpToTab.Checked == true) 
					{
						// Redirect to New Form - Mike Stone 19/12/2004
						returnTab = Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopDefault.aspx", NewTabID, "SelectedTabID=" + NewTabID.ToString());
					}
					else
					{
						// Do NOT Redirect to New Form - Mike Stone 19/12/2004
						// I guess every .aspx page needs to have a module tied to it. 
						// or you will get an error about edit access denied. 
						// Fix: RBP-594 by mike stone added returntabid to url.
						returnTab = Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Tabs/AddTab.aspx", "mID=" + Request.QueryString["mID"] + "&returntabid=" + Request.QueryString["returntabid"]);
					}
					Response.Redirect(returnTab);

				}
				catch
				{
					lblErrorNotAllowed.Visible = true;
				}

			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// The SaveTabData helper method is used to persist the
		/// current tab settings to the database.
		/// </summary>
		private int SaveTabData() 
		{
			// Construct Authorized User Roles string
			string authorizedRoles = "";

			// added by Jonathan Fong 05/08/2004 to support LDAP
			// www.gt.com.au
			bool useMemberList = HttpContext.Current.User is System.Security.Principal.WindowsPrincipal;
			useMemberList |= System.Configuration.ConfigurationSettings.AppSettings["LDAPLogin"] != null ? true : false;

			if (useMemberList)
				authorizedRoles = memRoles.Members;
			else
				foreach(ListItem item in authRoles.Items) 
				{
					if (item.Selected == true) 
					{
						authorizedRoles = authorizedRoles + item.Text + ";";
					}
				}

			// Add Tab info in the database
			int NewTabID = new TabsDB().AddTab(portalSettings.PortalID, Int32.Parse(parentTab.SelectedItem.Value), tabName.Text, 990000, authorizedRoles, showMobile.Checked, mobileTabName.Text);

			// Update custom settings in the database
			EditTable.UpdateControls();
		    
			return NewTabID;
		}

		/// <summary>
		/// The BindData helper method is used to update the tab's
		/// layout panes with the current configuration information
		/// </summary>
		private void BindData() 
		{
			TabSettings tab = portalSettings.ActiveTab;

			// Populate Tab Names, etc.
			tabName.Text = "New Tab";
			mobileTabName.Text = "";
			showMobile.Checked = false;

			// Populate the "ParentTab" Data
			TabsDB t = new TabsDB();
			SqlDataReader dr = t.GetTabsParent(portalSettings.PortalID, TabID);
			parentTab.DataSource = dr;
			parentTab.DataBind();
			dr.Close(); //by Manu, fixed bug 807858

			// added by Jonathan Fong 05/08/2004 to support LDAP
			// www.gt.com.au
			bool useMemberList = HttpContext.Current.User is System.Security.Principal.WindowsPrincipal;
			useMemberList |= System.Configuration.ConfigurationSettings.AppSettings["LDAPLogin"] != null ? true : false;

			if (useMemberList)
			{
				memRoles.Visible = true;
				authRoles.Visible = false;
				memRoles.Members = tab.AuthorizedRoles;
			}
			else
			{
			// Populate checkbox list with all security roles for this portal
			// and "check" the ones already configured for this tab
			UsersDB users = new UsersDB();
			SqlDataReader roles = users.GetPortalRoles(portalSettings.PortalID);

			// Clear existing items in checkboxlist
			authRoles.Items.Clear();

			ListItem allItem = new ListItem();
			allItem.Text = "All Users";

			if (tab.AuthorizedRoles.LastIndexOf("All Users") > -1) 
			{
				allItem.Selected = true;
			}

			authRoles.Items.Add(allItem);

			// Authenticated user role added
			// 15 nov 2002 - by manudea
			ListItem authItem = new ListItem();
			authItem.Text = "Authenticated Users";

			if (tab.AuthorizedRoles.LastIndexOf("Authenticated Users") > -1) 
			{
				authItem.Selected = true;
			}

			authRoles.Items.Add(authItem);
			// end authenticated user role added

				while(roles.Read()) 
				{
					ListItem item = new ListItem();
					item.Text = (string) roles["RoleName"];
					item.Value = roles["RoleID"].ToString();
            
					if ((tab.AuthorizedRoles.LastIndexOf(item.Text)) > -1)
						item.Selected = true;
                    
					authRoles.Items.Add(item);
				}
				roles.Close(); //by Manu, fixed bug 807858
			}

		}
		#endregion

		#region Web Form Designer generated code
		/// <summary>
		/// Raises the Init event.
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();

			//Confirm delete
			if (!(IsClientScriptBlockRegistered("confirmDelete")))
			{
				string[] s = {"CONFIRM_DELETE"};
				RegisterClientScriptBlock("confirmDelete", PortalSettings.GetStringResource("Rainbow.aspnet_client.Rainbow_scripts.confirmDelete.js", s));
			}

			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() 
		{    
			this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	}
}
