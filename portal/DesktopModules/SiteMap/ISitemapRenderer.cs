using System;

namespace Rainbow.DesktopModules.Sitemap
{
	/// <summary>
	/// This defines an interface for a Sitemap renderer.
	/// </summary>
	public interface ISitemapRenderer
	{
		
		/// <summary>
		/// The Render interface function
		/// </summary>
		System.Web.UI.WebControls.WebControl Render(SitemapItems list);
	}
}
