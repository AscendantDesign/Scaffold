//	BrowserDriver.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using SkiaSharpSvg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Scaffold.ScaffoldNodesUtil;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	BrowserDriver																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Interface and translation functionality for working with HTMLControl and
	/// other browser components.
	/// </summary>
	public class BrowserDriver
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	ConvertValue																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert the parameter object value to one compatible with an HTML
		/// attribute value.
		/// </summary>
		/// <param name="value">
		/// </param>
		/// <param name="quoted">
		/// Value indicating whether all values will be quoted.
		/// </param>
		/// <returns>
		/// </returns>
		private static string ConvertValue(object value, bool quoted = true)
		{
			double dValue = 0.0;
			string result = "";

			if(value == null)
			{
				result = "";
			}
			else if(value is string @string)
			{
				result = @string;
			}
			else if(SvgAnimationUtil.IsDouble(value))
			{
				double.TryParse(value.ToString(), out dValue);
				result = dValue.ToString("0.000000");
			}
			else
			{
				result = value.ToString();
			}
			if(quoted)
			{
				if(!(result.StartsWith("'") && result.EndsWith("'")) &&
					!(result.StartsWith("\"") && result.EndsWith("\"")) &&
					!(result.StartsWith("`") && result.EndsWith("`")))
				{
					result = "\"" + result + "\"";
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
		/// Create a new instance of the BrowserDriver Item.
		/// </summary>
		public BrowserDriver()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the BrowserDriver Item.
		/// </summary>
		/// <param name="hostHtml">
		/// Reference to the physical host HTML control.
		/// </param>
		/// <param name="hostStatus">
		/// Reference to an active status message label.
		/// </param>
		/// <param name="hostProgress">
		/// Reference to an active progress bar.
		/// </param>
		public BrowserDriver(HTMLControl hostHtml,
			ToolStripStatusLabel hostStatus = null,
			ToolStripProgressBar hostProgress = null)
		{
			mHostHtml = hostHtml;
			mHostStatus = hostStatus;
			mHostProgress = hostProgress;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Attr																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Write an attribute to the browser control.
		/// </summary>
		/// <param name="selector">
		/// The selector pattern to use on the jQuery command.
		/// </param>
		/// <param name="attributeName">
		/// Name of the attribute to set.
		/// </param>
		/// <param name="attributeValue">
		/// Value to place on the attribute.
		/// </param>
		public void Attr(string selector, string attributeName,
			object attributeValue)
		{
			mHostHtml.JavaScriptCommand(
				$"$('{selector}')." +
				$"attr('{attributeName}', {ConvertValue(attributeValue)});");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Css																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Write a CSS value to the browser control.
		/// </summary>
		/// <param name="selector">
		/// The selector pattern to use on the jQuery command.
		/// </param>
		/// <param name="attributeName">
		/// Name of the CSS attribute to set.
		/// </param>
		/// <param name="attributeValue">
		/// Value to place on the CSS attribute.
		/// </param>
		public void Css(string selector, string attributeName,
			object attributeValue)
		{
			mHostHtml.JavaScriptCommand(
				$"$('{selector}')." +
				$"css('{attributeName}', {ConvertValue(attributeValue)});");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetId																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the ID of the specified object.
		/// </summary>
		/// <param name="selector">
		/// Selector of an item on the document for which the ID is being queried.
		/// </param>
		/// <param name="token">
		/// </param>
		public async Task GetId(string selector, NameValueItem token)
		{
			if(mHostHtml != null && selector?.Length > 0)
			{
				mHostHtml.Messages.Clear();
				mHostHtml.AwaitingWebMessageReceived = true;
				mHostHtml.JavaScriptCommand(
					$"embeddedBuffer.value = $('{selector}').attr('id');");
				token.Name = token.Value = "";
				await mHostHtml.GetWebMessage(token);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	HostHtml																															*
		//*-----------------------------------------------------------------------*
		private HTMLControl mHostHtml = null;
		/// <summary>
		/// Get/Set a reference to the HTML control used for operations on this
		/// element.
		/// </summary>
		public HTMLControl HostHtml
		{
			get { return mHostHtml; }
			set { mHostHtml = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	HostProgress																													*
		//*-----------------------------------------------------------------------*
		private ToolStripProgressBar mHostProgress = null;
		/// <summary>
		/// Get/Set a reference to the active progress bar on the host form.
		/// </summary>
		public ToolStripProgressBar HostProgress
		{
			get { return mHostProgress; }
			set { mHostProgress = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	HostStatus																														*
		//*-----------------------------------------------------------------------*
		private ToolStripStatusLabel mHostStatus = null;
		/// <summary>
		/// Get/Set a reference to the status label on the host form.
		/// </summary>
		public ToolStripStatusLabel HostStatus
		{
			get { return mHostStatus; }
			set { mHostStatus = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	LoadAttributes																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Load the specified attribute names for the specified element.
		/// </summary>
		/// <param name="id">
		/// ID of the element for which attributes will be loaded.
		/// </param>
		/// <param name="element">
		/// Reference to the element object to be constructed for the data found.
		/// </param>
		public async Task LoadAttributes(string id,
			BrowserElementItem element)
		{
			int count = 0;
			int index = 0;
			Match match = null;
			MatchCollection matches = null;
			NameValueItem token = null;

			if(id?.Length > 0 && element != null)
			{
				token = new NameValueItem();
				element.Clear();
				if(mHostHtml != null)
				{
					element.Id = id;
					//	In this version, we want to get the entire element, then
					//	read the attributes from that section. Getting each
					//	attribute through the post message mechanism is far too
					//	expensive in round-trips.
					mHostHtml.Messages.Clear();
					mHostHtml.AwaitingWebMessageReceived = true;
					mHostHtml.JavaScriptCommand(
						$"embeddedBuffer.value = getElementOnly('{id}');");
					token.Name = token.Value = "";
					await mHostHtml.GetWebMessage(token);

					if(token.Name == "Message")
					{
						matches = Regex.Matches(token.Value, ResourceMain.rxHTMLElement);
						count = matches.Count;
						if(count > 0)
						{
							//	First match is the element tag.
							//	All additional matches are attributes and properties.
							element.ElementTag = GetValue(matches[0], "n");
						}
						for(index = 1; index < count; index ++)
						{
							//	Add each of the attributes by name and value.
							match = matches[index];
							element.Attributes.Add(new BrowserAttributeItem()
							{
								Name = GetValue(match, "n"),
								Value = GetValue(match, "v")
							});
						}
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Rotation																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Rotate an object around its center.
		/// </summary>
		/// <param name="selector">
		/// Selector of the object to rotate.
		/// </param>
		/// <param name="value">
		/// Amount to rotate the object.
		/// </param>
		/// <param name="x">
		/// Current X coordinate of the object.
		/// </param>
		/// <param name="y">
		/// Current Y coordinate of the object.
		/// </param>
		/// <param name="width">
		/// Current width of the object.
		/// </param>
		/// <param name="height">
		/// Current height of the object.
		/// </param>
		/// <param name="element">
		/// Reference to a browser element token to load.
		/// </param>
		public void Rotation(string selector, object value,
			double x, double y, double width, double height,
			BrowserElementItem element)
		{
			//double height = 0.0;
			string id = selector;
			//double width = 0.0;
			//double x = 0.0;
			//double y = 0.0;

			//	Remove hashtag for the ID use.
			if(id.StartsWith("#"))
			{
				id = id.Substring(1);
			}
			//await LoadAttributes(id, element);
			//if(element.Attributes.Exists(v => v.Name == "width"))
			//{
			//	double.TryParse(
			//		element.Attributes.First(v => v.Name == "width").Value, out width);
			//}
			//if(element.Attributes.Exists(v => v.Name == "height"))
			//{
			//	double.TryParse(
			//		element.Attributes.First(v => v.Name == "height").Value, out height);
			//}
			//if(element.Attributes.Exists(v => v.Name == "x"))
			//{
			//	double.TryParse(
			//		element.Attributes.First(v => v.Name == "x").Value, out x);
			//}
			//if(element.Attributes.Exists(v => v.Name == "y"))
			//{
			//	double.TryParse(
			//		element.Attributes.First(v => v.Name == "y").Value, out y);
			//}
			if(width != 0.0 && height != 0.0)
			{
				Attr(selector, "transform",
					$"rotate({ConvertValue(value, false)} " +
					$"{x + (width / 2.0)} " +
					$"{y + (height / 2.0)}" +
					")");
				//Attr(selector, "transform",
				//	$"rotate({ConvertValue(value, false)})");
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ThreadSleep																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Continue to process the UI while waiting on the calling thread for
		/// the specified number of milliseconds.
		/// </summary>
		/// <param name="milliseconds">
		/// Number of milliseconds to wait.
		/// </param>
		/// <returns>
		/// Reference to an active task.
		/// </returns>
		public async Task ThreadSleep(int milliseconds)
		{
			await Task.Delay(milliseconds);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	UserUnit																															*
		//*-----------------------------------------------------------------------*
		private SvgUserUnit mUserUnit = new SvgUserUnit();
		/// <summary>
		/// Get/Set a reference to the active user unit to pixel ratio.
		/// </summary>
		public SvgUserUnit UserUnit
		{
			get { return mUserUnit; }
			set { mUserUnit = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	BrowserAttributeCollection																							*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of BrowserAttributeItem Items.
	/// </summary>
	public class BrowserAttributeCollection : List<BrowserAttributeItem>
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
		//*	_Indexer																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the item from the collection by its name.
		/// </summary>
		/// <param name="name">
		/// Name of the item to find.
		/// </param>
		public BrowserAttributeItem this[string name]
		{
			get
			{
				BrowserAttributeItem result = this.FirstOrDefault(x => x.Name == name);
				if(result == null)
				{
					result = new BrowserAttributeItem() { Name = name };
					this.Add(result);
				}
				return result;
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	BrowserAttributeItem																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Single attribute name and value for a specific element.
	/// </summary>
	public class BrowserAttributeItem
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
		//*	Name																																	*
		//*-----------------------------------------------------------------------*
		private string mName = "";
		/// <summary>
		/// Get/Set the name of this attribute.
		/// </summary>
		public string Name
		{
			get { return mName; }
			set { mName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Value																																	*
		//*-----------------------------------------------------------------------*
		private string mValue = "";
		/// <summary>
		/// Get/Set the value of this attribute.
		/// </summary>
		public string Value
		{
			get { return mValue; }
			set { mValue = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	BrowserElementCollection																								*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of BrowserElementItem Items.
	/// </summary>
	public class BrowserElementCollection : List<BrowserElementItem>
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
	//*	BrowserElementItem																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about a single element.
	/// </summary>
	public class BrowserElementItem
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
		//*	Clear																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Clear the contents of this element so it can be reused.
		/// </summary>
		public void Clear()
		{
			mId = "";
			mElementTag = "";
			mAttributes.Clear();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Attributes																														*
		//*-----------------------------------------------------------------------*
		private BrowserAttributeCollection mAttributes =
			new BrowserAttributeCollection();
		/// <summary>
		/// Get a reference to the collection of attributes on this element.
		/// </summary>
		public BrowserAttributeCollection Attributes
		{
			get { return mAttributes; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ElementTag																														*
		//*-----------------------------------------------------------------------*
		private string mElementTag = "";
		/// <summary>
		/// Get/Set the tag name of this element.
		/// </summary>
		public string ElementTag
		{
			get { return mElementTag; }
			set { mElementTag = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Id																																		*
		//*-----------------------------------------------------------------------*
		private string mId = "";
		/// <summary>
		/// Get/Set the unique ID of this element within the document.
		/// </summary>
		public string Id
		{
			get { return mId; }
			set { mId = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
