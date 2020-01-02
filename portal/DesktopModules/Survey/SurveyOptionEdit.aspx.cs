using System.Data.SqlClient;
using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

using Esperantus;


namespace Rainbow.DesktopModules 
{

    public class SurveyOptionEdit : Rainbow.UI.EditItemPage
	{
		protected Esperantus.WebControls.Label lblTitle;
		protected Esperantus.WebControls.LinkButton AddOptBtn;
        protected System.Web.UI.WebControls.TextBox TxtNewOption;
		protected Esperantus.WebControls.RequiredFieldValidator ReqNewOpt;
        protected System.Web.UI.WebControls.LinkButton AddOptionBtn;
		protected Esperantus.WebControls.Label lblOption;
        protected System.Web.UI.WebControls.ListBox OptionList;
		protected Esperantus.WebControls.Label label11;
        protected System.Web.UI.WebControls.Label lblQuestion;
		protected Esperantus.WebControls.ImageButton upBtn;
		protected Esperantus.WebControls.ImageButton downBtn;
		protected Esperantus.WebControls.ImageButton deleteBtn;
		protected Esperantus.WebControls.LinkButton CancelOptBtn;
		protected Esperantus.WebControls.LinkButton btnReturnCancel;
		protected Esperantus.WebControls.Label Label1;
		protected Esperantus.WebControls.Label lblTypeOption;
		protected Esperantus.WebControls.Label lblNewOption;
		protected Esperantus.WebControls.Literal CreatedLabel;
		protected System.Web.UI.WebControls.Label CreatedBy;
		protected Esperantus.WebControls.Literal OnLabel;
		protected System.Web.UI.WebControls.Label CreatedDate;
        protected ArrayList portalOption = new ArrayList();



