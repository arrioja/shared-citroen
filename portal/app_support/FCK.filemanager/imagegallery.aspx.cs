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
using Rainbow.Configuration;
using Rainbow.Security;

namespace Rainbow.DesktopModules.FCK.filemanager.browse
{
	/// <summary>
	/// Imagegallery.
	/// </summary>
	[History("jviladiu@portalservices.net", "2004/06/09", "First Implementation FCKEditor in Rainbow")]
	public class imagegallery : Rainbow.UI.EditItemPage
	{
		// Messages
		private string NoFileMessage = "No file selected";
		private string UploadSuccessMessage = "Uploaded Sucess";
		private string NoImagesMessage = "No Images";
		private string NoFolderSpecifiedMessage = "No folder";
		private string NoFileToDeleteMessage = "No file to delete";
		private string InvalidFileTypeMessage = "Invalid file type";
		private string[] AcceptedFileTypes = new string[] {"jpg","jpeg","jpe","gif","bmp","png"};

		// Configuration
		private bool UploadIsEnabled = true;
		private bool DeleteIsEnabled = true;
		protected System.Web.UI.WebControls.Label gallerymessage;
		protected System.Web.UI.WebControls.Panel GalleryPanel;
		protected System.Web.UI.WebControls.Button UploadImage;
		protected System.Web.UI.WebControls.Button DeleteImage;
		protected System.Web.UI.WebControls.RegularExpressionValidator FileValidator;
		protected System.Web.UI.WebControls.Literal ResultsMessage;
		protected System.Web.UI.WebControls.Panel UploadPanel;
		protected System.Web.UI.WebControls.Panel MainPage;
		protected System.Web.UI.HtmlControls.HtmlInputFile UploadFile;
		protected System.Web.UI.HtmlControls.HtmlInputHidden FileToDelete;
		protected System.Web.UI.HtmlControls.HtmlInputHidden RootImagesFolder;
		protected System.Web.UI.HtmlControls.HtmlInputHidden CurrentImagesFolder;
		protected System.Web.UI.WebControls.Panel iframePanel;

		protected override void LoadSettings()
		{
			if (PortalSecurity.HasEditPermissions(this.portalSettings.ActiveModule) == false)
				PortalSecurity.AccessDeniedEdit();
		}

		private void Page_Load(object sender, System.EventArgs e) 
		{
			string isframe = string.Empty + Request["frame"];
	
			if (isframe != string.Empty) 
			{
				MainPage.Visible = true;
				iframePanel.Visible = false;
	
				string rif = string.Empty + Request["rif"];
				string cif = string.Empty + Request["cif"];

				if (cif != string.Empty && rif != string.Empty)
				{
					RootImagesFolder.Value = rif;
					CurrentImagesFolder.Value = cif;
				} 
				else
				{
					Hashtable ms = ModuleSettings.GetModuleSettings(portalSettings.ActiveModule);
					string DefaultImageFolder = "default";
					if (ms["MODULE_IMAGE_FOLDER"] != null) 
					{
						DefaultImageFolder = ms["MODULE_IMAGE_FOLDER"].ToString();
					}
					else if (portalSettings.CustomSettings["SITESETTINGS_DEFAULT_IMAGE_FOLDER"] != null) 
					{
						DefaultImageFolder = portalSettings.CustomSettings["SITESETTINGS_DEFAULT_IMAGE_FOLDER"].ToString();
					}
					RootImagesFolder.Value = portalSettings.PortalPath + "/images/" + DefaultImageFolder;
					RootImagesFolder.Value = RootImagesFolder.Value.Replace("//", "/");
					CurrentImagesFolder.Value = RootImagesFolder.Value;	
				}

				UploadPanel.Visible = UploadIsEnabled;
				DeleteImage.Visible = DeleteIsEnabled;

				string FileErrorMessage = string.Empty;
				string ValidationString = ".*(";
				//[\.jpg]|[\.jpeg]|[\.jpe]|[\.gif]|[\.bmp]|[\.png])$"
				for (int i=0;i<AcceptedFileTypes.Length; i++) 
				{
					ValidationString += "[\\." + AcceptedFileTypes[i] + "]";
					if (i < (AcceptedFileTypes.Length-1)) ValidationString += "|";
					FileErrorMessage += AcceptedFileTypes[i];
					if (i < (AcceptedFileTypes.Length-1)) FileErrorMessage += ", ";
				}
				FileValidator.ValidationExpression = ValidationString+")$";
				FileValidator.ErrorMessage=FileErrorMessage;

				if (!IsPostBack) 
				{
					DisplayImages();
				}
			} 
			else 
			{
		
			}
		}

