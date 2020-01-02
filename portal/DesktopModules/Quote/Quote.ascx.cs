using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections; 
using System.IO;
using System.Configuration;

using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Rainbow.Configuration;


namespace Rainbow.DesktopModules
{
	/// <summary>
	/// Display some text from a xml file. 
	/// </summary>
	public class Quote : PortalModuleControl
	{
		protected System.Web.UI.WebControls.Label lblQuote;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			bool MyQuote = "My Quote" == Settings["Quote source"].ToString();
			string textSize = Settings["Text size"].ToString();
			bool displayInItalic = "True" == Settings["Display in italic"].ToString();
			bool displayInBold = "True" == Settings["Display in bold"].ToString();
			string startTag = Settings["Start tag"].ToString();
			string endTag = Settings["End tag"].ToString();
			string quoteText;

			if (MyQuote)
			{
				quoteText = Settings["My Quote"].ToString();
			}
			else
			{
				Random objRan = new Random();
				ArrayList col = new ArrayList();
				string quoteFile = Settings["Quote file"].ToString();

				if(ConfigurationSettings.AppSettings.Get("QuoteFileFolder") != null )
				{
					if(ConfigurationSettings.AppSettings.Get("QuoteFileFolder").ToString().Length > 0 )
						quoteFile = ConfigurationSettings.AppSettings.Get("QuoteFileFolder").ToString() + quoteFile;
					else
						quoteFile = "~/DesktopModules/Quote/" + quoteFile;
				}
				else
				{
					quoteFile = "~/DesktopModules/Quote/" + quoteFile;
				}

				if(!quoteFile.EndsWith(".quote"))
					quoteFile += ".quote";
				
				if (File.Exists(Server.MapPath(quoteFile)))
				{
					try 
					{
						using (StreamReader sr = new StreamReader(Server.MapPath(quoteFile))) 
						{
							String line;
							while ((line = sr.ReadLine()) != null) 
								col.Add(line);
						}
					}
					catch (Exception ex) 
					{
						col.Add("Problems reading quotes file! <br> --- Jakob Hansen"); 
						Rainbow.Configuration.ErrorHandler.HandleException("Problems reading Quotes file.",ex);
					}
				}
				else
				{
					col.Add("Quotes file missing! <br> --- Jakob Hansen");  // hehe...
				}

				/* These are now in file demo.quote
				col.Add("Service is the rent we pay for being. It is the very purpose of life, and not something you do in your spare time. <br> --- Marion Wright Edelman");
				col.Add("You must be the change you wish to see in the world. <br> --- Mahatma Ghandi");
				col.Add("Make others happy and joyful. Your happiness will multiply a thousand fold. <br> --- Swami Sivananda");
				col.Add("The influence of each human being on others in this life is a kind of immortality. <br> --- John Quincy Adams");
				col.Add("Love sought is good, but given unsought is better. <br> --- Shakespeare");
				col.Add("Here is a test to find out whether your mission in life is complete. If you're alive, it isn't. <br> --- Richard Bach");
				col.Add("There is no such thing in anyone's life as an unimportant day. <br> --- Alexander Woollcott");
				col.Add("How far you go in life depends on your being tender with the young, compassionate with the aged, sympathetic with the striving and tolerant of the weak and the strong. Because someday in life you will have been all of these. <br> --- George Washington Carver");
				col.Add("People rarely succeed unless they have fun in what they are doing. <br> --- Dale Carnegie");
				col.Add("Sit on a baby's bib and SPIT HAPPENS <br> --- Anonymous");
				col.Add("May the smile on your face Come straight from your heart <br> --- Anonymous");
				col.Add("Most good judgment comes from experience. Most experience comes from bad judgment. <br> --- Anonymous");
				col.Add("The true \"final frontier\" is in the minds and the will of people. <br> -- Gen. Michael E. Ryan, U.S. Air Force Chief of Staff");
				col.Add("I don't pretend to understand the Universe - it's a great deal bigger than I am. <br> -- Thomas Carlyle");
				col.Add("When we try to pick out anything else in the Universe, we find it hitched to everything else in the Universe. <br> -- John Muir");
				col.Add("Not all who wander are lost. <br> -- Tolkien");
				*/

				quoteText = (string)col[objRan.Next(col.Count)];
			}

			if (displayInItalic)
				quoteText = "<i>" + quoteText + "</i>";
			if (displayInBold)
				quoteText = "<b>" + quoteText + "</b>";
			if (textSize != "Default")
				quoteText = "<H" + textSize[1] + ">" + quoteText + "</H" + textSize[1] + ">";
			if (startTag != "")
				quoteText = startTag + quoteText;
			if (endTag != "")
				quoteText += endTag;

			lblQuote.Text = quoteText;
		}


