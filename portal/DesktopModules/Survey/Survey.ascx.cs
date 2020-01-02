using System;
using System.Web;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data.SqlClient;

using Rainbow.UI;
using Rainbow.UI.DataTypes;
using Rainbow.UI.WebControls;
using Rainbow.Configuration;
using Esperantus;


namespace Rainbow.DesktopModules 
{
	/// <summary>
	/// Portal Survey module - Vote tool
	/// Written by: www.sysdatanet.com
	/// Moved into Rainbow by Jakob Hansen, hansen3000@hotmail.com
	/// </summary>
	public class Survey : PortalModuleControl 
	{
		protected System.Web.UI.WebControls.LinkButton SubmitButton;
		protected int test = 0;
		protected System.Web.UI.WebControls.PlaceHolder SurveyHolder;
		protected int VoteDayPeriod = 7;

		private void Page_Load(object sender, System.EventArgs e) 
		{
			test = int.Parse(Settings["Test"].ToString());
			VoteDayPeriod = int.Parse(Settings["VoteDayPeriod"].ToString());

            // Creation of the Survey - Begin
            // It uses cookies to find out whether the user has already done it
            string CookieName = "Survey" + ModuleID.ToString();
			if (test==1)
				CookieName += "Test";
			if ( Request.Cookies[CookieName] == null )
			{
                // Finds the dimension of the arrays
                SurveyDB DimArray = new SurveyDB();

                int DimArrRadio = DimArray.GetDimArray(ModuleID, "RD");
                DimArrRadio = DimArrRadio + 1;
                int DimArrCheck = DimArray.GetDimArray(ModuleID, "CH");
                DimArrCheck = DimArrCheck + 1;

                // Declaration of the ARRAYS
                Array ArrRadioButton = Array.CreateInstance(typeof(Int32), DimArrRadio);
                Array ArrCheckButton = Array.CreateInstance(typeof(Int32), DimArrCheck);
                Array arrRadioQuestionID = Array.CreateInstance(typeof(Int32), DimArrRadio);
                Array arrRadioOptionID = Array.CreateInstance(typeof(Int32), DimArrRadio);
                Array arrCheckQuestionID = Array.CreateInstance(typeof(Int32), DimArrCheck);
                Array arrCheckOptionID = Array.CreateInstance(typeof(Int32), DimArrCheck);

                // Indexes of the Arrays
                int i = 0;
                int j = 0;
                int ControlIndex = 0;

                if ( SurveyHolder.Controls.Count > 0 )
                    ControlIndex = SurveyHolder.Controls.Count;

                SurveyDB Questions = new SurveyDB();
                SqlDataReader result = Questions.GetQuestions(ModuleID);

                bool FirstTime = true;
                int GroupQuestionPrev = 0;
                bool GetSurveyID = true;

				try
				{
					while (result.Read()) // Reads the list one-by-one
					{
						if ( GetSurveyID )
						{
							ViewState["SurveyID"] = (int)result["SurveyID"];
							GetSurveyID = false;
						}

						if (! FirstTime ) 
						{
							if ( GroupQuestionPrev != (int)result["QuestionID"] )
							{
								Label lbl = new Label();
								lbl.Visible = true;
								lbl.Text = "<br />" + result["Question"].ToString();
								lbl.CssClass = "SurveyQuestion";
								SurveyHolder.Controls.Add(lbl);
								ControlIndex = ControlIndex + 1;
								SurveyHolder.Controls.Add(new LiteralControl("<br /><br />"));
								ControlIndex = ControlIndex + 1;
								GroupQuestionPrev = (int)result["QuestionID"];
							}
						} 
						else 
						{
							Label lbl = new Label();
							lbl.Visible = true;
							lbl.Text = "<br />" + result["Question"].ToString();
							lbl.CssClass = "SurveyQuestion";
							SurveyHolder.Controls.Add(lbl);
							ControlIndex = ControlIndex + 1;
							SurveyHolder.Controls.Add(new LiteralControl("<br /><br />"));

							ControlIndex = ControlIndex + 1;
							GroupQuestionPrev = (int)result["QuestionID"];
							FirstTime = false;
						}
						// Finds the Type of Option
						if ( result["TypeOption"].ToString() == "RD" )
						{
							i++;
							RadioButton RdButton = new RadioButton();
							RdButton.ID = "RdButton_" + i.ToString();
							RdButton.GroupName = "Option_" + GroupQuestionPrev.ToString();
							RdButton.Text = result["OptionDesc"].ToString();
							RdButton.CssClass = "SurveyOption";
							SurveyHolder.Controls.Add(RdButton);
							ControlIndex = ControlIndex + 1;
							// Save RadioButton position
							ArrRadioButton.SetValue(ControlIndex, i);
							arrRadioQuestionID.SetValue((int)result["QuestionID"], i);
							arrRadioOptionID.SetValue((int)result["OptionID"], i);
							// Adds a Literal
							SurveyHolder.Controls.Add(new LiteralControl("<br>"));
							ControlIndex = ControlIndex + 1;
						} 
						else
						{
							j++;;
							CheckBox ChButton = new CheckBox();
							ChButton.ID = "ChButton_" + j.ToString();
							ChButton.Text = result["OptionDesc"].ToString();
							ChButton.CssClass = "SurveyOption";
							SurveyHolder.Controls.Add(ChButton);
							ControlIndex++;
							// Saves Checkbox position
							ArrCheckButton.SetValue(ControlIndex, j);
							arrCheckQuestionID.SetValue((int)result["QuestionID"], j);
							arrCheckOptionID.SetValue((int)result["OptionID"], j);
							// Adds a literal
							SurveyHolder.Controls.Add(new LiteralControl("<br>"));
							ControlIndex++;
						}
					}
				}
				finally
				{
					result.Close(); //by Manu, fixed bug 807858
				}
                // Checks whether the Survey exist
                SurveyDB SurveyCheck = new SurveyDB();
                int RowCount = 0;
                RowCount = SurveyCheck.ExistSurvey(ModuleID);
                if ( RowCount > 0 )
				{
					SurveyHolder.Controls.Add(new LiteralControl("<br />"));
					ControlIndex = ControlIndex + 1;
					SubmitButton = new LinkButton();
					SubmitButton.ID = "Submit";
					SubmitButton.Text = Esperantus.Localize.GetString("SURVEY_SUBMIT","Submit",this);
					SubmitButton.CssClass = "SurveyButton";
					SurveyHolder.Controls.Add(SubmitButton);
					this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
                }
                ViewState["arrRadioButton"] = ArrRadioButton;
                ViewState["arrRadioQuestionID"] = arrRadioQuestionID;
                ViewState["arrRadioOptionID"] = arrRadioOptionID;
                ViewState["MaxRdIndex"] = i;
                ViewState["arrCheckButton"] = ArrCheckButton;
                ViewState["arrCheckQuestionID"] = arrCheckQuestionID;
                ViewState["arrCheckOptionID"] = arrCheckOptionID;
                ViewState["MaxChIndex"] = j;
                ViewState["URL"] = Request.Url.AbsoluteUri;
            }
			else
			{	// Creation of Chart Survey - Begin
                int SurveyID = int.Parse(Request.Cookies[CookieName].Value);
                SurveyDB Answers = new SurveyDB();
                SqlDataReader result = Answers.GetAnswers(SurveyID);
                bool FirstTime = true;
                int GroupQuestionPrev = 0;
                int tot = 0;
                float perc;

				try
				{
					while (result.Read()) // Reads the list one-by-one
					{
						if (! FirstTime ) // Shows the Question
						{
							if ( GroupQuestionPrev != (int)result["QuestionID"] ) 
							{
								Label lbl = new Label();
								lbl.Visible = true;
								lbl.Text = result["Question"].ToString();
								lbl.CssClass = "SurveyQuestion";
								SurveyHolder.Controls.Add(lbl);
								SurveyHolder.Controls.Add(new LiteralControl("<br /><br />"));
								GroupQuestionPrev = (int)result["QuestionID"];
								tot = GetTotAnswer(SurveyID, GroupQuestionPrev); // get the tot
							}
						} 
						else
						{
							Label lbl = new Label();
							lbl.Visible = true;
							lbl.Text = result["Question"].ToString();
							lbl.CssClass = "SurveyQuestion";
							SurveyHolder.Controls.Add(lbl);
							SurveyHolder.Controls.Add(new LiteralControl("<br /><br />"));

							GroupQuestionPrev = (int)result["QuestionID"];
							FirstTime = false;
							tot = GetTotAnswer(SurveyID, GroupQuestionPrev);
						}
						if ( tot == 0 )
							perc = 0;
						else
							perc = (float.Parse(result["Num"].ToString()) / (float)tot) * 100;
                    
						// Shows the AnswerOptions
						Panel Pan = new Panel();
						Pan.CssClass = "SurveyPanel";
						Pan.Visible = true;
						Pan.Width = System.Web.UI.WebControls.Unit.Percentage(perc);
						SurveyHolder.Controls.Add(Pan);

						Label lblOptDesc = new Label();
						lblOptDesc.Visible = true;
						lblOptDesc.Text = result["OptionDesc"].ToString() + "   " + perc.ToString("0") + "%  " + result["Num"].ToString() + " " + Esperantus.Localize.GetString("SURVEY_VOTES","votes",this)+"<br />";
						lblOptDesc.CssClass = "SurveyOption";
						SurveyHolder.Controls.Add(lblOptDesc);
						SurveyHolder.Controls.Add(new LiteralControl("<br />"));
					}
				}
				finally
				{
					result.Close(); //by Manu, fixed bug 807858
				}
            }
        }

