using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using System.Web.Caching;
using System.IO;
using System.Threading;
using System.Globalization;
using Rainbow.UI;
using Rainbow.UI.DataTypes;
using Rainbow.UI.WebControls;
using Rainbow.Configuration;

using Esperantus;
using Esperantus.WebControls;

namespace Rainbow.DesktopModules
{
	/// <summary>
	///	Summary description for LanguageSwitcher.
	/// </summary>
	public class LanguageSwitcher : PortalModuleControl
	{
		public const string LANGUAGE_DEFAULT = "en-US";

		protected Esperantus.WebControls.LanguageSwitcher LanguageSwitcher1;

		public static CultureInfo[] GetLanguageList(bool addInvariantCulture)
		{
			return GetLanguageCultureList().ToUICultureArray(addInvariantCulture);
		}

		public static Esperantus.WebControls.LanguageCultureCollection GetLanguageCultureList()
		{
			string strLangList = LANGUAGE_DEFAULT; //default for design time

			// Obtain PortalSettings from Current Context
			if (HttpContext.Current != null && HttpContext.Current.Items["PortalSettings"] != null)
			{
				//Do not remove these checks!! It fails installing modules on startup
				PortalSettings _portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
				if(_portalSettings.CustomSettings != null && _portalSettings.CustomSettings["SITESETTINGS_LANGLIST"] != null)
					strLangList = _portalSettings.CustomSettings["SITESETTINGS_LANGLIST"].ToString();
			}
			Esperantus.WebControls.LanguageCultureCollection langList;
			try
			{
				langList = (Esperantus.WebControls.LanguageCultureCollection) System.ComponentModel.TypeDescriptor.GetConverter(typeof(Esperantus.WebControls.LanguageCultureCollection)).ConvertTo(strLangList, typeof(Esperantus.WebControls.LanguageCultureCollection));
			}
			catch(Exception ex)
			{
				Rainbow.Configuration.ErrorHandler.HandleException("Failed to load languages, loading defaults", ex);
				langList = (Esperantus.WebControls.LanguageCultureCollection) System.ComponentModel.TypeDescriptor.GetConverter(typeof(Esperantus.WebControls.LanguageCultureCollection)).ConvertTo(LANGUAGE_DEFAULT, typeof(Esperantus.WebControls.LanguageCultureCollection));
			}
			return langList;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public LanguageSwitcher()
		{
			// Language Switcher Module - Type
			ArrayList languageSwitcherTypesOptions = new ArrayList();
			languageSwitcherTypesOptions.Add(new SettingOption((int)LanguageSwitcherType.DropDownList, Esperantus.Localize.GetString("LANGSWITCHTYPE_DROPDOWNLIST", "DropDownList", null)));
			languageSwitcherTypesOptions.Add(new SettingOption((int)LanguageSwitcherType.VerticalLinksList, Esperantus.Localize.GetString("LANGSWITCHTYPE_LINKS", "Links", null)));
			languageSwitcherTypesOptions.Add(new SettingOption((int)LanguageSwitcherType.HorizontalLinksList, Esperantus.Localize.GetString("LANGSWITCHTYPE_LINKSHORIZONTAL", "Links Horizontal", null)));

			SettingItem languageSwitchType = new SettingItem(new CustomListDataType(languageSwitcherTypesOptions, "Name", "Val"));
			languageSwitchType.EnglishName = "Language Switcher Type";
			languageSwitchType.Description = "Select here how your language switcher should look like.";
			languageSwitchType.Value = ((int)LanguageSwitcherType.DropDownList).ToString();
			languageSwitchType.Order = (int)SettingItemGroup.THEME_LAYOUT_SETTINGS + 910;
			languageSwitchType.Group = SettingItemGroup.THEME_LAYOUT_SETTINGS;
			_baseSettings.Add("LANGUAGESWITCHER_TYPES", languageSwitchType);

			// Language Switcher Module - DisplayOptions
			ArrayList languageSwitcherDisplayOptions = new ArrayList();
			languageSwitcherDisplayOptions.Add(new SettingOption((int)LanguageSwitcherDisplay.DisplayCultureList, Esperantus.Localize.GetString("LANGSWITCHTDISPLAY_CULTURELIST", "Using Culture Name", null)));
			languageSwitcherDisplayOptions.Add(new SettingOption((int)LanguageSwitcherDisplay.DisplayUICultureList, Esperantus.Localize.GetString("LANGSWITCHTDISPLAY_UICULTURELIST", "Using UI Culture Name", null)));
			languageSwitcherDisplayOptions.Add(new SettingOption((int)LanguageSwitcherDisplay.DisplayNone, Esperantus.Localize.GetString("LANGSWITCHTDISPLAY_NONE", "None", null)));

			// Flags
			SettingItem languageSwitchFlags = new SettingItem(new CustomListDataType(languageSwitcherDisplayOptions, "Name", "Val"));
			languageSwitchFlags.EnglishName = "Show Flags as";
			languageSwitchFlags.Description = "Select here how flags should look like.";
			languageSwitchFlags.Value = ((int)LanguageSwitcherDisplay.DisplayCultureList).ToString();
			languageSwitchFlags.Order = (int)SettingItemGroup.THEME_LAYOUT_SETTINGS + 920;
			languageSwitchFlags.Group = SettingItemGroup.THEME_LAYOUT_SETTINGS;
			_baseSettings.Add("LANGUAGESWITCHER_FLAGS", languageSwitchFlags);

			// Labels
			SettingItem languageSwitchLabels = new SettingItem(new CustomListDataType(languageSwitcherDisplayOptions, "Name", "Val"));
			languageSwitchLabels.EnglishName = "Show Labels as";
			languageSwitchLabels.Description = "Select here how Labels should look like.";
			languageSwitchLabels.Value = ((int)LanguageSwitcherDisplay.DisplayCultureList).ToString();
			languageSwitchLabels.Order = (int)SettingItemGroup.THEME_LAYOUT_SETTINGS + 930;
			languageSwitchLabels.Group = SettingItemGroup.THEME_LAYOUT_SETTINGS;
			_baseSettings.Add("LANGUAGESWITCHER_LABELS", languageSwitchLabels);

			// Language Switcher Module - NamesOptions
			ArrayList languageSwitcherNamesOptions = new ArrayList();
			languageSwitcherNamesOptions.Add(new SettingOption((int)LanguageSwitcherName.NativeName, Esperantus.Localize.GetString("LANGSWITCHTNAMES_NATIVENAME", "Native Name", null)));
			languageSwitcherNamesOptions.Add(new SettingOption((int)LanguageSwitcherName.EnglishName, Esperantus.Localize.GetString("LANGSWITCHTNAMES_ENGLISHNAME", "English Name", null)));
			languageSwitcherNamesOptions.Add(new SettingOption((int)LanguageSwitcherName.DisplayName, Esperantus.Localize.GetString("LANGSWITCHTNAMES_DISPLAYNAME", "Display Name", null)));

			// Names
			SettingItem languageSwitcherName = new SettingItem(new CustomListDataType(languageSwitcherNamesOptions, "Name", "Val"));
			languageSwitcherName.EnglishName = "Show names as";
			languageSwitcherName.Description = "Select here how names should look like.";
			languageSwitcherName.Value = ((int)LanguageSwitcherName.NativeName).ToString();
			languageSwitcherName.Order = (int)SettingItemGroup.THEME_LAYOUT_SETTINGS + 940;
			languageSwitcherName.Group = SettingItemGroup.THEME_LAYOUT_SETTINGS;
			_baseSettings.Add("LANGUAGESWITCHER_NAMES", languageSwitcherName);

			// Use flag images from portal's images folder?
			SettingItem customFlags = new SettingItem(new BooleanDataType());
			customFlags.Order = (int)SettingItemGroup.THEME_LAYOUT_SETTINGS + 950;
			customFlags.Group = SettingItemGroup.THEME_LAYOUT_SETTINGS;
			customFlags.EnglishName = "Use custom flags?";
			customFlags.Description = "Check this if you want to use custom flags from portal's images folder. Custom flags are located in portal folder. /images/flags/";
			customFlags.Value = "False";
			_baseSettings.Add("LANGUAGESWITCHER_CUSTOMFLAGS", customFlags);


			this.SupportsWorkflow = false;
		}

		public override Guid GuidID 
		{
			get
			{
				return new Guid("{25E3290E-3B9A-4302-9384-9CA01243C00F}");
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			InitializeComponent();

			// create ModuleTitle
//			ModuleTitle = new DesktopModuleTitle();
//			Controls.AddAt(0, ModuleTitle);

			LanguageSwitcher1 = new Esperantus.WebControls.LanguageSwitcher();
			Controls.Add(LanguageSwitcher1);

			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			//this.Cultures = LANGUAGE_DEFAULT; //completely wrong line! it removes culture check on this module and hides it :S
			this.Load += new System.EventHandler(this.LanguageSwitcher_Load);
		}
		#endregion

		private void LanguageSwitcher_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				LanguageSwitcher1.LanguageListString = GetLanguageCultureList().ToString();
				LanguageSwitcher1.ChangeLanguageAction = LanguageSwitcherAction.LinkRedirect;
				LanguageSwitcher1.Type = (LanguageSwitcherType) Enum.Parse(typeof(LanguageSwitcherType), Settings["LANGUAGESWITCHER_TYPES"].ToString());
				LanguageSwitcher1.Flags = (LanguageSwitcherDisplay) Enum.Parse(typeof(LanguageSwitcherDisplay), Settings["LANGUAGESWITCHER_FLAGS"].ToString());
				LanguageSwitcher1.Labels = (LanguageSwitcherDisplay) Enum.Parse(typeof(LanguageSwitcherDisplay), Settings["LANGUAGESWITCHER_LABELS"].ToString());
				LanguageSwitcher1.ShowNameAs = (LanguageSwitcherName) Enum.Parse(typeof(LanguageSwitcherName), Settings["LANGUAGESWITCHER_NAMES"].ToString());

				if (bool.Parse(Settings["LANGUAGESWITCHER_CUSTOMFLAGS"].ToString()))
					LanguageSwitcher1.ImagePath = portalSettings.PortalFullPath + "/images/flags/";
				else
					LanguageSwitcher1.ImagePath = Rainbow.Settings.Path.WebPathCombine (Rainbow.Settings.Path.ApplicationRoot, "aspnet_client/flags/");
			}
		}
	}
}