using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using Esperantus;
using Rainbow.Security;
using Rainbow.Configuration;
using Rainbow.UI;
using Rainbow.UI.WebControls;
using Rainbow.Admin;

namespace Rainbow.DesktopModules
{
    /// <summary>
    /// Placeable Registration module
    /// </summary>
    public class Register : RegisterFull
    {
		public override Guid GuidID 
		{
			get
			{
				return new Guid("{09C7351B-C9A1-454e-953F-E17E6E6EF092}");
			}
		}
    }
}