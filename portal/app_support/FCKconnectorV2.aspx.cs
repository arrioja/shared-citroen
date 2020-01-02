/*
 * FCKEditor File Manager Connector for Rainbow
 * Based on original FileBrowserConnector.cs from Frederico Caldeira Knabben (fredck@fckeditor.net)
 * 
 * Author : José Viladiu (jviladiu@portalServices.net) 2004/11/09
 */

using System;
using System.Globalization;
using System.Xml;
using System.Web;
using System.Collections;
using Rainbow.Configuration;
using Rainbow.Security;

namespace Rainbow.FCKeditorV2
{
	public class FileBrowserConnector : Rainbow.UI.EditItemPage //System.Web.UI.Page
	{
		private string sUserFilesPath ;
		private string sUserFilesDirectory ;

		protected override void LoadSettings()
		{
			if (PortalSecurity.HasEditPermissions(this.portalSettings.ActiveModule) == false)
				PortalSecurity.AccessDeniedEdit();
		}

		protected override void OnLoad(EventArgs e)
		{
			// Get the main request informaiton.
			string sCommand = Request.QueryString["Command"] ;
			if ( sCommand == null ) return ;

			string sResourceType = Request.QueryString["Type"] ;
			if ( sResourceType == null ) return ;

			string sCurrentFolder = Request.QueryString["CurrentFolder"] ;
			if ( sCurrentFolder == null ) return ;

			// Check the current folder syntax (must begin and start with a slash).
			if ( ! sCurrentFolder.EndsWith( "/" ) )
				sCurrentFolder += "/" ;
			if ( ! sCurrentFolder.StartsWith( "/" ) )
				sCurrentFolder = "/" + sCurrentFolder ;

			// File Upload doesn't have to return XML, so it must be intercepted before anything.
			if ( sCommand == "FileUpload" )
			{
				this.FileUpload( sResourceType, sCurrentFolder ) ;
				return ;
			}

			// Cleans the response buffer.
			Response.ClearHeaders() ;
			Response.Clear() ;

			// Prevent the browser from caching the result.
			Response.CacheControl = "no-cache" ;

			// Set the response format.
			Response.ContentEncoding	= System.Text.UTF8Encoding.UTF8 ;
			Response.ContentType		= "text/xml" ;

			XmlDocument oXML = new XmlDocument() ;
			XmlNode oConnectorNode = CreateBaseXml( oXML, sCommand, sResourceType, sCurrentFolder ) ;

			// Execute the required command.
			switch( sCommand )
			{
				case "GetFolders" :
					this.GetFolders( oConnectorNode, sResourceType, sCurrentFolder ) ;
					break ;
				case "GetFoldersAndFiles" :
					this.GetFolders( oConnectorNode, sResourceType, sCurrentFolder ) ;
					this.GetFiles( oConnectorNode, sResourceType, sCurrentFolder ) ;
					break ;
				case "CreateFolder" :
					this.CreateFolder( oConnectorNode, sResourceType, sCurrentFolder ) ;
					break ;
			}

			// Output the resulting XML.
			Response.Write( oXML.OuterXml ) ;

			Response.End() ;
		}

		#region Base XML Creation

		private XmlNode CreateBaseXml( XmlDocument xml, string command, string resourceType, string currentFolder )
		{
			// Create the XML document header.
			xml.AppendChild( xml.CreateXmlDeclaration( "1.0", "utf-8", null ) ) ;

			// Create the main "Connector" node.
			XmlNode oConnectorNode = XmlUtil.AppendElement( xml, "Connector" ) ;
			XmlUtil.SetAttribute( oConnectorNode, "command", command ) ;
			XmlUtil.SetAttribute( oConnectorNode, "resourceType", resourceType ) ;

			// Add the current folder node.
			XmlNode oCurrentNode = XmlUtil.AppendElement( oConnectorNode, "CurrentFolder" ) ;
			XmlUtil.SetAttribute( oCurrentNode, "path", currentFolder ) ;
			XmlUtil.SetAttribute( oCurrentNode, "url", GetUrlFromPath( resourceType, currentFolder) ) ;

			return oConnectorNode ;
		}

