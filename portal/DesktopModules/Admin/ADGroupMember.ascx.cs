namespace Rainbow.Admin
{
	using Esperantus;
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using Rainbow.UI.WebControls;
	using Rainbow.Helpers;

	/// <summary>
	///		Summary description for ADGroupMember.
	///		Updated 18.Jan.2005 Cory Isakson to fix Jira Bug RBP-382 Line 93
	/// </summary>
	public class ADGroupMember : PortalModuleControl
	{
		protected System.Web.UI.WebControls.DataGrid dgMembers;
		protected Esperantus.WebControls.LinkButton lnkShowMembers;
		protected Esperantus.WebControls.LinkButton lnkRefresh;
		protected System.Web.UI.WebControls.TextBox txtMembers;
		protected System.Web.UI.WebControls.DropDownList ddlDomain;
		protected System.Web.UI.WebControls.Panel pnlShowHide;
		protected System.Web.UI.WebControls.ImageButton btnShowHide;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hidShowMembers;
		protected System.Data.DataView dvMembers;

		public string Members
		{
			get
			{
				return txtMembers.Text;
			}
			set
			{
				txtMembers.Text = value;
			}
		}

		public bool Enabled
		{
			get 
			{
				return dgMembers.Enabled;
			}
			set
			{
				lnkRefresh.Enabled = value;
				lnkShowMembers.Enabled = value;
				dgMembers.Enabled = value;
				txtMembers.Enabled = value;
				ddlDomain.Enabled = value;
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!Page.IsPostBack)
			{
				// added by Jonathan Fong 22/07/2004 to support LDAP
				// www.gt.com.au
				string ldapGroup = System.Configuration.ConfigurationSettings.AppSettings["LDAPGroup"];
				if(ldapGroup != null)
				{
					string[] groups = ldapGroup.Split(";".ToCharArray());
				
					if (groups != null && groups.Length != 0 )
					{
						for (int i = 0; i < groups.Length; i++)
						{
							string name = groups[i].Trim();

							string[] val = name.Split("/".ToCharArray());							
							ddlDomain.Items.Add(new ListItem(val[0], groups[i]));
						}

					}				
				}
				if (Context.User is System.Security.Principal.WindowsPrincipal)
				{
					string[] domains = System.Configuration.ConfigurationSettings.AppSettings["ADdns"].Split(";".ToCharArray());
					if (domains.Length != 0 && domains != null)
					{
						for (int i = 0; i < domains.Length; i++)
						{
							string name = domains[i].Trim();
							if ( name.ToLower().StartsWith("ldap") )
								name = name.Substring(name.IndexOf("/", 8) + 1);
							else
								name = name.Substring(name.IndexOf("/") + 2);
							ListItem li = new ListItem(name, domains[i].Trim());
							ddlDomain.Items.Add(li);
						}

						if ( ddlDomain.Items.Count > 0 )
						{
							ddlDomain.SelectedIndex = 0;
							dvMembers.Table = FillMemberList(false);
							dvMembers.Sort = "DisplayName ASC";
							dvMembers.RowFilter = "AccountType = 'group'";
							dgMembers.DataBind();
						}
					}
				}
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dvMembers = new System.Data.DataView();
			((System.ComponentModel.ISupportInitialize)(this.dvMembers)).BeginInit();
			this.btnShowHide.Click += new System.Web.UI.ImageClickEventHandler(this.btnShowHide_Click);
			this.dgMembers.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgMembers_ItemCommand);
			this.dgMembers.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgMembers_PageIndexChanged);
			this.ddlDomain.SelectedIndexChanged += new System.EventHandler(this.ddlDomain_SelectedIndexChanged);
			this.lnkShowMembers.Click += new System.EventHandler(this.lnkShowMembers_Click);
			this.lnkRefresh.Click += new System.EventHandler(this.lnkRefresh_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			((System.ComponentModel.ISupportInitialize)(this.dvMembers)).EndInit();

		}
		#endregion

		private void lnkShowMembers_Click(object sender, System.EventArgs e)
		{
			dvMembers.Table = FillMemberList(false);
			if (hidShowMembers.Value == "0")
			{
				lnkShowMembers.TextKey = "AD_HIDE_USERS";
				lnkShowMembers.Text = "Hide users";
				dvMembers.Sort = "AccountType ASC, DisplayName ASC";
				dvMembers.RowFilter = string.Empty;
				hidShowMembers.Value = "1";
			}
			else
			{
				lnkShowMembers.TextKey = "AD_SHOW_USERS";
				lnkShowMembers.Text = "Show users";
				dvMembers.Sort = "DisplayName ASC";
				dvMembers.RowFilter = "AccountType = 'group'";
				hidShowMembers.Value = "0";
			}
			if (dgMembers.CurrentPageIndex > dvMembers.Count / dgMembers.PageSize)
				dgMembers.CurrentPageIndex = (int)dvMembers.Count / dgMembers.PageSize;
			dgMembers.DataBind();
		}

		private DataTable FillMemberList(bool refresh)
		{
			if (ddlDomain.SelectedItem.Value.IndexOf("://") > 0)
			{
			return Rainbow.Helpers.ADHelper.GetMemberList(refresh, ddlDomain.SelectedItem.Value, Cache);
			}
			return Rainbow.Helpers.LDAPHelper.GetMemberList(refresh, ddlDomain.SelectedItem.Value, Cache);
		}

		private void lnkRefresh_Click(object sender, System.EventArgs e)
		{
			dvMembers.Table = FillMemberList(true);
			if (hidShowMembers.Value == "0")
			{
				dvMembers.Sort = "DisplayName ASC";
				dvMembers.RowFilter = "AccountType = 'group'";
			}
			else
			{
				dvMembers.Sort = "AccountType ASC, DisplayName ASC";
				dvMembers.RowFilter = string.Empty;
			}
			if (dgMembers.CurrentPageIndex > dvMembers.Count / dgMembers.PageSize)
				dgMembers.CurrentPageIndex = (int)dvMembers.Count / dgMembers.PageSize;
			dgMembers.DataBind();
		}

		private void dgMembers_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			dvMembers.Table = FillMemberList(false);
			if (hidShowMembers.Value == "0")
			{
				dvMembers.Sort = "DisplayName ASC";
				dvMembers.RowFilter = "AccountType = 'group'";
			}
			else
			{
				dvMembers.Sort = "AccountType ASC, DisplayName ASC";
				dvMembers.RowFilter = string.Empty;
			}
			dgMembers.CurrentPageIndex = e.NewPageIndex;
			dgMembers.DataBind();
		}

		private void dgMembers_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if ( e.CommandName == "Select" )
			{
				string MemberToAdd = ((HtmlInputHidden)e.Item.Cells[0].Controls[3]).Value;
				if (txtMembers.Text.ToLower().Trim().IndexOf(MemberToAdd.ToLower().Trim() + " ") == -1 && 
					txtMembers.Text.ToLower().Trim().IndexOf(MemberToAdd.ToLower().Trim() + ";") == -1 &&
					txtMembers.Text.ToLower().Trim().IndexOf(MemberToAdd.ToLower().Trim() + "\n") == -1)
				{
					string[] members = txtMembers.Text.Split(";".ToCharArray());
					System.Text.StringBuilder sb = new System.Text.StringBuilder();
					for ( int i = 0; i < members.Length; i++ )
					{
						string mem = members[i].Trim();
						if ( mem != string.Empty )
						{
							sb.Append(mem);
							sb.Append("; ");
						}
					}
					sb.Append(MemberToAdd.Trim());
					sb.Append("; ");
					txtMembers.Text = sb.ToString();
				}
			}
		}

		private void ddlDomain_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			lnkShowMembers.TextKey = "AD_SHOW_USERS";
			lnkShowMembers.Text = "Show users";
			hidShowMembers.Value = "0";
			dgMembers.CurrentPageIndex = 0;
			dvMembers.Table = FillMemberList(false);
			dvMembers.Sort = "DisplayName ASC";
			dvMembers.RowFilter = "AccountType = 'group'";
			dgMembers.DataBind();		
		}

		private void btnShowHide_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if ( btnShowHide.ImageUrl == this.CurrentTheme.GetImage("Buttons_Max", "Max.gif").ImageUrl )
			{
				pnlShowHide.Visible = true;
				btnShowHide.ImageUrl = this.CurrentTheme.GetImage("Buttons_Min", "Min.gif").ImageUrl;
			}
			else
			{
				pnlShowHide.Visible = false;
				btnShowHide.ImageUrl = this.CurrentTheme.GetImage("Buttons_Max", "Max.gif").ImageUrl;
			}
		}

	}
}
