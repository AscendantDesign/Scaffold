//	SvgAnimationFrameElement.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace SkiaSharpSvg
{
	//*-------------------------------------------------------------------------*
	//*	SvgAnimationFrameElementCollection																			*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of SvgAnimationFrameElementItem Items.
	/// </summary>
	public class SvgAnimationFrameElementCollection :
		List<SvgAnimationFrameElementItem>
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
		//*	Clone																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Clone the contents of the source collection to the target.
		/// </summary>
		/// <param name="source">
		/// </param>
		/// <param name="target">
		/// </param>
		public static void Clone(SvgAnimationFrameElementCollection source,
			SvgAnimationFrameElementCollection target)
		{
			if(target != null)
			{
				target.Clear();
			}
			if(source?.Count > 0 && target != null)
			{
				foreach(SvgAnimationFrameElementItem item in source)
				{
					target.Add(new SvgAnimationFrameElementItem(item));
				}
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SvgAnimationFrameElementItem																						*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Individual element definition for the current frame.
	/// </summary>
	public class SvgAnimationFrameElementItem
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
		/// Create a new instance of the SvgAnimationFrameElementItem Item.
		/// </summary>
		public SvgAnimationFrameElementItem()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the SvgAnimationFrameElementItem Item.
		/// </summary>
		/// <param name="source">
		/// </param>
		public SvgAnimationFrameElementItem(SvgAnimationFrameElementItem source)
		{
			if(source != null)
			{
				mDescription = source.mDescription;
				mElementID = source.mElementID;
				mElementMark = source.mElementMark;
				mElementType = source.mElementType;
				mInterpolationCount = source.mInterpolationCount;
				mInterpolationType = source.mInterpolationType;
				//SvgAnimationPropertyCollection.Clone(source.mProperties, mProperties);
				foreach(KeyValuePair<string, object> entry in source.mProperties)
				{
					mProperties.Add(entry.Key, entry.Value);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AssignElementProperty																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Assign the value of the property to the element from the current values
		/// value in the scene mark setting.
		/// </summary>
		/// <param name="element">
		/// </param>
		/// <param name="property">
		/// </param>
		/// <returns>
		/// True if the property value was assigned. Otherwise, false.
		/// </returns>
		public static bool AssignElementProperty(
			SvgAnimationFrameElementItem element,
			KeyValuePair<string, object>? property)
		{
			bool result = false;

			if(element != null && property != null)
			{
				element.mElementType = SvgAnimationElementTypeEnum.Character;
				element.Description = element.Description;
				element.InterpolationCount = 0;
				element.InterpolationType = SvgAnimationInterpolationTypeEnum.None;
				element.mProperties.Add(property?.Key, property?.Value);
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Assign the value of the property to the element from the current values
		/// value in the scene mark setting.
		/// </summary>
		/// <param name="element">
		/// </param>
		/// <param name="setting">
		/// </param>
		/// <param name="interpolationIndex">
		/// </param>
		/// <returns>
		/// True if the property value was assigned. Otherwise, false.
		/// </returns>
		public static bool AssignElementProperty(
			SvgAnimationFrameElementItem element,
			SvgAnimationSceneMarkCharacterItem setting,
			int interpolationIndex)
		{
			string description = "";
			int interCount = 0;
			SvgAnimationInterpolationItem interpol = null;
			bool result = false;

			if(element != null && setting != null)
			{
				element.mElementType = SvgAnimationElementTypeEnum.Character;
				interCount = setting.Interpolations.Count;
				if(interCount > interpolationIndex)
				{
					interpol = setting.Interpolations[interpolationIndex];
					description = (interpol.Description.Length > 0 ?
						interpol.Description : setting.Description);
					element.Description = description;
					element.InterpolationCount = interpol.InterpolationCount;
					element.InterpolationType = interpol.InterpolationType;
					switch(interpol.InterpolationType)
					{
						case SvgAnimationInterpolationTypeEnum.BounceIn:
						case SvgAnimationInterpolationTypeEnum.CountIn:
						case SvgAnimationInterpolationTypeEnum.Immediate:
						case SvgAnimationInterpolationTypeEnum.LinearIn:
						case SvgAnimationInterpolationTypeEnum.None:
							//	All of the input-style interpolation types can
							//	use base name or .In suffix.
							if(interpol.InterpolationProperty.IndexOf(".") > 0)
							{
								//	The interpolation property has been expressed
								//	with a suffix. This is an explicit use of any
								//	defined property.
								if(setting.Properties.ContainsKey(
									interpol.InterpolationProperty))
								{
									element.Properties.Add(
										interpol.InterpolationProperty.
										Substring(0, interpol.InterpolationProperty.IndexOf(".")),
										setting.Properties[
											interpol.InterpolationProperty]);
									result = true;
								}
							}
							else
							{
								//	The interpolation property was expressed
								//	without a suffix. Check first for .In,
								//	then base value.
								if(setting.Properties.ContainsKey(
									interpol.InterpolationProperty + ".In"))
								{
									//	In suffix found. Use that value.
									element.Properties.Add(
										interpol.InterpolationProperty,
										setting.Properties[
											interpol.InterpolationProperty + ".In"]);
									interpol.InterpolationProperty += ".In";
									result = true;
								}
								else if(setting.Properties.ContainsKey(
									interpol.InterpolationProperty))
								{
									//	Base value found. Use that value.
									element.Properties.Add(
										interpol.InterpolationProperty,
										setting.Properties[
											interpol.InterpolationProperty]);
									result = true;
								}
							}
							break;
						default:
							//	All of the output-style interpolation types can
							//	use base name or .Out suffix.
							if(interpol.InterpolationProperty.IndexOf(".") > 0)
							{
								//	The interpolation property has been expressed
								//	with a suffix. This is an explicit use of any
								//	defined property.
								if(setting.Properties.ContainsKey(
									interpol.InterpolationProperty))
								{
									element.Properties.Add(
										interpol.InterpolationProperty.Substring(0,
										interpol.InterpolationProperty.IndexOf(".")),
										setting.Properties[
											interpol.InterpolationProperty]);
									result = true;
								}
							}
							else
							{
								//	The interpolation property was expressed
								//	without a suffix. Check first for .Out,
								//	then base value.
								if(setting.Properties.ContainsKey(
									interpol.InterpolationProperty + ".Out"))
								{
									//	In suffix found. Use that value.
									element.Properties.Add(
										interpol.InterpolationProperty,
										setting.Properties[
											interpol.InterpolationProperty + ".Out"]);
									interpol.InterpolationProperty += ".Out";
									result = true;
								}
								else if(setting.Properties.ContainsKey(
									interpol.InterpolationProperty))
								{
									//	Base value found. Use that value.
									element.Properties.Add(
										interpol.InterpolationProperty,
										setting.Properties[
											interpol.InterpolationProperty]);
									result = true;
								}
							}
							break;
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Description																														*
		//*-----------------------------------------------------------------------*
		private string mDescription = "";
		/// <summary>
		/// Get/Set a brief description of this element.
		/// </summary>
		[JsonProperty(Order = 0)]
		public string Description
		{
			get { return mDescription; }
			set { mDescription = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ElementID																															*
		//*-----------------------------------------------------------------------*
		private string mElementID = "";
		/// <summary>
		/// Get/Set the unique identification of the element to animate.
		/// </summary>
		[JsonProperty(Order = 1)]
		public string ElementID
		{
			get { return mElementID; }
			set { mElementID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ElementMark																														*
		//*-----------------------------------------------------------------------*
		private string mElementMark = "";
		/// <summary>
		/// Get/Set the name of the scene and mark, in dotted notation, if the
		/// element type is set to SceneMark.
		/// </summary>
		[JsonProperty(Order = 3)]
		public string ElementMark
		{
			get { return mElementMark; }
			set { mElementMark = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ElementType																														*
		//*-----------------------------------------------------------------------*
		private SvgAnimationElementTypeEnum mElementType =
			SvgAnimationElementTypeEnum.None;
		/// <summary>
		/// Get/Set the type of element being driven.
		/// </summary>
		[JsonProperty(Order = 2)]
		public SvgAnimationElementTypeEnum ElementType
		{
			get { return mElementType; }
			set { mElementType = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	InterpolationCount																										*
		//*-----------------------------------------------------------------------*
		private int mInterpolationCount = 0;
		/// <summary>
		/// Get/Set the count of steps taken by this interpolation, if measured.
		/// If the interpolation type is not a measured type, then this value is
		/// ignored.
		/// </summary>
		[JsonProperty(Order = 5)]
		public int InterpolationCount
		{
			get { return mInterpolationCount; }
			set { mInterpolationCount = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	InterpolationType																											*
		//*-----------------------------------------------------------------------*
		private SvgAnimationInterpolationTypeEnum mInterpolationType =
			SvgAnimationInterpolationTypeEnum.None;
		/// <summary>
		/// Get/Set the interpolation type assigned to this state.
		/// </summary>
		[JsonProperty(Order = 4)]
		public SvgAnimationInterpolationTypeEnum InterpolationType
		{
			get { return mInterpolationType; }
			set { mInterpolationType = value; }
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	Properties																														*
		////*-----------------------------------------------------------------------*
		//private SvgAnimationPropertyCollection mProperties =
		//	new SvgAnimationPropertyCollection();
		///// <summary>
		///// Get a reference to the collection of properties being driven on this
		///// element.
		///// </summary>
		//[JsonProperty(Order = 6)]
		//public SvgAnimationPropertyCollection Properties
		//{
		//	get { return mProperties; }
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Properties																														*
		//*-----------------------------------------------------------------------*
		private Dictionary<string, object> mProperties =
			new Dictionary<string, object>();
		/// <summary>
		/// Get a reference to the collection of properties being driven on this
		/// element.
		/// </summary>
		[JsonProperty(Order = 6)]
		public Dictionary<string, object> Properties
		{
			get { return mProperties; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*


}
