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
using Rainbow.UI;
using Rainbow.UI.WebControls;

namespace Rainbow.DesktopModules 
{
    /// <summary>
    /// by Jose Viladiu
	/// Moved into Rainbow by Jakob Hansen
	/// </summary>
    public class ComponentModule : PortalModuleControl 
    {
        protected System.Web.UI.WebControls.PlaceHolder ComponentHolder;

        /// <summary>
        /// The Page_Load event handler on this User Control is
        /// used to load and execute a user control block.
        /// The user control to execute is stored in the HtmlText 
        /// database table.  This method uses the Rainbow.HtmlTextDB()
        /// data component to encapsulate all data functionality.
        /// Is a simple variation from HtmlModule.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e) 
        {
            // Obtain the selected item from the HtmlText table
            ComponentModuleDB comp = new ComponentModuleDB();
            SqlDataReader dr = comp.GetComponentModule(ModuleID);
        
			try
			{
				if (dr.Read()) 
				{
					// Dynamically add the file content into the page
					string content = (string) dr["Component"];
					try 
					{
						ComponentHolder.Controls.Add(ParseControl(content));
					}
					catch (Exception controlError) 
					{
						ComponentHolder.Controls.Add
							(new LiteralControl("<p>Error in control: " + controlError + "<p>" + content));
					}
				}
			}
			finally
			{
				// Close the datareader
				dr.Close();     
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

			Rainbow.Helpers.SearchDefinition s = new Rainbow.Helpers.SearchDefinition("rb_ComponentModule", "Title", "Component", "CreatedByUser", "CreatedDate", searchField);
			
			//Add here extra search fields, this way
			//s.ArrSearchFields.Add("itm.ExtraFieldToSearch");

			// Builds and returns the SELECT query
			return s.SearchSqlSelect(portalID, userID, searchString, false);
		}
        
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{2B113F51-FEA3-499A-98E7-7B83C192FDBC}");
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


		#region Web Form Designer generated code
        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			InitializeComponent();

			// Create a new Title the control
//			ModuleTitle = new DesktopModuleTitle();
			// Set here title properties
			// Add support for the edit page
			this.EditText = "EDIT";
			this.AddUrl = "~/DesktopModules/ComponentModule/ComponentModuleEdit.aspx";
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
            this.Load += new System.EventHandler(this.Page_Load);

        }
		#endregion
    }
}