		public void UploadImage_OnClick(object sender, EventArgs e) 
		{	
			if (Page.IsValid) 
			{
				if (CurrentImagesFolder.Value != string.Empty) 
				{
					if (UploadFile.PostedFile.FileName.Trim() != string.Empty) 
					{
						if (IsValidFileType(UploadFile.PostedFile.FileName)) 
						{
							try 
							{
								string UploadFileName = string.Empty;
								string UploadFileDestination = string.Empty;
								UploadFileName = UploadFile.PostedFile.FileName;
								UploadFileName = UploadFileName.Substring(UploadFileName.LastIndexOf("\\")+1);
								UploadFileDestination = HttpContext.Current.Request.PhysicalApplicationPath;
								UploadFileDestination += CurrentImagesFolder.Value;
								UploadFileDestination += "\\";
								UploadFile.PostedFile.SaveAs(UploadFileDestination + UploadFileName);
								ResultsMessage.Text = UploadSuccessMessage;
							} 
							catch
							{
								//ResultsMessage.Text = "Your file could not be uploaded: " + ex.Message;
								ResultsMessage.Text = "There was an error.";
							}
						} 
						else 
						{
							ResultsMessage.Text = InvalidFileTypeMessage;
						}
					} 
					else 
					{
						ResultsMessage.Text = NoFileMessage;
					}
				} 
				else 
				{
					ResultsMessage.Text = NoFolderSpecifiedMessage;
				}
			} 
			else 
			{
				ResultsMessage.Text = InvalidFileTypeMessage;
		
			}
			DisplayImages();
		}

		public void DeleteImage_OnClick(object sender, EventArgs e) 
		{
			if (FileToDelete.Value != string.Empty && FileToDelete.Value != "undefined") 
			{
				try 
				{
					string AppPath = HttpContext.Current.Request.PhysicalApplicationPath;
					System.IO.File.Delete(AppPath  + CurrentImagesFolder.Value + "\\" + FileToDelete.Value);
					ResultsMessage.Text = "Deleted: " + FileToDelete.Value;
				} 
				catch 
				{			
					ResultsMessage.Text = "There was an error.";
				}
			} 
			else 
			{
				ResultsMessage.Text = NoFileToDeleteMessage;
			}
			DisplayImages();
		}

		private bool IsValidFileType(string FileName) 
		{
			string ext = FileName.Substring(FileName.LastIndexOf(".")+1,FileName.Length-FileName.LastIndexOf(".")-1);
			ext = ext.ToLower();
			for (int i=0; i<AcceptedFileTypes.Length; i++) 
			{		
				if (ext == AcceptedFileTypes[i]) 
				{
					return true;
				}	
			}
			return false;
		}

		private string[] ReturnFilesArray() 
		{
			if (CurrentImagesFolder.Value != string.Empty) 
			{
				try 
				{
					string AppPath = HttpContext.Current.Request.PhysicalApplicationPath;
					string ImageFolderPath = AppPath + CurrentImagesFolder.Value;
					string[] FilesArray = System.IO.Directory.GetFiles(ImageFolderPath,"*");
					return FilesArray;
			
			
				} 
				catch 
				{
		
					return null;
				}
			} 
			else 
			{
				return null;
			}

		}

		private string[] ReturnDirectoriesArray() 
		{
			if (CurrentImagesFolder.Value != string.Empty) 
			{
				try 
				{
					string AppPath = HttpContext.Current.Request.PhysicalApplicationPath;
					string CurrentFolderPath = AppPath + CurrentImagesFolder.Value;
					string[] DirectoriesArray = System.IO.Directory.GetDirectories(CurrentFolderPath,"*");
					return DirectoriesArray ;
				} 
				catch 
				{
					return null;
				}
			} 
			else 
			{
				return null;
			}
		}

