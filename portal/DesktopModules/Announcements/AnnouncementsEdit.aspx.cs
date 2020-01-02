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
using Rainbow.Admin;
using Esperantus;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules 
{
	[History("Jes1111", "2003/03/04", "Cache flushing now handled by inherited page")]
	public class AnnouncementsEdit : Rainbow.UI.AddEditItemPage
	{
		protected Esperantus.WebControls.Literal Literal1;
		protected Esperantus.WebControls.Literal Literal2;
		protected System.Web.UI.WebControls.TextBox TitleField;
		protected Esperantus.WebControls.RequiredFieldValidator RequiredTitle;
		protected Esperantus.WebControls.Literal Literal3;
		protected System.Web.UI.WebControls.TextBox MoreLinkField;
		protected Esperantus.WebControls.Literal Literal4;
		protected System.Web.UI.WebControls.TextBox MobileMoreField;
		protected Esperantus.WebControls.Literal Literal5;
		protected Esperantus.WebControls.Literal Literal6;
		protected System.Web.UI.WebControls.TextBox ExpireField;
		protected Esperantus.WebControls.RequiredFieldValidator RequiredExpireDate;
		protected Esperantus.WebControls.CompareValidator VerifyExpireDate;
		protected Esperantus.WebControls.Literal CreatedLabel;
		protected System.Web.UI.WebControls.Label CreatedBy;
		protected Esperantus.WebControls.Literal OnLabel;
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolderHTMLEditor;
		protected System.Web.UI.WebControls.Label CreatedDate;
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolderButtons;
   
		protected Rainbow.UI.WebControls.IHtmlEditor DesktopText;
		/// <summary>
		/// The Page_Load event on this Page is used to obtain the ModuleID
		/// and ItemID of the announcement to edit.
		/// It then uses the Rainbow.AnnouncementsDB() data component
		/// to populate the page's edit controls with the annoucement details.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e) 
		{
			// If the page is being requested the first time, determine if an
			// announcement itemID value is specified, and if so populate page
			// contents with the announcement details
			
			//Indah Fuldner
			Rainbow.UI.DataTypes.HtmlEditorDataType h = new Rainbow.UI.DataTypes.HtmlEditorDataType();
			h.Value = moduleSettings["Editor"].ToString();
			DesktopText = h.GetEditor(PlaceHolderHTMLEditor, ModuleID, bool.Parse(moduleSettings["ShowUpload"].ToString()), portalSettings);

			DesktopText.Width = new System.Web.UI.WebControls.Unit(moduleSettings["Width"].ToString());
			DesktopText.Height = new System.Web.UI.WebControls.Unit(moduleSettings["Height"].ToString());
			//End Indah Fuldner

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

			if (Page.IsPostBack == false) 
			{
				if (ItemID != 0) 
				{
					// Obtain a single row of announcement information
					AnnouncementsDB announcementDB = new AnnouncementsDB();
					SqlDataReader dr = announcementDB.GetSingleAnnouncement(ItemID, WorkFlowVersion.Staging);
                
					try
					{
						// Load first row into DataReader
						if(dr.Read())
						{
							TitleField.Text = (string) dr["Title"];
							MoreLinkField.Text = (string) dr["MoreLink"];
							MobileMoreField.Text = (string) dr["MobileMoreLink"];
							DesktopText.Text = (string) dr["Description"];
							ExpireField.Text = ((DateTime) dr["ExpireDate"]).ToShortDateString();
							CreatedBy.Text = (string) dr["CreatedByUser"];
							CreatedDate.Text = ((DateTime) dr["CreatedDate"]).ToShortDateString();
							// 15/7/2004 added localization by Mario Endara mario@softworks.com.uy
							if (CreatedBy.Text == "unknown")
							{
								CreatedBy.Text = Esperantus.Localize.GetString ( "UNKNOWN", "unknown");
							}
						}
					}
					finally
					{
						// Close the datareader
						dr.Close();
					}
				}
				else
				{
					ExpireField.Text = DateTime.Now.AddDays(Int32.Parse(moduleSettings["DelayExpire"].ToString())).ToShortDateString();
					deleteButton.Visible = false; // Cannot delete an unexsistent item
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
				al.Add ("CE55A821-2449-4903-BA1A-EC16DB93F8DB");
				return al;
			}
		}

		/// <summary>
		/// The UpdateBtn_Click event handler on this Page is used to either
		/// create or update an announcement.  It  uses the Rainbow.AnnouncementsDB()
		/// data component to encapsulate all data functionality.
		/// </summary>
		override protected void OnUpdate(EventArgs e) 
		{
			base.OnUpdate(e);
			
			// Only Update if the Entered Data is Valid
			if (Page.IsValid == true) 
			{
				// Create an instance of the Announcement DB component
				AnnouncementsDB announcementDB = new AnnouncementsDB();

				if (ItemID == 0) 
				{
					// Add the announcement within the Announcements table
					announcementDB.AddAnnouncement(ModuleID, ItemID, PortalSettings.CurrentUser.Identity.Email, TitleField.Text, DateTime.Parse(ExpireField.Text),DesktopText.Text, MoreLinkField.Text, MobileMoreField.Text);
				}
				else 
				{
					// Update the announcement within the Announcements table
					announcementDB.UpdateAnnouncement(ModuleID, ItemID, PortalSettings.CurrentUser.Identity.Email, TitleField.Text, DateTime.Parse(ExpireField.Text),DesktopText.Text, MoreLinkField.Text, MobileMoreField.Text);
				}

				// Redirect back to the portal home page
				this.RedirectBackToReferringPage();
			}
		}

		/// <summary>
		/// The DeleteBtn_Click event handler on this Page is used to delete an
		/// an announcement.  It  uses the Rainbow.AnnouncementsDB()
		/// data component to encapsulate all data functionality.
		/// </summary>
		override protected void OnDelete(EventArgs e) 
		{
			base.OnDelete(e);
			// Only attempt to delete the item if it is an existing item
			// (new items will have "ItemID" of 0)
			if (ItemID != 0) 
			{
				AnnouncementsDB announcementDB = new AnnouncementsDB();
				announcementDB.DeleteAnnouncement(ItemID);
			}

			// Redirect back to the portal home page
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
