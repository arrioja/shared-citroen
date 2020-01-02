using System;
using System.Collections;
using System.Configuration;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Data;
using System.Xml;
using System.Net;
using System.IO;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.UI.DataTypes;
using Rainbow.Configuration;
using Esperantus;

namespace AmazonFull
{
	public class Books : PortalModuleControl 
	{
		protected System.Web.UI.WebControls.DataList myDataList;

		public override bool Searchable
		{
			get
			{
				return false;
			}
		}
		
		public Books() 
		{
			SettingItem Columns = new SettingItem(new IntegerDataType());
			Columns.Required=true;
			Columns.Value = "3";
			Columns.MinValue=1;
			Columns.MaxValue=10;
			this._baseSettings.Add("Columns",Columns);

			SettingItem Width = new SettingItem(new IntegerDataType());
			Width.Value = "110";
			Width.MinValue = 50;
			Width.MaxValue = 250;
			this._baseSettings.Add("Width",Width);

			SettingItem PromoCode = new SettingItem(new StringDataType());
			if (ConfigurationSettings.AppSettings["AmazonPromoCode"] != null && ConfigurationSettings.AppSettings["AmazonPromoCode"].Length != 0)
				PromoCode.Value = ConfigurationSettings.AppSettings["AmazonPromoCode"].ToString();
			else 
				PromoCode.Value = string.Empty;
			this._baseSettings.Add("Promotion Code",PromoCode);

			SettingItem ShowDetails = new SettingItem(new StringDataType());
			ShowDetails.Value = "ProductName,OurPrice,Author";
			this._baseSettings.Add("Show Details",ShowDetails);

			SettingItem AmazonDevToken = new SettingItem(new StringDataType());
			if (ConfigurationSettings.AppSettings["AmazonDevToken"] != null && ConfigurationSettings.AppSettings["AmazonDevToken"].Length != 0)
				AmazonDevToken.Value = ConfigurationSettings.AppSettings["AmazonDevToken"].ToString();
			else 
				AmazonDevToken.Value = string.Empty;
			this._baseSettings.Add("Amazon Dev Token", AmazonDevToken);

			//Choose your editor here
			SupportsWorkflow = false;
		}

		private void Page_Load(object sender, System.EventArgs e) 
		{
			BooksDB books = new BooksDB();

			myDataList.DataSource = books.Getrb_BookList(ModuleID);
			myDataList.DataBind();

			myDataList.RepeatColumns = Int32.Parse(Settings["Columns"].ToString());
		}

		public string GetTdWidthPercentage(string Columns)
		{
			//Trace.Write("AmazonFullCaption","GetTdWidthPercentage()");
			int tdWidthPercent;
			try
			{
				tdWidthPercent = 100/Int32.Parse(Columns);
				return tdWidthPercent+"%";
			}
			catch(Exception)
			{
				return string.Empty;
			}
		}

		public string GetWebServiceDetails(string AmazonDevToken, string isbn, string ShowDetails, string PromoCode )
		{
			string BookDetails=string.Empty;
			if(ShowDetails.Length>0)
			{
				string strAmazonURL = "http://xml.amazon.com/onca/xml2?" +
					"t=" + PromoCode + 
					"&dev-t=" + AmazonDevToken +
					"&type=heavy&AsinSearch="+isbn+
					"&f=xml";

				XmlTextReader xmlTr1;
				xmlTr1= XmlReaderCached_v2(strAmazonURL);

				BookDetails = "<br>";
				ShowDetails="," + ShowDetails + ",";
				
				try 
				{
					//Trace.Write("AmazonFullCaption","GetTdWidthPercentage.If.Try()");
					while(xmlTr1.Read())
					{
						//Trace.Write("AmazonFullCaption","GetTdWidthPercentage.If.Try.While()");
						if(xmlTr1.NodeType.ToString()=="Element")
						{	
							//Trace.Write("AmazonFullCaption","GetTdWidthPercentage.If.Try.While.If()");
							string strName=xmlTr1.Name;
							if ( ( ShowDetails.IndexOf(","+strName+",") > -1) || ( ShowDetails.ToLower()==",all,") )
							{	
								//Trace.Write("AmazonFullCaption","GetTdWidthPercentage.If.Try.While.If.If()");
								xmlTr1.Read();
								BookDetails += strName + "&nbsp;&nbsp;<b>" + xmlTr1.Value + "</b><br>";
							}//END IF
						}//END IF
					}//END WHILE	
				} 
				catch 
				{
					//Trace.Write("AmazonFullCaption","GetTdWidthPercentage.If.Try.Catch()");
				}
				
				
			}//END IF
			//Trace.Write("AmazonFullCaption","GetWebServiceDetails.End()");
			return BookDetails;
		}//END FUNCTION

