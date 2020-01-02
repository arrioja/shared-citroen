using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Esperantus;
using Rainbow.Configuration;
using Rainbow.DesktopModules;
using Rainbow.UI;
using Literal = Esperantus.WebControls.Literal;
using Page = System.Web.UI.Page;

namespace Rainbow.Admin
{
	/// <summary>
	/// Blacklist Admin Module - Edit page<br/>
	/// This module is typically used togeteher with the Newsletter module.
	/// Using the Blacklist module you can block some of the registred users 
	/// from receiving emails. Invalid emails are automatically blacklisted 
	/// by newsletter module to prevent further errors.
	/// </summary>
	public class BlacklistEdit : AddEditItemPage
	{
		protected Literal Literal1;
		protected Repeater repListItem;

		private void Page_Load(object sender, EventArgs e)
		{
			string jScript =string.Empty;
			jScript+="<script language=\"javascript\"><!-- \n" ;
			jScript+="function AllCheckboxCheck(frm, flag){ \n" ;
			jScript+="var element; \n" ;
			jScript+="var numberOfControls = document.forms[frm].length; \n" ;
			jScript+="for (var i=0; i<numberOfControls; i++){ \n" ;
			jScript+="	element = document.forms[frm][i]; \n" ;
			jScript+="	if (element.type == \"checkbox\"){ \n" ;
			jScript+="		element.checked = !flag; \n" ;
			jScript+="		element.click();}}} \n" ;
			jScript+="--></script> \n" ;

			Page.RegisterClientScriptBlock ("AllCheckboxCheck", jScript);

			if (!IsPostBack) 
				BindListItem();
		}
		
		/// <summary>
		/// Set the module guids with free access to this page
		/// </summary>
		protected override ArrayList AllowedModules
		{
			get
			{
				ArrayList al = new ArrayList();
				al.Add ("2502DB18-B580-4F90-8CB4-C15E6E531017");
				return al;
			}
		}

		public void BindListItem()
		{
			BlacklistDB blacklist = new BlacklistDB();
			DataSet blist = blacklist.GetBlacklist(this.portalSettings.PortalID, true, false);
			
			repListItem.DataSource = blist;
			repListItem.DataBind();
		}

		public bool GetBlacklisted(object data)
		{
			string email = DataBinder.Eval(data, "Blacklisted").ToString();

			if (email.Length > 0)
           		return true;
			else
				return false;
		}

		public string GetDate(object data)
		{
			string date = DataBinder.Eval(data, "Date", "{0:yyyy-MM-dd}").ToString();

			if (date.Length > 0)
				return date;
			else
				return "-";
		}

		override protected void OnUpdate(EventArgs e) 
		{
			string email;
			Label lblEMail;
			CheckBox chkSelect;
	
			foreach (RepeaterItem SelectItem in repListItem.Items)
			{
				lblEMail = (Label) SelectItem.FindControl("lblEMail");
				email = lblEMail.Text;
				chkSelect = (CheckBox) SelectItem.FindControl("chkSelect");

				if (chkSelect.Checked)
				{
					//BlacklistDB.AddToBlackList(this.portalSettings.PortalID, email, "Blacklisted by " +  Context.User.Identity.Name);
					// Added EsperantusKeys for Localization 
					// Mario Endara mario@softworks.com.uy june-1-2004 
					BlacklistDB.AddToBlackList(this.portalSettings.PortalID, email, Localize.GetString ("NEWSLETTER_BLACKLISTED") + PortalSettings.CurrentUser.Identity.Email);
				}
				else
					BlacklistDB.DeleteFromBlackList(this.portalSettings.PortalID, email);

			}

			// Redirect back to the portal home page
			this.RedirectBackToReferringPage();
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
			this.Load += new EventHandler(this.Page_Load);

		}
		#endregion
	}
}