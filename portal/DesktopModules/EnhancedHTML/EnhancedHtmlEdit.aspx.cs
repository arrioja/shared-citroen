using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Rainbow.Configuration;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Esperantus;
using System.Globalization;

namespace Rainbow.DesktopModules 
{
	/// <summary>
	/// Rainbow EnhancedHtml Module EditItemPage
	/// Written by: José Viladiu, jviladiu@portalServices.net
	/// </summary>
	[History("jviladiu@portalServices.net", "2004/12/28", "Add support for include contents from others modules in a page")]
	[History("jviladiu@portalServices.net", "2004/07/05", "Changed SelectedValue for SelectedItem.Value in many places for compatibility with 1.0")]
	public class EnhancedHtmlEdit : Rainbow.UI.EditItemPage
	{
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolderHTMLEditor;
		protected System.Web.UI.WebControls.Panel pnlEditPage;
		protected Esperantus.WebControls.Button cmdPageUpdate;
		protected Esperantus.WebControls.Button cmdPageCancel;
		protected System.Web.UI.WebControls.TextBox txtPageName;
		protected System.Web.UI.WebControls.TextBox txtViewOrder;
		protected Esperantus.WebControls.Literal Literal1;
		protected Esperantus.WebControls.Literal Literal2;
		protected System.Web.UI.WebControls.Panel pnlSelectPage;
		protected System.Web.UI.WebControls.ListBox lstPages;
		protected Esperantus.WebControls.Button cmdNewPage;
		protected Esperantus.WebControls.Button cmdEditPage;
		protected Esperantus.WebControls.Button cmdDeletePage;
		protected Esperantus.WebControls.Button cmdReturn;
		protected System.Web.UI.WebControls.DropDownList listLanguages;
		protected Esperantus.WebControls.Literal Literal3;
		protected Esperantus.WebControls.Literal Literal4;
		protected Esperantus.WebControls.Literal Literal5;
		protected Esperantus.WebControls.Literal Literal6;
		protected Esperantus.WebControls.Label Label1;
		protected System.Web.UI.WebControls.ListBox lstLanguages;
		protected System.Web.UI.WebControls.RadioButtonList kindOfContent;
		protected System.Web.UI.WebControls.DropDownList listModules;
		protected System.Web.UI.WebControls.DropDownList listAllModules;
		protected Rainbow.UI.WebControls.IHtmlEditor DesktopText;

		private readonly string tokenModule = "#MODULE#";
		private readonly string tokenPortalModule = "#PORTALMODULE#";
		
		/// <summary>
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e)
        {
			HtmlEditorDataType h = new HtmlEditorDataType();
			h.Value = moduleSettings["Editor"].ToString();
			DesktopText = h.GetEditor(PlaceHolderHTMLEditor, ModuleID, bool.Parse(moduleSettings["ShowUpload"].ToString()), portalSettings);
			DesktopText.Width = new System.Web.UI.WebControls.Unit(moduleSettings["Width"].ToString());
			DesktopText.Height = new System.Web.UI.WebControls.Unit(moduleSettings["Height"].ToString());

			if (!Page.IsPostBack)
			{
				if (bool.Parse(moduleSettings["ENHANCEDHTML_GET_CONTENTS_FROM_PORTALS"].ToString())) 
				{
					kindOfContent.Items.Add(new ListItem("External Module", "Portal"));
					CustomListDataType cldtAll = new CustomListDataType(new ModulesDB().GetModulesAllPortals(), "ModuleTitle", "ModuleID");
					listAllModules.CssClass = "NormalTextBox";
					listAllModules.DataSource = cldtAll.DataSource;
					listAllModules.DataValueField = cldtAll.DataValueField;
					listAllModules.DataTextField = cldtAll.DataTextField;
					listAllModules.DataBind();
				}
				CustomListDataType cldt = new CustomListDataType(new ModulesDB().GetModulesSinglePortal(portalSettings.PortalID), "ModuleTitle", "ModuleID");
				listModules.CssClass = "NormalTextBox";
				listModules.DataSource = cldt.DataSource;
				listModules.DataValueField = cldt.DataValueField;
				listModules.DataTextField = cldt.DataTextField;
				listModules.DataBind();

				string comando = Esperantus.Localize.GetString("ENHANCEDHTML_CONFIRMDELETEMESSAGE", "Are You Sure You Wish To Delete This Item ?");
				cmdDeletePage.Attributes.Add("onClick", "javascript:return confirm('" + comando + "');");
				ViewState["UrlReferrer"] = HttpUrlBuilder.BuildUrl(TabID);

				CultureInfo[] listaLang = LanguageSwitcher.GetLanguageList(true);

				lstLanguages.Items.Add (new ListItem(Esperantus.Localize.GetString("ENHANCEDHTML_SHOWALLPAGES", "All Pages"), "0"));
				foreach (CultureInfo ci in listaLang)
				{
					lstLanguages.Items.Add (new ListItem(ci.DisplayName, (ci.LCID).ToString()));
					listLanguages.Items.Add(new ListItem(ci.DisplayName, (ci.LCID).ToString()));
				}
				lstLanguages.SelectedIndex = 0;
				listLanguages.SelectedIndex = 0;
				montaLista ();
			}
       }

