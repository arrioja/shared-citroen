using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow.Admin;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Rainbow.Configuration;
using Esperantus;

namespace Rainbow.DesktopModules
{
	public class ContentManager : PortalModuleControl
	{
		//listboxes
		protected System.Web.UI.WebControls.ListBox SourceListBox;
		protected System.Web.UI.WebControls.ListBox DestListBox;

		//drop down lists/combo boxes
		protected System.Web.UI.WebControls.DropDownList ModuleTypes;
		protected System.Web.UI.WebControls.DropDownList SourcePortal;
		protected System.Web.UI.WebControls.DropDownList DestinationPortal;
		protected System.Web.UI.WebControls.DropDownList SourceInstance;
		protected System.Web.UI.WebControls.DropDownList DestinationInstance;

		//buttons
		protected Esperantus.WebControls.LinkButton MoveLeft_Btn;
		protected Esperantus.WebControls.LinkButton MoveRight_Btn;
		protected Esperantus.WebControls.LinkButton Copyright_Btn;
		protected Esperantus.WebControls.LinkButton CopyAll_Btn;
		protected Esperantus.WebControls.LinkButton DeleteLeft_Btn;
		protected Esperantus.WebControls.LinkButton DeleteRight_Btn;
		protected System.Web.UI.WebControls.Label SourcePortalLabel;
		protected System.Web.UI.WebControls.Label DestinationPortalLabel;

        
		//HTML table for Portal Instances that is hidden/visible depending on SettingItem.
		protected System.Web.UI.HtmlControls.HtmlTable MultiPortalTable;

		/// <summary>
		/// The Page_Load event handler on this User Control populates the comboboxes
		/// for portals and module types for the ContentManager.
		/// It uses the Rainbow.ContentManagerDB()
		/// data component to encapsulate all data functionality.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				//populate module types dropdownlist.
				ContentManagerDB contentDB = new ContentManagerDB();

				//populate moduleTypes list
				ModuleTypes.DataSource = contentDB.GetModuleTypes();
				ModuleTypes.DataValueField = "ItemID";
				ModuleTypes.DataTextField = "FriendlyName";
				ModuleTypes.DataBind();

				//populate source portal list
				SourcePortal.DataValueField = "PortalID";
				SourcePortal.DataTextField = "PortalAlias";
				SourcePortal.DataSource = contentDB.GetPortals();
				SourcePortal.DataBind();

				//destination portal list.
				DestinationPortal.DataValueField = "PortalID";
				DestinationPortal.DataTextField = "PortalAlias";
				DestinationPortal.DataSource = contentDB.GetPortals();
				DestinationPortal.DataBind();

				//Function to set visibility for Portal dropdowns and select current portal
				//as default
				MultiPortalSupport();

