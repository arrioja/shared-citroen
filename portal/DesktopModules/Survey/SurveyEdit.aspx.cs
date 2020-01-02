using System.Data.SqlClient;
using System.Collections;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using Esperantus;
using Rainbow.Configuration;


namespace Rainbow.DesktopModules 
{

	/// <summary>
	/// Portal Survey module - Edit page part
	/// Written by: www.sysdatanet.com
	/// Moved into Rainbow by Jakob Hansen, hansen3000@hotmail.com
	/// </summary>
	public class SurveyEdit : Rainbow.UI.EditItemPage
	{
        protected System.Web.UI.WebControls.RadioButton RdBtnCheck;
        protected System.Web.UI.WebControls.RadioButton RdBtnRadio;
        protected System.Web.UI.WebControls.LinkButton btnRtnSurvey;
		protected Esperantus.WebControls.Label lblTitle;
		protected Esperantus.WebControls.Label label11;
		protected System.Web.UI.WebControls.Label lblDescSurvey;
		protected Esperantus.WebControls.LinkButton addBtn;
		protected Esperantus.WebControls.Label lblNewQuestion;
		protected System.Web.UI.WebControls.TextBox txtNewQuestion;
		protected Esperantus.WebControls.RequiredFieldValidator ReqQuestion;
		protected Esperantus.WebControls.Label lblOptionType;
		protected System.Web.UI.WebControls.LinkButton AddQuestionBtn;
		protected Esperantus.WebControls.LinkButton btnCancel;
		protected Esperantus.WebControls.Label lblQuestion;
		protected System.Web.UI.WebControls.ListBox QuestionList;
		protected Esperantus.WebControls.ImageButton upBtn;
		protected Esperantus.WebControls.ImageButton downBtn;
		protected Esperantus.WebControls.ImageButton editBtn;
		protected Esperantus.WebControls.ImageButton deleteBtn;
		protected Esperantus.WebControls.LinkButton btnBackSurvey;
		protected Esperantus.WebControls.Literal CreatedLabel;
		protected System.Web.UI.WebControls.Label CreatedBy;
		protected Esperantus.WebControls.Literal OnLabel;
		protected System.Web.UI.WebControls.Label CreatedDate;

        protected ArrayList portalQuestion = new ArrayList();


		private void Page_Load(object sender, System.EventArgs e) 
		{
			if (!Page.IsPostBack) 
			{
				// Set the ImageUrl for controls from current Theme
				upBtn.ImageUrl		= this.CurrentTheme.GetImage("Buttons_Up", "Up.gif").ImageUrl;
				downBtn.ImageUrl	= this.CurrentTheme.GetImage("Buttons_Down", "Down.gif").ImageUrl;
				editBtn.ImageUrl	= this.CurrentTheme.GetImage("Buttons_Edit", "Edit.gif").ImageUrl;
				deleteBtn.ImageUrl	= this.CurrentTheme.GetImage("Buttons_Delete", "Delete.gif").ImageUrl;
			}

            //*********************************************************
            // Checks whether the survey exist, if not it creates one
            //*********************************************************
            SurveyDB SurveyCheck = new SurveyDB();
            // puts the desc of Survey in the title
            this.lblDescSurvey.Text = SurveyCheck.ExistAddSurvey(ModuleID, PortalSettings.CurrentUser.Identity.Email);

			//TBD: Create a sproc that gets these fields:
			//CreatedBy.Text = (string) dr["CreatedByUser"];
			//CreatedDate.Text = ((DateTime) dr["CreatedDate"]).ToShortDateString();


            // Fill the Question Listbox
            SurveyDB QuestionList = new SurveyDB();
            SqlDataReader QList = QuestionList.GetQuestionList(ModuleID);

			try
			{
				while (QList.Read())
				{
					QuestionItem  t = new QuestionItem();
					t.QuestionName = QList["Question"].ToString();
					t.QuestionID = (int)QList["QuestionID"];
					t.QuestionOrder = (int)QList["ViewOrder"];
					t.TypeOption = QList["TypeOption"].ToString();
					portalQuestion.Add(t);
				}
			}
			finally
			{
				QList.Close(); //by Manu, fixed bug 807858
			}

            // if ( this is the first visit to the page, bind the tab data to the page listbox
            if ( Page.IsPostBack == false )
			{
                this.QuestionList.DataTextField = "QuestionName";
                this.QuestionList.DataValueField = "QuestionID";
                this.QuestionList.DataSource = portalQuestion;
                this.QuestionList.DataBind();
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
				al.Add ("2502DB18-B580-4F90-8CB4-C15E6E531018");
				return al;
			}
		}


        //*******************************************************
        //
        // The UpDown_Click server event handler on this page is
        // used to move a portal module up or down on a question//s layout pane
        //
        //*******************************************************
		protected void UpDown_Click( object sender,  ImageClickEventArgs e)
		{

            string cmd = ((ImageButton)(sender)).CommandName;

            if ( QuestionList.SelectedIndex != -1 ) 
			{
                int delta;

                // Determine the delta to apply in the order number for the module
                // within the list.  +3 moves down one item; -3 moves up one item
                if ( cmd == "down" )
                    delta = 3;
                else
                    delta = -3;

                QuestionItem t;
                t = (QuestionItem)portalQuestion[QuestionList.SelectedIndex];
                t.QuestionOrder += delta;

                // Reset the order numbers for the questions
                OrderQuestions();

                // Redirect to the same page to pick up changes
                Response.Redirect(Request.RawUrl);
            }
        }


