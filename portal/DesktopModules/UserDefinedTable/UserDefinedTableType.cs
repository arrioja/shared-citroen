using System;
using Esperantus;

namespace Rainbow.DesktopModules
{
	/// <summary>
	/// Class for storing a collection of table types
	/// </summary>
	public class UserDefinedTableType
	{
		private string typeText;
		private string typeValue;
		
		public string TypeText
		{
			get
			{
				return typeText;
			}
			set
			{
				typeText = value;
			}
		}

		public string TypeValue
		{
			get
			{
				return typeValue;
			}
			set
			{
				typeValue = value;
			}
		}
	}
}
