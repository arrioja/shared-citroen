using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.Security;
using Rainbow.Configuration;
using Esperantus;


namespace Rainbow.DesktopModules 
{
        
    public class Tabs : PortalModuleControl
    {
		protected Esperantus.WebControls.Label lblHead;
		protected System.Web.UI.WebControls.ListBox tabList;
		protected Esperantus.WebControls.ImageButton upBtn;
		protected Esperantus.WebControls.ImageButton downBtn;
		protected Esperantus.WebControls.LinkButton addBtn;


		protected ArrayList portalTabs;
		
		/// <summary>
		/// Admin Module
		/// </summary>
		public override bool AdminModule
		{
			get
			{
				return true;
			}
		}

        /// <summary>
        /// The Page_Load server event handler on this user control 
        /// is used to populate the current tab settings from the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Load(object sender, System.EventArgs e) 
        {
            portalTabs = new TabsDB().GetTabsFlat(portalSettings.PortalID);

            // If this is the first visit to the page, bind the tab data to the page listbox
            if (!Page.IsPostBack) 
            {
				// Set the ImageUrl for controls from current Theme
				upBtn.ImageUrl		= this.CurrentTheme.GetImage("Buttons_Up", "Up.gif").ImageUrl;
				downBtn.ImageUrl	= this.CurrentTheme.GetImage("Buttons_Down", "Down.gif").ImageUrl;
				
				tabList.DataBind();

				// 2/27/2003 Start - Ender Malkoc
				// After up or down button when the page is refreshed, 
				// select the previously selected tab from the list.
				if (Request.Params["selectedtabID"] != null) 
				{
					try
					{
						int tabIndex = Int32.Parse(Request.Params["selectedtabID"]);
						SelectTab(tabIndex);
					}
					catch
					{

					}
				}
				// 2/27/2003 End - Ender Malkoc
            }
        }

		/// <summary>
		/// This is where you add module settings. These settings  
		/// are used to control the behavior of the module
		/// </summary>
		/// <param></param>
		public Tabs()
		{
			// EHN: Add new version control for tabs module. 
			//      Mike Stone - 19/12/2004
			SettingItem TabVerstion = new SettingItem(new Rainbow.UI.DataTypes.BooleanDataType());
			TabVerstion.Value = "True";
			TabVerstion.EnglishName = "Use Old Version?";
			TabVerstion.Description = "If Checked the module acts has it always did. If not it uses the new short form which allows security to be set so the new tab will not be seen by all users.";
			TabVerstion.Order = 10;
			this._baseSettings.Add("TAB_VERSION", TabVerstion); 
		}



