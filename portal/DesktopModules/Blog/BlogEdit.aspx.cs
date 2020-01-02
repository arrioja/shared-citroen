using System;
using System.IO;
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
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Rainbow.Configuration;
using Esperantus;

namespace Rainbow.DesktopModules
{
	public class BlogEdit : Rainbow.UI.AddEditItemPage
    {
		protected Esperantus.WebControls.Literal Literal1;
		protected Esperantus.WebControls.Literal Literal2;
		protected System.Web.UI.WebControls.TextBox StartField;
		protected Esperantus.WebControls.RequiredFieldValidator RequiredStartDate;
		//protected Esperantus.WebControls.CompareValidator VerifyStartDate;
		protected Esperantus.WebControls.Literal Literal3;
		protected Esperantus.WebControls.Literal Literal4;
		protected System.Web.UI.WebControls.TextBox TitleField;
		protected Esperantus.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected Esperantus.WebControls.Literal Literal5;
		protected Esperantus.WebControls.Literal Literal6;
		protected Esperantus.WebControls.Literal Literal7;
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolderHTMLEditor;
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolderButtons;
		protected Esperantus.WebControls.Literal CreatedLabel;
		protected System.Web.UI.WebControls.Label CreatedBy;
		protected Esperantus.WebControls.Literal OnLabel;
		protected System.Web.UI.WebControls.Label CreatedDate;
		protected System.Web.UI.WebControls.TextBox ExcerptField;
		protected Rainbow.UI.WebControls.IHtmlEditor DesktopText;
		/// <summary>
		/// Author:					Joe Audette
		/// Created:				1/18/2004
		/// Last Modified:			2/5/2004
		/// 
		/// The Page_Load event on this Page is used to obtain the ModuleID
		/// and ItemID of the Blog Entry to edit.
		/// It then uses the Rainbow.BlogDB() data component
		/// to populate the page's edit controls with the Blog Entry details. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e)
        {
			// Add the setting
			HtmlEditorDataType h = new HtmlEditorDataType();
			h.Value = moduleSettings["Editor"].ToString();
			DesktopText = h.GetEditor(PlaceHolderHTMLEditor, ModuleID, bool.Parse(moduleSettings["ShowUpload"].ToString()), portalSettings);
			// Construct the page
			DesktopText.Width = new System.Web.UI.WebControls.Unit(moduleSettings["Width"].ToString());
			DesktopText.Height = new System.Web.UI.WebControls.Unit(moduleSettings["Height"].ToString());
			// Construct the page
			// Added css Styles by Mario Endara <mario@softworks.com.uy> (2004/10/26)
			updateButton.CssClass = "CommandButton";
			PlaceHolderButtons.Controls.Add(updateButton);
			PlaceHolderButtons.Controls.Add(new LiteralControl("&#160;"));
			cancelButton.CssClass = "CommandButton";
			PlaceHolderButtons.Controls.Add(cancelButton);
			PlaceHolderButtons.Controls.Add(new LiteralControl("&#160;"));
			deleteButton.CssClass = "CommandButton";
			PlaceHolderButtons.Controls.Add(deleteButton);
            // If the page is being requested the first time, determine if an
            // Blog itemID value is specified, and if so populate page
            // contents with the Blog details
            if (Page.IsPostBack == false) 
            {
                if (ItemID != 0)
                {
                    BlogDB blogData = new BlogDB();
                    SqlDataReader dr = blogData.GetSingleBlog(ItemID);
					try
					{
                    // Load first row into Datareader
						if(dr.Read())
						{
							StartField.Text = ((DateTime) dr["StartDate"]).ToString();
							TitleField.Text = (string) dr["Title"].ToString();
							ExcerptField.Text = (string) dr["Excerpt"].ToString();
							DesktopText.Text = Server.HtmlDecode(dr["Description"].ToString());
							CreatedBy.Text = (string) dr["CreatedByUser"].ToString();
							CreatedDate.Text = ((DateTime) dr["CreatedDate"]).ToString();

							// 15/7/2004 added localization by Mario Endara mario@softworks.com.uy
							if (CreatedBy.Text == "unknown")
							{
								CreatedBy.Text = Esperantus.Localize.GetString ( "UNKNOWN", "unknown");
							}
						}
					}
					finally
					{
	                    dr.Close();
	                }
                }
                else
                {
                    //New article - set defaults
                    StartField.Text = DateTime.Now.ToString();
                    CreatedBy.Text = PortalSettings.CurrentUser.Identity.Email;
                    CreatedDate.Text = DateTime.Now.ToString();
                }
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
				al.Add ("55EF407B-C9D6-47e3-B627-EFA6A5EEF4B2");
				return al;
			}
		}
		/// <summary>
		/// Is used to either create or update an Blog.
		/// It uses the Rainbow.BlogsDB()
		/// data component to encapsulate all data functionality.
		/// </summary>
        override protected void OnUpdate(EventArgs e) 
        {
			base.OnUpdate(e);

			// Only Update if Input Data is Valid
            if (Page.IsValid == true) 
            {
                BlogDB blogData = new BlogDB();
                // Provide Excerpt if not present
                if (ExcerptField.Text == string.Empty)
                {
                    ExcerptField.Text = ((HTMLText) DesktopText.Text).GetAbstractText(100);
                }
                if (ItemID == 0) 
                {
                    blogData.AddBlog(ModuleID, PortalSettings.CurrentUser.Identity.Email, ((HTMLText) TitleField.Text).InnerText, ((HTMLText) ExcerptField.Text).InnerText, Server.HtmlEncode(DesktopText.Text), DateTime.Parse(StartField.Text), true);        
                }
                else
                {
                    blogData.UpdateBlog(ModuleID, ItemID, PortalSettings.CurrentUser.Identity.Email, ((HTMLText) TitleField.Text).InnerText, ((HTMLText) ExcerptField.Text).InnerText, Server.HtmlEncode(DesktopText.Text), DateTime.Parse(StartField.Text), true);        
                }
				this.RedirectBackToReferringPage();
			}
        }
		/// <summary>
		/// The DeleteBtn_Click event handler on this Page is used to delete an
		/// a Blog.  It  uses the Rainbow.BlogsDB()
		/// data component to encapsulate all data functionality.
		/// </summary>
		/// <param name="e"></param>
        override protected void OnDelete(EventArgs e) 
        {
			base.OnDelete(e);
			// Only attempt to delete the item if it is an existing item
            // (new items will have "ItemID" of 0)
            if (ItemID != 0) 
            {
                BlogDB blogData = new BlogDB();
                blogData.DeleteBlog(ItemID);
            }
			this.RedirectBackToReferringPage();
		}
		#region Web Form Designer generated code
        /// <summary>
        /// On Init
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
			//Controls must be created here
			updateButton = new LinkButton();
			cancelButton = new LinkButton();
			deleteButton = new LinkButton();
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
