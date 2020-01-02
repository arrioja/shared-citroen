using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow;
using Rainbow.Admin;
using Rainbow.Security;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.Configuration;
using Rainbow.KickStarter.CommonClasses;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// Summary description for Property Page
	/// </summary>
	public class AddEditPage : Rainbow.UI.AddEditItemPage
	{
		protected Esperantus.WebControls.Literal Literal1;
		protected System.Web.UI.WebControls.PlaceHolder AddEditControlPlaceHolder;
		protected IEditModule AddEditControl;

		#region Web Form Designer generated code
        /// <summary>
        /// On init
        /// </summary>
        /// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			InitializeControl();
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}

		/// <summary>
		/// Purpose: Method to initialize the control.
		/// </summary>
		private void InitializeControl() 
		{
			if (ViewState["AddEditControl"] == null)
			{
				PortalModuleControl myControl = (PortalModuleControl) this.LoadControl(Rainbow.Settings.Path.ApplicationRoot + "/" + this.Module.DesktopSrc);
				ViewState["AddEditControl"] = (IEditModule) this.LoadControl(myControl.AddModuleControl);
				AddEditControlPlaceHolder.Controls.Add((Control) ViewState["AddEditControl"]);
			}
			AddEditControl = (IEditModule) ViewState["AddEditControl"];
			//Attach events
			AddEditControl.DataActionStart += new Rainbow.KickStarter.CommonClasses.DataChangeEventHandler(this.EditControl_DataActionStart);
			AddEditControl.DataActionEnd += new Rainbow.KickStarter.CommonClasses.DataChangeEventHandler(this.EditControl_DataActionEnd);
			AddEditControl.CancelEdit += new EventHandler(this.EditControl_CancelEdit);
		}
		#endregion

        private void Page_Load(object sender, System.EventArgs e)
        {
			//Check permissions and enable/disable buttons accordingly
			if (!PortalSecurity.IsInRoles("Admins"))
			{
				AddEditControl.AllowAdd = PortalSecurity.HasAddPermissions(ModuleID);
				AddEditControl.AllowDelete = PortalSecurity.HasDeletePermissions(ModuleID);
				AddEditControl.AllowUpdate = PortalSecurity.HasEditPermissions(ModuleID);
			}

			if (!IsPostBack)
			{
				if (AddEditControl.AllowUpdate && ItemID > 0) //If editing 
					AddEditControl.StartEdit(ItemID.ToString());
			}
        }

		/// <summary>
		/// Purpose: Method to Handle the EditControl actions events.
		/// </summary>
		/// <param name="sender" type="Rainbow.DesktopModules.WTS.BusinessLayer.Games"></param>
		/// <param name="eventArgs" type="DataChangeEventArgs"></param>
		protected void EditControl_DataActionStart(object sender, DataChangeEventArgs eventArgs) 
		{
		}

		/// <summary>
		/// Purpose: Method to Handle the EditControl actions events.
		/// </summary>
		/// <param name="sender" type="Rainbow.DesktopModules.WTS.BusinessLayer.Games"></param>
		/// <param name="eventArgs" type="DataChangeEventArgs"></param>
		protected void EditControl_DataActionEnd(object sender, DataChangeEventArgs eventArgs) 
		{
			OnUpdate(null);

			// Redirect back to the portal home page
			this.RedirectBackToReferringPage();
		}

		/// <summary>
		/// Cancel
		/// </summary>
		/// <param name="sender" type="object"></param>
		/// <param name="eventArgs" type="EventArgs"></param>
		protected void EditControl_CancelEdit(object sender, EventArgs eventArgs) 
		{
			OnCancel(null);
		}
	}
}