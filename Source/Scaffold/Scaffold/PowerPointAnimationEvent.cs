//	PowerPointAnimationEvent.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Office.Interop.PowerPoint;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	PowerPointAnimationEventArgs																						*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event arguments used while processing PowerPoint events.
	/// </summary>
	public class PowerPointAnimationEventArgs : EventArgs
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
		//*	Effect																																*
		//*-----------------------------------------------------------------------*
		private MsoAnimEffect? mEffect = null;
		/// <summary>
		/// Get/Set the active effect.
		/// </summary>
		public MsoAnimEffect? Effect
		{
			get { return mEffect; }
			set { mEffect = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	EffectDirection																												*
		//*-----------------------------------------------------------------------*
		private MsoAnimDirection? mEffectDirection = null;
		/// <summary>
		/// Get/Set the direction applied to the effect, if any.
		/// </summary>
		public MsoAnimDirection? EffectDirection
		{
			get { return mEffectDirection; }
			set { mEffectDirection = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	EffectDuration																												*
		//*-----------------------------------------------------------------------*
		private float mEffectDuration = 0f;
		/// <summary>
		/// Get/Set the duration of the effect.
		/// </summary>
		public float EffectDuration
		{
			get { return mEffectDuration; }
			set { mEffectDuration = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	EffectExit																														*
		//*-----------------------------------------------------------------------*
		private bool mEffectExit = false;
		/// <summary>
		/// Get/Set a value indicating whether this effect is exiting the slide.
		/// </summary>
		public bool EffectExit
		{
			get { return mEffectExit; }
			set { mEffectExit = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SlideIndex																														*
		//*-----------------------------------------------------------------------*
		private int mSlideIndex = 0;
		/// <summary>
		/// Get/Set the ordinal index of the slide within the active presentation.
		/// </summary>
		public int SlideIndex
		{
			get { return mSlideIndex; }
			set { mSlideIndex = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//* PowerPointAnimationEventHandler																					*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Handler for processing animation events in PowerPoint.
	/// </summary>
	/// <param name="sender">
	/// The object raising this event.
	/// </param>
	/// <param name="e">
	/// PowerPoint animation event arguments.
	/// </param>
	public delegate void PowerPointAnimationEventHandler(object sender,
		PowerPointAnimationEventArgs e);
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	PowerPointQuickAnimationEventArgs																				*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event arguments used while processing PowerPoint quick animation events.
	/// </summary>
	public class PowerPointQuickAnimationEventArgs :
		PowerPointAnimationEventArgs
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
		//*	NextDelayTime																													*
		//*-----------------------------------------------------------------------*
		private float mNextDelayTime = 0f;
		/// <summary>
		/// Get/Set the delay time to the next item.
		/// </summary>
		public float NextDelayTime
		{
			get { return mNextDelayTime; }
			set { mNextDelayTime = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NextDelayType																													*
		//*-----------------------------------------------------------------------*
		private EventDelayTypeEnum mNextDelayType = EventDelayTypeEnum.None;
		/// <summary>
		/// Get/Set the type of delay to use between multiple items.
		/// </summary>
		public EventDelayTypeEnum NextDelayType
		{
			get { return mNextDelayType; }
			set { mNextDelayType = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ShapeNames																														*
		//*-----------------------------------------------------------------------*
		private List<string> mShapeNames = new List<string>();
		/// <summary>
		/// Get/Set a reference to the list of shapes to animate, pre-sorted in
		/// the order of execution.
		/// </summary>
		public List<string> ShapeNames
		{
			get { return mShapeNames; }
			set { mShapeNames = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	StartDelayTime																												*
		//*-----------------------------------------------------------------------*
		private float mStartDelayTime = 0f;
		/// <summary>
		/// Get/Set the time to delay before starting the sequence.
		/// </summary>
		public float StartDelayTime
		{
			get { return mStartDelayTime; }
			set { mStartDelayTime = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	StartDelayType																												*
		//*-----------------------------------------------------------------------*
		private EventDelayTypeEnum mStartDelayType = EventDelayTypeEnum.None;
		/// <summary>
		/// Get/Set the type of delay to use before starting the sequence.
		/// </summary>
		public EventDelayTypeEnum StartDelayType
		{
			get { return mStartDelayType; }
			set { mStartDelayType = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//* PowerPointQuickAnimationEventHandler																		*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Handler for processing quick animation events in PowerPoint.
	/// </summary>
	/// <param name="sender">
	/// The object raising this event.
	/// </param>
	/// <param name="e">
	/// PowerPoint quick animation event arguments.
	/// </param>
	public delegate void PowerPointQuickAnimationEventHandler(object sender,
		PowerPointQuickAnimationEventArgs e);
	//*-------------------------------------------------------------------------*

}
