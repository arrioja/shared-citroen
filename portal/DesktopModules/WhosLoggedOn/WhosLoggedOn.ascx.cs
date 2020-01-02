using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.IO;
using System.Data.SqlClient;
using Esperantus;
using Rainbow.Security;
using Rainbow.Configuration;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Rainbow.Admin;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// Who's Logged On Module - Uses the monitoring database table to work
	/// out and display who is currently logged on, anonymous or otherwise
	/// Written by Paul Yarrow, paul@paulyarrow.com
	/// </summary>
	public class WhosLoggedOn : PortalModuleControl
	{
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label LabelAnonUsersCount;
		protected System.Web.UI.WebControls.Label LabelRegUsersOnlineCount;
		protected System.Web.UI.WebControls.Label LabelRegUserNames;
		private int minutesToCheckForUsers = 30;
		protected Esperantus.WebControls.Literal Literal1;
		protected Esperantus.WebControls.Literal Literal2;
		protected Esperantus.WebControls.Literal Literal3;
		private WhosLoggedOnDB whosDB = new WhosLoggedOnDB();
	
		/// <summary>
		/// Initial Revision by Paul Yarrow, paul@paulyarrow.com, 2003-07-29
		/// </summary>
		public WhosLoggedOn()
		{
			SettingItem cacheTime = new SettingItem(new IntegerDataType());
			cacheTime.Required = true;
			cacheTime.Order = 0;
			cacheTime.Value = "1";
			cacheTime.MinValue = 0;
			cacheTime.MaxValue = 60000;
			cacheTime.Description = Esperantus.Localize.GetString("WHOSLOGGEDONCACHETIMEOUT", "Specify an amount of time the who's logged on module will wait before checking again (0 - 60000)", this);
			this._baseSettings.Add("CacheTimeout", cacheTime);
		}

		/// <summary>
		/// Initial Revision by Paul Yarrow, paul@paulyarrow.com, 2003-07-13
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void RainbowVersion_Load(object sender, System.EventArgs e)
		{
			int cacheTime = Int32.Parse((SettingItem) Settings["CacheTimeout"]);

			int anonUserCount, regUsersOnlineCount;
			string regUsersString;
			whosDB.GetUsersOnline( portalSettings.PortalID,
							minutesToCheckForUsers,
							cacheTime,
							out anonUserCount,
							out regUsersOnlineCount,
							out regUsersString);

			LabelAnonUsersCount.Text = Convert.ToString(anonUserCount);
			LabelRegUsersOnlineCount.Text = Convert.ToString(regUsersOnlineCount);
			LabelRegUserNames.Text = regUsersString;
		}
	
		/// <summary>
		/// Initial Revision by Paul Yarrow, paul@paulyarrow.com, 2003-07-13
		/// </summary>
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{52AD3A51-121D-48bc-9782-02076E0D6A69}");
			}
		}

		# region Install / Uninstall Implementation
		public override void Install(System.Collections.IDictionary stateSaver)
		{
			string currentScriptName = System.IO.Path.Combine(Server.MapPath(TemplateSourceDirectory), "install.sql");

			

			ArrayList errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);
			if (errors.Count > 0)
			{
				// Call rollback
				throw new Exception("Error occurred:" + errors[0].ToString());
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
		}
		#endregion

		#region Web Form Designer generated code
		/// <summary>
		/// Raises Init event
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() 
		{    
			this.Load += new System.EventHandler(this.RainbowVersion_Load);

		}
		#endregion
	}
}