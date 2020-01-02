using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections;
using System.Configuration;

using Rainbow;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Rainbow.Configuration;
using Rainbow.Security;
using Rainbow.Helpers;
using Esperantus;


namespace Rainbow.DesktopModules
{
	/// <summary>
	/// Portal Search module
	/// Written by: Manu and Jakob hansen
	/// All fields in search result:
	/// ModuleName, Title, Abstract, ModuleID, ItemID, 
	/// CreatedByUser, CreatedDate, TabID, TabName, 
	/// ModuleGuidID, ModuleTitle
	/// </summary>
	[History("Mario Hartmann","mario@hartmann.net","2.1","2003/10/08","moved to separate folder")]
	[History("john.mandia@whitelightsolutions.com","2003/05/21","Changed Search Result links to use build url and fixed discussion bug")]
	[History("Jes1111","2003/04/24","Added !Cacheable property")]
	public class PortalSearch : PortalModuleControl
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.TextBox txtSearchString;
		protected System.Web.UI.WebControls.Label lblHits;
		protected Esperantus.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.DropDownList ddSearchModule;
		
		protected string sortOrder;
		protected int maxHits;
		protected bool showImage, showModuleName, showTitle, showAbstract;
		protected bool showCreatedByUser, showCreatedDate, showLink, showTabName, showTestInfo;
		protected bool showModuleTitle;
		protected System.Web.UI.WebControls.DropDownList ddSearchField;
		protected System.Web.UI.WebControls.Label lblError;
		protected Esperantus.WebControls.Literal lblModule;
		protected Esperantus.WebControls.Literal lblTopic;
		protected System.Web.UI.WebControls.DropDownList ddTopics;
		protected Esperantus.WebControls.Literal lblField;
		protected int testUserID;

		private void Page_Load(object sender, System.EventArgs e)
		{	
			lblError.Visible = false;

			sortOrder = Settings["SortOrder"].ToString();
			maxHits = Int32.Parse(Settings["MaxHits"].ToString());
			
			//showImage = "True" == Settings["ShowImage"].ToString();
			showModuleName = "True" == Settings["ShowModuleName"].ToString();
			showTitle = "True" == Settings["ShowSearchTitle"].ToString();
			showAbstract = "True" == Settings["ShowAbstract"].ToString();
			showCreatedByUser = "True" == Settings["ShowCreatedByUser"].ToString();
			showCreatedDate = "True" == Settings["ShowCreatedDate"].ToString();
			showLink = "True" == Settings["ShowLink"].ToString();
			showTabName = "True" == Settings["ShowTabName"].ToString();
			showTestInfo = "True" == Settings["ShowTestInfo"].ToString();
			showModuleTitle = "True" == Settings["ShowModuleTitle"].ToString();

			ddSearchField.Visible = "True" == Settings["showddField"].ToString(); 
			lblField.Visible = "True" == Settings["showddField"].ToString(); 
			ddTopics.Visible = "True" == Settings["showddTopic"].ToString(); 
			lblTopic.Visible = "True" == Settings["showddTopic"].ToString(); 
			ddSearchModule.Visible = "True" == Settings["showddModule"].ToString(); 
			lblModule.Visible = "True" == Settings["showddModule"].ToString(); 

			testUserID = Int32.Parse(Settings["TestUserID"].ToString());

			// Jes1111
			if (this.Cacheable)
			{
				base.Cacheable = true;
				this.ModuleConfiguration.Cacheable = true;
			}
			else
			{
				base.Cacheable = false;
				this.ModuleConfiguration.Cacheable = false;
			}

			if(!IsPostBack)
			{
				// TODO do a better databind
				SearchHelper.AddToDropDownList(this.PortalID, ref ddSearchModule);
				
				ddTopics.DataSource = SearchHelper.GetTopicList(this.PortalID);
				ddTopics.DataBind();

			    //Added by Rob Siera 19 aug 2004 - Search for the provided Default_Topic property. Select it if present.
				if(ddTopics.Items.FindByValue(Settings["defaultTopic"].ToString()) != null)
				{
					ddTopics.SelectedValue = Settings["defaultTopic"].ToString();
				}
				//End addition Rob Siera

				ListItem tmpItem;
				tmpItem= new ListItem(Esperantus.Localize.GetString("PORTALSEARCH_ALL", "All", null), string.Empty);
				ddSearchField.Items.Add(tmpItem);
				tmpItem = new ListItem(Esperantus.Localize.GetString("PORTALSEARCH_TITLE", "Title", null), "Title");
				ddSearchField.Items.Add(tmpItem);
			}
		}


