using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// ShortcutAll module provide a quick way to duplicate
	/// a module content in different page from different portals 
 	/// </summary>
	[History("Mario Hartmann","mario@hartmann.net","1.3","2003/10/08","moved to seperate folder")]
	public class ShortcutAll : Shortcut 
	{

		public ShortcutAll()
		{
			// Get a list of modules of all portals
			SettingItem LinkedModule = new SettingItem(new CustomListDataType(new ModulesDB().GetModulesAllPortals(), "ModuleTitle", "ModuleID"));
			LinkedModule.Required = true;
			LinkedModule.Order = 0;
			LinkedModule.Value = "0";
			//Overrides the base setting
			this._baseSettings["LinkedModule"] = LinkedModule;
		}

		#region General Implementation
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{F9F9C3A4-6E16-43b4-B540-984DDB5F1CD0}");
			}
		}
		#endregion

		#region Web Form Designer generated code
		/// <summary>
		/// On init
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			InitializeComponent();
			base.OnInit(e);

			int p = portalSettings.PortalID;
		}

		private void InitializeComponent() 
		{
		}
		#endregion
	}
}
