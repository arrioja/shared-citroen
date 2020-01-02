using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.XPath;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// Class that encapsulates all data logic necessary
	/// articles within the Portal database.
	/// </summary>
	public class ArticlesDB 
	{
		//This is used as a common setting from Articles
		public static string ImagesSetting = "ImageCollection";

		/// <summary>
		/// The GetArticles method returns a SqlDataReader containing all of the
		/// Articles for a specific portal module from the announcements
		/// database.
		/// <a href="GetArticles.htm" style="color:green">GetArticles Stored Procedure</a>
		/// </summary>
		/// <param name="moduleID"></param>
		/// <returns></returns>
		public SqlDataReader GetArticles(int moduleID)
		{
			// Create Instance of Connection and Command Object
			SqlConnection myConnection = PortalSettings.SqlConnectionString;
			SqlCommand myCommand = new SqlCommand("rb_GetArticles", myConnection);

			// Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure;

			// Add Parameters to SPROC
			SqlParameter parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
			parameterModuleID.Value = moduleID;
			myCommand.Parameters.Add(parameterModuleID);

			// Execute the command
			myConnection.Open();
			SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            
			// Return the datareader 
			return result;
		}

		/// <summary>
		/// The GetSingleArticle method returns a SqlDataReader containing details
		/// about a specific Article from the Articles database table.
		/// </summary>
		/// <param name="itemID"></param>
		/// <returns></returns>
		public SqlDataReader GetSingleArticle(int itemID) 
		{
			// Create Instance of Connection and Command Object
			SqlConnection myConnection = PortalSettings.SqlConnectionString;
			SqlCommand myCommand = new SqlCommand("rb_GetSingleArticle", myConnection);

			// Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure;

			// Add Parameters to SPROC
			SqlParameter parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
			parameterItemID.Value = itemID;
			myCommand.Parameters.Add(parameterItemID);

			// Execute the command
			myConnection.Open();
			SqlDataReader result = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            
			// Return the datareader 
			return result;
		}

		/// <summary>
		/// The GetSingleArticleWithImages method returns a SqlDataReader containing details
		/// about a specific Article from the Articles database table.
		/// </summary>
		/// <param name="itemID"></param>
		/// <param name="variation"></param>
		/// <returns></returns>
		public DataSet GetSingleArticleWithImages(int itemID, string variation) 
		{
			// Create Instance of Connection and Command Object
			SqlConnection myConnection = PortalSettings.SqlConnectionString;
			SqlCommand myCommand = new SqlCommand("rb_GetSingleArticleWithImages", myConnection);

			// Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure;

			// Add Parameters to SPROC
			SqlParameter parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
			parameterItemID.Value = itemID;
			myCommand.Parameters.Add(parameterItemID);

			SqlParameter parameterVariation = new SqlParameter("@Variation", SqlDbType.VarChar, 50);
			parameterVariation.Value = variation;
			myCommand.Parameters.Add(parameterVariation);

			SqlDataAdapter da = new SqlDataAdapter();
			da.SelectCommand = myCommand;

			// create a dataset and fill it
			DataSet ds = new DataSet();

			myConnection.Open();
			try
			{
				da.Fill(ds);
			}
			finally
			{
				myConnection.Close(); //by Manu fix close bug #2
			}

			// Return the dataset
			return ds;
		}

		/// <summary>
		/// The DeleteArticle method deletes a specified Article from
		/// the Articles database table.
		/// </summary>
		/// <param name="itemID"></param>
		public void DeleteArticle(int itemID) 
		{
			// Create Instance of Connection and Command Object
			SqlConnection myConnection = PortalSettings.SqlConnectionString;
			SqlCommand myCommand = new SqlCommand("rb_DeleteArticle", myConnection);

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
		/// The AddArticle method adds a new Article within the
		/// Articles database table, and returns ItemID value as a result.
		/// </summary>
		/// <param name="moduleID"></param>
		/// <param name="userName"></param>
		/// <param name="title"></param>
		/// <param name="subtitle"></param>
		/// <param name="articleAbstract"></param>
		/// <param name="description"></param>
		/// <param name="startDate"></param>
		/// <param name="expireDate"></param>
		/// <param name="isInNewsletter"></param>
		/// <param name="moreLink"></param>
		/// <returns></returns>
		public int AddArticle(int moduleID, string userName, string title, string subtitle, string articleAbstract, string description, DateTime startDate, DateTime expireDate, bool isInNewsletter, string moreLink) 
		{
			if (userName.Length < 1) 
			{
				userName = "unknown";
			}

			// Create Instance of Connection and Command Object
			SqlConnection myConnection = PortalSettings.SqlConnectionString;
			SqlCommand myCommand = new SqlCommand("rb_AddArticle", myConnection);

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

			SqlParameter parameterSubtitle = new SqlParameter("@Subtitle", SqlDbType.NVarChar, 200);
			parameterSubtitle.Value = subtitle;
			myCommand.Parameters.Add(parameterSubtitle);

			SqlParameter parameterAbstract = new SqlParameter("@Abstract",SqlDbType.NVarChar, 512);
			parameterAbstract.Value = articleAbstract;
			myCommand.Parameters.Add(parameterAbstract);

			SqlParameter parameterDescription = new SqlParameter("@Description", SqlDbType.NText);
			parameterDescription.Value = description;
			myCommand.Parameters.Add(parameterDescription);

			SqlParameter parameterStartDate = new SqlParameter("@StartDate", SqlDbType.DateTime);
			parameterStartDate.Value = startDate;
			myCommand.Parameters.Add(parameterStartDate);

			SqlParameter parameterExpireDate = new SqlParameter("@ExpireDate", SqlDbType.DateTime);
			parameterExpireDate.Value = expireDate;
			myCommand.Parameters.Add(parameterExpireDate);

			SqlParameter parameterIsInNewsletter = new SqlParameter("@IsInNewsletter", SqlDbType.Bit);
			parameterIsInNewsletter.Value = isInNewsletter;
			myCommand.Parameters.Add(parameterIsInNewsletter);

			SqlParameter parameterMoreLink = new SqlParameter("@MoreLink", SqlDbType.NVarChar, 150);
			parameterMoreLink.Value = moreLink;
			myCommand.Parameters.Add(parameterMoreLink);
			
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
		/// The UpdateArticle method updates a specified Article within
		/// the Articles database table.
		/// </summary>
		/// <param name="moduleID"></param>
		/// <param name="itemID"></param>
		/// <param name="userName"></param>
		/// <param name="title"></param>
		/// <param name="subtitle"></param>
		/// <param name="articleAbstract"></param>
		/// <param name="description"></param>
		/// <param name="startDate"></param>
		/// <param name="expireDate"></param>
		/// <param name="isInNewsletter"></param>
		/// <param name="moreLink"></param>
		public void UpdateArticle(int moduleID, int itemID, string userName, string title, string subtitle, string articleAbstract, string description, DateTime startDate, DateTime expireDate, bool isInNewsletter, string moreLink) 
		{
			if (userName.Length < 1) 
			{
				userName = "unknown";
			}

			// Create Instance of Connection and Command Object
			SqlConnection myConnection = PortalSettings.SqlConnectionString;
			SqlCommand myCommand = new SqlCommand("rb_UpdateArticle", myConnection);

			// Mark the Command as a SPROC
			myCommand.CommandType = CommandType.StoredProcedure;

			// Add Parameters to SPROC
			SqlParameter parameterItemID = new SqlParameter("@ItemID", SqlDbType.Int, 4);
			parameterItemID.Value = itemID;
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

			SqlParameter parameterSubtitle = new SqlParameter("@Subtitle", SqlDbType.NVarChar, 200);
			parameterSubtitle.Value = subtitle;
			myCommand.Parameters.Add(parameterSubtitle);

			SqlParameter parameterAbstract = new SqlParameter("@Abstract", SqlDbType.NVarChar, 512);
			parameterAbstract.Value = articleAbstract;
			myCommand.Parameters.Add(parameterAbstract);

			SqlParameter parameterDescription = new SqlParameter("@Description", SqlDbType.NText);
			parameterDescription.Value = description;
			myCommand.Parameters.Add(parameterDescription);

			SqlParameter parameterStartDate = new SqlParameter("@StartDate", SqlDbType.DateTime);
			parameterStartDate.Value = startDate;
			myCommand.Parameters.Add(parameterStartDate);

			SqlParameter parameterExpireDate = new SqlParameter("@ExpireDate", SqlDbType.DateTime);
			parameterExpireDate.Value = expireDate;
			myCommand.Parameters.Add(parameterExpireDate);

			SqlParameter parameterIsInNewsletter = new SqlParameter("@IsInNewsletter", SqlDbType.Bit);
			parameterIsInNewsletter.Value = isInNewsletter;
			myCommand.Parameters.Add(parameterIsInNewsletter);

			SqlParameter parameterMoreLink = new SqlParameter("@MoreLink", SqlDbType.NVarChar, 150);
			parameterMoreLink.Value = moreLink;
			myCommand.Parameters.Add(parameterMoreLink);

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