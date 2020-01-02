using System;
using Rainbow.Security; 
using Rainbow.Configuration;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// Summary description for DiscussionHelpers.
	/// </summary>
	public class DiscussionPermissions
	{
		/// <summary>
		/// See whether the current user has permissions to add a post to the discussion thread
		/// </summary>
		/// <param name="ModuleID">ID of the current Discussion Module</param>
		/// <returns>Returns true or flase</returns>
		public static bool HasAddPermissions(int ModuleID) 
		{
			if (PortalSecurity.HasAddPermissions(ModuleID) == true)
				return true;
			else
				return false;
		}

		public static bool HasEditPermissions(int ModuleID, string itemUserEmail) 
		{
			// this approach willnot be safe when we change from UserEmail to UserID
			// if UserID is not unique accross ALL portal instances on a given database
			string currentUserEmail = PortalSettings.CurrentUser.Identity.Email;

			if (PortalSecurity.HasEditPermissions(ModuleID) == true
				|| currentUserEmail == itemUserEmail)
				return true;
			else
				return false;
		}
		
		public static bool HasDeletePermissions(int ModuleID, int itemID, string itemUserEmail) 
		{
			string currentUserEmail = PortalSettings.CurrentUser.Identity.Email;
			
			if (PortalSecurity.HasDeletePermissions(ModuleID) == true)
				// || currentUserEmail == itemUserEmail))
				// also need to check for NUMBER of children
				// so someone doesn't delte a post with children
				// or just reattach the children
				return true;
			else
				return false;
		}

	}
}
