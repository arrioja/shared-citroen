using System;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.ComponentModel;
using System.Web;
using System.Web.Mail;
using System.Web.Caching;
using System.Security;
using System.Security.Principal;
using System.Web.Security;
using System.Data.SqlClient;
using System.Threading;
using System.Globalization;
using System.Security.Permissions;

using Rainbow.Admin;
using Rainbow.Design;
using Rainbow.Security;
using Rainbow.Configuration;
using Rainbow.Scheduler;


namespace Rainbow
{
	// 28_Aug_2003 Cory Isakson implmented the new webcompile DataType
	// The new class iterates through the site to compile all of the pages

	public class Global : Rainbow.UI.DataTypes.WebCompile.GlobalBase
	{
		//public static bool dbNeedsUpdate = false;

		
		#region WebCompile Overrides

			protected override int KeepAliveMinutes 
			{
				get { return 0; } // Should be less than Session Timeout, 0 disables
			}

			protected override string SkipFiles 
			{
				get { 
					// Skip the MobileDefault and Default pages
					//physicalPath = HttpContext.Current.Request.PhysicalApplicationPath;
					return @"mobiledefault.aspx;default.aspx"; 
				}
			}
			protected override string SkipFolders 
			{
				get { return @""; }
			}

			// Add the following only if you want to handle the Elapsed Event
			//public Global() 
			//{
			//	this.Elapsed += new System.EventHandler(this.Application_Elapsed);
			//}
			//
			//protected void Application_Elapsed(Object sender, EventArgs e) 
			//{
			//	// Fires when the KeepAliveMinutes has Elapsed
			//}
		#endregion

		static bool IsInteger(string str) 
		{
			foreach(char c in str) 
			{
				if(!Char.IsNumber(c)) 
				{
					return false;
				}
			}
			return true;
		}
		

