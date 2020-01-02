using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using Rainbow.UI.DataTypes;
using Esperantus;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules
{

	/// <summary>
	///	Summary description for EmailForm.
	/// </summary>
	public class EmailForm : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.TextBox txtCc;
		protected System.Web.UI.WebControls.TextBox txtBcc;
		protected System.Web.UI.WebControls.TextBox txtSubject;
		protected Rainbow.UI.WebControls.IHtmlEditor txtBody;
		protected System.Web.UI.WebControls.TextBox txtTo;

		private EmailAddressList _to;
		private EmailAddressList _cc;
		private EmailAddressList _bcc;
		protected System.Web.UI.WebControls.Label lblEmailAddressesNotOk;
		protected Esperantus.WebControls.Literal Literal1;
		protected Esperantus.WebControls.Literal Literal2;
		protected Esperantus.WebControls.Literal Literal3;
		protected Esperantus.WebControls.Literal Literal4;
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolderHTMLEditor;

		private bool _allAddressesOk = true;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if ( IsPostBack )
			{
				_allAddressesOk = true;
				// Initialize To addresses
				foreach (string em in txtTo.Text.Split(";".ToCharArray()))
				{
					try
					{
						if ( em.Trim() != string.Empty )
							To.Add(em);
					}
					catch (ArgumentException ae)
					{
						string message = ae.Message;
						_allAddressesOk = false;
					}
				}
				// Initialize Cc addresses
				foreach (string em in txtCc.Text.Split(";".ToCharArray()))
				{
					try
					{
						if ( em.Trim() != string.Empty )
							Cc.Add(em);
					}
					catch (ArgumentException ae)
					{
						string message = ae.Message;
						_allAddressesOk = false;
					}
				}
				// Initialize To addresses
				foreach (string em in txtBcc.Text.Split(";".ToCharArray()))
				{
					try
					{
						if ( em.Trim() != string.Empty )
							Bcc.Add(em);
					}
					catch (ArgumentException ae)
					{
						string message = ae.Message;
						_allAddressesOk = false;
					}
				}
				// Show error
				lblEmailAddressesNotOk.Visible = ! AllEmailAddressesOk;
			}
			else
			{
				txtTo.Text = string.Join(";",(string[])To.ToArray(typeof(string)));
				txtCc.Text = string.Join(";",(string[])Cc.ToArray(typeof(string)));
				txtBcc.Text = string.Join(";",(string[])Bcc.ToArray(typeof(string)));
			}
		}

		/// <summary>
		/// Collection containing all to email addresses
		/// </summary>
		public EmailAddressList To
		{
			get { return _to; }
		}

		/// <summary>
		/// Collection containing all cc email addresses
		/// </summary>
		public EmailAddressList Cc
		{
			get { return _cc; }
		}

		/// <summary>
		/// Collection containing all bcc email addresses
		/// </summary>
		public EmailAddressList Bcc
		{
			get { return _bcc; }
		}

		/// <summary>
		/// Contains subject
		/// </summary>
		public string Subject
		{
			get
			{
				return txtSubject.Text;
			}
			set
			{
				if ( value == null )
					throw new ArgumentNullException("Subject", "Subject can not contain null values!");
				txtSubject.Text = value;
			}
		}

		/// <summary>
		/// Contains text for the body of the email in html format
		/// </summary>
		public string HtmlBodyText
		{
			get
			{
				return txtBody.Text;
			}
			set
			{
				if ( value == null )
					throw new ArgumentNullException("HtmlBodyText", "HtmlBodyText can not contain null values!");
				txtBody.Text = value;
			}
		}

		/// <summary>
		/// Contains text for the body of the email in plain text format
		/// </summary>
		public string BodyText
		{
			get
			{
				return txtBody.Text;
			}
			set
			{
				if ( value == null )
					throw new ArgumentNullException("BodyText", "BodyText can not contain null values!");
				txtBody.Text = value;
			}
		}

		public bool AllEmailAddressesOk
		{
			get
			{
				return _allAddressesOk;
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);

			_to = new EmailAddressList();
			_cc = new EmailAddressList();
			_bcc = new EmailAddressList();

			HtmlEditorDataType h = new HtmlEditorDataType();
			PortalSettings pS = (PortalSettings) HttpContext.Current.Items["PortalSettings"];
			try 
			{
				h.Value = pS.CustomSettings["SITESETTINGS_DEFAULT_EDITOR"].ToString();
				txtBody = h.GetEditor(PlaceHolderHTMLEditor, int.Parse(Context.Request["mID"]), bool.Parse(pS.CustomSettings["SITESETTINGS_SHOWUPLOAD"].ToString()), pS);
			}
			catch 
			{
				txtBody = h.GetEditor(PlaceHolderHTMLEditor, int.Parse(Context.Request["mID"]), true, pS);
			}

			lblEmailAddressesNotOk.Text = Esperantus.Localize.GetString("EMF_ADDRESSES_NOT_OK", "The emailaddresses are not ok.", lblEmailAddressesNotOk);
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
