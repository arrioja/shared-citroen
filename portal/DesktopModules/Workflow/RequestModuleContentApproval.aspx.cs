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
	/// Summary description for RequestModuleContentApproval.
	/// </summary>
	[History("Geert.Audenaert@Syntegra.Com", "2003/03/11", "Added default content in emfailform.")]
	public class RequestModuleContentApproval : Rainbow.UI.Page 
	{
		protected Esperantus.WebControls.LinkButton btnRequestApproval;
		protected Esperantus.WebControls.LinkButton btnRequestApprovalAndSendMail;
		protected System.Web.UI.WebControls.Label lblEmailAddressesNotOk;
		protected EmailForm emailForm;

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Check if the user is authorized
			if ( ! (PortalSecurity.HasAddPermissions(ModuleID)
					|| PortalSecurity.HasEditPermissions(ModuleID)
					|| PortalSecurity.HasDeletePermissions(ModuleID)) )
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
				string[] emails = MailHelper.GetEmailAddressesInRoles(ms.AuthorizedApproveRoles.Split(";".ToCharArray()), portalSettings.PortalID);
				for ( int i=0; i < emails.Length; i++)
					emailForm.To.Add(emails[i]);
				// Subject
				emailForm.Subject = Esperantus.Localize.GetString ("SWI_REQUEST_APPROVAL_SUBJECT", "Request approval of the new content of '") + ms.ModuleTitle + "'";
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
			this.btnRequestApprovalAndSendMail.Click += new System.EventHandler(this.btnRequestApprovalAndSendMail_Click);
			this.btnRequestApproval.Click += new System.EventHandler(this.btnRequestApproval_Click);
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cancelButton_Click(object sender, System.EventArgs e)
		{
			this.RedirectBackToReferringPage();
		}

		private void btnRequestApproval_Click(object sender, System.EventArgs e)
		{
			RequestApproval();
		}

		private void btnRequestApprovalAndSendMail_Click(object sender, System.EventArgs e)
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
				RequestApproval();
			}
		}

		private void RequestApproval()
		{
			WorkFlowDB.RequestApproval(ModuleID);
			this.RedirectBackToReferringPage();
		}

	}
}
