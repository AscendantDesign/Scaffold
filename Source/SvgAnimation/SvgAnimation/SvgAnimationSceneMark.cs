//	SvgAnimationSceneMark.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharpSvg
{
	//*-------------------------------------------------------------------------*
	//*	SvgAnimationSceneMarkCollection																					*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of SvgAnimationSceneMarkItem Items.
	/// </summary>
	public class SvgAnimationSceneMarkCollection :
		List<SvgAnimationSceneMarkItem>
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
		public static void Clone(SvgAnimationSceneMarkCollection source,
			SvgAnimationSceneMarkCollection target)
		{
			if(target != null)
			{
				target.Clear();
			}
			if(source?.Count > 0 && target != null)
			{
				foreach(SvgAnimationSceneMarkItem item in source)
				{
					target.Add(new SvgAnimationSceneMarkItem(item));
				}
			}
		}
		//*-----------------------------------------------------------------------*



	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SvgAnimationSceneMarkItem																								*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Definition of an individual scene mark.
	/// </summary>
	public class SvgAnimationSceneMarkItem
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
		/// Create a new instance of the SvgAnimationSceneMarkItem Item.
		/// </summary>
		public SvgAnimationSceneMarkItem()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the SvgAnimationSceneMarkItem Item.
		/// </summary>
		public SvgAnimationSceneMarkItem(SvgAnimationSceneMarkItem source)
		{
			if(source != null)
			{
				mDescription = source.mDescription;
				mMarkName = source.mMarkName;
				SvgAnimationSceneMarkCharacterCollection.Clone(
					source.mMarkSettings, mMarkSettings);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Description																														*
		//*-----------------------------------------------------------------------*
		private string mDescription = "";
		/// <summary>
		/// Get/Set a brief description of this mark.
		/// </summary>
		[JsonProperty(Order = 0)]
		public string Description
		{
			get { return mDescription; }
			set { mDescription = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MarkName																															*
		//*-----------------------------------------------------------------------*
		private string mMarkName = "";
		/// <summary>
		/// Get/Set the name of the scene mark.
		/// </summary>
		[JsonProperty(Order = 1)]
		public string MarkName
		{
			get { return mMarkName; }
			set { mMarkName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MarkCharacters																												*
		//*-----------------------------------------------------------------------*
		private SvgAnimationSceneMarkCharacterCollection mMarkSettings =
			new SvgAnimationSceneMarkCharacterCollection();
		/// <summary>
		/// Get a reference to the collection of settings on this scene mark.
		/// </summary>
		[JsonProperty(Order = 2)]
		public SvgAnimationSceneMarkCharacterCollection MarkCharacters
		{
			get { return mMarkSettings; }
			set { mMarkSettings = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