		private void Search_Click(object sender, System.EventArgs e)
		{
			try 
			{
				int userID = -1;
				if (testUserID>0) 
					userID = testUserID;
				else
				{
					UsersDB u = new UsersDB();
					SqlDataReader s = u.GetSingleUser(PortalSettings.CurrentUser.Identity.Email, PortalID);
					try
					{
						if(s.Read())
							userID = Int32.Parse(s["UserID"].ToString());
					}
					finally
					{
						s.Close(); //by Manu, fixed bug 807858
					}
				}

				//Get topic
				string topicName = ddTopics.SelectedItem.Value;
				//All = no filter
				if	(topicName == Esperantus.Localize.GetString("PORTALSEARCH_ALL", "All", null))
					topicName = string.Empty;

				if(txtSearchString.Text.Length <= 2)
					throw new Exception(Esperantus.Localize.GetString("PORTALSEARCH_TONARROW", "Search string to narrow to be searched", null));
			
				SqlDataReader r = SearchHelper.SearchPortal(PortalID, userID, ddSearchModule.SelectedItem.Value, txtSearchString.Text, ddSearchField.SelectedItem.Value, sortOrder, string.Empty, topicName, string.Empty);

			    int hits;
				DataSet ds = FillPortalDS(PortalID, userID, r, out hits);

				DataView myDataView = new DataView();
				myDataView = ds.Tables[0].DefaultView;

				if (sortOrder.Equals("Title")) sortOrder = "cleanTitle";

				for(int i = 0; i < myDataView.Table.Columns.Count; i++) 
				{
					DataGrid1.Columns.Add(new BoundColumn());
					DataGrid1.Columns[i].HeaderText = myDataView.Table.Columns[i].Caption;
					((BoundColumn)DataGrid1.Columns[i]).DataField = myDataView.Table.Columns[i].ColumnName;
					if (myDataView.Table.Columns[i].ColumnName.Equals("cleanTitle"))
						DataGrid1.Columns[i].Visible = false;
					if (sortOrder.Equals (myDataView.Table.Columns[i].ColumnName))
						myDataView.Sort = sortOrder + " ASC";
				};

				DataGrid1.DataSource = myDataView;
				DataGrid1.DataBind();
				if (hits == 1)
					lblHits.Text = "1 " + Esperantus.Localize.GetString("PORTALSEARCH_HIT", "hit",null);
				else
					lblHits.Text = Convert.ToString(hits) + " " + Esperantus.Localize.GetString("PORTALSEARCH_HITS", "hit",null);
			}
			catch (Exception ex)
			{
				lblError.Text = ex.Message;
				lblError.Visible = true;
				return;
			}
		} 

		private DataSet CreatePortalDS(DataSet ds)
		{
			ds.Tables.Add("PortalSearch");
			//if (showImage)
			//	ds.Tables[0].Columns.Add("Image");
			if (showModuleName) {
				ds.Tables[0].Columns.Add("Module");
				ds.Tables[0].Columns["Module"].Caption = Esperantus.Localize.GetString("PORTALSEARCH_MODULE", "Module",null);
			};
			if (showModuleTitle) {
				ds.Tables[0].Columns.Add("Module Title");
				ds.Tables[0].Columns["Module Title"].Caption = Esperantus.Localize.GetString("PORTALSEARCH_MODULETITLE", "Module Title",null);
			};
			if (showTitle) {
				ds.Tables[0].Columns.Add("Title");
				ds.Tables[0].Columns["Title"].Caption = Esperantus.Localize.GetString("PORTALSEARCH_TITLE", "Title",null);
				ds.Tables[0].Columns.Add("cleanTitle");
			};
			if (showAbstract) {
				ds.Tables[0].Columns.Add("Abstract");
				ds.Tables[0].Columns["Abstract"].Caption = Esperantus.Localize.GetString("ABSTRACT", "Abstract",null);
			};
			if (showCreatedByUser) {
				ds.Tables[0].Columns.Add("User");
				ds.Tables[0].Columns["User"].Caption = Esperantus.Localize.GetString("PORTALSEARCH_USER", "User",null);
			};
			if (showCreatedDate) {
				ds.Tables[0].Columns.Add("Date");
				ds.Tables[0].Columns["Date"].Caption = Esperantus.Localize.GetString("DATE", "Date",null);
			};
			if (showLink) {
				ds.Tables[0].Columns.Add("Link");
				ds.Tables[0].Columns["Link"].Caption = Esperantus.Localize.GetString("PORTALSEARCH_LINK", "Link",null);
			};
			if (showTabName) {
				ds.Tables[0].Columns.Add("Tab");
				ds.Tables[0].Columns["Tab"].Caption = Esperantus.Localize.GetString("PORTALSEARCH_TAB", "Tab",null);
			};
			if (showTestInfo) {
				ds.Tables[0].Columns.Add("TestInfo");
				ds.Tables[0].Columns["TestInfo"].Caption = Esperantus.Localize.GetString("PORTALSEARCH_TESTINFO", "TestInfo",null);
			};
			return ds;
		}
 

