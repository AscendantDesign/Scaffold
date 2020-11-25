//	SvgAnimationInterpolation.cs
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
	//*	SvgAnimationInterpolationCollection																			*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of SvgAnimationInterpolationItem Items.
	/// </summary>
	public class SvgAnimationInterpolationCollection :
		List<SvgAnimationInterpolationItem>
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
		/// Clone the values of the source collection into the target collection.
		/// </summary>
		/// <param name="source">
		/// </param>
		/// <param name="target">
		/// </param>
		public static void Clone(SvgAnimationInterpolationCollection source,
			SvgAnimationInterpolationCollection target)
		{
			if(target != null)
			{
				target.Clear();
			}
			if(source?.Count > 0 && target != null)
			{
				foreach(SvgAnimationInterpolationItem item in source)
				{
					target.Add(new SvgAnimationInterpolationItem(item));
				}
			}
		}
		//*-----------------------------------------------------------------------*



	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SvgAnimationInterpolationItem																						*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Definition of a single interpolation in a context.
	/// </summary>
	public class SvgAnimationInterpolationItem
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
		/// Create a new instance of the SvgAnimationInterpolationItem Item.
		/// </summary>
		public SvgAnimationInterpolationItem()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the SvgAnimationInterpolationItem Item.
		/// </summary>
		/// <param name="source">
		/// </param>
		public SvgAnimationInterpolationItem(SvgAnimationInterpolationItem source)
		{
			if(source != null)
			{
				mDescription = source.mDescription;
				mInterpolationCount = source.mInterpolationCount;
				mInterpolationProperty = source.mInterpolationProperty;
				mInterpolationType = source.mInterpolationType;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Description																														*
		//*-----------------------------------------------------------------------*
		private string mDescription = "";
		/// <summary>
		/// Get/Set a brief description of this interpolation task.
		/// </summary>
		[JsonProperty(Order = 0)]
		public string Description
		{
			get { return mDescription; }
			set { mDescription = value; }
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
		[JsonProperty(Order = 3)]
		public int InterpolationCount
		{
			get { return mInterpolationCount; }
			set { mInterpolationCount = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	InterpolationProperty																									*
		//*-----------------------------------------------------------------------*
		private string mInterpolationProperty = "";
		/// <summary>
		/// Get/Set the name of the character property being interpolated.
		/// </summary>
		[JsonProperty(Order = 1)]
		public string InterpolationProperty
		{
			get { return mInterpolationProperty; }
			set { mInterpolationProperty = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	InterpolationType																											*
		//*-----------------------------------------------------------------------*
		private SvgAnimationInterpolationTypeEnum mInterpolationType =
			SvgAnimationInterpolationTypeEnum.None;
		/// <summary>
		/// Get/Set the type of interpolation used for this task.
		/// </summary>
		[JsonProperty(Order = 2)]
		public SvgAnimationInterpolationTypeEnum InterpolationType
		{
			get { return mInterpolationType; }
			set { mInterpolationType = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
