using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow.Admin;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// Articles
	/// </summary>
    public class Articles : PortalModuleControl
    {
        protected System.Web.UI.WebControls.DataList myDataList;
        protected System.Web.UI.WebControls.HyperLink PropertiesLink;
		
		/// <summary>
		/// 
		/// </summary>
		protected bool ShowDate
		{
			get
			{
				// Hide/show date
				return bool.Parse(Settings["ShowDate"].ToString());
			}
		}

		/// <summary>
		/// The Page_Load event handler on this User Control is used to
		/// obtain a DataReader of Article information from the Articles
		/// table, and then databind the results to a templated DataList
		/// server control.  It uses the Rainbow.ArticleDB()
		/// data component to encapsulate all data functionality.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e)
        {
			if (!IsPostBack)
			{
				// Obtain Articles information from the Articles table
				// and bind to the datalist control
				ArticlesDB Articles = new ArticlesDB();
				myDataList.DataSource = Articles.GetArticles(ModuleID);
				myDataList.DataBind();
			}
        }
      
        public Articles()
        {
			// Set Editor Settings jviladiu@portalservices.net 2004/07/30
			HtmlEditorDataType.HtmlEditorSettings (this._baseSettings, SettingItemGroup.MODULE_SPECIAL_SETTINGS);

			//Switches date display on/off
			SettingItem ShowDate = new SettingItem(new BooleanDataType());
			ShowDate.Value = "True";
			ShowDate.EnglishName = "Show Date";
			ShowDate.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			ShowDate.Order = 10;
			this._baseSettings.Add("ShowDate", ShowDate);

           //Added by Rob Siera
           SettingItem DefaultVisibleDays = new SettingItem(new IntegerDataType());
           DefaultVisibleDays.Value = "90";
           DefaultVisibleDays.EnglishName = "Default Days Visible";
           DefaultVisibleDays.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
           DefaultVisibleDays.Order = 20;
           this._baseSettings.Add("DefaultVisibleDays", DefaultVisibleDays);
           //
        }

		#region General Implementation
		/// <summary>
		/// Guid
		/// </summary>
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{87303CF7-76D0-49B1-A7E7-A5C8E26415BA}");
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
			Rainbow.Helpers.SearchDefinition s = new Rainbow.Helpers.SearchDefinition("rb_Articles", "Title", "Abstract", "CreatedByUser", "CreatedDate", searchField);

			//Add extra search fields here, this way
			s.ArrSearchFields.Add("itm.Description");

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

			// View state is not needed here, so we are disabling. - jminond
			this.myDataList.EnableViewState = false;

			// Create a new Title the control
//			ModuleTitle = new DesktopModuleTitle();
			// Set here title properties
			// Add support for the edit page
			this.AddText = "ADD_ARTICLE";
			this.AddUrl = "~/DesktopModules/Articles/ArticlesEdit.aspx";
			// Add title ad the very beginning of 
			// the control's controls collection
//			Controls.AddAt(0, ModuleTitle);
		
			base.OnInit(e);
		}

        private void InitializeComponent() 
        {
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
    }
}