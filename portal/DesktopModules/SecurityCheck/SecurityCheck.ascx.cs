using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Rainbow.UI;
using Rainbow.UI.DataTypes;
using Rainbow.UI.WebControls;
using Rainbow.Security;
using Rainbow.Configuration;
using Rainbow.Helpers;
using Esperantus;

namespace Rainbow.DesktopModules
{
	public class SecurityCheck : PortalModuleControl 
	{
		protected Esperantus.WebControls.Literal label_description;
		protected System.Web.UI.WebControls.DropDownList ddlRoles;
		protected Esperantus.WebControls.Button btnSearch;
		protected Esperantus.WebControls.ImageButton RoleDeleteBtn;
		protected DataSet dsModules ;
		protected System.Data.DataView myDataView;
		protected string sortField;
		protected System.Web.UI.WebControls.DataGrid dgModules;
		protected Esperantus.WebControls.CheckBox chkAdmin;
		protected string sortDirection;
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
			// If this is the first visit to the page, bind the role data to the DropDownList
			if (Page.IsPostBack == false) 
			{
				BindRoles();
				sortField = "TabName";
				sortDirection = "ASC";
				ViewState["SORTFIELD"] = sortField;
				ViewState["SORTDIRECTION"] = sortDirection;
			}
			else
			{
				sortField = (string) ViewState["SORTFIELD"];
				sortDirection = (string) ViewState["SORTDIRECTION"];
			}
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public SecurityCheck()
		{
		}

		/// <summary>
		/// Guid
		/// </summary>
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{8F74C9C4-543A-48fa-AB73-1C07D219899A}");
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
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.dgModules.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgModules_PageIndexChanged);
			this.dgModules.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.dgModules_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		# region Install / Uninstall Implementation
		public override void Install(System.Collections.IDictionary stateSaver)
		{
			// Don't do Anything
		}

