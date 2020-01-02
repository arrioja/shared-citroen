using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Mail;
using Rainbow.UI.DataTypes;
using Rainbow.UI.WebControls;
using Esperantus;
using Rainbow.Security;
using Rainbow.Helpers;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// Summary description for ApproveModuleContent.
	/// </summary>
	[History("Jes1111", "2003/03/04", "Added OnUpdate call to base page to handle cache flushing")]
	[History("Geert.Audenaert@Syntegra.Com", "2003/03/10", "Commented call from Jes, because it caused an error, and it wasn't necessary too")]
	[History("Geert.Audenaert@Syntegra.Com", "2003/03/11", "Added default destinators and text in the email form")]
	public class ApproveModuleContent : Rainbow.UI.Page 
	{
		protected Esperantus.WebControls.LinkButton btnApprove;
		protected Esperantus.WebControls.LinkButton btnApproveAndSendMail;
		protected EmailForm emailForm;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Check if the user is authorized
			if ( ! (PortalSecurity.HasApprovePermissions(ModuleID)) )
				PortalSecurity.AccessDeniedEdit();
	
			// Fill email form with default 
			if ( ! IsPostBack )
			{
				// Destinators
				ModuleSettings ms = null;
				for (int i=0; i < portalSettings.ActiveTab.Modules.Count; i ++)
				{
					ms = (ModuleSettings)portalSettings.ActiveTab.Modules[i];
					if ( ms.ModuleID == ModuleID )
						break;
				}
				string[] emails = MailHelper.GetEmailAddressesInRoles(ms.AuthorizedPublishingRoles.Split(";".ToCharArray()), portalSettings.PortalID);
				for ( int i=0; i < emails.Length; i++)
					emailForm.To.Add(emails[i]);
				// Subject
				emailForm.Subject = Esperantus.Localize.GetString ("SWI_REQUEST_PUBLISH_SUBJECT", "Request publishing for the new content of '") + ms.ModuleTitle + "'";
				// Message
				emailForm.HtmlBodyText = Esperantus.Localize.GetString ("SWI_REQUEST_BODY", "You can find the new content at:") + "<br><br><a href='" + UrlReferrer + "'>" + UrlReferrer + "</a>";
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnApproveAndSendMail.Click += new System.EventHandler(this.btnApproveAndSendMail_Click);
			this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
			this.cancelButton.Click += new System.EventHandler (this.cancelButton_Click); 
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			this.RedirectBackToReferringPage();
		}

		private void btnApprove_Click(object sender, System.EventArgs e)
		{
			Approve(e);
		}

		private void btnApproveAndSendMail_Click(object sender, System.EventArgs e)
		{
			if ( emailForm.AllEmailAddressesOk )
			{
				// Send mail
				MailMessage mm = new MailMessage();
				mm.From = MailHelper.GetCurrentUserEmailAddress(System.Configuration.ConfigurationSettings.AppSettings["EmailFrom"]);
				mm.To = string.Join(";",(string[])emailForm.To.ToArray(typeof(string)));
				mm.Cc = string.Join(";",(string[])emailForm.Cc.ToArray(typeof(string)));
				mm.Bcc = string.Join(";",(string[])emailForm.Bcc.ToArray(typeof(string)));
				mm.BodyFormat = MailFormat.Html;
				mm.Body = emailForm.BodyText;
				mm.Subject = emailForm.Subject;

				SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"];
				SmtpMail.Send(mm);

				// Request approval
				Approve(e);
			}			
		}

		private void Approve(EventArgs e)
		{
			// Geert.Audenaert
			// 10/03/2003
			// This is not necessary
			//base.OnUpdate(e);

			WorkFlowDB.Approve(ModuleID);
			this.RedirectBackToReferringPage();
		}
	}
}
