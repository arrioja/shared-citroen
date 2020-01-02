using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// The IframeModule provides an IFRAME where you can set the
	/// source URL and the height of the frame using the settings system.
	/// Default height is 200px and URL is http://www.rainbowportal.net
	/// Written by: Jakob Hansen, hansen3000@hotmail
	/// </summary>
	public class IframeModule : PortalModuleControl
	{
		protected System.Web.UI.WebControls.Literal LiteralIframe;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			string strURL = Settings["URL"].ToString();
			string height = Settings["Height"].ToString();
			string width = Settings["Width"].ToString();
			StringBuilder sb = new StringBuilder();
			sb.Append("<iframe");
			sb.Append(" src='"); sb.Append(strURL); sb.Append("'");
			sb.Append(" width='"); sb.Append(width); sb.Append("'");
			sb.Append(" height='"); sb.Append(height); sb.Append("'");
			sb.Append(" title='"); sb.Append(this.TitleText); sb.Append("'");
			sb.Append(">");
			sb.Append("</iframe>");

			LiteralIframe.Text = sb.ToString();
		}

		public override Guid GuidID 
		{
			get
			{
				return new Guid("{2502DB18-B580-4F90-8CB4-C15E6E531005}");
			}
		}

		
		public IframeModule() 
		{
			//MH:canged to support relativ url
			//SettingItem url = new SettingItem(new UrlDataType());
			SettingItem url = new SettingItem(new StringDataType());
			url.Required = true;
			url.Order = 1;
			url.Value = "http://www.rainbowportal.net";
			this._baseSettings.Add("URL", url);

			//MH: added to support width values
			SettingItem width = new SettingItem(new StringDataType());
			width.Required = true;
			width.Order = 2;
			width.Value = "250";
			//width.MinValue = 1;
			//width.MaxValue = 2000;
			this._baseSettings.Add("Width", width);

			//MH: changed to StringDataType to support  percent or pixel values
			//SettingItem width = new SettingItem(new IntegerDataType());
			SettingItem height = new SettingItem(new StringDataType());
			height.Required = true;
			height.Order = 3;
			height.Value = "250";
			//height.MinValue = 1;
			//height.MaxValue = 2000;
			this._baseSettings.Add("Height", height);
		}


		#region Web Form Designer generated code
		/// <summary>
		/// Raises Init event
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();

			// Create a new Title the control
//			ModuleTitle = new DesktopModuleTitle();
			// Add title ad the very beginning of 
			// the control's controls collection
//			Controls.AddAt(0, ModuleTitle);
		
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
