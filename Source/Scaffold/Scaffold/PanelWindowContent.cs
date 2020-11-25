//	PanelWindowContent.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	PanelWindowContent																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Content definition for a panel window pane.
	/// </summary>
	public class PanelWindowContent
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
		//*	AutoSize																															*
		//*-----------------------------------------------------------------------*
		private bool mAutoSize = false;
		/// <summary>
		/// Get/Set a value indicating whether the pane will be automatically sized
		/// to that of the controls.
		/// </summary>
		public bool AutoSize
		{
			get { return mAutoSize; }
			set { mAutoSize = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Controls																															*
		//*-----------------------------------------------------------------------*
		private List<PanelWindowContentControl> mControls =
			new List<PanelWindowContentControl>();
		/// <summary>
		/// Get a reference to the list of control definitions for this panel.
		/// </summary>
		public List<PanelWindowContentControl> Controls
		{
			get { return mControls; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Deserialize																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the deserialized object representation of the JSON string.
		/// </summary>
		/// <param name="content">
		/// JSON content representing PanelWindowContent structure.
		/// </param>
		public static PanelWindowContent Deserialize(string content)
		{
			PanelWindowContent result =
				JsonConvert.DeserializeObject<PanelWindowContent>(content);
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Margin																																*
		//*-----------------------------------------------------------------------*
		private int mMargin = 0;
		/// <summary>
		/// Get/Set the margin of the panel away from the child controls if
		/// AutoSize = true.
		/// </summary>
		public int Margin
		{
			get { return mMargin; }
			set { mMargin = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MaxExtent																															*
		//*-----------------------------------------------------------------------*
		private int mMaxExtent = 0;
		/// <summary>
		/// Get/Set the maximum extent of panel space seen by controls on this
		/// panel.
		/// </summary>
		public int MaxExtent
		{
			get { return mMaxExtent; }
			set { mMaxExtent = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Name																																	*
		//*-----------------------------------------------------------------------*
		private string mName = "";
		/// <summary>
		/// Get/Set the name of the control to load from the host form.
		/// </summary>
		public string Name
		{
			get { return mName; }
			set { mName = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	PanelWindowContentControl																								*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Definition for an individual control on a panel window content pane.
	/// </summary>
	public class PanelWindowContentControl
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
		//*	Dock																																	*
		//*-----------------------------------------------------------------------*
		private string mDock = "None";
		/// <summary>
		/// Get/Set the type of docking to use on this child control.
		/// </summary>
		public string Dock
		{
			get { return mDock; }
			set { mDock = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Name																																	*
		//*-----------------------------------------------------------------------*
		private string mName = "";
		/// <summary>
		/// Get/Set the name of the control to load from the host form.
		/// </summary>
		public string Name
		{
			get { return mName; }
			set { mName = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*



}
