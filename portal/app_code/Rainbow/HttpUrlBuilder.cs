using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Caching;
using Rainbow.Configuration;
using Rainbow.Web;


namespace Rainbow
{
	/// <summary>
	/// HttpUrlBuilder
	/// This Class is Responsible for all the Urls in Rainbow to prevent
	/// hardcoded urls. 
	/// This makes it easier to update urls across the multiple portals
	/// Original ideas from John Mandia, Cory Isakson, Jes and Manu.
	/// </summary>
	[History("john.mandia@whitelightsolutions.com", "2004/09/2", "Introduced the provider pattern for UrlBuilder so that people can implement their rules for how urls should be built")]
	[History("john.mandia@whitelightsolutions.com", "2003/08/13", "Removed Keywords splitter - rebuilt handler code to use a rules engine and changed code on url builder to make it cleaner and compatible")]
	[History("Jes1111", "2003/03/18", "Added Keyword Splitter feature, see explanation in web.config")]
	[History("Jes1111", "2003/04/24", "Fixed problem with '=' in Keyword Splitter")]
	public class HttpUrlBuilder
	{
		private static UrlBuilderProvider provider = UrlBuilderProvider.Instance();

		/// <summary> 
		/// Builds the url for get to current portal home page
		/// containing the application path, portal alias, tab ID, and language. 
		/// </summary> 
		public static string BuildUrl()
		{
			return(BuildUrl("~/" + DefaultPage, 0, 0, null, string.Empty, string.Empty, string.Empty));
		}

		/// <summary> 
		/// Builds the url for get to current portal home page
		/// containing the application path, portal alias, tab ID, and language. 
		/// </summary> 
		/// <param name="targetPage">Linked page</param> 
		public static string BuildUrl(string targetPage)
		{
			return(BuildUrl(targetPage, 0, 0, null, string.Empty, string.Empty, string.Empty));	
		}

		/// <summary> 
		/// Builds the url for get to current portal home page
		/// containing the application path, portal alias, tab ID, and language. 
		/// </summary> 
		/// <param name="targetPage">Linked page</param> 
		/// <param name="customAttributes">Any custom attribute that can be needed</param> 
		public static string BuildUrl(string targetPage, string customAttributes)
		{
			return(BuildUrl(targetPage, 0, 0, null, customAttributes, string.Empty, string.Empty));
		}

		/// <summary> 
		/// Takes a Tab ID and builds the url for get the desidered page 
		/// containing the application path, portal alias, tab ID, and language. 
		/// </summary> 
		/// <param name="tabID">ID of the tab</param> 
		public static string BuildUrl(int tabID)
		{
			return(BuildUrl("~/" + DefaultPage, tabID, 0, null, string.Empty, string.Empty, string.Empty));
		}

		/// <summary> 
		/// Takes a Tab ID and builds the url for get the desidered page 
		/// containing the application path, portal alias, tab ID, and language. 
		/// </summary> 
		/// <param name="tabID">ID of the tab</param> 
		/// <param name="urlKeywords">Add some keywords to uniquely identify this tab. Usual source is UrlKeyword from TabSettings.</param> 
		public static string BuildUrl(int tabID, string urlKeywords)
		{
			return(BuildUrl("~/" + DefaultPage, tabID, 0, null, string.Empty, string.Empty, urlKeywords));
		}

		/// <summary> 
		/// Takes a Tab ID and builds the url for get the desidered page (non default)
		/// containing the application path, portal alias, tab ID, and language. 
		/// </summary> 
		/// <param name="targetPage">Linked page</param> 
		/// <param name="tabID">ID of the tab</param> 
		public static string BuildUrl(string targetPage, int tabID)
		{
			return(BuildUrl(targetPage, tabID, 0, null, string.Empty, string.Empty, string.Empty));
		}

		/// <summary> 
		/// Takes a Tab ID and builds the url for get the desidered page (non default)
		/// containing the application path, portal alias, tab ID, and language. 
		/// </summary> 
		/// <param name="targetPage">Linked page</param> 
		/// <param name="tabID">ID of the tab</param> 
		/// <param name="customAttributes">Any custom attribute that can be needed</param> 
		public static string BuildUrl(string targetPage, int tabID, string customAttributes)
		{
			return BuildUrl(targetPage, tabID, 0, null, customAttributes, string.Empty, string.Empty);
		}

		/// <summary> 
		/// Takes a Tab ID and builds the url for get the desidered page (non default)
		/// containing the application path, portal alias, tab ID, and language. 
		/// </summary> 
		/// <param name="targetPage">Linked page</param> 
		/// <param name="tabID">ID of the tab</param> 
		/// <param name="customAttributes">Any custom attribute that can be needed</param> 
		/// <param name="currentAlias">Current Alias</param> 
		public static string BuildUrl(string targetPage, int tabID, string customAttributes, string currentAlias)
		{
			return BuildUrl(targetPage, tabID, 0, null, customAttributes, currentAlias, string.Empty);
		}

		/// <summary> 
		/// Takes a Tab ID and builds the url for get the desidered page (non default)
		/// containing the application path, portal alias, tab ID, and language. 
		/// </summary> 
		/// <param name="targetPage">Linked page</param> 
		/// <param name="tabID">ID of the tab</param> 
		/// <param name="culture">Client culture</param> 
		/// <param name="customAttributes">Any custom attribute that can be needed</param> 
		/// <param name="currentAlias">Current Alias</param> 
		public static string BuildUrl(string targetPage, int tabID, CultureInfo culture, string customAttributes, string currentAlias)
		{
			return BuildUrl(targetPage, tabID, 0, culture, customAttributes, currentAlias, string.Empty);
		}