				//functions to load the modules in the currently selected portal.
				LoadSourceModules();
				LoadDestinationModules();

			}
		}

				
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
		
		#region System Event Handlers for dropdownlists
		/// <summary>
		/// The ModuleTypesChanged event handler on this User Control fires when the
		/// Moduletypes dropdownlist has been changed(ex. Announcements to FAQ).
		/// The event then refreshes the source modules for that type and the destination
		/// modules for htat type.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ModuleTypesChanged(object sender, System.EventArgs e)
		{
			//does not change portals!!
			LoadSourceModules();
			LoadDestinationModules();
		}

		/// <summary>
		/// The SourcePortalChanged event handler on this User Control fires when the
		/// SourcePortal dropdownlist has been changed(ex. Portal Instance 1 changed to 2).
		/// The event then refreshes the source modules for that type.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SourcePortalChanged(object sender, System.EventArgs e)
		{
			//refresh source instances, destination instances should stay same?
			LoadSourceModules();
		}

		/// <summary>
		/// The DestinationPortalChanged event handler on this User Control fires when the
		/// DestinationPortal dropdownlist has been changed
		/// The event then refreshes the modules for that portal instance
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DestinationPortalChanged(object sender, System.EventArgs e)
		{
			LoadDestinationModules();
		}
		///<summary>
		///The source instance is changed.  Load the data for that instance, and also
		///This means reload the destination instances also so that the old source
		///instance is now a potential destination.
		///</summary>
		///<param name="sender"></param>
		///<param name="e"></param>
		private void SourceInstanceChanged(object sender, System.EventArgs e)
		{
			LoadDestinationModules();
			LoadSourceModuleData();
		}
		/// <summary>
		/// same SourceInstanceChanged only in reverse.  Load data for the destination
		/// instance instead of source instance, and reload source instances.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DestinationInstanceChanged(object sender, System.EventArgs e)
		{
			LoadDestinationModuleData();
		}
		#endregion

		#region Data binding Functions
		private void LoadSourceModules()
		{
			if(ModuleTypes.SelectedIndex > -1)
			{
				int ModuleTypeID = Int32.Parse(ModuleTypes.SelectedItem.Value);

				ContentManagerDB contentDB = new ContentManagerDB();

				SourceInstance.DataValueField = "ModuleID";
				SourceInstance.DataTextField = "TabModule";

				//if a destination module has been picked, exclude that item.
				//otherwise, show all.
				if(DestinationInstance.SelectedIndex > -1)
				{
					SourceInstance.DataSource = contentDB.GetModuleInstancesExc(
						ModuleTypeID,
						Int32.Parse(DestinationInstance.SelectedItem.Value),
						Int32.Parse(SourcePortal.SelectedItem.Value));
					SourceInstance.DataBind();
				}
				else
				{
					SourceInstance.DataSource = contentDB.GetModuleInstances(ModuleTypeID,
						Int32.Parse(SourcePortal.SelectedItem.Value));
					SourceInstance.DataBind();
				}
				

				//if items exist in the sourceinstance, select the first item
				if(SourceInstance.Items.Count > 0)
				{
					SourceInstance.SelectedIndex = 0;
					LoadSourceModuleData();
				}
				else
				{
					//if there are no instances, there can be no data!!
					SourceListBox.Items.Clear();
				}

			}
		}

		private void LoadSourceModuleData()
		{
			//check to be sure that a source instance has been selected before proceeding.
			//this can cause errors if not checked for!
			if(SourceInstance.SelectedIndex > -1)
			{
				int SourceModID = Int32.Parse(SourceInstance.SelectedItem.Value);
				int ModuleTypeID = Int32.Parse(ModuleTypes.SelectedItem.Value);

				ContentManagerDB contentDB = new ContentManagerDB();
				SourceListBox.DataValueField = "ItemID";
				SourceListBox.DataTextField = "ItemDesc";
				SourceListBox.DataSource = contentDB.GetModuleData(ModuleTypeID,SourceModID);
				SourceListBox.DataBind();
			}
		}

		private void LoadDestinationModules()
		{
			if(SourceInstance.SelectedIndex > -1)
			{
				DestinationInstance.Items.Clear();

				//Get the Module Type(ex announcements) and the Source ModuleID
				int ModuleTypeID = Int32.Parse(ModuleTypes.SelectedItem.Value);
				int SourceModID = Int32.Parse(SourceInstance.SelectedItem.Value);

				ContentManagerDB contentDB = new ContentManagerDB();

				DestinationInstance.DataValueField = "ModuleID";
				DestinationInstance.DataTextField = "TabModule";
				DestinationInstance.DataSource = contentDB.GetModuleInstancesExc(ModuleTypeID,
					SourceModID,Int32.Parse(DestinationPortal.SelectedItem.Value));
				DestinationInstance.DataBind();

				//if any items exist in destination instance dropdown, select first and
				//load data for that instance.
				if(DestinationInstance.Items.Count > 0)
				{
					DestinationInstance.SelectedIndex = 0;
					LoadDestinationModuleData();
				}
			}
		}

		private void LoadDestinationModuleData()
		{
			if(DestinationInstance.SelectedIndex > -1)
			{
				int DestModID = Int32.Parse(DestinationInstance.SelectedItem.Value);
				int ModuleTypeID = Int32.Parse(ModuleTypes.SelectedItem.Value);

				ContentManagerDB contentDB = new ContentManagerDB();
				DestListBox.DataValueField = "ItemID";
				DestListBox.DataTextField = "ItemDesc";
				DestListBox.DataSource = contentDB.GetModuleData(ModuleTypeID,DestModID);
				DestListBox.DataBind();
			}
		}

		/// <summary>
		/// This function does 2 things.
		///First:  sets visibility of source portal/destination portal table to true/false
		///          depending on if the Module Configuration Settings have multiportalsupport
		///          enabled.
		///Second:  Selects the current portal instance as the default in the listboxes.
		///even if the listboxes are invisible to the user they need to have valid data!!
		/// </summary>
		private void MultiPortalSupport()
		{
			bool enabled = bool.Parse(Settings["MultiPortalSupport"].ToString());
			MultiPortalTable.Visible = enabled;

			//whether visible or not, we need to make sure that the proper sourceportal
			//and destination portal are selected.  Default == running portal instance.
			//This function iterates through the portals listed in SourcePortal listbox
			//by changing the SelectedItem.Value until the current portal instance running
			//is selected in the SourcePortal dropdownlist
			for(int i = 0; i < SourcePortal.Items.Count; i++)
			{
				SourcePortal.SelectedIndex = i;
				DestinationPortal.SelectedIndex = i;

				if(SourcePortal.SelectedItem.Value == (portalSettings.PortalID).ToString())
					return;

			}//end for
		}
		#endregion

		#region BUTTON EVENTS
		private void MoveLeft_Click(object sender, System.EventArgs e)
		{

			if(DestListBox.SelectedIndex > -1 && SourceInstance.SelectedIndex > -1 &&
				DestinationInstance.SelectedIndex > -1)
			{

				int ModuleTypeID = Int32.Parse(ModuleTypes.SelectedItem.Value);
				//source module ID not needed.

				//these two lines opposite in MoveRight_Click
				int DestModID = Int32.Parse(SourceInstance.SelectedItem.Value);
				int ItemToMove = Int32.Parse(DestListBox.SelectedItem.Value);

				ContentManagerDB contentDB = new ContentManagerDB();
				contentDB.MoveItem(ModuleTypeID,ItemToMove,DestModID);
				LoadSourceModuleData();
				LoadDestinationModuleData();
			}

		}

		private void MoveRight_Click(object sender, System.EventArgs e)
		{
			if(SourceListBox.SelectedIndex > -1 && SourceInstance.SelectedIndex > -1 &&
				DestinationInstance.SelectedIndex > -1)
			{

				int ModuleTypeID = Int32.Parse(ModuleTypes.SelectedItem.Value);

				//these two lines opposite in MoveLeft_Click
				int DestModID = Int32.Parse(DestinationInstance.SelectedItem.Value);
				int ItemToMove = Int32.Parse(SourceListBox.SelectedItem.Value);

				ContentManagerDB contentDB = new ContentManagerDB();
				contentDB.MoveItem(ModuleTypeID,ItemToMove,DestModID);
				LoadSourceModuleData();
				LoadDestinationModuleData();
			}
		}

		private void Copyright_Click(object sender, System.EventArgs e)
		{
			if(SourceListBox.SelectedIndex > -1 && SourceInstance.SelectedIndex > -1 &&
				DestinationInstance.SelectedIndex > -1)
			{
				int ModuleTypeID = Int32.Parse(ModuleTypes.SelectedItem.Value);
				int DestModID = Int32.Parse(DestinationInstance.SelectedItem.Value);
				int ItemToMove = Int32.Parse(SourceListBox.SelectedItem.Value);

				ContentManagerDB contentDB = new ContentManagerDB();
				contentDB.CopyItem(ModuleTypeID,ItemToMove,DestModID);
				LoadDestinationModuleData();

			}
		}
		private void CopyAll_Click(object sender, System.EventArgs e)
		{
			if(SourceListBox.Items.Count > 0 && SourceInstance.SelectedIndex > -1 &&
				DestinationInstance.SelectedIndex > -1)
			{
				int ModuleTypeID = Int32.Parse(ModuleTypes.SelectedItem.Value);
				int DestModID = Int32.Parse(DestinationInstance.SelectedItem.Value);
				int SourceModID = Int32.Parse(SourceInstance.SelectedItem.Value);

				ContentManagerDB contentDB = new ContentManagerDB();
				contentDB.CopyAll(ModuleTypeID,SourceModID,DestModID);
				LoadDestinationModuleData();
			}
		}

		private void DeleteLeft_Click(object sender, System.EventArgs e)
		{
			if(SourceListBox.SelectedIndex > -1)
			{
				int ModuleTypeID = Int32.Parse(ModuleTypes.SelectedItem.Value);
				int ItemToDelete = Int32.Parse(SourceListBox.SelectedItem.Value);

				ContentManagerDB contentDB = new ContentManagerDB();
				contentDB.DeleteItem(ModuleTypeID,ItemToDelete);
				LoadSourceModuleData();

			}
		}

		private void DeleteRight_Click(object sender, System.EventArgs e)
		{
			if(DestListBox.SelectedIndex > -1)
			{
				int ModuleTypeID = Int32.Parse(ModuleTypes.SelectedItem.Value);
				int ItemToDelete = Int32.Parse(DestListBox.SelectedItem.Value);

				ContentManagerDB contentDB = new ContentManagerDB();
				contentDB.DeleteItem(ModuleTypeID,ItemToDelete);
				LoadDestinationModuleData();

			}
		}
		#endregion

		public ContentManager()
		{
			//setting item for show portals
			SettingItem showPortals = new SettingItem(new BooleanDataType());
			showPortals.Value="false";
			showPortals.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			showPortals.Description ="Enable or Disable Multi-Portal Support";
			this._baseSettings.Add("MultiPortalSupport", showPortals);

		}

		public override Guid GuidID
		{
			get
			{
				return new Guid("{EDDD32E0-2135-4276-9157-3478995CCCD2}");
			}
		}

		//#region Search Implementation
		/// <summary>
		/// Searchable module
		/// </summary>
		public override bool Searchable
		{
			get
			{
				return false;
			}
		}


		public override void Install(System.Collections.IDictionary stateSaver)
		{
			string currentScriptName = System.IO.Path.Combine(Server.MapPath(TemplateSourceDirectory), "install.sql");

			ArrayList errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);
			if (errors.Count > 0)
			{
				// Call rollback
				throw new Exception("Error occurred:" + errors[0].ToString());
			}

			System.IO.DirectoryInfo installDir = new System.IO.DirectoryInfo(System.IO.Path.Combine(Server.MapPath(TemplateSourceDirectory), "InstallScripts"));
			System.IO.FileInfo[] installFiles = installDir.GetFiles("*_Install.sql");
			foreach(System.IO.FileInfo scriptToInstall in installFiles)
			{
				currentScriptName = scriptToInstall.FullName;
				errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);
				if(errors.Count > 0)
				{
					//call rollback
					throw new Exception("Error occured:" + errors[0].ToString());
				}	
			}
		}

		public override void Uninstall(System.Collections.IDictionary stateSaver)
		{
			string currentScriptName = System.IO.Path.Combine(Server.MapPath(TemplateSourceDirectory), "uninstall.sql");

			ArrayList errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);

			if (errors.Count > 0)
			{
				// Call rollback
				throw new Exception("Error occurred:" + errors[0].ToString());
			}

			System.IO.DirectoryInfo installDir = new System.IO.DirectoryInfo(System.IO.Path.Combine(Server.MapPath(TemplateSourceDirectory), "InstallScripts"));
			System.IO.FileInfo[] installFiles = installDir.GetFiles("*_uninstall.sql");
			foreach(System.IO.FileInfo scriptToInstall in installFiles)
			{
				currentScriptName = scriptToInstall.FullName;
				errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);
				if(errors.Count > 0)
				{
					//call rollback
					throw new Exception("Error occured:" + errors[0].ToString());
				}	
			}
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Raises Init event
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			// Create a new Title the control
			AddUrl = "~/DesktopModules/ContentManager/ContentManagerEdit.aspx";

			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.ModuleTypes.SelectedIndexChanged += new System.EventHandler(this.ModuleTypesChanged);
			this.SourcePortal.SelectedIndexChanged += new System.EventHandler(this.SourcePortalChanged);
			this.DestinationPortal.SelectedIndexChanged += new System.EventHandler(this.DestinationPortalChanged);
			this.SourceInstance.SelectedIndexChanged += new System.EventHandler(this.SourceInstanceChanged);
			this.DestinationInstance.SelectedIndexChanged += new System.EventHandler(this.DestinationInstanceChanged);
			this.DeleteLeft_Btn.Click += new System.EventHandler(this.DeleteLeft_Click);
			this.MoveLeft_Btn.Click += new System.EventHandler(this.MoveLeft_Click);
			this.MoveRight_Btn.Click += new System.EventHandler(this.MoveRight_Click);
			this.Copyright_Btn.Click += new System.EventHandler(this.Copyright_Click);
			this.CopyAll_Btn.Click += new System.EventHandler(this.CopyAll_Click);
			this.DeleteRight_Btn.Click += new System.EventHandler(this.DeleteRight_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
