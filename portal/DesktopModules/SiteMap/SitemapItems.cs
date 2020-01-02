using System;
using System.Collections;

namespace Rainbow.DesktopModules.Sitemap
{
	/// <summary>
	/// Summary description for SitemapItems.
	/// </summary>
	public class SitemapItems : CollectionBase
	{
		/// <summary>
		/// Adds a SitemapItem item to the collection
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public int Add(SitemapItem item)
		{
			return InnerList.Add(item);
		}

		/// <summary>
		/// Adds an object to the collection (must be of type SitemapItem)
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public int Add(object o)
		{
			SitemapItem item = (SitemapItem) o;
			return InnerList.Add(item);
		}

		/// <summary>
		/// Indexer
		/// </summary>
		public SitemapItem this[int index]
		{
			get
			{
				return((SitemapItem) InnerList[index]);
			}
			set
			{
				InnerList[index] = (SitemapItem) value;
			}
		}

	}
}
