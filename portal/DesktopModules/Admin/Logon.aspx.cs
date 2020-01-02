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
using Rainbow.Security;
using Rainbow.Configuration;

namespace Rainbow.Admin
{
	/// <summary>
	/// Single click logon, useful for email and newsletters
	/// </summary>
	public class LogonPage : Rainbow.UI.Page
	{
		protected System.Web.UI.WebControls.PlaceHolder signIn;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			string _user = string.Empty;
			string _password = string.Empty;
			string _alias = string.Empty;

			// Get Login User from querystring
			if (Request.Params["usr"] != null)
			{
				_user = Request.Params["usr"];
				// Get Login Password from querystring
				if (Request.Params["pwd"] != null)
				{
					_password = Request.Params["pwd"];
				}
				// Get portalaias
				if (Request.Params["alias"] != null)
				{
					_alias = HttpUrlBuilder.BuildUrl("~/Default.aspx", 0, string.Empty, Request.Params["alias"]);
				}
				//try to validate logon
				if (PortalSecurity.SignOn(_user, _password, false, _alias) == null)
				{
					// Login failed
					PortalSecurity.AccessDenied();
				}
			}
			else
			{
				//if user has logged on
				if(Request.IsAuthenticated)
				{
					// Redirect user back to the Portal Home Page
					PortalSecurity.PortalHome();
				}
				else
				{
					//User not provided, display logon
					signIn.Controls.Add(this.LoadControl("~/DesktopModules/SignIn/Signin.ascx"));
				}
			}
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