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

namespace Rainbow 
{
	/// <summary>
	/// The Default.aspx page simply tests 
	/// the browser type and redirects either to
	/// the DesktopDefault or MobileDefault pages, 
	/// depending on the device type.
	/// </summary>
    public class Default : System.Web.UI.Page 
    {
        private void Page_Load(object sender, System.EventArgs e) 
        {
            if (Request.Browser["IsMobileDevice"] == "true" ) 
            {
				Server.Transfer("MobileDefault.aspx", false);
				//Response.Redirect("MobileDefault.aspx");
            }
            else 
            {
				Server.Transfer("DesktopDefault.aspx", true);
				//Response.Redirect("DesktopDefault.aspx");
            }
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