using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes; 
using Rainbow.Configuration;

using Esperantus;

namespace Rainbow.DesktopModules
{
    public class Contacts : PortalModuleControl 
	{
        protected System.Web.UI.WebControls.DataGrid myDataGrid;
		protected System.Data.DataView myDataView;
		protected string sortField;
		protected string sortDirection;

		/// <summary>
        /// The Page_Load event handler on this User Control is used to
        /// obtain a DataReader of contact information from the Contacts
        /// table, and then databind the results to a DataGrid
        /// server control.  It uses the Rainbow.ContactsDB()
        /// data component to encapsulate all data functionality.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e)
		{
 		    //MH: Added  01/10/2003 [mario@hartmann.net]
			//set visibility of the columns
			myDataGrid.Columns[3].Visible =(Settings["SHOW_COLUMN_EMAIL"]!=null)	? bool.Parse(Settings["SHOW_COLUMN_EMAIL"].ToString()): true;			
			myDataGrid.Columns[4].Visible =(Settings["SHOW_COLUMN_CONTACT1"]!=null)	? bool.Parse(Settings["SHOW_COLUMN_CONTACT1"].ToString()): true;
			myDataGrid.Columns[5].Visible =(Settings["SHOW_COLUMN_CONTACT2"]!=null)	? bool.Parse(Settings["SHOW_COLUMN_CONTACT2"].ToString()): true;
			myDataGrid.Columns[6].Visible =(Settings["SHOW_COLUMN_FAX"]!=null)	? bool.Parse(Settings["SHOW_COLUMN_FAX"].ToString()): true;
			myDataGrid.Columns[7].Visible =(Settings["SHOW_COLUMN_ADDRESS"]!=null)	? bool.Parse(Settings["SHOW_COLUMN_ADDRESS"].ToString()): true;
            //MH: End

			if (Page.IsPostBack == false) 
			{
				sortField = "Name";
				sortDirection = "ASC";
				if (sortField == "DueDate")
					sortDirection = "DESC";
				ViewState["SortField"] = sortField;
				ViewState["SortDirection"] = sortDirection;
			}
			else
			{
				sortField = (string) ViewState["SortField"];
				sortDirection = (string) ViewState["sortDirection"];
			}

			myDataView = new DataView();

			// Obtain contact information from Contacts table
			// and bind to the DataGrid Control
			ContactsDB contacts = new ContactsDB();
    
			DataSet contactData = contacts.GetContacts(ModuleID, Version);
			myDataView = contactData.Tables[0].DefaultView;

			if (!Page.IsPostBack)
				myDataView.Sort = sortField + " " + sortDirection;

			BindGrid();
		}

		protected void BindGrid()
		{
			myDataGrid.DataSource = myDataView;
			myDataGrid.DataBind();
		}
 
		public Contacts()
		{
			// Change by Geert.Audenaert@Syntegra.Com
			// Date: 27/2/2003
			SupportsWorkflow = true;
			// End Change Geert.Audenaert@Syntegra.Com

			//MH: Added  01/10/2003 [mario@hartmann.net] 
			// Hiding the Email, Contact1 and/or Contact2 Column
			SettingItem setItem = new SettingItem(new BooleanDataType());
			setItem.Value = "True";
			setItem.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			setItem.Description = "Switch for displaying the email column or not.";
			setItem.Order = 1;
			this._baseSettings.Add("SHOW_COLUMN_EMAIL", setItem);

			setItem = new SettingItem(new BooleanDataType());
			setItem.Value = "True";
			setItem.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			setItem.Description = "Switch for displaying the contact1 column or not.";
			setItem.Order = 1;
			this._baseSettings.Add("SHOW_COLUMN_CONTACT1", setItem);

			setItem = new SettingItem(new BooleanDataType());
			setItem.Value = "True";
			setItem.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			setItem.Description = "Switch for displaying the contact2 column or not.";
			setItem.Order = 2;
			this._baseSettings.Add("SHOW_COLUMN_CONTACT2", setItem);
			//MH: End

			setItem = new SettingItem(new BooleanDataType());
			setItem.Value = "True";
			setItem.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			setItem.Description = "Switch for displaying the Fax column or not.";
			setItem.Order = 3;
			this._baseSettings.Add("SHOW_COLUMN_FAX", setItem);

			setItem = new SettingItem(new BooleanDataType());
			setItem.Value = "True";
			setItem.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			setItem.Description = "Switch for displaying the Address column or not.";
			setItem.Order = 4;
			this._baseSettings.Add("SHOW_COLUMN_ADDRESS", setItem);

		}
 
