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
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.Security;
using Rainbow.Configuration;
using Esperantus;

namespace Rainbow.AdminAll 
{
	/// <summary>
	/// Add/Remove modules, assign modules to portals
	/// </summary>
	public class ModuleDefinitions : Rainbow.UI.EditItemPage
	{
		protected Esperantus.WebControls.Label Label1;
		protected Esperantus.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox FriendlyName;
		protected Esperantus.WebControls.RequiredFieldValidator Req1;
		protected Esperantus.WebControls.Label Label3;
		protected System.Web.UI.WebControls.TextBox DesktopSrc;
		protected Esperantus.WebControls.RequiredFieldValidator Req2;
		protected Esperantus.WebControls.Label Label4;
		protected System.Web.UI.WebControls.TextBox MobileSrc;
		protected Esperantus.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label lblGUID;
		protected Esperantus.WebControls.LinkButton selectAllButton;
		protected Esperantus.WebControls.LinkButton selectNoneButton;
		protected Esperantus.WebControls.Label Label6;
		protected System.Web.UI.WebControls.CheckBoxList PortalsName;
		protected Esperantus.WebControls.Label lblInvalidModule;
		protected Esperantus.WebControls.Label Label7;
		protected System.Web.UI.WebControls.TextBox InstallerFileName;
		protected Esperantus.WebControls.RequiredFieldValidator Requiredfieldvalidator1;
		protected Esperantus.WebControls.Button btnUseInstaller;
		protected Esperantus.WebControls.Button btnDescription;
		protected System.Web.UI.HtmlControls.HtmlTable tableInstaller;
		protected System.Web.UI.HtmlControls.HtmlTable tableManual;
		protected System.Web.UI.WebControls.Label lblErrorDetail;

		Guid defID;

		/// <summary>
		/// The Page_Load server event handler on this page is used
		/// to populate the role information for the page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Verify that the current user has access to access this page
			// Removed by Mario Endara <mario@softworks.com.uy> (2004/11/04)
//			if (PortalSecurity.IsInRoles("Admins") == false) 
//				PortalSecurity.AccessDeniedEdit();

			// Calculate security defID
			if (Request.Params["defID"] != null) 
				defID = new Guid(Request.Params["defID"]);

			ModulesDB modules = new ModulesDB();
            
			// If this is the first visit to the page, bind the definition data 
			if (Page.IsPostBack == false)
			{

				if (defID.ToString() == "00000000-0000-0000-0000-000000000000") 
				{
					ShowInstaller(true);
					// new module definition
					InstallerFileName.Text = "DesktopModules/MyModuleFolder/install.xml";
					FriendlyName.Text = "New Definition";
					DesktopSrc.Text = "DesktopModules/MyModule.ascx";
					MobileSrc.Text = "MobileModules/MyModule.ascx";
				}
				else 
				{
					ShowInstaller(false);
					// Obtain the module definition to edit from the database
					SqlDataReader dr = modules.GetSingleModuleDefinition(defID);
                
					// Read in first row from database
					while(dr.Read())
					{
						FriendlyName.Text = (string) dr["FriendlyName"].ToString();
						DesktopSrc.Text = (string) dr["DesktopSrc"].ToString();
						MobileSrc.Text = (string) dr["MobileSrc"].ToString();
						lblGUID.Text = dr["GeneralModDefID"].ToString();
					}
					dr.Close(); //by Manu, fixed bug 807858
				}

				// Populate checkbox list with all portals
				// and "check" the ones already configured for this tab
				SqlDataReader portals = modules.GetModuleInUse(defID);

				// Clear existing items in checkboxlist
				PortalsName.Items.Clear();

				while(portals.Read()) 
				{
					if (Convert.ToInt32(portals["PortalID"]) >= 0)
					{
						ListItem item = new ListItem();
						item.Text = (string) portals["PortalName"];
						item.Value = portals["PortalID"].ToString();
            
						if ((portals["checked"].ToString()) == "1") 
							item.Selected = true;
						else
							item.Selected = false;

						PortalsName.Items.Add(item);
					}
				}
				portals.Close(); //by Manu, fixed bug 807858
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
				al.Add ("D04BB5EA-A792-4E87-BFC7-7D0ED3ADD582");
				return al;
			}
		}