		private void Page_Load(object sender, System.EventArgs e) 
		{
			if (!Page.IsPostBack) 
			{
				// Set the ImageUrl for controls from current Theme
				upBtn.ImageUrl		= this.CurrentTheme.GetImage("Buttons_Up", "Up.gif").ImageUrl;
				downBtn.ImageUrl	= this.CurrentTheme.GetImage("Buttons_Down", "Down.gif").ImageUrl;
				deleteBtn.ImageUrl	= this.CurrentTheme.GetImage("Buttons_Delete", "Delete.gif").ImageUrl;
			}
			//TBD: Create a sproc that gets these fields:
			//CreatedBy.Text = (string) dr["CreatedByUser"];
			//CreatedDate.Text = ((DateTime) dr["CreatedDate"]).ToShortDateString();
			
			// get { QuestionID from querystring
            if (! (Request.Params["QuestionID"] == null) )
			{
                int QuestionID = 0;
                QuestionID = int.Parse(Request.Params["QuestionID"]);

                SurveyDB OptList = new SurveyDB();
                SqlDataReader OList = OptList.GetOptionList(QuestionID);

				try
				{
					while ( OList.Read() )
					{
						OptionItem o = new OptionItem();
						o.OptionName = OList["OptionDesc"].ToString();
						o.OptionID = (int)OList["OptionID"];
						o.OptionOrder = (int)OList["ViewOrder"];
						portalOption.Add(o);
					}
				}
				finally
				{
					OList.Close(); //by Manu, fixed bug 807858
				}

                if (! Page.IsPostBack ) 
				{
                    this.OptionList.DataTextField = "OptionName";
                    this.OptionList.DataValueField = "OptionID";
                    this.OptionList.DataSource = portalOption;
                    this.OptionList.DataBind();
                    this.lblQuestion.Text = Request.Params["Question"];
					if (Request.Params["TypeOption"]== "RD" ) 
						this.lblTypeOption.Text = Esperantus.Localize.GetString("SURVEY_RADIOBUTTONS","Radio buttons",this);
					else
						this.lblTypeOption.Text = Esperantus.Localize.GetString("SURVEY_CHECKBOXES","Checkboxes",this);
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
				al.Add ("2502DB18-B580-4F90-8CB4-C15E6E531018");
				return al;
			}
		}

        //*******************************************************
        //
        // The UpDown_Click server event handler on this page is
        // used to move an option up o down
        //
        //*******************************************************
		protected void UpDown_Click(object sender, ImageClickEventArgs e)
		{
			string cmd = ((ImageButton)sender).CommandName;

            if ( OptionList.SelectedIndex != -1 )
			{
                int delta;

                // Determine the delta to apply in the order number for the module
                // within the list.  +3 moves down one item; -3 moves up one item
                if ( cmd == "down" )
                    delta = 3;
                else
                    delta = -3;

                OptionItem o;
                o = (OptionItem)portalOption[OptionList.SelectedIndex];
                o.OptionOrder += delta;

                // Reset the order numbers for the questions
                OrderOptions();

                // Redirect to the same page to pick up changes
                Response.Redirect(Request.RawUrl);
            }

        }


        //*******************************************************
        //
        // The OrderOptions helper method is used to reset the display
        // order for the options
        //
        //*******************************************************//
		void OrderOptions()
		{
            int i = 1;

            // sort the arraylist
            portalOption.Sort();

            // renumber the order and save to database
            foreach (OptionItem o in portalOption)
			{

                // number the items 1, 3, 5, etc. to provide an empty order
                // number when moving items up and down in the list.
                o.OptionOrder = i;
                i += 2;

                // rewrite tab to database
                SurveyDB Order = new SurveyDB();
                Order.UpdateOptionOrder(o.OptionID, o.OptionOrder);
            }
        }


        //*******************************************************
        //
        // The AddOptBtn_Click server event handler is used to add
        // a new option.
        //
        //*******************************************************
		protected void AddOptBtn_Click(object sender, EventArgs e)
	    {
			this.lblNewOption.Visible= true;
			this.TxtNewOption.Visible = true;
            this.TxtNewOption.Text = Esperantus.Localize.GetString("SURVEY_NEWOPTION","New option",this) ;
            this.ReqNewOpt.Visible = true;
			this.AddOptionBtn.Text="<img src='" + this.CurrentTheme.GetImage("Buttons_Down", "Down.gif").ImageUrl + "' border=0>" + Esperantus.Localize.GetString("SURVEY_ADDOPTBELOW","Add to 'Options' Below",this);
			this.AddOptionBtn.Visible = true;
            this.CancelOptBtn.Visible = true;
        }


		//private void AddOptionBtn_Click( System.object sender,  System.EventArgs e)  AddOptionBtn.Click {
		protected void AddOptionBtn_Click(object sender, EventArgs e) 
		{
			// Determine QuestioID 
            int QuestionID = 0;

            // get { QuestionID from querystring
            if (! (Request.Params["QuestionID"] == null) ) 
                QuestionID = int.Parse(Request.Params["QuestionID"]);


            // new option go to the end of the list
            OptionItem o = new OptionItem();
            o.OptionName = this.TxtNewOption.Text;
            o.OptionID = -1;
            o.OptionOrder = 999;
            portalOption.Add(o);

            // write tab to database
            SurveyDB NewOption = new SurveyDB();
            o.OptionID = NewOption.AddOption(QuestionID, o.OptionName, o.OptionOrder);

            // Reset the order numbers for the tabs within the list
            OrderOptions();

            // Redirect to edit page
            Response.Redirect(Request.RawUrl);
        }


		protected void CancelOptBtn_Click(object sender, EventArgs e)
		{
			this.TxtNewOption.Visible = false;
            this.ReqNewOpt.Visible = false;
            this.AddOptionBtn.Visible = false;
            this.CancelOptBtn.Visible = false;
        }


		protected void deleteBtn_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
            OptionItem o;
            o = (OptionItem)portalOption[OptionList.SelectedIndex];

            SurveyDB DelOpt = new SurveyDB();
            DelOpt.DelOption(o.OptionID);

            // remove item from list
            portalOption.RemoveAt(OptionList.SelectedIndex);

            // reorder list
            OrderOptions();

            // Redirect to this site to refresh
            Response.Redirect(Request.RawUrl, false);
        }

		
		protected void btnReturnCancel_Click(object sender, EventArgs e)
		{
			int tabID = int.Parse(Request.Params["tabID"].ToString());
			Response.Redirect("SurveyEdit.aspx?mID=" + ModuleID + "&tabID=" + tabID);
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