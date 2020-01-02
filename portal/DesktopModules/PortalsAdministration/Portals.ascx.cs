using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.Security;
using Esperantus;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules
{
    /// <summary>
    /// Module to manage portals (AdminAll)
    /// </summary>
    public class Portals : PortalModuleControl
    {
		protected System.Web.UI.WebControls.ListBox portalList;

		/// <summary>
        /// 
        /// </summary>
        protected ArrayList portals;
   		
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
        ///  to populate the current portals list from the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e) 
        {
            portals = new ArrayList();
            PortalsDB portalsDb = new PortalsDB();
            SqlDataReader dr = portalsDb.GetPortals();
			try
			{
				while (dr.Read())
				{
					PortalItem p = new PortalItem();
					p.Name = dr["PortalName"].ToString();
					p.Path = dr["PortalPath"].ToString();
					p.ID = Convert.ToInt32(dr["PortalID"].ToString());
					portals.Add(p);
				}
			}
			finally
			{
				dr.Close(); //by Manu, fixed bug 807858
			}
                
            // If this is the first visit to the page, bind the tab data to the page listbox
            if (Page.IsPostBack == false) 
            {
                portalList.DataBind();
            }
        }
       
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{366C247D-4CFB-451D-A7AE-649C83B05841}");
			}
		}

		#region Web Form Designer generated code
        /// <summary>
        /// Raises OnInit event
        /// </summary>
        /// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			base.OnInit(e);
			InitializeComponent();

			// Add a link for the edit page
			this.AddText = "ADD_PORTAL";
			this.AddUrl = "~/DesktopModules/PortalsAdministration/AddNewPortal.aspx";
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() 
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

        /// <summary>
        /// OnDelete
        /// </summary>
        override protected void OnDelete()
        {
            if (portalList.SelectedIndex != -1)
            {
                try
                {
                    // must delete from database too
                    PortalItem p = (PortalItem) portals[portalList.SelectedIndex];
                    PortalsDB portalsdb = new PortalsDB();
                    //Response.Write("Will delete " + p.Name);
                    portalsdb.DeletePortal(p.ID);
                        
                    // remove item from list
                    portals.RemoveAt(portalList.SelectedIndex);
                    // rebind list
                    portalList.DataBind();
                }
                catch (System.Data.SqlClient.SqlException sqlex)
                {
					Rainbow.Configuration.ErrorHandler.HandleException(Esperantus.Localize.GetString("DELETE_PORTAL_ERROR", "There was an error on deleting the portal", this), sqlex);
                    Controls.Add(new LiteralControl("<br><span class=NormalRed>" + Esperantus.Localize.GetString("DELETE_PORTAL_ERROR", "There was an error on deleting the portal", this) + "<br>"));
                }
            }
            base.OnDelete();
        }

        /// <summary>
        /// OnEdit
        /// </summary>
        override protected void OnEdit()
        {
            if (portalList.SelectedIndex != -1)
            {
                // must delete from database too
                PortalItem p = (PortalItem) portals[portalList.SelectedIndex];

                //Add new portal
				// added mID by Mario Endara <mario@softworks.com.uy> to support security check (2004/11/09)
				Response.Redirect(HttpUrlBuilder.BuildUrl("~/DesktopModules/PortalsAdministration/EditPortal.aspx", 0, 
														"PortalID=" + p.ID + "&mID=" + ModuleID.ToString()));
            }
            base.OnEdit();
        }
    }
}