		#region Global Implementation
        /// <summary>
        /// GuidID
        /// </summary>
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{2502DB18-B580-4F90-8CB4-C15E6E5339EF}");
			}
		}

		#region Search Implementation
		/// <summary>
		/// Searchable module
		/// </summary>
		public override bool Searchable
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// Searchable module implementation
		/// </summary>
		/// <param name="portalID">The portal ID</param>
		/// <param name="userID">ID of the user is searching</param>
		/// <param name="searchString">The text to search</param>
		/// <param name="searchField">The fields where perfoming the search</param>
		/// <returns>The SELECT sql to perform a search on the current module</returns>
		public override string SearchSqlSelect(int portalID, int userID, string searchString, string searchField)
		{
			// Parameters:
			// Table Name: the table that holds the data
			// Title field: the field that contains the title for result, must be a field in the table
			// Abstract field: the field that contains the text for result, must be a field in the table
			// Search field: pass the searchField parameter you recieve.
			Rainbow.Helpers.SearchDefinition s = new Rainbow.Helpers.SearchDefinition("rb_Contacts", "Role", "Name", "CreatedByUser", "CreatedDate", searchField);
			
			//Add extra search fields here, this way
			s.ArrSearchFields.Add("itm.Email");
			s.ArrSearchFields.Add("itm.Contact1");
			s.ArrSearchFields.Add("itm.Contact2");
			
			// Builds and returns the SELECT query
			return s.SearchSqlSelect(portalID, userID, searchString);
		}

		#endregion

		# region Install / Uninstall Implementation
		public override void Install(System.Collections.IDictionary stateSaver)
		{
			string currentScriptName = System.IO.Path.Combine(Server.MapPath(TemplateSourceDirectory), "install.sql");
			ArrayList errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);
			if (errors.Count > 0)
			{
				// Call rollback
				throw new Exception("Error occurred:" + errors[0].ToString());
			}
		}

		public override void Uninstall(System.Collections.IDictionary stateSaver)
		{
			string currentScriptName = System.IO.Path.Combine(Server.MapPath(TemplateSourceDirectory), "uninstall.sql");
			ArrayList errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);
			if (errors.Count > 0)
			{
				// Call rollback
				throw new Exception("Error occurred:" + errors[0].ToString());
			}
		}

		# endregion

		#endregion

		#region Web Form Designer generated code
		/// <summary>
		/// Raises Init event
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();

			// Create a new Title the control
//			ModuleTitle = new DesktopModuleTitle();
			// Set here title properties
			// Add support for the edit page
			this.AddText = "CONTACTS_ADD";
			this.AddUrl = "~/DesktopModules/Contacts/ContactsEdit.aspx";
			// Add title ad the very beginnig of 
			// the control's controls collection
//			Controls.AddAt(0, ModuleTitle);
		
			base.OnInit(e);
		}

        private void InitializeComponent() 
        {
			this.myDataGrid.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.myDataGrid_SortCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void myDataGrid_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			if (sortField == e.SortExpression)
			{
				if (sortDirection == "ASC")
					sortDirection = "DESC";
				else
					sortDirection = "ASC";
			}

			ViewState["SortField"] = e.SortExpression;
			ViewState["sortDirection"] = sortDirection;

			myDataView.Sort = e.SortExpression + " " + sortDirection;
			BindGrid();
		}

    }
}