		#endregion

		#region Command Handlers

		private void GetFolders( XmlNode connectorNode, string resourceType, string currentFolder )
		{
			// Map the virtual path to the local server path.
			string sServerDir = this.ServerMapFolder( resourceType, currentFolder ) ;

			// Create the "Folders" node.
			XmlNode oFoldersNode = XmlUtil.AppendElement( connectorNode, "Folders" ) ;

			System.IO.DirectoryInfo oDir = new System.IO.DirectoryInfo( sServerDir ) ;
			System.IO.DirectoryInfo[] aSubDirs = oDir.GetDirectories() ;

			for ( int i = 0 ; i < aSubDirs.Length ; i++ )
			{
				// Create the "Folders" node.
				XmlNode oFolderNode = XmlUtil.AppendElement( oFoldersNode, "Folder" ) ;
				XmlUtil.SetAttribute( oFolderNode, "name", aSubDirs[i].Name ) ;
			}
		}

		private void GetFiles( XmlNode connectorNode, string resourceType, string currentFolder )
		{
			// Map the virtual path to the local server path.
			string sServerDir = this.ServerMapFolder( resourceType, currentFolder ) ;

			// Create the "Files" node.
			XmlNode oFilesNode = XmlUtil.AppendElement( connectorNode, "Files" ) ;

			System.IO.DirectoryInfo oDir = new System.IO.DirectoryInfo( sServerDir ) ;
			System.IO.FileInfo[] aFiles = oDir.GetFiles() ;

			for ( int i = 0 ; i < aFiles.Length ; i++ )
			{
				Decimal iFileSize = Math.Round( (Decimal)aFiles[i].Length / 1024 ) ;
				if ( iFileSize < 1 && aFiles[i].Length != 0 ) iFileSize = 1 ;

				// Create the "File" node.
				XmlNode oFileNode = XmlUtil.AppendElement( oFilesNode, "File" ) ;
				XmlUtil.SetAttribute( oFileNode, "name", aFiles[i].Name ) ;
				XmlUtil.SetAttribute( oFileNode, "size", iFileSize.ToString( CultureInfo.InvariantCulture ) ) ;
			}
		}

		private void CreateFolder( XmlNode connectorNode, string resourceType, string currentFolder )
		{
			string sErrorNumber = "0" ;

			string sNewFolderName = Request.QueryString["NewFolderName"] ;

			if ( sNewFolderName == null || sNewFolderName.Length == 0 )
				sErrorNumber = "102" ;
			else
			{
				// Map the virtual path to the local server path of the current folder.
				string sServerDir = this.ServerMapFolder( resourceType, currentFolder ) ;

				try
				{
					System.IO.Directory.CreateDirectory(System.IO.Path.Combine( sServerDir, sNewFolderName )) ;
				}
				catch ( ArgumentException )
				{
					sErrorNumber = "102" ;
				}
				catch ( System.IO.PathTooLongException )
				{
					sErrorNumber = "102" ;
				}
				catch ( System.IO.IOException )
				{
					sErrorNumber = "101" ;
				}
				catch ( System.Security.SecurityException )
				{
					sErrorNumber = "103" ;
				}
				catch ( Exception )
				{
					sErrorNumber = "110" ;
				}
			}

			// Create the "Error" node.
			XmlNode oErrorNode = XmlUtil.AppendElement( connectorNode, "Error" ) ;
			XmlUtil.SetAttribute( oErrorNode, "number", sErrorNumber ) ;
		}

