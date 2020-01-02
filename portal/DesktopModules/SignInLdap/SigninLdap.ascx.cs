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
using System.Web.Mail;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.Admin;
using Rainbow.Security;
using Rainbow.Configuration;
using Esperantus;
using Rainbow.UI.DataTypes;
using Rainbow.Helpers;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// The SignIn User Control enables clients to authenticate themselves using 
	/// the ASP.NET Forms based authentication system.
	///
	/// When a client enters their username/password within the appropriate
	/// textboxes and clicks the "Login" button, the LoginBtn_Click event
	/// handler executes on the server and attempts to validate their
	/// credentials against a SQL database.
	///
	/// If the password check succeeds, then the LoginBtn_Click event handler
	/// sets the customers username in an encrypted cookieID and redirects
	/// back to the portal home page.
	/// 
	/// If the password check fails, then an appropriate error message
	/// is displayed.
	/// </summary>
	public class SigninLdap : PortalModuleControl
	{
		protected Esperantus.WebControls.Literal LoginTitle;
		protected Esperantus.WebControls.Literal EmailLabel;
		protected System.Web.UI.WebControls.TextBox email;
		protected Esperantus.WebControls.Literal PasswordLabel;
		protected System.Web.UI.WebControls.TextBox password;
		protected Esperantus.WebControls.CheckBox RememberCheckBox;
		protected Esperantus.WebControls.Button LoginBtn;
		protected Esperantus.WebControls.LinkButton RegisterBtn;
		protected Esperantus.WebControls.LinkButton SendPasswordBtn;
		protected Esperantus.WebControls.Literal Group;
		protected System.Web.UI.WebControls.DropDownList GroupList;
		protected System.Web.UI.HtmlControls.HtmlTableRow TRGroup;
		protected System.Web.UI.HtmlControls.HtmlTableRow TRGroupList;
		protected Esperantus.WebControls.CheckBox LDAPCheckBox;
		protected Esperantus.WebControls.Label Message;		

		private void LoginBtn_Click(Object sender, System.EventArgs e) 
		{
			// modified by Jonathan Fong
			// www.gt.com.au
			string id = null;
			if (LDAPCheckBox.Checked && GroupList.SelectedValue != "Default")
			{
				string dn = "cn=" + email.Text + "," + GroupList.SelectedValue;
				LDAPHelper.SignOn(dn, password.Text, RememberCheckBox.Checked, null);
			}
			else
			{
				id = PortalSecurity.SignOn(email.Text, password.Text, RememberCheckBox.Checked);
			}

			if (id == null)
			{
					Message.Text = "Login failed";
					Message.TextKey = "LOGIN_FAILED";
			}
		}
        
		private void RegisterBtn_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Register/Register.aspx"));
		}

		
		private void SendPasswordBtn_Click(object sender, System.EventArgs e)
		{
			if (email.Text == string.Empty)
			{
				Message.Text = "Please enter you email address";
				Message.TextKey = "SIGNIN_ENTER_EMAIL_ADDR";
				return;
			}
			
			Security.UsersDB usersDB = new Security.UsersDB();

			//Obtain single row of User information
			SqlDataReader dr = usersDB.GetSingleUser(email.Text, portalSettings.PortalID);
			try
			{
					if (dr.Read())
			 {  
				 string AppName = portalSettings.PortalName;
				 string Pswrd = (string) dr["password"];
				 string Name = (string) dr["Email"];
				 string LoginUrl = Rainbow.Settings.Path.ApplicationFullPath + "/DesktopModules/Admin/Logon.aspx?Usr=" + Name + "&Pwd=" + Pswrd + "&Alias=" + portalSettings.PortalAlias;
				 MailMessage mail = new System.Web.Mail.MailMessage();
				
				 // Geert.Audenaert@Syntegra.Com
				 // Date 19 March 2003
				 // We have to use a correct sender address, 
				 // because most SMTP servers reject it otherwise
				 mail.From = System.Configuration.ConfigurationSettings.AppSettings["EmailFrom"].ToString();
				 mail.To = email.Text;
				 mail.Subject = AppName + " - " + Esperantus.Localize.GetString("SIGNIN_SEND_PWD", "Send me password", this);

				 System.Text.StringBuilder sb = new System.Text.StringBuilder();

				 sb.Append(Name);
				 sb.Append(",");
				 sb.Append("\r\n\r\n");
				 sb.Append(Esperantus.Localize.GetString("SIGNIN_PWD_REQUESTED", "This is the password you requested", this));
				 sb.Append(" ");
				 sb.Append(Pswrd);
				 sb.Append("\r\n\r\n");
				 sb.Append(Esperantus.Localize.GetString("SIGNIN_THANK_YOU", "Thanks for your visit.", this));
				 sb.Append(" ");
				 sb.Append(AppName);
				 sb.Append("\r\n\r\n");
				 sb.Append(Esperantus.Localize.GetString("SIGNIN_YOU_CAN_LOGIN_FROM", "You can login from", this));
				 sb.Append(":");
				 sb.Append("\r\n");
				 sb.Append(Rainbow.Settings.Path.ApplicationFullPath);
				 sb.Append("\r\n\r\n");
				 sb.Append(Esperantus.Localize.GetString("SIGNIN_USE_DIRECT_URL", "Or using direct url", this));
				 sb.Append("\r\n");
				 sb.Append(LoginUrl);
				 sb.Append("\r\n\r\n");
				 sb.Append(Esperantus.Localize.GetString("SIGNIN_URL_WARNING", "NOTE: The address above may not show up on your screen as one line. This would prevent you from using the link to access the web page. If this happens, just use the 'cut' and 'paste' options to join the pieces of the URL.", this));

				 mail.Body = sb.ToString();
				 mail.BodyFormat = MailFormat.Text;

				 System.Web.Mail.SmtpMail.SmtpServer = Rainbow.Settings.Portal.SmtpServer;
				 System.Web.Mail.SmtpMail.Send(mail);

				 Message.Text = Esperantus.Localize.GetString("SIGNIN_PWD_WAS_SENT", "Your password was sent to the addess you provided", this);
				 Message.TextKey = "SIGNIN_PWD_WAS_SENT";
			 }
			 else 
			 {
				 Message.Text = Esperantus.Localize.GetString("SIGNIN_PWD_MISSING_IN_DB", "The email you specified does not exists on our database", this);
				 Message.TextKey = "SIGNIN_PWD_MISSING_IN_DB";
			 }
			}
			finally
			{
				dr.Close(); //by Manu, fixed bug 807858
			}
		}

		public SigninLdap()
		{
			SettingItem HideAutomatically = new SettingItem(new Rainbow.UI.DataTypes.BooleanDataType());
			HideAutomatically.Value = "True";
			HideAutomatically.EnglishName = "Hide automatically";
			this._baseSettings.Add("SIGNIN_AUTOMATICALLYHIDE", HideAutomatically);

			// 18/08/2004 Jonathan
			SettingItem LDAPCheck = new SettingItem(new BooleanDataType());
			LDAPCheck.Value = "False";
			LDAPCheck.EnglishName = "LDAP";
			this._baseSettings.Add("LDAP", LDAPCheck);


			//1.2.8.1743b - 09/10/2003
			//New setting on Signin fo disable IE autocomplete by Mike Stone
			//If you uncheck this setting IE will not remember user name and passwords. 
			//Note that users who have memorized passwords will not be effected until their computer 
			//is reset, only new users and/or computers will honor this. 
			SettingItem AutoComplete = new SettingItem(new Rainbow.UI.DataTypes.BooleanDataType());
			AutoComplete.Value = "True";
			AutoComplete.EnglishName = "Allow IE Autocomplete";
			AutoComplete.Description = "If Checked IE Will try to remember logins";
			this._baseSettings.Add("SIGNIN_ALLOW_AUTOCOMPLETE", AutoComplete); 
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
				return new Guid("{64EE6673-434A-47fe-A54F-43AD9B2F2C05}");
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
			// use "View = Unauthenticated Users" instead
			//			//Hide control if not needed
			//			if (Request.IsAuthenticated)
			//				this.Visible = false;

			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() 
		{
			this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
			this.RegisterBtn.Click += new System.EventHandler(this.RegisterBtn_Click);
			this.SendPasswordBtn.Click += new System.EventHandler(this.SendPasswordBtn_Click);
			this.Load += new System.EventHandler(this.Signin_Load);

		}
		#endregion

		private void Signin_Load(object sender, System.EventArgs e)
		{
			if(!bool.Parse(portalSettings.CustomSettings["SITESETTINGS_ALLOW_NEW_REGISTRATION"].ToString()))
				RegisterBtn.Visible = false;

	
			if(bool.Parse(Settings["SIGNIN_AUTOMATICALLYHIDE"].ToString()) && Request.IsAuthenticated)
			{
				this.Visible = false;
			}
			else if(!bool.Parse(Settings["SIGNIN_ALLOW_AUTOCOMPLETE"].ToString()))
			{
				//New setting on Signin fo disable IE autocomplete by Mike Stone
				password.Attributes.Add("autocomplete", "off");
			}

			if (!IsPostBack)
			{
				GroupList.Items.Add(new ListItem("Default", "Default"));

				string contexts = System.Configuration.ConfigurationSettings.AppSettings["LDAPContexts"];
				if (contexts != null)
				{
					string[] names = contexts.Split(";".ToCharArray());
					for (int i=0; i < names.Length; i++)
					{
						GroupList.Items.Add(new ListItem(LDAPHelper.GetContext(names[i]), names[i]));
					}
				}

				LDAPCheckBox.Checked = "True" == Settings["LDAP"].ToString();
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			TRGroup.Visible = LDAPCheckBox.Checked;
			TRGroupList.Visible = LDAPCheckBox.Checked;
			SendPasswordBtn.Visible = !LDAPCheckBox.Checked;
			EmailLabel.TextKey = LDAPCheckBox.Checked ? "USERNAME" : "EMAIL";

			base.OnPreRender (e);
		}
	}
}
