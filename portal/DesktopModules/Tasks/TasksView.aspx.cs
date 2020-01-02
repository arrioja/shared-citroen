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
using Rainbow.Configuration;


namespace Rainbow.DesktopModules
{
	/// IBS Portal Tasks Module - Display all info about single task
	/// Writen by: ?
	/// Moved into Rainbow by Jakob Hansen, hansen3000@hotmail.com
	public class TaskView : Rainbow.UI.ViewItemPage
    {
        protected System.Web.UI.WebControls.Label TitleField;
        protected System.Web.UI.WebControls.Label longdesc;
        ///Chris Farrell, 5/27/04, chris@cftechconsulting.com
        ///fix longdesc does not wrap text.
        ///protected System.Web.UI.HtmlControls.HtmlGenericControl longdesc;
		protected System.Web.UI.WebControls.Label PercentCompleteField;
		protected System.Web.UI.WebControls.Label StatusField;
		protected System.Web.UI.WebControls.Label PriorityField;
		protected System.Web.UI.WebControls.Label AssignedField;
		protected System.Web.UI.WebControls.Label StartField;
		protected System.Web.UI.WebControls.Label DueField;
        protected System.Web.UI.WebControls.Label CreatedBy;
        protected System.Web.UI.WebControls.Label CreatedDate;
		protected System.Web.UI.WebControls.Label ModifiedBy;
		protected System.Web.UI.WebControls.Label ModifiedDate;
		protected Esperantus.WebControls.Literal Literal1;
		protected Esperantus.WebControls.Literal Literal2;
		protected Esperantus.WebControls.Literal Literal3;
		protected Esperantus.WebControls.Literal Literal4;
		protected Esperantus.WebControls.Literal Literal5;
		protected Esperantus.WebControls.Literal Literal6;
		protected Esperantus.WebControls.Literal Literal7;
		protected Esperantus.WebControls.Literal Literal8;
		protected Esperantus.WebControls.Literal Literal9;
		protected Esperantus.WebControls.Literal CreatedLabel;
		protected Esperantus.WebControls.Literal OnLabel;
		protected Esperantus.WebControls.Literal ModifiedLabel;
		protected Esperantus.WebControls.Literal ModifiedOnLabel;

		protected string EditLink = string.Empty;

		/// <summary>
		/// The Page_Load event on this Page is used to obtain the ModuleID
		/// and ItemID of the task to display.
		/// It then uses the Rainbow.TasksDB() data component
		/// to populate the page's edit controls with the task details.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e)
        {

			// Verify that the current user has access to edit this module
			if (Rainbow.Security.PortalSecurity.HasEditPermissions(ModuleID))
			{
				EditLink = "<a href= \"TasksEdit.aspx?ItemID=" + ItemID;
				EditLink += "&mID=" +  ModuleID + "\" class=\"Normal\">Edit</a>";
			}

            if (Page.IsPostBack == false)
            {
				//Chris Farrell, chris@cftechconsulting.com, 5/28/04.
				//Improper Identity seed in the ItemID means that there may be tasks
				//with a ItemID = 0.  This is not the way it should be, but there is no
				//reason to NOT show the task with ItemID = 0 and that helps reduce
				//the pains from this bug for users who already have data present.

				// Obtain a single row of Task information
				TasksDB Tasks = new TasksDB();
				SqlDataReader dr = Tasks.GetSingleTask(ItemID);

				try
				{
					// Read first row from database
					if(dr.Read())
					{
						TitleField.Text = (string) dr["Title"];
						longdesc.Text = (string) dr["Description"];
						StartField.Text = ((DateTime) dr["StartDate"]).ToShortDateString();
						DueField.Text = ((DateTime) dr["DueDate"]).ToShortDateString();
						CreatedBy.Text = (string) dr["CreatedByUser"];
						ModifiedBy.Text = (string) dr["ModifiedByUser"];
						PercentCompleteField.Text = ((Int32) dr["PercentComplete"]).ToString();
						AssignedField.Text = (string) dr["AssignedTo"];
						CreatedDate.Text = ((DateTime) dr["CreatedDate"]).ToString();
						ModifiedDate.Text = ((DateTime) dr["ModifiedDate"]).ToString();
						StatusField.Text = Esperantus.Localize.GetString("TASK_STATE_"+(string) dr["Status"],(string) dr["Status"],StatusField);
						PriorityField.Text = Esperantus.Localize.GetString("TASK_PRIORITY_"+(string) dr["Priority"],(string) dr["Priority"],PriorityField);
						// 15/7/2004 added localization by Mario Endara mario@softworks.com.uy
						if (CreatedBy.Text == "unknown")
						{
							CreatedBy.Text = Esperantus.Localize.GetString ( "UNKNOWN", "unknown");
						}
						// 15/7/2004 added localization by Mario Endara mario@softworks.com.uy
						if (ModifiedBy.Text == "unknown")
						{
							ModifiedBy.Text = Esperantus.Localize.GetString ( "UNKNOWN", "unknown");
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
				al.Add ("2502DB18-B580-4F90-8CB4-C15E6E531012");
				al.Add ("2502DB18-B580-4F90-8CB4-C15E6E531030"); // Access from portalSearch
				al.Add ("2502DB18-B580-4F90-8CB4-C15E6E531052"); // Access from serviceItemList				
				return al;
			}
		}

		#region Web Form Designer generated code
        /// <summary>
        /// Raises OnInitEvent
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
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
