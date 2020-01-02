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

using Rainbow.Configuration;
using Esperantus;


namespace Rainbow.DesktopModules 
{
	/// <summary>
	/// Users Defined Table module - Manage page part
	/// Written by: Shaun Walker (IbuySpy Workshop)
	/// Moved into Rainbow by Jakob Hansen, hansen3000@hotmail.com
	/// </summary>
	[History("Ender", "2003/03/18", "Added file and Xsl functionality")]
	public class UserDefinedTableManage : Rainbow.UI.EditItemPage
	{
		protected Esperantus.WebControls.LinkButton cmdAddField;
		protected System.Web.UI.WebControls.DataGrid grdFields;
		protected Esperantus.WebControls.LinkButton cmdCancel;
		protected Esperantus.WebControls.Label ManageTableLabel;


		/// <summary>
		/// The Page_Load event on this Page is used to ...
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e) 
		{
			if ( Page.IsPostBack == false )
			{				
				BindData();

				// Store URL Referrer to return to portal
				//ViewState("UrlReferrer") = Replace(Request.UrlReferrer.ToString(), "insertrow=true&", string.Empty);
				ViewState["UrlReferrer"] = Request.UrlReferrer.ToString().Replace("insertrow=true&", string.Empty);
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

		//private void cmdCancel_Click( object sender,  EventArgs e)  cmdCancel.Click, cmdCancel.Click 
		protected void cmdCancel_Click(object sender, EventArgs e)
		{
			// Redirect back to the portal home page
			this.RedirectBackToReferringPage();
		}

		
		protected void grdFields_CancelEdit(object sender, DataGridCommandEventArgs e)
		{
			grdFields.EditItemIndex = -1;
			BindData();
		}

		
		public void grdFields_Edit(object sender, DataGridCommandEventArgs e)
		{
			grdFields.EditItemIndex = e.Item.ItemIndex;
			grdFields.SelectedIndex = -1;
			BindData();
		}

		
		public void grdFields_Update(object sender, DataGridCommandEventArgs e)
		{
			CheckBox chkVisible = (CheckBox) e.Item.Cells[1].Controls[1];
			TextBox txtFieldTitle = (TextBox) e.Item.Cells[2].Controls[1];
			DropDownList cboFieldType = (DropDownList) e.Item.Cells[3].Controls[1];

			if ( txtFieldTitle.Text != string.Empty )
			{
				UserDefinedTableDB  objUserDefinedTable = new UserDefinedTableDB();

				if ( int.Parse(grdFields.DataKeys[e.Item.ItemIndex].ToString()) == -1 )
					objUserDefinedTable.AddUserDefinedField(ModuleID, txtFieldTitle.Text, chkVisible.Checked, cboFieldType.SelectedItem.Value);
				else
					objUserDefinedTable.UpdateUserDefinedField(int.Parse(grdFields.DataKeys[e.Item.ItemIndex].ToString()), txtFieldTitle.Text, chkVisible.Checked, cboFieldType.SelectedItem.Value);

				grdFields.EditItemIndex = -1;
				BindData();
			} 
			else 
			{
				grdFields.EditItemIndex = -1;
				BindData();
			}
		}

 
		public void grdFields_Delete(object sender, DataGridCommandEventArgs e)
		{
			UserDefinedTableDB  objUserDefinedTable = new UserDefinedTableDB();
			objUserDefinedTable.DeleteUserDefinedField(int.Parse(grdFields.DataKeys[e.Item.ItemIndex].ToString()));

			grdFields.EditItemIndex = -1;
			BindData();
		}

		
		public void grdFields_Move(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			UserDefinedTableDB  objUserDefinedTable = new UserDefinedTableDB();

			switch (e.CommandArgument.ToString())
			{
				case "Up":
					objUserDefinedTable.UpdateUserDefinedFieldOrder(int.Parse(grdFields.DataKeys[e.Item.ItemIndex].ToString()), -1);
					BindData();
					break;
				case "Down":
					objUserDefinedTable.UpdateUserDefinedFieldOrder(int.Parse(grdFields.DataKeys[e.Item.ItemIndex].ToString()), 1);
					BindData();
					break;
			}
		}


		protected void cmdAddField_Click(object sender,  System.EventArgs e)
		{
			grdFields.EditItemIndex = 0;
			BindData(true);
		}


		public DataSet ConvertDataReaderToDataSet( SqlDataReader reader)
		{

			DataSet dataSet = new DataSet();

			DataTable schemaTable = reader.GetSchemaTable();

			DataTable dataTable = new DataTable();

			int intCounter;

			for ( intCounter = 0 ; intCounter <= schemaTable.Rows.Count - 1; intCounter++)
			{
				DataRow dataRow = schemaTable.Rows[intCounter];
				string columnName = dataRow["ColumnName"].ToString();
				DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
				dataTable.Columns.Add(column);
			} 

			dataSet.Tables.Add(dataTable);

			try
			{
				while ( reader.Read())
				{
					DataRow dataRow = dataTable.NewRow();

					for (intCounter = 0; intCounter <= reader.FieldCount - 1; intCounter++)
						dataRow[intCounter] = reader.GetValue(intCounter);

					dataTable.Rows.Add(dataRow);
				}
			}
			finally
			{
				reader.Close(); //by Manu, fixed bug 807858
			}
			return dataSet;
		}


		protected void BindData()
		{
			BindData(false);
		}


		protected void BindData(bool blnInsertField)
		{
			UserDefinedTableDB  objUserDefinedTable = new UserDefinedTableDB();
			SqlDataReader dr = objUserDefinedTable.GetUserDefinedFields(ModuleID);

			DataSet ds;
			ds = ConvertDataReaderToDataSet(dr);

			// inserting a new field
			if ( blnInsertField )
			{
				DataRow row;
				row = ds.Tables[0].NewRow();
				row["UserDefinedFieldID"] = "-1";
				row["FieldTitle"] = string.Empty;
				row["Visible"] = true;
				row["FieldType"] = "String";
				ds.Tables[0].Rows.InsertAt(row, 0);
				grdFields.EditItemIndex = 0;
			}

			grdFields.DataSource = ds;
			grdFields.DataBind();
		}


		protected void grdFields_ItemCreated(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			Control cmdDeleteUserDefinedField = e.Item.FindControl("cmdDeleteUserDefinedField");

			if (cmdDeleteUserDefinedField != null )
			{
				ImageButton imgBut = (ImageButton) cmdDeleteUserDefinedField;
				imgBut.Attributes.Add("onClick", "javascript: return confirm('Are you sure you wish to delete this field?')");
			}
		}
	

		public string GetFieldTypeName(string strFieldType)
		{
			switch (strFieldType)
			{
				case "String" 	: return Esperantus.Localize.GetString("USERTABLE_TYPE_STRING", "Text",null);
				case "Int32" 	: return Esperantus.Localize.GetString("USERTABLE_TYPE_INT32", "Integer",null);
				case "Decimal" 	: return Esperantus.Localize.GetString("USERTABLE_TYPE_DECIMAL", "Decimal",null);
				case "DateTime" : return Esperantus.Localize.GetString("USERTABLE_TYPE_DATETIME", "Date",null);
				case "Boolean" 	: return Esperantus.Localize.GetString("USERTABLE_TYPE_BOOLEAN", "True/False",null);
				case "File" 	: return Esperantus.Localize.GetString("USERTABLE_TYPE_FILE", "File",null);
				case "Image" 	: return Esperantus.Localize.GetString("USERTABLE_TYPE_IMAGE", "Image",null);
				default			: return Esperantus.Localize.GetString("USERTABLE_TYPE_STRING", "Text",null);
			}
		}

		
		public int GetFieldTypeIndex(string strFieldType)
		{
			switch (strFieldType)
			{
				case "String" : return 0;
				case "Int32" : return 1;
				case "Decimal" : return 2;
				case "DateTime" : return 3;
				case "Boolean" : return 4;
				case "File" : return 5;
				case "Image" : return 6;
				default: return 0;
			}
		}

		public string IfVisible(object data, string trueStr, string falseStr)
		{
			bool check = bool.Parse(DataBinder.Eval(data, "Visible").ToString());
			return (check ? this.CurrentTheme.GetImage(trueStr, trueStr + ".gif").ImageUrl : 
							this.CurrentTheme.GetImage(falseStr, falseStr + ".gif").ImageUrl);
		}

		public UserDefinedTableType[] GetTableTypes()
		{
			UserDefinedTableType[] tableTypes = new UserDefinedTableType[7];
			tableTypes[0] = new UserDefinedTableType();
			tableTypes[0].TypeText = Esperantus.Localize.GetString("USERTABLE_TYPE_STRING", "Text",null);
			tableTypes[0].TypeValue = "String";			
			tableTypes[1] = new UserDefinedTableType();
			tableTypes[1].TypeText = Esperantus.Localize.GetString("USERTABLE_TYPE_INT32", "Integer",null);
			tableTypes[1].TypeValue = "Int32";	
			tableTypes[2] = new UserDefinedTableType();
			tableTypes[2].TypeText = Esperantus.Localize.GetString("USERTABLE_TYPE_DECIMAL", "Decimal",null);
			tableTypes[2].TypeValue = "Decimal";	
			tableTypes[3] = new UserDefinedTableType();
			tableTypes[3].TypeText = Esperantus.Localize.GetString("USERTABLE_TYPE_DATETIME", "Date",null);
			tableTypes[3].TypeValue = "DateTime";	
			tableTypes[4] = new UserDefinedTableType();
			tableTypes[4].TypeText = Esperantus.Localize.GetString("USERTABLE_TYPE_BOOLEAN", "True/False",null);
			tableTypes[4].TypeValue = "Boolean";	
			tableTypes[5] = new UserDefinedTableType();
			tableTypes[5].TypeText = Esperantus.Localize.GetString("USERTABLE_TYPE_FILE", "File",null);
			tableTypes[5].TypeValue = "File";	
			tableTypes[6] = new UserDefinedTableType();
			tableTypes[6].TypeText = Esperantus.Localize.GetString("USERTABLE_TYPE_IMAGE", "Image",null);
			tableTypes[6].TypeValue = "Image";	
			return tableTypes;
		}

		#region Web Form Designer generated code
		/// <summary>
		/// Raises OnInitEvent
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


    }

}