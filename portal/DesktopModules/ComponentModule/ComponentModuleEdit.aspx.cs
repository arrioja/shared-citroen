using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

using Rainbow.Admin;
using Rainbow.Configuration;
using Esperantus;


namespace Rainbow.DesktopModules 
{
	/// <summary>
	/// 
	/// </summary>
    public class ComponentModuleEdit : Rainbow.UI.AddEditItemPage
    {
        protected System.Web.UI.WebControls.TextBox TitleField;
        protected System.Web.UI.WebControls.RequiredFieldValidator RequiredTitle;
        protected System.Web.UI.WebControls.TextBox ComponentField;
        protected System.Web.UI.WebControls.RequiredFieldValidator RequiredComponent;
		protected Esperantus.WebControls.Literal CreatedLabel;
		protected Esperantus.WebControls.Literal OnLabel;
		protected System.Web.UI.WebControls.Label CreatedBy;
        protected System.Web.UI.WebControls.Label CreatedDate;

		/// <summary>
		/// The Page_Load event on this Page is used to obtain the ModuleID
		/// and ItemID of the event to edit.
		/// It then uses the Rainbow.ComponentModuleDB() data component
		/// to populate the page's edit controls with the control details.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e) 
        {
            if (Page.IsPostBack == false) 
            {
                // Obtain a single row of event information
                ComponentModuleDB comp = new ComponentModuleDB();
                SqlDataReader dr = comp.GetComponentModule(this.ModuleID);
            
				try
				{
					// Read first row from database
					if (dr.Read())
					{
						TitleField.Text = (string) dr["Title"];
						ComponentField.Text = (string) dr["Component"];
						CreatedBy.Text = (string) dr["CreatedByUser"];
						CreatedDate.Text = ((DateTime) dr["CreatedDate"]).ToShortDateString();
						// 15/7/2004 added localization by Mario Endara mario@softworks.com.uy
						if (CreatedBy.Text == "unknown" || CreatedBy.Text == string.Empty)
						{
							CreatedBy.Text = Esperantus.Localize.GetString ( "UNKNOWN", "unknown");
						}
					}
				}
				finally
				{
					dr.Close();
				}
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
				al.Add ("2B113F51-FEA3-499A-98E7-7B83C192FDBC");
				return al;
			}
		}

		/// <summary>
		/// The UpdateBtn_Click event handler on this Page is used to either
		/// create or update an event.  It uses the Rainbow.EventsDB()
		/// data component to encapsulate all data functionality.
		/// </summary>
		/// <param name="e"></param>
		override protected void OnUpdate(EventArgs e)
		{
			base.OnUpdate(e);
			
			// Only Update if the Entered Data is Valid
            if (Page.IsValid == true) 
            {
                // Create an instance of the Event DB component
				ComponentModuleDB comp = new ComponentModuleDB();

                comp.UpdateComponentModule(ModuleID, PortalSettings.CurrentUser.Identity.Email, TitleField.Text, ComponentField.Text);

                // Redirect back to the portal home page
				this.RedirectBackToReferringPage();
			}
        }

		#region Web Form Designer generated code
        /// <summary>
        /// Raises OnInitEvent
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            //Translate
			// Added EsperantusKeys for Localization 
			// Mario Endara mario@softworks.com.uy june-1-2004 
            RequiredTitle.ErrorMessage = Esperantus.Localize.GetString("ERROR_VALID_TITLE");
            RequiredComponent.ErrorMessage = Esperantus.Localize.GetString("ERROR_VALID_DESCRIPTION");
 
            InitializeComponent();

            base.OnInit(e);
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

    }
}