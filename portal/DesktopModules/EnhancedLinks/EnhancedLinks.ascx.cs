using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Rainbow.Configuration;
using Esperantus;

namespace Rainbow.DesktopModules 
{
	/// <summary>
	/// Rainbow EnhancedLinks Module
	/// Written by: José Viladiu, jviladiu@portalServices.net
	/// </summary>
	public class EnhancedLinks : PortalModuleControl 
	{
		protected System.Web.UI.WebControls.DropDownList cboLinks;
		protected System.Web.UI.WebControls.Panel results;

		/// <summary>
		/// The Page_Load event handler on this User Control is used to
		/// obtain a DataReader of link information from the EnhancedLinks
		/// table, and then databind the results to a templated DataList
		/// server control.  It uses the Rainbow.EnhancedLinkDB()
		/// data component to encapsulate all data functionality.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e) 
		{
			string iconContainer = Settings["ENHANCEDLINKS_ICONPATH"].ToString();
			string defaultImage = Settings["ENHANCEDLINKS_DEFAULTIMAGE"].ToString();
			bool dropDownList = Settings["ENHANCEDLINKS_SWITCHERTYPES"].ToString().Equals("1");
			bool doExpandAll  = bool.Parse(Settings["ENHANCEDLINKS_EXPANDALL"].ToString());
			int maxColumns = int.Parse(Settings["ENHANCEDLINKS_MAXCOLUMS"].ToString());
			int actualColumn = 1;
			
			if (dropDownList)
			{
				if (!Page.IsPostBack) 
				{
					results.Visible = false;
					cboLinks.Visible = true;
					cboLinks.DataSource = mountDataList();
					cboLinks.DataTextField = "TextField";
					cboLinks.DataValueField = "ValueField";
					cboLinks.SelectedIndex = 0;
					cboLinks.DataBind();
				}
				return;
			}
			int itemCount = 0;
			EnhancedLinkDB enhancedLinks = new EnhancedLinkDB();
			StringBuilder listStr = new StringBuilder();
			listStr.Append("<table border=0 cellspacing=5 cellpadding=0>");
			SqlDataReader dr = enhancedLinks.GetEnhancedLinks(ModuleID, Version);

			listStr.Append("<tr>");

			while (dr.Read()) 
			{
				string imageStr = (string)dr["ImageURL"];
				string linkStr = (string)dr["Url"];
				string titleStr = (string)dr["Title"];
				string descStr = (string)dr["Description"];
				string targetStr = (string)dr["Target"];
				bool isSeparator = linkStr.Equals("SEPARATOR");

				if (isSeparator) 
				{
					listStr.Append("<td>&nbsp;</td></tr><tr>");
					actualColumn = 1;
				}
				if (IsEditable) 
				{
					listStr.Append("<td><a href=\"" + GetLinkUrl(dr["ItemID"].ToString(), linkStr, true)+"\"><img src='" + this.CurrentTheme.GetImage("Buttons_Edit", "Edit.gif").ImageUrl + "' border=0></a>&nbsp;</td>");
				}
				string auxImage;
				if ((imageStr!=null) && (imageStr.Length>0)) 
				{
					auxImage = this.portalSettings.PortalFullPath + "/" + iconContainer + "/" + imageStr;
				} 
				else if ((defaultImage!=null) && (defaultImage.Length>0))
				{
					auxImage = this.portalSettings.PortalFullPath + "/" + iconContainer + "/" + defaultImage;
				}
				else 
				{
				    auxImage = this.CurrentTheme.GetImage("NavLink", "navlink.gif").ImageUrl;
				}
				string altStr = string.Empty;
				if ((descStr!=null) && (descStr.Length>0)) 
				{
					altStr = " title='" + descStr + "'";
				}
				targetStr = " target='" + targetStr + "'";
				listStr.Append("<td valign=center align=center><img src='" + auxImage + "' border=0></td>");
				if (isSeparator) 
				{
					listStr.Append("<td valign=center><a class='EnhancedTitle'>" + titleStr + "</a></td></tr><tr><td colspan=40><hr></td></tr><tr>");
				} 
				else 
				{
					listStr.Append("<td valign=center><a class='EnhancedLink' href='" + GetLinkUrl(null, linkStr, false) +"'" + targetStr + altStr + ">" + titleStr + "</a>");
					if ((doExpandAll) && (descStr!=null) && (descStr.Length>0)) 
					{
						listStr.Append("<br>" + "<a class='EnhancedDescription'>" + descStr + "</a>");
					}
				}
				if (!isSeparator) 
				{
					listStr.Append("</td>");
					if (actualColumn < maxColumns) 
					{
						listStr.Append("<td>&nbsp;</td>");
						actualColumn++;
					} 
					else 
					{
						listStr.Append("</tr>");
						actualColumn = 1;
					}
				}
				itemCount++;
			}
			dr.Close();

			listStr.Append("</tr>");
			listStr.Append("</table>\r\n");

			if (itemCount>0) 
			{
				results.Visible = true;
				results.Controls.Add(ParseControl(listStr.ToString()));
			}
		}
 
