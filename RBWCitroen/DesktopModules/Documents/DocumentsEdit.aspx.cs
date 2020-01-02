using System;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Rainbow.UI.WebControls;
using Rainbow.Configuration;
using Esperantus;

namespace Rainbow.DesktopModules 
{
	/// <summary>
	/// Update and edit documents.
	/// Update 14 nov 2002 - Bug on buttonclick events
	/// </summary>
	[History("Jes1111", "2003/03/04", "Cache flushing now handled by inherited page")]
	[History("jviladiu@portalServices.net", "2004/07/02", "Corrections for save documents in database")]
	public class DocumentsEdit : Rainbow.UI.AddEditItemPage
	{
		protected Esperantus.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.TextBox NameField;
		protected System.Web.UI.WebControls.TextBox CategoryField;
		protected System.Web.UI.WebControls.TextBox PathField;
		protected System.Web.UI.HtmlControls.HtmlInputFile FileUpload;
		protected Esperantus.WebControls.Label PageTitleLabel;
		protected Esperantus.WebControls.Label FileNameLabel;
		protected Esperantus.WebControls.Label CategoryLabel;
		protected Esperantus.WebControls.Label UrlLabel;
		protected Esperantus.WebControls.Label OrLabel;
		protected Esperantus.WebControls.Label UploadLabel;
		protected Esperantus.WebControls.Literal CreatedLabel;
		protected Esperantus.WebControls.Literal OnLabel;
		protected System.Web.UI.WebControls.Label CreatedBy;
		protected System.Web.UI.WebControls.Label CreatedDate;
		protected Esperantus.WebControls.Label Message;

		string PathToSave;

		/// <summary>
		/// The Page_Load event on this Page is used to obtain the ModuleID
		/// and ItemID of the document to edit.
		/// It then uses the DocumentDB() data component
		/// to populate the page's edit controls with the document details.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e) 
		{
			// If the page is being requested the first time, determine if an
			// document itemID value is specified, and if so populate page
			// contents with the document details

			if (!Page.IsPostBack) 
			{
				if (ModuleID > 0)
					PathToSave = ((SettingItem) moduleSettings["DocumentPath"]).FullPath;

				if (ItemID > 0) 
				{
					// Obtain a single row of document information
					DocumentDB documents = new DocumentDB();
					SqlDataReader dr = documents.GetSingleDocument(ItemID, WorkFlowVersion.Staging);
                
					try
					{
						// Load first row into Datareader
						if(dr.Read())
						{
							NameField.Text = (string) dr["FileFriendlyName"];
							PathField.Text = (string) dr["FileNameUrl"];
							CategoryField.Text = (string) dr["Category"];
							CreatedBy.Text = (string) dr["CreatedByUser"];
							CreatedDate.Text = ((DateTime) dr["CreatedDate"]).ToShortDateString();
							// 15/7/2004 added localization by Mario Endara mario@softworks.com.uy
							if (CreatedBy.Text == "unknown")
							{
								CreatedBy.Text = Esperantus.Localize.GetString ( "UNKNOWN", "unknown");
							}
						}
					}
					finally
					{
						dr.Close();
					}
				}
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
				al.Add ("F9645B82-CB45-4C4C-BB2D-72FA42FE2B75");
				return al;
			}
		}

