using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.Admin;
using Rainbow.Security;
using Rainbow.Configuration;
using Esperantus;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// The SignInLink module shows "signin" and "register" links, as
	/// an alternative to the signin form. Written by Jes1111.
	/// </summary>
    public class SigninLink : PortalModuleControl
	{
		protected Esperantus.WebControls.LinkButton SignInBtn;
		protected Esperantus.WebControls.LinkButton RegisterBtn;

        private void SignInBtn_Click(Object sender, System.EventArgs e) 
        {
			Response.Redirect(Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Admin/LogOn.aspx"));
        }
        
        private void RegisterBtn_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Register/Register.aspx"));
		}

		public SigninLink()
		{
		}

		/// <summary>
		/// Overrides ModuleSetting to render this module type un-cacheable
		/// </summary>
		public override bool Cacheable
		{
			get
			{
				return false;
			}
		}
		#region General Implementation
        public override Guid GuidID 
        {
            get
            {
                return new Guid("{E2AE1D7E-E2FE-466f-A2F4-EB9465BC8966}");
            }
        }
		#endregion

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
			this.SignInBtn.Click += new System.EventHandler(this.SignInBtn_Click);
			this.RegisterBtn.Click += new System.EventHandler(this.RegisterBtn_Click);
			this.Load += new System.EventHandler(this.SigninLink_Load);

		}
		#endregion

		private void SigninLink_Load(object sender, System.EventArgs e)
		{
			if(!bool.Parse(portalSettings.CustomSettings["SITESETTINGS_ALLOW_NEW_REGISTRATION"].ToString()))
				RegisterBtn.Visible = false;
		}
    }
}