		protected int GetTotAnswer(int SurveyID, int QuestionID)
		{
			return new SurveyDB().GetAnswerNum(SurveyID, QuestionID); // get the number of answers for a QuestionID
        }

		public void SubmitButton_Click(object sender, System.EventArgs e) 
		{
			Array ArrRadioButton = (Array)ViewState["arrRadioButton"];
			Array ArrCheckButton = (Array)ViewState["arrCheckButton"];
			int MaxRdIndex = (int)ViewState["MaxRdIndex"];
			Array arrRadioQuestionID = (Array)ViewState["arrRadioQuestionID"];
			Array arrRadioOptionID = (Array)ViewState["arrRadioOptionID"];
			int MaxChIndex = (int)ViewState["MaxChIndex"];
			Array arrCheckQuestionID = (Array)ViewState["arrCheckQuestionID"];
			Array arrCheckOptionID = (Array)ViewState["arrCheckOptionID"];

            for (int i = 1 ; i <= MaxRdIndex; i++)
			{
                RadioButton RadioButtonID = new RadioButton();
                RadioButtonID = (RadioButton) SurveyHolder.Controls[((int)(ArrRadioButton.GetValue(i))) - 1];
                if ( RadioButtonID.Checked )
				{
                    // get { The QuestionID and OptionID
                    int QuestionID = (int)arrRadioQuestionID.GetValue(i);
                    int OptionID = (int)arrRadioOptionID.GetValue(i);
                    int SurveyID = (int)ViewState["SurveyID"];
                    SurveyDB AddAnswer = new SurveyDB();
                    AddAnswer.AddAnswer(SurveyID, QuestionID, OptionID);
                }
            }
            for (int j = 1 ; j <= MaxChIndex; j++)
			{
                CheckBox CheckButtonID = new CheckBox();
				CheckButtonID = (CheckBox) SurveyHolder.Controls[((int)(ArrCheckButton.GetValue(j))) - 1];
                if ( CheckButtonID.Checked )
				{
                    int QuestionID = (int)arrCheckQuestionID.GetValue(j);
                    int OptionID = (int)arrCheckOptionID.GetValue(j);
                    int SurveyID = (int)ViewState["SurveyID"];
                    SurveyDB AddAnswer = new SurveyDB();

                    AddAnswer.AddAnswer(SurveyID, QuestionID, OptionID);
                }
            }
            // Store a cookie to show the chart after the submit
            string CookieName = "Survey" + ModuleID.ToString();
            Response.Cookies[CookieName].Value = ViewState["SurveyID"].ToString();
            Response.Cookies[CookieName].Expires = DateTime.Now.AddDays(VoteDayPeriod);
            Response.Redirect(ViewState["URL"].ToString());
        }

