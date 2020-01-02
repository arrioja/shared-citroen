using System;
using System.Collections;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Rainbow.Configuration;
using Esperantus;

namespace Rainbow.DesktopModules 
{
	/// <summary>
	/// Rainbow EnhancedHtml Module
	/// Written by: José Viladiu, jviladiu@portalServices.net
	/// This module show a list of pages with navigation control
	/// and language control.
	/// </summary>
	public class EnhancedHtml : PortalModuleControl 
	{
        protected System.Web.UI.WebControls.PlaceHolder HtmlHolder;

		#region private variables
		private string pageID;
		private string modeID;
		private string currentModeURL;
		private string currentURL;
		private string currentMenuSeparator;
		private readonly string tokenModule = "#MODULE#";
		private readonly string tokenPortalModule = "#PORTALMODULE#";
		#endregion

		#region page load
		/// <summary>
		/// The Page_Load event handler on this User Control is
		/// used to render blocks of HTML or text to the page.  
		/// The text/HTML to render is stored in the EnhancedHtml 
		/// database table.  This method uses the Rainbow.EnhancedHtmlDB()
		/// data component to encapsulate all data functionality.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e) 
		{
			bool addInvariantCulture = bool.Parse(Settings["ENHANCEDHTML_ADDINVARIANTCULTURE"].ToString());
			bool showTitle = bool.Parse(Settings["ENHANCEDHTML_SHOWTITLEPAGE"].ToString());
			estableceParametros ();
			DataTable paginas = giveMePages(addInvariantCulture);
			if (paginas.Rows.Count == 0) return;

			if (modeID == "1") 
			{
				#region render all pages
				string ehMultiPageMode = Esperantus.Localize.GetString("ENHANCEDHTML_MULTIPAGEMODE", "Multi Page Mode");

				foreach (DataRow fila in paginas.Rows)
				{
					Content = fila["DesktopHtml"];
					int i = int.Parse((string) fila["ItemID"]);
					string aux = "<p/><table width='100%' border='0' cellpadding='0' cellspacing='0'><tr>";

					if (showTitle)
					{
						aux += "<br><td width='100%' align='left'><span class='EnhancedHtmlTitlePage'>" + fila["Title"] + "</span><hr></td>";
					}
					// Not show navigation in Print Page
					if (paginas.Rows.Count > 1 && !this.NamingContainer.ToString().Equals("ASP.print_aspx")) 
					{
						aux += "<td align='right'><a href=" + dameUrl("&ModeID=0&PageID=" + fila["ItemID"]) + ">";
						aux += "<img src='" + Rainbow.Settings.Path.ApplicationRoot + "/DesktopModules/EnhancedHTML/multipage.gif' alt='" + ehMultiPageMode + "' border='0' /></a></td>";
					}
					aux += "</tr></table>";

					HtmlHolder.Controls.Add(new LiteralControl(aux));
					HtmlHolder.Controls.Add(toShow(Content.ToString()));
				}
				#endregion
			}
			else 
			{
				#region render selected page
				bool showMultiMode   = bool.Parse(Settings["ENHANCEDHTML_SHOWMULTIMODE"].ToString()) && paginas.Rows.Count > 1;
				bool showUpmenu	     = bool.Parse(Settings["ENHANCEDHTML_SHOWUPMENU"].ToString()) && paginas.Rows.Count > 1;
				bool showDownmenu    = bool.Parse(Settings["ENHANCEDHTML_SHOWDOWNMENU"].ToString()) && paginas.Rows.Count > 1;

				// Not show navigation in Print Page
				if (this.NamingContainer.ToString().Equals("ASP.print_aspx")) 
				{
					showMultiMode = false;
					showUpmenu    = false;
					showDownmenu  = false;
				}
				string alignUpmenu   = dameAlineacion(Settings["ENHANCEDHTML_ALIGNUPMENU"].ToString());
				string alignDownmenu = dameAlineacion(Settings["ENHANCEDHTML_ALIGNDOWNMENU"].ToString());
				string upmenu = string.Empty;
				string downmenu = string.Empty;
				string titulo = string.Empty;
				string buffer = string.Empty;
				string referencia = string.Empty;
				string prevRef = string.Empty;
				string nextRef = string.Empty;
				int i;
				int totalPages = 0;
				int actualPage = -1;
				bool primera = (pageID == null);
				bool first = true;
				string aux;

				string ehPage = Esperantus.Localize.GetString("ENHANCEDHTML_PAGE", "Page");
				string ehOf = Esperantus.Localize.GetString("ENHANCEDHTML_OF", "of");
				string ehSinglePageMode = Esperantus.Localize.GetString("ENHANCEDHTML_SINGLEPAGEMODE", "Single Page Mode");
				
				foreach (DataRow fila in paginas.Rows)
				{
					i = int.Parse((string)fila["ItemID"]);
					totalPages++;
					if (first)
					{
						first = false;
					} 
					else
					{
						upmenu += currentMenuSeparator;
					}
					if (primera)
					{
						primera = false;
						pageID = i.ToString();
					} 
					if (i == int.Parse(pageID))
					{
						actualPage = totalPages;
						buffer = (string) fila["DesktopHtml"];
						titulo = (string) fila["Title"];
						//fix by Rob Siera to prevent css problem with ContentPane A: classes being inherited (and not overwritten)
						upmenu += "<span class='EnhancedHtmlLink'>" + titulo + "</span>";
						//upmenu += "<a class='EnhancedHtmlLink' >" + titulo + "</a>";
						if (referencia != string.Empty)
						{
							prevRef = referencia;
						}
					}
					else 
					{
						referencia = "<a href='" + dameUrl("&ModeID=0&PageID=" + i.ToString()) + "' " + "class='EnhancedHtmlLink'>" + fila["Title"] + "</a>";
						upmenu += referencia;
						if (totalPages - 1 == actualPage) 
						{
							nextRef = referencia;
						}
					}
				}
				if (prevRef != string.Empty) downmenu += prevRef + currentMenuSeparator;
				downmenu += ehPage + " " + actualPage.ToString() + " " + ehOf + " " + totalPages.ToString();
				if (nextRef != string.Empty) downmenu += currentMenuSeparator + nextRef;

				aux = "<br><table width='100%' border='0' cellpadding='0' cellspacing='0'><tr>";
				if (showTitle)
				{
					aux += "<td align='left'><span class='EnhancedHtmlTitlePage'>" + titulo + "</span></td>";
					if (showMultiMode) 
					{
						aux += "<td align='right'><a href=" + dameUrl("&ModeID=1") + ">";
						aux += "<img src='" + Rainbow.Settings.Path.ApplicationRoot + "/DesktopModules/EnhancedHTML/singlepage.gif' alt='" + ehSinglePageMode + "' border='0' /></a></td>";
					}
					aux += "</tr><tr><td colspan=2><hr></td></tr><tr>";
				}
				if (showUpmenu && totalPages != 0)
				{
					aux += "<td class='EnhancedHtmlIndexMenu' align='" + alignUpmenu + "'>" + upmenu +"</td>";
				}
				if (!showTitle && showMultiMode)
				{
					aux += "<td align='right'><a href=" + dameUrl("&ModeID=1") + ">";
					aux += "<img src='" + Rainbow.Settings.Path.ApplicationRoot + "/DesktopModules/EnhancedHTML/singlepage.gif' alt='" + ehSinglePageMode + "' border='0' /></a></td>";
				}
				if (!aux.Equals("<br><table width='100%' border='0' cellpadding='0' cellspacing='0'><tr>")) // If table is empty not add to the control
				{
					aux += "</tr></table><br>";
					HtmlHolder.Controls.Add(new LiteralControl(aux));
				}
				HtmlHolder.Controls.Add(toShow(buffer));
				if (showDownmenu && totalPages != 0)
				{
					HtmlHolder.Controls.Add(new LiteralControl("<hr><table width='100%' border='0' cellpadding='0' cellspacing='0'><tr><td align='" + alignDownmenu + "'>" + downmenu + "</td></tr></table>"));
				}
				#endregion
			}
		}
		#endregion

