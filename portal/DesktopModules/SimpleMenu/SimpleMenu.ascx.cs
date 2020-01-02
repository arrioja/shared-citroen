using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using System.Text;

using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes; 
using Rainbow.Configuration;
using Rainbow.Design;

namespace Rainbow.DesktopModules.SimpleMenu
{
	/// <summary>
	/// SimpleMenu Module
	/// created 30/04/2003 by Mario Hartmann 
	/// mario@hartmann.net // http://mario.hartmann.net
	/// </summary>
	[History("Mario Hartmann","mario@hartmann.net","1.0","2004/06/06","added dynamic loading of Menutypes")]
	[History("Mario Hartmann","mario@hartmann.net","0.10 beta","2003/10/01","added Solpart Menu")]
	[History("Mario Hartmann","mario@hartmann.net","0.9 beta","2003/09/08","added horizontal alignment")]
	[History("Mario Hartmann","mario@hartmann.net","0.8 beta","2003/06/19","added new ItemMenu")]
	[History("Mario Hartmann","mario@hartmann.net","0.5 beta","2003/05/27","initial beta release")]
	public class SimpleMenu : PortalModuleControl 
	{
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolder;

		/// <summary>
		/// Public constructor. Sets base settings for module.
		/// </summary>
		public SimpleMenu() 
		{
			SettingItem setParentTabID = new SettingItem(new IntegerDataType());
			setParentTabID.Required = true;
			setParentTabID.Value = "0";
			setParentTabID.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			setParentTabID.EnglishName = "ParentTabId";
			setParentTabID.Description = "Sets the Id of then Parent tab for the menu (this tab may be hidden or inaccessible for the logged on user.)";
			setParentTabID.Order = 1;
			this._baseSettings.Add("sm_ParentTabID", setParentTabID);

			//localized by Pekka Ylenius
			ArrayList SetRepeatDirectionArrayList = new ArrayList();
			SetRepeatDirectionArrayList.Add( new SettingOption(0, 
				Esperantus.Localize.GetString("HORIZONTAL", "Horizontal"))); 
			SetRepeatDirectionArrayList.Add( new SettingOption(1, 
				Esperantus.Localize.GetString("VERTICAL", "Vertical"))); 

			SettingItem setMenuRepeatDirection = new SettingItem(new CustomListDataType(SetRepeatDirectionArrayList, "Name", "Val")); 

			setMenuRepeatDirection.Required = true; 
			setMenuRepeatDirection.Order = 2; 
			setMenuRepeatDirection.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS; 
			setMenuRepeatDirection.Description = "Sets the repeat direction for menu rendering."; 
			setMenuRepeatDirection.EnglishName = "Menu RepeatDirection";
			this._baseSettings.Add("sm_Menu_RepeatDirection", setMenuRepeatDirection); 

			// MenuLayouts
			Hashtable menuTypes = new Hashtable();
			foreach(string menuTypeControl in System.IO.Directory.GetFiles(HttpContext.Current.Server.MapPath(Rainbow.Settings.Path.WebPathCombine(Rainbow.Settings.Path.ApplicationRoot, "/DesktopModules/SimpleMenu/SimpleMenuTypes/")), "*.ascx"))
			{
				string menuTypeControlDisplayName = menuTypeControl.Substring(menuTypeControl.LastIndexOf("\\") + 1, menuTypeControl.LastIndexOf(".") - menuTypeControl.LastIndexOf("\\") - 1);
				string menuTypeControlName = menuTypeControl.Substring(menuTypeControl.LastIndexOf("\\") + 1);
				menuTypes.Add(menuTypeControlDisplayName, menuTypeControlName);
			}

			// Thumbnail Layout Setting
			SettingItem menuTypeSetting = new SettingItem(new CustomListDataType(menuTypes, "Key", "Value"));
			menuTypeSetting.Required = true;
			menuTypeSetting.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			menuTypeSetting.Value = "StaticItemMenu.ascx";
			menuTypeSetting.Description = "Sets the type of menu this module use.";
			menuTypeSetting.EnglishName = "MenuType";
			menuTypeSetting.Order = 3;
			this._baseSettings.Add("sm_MenuType", menuTypeSetting);

			
			ArrayList SetBindingArrayList  = new ArrayList();
			SetBindingArrayList.Add(new SettingOption((int)BindOption.BindOptionNone, Esperantus.Localize.GetString("BIND_OPTION_NONE","BindOptionNone")));
			SetBindingArrayList.Add(new SettingOption((int)BindOption.BindOptionTop, Esperantus.Localize.GetString("BIND_OPTION_TOP","BindOptionTop")));
			SetBindingArrayList.Add(new SettingOption((int)BindOption.BindOptionChildren, Esperantus.Localize.GetString("BIND_OPTION_CHILDREN","BindOptionChildren")));
			SetBindingArrayList.Add(new SettingOption((int)BindOption.BindOptionCurrentChilds, Esperantus.Localize.GetString("BIND_OPTION_CURRENT_CHILDS","BindOptionCurrentChilds")));
			SetBindingArrayList.Add(new SettingOption((int)BindOption.BindOptionDefinedParent, Esperantus.Localize.GetString("BIND_OPTION_DEFINED_PARENT","BindOptionDefinedParent")));
			SetBindingArrayList.Add(new SettingOption((int)BindOption.BindOptionSiblings, Esperantus.Localize.GetString("BIND_OPTION_SIBLINGS","BindOptionSiblings")));
			SetBindingArrayList.Add(new SettingOption((int)BindOption.BindOptionSubtabSibling, Esperantus.Localize.GetString("BIND_OPTION_SUBTAB_SIBLING","BindOptionSubtabSibling")));
			SetBindingArrayList.Add(new SettingOption((int)BindOption.BindOptionTabSibling, Esperantus.Localize.GetString("BIND_OPTION_TABSIBLING","BindOptionTabSibling")));

			SettingItem setMenuBindingType = new SettingItem(new CustomListDataType(SetBindingArrayList, "Name", "Val"));
			setMenuBindingType.Required = true;
			setMenuBindingType.Order = 4;
			setMenuBindingType.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			setMenuBindingType.EnglishName = "MenuBindingType";
			this._baseSettings.Add("sm_MenuBindingType", setMenuBindingType);

			//			SettingItem setHeaderText = new SettingItem(new StringDataType());
			//			setHeaderText.Required = false;
			//			setHeaderText.Value = string.Empty;
			//			setHeaderText.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			//			setHeaderText.Description ="Sets a header text of the static menu (the special setting <CurrentTab> displays the current TabName).";
			//			setHeaderText.Order = 5;
			//			this._baseSettings.Add("sm_Menu_HeaderText", setHeaderText);
			//		
			//			SettingItem setFooterText = new SettingItem(new StringDataType());
			//			setFooterText.Required = false;
			//			setFooterText.Value = string.Empty;
			//			setFooterText.Group = SettingItemGroup.MODULE_SPECIAL_SETTINGS;
			//			setFooterText.Description ="Sets a footer text of the static menu.";
			//
			//			setFooterText.Order = 6;
			//			this._baseSettings.Add("sm_Menu_FooterText", setFooterText);
		
		}


