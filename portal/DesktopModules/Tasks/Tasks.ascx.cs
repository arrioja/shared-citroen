using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Rainbow;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Rainbow.Security;
using Rainbow.Configuration;
using Esperantus;
using Rainbow.DesktopModules;


namespace Rainbow.DesktopModules
{

	/// <summary>
	/// Portal Tasks module - Task list tool
	/// Written by: ?
	/// Moved into Rainbow by Jakob Hansen, hansen3000@hotmail.com
	/// </summary>
	public class Tasks : PortalModuleControl
	{
		protected System.Web.UI.WebControls.DataGrid myDataGrid;
		protected System.Data.DataView myDataView;
		protected string sortField;
		protected string sortDirection;
		protected System.Web.UI.WebControls.Literal Literal1;


        /// <summary>
        /// The Page_Load event handler on this User Control is used to
        /// obtain a DataReader of task information from the Tasks
        /// table, and then databind the results to a templated DataList
        /// server control.  It uses the Rainbow.TasksDB()
        /// data component to encapsulate all data functionality.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Page.IsPostBack == false)
			{
				sortField = Settings["TASKS_SORT_FIELD"].ToString();
				sortDirection = "ASC";
//				if (sortField == "DueDate")
//					sortDirection = "DESC";
				ViewState["SortField"] = sortField;
				ViewState["SortDirection"] = sortDirection;
			}
			else
			{
				sortField = (string) ViewState["SortField"];
				sortDirection = (string) ViewState["sortDirection"];
			}

			myDataView = new DataView();

			// Obtain task information from Tasks table
			// and bind to the DataGrid Control
			TasksDB tasks = new TasksDB();

			DataSet taskData = tasks.GetTasks(ModuleID);
			myDataView = taskData.Tables[0].DefaultView;

			if (!Page.IsPostBack)
				myDataView.Sort = sortField + " " + sortDirection;

			BindGrid();
		}


		/// <summary>
		/// The SortTasks event handler sorts the task list (a DataGrid control)
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		protected void SortTasks(Object source, DataGridSortCommandEventArgs e)
		{
			if (sortField == e.SortExpression)
			{
				if (sortDirection == "ASC")
					sortDirection = "DESC";
				else
					sortDirection = "ASC";
			}
			else
			{
				if (e.SortExpression == "DueDate")
					sortDirection = "DESC";
			}

			ViewState["SortField"] = e.SortExpression;
			ViewState["sortDirection"] = sortDirection;

			myDataView.Sort = e.SortExpression + " " + sortDirection;
			BindGrid();
		}

		protected void BindGrid()
		{
			myDataGrid.DataSource = myDataView;
			myDataGrid.DataBind();
		}

		protected string PerCent(object val)
		{
			string left = Convert.ToString(val);
			int newVal=Convert.ToInt32(val);
			if (newVal==0)
				return "<td width=100% class='Normal'>&nbsp;0%</td>" ;
			if (newVal==100)
				return "<td class='Task_Complete' width=100% align=right>100%&nbsp;</td>";
			if (newVal<50)
				return "<td class='Task_Beginning' width="+left+"%>&nbsp;</td><td class='Normal'>&nbsp;"+left+"%</td>" ;
			else
				return "<td class='Task_NearlyComplete' align=right width="+left+"%>"+left+"%&nbsp;</td><td class='Normal'>&nbsp;</td>" ;
		}

		/// <summary>
		/// If the module is searchable you
		/// must override the property to return true
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

			Rainbow.Helpers.SearchDefinition s = new Rainbow.Helpers.SearchDefinition("rb_Tasks", "Title", "Description", "CreatedByUser", "CreatedDate", searchField);

			//Add here extra search fields, this way
			//s.ArrSearchFields.Add("itm.ExtraFieldToSearch");

			// Builds and returns the SELECT query
			return s.SearchSqlSelect(portalID, userID, searchString);
		}


		public override Guid GuidID
		{
			get
			{
				return new Guid("{2502DB18-B580-4F90-8CB4-C15E6E531012}");
			}
		}

        /// <summary>
        /// Public constructor. Sets base settings for module.
        /// </summary>
        public Tasks()
        {
			// Set Editor Settings jviladiu@portalservices.net 2004/07/30
			HtmlEditorDataType.HtmlEditorSettings (this._baseSettings, SettingItemGroup.MODULE_SPECIAL_SETTINGS);

			SettingItem setSortField = new SettingItem(new ListDataType("Title;Status;Priority;DueDate;AssignedTo;PercentComplete"));
			setSortField.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			setSortField.Required = true;
			setSortField.Value = "DueDate";
			this._baseSettings.Add("TASKS_SORT_FIELD", setSortField);

			SettingItem defaultAssignee = new SettingItem(new StringDataType());
			defaultAssignee.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			defaultAssignee.Value = "nobody";
			defaultAssignee.EnglishName = "Default Assignee";
			defaultAssignee.Description = "Is the name of the person which the task is automatically assigned.";
			this._baseSettings.Add("TASKS_DEFAULT_ASSIGNEE", defaultAssignee);		

			// Task modules list
			Rainbow.Configuration.ModulesDB m = new Rainbow.Configuration.ModulesDB();
			ArrayList taskModulesListOptions = new ArrayList();
			System.Data.SqlClient.SqlDataReader r = null;
			try
			{
				r = m.GetModulesByName("Tasks", portalSettings.PortalID);
				while(r.Read())
				{
					taskModulesListOptions.Add(new SettingOption(int.Parse(r["ModuleID"].ToString()), r["ModuleTitle"].ToString()));
				}
			}
			catch //install time
			{
			} 
			finally
			{
				if (r != null && r.IsClosed == false)
					r.Close();
			}

			SettingItem linkedModules = new SettingItem(new MultiSelectListDataType(taskModulesListOptions, "Name", "Val"));
			linkedModules.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			linkedModules.Value = "0";
			linkedModules.EnglishName = "Linked Modules";
			linkedModules.Description = "Chose here any module that will automatically recieve a copy of all new assigned task.";
			this._baseSettings.Add("TASKS_LINKED_MODULES", linkedModules);
		}


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


		#region Web Form Designer generated code
        /// <summary>
        /// Raises OnInitEvent
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
			InitializeComponent();
			this.AddUrl = "~/DesktopModules/Tasks/TasksEdit.aspx";
			base.OnInit(e);
		}

        private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