		private void FileUpload( string resourceType, string currentFolder )
		{
			HttpPostedFile oFile = Request.Files["NewFile"] ;

			string sErrorNumber = "0" ;
			string sFileName = string.Empty ;

			if ( oFile != null )
			{
				// Map the virtual path to the local server path.
				string sServerDir = this.ServerMapFolder( resourceType, currentFolder ) ;

				// Get the uploaded file name.
				sFileName = System.IO.Path.GetFileName( oFile.FileName ) ;

				int iCounter = 0 ;

				while ( true )
				{
					string sFilePath = System.IO.Path.Combine( sServerDir, sFileName ) ;

					if ( System.IO.File.Exists( sFilePath ) )
					{
						iCounter++ ;
						sFileName = 
							System.IO.Path.GetFileNameWithoutExtension( oFile.FileName ) +
							"(" + iCounter + ")" +
							System.IO.Path.GetExtension( oFile.FileName ) ;

						sErrorNumber = "201" ;
					}
					else
					{
						oFile.SaveAs( sFilePath ) ;
						break ;
					}
				}
			}
			else
				sErrorNumber = "202" ;

			Response.Clear() ;

			Response.Write( "<script type=\"text/javascript\">" ) ;
			Response.Write( "window.parent.frames['frmUpload'].OnUploadCompleted(" + sErrorNumber + ",'" + sFileName.Replace( "'", "\\'" ) + "') ;" ) ;
			Response.Write( "</script>" ) ;

			Response.End() ;
		}

		#endregion

		#region Directory Mapping

		private string ServerMapFolder( string resourceType, string folderPath )
		{
			// Ensure that the directory exists.
			System.IO.Directory.CreateDirectory( this.UserFilesDirectory ) ;

			// Return the resource type directory combined with the required path.
			return System.IO.Path.Combine( this.UserFilesDirectory, folderPath.TrimStart('/') ) ;
		}

		private string GetUrlFromPath( string resourceType, string folderPath )
		{
			if ( resourceType == null || resourceType.Length == 0 )
				return this.UserFilesPath.TrimEnd('/') + folderPath ;
			else
				return this.UserFilesPath + folderPath ;
		}

		private string UserFilesPath
		{
			get
			{
				if ( sUserFilesPath == null )
				{
					PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
					if (portalSettings == null) return null;
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
					sUserFilesPath = Rainbow.Settings.Path.WebPathCombine(Rainbow.Settings.Path.ApplicationRoot, portalSettings.PortalPath, "images", DefaultImageFolder);
				}
				return sUserFilesPath ;
			}
		}

		private string UserFilesDirectory
		{
			get	
			{
				if ( sUserFilesDirectory == null )
				{
					// Get the local (server) directory path translation.
					sUserFilesDirectory = Server.MapPath( this.UserFilesPath ) ;
				}
				return sUserFilesDirectory ;
			}
		}

		#endregion
	}

	internal sealed class XmlUtil
	{
		private XmlUtil()
		{}

		public static XmlNode AppendElement( XmlNode node, string newElementName )
		{
			return AppendElement( node, newElementName, null ) ;
		}

		public static XmlNode AppendElement( XmlNode node, string newElementName, string innerValue )
		{
			XmlNode oNode ;

			if ( node is XmlDocument )
				oNode = node.AppendChild( ((XmlDocument)node).CreateElement( newElementName ) ) ;
			else
				oNode = node.AppendChild( node.OwnerDocument.CreateElement( newElementName ) ) ;

			if ( innerValue != null )
				oNode.AppendChild( node.OwnerDocument.CreateTextNode( innerValue ) ) ;

			return oNode ;
		}

		public static XmlAttribute CreateAttribute( XmlDocument xmlDocument, string name, string value )
		{
			XmlAttribute oAtt = xmlDocument.CreateAttribute( name ) ;
			oAtt.Value = value ;
			return oAtt ;
		}

		public static void SetAttribute( XmlNode node, string attributeName, string attributeValue )
		{
			if ( node.Attributes[ attributeName ] != null )
				node.Attributes[ attributeName ].Value = attributeValue ;
			else
				node.Attributes.Append( CreateAttribute( node.OwnerDocument, attributeName, attributeValue ) ) ;
		}
	}

}
