using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using Esperantus;

//Namespaces added for editor and config settings
//Chris Farrell, 10/27/03, chris@cftechconsulting.com
using Rainbow.Admin;
using Rainbow.Configuration;


namespace Rainbow.DesktopModules
{

	/// <summary>
	/// IBS Portal FAQ module - Edit page part
	/// (c)2002 by Christopher S Judd, CDP &amp; Horizons, LLC
	/// Moved into Rainbow by Jakob Hansen, hansen3000@hotmail.com
	/// </summary>
	public class FAQsEdit : Rainbow.UI.EditItemPage
	{
		protected System.Web.UI.WebControls.TextBox Question;
		
		protected Esperantus.WebControls.RequiredFieldValidator RequiredFieldValidatorQuestion;
		
		protected Esperantus.WebControls.Literal CreatedLabel;
		protected Esperantus.WebControls.Literal OnLabel;
		protected System.Web.UI.WebControls.Label CreatedBy;
		protected System.Web.UI.WebControls.Label CreatedDate;
		protected Esperantus.WebControls.Literal Literal1;
		protected Esperantus.WebControls.Literal Literal2;
		protected Esperantus.WebControls.Literal Literal3;
		int itemID = -1;

        /*Editor added 10/27/03 by Chris Farrell, chris@cftechconsulting.com*/
        //protected System.Web.UI.WebControls.TextBox Answer;
        //protected Esperantus.WebControls.RequiredFieldValidator RequiredFieldValidatorAnswer;
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolderHTMLEditor;   
		protected Rainbow.UI.WebControls.IHtmlEditor DesktopText;


		private void Page_Load(object sender, System.EventArgs e)
		{
		    //Editor placeholder setup
			Rainbow.UI.DataTypes.HtmlEditorDataType h = new Rainbow.UI.DataTypes.HtmlEditorDataType();
			h.Value = moduleSettings["Editor"].ToString();
			DesktopText = h.GetEditor(PlaceHolderHTMLEditor, ModuleID, bool.Parse(moduleSettings["ShowUpload"].ToString()), portalSettings);

			DesktopText.Width = new System.Web.UI.WebControls.Unit(moduleSettings["Width"].ToString());
			DesktopText.Height = new System.Web.UI.WebControls.Unit(moduleSettings["Height"].ToString());


			//  Determine itemID of FAQ to Update
			if (Request.Params["itemID"] != null ) 
			{
				itemID = Int32.Parse(Request.Params["itemID"]);
			}

			//	populate with FAQ Details  
			if (Page.IsPostBack == false ) 
			{

				if (itemID != -1 ) 
				{
					//  get a single row of FAQ info
					FAQsDB questions = new FAQsDB();
					SqlDataReader dr = questions.GetSingleFAQ(itemID);

					try
					{
						//  Read database
						dr.Read();
						Question.Text = (string) dr["Question"];
						//Answer.Text = (string) dr["Answer"];
						DesktopText.Text = (string) dr["Answer"];
						CreatedBy.Text = (string) dr["CreatedByUser"];
						CreatedDate.Text = ((DateTime) dr["CreatedDate"]).ToShortDateString();
						// 15/7/2004 added localization by Mario Endara mario@softworks.com.uy
						if (CreatedBy.Text == "unknown" || CreatedBy.Text == string.Empty)
						{
							CreatedBy.Text = Esperantus.Localize.GetString ( "UNKNOWN", "unknown");
						}
					}
					finally
					{
						dr.Close();
					}
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
				al.Add ("2502DB18-B580-4F90-8CB4-C15E6E531000");
				return al;
			}
		}

		override protected void OnUpdate(EventArgs e)
		{
			base.OnUpdate(e);

			// Don't Allow empty data
			if (Question.Text == string.Empty || DesktopText.Text == string.Empty) 
				return;

			//  Update only if entered data is valid
			if (Page.IsValid == true) 
			{
				FAQsDB questions = new FAQsDB();

				if (itemID == -1) 
				{
					//  Add the question within the questions table
					questions.AddFAQ(ModuleID, itemID, PortalSettings.CurrentUser.Identity.Email, Question.Text, DesktopText.Text);
				} 
				else 
				{
					//  Update the question within the questions table
					questions.UpdateFAQ(ModuleID, itemID, PortalSettings.CurrentUser.Identity.Email, Question.Text, DesktopText.Text);
				}

				this.RedirectBackToReferringPage();
			}
		}


		override protected void OnDelete(EventArgs e) 
		{
			base.OnDelete(e);

			//  Only attempt to delete the item if it is an existing item
			//  (new items will have "itemID" of -1)
			if (itemID != -1 ) 
			{
				FAQsDB questions = new FAQsDB();
				questions.DeleteFAQ(itemID);
			}
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