		/// <summary>
		/// The UpDown_Click server event handler on this page is
		/// used to move a portal module up or down on a tab's layout pane
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UpDown_Click(Object sender, ImageClickEventArgs e) 
		{
			string cmd = ((ImageButton)sender).CommandName;
        
			if (tabList.SelectedIndex > -1) 
			{
				int delta;
            
				// Determine the delta to apply in the order number for the module
				// within the list.  +3 moves down one item; -3 moves up one item
				if (cmd == "down") 
				{
					delta = 3;
				}
				else 
				{
					delta = -3;
				}

				TabItem t;
				t = (TabItem) portalTabs[tabList.SelectedIndex];

				// 12/16/2002 Start - Cory Isakson 
				// Change the tab by the delta only
				t.Order += delta;
				//t.Order = AddToOrder(t.Order, t.NestLevel, delta); 
				// 12/16/2002 End - Cory Isakson 
            
				// Reset the order numbers for the tabs within the portal  
				OrderTabs();

				// Redirect to this site to refresh
				Response.Redirect(Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopDefault.aspx", TabID, "selectedtabID=" + t.ID));
			}		
		}

        /// <summary>
        /// The DeleteBtn_Click server event handler is used to delete
        /// the selected tab from the portal
        /// </summary>
        override protected void OnDelete()
        {
            base.OnDelete();

            if (tabList.SelectedIndex > -1)
            {
                try
                {
                    // must delete from database too
                    TabItem t = (TabItem) portalTabs[tabList.SelectedIndex];
                    TabsDB tabs = new TabsDB();
                    tabs.DeleteTab(t.ID);
                        
                    // remove item from list
                    portalTabs.RemoveAt(tabList.SelectedIndex);

					#region GBS Fix RBM-203 
					if (tabList.SelectedIndex > 0) 
						t = (TabItem) portalTabs[tabList.SelectedIndex-1]; 
					#endregion 

                    // reorder list
                    OrderTabs();
            
                    // Redirect to self page to refresh
					Response.Redirect(Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopDefault.aspx", TabID, "SelectedTabID=" + t.ID));
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    Controls.Add(new LiteralControl("<br><span class='Error'>" + Esperantus.Localize.GetString("TAB_DELETE_FAILED", "Failed to delete Tab", this) + "<br>"));
                }
            }
        }

        /// <summary>
        /// The AddTab_Click server event handler is used
        /// to add a new tab for this portal
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="e"></param>
        private void AddTab_Click(Object Sender, EventArgs e) 
        {
			 // EHN: Adding support for new form that only hits db after user
			//		commits by clicking the save button. 
			// Mike Stone - 19/12/2004
			if (Settings["TAB_VERSION"] != null)
			{
				if(Settings["TAB_VERSION"].ToString() == "True") // Use Old Version
				{
					// New tabs go to the end of the list
					TabItem t = new TabItem();
					t.Name = Esperantus.Localize.GetString("TAB_NAME", "New Tab Name");  //Just in case it comes to be empty
					t.ID = -1;
					t.Order = 990000;
					portalTabs.Add(t);

					// write tab to database
					TabsDB tabs = new TabsDB();
					t.ID = tabs.AddTab(portalSettings.PortalID, t.Name, t.Order);
        
					// Reset the order numbers for the tabs within the list  
					OrderTabs();


					// Redirect to edit page
					// 3_aug_2004 Cory Isakson added returntabid so that TabLayout could return to the tab it was called from.
					// added mID by Mario Endara <mario@softworks.com.uy> to support security check (2004/11/09)
					Response.Redirect(Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Tabs/TabLayout.aspx", t.ID, "mID=" + ModuleID.ToString() + "&returntabid=" + Page.TabID));
				}
				else
				{
					// Redirect to New Form - Mike Stone 19/12/2004
					Response.Redirect(Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Tabs/AddTab.aspx", "mID=" + ModuleID.ToString() + "&returntabid=" + Page.TabID));
				}
			}

        }

        /// <summary>
        /// The EditBtn_Click server event handler is used to edit
        /// the selected tab within the portal
        /// </summary>
        override protected void OnEdit()
        {
            // Redirect to edit page of currently selected tab
            if (tabList.SelectedIndex > -1) 
            {
                // Redirect to module settings page
                TabItem t = (TabItem) portalTabs[tabList.SelectedIndex];
            
				// added mID by Mario Endara <mario@softworks.com.uy> to support security check (2004/11/09)
				Response.Redirect(Rainbow.HttpUrlBuilder.BuildUrl("~/DesktopModules/Tabs/TabLayout.aspx", t.ID, "mID=" + ModuleID.ToString() + "&returntabid=" + Page.TabID));
			}
        }

        /// <summary>
        /// The OrderTabs helper method is used to reset 
        /// the display order for tabs within the portal
        /// </summary>
        private void OrderTabs () 
        {
            int i = 1;
        
            // sort the arraylist
            portalTabs.Sort();
        
            // renumber the order and save to database
            foreach (TabItem t in portalTabs) 
            {
                // number the items 1, 3, 5, etc. to provide an empty order
                // number when moving items up and down in the list.
				t.Order = i; 
                i += 2;
            
                // rewrite tab to database
                TabsDB tabs = new TabsDB();
				// 12/16/2002 Start - Cory Isakson 
                tabs.UpdateTabOrder(t.ID, t.Order); 
				// 12/16/2002 End - Cory Isakson 
            }
			//gbs: Invalidate cache, fix for bug RBM-220
			Rainbow.Settings.Cache.CurrentCache.RemoveAll("_TabNavigationSettings_");
		}
    
		/// <summary>
		/// Given the tabID of a tab, this function selects the right tab in the provided list control
		/// </summary>
		/// <param name="tabID">tabID of the tab that needs to be selected</param>
		private void SelectTab (int tabID)
		{
			for(int i = 0 ; i < tabList.Items.Count ; i++)
			{
				if(((TabItem)portalTabs[i]).ID == tabID)
				{
					if(tabList.SelectedItem != null) tabList.SelectedItem.Selected = false;
					tabList.Items[i].Selected = true;
					return;
				}
			}
			return;
		}

        /// <summary>
        /// GuidID
        /// </summary>
        public override Guid GuidID 
        {
            get
            {
                return new Guid("{1C575D94-70FC-4A83-80C3-2087F726CBB3}");
            }
        }

		#region Web Form Designer generated code
		/// <summary>
		/// Raises OnInit Event
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

        private void InitializeComponent() 
        {
			this.upBtn.Click += new System.Web.UI.ImageClickEventHandler(this.UpDown_Click);
			this.downBtn.Click += new System.Web.UI.ImageClickEventHandler(this.UpDown_Click);
			this.addBtn.Click += new System.EventHandler(this.AddTab_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

    }
}
