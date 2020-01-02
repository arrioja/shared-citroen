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
using Rainbow.DesktopModules;
using Rainbow.Configuration;

namespace Rainbow.DesktopModules 
{
	/// <summary>
	/// Users Defined Table module - Edit page part
	/// Written by: Shaun Walker (IbuySpy Workshop)
	/// Moved into Rainbow by Jakob Hansen, hansen3000@hotmail.com
	/// </summary>
	[History("Ender", "2003/03/18", "Added file and Xsl functionality")]
	[History("mario@hartmann.net", "2004/05/28", "Added image and file selection by dropdown functionality")]
	public class UserDefinedTableEdit : Rainbow.UI.EditItemPage
	{
		protected System.Web.UI.WebControls.Table tblFields;
		protected System.Web.UI.WebControls.Label lblMessage;
		private int UserDefinedRowID = -1;
		protected Esperantus.WebControls.Label EditTableRow;

		//protected string prefix = "_ctl0:";
		protected string prefix = string.Empty;


		/// <summary>
		/// The Page_Load event on this Page is used to ...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e) 
		{

			if (! (Request.Params["UserDefinedRowID"] == null) )
				UserDefinedRowID = Int32.Parse(Request.Params["UserDefinedRowID"].ToString());

			BuildTable();

			if ( Page.IsPostBack == false )
			{

				if ( UserDefinedRowID != -1 )
				{
					Control tb;
					UserDefinedTableDB  objUserDefinedTable = new UserDefinedTableDB();
					SqlDataReader dr = objUserDefinedTable.GetSingleUserDefinedRow(UserDefinedRowID, ModuleID);
					try
					{
						while (dr.Read())
						{
							tb = tblFields.FindControl(dr["FieldTitle"].ToString());
							if(tb != null )
							{
								if (tb.GetType() == typeof(TextBox))
									((TextBox) tb).Text = dr["FieldValue"].ToString();
								if (tb.GetType() == typeof(DropDownList))
									if (((DropDownList) tb).Items.Count >0)
									{
										try{((DropDownList) tb).Items.FindByValue(dr["FieldValue"].ToString()).Selected = true;}
										catch{((DropDownList) tb).Items[0].Selected = true;}
									}
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
					this.deleteButton.Visible = false;
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
				al.Add ("2502DB18-B580-4F90-8CB4-C15E6E531021");
				return al;
			}
		}

		private void BuildTable()
		{
			UserDefinedTableDB  objUserDefinedTable = new UserDefinedTableDB();
			TableRow objRow;
			TableCell objCell;

			SqlDataReader dr = objUserDefinedTable.GetUserDefinedFields(ModuleID);
			try
			{
				while (dr.Read())
				{
					objRow = new TableRow();

					objCell = new TableCell();
					objCell.Controls.Add(new LiteralControl(dr["FieldTitle"].ToString() + ":"));
					objCell.CssClass = "SubHead";
					objRow.Cells.Add(objCell);

					objCell = new TableCell();
					switch(dr["FieldType"].ToString())
					{
						case "String":
						{
							TextBox objTextBox = new TextBox();
							objTextBox.ID = dr["FieldTitle"].ToString();
							objTextBox.Columns = 50;
							objTextBox.Rows = 5;
							objTextBox.TextMode = TextBoxMode.MultiLine;
							objTextBox.CssClass = "NormalTextBox";
							objCell.Controls.Add(objTextBox);
						}
							break;
						case "File": case "Image":  
						{
							DropDownList imageList = new DropDownList();
							imageList.ID = dr["FieldTitle"].ToString();
							//add a default empty entry
							imageList.Items.Add(new ListItem("[---------------------------]",string.Empty));

						
							HtmlInputFile fileInputBox = new HtmlInputFile();
							fileInputBox.ID = dr["FieldTitle"].ToString()+ "_Upload";
							fileInputBox.Size = 30;


							string pathToFiles = string.Empty;
							string [] fileArray = new string[0];

							if (dr["FieldType"].ToString() =="Image")
							{
								// get the path to the files
								pathToFiles = Server.MapPath(((SettingItem) moduleSettings["ImagePath"]).FullPath) + "\\";
								// retrieving a list of files for the dropdownbox
								fileArray = Rainbow.Helpers.IOHelper.GetFiles(pathToFiles,"*.jpg;*.png;*.gif");

								//set the accept variable on the input element
								fileInputBox.Attributes.Add("accept","image/*");
							}
							else
							{
								// get the path to the files
								pathToFiles = Server.MapPath(((SettingItem) moduleSettings["DocumentPath"]).FullPath) + "\\";
								// retrieving a list of files for the dropdownbox
								fileArray = Rainbow.Helpers.IOHelper.GetFiles(pathToFiles,"*.*");
							}

							//now fill the dropdown box
							foreach (string entry in fileArray )
								imageList.Items.Add(entry.Substring(entry.LastIndexOf("\\") + 1 ));
							imageList.DataBind();
						
							imageList.Attributes.Add("onChange",dr["FieldTitle"].ToString()+ "_Upload.value='';");
							objCell.Controls.Add(imageList);
						
							objCell.Controls.Add(new LiteralControl ("&nbsp;"));

							fileInputBox.Attributes.Add("onChange",dr["FieldTitle"].ToString()+".selectedIndex=0;");
							objCell.Controls.Add(fileInputBox);
						}
							break;
						default:
						{
							TextBox objTextBox = new TextBox();
							objTextBox.ID = dr["FieldTitle"].ToString();
							objTextBox.Columns = 50;
							objTextBox.CssClass = "NormalTextBox";
							objCell.Controls.Add(objTextBox);
						}
							break;
					}


					objRow.Cells.Add(objCell);

					tblFields.Rows.Add(objRow);
				}
			}
			finally
			{
				dr.Close();
			}
		}

		/// <summary>
		/// The UpdateBtn_Click event handler on this Page is used to either
		/// create or update a row. It uses the Rainbow.UserDefinedTableDB()
		/// data component to encapsulate all data functionality.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnUpdate(EventArgs e) 
		{
			// Calling base we check if the user has rights on updating
			base.OnUpdate(e);

            UserDefinedTableDB  objUserDefinedTable = new UserDefinedTableDB();
            bool ValidInput = true;
            string strMessage = string.Empty;

            SqlDataReader dr = objUserDefinedTable.GetUserDefinedFields(ModuleID);
			try
			{
				while (dr.Read())
				{
					//if ( Request.Form[prefix + dr["FieldTitle"]] != null && Request.Form[prefix + dr["FieldTitle"]].ToString() != string.Empty )
					if ( Request.Form[prefix + dr["FieldTitle"]] != null)
					{
						switch (dr["FieldType"].ToString())
						{
							case "Int32":
								try 
								{
									int obj = int.Parse(Request.Form[prefix + dr["FieldTitle"].ToString()]);
								} 
								catch 
								{
									strMessage += "<br>" + dr["FieldTitle"].ToString() + " "+ Esperantus.Localize.GetString("UDT_VALIDINTEGER", "must contain a valid integer value");
									ValidInput = false;
								}
								break;
							case "Decimal":
								try 
								{
									Decimal obj = Decimal.Parse(Request.Form[prefix + dr["FieldTitle"].ToString()]);
								} 
								catch 
								{
									strMessage += "<br>" + dr["FieldTitle"].ToString() + " "+ Esperantus.Localize.GetString("UDT_VALIDDECIMAL", "must contain a valid decimal value");
									ValidInput = false;
								}
								break;
							case "DateTime":
								try 
								{
									DateTime obj = DateTime.Parse(Request.Form[prefix + dr["FieldTitle"].ToString()]);
								} 
								catch 
								{
									strMessage += "<br>" + dr["FieldTitle"].ToString() + " "+ Esperantus.Localize.GetString("UDT_VALIDDATE", "must contain a valid date value");
									ValidInput = false;
								}
								break;
							case "Boolean":
								try 
								{
									bool obj = bool.Parse(Request.Form[prefix + dr["FieldTitle"].ToString()]);
								} 
								catch 
								{
									strMessage += "<br>" + dr["FieldTitle"].ToString() + " "+ Esperantus.Localize.GetString("UDT_VALIDBOOLEAN", "must contain a valid true/false value");
									ValidInput = false;
								}
								break;
						}
					}
				}
			}
			finally
			{
				dr.Close();
			}

            if ( ValidInput )
			{
                if ( UserDefinedRowID == -1 )
                    UserDefinedRowID = objUserDefinedTable.AddUserDefinedRow(ModuleID, out UserDefinedRowID);

                dr = objUserDefinedTable.GetUserDefinedFields(ModuleID);
				try
				{
					while (dr.Read())
					{
						string fieldValue = Request.Form[prefix + dr["FieldTitle"].ToString()];

						if(dr["FieldType"].ToString() == "File" || dr["FieldType"].ToString() == "Image")
						{
							HtmlInputFile fileControl = (HtmlInputFile)Page.FindControl(prefix + dr["FieldTitle"].ToString()+ "_Upload");
							if (fileControl.PostedFile.ContentLength > 0 )
							{
								fieldValue = fileControl.PostedFile.FileName.Substring(fileControl.PostedFile.FileName.LastIndexOf("\\") + 1);

								string pathToSave=string.Empty ;

								if (dr["FieldType"].ToString() == "Image")
									pathToSave = Server.MapPath(((SettingItem) moduleSettings["ImagePath"]).FullPath) + "\\";
								else
									pathToSave = Server.MapPath(((SettingItem) moduleSettings["DocumentPath"]).FullPath) + "\\";

								try
								{
									fileControl.PostedFile.SaveAs(pathToSave + fieldValue);
								}
								catch(System.IO.DirectoryNotFoundException ex)
								{
									// If the directory is not found, create and then save
									System.IO.Directory.CreateDirectory(pathToSave);
									//System.IO.File.Delete(pathToSave + fieldValue);
									fileControl.PostedFile.SaveAs(pathToSave + fieldValue);

									//This line is here to supress the warning
									ex.ToString();
								}
							}
						}

						objUserDefinedTable.UpdateUserDefinedData(UserDefinedRowID, int.Parse(dr["UserDefinedFieldID"].ToString()), fieldValue);
					}
				}
				finally
				{
					dr.Close();
				}

                objUserDefinedTable.UpdateUserDefinedRow(UserDefinedRowID);

				// Redirect back to the portal home page
				this.RedirectBackToReferringPage();
			} 
			else 
			{
                lblMessage.Text = strMessage;
            }

        }


 		/// <summary>
		/// The DeleteBtn_Click event handler on this Page is used to delete
		/// a row. It uses the Rainbow.UserDefinedTableDB() data component to
		/// encapsulate all data functionality.
		/// </summary>
		/// <param name="e"></param>
		override protected void OnDelete(EventArgs e) 
		{
			// Calling base we check if the user has rights on deleting
			base.OnUpdate(e);
			
			if (UserDefinedRowID != -1)
			{
				UserDefinedTableDB  objUserDefinedTable = new UserDefinedTableDB();
	            objUserDefinedTable.DeleteUserDefinedRow(UserDefinedRowID);
			}

            // Redirect back to the portal home page
			this.RedirectBackToReferringPage();
        }


		#region Web Form Designer generated code
        /// <summary>
        /// Raises OnInitEvent
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


    }

}