		/// <summary>
		/// The Page_Load event handler on this User Control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e)
		{
			string menuType = "SimpleMenu";
			if (Settings["sm_MenuType"] != null)
				menuType = (Settings["sm_MenuType"].ToString());

			try
			{
				SimpleMenuType  theMenu = (SimpleMenuType) this.LoadControl(Rainbow.Settings.Path.ApplicationRoot + "/DesktopModules/SimpleMenu/SimpleMenuTypes/" + menuType);
				theMenu.GlobalPortalSettings = portalSettings;
				theMenu.ModuleSettings	 = Settings;
				theMenu.DataBind();
				PlaceHolder.Controls.Add(theMenu);   
			}
			catch (Exception)
			{
				Literal tmpError = new Literal ();
				tmpError.Text=Esperantus.Localize.GetString("ERROR_MENUETYPE_LOAD", "The MenuType '{1}' cannot be loaded.",this).Replace("{1}",menuType);
				PlaceHolder.Controls.Add (tmpError);
			}
		}


		#region General module Implementation
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{D3182CD6-DAFF-4E72-AD9E-0B28CB44F006}");
			}
		}


		#region  Search Implementation
		/// <summary>
		/// If the module is searchable you
		/// must override the property to return true
		/// </summary>
		public override bool Searchable
		{
			get
			{
				return false;
			}
		}		
	
		#endregion

		# region Install / Uninstall Implementation
		//		public override void Install(System.Collections.IDictionary stateSaver)
		//		{
		//			string currentScriptName = System.IO.Path.Combine(Server.MapPath(TemplateSourceDirectory), "install.sql");
		//			ArrayList errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);
		//			if (errors.Count > 0)
		//			{
		//				// Call rollback
		//				throw new Exception("Error occurred:" + errors[0].ToString());
		//			}
		//		}
		//
		//		public override void Uninstall(System.Collections.IDictionary stateSaver)
		//		{
		//			string currentScriptName = System.IO.Path.Combine(Server.MapPath(TemplateSourceDirectory), "uninstall.sql");
		//			ArrayList errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);
		//			if (errors.Count > 0)
		//			{
		//				// Call rollback
		//				throw new Exception("Error occurred:" + errors[0].ToString());
		//			}
		//		}

		# endregion
		
		#endregion


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	}
}