		/// <summary>
		/// Application_BeginRequest Event
		///
		/// The Application_BeginRequest method is an ASP.NET event that executes 
		/// on each web request into the portal application.  The below method
		/// obtains the current tabIndex and TabID from the querystring of the 
		/// request -- and then obtains the configuration necessary to process
		/// and render the request.
		///
		/// This portal configuration is stored within the application's "Context"
		/// object -- which is available to all pages, controls and components
		/// during the processing of a single request.
		/// 
		/// Changed portal alias behavior. Now you can change portals via querystring.
		/// An option was added to web.config:
		/// IgnoreFirstDomain (boolean): if true the trailing domin is ignored
		/// (as in original rainbow), if false the full domain is considered.
		/// Please note that changing this option requires the complete update 
		/// of every portalalias on db.
		/// Thanks to Cory Isakson for suggestions and vb code. 2002/11/29
		/// </summary>
		[History("john.mandia@whitelightsolutions.com", "2004/05/29", "changed portal alias checking behavior")]
		[History("manu", "2002/11/29", "changed portal alias behavior")]
		[History("cory isakson", "2003/02/13", "changed portal alias behavior: always carry portal alias in URL or QS")]
		[History("bill anderson", "2003/05/13", "track user information for anonymous (min/max/close)")]
		[History("cory isakson", "2003/09/10", "added overrides for WebCompile Feature")]
		protected void Application_BeginRequest(Object sender, EventArgs e) 
		{
			//Important patch http://support.microsoft.com/?kbid=887459
			if (Request.Path.IndexOf('\\') >= 0 || System.IO.Path.GetFullPath(Request.PhysicalPath) != Request.PhysicalPath) 
			{
				throw new HttpException(404, "not found");
			}

			// Check dbVersion
			if (!Request.RawUrl.EndsWith("/Setup/Update.aspx") && PortalSettings.DatabaseVersion < PortalSettings.CodeVersion)
				Response.Redirect(Rainbow.Settings.Path.ApplicationRoot + "/Setup/Update.aspx");

			// john.mandia@whitelightsolutions.com: If we are going to update and update.aspx doesn't call any portalsettings why call it. Especially when this may be with a fresh db.
			if(!Request.RawUrl.EndsWith("/Setup/Update.aspx"))
			{
				//flags and variables for Alias and TabID
				int tabID = 0;
				// Another Alteration By john.mandia@whitelightsolutions.com
				//string portalAlias = null;
				string portalAlias = Rainbow.Settings.Portal.UniqueID;
			
				string cookiePortalAlias = null;
				bool saveCookie = false;
				bool refreshSite = false;    
				PortalSettings settings;

				//Note: TabID, Alias, and Language can be passed in Querystring
				//		The HTTP Handler will make them appear as virtual directories

				// Get TabID from QueryString
				// by manu, change to faster execution testing with no try catch
				if (Request.Params["TabID"] != null && IsInteger(Request.Params["TabID"])) 
					tabID = Int32.Parse(Request.Params["tabID"]);

				//Try to get alias from cookie to determine if Alias has been changed
				if (Request.Cookies["PortalAlias"] != null)
				{
					cookiePortalAlias = Request.Cookies["PortalAlias"].Value;
					if (cookiePortalAlias.ToUpper() != portalAlias.ToUpper())
					{
						//Portal has changed since last page request
						refreshSite = true;
						saveCookie = true;
					}
				}
				else
				{	
					//First time visit to portal
					// --- change by Thierry (tiptopweb) 12/4/2003
					// removed : this is creating a deadlock if the user does not have cookies enabled!
					//refreshSite = true;  
					// --- end change by Thierry (tiptopweb) 12/4/2003
					saveCookie = true;
				}

				// Jes1111 - temporarily disabled because of new language stuff, until a solution is found
				//			if(ConfigurationSettings.AppSettings["PortalSettingCaching"] != null)
				//			{
				//				//Caching code
				//				int expireinseconds = Int32.Parse(ConfigurationSettings.AppSettings["PortalSettingCaching"]);
				//				if(expireinseconds > 0)
				//				{
				//					string uniqueCacheData = string.Concat(tabID, "-", portalAlias);
				//					if(Context.Cache[uniqueCacheData] == null)
				//					{
				//						settings = new PortalSettings(tabID, portalAlias);
				//						Context.Cache.Insert(uniqueCacheData, settings, null, Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(expireinseconds));
				//					}
				//					else
				//					{
				//						settings = (PortalSettings) Context.Cache[uniqueCacheData];
				//					}
				//				}
				//				else
				//				{
				//					//No cache
				//					settings = new PortalSettings(tabID, portalAlias);
				//				}
				//				//end Caching code
				//			}
				//			else
				//			{
				//				//No caching code
				//				settings = new PortalSettings(tabID, portalAlias);
				//				//end No caching code
				//			}
				//john.mandia@whitelightsolutions.com: Added Try Catch Here and added special redirect
				//http://sourceforge.net/tracker/index.php?func=detail&aid=956528&group_id=66837&atid=515929
				try 
				{
					settings = new PortalSettings(tabID, portalAlias);
					//Check to see that the current portalAlias was a valid Alias
					if(settings.PortalAlias == null)
					{
						// try the default portal
						//Domain is not a valid alias we revert to Default Alias set in web.config
						//Rainbow.Helpers.LogHelper.Logger.Log(Rainbow.Configuration.LogLevel.Info, portalAlias + " is not a valid alias. We revert to Default Alias set in web.config");
						
						portalAlias = ConfigurationSettings.AppSettings["DefaultPortal"];
						settings = new PortalSettings(tabID, portalAlias);
					
						if(settings.PortalAlias == null)
						{
							// Default Alias Failed Log Error And Redirect To Friendly Page
							Rainbow.Helpers.LogHelper.Logger.Log(Rainbow.Configuration.LogLevel.Info, "Default Alias set in web.config did not work when called from global.asax");
							Response.Redirect("~/app_support/ErrorNoPortal.html",true);
						}
						saveCookie = true;
						
					}
					portalAlias = settings.PortalAlias;

					// Portal Settings has passed the test so add it to Context
					Context.Items.Add("PortalSettings", settings);
				}
				catch (Exception ex)
				{
					// Had problems getting PortalSettings
					Rainbow.Helpers.LogHelper.Logger.Log(Rainbow.Configuration.LogLevel.Info, "An Error Occurred In Global.asax when trying to get portalsettings. If the error is thread abort it means portalalias:" + portalAlias + " could not be found and the user was redirected.", ex);
					Response.Redirect("~/app_support/ErrorNoPortal.html",true);
				}// End of john.mandia@whitelightsolutions.com Mod
			
				// Save cookies
				if (saveCookie)
				{
					Response.Cookies["PortalAlias"].Path = "/";
					Response.Cookies["PortalAlias"].Value = portalAlias;
				}

				// --- change by Thierry (tiptopweb) 12/4/2003
				// the previous refresh on the command line could be generating problems
				// with links to the portal from emails... 
				// if switching portals then clean parameters 
				// Must be the last instruction in this method 
				if (refreshSite) // this should be called only if Cookies enabled!
				{
					// Signout and force the browser to refresh
					// only once to avoid any dead-lock
					if (Request.Cookies["refreshed"] == null || (Request.Cookies["refreshed"] != null && Response.Cookies["refreshed"].Value == "false"))
					{
						string rawUrl = HttpContext.Current.Request.RawUrl;

						//by Manu avoid endless loop when portal does not exists
						if (rawUrl.EndsWith("init"))
							Response.Redirect("~/app_support/ErrorNoPortal.html",true);

						// add parameter at the end of the command line to detect the dead-lock 
						if (rawUrl.LastIndexOf(@"?") > 0)
							rawUrl += "&init";
						else rawUrl += "?init";

						Response.Cookies["refreshed"].Value = "true";
						Response.Cookies["refreshed"].Path = "/";
						Response.Cookies["refreshed"].Expires = DateTime.Now.AddMinutes(1);

						// sign-out, if refreshed param on the command line we will not call it again
						PortalSecurity.SignOut(rawUrl, false);
					}
				}

				// invalidate cookie, so the page can be refreshed when needed
				if(Request.Cookies["refreshed"] != null)
				{
					Response.Cookies["refreshed"].Path = "/";
					Response.Cookies["refreshed"].Value = "false";
					Response.Cookies["refreshed"].Expires = DateTime.Now.AddMinutes(1);
				}
				// --- end change by Thierry (tiptopweb) 12/4/2003
			}
		}

