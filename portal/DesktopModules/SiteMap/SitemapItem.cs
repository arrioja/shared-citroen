using System;

namespace Rainbow.DesktopModules.Sitemap
{
	/// <summary>
	/// A sitemap item. This just defines the simple data needed for the sitemap items.
	/// </summary>
	public class SitemapItem
	{
		public int ID;
		public string Name;
		public string Url;
		public int NestLevel;

		public SitemapItem()
		{
		}

		public SitemapItem(int id, string name, string url, int nestlevel)
		{
			ID = id;
			Name = name;
			Url = url;
			NestLevel = nestlevel;
		}
	}
}
