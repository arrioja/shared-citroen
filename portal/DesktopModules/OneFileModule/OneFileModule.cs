using System;
using System.Web.UI;
using System.Xml;
using System.Xml.XPath;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Esperantus;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules 
{
    /// <summary>
    /// The OneFileModule provides basis for a module that only
    /// consists of a single .ascx file. 
    /// See the OneFileModule Kit for documentation and examples.
	/// Written by: Jakob Hansen, hansen3000@hotmail
	/// </summary>
    public class OneFileModule : PortalModuleControl 
    {

		public enum SettingsType {Off, Str, Xml, StrAndXml}   // With StrAndXml SettingsStr are searched first
		protected SettingsType _settingsType = SettingsType.Off;
		
		protected string _settingsStr = string.Empty;      // The content of setting "Settings string"
		protected string _xmlFileName = string.Empty;      // The filename in setting "XML settings file"
		protected XmlDocument _settingsXml;      // The XML in file _xmlFileName
		protected bool _debugMode = false;       // True if setting "Debug Mode" is clicked
		protected bool _settingsExists = false;  // False if settings are missing


		/// <summary>
		/// Same as "Settings string" in the setting system
		/// </summary>
		public string SettingsStr
		{
			get {return _settingsStr;} 
		}

		/// <summary>
		/// Same as "XML settings file" in the setting system
		/// </summary>
		public string XmlFileName
		{
			get {return _xmlFileName;} 
		}

		/// <summary>
		/// The XML in file XmlFileName
		/// </summary>
		public XmlDocument SettingsXml
		{
			get {return _settingsXml;} 
		}

		/// <summary>
		/// Same as "Debug Mode" in the setting system.
		/// </summary>
		public bool DebugMode
		{
			get {return _debugMode;} 
			set {_debugMode=value;}
		}

		/// <summary>
		/// False if settings are missing
		/// </summary>
		public bool SettingsExists
		{
			get {return _settingsExists;} 
		}


		/// <summary>
		/// Fills all settings: SettingsStr, SettingsXml and DebugMode
		/// InitSettings() should be the first line of code in Page_Load().
		/// If settings is missing settingsExists is set to false
		/// Note: It is not mandatory to use InitSettings() in the 
		/// Page_Load() - The programmer can just leave it out if he 
		/// decides that he is not going to use the setting system that
		/// class OneFileModule provides.
		/// </summary>
		protected void InitSettings(SettingsType settingsType)
		{
			_settingsType = settingsType;
			if (_settingsType == SettingsType.Off)
				return;

			_debugMode = "True" == Settings["Debug Mode"].ToString();
			_xmlFileName = Settings["XML settings file"].ToString();

			_settingsExists = true;

			if (_settingsType == SettingsType.Str || _settingsType == SettingsType.StrAndXml)
			{
				_settingsStr = Settings["Settings string"].ToString();
				if (_settingsStr == string.Empty)
				{
					_settingsExists = false;
					Controls.Add(new LiteralControl("<br><span class='Error'>Settings string is missing</span><br>"));
				}
			}
		
			if (_settingsType == SettingsType.Xml || _settingsType == SettingsType.StrAndXml)
			{
				_settingsXml = new System.Xml.XmlDocument();
				if (GetSettingsXml(ref _settingsXml, _xmlFileName) == false)
				{
					_settingsExists = false;
					Controls.Add(new LiteralControl("<br>" + "<span class='Error'>XML " + Esperantus.Localize.GetString("FILE_NOT_FOUND").Replace("%1%", _xmlFileName) + "<br>"));
				}
			}
		
		}

		/// <summary>
		/// Creates 3 fields in the settings system: 
		/// "Settings string", "XML settings file" and "Debug Mode"
		/// </summary>
		public OneFileModule()
        {
			SettingItem setting = new SettingItem(new StringDataType());
			setting.Required = false;
			setting.Order = 1;
			setting.Value = string.Empty;
			this._baseSettings.Add("Settings string", setting);

			SettingItem xmlFile = new SettingItem(new PortalUrlDataType());
			xmlFile.Required = false;
			xmlFile.Order = 2;
			this._baseSettings.Add("XML settings file", xmlFile);
		
			SettingItem debugMode = new SettingItem(new BooleanDataType());
			debugMode.Order = 3;
			debugMode.Value = "True";
			this._baseSettings.Add("Debug Mode", debugMode);
		}


		/// <summary>
		/// Get the setting value from SettingsStr. 
		/// If not found in SettingsStr then SettingsXml are searched.
		/// This function uses GetStrSetting() and GetXmlSetting
		/// </summary>
		/// <param name="settingName"></param>
		/// <returns></returns>
		protected string GetSetting(string settingName)
		{
			if (_settingsType == SettingsType.Off)
				return string.Empty;

			string retVal;
			retVal = GetStrSetting(settingName);
			if (retVal == string.Empty)
				retVal = GetXmlSetting(settingName);
			return retVal;
		}
		

		/// <summary>
		/// Get the setting value from SettingsStr which has the form: 
		/// nameA=valueA;nameB=valueB;nameC=valueC
		/// </summary>
		/// <param name="settingName"></param>
		/// <returns></returns>
		protected string GetStrSetting(string settingName)
		{
			if (_settingsType == SettingsType.Off || 
				_settingsType == SettingsType.Xml || 
				_settingsStr == string.Empty)
				return string.Empty;
			
			int idxStart = _settingsStr.IndexOf(settingName);
			if (idxStart == -1)
				return string.Empty;

			idxStart = _settingsStr.IndexOf('=', idxStart);
			if (idxStart == -1)
				return string.Empty;

			string val;
			int idxEnd = _settingsStr.IndexOf(';', idxStart);
			if (idxEnd == -1)
			{
				if (_settingsStr.Substring(idxStart).Length == 0)
					val = string.Empty;
				else
					val = _settingsStr.Substring(++idxStart);
			}
			else
			{
				idxStart++;
				val = _settingsStr.Substring(idxStart, (idxEnd-idxStart));
			}

			return val;
		}


		/// <summary>
		/// Fills the settingsXml parameter with the xml from a file
		/// </summary>
		/// <param name="settingsXml"></param>
		/// <param name="file"></param>
		/// <returns></returns>
		protected bool GetSettingsXml(ref XmlDocument settingsXml, string file)
		{
			bool retValue = true;

			PortalUrlDataType pt = new PortalUrlDataType();
			pt.Value = file;
			string xmlFile = pt.FullPath;
		
			if ((xmlFile != null) && (xmlFile != string.Empty)) 
			{
				if  (System.IO.File.Exists(Server.MapPath(xmlFile))) 
				{
					settingsXml.Load(Server.MapPath(xmlFile));
				}
				else 
				{
					retValue = false;
				}
			}
			return retValue;
		}


		/// <summary>
		/// Get the setting value from the XML Document SettingsXml.
		/// settingName is a XPath expression.
		/// </summary>
		/// <param name="settingName"></param>
		/// <returns></returns>
		protected string GetXmlSetting(string settingName)
		{
			if (_settingsType == SettingsType.Off || _settingsType == SettingsType.Str)
				return string.Empty;
			
			string val = string.Empty;

			// Add default root to the xpath expression if missing
			if (settingName.IndexOf('/') == -1)
				settingName = "settings/" + settingName;

			XmlNodeReader xmlNodeReader = new XmlNodeReader (_settingsXml.SelectSingleNode(settingName));

			try
			{
				if (xmlNodeReader.Read())
				{
					// If we get here the setting is in the xml file
					// Move to next node (the text node containg the actual setting value)
					if (xmlNodeReader.Read())  
						val = xmlNodeReader.Value;
				}
			}
			finally
			{
				xmlNodeReader.Close(); //by Manu, fixed bug 807858
			}


			return val;
		}

    }
}