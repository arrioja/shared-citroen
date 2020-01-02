using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.Security;
using Rainbow.Configuration;
using Esperantus;

namespace Rainbow.DesktopModules
{            
    public class Users : PortalModuleControl 
    {
		protected Esperantus.WebControls.Label RegisteredLabel;
        protected System.Web.UI.WebControls.Literal Message;
        protected System.Web.UI.WebControls.DropDownList allUsers;
		protected Esperantus.WebControls.Literal DomainMessage1;
		protected Esperantus.WebControls.Literal DomainMessage2;
		protected Esperantus.WebControls.Literal DomainMessage3;
		protected System.Web.UI.WebControls.PlaceHolder UserDomain;
		protected Esperantus.WebControls.Literal FormsMessage1;
		protected Esperantus.WebControls.Literal FormsMessage2;
		protected Esperantus.WebControls.Literal FormsMessage3;
		protected System.Web.UI.WebControls.PlaceHolder UserForm;
        protected System.Web.UI.WebControls.LinkButton addNew;

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

		public Users()
		{
			//Moved to portal settings
//			// If we can allow identity edit
//			SettingItem AllowEditUserID = new SettingItem(new Rainbow.UI.DataTypes.BooleanDataType());
//			AllowEditUserID.Value = "False";
//			this._baseSettings.Add("AllowEditUserID", AllowEditUserID);
		}

        /// <summary>
        /// The Page_Load server event handler on this user control is used
        /// to populate the current roles settings from the configuration system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e) 
        {
            // If this is the first visit to the page, bind the role data to the datalist
            if (Page.IsPostBack == false) 
            {
                BindData();
            }
        }

        /// <summary>
        /// The DeleteUser_Click server event handler
        /// is used to delete an user for this portal
        /// </summary>
        protected override void OnDelete() 
        {
            // get user id from dropdownlist of users
            UsersDB users = new UsersDB();
            users.DeleteUser(Int32.Parse(allUsers.SelectedItem.Value));
        
			base.OnDelete();

            // Rebind list
            BindData();
        }
		
//		string AllowEditUserID
//		{
//			get
//			{
//				return bool.Parse(Settings["AllowEditUserID"].ToString()) ? "&AllowEditUserID=true" : string.Empty;
//			}	
//		}

        /// <summary>
        /// The EditUser_Click server event handler is used to add
        /// a new security role for this portal 
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void EditUser_Click(Object Sender, CommandEventArgs e)
        {
            // get user id from dropdownlist of users
            int userID = -1;
            string _userName = string.Empty;
        
            if (e.CommandName == "edit")
            {
                userID = Int32.Parse(allUsers.SelectedItem.Value);
                _userName = Server.UrlEncode(allUsers.SelectedItem.Text);
            }
        
            // redirect to edit page
			// changed by Mario Endara <mario@softworks.com.uy> (2004/11/05)
			// we need mID in the URL to apply security checking in the target
			//Response.Redirect(HttpUrlBuilder.BuildUrl("~/DesktopModules/Users/UsersManage.aspx", TabID, "userID=" + userID + "&username=" + _userName + AllowEditUserID));
			Response.Redirect(HttpUrlBuilder.BuildUrl("~/DesktopModules/Users/UsersManage.aspx", TabID, "mID=" + ModuleID + "&userID=" + userID + "&username=" + _userName));
        }

        /// <summary>
        /// The BindData helper method is used to bind the list of 
        /// users for this portal to an asp:DropDownList server control
        /// </summary>
        private void BindData()
        {
            // change the message between Windows and Forms authentication
			if (Context.User.Identity.AuthenticationType == "Forms")
			{
				UserDomain.Visible = false;
				UserForm.Visible = true;
			}
			else
			{
				UserDomain.Visible = true;
				UserForm.Visible = false;
			}

			// Get the list of registered users from the database
            allUsers.DataSource = new UsersDB().GetUsers(portalSettings.PortalID);
            // bind all portal users to dropdownlist
            allUsers.DataBind();
        }
     
        /// <summary>
        /// GuidID
        /// </summary>
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{B6A48596-9047-4564-8555-61E3B31D7272}");
			}
		}
          
		#region Web Form Designer generated code
        /// <summary>
        /// Raises the Init event.
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {
			InitializeComponent();
			//this.AddUrl = HttpUrlBuilder.BuildUrl("~/DesktopModules/Users/UsersManage.aspx", TabID, "userID=-1" + AllowEditUserID);
//			this.AddUrl = HttpUrlBuilder.BuildUrl("~/DesktopModules/Users/UsersManage.aspx", TabID, "userID=-1");
			// changed by Mario Endara <mario@softworks.com.uy> (2004/11/05)
			// userID=-1 is not needed and with parameters, when building the URL TabID and mID are added
			this.AddUrl = "~/DesktopModules/Users/UsersManage.aspx";
			this.AddText = "ADD_NEW_USER";
			base.OnInit(e);
        }

        private void InitializeComponent() 
        {
			this.EditBtn.Command += new System.Web.UI.WebControls.CommandEventHandler(this.EditUser_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

    }
}
