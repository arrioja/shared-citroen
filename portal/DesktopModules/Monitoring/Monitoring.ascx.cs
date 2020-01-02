using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.IO;
using Esperantus;
using Rainbow.Security;
using Rainbow.Configuration;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Rainbow.Admin;
using System.Text;
using System.Web.Mail;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// Rainbow Monitoring Module - Shows website usage stats
	/// Written by: Paul Yarrow, paul@paulyarrow.com
	/// </summary>
	public class Monitoring : PortalModuleControl, Rainbow.Scheduler.ISchedulable
	{
		protected System.Web.UI.WebControls.DataGrid myDataGrid;
		protected System.Data.DataView myDataView;
		protected string sortField;
		protected string sortDirection;

		protected System.Web.UI.WebControls.CheckBox CheckBoxIncludeAdmin;
		protected System.Web.UI.WebControls.DropDownList cboReportType;
		protected System.Web.UI.WebControls.TextBox txtStartDate;
		protected System.Web.UI.WebControls.TextBox txtEndDate;
		protected System.Web.UI.WebControls.LinkButton cmdDisplay;
		protected System.Web.UI.WebControls.Label lblMessage;
		protected Esperantus.WebControls.Literal Literal2;
		protected Esperantus.WebControls.RequiredFieldValidator RequiredStartDate;
		protected Esperantus.WebControls.CompareValidator VerifyStartDate;
		protected Esperantus.WebControls.Literal Literal3;
		protected Esperantus.WebControls.RequiredFieldValidator RequiredEndDate;
		protected Esperantus.WebControls.CompareValidator VerifyExpireDate;
		protected SqlCommand sqlComm1;
		protected SqlDataAdapter sqlDA1;
		protected System.Web.UI.WebControls.CheckBox CheckBoxPageRequests;
		protected System.Web.UI.WebControls.CheckBox CheckBoxLogons;
		protected System.Web.UI.WebControls.CheckBox CheckBoxLogouts;
		protected System.Web.UI.WebControls.CheckBox CheckBoxIncludeMonitorPage;
		protected System.Web.UI.WebControls.CheckBox CheckBoxIncludeAdminUser;
		protected System.Web.UI.WebControls.Image ChartImage;
		protected System.Web.UI.WebControls.CheckBox CheckBoxIncludeMyIPAddress;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label LabelNoData;
		protected System.Web.UI.WebControls.Panel MonitoringPanel;
		protected System.Web.UI.WebControls.Label ErrorLabel;
		protected string lastReportType;

		/// <summary>
		/// Initial Revision by Paul Yarrow, paul@paulyarrow.com, 2003-07-13
		/// </summary>
		public Monitoring()
		{
			SettingItem setSortField = new SettingItem(new ListDataType("ActivityTime;ActivityType;Name;PortalName;TabName;UserHostAddress;UserAgent"));
			setSortField.Required = true;
			setSortField.Value = "ActivityTime";
			setSortField.Order = 1;
			this._baseSettings.Add("SortField", setSortField);
		}

		/// <summary>
		/// The Page_Load event handler on this User Control is used to
		/// determine sort field and order, and then databind the required
		/// monitoring table rows, generating a graph if necessary.
		/// Initial Revision by Paul Yarrow, paul@paulyarrow.com, 2003-07-13
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Monitoring_Load(object sender, System.EventArgs e)
		{
			if(PortalSettings.IsMonitoringEnabled)
			{
				// Set the variables correctly depending
				// on whether this is the first page view
				if (Page.IsPostBack == false) 
				{
					sortField = Settings["SortField"].ToString();
					sortDirection = "ASC";
					if (sortField == "ActivityTime")
					{
						sortDirection = "DESC";
					}
					ViewState["SortField"] = sortField;
					ViewState["sortDirection"] = sortDirection;

					txtStartDate.Text = DateTime.Now.AddDays(-6).ToShortDateString();
					txtEndDate.Text = DateTime.Now.ToShortDateString();
				}
				else
				{
					sortField = (string) ViewState["SortField"];
					sortDirection = (string) ViewState["sortDirection"];
				}

				lastReportType = cboReportType.SelectedItem.Value;

				BindGrid();	
				
				MonitoringPanel.Visible = true;
				ErrorLabel.Visible = false;
			}
			else
			{
				ErrorLabel.Text = "Monitoring is disabled. Put EnableMonitoring to true in web.config.";
				MonitoringPanel.Visible = false;
				ErrorLabel.Visible = true;
			}
		}

		/// <summary>
		/// The SortTasks event handler sorts the monitoring list (a DataGrid control)
		/// Initial Revision by Paul Yarrow, paul@paulyarrow.com, 2003-07-13
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		protected void SortTasks(Object source, DataGridSortCommandEventArgs e)
		{
			if (sortField == e.SortExpression)
			{
				if (sortDirection == "ASC")
				{
					sortDirection = "DESC";
				}
				else
				{
					sortDirection = "ASC";
				}
			}
			else
			{
				if (e.SortExpression == "DueDate")
				{
					sortDirection = "DESC";
				}
			}

			ViewState["SortField"] = e.SortExpression;
			ViewState["sortDirection"] = sortDirection;

			BindGrid();
		}
    
		/// <summary>
		/// Initial Revision by Paul Yarrow, paul@paulyarrow.com, 2003-07-13
		/// </summary>
		protected void BindGrid()
		{
			if (txtStartDate.Text.Length > 0 && 
				txtEndDate.Text.Length > 0)
			{
				// Read in the data regardless
				MonitoringDB monitorDB = new MonitoringDB();

				DateTime startDate = DateTime.Parse(txtStartDate.Text);
				DateTime endDate = DateTime.Parse(txtEndDate.Text);

				bool showChart = true;
				string chartType = string.Empty;

				switch (cboReportType.SelectedItem.Value)
				{
					case "Detailed Site Log":

						if (cboReportType.SelectedItem.Value != lastReportType)
						{
							sortField = "ActivityTime";
							sortDirection = "DESC";
							ViewState["SortField"] = sortField;
							ViewState["sortDirection"] = sortDirection;
						}

						showChart = false;

						break;

					case "Page Popularity":

						if (cboReportType.SelectedItem.Value != lastReportType)
						{
							sortField = "Requests";
							sortDirection = "DESC";
							ViewState["SortField"] = sortField;
							ViewState["sortDirection"] = sortDirection;
						}

						chartType = "pie";
						break;

					case "Most Active Users":

						if (cboReportType.SelectedItem.Value != lastReportType)
						{
							sortField = "Actions";
							sortDirection = "DESC";
							ViewState["SortField"] = sortField;
							ViewState["sortDirection"] = sortDirection;
						}

						chartType = "pie";
						break;

					case "Page Views By Day":

						if (cboReportType.SelectedItem.Value != lastReportType)
						{
							sortField = "[Date]";
							sortDirection = "ASC";
							ViewState["SortField"] = sortField;
							ViewState["sortDirection"] = sortDirection;
						}

						chartType = "bar";
						break;

					case "Page Views By Browser Type":

						if (cboReportType.SelectedItem.Value != lastReportType)
						{
							sortField = "[Views]";
							sortDirection = "DESC";
							ViewState["SortField"] = sortField;
							ViewState["sortDirection"] = sortDirection;
						}

						chartType = "pie";
						break;
				}				

				DataSet monitorData = monitorDB.GetMonitoringStats(startDate,
																	endDate,
																	cboReportType.SelectedItem.Value,
																	portalSettings.ActiveTab.TabID,
																	CheckBoxIncludeMonitorPage.Checked,
																	CheckBoxIncludeAdminUser.Checked,
																	CheckBoxPageRequests.Checked,
																	CheckBoxLogons.Checked,
																	CheckBoxLogouts.Checked,
																	CheckBoxIncludeMyIPAddress.Checked,
																	portalSettings.PortalID);
				myDataView = monitorData.Tables[0].DefaultView;
				myDataView.Sort = sortField + " " + sortDirection;
				myDataGrid.DataSource = myDataView;
				myDataGrid.DataBind();
	
				if (monitorData.Tables[0].Rows.Count > 0)
				{
					myDataGrid.Visible = true;
					LabelNoData.Visible = false;
				}
				else
				{
					myDataGrid.Visible = false;
					LabelNoData.Visible = true;
				}

				if (showChart)
				{
					StringBuilder xValues = new StringBuilder();
					StringBuilder yValues = new StringBuilder();

					foreach (DataRow dr in monitorData.Tables[0].Rows)
					{
						xValues.Append(dr[0]);
						yValues.Append(dr[1]);
						xValues.Append("|");
						yValues.Append("|");
					}

					if (xValues.Length > 0 && yValues.Length > 0)
					{
						xValues.Remove(xValues.Length-1, 1);
						yValues.Remove(yValues.Length-1, 1);

						ChartImage.ImageUrl = Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Monitoring/ChartGenerator.aspx?" +
							"xValues=" + xValues.ToString() + 
							"&yValues=" + yValues.ToString() + 
							"&ChartType=" + chartType);
				
						ChartImage.Visible = true;
					}
					else
					{
						ChartImage.Visible = false;
					}
				}
				else
				{
					ChartImage.Visible = false;
				}
			}
		}

		/// <summary>
		/// Initial Revision by Paul Yarrow, paul@paulyarrow.com, 2003-07-13
		/// </summary>
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{3B8E3585-58B7-4f56-8AB6-C04A2BFA6589}");
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

		#region Web Form Designer generated code
		/// <summary>
		/// Raises Init event
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
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
			this.cboReportType.SelectedIndexChanged += new System.EventHandler(this.cboReportType_SelectedIndexChanged);
			this.cmdDisplay.Click += new System.EventHandler(this.cmdDisplay_Click);
			this.Load += new System.EventHandler(this.Monitoring_Load);

		}
		#endregion

		private void cmdDisplay_Click(object sender, System.EventArgs e)
		{
			if (Page.IsValid == true) 
			{
				BindGrid();
			}
		}

		private void cboReportType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			BindGrid();
		}

		public void ScheduleCommit(Rainbow.Scheduler.SchedulerTask task)
		{
			// TODO:  Add Monitoring.ScheduleCommit implementation
		}
	
		public void ScheduleDo(Rainbow.Scheduler.SchedulerTask task)
		{
			// TODO:  Add Monitoring.ScheduleDo implementation
		}
	
		public void ScheduleRollback(Rainbow.Scheduler.SchedulerTask task)
		{
			// TODO:  Add Monitoring.ScheduleRollback implementation
		}
	}
}