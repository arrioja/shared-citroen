using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Rainbow.Admin;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.Configuration;
using Esperantus;
using Rainbow.Security;

/* known bugs
 1) if you delete the entire thread while viewing in this window, it should automatically take you back to the Dicussion.ascx view
 
*/
namespace Rainbow.DesktopModules
{
	public class DiscussionViewThread : Rainbow.UI.EditItemPage 
	{
		protected System.Web.UI.WebControls.DataList ThreadList;
		

		/// <summary>
		/// On the first invocation of Page_Load, the data is bound using BindList();
		/// </summary>
		/// <param name="e"></param>
		/// <returns>returns nothing (void)</returns>
		private void Page_Load(object sender, System.EventArgs e) 
		{
			if (Page.IsPostBack == false) 
			{
				BindList(GetItemID());
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
				al.Add ("2D86166C-4BDC-4A6F-A028-D17C2BB177C8");
				al.Add ("2502DB18-B580-4F90-8CB4-C15E6E531030"); // Access from portalSearch
				al.Add ("2502DB18-B580-4F90-8CB4-C15E6E531052"); // Access from serviceItemList				
				return al;
			}
		}

		/// <summary> extracts this threads ItemID from the URL </summary>
		private int GetItemID()  
		{
			if (HttpContext.Current != null && Request.Params["ItemID"] != null)
			{
				return Int32.Parse(Request.Params["ItemID"]);
			}
			else
			{
				/* throw up an error dialog here */
				Response.Write("Error, invalid <ItemID> given to DiscussionViewThread");
				return 0;
			}
		}


		/// <summary> Binds the threads from the database to the list control </summary>
		/// <param name="ItemID"> itemID of ANY of the topics in the thread </param>
		/// <returns>returns nothing</returns>
		private void BindList(int ItemID) 
		{
			// Obtain a list of discussion messages for the module and bind to datalist
			DiscussionDB discuss = new DiscussionDB();
			ThreadList.DataSource = discuss.GetThreadMessages(ItemID, 'Y'); // 'Y' means include rootmessage
			ThreadList.DataBind();
		}

		/// <summary> ThreadList_Select processes user events to add, edit, and delete topics </summary>
		/// <param name="Sender"></param>
		/// <param name="e">DataListCommandEventAargs e</param>
		/// <returns>returns nothing</returns>
		public void ThreadList_Select(object Sender, DataListCommandEventArgs e) 
		{
			// Determine the command of the button
			string command = ((System.Web.UI.WebControls.CommandEventArgs)(e)).CommandName;

			switch (command)
			{
				case "delete":  
					DiscussionDB discuss = new DiscussionDB();
					int ItemID = Int32.Parse(e.CommandArgument.ToString());
					discuss.DeleteChildren(ItemID);
					break;
				case "return_to_discussion_list":
					RedirectBackToReferringPage();
					break;
				default:
					break;
			}
			BindList(GetItemID());
			return;
		}
		
		/// <summary> Invoked when each data row is bound to the list.
		/// Adds a clientside Java script to confirm row deltes </summary>
		/// <param name="sender"></param>
		/// <param name="e">DataListCommandEventAargs e</param>
		/// <returns>returns nothing</returns>
		protected void OnItemDataBound(object sender, DataListItemEventArgs e)
		{
			if (e.Item.FindControl("deleteBtn") != null)
			{
				// 13/7/2004 Added Localization Mario Endara mario@softworks.com.uy
				((ImageButton)e.Item.FindControl("deleteBtn")).Attributes.Add("onClick", "return confirm('" + 
					Esperantus.Localize.GetString ("DISCUSSION_DELETE_RESPONSE", "Are you sure you want to delete the selected response message and ALL of its children ?") + "');");
			}
			// 15/7/2004 added localization by Mario Endara mario@softworks.com.uy
			if (e.Item.FindControl("Label10") != null)
			{
				if (((Label)e.Item.FindControl("Label10")).Text == "unknown")
				{
					((Label)e.Item.FindControl("Label10")).Text = Esperantus.Localize.GetString ("UNKNOWN", "unknown");
				}
			}
			
		}

