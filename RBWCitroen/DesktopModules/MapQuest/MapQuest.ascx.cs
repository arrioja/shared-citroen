using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;

using Rainbow.UI;
using Rainbow.UI.DataTypes; 
using Rainbow.UI.WebControls;
using Rainbow.Configuration; 
using Esperantus;


namespace Rainbow.DesktopModules 
{

    /// <summary>
	/// MapQuest module
	/// Written by: Shaun Walker (released the module in VB.NET)
	/// Moved into Rainbow by Jakob Hansen, hansen3000@hotmail.com
	/// </summary>
    public class MapQuest : PortalModuleControl 
    {
		protected System.Web.UI.WebControls.Label lblLocation;
		protected System.Web.UI.WebControls.Label lblAddress;
		protected System.Web.UI.WebControls.HyperLink hypMap;
		protected System.Web.UI.WebControls.HyperLink hypDirections;

		protected bool showMap;
		protected System.Web.UI.WebControls.RadioButtonList RadioButtonList1;
		protected System.Web.UI.WebControls.Literal Literal1;
		protected bool showAddress;

        /// <summary>
        /// The Page_Load event handler on this User Control uses
        /// the Portal configuration system to obtain the MapQuest picture
        /// using the string "mqmapgend" starting point for the image URL.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e)
		{
            
			showMap = "True" == Settings["ShowMap"].ToString();
			showAddress = "True" == Settings["ShowAddress"].ToString();

			if (!Page.IsPostBack)
			{
				lblLocation.Text = Settings["Location"].ToString();
				if (showAddress)
				{
					if (Settings["Street"].ToString() != string.Empty)
						lblAddress.Text += Settings["Street"].ToString() + "<br>";

					if (Settings["Region"].ToString() != string.Empty)
						lblAddress.Text += Settings["City"].ToString() + ", " + Settings["Region"].ToString() + "<br>";
					else
						lblAddress.Text += Settings["City"].ToString() + "<br>";

					if (Settings["PostalCode"].ToString() != string.Empty)
						lblAddress.Text += Settings["PostalCode"].ToString() + "<br>";

					if (Settings["Country"].ToString() != string.Empty)
						lblAddress.Text += Settings["Country"].ToString();
				}

				int zoom;
				try 
				{
					zoom = int.Parse (Settings["Zoom"].ToString()) - 1;
				}
				catch
				{
					zoom = 7;
				}
				RadioButtonList1.Items[zoom].Selected = true;
				if (!bool.Parse(Settings["ShowZoom"].ToString()))
				{
					RadioButtonList1.Visible = false;
					Literal1.Visible = false;
				}
				show ();
			}
        }

		private void show ()
		{
			hypMap.NavigateUrl = BuildMapURL();
			hypMap.Target = "_" + Settings["Target"].ToString();

			if (showMap)
				hypMap.ImageUrl = GetMapImageURL(hypMap.NavigateUrl);
			else
				hypMap.Text = "Show Map";

			hypDirections.Text = "Get Directions";
			hypDirections.NavigateUrl = BuildDirectionsURL();
		}

		private string EncodeValue(string strValue)
		{
			strValue = strValue.Replace("\n", string.Empty);
			strValue = strValue.Replace("\r", string.Empty);
			strValue = strValue.Replace("%", "%25");
			strValue = strValue.Replace("&", "%26");
			strValue = strValue.Replace("+", "%30");
			strValue = strValue.Replace(" ", "+");
			return strValue;
		}

		private string BuildMapURL()
		{
			string strURL;
			strURL = "http://www.mapquest.com/maps/map.adp";
			strURL += "?address=" + EncodeValue(Settings["Street"].ToString());
			strURL += "&city=" + EncodeValue(Settings["City"].ToString());
			strURL += "&state=" + EncodeValue(Settings["Region"].ToString());
			strURL += "&country=" + EncodeValue(Settings["Country"].ToString());
			strURL += "&zip=" + EncodeValue(Settings["PostalCode"].ToString());
			strURL += "&zoom=" + RadioButtonList1.SelectedItem.Value;
			return strURL;
		}
		
		private string GetMapImageURL(string strURL)
		{
			try
			{

				HttpWebRequest objRequest;
				objRequest = (HttpWebRequest)WebRequest.Create(strURL);
				HttpWebResponse objResponse;
				objResponse = (HttpWebResponse)objRequest.GetResponse();
				StreamReader sr; 
				sr = new StreamReader(objResponse.GetResponseStream());
				string strResponse = sr.ReadToEnd();
				sr.Close();

				int intPos1;
				int intPos2;

				intPos1 = strResponse.IndexOf("mqmapgend");
				intPos1 = strResponse.LastIndexOf("http://", intPos1);
				intPos2 = strResponse.IndexOf("\"", intPos1);

				return strResponse.Substring(intPos1, intPos2 - intPos1);
			}
			catch
			{
				//error accessing MapQuest website 
				return string.Empty;
			}
		}