		public byte[] ConvertStr2ByteArray(string strInput) 
		{
			//Trace.Write("AmazonFullCaption","ConvertStr2ByteArray.Begin()");

			int intCounter=0;
			char[] arrChar;

			arrChar = strInput.ToCharArray();
		
	    
			byte[] arrByte;
			arrByte = new byte[arrChar.Length-1];
				
			for(intCounter=0; intCounter<=arrByte.Length - 1; intCounter++)
			{
				arrByte[intCounter] = Convert.ToByte(arrChar[intCounter]);
			}

			//Trace.Write("AmazonFullCaption","ConvertStr2ByteArray.End()");
			return arrByte;
		}


		private string MD5checksum( string  strParm1)
		{
			//Trace.Write("AmazonFullCaption","MD5checksum.Begin()");
			byte[] arrHashInput;
			byte[] arrHashOutput;
			MD5CryptoServiceProvider objMD5 = new MD5CryptoServiceProvider();

			arrHashInput = ConvertStr2ByteArray(strParm1);
			arrHashOutput = objMD5.ComputeHash(arrHashInput);

			//Trace.Write("AmazonFullCaption","MD5checksum.End()");
			return BitConverter.ToString(arrHashOutput);
		}


//		private XmlTextReader XmlReaderCached_v1(string strXML ){
//			
//			XmlTextReader xmlTrCached = new XmlTextReader(strXML);
//			//Trace.Write("AmazonFullCaption","XmlReaderCached_v1()");
//			return(xmlTrCached);
//		}


		private XmlTextReader XmlReaderCached_v2(string strXML)
		{
			//Trace.Write("AmazonFullCaption","XmlReaderCached_v2.Begin()");

			XmlTextReader xmlTrCached; 
			string strChksum  = MD5checksum(strXML);
			string strInCache;

			if( Cache[strChksum] == null )
			{
				//Trace.Write("AmazonFullCaption","XmlReaderCached_v2.If()");
				//Trace.Write("cache miss",strXML);
				strInCache=HttpGet(strXML);
				if(strInCache=="error")
				{
					//Trace.Write("AmazonFullCaption","XmlReaderCached_v2.If.If()");
				}
				else
				{
					//Trace.Write("AmazonFullCaption","XmlReaderCached_v2.If.If.Else()");
					Cache.Insert(strChksum,strInCache,null,DateTime.Now.AddMinutes(60), System.Web.Caching.Cache.NoSlidingExpiration);
				}
			}
			else
			{
				//Trace.Write("AmazonFullCaption","XmlReaderCached_v2.If.Else()");
				//Trace.Write("cache hit",strXML);
				strInCache=Cache[strChksum].ToString();
			}

			StringReader strReader1 = new StringReader(strInCache);
			xmlTrCached=new XmlTextReader(strReader1);
			//Trace.Write("AmazonFullCaption","XmlReaderCached_v2.End()");
			return(xmlTrCached);
		}


		private string HttpGet(string strURL) {
			//Response.Write("HttpGet(" + strURL + ")<BR>");
			//Trace.Write("AmazonFullCaption","HttpGet.Begin()");

			StreamReader sr1=null;
			string strTemp;
			WebResponse webResponse1;
			WebRequest webRequest1;

			try {
				//Trace.Write("AmazonFullCaption","HttpGet.Try()");
				webRequest1 = WebRequest.Create(strURL);
				webResponse1 = webRequest1.GetResponse();
				sr1 = new StreamReader(webResponse1.GetResponseStream());
				strTemp=sr1.ReadToEnd();
			} catch {
				strTemp="error";
				//Trace.Write("AmazonFullCaption","HttpGet.Catch()");
			} finally {
				if(sr1!=null)
					sr1.Close();
				//Trace.Write("AmazonFullCaption","HttpGet.Finally()");
			}

			//Trace.Write("AmazonFullCaption","HttpGet.End()");
			return(strTemp);
		}


        public override Guid GuidID
		{
			get
			{
				return new Guid("{5A2E8E9C-B9C7-439a-BFF9-54CA78762818}");
			}			
		}

		public override void Install(System.Collections.IDictionary stateSaver)
		{
			string currentScriptName = System.IO.Path.Combine(Server.MapPath(TemplateSourceDirectory), "install.sql");

			ArrayList errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);

			if (errors.Count > 0)
			{
				throw new Exception("Error occurred:" + errors[0].ToString());
			}

		}

		public override void Uninstall(System.Collections.IDictionary stateSaver)
		{
			string currentScriptName = System.IO.Path.Combine(Server.MapPath(TemplateSourceDirectory), "uninstall.sql");

			ArrayList errors = Rainbow.Helpers.DBHelper.ExecuteScript(currentScriptName, true);

			if (errors.Count > 0)
			{
				throw new Exception("Error occurred:" + errors[0].ToString());
			}
		}


		#region Web Form Designer generated code
		/// <summary>
		///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();

			// Set here title properties
			// Add support for the edit page
			AddUrl = "~/DesktopModules/AmazonFull/BooksEdit.aspx";

			base.OnInit(e);
		}

		private void InitializeComponent() 
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
