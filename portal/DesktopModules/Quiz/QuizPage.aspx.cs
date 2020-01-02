using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Configuration;

using Rainbow.UI.DataTypes;
using Rainbow.Design;
using Rainbow.Helpers;
using Rainbow.Configuration;
using Esperantus;


namespace Rainbow.DesktopModules
{
	/// <summary>
	/// Summary description for QuizPage.
	/// </summary>
	public class QuizPage : Rainbow.UI.ViewItemPage
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl QuizScreen;
		protected System.Web.UI.HtmlControls.HtmlGenericControl ResultScreen;

		protected System.Web.UI.WebControls.Label lblQuiz;
		protected System.Web.UI.WebControls.RadioButtonList rblAnswer;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator1;
		protected System.Web.UI.WebControls.Button btnSubmit;

		protected System.Web.UI.WebControls.Label lblTotalQuestion;
		protected System.Web.UI.WebControls.Label lblTimeSpent;
		protected System.Web.UI.WebControls.Label lblQuestion;
		protected System.Web.UI.WebControls.Label lblResult;

		int intTotalQuestion;
		int intQuestionNo = 1;
		int intScore = 0;
		ArrayList arrAnswerHistory = new ArrayList();
		XmlDocument xDoc = new XmlDocument();

		string XmlFile;
		PortalUrlDataType PieUrl;
		
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			lblQuiz.Text = moduleSettings["QuizName"].ToString();

			PieUrl = new PortalUrlDataType();
			PieUrl.Value = "/Quiz/Pie.gif";

			PortalUrlDataType pt;
			pt = new PortalUrlDataType();
			pt.Value = moduleSettings["XMLsrc"].ToString();
			string xmlsrc = pt.FullPath;
			
			bool xmlsrcOk = false;
			if ((xmlsrc != null) && (xmlsrc != string.Empty)) 
			{
				XmlFile = Server.MapPath(xmlsrc);
				if (System.IO.File.Exists(XmlFile)) 
				{
					//Load xml data
					xDoc.Load(XmlFile);
					xmlsrcOk = true;
				}
				else 
				{
					QuizScreen.Visible = false;
					ResultScreen.Visible = false;
					Controls.Add(new LiteralControl("<br>" + "<span class='Error'>" + Esperantus.Localize.GetString("FILE_NOT_FOUND").Replace("%1%", xmlsrc) + "<br>"));
				}
			}

			//Start a new quiz?
			if (!Page.IsPostBack ) 
			{
				if (xmlsrcOk)
				{
					//Yes! Count total question
					intTotalQuestion = xDoc.SelectNodes("/quiz/mchoice").Count;

					//Record start time
					ViewState["StartTime"] = DateTime.Now;

					ShowQuestion(intQuestionNo);
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
				al.Add ("2502DB18-B580-4F90-8CB4-C15E6E531050");
				return al;
			}
		}

		protected void btnSubmit_Click(object src,  EventArgs e)
		{
            //Retrieve essential variables from state bag
            intTotalQuestion = (int) ViewState["TotalQuestion"];
            intQuestionNo = (int) ViewState["QuestionNo"];
            intScore = (int) ViewState["Score"];
            arrAnswerHistory = (ArrayList) ViewState["AnswerHistory"];

            //Correct answer?
            if ( rblAnswer.SelectedItem.Value == ViewState["CorrectAnswer"].ToString() )
			{
                intScore += 1;
                arrAnswerHistory.Add(0);
            } 
			else
			{
                arrAnswerHistory.Add(int.Parse(rblAnswer.SelectedItem.Value));
            }

            //} of quiz?
            if ( intQuestionNo == intTotalQuestion )
			{
                //Yes! Show the result...
                QuizScreen.Visible = false;
                ResultScreen.Visible = true;

                //Render result screen
                ShowResult();

            } 
			else
			{
                //! yet! Show another question...
                QuizScreen.Visible = true;
                ResultScreen.Visible = false;
                intQuestionNo += 1;

                //Render next question
                ShowQuestion(intQuestionNo);
            }
        }


