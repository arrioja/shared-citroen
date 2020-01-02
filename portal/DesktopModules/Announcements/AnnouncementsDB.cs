using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules 
{
	/// <summary>
	/// Class that encapsulates all data logic necessary to add/query/delete
	/// announcements within the Portal database.
	/// </summary>
    public class AnnouncementsDB 
    {
		/// <summary>
		/// The GetAnnouncements method returns a DataSet containing all of the
		/// announcements for a specific portal module from the Announcements
		/// database table.
		/// </summary>
		/// <remarks>
		/// A DataSet is returned from this method to allow 
		/// this method to support both desktop and mobile Web UI.
		/// </remarks>
		/// <remarks>
		/// GetAnnouncements Stored Procedure
		/// </remarks>
		/// <param name="moduleID"></param>
		/// <param name="version"></param>
		/// <returns></returns>
        public DataSet GetAnnouncements(int moduleID, WorkFlowVersion version) 
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlDataAdapter myCommand = new SqlDataAdapter("rb_GetAnnouncements", myConnection);

            // Mark the Command as a SPROC
            myCommand.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleID.Value = moduleID;
            myCommand.SelectCommand.Parameters.Add(parameterModuleID);

			// Change by Geert.Audenaert@Syntegra.Com
			// Date: 7/2/2003
			SqlParameter parameterWorkflowVersion = new SqlParameter("@WorkflowVersion", SqlDbType.Int, 4);
			parameterWorkflowVersion.Value = (int)version;
			myCommand.SelectCommand.Parameters.Add(parameterWorkflowVersion);
			// End Change Geert.Audenaert@Syntegra.Com

            // Create and Fill the DataSet
            DataSet myDataSet = new DataSet();
			try
			{
				myCommand.Fill(myDataSet);
			}
			finally
			{
				myConnection.Close(); //by Manu fix close bug #2
			}
            // Return the DataSet
            return myDataSet;
        }

		/// <summary>
		/// The GetSingleAnnouncement method returns a SqlDataReader containing details
		/// about a specific announcement from the Announcements database table.
		/// </summary>
		/// <remarks>
		/// GetSingleAnnouncement Stored Procedure
		/// </remarks>
		/// <param name="itemID"></param>
		/// <param name="version"></param>
		/// <returns></returns>
        public SqlDataReader GetSingleAnnouncement(int itemID, WorkFlowVersion version) 
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlCommand myCommand = new SqlCommand("rb_GetSingleAnnouncement", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = itemID;
            myCommand.Parameters.Add(parameterItemID);

			// Change by Geert.Audenaert@Syntegra.Com
			// Date: 7/2/2003
			SqlParameter parameterWorkflowVersion = new SqlParameter("@WorkflowVersion", SqlDbType.Int, 4);
			parameterWorkflowVersion.Value = (int)version;
			myCommand.Parameters.Add(parameterWorkflowVersion);
			// End Change Geert.Audenaert@Syntegra.Com

            // Execute the command
            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            
            // Return the datareader 
            return result;
        }

		/// <summary>
		/// The DeleteAnnouncement method deletes the specified announcement from
		/// the Announcements database table.
		/// </summary>
		/// <remarks>
		/// DeleteAnnouncement Stored Procedure
		/// </remarks>
		/// <param name="itemID"></param>
        public void DeleteAnnouncement(int itemID) 
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlCommand myCommand = new SqlCommand("rb_DeleteAnnouncement", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = itemID;
            myCommand.Parameters.Add(parameterItemID);

            myConnection.Open();
			try
			{
				myCommand.ExecuteNonQuery();
			}
			finally
			{
				myConnection.Close();
			}
		}

        //*********************************************************************
        //
        // AddAnnouncement Method
        //
        // The AddAnnouncement method adds a new announcement to the
        // Announcements database table, and returns the ItemID value as a result.
        //
        // Other relevant sources:
        //     + <a href="AddAnnouncement.htm" style="color:green">AddAnnouncement Stored Procedure</a>
        //
        //*********************************************************************

        public int AddAnnouncement(int moduleID, int itemID, string userName, string title, DateTime expireDate, string description, string moreLink, string mobileMoreLink) 
        {

            if (userName.Length < 1) 
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlCommand myCommand = new SqlCommand("rb_AddAnnouncement", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterItemID);

            SqlParameter parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleID.Value = moduleID;
            myCommand.Parameters.Add(parameterModuleID);

            SqlParameter parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;
            myCommand.Parameters.Add(parameterUserName);

            SqlParameter parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 150);
            parameterTitle.Value = title;
            myCommand.Parameters.Add(parameterTitle);

            SqlParameter parameterMoreLink = new SqlParameter("@MoreLink", SqlDbType.NVarChar, 150);
            parameterMoreLink.Value = moreLink;
            myCommand.Parameters.Add(parameterMoreLink);

            SqlParameter parameterMobileMoreLink = new SqlParameter("@MobileMoreLink", SqlDbType.NVarChar, 150);
            parameterMobileMoreLink.Value = mobileMoreLink;
            myCommand.Parameters.Add(parameterMobileMoreLink);

            SqlParameter parameterExpireDate = new SqlParameter("@ExpireDate", SqlDbType.DateTime, 8);
            parameterExpireDate.Value = expireDate;
            myCommand.Parameters.Add(parameterExpireDate);

            SqlParameter parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 2000);
            parameterDescription.Value = description;
            myCommand.Parameters.Add(parameterDescription);

            myConnection.Open();
			try
			{
				myCommand.ExecuteNonQuery();
			}
			finally
			{
				myConnection.Close();
			}

            return (int)parameterItemID.Value;
        }

        //*********************************************************************
        //
        // UpdateAnnouncement Method
        //
        // The UpdateAnnouncement method updates the specified announcement within
        // the Announcements database table.
        //
        // Other relevant sources:
        //     + <a href="UpdateAnnouncement.htm" style="color:green">UpdateAnnouncement Stored Procedure</a>
        //
        //*********************************************************************

        public void UpdateAnnouncement(int moduleID, int itemID, string userName, string title, DateTime expireDate, string description, string moreLink, string mobileMoreLink) 
        {

            if (userName.Length < 1) userName = "unknown";

            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlCommand myCommand = new SqlCommand("rb_UpdateAnnouncement", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = itemID;
            myCommand.Parameters.Add(parameterItemID);

            SqlParameter parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;
            myCommand.Parameters.Add(parameterUserName);

            SqlParameter parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 150);
            parameterTitle.Value = title;
            myCommand.Parameters.Add(parameterTitle);

            SqlParameter parameterMoreLink = new SqlParameter("@MoreLink", SqlDbType.NVarChar, 150);
            parameterMoreLink.Value = moreLink;
            myCommand.Parameters.Add(parameterMoreLink);

            SqlParameter parameterMobileMoreLink = new SqlParameter("@MobileMoreLink", SqlDbType.NVarChar, 150);
            parameterMobileMoreLink.Value = mobileMoreLink;
            myCommand.Parameters.Add(parameterMobileMoreLink);

            SqlParameter parameterExpireDate = new SqlParameter("@ExpireDate", SqlDbType.DateTime, 8);
            parameterExpireDate.Value = expireDate;
            myCommand.Parameters.Add(parameterExpireDate);

            SqlParameter parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 2000);
            parameterDescription.Value = description;
            myCommand.Parameters.Add(parameterDescription);

            myConnection.Open();
			try
			{
				myCommand.ExecuteNonQuery();
			}
			finally
			{
				myConnection.Close();
			}
		}
    }
}
