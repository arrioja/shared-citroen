using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Collections;

using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.Configuration;
using Rainbow.UI.DataTypes;


namespace Rainbow.DesktopModules
{
	/// <summary>
	/// EventLogs - Windows Event viewer
	/// Written by: Hervé LE ROY (www.hleroy.com)
	/// Moved into Rainbow by Jakob Hansen
	/// </summary>
	public class EventLogs : PortalModuleControl
	{
	    protected System.Web.UI.WebControls.TextBox MachineName;
        protected System.Web.UI.WebControls.DropDownList LogName;
	    protected System.Web.UI.WebControls.DropDownList LogSource;
	    protected System.Web.UI.WebControls.Label Message;
	    protected System.Web.UI.WebControls.DataGrid LogGrid;

		protected string sortField;
		protected string sortDirection;
		
		/// <summary>
		/// The Page_Load server event handler is used to initialize the sort column
		/// and populate the list of event logs
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (! Page.IsPostBack) 
			{
				MachineName.Text = Settings["MachineName"].ToString();
				sortField = Settings["SortField"].ToString();
				sortDirection = Settings["SortDirection"].ToString();
				ViewState["SortField"] = sortField;
				ViewState["sortDirection"] = sortDirection;
				
				PopulateListOfLogs();
			}
			else
			{
				sortField = (string) ViewState["SortField"];
				sortDirection = (string) ViewState["sortDirection"];
			}
		}


		/// <summary>
		/// The GetEntryTypeImage function is used to get the image path
		/// for the different event log entry types (warning, error, info)
		/// </summary>
		/// <param name="EntryType"></param>
		public string GetEntryTypeImage(EventLogEntryType EntryType)
		{
            string  strImage;
            switch (EntryType)
			{
                case EventLogEntryType.Warning:
					strImage = "EventLogs_Warning.png";
					break;
                case EventLogEntryType.Error:
                    strImage = "EventLogs_Error.png";
					break;
				default:
                    strImage = "EventLogs_Info.png";
					break;
			}
			return Rainbow.Settings.Path.WebPathCombine(Rainbow.Settings.Path.ApplicationRoot, "DesktopModules/EventLogs", strImage);
        }


		/// <summary>
		/// The PopulateListOfLogs sub is used to fill the LogName drop down list
		/// with the event logs found on the machine (Application, Security, System
		/// are the default logs you may found)
		/// </summary>
		private void PopulateListOfLogs() 
		{
            LogName.Items.Clear();
            Message.Text = string.Empty;
            try {
                // Browse event logs for machine name
                foreach (EventLog myEventLog in EventLog.GetEventLogs(MachineName.Text))
				{
                    LogName.Items.Add(myEventLog.LogDisplayName);
                } //
                // Populate list of sources
                PopulateListOfSources();
            } 
			catch
			{
                // Probably wrong machine name
                Message.Text = "Error while browsing event logs on machine " + MachineName.Text + ". Probably wrong machine name";
                LogName.Items.Clear();
                LogSource.Items.Clear();
            }

        }


		/// <summary>
		/// The PopulateListOfSources sub is used to fill the LogSource drop down list
        /// with the different sources found in the selected event log
		/// </summary>
		private void PopulateListOfSources() 
		{

            Message.Text = string.Empty;
            try {
                EventLog myEventLog = new EventLog(LogName.SelectedItem.Text, MachineName.Text);
                EventLogEntryCollection myLogEntryCollection = myEventLog.Entries;
                
                ArrayList mySourceArray = new ArrayList(); // Array used to sort strings before populating the drop down list;
                // Browse event entries for different source name
                foreach (EventLogEntry myLogEntry in myLogEntryCollection)
				{
                    if ((mySourceArray.IndexOf(myLogEntry.Source) < 0)) 
					{
                        mySourceArray.Add(myLogEntry.Source);
                    }
                } //
                // Sort the source array
                mySourceArray.Sort();
                // Add the source names to the drop down list
                LogSource.Items.Clear();
                LogSource.Items.Add("(all)");
                foreach (string Source in mySourceArray)
				{
                    LogSource.Items.Add(Source);
                } //
                // Bind grid
                BindGrid();
            } 
			catch
			{
                // An error as happened. Mostly permissions problems (when accessing security log for example)
                Message.Text = "Error while browsing source entries for " + LogName.SelectedItem.Text + ". Probably insufficient permissions";
                LogSource.Items.Clear();
            }

        }


