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


namespace Rainbow.DesktopModules
{
    public class Announcements : PortalModuleControl 
    {
        protected System.Web.UI.WebControls.DataList myDataList;

        /// <summary>
        /// The Page_Load event handler on this User Control is used to
        /// obtain a DataSet of announcement information from the Announcements
        /// table, and then databind the results to a templated DataList
        /// server control.  It uses the Rainbow.AnnouncementsDB()
        /// data component to encapsulate all data functionality. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e) 
        {
			if (Page.IsPostBack == false)
			{
				//myDataList appearance
				if(bool.Parse(Settings["ShowBorder"].ToString()))
					myDataList.ItemStyle.BorderWidth=Unit.Pixel(1);

				myDataList.RepeatDirection=(Settings["RepeatDirectionSetting"].ToString() == "Horizontal" ? RepeatDirection.Horizontal : 
					RepeatDirection.Vertical);

				myDataList.RepeatColumns=Int32.Parse(Settings["RepeatColumns"].ToString());

				//myDataList content
				string sortField = Settings["SortField"].ToString();
				string sortDirection = Settings["SortDirection"].ToString();

				// Obtain announcement information from Announcements table
				// and bind to the datalist control
				AnnouncementsDB announcements = new AnnouncementsDB();

				DataSet announces = announcements.GetAnnouncements(ModuleID, Version);
				
				DataView myDataView = new DataView();
				myDataView = announces.Tables[0].DefaultView;
				myDataView.Sort = sortField + " " + sortDirection;
				
				myDataList.DataSource = myDataView;
				myDataList.DataBind();
			}
        }

        /// <summary>
        /// Public constructor. Sets base settings for module.
        /// </summary>
        public Announcements() 
        {
			// Set Editor Settings jviladiu@portalservices.net 2004/07/30
			HtmlEditorDataType.HtmlEditorSettings (this._baseSettings, SettingItemGroup.MODULE_SPECIAL_SETTINGS);

			//Custom settings
            SettingItem DelayExpire = new SettingItem(new IntegerDataType());
            DelayExpire.Value = "60";
            DelayExpire.MinValue = 0;
            DelayExpire.MaxValue = 3650; //10 years
			DelayExpire.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			DelayExpire.Description =string.Empty;
            this._baseSettings.Add("DelayExpire", DelayExpire);

			//Indah Fuldner
			SettingItem RepeatDirection = new SettingItem(new ListDataType("Vertical;Horizontal"));
			RepeatDirection.Required=true;
			RepeatDirection.Value = "Vertical";
			RepeatDirection.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			RepeatDirection.Description =string.Empty;
			this._baseSettings.Add("RepeatDirectionSetting", RepeatDirection);

			SettingItem RepeatColumn = new SettingItem(new IntegerDataType());
			RepeatColumn.Required=true;
			RepeatColumn.Value = "1";
			RepeatColumn.MinValue=1;
			RepeatColumn.MaxValue=10;
			RepeatColumn.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			RepeatColumn.Description =string.Empty;
			this._baseSettings.Add("RepeatColumns", RepeatColumn);

			SettingItem showItemBorder = new SettingItem(new BooleanDataType());
			showItemBorder.Value="false";        
			showItemBorder.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			showItemBorder.Description =string.Empty;
			this._baseSettings.Add("ShowBorder", showItemBorder);
			//End Indah Fuldner

			SettingItem setSortField = new SettingItem(new ListDataType("Title;CreatedDate;ExpireDate"));
			setSortField.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			setSortField.Required = true;
			setSortField.EnglishName = "Sort Field";
			setSortField.Value = "ExpireDate";
			this._baseSettings.Add("SortField", setSortField);

			SettingItem setSortDirection = new SettingItem(new ListDataType("ASC;DESC"));
			setSortDirection.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			setSortDirection.Required = true;
			setSortDirection.EnglishName = "Sort Direction";
			setSortDirection.Value = "DESC";
			this._baseSettings.Add("SortDirection", setSortDirection);

			// Change by Geert.Audenaert@Syntegra.Com
			// Date: 27/2/2003
			SupportsWorkflow = true;
			// End Change Geert.Audenaert@Syntegra.Com
        }
 
		#region General Implementation
        /// <summary>
        /// GuidID 
        /// </summary>
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{CE55A821-2449-4903-BA1A-EC16DB93F8DB}");
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
			Rainbow.Helpers.SearchDefinition s = new Rainbow.Helpers.SearchDefinition("rb_Announcements", "Title", "Description", "CreatedByUser", "CreatedDate", searchField);
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
			this.AddUrl = "~/DesktopModules/Announcements/AnnouncementsEdit.aspx";
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