		public Quote() 
		{
			SettingItem setQuoteSource = new SettingItem(new ListDataType("File;My Quote"));
			setQuoteSource.Value = "File";
			setQuoteSource.Order = 1;
			setQuoteSource.EnglishName = "Quote source?";
			setQuoteSource.Description = "Get quotes from a file or display the text from field My Quote";
			this._baseSettings.Add("Quote source", setQuoteSource);

			ListDataType fileList = new ListDataType(this.GetListOfQuoteFiles());
			SettingItem setQuoteFile = new SettingItem(fileList);
			setQuoteFile.Value = "demo.quote";
			setQuoteFile.Order = 2;
			setQuoteFile.EnglishName = "Quote file";
			setQuoteFile.Description = "The name of the file containing quotes";
			this._baseSettings.Add("Quote file", setQuoteFile);

			SettingItem setMyQuote = new SettingItem(new StringDataType());
			setMyQuote.Value = "Enter your a quote here!";
			setMyQuote.Order = 3;
			setMyQuote.EnglishName = "My Quote";
			setMyQuote.Description = "Enter any quote here and set Quote source to My Quote";
			this._baseSettings.Add("My Quote", setMyQuote);
			
			SettingItem setTextSize = new SettingItem(new ListDataType("Default;H1 (largest);H2;H3;H4;H5;H6 (smallest)"));
			setTextSize.Value = "Default";
			setTextSize.Order = 4;
			setTextSize.EnglishName = "Text size";
			setTextSize.Description = "Text size of the quote text. The 6 build-in heading sizes (HTML tag <H1>,<H2>,etc)";
			_baseSettings.Add("Text size", setTextSize);

			SettingItem setDisplayInItalic = new SettingItem(new BooleanDataType());
			setDisplayInItalic.Value = "true";
			setDisplayInItalic.Order = 5;
			setDisplayInItalic.EnglishName = "Display in italic?";
			setDisplayInItalic.Description = "Display all the quote text in italic style (HTML tag <i>)";
			this._baseSettings.Add("Display in italic", setDisplayInItalic);

			SettingItem setDisplayInBold = new SettingItem(new BooleanDataType());
			setDisplayInBold.Value = "false";
			setDisplayInBold.Order = 6;
			setDisplayInBold.EnglishName = "Display in bold?";
			setDisplayInBold.Description = "Display all the quote text in bold/fat letters (HTML tag <b>)";
			this._baseSettings.Add("Display in bold", setDisplayInBold);

			SettingItem setStartTag = new SettingItem(new StringDataType());
			setStartTag.Value = "";
			setStartTag.Order = 7;
			setStartTag.EnglishName = "Start tag";
			setStartTag.Description = "Enter any special customizing HTML start tag here, e.g. a marquee tag make the text scroll";
			this._baseSettings.Add("Start tag", setStartTag);

			SettingItem setEndTag = new SettingItem(new StringDataType());
			setEndTag.Value = "";
			setEndTag.Order = 8;
			setEndTag.EnglishName = "End tag";
			setEndTag.Description = "Must correspond to the Start tag";
			this._baseSettings.Add("End tag", setEndTag);

		}

		/// <summary>
		/// Author:		Joe Audette
		/// Added:		7/31/2003
		/// Allows you to add files with queries without compiling
		/// Any query file placed in the folder specified in the web.config willshow up
		/// in the dropdown list
		/// </summary>
		/// <returns>FileInfo[]</returns>
		public  FileInfo[] GetListOfQuoteFiles()
		{
			string quoteFilePath = string.Empty;
			
			if (ConfigurationSettings.AppSettings["QuoteFileFolder"] != null && ConfigurationSettings.AppSettings["QuoteFileFolder"].Length > 0)
				quoteFilePath = ConfigurationSettings.AppSettings["QuoteFileFolder"];
			else
			{
				//this will default to the folder where the .query files are located by default
				quoteFilePath = System.Web.HttpContext.Current.Server.MapPath(this.TemplateSourceDirectory);
			}

			try
			{
				if(Directory.Exists(quoteFilePath))
				{
					DirectoryInfo dir = new DirectoryInfo(quoteFilePath);
					return dir.GetFiles("*.quote");
				}
				else
				{
					Rainbow.Helpers.LogHelper.Log.Warn("Default Quote file folder/location not found: '" + quoteFilePath + "'");
				}
			}
			catch(Exception ex)
			{	
				Rainbow.Configuration.ErrorHandler.HandleException("Quote file folder/location not found: " + quoteFilePath, ex);
			}
			return null;
		}



		public override Guid GuidID 
		{
			get
			{
				return new Guid("{2502DB18-B580-4F90-8CB4-C15E6E531053}");
			}
		}


		#region Web Form Designer generated code
		/// <summary>
		/// Raises Init event
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		///<summary>
		///	Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		///</summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
