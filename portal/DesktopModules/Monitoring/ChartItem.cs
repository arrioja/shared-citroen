using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;

namespace RainbowCharts
{
	//*********************************************************************
	//
	// ChartItem Class
	//
	// This class represents a data point in a chart
	//
	//*********************************************************************

	/// <summary>
	/// One of the Graphing classes from the reporting asp.net starter kit
	/// on www.asp.net - http://www.asp.net/Default.aspx?tabindex=9&amp;tabID=47
	/// Made very minor changes to the code to use with monitoring module.
	/// Imported by Paul Yarrow, paul@paulyarrow.com
	/// </summary>
	public class ChartItem 
	{
		private string _label;
		private string _description;
		private float _value;
		private Color _color;
		private float _startPos;
		private float _sweepSize;

		private ChartItem()	{}
		
		public ChartItem(string label, string desc, float data, float start, float sweep, Color clr)
		{
			_label = label;
			_description = desc;
			_value = data;
			_startPos = start;
			_sweepSize = sweep;
			_color = clr;
		}

		public string Label 
		{
			get{ return _label; }
			set{ _label = value; }
		}

		public string Description 
		{
			get{ return _description; }
			set{ _description = value; }
		} 

		public float Value 
		{
			get{ return _value; }
			set{ _value = value; }
		}

		public Color ItemColor 
		{
			get{ return _color; }
			set{ _color = value; }
		}

		public float StartPos
		{
			get{ return _startPos; }
			set{ _startPos = value; }
		}

		public float SweepSize
		{
			get{ return _sweepSize; }
			set{ _sweepSize = value; }
		}
	}

	//*********************************************************************
	//
	// Custom Collection for ChartItems
	//
	//*********************************************************************

	public class ChartItemsCollection : CollectionBase 
	{
		public ChartItem this[int index] 
		{
			get{ return (ChartItem)(List[index]); }
			set{ List[index] = value; }
		}
 
		public int Add(ChartItem value) 
		{
			return List.Add(value);
		}
 
		public int IndexOf(ChartItem value) 
		{
			return List.IndexOf(value);
		}
 
		public bool Contains(ChartItem value) 
		{
			return List.Contains(value);
		}

		public void Remove(ChartItem value) 
		{
			List.Remove(value);
		}
	}
}
