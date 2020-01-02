using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules 
{
	/// <summary>
	/// Class that encapsulates all data logic necessary to add/query/delete
	/// links within the Portal database.
	/// Rainbow EnhancedLinks Module
	/// Written by: José Viladiu, jviladiu@portalServices.net
	/// </summary>
    public class EnhancedLinkDB 
    {
		/// <summary>
        /// The GetEnhancedLinks method returns a SqlDataReader containing all of the
        /// links for a specific portal module from the database.
		/// </summary>
		/// <param name="moduleID"></param>
		/// <param name="version"></param>
		/// <returns></returns>
        public SqlDataReader GetEnhancedLinks(int moduleID, WorkFlowVersion version) 
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlCommand myCommand = new SqlCommand("rb_GetEnhancedLinks", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleID.Value = moduleID;
            myCommand.Parameters.Add(parameterModuleID);

			SqlParameter parameterWorkflowVersion = new SqlParameter("@WorkflowVersion", SqlDbType.Int, 4);
			parameterWorkflowVersion.Value = (int)version;
			myCommand.Parameters.Add(parameterWorkflowVersion);

            // Execute the command
            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            
            // Return the datareader 
            return result;
        }

		/// <summary>
		/// The GetSingleEnhancedLink method returns a SqlDataReader containing details
		/// about a specific link from the EnhancedLinks database table.
		/// </summary>
		/// <param name="itemID"></param>
		/// <param name="version"></param>
		/// <returns></returns>
        public SqlDataReader GetSingleEnhancedLink(int itemID, WorkFlowVersion version) 
        {

            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlCommand myCommand = new SqlCommand("rb_GetSingleEnhancedLink", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = itemID;
            myCommand.Parameters.Add(parameterItemID);

			SqlParameter parameterWorkflowVersion = new SqlParameter("@WorkflowVersion", SqlDbType.Int, 4);
			parameterWorkflowVersion.Value = (int)version;
			myCommand.Parameters.Add(parameterWorkflowVersion);

            // Execute the command
            myConnection.Open();
            SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            
            // Return the datareader 
            return result;
        }

		/// <summary>
		/// The DeleteEnhancedLink method deletes a specified link from
		/// the EnhancedLinks database table.
		/// </summary>
		/// <param name="itemID"></param>
        public void DeleteEnhancedLink(int itemID) 
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlCommand myCommand = new SqlCommand("rb_DeleteEnhancedLink", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = itemID;
            myCommand.Parameters.Add(parameterItemID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

		/// <summary>
		/// The AddEnhancedLink method adds a new link within the
		/// EnhancedLinks database table, and returns ItemID value as a result.
		/// </summary>
		/// <param name="moduleID"></param>
		/// <param name="itemID"></param>
		/// <param name="userName"></param>
		/// <param name="title"></param>
		/// <param name="url"></param>
		/// <param name="mobileUrl"></param>
		/// <param name="viewOrder"></param>
		/// <param name="description"></param>
		/// <param name="imageUrl"></param>
		/// <param name="clicks"></param>
		/// <param name="target"></param>
		/// <returns></returns>
        public int AddEnhancedLink(int moduleID, int itemID, string userName, string title, string url, string mobileUrl, 
        						   int viewOrder, string description, string imageUrl, int clicks, string target) 
        {

            if (userName.Length < 1) 
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlCommand myCommand = new SqlCommand("rb_AddEnhancedLink", myConnection);

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

            SqlParameter parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 100);
            parameterTitle.Value = title;
            myCommand.Parameters.Add(parameterTitle);

            SqlParameter parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 100);
            parameterDescription.Value = description;
            myCommand.Parameters.Add(parameterDescription);

            SqlParameter parameterImageUrl = new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 250);
            parameterImageUrl.Value = imageUrl;
            myCommand.Parameters.Add(parameterImageUrl);

            SqlParameter parameterClicks = new SqlParameter("@Clicks", SqlDbType.Int, 4);
            parameterClicks.Value = clicks;
            myCommand.Parameters.Add(parameterClicks);

			SqlParameter parameterTarget = new SqlParameter("@Target", SqlDbType.NVarChar, 10);
			parameterTarget.Value = target;
			myCommand.Parameters.Add(parameterTarget);

            SqlParameter parameterUrl = new SqlParameter("@Url", SqlDbType.NVarChar, 800);
            parameterUrl.Value = url;
            myCommand.Parameters.Add(parameterUrl);

            SqlParameter parameterMobileUrl = new SqlParameter("@MobileUrl", SqlDbType.NVarChar, 250);
            parameterMobileUrl.Value = mobileUrl;
            myCommand.Parameters.Add(parameterMobileUrl);

			SqlParameter parameterViewOrder = new SqlParameter("@ViewOrder", SqlDbType.Int, 4);
			parameterViewOrder.Value = viewOrder;
			myCommand.Parameters.Add(parameterViewOrder);
			
            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            return (int)parameterItemID.Value;
        }

		/// <summary>
		/// The UpdateEnhancedLink method updates a specified link within
		/// the EnhancedLinks database table.
		/// </summary>
		/// <param name="moduleID"></param>
		/// <param name="itemID"></param>
		/// <param name="userName"></param>
		/// <param name="title"></param>
		/// <param name="url"></param>
		/// <param name="mobileUrl"></param>
		/// <param name="viewOrder"></param>
		/// <param name="description"></param>
		/// <param name="imageUrl"></param>
		/// <param name="clicks"></param>
		/// <param name="target"></param>
        public void UpdateEnhancedLink(int moduleID, int itemID, string userName, string title, string url, string mobileUrl, 
        							   int viewOrder, string description, string imageUrl, int clicks, string target) 
        {

            if (userName.Length < 1) 
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlCommand myCommand = new SqlCommand("rb_UpdateEnhancedLink", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = itemID;
            myCommand.Parameters.Add(parameterItemID);

            SqlParameter parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;
            myCommand.Parameters.Add(parameterUserName);

            SqlParameter parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 100);
            parameterTitle.Value = title;
            myCommand.Parameters.Add(parameterTitle);

            SqlParameter parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 100);
            parameterDescription.Value = description;
            myCommand.Parameters.Add(parameterDescription);

            SqlParameter parameterImageUrl = new SqlParameter("@ImageUrl", SqlDbType.NVarChar, 250);
            parameterImageUrl.Value = imageUrl;
            myCommand.Parameters.Add(parameterImageUrl);

            SqlParameter parameterClicks = new SqlParameter("@Clicks", SqlDbType.Int, 4);
            parameterClicks.Value = clicks;
            myCommand.Parameters.Add(parameterClicks);

            SqlParameter parameterUrl = new SqlParameter("@Url", SqlDbType.NVarChar, 800);
            parameterUrl.Value = url;
            myCommand.Parameters.Add(parameterUrl);

            SqlParameter parameterMobileUrl = new SqlParameter("@MobileUrl", SqlDbType.NVarChar, 250);
            parameterMobileUrl.Value = mobileUrl;
            myCommand.Parameters.Add(parameterMobileUrl);

            SqlParameter parameterViewOrder = new SqlParameter("@ViewOrder", SqlDbType.Int, 4);
            parameterViewOrder.Value = viewOrder;
            myCommand.Parameters.Add(parameterViewOrder);
			
			SqlParameter parameterTarget = new SqlParameter("@Target", SqlDbType.NVarChar, 10);
			parameterTarget.Value = target;
			myCommand.Parameters.Add(parameterTarget);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }
    }
}