		/// <summary>
		/// If the client is authenticated with the application, then determine
		/// which security roles he/she belongs to and replace the "User" intrinsic
		/// with a custom IPrincipal security object that permits "User.IsInRole"
		/// role checks within the application
		///
		/// Roles are cached in the browser in an in-memory encrypted cookie.  If the
		/// cookie doesn't exist yet for this session, create it.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Application_AuthenticateRequest(Object sender, EventArgs e) 
		{
			// john.mandia@whitelightsolutions.com: Check. If it's a fresh db then then is no portalSettings instance. 
			if(HttpContext.Current.Items["PortalSettings"] != null && !Request.RawUrl.EndsWith("/Setup/Update.aspx"))
			{
				// Obtain PortalSettings from Current Context
				PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];

				// Auto-login a user who has a portal Alias login cookie
				// Try to authenticate the user with the cookie value
				if ((Request.IsAuthenticated != true) && (Request.Cookies["Rainbow_" + portalSettings.PortalAlias] != null))
				{
					string user;
					user = Request.Cookies["Rainbow_" + portalSettings.PortalAlias.ToLower()].Value;
				
					// Create the FormsAuthentication cookie
					FormsAuthentication.SetAuthCookie(user, true);	

					// Create a FormsAuthentication ticket.
					FormsAuthenticationTicket cTicket = new FormsAuthenticationTicket
						(
						1,                              // version
						user,							// user name
						DateTime.Now,                   // issue time
						DateTime.Now.AddHours(1),       // expires every hour
						false,                          // don't persist cookie
						string.Empty								// roles
						);
				
					// Set the current User Security to the FormsAuthenticated User
					//Context.User = new GenericPrincipal(new FormsIdentity(cTicket), null);
					Context.User = new RainbowPrincipal(new FormsIdentity(cTicket), null);
				}

				// 4.Apr.2003 NTLM check was not working so I replaced it with a check to security.principal (Cory Isakson)
				// if (Request.IsAuthenticated == true && Context.User.Identity.AuthenticationType != "NTLM")
				if (Request.IsAuthenticated == true && !(HttpContext.Current.User is System.Security.Principal.WindowsPrincipal))
				{
				// added by Jonathan Fong 22/07/2004 to support LDAP 
				string[] names = Context.User.Identity.Name.Split("|".ToCharArray());
				if (names.Length == 3 && names[2].StartsWith("cn="))
				{
					Context.User = new RainbowPrincipal(
						new Rainbow.Security.User(Context.User.Identity.Name, "LDAP"), Helpers.LDAPHelper.GetRoles(names[2]));
				} 
				else
				{
					// Add our own custom principal to the request containing the roles in the auth ticket
					//Context.User = new GenericPrincipal(Context.User.Identity, roles);
					Context.User = new RainbowPrincipal(Context.User.Identity, Security.PortalSecurity.GetRoles());
				}
					// 28.Apr.2003 Remove Windows Specific settings for Forms authentication users
					// Remove Windows specific custom settings
					if (portalSettings.CustomSettings != null)
						portalSettings.CustomSettings.Remove("WindowsAdmins");
				}
					// START bja@reedtek.com
					// need to get a unique id for user
				else if ( Request.IsAuthenticated == false && Rainbow.BLL.Utils.GlobalResources.SupportWindowMgmt) 
				{	
					// [Needed a uid, even for annoymous users] - START bja@reedtek.com
					string annoyUser = string.Empty;
					// cookie bag
					BLL.Utils.IWebBagHolder abag =  BLL.Utils.BagFactory.instance.create(BLL.Utils.BagFactory.BagFactoryType.CookieType);
					// user data already set
					annoyUser = (string)abag[Rainbow.Configuration.GlobalInternalStrings.UserWinMgmtIndex];
					// if no cookie then let's get one
					if ( annoyUser == null )
					{
						// new uid for window mgmt.
						System.Guid guid = System.Guid.NewGuid();
						// save the data into a cookie bag
						abag[Rainbow.Configuration.GlobalInternalStrings.UserWinMgmtIndex] =  guid.ToString();					
					} 
				}
				// END bja@reedtek.com

			}
		} // end of Application_AuthenticateRequest