		private DataSet FillPortalDS(int portalID, int userID, SqlDataReader portalSearchResult, out int hits)
		{
			hits = 0;
			DataSet ds = new DataSet();
			try
			{
				ds = CreatePortalDS(ds);

				string strTmp, strLink, strModuleName;
				string strModuleID, strItemID, strLocate;
				string strTabID, strTabName;
				string strModuleGuidID, strModuleTitle;
				DataRow dr;

				try
				{
					while( hits<=maxHits && portalSearchResult.Read() ) 
					{
						dr = ds.Tables["PortalSearch"].NewRow();
					
						strModuleName = portalSearchResult.GetString(0);
						strModuleID = portalSearchResult.GetInt32(3).ToString();
						strItemID = portalSearchResult.GetInt32(4).ToString();
						strLocate = "mID=" + strModuleID + "&ItemID=" + strItemID;
						strTabID = portalSearchResult.GetInt32(7).ToString();
						strTabName = portalSearchResult.GetString(8).ToString();
						strModuleGuidID = portalSearchResult.GetGuid(9).ToString().ToUpper();
						strModuleTitle = portalSearchResult.GetString(10);
						//strLink = Rainbow.Settings.Path.ApplicationRoot;

						// john.mandia@whitelightsolutions.com
						// Changed the way links were created so that it utilises BuildUrl.
						switch (strModuleGuidID)
						{
							case "2D86166C-4BDC-4A6F-A028-D17C2BB177C8":   //Discussions
								// Mark McFarlane 
								// added support for a new page that lets you view an entire thread 
								// URL requires tabID = 0
								strLink = HttpUrlBuilder.BuildUrl("~/DesktopModules/Discussion/DiscussionViewThread.aspx", 0, strLocate);
								break;
							case "2502DB18-B580-4F90-8CB4-C15E6E531012":   //Tasks
								strLink = HttpUrlBuilder.BuildUrl("~/DesktopModules/Tasks/TasksView.aspx",Convert.ToInt32(strTabID),strLocate);
								break;
							case "87303CF7-76D0-49B1-A7E7-A5C8E26415BA":  //Articles
								// Rob Siera
								// Added support to link to the article itself, instead op the page of with article module
								strLink = HttpUrlBuilder.BuildUrl("~/DesktopModules/Articles/ArticlesView.aspx", 0, strLocate);
								break;
							case "EC24FABD-FB16-4978-8C81-1ADD39792377":  //Products
								// Manu
								int tabID = PortalSettings.GetRootTab(Convert.ToInt32(strTabID), portalSettings.DesktopTabs).TabID;
								strLink = HttpUrlBuilder.BuildUrl("~/DesktopDefault.aspx", tabID, "mID=" + strModuleID + "&ItemID=" + strTabID);
								break;

							case "875254B7-2471-491F-BAF8-4AFC261CC224":  //EnhancedHtml
								// José Viladiu
								// Added support to link to the specific page
								strLink = HttpUrlBuilder.BuildUrl("~/DesktopDefault.aspx", Convert.ToInt32(strTabID), strLocate);
								break;
							default: 
								strLink = HttpUrlBuilder.BuildUrl(Convert.ToInt32(strTabID));  // "/DesktopDefault.aspx?tabID=" + ;
								break;
						}


						//if (showImage)
						//{
						//	dr["Image"] = "<a href='" + strLink + "'>" + strModuleGuidID + ".gif" + "</a>";
						//}

						if (showModuleName)
						{
							dr["Module"] = strModuleName;
						}

						if (showModuleTitle)
						{
							dr["Module Title"] = strModuleTitle;
						}

						if (showTitle)
						{
							if (strModuleGuidID == "0B113F51-FEA3-499A-98E7-7B83C192FDBB" ||  //Html Document
								strModuleGuidID == "2B113F51-FEA3-499A-98E7-7B83C192FDBB")  //Html WYSIWYG Edit (V2)
							{
								// We use the database field [rb.Modules].[ModuleTitle]:
								strTmp = strModuleTitle;
							}
							else
							{
								if (portalSearchResult.IsDBNull(1))
									strTmp = Esperantus.Localize.GetString("PORTALSEARCH_MISSING", "Missing",null);
								else
									strTmp = portalSearchResult.GetString(1);
							}
							dr["Title"] = "<a href='" + strLink + "'>" + strTmp + "</a>";
							dr["cleanTitle"] = strTmp;
						}

						if (showAbstract)
						{
							if (portalSearchResult.IsDBNull(2))
								strTmp = Esperantus.Localize.GetString("PORTALSEARCH_MISSING", "Missing",null);
							else
								strTmp = portalSearchResult.GetString(2);
						
							// Remove any html tags:
							HTMLText html = SearchHelper.DeleteBeforeBody(Server.HtmlDecode(strTmp));
							dr["Abstract"] = html.InnerText;
						}

						if (showCreatedByUser)
						{
							if (portalSearchResult.IsDBNull(5))
								strTmp = Esperantus.Localize.GetString("PORTALSEARCH_MISSING", "Missing",null);
							else
								strTmp = portalSearchResult.GetString(5);
							// 15/7/2004 added localization by Mario Endara mario@softworks.com.uy
							if (strTmp == "unknown")
							{
								strTmp = Esperantus.Localize.GetString ( "UNKNOWN", "unknown");
							}
							dr["User"] = strTmp;
						}

						if (showCreatedDate)
						{
							if (portalSearchResult.IsDBNull(6))
								strTmp = Esperantus.Localize.GetString("PORTALSEARCH_MISSING", "Missing",null);
							else
							{
								try	
								{
									strTmp = portalSearchResult.GetDateTime(6).ToShortDateString();
								}
								catch 
								{
									strTmp = string.Empty;
								}
							}
						
							// If GetDateTime(6) is an empty string the date "1/1/1900" is returned.
							if (strTmp == "1/1/1900") strTmp = string.Empty;
							dr["Date"] = strTmp;
						}

						if (showLink)
						{
							dr["Link"] = "<a href='" + strLink + "'>" + strLink + "</a>";
						}

						if (showTabName)
						{
							if (portalSearchResult.IsDBNull(8))
								strTmp = Esperantus.Localize.GetString("PORTALSEARCH_MISSING", "Missing",null);
							else
								strTmp = portalSearchResult.GetString(8);
							dr["Tab"] = "<a href='" + HttpUrlBuilder.BuildUrl(Convert.ToInt32(strTabID)) + "'>" + strTmp + "</a>";
						}

						if (showTestInfo)
						{
							dr["TestInfo"] = "ModuleGuidID=" + strModuleGuidID + "<br>" +
								"ModuleID=" + strModuleID + ", ItemID=" + strItemID + "<br>" + 
								"PortalID=" + portalID.ToString() + ", UserID=" + userID.ToString() + "<br>" +
								"TabID=" + strTabID + ", TabName=" + strTabName;
						}

						ds.Tables["PortalSearch"].Rows.Add(dr);
						hits++;
					}
				}
				finally
				{
					portalSearchResult.Close();
				}
			}
			catch(Exception e)
			{
				lblHits.Text=e.Message;
				return null;
			}
			return ds;
		}

		
		public PortalSearch() 
		{
			SettingItem setSortOrder = new SettingItem(new ListDataType("ModuleName;Title;CreatedByUser;CreatedDate;TabName"));
			setSortOrder.Required = true;
			setSortOrder.Value = "ModuleName";
			setSortOrder.Order = 1;
			this._baseSettings.Add("SortOrder", setSortOrder);

			//SettingItem showImage = new SettingItem(new BooleanDataType());
			//showImage.Order = 2;
			//showImage.Value = "True";
			//this._baseSettings.Add("ShowImage", showImage);

			SettingItem showModuleName = new SettingItem(new BooleanDataType());
			showModuleName.Order = 3;
			showModuleName.Value = "True";
			this._baseSettings.Add("ShowModuleName", showModuleName);

			SettingItem showSearchTitle = new SettingItem(new BooleanDataType());
			showSearchTitle.Order = 4;
			showSearchTitle.Value = "True";
			this._baseSettings.Add("ShowSearchTitle", showSearchTitle);

			SettingItem showAbstract = new SettingItem(new BooleanDataType());
			showAbstract.Order = 5;
			showAbstract.Value = "True";
			this._baseSettings.Add("ShowAbstract", showAbstract);

			SettingItem showCreatedByUser = new SettingItem(new BooleanDataType());
			showCreatedByUser.Order = 6;
			showCreatedByUser.Value = "True";
			this._baseSettings.Add("ShowCreatedByUser", showCreatedByUser);

			SettingItem showCreatedDate = new SettingItem(new BooleanDataType());
			showCreatedDate.Order = 7;
			showCreatedDate.Value = "True";
			this._baseSettings.Add("ShowCreatedDate", showCreatedDate);

			SettingItem showLink = new SettingItem(new BooleanDataType());
			showLink.Order = 8;
			showLink.Value = "False";
			this._baseSettings.Add("ShowLink", showLink);

			SettingItem showTabName = new SettingItem(new BooleanDataType());
			showTabName.Order = 9;
			showTabName.Value = "True";
			this._baseSettings.Add("ShowTabName", showTabName);

			SettingItem showTestInfo = new SettingItem(new BooleanDataType());
			showTestInfo.Order = 10;
			showTestInfo.Value = "False";
			this._baseSettings.Add("ShowTestInfo", showTestInfo);

			SettingItem maxHits = new SettingItem(new IntegerDataType());
			maxHits.Required = true;
			maxHits.Order = 11;
			maxHits.Value = "100";
			//maxHits.MinValue = 1;
			//maxHits.MaxValue = 1000;
			this._baseSettings.Add("MaxHits", maxHits);

			SettingItem showModuleTitle = new SettingItem(new BooleanDataType());
			showModuleTitle.Order = 12;
			showModuleTitle.Value = "False";
			this._baseSettings.Add("ShowModuleTitle", showModuleTitle);

			SettingItem testUserID = new SettingItem(new IntegerDataType());
			testUserID.Required = true;
			testUserID.Order = 13;
			testUserID.Value = "-1";
			this._baseSettings.Add("TestUserID", testUserID);
			
			SettingItem showddModule = new SettingItem(new BooleanDataType());
			showddModule.Value = "true";
			showddModule.Order = 14;
			showddModule.EnglishName = "Show Module list";
			showddModule.Description = "Show the module drop down list.";
			this._baseSettings.Add("showddModule", showddModule);
			
			SettingItem showddTopic = new SettingItem(new BooleanDataType());
			showddTopic.Value = "true";
			showddTopic.Order = 15;
			showddTopic.EnglishName = "Show Topics list";
			showddTopic.Description = "Show the topics drop down list.";
			this._baseSettings.Add("showddTopic", showddTopic);

			//Added by Rob Siera - 19 aug 2004 - Provide default Topic to search for
			SettingItem defaultTopic = new SettingItem(new StringDataType());
			defaultTopic.Value = "All";
			defaultTopic.Order = 16;
			defaultTopic.EnglishName = "Default Topic";
			defaultTopic.Description = "Set the default Topic to search.";
			this._baseSettings.Add("defaultTopic", defaultTopic);		
			//End addition Rob Siera

			SettingItem showddField = new SettingItem(new BooleanDataType());
			showddField.Value = "true";
			showddField.Order = 17;
			showddField.EnglishName = "Show Field list";
			showddField.Description = "Show the field drop down list.";
			this._baseSettings.Add("showddField", showddField);
		}

		// Jes1111
		/// <summary>
		/// Overrides ModuleSetting to render this module type un-cacheable
		/// </summary>
		public override bool Cacheable
		{
			get
			{
				return false;
			}
		}


		public override Guid GuidID 
		{
			get
			{
				return new Guid("{2502DB18-B580-4F90-8CB4-C15E6E531030}");
			}
		}

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
		///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnSearch.Click += new System.EventHandler(this.Search_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	}
}
