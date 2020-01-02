using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Rainbow.Security;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules
{
    /// <summary>
    /// Module that shows available modules for current portal (readonly)
    /// </summary>
    public class ModuleDefs : PortalModuleControl
    {
		protected System.Web.UI.WebControls.DataList userModules;
		protected System.Web.UI.WebControls.DataList adminModules;
		protected Esperantus.WebControls.Literal userTitle;
		protected Esperantus.WebControls.Literal adminTitle;

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
            // If this is the first visit to the page, bind the definition data to the datalist
            if (Page.IsPostBack == false) {
                BindData();
			}
        }

        /// <summary>
        /// The BindData helper method is used to bind the list of 
        /// module definitions for this portal to an asp:datalist server control
        /// </summary>
        protected void BindData()
        {
            // Get the portal's defs from the database
            SqlDataReader dr = new ModulesDB().GetCurrentModuleDefinitions(portalSettings.PortalID);
 
			DataTable userTable = new DataTable();
			userTable.Columns.Add (new DataColumn("FriendlyName", typeof(string)));
			userTable.Columns.Add (new DataColumn("ModuleDefID", typeof(string)));

			DataTable adminTable = new DataTable();
			adminTable.Columns.Add (new DataColumn("FriendlyName", typeof(string)));
			adminTable.Columns.Add (new DataColumn("ModuleDefID", typeof(string)));
			
			DataRow drow;
			while (dr.Read()) 
			{
				if (bool.Parse(dr["Admin"].ToString())) 
				{
					drow = adminTable.NewRow();
					drow["ModuleDefID"] = dr["ModuleDefID"];
					string aux = dr["FriendlyName"].ToString();
					if (aux.StartsWith ("Admin")) 
					{
						aux = aux.Substring(5);
						while (aux[0] == ' ' || aux[0] == '-') aux = aux.Substring(1);
					}
					drow["FriendlyName"] = aux;
					adminTable.Rows.Add(drow);
				}
				else 
				{
					drow = userTable.NewRow();
					drow["ModuleDefID"] = dr["ModuleDefID"];
					drow["FriendlyName"] = dr["FriendlyName"];
					userTable.Rows.Add(drow);
				}
			}
			userModules.DataSource = userTable;
			userModules.DataBind();
			adminModules.DataSource = adminTable;
			adminModules.DataBind();
			dr.Close();
        }
      
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{5E0DB0C7-FD54-4F55-ACF5-6ECF0EFA59C0}");
			}
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Raises OnInit Event
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() 
        {
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
    }
}
