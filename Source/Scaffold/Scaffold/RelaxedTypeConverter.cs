//	RelaxedTypeConverter.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using CefSharp;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms.VisualStyles;

using static Scaffold.ScaffoldUtil;
using static Scaffold.ScaffoldNodesUtil;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	RelaxedTypeConverterCollection																					*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of RelaxedTypeConverterItem Items.
	/// </summary>
	public class RelaxedTypeConverterCollection : List<RelaxedTypeConverterItem>
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* ConvertToBool																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert the provided value to boolean.
		/// </summary>
		/// <param name="value">
		/// The value to convert.
		/// </param>
		/// <returns>
		/// System.Boolean representing the caller's value.
		/// </returns>
		private object ConvertToBool(object value)
		{
			bool result = false;
			string stringVal = "";

			if(value != null)
			{
				if(value is bool boolVal)
				{
					result = boolVal;
				}
				else
				{
					stringVal = value.ToString();
					if(stringVal.Length > 0)
					{
						bool.TryParse(stringVal, out result);
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ConvertToColor																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert the provided value to System.Drawing.Color.
		/// </summary>
		/// <param name="value">
		/// The value to convert.
		/// </param>
		/// <returns>
		/// System.Drawing.Color representing the caller's value.
		/// </returns>
		private object ConvertToColor(object value)
		{
			char[] comma = new char[] { ',' };
			Color result = Color.Empty;
			string valueString = "";
			string[] valueStrings = new string[0];

			if(value != null)
			{
				if(value is Color colorVal)
				{
					//	Straight conversion from another color.
					result = colorVal;
				}
				else if(value is int intVal)
				{
					//	Integer ARGB.
					result = Color.FromArgb(intVal);
				}
				else
				{
					//	The value is a string.
					valueString = value.ToString().ToLower();
					if(valueString.StartsWith("#"))
					{
						//	Hex color string.
						if(valueString.Length == 7 || valueString.Length == 9)
						{
							//	RGB or RGBA.
							result = FromHex(valueString);
						}
					}
					else if(IsNumeric(valueString) &&
						!IsNumericFloatingPoint(valueString))
					{
						//	Integer.
						result = Color.FromArgb(ToInt(valueString));
					}
					else if(valueString.StartsWith("argb("))
					{
						//	argb(a, r, g, b)
						valueString = InsideOf(valueString, "(", ")");
						if(valueString.Length > 0)
						{
							valueStrings = valueString.Split(comma);
							if(valueStrings.Length == 4)
							{
								result = Color.FromArgb(
									ToInt(valueStrings[0].Trim()),
									ToInt(valueStrings[1].Trim()),
									ToInt(valueStrings[2].Trim()),
									ToInt(valueStrings[3].Trim()));
							}
						}
					}
					else if(valueString.StartsWith("rgb("))
					{
						//	rgb(r, g, b)
						valueString = InsideOf(valueString, "(", ")");
						if(valueString.Length > 0)
						{
							valueStrings = valueString.Split(comma);
							if(valueStrings.Length == 3)
							{
								result = Color.FromArgb(
									ToInt(valueStrings[0].Trim()),
									ToInt(valueStrings[1].Trim()),
									ToInt(valueStrings[2].Trim()));
							}
						}
					}
					else if(value.ToString().Length > 0)
					{
						//	Color name.
						try
						{
							result = Color.FromName(valueString);
						}
						catch { }
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ConvertToDateTime																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert the provided value to DateTime.
		/// </summary>
		/// <param name="value">
		/// The value to convert.
		/// </param>
		/// <returns>
		/// DateTime representation of the caller's value.
		/// </returns>
		private object ConvertToDateTime(object value)
		{
			DateTime result = DateTime.MinValue;
			string stringVal = "";

			if(value != null)
			{
				if(value is DateTime datetimeVal)
				{
					result = datetimeVal;
				}
				else
				{
					stringVal = value.ToString();
					if(stringVal.Length > 0)
					{
						DateTime.TryParse(stringVal, out result);
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ConvertToDouble																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert the provided value to Double.
		/// </summary>
		/// <param name="value">
		/// The value to convert.
		/// </param>
		/// <returns>
		/// Double representation of the caller's value.
		/// </returns>
		private object ConvertToDouble(object value)
		{
			double result = 0d;

			if(value != null)
			{
				if(value is double doubleVal)
				{
					//	Value is already a double.
					result = doubleVal;
				}
				else if(IsNumeric(value))
				{
					double.TryParse(value.ToString(), out result);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ConvertToGuid																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert the provided value to GUID.
		/// </summary>
		/// <param name="value">
		/// The value to convert.
		/// </param>
		/// <returns>
		/// Guid representation of the caller's value, if successful. Otherwise,
		/// Guid.Empty.
		/// </returns>
		private object ConvertToGuid(object value)
		{
			Guid result = Guid.Empty;
			string stringVal = "";

			if(value != null)
			{
				if(value is Guid guidVal)
				{
					//	Guid to Guid.
					result = guidVal;
				}
				else
				{
					stringVal = value.ToString();
					if(stringVal.Length > 0)
					{
						Guid.TryParse(stringVal, out result);
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ConvertToHexColor																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert the provided value to a hex color value, prefixed with a hash
		/// sign.
		/// </summary>
		/// <param name="value">
		/// The value to convert.
		/// </param>
		/// <returns>
		/// String representation of a hex color value.
		/// </returns>
		private object ConvertToHexColor(object value)
		{
			char[] comma = new char[] { ',' };
			string result = "#00000000";
			string valueString = "";
			string[] valueStrings = new string[0];

			if(value != null)
			{
				if(value is Color colorVal)
				{
					//	Color to string.
					result = ToHex(colorVal, true);
				}
				else if(value is int intVal)
				{
					//	Integer ARGB.
					result = ToHex(Color.FromArgb(intVal), true);
				}
				else
				{
					//	The value is a string.
					valueString = value.ToString().ToLower();
					if(valueString.StartsWith("#"))
					{
						//	Hex to hex.
						result = valueString;
					}
					else if(IsNumeric(valueString) &&
						!IsNumericFloatingPoint(valueString))
					{
						//	Integer.
						result = ToHex(Color.FromArgb(ToInt(valueString)), true);
					}
					else if(valueString.StartsWith("argb("))
					{
						//	argb(a, r, g, b)
						valueString = InsideOf(valueString, "(", ")");
						if(valueString.Length > 0)
						{
							valueStrings = valueString.Split(comma);
							if(valueStrings.Length == 4)
							{
								result = ToHex(Color.FromArgb(
									ToInt(valueStrings[0].Trim()),
									ToInt(valueStrings[1].Trim()),
									ToInt(valueStrings[2].Trim()),
									ToInt(valueStrings[3].Trim())), true);
							}
						}
					}
					else if(valueString.StartsWith("rgb("))
					{
						//	rgb(r, g, b)
						valueString = InsideOf(valueString, "(", ")");
						if(valueString.Length > 0)
						{
							valueStrings = valueString.Split(comma);
							if(valueStrings.Length == 3)
							{
								result = ToHex(Color.FromArgb(
									ToInt(valueStrings[0].Trim()),
									ToInt(valueStrings[1].Trim()),
									ToInt(valueStrings[2].Trim())), true);
							}
						}
					}
					else
					{
						//	Color name.
						try
						{
							result = ToHex(Color.FromName(valueString), true);
						}
						catch { }
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ConvertToInt																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert the provided value to an integer.
		/// </summary>
		/// <param name="value">
		/// The value to convert.
		/// </param>
		/// <returns>
		/// Int32 representation of the caller's value.
		/// </returns>
		private object ConvertToInt(object value)
		{
			int result = 0;

			if(value != null)
			{
				if(value is int intVal)
				{
					//	Value is already an integer.
					result = intVal;
				}
				else if(IsNumeric(value) &&
					!IsNumericFloatingPoint(value) &&
					!IsNumericScientific(value))
				{
					int.TryParse(value.ToString(), out result);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ConvertToSingle																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert the provided value to Single.
		/// </summary>
		/// <param name="value">
		/// The value to convert.
		/// </param>
		/// <returns>
		/// Single floating point numeric representation of the caller's value.
		/// </returns>
		private object ConvertToSingle(object value)
		{
			float result = 0f;

			if(value != null)
			{
				if(value is float singleVal)
				{
					//	Value is already a single.
					result = singleVal;
				}
				else if(IsNumeric(value))
				{
					float.TryParse(value.ToString(), out result);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ConvertToString																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert the provided value to a string.
		/// </summary>
		/// <param name="value">
		/// The value to convert.
		/// </param>
		/// <returns>
		/// String representation of the caller's value, if valid. Otherwise,
		/// and empty string.
		/// </returns>
		private object ConvertToString(object value)
		{
			string result = "";

			if(value != null)
			{
				if(value is bool boolVal)
				{
					result = boolVal.ToString();
				}
				else if(value is Color colorVal)
				{
					result = ToHex(colorVal, true);
				}
				else if(value is DateTime dateTimeVal)
				{
					result = dateTimeVal.ToString("MM/dd/yyyy HH:mm:ss");
				}
				else if(value is double doubleVal)
				{
					result = doubleVal.ToString("0.000");
				}
				else if(value is Guid guidVal)
				{
					result = guidVal.ToString("D");
				}
				else if(value is int intVal)
				{
					result = intVal.ToString();
				}
				else
				{
					result = value.ToString();
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the RelaxedTypeConverterCollection Item.
		/// </summary>
		public RelaxedTypeConverterCollection()
		{
			RelaxedTypeConverterTargetItem target = null;

			Add(new RelaxedTypeConverterItem("bool"));
			Add(new RelaxedTypeConverterItem("color"));
			Add(new RelaxedTypeConverterItem("datetime"));
			Add(new RelaxedTypeConverterItem("double"));
			Add(new RelaxedTypeConverterItem("guid"));
			Add(new RelaxedTypeConverterItem("hexcolor"));
			Add(new RelaxedTypeConverterItem("int"));
			Add(new RelaxedTypeConverterItem("integer"));
			Add(new RelaxedTypeConverterItem("single"));
			Add(new RelaxedTypeConverterItem("string"));
			foreach(RelaxedTypeConverterItem item in this)
			{
				target = new RelaxedTypeConverterTargetItem("bool");
				target.Converter = this.ConvertToBool;
				item.Targets.Add(target);
				target = new RelaxedTypeConverterTargetItem("color");
				target.Converter = this.ConvertToColor;
				item.Targets.Add(target);
				target = new RelaxedTypeConverterTargetItem("datetime");
				target.Converter = this.ConvertToDateTime;
				item.Targets.Add(target);
				target = new RelaxedTypeConverterTargetItem("double");
				target.Converter = this.ConvertToDouble;
				item.Targets.Add(target);
				target = new RelaxedTypeConverterTargetItem("guid");
				target.Converter = this.ConvertToGuid;
				item.Targets.Add(target);
				target = new RelaxedTypeConverterTargetItem("hexcolor");
				target.Converter = this.ConvertToHexColor;
				item.Targets.Add(target);
				target = new RelaxedTypeConverterTargetItem("int");
				target.Converter = this.ConvertToInt;
				item.Targets.Add(target);
				target = new RelaxedTypeConverterTargetItem("integer");
				target.Converter = this.ConvertToInt;
				item.Targets.Add(target);
				target = new RelaxedTypeConverterTargetItem("single");
				target.Converter = this.ConvertToSingle;
				item.Targets.Add(target);
				target = new RelaxedTypeConverterTargetItem("string");
				target.Converter = this.ConvertToString;
				item.Targets.Add(target);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Convert																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert the caller's value from the specified relaxed source type to
		/// the relaxed target type.
		/// </summary>
		/// <param name="value">
		/// Value to convert.
		/// </param>
		/// <param name="sourceType">
		/// The source type of the value provided.
		/// </param>
		/// <param name="targetType">
		/// The target type of the return value.
		/// </param>
		/// <returns>
		/// The converted value.
		/// </returns>
		public object Convert(object value, string sourceType, string targetType)
		{
			string lSourceType = "";
			string lTargetType = "";
			object result = null;
			RelaxedTypeConverterItem source = null;
			RelaxedTypeConverterTargetItem target = null;

			if(value != null && sourceType?.Length > 0 && targetType?.Length > 0)
			{
				source = this.FirstOrDefault(x =>
					x.SourceTypeName.ToLower() == sourceType.ToLower());
				if(source != null)
				{
					//	Source type found.
					target = source.Targets.FirstOrDefault(x =>
						x.TargetTypeName.ToLower() == targetType.ToLower());
					if(target != null)
					{
						//	Target type found.
						if(target.Converter != null)
						{
							result = target.Converter(value);
						}
					}
				}
			}
			else if(value == null && sourceType.Length > 0 && targetType.Length > 0)
			{
				lSourceType = sourceType.ToLower();
				lTargetType = targetType.ToLower();
				switch(lSourceType)
				{
					case "string":
						switch(lTargetType)
						{
							case "bool":
								result = false;
								break;
							case "color":
							case "hexcolor":
								result = Color.Empty;
								break;
							case "datetime":
								result = DateTime.MinValue;
								break;
							case "double":
								result = 0d;
								break;
							case "guid":
								result = Guid.Empty;
								break;
							case "int":
							case "integer":
								result = 0;
								break;
							case "single":
								result = 0f;
								break;
							case "string":
								result = "";
								break;
						}
						break;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	RelaxedTypeConverterItem																								*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information to convert from a specified relaxed type or style.
	/// </summary>
	public class RelaxedTypeConverterItem
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the RelaxedTypeConverterItem Item.
		/// </summary>
		public RelaxedTypeConverterItem()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the RelaxedTypeConverterItem Item.
		/// </summary>
		/// <param name="sourceTypeName">
		/// Name of the relaxed type or style to add.
		/// </param>
		public RelaxedTypeConverterItem(string sourceTypeName)
		{
			mSourceTypeName = sourceTypeName;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Targets																																*
		//*-----------------------------------------------------------------------*
		private RelaxedTypeConverterTargetCollection mTargets =
			new RelaxedTypeConverterTargetCollection();
		/// <summary>
		/// Get a reference to the collection of targets for this conversion type.
		/// </summary>
		public RelaxedTypeConverterTargetCollection Targets
		{
			get { return mTargets; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SourceTypeName																												*
		//*-----------------------------------------------------------------------*
		private string mSourceTypeName = "";
		/// <summary>
		/// Get/Set the name of the source conversion type.
		/// </summary>
		public string SourceTypeName
		{
			get { return mSourceTypeName; }
			set { mSourceTypeName = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	RelaxedTypeConverterTargetCollection																		*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of RelaxedTypeConverterTargetItem Items.
	/// </summary>
	public class RelaxedTypeConverterTargetCollection :
		List<RelaxedTypeConverterTargetItem>
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	RelaxedTypeConverterTargetItem																					*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Target type to which a conversion can be made.
	/// </summary>
	public class RelaxedTypeConverterTargetItem
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the RelaxedTypeConverterTargetItem Item.
		/// </summary>
		public RelaxedTypeConverterTargetItem()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the RelaxedTypeConverterTargetItem Item.
		/// </summary>
		/// <param name="targetTypeName">
		/// Name of the relaxed type or style to add.
		/// </param>
		public RelaxedTypeConverterTargetItem(string targetTypeName)
		{
			mTargetTypeName = targetTypeName;
		}
		//*-----------------------------------------------------------------------*

		public Func<object, object> Converter = null;

		//*-----------------------------------------------------------------------*
		//*	TargetTypeName																												*
		//*-----------------------------------------------------------------------*
		private string mTargetTypeName = "";
		/// <summary>
		/// Get/Set the name of the target conversion type.
		/// </summary>
		public string TargetTypeName
		{
			get { return mTargetTypeName; }
			set { mTargetTypeName = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*


}
