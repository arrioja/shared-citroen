using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Xml;

using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

using Rainbow.UI;
using Rainbow.UI.DataTypes;
using Rainbow.UI.WebControls;
using Rainbow.Configuration;
using Esperantus;

using System.Diagnostics;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// Summary description for UserDefinedTableXSLctl.
	/// </summary>
	[DefaultProperty("Text"), ToolboxData("<{0}:UserDefinedTableXSLctl runat=server></{0}:UserDefinedTableXSLctl>")]
	public class UserDefinedTableXML : System.Web.UI.WebControls.Xml, IPostBackEventHandler 
	{
		private DataView dv;
		private string sortField;
		private string sortOrder;
		private bool isEditable;
		private int tabID;
		private int moduleID;
		private int showdetailID = 0;
	
		#region Constructors
		public UserDefinedTableXML(DataView XMLdataview, int TabID, int ModuleID, bool IsEditable, string SortField, string SortOrder)
		{
			dv=XMLdataview;
			tabID=TabID;
			moduleID=ModuleID;
			isEditable=IsEditable;
			sortField=SortField;
			sortOrder=SortOrder;
		}
		#endregion

		#region Control creation

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{
			// *** Write it back to the server
			output.Write(RenderedXML());
		}

		private string RenderedXML()
		{
			base.Document=this.XmlData();

			// *** Write the HTML into this string builder
			StringBuilder sb = new StringBuilder();
			System.IO.StringWriter sw = new System.IO.StringWriter(sb);
 
			HtmlTextWriter hWriter = new HtmlTextWriter(sw);
			base.Render(hWriter);
 
			// *** insert Sorting links (if minimal one is used in output)
			if(sb.ToString().IndexOf("@@sort.")>-1)
			{
				foreach (DataColumn fldname in dv.Table.Columns)
				{
					if(fldname.ColumnName==this.sortField)
					{
						if(SortOrder=="ASC")
						{
							sb.Replace("@@sort."+this.SortField+"@@",GetSortingUrl(this.SortField,"DESC"));
							sb.Replace("@@imgsortorder."+this.SortField+"@@",GetSortOrderImg("ASC"));
						}
						else
						{
							sb.Replace("@@sort."+this.SortField+"@@",GetSortingUrl(this.SortField,"ASC"));
							sb.Replace("@@imgsortorder."+this.SortField+"@@",GetSortOrderImg("DESC"));
						}
					}
					else
					{
						sb.Replace("@@sort."+fldname.ColumnName+"@@",GetSortingUrl(fldname.ColumnName,"ASC"));
						sb.Replace("@@imgsortorder."+fldname.ColumnName+"@@",string.Empty);
					}
				}
			}

			// *** insert ShowDetail links 
			int cmdPos;
			cmdPos = sb.ToString().IndexOf("@@ShowDetail");
			while (cmdPos>-1)
			{
				string s = sb.ToString().Substring(cmdPos + 12);
				int p2 = s.IndexOf("@");
				s = s.Substring(0,p2);
				int idnr = int.Parse(s);
				sb.Replace("@@ShowDetail"+idnr.ToString()+"@@",GetShowDetailUrl(idnr));
				cmdPos = sb.ToString().IndexOf("@@ShowDetail");
			}

			// *** Localize
			//
			cmdPos = sb.ToString().ToUpper().IndexOf("@@LOCALIZE");
			while (cmdPos>-1)
			{
				string s = sb.ToString().Substring(cmdPos + 11);
				int p2 = s.IndexOf("@");
				s = s.Substring(0,p2);
				string lkey = s.ToUpper();
				string srepl = sb.ToString().Substring(cmdPos,11+p2+2);
				sb.Replace(srepl,Esperantus.Localize.GetString(lkey));
				cmdPos = sb.ToString().ToUpper().IndexOf("@@LOCALIZE");
			}

			// *** SortOrder images
			//
//			cmdPos = sb.ToString().ToUpper().IndexOf("@@IMGSORTORDER");
//			while (cmdPos>-1)
//			{
//				string s = sb.ToString().Substring(cmdPos + 15);
//				int p2 = s.IndexOf("@");
//				s = s.Substring(0,p2);
//				string lkey = s.ToUpper();
//				string srepl = sb.ToString().Substring(cmdPos,15+p2+2);
//				sb.Replace(srepl,Esperantus.Localize.GetString(lkey));
//				cmdPos = sb.ToString().ToUpper().IndexOf("@@IMGSORTORDER");
//			}
			//

			return sb.ToString(); 

		}

		#endregion

		#region Events and delegates

		
		/// <summary>
		/// Implement the RaisePostBackEvent method from the IPostBackEventHandler interface. 
		/// Define the method of IPostBackEventHandler that raises change events.
		/// To capture Sorting request issued from XML/XSL data
		/// </summary>
		/// <param name="eventArgument"></param>
		public void RaisePostBackEvent(string eventArgument)
		{
			string[] strEvent = eventArgument.Split('|');
			if(strEvent[0]=="Sort")
			{
				UDTXSLSortEventArgs newEvent = new UDTXSLSortEventArgs(strEvent[1],strEvent[2]);
				OnSort(newEvent);
			}
			else if(strEvent[0]=="ShowDetail")
			{
				this.showdetailID = int.Parse(strEvent[1]);
				//UDTXSLShowDetailEventArgs newEvent = new UDTXSLShowDetailEventArgs(strEvent[1]);
				//OnShowDetail(newEvent);
			}
		}

		/// <summary>
		/// The Sort event is defined using the event keyword.
		/// </summary>
		public event UDTXSLSortEventHandler SortCommand;

		/// <summary>
		/// Calls Sort Delegate 
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnSort(UDTXSLSortEventArgs e)
		{
			if (SortCommand != null)
				SortCommand(this, e); //Invokes the delegates
		}



		#endregion

		#region Private Implementation


		private XmlDocument XmlData()
		{
			// sort data view
			if(this.sortField != string.Empty) 
			{
				dv.Sort = SortField + " " +SortOrder;
			}
			XmlDocument xmlDocument = new XmlDocument();

			XmlNode rootElement = xmlDocument.CreateElement("UserDefinedTable");

			//Add root Attributes 
				//ModuleID
			XmlAttribute newAttribute;
			newAttribute = xmlDocument.CreateAttribute("ModuleID");
			newAttribute.Value = ModuleID.ToString();
			rootElement.Attributes.Append(newAttribute);
				//ShowDetail ID
			newAttribute = xmlDocument.CreateAttribute("ShowDetail");
			newAttribute.Value = this.showdetailID.ToString();
			rootElement.Attributes.Append(newAttribute);
				//Language
			newAttribute = xmlDocument.CreateAttribute("xml:lang");
			newAttribute.Value = Esperantus.Localize.GetCurrentNeutralCultureName();
			rootElement.Attributes.Append(newAttribute);
			newAttribute=null;

			xmlDocument.AppendChild(rootElement);

			IEnumerator iterator = dv.GetEnumerator();
			DataRowView drv;
			int i= 0;
			while(iterator.MoveNext())
			{      
				drv = (DataRowView)iterator.Current;
				i++;
				if(showdetailID == 0  || showdetailID.ToString()==drv["UserDefinedRowID"].ToString()) //Don't bother to make xml data for ever record if we only want one 
				{
					XmlNode rowNode = xmlDocument.CreateElement("Row");

					XmlAttribute rowIDAttribute = xmlDocument.CreateAttribute("ID");
					rowIDAttribute.Value = drv["UserDefinedRowID"].ToString();
					rowNode.Attributes.Append(rowIDAttribute);

					foreach(DataColumn dc in dv.Table.Columns)
					{
						if(dc.ColumnName != "UserDefinedRowID")
						{
							XmlNode fieldNode = xmlDocument.CreateElement(dc.ColumnName);
							fieldNode.InnerText = drv[dc.ColumnName].ToString();
							rowNode.AppendChild(fieldNode);					
						}
					}

					XmlNode extraNode;
					//Rob Siera - 04 nov 2004 - Add EditURL to XML output
					extraNode = xmlDocument.CreateElement("EditURL");
					if(IsEditable)
					{
						extraNode.InnerText = HttpUrlBuilder.BuildUrl("~/DesktopModules/UserDefinedTable/UserDefinedTableEdit.aspx", tabID, "&mID=" + ModuleID + "&UserDefinedRowID=" + drv["UserDefinedRowID"].ToString()); 
					}		
					else
					{
						extraNode.InnerText = string.Empty;
					}
					rowNode.AppendChild(extraNode);

					//Rob Siera - 11 dec 2004 - Add ShowDetailURL to XML output
					extraNode = xmlDocument.CreateElement("ShowDetailURL");
					extraNode.InnerText = "@@ShowDetail" + drv["UserDefinedRowID"].ToString() + "@@"; 
					rowNode.AppendChild(extraNode);
	
					rootElement.AppendChild(rowNode);
				}
			}
			return xmlDocument;
		}



		/// <summary>
		/// Sort link creator
		/// </summary>
		/// <returns></returns>
		private string GetSortingUrl(string field, string order)
		{
			return "javascript:" + Page.GetPostBackEventReference(this, "Sort|" + field + "|" + order);
		}

		/// <summary>
		/// ShowDetail link creator
		/// </summary>
		/// <returns></returns>
		private string GetShowDetailUrl(int idnr)
		{
			return "javascript:" + Page.GetPostBackEventReference(this, "ShowDetail|" + idnr.ToString());
		}

		/// <summary>
		/// ShowDetail link creator
		/// </summary>
		/// <returns></returns>
		private string GetSortOrderImg(string order)
		{
			string s ;
			if(order=="DESC")
			{
				s = @"<img src='" + Rainbow.Settings.Path.WebPathCombine(Rainbow.Settings.Path.ApplicationRoot,"DesktopModules/UserDefinedTable/sortdescending.gif") + "' width='10' height='9' border='0'>";
			}
			else
			{
				s = @"<img src='" + Rainbow.Settings.Path.WebPathCombine(Rainbow.Settings.Path.ApplicationRoot,"DesktopModules/UserDefinedTable/sortascending.gif") + "' width='10' height='9' border='0'>";
			}
			return s;
		}

		#endregion

		#region Properties

		public string InnerXml
		{
				get
			{
				base.Document=this.XmlData();
				return base.Document.InnerXml;
			}
		}

		public DataView UDTdata
		{
			get
			{
				return this.dv;
			}
		}
		public string SortField
		{
			get
			{
				return this.sortField;
			}
			set
			{
				this.sortField=value;
			}		
		} 
		public string SortOrder
		{
			get
			{
				return this.sortOrder;
			}
			set
			{
				this.sortOrder=value;
			}		
		} 
		public bool IsEditable
		{
			get
			{
				return this.isEditable;
			}
		} 
		public int ModuleID
		{
			get
			{
				return this.moduleID;
			}
		} 

		#endregion

	}


	/// <summary>
	/// UDTXSLSortEventHandler
	/// </summary>
	public delegate void UDTXSLSortEventHandler(object sender, UDTXSLSortEventArgs e);
	public class UDTXSLSortEventArgs : EventArgs
	{
		private string sortField;
		private string sortOrder;

		public UDTXSLSortEventArgs(string sortField, string sortOrder) : base()
		{
			this.sortField = sortField;
			this.sortOrder = sortOrder;
		}
 
		public string SortField
		{
			get
			{
				return this.sortField;
			}	
		} 
		public string SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		} 
	}
}
