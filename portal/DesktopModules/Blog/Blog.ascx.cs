using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
using Esperantus;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// Author:					Joe Audette
	/// Created:				1/18/2004
	/// Last Modified:			2/8/2004
	/// </summary>
    public class Blog : PortalModuleControl
    {
        protected DataList myDataList;
        protected HyperLink PropertiesLink;
		protected HtmlAnchor lnkRSS;
		protected Label lblEntryCount;
		protected Label lblCopyright;
		protected Label lblCommentCount;
		protected HtmlImage imgRSS;
		protected Esperantus.WebControls.Literal SyndicationLabel;
		protected Esperantus.WebControls.Literal StatisticsLabel;
		protected Esperantus.WebControls.Literal ArchivesLabel;
		protected DataList dlArchive;
		protected string Feedback ;
		
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
		/// obtain a DataReader of Blog information from the Blogs
		/// table, and then databind the results to a templated DataList
		/// server control.  It uses the Rainbow.BlogDB()
		/// data component to encapsulate all data functionality.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e)
        {

			// Added EsperantusKeys for Localization 
			// Mario Endara mario@softworks.com.uy june-1-2004 
			Feedback = Esperantus.Localize.GetString ("BLOG_FEEDBACK");

			if (!IsPostBack)
			{
				lnkRSS.HRef = Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Blog/RSS.aspx",TabID,"&mID=" + ModuleID );
				imgRSS.Src = Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Blog/xml.gif");
				lblCopyright.Text = Settings["Copyright"].ToString();
				// Obtain Blogs information from the Blogs table
				// and bind to the datalist control
				BlogDB blogData = new BlogDB();
				myDataList.DataSource = blogData.GetBlogs(ModuleID);
				myDataList.DataBind();

				dlArchive.DataSource = blogData.GetBlogMonthArchive(ModuleID);
				dlArchive.DataBind();
	
				SqlDataReader dr = blogData.GetBlogStats(ModuleID);
				try
				{
					if (dr.Read())
					{
						lblEntryCount.Text = Esperantus.Localize.GetString("BLOG_ENTRIES", "Entries", null) + 
							" (" + (string) dr["EntryCount"].ToString() + ")";
						lblCommentCount.Text = Esperantus.Localize.GetString("BLOG_COMMENTS", "Comments", null) +
							" (" + (string) dr["CommentCount"].ToString() + ")";
					}
				}
				finally
				{
					// close the datareader
					dr.Close();
				}
			}
        }
      
        public Blog()
        {
			// Set Editor Settings jviladiu@portalservices.net 2004/07/30
			HtmlEditorDataType.HtmlEditorSettings (this._baseSettings, SettingItemGroup.MODULE_SPECIAL_SETTINGS);

			//Number of entries to display
			SettingItem EntriesToShow = new SettingItem(new IntegerDataType());
			EntriesToShow.Value = "10";
			EntriesToShow.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			EntriesToShow.Order = 10;
			this._baseSettings.Add("Entries To Show", EntriesToShow);

			//Channel Description
			SettingItem Description = new SettingItem(new StringDataType());
			Description.Value = "Description";
			Description.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			Description.Order = 20;
			this._baseSettings.Add("Description", Description);

			//Channel Copyright
			SettingItem Copyright = new SettingItem(new StringDataType());
			Copyright.Value = "Copyright";
			Copyright.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			Copyright.Order = 30;
			this._baseSettings.Add("Copyright", Copyright);

			//Channel Language
			SettingItem Language = new SettingItem(new StringDataType());
			Language.Value = "en-us";
			Language.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			Language.Order = 40;
			this._baseSettings.Add("Language", Language);

			//Author
			SettingItem Author = new SettingItem(new StringDataType());
			Author.Value = "Author";
			Author.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			Author.Order = 50;
			this._baseSettings.Add("Author", Author);

			//Author Email
			SettingItem AuthorEmail = new SettingItem(new StringDataType());
			AuthorEmail.Value = "author@portal.com";
			AuthorEmail.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			AuthorEmail.Order = 60;
			this._baseSettings.Add("Author Email", AuthorEmail);

			//Time to live in minutes for RSS
			//how long a channel can be cached before refreshing from the source
			SettingItem TimeToLive = new SettingItem(new IntegerDataType());
			TimeToLive.Value = "120";
			TimeToLive.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			TimeToLive.Order = 70;
			this._baseSettings.Add("RSS Cache Time In Minutes", TimeToLive);
        }

		#region General Implementation
		/// <summary>
		/// Guid
		/// </summary>
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{55EF407B-C9D6-47e3-B627-EFA6A5EEF4B2}");

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
			Rainbow.Helpers.SearchDefinition s = new Rainbow.Helpers.SearchDefinition("rb_Blogs", "Title", "Excerpt", "CreatedByUser", "CreatedDate", searchField);

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

			// Create a new Title the control
//			ModuleTitle = new DesktopModuleTitle();
			// Set here title properties
			// Add support for the edit page
			this.AddText = "ADD_ARTICLE";
			this.AddUrl = "~/DesktopModules/Blog/BlogEdit.aspx";
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