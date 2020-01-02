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
using Rainbow.Design;
using Rainbow.Helpers;
using Rainbow.Configuration;
using Esperantus;

namespace Rainbow.DesktopModules
{
    public class ArticlesView : Rainbow.UI.ViewItemPage
	{
		protected System.Web.UI.WebControls.Label Title;
		protected System.Web.UI.WebControls.Label StartDate;
		protected System.Web.UI.WebControls.Label CreatedDate;
		protected Esperantus.WebControls.Literal OnLabel;
		protected System.Web.UI.WebControls.Label CreatedBy;
		protected Esperantus.WebControls.Literal CreatedLabel;
		protected System.Web.UI.WebControls.Label Description;
		protected System.Web.UI.WebControls.Label Subtitle;

		/// <summary>
		/// The Page_Load server event handler on this page is used
		/// to obtain the Articles list, and to then display
		/// the message contents.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e)
		{
            // Populate message contents if this is the first visit to the page
            if (!Page.IsPostBack && ModuleID > 0 && ItemID > 0 )
			{
				StartDate.Visible = bool.Parse(moduleSettings["ShowDate"].ToString());
                BindData();
            }
        }

		/// <summary>
		/// Set the module guids with free access to this page
		/// </summary>
		protected override ArrayList AllowedModules
		{
			get
			{
				ArrayList al = new ArrayList();
				al.Add ("87303CF7-76D0-49B1-A7E7-A5C8E26415BA");
				al.Add ("2502DB18-B580-4F90-8CB4-C15E6E531030"); // Access from portalSearch
				al.Add ("2502DB18-B580-4F90-8CB4-C15E6E531052"); // Access from serviceItemList				
				return al;
			}
		}

		/// <summary>
		/// The BindData method is used to obtain details of a message
		/// from the Articles table, and update the page with
		/// the message content.
		/// </summary>
        void BindData()
		{
            // Obtain the selected item from the Articles table
            ArticlesDB Article = new ArticlesDB();
            SqlDataReader dr = Article.GetSingleArticle(ItemID);

			try
			{
				// Load first row from database
				if (dr.Read())
				{
					// Update labels with message contents
					Title.Text = (string) dr["Title"].ToString();

					//Chris@cftechconsulting.com  5/24/04 added subtitle to ArticlesView.
					if(dr["Subtitle"].ToString().Length > 0)
					{
						Subtitle.Text = "(" + (string) dr["Subtitle"].ToString() + ")";
					}
					StartDate.Text = ((DateTime) dr["StartDate"]).ToShortDateString();
					Description.Text = Server.HtmlDecode((string) dr["Description"].ToString());
					CreatedDate.Text = ((DateTime) dr["CreatedDate"]).ToShortDateString();
					CreatedBy.Text = (string) dr["CreatedByUser"].ToString();
					// 15/7/2004 added localization by Mario Endara mario@softworks.com.uy
					if (CreatedBy.Text == "unknown")
					{
						CreatedBy.Text = Esperantus.Localize.GetString ( "UNKNOWN", "unknown");
					}

					//Chris Farrell, chris@cftechconsulting.com, 5/24/2004
					if(!bool.Parse(moduleSettings["MODULESETTINGS_SHOW_MODIFIED_BY"].ToString()))
					{
						CreatedLabel.Visible = false;
						CreatedDate.Visible = false;
						OnLabel.Visible = false;
						CreatedBy.Visible = false;
					}
				}
			}
			finally
			{
				// close the datareader
				dr.Close();
			}
        }

		#region Web Form Designer generated code
		/// <summary>
		/// Raises OnInitEvent
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
			InitializeComponent();

			// - jminond
			// View item pages in general have no need for viewstate
			// Especailly big texts.
			this.Page.EnableViewState = false;

			base.OnInit(e);
		}

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
    }
}