        //*******************************************************
        //
        // The EditBtn_Click server event handler is used to edit
        // the selected question
        //
        //*******************************************************
		protected void EditBtn_Click( object sender,  ImageClickEventArgs e)
		{

            // Redirect to edit page of currently selected tab
            if ( QuestionList.SelectedIndex != -1 ) 
			{
                // Determine the QuestionID
                QuestionItem t = (QuestionItem)portalQuestion[QuestionList.SelectedIndex];
                int tabID = int.Parse(Request.Params["tabID"]);
                Response.Redirect("SurveyOptionEdit.aspx?mID=" + ModuleID + "&QuestionID=" + t.QuestionID + "&Question=" + t.QuestionName + "&TypeOption=" + t.TypeOption + "&tabID=" + tabID);
            }

        }


        //*******************************************************
        //
        // The AddBtn_Click server event handler is used to add
        // a new question.
        //
        //*******************************************************
		protected void AddBtn_Click( object Sender,  EventArgs e)
		{
            this.lblNewQuestion.Visible = true;
            this.txtNewQuestion.Visible = true;
            this.txtNewQuestion.Text = Esperantus.Localize.GetString("SURVEY_NEWQUESTION","New question",this) ;
			this.RdBtnCheck.Text= Esperantus.Localize.GetString("SURVEY_CHECKBOXES","Checkboxes",this);
            this.RdBtnCheck.Visible = true;
			this.RdBtnRadio.Text= Esperantus.Localize.GetString("SURVEY_RADIOBUTTONS","Radio buttons",this);
            this.RdBtnRadio.Visible = true;
			this.AddQuestionBtn.Text="<img src='" + this.CurrentTheme.GetImage("Buttons_Down", "Down.gif").ImageUrl + "' border=0>" + Esperantus.Localize.GetString("SURVEY_ADDBELOW","Add to 'Questions' Below",this);
            this.AddQuestionBtn.Visible = true;
            this.ReqQuestion.Visible = true;
            this.lblOptionType.Visible = true;
            this.btnCancel.Visible = true;
        }


        //*******************************************************
        //
        // The OrderQuestions helper method is used to reset the display
        // order for the questions
        //
        //*******************************************************//
		void OrderQuestions()
		{
             int i = 1;

            // sort the arraylist
            portalQuestion.Sort();

            // renumber the order and save to database
            foreach (QuestionItem t in portalQuestion)
			{
                // number the items 1, 3, 5, etc. to provide an empty order
                // number when moving items up and down in the list.
                t.QuestionOrder = i;
                i += 2;

                // rewrite tab to database
                SurveyDB Order = new SurveyDB();
                Order.UpdateQuestionOrder(t.QuestionID, t.QuestionOrder);
            } // t
        }


        //*******************************************************
        //
        // The DeleteBtn_Click server event handler is used to delete
        // the selected question
        //
        //*******************************************************
		protected void DeleteBtn_Click( object sender,  ImageClickEventArgs e)
		{
            if ( QuestionList.SelectedIndex != -1 ) 
			{
                // must delete from database too
                QuestionItem t = (QuestionItem)portalQuestion[QuestionList.SelectedIndex];
                SurveyDB DelQuestion = new SurveyDB();
                DelQuestion.DelQuestion(t.QuestionID);

                // remove item from list
                portalQuestion.RemoveAt(QuestionList.SelectedIndex);

                // reorder list
                OrderQuestions();

                // Redirect to this site to refresh
                Response.Redirect(Request.RawUrl);
            }
        }

		
		protected void AddQuestionBtn_Click( object sender,  EventArgs e)  
		{
			// Determine the TypeOption
			string TypeOption;
			if ( this.RdBtnRadio.Checked ) 
				TypeOption = "RD";
			else 
				TypeOption = "CH";

			// new question go to the end of the list
			QuestionItem  t = new QuestionItem();
			t.QuestionName = this.txtNewQuestion.Text;
			t.QuestionID = -1;
			t.QuestionOrder = 999;
			portalQuestion.Add(t);

			// write tab to database
			SurveyDB NewQuestion = new SurveyDB();
			t.QuestionID = NewQuestion.AddQuestion(ModuleID, t.QuestionName, t.QuestionOrder, TypeOption);

			// Reset the order numbers for the tabs within the list
			OrderQuestions();

			// Redirect to edit page
			Response.Redirect(Request.RawUrl);
		}


		protected void btnCancel_Click( object sender, EventArgs e)
		{
            this.lblNewQuestion.Visible = false;
            this.txtNewQuestion.Visible = false;
            this.txtNewQuestion.Text = "new Question";
            this.RdBtnCheck.Visible = false;
            this.RdBtnRadio.Visible = false;
            this.AddQuestionBtn.Visible = false;
            this.ReqQuestion.Visible = false;
            this.lblOptionType.Visible = false;
            this.btnCancel.Visible = false;
        }


		protected void btnBackSurvey_Click( object sender,  EventArgs e)
		{
			int tabID = int.Parse(Request.Params["tabID"].ToString());
			Response.Redirect("~/DesktopDefault.aspx" + "?tabID=" + tabID);
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