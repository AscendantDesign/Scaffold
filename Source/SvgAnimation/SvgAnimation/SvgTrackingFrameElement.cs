//	SvgTrackingFrameElement.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace SkiaSharpSvg
{
	//*-------------------------------------------------------------------------*
	//*	SvgTrackingFrameElementCollection																				*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of SvgTrackingFrameElementItem Items.
	/// </summary>
	public class SvgTrackingFrameElementCollection :
		List<SvgTrackingFrameElementItem>
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
		/// <param name="frame">
		/// Reference to the frame for which this item is being added.
		/// </param>
		/// <param name="element">
		/// Reference to the source element for which this item is being added.
		/// </param>
		/// <param name="property">
		/// Reference to the property at the specified frame.
		/// </param>
		/// <returns>
		/// Newly created and added tracking frame element.
		/// </returns>
		public SvgTrackingFrameElementItem Add(SvgAnimationFrameItem frame,
			SvgAnimationFrameElementItem element,
			KeyValuePair<string, object> property)
		{
			SvgTrackingFrameElementItem result = null;

			if(frame != null && element != null)
			{
				result = new SvgTrackingFrameElementItem();
				result.FrameIndex = frame.FrameIndex;
				result.InterpolationCount = element.InterpolationCount;
				result.InterpolationType = element.InterpolationType;
				result.PropertyValue = property.Value;
				this.Add(result);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*



	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SvgTrackingFrameElementItem																							*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Frame element information for tracking.
	/// </summary>
	public class SvgTrackingFrameElementItem
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
		//*	FrameIndex																														*
		//*-----------------------------------------------------------------------*
		private int mFrameIndex = 0;
		/// <summary>
		/// Get/Set the index of this frame in the timeline.
		/// </summary>
		public int FrameIndex
		{
			get { return mFrameIndex; }
			set { mFrameIndex = value; }
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

		//*-----------------------------------------------------------------------*
		//*	PropertyValue																													*
		//*-----------------------------------------------------------------------*
		private object mPropertyValue = null;
		/// <summary>
		/// Get/Set the value of this property at the specified frame.
		/// </summary>
		public object PropertyValue
		{
			get { return mPropertyValue; }
			set { mPropertyValue = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
