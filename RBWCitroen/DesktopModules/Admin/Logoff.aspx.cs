using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using Rainbow.Configuration;
using Rainbow.Security;

namespace Rainbow.Admin
{
    /// <summary>
	/// The Logoff page is responsible for signing out a user 
	/// from the cookie authentication, and then redirecting 
	/// the user back to the portal home page.
	/// This page is executed when the user	clicks 
	/// the Logoff button at the top of the page.
    /// </summary>
    public class Logoff : Rainbow.UI.Page
    {
        private void Page_Load(object sender, System.EventArgs e)
        {
			// Signout
			PortalSecurity.SignOut();
        }

		#region Web Form Designer generated code
        /// <summary>
        /// Raises the Init event.
        /// </summary>
        /// <param name="e"></param>
		override protected void OnInit(EventArgs e)
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