		/// <summary>
		/// OnUpdate installs or refresh module definiton on db
		/// </summary>
		/// <param name="e"></param>
		protected override void OnUpdate(EventArgs e)
		{
			if (Page.IsValid) 
			{
				try
				{
					if (!btnUseInstaller.Visible)
						Rainbow.Helpers.ModuleInstall.InstallGroup(Server.MapPath(Rainbow.Settings.Path.ApplicationRoot + "/" + InstallerFileName.Text), lblGUID.Text == string.Empty);
					else
						Rainbow.Helpers.ModuleInstall.Install(FriendlyName.Text, DesktopSrc.Text, MobileSrc.Text, lblGUID.Text == string.Empty);

					ModulesDB modules = new ModulesDB();

					// Update the module definition
					for (int i = 0; i < PortalsName.Items.Count; i++)
					{
						modules.UpdateModuleDefinitions(defID, Convert.ToInt32(PortalsName.Items[i].Value), (bool) PortalsName.Items[i].Selected);
					}
	            
					// Redirect back to the portal admin page
					RedirectBackToReferringPage();
				}
				catch(Exception ex)
				{
					lblErrorDetail.Text = Esperantus.Localize.GetString("MODULE_DEFINITIONS_INSTALLING", "An error occurred installing.", this) + "<br>";
					if (!btnUseInstaller.Visible)
						lblErrorDetail.Text += " Installer: " + Server.MapPath(Rainbow.Settings.Path.ApplicationRoot + "/" + InstallerFileName.Text);
					else
						lblErrorDetail.Text += " Module: '" + FriendlyName.Text + "' - Source: '" + DesktopSrc.Text + "' - Mobile: '" + MobileSrc.Text + "'";
					lblErrorDetail.Visible = true;

					Rainbow.Configuration.ErrorHandler.HandleException(lblErrorDetail.Text, ex);
				}
			}
		}


		/// <summary>
		/// Delete a Module definition
		/// </summary>
		/// <param name="e"></param>
		protected override void OnDelete(EventArgs e)
		{
			try
			{
				if (!btnUseInstaller.Visible)
					Rainbow.Helpers.ModuleInstall.UninstallGroup(Server.MapPath(Rainbow.Settings.Path.ApplicationRoot + "/" + InstallerFileName.Text));
				else
					Rainbow.Helpers.ModuleInstall.Uninstall(DesktopSrc.Text, MobileSrc.Text);

				// Redirect back to the portal admin page
				RedirectBackToReferringPage();
			}
			catch(Exception ex)
			{
				lblErrorDetail.Text = Esperantus.Localize.GetString("MODULE_DEFINITIONS_DELETE_ERROR", "An error occurred deleting module.", this);
				lblErrorDetail.Visible = true;
				Rainbow.Configuration.ErrorHandler.HandleException(lblErrorDetail.Text, ex);
			}
		}

		private void selectAllButton_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < PortalsName.Items.Count; i++)
			{
				PortalsName.Items[i].Selected = true;
			}
		}

		private void selectNoneButton_Click(object sender, System.EventArgs e)
		{
			for (int i = 0; i < PortalsName.Items.Count; i++)
			{
				PortalsName.Items[i].Selected = false;
			}
		}

		#region Web Form Designer generated code
		/// <summary>
		/// OnInit
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
			this.btnUseInstaller.Click += new System.EventHandler(this.btnUseInstaller_Click);
			this.btnDescription.Click += new System.EventHandler(this.btnDescription_Click);
			this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
			this.selectNoneButton.Click += new System.EventHandler(this.selectNoneButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnUseInstaller_Click(object sender, System.EventArgs e)
		{
			ShowInstaller(true);
		}

		private void ShowInstaller(bool installer)
		{
			if (installer)
			{
				tableInstaller.Visible = true;
				tableManual.Visible = false;
				btnUseInstaller.Visible = false;
				btnDescription.Visible = true;
			}
			else
			{
				tableInstaller.Visible = false;
				tableManual.Visible = true;
				btnUseInstaller.Visible = true;
				btnDescription.Visible = false;			
			}
		
		}

		private void btnDescription_Click(object sender, System.EventArgs e)
		{
			ShowInstaller(false);
		}

	}
}