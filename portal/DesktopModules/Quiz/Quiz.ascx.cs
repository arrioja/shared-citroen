using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Esperantus;
using Rainbow.Configuration;


namespace Rainbow.DesktopModules 
{

    /// <summary>
    /// Quiz Module
    /// </summary>
    public class Quiz : PortalModuleControl 
    {
        protected System.Web.UI.WebControls.HyperLink lnkQuiz;

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e) 
        {
			lnkQuiz.Text = Settings["QuizName"].ToString();
			lnkQuiz.NavigateUrl = Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Quiz/QuizPage.aspx","mID=" + ModuleID);
        }

		/// <summary>
		/// Contstructor
		/// </summary>
		public Quiz()
		{
			SettingItem QuizName = new SettingItem(new StringDataType());
			QuizName.Required = true;
			QuizName.Order = 1;
			QuizName.Value = "About Australia (Demo1)";
			this._baseSettings.Add("QuizName", QuizName);

			SettingItem XMLsrc = new SettingItem(new PortalUrlDataType());
			XMLsrc.Required = true;
			XMLsrc.Order = 2;
			XMLsrc.Value = "/Quiz/Demo1.xml";
			this._baseSettings.Add("XMLsrc", XMLsrc);
		}


        public override Guid GuidID 
        {
            get
            {
                return new Guid("{2502DB18-B580-4F90-8CB4-C15E6E531050}");
            }
        }


		#region Web Form Designer generated code
        /// <summary>
        /// On init
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
			InitializeComponent();
			base.OnInit(e);
		}

        /// <summary>
        ///	Required method for Designer support - do not modify
        ///	the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() 
        {
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
    }
}
