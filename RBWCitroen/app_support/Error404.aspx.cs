using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow.Configuration;

namespace Rainbow.Error
{
    /// <summary>
    /// Default 404 error page
    /// </summary>
    public class ErrorPage404 : Rainbow.UI.Page
    {
		protected Esperantus.WebControls.Label Label1;
		protected Esperantus.WebControls.Label Label2;
		protected Esperantus.WebControls.Label Label3;
		protected Esperantus.WebControls.HyperLink ReturnHome;
        
        public void Page_Error(object sender,EventArgs e)
        {
            Response.Redirect("Error404.html", true);
        }
       
		#region Web Form Designer generated code
        /// <summary>
        /// Raises the Init event.
        /// </summary>
        /// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();

			ReturnHome.NavigateUrl = HttpUrlBuilder.BuildUrl();
		
			base.OnInit(e);
		}

        /// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() 
		{    
			this.Error += new System.EventHandler(this.Page_Error);

		}
		#endregion
    }
}