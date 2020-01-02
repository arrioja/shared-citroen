using System.Data;
using System.Data.SqlClient;
using System.Text;
using Rainbow.Configuration;
using Rainbow.Helpers;

namespace Rainbow.DesktopModules 
{

	/// <summary>
	/// Blacklist module
	/// Class that encapsulates all data logic necessary to add/query/delete
	/// blackliste users within the Portal database.
	/// Written by: Manu and Jakob Hansen
	/// </summary>
    public class BlacklistDB 
	{

        /// <summary>
        /// The AddToBlackList adds specified email to current Blacklist.
        /// Uses AddToBlackList Stored Procedure.
        /// </summary>
        /// <param name="portalID"></param>
        /// <returns></returns>
        static public void AddToBlackList(int portalID, string EMail, string Reason) 
        {
			if (PortalSettings.UseSingleUserBase) portalID = 0;
			
			// Create Instance of Connection and Command Object
        	SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlCommand myCommand = new SqlCommand("rb_AddToBlackList", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterPortalID = new SqlParameter("@PortalID", SqlDbType.Int, 4);
            parameterPortalID.Value = portalID;
            myCommand.Parameters.Add(parameterPortalID);

            SqlParameter parameterEMail = new SqlParameter("@EMail", SqlDbType.NVarChar, 100);
            parameterEMail.Value = EMail;
            myCommand.Parameters.Add(parameterEMail);

            SqlParameter parameterReason = new SqlParameter("@Reason", SqlDbType.NVarChar, 150);
            parameterReason.Value = Reason;
            myCommand.Parameters.Add(parameterReason);

            // Open the database connection and execute the command
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
		
		/// <summary>
		/// The DeleteFromBlackList deletes a specified email from current Blacklist.
		/// Uses DeleteFromBlackList Stored Procedure.
		/// </summary>
		/// <param name="portalID"></param>
		/// <returns></returns>
		static public void DeleteFromBlackList(int portalID, string EMail) 
		{
			if (PortalSettings.UseSingleUserBase) portalID = 0;
			
			// Create Instance of Connection and Command Object
			SqlConnection myConnection = PortalSettings.SqlConnectionString;
			SqlCommand myCommand = new SqlCommand("rb_DeleteFromBlackList", myConnection);
			myCommand.CommandType = CommandType.StoredProcedure;

			// Add Parameters to SPROC
			SqlParameter parameterPortalID = new SqlParameter("@PortalID", SqlDbType.Int, 4);
			parameterPortalID.Value = portalID;
			myCommand.Parameters.Add(parameterPortalID);

			SqlParameter parameterEMail = new SqlParameter("@EMail", SqlDbType.NVarChar, 100);
			parameterEMail.Value = EMail;
			myCommand.Parameters.Add(parameterEMail);

			// Open the database connection and execute the command
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

		public DataSet GetBlacklist(int portalID, bool showAllUsers, bool SendNewsletterOnly) 
		{
			if (PortalSettings.UseSingleUserBase) portalID = 0;
			
			StringBuilder select;
			select = new StringBuilder(2048);
			select.Append(" SELECT usr.Name, usr.Email, usr.SendNewsletter, usr.LastSend");
			select.Append(", bl.Email AS Blacklisted, bl.Date, ISNULL(bl.Reason, '-') AS Reason");
			select.Append(" FROM [rb_Users] usr, [rb_BlackList] bl");
			if (showAllUsers)
				select.Append(" WHERE usr.Email *= bl.Email");  // Note: the easy outer join!
			else
				select.Append(" WHERE usr.Email = bl.Email");
			select.Append(" AND usr.PortalID = " + portalID.ToString());

			if (SendNewsletterOnly)
				select.Append(" AND usr.SendNewsletter = 1");

			select.Append(" ORDER BY bl.Date DESC, usr.Name");

			return DBHelper.GetDataSet(select.ToString());
		}
	}
}