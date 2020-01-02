using System;
using System.IO;
using System.Web.UI.WebControls;
using Rainbow.Configuration;
using Rainbow.Design;
using Rainbow.Helpers;
using Rainbow.Security;
using Rainbow.UI;

namespace Rainbow
{
	/// <summary>
	/// The DesktopDefault.aspx page is used 
	/// to load and populate each Portal View.
	/// It accomplishes this by reading the layout configuration 
	/// of the portal from the Portal Configuration	system, 
	/// and then using this information to dynamically 
	/// instantiate portal modules (each implemented 
	/// as an ASP.NET User Control), and then inject them into the page.
	/// </summary>
	public class DesktopDefault : Page
	{
		protected PlaceHolder LayoutPlaceHolder;

		#region Web Form Designer generated code

		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.DesktopDefault_Load);
		}

		#endregion

		private void DesktopDefault_Load(object sender, EventArgs e)
		{
			// Ensure that the visiting user has access to the current page
			if (PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles) == false)
			{
				PortalSecurity.AccessDenied();
			}
			else
			{
				LoadPage();
			}
		}

		private void LoadPage()
		{
			const string LAYOUT_BASE_PAGE = "DesktopDefault.ascx";
			string defaultLayoutPath = string.Concat(LayoutManager.WebPath, "/Default/", LAYOUT_BASE_PAGE);

			try
			{
				// Thierry (Tiptopweb), 4 July 2003, all moved to portalSettings
				// new version using custom settings from the TabSettings.cs
				// no need to detect the shop module, add the layout when adding the shop module
				// also useful to switch design for home page or other pages!

				string layoutPath = string.Concat(portalSettings.PortalLayoutPath, LAYOUT_BASE_PAGE);
				LayoutPlaceHolder.Controls.Add(Page.LoadControl(layoutPath));
			}
			catch (FileNotFoundException ex)
			{
				ErrorHandler.HandleException("FileNotFound", ex);

				LayoutPlaceHolder.Controls.Add(Page.LoadControl(defaultLayoutPath));
				ErrorHandler.HandleException(ex);
			}
			LogEntry();
		}

		private void LogEntry()
		{
			if (PortalSettings.IsMonitoringEnabled)
			{
				//Ender, 31 July 2003, This is added to support the monitoring module written by Paul Yarrow
				try
				{
					Monitoring.LogEntry(long.Parse(PortalSettings.CurrentUser.Identity.ID), portalSettings.PortalID, Convert.ToInt32(TabID), "PageRequest", string.Empty);
				}
				catch (Exception ex)
				{
					//This error is not fatal, we can keep going
					LogHelper.Logger.Log(LogLevel.Debug, "Monitoring LogEntry Error", ex);

					//TODO: Specify better which error type can be raised
				}
			}
		}
	}
}