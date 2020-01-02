using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
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
	public class Documents : PortalModuleControl 
	{
		protected System.Web.UI.WebControls.DataGrid myDataGrid;
		protected System.Data.DataView myDataView;
		protected string sortField;
		protected string sortDirection;

		/// <summary>
		/// Constructor
		/// </summary>
		public Documents() 
		{
			// 17/12/2004 added localization for new settings by José Viladiu (jviladiu@portalServices.net)

			#region Document Setting Items

			SettingItem DocumentPath = new SettingItem(new PortalUrlDataType());
			DocumentPath.Required = true;
			DocumentPath.Value = "Documents";
			DocumentPath.Order = 1;
			DocumentPath.EnglishName = "Document path";
			DocumentPath.Description = "Folder for store the documents";
			this._baseSettings.Add("DocumentPath", DocumentPath);

			// Add new functionalities by jviladiu@portalServices.net (02/07/2004)
			SettingItem ShowImages = new SettingItem(new BooleanDataType());
			ShowImages.Value = "true";
			ShowImages.Order = 5;
			ShowImages.EnglishName = "Show Image Icons?";
			ShowImages.Description = "Mark this if you like see Image Icons";
			this._baseSettings.Add("DOCUMENTS_SHOWIMAGES", ShowImages);

			SettingItem SaveInDataBase = new SettingItem(new BooleanDataType());
			SaveInDataBase.Value = "false";
			SaveInDataBase.Order = 10;
			SaveInDataBase.EnglishName = "Save files in DataBase?";
			SaveInDataBase.Description = "Mark this if you like save files in DataBase";
			this._baseSettings.Add("DOCUMENTS_DBSAVE", SaveInDataBase);

			// Added sort by fields by Chris Thames [icecold_2@hotmail.com] (11/17/2004)
			SettingItem	SortByField	= new SettingItem(new ListDataType(Esperantus.Localize.GetString("DOCUMENTS_SORTBY_FIELD_LIST", "File Name;Created Date")));
			SortByField.Required=true;
			SortByField.Value =	"File Name";
			SortByField.Order = 11;
			SortByField.EnglishName = "Sort Field?";
			SortByField.Description = "Sort by File Name or by Created Date?";
			this._baseSettings.Add("DOCUMENTS_SORTBY_FIELD", SortByField);

			SettingItem SortByDirection = new SettingItem(new ListDataType(Esperantus.Localize.GetString("DOCUMENTS_SORTBY_DIRECTION_LIST", "Ascending;Descending")));
			SortByDirection.Value = "Ascending";
			SortByDirection.Order = 12;
			SortByDirection.EnglishName = "Sort ascending or descending?";
			SortByDirection.Description = "Ascending: A to Z or 0 - 9. Descending: Z - A or 9 - 0.";
			this._baseSettings.Add("DOCUMENTS_SORTBY_DIRECTION", SortByDirection);
			// End

			// Added by Jakob Hansen 07/07/2004
			SettingItem showTitle = new SettingItem(new BooleanDataType());
			showTitle.Value = "true";
			showTitle.Order = 15;
			showTitle.EnglishName = "Show Title column?";
			showTitle.Description = "Mark this if the title column should be displayed";
			this._baseSettings.Add("DOCUMENTS_SHOWTITLE", showTitle);

			SettingItem showOwner = new SettingItem(new BooleanDataType());
			showOwner.Value = "true";
			showOwner.Order = 16;
			showOwner.EnglishName = "Show Owner column?";
			showOwner.Description = "Mark this if the owner column should be displayed";
			this._baseSettings.Add("DOCUMENTS_SHOWOWNER", showOwner);

			SettingItem showArea = new SettingItem(new BooleanDataType());
			showArea.Value = "true";
			showArea.Order = 17;
			showArea.EnglishName = "Show Area column";
			showArea.Description = "Mark this if the area column should be displayed";
			this._baseSettings.Add("DOCUMENTS_SHOWAREA", showArea);

			SettingItem showLastUpdated = new SettingItem(new BooleanDataType());
			showLastUpdated.Value = "true";
			showLastUpdated.Order = 18;
			showLastUpdated.EnglishName = "Show Last Updated column";
			showLastUpdated.Description = "Mark this if the Last Updated column should be displayed";
			this._baseSettings.Add("DOCUMENTS_SHOWLASTUPDATED", showLastUpdated);
			// End Change Jakob Hansen

			#endregion

			// Change by Geert.Audenaert@Syntegra.Com
			// Date: 27/2/2003
			SupportsWorkflow = true;
			// End Change Geert.Audenaert@Syntegra.Com
		}

		/// <summary>
        /// The Page_Load event handler on this User Control is used to
        /// obtain a SqlDataReader of document information from the 
        /// Documents table, and then databind the results to a DataGrid
        /// server control.  It uses the Rainbow.DocumentDB()
        /// data component to encapsulate all data functionality.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e) 
		{
			if (!Page.IsPostBack) 
			{
				string sortFieldOption		= Settings["DOCUMENTS_SORTBY_FIELD"].ToString();
				string sortDirectionOption	= Settings["DOCUMENTS_SORTBY_DIRECTION"].ToString();
				if (sortFieldOption.Length > 0)
				{
					if (Esperantus.Localize.GetString("DOCUMENTS_SORTBY_FIELD_LIST", "File Name;Created Date").IndexOf(sortFieldOption) > 0)
						sortField = "CreatedDate";
					else
						sortField = "FileFriendlyName";

					if (Esperantus.Localize.GetString("DOCUMENTS_SORTBY_DIRECTION_LIST", "Ascending;Descending").IndexOf(sortDirectionOption) > 0)
						sortDirection = "DESC";
					else
						sortDirection = "ASC";
				}
				else
				{
					sortField = "FileFriendlyName";
					sortDirection = "ASC";
					if (sortField == "DueDate")
						sortDirection = "DESC";
				}
				ViewState["SortField"] = sortField;
				ViewState["SortDirection"] = sortDirection;
			}
			else
			{
				sortField = (string) ViewState["SortField"];
				sortDirection = (string) ViewState["sortDirection"];
			}

			myDataView = new DataView();

			// Obtain Document Data from Documents table
			// and bind to the datalist control
			DocumentDB documents = new DocumentDB();

			// DataSet documentsData = documents.GetDocuments(ModuleID, Version);
			// myDataView = documentsData.Tables[0].DefaultView;
			setDataView (documents.GetDocuments(ModuleID, Version));

			if (!Page.IsPostBack)
				myDataView.Sort = sortField + " " + sortDirection;

			BindGrid();
		}

		protected void BindGrid()
		{
			myDataGrid.DataSource = myDataView;

			myDataGrid.Columns[0].Visible = IsEditable;
			myDataGrid.Columns[1].Visible = bool.Parse(Settings["DOCUMENTS_SHOWIMAGES"].ToString());
			myDataGrid.Columns[2].Visible = bool.Parse(Settings["DOCUMENTS_SHOWTITLE"].ToString());
			myDataGrid.Columns[3].Visible = bool.Parse(Settings["DOCUMENTS_SHOWOWNER"].ToString());
			myDataGrid.Columns[4].Visible = bool.Parse(Settings["DOCUMENTS_SHOWAREA"].ToString());
			myDataGrid.Columns[5].Visible = bool.Parse(Settings["DOCUMENTS_SHOWLASTUPDATED"].ToString());

			myDataGrid.DataBind();
		}


		#region Image documents by jviladiu@portalservices.net (02/07/2004)

		private void setDataView (DataSet documentsData) 
		{
			myDataView = documentsData.Tables[0].DefaultView;
			foreach (DataRow dr in myDataView.Table.Rows) 
			{
				dr["contentType"] = imageAsign ((string) dr["contentType"]);
			}
		}

		private string imageAsign (string contentType) 
		{
			switch (contentType.ToLower())
			{
				case "pdf": return "pdf.gif";
				case "doc": 
				case "rtf": return "doc.gif";
				case "xls": return "xls.gif";
				case "zip":
				case "rar": return "zip.gif";
				case "txt": return "txt.gif";
				case "ppt": return "ppt.gif";
				case "mdb": return "mdb.gif";
				case "gif":
				case "jpg":
				case "bmp":
				case "jpeg":
				case "png": return "img.gif";
				case "mp3": return "mp3.gif";
				case "swf":
				case "fla": return "swf.gif";
				case "exe":
				case "com":
				case "dll":
				case "bat": return "exe.gif";
				case "c":
				case "h":
				case "bas":
				case "cs":
				case "vb": return "source.gif";
				case "css": return "css.gif";
				case "config":
				case "ini":
				case "sys": return "conf.gif";
				default: return "other.gif";
			}			
		}
		#endregion

		/// <summary>
        /// GetBrowsePath() is a helper method used to create the url   
        /// to the document.  If the size of the content stored in the   
        /// database is non-zero, it creates a path to browse that.   
        /// Otherwise, the FileNameUrl value is used.
        ///
        /// This method is used in the databinding expression for
        /// the browse Hyperlink within the DataGrid, and is called 
        /// for each row when DataGrid.DataBind() is called.  It is 
        /// defined as a helper method here (as opposed to inline 
        /// within the template) to improve code organization and
        /// avoid embedding logic within the content template.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="size"></param>
        /// <param name="documentID"></param>
        /// <returns></returns>
		protected string GetBrowsePath(string url, object size, int documentID) 
		{
			if (size != DBNull.Value && (int) size > 0) 
			{
				// if there is content in the database, create an url to browse it
				// Add ModuleID into url for correct security access. jviladiu@portalServices.net (02/07/2004)
				return (HttpUrlBuilder.BuildUrl("~/DesktopModules/Documents/DocumentsView.aspx", "ItemID=" + documentID.ToString() + "&MId=" + ModuleID.ToString() + "&wversion=" + Version.ToString()));
			}
			else 
			{
				// otherwise, return the FileNameUrl
				return url;
			}
		}

		#region General Implementation
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{F9645B82-CB45-4C4C-BB2D-72FA42FE2B75}");
			}
		}

		#region Search Implementation

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
			Rainbow.Helpers.SearchDefinition s = new Rainbow.Helpers.SearchDefinition("rb_Documents", "FileFriendlyName", "FileNameUrl", "CreatedByUser", "CreatedDate", searchField);
			
			//Add extra search fields here, this way
			s.ArrSearchFields.Add("itm.Category");

			return s.SearchSqlSelect(portalID, userID, searchString);
		}
		#endregion

		# region Install / Uninstall Implementation
		public override void Install(System.Collections.IDictionary stateSaver)
		{
			string currentScriptName = System.IO.Path.Combine(Server.MapPath(TemplateSourceDirectory), "install.sql");
			ArrayList errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);
			if (errors.Count > 0)
			{
				// Call rollback
				throw new Exception("Error occurred:" + errors[0].ToString());
			}
		}

		public override void Uninstall(System.Collections.IDictionary stateSaver)
		{
			string currentScriptName = System.IO.Path.Combine(Server.MapPath(TemplateSourceDirectory), "uninstall.sql");
			ArrayList errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);
			if (errors.Count > 0)
			{
				// Call rollback
				throw new Exception("Error occurred:" + errors[0].ToString());
			}
		}
		# endregion

		#endregion          

		#region Web Form Designer generated code
		/// <summary>
		/// Raises Init event
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();

			// Create a new Title the control
//			ModuleTitle = new DesktopModuleTitle();
			// Set here title properties
			// Add support for the edit page
			this.AddText = "ADD_DOCUMENT";
			this.AddUrl = "~/DesktopModules/Documents/DocumentsEdit.aspx";
			// Add title ad the very beginning of 
			// the control's controls collection
//			Controls.AddAt(0, ModuleTitle);
		
			base.OnInit(e);
		}

		private void InitializeComponent() 
		{
			this.myDataGrid.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.myDataGrid_SortCommand);
			this.myDataGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.myDataGrid_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void myDataGrid_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			if (sortField == e.SortExpression)
			{
				if (sortDirection == "ASC")
					sortDirection = "DESC";
				else
					sortDirection = "ASC";
			}

			ViewState["SortField"] = e.SortExpression;
			ViewState["sortDirection"] = sortDirection;

			myDataView.Sort = e.SortExpression + " " + sortDirection;
			BindGrid();
		}

		private void myDataGrid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			// 15/7/2004 added localization by Mario Endara mario@softworks.com.uy
			if (e.Item.Cells [3].Text == "unknown")
			{
				e.Item.Cells [3].Text = Esperantus.Localize.GetString ( "UNKNOWN", "unknown");
			}
		}
	}
}
