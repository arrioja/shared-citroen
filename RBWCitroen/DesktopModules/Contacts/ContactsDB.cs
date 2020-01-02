using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules 
{

	/// <summary>
	/// Class that encapsulates all data logic necessary to add/query/delete
	/// contacts within the Portal database.
	/// </summary>
    public class ContactsDB 
    {
		/// <summary>
		/// The GetContacts method returns a DataSet containing all of the
		/// contacts for a specific portal module from the contacts
		/// database.
		///
		/// NOTE: A DataSet is returned from this method to allow this method to support
		/// both desktop and mobile Web UI.
		/// </summary>
		/// <param name="moduleID"></param>
		/// <param name="version"></param>
		/// <returns></returns>
		public DataSet GetContacts(int moduleID, WorkFlowVersion version) 
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlDataAdapter myCommand = new SqlDataAdapter("rb_GetContacts", myConnection);

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
		/// The GetSingleContact method returns a SqlDataReader containing details
		/// about a specific contact from the Contacts database table.
		/// </summary>
		/// <param name="itemID"></param>
		/// <param name="version"></param>
		/// <returns></returns>
        public SqlDataReader GetSingleContact(int itemID, WorkFlowVersion version) 
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlCommand myCommand = new SqlCommand("rb_GetSingleContact", myConnection);

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
		/// The DeleteContact method deletes the specified contact from
		/// the Contacts database table.
		/// </summary>
		/// <param name="itemID"></param>
        public void DeleteContact(int itemID) 
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlCommand myCommand = new SqlCommand("rb_DeleteContact", myConnection);

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

		/// <summary>
		/// The AddContact method adds a new contact to the Contacts
		/// database table, and returns the ItemID value as a result.
		/// </summary>
		/// <param name="moduleID"></param>
		/// <param name="itemID"></param>
		/// <param name="userName"></param>
		/// <param name="name"></param>
		/// <param name="role"></param>
		/// <param name="email"></param>
		/// <param name="contact1"></param>
		/// <param name="contact2"></param>
		/// <param name="Fax"></param>
		/// <param name="Address"></param>
		/// <returns></returns>
        public int AddContact(int moduleID, int itemID, string userName, string name, string role, string email, string contact1, string contact2, string Fax, string Address) 
        {
            if (userName.Length < 1) 
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlCommand myCommand = new SqlCommand("rb_AddContact", myConnection);

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

            SqlParameter parameterName = new SqlParameter("@Name", SqlDbType.NVarChar, 100);
            parameterName.Value = name;
            myCommand.Parameters.Add(parameterName);

            SqlParameter parameterRole = new SqlParameter("@Role", SqlDbType.NVarChar, 100);
            parameterRole.Value = role;
            myCommand.Parameters.Add(parameterRole);

            SqlParameter parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
            parameterEmail.Value = email;
            myCommand.Parameters.Add(parameterEmail);

            SqlParameter parameterContact1 = new SqlParameter("@Contact1", SqlDbType.NVarChar, 100);
            parameterContact1.Value = contact1;
            myCommand.Parameters.Add(parameterContact1);

            SqlParameter parameterContact2 = new SqlParameter("@Contact2", SqlDbType.NVarChar, 100);
            parameterContact2.Value = contact2;
            myCommand.Parameters.Add(parameterContact2);

			SqlParameter parameterFax = new SqlParameter("@Fax", SqlDbType.NVarChar, 100);
			parameterFax.Value = Fax;
			myCommand.Parameters.Add(parameterFax);

			SqlParameter parameterAddress = new SqlParameter("@Address", SqlDbType.NVarChar, 100);
			parameterAddress.Value = Address;
			myCommand.Parameters.Add(parameterAddress);

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

		/// <summary>
		/// The UpdateContact method updates the specified contact within
		/// the Contacts database table.
		/// </summary>
		/// <param name="moduleID"></param>
		/// <param name="itemID"></param>
		/// <param name="userName"></param>
		/// <param name="name"></param>
		/// <param name="role"></param>
		/// <param name="email"></param>
		/// <param name="contact1"></param>
		/// <param name="contact2"></param>
		/// <param name="Fax"></param>
		/// <param name="Address"></param>
        public void UpdateContact(int moduleID, int itemID, string userName, string name, string role, string email, string contact1, string contact2, string Fax, string Address) 
        {

            if (userName.Length < 1) 
            {
                userName = "unknown";
            }

            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlCommand myCommand = new SqlCommand("rb_UpdateContact", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = itemID;
            myCommand.Parameters.Add(parameterItemID);

            SqlParameter parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;
            myCommand.Parameters.Add(parameterUserName);

            SqlParameter parameterName = new SqlParameter("@Name", SqlDbType.NVarChar, 100);
            parameterName.Value = name;
            myCommand.Parameters.Add(parameterName);

            SqlParameter parameterRole = new SqlParameter("@Role", SqlDbType.NVarChar, 100);
            parameterRole.Value = role;
            myCommand.Parameters.Add(parameterRole);

            SqlParameter parameterEmail = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
            parameterEmail.Value = email;
            myCommand.Parameters.Add(parameterEmail);

            SqlParameter parameterContact1 = new SqlParameter("@Contact1", SqlDbType.NVarChar, 100);
            parameterContact1.Value = contact1;
            myCommand.Parameters.Add(parameterContact1);

            SqlParameter parameterContact2 = new SqlParameter("@Contact2", SqlDbType.NVarChar, 100);
            parameterContact2.Value = contact2;
            myCommand.Parameters.Add(parameterContact2);

            SqlParameter parameterFax = new SqlParameter("@Fax", SqlDbType.NVarChar, 100);
            parameterFax.Value = Fax;
            myCommand.Parameters.Add(parameterFax);

            SqlParameter parameterAddress = new SqlParameter("@Address", SqlDbType.NVarChar, 100);
            parameterAddress.Value = Address;
            myCommand.Parameters.Add(parameterAddress);

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