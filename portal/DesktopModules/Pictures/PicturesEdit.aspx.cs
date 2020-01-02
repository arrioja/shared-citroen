using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using Rainbow.UI.WebControls;
using Rainbow.Configuration;
using Esperantus;
using Rainbow.Admin;
using Rainbow.DesktopModules;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// Rainbow Portal Pictures module - Edit page part
	/// (c)2002 by Ender Malkoc
	/// </summary>
	[History("Jes1111", "2003/03/04", "Cache flushing now handled by inherited page")]
	public class PicturesEdit : Rainbow.UI.AddEditItemPage
	{
		protected System.Web.UI.HtmlControls.HtmlInputFile flPicture;
		protected System.Web.UI.WebControls.TextBox BulkDir;
		protected System.Web.UI.WebControls.TextBox Caption;
		protected System.Web.UI.WebControls.TextBox ShortDescription;
		protected System.Web.UI.WebControls.TextBox LongDescription;
		protected System.Web.UI.WebControls.TextBox DisplayOrder;
		protected System.Web.UI.WebControls.TextBox Keywords;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidatorPicture;
		protected System.Web.UI.WebControls.Label PageTitleLabel;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidatorShortDescription;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidatorLongDescription;
		protected System.Web.UI.WebControls.Label Message;
		protected System.Web.UI.WebControls.DropDownList selFlip;
		protected System.Web.UI.WebControls.DropDownList selRotate;
		protected System.Web.UI.WebControls.CheckBox chkIncludeExif;
		protected Esperantus.WebControls.Literal Literal1;
		protected Esperantus.WebControls.Literal Literal2;
		protected Esperantus.WebControls.Literal Literal3;
		protected Esperantus.WebControls.Literal Literal4;
		protected Esperantus.WebControls.Literal Literal5;
		protected Esperantus.WebControls.Literal Literal6;
		protected Esperantus.WebControls.Literal Literal7;
		protected Esperantus.WebControls.Literal Literal8;
		protected Esperantus.WebControls.Literal Literal9;
		protected Esperantus.WebControls.Literal BulkDirLiteral;
		protected XmlDocument Metadata;

		protected string ThumbnailFilename
		{
			get { return (string)ViewState["ThumbnailFilename"]; }
			set { ViewState["ThumbnailFilename"] = value; }
		}

		protected string ModifiedFilename
		{
			get { return (string)ViewState["ModifiedFilename"]; }
			set { ViewState["ModifiedFilename"] = value; }
		}

		protected string MetadataXml
		{
			get { return (string)ViewState["MetadataXml"]; }
			set { ViewState["MetadataXml"] = value; }
		}

		/// <summary>
		/// The Page_Load event on this Page is used to obtain the ModuleID
		/// and ItemID of the picture to edit.
		///
		/// It then uses the Rainbow.PicturesDB() data component
		/// to populate the page's edit controls with the picture details.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		[History("Tim Capps","tim@cappsnet.com","2.4 beta","2004/02/18","added hide/show bulk load logic")]
		private void Page_Load(object sender, System.EventArgs e) 
		{
			// If the page is being requested the first time, determine if a
			// picture itemID value is specified, and if so populate page
			// contents with the picture details

			Metadata = new XmlDocument();

			if (Page.IsPostBack == false) 
			{
				BulkDir.Visible=false;				// make bulk load controls not visible for now
				BulkDirLiteral.Visible=false;

				if (ItemID != 0) 
				{
					// Obtain a single row of picture information
					PicturesDB pictures = new PicturesDB();
					SqlDataReader dr = pictures.GetSinglePicture(ItemID, WorkFlowVersion.Staging);
                
					try
					{
						// Read first row from database
						if(dr.Read())
						{
							ShortDescription.Text = (string) dr["ShortDescription"];
							MetadataXml = (string) dr["MetadataXml"];
							Metadata.LoadXml(MetadataXml);
							Keywords.Text = GetMetadata("Keywords");
							LongDescription.Text = GetMetadata("LongDescription");
							this.Caption.Text = GetMetadata("Caption");
							DisplayOrder.Text = ((int) dr["DisplayOrder"]).ToString();
							ThumbnailFilename = GetMetadata("ThumbnailFilename");
							ModifiedFilename = GetMetadata("ModifiedFilename");
						}
					}
					finally
					{
						// Close datareader
						dr.Close();
					}
				}
				else
				{
					Metadata.AppendChild(Metadata.CreateElement("Metadata"));
					MetadataXml = Metadata.OuterXml;
					if (bool.Parse((SettingItem) moduleSettings["AllowBulkLoad"])==true) 
					{
						// they are adding, and we are allowed to bulk load so
						// make the controls visible
						BulkDir.Visible=true;
						BulkDirLiteral.Visible=true;
					}
				}
			}
			else
			{
				Metadata.LoadXml(MetadataXml);
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
				al.Add ("B29CB86B-AEA1-4E94-8B77-B4E4239258B0");
				return al;
			}
		}

		/// <summary>
		/// The UpdateBtn_Click event handler on this Page is used to either
		/// create or update a picture.  It  uses the Rainbow.PicturesDB()
		/// data component to encapsulate all data functionality.
		/// </summary>  
		[History("Tim Capps","tim@cappsnet.com","2.4 beta","2004/02/18","added bulk load logic")]
		override protected void OnUpdate(EventArgs e) 
		{
			base.OnUpdate(e);

			// Only Update if Entered data is Valid
			if (Page.IsValid == true) 
			{
				// Create an instance of the PicturesDB component
				PicturesDB pictures = new PicturesDB();
				Bitmap fullPicture = null;

				//Get the resize option for the thumbnail
				Pictures.ResizeOption thumbnailResize = 
					moduleSettings["ThumbnailResize"].ToString() == string.Empty ? 
					Pictures.ResizeOption.FixedWidthHeight :
					(Pictures.ResizeOption)Int32.Parse((SettingItem) moduleSettings["ThumbnailResize"]);

				//Get the resize option for the original picture
				Pictures.ResizeOption originalResize = 
					moduleSettings["OriginalResize"].ToString() == string.Empty ?
					Pictures.ResizeOption.NoResize :
					(Pictures.ResizeOption)Int32.Parse((SettingItem) moduleSettings["OriginalResize"]);

				//Where are we going to save the picture?
				string PathToSave = Server.MapPath(((SettingItem) moduleSettings["AlbumPath"]).FullPath) + "\\";

				//Dimensions of the thumbnail as specified in settings
				int thumbnailWidth = Int32.Parse((SettingItem) moduleSettings["ThumbnailWidth"]);
				int thumbnailHeight = Int32.Parse((SettingItem) moduleSettings["ThumbnailHeight"]);

				//Dimensions of the original picture as specified in settings
				int originalWidth = Int32.Parse((SettingItem) moduleSettings["OriginalWidth"]);
				int originalHeight = Int32.Parse((SettingItem) moduleSettings["OriginalHeight"]);

				// See if they are doing a bulk load.  They must have specified
				// a bulk load directory (which is on the server) and they must not
				// have specified a specific upload file name, and they must not be
				// editing an item.
				if (ItemID==0 && flPicture.PostedFile.FileName == string.Empty &&
					BulkDir.Text!=string.Empty) 
				{
					// They are bulk loading - run through the specified directory and
					// insert all .JPG files into the database.  User is expected to
					// provide a FULL file path on the SERVER.
					string [] files = System.IO.Directory.GetFiles(BulkDir.Text, "*.jpg");

					foreach (string file in files) 
					{
						MetadataXml=null;

						// Create a new FileInfo object for this filename
						System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);

						//Create new filenames for the thumbnail and the original picture
						ModifiedFilename = ModuleID.ToString() + "m" + System.Guid.NewGuid().ToString() + ".jpg";
						SetMetadata("ModifiedFilename", ModifiedFilename);

						ThumbnailFilename = ModuleID.ToString() + "m" + System.Guid.NewGuid().ToString() + ".jpg";
						SetMetadata("ThumbnailFilename", ThumbnailFilename);

						//Full path of the original picture
						string physicalPath = PathToSave + ModifiedFilename;

						//Make sure the picture target directory exists
						System.IO.Directory.CreateDirectory(PathToSave);

						SetMetadata("OriginalFilename", fileInfo.FullName);
					
						try
						{
							//Create a bitmap from the saved picture
							fullPicture = new Bitmap(fileInfo.FullName);
						}
						catch(Exception ex)
						{
							Message.Text = Esperantus.Localize.GetString("PICTURES_INVALID_IMAGE_FILE", "Invalid Image File", this) + "!<br>" +  ex.Message;
							return;                            
						}

						SetMetadata("OriginalWidth", fullPicture.Width.ToString());
						SetMetadata("OriginalHeight", fullPicture.Height.ToString());

						if(chkIncludeExif.Checked) SetExifInformation(fullPicture);

						RotateFlip(fullPicture, selFlip.SelectedItem.Value, selRotate.SelectedItem.Value);

						try
						{
							//Resize the original picture with the given settings to create the thumbnail
							Bitmap thumbnail = ResizeImage(fullPicture, thumbnailWidth, thumbnailHeight, thumbnailResize);
							thumbnail.Save(PathToSave + ThumbnailFilename, ImageFormat.Jpeg);
							SetMetadata("ThumbnailWidth", thumbnail.Width.ToString());
							SetMetadata("ThumbnailHeight", thumbnail.Height.ToString());
							thumbnail.Dispose();
						}
						catch(Exception ex)
						{
							Message.Text = Esperantus.Localize.GetString("PICTURES_THUMBNAIL_ERROR", "Error occured while creating the thumbnail", this) + "!<br>" +  ex.Message;
							return;                            
						}

						Bitmap modified = null;

						try
						{
							//Resize original image
							modified = ResizeImage(fullPicture, originalWidth, originalHeight, originalResize);
						}
						catch(Exception ex)
						{
							Message.Text = Esperantus.Localize.GetString("PICTURES_RESIZE_ERROR", "Error occured while resizing the image", this) + "!<br>" +  ex.Message;
							return;                            
						}

						SetMetadata("ModifiedWidth", modified.Width.ToString());
						SetMetadata("ModifiedHeight", modified.Height.ToString());

						fullPicture.Dispose();

						//Save the resized one
						modified.Save(physicalPath, ImageFormat.Jpeg);
						modified.Dispose();

						FileInfo fi = new FileInfo(physicalPath);
						SetMetadata("ModifiedFileSize", fi.Length.ToString());

						int displayOrder = 0;

						try
						{
							displayOrder = Int32.Parse(DisplayOrder.Text);
						}
						catch { }

						SetMetadata("ShortDescription", ShortDescription.Text);
						SetMetadata("LongDescription", LongDescription.Text);
						SetMetadata("Caption", Caption.Text);
						SetMetadata("Keywords", Keywords.Text);
						SetMetadata("UploadDate", DateTime.Now.ToString());
						SetMetadata("CreatedBy",PortalSettings.CurrentUser.Identity.Email);
						SetMetadata("DisplayOrder", displayOrder.ToString());

						//Add new picture to the database
						ItemID = pictures.AddPicture(ModuleID, ItemID, displayOrder, MetadataXml, ShortDescription.Text, Keywords.Text, PortalSettings.CurrentUser.Identity.Email, DateTime.Now);
					}
				} 
				else 
				{
					// Determine whether a file was uploaded
					if (flPicture.PostedFile.FileName != string.Empty) 
					{
						//Create new filenames for the thumbnail and the original picture
						ModifiedFilename = ModuleID.ToString() + "m" + System.Guid.NewGuid().ToString() + ".jpg";
						SetMetadata("ModifiedFilename", ModifiedFilename);

						ThumbnailFilename = ModuleID.ToString() + "m" + System.Guid.NewGuid().ToString() + ".jpg";
						SetMetadata("ThumbnailFilename", ThumbnailFilename);

						//Full path of the original picture
						string physicalPath = PathToSave + ModifiedFilename;

						try
						{
							// Save the picture
							flPicture.PostedFile.SaveAs(physicalPath);
						}
						catch(System.IO.DirectoryNotFoundException ex)
						{
							// If the directory is not found, create and then save
							System.IO.Directory.CreateDirectory(PathToSave);
							flPicture.PostedFile.SaveAs(physicalPath);

							//This line is here to supress the warning
							ex.ToString();
						}
						catch(Exception ex)
						{
							// If other error occured report to the user
							Message.Text = Esperantus.Localize.GetString("PICTURES_INVALID_FILENAME", "Invalid Filename", this) + "!<br>" +  ex.Message;
							return;                            
						}

						SetMetadata("OriginalFilename", flPicture.PostedFile.FileName);
					
						try
						{
							//Create a bitmap from the saved picture
							fullPicture = new Bitmap(physicalPath);
						}
						catch(Exception ex)
						{
							Message.Text = Esperantus.Localize.GetString("PICTURES_INVALID_IMAGE_FILE", "Invalid Image File", this) + "!<br>" +  ex.Message;
							return;                            
						}

						SetMetadata("OriginalWidth", fullPicture.Width.ToString());
						SetMetadata("OriginalHeight", fullPicture.Height.ToString());

						if(chkIncludeExif.Checked) SetExifInformation(fullPicture);

						RotateFlip(fullPicture, selFlip.SelectedItem.Value, selRotate.SelectedItem.Value);

						try
						{
							//Resize the original picture with the given settings to create the thumbnail
							Bitmap thumbnail = ResizeImage(fullPicture, thumbnailWidth, thumbnailHeight, thumbnailResize);
							thumbnail.Save(PathToSave + ThumbnailFilename, ImageFormat.Jpeg);
							SetMetadata("ThumbnailWidth", thumbnail.Width.ToString());
							SetMetadata("ThumbnailHeight", thumbnail.Height.ToString());
							thumbnail.Dispose();
						}
						catch(Exception ex)
						{
							Message.Text = Esperantus.Localize.GetString("PICTURES_THUMBNAIL_ERROR", "Error occured while creating the thumbnail", this) + "!<br>" +  ex.Message;
							return;                            
						}

						Bitmap modified = null;

						try
						{
							//Resize original image
							modified = ResizeImage(fullPicture, originalWidth, originalHeight, originalResize);
						}
						catch(Exception ex)
						{
							Message.Text = Esperantus.Localize.GetString("PICTURES_RESIZE_ERROR", "Error occured while resizing the image", this) + "!<br>" +  ex.Message;
							return;                            
						}

						SetMetadata("ModifiedWidth", modified.Width.ToString());
						SetMetadata("ModifiedHeight", modified.Height.ToString());

						fullPicture.Dispose();

						//Delete the original
						System.IO.File.Delete(physicalPath);

						//Save the resized one
						modified.Save(physicalPath, ImageFormat.Jpeg);
						modified.Dispose();

						FileInfo fi = new FileInfo(physicalPath);
						SetMetadata("ModifiedFileSize", fi.Length.ToString());

					}
					else if(ItemID == 0)
					{
						Message.Text = Esperantus.Localize.GetString("PICTURES_SPECIFY_FILENAME", "Please specify a filename", this)  + "!<br>";
						return;                            					
					}

					int displayOrder = 0;

					try
					{
						displayOrder = Int32.Parse(DisplayOrder.Text);
					}
					catch { }

					SetMetadata("ShortDescription", ShortDescription.Text);
					SetMetadata("LongDescription", LongDescription.Text);
					SetMetadata("Caption", Caption.Text);
					SetMetadata("Keywords", Keywords.Text);
					SetMetadata("UploadDate", DateTime.Now.ToString());
					SetMetadata("CreatedBy",PortalSettings.CurrentUser.Identity.Email);
					SetMetadata("DisplayOrder", displayOrder.ToString());

					if (ItemID == 0) 
					{
						//If this is a new picture add it to the database
						ItemID = pictures.AddPicture(ModuleID, ItemID, displayOrder, MetadataXml, ShortDescription.Text, Keywords.Text, PortalSettings.CurrentUser.Identity.Email, DateTime.Now);
					}
					else
					{
						//Update the existing one
						pictures.UpdatePicture(ModuleID, ItemID, displayOrder, MetadataXml, ShortDescription.Text, Keywords.Text, PortalSettings.CurrentUser.Identity.Email, DateTime.Now);
					}
				}
				// Redirect back to the portal home page
				this.RedirectBackToReferringPage();
			}
		}

		/// <summary>
		/// The DeleteBtn_Click event handler on this Page is used to delete
		/// a picture.  It  uses the Rainbow.PicturesDB()
		/// data component to encapsulate all data functionality.
		/// </summary>
		override protected void OnDelete(EventArgs e)  
		{
			base.OnDelete(e);

			// Only attempt to delete the item if it is an existing item
			// (new items will have "ItemID" of 0)

			if (ItemID != 0) 
			{
				PicturesDB pictures = new PicturesDB();
				string PathToDelete = Server.MapPath(((SettingItem) moduleSettings["AlbumPath"]).FullPath) + "\\";

				SqlDataReader dr = pictures.GetSinglePicture(ItemID, WorkFlowVersion.Staging);
                
				string filename = string.Empty;
				string thumbnailFilename = string.Empty;
				try
				{
					// Read first row from database
					if (dr.Read())
					{
						Metadata.LoadXml((string)dr["MetadataXml"]);

						filename = GetMetadata("ModifiedFilename");
						thumbnailFilename = GetMetadata("ThumbnailFilename");
					}
				}
				finally
				{
					// Close datareader
					dr.Close();
				}

				try
				{
					//Delete the files
					System.IO.File.Delete(PathToDelete + filename);
					System.IO.File.Delete(PathToDelete + thumbnailFilename);
				}
				catch
				{
					// We don't really have much to do at this point
				}

				//Delete the record from database.
				pictures.DeletePicture(ItemID);
				
			}

			// Redirect back to the portal home page
			this.RedirectBackToReferringPage();
		}

		public PicturesEdit() 
		{
			Page.Init += new System.EventHandler(Page_Init);
		}

		public string GetMetadata(string key)
		{
			XmlNode targetNode = Metadata.SelectSingleNode("/Metadata/@" + key);
			if (targetNode == null)
			{
				return null;
			}
			else
			{
				return targetNode.Value;
			}
		}

		public void SetMetadata(string key, string data)
		{
			XmlNode targetNode = this.Metadata.SelectSingleNode("/Metadata/@" + key);
			if (targetNode == null)
			{
				XmlAttribute newAttribute = Metadata.CreateAttribute(key);
				newAttribute.Value = data;
				Metadata.DocumentElement.Attributes.Append(newAttribute);
			}
			else
			{
				targetNode.Value = data;
			}

			MetadataXml = Metadata.OuterXml;
		}

		private string ConvertByteArrayToString(byte[] array)
		{
			StringBuilder sb = new StringBuilder();
			for(int i = 0; i < array.Length - 1; i++) sb.Append((char)array[i]);
			return sb.ToString();
		}

		private byte ConvertByteArrayToByte(byte[] array)
		{
			return array[0];
		}

		private short ConvertByteArrayToShort(byte[] array)
		{
			short val = 0;
			for(int i = 0; i < array.Length; i++) val += (short)(array[i] * System.Math.Pow(2,(i * 8))  );
			return val;
		}

		private long ConvertByteArrayToLong(byte[] array)
		{
			long val = 0;
			for(int i = 0; i < array.Length; i++) val += (array[i] * (long)System.Math.Pow(2,(i * 8))  );
			return val;
		}

		private string ConvertByteArrayToRational(byte[] array)
		{
			int val1 = 0;
			int val2 = 0;

			for(int i = 0; i < 4; i++) val1 += (array[i] * (int)System.Math.Pow(2,(i * 8))  );
			for(int i = 4; i < 8; i++) val2 += (array[i] * (int)System.Math.Pow(2,((i-4) * 8))  );
			if(val2 == 1)
			{
				return val1.ToString();
			}
			else
			{
				return ((double)val1 / (double)val2).ToString();
			}
		}

		private string ConvertByteArrayToSRational(byte[] array)
		{
			int val1 = 0;
			int val2 = 0;

			for(int i = 0; i < 4; i++) val1 += (array[i] * (int)System.Math.Pow(2,(i * 8))  );
			for(int i = 4; i < 8; i++) val2 += (array[i] * (int)System.Math.Pow(2,((i-4) * 8))  );
			if(val2 == 1)
			{
				return val1.ToString();
			}
			else
			{
				return ((double)val1 / (double)val2).ToString();
			}
		}

		private Bitmap RotateFlip(Bitmap original, string flip, string rotate)
		{
			RotateFlipType rotateFlipType = RotateFlipType.RotateNoneFlipNone;

			if(flip == "None" && rotate == "180")
			{
				rotateFlipType = RotateFlipType.Rotate180FlipNone;
			}
			else if(flip == "X" && rotate == "180")
			{
				rotateFlipType = RotateFlipType.Rotate180FlipX;
			}
			else if(flip == "XY" && rotate == "180")
			{
				rotateFlipType = RotateFlipType.Rotate180FlipXY;
			}
			else if(flip == "Y" && rotate == "180")
			{
				rotateFlipType = RotateFlipType.Rotate180FlipY;
			}
			else if(flip == "None" && rotate == "270")
			{
				rotateFlipType = RotateFlipType.Rotate270FlipNone;
			}
			else if(flip == "X" && rotate == "270")
			{
				rotateFlipType = RotateFlipType.Rotate270FlipX;
			}
			else if(flip == "XY" && rotate == "270")
			{
				rotateFlipType = RotateFlipType.Rotate270FlipXY;
			}
			else if(flip == "Y" && rotate == "270")
			{
				rotateFlipType = RotateFlipType.Rotate270FlipY;
			}
			else if(flip == "None" && rotate == "90")
			{
				rotateFlipType = RotateFlipType.Rotate90FlipNone;
			}
			else if(flip == "X" && rotate == "90")
			{
				rotateFlipType = RotateFlipType.Rotate90FlipX;
			}
			else if(flip == "XY" && rotate == "90")
			{
				rotateFlipType = RotateFlipType.Rotate90FlipXY;
			}
			else if(flip == "Y" && rotate == "90")
			{
				rotateFlipType = RotateFlipType.Rotate90FlipY;
			}
			else if(flip == "None" && rotate == "None")
			{
				rotateFlipType = RotateFlipType.RotateNoneFlipNone;
			}
			else if(flip == "X" && rotate == "None")
			{
				rotateFlipType = RotateFlipType.RotateNoneFlipX;
			}
			else if(flip == "XY" && rotate == "None")
			{
				rotateFlipType = RotateFlipType.RotateNoneFlipXY;
			}
			else if(flip == "Y" && rotate == "None")
			{
				rotateFlipType = RotateFlipType.RotateNoneFlipY;
			}
			
			original.RotateFlip(rotateFlipType);
			return original;
		}

		/// <summary>
		/// Resize a given image
		/// </summary>
		/// <param name="original">Original Bitmap that needs resizing</param>
		/// <param name="newWidth">New width of the bitmap</param>
		/// <param name="newHeight">New height of the bitmap</param>
		/// <param name="option">Option for resizing</param>
		/// <returns></returns>
		private Bitmap ResizeImage(Bitmap original, int newWidth, int newHeight, Pictures.ResizeOption option)
		{
			if (original.Width == 0 || original.Height == 0 || newWidth == 0 || newHeight == 0 ) return null;
			if (original.Width < newWidth) newWidth = original.Width;
			if (original.Height < newHeight) newHeight = original.Height;

			switch(option)
			{
				case Pictures.ResizeOption.NoResize:
					newWidth = original.Width;
					newHeight = original.Height;
					break;
				case Pictures.ResizeOption.FixedWidthHeight:
					break;
				case Pictures.ResizeOption.MaintainAspectWidth:
					newHeight = (newWidth * original.Height / original.Width);
					break;
				case Pictures.ResizeOption.MaintainAspectHeight:
					newWidth = (newHeight * original.Width / original.Height);
					break;
			}
			Bitmap newBitmap = new Bitmap(newWidth, newHeight);
			Graphics g = Graphics.FromImage(newBitmap);
			g.DrawImage(original, 0, 0, newWidth, newHeight);
			return newBitmap;
		}

		private void Page_Init(object sender, EventArgs e) 
		{
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			InitializeComponent();
		}

		public bool ThumbnailCallback()
		{
			return false;
		}

		private void SetExifInformation(Bitmap bitmap)
		{
			foreach(PropertyItem pi in bitmap.PropertyItems)
			{
				switch(pi.Id)
				{
					case 0x010F:
						SetMetadata("EquipMake", ConvertByteArrayToString(pi.Value));
						break;
					case 0x0110:
						SetMetadata("EquipModel", ConvertByteArrayToString(pi.Value));
						break;
					case 0x0112:
					switch(ConvertByteArrayToShort(pi.Value))
					{
						case 1:
							SetMetadata("Orientation", "upper left");
							break;
						case 2:
							SetMetadata("Orientation", "upper right");
							break;
						case 3:
							SetMetadata("Orientation", "lower right");
							break;
						case 4:
							SetMetadata("Orientation", "lower left");
							break;
						case 5:
							SetMetadata("Orientation", "upper left flipped");
							break;
						case 6:
							SetMetadata("Orientation", "upper right flipped");
							break;
						case 7:
							SetMetadata("Orientation", "lower right flipped");
							break;
						case 8:
							SetMetadata("Orientation", "lower left flipped");
							break;
					}
						break;
					case 0x011a:
						SetMetadata("XResolution", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x011b:
						SetMetadata("YResolution", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x0128:
						SetMetadata("ResolutionUnit", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0132:
						SetMetadata("Datetime", ConvertByteArrayToString(pi.Value));
						break;
					case 0x0213:
						SetMetadata("YCbCrPositioning", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x00FE:
						SetMetadata("NewSubfileType", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x00FF:
						SetMetadata("SubfileType", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0100:
						SetMetadata("ImageWidth", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x0101:
						SetMetadata("ImageHeight", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x0102:
						SetMetadata("BitsPerSample", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0103:
						SetMetadata("Compression", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0106:
						SetMetadata("PhotometricInterp", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0107:
						SetMetadata("ThreshHolding", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0108:
						SetMetadata("CellWidth", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0109:
						SetMetadata("CellHeight", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x010A:
						SetMetadata("FillOrder", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x010D:
						SetMetadata("DocumentName", ConvertByteArrayToString(pi.Value));
						break;
					case 0x010E:
						SetMetadata("ImageDescription", ConvertByteArrayToString(pi.Value));
						break;
					case 0x0111:
						SetMetadata("StripOffsets", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x0115:
						SetMetadata("SamplesPerPixel", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0116:
						SetMetadata("RowsPerStrip", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x0117:
						SetMetadata("StripBytesCount", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x0118:
						SetMetadata("MinSampleValue", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0119:
						SetMetadata("MaxSampleValue", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x011C:
						SetMetadata("PlanarConfig", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x011D:
						SetMetadata("PageName", ConvertByteArrayToString(pi.Value));
						break;
					case 0x011E:
						SetMetadata("XPosition", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x011F:
						SetMetadata("YPosition", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x0120:
						SetMetadata("FreeOffset", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x0121:
						SetMetadata("FreeByteCounts", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x0122:
						SetMetadata("GrayResponseUnit", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0123:
						SetMetadata("GrayResponseCurve", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0124:
						SetMetadata("T4Option", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x0125:
						SetMetadata("T6Option", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x0129:
						SetMetadata("PageNumber", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x012D:
						SetMetadata("TransferFunction", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0131:
						SetMetadata("SoftwareUsed", ConvertByteArrayToString(pi.Value));
						break;
					case 0x013B:
						SetMetadata("Artist", ConvertByteArrayToString(pi.Value));
						break;
					case 0x013C:
						SetMetadata("HostComputer", ConvertByteArrayToString(pi.Value));
						break;
					case 0x013D:
						SetMetadata("Predictor", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x013E:
						SetMetadata("WhitePoint", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x013F:
						SetMetadata("PrimaryChromaticities", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x0140:
						SetMetadata("ColorMap", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0141:
						SetMetadata("HalftoneHints", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0142:
						SetMetadata("TileWidth", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x0143:
						SetMetadata("TileLength", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x0144:
						SetMetadata("TileOffset", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x0145:
						SetMetadata("TileByteCounts", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x014C:
						SetMetadata("InkSet", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x014D:
						SetMetadata("InkNames", ConvertByteArrayToString(pi.Value));
						break;
					case 0x014E:
						SetMetadata("NumberOfInks", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0150:
						SetMetadata("DotRange", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0151:
						SetMetadata("TargetPrinter", ConvertByteArrayToString(pi.Value));
						break;
					case 0x0152:
						SetMetadata("ExtraSamples", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0153:
						SetMetadata("SampleFormat", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0156:
						SetMetadata("TransferRange", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0200:
						SetMetadata("JPEGProc", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0201:
						SetMetadata("JPEGInterFormat", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x0202:
						SetMetadata("JPEGInterLength", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x0203:
						SetMetadata("JPEGRestartInterval", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0205:
						SetMetadata("JPEGLosslessPredictors", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0206:
						SetMetadata("JPEGPointTransforms", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0207:
						SetMetadata("JPEGQTables", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x0208:
						SetMetadata("JPEGDCTables", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x0209:
						SetMetadata("JPEGACTables", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x0211:
						SetMetadata("YCbCrCoefficients", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x0212:
						SetMetadata("YCbCrSubsampling", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x0214:
						SetMetadata("REFBlackWhite", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x0301:
						SetMetadata("Gamma", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x0302:
						SetMetadata("ICCProfileDescriptor", ConvertByteArrayToString(pi.Value));
						break;
					case 0x0303:
					switch(ConvertByteArrayToShort(pi.Value))
					{
						case 0:
							SetMetadata("SRGBRenderingIntent", "perceptual");
							break;
						case 1:
							SetMetadata("SRGBRenderingIntent", "relative colorimetric");
							break;
						case 2:
							SetMetadata("SRGBRenderingIntent", "saturation");
							break;
						case 3:
							SetMetadata("SRGBRenderingIntent", "absolute colorimetric");
							break;
					}
						break;
					case 0x0320:
						SetMetadata("ImageTitle", ConvertByteArrayToString(pi.Value));
						break;
					case 0x5001:
						SetMetadata("ResolutionXUnit", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x5002:
						SetMetadata("ResolutionYUnit", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x5003:
						SetMetadata("ResolutionXLengthUnit", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x5004:
						SetMetadata("ResolutionYLengthUnit", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x5005:
						SetMetadata("PrintFlags", ConvertByteArrayToString(pi.Value));
						break;
					case 0x5006:
						SetMetadata("PrintFlagsVersion", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x5007:
						SetMetadata("PrintFlagsCrop", ConvertByteArrayToByte(pi.Value).ToString());
						break;
					case 0x5008:
						SetMetadata("PrintFlagsBleedWidth", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x5009:
						SetMetadata("PrintFlagsBleedWidthScale", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x500A:
						SetMetadata("HalftoneLPI", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x500B:
						SetMetadata("HalftoneLPIUnit", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x500C:
						SetMetadata("HalftoneDegree", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x500D:
						SetMetadata("HalftoneShape", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x500E:
						SetMetadata("HalftoneMisc", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x500F:
						SetMetadata("HalftoneScreen", ConvertByteArrayToByte(pi.Value).ToString());
						break;
					case 0x5010:
						SetMetadata("JPEGQuality", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x5012:
						SetMetadata("ThumbnailFormat", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x5013:
						SetMetadata("ThumbnailWidth", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x5014:
						SetMetadata("ThumbnailHeight", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x5015:
						SetMetadata("ThumbnailColorDepth", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x5016:
						SetMetadata("ThumbnailPlanes", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x5017:
						SetMetadata("ThumbnailRawBytes", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x5018:
						SetMetadata("ThumbnailSize", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x5019:
						SetMetadata("ThumbnailCompressedSize", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x501B:
						SetMetadata("ThumbnailData", ConvertByteArrayToByte(pi.Value).ToString());
						break;
					case 0x5020:
						SetMetadata("ThumbnailImageWidth", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x5021:
						SetMetadata("ThumbnailImageHeight", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x5022:
						SetMetadata("ThumbnailBitsPerSample", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x5023:
						SetMetadata("ThumbnailCompression", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x5024:
						SetMetadata("ThumbnailPhotometricInterp", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x5025:
						SetMetadata("ThumbnailImageDescription", ConvertByteArrayToString(pi.Value));
						break;
					case 0x5026:
						SetMetadata("ThumbnailEquipMake", ConvertByteArrayToString(pi.Value));
						break;
					case 0x5027:
						SetMetadata("ThumbnailEquipModel", ConvertByteArrayToString(pi.Value));
						break;
					case 0x5028:
						SetMetadata("ThumbnailStripOffsets", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x5029:
						SetMetadata("ThumbnailOrientation", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x502A:
						SetMetadata("ThumbnailSamplesPerPixel", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x502B:
						SetMetadata("ThumbnailRowsPerStrip", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x502C:
						SetMetadata("ThumbnailStripBytesCount", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x502F:
						SetMetadata("ThumbnailPlanarConfig", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x5030:
						SetMetadata("ThumbnailResolutionUnit", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x5031:
						SetMetadata("ThumbnailTransferFunction", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x5032:
						SetMetadata("ThumbnailSoftwareUsed", ConvertByteArrayToString(pi.Value));
						break;
					case 0x5033:
						SetMetadata("ThumbnailDateTime", ConvertByteArrayToString(pi.Value));
						break;
					case 0x5034:
						SetMetadata("ThumbnailArtist", ConvertByteArrayToString(pi.Value));
						break;
					case 0x5035:
						SetMetadata("ThumbnailWhitePoint", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x5036:
						SetMetadata("ThumbnailPrimaryChromaticities", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x5037:
						SetMetadata("ThumbnailYCbCrCoefficients", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x5038:
						SetMetadata("ThumbnailYCbCrSubsampling", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x5039:
						SetMetadata("ThumbnailYCbCrPositioning", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x503A:
						SetMetadata("ThumbnailRefBlackWhite", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x503B:
						SetMetadata("ThumbnailCopyRight", ConvertByteArrayToString(pi.Value));
						break;
					case 0x5090:
						SetMetadata("LuminanceTable", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x5091:
						SetMetadata("ChrominanceTable", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x5100:
						SetMetadata("FrameDelay", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x5101:
						SetMetadata("LoopCount", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x5110:
						SetMetadata("PixelUnit", ConvertByteArrayToByte(pi.Value).ToString());
						break;
					case 0x5111:
						SetMetadata("PixelPerUnitX", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x5112:
						SetMetadata("PixelPerUnitY", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x5113:
						SetMetadata("PaletteHistogram", ConvertByteArrayToByte(pi.Value).ToString());
						break;
					case 0x8298:
						SetMetadata("Copyright", ConvertByteArrayToString(pi.Value));
						break;
					case 0x829A:
						SetMetadata("ExifExposureTime", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x829D:
						SetMetadata("ExifFNumber", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x8769:
						SetMetadata("ExifIFD", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x8773:
						SetMetadata("ICCProfile", ConvertByteArrayToByte(pi.Value).ToString());
						break;
					case 0x8822:
					switch(ConvertByteArrayToShort(pi.Value))
					{
						case 0:
							SetMetadata("ExifExposureProg", "Not defined");
							break;
						case 1:
							SetMetadata("ExifExposureProg", "Manual");
							break;
						case 2:
							SetMetadata("ExifExposureProg", "Normal Program");
							break;
						case 3:
							SetMetadata("ExifExposureProg", "Aperture Priority");
							break;
						case 4:
							SetMetadata("ExifExposureProg", "Shutter Priority");
							break;
						case 5:
							SetMetadata("ExifExposureProg", "Creative program (biased toward depth of field)");
							break;
						case 6:
							SetMetadata("ExifExposureProg", "Action program (biased toward fast shutter speed)");
							break;
						case 7:
							SetMetadata("ExifExposureProg", "Portrait mode (for close-up photos with the background out of focus)");
							break;
						case 8:
							SetMetadata("ExifExposureProg", "Landscape mode (for landscape photos with the background in focus)");
							break;
						default:
							SetMetadata("ExifExposureProg", "Unknown");
							break;
					}
						break;
					case 0x8824:
						SetMetadata("ExifSpectralSense", ConvertByteArrayToString(pi.Value));
						break;
					case 0x8825:
						SetMetadata("GpsIFD", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0x8827:
						SetMetadata("ExifISOSpeed", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0x9003:
						SetMetadata("ExifDTOrig", ConvertByteArrayToString(pi.Value));
						break;
					case 0x9004:
						SetMetadata("ExifDTDigitized", ConvertByteArrayToString(pi.Value));
						break;
					case 0x9102:
						SetMetadata("ExifCompBPP", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x9201:
						SetMetadata("ExifShutterSpeed", ConvertByteArrayToSRational(pi.Value));
						break;
					case 0x9202:
						SetMetadata("ExifAperture", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x9203:
						SetMetadata("ExifBrightness", ConvertByteArrayToSRational(pi.Value));
						break;
					case 0x9204:
						SetMetadata("ExifExposureBias", ConvertByteArrayToSRational(pi.Value));
						break;
					case 0x9205:
						SetMetadata("ExifMaxAperture", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x9206:
						SetMetadata("ExifSubjectDist", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x9207:
					switch(ConvertByteArrayToShort(pi.Value))
					{
						case 0:
							SetMetadata("ExifMeteringMode", "Unknown");
							break;
						case 1:
							SetMetadata("ExifMeteringMode", "Average");
							break;
						case 2:
							SetMetadata("ExifMeteringMode", "CenterWeightedAverage");
							break;
						case 3:
							SetMetadata("ExifMeteringMode", "Spot");
							break;
						case 4:
							SetMetadata("ExifMeteringMode", "MultiSpot");
							break;
						case 5:
							SetMetadata("ExifMeteringMode", "Pattern");
							break;
						case 6:
							SetMetadata("ExifMeteringMode", "Partial");
							break;
						case 255:
							SetMetadata("ExifMeteringMode", "Other");
							break;
						default:
							SetMetadata("ExifMeteringMode", "Unknown");
							break;
					}
						break;
					case 0x9208:
					switch(ConvertByteArrayToShort(pi.Value))
					{
						case 0:
							SetMetadata("ExifLightSource", "Unknown");
							break;
						case 1:
							SetMetadata("ExifLightSource", "Daylight");
							break;
						case 2:
							SetMetadata("ExifLightSource", "Flourescent");
							break;
						case 3:
							SetMetadata("ExifLightSource", "Tungsten");
							break;
						case 17:
							SetMetadata("ExifLightSource", "Standard Light A");
							break;
						case 18:
							SetMetadata("ExifLightSource", "Standard Light B");
							break;
						case 19:
							SetMetadata("ExifLightSource", "Standard Light C");
							break;
						case 20:
							SetMetadata("ExifLightSource", "D55");
							break;
						case 21:
							SetMetadata("ExifLightSource", "D65");
							break;
						case 22:
							SetMetadata("ExifLightSource", "D75");
							break;
						case 255:
							SetMetadata("ExifLightSource", "Other");
							break;
						default:
							SetMetadata("ExifLightSource", "Unknown");
							break;
					}
						break;
					case 0x9209:
					switch(ConvertByteArrayToShort(pi.Value))
					{
						case 0:
							SetMetadata("ExifFlash", "Flash did not fire");
							break;
						case 1:
							SetMetadata("ExifFlash", "Flash fired");
							break;
						case 5:
							SetMetadata("ExifFlash", "Strobe return light not detected");
							break;
						default:
							SetMetadata("ExifFlash", "Uknown");
							break;
					}
						break;
					case 0x920A:
						SetMetadata("ExifFocalLength", ConvertByteArrayToRational(pi.Value));
						break;
					case 0x9290:
						SetMetadata("ExifDTSubsec", ConvertByteArrayToString(pi.Value));
						break;
					case 0x9291:
						SetMetadata("ExifDTOrigSS", ConvertByteArrayToString(pi.Value));
						break;
					case 0x9292:
						SetMetadata("ExifDTDigSS", ConvertByteArrayToString(pi.Value));
						break;
					case 0xA001:
					switch(ConvertByteArrayToLong(pi.Value))
					{
						case 0x1:
							SetMetadata("ExifColorSpace", "sRGB");
							break;
						case 0xFFFF:
							SetMetadata("ExifColorSpace", "Uncalibrated");
							break;
						default:
							SetMetadata("ExifColorSpace", "Reserved");
							break;
					}
						break;
					case 0xA002:
						SetMetadata("ExifPixXDim", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0xA003:
						SetMetadata("ExifPixYDim", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0xA004:
						SetMetadata("ExifRelatedWav", ConvertByteArrayToString(pi.Value));
						break;
					case 0xA005:
						SetMetadata("ExifInterop", ConvertByteArrayToLong(pi.Value).ToString());
						break;
					case 0xA20B:
						SetMetadata("ExifFlashEnergy", ConvertByteArrayToRational(pi.Value));
						break;
					case 0xA20E:
						SetMetadata("ExifFocalXRes", ConvertByteArrayToRational(pi.Value));
						break;
					case 0xA20F:
						SetMetadata("ExifFocalYRes", ConvertByteArrayToRational(pi.Value));
						break;
					case 0xA210:
					switch(ConvertByteArrayToShort(pi.Value))
					{
						case 2:
							SetMetadata("ExifFocalResUnit", "Inch");
							break;
						case 3:
							SetMetadata("ExifFocalResUnit", "Centimeter");
							break;
					}
						break;
					case 0xA214:
						SetMetadata("ExifSubjectLoc", ConvertByteArrayToShort(pi.Value).ToString());
						break;
					case 0xA215:
						SetMetadata("ExifExposureIndex", ConvertByteArrayToRational(pi.Value));
						break;
					case 0xA217:
					switch(ConvertByteArrayToShort(pi.Value))
					{
						case 1:
							SetMetadata("ExifSensingMethod", "Not defined");
							break;
						case 2:
							SetMetadata("ExifSensingMethod", "One-chip color area sensor");
							break;
						case 3:
							SetMetadata("ExifSensingMethod", "Two-chip color area sensor");
							break;
						case 4:
							SetMetadata("ExifSensingMethod", "Three-chip color area sensor");
							break;
						case 5:
							SetMetadata("ExifSensingMethod", "Color sequential area sensor");
							break;
						case 7:
							SetMetadata("ExifSensingMethod", "Trilinear sensor");
							break;
						case 8:
							SetMetadata("ExifSensingMethod", "Color sequential linear sensor");
							break;
						default:
							SetMetadata("ExifSensingMethod", "Reserved");
							break;
					}
						break;
				}
			}
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Raises OnInitEvent
		/// </summary>
		/// <param name="e"></param>
		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
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