		#region getContents to show
		private Control toShow (string text) 
		{
			int module = 0;
			if (text.StartsWith(tokenModule)) 
			{
				module = int.Parse(text.Substring(tokenModule.Length));
			}
			else if (text.StartsWith(tokenPortalModule)) 
			{
				module = int.Parse(text.Substring(tokenPortalModule.Length));
			}
			else return new LiteralControl(text.ToString());

			PortalModuleControl portalModule;
			string ControlPath = string.Empty;
			using (SqlDataReader dr = ModuleSettings.GetModuleDefinitionByID(module))
			{
				if (dr.Read())
					ControlPath = Rainbow.Settings.Path.ApplicationRoot + "/" + dr["DesktopSrc"].ToString();
			}
			try
			{
				if (ControlPath == null || ControlPath.Length == 0)
				{
					return new LiteralControl("Module '" + module + "' not found! ");
				}
				portalModule = (PortalModuleControl) Page.LoadControl(ControlPath);

				//Sets portal ID
				portalModule.PortalID = PortalID;
	            
				ModuleSettings  m = new ModuleSettings();           
				m.ModuleID = module;
				m.TabID = this.ModuleConfiguration.TabID;
				m.PaneName = this.ModuleConfiguration.PaneName;
				m.ModuleTitle = this.ModuleConfiguration.ModuleTitle;
				m.AuthorizedEditRoles = string.Empty;
				m.AuthorizedViewRoles = string.Empty;
				m.AuthorizedAddRoles = string.Empty;
				m.AuthorizedDeleteRoles = string.Empty;
				m.AuthorizedPropertiesRoles = string.Empty;
				m.CacheTime = this.ModuleConfiguration.CacheTime;
				m.ModuleOrder = this.ModuleConfiguration.ModuleOrder;
				m.ShowMobile = this.ModuleConfiguration.ShowMobile;
				m.DesktopSrc = ControlPath;

				portalModule.ModuleConfiguration = m;

				portalModule.Settings["MODULESETTINGS_APPLY_THEME"] = false;
				portalModule.Settings["MODULESETTINGS_SHOW_TITLE"] = false;
			}
			catch(Exception ex)
			{
				Rainbow.Configuration.ErrorHandler.HandleException("Shortcut: Unable to load control '" + ControlPath + "'!", ex);
				return new LiteralControl("<br><span class=NormalRed>" + "Unable to load control '" + ControlPath + "'!" + "<br>");
			}
            
			portalModule.PropertiesUrl = string.Empty;
			portalModule.AddUrl = string.Empty;       //Readonly
			portalModule.AddText = string.Empty;      //Readonly
			portalModule.EditUrl = string.Empty;      //Readonly
			portalModule.EditText = string.Empty;     //Readonly
			portalModule.OriginalModuleID = this.ModuleID;

			Rainbow.Settings.Cache.CurrentCache.Remove(Rainbow.Settings.Cache.Key.ModuleSettings(module));
			return portalModule;
		}
		#endregion

