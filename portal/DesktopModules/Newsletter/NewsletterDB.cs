using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mail;
using Rainbow.Configuration;
using Rainbow.Helpers;
using Rainbow.Settings;

namespace Rainbow.DesktopModules
{
    /// <summary>
    /// Class that encapsulates all data logic
    /// necessary to send newsletters.
    /// </summary>
    public class NewsletterDB
    {
        /// <summary>
        /// Get only the records with SendNewsletter enabled from the "Users" database table.
        /// Uses GetUsersNewsletter stored procedure.
        /// </summary>
        /// <param name="portalID"></param>
        /// <param name="MaxUsers"></param>
        /// <param name="MinSend"></param>
        /// <returns></returns>
        public SqlDataReader GetUsersNewsletter(int portalID, int MaxUsers, int MinSend) 
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlCommand myCommand = new SqlCommand("rb_GetUsersNewsletter", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterPortalID = new SqlParameter("@PortalID", SqlDbType.Int);
            parameterPortalID.Value = portalID;
            myCommand.Parameters.Add(parameterPortalID);

            SqlParameter parameterMaxUsers = new SqlParameter("@MaxUsers", SqlDbType.Int);
            parameterMaxUsers.Value = MaxUsers;
            myCommand.Parameters.Add(parameterMaxUsers);

            SqlParameter parameterMinSend = new SqlParameter("@MinSend", SqlDbType.Int);
            parameterMinSend.Value = MinSend;
            myCommand.Parameters.Add(parameterMinSend);

            // Open the database connection and execute the command
            myConnection.Open();
            SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Return the datareader
            return dr;
        }

        /// <summary>
        /// The GetUsersNewsletterCount method returns the users count.
        /// Uses GetUsersNewsletter Stored Procedure.
        /// </summary>
        /// <param name="portalID"></param>
        /// <returns></returns>
        public int GetUsersNewsletterCount(int portalID, int MaxUsers, int MinSend) 
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlCommand myCommand = new SqlCommand("rb_GetUsersNewsletter", myConnection);
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            SqlParameter parameterPortalID = new SqlParameter("@PortalID", SqlDbType.Int);
            parameterPortalID.Value = portalID;
            myCommand.Parameters.Add(parameterPortalID);

            SqlParameter parameterUserCount = new SqlParameter("@UserCount", SqlDbType.Int, 4);
            parameterUserCount.Direction = ParameterDirection.Output;
            myCommand.Parameters.Add(parameterUserCount);

            SqlParameter parameterMaxUsers = new SqlParameter("@MaxUsers", SqlDbType.Int);
            parameterMaxUsers.Value = MaxUsers;
            myCommand.Parameters.Add(parameterMaxUsers);

            SqlParameter parameterMinSend = new SqlParameter("@MinSend", SqlDbType.Int);
            parameterMinSend.Value = MinSend;
            myCommand.Parameters.Add(parameterMinSend);

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

            return (int)parameterUserCount.Value;
        }

        /// <summary>
        /// The SendNewsletterTo marks the provided email with the last
        /// send date, this avoids multiple send to the same email within specified frame.
        /// Uses SendNewsletterTo Stored Procedure.
        /// </summary>
        /// <param name="portalID"></param>
        /// <returns></returns>
        public void SendNewsletterTo(int portalID, string EMail) 
        {
            // Create Instance of Connection and Command Object
            SqlConnection myConnection = PortalSettings.SqlConnectionString;
            SqlCommand myCommand = new SqlCommand("rb_SendNewsletterTo", myConnection);
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
  
        public string SendMessage(string From, string To, string Name, string Pwd, string LoginPage, string Subject, string Body, bool Send, bool HtmlMode, bool breakLines)
        {
			// Obtain PortalSettings from Current Context
			PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];

            string LoginUrl;
            //If an alternate home is given use this
            if (LoginPage.Length > 0)
                LoginUrl = LoginPage + "?Usr=" + To + "&Pwd=" + Pwd;
            else
                LoginUrl = Path.WebPathCombine(Path.ApplicationFullPath, "/DesktopModules/Admin/Logon.aspx?Usr=" + To + "&Pwd=" + Pwd + "&Alias=" + portalSettings.PortalAlias);

        	MailFormat format;
            
            //Interprets TAGS
            //{NAME} = UserName
            //{PASSWORD} = Password
            //{EMAIL} = UserEmail
            //{LOGINURL} = A direct url that can be used to logon automatically
            Body = Body.Replace("{NAME}" , Name);
            Body = Body.Replace("{PASSWORD}" , Pwd);
            if (HtmlMode)
            {
                format = MailFormat.Html;
                Body = Body.Replace("{EMAIL}", "<A Href=\"mailto:" + To + "\">" + To + "</A>");
                Body = Body.Replace("{LOGINURL}", "<A Href=\"" + LoginUrl + "\">" + LoginUrl + "</A>");
                
				//This option is useful is you type the text and you want to send as html.
				if (breakLines)
					Body = Body.Replace("\n", "<br>");
            }
            else
            {
                format = MailFormat.Text;
                Body = Body.Replace("{EMAIL}", To);
                Body = Body.Replace("{LOGINURL}", LoginUrl);

                //Break rows - must be the last
                Body = ((HTMLText) Body).GetBreakedText(78);
            }

            // Send only if true
            if (Send)
            {
                MailMessage mail = new MailMessage();
                mail.From = From;
                mail.To = To;
                mail.Subject = Subject;
                mail.Body = Body;
                mail.BodyFormat = format;
                mail.Priority = MailPriority.Low;
				SmtpMail.SmtpServer = Portal.SmtpServer;     
				SmtpMail.Send(mail);
            }
            return Body;
        }
    }
}
