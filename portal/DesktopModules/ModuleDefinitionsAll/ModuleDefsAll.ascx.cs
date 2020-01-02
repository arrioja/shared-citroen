using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Rainbow;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.Security;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules
{
    /// <summary>
    /// Control to show/edit portals modules (AdminAll)
    /// </summary>
    public class ModuleDefsAll : PortalModuleControl 
    {
        protected System.Web.UI.WebControls.DataList defsList;

		/// <summary>
		/// Admin Module
		/// </summary>
		public override bool AdminModule
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// The Page_Load server event handler on this user control is used
		/// to populate the current defs settings from the configuration system
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e) 
        {
            if (!Page.IsPostBack) 
                BindData();
        }
    
		/// <summary>
		/// The BindData helper method is used to bind the list of 
		/// module definitions for this portal to an asp:datalist server control
		/// </summary>
        private void BindData() 
        {
            // Get the portal's defs from the database
            defsList.DataSource = new ModulesDB().GetModuleDefinitions();
            defsList.DataBind();
        }
  
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{D04BB5EA-A792-4E87-BFC7-7D0ED3ADD582}");
			}
		}

		#region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
			InitializeComponent();
			this.AddUrl = "~/DesktopModules/ModuleDefinitions/ModuleDefinitions.aspx";
			base.OnInit(e);
		}

        /// <summary>
        ///	Required method for Designer support - do not modify
        ///	the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() 
        {
			this.defsList.ItemCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.defsList_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// The DefsList_ItemCommand server event handler on this page 
		/// is used to handle the user editing module definitions
		/// from the DefsList &lt;asp:datalist&gt; control
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void defsList_ItemCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e)
		{
			Guid GeneralModDefID = new Guid(defsList.DataKeys[e.Item.ItemIndex].ToString());

			// Go to edit page
			Response.Redirect(HttpUrlBuilder.BuildUrl("~/DesktopModules/ModuleDefinitions/ModuleDefinitions.aspx", TabID, "DefID=" + GeneralModDefID + "&Mid=" + ModuleID));
		}

    }
}