		#region select pages to show
		private void addPageRow (DataTable tabla, string item, string title, string content) 
		{
			DataRow fila = tabla.NewRow();
			fila["ItemID"] = item;
			fila["Title"] = title;
			fila["DesktopHtml"] = Server.HtmlDecode(content);
			tabla.Rows.Add(fila);
		}		
		
		private DataTable giveMePages (bool addInvariantCulture)
		{
			bool selected = false;
			int selectedPage = -1;
			if (!(pageID == null)) selectedPage = int.Parse(pageID);

			DataTable tabla = new DataTable("LocalizedPages");

			tabla.Columns.Add(new DataColumn("ItemID", typeof(string)));
			tabla.Columns.Add(new DataColumn("Title", typeof(string)));
			tabla.Columns.Add(new DataColumn("DesktopHtml", typeof(string)));

			EnhancedHtmlDB ehdb = new EnhancedHtmlDB();
			
			using (SqlDataReader dr = ehdb.GetLocalizedPages(ModuleID, portalSettings.PortalUILanguage.LCID, Version))
			{
				while (dr.Read()) 
				{
					addPageRow(tabla, dr["ItemID"].ToString(), (string) dr["Title"],(string) dr["DesktopHtml"]);
					if (int.Parse(dr["ItemID"].ToString()) == selectedPage) selected = true;
				}

				if (tabla.Rows.Count == 0) 
				{
					if (portalSettings.PortalUILanguage.Parent.LCID != System.Globalization.CultureInfo.InvariantCulture.LCID)
					{
						using (SqlDataReader dr1 = ehdb.GetLocalizedPages(ModuleID, portalSettings.PortalUILanguage.Parent.LCID, Version))
						{
							while (dr1.Read()) 
							{
								addPageRow(tabla, dr1["ItemID"].ToString(), (string) dr1["Title"],(string) dr1["DesktopHtml"]);
								if (int.Parse(dr1["ItemID"].ToString()) == selectedPage) selected = true;
							}
						}
					}
				}
				
				if (addInvariantCulture || tabla.Rows.Count == 0)
				{
					using (SqlDataReader dr2 = ehdb.GetLocalizedPages(ModuleID, System.Globalization.CultureInfo.InvariantCulture.LCID, Version))
					{
						while (dr2.Read()) 
						{
							addPageRow(tabla, dr2["ItemID"].ToString(), (string) dr2["Title"],(string) dr2["DesktopHtml"]);
							if (int.Parse(dr2["ItemID"].ToString()) == selectedPage) selected = true;
						}
					}
				}
			}

			if (!selected) pageID = null;
			return tabla;
		}
		#endregion

