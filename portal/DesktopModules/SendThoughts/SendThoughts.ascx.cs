using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Mail;

using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes; 
using Rainbow.Configuration;
using Esperantus;


namespace Rainbow.DesktopModules
{
	
	/// <summary>
	/// This module sends an email with some input from the portal user
	/// Written by: Vlado
	///
	/// Moved into Rainbow by Jakob Hansen, hansen3000@hotmail.com
	/// </summary>
	public class SendThoughts : PortalModuleControl
	{

		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Panel EditPanel;
		protected System.Web.UI.WebControls.TextBox txtEMail;
		protected System.Web.UI.WebControls.TextBox txtName;
		protected System.Web.UI.WebControls.TextBox txtSubject;
		protected System.Web.UI.WebControls.TextBox txtBody;
		protected Esperantus.WebControls.LinkButton SendBtn;
		protected Esperantus.WebControls.LinkButton ClearBtn;
		protected Esperantus.WebControls.RegularExpressionValidator validEMailRegExp;
		protected Esperantus.WebControls.RequiredFieldValidator rfvEMail;
		protected Esperantus.WebControls.RequiredFieldValidator rfvMessageBody;
		protected Esperantus.WebControls.Literal Literal1;
		protected Esperantus.WebControls.Literal Literal2;
		protected Esperantus.WebControls.Literal Literal3;
		protected Esperantus.WebControls.Literal Literal4;

  	    protected string strServerVariables;
		protected string EMailAddress;
		
		
		/// <summary>
		/// Page_Load reads setting items "Email" and "Description".
		/// All messages will be sent to "Email".
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e)
		{

			EMailAddress = Settings["EMail"].ToString();
			string DescText = Settings["Description"].ToString();
		
			if (Page.IsPostBack == false) 
			{
				// Set Image EMailAddress and Desc Properties
				if (EMailAddress == null || EMailAddress.Length == 0)
				{
					Label1.Text = Esperantus.Localize.GetString("SENDTHTS_RECIPIENT","Recipient's EMail address not set.",this.Label1)+"<br>";
					EditPanel.Visible = false;
				}

				txtEMail.Text = PortalSettings.CurrentUser.Identity.Email;
			}
		
			if (!(DescText == null) && DescText.Length != 0)
				Label2.Text = DescText;

			strServerVariables += "HTTP_USER_AGENT: " + Request.ServerVariables["HTTP_USER_AGENT"] + "<br>";
			strServerVariables += "HTTP_HOST: " + Request.ServerVariables["HTTP_HOST"] + "<br>";
			strServerVariables += "REMOTE_HOST: " + Request.ServerVariables["REMOTE_HOST"] + "<br>";
			strServerVariables += "REMOTE_ADDR: " + Request.ServerVariables["REMOTE_ADDR"] + "<br>";
			strServerVariables += "LOCAL_ADDR: " + Request.ServerVariables["LOCAL_ADDR"] + "<br>";
			strServerVariables += "HTTP_REFERER: " + Request.ServerVariables["HTTP_REFERER"] + "<br>";
		}


		/// <summary>
		/// The SendBtn_Click server event handler on this page is
		/// used to handle the scenario where a user clicks the "Send"
		/// button after entering a response to a message post.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void SendBtn_Click(object sender, System.EventArgs e)
		{
			MailMessage mail = new MailMessage();

			mail.BodyFormat = MailFormat.Html;
			mail.From = txtEMail.Text;
			mail.To = EMailAddress;
			mail.Subject = txtSubject.Text;
			mail.Body = 
				txtBody.Text + "<br><br>" + 
				Esperantus.Localize.GetString("SENDTHTS_NAME","Name",this)+": " + txtName.Text + "<br>" + 
				Esperantus.Localize.GetString("SENDTHTS_REMAIL","Real EMail Address",this)+": " + PortalSettings.CurrentUser.Identity.Email + "<br><br>" + 
				strServerVariables;
			SmtpMail.SmtpServer = Rainbow.Settings.Portal.SmtpServer;
			SmtpMail.Send(mail);

			Label2.Text = Esperantus.Localize.GetString("SENDTHTS_SENT","The message was sent - thank you for your message!",this.Label2);
			EditPanel.Visible = false;
		}


		/// <summary>
		/// The ClearBtn_Click server event handler on this page is used
		/// to handle the scenario where a user clicks the "cancel"
		/// button to discard a message post and toggle out of edit mode.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void ClearBtn_Click(object sender, System.EventArgs e)
		{
			txtSubject.Text = string.Empty;
			txtBody.Text = string.Empty;
		}


		public override Guid GuidID 
		{
			get
			{
				return new Guid("{2502DB18-B580-4F90-8CB4-C15E6E531003}");
			}
		}


		/// <summary>
		/// The Constructor adds the Setting items "Email" and "Description"
		/// All messages will be sent to "Email"
		/// </summary>
		public SendThoughts() 
		{
			SettingItem setEMail = new SettingItem(new StringDataType());
			setEMail.Required = true;
			setEMail.Value = string.Empty;
			setEMail.Order = 1;
			this._baseSettings.Add("EMail", setEMail);

			SettingItem setDescription = new SettingItem(new StringDataType());
			setDescription.Required = true;
			setDescription.Value = Esperantus.Localize.GetString("SENDTHTS_DES_TXT","Write a description here...",this);
			setDescription.Order = 2;
			this._baseSettings.Add("Description", setDescription);
		}

		#region Web Form Designer generated code
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
