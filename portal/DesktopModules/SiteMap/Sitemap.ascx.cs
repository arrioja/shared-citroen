using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.ComponentModel;

using Rainbow.UI;
using Rainbow.Configuration;
using Rainbow.UI.WebControls;
using Esperantus; 
using Rainbow.Security; 
using Rainbow.UI.DataTypes; 

namespace Rainbow.DesktopModules.Sitemap
{
	/// <summary>
	///		Summary description for Sitemap.
	/// </summary>
	public class Sitemap : PortalModuleControl, INavigation
	{
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolder1;
		private SitemapItems list;
		protected int showTabID;  //Controls which tab is displayed. When 0 the current tab is displyed.

		public Sitemap()
		{
			//Bind to Tab setting
			SettingItem BindToTab = new SettingItem(new BooleanDataType());
			BindToTab.Value = "false";
			this._baseSettings.Add("BindToTab", BindToTab);

			SettingItem showTabID = new SettingItem(new IntegerDataType());
			showTabID.Required = true;
			showTabID.Value = "0";
			showTabID.MinValue = 0;
			showTabID.MaxValue = int.MaxValue;
			this._baseSettings.Add("ShowTabID", showTabID);

			// no viewstate needed
			this.EnableViewState = false;
			
			//init member variables
			list = new SitemapItems();
            _autoBind = true;
			_bind = BindOption.BindOptionNone;

		}

		#region Databinding region

		/// <summary>
		/// Do databind.
		/// </summary>
		public override void DataBind() 
		{ 
			MakeSitemap();
		}

		#endregion

		#region INavigation implementation 
		private BindOption _bind = BindOption.BindOptionTop; 
		private bool _autoBind = false; 
		//MH: added 27/05/2003 by mario@hartmann.net
		private int _definedParentTab = -1;
		//MH: end

		/// <summary> 
		/// Indicates if control should bind when loads 
		/// </summary> 
		[ 
		Category("Data"), 
		PersistenceMode(PersistenceMode.Attribute) 
		] 
		public bool AutoBind 
		{ 
			get{return _autoBind;} 
			set{_autoBind = value;} 
		} 

		/// <summary> 
		/// Describes how this control should bind to db data 
		/// </summary> 
		[ 
		Category("Data"), 
		PersistenceMode(PersistenceMode.Attribute) 
		] 
		public BindOption Bind 
		{ 
			get {return _bind;} 
			set 
			{ 
				if(_bind != value) 
				{ 
					_bind = value; 
				} 
			} 
		} 
		//MH: added 27/05/2003 by mario@hartmann.net
		/// <summary>
		/// Defines the ParentTabID when using BindOptionDefinedParent
		/// </summary>
		[
		Category("Data"),
		PersistenceMode(PersistenceMode.Attribute)
		]
		public int ParentTabID
		{ 
			get {return _definedParentTab;}
			set
			{
				if(_definedParentTab != value)
				{
					_definedParentTab = value;
				}
			}
		}
		//MH: end
		#endregion 

		protected override void CreateChildControls()
		{
			// if runtime render the sitemap, else show the placeholder
			if (HttpContext.Current != null)
			{
				string imgFolder = Rainbow.Settings.Path.WebPathCombine(Rainbow.Settings.Path.ApplicationRoot, "DesktopModules/SiteMap/");
				// create the table renderer and set it's properties.
				TableSitemapRenderer tableRenderer = new TableSitemapRenderer();
				tableRenderer.ImageRootNodeUrl		= imgFolder + "sm_RootNode.gif";
				tableRenderer.ImageNodeUrl			= imgFolder + "sm_Node.gif";
				tableRenderer.ImageSpacerUrl		= imgFolder + "sm_Spacer.gif";
				tableRenderer.ImageStraightLineUrl	= imgFolder + "sm_line_I.gif";
				tableRenderer.ImageCrossedLineUrl	= imgFolder + "sm_line_T.gif";
				tableRenderer.ImageLastNodeLineUrl	= imgFolder + "sm_line_L.gif";
				tableRenderer.ImagesHeight = 20;
				tableRenderer.ImagesWidth = 20;
				tableRenderer.TableWidth = new Unit(98,UnitType.Percentage);
				tableRenderer.CssStyle = "Itemdate";

				this.PlaceHolder1.Controls.Add(tableRenderer.Render(list));
			}
			else
			{
				Table t = new Table();			
				TableRow r = new TableRow();
				TableCell c = new TableCell();
				c.Text = "Placeholder for Sitemap";
				t.BorderWidth = 1;
				r.Cells.Add(c);
				t.CellPadding = 0;
				t.CellSpacing = 0;
				t.Width = new Unit(98, UnitType.Percentage);
				t.Rows.Add(r);
				this.PlaceHolder1.Controls.Add(t);
			}
		}

		#region Sitemap creation region
		/// <summary>
		/// This function creates from the PortalSettings structure a list with the tabs in the right order
		/// </summary>
		private void MakeSitemap()
		{
			bool currentTabOnly = (Bind == BindOption.BindOptionCurrentChilds);
			
			PortalSettings portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"]; 
			
			int level = 0;

			// Add Portal Root when showing all tabs
			if (!currentTabOnly)
			{
				list.Add(new SitemapItem(0,portalSettings.PortalName, Rainbow.HttpUrlBuilder.BuildUrl("~/"),0));
				level++;
			}

			// Now loop all tabs to find the right ones to init the recursive functions
			for (int i=0; i < portalSettings.DesktopTabs.Count; ++i)
			{
				TabStripDetails tab = (Rainbow.Configuration.TabStripDetails)portalSettings.DesktopTabs[i]; 

				//if showing all tabs, look for root tabs
				if (!currentTabOnly)
				{
					if (tab.ParentTabID == 0)
					{
						RecurseSitemap(tab, level);
					}
				}
				else
				{
					//else find the current tab
					int tabID = (showTabID>0)? showTabID : portalSettings.ActiveTab.TabID;
					if (tab.TabID == tabID)
					{
						RecurseSitemap(tab, level);
					}
				}
			}
		}

		/// <summary>
		/// This is the recursive function to add all tabs with it's child tabs
		/// </summary>
		private void RecurseSitemap(TabStripDetails tab, int level)
		{
			//only add tabs we have access to
			if (PortalSecurity.IsInRoles( tab.AuthorizedRoles))
			{
				//first add the tab, then add it's children
				list.Add(new SitemapItem(tab.TabID, tab.TabName, Rainbow.HttpUrlBuilder.BuildUrl(tab.TabID), level));

				for(int i=0; i < tab.Tabs.Count; ++i)
				{
					RecurseSitemap(tab.Tabs[i], level + 1);
				}
			}
		}
		
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			showTabID = Int32.Parse(Settings["ShowTabID"].ToString());

			if(bool.Parse(Settings["BindToTab"].ToString()))
			{
				this.Bind = BindOption.BindOptionCurrentChilds;
			}
			else
			{
				this.Bind = BindOption.BindOptionNone;
			}
			
			if(AutoBind)
			{
				DataBind(); 
			}
		}

		public override Guid GuidID 
		{
			get
			{
				return new Guid("{429A98E3-7A07-4d9a-A578-3ED8DD158306}");
			}
		}

			
		#region Install/Uninstall
		public override void Install(System.Collections.IDictionary stateSaver)
		{
			//nothing to do yet, just call base
			base.Install (stateSaver);
		}
	
		public override void Uninstall(System.Collections.IDictionary stateSaver)
		{
			//nothing to do yet, just call base
			base.Uninstall (stateSaver);
		}
		#endregion


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
