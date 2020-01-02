/*
 * This code is released under Duemetri Public License (DPL) Version 1.2.
 * Original Coder: Indah Fuldner [indah@die-seitenweber.de]
 * modified by Mario Hartmann [mario@hartmann.net // http://mario.hartmann.net/]
 * Version: C#
 * Product name: Rainbow
 * Official site: http://www.rainbowportal.net
 * Last updated Date: 04/JUN/2004
 * Derivate works, translation in other languages and binary distribution
 * of this code must retain this copyright notice and include the complete 
 * licence text that comes with product.
*/

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules 
{
	/// <summary>
	/// 
	/// </summary>
	public class FlashEdit: Rainbow.UI.AddEditItemPage
	{
		protected System.Web.UI.WebControls.TextBox Src;
		protected System.Web.UI.WebControls.TextBox Width;
		protected System.Web.UI.WebControls.TextBox Height;
		protected System.Web.UI.WebControls.TextBox BackgroundCol;
   
		protected Esperantus.WebControls.Literal Literal2;
		protected Esperantus.WebControls.Literal Literal1;
		protected Esperantus.WebControls.Literal Literal3;
		protected Esperantus.WebControls.Literal Literal4;
		protected Esperantus.WebControls.Literal Literal5;
  		protected Esperantus.WebControls.HyperLink showGalleryButton;

		public string showGallery=string.Empty;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e) 
		{    
			// Form the script that is to be registered at client side.
			string scriptString = "<script language=JavaScript>function newWindow(file,window) {";
			scriptString += "msgWindow=open(file,window,'resizable=yes,width=600,height=500,scrollbars=yes');";
			scriptString += "if (msgWindow.opener == null) msgWindow.opener = self;";
			scriptString += "}</script>";

			if(!this.IsClientScriptBlockRegistered("newWindow"))
				this.RegisterClientScriptBlock("newWindow", scriptString);

			showGalleryButton.NavigateUrl = "javascript:newWindow('UploadFlash.aspx?FieldID=Src&mID=" + ModuleID + "','gallery')";
      
			if (Page.IsPostBack == false) 
			{
				if (this.ModuleID > 0) 
				{
					Hashtable settings;
                
					// Get settings from the database
					settings = Rainbow.Configuration.ModuleSettings.GetModuleSettings(ModuleID);
                
					if ( settings["src"] != null)
						Src.Text =  settings["src"].ToString();
					if ( settings["width"] != null)
						Width.Text =  settings["width"].ToString();
					if ( settings["height"] != null)
						Height.Text = settings["height"].ToString();     
					if ( settings["backcolor"] != null)
						BackgroundCol.Text = settings["backcolor"].ToString();     
				}
            
				// Store URL Referrer to return to portal
				ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
			}
		}

		/// <summary>
		/// Set the module guids with free access to this page
		/// </summary>
		protected override ArrayList AllowedModules
		{
			get
			{
				ArrayList al = new ArrayList();
				al.Add ("623EC4DD-BA40-421c-887D-D774ED8EBF02");
				return al;
			}
		}

		/// <summary>
		/// The UpdateBtn_Click event handler on this Page is used to save
		/// the settings to the ModuleSettings database table.  It  uses the 
		/// Rainbow.DesktopModulesDB() data component to encapsulate the data 
		/// access functionality.
		/// </summary>
		/// <param name="e"></param>
		override protected void OnUpdate(EventArgs e) 
		{
			base.OnUpdate(e);
			
			// Update settings in the database
			ModuleSettings.UpdateModuleSetting(ModuleID, "src", Src.Text);
			ModuleSettings.UpdateModuleSetting(ModuleID, "height", Height.Text);
			ModuleSettings.UpdateModuleSetting(ModuleID, "width", Width.Text);
			ModuleSettings.UpdateModuleSetting(ModuleID, "backcolor", BackgroundCol.Text);
   
			// Redirect back to the portal home page
			Response.Redirect((string) ViewState["UrlReferrer"]);
		}

		/// <summary>
		/// The CancelBtn_Click event handler on this Page is used to cancel
		/// out of the page, and return the user back to the portal home page.
		/// </summary>
		/// <param name="e"></param>
		override protected void OnCancel(EventArgs e) 
		{
			base.OnCancel(e);
			// Redirect back to the portal home page
			Response.Redirect((string) ViewState["UrlReferrer"]);
		}
      
		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnInit(EventArgs e)
		{  
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.           
			InitializeComponent();
			base.OnInit(e);
		}


		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() 
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