		#region get/set initial parameters		
		private void estableceParametros ()
		{
			HttpCookie cookie1;
			DateTime time1;
			TimeSpan span1;
			int num1;
			string moduleCookie;
			bool moduleInUrl = false;

			if (Page.Request.Params["mID"] != null) 
			{
				moduleInUrl = int.Parse(Page.Request.Params["mID"]) == ModuleID;
			}

			currentMenuSeparator = "<span class='EnhancedHTMLSeparator'> | </span>";

			currentURL = HttpUrlBuilder.BuildUrl(Page.Request.Path, TabID, ModuleID);
			currentURL = currentURL.Replace ("//", "/");

			if (moduleInUrl) 
			{
				if (Page.Request.Params["PageID"] != null)
				{
					pageID = Page.Request.Params["PageID"].ToString();
				}
				else if (Page.Request.Params["ItemID"] != null)
				{
					pageID = Page.Request.Params["ItemID"].ToString();
				}
			}
			currentModeURL = currentURL;
			moduleCookie = "EnhancedHtml:" + ModuleID.ToString();
			modeID = "0";
			if (Page.Request.Params["ModeID"] != null)
			{
				modeID = Page.Request.Params["ModeID"].ToString();
				cookie1 = new HttpCookie(moduleCookie);
				cookie1.Value = modeID;
				time1 = DateTime.Now;
				span1 = new TimeSpan (90, 0, 0, 0);
				cookie1.Expires = time1.Add(span1);
				base.Response.AppendCookie(cookie1);
			}
			else
			{
				if (base.Request.Cookies[moduleCookie] != null)
				{
					modeID = Request.Cookies[moduleCookie].Value;
				}
			}
			num1 = currentModeURL.IndexOf("ModeID=");
			if (num1 > 0)
			{
				currentModeURL = currentModeURL.Substring(0, (num1 - 1));
				currentURL = currentModeURL;
			}
			num1 = currentURL.IndexOf("PageID=");
			if (num1 > 0)
			{
				currentURL = currentURL.Substring(0, (num1 - 1));
			}
			if (modeID == null)
			{
				modeID = "0";
			}
		}
		#endregion

		#region auxiliary functions
		private string dameAlineacion (string n)
		{
			if (n.Equals("1")) return "left";
			if (n.Equals("2")) return "center";
			if (n.Equals("3")) return "right";
			return "left";
		}

		private string dameUrl (string custom) 
		{
			// Sugerence by Mario Endara mario@softworks.com.uy 21-jun-2004
			// for compatibility with handler splitter
			return HttpUrlBuilder.BuildUrl("~/" + HttpUrlBuilder.DefaultPage, TabID, ModuleID, null, custom, string.Empty, string.Empty);
		}

		#endregion
		