		/// <summary>
		/// GetReplyImage check to see whether the current user has permissions to contribute to the discussion thread
		/// Users with proper permission see an image they can click  on to post a reply, otherwise they see nothing.
		/// </summary>
		/// <returns>Returns either a 1x1 image or the reply.gif icon</returns>
		protected string GetReplyImage() 
		{
			// leave next commented statement in for testing back doors 
			// return "~/images/reply.gif";
			if (DiscussionPermissions.HasAddPermissions(ModuleID) == true)
				return getLocalImage("reply.gif");
			else
				return getLocalImage("1x1.gif");
		}

		protected string GetEditImage(string itemUserEmail) 
		{
			if (DiscussionPermissions.HasEditPermissions(ModuleID, itemUserEmail))
				return getLocalImage("edit.gif");
			else
				return getLocalImage("1x1.gif");
		}
		
		protected string GetDeleteImage(int itemID, string itemUserEmail) 
		{
			if (DiscussionPermissions.HasDeletePermissions(ModuleID, itemID, itemUserEmail) == true)
				return getLocalImage("delete.gif");
			else
				return getLocalImage("1x1.gif");
		}

		/// <summary>
		/// The FormatUrl method is a helper messages called by a
		/// databinding statement within the &lt;asp:DataList&gt; server
		/// control template.  It is defined as a helper method here
		/// (as opposed to inline within the template) to improve 
		/// code organization and avoid embedding logic within the 
		/// content template.</summary>
		/// <param name="item">ID of the currently selected topic</param>
		/// <returns>Returns a properly formatted URL to call the DiscussionEdit page</returns>
		protected string FormatUrlEditItem(int item, string mode) 
		{
			return(HttpUrlBuilder.BuildUrl("~/DesktopModules/Discussion/DiscussionEdit.aspx", "ItemID=" + item + "&Mode=" + mode + "&mID=" + ModuleID + "&edit=1"));
		}
		
		/// <summary>
		/// The NodeImage method is a helper method called by a
		/// databinding statement within the &lt;asp:datalist&gt; server
		/// control template.  It controls whether or not an item
		/// in the list should be rendered as an expandable topic
		/// or just as a single node within the list.
		/// </summary>
		/// <param name="count">Number of replys to the selected topic</param>
		/// <returns></returns>
		protected string NodeImage(int count) 
		{
			return getLocalImage("plus.gif");			
		}

		protected string getLocalImage (string img)
		{
			return Rainbow.Settings.Path.WebPathCombine(Rainbow.Settings.Path.ApplicationRoot, "DesktopModules/Discussion/images", img);
		}
 

		protected override void LoadSettings()
		{
			// Verify that the current user has proper permissions for this module

			// need to reanable this code as second level check in case users hack URLs to come to this page
			// if (PortalSecurity.HasEditPermissions(ModuleID) == false && PortalSecurity.IsInRoles("Admins") == false)
			//	PortalSecurity.AccessDeniedEdit();

			// base.LoadSettings();
		}
		
		#region Web Form Designer generated code
        
		/// <summary>
		/// Raises Init event
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			
			/*
			 * // Create a new Title the control
			ModuleTitle = new DesktopModuleTitle();
			
			// Add a link for the edit page
			// ModuleTitle.AddTarget = "_new";  // uncomment this if you want replies and new posts in a new web browser window
			ModuleTitle.AddText = "DS_NEWTHREAD";
			ModuleTitle.AddUrl = "~/DesktopModules/Discussion/DiscussionEdit.aspx";
			
			// Add title at the very beginning of the control's controls collection
			Controls.AddAt(0, ModuleTitle);
		
			*/
			base.OnInit(e);
			
		}

		private void InitializeComponent() 
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
