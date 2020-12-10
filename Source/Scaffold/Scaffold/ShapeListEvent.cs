using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	ShapeListEventArgs																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event arguments for a list of shapes.
	/// </summary>
	public class ShapeListEventArgs : EventArgs
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
		/// Create a new instance of the ShapeListEventArgs Item.
		/// </summary>
		public ShapeListEventArgs()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the ShapeListEventArgs Item.
		/// </summary>
		public ShapeListEventArgs(int slideIndex)
		{
			mSlideIndex = slideIndex;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Handled																																*
		//*-----------------------------------------------------------------------*
		private bool mHandled = false;
		/// <summary>
		/// Get/Set a value indicating whether the event was handled.
		/// </summary>
		public bool Handled
		{
			get { return mHandled; }
			set { mHandled = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Names																																	*
		//*-----------------------------------------------------------------------*
		private List<string> mNames = new List<string>();
		/// <summary>
		/// Get a reference to the list of names.
		/// </summary>
		public List<string> Names
		{
			get { return mNames; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ShapeTypes																														*
		//*-----------------------------------------------------------------------*
		private List<string> mShapeTypes = new List<string>();
		/// <summary>
		/// Get a reference to the collection shape types associated with the
		/// names.
		/// </summary>
		public List<string> ShapeTypes
		{
			get { return mShapeTypes; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SlideIndex																														*
		//*-----------------------------------------------------------------------*
		private int mSlideIndex = 0;
		/// <summary>
		/// Get/Set the slide index.
		/// </summary>
		public int SlideIndex
		{
			get { return mSlideIndex; }
			set { mSlideIndex = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SlideIndexValid																												*
		//*-----------------------------------------------------------------------*
		private bool mSlideIndexValid = true;
		/// <summary>
		/// Get/Set a value indicating whether the specified slide index is valid.
		/// </summary>
		/// <remarks>
		/// Typically used for a slide request. The listener will set this value
		/// to false if the specified slide did not exist.
		/// </remarks>
		public bool SlideIndexValid
		{
			get { return mSlideIndexValid; }
			set { mSlideIndexValid = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//* ShapeListEventHandler																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event handler that processes information about shapes on a slide.
	/// </summary>
	/// <param name="sender">
	/// The object raising this event.
	/// </param>
	/// <param name="e">
	/// Shape list event arguments.
	/// </param>
	public delegate void ShapeListEventHandler(object sender,
		ShapeListEventArgs e);
	//*-------------------------------------------------------------------------*

}
