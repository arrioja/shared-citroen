using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.OleDb;

using Rainbow; 
using Rainbow.UI; 
using Rainbow.UI.WebControls; 
using Rainbow.UI.DataTypes; 
using Rainbow.Security; 
using Rainbow.Configuration; 
using Esperantus; 
using Rainbow.DesktopModules; 


namespace Rainbow.DesktopModules 
{

	/// <summary>
	/// DatabaseTool Module
	/// Based on VB code Written by Sreedhar Koganti (w3coder)
	/// Modifications (lots!) and conversion for Rainbow by Jakob hansen
	/// </summary>
	public class DatabaseTool : PortalModuleControl 
	{
		protected System.Web.UI.WebControls.Panel panConnected;
		protected System.Web.UI.WebControls.Label lblConnectedError;

		protected System.Web.UI.WebControls.TextBox tbUserName;
		protected System.Web.UI.WebControls.DropDownList ddObjectSelectList;
		protected System.Web.UI.WebControls.ListBox lbObjects;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Panel panQueryBox;
		protected System.Web.UI.WebControls.TextBox txtQueryBox;
		protected System.Web.UI.WebControls.Label lblRes;

		protected bool Trusted_Connection;
		protected string ServerName;
		protected string DatabaseName;
		protected string UserID;
		protected string Password;

		protected string InfoFields;
		protected string InfoExtendedFields;
		protected bool ShowQueryBox;
		protected int QueryBoxHeight;

		protected bool Connected = false;
		// Added EsperantusKeys for Localization 
		// Mario Endara mario@softworks.com.uy june-1-2004 
		protected Esperantus.WebControls.Label Label1;
		protected Esperantus.WebControls.Button btnGetObjectProps;
		protected Esperantus.WebControls.Button btnGetObjectInfo;
		protected Esperantus.WebControls.Button btnGetObjectInfoExtended;
		protected Esperantus.WebControls.Button btnGetObjectData;
		protected Esperantus.WebControls.Button btnQueryExecute;
		protected string ConnectionString;


		private void Page_Load(object sender, System.EventArgs e)
		{

			Trusted_Connection = "True" == this.Settings["Trusted Connection"].ToString();
			ServerName =  this.Settings["ServerName"].ToString();
			DatabaseName = this.Settings["DatabaseName"].ToString();
			UserID = this.Settings["UserID"].ToString();
			Password = this.Settings["Password"].ToString();

			if (Trusted_Connection) 
				ConnectionString = "Server=" + ServerName + ";Trusted_Connection=true;database=" + DatabaseName;
			else
				ConnectionString = "Server=" + ServerName + ";database=" + DatabaseName + ";uid=" + UserID + ";pwd=" + Password + ";";

			Connected = Connect(lblRes);
			
			panConnected.Visible = Connected;
			if (Connected)
			{
				lblConnectedError.Visible = false;

				InfoFields = this.Settings["InfoFields"].ToString();
				InfoExtendedFields = this.Settings["InfoExtendedFields"].ToString();

				ShowQueryBox = "True" == this.Settings["Show Query Box"].ToString();
				QueryBoxHeight = int.Parse(this.Settings["Query Box Height"].ToString());
				panQueryBox.Visible = ShowQueryBox;
				txtQueryBox.Height = QueryBoxHeight;

				if (!Page.IsPostBack)
					FillObjects("U", "dbo");   // The User table is the initial selected object
			}
			else
			{
				lblConnectedError.Visible = true;
				lblConnectedError.Text = "Please connect to a database... (check settings)";
			}

		}


		protected bool Connect(System.Web.UI.WebControls.Label lbl)
		{
			lbl.Text = string.Empty;
			bool retValue;
			try 
			{
				SqlConnection SqlCon = new SqlConnection(ConnectionString);
				SqlDataAdapter DA = new SqlDataAdapter("SELECT NULL", SqlCon);
				SqlCon.Open();

				SqlCon.Close();
				SqlCon.Dispose();
				retValue = true;
			} 
			catch (Exception ex) 
			{
				lbl.Text = "Error: " + ex.Message;
				retValue = false;
			}
			return retValue;
		}

		
		protected void FillObjects(string xtype, string user)
		{
			lblRes.Text = string.Empty;
			try 
			{
				SqlConnection SqlCon = new SqlConnection(ConnectionString);
				SqlDataAdapter DA = new SqlDataAdapter("Select name,id from sysobjects where uid=USER_ID('" + user + "') AND xtype='" + xtype + "' order by name", SqlCon);
				SqlCon.Open();

				DataSet DS = new DataSet();
				try
				{
					DA.Fill(DS, "Table");
					lbObjects.DataSource = DS;
					lbObjects.DataTextField = "name";
					lbObjects.DataValueField = "id";
					lbObjects.DataBind();
					lbObjects.SelectedIndex = 0;
				}
				finally
				{
					SqlCon.Close();
				}
			} 
			catch (Exception ex) 
			{
				lblRes.Text = "Error: " + ex.Message;
			}
		}


