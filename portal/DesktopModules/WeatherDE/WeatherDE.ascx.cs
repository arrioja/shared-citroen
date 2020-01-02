using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;

using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.Configuration;
using Rainbow.UI.DataTypes;


namespace Rainbow.DesktopModules
{
	/// <summary>
	/// WeatherDE (German Weather) using german zipcodes.
	/// Adapted from original version by: Mario Hartmann, Mario@Hartmann.net
	///
	/// USWeather
	/// Written by: Jason Schaitel, Jason_Schaitel@hotmail.com
	/// Moved into Rainbow by Jakob Hansen, hansen3000@hotmail.com
	/// </summary>
	public class WeatherDE : PortalModuleControl
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl pWeatherDE;

		public enum WeatherOption
		{
			Today,Forecast
		}


		public WeatherDE() 
		{
			SettingItem setZip = new SettingItem(new DoubleDataType());
			setZip.MinValue = 0;
			setZip.MaxValue = 99999;
			setZip.Required = true;
			setZip.Value = "88045";
			setZip.Order = 1;
			this._baseSettings.Add("WeatherZip", setZip);


			// Module Weather Options
			ArrayList ModuleWeatherOption = new ArrayList();
			ModuleWeatherOption.Add(new SettingOption((int)WeatherOption.Today, "Today"));
			ModuleWeatherOption.Add(new SettingOption((int)WeatherOption.Forecast , "Forecast"));

			SettingItem setOption = new SettingItem(new CustomListDataType(ModuleWeatherOption, "Name", "Val"));
			setOption.Required = true;
			setOption.Value = ((int)WeatherOption.Today).ToString();
			setOption.Order = 2;
			this._baseSettings.Add("WeatherOption", setOption);


			// Module Weather Design
			ArrayList ModuleWeatherDesignValue = new ArrayList();

			ModuleWeatherDesignValue.Add(new SettingOption(0, "1"));
			ModuleWeatherDesignValue.Add(new SettingOption(1 ,"1b"));
			ModuleWeatherDesignValue.Add(new SettingOption(2 ,"1c"));
			ModuleWeatherDesignValue.Add(new SettingOption(3 ,"2"));
			ModuleWeatherDesignValue.Add(new SettingOption(4 ,"2b"));
			ModuleWeatherDesignValue.Add(new SettingOption(5 ,"3"));
 
			SettingItem setDesign = new SettingItem(new  CustomListDataType(ModuleWeatherDesignValue, "Name", "Name"));
			setDesign.Required = true;
			setDesign.Value = "1";
			setDesign.Order = 3;
			this._baseSettings.Add("WeatherDesign", setDesign);

			// Module Weather CityIndex		
			SettingItem setCityIndex = new SettingItem(new DoubleDataType());
			setCityIndex.MinValue = 0;
			setCityIndex.MaxValue = 99999;
			setCityIndex.Required = false;
			setCityIndex.Value = "0";
			setCityIndex.Order = 4;
			this._baseSettings.Add("WeatherCityIndex", setCityIndex);
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
			string strZip ="88045";
			string strCityIndex = "0";
			string strDesign = "1";
			string strTempOption = "0";
			string strOption ="C";

			if (Settings["WeatherZip"]!=null) 
				strZip = Settings["WeatherZip"].ToString() ;
			
			if (Settings["WeatherCityIndex"]!= null)
				strCityIndex = Settings["WeatherCityIndex"].ToString();
			
			if (Settings["WeatherDesign"]!=null) 
				strDesign = Settings["WeatherDesign"].ToString();
			
			if (Settings["WeatherSetting"]!=null) 
				strTempOption =  Settings["WeatherSetting"].ToString();
			
			if (strTempOption == "1")
				strOption = "F";
			//			else 
			//				strOption = "C";


			string MyHTML =string.Empty;
			MyHTML +=  "<!-- BEGIN wetter.com-Button -->"   ;
			MyHTML += "<a href='http://www.wetter.com/home/extern/ex_search.php?ms=1&ss=1&sss=2&search=" + strZip + "' target='_new'>";
			MyHTML += "<img src='http://www.wetter.com/home/woys/woys.php?," + strOption + "," + strDesign + ",DEPLZ," + strZip + ","+ strCityIndex + "' border='0'></a>";
			MyHTML +=  "<!-- END wetter.com-Button -->";

			pWeatherDE.InnerHtml = MyHTML;
		}


		#region general Module Implementation
	
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{D3182CD6-DAFF-4E72-AD9E-0B28CB44F000}");
			}
		}


		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			this.EditText = "EDIT";
			this.EditUrl ="~/DesktopModules/WeatherDE/WeatherDEEdit.aspx";
			base.OnInit(e);
		}
		
		/// <summary>
		///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