		private static bool IsNumeric(object value) 
		{ 
			try 
			{ 
				double d = System.Double.Parse(value.ToString(), System.Globalization.NumberStyles.Any); 
				return true; 
			} 
			catch (FormatException) 
			{ 
				return false; 
			} 
		}

		private ICollection mountDataList() 
		{
			EnhancedLinkDB enhancedLinks = new EnhancedLinkDB();
			SqlDataReader dr = enhancedLinks.GetEnhancedLinks(ModuleID, Version);
			DataTable dt = new DataTable();
			dt.Columns.Add(new DataColumn("TextField", typeof(string)));
			dt.Columns.Add(new DataColumn("ValueField", typeof(string)));
			if (IsEditable) 
			{
				dt.Rows.Add (CreateRow(Esperantus.Localize.GetString("ENHANCEDLINKS_EDITLINK", "Edit Link", null), string.Empty, dt));
			}
			else
			{
				dt.Rows.Add (CreateRow(ModuleTitle.Title, string.Empty, dt));
			}
			while (dr.Read()) 
			{
				string linkStr = (string)dr["Url"];
				if (!linkStr.Equals ("SEPARATOR"))
				{
					dt.Rows.Add(CreateRow((string)dr["Title"], dr["ItemID"].ToString(), dt));
				}
			}
			DataView dv = new DataView(dt);
			return dv;
		}

		private DataRow CreateRow(string Text, string Value, DataTable dt)
		{
			DataRow dr = dt.NewRow();
			dr[0] = Text;
			dr[1] = Value;
			return dr;
		}

		private string GetLinkUrl(object itemID, object url, bool editionMode)
		{
			if (editionMode) 
			{
				if (IsEditable)
				{
					return HttpUrlBuilder.BuildUrl("~/DesktopModules/EnhancedLinks/EnhancedLinksEdit.aspx","ItemID=" + itemID.ToString() + "&mID=" + ModuleID.ToString());
				}
				else
				{
					return string.Empty;
				}
			} 
			else 
			{
				string linkStr = url.ToString();
				if (linkStr.IndexOf("://")<0) 
				{
					if (IsNumeric(linkStr)) 
					{                    
						return HttpUrlBuilder.BuildUrl("~/DesktopDefault.aspx?tabid=" + linkStr);
					} 
					else 
					{
						if (linkStr.IndexOf("~/") < 0) 
						{
							linkStr = "~/" + linkStr;
						}
						return HttpUrlBuilder.BuildUrl(linkStr);
					}
				} 
				return linkStr;
			}
		}

		#region General Module Implementation

