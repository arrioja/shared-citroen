using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules 
{
	public class ImageModule : PortalModuleControl 
	{
		protected System.Web.UI.WebControls.Image Image1;

		/// <summary>
		/// The Page_Load event handler on this User Control uses
		/// the Portal configuration system to obtain image details.
		/// It then sets these properties on an &lt;asp:Image&gt; server control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e) 
		{
			string imageSrc = Rainbow.Settings.Path.WebPathCombine(Rainbow.Settings.Path.ApplicationRoot, portalSettings.PortalPath, (string) Settings["src"].ToString());
			string imageHeight = Settings["height"].ToString();
			string imageWidth = Settings["width"].ToString();

			// Set Image Source, Width and Height Properties
			if ((imageSrc != null) && (imageSrc != string.Empty)) 
			{
				Image1.ImageUrl = imageSrc;
			}

			if ((imageWidth != null) && (imageWidth.Length > 0) && (int.Parse(imageWidth) > 0)) 
			{
				Image1.Width = int.Parse(imageWidth);
			}

			if ((imageHeight != null) && (imageHeight.Length > 0) && (int.Parse(imageHeight) > 0)) 
			{
				Image1.Height = int.Parse(imageHeight);
			}
		}
 
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{BCF1F338-4564-461C-9606-CB024D10294E}");
			}
		}

		public ImageModule() 
		{                
			SettingItem src = new SettingItem(new UploadedFileDataType()); //PortalUrlDataType
			src.Required = true;
			src.Order = 1;
			this._baseSettings.Add("src", src);

			SettingItem width = new SettingItem(new IntegerDataType());
			width.Required = true;
			width.MinValue = 0;
			width.MaxValue = 2048;
			width.Value = "150";
			width.Order = 2;
			this._baseSettings.Add("width", width);

			SettingItem height = new SettingItem(new IntegerDataType());
			height.Required = true;
			height.MinValue = 0;
			height.MaxValue = 2048;
			height.Value = "250";
			height.Order = 1;
			this._baseSettings.Add("height", height);
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
