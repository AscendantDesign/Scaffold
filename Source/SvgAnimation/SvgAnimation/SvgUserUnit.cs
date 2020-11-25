//	SvgUserUnit.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharpSvg
{
	//*-------------------------------------------------------------------------*
	//*	SvgUserUnit																															*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Maintains user conversion metric for the current context.
	/// </summary>
	public class SvgUserUnit
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
		//*	SetUserUnitX																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the user unit X ratio.
		/// </summary>
		/// <param name="viewBoxWidth">
		/// Width parameter of the viewbox.
		/// </param>
		/// <param name="svgWidth">
		/// Width of the SVG object.
		/// </param>
		public void SetUserUnitX(double viewBoxWidth, double svgWidth)
		{
			if(svgWidth != 0.0)
			{
				mX = viewBoxWidth / svgWidth;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SetUserUnitY																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the user unit Y ratio.
		/// </summary>
		/// <param name="viewBoxHeight">
		/// Height parameter of the viewbox.
		/// </param>
		/// <param name="svgHeight">
		/// Height of the SVG object.
		/// </param>
		public void SetUserUnitY(double viewBoxHeight, double svgHeight)
		{
			if(svgHeight != 0.0)
			{
				mY = viewBoxHeight / svgHeight;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ToPixelX																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the pixel coordinate of the specified value.
		/// </summary>
		/// <param name="userCoordinate">
		/// User coordinate to convert.
		/// </param>
		/// <returns>
		/// Pixel coordinate related to the caller's user coordinate for the
		/// current user unit settings.
		/// </returns>
		public double ToPixelX(double userCoordinate)
		{
			double result = 0.0;

			if(mX != 0)
			{
				result = userCoordinate / mX;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ToPixelY																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the pixel coordinate of the specified value.
		/// </summary>
		/// <param name="userCoordinate">
		/// User coordinate to convert.
		/// </param>
		/// <returns>
		/// Pixel coordinate related to the caller's user coordinate for the
		/// current user unit settings.
		/// </returns>
		public double ToPixelY(double userCoordinate)
		{
			double result = 0.0;

			if(mY != 0)
			{
				result = userCoordinate / mY;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ToUserX																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the user coordinate related to the specified pixel value.
		/// </summary>
		/// <param name="pixelCoordinate">
		/// </param>
		/// <returns>
		/// </returns>
		public double ToUserX(double pixelCoordinate)
		{
			double result = pixelCoordinate * mX;
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ToUserY																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the user coordinate related to the specified pixel value.
		/// </summary>
		/// <param name="pixelCoordinate">
		/// </param>
		/// <returns>
		/// </returns>
		public double ToUserY(double pixelCoordinate)
		{
			double result = pixelCoordinate * mY;
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	X																																			*
		//*-----------------------------------------------------------------------*
		private double mX = 1.0;
		/// <summary>
		/// Get/Set the X user unit ratio.
		/// </summary>
		public double X
		{
			get { return mX; }
			set { mX = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Y																																			*
		//*-----------------------------------------------------------------------*
		private double mY = 1.0;
		/// <summary>
		/// Get/Set the Y user unit ratio.
		/// </summary>
		public double Y
		{
			get { return mY; }
			set { mY = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
