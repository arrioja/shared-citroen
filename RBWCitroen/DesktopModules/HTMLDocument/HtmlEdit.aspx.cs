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
using Rainbow.Configuration;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Esperantus;
namespace Rainbow.DesktopModules 
{
	/// <summary>
	/// 
	/// </summary>
	[History("Jes1111", "2003/03/04", "Cache flushing now handled by inherited page")]
	public class HtmlEdit : Rainbow.UI.EditItemPage
	{
        protected System.Web.UI.WebControls.TextBox MobileSummary;
        protected System.Web.UI.WebControls.TextBox MobileDetails;
		protected System.Web.UI.HtmlControls.HtmlTableRow MobileRow;
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolderHTMLEditor;
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolderButtons;
		protected Esperantus.WebControls.Literal Literal1;
		protected Esperantus.WebControls.Literal Literal2;
		protected Esperantus.WebControls.Literal Literal3;
		protected Esperantus.WebControls.Literal Literal4;
		protected Rainbow.UI.WebControls.IHtmlEditor DesktopText;
		/// <summary>
		/// Set the module guids with free access to this page
		/// </summary>
		protected override ArrayList AllowedModules
		{
			get
			{
				ArrayList al = new ArrayList();
				al.Add ("0B113F51-FEA3-499A-98E7-7B83C192FDBB");
				return al;
			}
		}
        /// <summary>
        /// The Page_Load event on this Page is used to obtain the ModuleID
        /// of the xml module to edit.
        /// It then uses the Rainbow.HtmlTextDB() data component
        /// to populate the page's edit controls with the text details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e)
        {
			// Add the setting
			HtmlEditorDataType h = new HtmlEditorDataType();
			h.Value = moduleSettings["Editor"].ToString();
			DesktopText = h.GetEditor(PlaceHolderHTMLEditor, ModuleID, bool.Parse(moduleSettings["ShowUpload"].ToString()), portalSettings);
			DesktopText.Width = new System.Web.UI.WebControls.Unit(moduleSettings["Width"].ToString());
			DesktopText.Height = new System.Web.UI.WebControls.Unit(moduleSettings["Height"].ToString());
			if(bool.Parse(moduleSettings["ShowMobile"].ToString()))
			{
				MobileRow.Visible = true;
				MobileSummary.Width = new System.Web.UI.WebControls.Unit(moduleSettings["Width"].ToString());
				MobileDetails.Width = new System.Web.UI.WebControls.Unit(moduleSettings["Width"].ToString());
			}
			else
			{
				MobileRow.Visible = false;
			}
			// Construct the page
			// Added css Styles by Mario Endara <mario@softworks.com.uy> (2004/10/26)
			updateButton.CssClass = "CommandButton";
			PlaceHolderButtons.Controls.Add(updateButton);
			PlaceHolderButtons.Controls.Add(new LiteralControl("&#160;"));
			cancelButton.CssClass = "CommandButton";
			PlaceHolderButtons.Controls.Add(cancelButton);
            if (Page.IsPostBack == false)
            {
                // Obtain a single row of text information
                HtmlTextDB text = new HtmlTextDB();
				// Change by Geert.Audenaert@Syntegra.Com - Date: 7/2/2003
				// Original: SqlDataReader dr = text.GetHtmlText(ModuleID);
				SqlDataReader dr = text.GetHtmlText(ModuleID, WorkFlowVersion.Staging);
				// End Change Geert.Audenaert@Syntegra.Com
				try
				{
                if (dr.Read())
                {
                    DesktopText.Text = Server.HtmlDecode((string) dr["DesktopHtml"]);
                    MobileSummary.Text = Server.HtmlDecode((string) dr["MobileSummary"]);
                    MobileDetails.Text = Server.HtmlDecode((string) dr["MobileDetails"]);
                }
                else
                {
                    DesktopText.Text = Esperantus.Localize.GetString("HTMLDOCUMENT_TODO_ADDCONTENT", "Todo: Add Content...",null);
                    MobileSummary.Text = Esperantus.Localize.GetString("HTMLDOCUMENT_TODO_ADDCONTENT", "Todo: Add Content...",null);
                    MobileDetails.Text = Esperantus.Localize.GetString("HTMLDOCUMENT_TODO_ADDCONTENT", "Todo: Add Content...", null);
                }
				}
				finally
				{
                dr.Close();
            }
        }
        }
        /// <summary>
        /// The UpdateBtn_Click event handler on this Page is used to save
        /// the text changes to the database.
        /// </summary>
        override protected void OnUpdate(EventArgs e) 
        {
			base.OnUpdate(e);
			// Create an instance of the HtmlTextDB component
            HtmlTextDB text = new HtmlTextDB();
            // Update the text within the HtmlText table
            text.UpdateHtmlText(ModuleID, Server.HtmlEncode(DesktopText.Text), Server.HtmlEncode(MobileSummary.Text), Server.HtmlEncode(MobileDetails.Text));
			this.RedirectBackToReferringPage();
        }
		#region Web Form Designer generated code
        /// <summary>
        /// Raises OnInitEvent
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
			//Controls must be created here
			updateButton = new LinkButton();
			cancelButton = new LinkButton();
			// Call requested by ASP.NET
            InitializeComponent();
			// Call base first
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
