using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using System.Text;

using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes; 
using Rainbow.Configuration;


namespace Rainbow.DesktopModules 
{

	/// <summary>
	/// BreadCrumbs Module
	/// </summary>
	public class BreadCrumbs : PortalModuleControl 
	{
		protected Rainbow.UI.WebControls.BreadCrumbs BreadCrumbs1;
		/// <summary>
		/// Public constructor. Sets base settings for module.
		/// </summary>
		public BreadCrumbs() 
		{
		}

		/// <summary>
		/// The Page_Load event handler on this User Control is used to
		/// databind the used BreadCrumbsControl.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e)
		{
			//BreadCrumbs1.DataBind();
		}


		#region general Module Implementation
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{D3182CD6-DAFF-4E72-AD9E-0B28CB44F007}");
			}
		}

	
		#endregion


		#region Web Form Designer generated code
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	}
}
