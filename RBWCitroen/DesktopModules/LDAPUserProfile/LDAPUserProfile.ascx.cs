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
using System.Web.Security;
using System.IO;
using Esperantus;
using Rainbow.Security;
using Rainbow.Configuration;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.Admin;
using Rainbow.Helpers;

namespace Rainbow.DesktopModules
{
    /// <summary>
    /// Placeable Registration (Full) module
    /// </summary>

    public class LDAPUserProfile : PortalModuleControl, IEditUserProfile
    {
		protected System.Web.UI.WebControls.TextBox NameField;
		protected Esperantus.WebControls.Label NameLabel;
		protected System.Web.UI.WebControls.TextBox UseridField;
		protected Esperantus.WebControls.Label UseridLabel;
		protected Esperantus.WebControls.Label PageTitleLabel;
		protected System.Web.UI.HtmlControls.HtmlTableRow UserIDRow;
		protected Esperantus.WebControls.Label EmailLabel;
		protected Esperantus.WebControls.Label MembershipLabel;
		protected System.Web.UI.WebControls.ListBox MembershipListBox;
		protected System.Web.UI.WebControls.TextBox EmailField;
		protected System.Web.UI.WebControls.TextBox DepartmentField;
		protected Esperantus.WebControls.Label DepartmentLabel;
		protected Esperantus.WebControls.Label ErrorMessage;

		#region Web Form Designer generated code
        /// <summary>
        /// Raises the Init event.
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();


			// Add title and configuration
//			ModuleTitle = new DesktopModuleTitle();
			//ModuleConfiguration = new ModuleSettings();
			//ModuleConfiguration.ModuleTitle = Esperantus.Localize.GetString("REGISTER");

//			Controls.AddAt(0, ModuleTitle);

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

        private void Page_Load(object sender, System.EventArgs e)
        {
			try
			{
				RainbowPrincipal user = HttpContext.Current.User as RainbowPrincipal;
				if (user != null)
				{
					Hashtable userProfile = LDAPHelper.GetUserProfile(user.Identity.ID);
					this.UseridField.Text = ((string[]) userProfile["DN"])[0];
					this.NameField.Text = ((string[]) userProfile["FULLNAME"])[0];
					this.EmailField.Text = ((string[]) userProfile["MAIL"])[0];
					this.DepartmentField.Text = ((string[]) userProfile["OU"])[0];
					this.MembershipListBox.DataSource = userProfile["GROUPMEMBERSHIP"];
					this.MembershipListBox.DataBind();
				}
			}
			catch(Exception ex)
			{
				ErrorMessage.Visible = true;
				Rainbow.Configuration.ErrorHandler.HandleException("Error retrieving user", ex);
			}
		}

		public override Guid GuidID 
		{
			get
			{
				return new Guid("{224D9473-3AD4-4850-9F07-9055957D7BE7}");
			}
		}

		#region IEditUserProfile Members

		public bool EditMode
		{
			get
			{
				// TODO:  Add LDAPUserProfile.EditMode getter implementation
				return false;
			}
		}

		public string RedirectPage
		{
			get
			{
				// TODO:  Add LDAPUserProfile.RedirectPage getter implementation
				return null;
			}
			set
			{
				// TODO:  Add LDAPUserProfile.RedirectPage setter implementation
			}
		}

		public int SaveUserData()
		{
			// TODO:  Add LDAPUserProfile.SaveUserData implementation
			return 0;
		}

		#endregion
	}
}