		/// <summary>
		/// Set the module guids with free access to this page
		/// </summary>
		protected override ArrayList AllowedModules
		{
			get
			{
				ArrayList al = new ArrayList();
				al.Add ("875254B7-2471-491f-BAF8-4AFC261CC224");
				return al;
			}
		}

		private void montaLista ()
		{
			bool showAllPages = (int.Parse(lstLanguages.SelectedItem.Value) < 1) ;
			CultureInfo[] listaLang = LanguageSwitcher.GetLanguageList(true);
			EnhancedHtmlDB ehdb = new EnhancedHtmlDB();
			SqlDataReader dr;
			lstPages.Items.Clear();

			if (showAllPages)
			{
				dr = ehdb.GetAllPages(ModuleID, WorkFlowVersion.Staging);
			} 
			else 
			{
				dr = ehdb.GetLocalizedPages (ModuleID, int.Parse(lstLanguages.SelectedItem.Value), WorkFlowVersion.Staging);
			}


			try
			{
				while (dr.Read())
				{
					if (showAllPages)
					{
						int ccode = int.Parse(dr["CultureCode"].ToString());
						string texto = string.Empty;

						foreach (CultureInfo ci in listaLang)
						{
							if (ccode == ci.LCID) texto = ci.DisplayName;
						}
						if (texto != string.Empty) 
						{
							lstPages.Items.Add(new ListItem ((string) dr["Title"]+ " (" + texto + ")", dr["ItemID"].ToString()));
						}
					}
					else 
					{
						lstPages.Items.Add(new ListItem ((string) dr["Title"], dr["ItemID"].ToString()));
					}
				}
			}
			finally
			{
				dr.Close();	
			}
			if (lstPages.Items.Count > 0)
			{
				lstPages.SelectedIndex = 0;
				cmdEditPage.Enabled = true;
				cmdDeletePage.Enabled = true;
			} 
			else 
			{
				cmdEditPage.Enabled = false;
				cmdDeletePage.Enabled = false;
			}
		}


		#region Web Form Designer generated code
        /// <summary>
        /// Raises OnInitEvent
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
			// Call requested by ASP.NET
            InitializeComponent();

