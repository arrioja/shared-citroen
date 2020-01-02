using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.Configuration;
using Rainbow.UI.DataTypes;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// Weather (Using USA zipcodes)
	/// Written by: Jason Schaitel, Jason_Schaitel@hotmail.com
	/// Moved into Rainbow by Jakob Hansen, hansen3000@hotmail.com
	/// </summary>
	public class WeatherUS : PortalModuleControl
	{
		protected System.Web.UI.HtmlControls.HtmlGenericControl pWeather;

		[History("mario@hartmann.net", "2003/06/11", "changed DataType of setZip to StringdataType to enable leading zeros in zip.")]
		public WeatherUS() 
		{
			SettingItem setZip = new SettingItem(new StringDataType());
			//setZip.MinValue = 0;
			//setZip.MaxValue = 99999;
			setZip.Required = true;
			setZip.Value = "10001";
			setZip.Order = 1;
			this._baseSettings.Add("Zip", setZip);

			SettingItem setOption = new SettingItem(new StringDataType());
			setOption.Required = true;
			setOption.Value = "0";
			setOption.Order = 2;
			this._baseSettings.Add("Option", setOption);
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			string Zip ="10001";
			string Option = "0";
			
			if (Settings["Zip"] != null)
				Zip = Settings["Zip"].ToString();
			if (Settings["Option"]!=null)
				Option = Settings["Option"].ToString();
			
			string MyHTML =string.Empty;

			MyHTML += "<a href='http://www.wx.com/myweather.cfm?ZIP=" + Zip + "' target='_new'>";
			if (Option== "0")
			{
				MyHTML += "<img src='http://www.wx.com/partnership/sticker.cfm?zip=" + Zip + "' border='0'></a>";
			}
			else
			{
				MyHTML += "<img src='http://www4.wx.com/partnership/miniradar_servlet.cfm?zip=" + Zip + "&size=0' border='0'></a>";
			}

			pWeather.InnerHtml = MyHTML;
		}


		public override Guid GuidID 
		{
			get
			{
				return new Guid("{2502DB18-B580-4F90-8CB4-C15E6E531006}");
			}
		}



		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			this.EditText = "EDIT";
			this.EditUrl ="~/DesktopModules/WeatherUS/WeatherUSEdit.aspx";
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