		protected void Application_Error(object sender, System.EventArgs e)
		{
			ErrorHandler.HandleException();
		}

		/// <summary>
		/// Application_Start()
		/// </summary>
		protected void Application_Start()
		{
//			Rainbow.Helpers.LogHelper.Logger.Log(Rainbow.Configuration.LogLevel.Info, "Application Started, ver " + PortalSettings.CodeVersion);
		
			if(bool.Parse(ConfigurationSettings.AppSettings["CheckForFilePermission"].ToString()))
			{
				// Verify if ASPN net user has rights on application folder
				FileIOPermission MyPermission = new FileIOPermission(FileIOPermissionAccess.Write, HttpContext.Current.Request.PhysicalApplicationPath);
				try
				{
					MyPermission.Demand(); //it doesn't work :( - if anybody make it working please let me know (manu)
				}
				catch(Exception ex)
				{
					Rainbow.Helpers.LogHelper.Logger.Log(Rainbow.Configuration.LogLevel.Fatal, "ASPNET Account does not have rights to the filesystem", ex);
					HttpContext.Current.Response.Redirect("~/app_support/ErrorNoFilePermission.html",false);
					ErrorHandler.HandleException("ASPNET Account does not have rights to the filesystem",ex);
				}

				try
				{
					string myNewDir = Path.Combine(Rainbow.Settings.Path.ApplicationPhysicalPath, "_createnewdir");


					if (!System.IO.Directory.Exists(myNewDir))
						System.IO.Directory.CreateDirectory(myNewDir);
                
					if (System.IO.Directory.Exists(myNewDir))
						System.IO.Directory.Delete(myNewDir);
				}
				catch(Exception ex)
				{
					Rainbow.Helpers.LogHelper.Logger.Log(Rainbow.Configuration.LogLevel.Fatal, "ASPNET Account does not have rights to the filesystem", ex);
					HttpContext.Current.Response.Redirect("~/app_support/ErrorNoFilePermission.html",false);
					ErrorHandler.HandleException("ASPNET Account does not have rights to the filesystem", ex);
					
				}
			}

			//Esperantus settings
			//If not found on web.confg defaults are provided here
			try
			{
				// Get Esperantus config
				NameValueCollection mySettings = (NameValueCollection) ConfigurationSettings.GetConfig("Esperantus");

				//Initializes default key and countries store
				if (mySettings != null) //Esperantus section must be specified anyway
				{
					string baseDir = "/";
					if (Context.Request.ApplicationPath.Length > 0)
						baseDir = Context.Request.ApplicationPath;
					baseDir = Server.MapPath(baseDir);
					string binDir = Path.Combine(baseDir, "bin");
					string resDir = Path.Combine(baseDir, "Resources");

					// Try to get KeyStore, if not found set defaults
					if (mySettings["KeysStore"] == null && mySettings["KeysStoreParameters"] == null)
					{
						string esperantusConfig = @"AssemblyName=" + Path.Combine(binDir, "Rainbow.dll") + ";KeysSubStore=Rainbow.Resources.Rainbow;Path=" + resDir + ";FilesPrefix=Rainbow";
						//Rainbow.Helpers.LogHelper.Logger.Log(Rainbow.Configuration.LogLevel.Info, "Running Esperantus from: " + esperantusConfig);
						Esperantus.Data.DataFactory.SetDefaultKeysStore("Hybrid", esperantusConfig);
					}

					// CountryRegionsStore as reinbow default
					if (mySettings["CountryRegionsStore"] == null && mySettings["CountryRegionsStoreParameters"] == null)
					{
						Esperantus.Data.DataFactory.SetDefaultCountryRegionsStore("Resources", @"AssemblyName=" + binDir + @"\Rainbow.dll;CountriesSubStore=Rainbow.Resources.Countries.xml");
					}

					// ***********************************************
					// * Uncomment this code for overwrite Resources/Countries.xml
					// * with you custom database list, then rebuil reainbow
					// * and comment it again
					// ***********************************************
					//				if (mySettings["CountryRegionsStore"] == null && mySettings["CountryRegionsStoreParameters"] == null)
					//				{
					//					// We try to convert a SQLProvider string in a valid OleDb SQL connection string. It is not perfect.
					//					string toBeTransformedConnectionString = ConfigurationSettings.AppSettings["connectionString"].ToLower();
					//					toBeTransformedConnectionString = toBeTransformedConnectionString.Replace("trusted_connection=true", "Integrated Security=SSPI");
					//					toBeTransformedConnectionString = toBeTransformedConnectionString.Replace("database=", "Initial Catalog=");
					//					toBeTransformedConnectionString = toBeTransformedConnectionString.Replace("server=", "Data Source=");
					//					//Rainbow.Helpers.LogHelper.Logger.Log(Rainbow.Configuration.LogLevel.Info, "Running Esperantus Countries from: " + toBeTransformedConnectionString);
					//					Esperantus.Data.DataFactory.SetDefaultCountryRegionsStore("OleDB", "Provider=SQLOLEDB.1;" + toBeTransformedConnectionString);
					//
					//					Esperantus.CountryInfo.SaveCountriesAsXML(Esperantus.CountryTypes.AllCountries, Esperantus.CountryFields.Name, Path.Combine(resDir, "Countries.xml"));
					//				}
				}
			}
			catch(Exception ex)
			{
				Rainbow.Helpers.LogHelper.Logger.Log(Rainbow.Configuration.LogLevel.Fatal, "Error in esperantus section", ex);
				Response.End();
			}
			
			//Start scheduler
			if(ConfigurationSettings.AppSettings["SchedulerEnable"].ToLower() == "yes") 
			{
				int cacheSize;
				long period;

				try {
					cacheSize = int.Parse( ConfigurationSettings.AppSettings["SchedulerCacheSize"].ToLower() );
				}
				catch {
					cacheSize = 50;
				}

				try {
					period = int.Parse( ConfigurationSettings.AppSettings["SchedulerPeriod"].ToLower() );
				}
				catch {
					period = 60000;
				}

				PortalSettings.Scheduler = CachedScheduler.GetScheduler(
					HttpContext.Current.Server.MapPath(Rainbow.Settings.Path.ApplicationRoot),
					PortalSettings.SqlConnectionString,
					period,
					cacheSize);
				PortalSettings.Scheduler.Start();
			}

			if(ConfigurationSettings.AppSettings.Get("UseProxyServerForServerWebRequests") == "true")
			{
				System.Net.GlobalProxySelection.Select = PortalSettings.GetProxy();
			}
		}
	}
}