		protected void FillDataGrid(string SQL)
		{
			lblRes.Text = string.Empty;
			try 
			{
				SqlConnection SqlCon = new SqlConnection(ConnectionString);
				SqlDataAdapter DA = new SqlDataAdapter(SQL, SqlCon);
				SqlCon.Open();

				DataSet DS = new DataSet();
				try
				{
					DA.Fill(DS, "Table");
					DataGrid1.DataSource = DS;
					DataGrid1.DataBind();
				}
				finally
				{
					SqlCon.Close();
				}
			} 
			catch (Exception ex)
			{
				lblRes.Text = "Error: " + ex.Message;
			}
		}


		protected void GetTableField(string selectCmd, int idxField)
		{
			lblRes.Text = string.Empty;
			try 
			{
				SqlConnection SqlCon = new SqlConnection(ConnectionString);
				SqlCommand SqlComm = new SqlCommand(selectCmd, SqlCon);

				SqlComm.Connection.Open();
				try
				{
					SqlDataReader dr = SqlComm.ExecuteReader(CommandBehavior.CloseConnection); 
					try
					{
						if (dr.Read())
							txtQueryBox.Text = dr[idxField].ToString(); 
						else
							txtQueryBox.Text = "No data for SQL: \n" + selectCmd;
					}
					finally
					{
						dr.Close(); //by Manu, fixed bug 807858
					}
				}
				finally
				{
					SqlCon.Close();
				}
			} 
			catch (Exception ex)
			{
				lblRes.Text = "Error: " + ex.Message;
			}
		}

		protected void ObjectSelectList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// There are only data in tables or views:
			if (ddObjectSelectList.SelectedItem.Value == "U" || 
				ddObjectSelectList.SelectedItem.Value == "V")
				btnGetObjectData.Visible = true;
			else
				btnGetObjectData.Visible = false;

			FillObjects(ddObjectSelectList.SelectedItem.Value, tbUserName.Text);
		}


		protected void GetObjectInfo_Click(object sender, System.EventArgs e)
		{
			FillDataGrid("SELECT " + InfoFields + " FROM sysobjects WHERE uid=USER_ID('" + tbUserName.Text + "') AND id=" + lbObjects.SelectedItem.Value.ToString());
		}
		
		
		protected void GetObjectInfoExtended_Click(object sender, System.EventArgs e)
		{
			FillDataGrid("SELECT " + InfoExtendedFields + " FROM sysobjects WHERE uid=USER_ID('" + tbUserName.Text + "') AND id=" + lbObjects.SelectedItem.Value.ToString());
		}
		
		
		protected void GetObjectProps_Click(object sender, System.EventArgs e)
		{
			string SQL = string.Empty;
			if (ddObjectSelectList.SelectedItem.Value == "U" ||
			    ddObjectSelectList.SelectedItem.Value == "V")
			{
				SQL += "EXEC sp_columns";
				SQL += " @table_name = '" + lbObjects.SelectedItem.Text.ToString()+ "'";
				SQL += ",@table_owner = '" + tbUserName.Text + "'";
				FillDataGrid(SQL);
			}
			else
			{
				SQL += " SELECT c.[text] FROM sysobjects o, syscomments c";
				SQL += " WHERE o.uid=USER_ID('" + tbUserName.Text + "')";
				SQL += " AND o.id=c.id";
				SQL += " AND o.id=" + lbObjects.SelectedItem.Value.ToString();

				GetTableField(SQL, 0);
			}
		}
		
		
		protected void GetObjectData_Click(object sender, System.EventArgs e)
		{
			FillDataGrid("SELECT * FROM " + lbObjects.SelectedItem.Text.Trim());
		}
 