		#region constructor
		public EnhancedHtml()
		{
			int _groupOrderBase;
			SettingItemGroup _Group;

			#region Module Settings
			_Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			_groupOrderBase = 0;

			HtmlEditorDataType.HtmlEditorSettings (this._baseSettings, _Group);

			SettingItem ShowTitlePage = new SettingItem(new BooleanDataType());
			ShowTitlePage.Value = "false";
			ShowTitlePage.Order = _groupOrderBase + 10;
			ShowTitlePage.Group = _Group;
			ShowTitlePage.EnglishName = "Show Title Page?";
			ShowTitlePage.Description = "Mark this if you like see the Title Page";
			this._baseSettings.Add("ENHANCEDHTML_SHOWTITLEPAGE", ShowTitlePage);

			SettingItem ShowUpMenu = new SettingItem(new BooleanDataType());
			ShowUpMenu.Value = "false";
			ShowUpMenu.Order = _groupOrderBase + 20;
			ShowUpMenu.Group = _Group;
			ShowUpMenu.EnglishName = "Show Index Menu?";
			ShowUpMenu.Description = "Mark this if you like see a index menu whith the titles of all pages";
			this._baseSettings.Add("ENHANCEDHTML_SHOWUPMENU", ShowUpMenu);

			ArrayList alignUpMenu = new ArrayList();
			alignUpMenu.Add(new SettingOption(1, Esperantus.Localize.GetString("LEFT","Left")));
			alignUpMenu.Add(new SettingOption(2, Esperantus.Localize.GetString("CENTER","Center")));
			alignUpMenu.Add(new SettingOption(3, Esperantus.Localize.GetString("RIGHT","Right")));

			SettingItem labelAlignUpMenu = new SettingItem(new CustomListDataType(alignUpMenu, "Name", "Val"));
			labelAlignUpMenu.Description = "Select here the align for index menu";
			labelAlignUpMenu.EnglishName = "Align Index Menu";
			labelAlignUpMenu.Value ="1";
			labelAlignUpMenu.Order = _groupOrderBase + 30;
			this._baseSettings.Add("ENHANCEDHTML_ALIGNUPMENU", labelAlignUpMenu);

			SettingItem ShowDownMenu = new SettingItem(new BooleanDataType());
			ShowDownMenu.Value = "true";
			ShowDownMenu.Order = _groupOrderBase + 40;
			ShowDownMenu.Group = _Group;
			ShowDownMenu.EnglishName = "Show Navigation Menu?";
			ShowDownMenu.Description = "Mark this if you like see a navigation menu with previous and next page";
			this._baseSettings.Add("ENHANCEDHTML_SHOWDOWNMENU", ShowDownMenu);

			ArrayList alignDownMenu = new ArrayList();
			alignDownMenu.Add(new SettingOption(1, Esperantus.Localize.GetString("LEFT","Left")));
			alignDownMenu.Add(new SettingOption(2, Esperantus.Localize.GetString("CENTER","Center")));
			alignDownMenu.Add(new SettingOption(3, Esperantus.Localize.GetString("RIGHT","Right")));

			SettingItem labelAlignDownMenu = new SettingItem(new CustomListDataType(alignDownMenu, "Name", "Val"));
			labelAlignDownMenu.Description = "Select here the align for index menu";
			labelAlignDownMenu.EnglishName = "Align Navigation Menu";
			labelAlignDownMenu.Value ="3";
			labelAlignDownMenu.Order = _groupOrderBase + 50;
			this._baseSettings.Add("ENHANCEDHTML_ALIGNDOWNMENU", labelAlignDownMenu);

			SettingItem AddInvariant = new SettingItem(new BooleanDataType());
			AddInvariant.Value = "true";
			AddInvariant.Order = _groupOrderBase + 60;
			AddInvariant.Group = _Group;
			AddInvariant.EnglishName = "Add Invariant Culture?";
			AddInvariant.Description = "Mark this if you like see pages with invariant culture after pages with actual culture code";
			this._baseSettings.Add("ENHANCEDHTML_ADDINVARIANTCULTURE", AddInvariant);

			SettingItem ShowMultiMode = new SettingItem(new BooleanDataType());
			ShowMultiMode.Value = "true";
			ShowMultiMode.Order = _groupOrderBase + 70;
			ShowMultiMode.Group = _Group;
			ShowMultiMode.EnglishName = "Show Multi-Mode icon?";
			ShowMultiMode.Description = "Mark this if you like see icon multimode page";
			this._baseSettings.Add("ENHANCEDHTML_SHOWMULTIMODE", ShowMultiMode);

			SettingItem GetContentsFromPortals = new SettingItem(new BooleanDataType());
			GetContentsFromPortals.Value = "false";
			GetContentsFromPortals.Order = _groupOrderBase + 80;
			GetContentsFromPortals.Group = _Group;
			GetContentsFromPortals.EnglishName = "Get contents from others Portals?";
			GetContentsFromPortals.Description = "Mark this if you like get contents from modules in others portals in the same database";
			this._baseSettings.Add("ENHANCEDHTML_GET_CONTENTS_FROM_PORTALS", GetContentsFromPortals);

			#endregion

			this.SupportsWorkflow = true;
		}
		#endregion
		
