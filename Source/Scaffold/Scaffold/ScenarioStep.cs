//	ScenarioStep.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	ScenarioStepItem																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// A single step within a scenario.
	/// </summary>
	public class ScenarioStepItem
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
		//*	Answers																																*
		//*-----------------------------------------------------------------------*
		private ObservableCollection<AnswerItem> mAnswers =
			new ObservableCollection<AnswerItem>();
		/// <summary>
		/// Get/Set a reference to the list of answers available for this question.
		/// </summary>
		public ObservableCollection<AnswerItem> Answers
		{
			get { return mAnswers; }
			set { mAnswers = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	BackgroundColor																												*
		//*-----------------------------------------------------------------------*
		private Color mBackgroundColor = Color.Empty;
		/// <summary>
		/// Get/Set the background color to assign to a node with this step.
		/// </summary>
		public Color BackgroundColor
		{
			get { return mBackgroundColor; }
			set { mBackgroundColor = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Question																															*
		//*-----------------------------------------------------------------------*
		private QuestionItem mQuestion = new QuestionItem();
		/// <summary>
		/// Get/Set a reference to the question to be asked on this step.
		/// </summary>
		public QuestionItem Question
		{
			get { return mQuestion; }
			set { mQuestion = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SlideIndex																														*
		//*-----------------------------------------------------------------------*
		private int mSlideIndex = 0;
		/// <summary>
		/// Get/Set the index of the slide to which this question is related.
		/// </summary>
		public int SlideIndex
		{
			get { return mSlideIndex; }
			set { mSlideIndex = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SlideLocation																													*
		//*-----------------------------------------------------------------------*
		private Point mSlideLocation = new Point(0, 0);
		/// <summary>
		/// Get/Set the target location of this item upon the slide.
		/// </summary>
		public Point SlideLocation
		{
			get { return mSlideLocation; }
			set { mSlideLocation = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Sources																																*
		//*-----------------------------------------------------------------------*
		private ObservableCollection<ScenarioStepItem> mSources =
			new ObservableCollection<ScenarioStepItem>();
		/// <summary>
		/// Get a reference to the list of sources from which this step is
		/// branching.
		/// </summary>
		public ObservableCollection<ScenarioStepItem> Sources
		{
			get { return mSources; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	StepName																															*
		//*-----------------------------------------------------------------------*
		private string mStepName = "";
		/// <summary>
		/// Get/Set the unique name of the step within the decision tree.
		/// </summary>
		public string StepName
		{
			get { return mStepName; }
			set { mStepName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ToolTip																																*
		//*-----------------------------------------------------------------------*
		private string mToolTip = "";
		/// <summary>
		/// Get/Set a hint or annotation to display when the mouse hovers over this
		/// object.
		/// </summary>
		public string ToolTip
		{
			get { return mToolTip; }
			set { mToolTip = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
