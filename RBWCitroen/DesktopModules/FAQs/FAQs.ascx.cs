using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections;

using Rainbow.UI;
using Rainbow.UI.WebControls;

//using System.Web.UI.HtmlControls;
using Rainbow.UI.DataTypes;
using Rainbow.Configuration;


namespace Rainbow.DesktopModules
{
	/// <summary>
	/// IBS Portal FAQ module
	/// (c)2002 by Christopher S Judd, CDP &amp; Horizons, LLC
	/// Moved into Rainbow by Jakob Hansen, hansen3000@hotmail.com
	/// </summary>
	public class FAQs : PortalModuleControl 
	{
		protected System.Web.UI.WebControls.Literal Literal1;
		protected System.Web.UI.WebControls.DataList myDataList;


        /// <summary>
        /// FAQs constructor added 10/27/03 by Chris Farrell, chris@cftechconsulting.com
        /// </summary>
        public FAQs() 
		{
			// Set Editor Settings jviladiu@portalservices.net 2004/07/30
			HtmlEditorDataType.HtmlEditorSettings (this._baseSettings, SettingItemGroup.MODULE_SPECIAL_SETTINGS);

			SupportsWorkflow = false;
        }

		/// <summary>
		/// The Page_Load event on this page calls the BindData() method
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e)
		{
			BindData();
		}


		/// <summary>
		/// The myDataList_ItemCommand function is used to 
		/// determine the FAQ the user selected in the form
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void myDataList_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
		{
			// hide answer if shown, show answer if hidden
			if (myDataList.SelectedIndex == e.Item.ItemIndex) 
				myDataList.SelectedIndex = -1;
			else 
				myDataList.SelectedIndex = e.Item.ItemIndex;

			BindData();
		}

		
		/// <summary>
		/// The Binddata method on this User Control is used to
		/// obtain a DataReader of event information from the FAQ
		/// table, and then databind the results to a templated DataList
		/// server control. It uses the Rainbow.FAGsDB()
		/// data component to encapsulate all data functionality.
		/// </summary>
		private void BindData()
		{
			FAQsDB questions = new FAQsDB();
			myDataList.DataSource = questions.GetFAQ(ModuleID);
			myDataList.DataBind();
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
		/// <param name="userID">Id of the user is searching</param>
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

			Rainbow.Helpers.SearchDefinition s = new Rainbow.Helpers.SearchDefinition("rb_FAQs", "Question", "Answer", "CreatedByUser", "CreatedDate", searchField);
			
			//Add here extra search fields, this way
			//s.ArrSearchFields.Add("itm.ExtraFieldToSearch");

			// Builds and returns the SELECT query
			return s.SearchSqlSelect(portalID, userID, searchString);
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


		public override Guid GuidID 
		{
			get
			{
				return new Guid("{2502DB18-B580-4F90-8CB4-C15E6E531000}");
			}
		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			InitializeComponent();

			// No need for viewstate here. - jminond
			this.myDataList.EnableViewState = false;


			// Create a new Title the control
//			ModuleTitle = new DesktopModuleTitle();
			// Set here title properties
			// Add support for the edit page
			this.AddUrl = "~/DesktopModules/FAQs/FAQsEdit.aspx";
			// Add title ad the very beginning of 
			// the control's controls collection
//			Controls.AddAt(0, ModuleTitle);

			// Call base init procedure
			base.OnInit(e);
		}
		
		/// <summary>
		///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.myDataList.ItemCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.myDataList_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	}
}
