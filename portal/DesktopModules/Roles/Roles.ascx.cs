using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.Security;
using Rainbow.Configuration;
using Esperantus;

namespace Rainbow.DesktopModules
{
    public class Roles : PortalModuleControl 
    {
        protected System.Web.UI.WebControls.DataList rolesList;
		protected Esperantus.WebControls.LinkButton AddRoleBtn;
		protected Esperantus.WebControls.Literal label_description;
		protected Esperantus.WebControls.Literal labelError;
		protected Esperantus.WebControls.ImageButton RoleDeleteBtn;

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

		private void Page_Load(object sender, System.EventArgs e) 
        {
			foreach(Control c in rolesList.Controls)
			{
				foreach(Control dl in c.Controls)
				{
					if(dl.GetType().Name == "Label")
					{
						//Response.Write(c.UniqueID);
						if(dl.UniqueID.IndexOf("ImageButton2") >= 0)
							((Label) dl).Text = Esperantus.Localize.GetString("EDITBTN");

						if(dl.UniqueID.IndexOf("ImageButton1") >= 0)
							((Label) dl).Text = Esperantus.Localize.GetString("DELETEBTN");
					}
				}
			}

            // If this is the first visit to the page, bind the role data to the datalist
            if (Page.IsPostBack == false) 
				BindData();
        }

		private void RolesList_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
		{
			// 20/7/2004 changed by Mario Endara mario@softworks.com.uy
			// don't let the user to edit or delete the role "Admins"
			// the rolename is an hyperlink to the list of users of the role
			Control dl = e.Item.FindControl("ImageButton1");
			Control d2 = e.Item.FindControl("ImageButton2");
			HyperLink d3 = (HyperLink) e.Item.FindControl("Name");
			int roleID = (int) rolesList.DataKeys[e.Item.ItemIndex];
			if(d3 != null)
			{
				string _roleName = d3.Text ;
				// Added by Mario Endara <mario@softworks.com.uy> 2004/11/04
				// if the user is not member of the "Admins" role, he can´t access to the members of the Admins role
				// added mID by Mario Endara <mario@softworks.com.uy> to support security check (2004/11/27)
				if (PortalSecurity.IsInRoles("Admins") == true || _roleName != "Admins") 
					d3.NavigateUrl = HttpUrlBuilder.BuildUrl("~/DesktopModules/Roles/SecurityRoles.aspx", TabID, "mID=" + ModuleID.ToString() + "&roleID=" + roleID + "&rolename=" + _roleName );

			if(dl != null)
			{
					if (_roleName == "Admins") 
						dl.Visible = false;
				((Esperantus.WebControls.ImageButton) dl).Attributes.Add("OnClick","return confirmDelete()");
			}
				if(d2 != null)
				{
					if (_roleName == "Admins") 
						d2.Visible = false;
				}
			}
		}

		private void AddRole_Click(Object Sender, EventArgs e) 
		{
			//http://sourceforge.net/tracker/index.php?func=detail&aid=828580&group_id=66837&atid=515929
			try 
			{
				// Add a new role to the database
				// Added EsperantusKeys for Localization 
				// Mario Endara mario@softworks.com.uy june-10-2004 
				new UsersDB().AddRole(portalSettings.PortalID, Esperantus.Localize.GetString("ROLE_NEW_ROLE"));

				// disable add button 
				AddRoleBtn.Enabled = false;

				// set the edit item index to the last item
				rolesList.EditItemIndex = rolesList.Items.Count;
			}
			catch(Exception ex)
			{
				// new role is already present more than likely
				Rainbow.Configuration.ErrorHandler.HandleException("AddRole_Click error: new role is already present more than likely", ex);
			} 

			// Rebind list
			BindData();
		}

		/// <summary>
        /// The RolesList_ItemCommand server event handler on this page 
        /// is used to handle the user editing and deleting roles
        /// from the RolesList asp:datalist control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RolesList_ItemCommand(object sender, DataListCommandEventArgs e) 
		{
			//http://sourceforge.net/tracker/index.php?func=detail&aid=828580&group_id=66837&atid=515929
			UsersDB users = new UsersDB();
			int roleID = (int) rolesList.DataKeys[e.Item.ItemIndex];
			bool enable = true; // enable add - bja

			if (e.CommandName == "edit") 
			{
				// Set editable list item index if "edit" button clicked next to the item
				rolesList.EditItemIndex = e.Item.ItemIndex;
				// disable the add function
				enable = false;
				// Repopulate the datalist control
				BindData();
			}

			else if (e.CommandName == "apply") 
			{
				// Apply changes
				string _roleName = ((TextBox) e.Item.FindControl("roleName")).Text;

				// update database
				users.UpdateRole(roleID, _roleName);

				// Disable editable list item access
				rolesList.EditItemIndex = -1;

				// Repopulate the datalist control
				BindData();
			}
			else if (e.CommandName == "delete") 
			{

				// john.mandia@whitelightsolutions.com: 30th May 2004: Added Try And Catch To Delete Role
				// update database
				try
				{
					users.DeleteRole(roleID);

				}
				catch
				{
					labelError.Visible = true;
				}
				// End of john.mandia@whitelightsolutions.com Update

				// Ensure that item is not editable
				rolesList.EditItemIndex = -1;

				// Repopulate list
				BindData();
			}
			else if (e.CommandName == "members") 
			{

				// Save role name changes first
				string _roleName = ((TextBox) e.Item.FindControl("roleName")).Text;
				users.UpdateRole(roleID, _roleName);

				// redirect to edit page
				Response.Redirect(HttpUrlBuilder.BuildUrl("~/DesktopModules/Roles/SecurityRoles.aspx", TabID, "mID=" + ModuleID.ToString() + "&roleID=" + roleID + "&rolename=" + _roleName ));
			} 
			// reset the enable state of the add
			// set add button -- bja
			AddRoleBtn.Enabled = enable;
		}


		/// <summary>
		/// The BindData helper method is used to bind the list of 
		/// security roles for this portal to an asp:datalist server control
		/// </summary>
        private void BindData() 
        {
            // Get the portal's roles from the database
			UsersDB users = new UsersDB();
        
			System.Data.SqlClient.SqlDataReader dr = users.GetPortalRoles(portalSettings.PortalID);
            rolesList.DataSource = dr;
            rolesList.DataBind();
			dr.Close(); //by Manu, fixed bug 807858
        }
   
		/// <summary>
		/// Guid
		/// </summary>
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{A406A674-76EB-4BC1-BB35-50CD2C251F9C}");
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

        private void InitializeComponent() 
        {
			this.rolesList.ItemCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.RolesList_ItemCommand);
			this.rolesList.ItemDataBound += new System.Web.UI.WebControls.DataListItemEventHandler(this.RolesList_ItemDataBound);
			this.AddRoleBtn.Click += new System.EventHandler(this.AddRole_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
    }
}