		#region Searchable module implementation

		/// <summary>
		/// Searchable module
		/// </summary>
		public override bool Searchable
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// Searchable module implementation
		/// </summary>
		/// <param name="portalID">The portal ID</param>
		/// <param name="userID">ID of the user is searching</param>
		/// <param name="searchString">The text to search</param>
		/// <param name="searchField">The fields where perfoming the search</param>
		/// <returns>The SELECT sql to perform a search on the current module</returns>
		public override string SearchSqlSelect(int portalID, int userID, string searchString, string searchField)
		{
			// For better performance is necessary to add in 
			// method FillPortalDS from PortalSearch.ascx.cs the
			// next case:
			//    case "875254B7-2471-491F-BAF8-4AFC261CC224":  //EnhancedHtml
			//       strLink = HttpUrlBuilder.BuildUrl("~/DesktopDefault.aspx", Convert.ToInt32(strTabID), strLocate);
			//       break;

			string dbTable = "rb_EnhancedHtml";
			if (Version == WorkFlowVersion.Staging)
				dbTable += "_st";
			Rainbow.Helpers.SearchDefinition s = new Rainbow.Helpers.SearchDefinition(dbTable, "Title", "DesktopHtml", "CreatedByUser", "CreatedDate", searchField);
			string retorno = s.SearchSqlSelect(portalID, userID, searchString);
			if (HttpContext.Current != null && HttpContext.Current.Items["PortalSettings"] != null)
			{
				PortalSettings pS = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
				retorno += " AND ((itm.CultureCode = '" + pS.PortalUILanguage.LCID.ToString() + "') OR (itm.CultureCode = '" + System.Globalization.CultureInfo.InvariantCulture.LCID.ToString() + "'))";
			}
			return retorno;
		}
		#endregion

		#region Web Form Designer generated code
		/// <summary>
		/// OnInit
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{

			InitializeComponent();

			// Add title
//			ModuleTitle = new DesktopModuleTitle();
			this.EditUrl = "~/DesktopModules/EnhancedHtml/EnhancedHtmlEdit.aspx";
//			Controls.AddAt(0, ModuleTitle);

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

		# region Install / Uninstall Implementation

		public override Guid GuidID 
		{
			get
			{
				return new Guid("{875254B7-2471-491f-BAF8-4AFC261CC224}");
			}
		}

		public override void Install(System.Collections.IDictionary stateSaver)
		{
			string currentScriptName = System.IO.Path.Combine(Server.MapPath(TemplateSourceDirectory), "install.sql");
			ArrayList errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);
			if (errors.Count > 0)
			{
				throw new Exception("Error occurred:" + errors[0].ToString());
			}
		}

		public override void Uninstall(System.Collections.IDictionary stateSaver)
		{
			string currentScriptName = System.IO.Path.Combine(Server.MapPath(TemplateSourceDirectory), "uninstall.sql");
			ArrayList errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);
			if (errors.Count > 0)
			{
				throw new Exception("Error occurred:" + errors[0].ToString());
			}
		}
		#endregion
	}
}
