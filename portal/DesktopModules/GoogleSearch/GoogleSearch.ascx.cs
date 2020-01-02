using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Rainbow.UI;
using Rainbow.UI.DataTypes;
using Rainbow.UI.WebControls;
using Rainbow.Configuration;
using Rainbow.DesktopModules.GoogleSearchWebservice.com.google.api;


namespace Rainbow.DesktopModules
{
	/// <summary>
	/// GoogleSearch module
	/// Written by: James Melvin, james@commercechain.co.za,
	/// http://www.commercechain.co.za
	/// Moved into Rainbow by Jakob Hansen, hansen3000@hotmail.com
	/// </summary>
	[History("Jakob","2003/04/30","Added !Cacheable property")]
	public class GoogleSearch : PortalModuleControl
	{
	
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.TextBox txtSearchString;
		protected System.Web.UI.WebControls.Label lblHits;
		protected System.Web.UI.WebControls.TextBox TextBox2;

		protected int maxResults;
		protected string licKey;
		protected bool showSnippet, showSummary, showURL;
		protected Esperantus.WebControls.Label Label1;
		protected Esperantus.WebControls.Button Search;
		protected string Target;
   

		private void Page_Load(object sender, System.EventArgs e)
		{
			maxResults = int.Parse(Settings["MaxResults"].ToString());
			licKey = Settings["LicKey"].ToString();
			showSnippet = bool.Parse(Settings["ShowSnippet"].ToString());
			showSummary = bool.Parse(Settings["ShowSummary"].ToString());
			showURL = bool.Parse(Settings["ShowURL"].ToString());
			Target = "_" + Settings["Target"].ToString();

			// Jakob Hansen
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
		}
 

		private void Search_Click(object sender, System.EventArgs e)
		{

			GoogleSearchService s = new GoogleSearchService();
 
			/*
			s.doGoogleSearch(string license,string query, int start, int maxResults,
			bool filter,string restrict,bool safeSearch, string lr, string ie, string oe) 
			*/

			try 
			{
				int start = (Convert.ToInt32(TextBox2.Text)-1) * 10;

				GoogleSearchResult r = s.doGoogleSearch(licKey, txtSearchString.Text, start, maxResults, false, string.Empty, false, string.Empty, string.Empty, string.Empty);

				// Extract the estimated number of results for the search and display it
			    int estResults = r.estimatedTotalResultsCount;

				lblHits.Text = Convert.ToString(estResults) + " Results found";

				DataSet ds1 = new DataSet();
				DataSet ds =FillGoogleDS(ds1,r);
				DataGrid1.DataSource=ds;
				DataGrid1.DataBind();
			}
			catch (Exception ex)
			{
				lblHits.Text = ex.Message;
				return;
			}

			return;
		}
 

		private DataSet CreateGoogleDS(DataSet ds)
		{
			ds.Tables.Add("GoogleSearch");
			ds.Tables[0].Columns.Add("Title");
			if (showSnippet)
				ds.Tables[0].Columns.Add("Snippet");
			if (showSummary)
				ds.Tables[0].Columns.Add("Summary");
			if (showURL)
				ds.Tables[0].Columns.Add("URL");
			return ds;
		}
 

		private DataSet FillGoogleDS(DataSet ds, GoogleSearchResult srchResult)
		{
			try
			{
				ds = CreateGoogleDS(ds);
				int i =0;
				DataRow dr;
				string strURL=null;

				for (i = 0; i<srchResult.resultElements.Length; i++)
				{
					dr = ds.Tables["GoogleSearch"].NewRow();
					strURL=srchResult.resultElements[i].URL.ToString();
					dr["Title"] = "<a href=" + strURL + " Target='" + Target + "' >"+srchResult.resultElements[i].title.ToString()+"</a>";
					if (showSnippet)
						dr["Snippet"] = srchResult.resultElements[i].snippet.ToString();
					if (showSummary)
						dr["Summary"] = srchResult.resultElements[i].summary.ToString();
					if (showURL)
						dr["URL"] = "<a href=" + strURL + " Target='" + Target + "' >"+strURL+"</a>";
					ds.Tables["GoogleSearch"].Rows.Add(dr);
				}
			}
			catch(Exception e)
			{
				lblHits.Text=e.Message;
				return null;
			}
			return ds;
		}

		
		public GoogleSearch() 
		{
			SettingItem maxResults = new SettingItem(new IntegerDataType());
			maxResults.Required = true;
			maxResults.Order = 1;
			maxResults.Value = "10";
			maxResults.MinValue = 1;
			maxResults.MaxValue = 1000;
			this._baseSettings.Add("MaxResults", maxResults);

			SettingItem licKey = new SettingItem(new StringDataType());
			licKey.Required = true;
			licKey.Value = "Ffjju8cu0FDY3UMmGWxwgV5bfpsBFCJP";
			licKey.Order = 2;
			this._baseSettings.Add("LicKey", licKey);

			SettingItem showSnippet = new SettingItem(new BooleanDataType());
			showSnippet.Order = 3;
			showSnippet.Value = "true";
			this._baseSettings.Add("ShowSnippet", showSnippet);

			SettingItem showSummary = new SettingItem(new BooleanDataType());
			showSummary.Order = 4;
			showSummary.Value = "true";
			this._baseSettings.Add("ShowSummary", showSummary);

			SettingItem showURL = new SettingItem(new BooleanDataType());
			showURL.Order = 5;
			showURL.Value = "false";
			this._baseSettings.Add("ShowURL", showURL);

			SettingItem setTarget = new SettingItem(new ListDataType("blank;parent;self;top"));
			setTarget.Required = true;
			setTarget.Order = 10;
			setTarget.Value = "blank";
			this._baseSettings.Add("Target", setTarget);

		}

		
		// Jakob Hansen
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
				return new Guid("{2502DB18-B580-4F90-8CB4-C15E6E531008}");
			}
		}
		

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
//			ModuleTitle = new DesktopModuleTitle();
//			Controls.AddAt(0, ModuleTitle);
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Search.Click += new System.EventHandler(this.Search_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