		/// <summary>
		/// The BindGrid sub is used to bind the event log entries with the data grid
        /// This could be done directly but we chose to use an intermediate data view
        /// filled with the event log entries. This allows us to filter on the source
        /// the entries we pass to the data view and ultimately to sort the dataview
		/// </summary>
		private void BindGrid()
		{
            Message.Text = string.Empty;
            try {
				DataTable myDataTable;
				DataRow myDataRow;
				EventLog  myEventLog = new EventLog();
				string  myEventLogSource;
                myEventLog.MachineName = MachineName.Text;
                myEventLog.Log = LogName.SelectedItem.Text;
                myEventLogSource = LogSource.SelectedItem.Text;

				myDataTable = new DataTable();
                myDataTable.Columns.Add(new DataColumn("EntryType", typeof(EventLogEntryType)));
                myDataTable.Columns.Add(new DataColumn("TimeGenerated", typeof(DateTime)));
                myDataTable.Columns.Add(new DataColumn("Source", typeof(string )));
                myDataTable.Columns.Add(new DataColumn("EventID", typeof(int)));
                myDataTable.Columns.Add(new DataColumn("Message", typeof(string )));
                // Fill the data table with the event log entries
                foreach (EventLogEntry myEventLogEntry in myEventLog.Entries)
				{
                    if ((myEventLogSource == "(all)") || (myEventLogSource == myEventLogEntry.Source) ) 
					{
                        myDataRow = myDataTable.NewRow();
                        myDataRow[0] = myEventLogEntry.EntryType;
                        myDataRow[1] = myEventLogEntry.TimeGenerated;
                        myDataRow[2] = myEventLogEntry.Source;
                        myDataRow[3] = myEventLogEntry.EventID;
                        myDataRow[4] = myEventLogEntry.Message;
                        myDataTable.Rows.Add(myDataRow);
                    }
                } //
                // return a data view of the data table
                DataView myDataView = new DataView(myDataTable);
                // Sort the data view on specified column
				myDataView.Sort = sortField + " " + sortDirection; 
                // Bind the data view with the data grid
                LogGrid.DataSource = myDataView;
                LogGrid.DataBind();
            } 
			catch
			{
                Message.Text = "Unknown error while binding event log entries to the data grid";
            }

        }


		public void MachineName_Change(object sender, System.EventArgs e)  //MachineName.TextChanged 
		{
            PopulateListOfLogs();
        }

		public void LogName_Change(object sender, System.EventArgs e)  //LogName.SelectedIndexChanged 
		{
            PopulateListOfSources();
        }


		public void LogSource_Change(object sender, System.EventArgs e)  //LogSource.SelectedIndexChanged 
		{
            LogGrid.CurrentPageIndex = 0;
            BindGrid();
        }

		public void LogGrid_Change(object sender, System.Web.UI.WebControls.DataGridPageChangedEventArgs e) // LogGrid.PageIndexChanged 
		{
            LogGrid.CurrentPageIndex = e.NewPageIndex;
            BindGrid();
        }

		public void LogGrid_Sort(object sender, DataGridSortCommandEventArgs e) // LogGrid.SortCommand
		{
            sortField = e.SortExpression;
            BindGrid();
        }

		/// <summary>
		/// Public constructor. Sets base settings for module.
		/// </summary>
		public EventLogs() 
		{
			SettingItem setMachineName = new SettingItem(new StringDataType());
			setMachineName.Required = true;
			setMachineName.Value = ".";
			setMachineName.Order = 1;
			this._baseSettings.Add("MachineName", setMachineName);

			SettingItem setSortField = new SettingItem(new ListDataType("EntryType;TimeGenerated;Source;EventID;Message"));
			setSortField.Required = true;
			setSortField.Value = "TimeGenerated";
			setSortField.Order = 2;
			this._baseSettings.Add("SortField", setSortField);

			SettingItem setSortDirection = new SettingItem(new ListDataType("ASC;DESC"));
			setSortDirection.Required = true;
			setSortDirection.Value = "DESC";
			setSortDirection.Order = 3;
			this._baseSettings.Add("SortDirection", setSortDirection);
		}

		public override Guid GuidID 
		{
			get
			{
				return new Guid("{2502DB18-B580-4F90-8CB4-C15E6E531051}");
			}
		}

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

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
//			ModuleTitle = new DesktopModuleTitle();
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
	}
}
