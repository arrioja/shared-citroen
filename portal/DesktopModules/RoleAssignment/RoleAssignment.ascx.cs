using System;
using System.Web.UI.WebControls;
using Rainbow.Security;
using Rainbow.Configuration;
using Rainbow.UI.DataTypes; 
using Rainbow.UI.WebControls;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// Role Assignment Viewer is a module that allows the user to assign or remove 1 or more users from a particular group
	/// Created by John Mandia (www.whitelightsolutions.com)
	/// </summary>
	public class RoleAssignment : PortalModuleControl
	{		
		#region Module Controls
		protected System.Web.UI.WebControls.DropDownList MemberType;
		protected System.Web.UI.WebControls.DropDownList RoleSorter;
		protected System.Web.UI.WebControls.ListBox UserList;
		protected Esperantus.WebControls.LinkButton CriteriaSelection;
		protected Esperantus.WebControls.LinkButton AddToRole;
		protected Esperantus.WebControls.LinkButton RemoveFromRole;
		protected Esperantus.WebControls.LinkButton AddModuleBtn;
		protected Esperantus.WebControls.Literal title;
		protected System.Web.UI.WebControls.DropDownList DataTypeSelection;
		protected Esperantus.WebControls.Literal RoleSelectionAddLabel;
		protected Esperantus.WebControls.Literal RoleSelectionRemoveLabel;
		protected Esperantus.WebControls.Literal RoleName;
		protected Esperantus.WebControls.Literal Warning;
		#endregion
		
		#region Module Variables
		UsersDB users = new UsersDB();
		#endregion
		
		#region Page Load
		/// <summary>
		/// Page Load Event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Viewer_Load(object sender, System.EventArgs e)
		{
			if(!Page.IsPostBack)
				BindRoleData(RoleSorter,true);
		}
		#endregion
		
		#region Module Methods
		/// <summary>
		/// Binds A Control That Is Past To It With A Portals Roles
		/// </summary>
		/// <param name="dropDown"></param>
		/// <param name="addNoRoles"></param>
		private void BindRoleData(System.Web.UI.WebControls.DropDownList dropDown,bool addNoRoles) 
		{
			// Get the portal's roles from the database
			       
			System.Data.SqlClient.SqlDataReader roleDataReader = users.GetPortalRoles(portalSettings.PortalID);
			
			dropDown.DataSource = roleDataReader;
			dropDown.DataTextField = "RoleName";
			dropDown.DataValueField = "RoleID";
			dropDown.DataBind();
			if(addNoRoles)
				dropDown.Items.Add("Not In Any Role");
			
			roleDataReader.Close(); 

			if (PortalSecurity.IsInRoles("Admins") == false) 
			{
				// Added by Mario Endara <mario@softworks.com.uy> 2004/11/04
				// if the user is not member of the "Admins" role, remove it from the dropdownlist
				if (dropDown.Items.FindByText("Admins") != null)
					dropDown.Items.Remove(dropDown.Items.FindByText("Admins"));
			}

		}

		/// <summary>
		/// Hides the Controls that allow you to select users and either add or remove them from roles.
		/// </summary>
		private void HideUserSelectionPanel()
		{
			UserList.Visible = false;
			AddToRole.Visible = false;
			RemoveFromRole.Visible = false;
			RoleSelectionAddLabel.Visible = false;
			RoleSelectionRemoveLabel.Visible = false;
			RoleName.Visible = false;
			Warning.Visible = false;
		}
		#endregion

		#region Module Events
		/// <summary>
		/// OnClick grabs the criteria from the dropdown lists and populates the relevant controls with data.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CriteriaSelection_Click(object sender, System.EventArgs e)
		{
			// Show The User List
			UserList.Visible = true;

			// Hide The No User Selected Warning (In case They Click Criteria Button With Same Role Selected)
			Warning.Visible=false;
			
			// Retrieve users 
			System.Data.SqlClient.SqlDataReader userList;

			if(MemberType.SelectedValue.ToString()=="0")
			{
				// Show add to role 
				AddToRole.Visible = true;
				//Hide Remove From Role
				RemoveFromRole.Visible = false;
				//Show Add Label
				RoleSelectionAddLabel.Visible = true;
				// Hide Remove Label
				RoleSelectionRemoveLabel.Visible = false;

				//Make RoleName Visible
				RoleName.Visible = true;
				RoleName.Text = Esperantus.Localize.GetString("TI_ROLEADMINISTRATIONSELECTIONROLENAME", "Not In Any Role - Please select a role from the drop-down list above");
				
				if(RoleSorter.SelectedValue=="Not In Any Role")
				{
					userList = users.GetUsersNoRole(this.PortalID);
				}
				else
				{
					RoleName.Visible = true;
					RoleName.Text = RoleSorter.SelectedItem.ToString();
					userList = users.GetRoleNonMembers(Convert.ToInt32(RoleSorter.SelectedValue.ToString()),this.PortalID);
				}

				UserList.DataSource = userList;
				if(DataTypeSelection.SelectedValue == "Email")
				{
					UserList.DataTextField = "Email";
				}
				else
				{
					UserList.DataTextField = "Name";
				}
				UserList.DataValueField = "UserID";
				UserList.DataBind();
				userList.Close();
			}
			else
			{
				// Show Remove From Role Button
				RemoveFromRole.Visible = true;
				// Hide Add To Role
				AddToRole.Visible = false;
				// Retrieve users that are in the role selected

				//Hide Add Label
				RoleSelectionAddLabel.Visible = false;
				// Show Remove Label
				RoleSelectionRemoveLabel.Visible = true;
				
				userList = users.GetRoleMembers(Convert.ToInt32(RoleSorter.SelectedValue.ToString()));
				
				UserList.DataSource = userList;
				if(DataTypeSelection.SelectedValue == "Email")
				{
					UserList.DataTextField = "Email";
				}
				else
				{
					UserList.DataTextField = "Name";
				}
				UserList.DataValueField = "UserID";
				UserList.DataBind();
				userList.Close();

				RoleName.Visible = true;
				RoleName.Text = RoleSorter.SelectedItem.ToString();
			}
		}

		/// <summary>
		/// Adds Selected User(s) To Role In The Dropdown List
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddToRole_Click(object sender, System.EventArgs e)
		{
			if(RoleSorter.SelectedValue=="Not In Any Role")
			{
				RoleName.Text = Esperantus.Localize.GetString("TI_ROLEADMINISTRATIONSELECTIONROLENAMEERROR", "Not In Any Role Is Selected. Please select a different role from the drop-down list above");
			}
			else
			{
				if(UserList.SelectedIndex > -1)
				{
					foreach(ListItem i in UserList.Items)
					{ 
						if(i.Selected)
						{ 
							users.AddUserRole(Convert.ToInt32(RoleSorter.SelectedValue.ToString()),Convert.ToInt32(i.Value.ToString()));
						}     
					}
				
					HideUserSelectionPanel();
					RoleName.Text = Esperantus.Localize.GetString("TI_ROLEADMINISTRATIONSELECTIONUPDATESUCCESSFUL", "Your Update Was SuccessFul!");
					RoleName.Visible = true;
				}
				else
				{
					Warning.Visible = true;
					Warning.Text = Esperantus.Localize.GetString("TI_ROLEADMINISTRATIONWARNING", "Please Select One or More Users Before Submitting!");
				}
			}	
		}

		/// <summary>
		/// Removes the Selected User(s) from The Role Selected In The Dropdown List
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RemoveFromRole_Click(object sender, System.EventArgs e)
		{
			if(UserList.SelectedIndex > -1)
			{
				foreach(ListItem i in UserList.Items)
				{ 
					if(i.Selected)
					{ 
						users.DeleteUserRole(Convert.ToInt32(RoleSorter.SelectedValue.ToString()),Convert.ToInt32(i.Value.ToString()));     
					} 
				} 

				// Now Hide The User Area
				HideUserSelectionPanel();
				RoleName.Text = Esperantus.Localize.GetString("TI_ROLEADMINISTRATIONSELECTIONUPDATESUCCESSFUL", "Your Update Was SuccessFul!");
				RoleName.Visible = true;
			}
			else
			{
				Warning.Visible = true;
				Warning.Text = Esperantus.Localize.GetString("TI_ROLEADMINISTRATIONWARNING", "Please Select One or More Users Before Submitting!");
			}
		}

		/// <summary>
		/// Changes The Role DropDown To Either Include "Not In Any Role" Or Exclude It
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MemberType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(MemberType.SelectedValue.ToString()=="0")
			{
				BindRoleData(RoleSorter,true);
			}
			else
			{
				BindRoleData(RoleSorter,false);
			}
			HideUserSelectionPanel();
		}

		/// <summary>
		/// When the role selector is changed we need to update the "Role" Label so that administators
		/// know which role users will be assigned to.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void RoleSorter_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(MemberType.SelectedValue.ToString()=="1")
			{
				HideUserSelectionPanel();
			}

			RoleName.Text = RoleSorter.SelectedItem.ToString();
			
			// Need to ensure that the warning about the need to select a user is only shown when 
			// either the add or remove buttons are clicked and not when the RoleSorter is changed.
			Warning.Visible = false;
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
				return new Guid("{5EEE69A2-35BA-4b54-8451-E13B0CD24E99}");
			}
		}

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
		#endregion
		
		#region Web Form Designer generated code
		/// <summary>
		/// Raises Init event
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
			this.MemberType.SelectedIndexChanged += new System.EventHandler(this.MemberType_SelectedIndexChanged);
			this.RoleSorter.SelectedIndexChanged += new System.EventHandler(this.RoleSorter_SelectedIndexChanged);
			this.CriteriaSelection.Click += new System.EventHandler(this.CriteriaSelection_Click);
			this.AddToRole.Click += new System.EventHandler(this.AddToRole_Click);
			this.RemoveFromRole.Click += new System.EventHandler(this.RemoveFromRole_Click);
			this.Load += new System.EventHandler(this.Viewer_Load);
		}
		#endregion
	}
}