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
using Rainbow.UI.WebControls;
using System.Web.Mail;
using Esperantus;
using Rainbow.Security;
using Rainbow.Configuration;
using Rainbow.Helpers;
using Rainbow.UI.DataTypes;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// Summary description for RejectModuleContent.
	/// </summary>
	public class RejectModuleContent : Rainbow.UI.Page 
	{
		protected Esperantus.WebControls.LinkButton btnReject;
		protected Esperantus.WebControls.LinkButton btnRejectAndSendMail;
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
				string tmp = ms.AuthorizedAddRoles.Trim();
				tmp += ms.AuthorizedEditRoles.Trim();
				tmp += ms.AuthorizedDeleteRoles.Trim();
				string[] emails = MailHelper.GetEmailAddressesInRoles(tmp.Split(";".ToCharArray()), portalSettings.PortalID);
				for ( int i=0; i < emails.Length; i++)
					emailForm.To.Add(emails[i]);
				// Subject
				emailForm.Subject = Esperantus.Localize.GetString ("SWI_REJECT_SUBJECT1", "The new content of ") + "'" + ms.ModuleTitle + "'" + Localize.GetString ("SWI_REJECT_SUBJECT2", " has been rejected");
				// Message
				emailForm.HtmlBodyText = Esperantus.Localize.GetString ("SWI_REJECT_BODY", "You can find the rejected content at:") + "<br><br><a href='" + UrlReferrer + "'>" + UrlReferrer + "</a>";
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
			this.btnRejectAndSendMail.Click += new System.EventHandler(this.btnRejectAndSendMail_Click);
			this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
			this.cancelButton.Click += new System.EventHandler (this.cancelButton_Click); 
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			this.RedirectBackToReferringPage();
		}

		private void btnReject_Click(object sender, System.EventArgs e)
		{
			Reject();
		}

		private void btnRejectAndSendMail_Click(object sender, System.EventArgs e)
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
				Reject();
			}			
		}

		private void Reject()
		{
			WorkFlowDB.Reject(ModuleID);
			this.RedirectBackToReferringPage();
		}

	}
}
