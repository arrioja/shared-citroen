using System;
using System.Xml;
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
using Rainbow.Design;

namespace Rainbow.DesktopModules 
{
	public class PictureView : Rainbow.UI.ViewItemPage
	{
		protected Esperantus.WebControls.Label lblError;
		protected System.Web.UI.WebControls.PlaceHolder Picture;

		/// <summary>
		/// The Page_Load event handler on this Page is used to
		/// obtain obtain the contents of a picture from the 
		/// Pictures table, construct an HTTP Response of the
		/// correct type for the picture, and then stream the 
		/// picture contents to the response.  It uses the 
		/// Rainbow.PictureDB() data component to encapsulate 
		/// the data access functionality.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e) 
		{
			if (!Page.IsPostBack && ModuleID > 0 && ItemID > 0 )
			{
				// Obtain a single row of picture information
				PicturesDB pictures = new PicturesDB();
				WorkFlowVersion version = Request.QueryString["wversion"] == "Staging" ? WorkFlowVersion.Staging : WorkFlowVersion.Production;
				SqlDataReader dr = pictures.GetSinglePicture(ItemID, version);
                
				PictureItem pictureItem;
				XmlDocument metadata = new XmlDocument();

				try
				{
					// Read first row from database
					if(dr.Read())
					{
						pictureItem = (PictureItem) Page.LoadControl(Rainbow.Settings.Path.ApplicationRoot + "/Design/PictureLayouts/" + moduleSettings["ImageLayout"]);
			
						metadata.LoadXml((string)dr["MetadataXml"]);

						XmlAttribute albumPath = metadata.CreateAttribute("AlbumPath");
						albumPath.Value = ((SettingItem) moduleSettings["AlbumPath"]).FullPath;

						XmlAttribute itemID = metadata.CreateAttribute("ItemID");
						itemID.Value = ((int) dr["ItemID"]).ToString();

						XmlAttribute moduleID = metadata.CreateAttribute("ModuleID");
						moduleID.Value = this.ModuleID.ToString();

						XmlAttribute wVersion = metadata.CreateAttribute("WVersion");
						wVersion.Value = version.ToString();

						if(dr["PreviousItemID"] != System.DBNull.Value)
						{
							XmlAttribute previousItemID = metadata.CreateAttribute("PreviousItemID");
							previousItemID.Value = ((int) dr["PreviousItemID"]).ToString();
							metadata.DocumentElement.Attributes.Append(previousItemID);
						}

						if(dr["NextItemID"] != System.DBNull.Value)
						{
							XmlAttribute nextItemID = metadata.CreateAttribute("NextItemID");
							nextItemID.Value = ((int) dr["NextItemID"]).ToString();
							metadata.DocumentElement.Attributes.Append(nextItemID);
						}

						metadata.DocumentElement.Attributes.Append(albumPath);
						metadata.DocumentElement.Attributes.Append(itemID);
						metadata.DocumentElement.Attributes.Append(moduleID);
						metadata.DocumentElement.Attributes.Append(wVersion);		

						if(version == WorkFlowVersion.Production)
						{
							XmlNode modifiedFilenameNode = metadata.DocumentElement.SelectSingleNode("@ModifiedFilename");
							XmlNode thumbnailFilenameNode = metadata.DocumentElement.SelectSingleNode("@ThumbnailFilename");

							modifiedFilenameNode.Value = modifiedFilenameNode.Value.Replace(".jpg", ".Production.jpg");
							thumbnailFilenameNode.Value = thumbnailFilenameNode.Value.Replace(".jpg", ".Production.jpg");
						}
				

						pictureItem.Metadata = metadata;
						pictureItem.DataBind();

						Picture.Controls.Add(pictureItem);
					}
				}
				catch
				{
					lblError.Visible = true;
					Picture.Visible = false;
					return;
				}
				finally
				{
					// Close datareader
					dr.Close();
				}

				this.DataBind();
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

		#region Web Form Designer generated code
		/// <summary>
		/// Raises OnInitEvent
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
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