		/// <summary>
		/// The UpdateBtn_Click event handler on this Page is used to either
		/// create or update an document.  It  uses the DocumentDB()
		/// data component to encapsulate all data functionality.
		/// </summary>
		[History("jviladiu@portalServices.net", "2004/07/02", "Corrections for save documents in database")]
		override protected void OnUpdate(EventArgs e) 
		{
			base.OnUpdate(e);
			byte [] buffer = new byte[0];
			int size = 0;

			// Only Update if Input Data is Valid
			if (Page.IsValid) 
			{
				// Create an instance of the Document DB component
				DocumentDB documents = new DocumentDB();

				// Determine whether a file was uploaded
				if (FileUpload.PostedFile.FileName != string.Empty) 
				{
					FileInfo fInfo = new FileInfo(FileUpload.PostedFile.FileName);
					if (bool.Parse(moduleSettings["DOCUMENTS_DBSAVE"].ToString())) 
					{
						System.IO.Stream stream = FileUpload.PostedFile.InputStream;
						buffer  = new byte[FileUpload.PostedFile.ContentLength];
						size = FileUpload.PostedFile.ContentLength;
						try
						{
							stream.Read(buffer, 0, size);
							PathField.Text = fInfo.Name;
						}
						finally
						{
							stream.Close(); //by manu
						}
					} 
					else 
					{
						PathToSave = ((SettingItem) moduleSettings["DocumentPath"]).FullPath;
						// jviladiu@portalServices.net (02/07/2004). Create the Directory if not exists.
						if (!System.IO.Directory.Exists(Server.MapPath(PathToSave)))
							System.IO.Directory.CreateDirectory(Server.MapPath(PathToSave));
			
						string virtualPath = Rainbow.Settings.Path.WebPathCombine(PathToSave, fInfo.Name);
						string phyiscalPath = Server.MapPath(virtualPath);

						while(System.IO.File.Exists(phyiscalPath))
						{
							// Calculate virtualPath of the newly uploaded file
							virtualPath = Rainbow.Settings.Path.WebPathCombine(PathToSave, Guid.NewGuid().ToString() + fInfo.Extension);

							// Calculate physical path of the newly uploaded file
							phyiscalPath = Server.MapPath(virtualPath);
						}

						try
						{
							// Save file to uploads directory
							FileUpload.PostedFile.SaveAs(phyiscalPath);

							// Update PathFile with uploaded virtual file location
							PathField.Text = virtualPath;
						}
						catch(Exception ex)
						{
							Message.Text = Esperantus.Localize.GetString ("ERROR_FILE_NAME", "Invalid file name!<br>") +  ex.Message;
							return;                            
						}
					}
				}
				// Change for save contenType and document buffer
				// documents.UpdateDocument(ModuleID, ItemID, PortalSettings.CurrentUser.Identity.Email, NameField.Text, PathField.Text, CategoryField.Text, new byte[0], 0, string.Empty );
				string contentType = PathField.Text.Substring(PathField.Text.LastIndexOf(".") + 1).ToLower();
				documents.UpdateDocument(ModuleID, ItemID, PortalSettings.CurrentUser.Identity.Email, NameField.Text, PathField.Text, CategoryField.Text, buffer, size, contentType );

				this.RedirectBackToReferringPage();
			}
		}
    
		/// <summary>
		/// The DeleteBtn_Click event handler on this Page is used to delete an
		/// a document. It uses the Rainbow.DocumentsDB()
		/// data component to encapsulate all data functionality.
		/// </summary>
		override protected void OnDelete(EventArgs e) 
		{
			base.OnDelete(e);

			// Only attempt to delete the item if it is an existing item
			// (new items will have "ItemID" of 0)
			//TODO: Ask confim before delete
			if (ItemID != 0) 
			{
				DocumentDB documents = new DocumentDB();
				documents.DeleteDocument(ItemID, Server.MapPath(PathField.Text));
			}
			this.RedirectBackToReferringPage();
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Raises OnInitEvent
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			InitializeComponent();

			//Translate
			CreatedLabel.Text = Esperantus.Localize.GetString("CREATED_BY");
			OnLabel.Text = Esperantus.Localize.GetString("ON");

			PageTitleLabel.Text = Esperantus.Localize.GetString("DOCUMENT_DETAILS");
			FileNameLabel.Text = Esperantus.Localize.GetString("FILE_NAME");
			CategoryLabel.Text = Esperantus.Localize.GetString("CATEGORY");
			UrlLabel.Text = Esperantus.Localize.GetString("URL");
			UploadLabel.Text = Esperantus.Localize.GetString("UPLOAD_FILE");
			OrLabel.Text = "---" + Esperantus.Localize.GetString("OR") + "---";

			RequiredFieldValidator1.Text = Esperantus.Localize.GetString("VALID_FILE_NAME");
		
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() 
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

	}
}