		public EnhancedLinks()
		{
			SupportsWorkflow = true;

			SettingItem IconPath = null;
			if (portalSettings != null)
			{
				IconPath = new SettingItem(new FolderDataType(HttpContext.Current.Server.MapPath(portalSettings.PortalFullPath ), "IconContainer"));
				IconPath.Value = "IconContainer";
				IconPath.Order = 5;
				IconPath.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
				IconPath.EnglishName = "Container for Icons";
				IconPath.Description = "Portal directory for upload used icons";
			}
			this._baseSettings.Add("ENHANCEDLINKS_ICONPATH", IconPath);

			ArrayList styleLink = new ArrayList();
			styleLink.Add(new SettingOption(1, Esperantus.Localize.GetString("ENHANCEDLINKS_DROPDOWNLIST", "DropDownList", null)));
			styleLink.Add(new SettingOption(2, Esperantus.Localize.GetString("ENHANCEDLINKS_LINKS", "Links", null)));

			SettingItem MaxColums = new SettingItem(new IntegerDataType());
			MaxColums.Value = "1";
			MaxColums.EnglishName = "Max Colums";
			MaxColums.Description = "Maximun number of colums";
			MaxColums.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			MaxColums.Order = 10;
			this._baseSettings.Add("ENHANCEDLINKS_MAXCOLUMS", MaxColums);

			SettingItem labelStyleLink = new SettingItem(new CustomListDataType(styleLink, "Name", "Val"));
			labelStyleLink.Description = "Select here how your module should look like";
			labelStyleLink.EnglishName = "Style Links";
			labelStyleLink.Value ="2";
			labelStyleLink.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			labelStyleLink.Order = 15;
			this._baseSettings.Add("ENHANCEDLINKS_SWITCHERTYPES", labelStyleLink);

			SettingItem ImageDefault = new SettingItem(new StringDataType());
			ImageDefault.Value = "navLink.gif";
			ImageDefault.EnglishName = "Default Image for link";
			ImageDefault.Description = "Select here a image for links with no special setting image";
			ImageDefault.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			ImageDefault.Order = 20;
			this._baseSettings.Add("ENHANCEDLINKS_DEFAULTIMAGE", ImageDefault);

			SettingItem ExpandAll = new SettingItem(new BooleanDataType());
			ExpandAll.Value = "false";
			ExpandAll.EnglishName = "Show Description";
			ExpandAll.Description = "Mark this if you like to see the description down the link";
			ExpandAll.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			ExpandAll.Order = 25;
			this._baseSettings.Add("ENHANCEDLINKS_EXPANDALL", ExpandAll);
		}

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
   
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{96BC3CE0-0409-4ab1-A0C2-67D6C4D68193}");
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
			Rainbow.Helpers.SearchDefinition s = new Rainbow.Helpers.SearchDefinition("rb_EnhancedLinks", "Title", "Description", "CreatedByUser", "CreatedDate", searchField);
			
			s.ArrSearchFields.Add("itm.Url");
			s.ArrSearchFields.Add("itm.ImageUrl");
			
			return s.SearchSqlSelect(portalID, userID, searchString, true);
		}
		#endregion

		#region Web Form Designer generated code
		/// <summary>
		/// Raises Init event
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
			this.AddUrl = "~/DesktopModules/EnhancedLinks/EnhancedLinksEdit.aspx";
			base.OnInit(e);
        }

        /// <summary>
		///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() 
		{
			this.cboLinks.SelectedIndexChanged += new System.EventHandler(this.cmdGo);
			this.Load += new System.EventHandler(this.Page_Load);
		}

		private void cmdGo (object sender, System.EventArgs e) 
		{
			string auxItem = cboLinks.SelectedItem.Value;
			cboLinks.SelectedIndex = 0;
			if (IsEditable) 
			{
				Response.Redirect(GetLinkUrl(auxItem, null, true), true);
			} 
			else 
			{
				string strURL = string.Empty;
				EnhancedLinkDB enhancedLinks = new EnhancedLinkDB();
				SqlDataReader dr = enhancedLinks.GetSingleEnhancedLink(Int32.Parse(auxItem), Version);
				if (dr.Read()) 
				{
					strURL = dr["Url"].ToString();
				}
				dr.Close();
				Response.Redirect(GetLinkUrl(null, strURL, false), true);
			}
		}
		
		#endregion

		# region Install / Uninstall Implementation
		public override void Install(System.Collections.IDictionary stateSaver)
		{
			string currentScriptName = Server.MapPath(this.TemplateSourceDirectory + "/Install.sql");
			ArrayList errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);
			if (errors.Count > 0)
			{
				// Call rollback
				throw new Exception("Error occurred:" + errors[0].ToString());
			}
		}

		public override void Uninstall(System.Collections.IDictionary stateSaver)
		{
			string currentScriptName = Server.MapPath(this.TemplateSourceDirectory + "/Uninstall.sql");
			ArrayList errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);
			if (errors.Count > 0)
			{
				// Call rollback
				throw new Exception("Error occurred:" + errors[0].ToString());
			}
		}
		#endregion

	}
}