		private string BuildDirectionsURL()
		{
			string strURL;
			strURL = "http://www.mapquest.com/directions/main.adp?go=1";
			strURL += "&2a=" + EncodeValue(Settings["Street"].ToString());
			strURL += "&2c=" + EncodeValue(Settings["City"].ToString()); 
			strURL += "&2s=" + EncodeValue(Settings["Region"].ToString()); 
			strURL += "&2y=" + EncodeValue(Settings["Country"].ToString()); 
			strURL += "&2z=" + EncodeValue(Settings["PostalCode"].ToString()); 
			/*
			If Request.IsAuthenticated Then 
			' if you have expanded the Users table in IBuySpy to include more demographic fields you can use them 
			' here to automate the Get Directions feature 

			' Obtain PortalSettings from Current Context 
			'Dim _portalSettings As PortalSettings = CType(HttpContext.Current.Items("PortalSettings"), PortalSettings) 

			'Dim objUser As New ASPNetPortal.UsersDB() 

			'Dim drUser As SqlDataReader = objUser.GetSingleUser(_portalSettings.PortalID, Int32.Parse(context.User.Identity.Name)) 
			'If drUser.Read Then 
			' strURL += "&1a=" & EncodeValue(drUser("Street").ToString) 
			' strURL += "&1c=" & EncodeValue(drUser("City").ToString) 
			' strURL += "&1s=" & EncodeValue(drUser("Region").ToString) 
			' strURL += "&1y=" & EncodeValue(drUser("Country").ToString) 
			' strURL += "&1z=" & EncodeValue(drUser("PostalCode").ToString) 
			'End If 
			'drUser.Close() 
			End If
			*/ 

			return strURL;
		}

        
		/// <summary>
		/// Contstructor
		/// </summary>
		public MapQuest()
        {
			SettingItem setLocation = new SettingItem(new StringDataType());
			setLocation.Required = true;
			setLocation.Order = 10;
			this._baseSettings.Add("Location", setLocation);

			SettingItem setStreet = new SettingItem(new StringDataType());
			setStreet.Required = false;
			setStreet.Order = 20;
			this._baseSettings.Add("Street", setStreet);

			SettingItem setCity = new SettingItem(new StringDataType());
			setCity.Required = true;
			setCity.Order = 30;
			this._baseSettings.Add("City", setCity);

			SettingItem setRegion = new SettingItem(new StringDataType());
			setRegion.Required = false;
			setRegion.Order = 40;
			setRegion.Value = string.Empty;  //Same as State for US
			this._baseSettings.Add("Region", setRegion);

			SettingItem setCountry = new SettingItem(new StringDataType());
			setCountry.Required = false;
			setCountry.Order = 50;
			this._baseSettings.Add("Country", setCountry);

			SettingItem setPostalCode = new SettingItem(new StringDataType());
			setPostalCode.Required = false;
			setPostalCode.Order = 60;
			this._baseSettings.Add("PostalCode", setPostalCode);

			SettingItem setShowMap = new SettingItem(new BooleanDataType());
			setShowMap.Order = 70;
			setShowMap.Value = "True";
			this._baseSettings.Add("ShowMap", setShowMap);

			SettingItem setShowAddress = new SettingItem(new BooleanDataType());
			setShowAddress.Order = 80;
			setShowAddress.Value = "False";
			this._baseSettings.Add("ShowAddress", setShowAddress);

			SettingItem setZoom = new SettingItem(new IntegerDataType());
			setZoom.Required = true;
			setZoom.Order = 90;
			setZoom.Value = "7";
			setZoom.MinValue = 1;
			setZoom.MaxValue = 10;
			this._baseSettings.Add("Zoom", setZoom);

			SettingItem setShowZoom = new SettingItem(new BooleanDataType());
			setShowZoom.Order = 100;
			setShowAddress.Value = "False";
			this._baseSettings.Add("ShowZoom", setShowZoom);

			SettingItem setTarget = new SettingItem(new ListDataType("blank;parent;self;top"));
			setTarget.Required = true;
			setTarget.Order = 110;
			setTarget.Value = "blank";
			this._baseSettings.Add("Target", setTarget);
		}


        public override Guid GuidID 
        {
            get
            {
                return new Guid("{2502DB18-B580-4F90-8CB4-C15E6E531016}");
            }
        }

		#region Web Form Designer generated code
        /// <summary>
        /// On init
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
			InitializeComponent();
			base.OnInit(e);
		}

        /// <summary>
        ///	Required method for Designer support - do not modify
        ///	the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() 
        {
			this.RadioButtonList1.SelectedIndexChanged += new System.EventHandler(this.zoom_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void zoom_Click(object sender, System.EventArgs e)
		{
			show ();
		}

    }
}
