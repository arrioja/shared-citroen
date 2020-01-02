using System;
using System.IO;
using System.Net;
using System.Text;
using System.Drawing;
using System.Web.Caching;
using System.Web.Services;
using System.Drawing.Imaging;
using System.Collections;

using Rainbow.Design;
using Rainbow.Helpers;
using Rainbow.Configuration;
using Esperantus;

namespace Rainbow.DesktopModules
{

	/// <summary>
	/// DailyDilbert Module
	/// Based on VB Module Written by SnowCovered.com
	/// Modifications and conversion for C# IBS Portal (c)2002 by Christopher S Judd, CDP
	/// email- dotNet@HorizonsLLC.com    web- www.HorizonsLLC.com/IBS
	/// Modifications and conversion for Rainbow Jakob hansen
	/// </summary>
	public class DailyDilbertImage : Rainbow.UI.ViewItemPage
	{

		/// <summary>
		/// The Page_Load server event handler on this User Control is used
		/// to stream the current comic from the DailyDilbert web service
		/// and output an image for use on the web control
		///
		/// The image returned is a thumbnail based on a % of 
		/// the original picture size. If no % reduction then
		/// the image will be viewed at 100%. if (ModuleID cannot
		/// be determined, then the image will be viewed at 100%.
		/// Since when clicking on the comic from the module, ModuleID
		/// is not sent, it displays the image in a new window at 100%.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e)
		{
			Response.ContentType = "image/gif";

			string strAddress;
			string strImageAddress;

			string ImagePercent;

			// Covert image percent to an integer, else set it to 100%
			Double dblImagePercent ;
			try 
			{
				ImagePercent = moduleSettings["ImagePercent"].ToString();
				dblImagePercent = Convert.ToDouble(ImagePercent);
			} 
			catch 
			{
				dblImagePercent = 100;
			}
		
			if (dblImagePercent == 0 ) 
			{
				dblImagePercent = 100;
			}
			dblImagePercent = dblImagePercent * 0.01;

			string cacheKey = "DAILY_DILBERT";
			Image myThumbnail = null;

			if (Cache[cacheKey] == null)
			{
				WebClient objHTTPReq = new WebClient();
				MemoryStream objMemStr = new MemoryStream();

				// Get the image from the service and create a thumbnail for output
				try 
				{

					strAddress = "http://www.dilbert.com/comics/dilbert/archive/";

					StreamReader objStream = new StreamReader(objHTTPReq.OpenRead(strAddress), Encoding.ASCII);

					strAddress = objStream.ReadToEnd();

					if (strAddress.IndexOf("/comics/dilbert/archive/images/dilbert") > 0 ) 
					{
						// Setup the URL of the image to capture
						strImageAddress = "http://www.dilbert.com";
						strImageAddress += strAddress.Substring(strAddress.IndexOf("/comics/dilbert/archive/images/dilbert"), 56);

						// Remove the & if it was added to the URL to prevent errors
						strImageAddress = strImageAddress.Replace("&", string.Empty);

						// Create the bitmap based on the image address
						Bitmap objDilbertImg = new Bitmap(objHTTPReq.OpenRead(strImageAddress));

						// Set the width and height based on the % of the current image size
						int Width = Convert.ToInt32(objDilbertImg.Width * dblImagePercent);
						int Height = Convert.ToInt32(objDilbertImg.Height * dblImagePercent);

						// Create a thumbnail using the new size
						myThumbnail = objDilbertImg.GetThumbnailImage(Width, Height, null, IntPtr.Zero);

						// Set the output type and send image
						Cache.Insert(cacheKey, myThumbnail, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(60));
					}
				} 
				catch(Exception ex)
				{
					Rainbow.Configuration.ErrorHandler.HandleException("Daily dilbert error", ex);
					//Helpers.LogHelper.Logger.Log(Rainbow.Configuration.LogLevel.Warn, "Daily dilbert error", ex);
				}

				objMemStr = null;
				objHTTPReq = null;

			}
			else
			{
				myThumbnail = (Image) Cache[cacheKey];
			}
			if(myThumbnail != null)
			{
				myThumbnail.Save(Response.OutputStream, ImageFormat.Gif);
			}		
		}
		
		/// <summary>
		/// Set the module guids with free access to this page
		/// </summary>
		protected override ArrayList AllowedModules
		{
			get
			{
				ArrayList al = new ArrayList();
				al.Add ("2502DB18-B580-4F90-8CB4-C15E6E531031");
				return al;
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	}
}