		public override void Uninstall(System.Collections.IDictionary stateSaver)
		{
			// Don't do Anything
		}
		#endregion
		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			BindData();
			BindGrid();
		}

		private void dgModules_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dgModules.CurrentPageIndex = e.NewPageIndex;
			BindData();
			BindGrid();
		}

		private void dgModules_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			if (sortField == e.SortExpression && sortDirection == "ASC")
				sortDirection = "DESC";
			else
				sortDirection = "ASC";

			ViewState["SORTFIELD"] = e.SortExpression;
			ViewState["SORTDIRECTION"] = sortDirection;
			BindData();
			BindGrid();
		}
		/// <summary>
		/// This method is used to bind the list of 
		/// security roles for this portal to an asp:dropdownlist server control
		/// </summary>
		private void BindRoles() 
		{
			// Get the portal's roles from the database
			UsersDB users = new UsersDB();
        
			System.Data.SqlClient.SqlDataReader dr = users.GetPortalRoles(portalSettings.PortalID);
			ddlRoles.DataSource = dr;
			ddlRoles.DataBind();
			dr.Close(); 
			ListItem noAdminItem = new ListItem();
			noAdminItem.Text = Esperantus.Localize.GetString ( "SECURITYCHECK_NOADMIN", "No Admin");
			noAdminItem.Value = "-1";
			ddlRoles.Items.Add(noAdminItem);
		}
   
		/// <summary>
		/// The BindData method on this User Control is used to obtain a DataSet of Modules'security information 
		/// from the rb_Modules table
		/// </summary>
		private void BindData()
		{
			StringBuilder select = new StringBuilder(string.Empty, 2048);

			if (ddlRoles.SelectedValue == "-1")
			{
				// Search for all non Admin roles
				select.Append("SELECT ModuleID, TabName, ModuleTitle, FriendlyName, IsAdmin = \n");
				select.Append("Case Admin\n");
				select.Append("When 1 Then 'X'\n");
				select.Append("Else ' '\n");
				select.Append("End,\n");
				select.Append("CanView = \n");
				select.Append("Case AuthorizedViewRoles\n");
				select.Append("When 'Admins' then ' '\n");
				select.Append("When 'Admins;' then ' '\n");
				select.Append("Else 'X'\n");
				select.Append("End,\n");
				select.Append("CanEdit = \n");
				select.Append("Case AuthorizedEditRoles\n");
				select.Append("When 'Admins' then ' '\n");
				select.Append("When 'Admins;' then ' '\n");
				select.Append("Else 'X'\n");
				select.Append("End,\n");
				select.Append("CanAdd = \n");
				select.Append("Case AuthorizedAddRoles\n");
				select.Append("When 'Admins' then ' '\n");
				select.Append("When 'Admins;' then ' '\n");
				select.Append("Else 'X'\n");
				select.Append("End,\n");
				select.Append("CanDelete = \n");
				select.Append("Case AuthorizedDeleteRoles\n");
				select.Append("When 'Admins' then ' '\n");
				select.Append("When 'Admins;' then ' '\n");
				select.Append("Else 'X'\n");
				select.Append("End,\n");
				select.Append("CanProperties = \n");
				select.Append("Case AuthorizedPropertiesRoles\n");
				select.Append("When 'Admins' then ' '\n");
				select.Append("When 'Admins;' then ' '\n");
				select.Append("Else 'X'\n");
				select.Append("End,\n");
				select.Append("CanMove = \n");
				select.Append("Case AuthorizedMoveModuleRoles\n");
				select.Append("When 'Admins' then ' '\n");
				select.Append("When 'Admins;' then ' '\n");
				select.Append("Else 'X'\n");
				select.Append("End,\n");
				select.Append("CanDeleteModule = \n");
				select.Append("Case AuthorizedDeleteModuleRoles\n");
				select.Append("When 'Admins' then ' '\n");
				select.Append("When 'Admins;' then ' '\n");
				select.Append("Else 'X'\n");
				select.Append("End\n");
				select.Append("from rb_Modules\n");
				select.Append("Inner Join rb_Tabs on rb_Modules.TabID = rb_Tabs.TabID and PortalID=" + PortalID.ToString() + "\n");
				select.Append("Inner Join rb_ModuleDefinitions on rb_Modules.ModuleDefID = rb_ModuleDefinitions.ModuleDefID\n");
				select.Append("Inner Join rb_GeneralModuleDefinitions on rb_ModuleDefinitions.GeneralModDefID = rb_GeneralModuleDefinitions.GeneralModDefID\n");
			}
			else
			{
				// Search for a specific Role
				string roleName = ddlRoles.SelectedItem.Text;

				select.Append("SELECT ModuleID, TabName, ModuleTitle, FriendlyName, IsAdmin = \n");
				select.Append("Case Admin\n");
				select.Append("When 1 Then 'X'\n");
				select.Append("Else ' '\n");
				select.Append("End,\n");
				select.Append("CanView = \n");
				select.Append("Case PATINDEX('%" + roleName + 
					"%', Replace(Replace(Replace(AuthorizedViewRoles, 'Authenticated Users', '" +
					roleName + "'), 'Unauthenticated Users', '" +
					roleName + "'), 'All Users', '" + roleName + "'))\n");
				select.Append("When 0 then ' '\n");
				select.Append("Else 'X'\n");
				select.Append("End,\n");
				select.Append("CanEdit = \n");
				select.Append("Case PATINDEX('%" + roleName + 
					"%', Replace(Replace(Replace(AuthorizedEditRoles, 'Authenticated Users', '" +
					roleName + "'), 'Unauthenticated Users', '" +
					roleName + "'), 'All Users', '" + roleName + "'))\n");
				select.Append("When 0 then ' '\n");
				select.Append("Else 'X'\n");
				select.Append("End,\n");
				select.Append("CanAdd = \n");
				select.Append("Case PATINDEX('%" + roleName + 
					"%', Replace(Replace(Replace(AuthorizedAddRoles, 'Authenticated Users', '" +
					roleName + "'), 'Unauthenticated Users', '" +
					roleName + "'), 'All Users', '" + roleName + "'))\n");
				select.Append("When 0 then ' '\n");
				select.Append("Else 'X'\n");
				select.Append("End,\n");
				select.Append("CanDelete = \n");
				select.Append("Case PATINDEX('%" + roleName + 
					"%', Replace(Replace(Replace(AuthorizedDeleteRoles, 'Authenticated Users', '" +
					roleName + "'), 'Unauthenticated Users', '" +
					roleName + "'), 'All Users', '" + roleName + "'))\n");
				select.Append("When 0 then ' '\n");
				select.Append("Else 'X'\n");
				select.Append("End,\n");
				select.Append("CanProperties = \n");
				select.Append("Case PATINDEX('%" + roleName + 
					"%', Replace(Replace(Replace(AuthorizedPropertiesRoles, 'Authenticated Users', '" +
					roleName + "'), 'Unauthenticated Users', '" +
					roleName + "'), 'All Users', '" + roleName + "'))\n");
				select.Append("When 0 then ' '\n");
				select.Append("Else 'X'\n");
				select.Append("End,\n");
				select.Append("CanMove = \n");
				select.Append("Case PATINDEX('%" + roleName + 
					"%', Replace(Replace(Replace(AuthorizedMoveModuleRoles, 'Authenticated Users', '" +
					roleName + "'), 'Unauthenticated Users', '" +
					roleName + "'), 'All Users', '" + roleName + "'))\n");
				select.Append("When 0 then ' '\n");
				select.Append("Else 'X'\n");
				select.Append("End,\n");
				select.Append("CanDeleteModule = \n");
				select.Append("Case PATINDEX('%" + roleName + 
					"%', Replace(Replace(Replace(AuthorizedDeleteModuleRoles, 'Authenticated Users', '" +
					roleName + "'), 'Unauthenticated Users', '" +
					roleName + "'), 'All Users', '" + roleName + "'))\n");
				select.Append("When 0 then ' '\n");
				select.Append("Else 'X'\n");
				select.Append("End\n");
				select.Append("from rb_Modules\n");
				select.Append("Inner Join rb_Tabs on rb_Modules.TabID = rb_Tabs.TabID and PortalID=" + PortalID.ToString() + "\n");
				select.Append("Inner Join rb_ModuleDefinitions on rb_Modules.ModuleDefID = rb_ModuleDefinitions.ModuleDefID\n");
				select.Append("Inner Join rb_GeneralModuleDefinitions on rb_ModuleDefinitions.GeneralModDefID = rb_GeneralModuleDefinitions.GeneralModDefID\n");
			}

			if (chkAdmin.Checked)
			{
				select.Append("Where Admin = 1\n");
			}

			select.Append("order by TabName");
			string selectSQL = select.ToString();

			SqlConnection sqlConnection = PortalSettings.SqlConnectionString;
			SqlDataAdapter sqlCommand = new SqlDataAdapter(selectSQL, sqlConnection);

			try 
			{
				sqlConnection.Open();
				dsModules = new DataSet();
				sqlCommand.Fill(dsModules);
				sqlConnection.Close();
			}
			catch (Exception e)
			{
				Rainbow.Configuration.ErrorHandler.HandleException("Error in Search: " + e.ToString() + " " + select.ToString(), e);
				throw new Exception("Error in Search selection.");
			}

			myDataView = new DataView();
			myDataView = dsModules.Tables[0].DefaultView;
			
		}

		private void BindGrid()
		{

			myDataView.Sort = sortField + " " + sortDirection;
			dgModules.DataSource = myDataView;
			dgModules.DataBind();
			
		}

	}
}