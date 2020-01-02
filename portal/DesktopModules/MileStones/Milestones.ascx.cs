using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

//Add Rainbow Namespaces
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.Configuration;
using Esperantus;

namespace Rainbow.DesktopModules.Milestones
{
	/// <summary>
	///	Summary description for Milestones.
	///	Notice we have changed base class from System.Web.UI.UserControl
	///	to Rainbow.UI.WebControls.PortalModuleControl
	/// </summary>
	/// Remove abstract, searchable classes cannot be abstract
	public class Milestones : Rainbow.UI.WebControls.PortalModuleControl
	{
		protected System.Web.UI.WebControls.DataGrid myDataGrid;

		private void Page_Load(object sender, System.EventArgs e)
		{

		// Added EsperantusKeys for Localization 
		// Mario Endara mario@softworks.com.uy 11/05/2004 

			foreach(DataGridColumn column in myDataGrid.Columns)
			{
				switch(myDataGrid.Columns.IndexOf(column))       
				{         
					case 1:
						column.HeaderText = Esperantus.Localize.GetString ("MILESTONES_TITLE");
						break;
					case 2:
						column.HeaderText = Esperantus.Localize.GetString("MILESTONES_COMPLETION_DATE");
						break;
					case 3:
						column.HeaderText = Esperantus.Localize.GetString("MILESTONES_STATUS");
						break;
				}
			}

			if (!Page.IsPostBack) 
			{
				// Create an instance of MilestonesDB class
				MilestonesDB milestones = new MilestonesDB();

				// Get the Milstones data for the current module.
				// ModuleID is defined on base class and contains
				// a reference to the current module.
				myDataGrid.DataSource = milestones.GetMilestones(ModuleID, Version);

				// Bind the milestones data to the grid.
				myDataGrid.DataBind();
			}
		}

		/// <summary>
		/// Override base Guid implementation 
		/// to provide an unique id for your control
		/// </summary>
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{B8784E32-688A-4b8a-87C4-DF108BF12DBE}");
			}
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

		public Milestones()
		{
			// Change by David.Verberckmoes@syntegra.com
			// Date: 20030324
			SupportsWorkflow = true;
			// End Change David.Verberckmoes@syntegra.com
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

			Rainbow.Helpers.SearchDefinition s = new Rainbow.Helpers.SearchDefinition("rb_Milestones", "Title", "Status", "CreatedByUser", "CreatedDate", searchField);
			
			//Add here extra search fields, this way
			//s.ArrSearchFields.Add("itm.ExtraFieldToSearch");

			// Builds and returns the SELECT query
			return s.SearchSqlSelect(portalID, userID, searchString);
		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			this.AddUrl = "~/DesktopModules/MileStones/MilestonesEdit.aspx";
			base.OnInit(e);
		}
		
		/// <summary>
		///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
