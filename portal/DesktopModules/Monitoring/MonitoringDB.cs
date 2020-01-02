using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

using Rainbow.Configuration;



namespace Rainbow.DesktopModules 
{
	/// <summary>
	/// Database access code for the Monitoring Module
	/// Written by Paul Yarrow, paul@paulyarrow.com
	/// </summary>
	public class MonitoringDB
	{
		public DataSet GetMonitoringStats(DateTime startDate, 
												DateTime endDate, 
												string reportType,
												long currentTabID,
												bool includeMonitoringPage,
												bool includeAdminUser,
												bool includePageRequests,
												bool includeLogon,
												bool includeLogoff,
												bool includeMyIPAddress,
												int portalID)
		{
			endDate = endDate.AddDays(1);

			// Firstly get the logged in users
			SqlConnection myConnection = PortalSettings.SqlConnectionString;
			SqlDataAdapter myCommand = new SqlDataAdapter("rb_GetMonitoringEntries", myConnection);
			myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

			// Add Parameters to SPROC
			SqlParameter  parameterPortalID = new SqlParameter("@PortalID", SqlDbType.Int, 4);
			parameterPortalID.Value = portalID;
			myCommand.SelectCommand.Parameters.Add(parameterPortalID);

			SqlParameter  parameterStartDate = new SqlParameter("@StartDate", SqlDbType.DateTime, 8);
			parameterStartDate.Value = startDate;
			myCommand.SelectCommand.Parameters.Add(parameterStartDate);

			SqlParameter  parameterEndDate = new SqlParameter("@EndDate", SqlDbType.DateTime, 8);
			parameterEndDate.Value = endDate;
			myCommand.SelectCommand.Parameters.Add(parameterEndDate);

			SqlParameter  parameterReportType = new SqlParameter("@ReportType", SqlDbType.NVarChar, 50);
			parameterReportType.Value = reportType;
			myCommand.SelectCommand.Parameters.Add(parameterReportType);

			SqlParameter  parameterCurrentTabID = new SqlParameter("@CurrentTabID", SqlDbType.BigInt, 8);
			parameterCurrentTabID.Value = currentTabID;
			myCommand.SelectCommand.Parameters.Add(parameterCurrentTabID);

			SqlParameter  parameterIncludeMoni = new SqlParameter("@IncludeMonitorPage", SqlDbType.Bit, 1);
			parameterIncludeMoni.Value = includeMonitoringPage;
			myCommand.SelectCommand.Parameters.Add(parameterIncludeMoni);

			SqlParameter  parameterIncludeAdmin = new SqlParameter("@IncludeAdminUser", SqlDbType.Bit, 1);
			parameterIncludeAdmin.Value = includeAdminUser;
			myCommand.SelectCommand.Parameters.Add(parameterIncludeAdmin);

			SqlParameter  parameterIncludePageRequests = new SqlParameter("@IncludePageRequests", SqlDbType.Bit, 1);
			parameterIncludePageRequests.Value = includePageRequests;
			myCommand.SelectCommand.Parameters.Add(parameterIncludePageRequests);

			SqlParameter  parameterIncludeLogon = new SqlParameter("@IncludeLogon", SqlDbType.Bit, 1);
			parameterIncludeLogon.Value = includeLogon;
			myCommand.SelectCommand.Parameters.Add(parameterIncludeLogon);

			SqlParameter  parameterIncludeLogoff = new SqlParameter("@IncludeLogoff", SqlDbType.Bit, 1);
			parameterIncludeLogoff.Value = includeLogoff;
			myCommand.SelectCommand.Parameters.Add(parameterIncludeLogoff);

			SqlParameter  parameterIncludeIPAddress = new SqlParameter("@IncludeIPAddress", SqlDbType.Bit, 1);
			parameterIncludeIPAddress.Value = includeMyIPAddress;
			myCommand.SelectCommand.Parameters.Add(parameterIncludeIPAddress);

			SqlParameter  parameterIPAddress = new SqlParameter("@IPAddress", SqlDbType.NVarChar, 16);
			parameterIPAddress.Value = HttpContext.Current.Request.UserHostAddress;
			myCommand.SelectCommand.Parameters.Add(parameterIPAddress);

			// Create and Fill the DataSet
			DataSet myDataSet = new DataSet();
			try
			{
				myCommand.Fill(myDataSet);
			}
			finally
			{
				myConnection.Close(); 
			}

			// Return the DataSet
			return myDataSet;
		}
	}
}