			// Call base first
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() 
        {    
			this.lstLanguages.SelectedIndexChanged += new System.EventHandler(this.selectLocalizedPages);
			this.cmdNewPage.Click += new System.EventHandler(this.cmdNewPage_Click);
			this.cmdEditPage.Click += new System.EventHandler(this.cmdEditPage_Click);
			this.cmdDeletePage.Click += new System.EventHandler(this.cmdDeletePage_Click);
			this.cmdReturn.Click += new System.EventHandler(this.cmdReturn_Click);
			this.kindOfContent.SelectedIndexChanged += new System.EventHandler(this.kindOfContents_Click);
			this.cmdPageUpdate.Click += new System.EventHandler(this.cmdPageUpdate_Click);
			this.cmdPageCancel.Click += new System.EventHandler(this.cmdPageCancel_Click);
			this.listModules.SelectedIndexChanged += new System.EventHandler(this.kindOfContents_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		#region Events
		
		protected void cmdReturn_Click(object sender, EventArgs e)
		{
			Response.Redirect(((string) ViewState["UrlReferrer"]), true);
		}

		protected void cmdPageCancel_Click(object sender, EventArgs e)
		{
			pnlSelectPage.Visible = true;
			pnlEditPage.Visible = false;
			if (lstPages.SelectedItem == null) // Cancel a new page
			{
				if (lstPages.Items.Count > 0)
				{
					lstPages.SelectedIndex = 0;
					cmdEditPage.Enabled = true;
					cmdDeletePage.Enabled = true;
				} 
				else 
				{
					lstPages.SelectedIndex = -1;
					cmdEditPage.Enabled = false;
					cmdDeletePage.Enabled = false;
				}
			}
		}

		protected void cmdDeletePage_Click(object sender, EventArgs e)
		{
			if ((lstPages.SelectedItem.Value != null) && (lstPages.SelectedItem.Value != "-1"))
			{
				EnhancedHtmlDB tdb1 = new EnhancedHtmlDB();
				tdb1.DeletePage(int.Parse(this.lstPages.SelectedItem.Value));
				if (lstPages.Items.Count > 0)
				{
					lstPages.SelectedIndex = 0;
					cmdEditPage.Enabled = true;
					cmdDeletePage.Enabled = true;
				} 
				else 
				{
					lstPages.SelectedIndex = -1;
					cmdEditPage.Enabled = false;
					cmdDeletePage.Enabled = false;
				}
				Response.Redirect(Request.Url.ToString(), true);
			}
		}

		protected void cmdEditPage_Click(object sender, EventArgs e)
		{
			pnlSelectPage.Visible = false;
			pnlEditPage.Visible = true;
			txtPageName.Text = string.Empty;
			txtViewOrder.Text = "100";

			EnhancedHtmlDB ehdb = new EnhancedHtmlDB();
			SqlDataReader dr = ehdb.GetSinglePage(int.Parse(lstPages.SelectedItem.Value), WorkFlowVersion.Staging);

			try
			{
				if (dr.Read())
				{
					txtPageName.Text = ((string) dr["Title"]);
					txtViewOrder.Text = dr["ViewOrder"].ToString();
					foreach (ListItem li in listLanguages.Items) 
					{
						li.Selected = li.Value.Equals(dr["CultureCode"].ToString());
					}
					DesktopText.Text = Server.HtmlDecode(((string) dr["DesktopHtml"]));
				}
			}
			finally
			{
				dr.Close();
			}
			if (DesktopText.Text.StartsWith(tokenModule)) 
			{
				kindOfContent.SelectedIndex = 1;
				int module = int.Parse(DesktopText.Text.Substring(tokenModule.Length));
				foreach (ListItem li in listModules.Items)
					if (int.Parse(li.Value) == module) 
						li.Selected =true;
			}
			else if (DesktopText.Text.StartsWith(tokenPortalModule)) 
			{
				kindOfContent.SelectedIndex = 2;
				int module = int.Parse(DesktopText.Text.Substring(tokenPortalModule.Length));
				foreach (ListItem li in listAllModules.Items)
					if (int.Parse(li.Value) == module) 
						li.Selected =true;
			}
			else
				kindOfContent.SelectedIndex = 0;
			kindOfContents_Click (sender, e);
		}

		protected void cmdNewPage_Click(object sender, EventArgs e)
		{
			pnlSelectPage.Visible = false;
			pnlEditPage.Visible = true;
			txtPageName.Text = string.Empty;
			txtViewOrder.Text = "100";
			DesktopText.Text = string.Empty;
			lstPages.SelectedIndex = -1;
		}

		protected void cmdPageUpdate_Click(object sender, EventArgs e)
		{
			string titlePage = txtPageName.Text;
			if (((titlePage == null) || (titlePage.Length < 1)) || (titlePage.Trim().Length < 1))
			{
				titlePage = "Page";
			}
			string orderView = txtViewOrder.Text;
			int order = 0;
			try 
			{
				order = int.Parse(orderView);
			}
			catch 
			{
			}
			if (order < 1) 
			{
				orderView = "100";
				txtViewOrder.Text = orderView;
				order = 100;
			}
			string text = string.Empty;
			int i;

			switch (kindOfContent.SelectedValue) 
			{
				case "Editor":
					text = Server.HtmlEncode(DesktopText.Text);
					if (text.StartsWith(tokenModule) || text.StartsWith(tokenPortalModule))
						text = string.Empty;
					break;
				case "Module":
					i = int.Parse(listModules.SelectedValue);
					if (i == 0 || i == this.ModuleID) return; // Cannot select this module
					text = tokenModule + listModules.SelectedValue;
					break;
				case "Portal":
					i = int.Parse(listAllModules.SelectedValue);
					if (i == 0 || i == this.ModuleID) return; // Cannot select this module
					text = tokenPortalModule + listAllModules.SelectedValue;
					break;
			}

			string user = PortalSettings.CurrentUser.Identity.Email;
			EnhancedHtmlDB tdb1 = new EnhancedHtmlDB();
			if (lstPages.SelectedIndex >= 0)
			{
				int itemID = int.Parse(lstPages.SelectedItem.Value);
				tdb1.UpdatePage(ModuleID, itemID, user, titlePage, order, int.Parse(listLanguages.SelectedItem.Value), text); 
			}
			else
			{
				tdb1.AddPage(ModuleID, 0, user, titlePage, order, int.Parse(listLanguages.SelectedItem.Value), text);
			}
			pnlSelectPage.Visible = true;
			pnlEditPage.Visible = false;
			montaLista();
		}

		private void selectLocalizedPages(object sender, System.EventArgs e)
		{
			montaLista();
		}

		private void kindOfContents_Click(object sender, System.EventArgs e)
		{
			switch (kindOfContent.SelectedValue) 
			{
				case "Editor":
					Literal2.TextKey = "HTML_DESKTOP_CONTENT";
					Literal2.Text = "Desktop HTML Content";
					PlaceHolderHTMLEditor.Visible = true;
					listModules.Visible = false;
					listAllModules.Visible = false;
					string text = Server.HtmlEncode(DesktopText.Text);
					if (text.StartsWith(tokenModule) || text.StartsWith(tokenPortalModule))
						DesktopText.Text = string.Empty;
					break;
				case "Module":
					Literal2.TextKey = "ENHANCEDHTML_SELECT_MODULE";
					Literal2.Text = "Select a module from this portal for show the contents in this page";
					PlaceHolderHTMLEditor.Visible = false;
					listModules.Visible = true;
					listAllModules.Visible = false;
					break;
				case "Portal":
					Literal2.TextKey = "ENHANCEDHTML_SELECT_MODULE_PORTAL";
					Literal2.Text = "Select a module in database portals for show the contents in this page";
					PlaceHolderHTMLEditor.Visible = false;
					listModules.Visible = false;
					listAllModules.Visible = true;
					break;
			}
		
		}
		#endregion


    }
}

