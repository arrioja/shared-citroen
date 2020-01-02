using System;
using System.Web.UI.WebControls;

namespace Rainbow.DesktopModules.Sitemap
{
	/// <summary>
	/// A concrete sitemaprenderer. This class takes een SitemapItems class and generates
	/// a Table from it.
	/// </summary>
	public class TableSitemapRenderer : ISitemapRenderer
	{
		#region member variables

		//member variables
		protected string _rootNodeUrl;
		protected string _nodeUrl;
		protected string _spacerUrl;
		protected string _straightLineUrl;
		protected string _crossedLineUrl;
		protected string _lastNodeLineUrl;
		
		// all images should have the same height and width
		protected int _imagesHeight;
		protected int _imagesWidth;
		
		protected Unit _tableWidth;

		protected string _cssStyle;

		#endregion
		
		#region constructor
		public TableSitemapRenderer()
		{
			InitVariables();
		}

		//set default values
		protected virtual void InitVariables()
		{
			//init member variables
			_rootNodeUrl = string.Empty;
			_nodeUrl = string.Empty;
			_spacerUrl = string.Empty;
			_straightLineUrl = string.Empty;
			_crossedLineUrl = string.Empty;
			_lastNodeLineUrl = string.Empty;
			_cssStyle = string.Empty;

			_imagesHeight = 0;
			_imagesWidth = 0;

			//default table width to 98%
			_tableWidth = new Unit(98, UnitType.Percentage);
		}

		#endregion

		#region ISitemapRenderer Interface implementation
		/// <summary>
		/// Render
		/// This function creates a treeview in a table from the SitemapItems list
		/// </summary>
		public virtual System.Web.UI.WebControls.WebControl Render(SitemapItems list)
		{
			//init table with a width of 98%
			Table t = new Table();
			t.Width = TableWidth;
			t.BorderWidth = 0;
			t.CellSpacing = 0;
			t.CellPadding = 0;

            int cols = MaxLevel(list) + 2;
			
			// an array of chars is used to determine what images to show on each row
			// the chars have the following meaning:
			// + --> crossed line
			// | --> straight line
			// \ --> line for last node on branch
			// N --> node
			// R --> root node
			// S --> space
			char [] strRow = new char[cols];
						
			//init row to spaces
			for (int i=0; i<cols; ++i)strRow[i] = 'S';

			for (int i=0; i<list.Count; ++i)
			{
				// replace the cross of the previous row in a straight line on the current row
				// do the same for last_node_line and Spaces
				for(int j=0; j<cols; ++j)
				{
					if (strRow[j]=='+') strRow[j]='|';
					if (strRow[j]=='\\') strRow[j]='S';
				}

				// show a root node image if nestlevel = 0
				if (list[i].NestLevel == 0) 
				{
					strRow[list[i].NestLevel] = 'R';
				}
				else
				{
					strRow[list[i].NestLevel] = 'N';
				}
				
				//everything after the node can be replaces by spaces
				for (int j=list[i].NestLevel+1; j<cols; ++j)strRow[j]=' ';

				// show no lines before the node when it's a root node
				if (list[i].NestLevel > 0)
				{
					if (LastItemAtLevel(i,list))
					{
						//if it's the last node at that level of the current branch,
						//show a last node line
						strRow[list[i].NestLevel - 1] = '\\';
					}
					else
					{
						//else show a crossed line
						strRow[list[i].NestLevel - 1] = '+';
					}
				}

				// the images are determined in the char array, now make a TableRow from it
				TableRow r = new TableRow();
				TableCell c;
				
				//only use the char array till the node
				for (int j=0; j <= list[i].NestLevel; ++j)
				{
					c = new TableCell();
					Image img = new Image();
					img.BorderWidth = 0;
					
					img.Width = ImagesWidth;
					img.Height = ImagesHeight;
					c.Width = ImagesWidth;
					
					//what image to use
					switch(strRow[j])
					{
						case '+':
							img.ImageUrl = ImageCrossedLineUrl;
							break;
						case '\\':
							img.ImageUrl = ImageLastNodeLineUrl;
							break;
						case '|':
							img.ImageUrl = ImageStraightLineUrl;
							break;
						case 'S':
							img.ImageUrl = ImageSpacerUrl;
							break;
						case 'N':
							img.ImageUrl = ImageNodeUrl;
							break;
						case 'R':
							img.ImageUrl = ImageRootNodeUrl;
							break;
					}

					c.Controls.Add(img);
					r.Cells.Add(c);
				}
				
				// the images are done for this row, now make the hyperlink
				c = new TableCell();
				//make sure it fills the all the space left
				c.Width = new Unit(100,UnitType.Percentage);
				c.ColumnSpan = cols - 1 - list[i].NestLevel;
				Literal lit = new Literal();
				lit.Text = "&nbsp;";
				c.Controls.Add(lit);
				HyperLink l = new HyperLink();
				l.Text = list[i].Name;
				l.NavigateUrl = list[i].Url;
				l.CssClass = CssStyle;

				//row is done and add everything to the table
				c.Controls.Add(l);
				r.Cells.Add(c);
				t.Rows.Add(r);
			}

			return t;
		}		
		#endregion
		
