//	ColorEvents.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	ColorEventArgs																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Color event arguments.
	/// </summary>
	public class ColorEventArgs : EventArgs
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
		/// Create a new instance of the ColorEventArgs Item.
		/// </summary>
		public ColorEventArgs()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the ColorEventArgs Item.
		/// </summary>
		/// <param name="originalValue">
		/// The original color.
		/// </param>
		/// <param name="newValue">
		/// The new color.
		/// </param>
		public ColorEventArgs(Color originalValue, Color newValue)
		{
			mNewValue = newValue;
			mOriginalValue = originalValue;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NewValue																															*
		//*-----------------------------------------------------------------------*
		private Color mNewValue = Color.Empty;
		/// <summary>
		/// Get/Set the new color value.
		/// </summary>
		public Color NewValue
		{
			get { return mNewValue; }
			set { mNewValue = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	OriginalValue																													*
		//*-----------------------------------------------------------------------*
		private Color mOriginalValue = Color.Empty;
		/// <summary>
		/// Get/Set the original color value.
		/// </summary>
		public Color OriginalValue
		{
			get { return mOriginalValue; }
			set { mOriginalValue = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//* ColorEventHandler																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event handler for color values.
	/// </summary>
	/// <param name="sender">
	/// The object raising this event.
	/// </param>
	/// <param name="e">
	/// Color event arguments.
	/// </param>
	public delegate void ColorEventHandler(object sender, ColorEventArgs e);
	//*-------------------------------------------------------------------------*
}
