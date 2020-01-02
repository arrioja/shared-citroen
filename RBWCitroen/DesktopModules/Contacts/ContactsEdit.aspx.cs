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
using Esperantus;
using Rainbow.Admin;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules
{

	/// <summary>
	/// Page for editing contacts
	/// </summary>
	[History("Jes1111", "2003/03/04", "Cache flushing now handled by inherited page")]
    public class ContactsEdit : Rainbow.UI.AddEditItemPage
    {
        protected System.Web.UI.WebControls.TextBox NameField;
		protected Esperantus.WebControls.RequiredFieldValidator RequiredName;
        protected System.Web.UI.WebControls.TextBox RoleField;
        protected System.Web.UI.WebControls.TextBox EmailField;
        protected System.Web.UI.WebControls.TextBox Contact1Field;
        protected System.Web.UI.WebControls.TextBox Contact2Field;
        protected System.Web.UI.WebControls.TextBox FaxField;
        protected System.Web.UI.WebControls.TextBox AddressField;
		protected System.Web.UI.WebControls.Label CreatedDate;
		protected System.Web.UI.WebControls.Label CreatedBy;
		protected Esperantus.WebControls.Literal CreatedLabel;
		protected Esperantus.WebControls.Literal OnLabel;
		protected Esperantus.WebControls.Literal Literal1;
		protected Esperantus.WebControls.Literal Literal2;
		protected Esperantus.WebControls.Literal Literal3;
		protected Esperantus.WebControls.Literal Literal4;
		protected Esperantus.WebControls.Literal Literal5;
		protected Esperantus.WebControls.Literal Literal6;
		protected Esperantus.WebControls.Literal Literal7;
		protected Esperantus.WebControls.Literal Literal8;
		protected Esperantus.WebControls.RegularExpressionValidator ValidEmail;

		/// <summary>
		/// The Page_Load event on this Page is used to obtain the ModuleID
		/// and ItemID of the contact to edit.
		///
		/// It then uses the Rainbow.ContactsDB() data component
		/// to populate the page's edit controls with the contact details.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e) 
        {
            // If the page is being requested the first time, determine if an
            // contact itemID value is specified, and if so populate page
            // contents with the contact details
            if (Page.IsPostBack == false) 
            {
                if (ItemID != 0) 
                {
                    // Obtain a single row of contact information
                    ContactsDB contacts = new ContactsDB();
                    SqlDataReader dr = contacts.GetSingleContact(ItemID, WorkFlowVersion.Staging);
                
					try
					{
						// Read first row from database
						if(dr.Read())
						{
							NameField.Text = (dr["Name"] == DBNull.Value) ? string.Empty : (string) (dr["Name"]);
							RoleField.Text = (dr["Role"] == DBNull.Value) ? string.Empty : (string) (dr["Role"]);
							EmailField.Text = (dr["Email"] == DBNull.Value) ? string.Empty : (string) (dr["Email"]);
							Contact1Field.Text = (dr["Contact1"] == DBNull.Value) ? string.Empty : (string) (dr["Contact1"]);
							Contact2Field.Text = (dr["Contact2"] == DBNull.Value) ? string.Empty : (string) (dr["Contact2"]);
							FaxField.Text = (dr["Fax"] == DBNull.Value) ? string.Empty : (string) (dr["Fax"]);
							AddressField.Text = (dr["Address"] == DBNull.Value) ? string.Empty : (string) (dr["Address"]);
							CreatedBy.Text = (dr["CreatedByUser"] == DBNull.Value) ? string.Empty : (string) (dr["CreatedByUser"]);
							CreatedDate.Text = (dr["CreatedDate"] == DBNull.Value) ? DateTime.Now.ToShortDateString() : ((DateTime) dr["CreatedDate"]).ToShortDateString();
							// 15/7/2004 added localization by Mario Endara mario@softworks.com.uy
							if (CreatedBy.Text == "unknown")
							{
								CreatedBy.Text = Esperantus.Localize.GetString ( "UNKNOWN", "unknown");
							}
						}
					}
					finally
					{
						// Close datareader
						dr.Close();
					}
                }
                else
                {
                    deleteButton.Visible = false; // Cannot delete an unexsistent item
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
				al.Add ("2502DB18-B580-4F90-8CB4-C15E6E5339EF");
				return al;
			}
		}

		/// <summary>
		/// The UpdateBtn_Click event handler on this Page is used to either
		/// create or update a contact.  It  uses the Rainbow.ContactsDB()
		/// data component to encapsulate all data functionality.
		/// </summary>
        protected override void OnUpdate(EventArgs e) 
        {
			base.OnUpdate(e);

			// Only Update if Entered data is Valid
            if (Page.IsValid == true) 
            {
                // Create an instance of the ContactsDB component
                ContactsDB contacts = new ContactsDB();

                if (ItemID == 0) 
                {
                    // Add the contact within the contacts table
                    contacts.AddContact( ModuleID, ItemID, PortalSettings.CurrentUser.Identity.Email, NameField.Text, RoleField.Text, EmailField.Text, Contact1Field.Text, Contact2Field.Text, FaxField.Text, AddressField.Text);
                }
                else 
                {
                    // Update the contact within the contacts table
                    contacts.UpdateContact( ModuleID, ItemID, PortalSettings.CurrentUser.Identity.Email, NameField.Text, RoleField.Text, EmailField.Text, Contact1Field.Text, Contact2Field.Text, FaxField.Text, AddressField.Text);
                }

                // Redirect back to the portal home page
				this.RedirectBackToReferringPage();
			}
        }

		/// <summary>
		/// The DeleteBtn_Click event handler on this Page is used to delete an
		/// a contact.  It  uses the Rainbow.ContactsDB()
		/// data component to encapsulate all data functionality.
		/// </summary>
        protected override void OnDelete(EventArgs e) 
        {
			base.OnDelete(e);

			// Only attempt to delete the item if it is an existing item
            // (new items will have "ItemID" of 0)

            if (ItemID != 0) 
            {
                ContactsDB contacts = new ContactsDB();
                contacts.DeleteContact(ItemID);
            }

            // Redirect back to the portal home page
			this.RedirectBackToReferringPage();
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Raises OnInitEvent
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
            //Translate
            RequiredName.ErrorMessage = Esperantus.Localize.GetString("CONTACTS_VALID_NAME");
            
			InitializeComponent();
		
			// Change by Geert.Audenaert@Syntegra.Com
			// Date: 10/2/2003
			base.updateButton = this.updateButton;
			base.deleteButton = this.deleteButton;
			base.cancelButton = this.cancelButton;
			// End Change Geert.Audenaert@Syntegra.Com

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