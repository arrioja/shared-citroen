using System;
using System.IO;
using System.Web.UI.WebControls;
using Rainbow.Configuration;
using Rainbow.UI.DataTypes;
using Rainbow.UI.WebControls;
using Path = Rainbow.Settings.Path;

namespace Rainbow.DesktopModules
{
	/// <summary>
	///		:::::::::::::::::::::::::
	///		::  FileDirectoryTree  ::
	///		:::::::::::::::::::::::::
	///		
	///		Developed by: Josue de la Torre, josue@jdlt.com, www.jdlt.com
	///		Module traverses and displays files and directories as an 
	///		HTML-representation of a nested file tree.
	/// </summary>
	/// 
	///		::::::::::::::::::::::
	///		::  Module History  ::
	///		::::::::::::::::::::::
	///		
	///		07/23/2003	First Release - Josue de la Torre (josue@jdlt.com)
	public class FileDirectoryTree : PortalModuleControl
	{
		protected Literal LiteralFileDirectoryTree;
		protected LinkButton LinkButton1;
		protected PlaceHolder myPlaceHolder;

		private string path, myStyle, LinkType;

		private void Page_Load(object sender, EventArgs e)
		{
			path = Settings["Directory"].ToString();
			myStyle = Settings["Style"].ToString();
			LinkType = Settings["LinkType"].ToString();

			// Check if the last character is an backslash.  If not, append it. 
			if (path.Substring(path.Length - 1, 1) != "\\")
				path += "\\";

			// Support for old installs may have physical path we want virtual.
			if(path.IndexOf(":") >= 0)
			{
				// find app root from phsyical path and cut so we only have virtual path
				path = path.Substring(Rainbow.Settings.Path.ApplicationPhysicalPath.Length);
			}

			// Check to make sure path exists before entering render methods
			if (Directory.Exists(Server.MapPath(path)))
			{
				Write("<span style='" + myStyle + "'>\n");
				parseDirectory(Server.MapPath(path));
				// Close the span and create the Toggle javascript function.
				Write("</span>");
			}
			else
			{
				Write("<span class='Error'>Error! The directory path you specified does not exist.</span>");
			}
		}


		/// <summary>
		/// This function traverses a given directory and finds all its nested directories and 
		/// files.  As the function encounters nested directories, it calls a new instance of 
		/// the procedure passing the new found directory as a parameter.  Files within the 
		/// directories are nested and tabulated.
		/// 
		/// </summary>
		/// <param name="path">Directory path to traverse.</param>
		private void parseDirectory(string path)
		{
			string[] entry;
			try
			{
				// Retrieve all entry (entry & directories) from the current path
				entry = Directory.GetFileSystemEntries(path);

				// For each entry in the directory...
				for (int i = 0; i < entry.Length; i++)
				{
					// Trim the file path from the file, leaving the filename
					string filename = entry[i].Replace(path, string.Empty);

					// If the current entry is a directory...
					if (Directory.Exists(entry[i]))
					{
						// Find how many entry the subdirectory has and create an objectID name for the subdirectory
						int subentries = Directory.GetFileSystemEntries(entry[i]).Length;
						string objectID = entry[i].Replace(Settings["Directory"].ToString(), string.Empty).Replace("\\", "~");

						// Define the span that holds the opened/closed directory icon
						Write("<span id='" + objectID + "Span' style='width=16;font-family:wingdings'>");

						if (Settings["Collapsed"].ToString().Equals("True"))
							Write("0");
						else
							Write("1");

						Write("</span><a href=\"javascript:Toggle('" + objectID + "')\" " +
							// Create a hover tag that contains content details about the subdirectory.
							"title=\"" + subentries.ToString() + " entries found.\">" + filename + "</a>" +
							"<br>\n<span id='" + objectID + "' style='");

						if (Settings["Collapsed"].ToString().Equals("True"))
							Write("DISPLAY: none; ");
						else
							Write("DISPLAY: inline; ");

						if (!Settings["Indent"].ToString().Equals(string.Empty)) Write("LEFT: " + Settings["Indent"].ToString() + "; ");

						// Call the parseDirectory for the new subdirectory.
						Write("POSITION: relative;'>\n");

						parseDirectory(entry[i] + "\\");

						Write("</span>\n");
					}
					else // ...the current entry is a file.
					{
						// create a file icon 
						Write("<span style='width=16;font-family:webdings'>�</span>");

						if (LinkType.Equals("Network Share"))
						{
							Write("<a href='" + entry[i] + "' title='Last Write Time: " + File.GetLastWriteTime(entry[i]).ToString());
							Write("' target='_" + Settings["Target"].ToString() + "'>" + filename + "</a>");
						}
						else
						{
							// Create the link to the file.
							LinkButton lb = new LinkButton();
							lb.Text = filename;
							lb.CommandArgument = entry[i];
							lb.Click += new EventHandler(Download);
							myPlaceHolder.Controls.Add(lb);
						}

						Write("<br>\n");
					}
				}
			} 

				/* Unauthorized Access Exception: 
				* 
				* This error thrown when the server does not have rights to read 
				* the current path.  Please read the included documentation before 
				* uncommenting the following lines of code.
				*/

				//	catch (UnauthorizedAccessException) 
				//	{
				//		string redirect = Request.Url.PathAndQuery.Replace("/DesktopDefault.aspx", "/WADesktopDefault.aspx");
				//		Response.Redirect(redirect);
				//	}
			catch (DirectoryNotFoundException)
			{
				Write("<span class='Error'>Error!  The directory path you specified does not exist.</span>");
				return;
			}
			catch (Exception e1) // All other exceptions...
			{
				Write("<span class='Error'>" + e1.ToString() + "</span>");
				return;
			}
		}