		/// <summary> 
		/// Takes a Tab ID and builds the url for get the desidered page (non default)
		/// containing the application path, portal alias, tab ID, and language. 
		/// </summary> 
		/// <param name="targetPage">Linked page</param> 
		/// <param name="tabID">ID of the tab</param> 
		/// <param name="modID">ID of the module</param>
		public static string BuildUrl(string targetPage, int tabID, int modID)
		{
			return BuildUrl(targetPage, tabID, modID, null, string.Empty, string.Empty, string.Empty);
		}

		/// <summary> 
		/// Takes a Tab ID and builds the url for get the desidered page (non default)
		/// containing the application path, portal alias, tab ID, and language. 
		/// </summary> 
		/// <param name="targetPage">Linked page</param> 
		/// <param name="tabID">ID of the tab</param> 
		/// <param name="modID">ID of the module</param> 
		/// <param name="culture">Client culture</param> 
		/// <param name="customAttributes">Any custom attribute that can be needed. Use the following format...single attribute: paramname--paramvalue . Multiple attributes: paramname--paramvalue/paramname2--paramvalue2/paramname3--paramvalue3 </param> 
		/// <param name="currentAlias">Current Alias</param> 
		/// <param name="urlKeywords">Add some keywords to uniquely identify this tab. Usual source is UrlKeyword from TabSettings.</param> 
		public static string BuildUrl(string targetPage, int tabID, int modID, CultureInfo culture, string customAttributes, string currentAlias, string urlKeywords)
		{
			PortalSettings currentSetting = null;
			
			if (HttpContext.Current.Items["PortalSettings"] != null)
					currentSetting = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
			
			if (culture == null)
			{
				if (currentSetting != null)
					culture = currentSetting.PortalContentLanguage;
				else
					culture = Thread.CurrentThread.CurrentUICulture;
			}

			if (currentAlias == null || currentAlias == string.Empty)
			{
				if (currentSetting != null)
					currentAlias = currentSetting.PortalAlias;
				else
					currentAlias = ConfigurationSettings.AppSettings["DefaultPortal"];
			}
			// prepare for additional querystring values
			string completeCustomAttributes =  customAttributes;

			/*

			// Start of John Mandia's UrlBuilder Enhancement - Uncomment to test (see history for details)
			// prepare the customAttributes so that they may include any additional existing parameters
			
			// get the current tab id
			int currentTabID = 0;
			if (HttpContext.Current.Request.Params["tabID"] != null)
				currentTabID = Int32.Parse(HttpContext.Current.Request.Params["tabID"]);

			if(tabID == currentTabID)
			{
				// this link is being generated for the current page the user is on
				foreach(string name in HttpContext.Current.Request.QueryString)
				{
					if((HttpContext.Current.Request.QueryString[ name ] != string.Empty) && (HttpContext.Current.Request.QueryString[ name ] != null) && (name != null))
					{
							// do not add any of the common parameters
							if((name.ToLower() !="tabid") && (name.ToLower() != "mid") && (name.ToLower() != "alias") && (name.ToLower() != "lang") && (name.ToLower() != "returntabid") && (name != null))
							{
								if(!(customAttributes.ToLower().IndexOf(name.ToLower()+"=")> -1))
								{
									completeCustomAttributes += "&" + name + "=" + HttpContext.Current.Request.QueryString[ name ];
								}						
							}
					}
				}
			}
			
			*/

			return provider.BuildUrl( targetPage, tabID, modID, culture, completeCustomAttributes, currentAlias, urlKeywords);	
		}

		public static string DefaultPage
		{
			get
			{
				//UrlBuilderProvider provider = UrlBuilderProvider.Instance();
				 return provider.DefaultPage;		
			}
		}

		public static string DefaultSplitter
		{
			get
			{
				//UrlBuilderProvider provider = UrlBuilderProvider.Instance();
				return provider.DefaultSplitter;		
			}
		}

		[Obsolete("Please use the new Rainbow.Settings.Path.WebPathCombine()")]
		public static string WebPathCombine(params string[] values)
		{
			return Rainbow.Settings.Path.WebPathCombine(values);
		}

		/// <summary> 
		/// Builds the url for get to current portal home page
		/// containing the application path, portal alias, tab ID, and language. 
		/// </summary> 
		public static string UrlKeyword(int tabID)
		{
			//UrlBuilderProvider provider = UrlBuilderProvider.Instance();
			return provider.UrlKeyword(tabID);		
		}

		/// <summary> 
		/// Returns the page name that has been specified. 
		/// </summary> 
		public static string UrlPageName(int tabID)
		{
			//UrlBuilderProvider provider = UrlBuilderProvider.Instance();
			return provider.UrlPageName(tabID);	
		}

		/// <summary> 
		/// 2_aug_2004 Cory Isakson enhancement
		/// Determines if a tab is simply a placeholder in the navigation
		/// </summary> 
		public static bool IsPlaceholder(int tabID)
		{
			//UrlBuilderProvider provider = UrlBuilderProvider.Instance();
			return provider.IsPlaceholder(tabID);
		}

		/// <summary> 
		/// 2_aug_2004 Cory Isakson enhancement
		/// Returns the URL for a tab that is a link only.
		/// </summary> 
		public static string TabLink(int tabID)
		{
			//UrlBuilderProvider provider = UrlBuilderProvider.Instance();
			return provider.TabLink(tabID);
		}

		/// <summary> 
		/// Clears any Url Elements e.g IsPlaceHolder, TabLink, UrlKeywords and PageName etc
		/// that may be stored (either in cache, xml etc depending on the UrlBuilder implementation  
		/// </summary> 
		public static void Clear(int tabID)
		{
			provider.Clear(tabID);		
		}

	}
}