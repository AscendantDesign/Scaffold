//	SvgAnimationProperty.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SkiaSharpSvg
{
	//*-------------------------------------------------------------------------*
	//*	SvgAnimationPropertyCollection																					*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of SvgAnimationPropertyItem Items.
	/// </summary>
	public class SvgAnimationPropertyCollection : List<SvgAnimationPropertyItem>
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
		//*	Add																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add a new item to the collection by member values.
		/// </summary>
		/// <param name="propertyName">
		/// Name of the property.
		/// </param>
		/// <param name="propertyValue">
		/// Value of the property.
		/// </param>
		/// <returns>
		/// Reference to newly created and added property.
		/// </returns>
		public SvgAnimationPropertyItem Add(string propertyName,
			object propertyValue)
		{
			SvgAnimationPropertyItem result = new SvgAnimationPropertyItem();

			result.Name = propertyName;
			result.Value = propertyValue;
			this.Add(result);

			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Clone																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a clone of the source collection, placing the new values in the
		/// target collection.
		/// </summary>
		/// <param name="source">
		/// </param>
		/// <param name="target">
		/// </param>
		public static void Clone(SvgAnimationPropertyCollection source,
			SvgAnimationPropertyCollection target)
		{
			if(target != null)
			{
				target.Clear();
			}
			if(source?.Count > 0 && target != null)
			{
				foreach(SvgAnimationPropertyItem item in source)
				{
					target.Add(new SvgAnimationPropertyItem(item));
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Set																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the value of the specified property, creating that property if not
		/// found.
		/// </summary>
		/// <param name="propertyName">
		/// </param>
		/// <param name="value">
		/// </param>
		public void Set(string propertyName, object value)
		{
			SvgAnimationPropertyItem property =
				this.FirstOrDefault(x => x.Name == propertyName);

			if(property == null)
			{
				property = new SvgAnimationPropertyItem();
				property.Name = propertyName;
				this.Add(property);
			}
			property.Value = value;
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SvgAnimationPropertyCollectionConverter																	*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Custom JSON converter for SVG animation properties collection.
	/// </summary>
	public class SvgAnimationPropertyCollectionConverter :
		JsonConverter<SvgAnimationPropertyCollection>
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
		//* ReadJson																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read JSON from the file into the local collection.
		/// </summary>
		/// <param name="reader">
		/// Reference to the active stream reader.
		/// </param>
		/// <param name="objectType">
		/// Type of object to create.
		/// </param>
		/// <param name="existingValue">
		/// The existing collection to populate.
		/// </param>
		/// <param name="hasExistingValue">
		/// Value indicating whether an existing value already exists.
		/// </param>
		/// <param name="serializer">
		/// Reference to the deserializer.
		/// </param>
		/// <returns>
		/// Reference to a newly created SVG animation property collection, if
		/// existing value was not provided. Otherwise, a reference to the
		/// existing collection.
		/// </returns>
		public override SvgAnimationPropertyCollection ReadJson(JsonReader reader,
			Type objectType,
			SvgAnimationPropertyCollection existingValue,
			bool hasExistingValue, JsonSerializer serializer)
		{
			JArray array = JArray.Load(reader);
			SvgAnimationPropertyCollection result =
				(hasExistingValue ?
				existingValue :
				new SvgAnimationPropertyCollection());

			Debug.WriteLine("SvgAnimationPropertyCollection.ReadJson");
			foreach(JObject jObject in array)
			{
				foreach(var property in jObject)
				{
					result.Add(property.Key, property.Value);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* WriteJson																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Write the members of the collection to JSON as a string, object
		/// dictionary series.
		/// </summary>
		/// <param name="writer">
		/// Reference to the stream writer.
		/// </param>
		/// <param name="value">
		/// Reference to the collection to be serialized.
		/// </param>
		/// <param name="serializer">
		/// Reference to the active serializer engine.
		/// </param>
		public override void WriteJson(JsonWriter writer,
			SvgAnimationPropertyCollection value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SvgAnimationPropertyItem																								*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Name / Value property used within animations.
	/// </summary>
	public class SvgAnimationPropertyItem
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
		/// Create a new instance of the SvgAnimationPropertyItem Item.
		/// </summary>
		public SvgAnimationPropertyItem()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the SvgAnimationPropertyItem Item.
		/// </summary>
		public SvgAnimationPropertyItem(SvgAnimationPropertyItem source)
		{
			if(source != null)
			{
				this.mName = source.mName;
				this.mValue = source.mValue;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Name																																	*
		//*-----------------------------------------------------------------------*
		private string mName = "";
		/// <summary>
		/// Get/Set the name of this item.
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
		private object mValue = null;
		/// <summary>
		/// Get/Set the value of this item.
		/// </summary>
		public object Value
		{
			get { return mValue; }
			set { mValue = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