		public void DisplayImages() 
		{
			string[] FilesArray = ReturnFilesArray();
			string[] DirectoriesArray = ReturnDirectoriesArray();
			string AppPath = HttpContext.Current.Request.PhysicalApplicationPath;
			string AppUrl;
	
			//Get the application's URL
			AppUrl = Request.ApplicationPath;
			if (!AppUrl.EndsWith("/")) AppUrl += "/";
			AppUrl = AppUrl.Replace("//", "/");
	
			GalleryPanel.Controls.Clear();
			if ( (FilesArray == null || FilesArray.Length == 0) && (DirectoriesArray == null || DirectoriesArray.Length == 0) ) 
			{
				gallerymessage.Text = NoImagesMessage + ": " + RootImagesFolder.Value;
			} 
			else 
			{
				string ImageFileName = string.Empty;
				string ImageFileLocation = string.Empty;

				int thumbWidth = 94;
				int thumbHeight = 94;
		
				if (CurrentImagesFolder.Value != RootImagesFolder.Value) 
				{
					System.Web.UI.HtmlControls.HtmlImage myHtmlImage = new System.Web.UI.HtmlControls.HtmlImage();
					myHtmlImage.Src = Rainbow.Settings.Path.ApplicationRoot + "/DesktopModules/FCK/filemanager/folder.gif";
					myHtmlImage.Attributes["unselectable"]="on"; 
					myHtmlImage.Attributes["align"]="absmiddle"; 
					myHtmlImage.Attributes["vspace"]="36"; 

					string ParentFolder = CurrentImagesFolder.Value.Substring(0,CurrentImagesFolder.Value.LastIndexOf("\\"));

					System.Web.UI.WebControls.Panel myImageHolder = new System.Web.UI.WebControls.Panel();					
					myImageHolder.CssClass = "imageholder";
					myImageHolder.Attributes["unselectable"]="on"; 
					myImageHolder.Attributes["onclick"]="divClick(this,'');";  
					myImageHolder.Attributes["ondblclick"]="gotoFolder('" + RootImagesFolder.Value + "','" + ParentFolder.Replace("\\","\\\\") + "');";  
					myImageHolder.Controls.Add(myHtmlImage);

					System.Web.UI.WebControls.Panel myMainHolder = new System.Web.UI.WebControls.Panel();
					myMainHolder.CssClass = "imagespacer";
					myMainHolder.Controls.Add(myImageHolder);

					System.Web.UI.WebControls.Panel myTitleHolder = new System.Web.UI.WebControls.Panel();
					myTitleHolder.CssClass = "titleHolder";
					myTitleHolder.Controls.Add(new LiteralControl("Up"));
					myMainHolder.Controls.Add(myTitleHolder);

					GalleryPanel.Controls.Add(myMainHolder);		
			
				}
		
				foreach (string _Directory in DirectoriesArray) 
				{
					try 
					{
						string DirectoryName = _Directory.ToString();

						System.Web.UI.HtmlControls.HtmlImage myHtmlImage = new System.Web.UI.HtmlControls.HtmlImage();
						myHtmlImage.Src = Rainbow.Settings.Path.ApplicationRoot + "/DesktopModules/FCK/filemanager/folder.gif";
						myHtmlImage.Attributes["unselectable"]="on"; 
						myHtmlImage.Attributes["align"]="absmiddle"; 
						myHtmlImage.Attributes["vspace"]="29"; 

						System.Web.UI.WebControls.Panel myImageHolder = new System.Web.UI.WebControls.Panel();					
						myImageHolder.CssClass = "imageholder";
						myImageHolder.Attributes["unselectable"]="on"; 
						myImageHolder.Attributes["onclick"]="divClick(this);";  
						myImageHolder.Attributes["ondblclick"]="gotoFolder('" + RootImagesFolder.Value + "','" + DirectoryName.Replace(AppPath,string.Empty).Replace("\\","\\\\") + "');";  
						myImageHolder.Controls.Add(myHtmlImage);

						System.Web.UI.WebControls.Panel myMainHolder = new System.Web.UI.WebControls.Panel();
						myMainHolder.CssClass = "imagespacer";
						myMainHolder.Controls.Add(myImageHolder);

						System.Web.UI.WebControls.Panel myTitleHolder = new System.Web.UI.WebControls.Panel();
						myTitleHolder.CssClass = "titleHolder";
						myTitleHolder.Controls.Add(new LiteralControl(DirectoryName.Replace(AppPath + CurrentImagesFolder.Value + "\\",string.Empty)));
						myMainHolder.Controls.Add(myTitleHolder);

						GalleryPanel.Controls.Add(myMainHolder);		
					} 
					catch 
					{
						// nothing for error
					}
				}
		
				foreach (string ImageFile in FilesArray) 
				{
					try 
					{
						ImageFileName = ImageFile.ToString();
						ImageFileName = ImageFileName.Substring(ImageFileName.LastIndexOf("\\")+1);
						ImageFileLocation = AppUrl;
//						ImageFileLocation = ImageFileLocation.Substring(ImageFileLocation.LastIndexOf("\\")+1);
						//galleryfilelocation += "/";
						ImageFileLocation += CurrentImagesFolder.Value;
						ImageFileLocation += "/";
						ImageFileLocation += ImageFileName;
						ImageFileLocation = ImageFileLocation.Replace("//", "/");
						System.Web.UI.HtmlControls.HtmlImage myHtmlImage = new System.Web.UI.HtmlControls.HtmlImage();
						myHtmlImage.Src = ImageFileLocation;
						System.Drawing.Image myImage = System.Drawing.Image.FromFile(ImageFile.ToString());
						myHtmlImage.Attributes["unselectable"]="on";  
						//myHtmlImage.border=0;

						// landscape image
						if (myImage.Width > myImage.Height) 
						{
							if (myImage.Width > thumbWidth) 
							{
								myHtmlImage.Width = thumbWidth;
								myHtmlImage.Height = Convert.ToInt32(myImage.Height * thumbWidth/myImage.Width);						
							} 
							else 
							{
								myHtmlImage.Width = myImage.Width;
								myHtmlImage.Height = myImage.Height;
							}
							// portrait image
						} 
						else 
						{
							if (myImage.Height > thumbHeight) 
							{
								myHtmlImage.Height = thumbHeight;
								myHtmlImage.Width = Convert.ToInt32(myImage.Width * thumbHeight/myImage.Height);
							} 
							else 
							{
								myHtmlImage.Width = myImage.Width;
								myHtmlImage.Height = myImage.Height;
							}
						}
				
						if (myHtmlImage.Height < thumbHeight) 
						{
							myHtmlImage.Attributes["vspace"] = Convert.ToInt32((thumbHeight/2)-(myHtmlImage.Height/2)).ToString(); 
						}


						System.Web.UI.WebControls.Panel myImageHolder = new System.Web.UI.WebControls.Panel();					
						myImageHolder.CssClass = "imageholder";
						myImageHolder.Attributes["onclick"]="divClick(this,'" + ImageFileName + "');";  
						myImageHolder.Attributes["ondblclick"]="returnImage('" + ImageFileLocation.Replace("\\","/") + "','" + myImage.Width.ToString() + "','" + myImage.Height.ToString() + "');";  
						myImageHolder.Controls.Add(myHtmlImage);


						System.Web.UI.WebControls.Panel myMainHolder = new System.Web.UI.WebControls.Panel();
						myMainHolder.CssClass = "imagespacer";
						myMainHolder.Controls.Add(myImageHolder);

						System.Web.UI.WebControls.Panel myTitleHolder = new System.Web.UI.WebControls.Panel();
						myTitleHolder.CssClass = "titleHolder";
						myTitleHolder.Controls.Add(new LiteralControl(ImageFileName + "<BR>" + myImage.Width.ToString() + "x" + myImage.Height.ToString()));
						myMainHolder.Controls.Add(myTitleHolder);

						//GalleryPanel.Controls.Add(myImage);
						GalleryPanel.Controls.Add(myMainHolder);
				
						myImage.Dispose();
					} 
					catch 
					{

					}
				}
				gallerymessage.Text = string.Empty;
			}
		}

		#region Código generado por el Diseñador de Web Forms
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
