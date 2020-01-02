using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Esperantus;

namespace Rainbow.Admin
{
    public class RequireRegistration : UserControl
    {
		protected Esperantus.WebControls.HyperLink RegisterHyperlink;
		protected Esperantus.WebControls.Label LabelRegister;
		protected Esperantus.WebControls.Label LabelAlreadyAccount;
		protected Esperantus.WebControls.Label LabelRegisterNow;
		protected Esperantus.WebControls.HyperLink SignInHyperLink;

		#region Web Form Designer generated code
        /// <summary>
        /// Raises the Init event.
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            
            SignInHyperLink.NavigateUrl = Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Admin/Logon.aspx");
            RegisterHyperlink.NavigateUrl = Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Register/Register.aspx");
		
            base.OnInit(e);
        }

        private void InitializeComponent() 
        {

		}
		#endregion

    }
}