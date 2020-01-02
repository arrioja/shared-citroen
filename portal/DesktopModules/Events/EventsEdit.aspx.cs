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
using Esperantus;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules 
{
	///	<summary>
	///	Edit events
	///	</summary>
	[History("Jes1111",	"2003/03/04", "Cache flushing now handled by inherited page")]
	[History("devsolution",	"devsolution@yahoo.com", "", "2003/06/17", "Added Calendar capability")]
	public class EventsEdit	: Rainbow.UI.AddEditItemPage
	{
		protected System.Web.UI.WebControls.TextBox	TitleField;
		protected Esperantus.WebControls.RequiredFieldValidator	RequiredTitle;
		protected System.Web.UI.WebControls.TextBox	WhereWhenField;
		protected Esperantus.WebControls.RequiredFieldValidator	RequiredWhereWhen;
		protected System.Web.UI.WebControls.TextBox	ExpireField;
		protected Esperantus.WebControls.RequiredFieldValidator	RequiredExpireDate;
		protected Esperantus.WebControls.CompareValidator VerifyExpireDate;
		protected System.Web.UI.WebControls.Label CreatedBy;
		protected Esperantus.WebControls.Literal CreatedLabel;
		protected Esperantus.WebControls.Literal OnLabel;
		protected System.Web.UI.WebControls.PlaceHolder	PlaceHolderHTMLEditor;
		protected System.Web.UI.WebControls.Label CreatedDate;
		protected Rainbow.UI.WebControls.IHtmlEditor DescriptionField;

		// devsolution 2003/6/17: Added	items for calendar control
		protected System.Web.UI.WebControls.RadioButtonList	AllDay;
		protected System.Web.UI.WebControls.TextBox	StartDate;
		protected System.Web.UI.WebControls.DropDownList StartHour;
		protected System.Web.UI.WebControls.DropDownList StartMinute;
		protected Esperantus.WebControls.Label Label5;
		protected Esperantus.WebControls.Label Label1;
		protected Esperantus.WebControls.Label Label2;
		protected Esperantus.WebControls.Label Label3;
		protected Esperantus.WebControls.Label Label7;
		protected Esperantus.WebControls.Label Label8;
		protected Esperantus.WebControls.Label Label4;
		protected Esperantus.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected Esperantus.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.DropDownList StartAMPM;
		// devsolution 2003/6/17: Finished - Added items for calendar control

		///	<summary>
		///	The	Page_Load event	on this	Page is	used to	obtain the ModuleID
		///	and	ItemID of the event	to edit.
		///	It then	uses the Rainbow.EventsDB()	data component
		///	to populate	the	page's edit	controls with the event	details.
		///	</summary>
		///	<param name="sender"></param>
		///	<param name="e"></param>
		private	void Page_Load(object sender, System.EventArgs e) 
		{
		
			// Added EsperantusKeys for Localization 
			// Mario Endara mario@softworks.com.uy 11/05/2004 
			
			foreach(ListItem item in AllDay.Items )
			{
				switch(AllDay.Items.IndexOf (item))
				{         
					case 0:
						item.Text = Esperantus.Localize.GetString ("EVENTS_ALLDAY");
						break;
					case 1:
						item.Text = Esperantus.Localize.GetString("EVENTS_STARTAT");
						break;
				}
			}
		
			//Change Indah Fuldner indah@die-seitenweber.de
			Rainbow.UI.DataTypes.HtmlEditorDataType	h =	new	Rainbow.UI.DataTypes.HtmlEditorDataType();
			h.Value	= moduleSettings["Editor"].ToString();
			DescriptionField = h.GetEditor(PlaceHolderHTMLEditor, ModuleID,	bool.Parse(moduleSettings["ShowUpload"].ToString()), portalSettings);

			DescriptionField.Width = new System.Web.UI.WebControls.Unit(moduleSettings["Width"].ToString());
			DescriptionField.Height	= new System.Web.UI.WebControls.Unit(moduleSettings["Height"].ToString());
			//End Change Indah Fuldner indah@die-seitenweber.de

			// If the page is being	requested the first	time, determine	if an
			// event itemID	value is specified,	and	if so populate page
			// contents	with the event details

			if (Page.IsPostBack	== false) 
			{
				if (ItemID != 0) 
				{
					// Obtain a	single row of event	information
					EventsDB events	= new EventsDB();
					SqlDataReader dr = events.GetSingleEvent(ItemID, WorkFlowVersion.Staging);
				
					try
					{
						// Read	first row from database
						if(dr.Read())
						{
							TitleField.Text	= (string) dr["Title"];
							DescriptionField.Text =	(string) dr["Description"];

							// devsolution 2003/6/17: Added items for calendar control
							if((bool)dr["AllDay"])
							{
								AllDay.SelectedIndex=0;
							}
							else
							{
								int	hour=0;
								int	minute=0;

								AllDay.SelectedIndex=1;
								StartHour.Enabled =	StartMinute.Enabled	= StartAMPM.Enabled	= true;

								string[] TimeParts = dr["StartTime"].ToString().Split(new Char[] {':'});

								try
								{
									if(TimeParts[0].Length > 0)	hour = int.Parse(TimeParts[0]);
									if(TimeParts.Length	> 1) minute	= int.Parse(TimeParts[1]);
								}
								catch
								{
								}

								if(hour>11)
								{
									StartAMPM.SelectedIndex	= 1;
									if(hour>12)	hour-=12;
								}
								else
								{
									if(hour==0)	hour=12;
									StartAMPM.SelectedIndex	= 0;
								}

								StartHour.SelectedIndex	= hour-1;
								StartMinute.SelectedIndex =	minute/5;
							}
							if(dr["StartDate"] != DBNull.Value )
								StartDate.Text = ((DateTime) dr["StartDate"]).ToShortDateString();
							else
								StartDate.Text = string.Empty;
							// devsolution 2003/6/17: Finished - Added items for calendar control

							ExpireField.Text = ((DateTime) dr["ExpireDate"]).ToShortDateString();
							CreatedBy.Text = (string) dr["CreatedByUser"];
							WhereWhenField.Text	= (string) dr["WhereWhen"];
							CreatedDate.Text = ((DateTime) dr["CreatedDate"]).ToShortDateString();
							// 15/7/2004 added localization by Mario Endara mario@softworks.com.uy
							if (CreatedBy.Text == "unknown")
							{
								CreatedBy.Text = Esperantus.Localize.GetString ( "UNKNOWN", "unknown");
							}
						}
					}
					finally
					{
						dr.Close();
					}
				}
				else
				{
					ExpireField.Text = DateTime.Now.AddDays(Int32.Parse(moduleSettings["DelayExpire"].ToString())).ToShortDateString();
					deleteButton.Visible = false; // Cannot	delete an unexsistent item
				}
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
				al.Add ("EF9B29C5-E481-49A6-9383-8ED3AB42DDA0");
				return al;
			}
		}

		/// <summary>
		/// DevSolution 2003/6/17
		///	AllDay_SelectedIndexChanged fired when AllDay or Specific Time selected
		///	so that the appropriate combo boxes can be disabled for useability
		/// </summary>
		/// <param name="sender">Who is sending the request</param>
		/// <param name="e">Standard EventArgs</param>
		private	void AllDay_SelectedIndexChanged(object	sender,	System.EventArgs e)
		{
			StartMinute.Enabled	= StartAMPM.Enabled	= StartHour.Enabled	= (AllDay.SelectedItem.Value ==	"0");
		}

		///	<summary>
		///	The	UpdateBtn_Click	event handler on this Page is used to either
		///	create or update an	event.	It uses	the	Rainbow.EventsDB()
		///	data component to encapsulate all data functionality.
		///	</summary>
		/// <param name="e">Standard EventArgs</param>
		override protected void	OnUpdate(EventArgs e)
		{
			base.OnUpdate(e);

			// Only	Update if the Entered Data is Valid
			if (Page.IsValid ==	true) 
			{
				// Create an instance of the Event DB component
				EventsDB events	= new EventsDB();

				// devsolution 2003/6/17: Added items for calendar control
				string StartTime = string.Empty;
				bool IsAllDay =	(AllDay.SelectedItem.Value == "1");

				if(IsAllDay)
				{
					int	hour=int.Parse(StartHour.SelectedItem.Text);
					int	minute=int.Parse(StartMinute.SelectedItem.Text);

					if(StartAMPM.SelectedItem.Value=="PM") 
					{
						if(hour<12)	hour+=12;
					}
					else
					{
						if(hour==12) hour-=12;
					}
					StartTime =	string.Format("{0:00}:{1:00}:00", hour,	minute);
				}
				// devsolution 2003/6/17: Finished - Added items for calendar control

				if (ItemID == 0) 
				{
					// Add the event within	the	Events table
					events.AddEvent( ModuleID, ItemID, PortalSettings.CurrentUser.Identity.Email, TitleField.Text, DateTime.Parse(ExpireField.Text), DescriptionField.Text,	WhereWhenField.Text, IsAllDay, StartDate.Text, StartTime);
				}
				else 
				{
					// Update the event	within the Events table
					events.UpdateEvent(	ModuleID, ItemID, PortalSettings.CurrentUser.Identity.Email, TitleField.Text, DateTime.Parse(ExpireField.Text),	DescriptionField.Text, WhereWhenField.Text,	IsAllDay, StartDate.Text, StartTime	);
				}

				// Redirect	back to	the	portal home	page
				this.RedirectBackToReferringPage();
			}
		}

		///	<summary>
		///	The	DeleteBtn_Click	event handler on this Page is used to delete an
		///	an event.  It  uses	the	Rainbow.EventsDB() data	component to
		///	encapsulate	all	data functionality.
		///	</summary>
		override protected void	OnDelete(EventArgs e) 
		{
			base.OnDelete(e);

			// Only	attempt	to delete the item if it is	an existing	item
			// (new	items will have	"ItemID" of	0)

			if (ItemID != 0) 
			{
				EventsDB events	= new EventsDB();
				events.DeleteEvent(ItemID);
			}

			// Redirect	back to	the	portal home	page
			this.RedirectBackToReferringPage();
		}

		#region	Web	Form Designer generated	code
		///	<summary>
		///	Raises OnInitEvent
		///	</summary>
		///	<param name="e"></param>
		protected override void	OnInit(EventArgs e)
		{
			//Translate
			RequiredTitle.ErrorMessage = Esperantus.Localize.GetString("EVENTS_VALID_TITLE");
			// RequiredDescription.ErrorMessage = Esperantus.Localize.GetString("EVENTS_VALID_DESCRIPTION");
			RequiredWhereWhen.ErrorMessage = Esperantus.Localize.GetString("EVENTS_VALID_WHERE-WHEN");
			RequiredExpireDate.ErrorMessage	= Esperantus.Localize.GetString("EVENTS_VALID_EXPIRE");
			VerifyExpireDate.ErrorMessage =	Esperantus.Localize.GetString("EVENTS_VALID_EXPIRE");
 
			InitializeComponent();

			base.OnInit(e);
		}

		///	<summary>
		///	Required method	for	Designer support - do not modify
		///	the	contents of	this method	with the code editor.
		///	</summary>
		private	void InitializeComponent() 
		{
			this.AllDay.SelectedIndexChanged += new System.EventHandler(this.AllDay_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	}
}
