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
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Rainbow.Configuration;
using Esperantus;

namespace Rainbow.DesktopModules 
{
    public class HtmlModule : PortalModuleControl 
    {
        protected System.Web.UI.WebControls.PlaceHolder HtmlHolder;
				
		/// <summary>
		/// Searchable module
		/// </summary>
		public override bool Searchable
		{
			get
			{
				return true;
			}
		}

        /// <summary>
        /// The Page_Load event handler on this User Control is
        /// used to render a block of HTML or text to the page.  
        /// The text/HTML to render is stored in the HtmlText 
        /// database table.  This method uses the Rainbow.HtmlTextDB()
        /// data component to encapsulate all data functionality.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [History("William Forney","bill@improvdesign.com","Moved data reader code to the DB class where it belongs & commented it out.")]
        private void Page_Load(object sender, System.EventArgs e) 
        {
            // Obtain the selected item from the HtmlText table
            HtmlTextDB text = new HtmlTextDB();
//			// Change by Geert.Audenaert@Syntegra.Com - Date: 7/2/2003
//			// Original: SqlDataReader dr = text.GetHtmlText(ModuleID);
//			SqlDataReader dr = text.GetHtmlText(ModuleID, Version);
//			// End Change Geert.Audenaert@Syntegra.Com
//
//			try
//			{
//				if (dr.Read())
//				{
//					// Jes1111
//					// Dynamically add the file content into the page
					this.Content = Server.HtmlDecode(text.GetHtmlTextString(ModuleID, Version));
					this.HtmlHolder.Controls.Add(new LiteralControl(this.Content.ToString()));
//				}
//			}
//			finally
//			{
//				// Close the datareader
//				dr.Close();
//			}
		}
 
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{0B113F51-FEA3-499A-98E7-7B83C192FDBB}");
			}
		}
		
		public HtmlModule()
		{
			int _groupOrderBase;
			SettingItemGroup _Group;

			#region Module Special Settings
			_Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			_groupOrderBase = (int)SettingItemGroup.MODULE_SPECIAL_SETTINGS;

			HtmlEditorDataType.HtmlEditorSettings (this._baseSettings, _Group);

			//If false the input box for mobile content will be hidden
			SettingItem ShowMobileText = new SettingItem(new BooleanDataType());
			ShowMobileText.Value = "true";
			ShowMobileText.Order = _groupOrderBase + 10;
			ShowMobileText.Group = _Group;
			this._baseSettings.Add("ShowMobile", ShowMobileText);
			#endregion

			this.SupportsWorkflow = true;

			// No need for view state on view. - jminond
			this.EnableViewState = false;
		}
		
		/// <summary>
		/// Searchable module implementation
		/// </summary>
		/// <param name="portalID">The portal ID</param>
		/// <param name="userID">ID of the user is searching</param>
		/// <param name="searchString">The text to search</param>
		/// <param name="searchField">The fields where perfoming the search</param>
		/// <returns>The SELECT sql to perform a search on the current module</returns>
		public override string SearchSqlSelect(int portalID, int userID, string searchString, string searchField)
		{
			Rainbow.Helpers.SearchDefinition s = new Rainbow.Helpers.SearchDefinition("rb_HtmlText", "DesktopHtml", "DesktopHtml", searchField);
			return s.SearchSqlSelect(portalID, userID, searchString, false);
		}

		#region Web Form Designer generated code
        /// <summary>
        /// OnInit
        /// </summary>
        /// <param name="e"></param>
        override protected void OnInit(EventArgs e)
        {

            InitializeComponent();

			// Add title
//			ModuleTitle = new DesktopModuleTitle();
			this.EditUrl = "~/DesktopModules/HTMLDocument/HtmlEdit.aspx";
//			Controls.AddAt(0, ModuleTitle);

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

		# region Install / Uninstall Implementation
		public override void Install(System.Collections.IDictionary stateSaver)
		{
			string currentScriptName = System.IO.Path.Combine(Server.MapPath(TemplateSourceDirectory), "install.sql");
			ArrayList errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);
			if (errors.Count > 0)
			{
				// Call rollback
				throw new Exception("Error occurred:" + errors[0].ToString());
			}
		}

		public override void Uninstall(System.Collections.IDictionary stateSaver)
		{
			string currentScriptName = System.IO.Path.Combine(Server.MapPath(TemplateSourceDirectory), "uninstall.sql");
			ArrayList errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);
			if (errors.Count > 0)
			{
				// Call rollback
				throw new Exception("Error occurred:" + errors[0].ToString());
			}
		}
		#endregion

    }
}
