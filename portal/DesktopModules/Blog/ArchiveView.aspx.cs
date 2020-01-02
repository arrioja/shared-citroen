using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow.Design;
using Rainbow.Helpers;
using Rainbow.Configuration;
using Esperantus;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// Author:					Joe Audette
	/// Created:				1/18/2004
	/// Last Modified:			2/18/2004 (Jakob Hansen did localizing)
	/// </summary>
	public class ArchiveView : Rainbow.UI.ViewItemPage
	{
		protected DataList myDataList;
		protected HtmlAnchor lnkRSS;
		protected HtmlImage imgRSS;
		protected Label lblEntryCount;
		protected Label lblCommentCount;
		protected DataList dlArchive;
		protected Label lblHeader;
		protected Esperantus.WebControls.Literal BlogPageLabel;
		protected Esperantus.WebControls.Literal SyndicationLabel;
		protected Esperantus.WebControls.Literal StatisticsLabel;
		protected Esperantus.WebControls.Literal ArchivesLabel;
		protected Label lblCopyright;
		protected string Feedback;

		private void Page_Load(object sender, System.EventArgs e)
		{
		
			// Added EsperantusKeys for Localization 
			// Mario Endara mario@softworks.com.uy june-1-2004 
			Feedback = Esperantus.Localize.GetString ("BLOG_FEEDBACK");

			if(!IsPostBack)
			{
				lnkRSS.HRef = Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Blog/RSS.aspx",TabID,"&mID=" + ModuleID );
				imgRSS.Src = Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Blog/xml.gif");
				lblCopyright.Text = moduleSettings["Copyright"].ToString();
		
				BlogDB blogDB = new BlogDB();
				int month = -1;
				int year = -1;
				try
				{
					month = int.Parse(Request.Params.Get("month"));
					year = int.Parse(Request.Params.Get("year"));
				}
				catch{}

				if((month > -1)&&(year > -1))
				{
					this.lblHeader.Text = Esperantus.Localize.GetString("BLOG_POSTSFROM", "Posts From", null) +
						" " + DateTime.Parse(month.ToString() + "/1/" + year.ToString()).ToString("MMMM, yyyy");
					myDataList.DataSource = blogDB.GetBlogEntriesByMonth(month, year, ModuleID);
				}
				else
				{
					myDataList.DataSource = blogDB.GetBlogs(ModuleID);
				}
				myDataList.DataBind();

				dlArchive.DataSource = blogDB.GetBlogMonthArchive(ModuleID);
				dlArchive.DataBind();
	
				SqlDataReader dataReader = blogDB.GetBlogStats(ModuleID);
				try
				{
					if (dataReader.Read())
					{
						lblEntryCount.Text = Esperantus.Localize.GetString("BLOG_ENTRIES", "Entries", null) + 
							" (" + (string) dataReader["EntryCount"].ToString() + ")";
						lblCommentCount.Text = Esperantus.Localize.GetString("BLOG_COMMENTS", "Comments", null) +
							" (" + (string) dataReader["CommentCount"].ToString() + ")";
					
					}
				}
				finally
				{
					dataReader.Close();
				}
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
