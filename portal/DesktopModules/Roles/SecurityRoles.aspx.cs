using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow.Security;
using Rainbow.Configuration;
using Esperantus;

namespace Rainbow.DesktopModules
{
    /// <summary>
    /// The SecurityRoles.aspx page is used to create and edit
    /// security roles within the Portal application.
    /// </summary>
    public class SecurityRoles : Rainbow.UI.EditItemPage
    {
        protected System.Web.UI.WebControls.Label Message;
        protected System.Web.UI.WebControls.TextBox windowsUserName;
        protected System.Web.UI.WebControls.DropDownList allUsers;
        protected System.Web.UI.WebControls.DataList usersInRole;
        protected System.Web.UI.HtmlControls.HtmlGenericControl title;
		// Added EsperantusKeys for Localization 
		// Mario Endara mario@softworks.com.uy june-1-2004 
		protected Esperantus.WebControls.LinkButton addNew;
		protected Esperantus.WebControls.LinkButton addExisting;
		protected Esperantus.WebControls.LinkButton saveBtn;

        int    roleID   = -1;
        string roleName = string.Empty;

		/// <summary>
		/// The Page_Load server event handler on this page is used
		/// to populate the role information for the page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e) 
        {	
            // Calculate security roleID
            if (Request.Params["roleid"] != null) 
            {
                roleID = Int32.Parse(Request.Params["roleid"]);
            }
            if (Request.Params["rolename"] != null) 
            {
                roleName = (string)Request.Params["rolename"];
            }
        
            // If this is the first visit to the page, bind the role data to the datalist
            if (Page.IsPostBack == false) 
                BindData();
        }

		/// <summary>
		/// Set the module guids with free access to this page
		/// </summary>
		protected override ArrayList AllowedModules
		{
			get
			{
				ArrayList al = new ArrayList();
				al.Add ("A406A674-76EB-4BC1-BB35-50CD2C251F9C");
				return al;
			}
		}

		/// <summary>
		/// The Save_Click server event handler on this page is used
		/// to save the current security settings to the configuration system
		/// </summary>
		/// <param name="Sender"></param>
		/// <param name="e"></param>
        private void Save_Click(Object Sender, EventArgs e)
        {
            // Navigate back to admin page
            Response.Redirect(Rainbow.HttpUrlBuilder.BuildUrl(TabID));
        }

		/// <summary>
		/// The AddUser_Click server event handler is used to add
		/// a new user to this security role.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void AddUser_Click(Object sender, EventArgs e)
        {
            int userID;
        
            if (((LinkButton)sender).ID == "addNew")
            {
                // add new user to users table
                UsersDB users = new UsersDB();
                if ((userID = users.AddUser(windowsUserName.Text, windowsUserName.Text, "acme", portalSettings.PortalID)) == -1)
                {
					// Added EsperantusKeys for Localization 
					// Mario Endara mario@softworks.com.uy june-1-2004 
                    Message.Text = Esperantus.Localize.GetString ("ROLE_ERROR_ADD").Replace("%1%", windowsUserName.Text );
                }
            }
            else 
            {
                //get user id from dropdownlist of existing users
                userID = Int32.Parse(allUsers.SelectedItem.Value);
            }
              
            if (userID != -1) 
            {
                // Add a new userRole to the database
				UsersDB users = new UsersDB();
				users.AddUserRole(roleID, userID);
            }
        
            // Rebind list
            BindData();
        }

		/// <summary>
		/// The usersInRole_ItemCommand server event handler on this page 
		/// is used to handle the user editing and deleting roles
		/// from the usersInRole asp:datalist control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void usersInRole_ItemCommand(object sender, DataListCommandEventArgs e) 
        {
			UsersDB users = new UsersDB();
			
			int userID = (int) usersInRole.DataKeys[e.Item.ItemIndex];
       
            if (e.CommandName == "delete") 
            {
                // update database
                users.DeleteUserRole(roleID, userID);

                // Ensure that item is not editable
                usersInRole.EditItemIndex = -1;

                // Repopulate list
                BindData();
            }
        }

		/// <summary>
		/// The BindData helper method is used to bind the list of 
		/// security roles for this portal to an asp:datalist server control
		/// </summary>
        private void BindData()
        {
            // unhide the Windows Authentication UI, if application
            if (User.Identity.AuthenticationType != "Forms") 
            {
                windowsUserName.Visible = true;
                addNew.Visible = true;
            }

            // add the role name to the title
            if (roleName != string.Empty) 
            {
				// Added EsperantusKeys for Localization 
				// Mario Endara mario@softworks.com.uy june-1-2004 
                title.InnerText = Esperantus.Localize.GetString("ROLE_MEMBERSHIP") + roleName;
            }
		
            // Get the portal's roles from the database
			UsersDB users = new UsersDB();
        
            // bind users in role to DataList
			System.Data.SqlClient.SqlDataReader drRoles = users.GetRoleMembers(roleID);
            usersInRole.DataSource = drRoles;
            usersInRole.DataBind();
			drRoles.Close(); //by Manu, fixed bug 807858

            // bind all portal users to dropdownlist
			System.Data.DataSet drUsers = users.GetUsers(portalSettings.PortalID);
            allUsers.DataSource = drUsers;
            allUsers.DataBind();
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
			this.addNew.Click += new System.EventHandler(this.AddUser_Click);
			this.addExisting.Click += new System.EventHandler(this.AddUser_Click);
			this.usersInRole.ItemCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.usersInRole_ItemCommand);
			this.saveBtn.Click += new System.EventHandler(this.Save_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
    }
}