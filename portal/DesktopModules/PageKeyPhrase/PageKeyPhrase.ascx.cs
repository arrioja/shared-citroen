using System;
using System.Web.UI.WebControls;

using Rainbow.Configuration;
using Rainbow.UI.DataTypes; 
using Rainbow.UI.WebControls;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// PageKeyPhrase is a module that uses the PageKeyPhrase Control to allow users to position the menu keyphrase without the need to touch the layout.
	/// </summary>
	[History("John.Mandia@whitelightsolutions.com","2003/10/25","Created module and the control")]
	public class PageKeyPhraseModule : PortalModuleControl
	{
		protected Rainbow.UI.WebControls.PageKeyPhrase _thisTabKeyPhrase;
		public PageKeyPhraseModule()
		{
			
		}

		private void PageKeyPhraseModule_Load(object sender, System.EventArgs e)
		{
			//_thisTabKeyPhrase.DataBind();
		}
	
		#region General Implementation
		
		/// <summary>
		/// GuidID 
		/// </summary>
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{ED58A5FB-D041-4a4b-826E-654250B61E7C}");
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
			this.Load += new System.EventHandler(this.PageKeyPhraseModule_Load);

		}
		#endregion


		
	}
}