		void ShowQuestion( int intQuestionNo)
		{
			XmlNodeList xNodeList;
			XmlNode xNode;
			string strXPath;
			int i;
			TimeSpan tsTimeSpent;

            strXPath = "/quiz/mchoice[" + intQuestionNo.ToString() + "]";

            //Extract question
            lblQuestion.Text = intQuestionNo.ToString() + ". " + xDoc.SelectSingleNode(strXPath + "/question").InnerXml;

            //Extract answers
            xNodeList = xDoc.SelectNodes(strXPath + "/answer");

            //Clear previous listitems
            rblAnswer.Items.Clear();

			int cnt;
            for ( i = 0 ; i<= xNodeList.Count - 1; i++)
			{
				cnt = i + 1;

                //Add item to radiobuttonlist
                rblAnswer.Items.Add(new ListItem(xNodeList.Item(i).InnerText, cnt.ToString()));

                //Extract correct answer
				xNode = xNodeList.Item(i).Attributes.GetNamedItem("correct");
                if (xNode != null )
				{
                    if ( xNode.Value == "yes" )
                        ViewState["CorrectAnswer"] = cnt;
                }
            } //

            //Output Total Question
            lblTotalQuestion.Text = intTotalQuestion.ToString();

            //Output Time Spent
            tsTimeSpent = DateTime.Now.Subtract((DateTime) ViewState["StartTime"]);
            lblTimeSpent.Text = tsTimeSpent.Minutes.ToString() + ":" + tsTimeSpent.Seconds.ToString();

            //Store essential data to viewstate
            ViewState["TotalQuestion"] = intTotalQuestion;
            ViewState["Score"] = intScore;
            ViewState["QuestionNo"] = intQuestionNo;
            ViewState["AnswerHistory"] = arrAnswerHistory;
        }

		
		void ShowResult()
		{
			string  strResult;
			string  strXPath;

			TimeSpan tsTimeSpent;
            tsTimeSpent = DateTime.Now.Subtract((DateTime)ViewState["StartTime"]);

            PieChart();

            Random rdm = new Random();

            strResult = "<table width=60% border=0 cellspacing=0 cellpading=4>";
            strResult += "<tr><td align=center>Correct: " + intScore.ToString() + " of " + intTotalQuestion.ToString() + "&nbsp;&nbsp;&nbsp;Time Spent: " + tsTimeSpent.Minutes.ToString() + ":" + tsTimeSpent.Seconds.ToString() + "</td></tr>";
			strResult += "<tr><td align=center><img src='" + PieUrl.FullPath +  "?rdm=" + rdm.Next().ToString() + "'></td></tr>";
			strResult += "<tr><td align=center><br>&nbsp;</td></tr>";

            strResult += "<tr><td align=center>";
            strResult += "<table width=90% border=0 cellspacing=0 cellpading=4>";
            strResult += "<tr><td colspan=2></td></tr>";
            for (int i = 1 ; i <= intTotalQuestion; i++)
			{
                strXPath = "/quiz/mchoice[" + i.ToString() + "]";

                strResult += "<tr><td align=center><b>" + i.ToString() + ". " + xDoc.SelectNodes(strXPath + "/question").Item(0).InnerXml + "</b><br>";
                if ( ((int)arrAnswerHistory[i - 1]) == 0 )
				{
                    strResult += "<font color='green'><b>Correct</b></font><br><br></td></tr>";
                } 
				else
				{
                    strResult += "<b>You answered:</b> " + xDoc.SelectNodes(strXPath + "/answer[" + arrAnswerHistory[i - 1].ToString() + "]").Item(0).InnerXml + "<br>";
                    strResult += "<font color='red'><b>Incorrect</b></font><br><br></td></tr>";
                }
            }
            strResult += "</table></td></tr></table>";

            lblResult.Text = strResult;
        }


		void PieChart()
		{

			ArrayList arrMarks = new ArrayList();
			Bitmap objBitmap;
			Graphics objGraphics;
			Pen objPen;
			Font objFont;

            ArrayList DrawAngle = new ArrayList();
            ArrayList ResultsPercentage = new ArrayList();
            ArrayList FillPieColor = new ArrayList();

            int startAngle;
            int a;
            int b;
            int i;

            arrMarks.Add("Correct");
            arrMarks.Add("Incorrect");
		
			
			float fltTotalQuestion = intTotalQuestion;
			i = (int) Math.Round((intScore / fltTotalQuestion) * 360);
            DrawAngle.Add(i);
			i = (int) Math.Round((intScore / fltTotalQuestion) * 100);
            ResultsPercentage.Add(i);
			
			i = (int) Math.Round(((intTotalQuestion - intScore) / fltTotalQuestion) * 360);
            DrawAngle.Add(i);
			i= (int) Math.Round(((intTotalQuestion - intScore) / fltTotalQuestion) * 100);
            ResultsPercentage.Add(i);


            FillPieColor.Add(Brushes.Green);
            FillPieColor.Add(Brushes.Red);

            objBitmap = new Bitmap(400, 260);

            objGraphics = Graphics.FromImage(objBitmap);
            objGraphics.Clear(Color.White);

            objPen = new Pen(Color.Black);
            objPen.Width = 1;

            objFont = new Font("Arial", 10, GraphicsUnit.Point);

            startAngle = 270;  // start from "12 hour"
            b = 50;
            for ( i = 0 ; i <= 1; i++)
			{
				a = (int) DrawAngle[i];
                objGraphics.FillPie((System.Drawing.Brush) FillPieColor[i], 50, 50, 200, 200, startAngle, a);
                objGraphics.DrawPie(objPen, 50, 50, 200, 200, startAngle, a);

                objGraphics.FillRectangle((System.Drawing.Brush) FillPieColor[i], 260, b + 6, 10, 10);
                objGraphics.DrawRectangle(objPen, 260, b + 6, 10, 10);

                objGraphics.DrawString(arrMarks[i] + " (" + ResultsPercentage[i].ToString() + "%)", objFont, Brushes.Black, 275, b);

                startAngle = startAngle + a;
                b = b + 20;
            }

            objBitmap.Save(Server.MapPath(PieUrl.FullPath), ImageFormat.Gif);
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
			// Added EsperantusKeys for Localization 
			// Mario Endara mario@softworks.com.uy june-1-2004 
            Requiredfieldvalidator1.ErrorMessage = Esperantus.Localize.GetString("ERROR_QUIZ_ANSWER");
	
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
