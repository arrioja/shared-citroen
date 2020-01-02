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

//Add Rainbow Namespaces
using Rainbow.Admin;
using Rainbow.Configuration;
using Esperantus;

namespace Rainbow.DesktopModules.Milestones
{
	/// <summary>
	/// IBS Portal Milestone Module - Edit page part
	/// Writen by: Elaine Ossipov  - 9/11/2002 - admin@sbsc.net
	/// Moved into Rainbow by Jakob Hansen, hansen3000@hotmail.com
	/// Updated by Manu as Rainbow Tutorial
	/// </summary>
	public class MilestonesEdit : Rainbow.UI.AddEditItemPage
	{
		protected Esperantus.WebControls.Literal Literal1;
		protected Esperantus.WebControls.Literal Literal2;
		protected System.Web.UI.WebControls.TextBox TitleField;
		protected Esperantus.WebControls.RequiredFieldValidator Req1;
		protected Esperantus.WebControls.Literal Literal3;
		protected System.Web.UI.WebControls.TextBox EstCompleteDate;
		protected Esperantus.WebControls.RequiredFieldValidator Req2;
		protected Esperantus.WebControls.CompareValidator VerifyCompleteDate;
		protected Esperantus.WebControls.Literal Literal4;
		protected System.Web.UI.WebControls.TextBox StatusBox;
		protected Esperantus.WebControls.RequiredFieldValidator Req3;
		protected System.Web.UI.WebControls.Label CreatedBy;
		protected System.Web.UI.WebControls.Label CreatedDate;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			// If the page is being requested the first time, determine if a
			// Milestone itemID value is specified, and if so, 
			// populate the page contents with the Milestone details.
			if (Page.IsPostBack == false) 
			{
				//Item id is defined in base class
				if (ItemID > 0) 
				{
					//Obtain a single row of Milestone information.
					MilestonesDB milestonesDB = new MilestonesDB();
					SqlDataReader dr = milestonesDB.GetSingleMilestones(ItemID, WorkFlowVersion.Staging);

					try
					{
						//Load the first row into the DataReader
						if(dr.Read())
						{
							TitleField.Text = (string) dr["Title"];
							EstCompleteDate.Text = ((DateTime) dr["EstCompleteDate"]).ToShortDateString();
							StatusBox.Text = (string) dr["Status"];
							CreatedBy.Text = (string) dr["CreatedByUser"];
							CreatedDate.Text = ((DateTime) dr["CreatedDate"]).ToShortDateString();
							// 15/7/2004 added localization by Mario Endara mario@softworks.com.uy
							if (CreatedBy.Text == "unknown" || CreatedBy.Text == string.Empty)
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
					//Provide defaults
					EstCompleteDate.Text = DateTime.Now.AddDays(60).ToShortDateString();
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
				al.Add ("B8784E32-688A-4b8a-87C4-DF108BF12DBE");
				return al;
			}
		}

		/// <summary>
		/// This procedure is automaticall
		/// called on Update
		/// </summary>
		protected override void OnUpdate(EventArgs e) 
		{
			// Calling base we check if the user has rights on updating
			base.OnUpdate(e);

			// Update onlyif the entered data is Valid
			if (Page.IsValid == true)
			{
				MilestonesDB milestonesDb = new MilestonesDB();
				if (ItemID <= 0)
					milestonesDb.AddMilestones(ItemID, ModuleID, PortalSettings.CurrentUser.Identity.Email, DateTime.Now, TitleField.Text, DateTime.Parse(EstCompleteDate.Text), StatusBox.Text);
				else
					milestonesDb.UpdateMilestones(ItemID, ModuleID, PortalSettings.CurrentUser.Identity.Email, DateTime.Now, TitleField.Text, DateTime.Parse(EstCompleteDate.Text), StatusBox.Text);

				// Redirects to the referring page
				// This method is provided by the base class
				this.RedirectBackToReferringPage();
			}
		}

		/// <summary>
		/// This procedure is automaticall
		/// called on Update
		/// </summary>
		override protected void OnDelete(EventArgs e) 
		{
			// Calling base we check if the user has rights on deleting
			base.OnUpdate(e);

			if (ItemID > 0) 
			{
				MilestonesDB milestonesDb = new MilestonesDB();
				milestonesDb.DeleteMilestones(ItemID);
			}

			// This method is provided by the base class
			this.RedirectBackToReferringPage();
		}
		
		#region Web Form Designer generated code
		/// <summary>
		/// Raises OnInitEvent
		/// </summary>
		/// <param name="e"></param>
		protected override void OnInit(EventArgs e)
		{
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
