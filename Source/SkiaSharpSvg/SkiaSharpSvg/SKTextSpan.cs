//	SKTextSpan.cs
//	Copyright (c) 2017 Xamarin, Inc.
//	This file is a part of the library SkiaSharp.Extended.Svg.
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using SkiaSharp;

namespace SkiaSharpSvg
{
	//*-------------------------------------------------------------------------*
	//*	SKTextSpan																															*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Text span handler.
	/// </summary>
	internal class SKTextSpan
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
		/// Create a new instance of the SKTextSpan Item.
		/// </summary>
		/// <param name="text">
		/// Text content.
		/// </param>
		/// <param name="fill">
		/// Bucket fill style.
		/// </param>
		/// <param name="x">
		/// X coordinate of the corner.
		/// </param>
		/// <param name="y">
		/// Y coordinate of the corner.
		/// </param>
		/// <param name="baselineShift">
		/// Baseline shift amount, in user units.
		/// </param>
		public SKTextSpan(string text, SKPaint fill,
			float? x = null, float? y = null, float? baselineShift = null)
		{
			Text = text;
			Fill = fill;
			X = x;
			Y = y;
			BaselineShift = baselineShift;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* BaselineShift																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the baseline shift amount, in user units.
		/// </summary>
		public float? BaselineShift { get; }
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Fill																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the bucket fill style.
		/// </summary>
		public SKPaint Fill { get; }
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* MeasureTextWidth																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the text width measurement, in user units.
		/// </summary>
		/// <returns>
		/// Width of text content, in user units.
		/// </returns>
		public float MeasureTextWidth() => Fill.MeasureText(Text);
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Text																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the text content of this item.
		/// </summary>
		public string Text { get; }
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* X																																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the X coordinate of the corner of this item.
		/// </summary>
		public float? X { get; }
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Y																																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the Y coordinate of the corner of this item.
		/// </summary>
		public float? Y { get; }
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
