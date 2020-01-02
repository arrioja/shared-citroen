using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules {
    /// <summary>
    /// Summary description for ContentManager.
    /// </summary>
    public class ContentManagerDB {

        public SqlDataReader GetModuleTypes()
        {
            //  Create Instance of Connection and Command Object
			SqlConnection myConnection = PortalSettings.SqlConnectionString;
			SqlCommand myCommand = new SqlCommand("rb_ContentMgr_GetModuleTypes", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            //  Execute the command
            myConnection.Open();
            return myCommand.ExecuteReader(CommandBehavior.CloseConnection);

        }
        public SqlDataReader GetPortals()
        {
            //  Create Instance of Connection and Command Object
			SqlConnection myConnection = PortalSettings.SqlConnectionString;
			SqlCommand myCommand = new SqlCommand("rb_ContentMgr_GetPortals", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            //  Execute the command
            myConnection.Open();
            return myCommand.ExecuteReader(CommandBehavior.CloseConnection);

        }
        public SqlDataReader GetModuleInstances(int ItemID,int PortalID)
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlCommand myCommand = new SqlCommand("rb_ContentMgr_GetModuleInstances",myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int,4);
            parameterItemID.Value = ItemID;
            myCommand.Parameters.Add(parameterItemID);

            SqlParameter parameterPortalID = new SqlParameter("@PortalID", SqlDbType.Int,4);
            parameterPortalID.Value = PortalID;
            myCommand.Parameters.Add(parameterPortalID);

            myConnection.Open();
            return myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        }

        ///ExcludeItem == ModuleID of selected Module to use for source.
        //Since we do not want to allow copy from A->A,B->B we exclude that item.
        public SqlDataReader GetModuleInstancesExc(int ItemID, int ExcludeItem, int PortalID)
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlCommand myCommand = new SqlCommand("rb_ContentMgr_GetModuleInstancesExc",myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int,4);
            parameterItemID.Value = ItemID;
            myCommand.Parameters.Add(parameterItemID);

            SqlParameter parameterExcludeItem = new SqlParameter("@ExcludeItem", SqlDbType.Int,4);
            parameterExcludeItem.Value = ExcludeItem;
            myCommand.Parameters.Add(parameterExcludeItem);

            SqlParameter parameterPortalID = new SqlParameter("@PortalID", SqlDbType.Int,4);
            parameterPortalID.Value = PortalID;
            myCommand.Parameters.Add(parameterPortalID);

            myConnection.Open();
            return myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public SqlDataReader GetModuleData(int ContentMgr_ItemID,int ModuleID)
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlCommand myCommand = new SqlCommand("rb_ContentMgr_GetModuleData", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int,4);
            parameterModuleID.Value = ModuleID;
            myCommand.Parameters.Add(parameterModuleID);

            SqlParameter parameterContentMgr_ItemID = new SqlParameter("@ContentMgr_ItemID", SqlDbType.Int,4);
            parameterContentMgr_ItemID.Value = ContentMgr_ItemID;
            myCommand.Parameters.Add(parameterContentMgr_ItemID);

            myConnection.Open();
            return myCommand.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public void MoveItem(int ContentMgr_ItemID,int TargetItemID,int TargetModuleID)
        {
            //  Create Instance of Connection and Command Object
			SqlConnection myConnection = PortalSettings.SqlConnectionString;
			SqlCommand myCommand = new SqlCommand("rb_ContentMgr_MoveItem", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            //  Add Parameters to SPROC
            SqlParameter parameterContentMgr_ItemID = new SqlParameter("@ContentMgr_ItemID", SqlDbType.Int, 4);
            parameterContentMgr_ItemID.Value = ContentMgr_ItemID;
            myCommand.Parameters.Add(parameterContentMgr_ItemID);

            SqlParameter parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = TargetItemID;
            myCommand.Parameters.Add(parameterItemID);

            SqlParameter parameterTargetModuleID = new SqlParameter("@TargetModuleID", SqlDbType.Int, 4);
            parameterTargetModuleID.Value = TargetModuleID;
            myCommand.Parameters.Add(parameterTargetModuleID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }
        /*copy item here!!!!*/
        public void CopyItem(int ContentMgr_ItemID,int ItemID, int TargetModuleID)
        {
            //  Create Instance of Connection and Command Object
			SqlConnection myConnection = PortalSettings.SqlConnectionString;
			SqlCommand myCommand = new SqlCommand("rb_ContentMgr_CopyItem", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            //  Add Parameters to SPROC
            SqlParameter parameterContentMgr_ItemID = new SqlParameter("@ContentMgr_ItemID", SqlDbType.Int, 4);
            parameterContentMgr_ItemID.Value = ContentMgr_ItemID;
            myCommand.Parameters.Add(parameterContentMgr_ItemID);

            SqlParameter parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = ItemID;
            myCommand.Parameters.Add(parameterItemID);

            SqlParameter parameterTargetModuleID = new SqlParameter("@TargetModuleID", SqlDbType.Int, 4);
            parameterTargetModuleID.Value = TargetModuleID;
            myCommand.Parameters.Add(parameterTargetModuleID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();

        }
        /*copy all here!!!*/

        public void CopyAll(int ContentMgr_ItemID,int SourceModuleID, int TargetModuleID)
        {
            //  Create Instance of Connection and Command Object
			SqlConnection myConnection = PortalSettings.SqlConnectionString;
			SqlCommand myCommand = new SqlCommand("rb_ContentMgr_CopyAll", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            //  Add Parameters to SPROC
            SqlParameter parameterContentMgr_ItemID = new SqlParameter("@ContentMgr_ItemID", SqlDbType.Int, 4);
            parameterContentMgr_ItemID.Value = ContentMgr_ItemID;
            myCommand.Parameters.Add(parameterContentMgr_ItemID);

            SqlParameter parameterSourceModuleID = new SqlParameter("@SourceModuleID", SqlDbType.Int, 4);
            parameterSourceModuleID.Value = SourceModuleID;
            myCommand.Parameters.Add(parameterSourceModuleID);

            SqlParameter parameterTargetModuleID = new SqlParameter("@TargetModuleID", SqlDbType.Int, 4);
            parameterTargetModuleID.Value = TargetModuleID;
            myCommand.Parameters.Add(parameterTargetModuleID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }
        public void DeleteItem(int ContentMgr_ItemID,int ItemID)
        {
            //  Create Instance of Connection and Command Object
			SqlConnection myConnection = PortalSettings.SqlConnectionString;
			SqlCommand myCommand = new SqlCommand("rb_ContentMgr_DeleteItem", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            //  Add Parameters to SPROC
            SqlParameter parameterContentMgr_ItemID = new SqlParameter("@ContentMgr_ItemID", SqlDbType.Int, 4);
            parameterContentMgr_ItemID.Value = ContentMgr_ItemID;
            myCommand.Parameters.Add(parameterContentMgr_ItemID);

            SqlParameter parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemID.Value = ItemID;
            myCommand.Parameters.Add(parameterItemID);

            myConnection.Open();
            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }
    }//end class
}//end namespace