		public void Write(string text)
		{
			Literal l = new Literal();
			l.Text = text;
			myPlaceHolder.Controls.Add(l);
		}

		private void Download(object sender, EventArgs e)
		{
			string filepath = ((LinkButton) sender).CommandArgument;
			string filename = filepath.Substring(filepath.LastIndexOf('\\') + 1, filepath.Length - filepath.LastIndexOf('\\') - 1);
	
			Stream s = null;
			Byte[] buffer = new byte[0];
			try
			{
				s = File.Open(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
				buffer = new Byte[s.Length];
				s.Read(buffer, 0, (Int32) s.Length);
			}
			catch(Exception ex)
			{
				Response.ClearContent();
				Response.Write(ex.Message);
				Response.End();
			}
			finally
			{
				if (s != null)
					s.Close(); //by manu
			}
			Response.ClearHeaders();
			Response.ClearContent();
			Response.ContentType = "application/octet-stream";
			Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
			Response.BinaryWrite(buffer);
			Response.End();
		}

		public FileDirectoryTree()
		{
			SettingItem directory = new SettingItem(new StringDataType());
			directory.EnglishName = "Directory Path";
			directory.Required = true;
			directory.Order = 1;
			// Changed to virutal root from physical
			directory.Value = Rainbow.Settings.Path.ApplicationRoot;
			//directory.Value = Path.ApplicationPhysicalPath;
			this._baseSettings.Add("Directory", directory);

			SettingItem LinkType = new SettingItem(new ListDataType("Downloadable Link;Network Share"));
			LinkType.EnglishName = "Link Type";
			LinkType.Order = 2;
			LinkType.Value = "Downloadable Link";
			this._baseSettings.Add("LinkType", LinkType);

			SettingItem Target = new SettingItem(new ListDataType("blank;parent;self;top"));
			Target.EnglishName = "Target Window";
			Target.Required = false;
			Target.Order = 3;
			Target.Value = "blank";
			this._baseSettings.Add("Target", Target);

			SettingItem Collapsed = new SettingItem(new BooleanDataType());
			Collapsed.EnglishName = "Collapsed View";
			Collapsed.Order = 4;
			Collapsed.Value = "true";
			this._baseSettings.Add("Collapsed", Collapsed);

			SettingItem Style = new SettingItem(new StringDataType());
			Style.EnglishName = "Style";
			Style.Required = false;
			Style.Order = 5;
			Style.Value = string.Empty;
			this._baseSettings.Add("Style", Style);

			SettingItem Indent = new SettingItem(new StringDataType());
			Indent.EnglishName = "SubDirectory Indent (px)";
			Indent.Required = false;
			Indent.Order = 6;
			Indent.Value = "20px";
			this._baseSettings.Add("Indent", Indent);
		}

		public override Guid GuidID
		{
			get { return new Guid("{2502DB18-B580-4F90-8CB4-C15E6E53100B}"); }
		}

		#region Web Form Designer generated code

		override protected void OnInit(EventArgs e)
		{
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			InitializeComponent();

			// no need for viewstate here - jminond
			this.myPlaceHolder.EnableViewState = false;

			base.OnInit(e);
		}

		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);
		}

		#endregion
	}
}