		protected void QueryExecute_Click(object sender, System.EventArgs e)
		{
			lblRes.Text = string.Empty;
			try
			{
				SqlConnection SqlCon = new SqlConnection(ConnectionString);

				string SQL = txtQueryBox.Text.Trim();
				if (SQL.Length > 6 && SQL.Substring(0, 6).ToUpper() == "SELECT" ) 
				{
					SqlDataAdapter DA = new SqlDataAdapter(SQL, SqlCon);
					SqlCon.Open();

					DataSet DS = new DataSet();
					try
					{
						DA.Fill(DS, "Table");
						DataGrid1.DataSource = DS;
						DataGrid1.DataBind();
						lblRes.Text = "Successful Query...";
					}
					finally
					{
						SqlCon.Close();
					}
				} 
				else 
				{
					SqlCommand SqlComm = new SqlCommand(SQL, SqlCon);
					SqlCon.Open();

					try
					{
						int Rowseff = SqlComm.ExecuteNonQuery();
						lblRes.Text = "Effected Rows: " + Rowseff.ToString();
					}
					finally
					{
						SqlCon.Close();
					}
				}
			} 
			catch (Exception ex)
			{
				lblRes.Text = "Error: " + ex.Message;
			}
		}


		public override Guid GuidID 
		{
			get
			{
				return new Guid("{2502DB18-B580-4F90-8CB4-C15E6E531032}");
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

		/// <summary>
		/// Public constructor. Sets base settings for module.
		/// </summary>
		public DatabaseTool() 
		{
			SettingItem Trusted_Connection = new SettingItem(new BooleanDataType());
			Trusted_Connection.Order = 1;
			//Trusted_Connection.Required = true;   // hmmm... problem here! Dont set to true!" 
			Trusted_Connection.Value = "True";
			this._baseSettings.Add("Trusted Connection", Trusted_Connection);

			SettingItem ServerName = new SettingItem(new StringDataType());
			ServerName.Order = 2;
			ServerName.Required = true;
			ServerName.Value = "localhost";
			this._baseSettings.Add("ServerName", ServerName);

			SettingItem DatabaseName = new SettingItem(new StringDataType());
			DatabaseName.Order = 3;
			DatabaseName.Required = true;
			DatabaseName.Value = "Rainbow";
			this._baseSettings.Add("DatabaseName", DatabaseName);

			SettingItem UserID = new SettingItem(new StringDataType());
			UserID.Order = 4;
			UserID.Required = false;
			UserID.Value = string.Empty;
			this._baseSettings.Add("UserID", UserID);

			SettingItem Password = new SettingItem(new StringDataType());
			Password.Order = 5;
			Password.Required = false;
			Password.Value = string.Empty;
			this._baseSettings.Add("Password", Password);

			SettingItem InfoFields = new SettingItem(new StringDataType());
			InfoFields.Order = 6;
			InfoFields.Required = true;
			InfoFields.Value = "name,id,xtype,uid";   // for table sysobjects
			this._baseSettings.Add("InfoFields", InfoFields);

			SettingItem InfoExtendedFields = new SettingItem(new StringDataType());
			InfoExtendedFields.Order = 7;
			InfoExtendedFields.Required = true;
			InfoExtendedFields.Value = "*";   // for table sysobjects
			this._baseSettings.Add("InfoExtendedFields", InfoExtendedFields);

			SettingItem ShowQueryBox = new SettingItem(new BooleanDataType());
			ShowQueryBox.Order = 8;
			//ShowQueryBox.Required = true;   // hmmm... problem here! Dont set to true!" 
			ShowQueryBox.Value = "True";
			this._baseSettings.Add("Show Query Box", ShowQueryBox);

			SettingItem QueryBoxHeight = new SettingItem(new IntegerDataType());
			QueryBoxHeight.Order = 9;
			QueryBoxHeight.Required = true;
			QueryBoxHeight.Value = "150";
			QueryBoxHeight.MinValue = 10;
			QueryBoxHeight.MaxValue = 2000;
			this._baseSettings.Add("Query Box Height", QueryBoxHeight);
		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
//			ModuleTitle = new DesktopModuleTitle();
//			Controls.AddAt(0, ModuleTitle);
			base.OnInit(e);
		}
		
		/// <summary>
		///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ddObjectSelectList.SelectedIndexChanged += new System.EventHandler(this.ObjectSelectList_SelectedIndexChanged);
			this.btnGetObjectInfo.Click += new System.EventHandler(this.GetObjectInfo_Click);
			this.btnGetObjectInfoExtended.Click += new System.EventHandler(this.GetObjectInfoExtended_Click);
			this.btnGetObjectProps.Click += new System.EventHandler(this.GetObjectProps_Click);
			this.btnGetObjectData.Click += new System.EventHandler(this.GetObjectData_Click);
			this.btnQueryExecute.Click += new System.EventHandler(this.QueryExecute_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

	}
}
