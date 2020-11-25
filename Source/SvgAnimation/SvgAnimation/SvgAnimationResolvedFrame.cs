//	SvgAnimationResolvedFrame.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkiaSharpSvg
{
	//*-------------------------------------------------------------------------*
	//*	SvgAnimationResolvedFrameCollection																			*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of SvgAnimationResolvedFrameItem Items.
	/// </summary>
	public class SvgAnimationResolvedFrameCollection :
		List<SvgAnimationResolvedFrameItem>
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
		//*	GetIndex																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a frame by its frame index.
		/// </summary>
		/// <param name="frameIndex">
		/// Frame index of the frame to retrieve.
		/// </param>
		/// <returns>
		/// Reference to the first animation frame having the specified frame
		/// index, if found. Otherwise, null.
		/// </returns>
		public SvgAnimationResolvedFrameItem GetIndex(int frameIndex)
		{
			SvgAnimationResolvedFrameItem result =
				this.FirstOrDefault(x => x.FrameIndex == frameIndex);
			return result;
		}
		//*-----------------------------------------------------------------------*



	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SvgAnimationResolvedFrameItem																						*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about a fully resolved animation frame.
	/// </summary>
	/// <remarks>
	/// The character / property information is combined, and has only a single
	/// value per entry.
	/// </remarks>
	public class SvgAnimationResolvedFrameItem
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
		//*	Description																														*
		//*-----------------------------------------------------------------------*
		private string mDescription = "";
		/// <summary>
		/// Get/Set the description of this item.
		/// </summary>
		public string Description
		{
			get { return mDescription; }
			set { mDescription = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Entries																																*
		//*-----------------------------------------------------------------------*
		private SvgAnimationCharacterFrameCollection mEntries =
			new SvgAnimationCharacterFrameCollection();
		/// <summary>
		/// Get a reference to the collection of unique character / property /
		/// value entries for this frame.
		/// </summary>
		public SvgAnimationCharacterFrameCollection Entries
		{
			get { return mEntries; }
			set { mEntries = value; }
		}
		//*-----------------------------------------------------------------------*

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

	}
	//*-------------------------------------------------------------------------*
}
