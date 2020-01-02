using System;
using System.Collections;
using System.Web.Services;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

using Rainbow.Helpers;


namespace Rainbow.Services 
{

	/// <summary>
	/// The class that implements the community Web service. 
	/// The community Web service exposes content items from this 
	/// Rainbow site to the web
	/// </summary>
	public class CommunityService : WebService 
	{
		/// <summary>
		/// Returns a ServiceResponseInfo object that represents a 
		/// a collection of content items.
		/// </summary>
		/// <param name="requestInfo"></param>
		/// <returns>ServiceResponseInfo</returns>
		[WebMethod(CacheDuration=600)]
		[XmlInclude(typeof(ServiceResponseInfoItem))]
		public ServiceResponseInfo GetCommunityContent(ServiceRequestInfo requestInfo)
		{
			requestInfo.LocalMode = true;
			requestInfo.ListType = ServiceListType.Item;
			
			ServiceResponseInfo responseInfo;
			responseInfo = ServiceHelper.CallService(-1, -1, string.Empty, ref requestInfo, null);

			return responseInfo;
		}


		public CommunityService()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion	
	}
}
