using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow.Configuration;
using Rainbow.Security;

namespace Rainbow.DesktopModules.FCK.filemanager.upload.aspx
{
	/// <summary>
	/// upload files to server.
	/// </summary>
	[History("jviladiu@portalservices.net", "2004/06/09", "First Implementation FCKEditor in Rainbow")]
	public class upload : Rainbow.UI.EditItemPage 
	{

		protected override void LoadSettings()
		{
			if (PortalSecurity.HasEditPermissions(this.portalSettings.ActiveModule) == false)
				PortalSecurity.AccessDeniedEdit();
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Request.Files.Count > 0)
			{
				System.Web.HttpPostedFile oFile = Request.Files.Get("FCKeditor_File") ;
	
				string fileName = oFile.FileName.Substring(oFile.FileName.LastIndexOf("\\") + 1);
				Hashtable ms = ModuleSettings.GetModuleSettings(portalSettings.ActiveModule);
				string DefaultImageFolder = "default";
				if (ms["MODULE_IMAGE_FOLDER"] != null) 
				{
					DefaultImageFolder = ms["MODULE_IMAGE_FOLDER"].ToString();
				}
				else if (portalSettings.CustomSettings["SITESETTINGS_DEFAULT_IMAGE_FOLDER"] != null) 
				{
					DefaultImageFolder = portalSettings.CustomSettings["SITESETTINGS_DEFAULT_IMAGE_FOLDER"].ToString();
				}
				string sFileURL  = portalSettings.PortalFullPath + "/images/" + DefaultImageFolder + "/" + fileName;
				string sFilePath = Server.MapPath(sFileURL) ;
			
				oFile.SaveAs(sFilePath) ;
			
				Response.Write("<SCRIPT language=javascript>window.opener.setImage('" + sFileURL + "') ; window.close();</" + "SCRIPT>") ;
			}
		}

		#region Código generado por el Diseñador de Web Forms
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