		#region Render helper functions
		/// <summary>
		/// Returns true if node is last node for the current branch on that level
		/// </summary>
		protected virtual bool LastItemAtLevel(int index, SitemapItems list)
		{
			int level = list[index].NestLevel;

			for (int i=index+1; i<list.Count;++i)
			{
				if (list[i].NestLevel < level)
				{
					return true;
				}
				
				if (list[i].NestLevel == level)
				{
					return false;
				}
			}
			return true;
		}

		protected virtual int MaxLevel(SitemapItems list)
		{
			int level = 0;

			for (int i=0; i<list.Count; ++i)
			{
				if (list[i].NestLevel > level)
				{
					level = list[i].NestLevel;
				}
			}

			return level;
		}
		#endregion

		#region property definitions
		/// <summary>
		/// Url for the RootNode image
		/// </summary>
		public string ImageRootNodeUrl
		{
			get
			{
				return _rootNodeUrl;
			}
			set
			{
				_rootNodeUrl = value;
			}
		}

		/// <summary>
		/// Url for the other Nodes image
		/// </summary>
		public string ImageNodeUrl
		{
			get
			{
				return _nodeUrl;
			}
			set
			{
				_nodeUrl = value;
			}
		}

		/// <summary>
		/// Url for the spacer image
		/// </summary>
		public string ImageSpacerUrl
		{
			get
			{
				return _spacerUrl;
			}
			set
			{
				_spacerUrl = value;
			}
		}

		/// <summary>
		/// Url for the straightline image
		/// </summary>
		public string ImageStraightLineUrl
		{
			get
			{
				return _straightLineUrl;
			}
			set
			{
				_straightLineUrl = value;
			}		
		}

		/// <summary>
		/// Url for the Crossed line image
		/// </summary>
		public string ImageCrossedLineUrl
		{
			get
			{
				return _crossedLineUrl;
			}
			set
			{
				_crossedLineUrl = value;
			}
		}

		/// <summary>
		/// Url for the Last Node Line image
		/// </summary>
		public string ImageLastNodeLineUrl
		{
			get
			{
				return _lastNodeLineUrl;
			}
			set
			{
				_lastNodeLineUrl = value;
			}
		}

		/// <summary>
		/// CSS style for the hyperlinks
		/// </summary>
		public string CssStyle
		{
			get
			{
				return _cssStyle;
			}
			set
			{
				_cssStyle = value;
			}
		}

		/// <summary>
		/// Height of the images. All images should have the same height
		/// </summary>
		public int ImagesHeight
		{
			get
			{
				return _imagesHeight;
			}
			set
			{
				_imagesHeight = value;
			}
		}

		/// <summary>
		/// Width of the images. All images should have the same width
		/// </summary>
		public int ImagesWidth
		{
			get
			{
				return _imagesWidth;
			}
			set
			{
				_imagesWidth = value;
			}		
		}

		/// <summary>
		/// Width of the table. defaults to 98%
		/// </summary>
		public Unit TableWidth
		{
			get
			{
				return _tableWidth;
			}
			set
			{
				_tableWidth = value;
			}		
		}


		#endregion

	}
}