		public override Guid GuidID 
		{
			get
			{
				return new Guid("{2502DB18-B580-4F90-8CB4-C15E6E531018}");
			}
		}
        
		/// <summary>
		/// Public constructor. Sets base settings for module.
		/// </summary>
		public Survey() 
		{
			SettingItem itmVoteDayPeriod = new SettingItem(new IntegerDataType());
			itmVoteDayPeriod.Required = true;
			itmVoteDayPeriod.Order = 1;
			itmVoteDayPeriod.Value = "7";
			itmVoteDayPeriod.MinValue = 1;
			itmVoteDayPeriod.MaxValue = 365;
			this._baseSettings.Add("VoteDayPeriod", itmVoteDayPeriod);

			SettingItem itmTest = new SettingItem(new IntegerDataType());
			itmTest.Required = true;
			itmTest.Order = 2;
			itmTest.Value = "0";
			this._baseSettings.Add("Test", itmTest);
		}

		public override void Install(System.Collections.IDictionary stateSaver)
		{
			string currentScriptName = System.IO.Path.Combine(Server.MapPath(TemplateSourceDirectory), "install.sql");
			ArrayList errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);
			if (errors.Count > 0)
			{
				// Call rollback
				throw new Exception("Error occurred:" + errors[0].ToString());
			}
		}

		public override void Uninstall(System.Collections.IDictionary stateSaver)
		{
			string currentScriptName = System.IO.Path.Combine(Server.MapPath(TemplateSourceDirectory), "uninstall.sql");
			ArrayList errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);
			if (errors.Count > 0)
			{
				throw new Exception("Error occurred:" + errors[0].ToString()); // Call rollback
			}
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Raises OnInitEvent
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			this.AddUrl = Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Survey/SurveyEdit.aspx",this.TabID, "mID=" + ModuleID + "&dummy=0");
			base.OnInit(e);
		}

		private void InitializeComponent() 
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}

}