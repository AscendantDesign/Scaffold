//	ColorToken.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Scaffold.ScaffoldUtil;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	ColorToken																															*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// A token of a color that can be passed to different parts of the
	/// monitoring and tracking system without changing the values at all.
	/// </summary>
	/// <remarks>
	/// <para>When converting between color systems there are most often small
	/// amounts of error that eventually end up offsetting the entire balance.
	/// Passing this token, instead of a System.Drawing.Color, allows the system
	/// to tolerate a small amount of numeric distortion without altering the
	/// selected value.</para>
	/// <para>When using this token, the channels for the current mode are
	/// maintained when changing only the value of a single channel. In other
	/// words, in HSL mode, when only Hue is changed, the values of Saturation
	/// and Luminance are not changed, although the current Color is updated.
	/// </para>
	/// <para>Action subscribers to this token, such as sliders and controls,
	/// should only check the values specifically related to their current
	/// channels when determining whether to update their settings.
	/// </para>
	/// </remarks>
	public class ColorToken
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* UpdateRGB																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the Red, Green, Blue, and Alpha properties from the current
		/// color base.
		/// </summary>
		private void UpdateRGB()
		{
			mRed = mColor.R;
			mGreen = mColor.G;
			mBlue = mColor.B;
			mAlpha = mColor.A;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UpdateHSL																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the Hue, Saturation, and Luminance properties from the current
		/// color base.
		/// </summary>
		private void UpdateHSL()
		{
			mHue = mColor.GetHue();
			mSaturation = mColor.GetSaturation();
			mLuminance = mColor.GetBrightness();
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnColorChange																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the ColorChange event when the base color has been changed.
		/// </summary>
		/// <param name="e">
		/// Color event arguments.
		/// </param>
		protected virtual void OnColorChange(ColorEventArgs e)
		{
			ColorChange?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	Alpha																																	*
		//*-----------------------------------------------------------------------*
		private int mAlpha = 0;
		/// <summary>
		/// Get the alpha value of the color.
		/// </summary>
		public int Alpha
		{
			get { return mAlpha; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Blue																																	*
		//*-----------------------------------------------------------------------*
		private int mBlue = 0;
		/// <summary>
		/// Get the blue level of the current color.
		/// </summary>
		public int Blue
		{
			get { return mBlue; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Color																																	*
		//*-----------------------------------------------------------------------*
		private System.Drawing.Color mColor = System.Drawing.Color.Empty;
		/// <summary>
		/// Get/Set the reference color.
		/// </summary>
		/// <remarks>
		/// This is the actual reference color upon which all other output values
		/// are derived. This value is not changed until a new reference color is
		/// selected. All operations on this object are in reference to ARGB.
		/// </remarks>
		public System.Drawing.Color Color
		{
			get { return mColor; }
			set
			{
				mColor = value;
				UpdateRGB();
				UpdateHSL();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ColorChange																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the base color has been changed.
		/// </summary>
		public event ColorEventHandler ColorChange;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ColorMode																															*
		//*-----------------------------------------------------------------------*
		private ColorSliderModeEnum mColorMode = ColorSliderModeEnum.HSL;
		/// <summary>
		/// Get/Set the current color operating mode of this token.
		/// </summary>
		public ColorSliderModeEnum ColorMode
		{
			get { return mColorMode; }
			set { mColorMode = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Green																																	*
		//*-----------------------------------------------------------------------*
		private int mGreen = 0;
		/// <summary>
		/// Get the green level of the current color.
		/// </summary>
		public int Green
		{
			get { return mGreen; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Hue																																		*
		//*-----------------------------------------------------------------------*
		private float mHue = 0f;
		/// <summary>
		/// Get the hue value, in degrees.
		/// </summary>
		public float Hue
		{
			get { return mHue; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ModifyChannel																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Modify one channel of the color without changing any of the others.
		/// </summary>
		/// <param name="alpha">
		/// Alpha level. 0 to 255.
		/// </param>
		/// <param name="red">
		/// Red gun level. 0 to 255.
		/// </param>
		/// <param name="green">
		/// Green gun level. 0 to 255.
		/// </param>
		/// <param name="blue">
		/// Blue gun level. 0 to 255.
		/// </param>
		/// <param name="hue">
		/// Hue position. 0.0 to 360.0.
		/// </param>
		/// <param name="saturation">
		/// Saturation level. 0.0 to 1.0.
		/// </param>
		/// <param name="luminance">
		/// Luminance level. 0.0 to 1.0.
		/// </param>
		/// <remarks>
		/// When making a single channel change in RGB mode, the color changes will
		/// always match up perfectly. However, when making a single channel change
		/// in HSL mode, the color changes will almost never match up perfectly.
		/// </remarks>
		public void ModifyChannel(int alpha = -1, int red = -1, int green = -1,
			int blue = -1, float hue = -1f, float saturation = -1f,
			float luminance = -1f)
		{
			if(alpha != -1 || red != -1 || green != -1 || blue != -1)
			{
				//	Straight coversion. No work needed.
				if(alpha != -1)
				{
					mAlpha = alpha;
				}
				else if(red != -1)
				{
					mRed = red;
				}
				else if(green != -1)
				{
					mGreen = green;
				}
				else if(blue != -1)
				{
					mBlue = blue;
				}
				mColor = System.Drawing.Color.FromArgb(mAlpha, mRed, mGreen, mBlue);
				if(mColorMode == ColorSliderModeEnum.RGB)
				{
					UpdateHSL();
				}
				OnColorChange(new ColorEventArgs(System.Drawing.Color.Empty, mColor));
			}
			else if(hue != -1f || saturation != -1f || luminance != -1f)
			{
				if(hue != -1f)
				{
					mHue = hue;
				}
				else if(saturation != -1f)
				{
					mSaturation = saturation;
					//	If luminance is locked, unlock it.
					if(mLuminance == 0f && mSaturation != 0f)
					{
						mLuminance = mSaturation;
					}
				}
				else if(luminance != -1f)
				{
					mLuminance = luminance;
					//	If saturation is locked, unlock it.
					if(mSaturation == 0f && mLuminance != 0f)
					{
						mSaturation = mLuminance;
					}
				}
				mColor = FromHSL(mHue, mSaturation, mLuminance, mAlpha);
				if(mColorMode == ColorSliderModeEnum.HSL)
				{
					UpdateRGB();
				}
				OnColorChange(new ColorEventArgs(System.Drawing.Color.Empty, mColor));
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Luminance																															*
		//*-----------------------------------------------------------------------*
		private float mLuminance = 0f;
		/// <summary>
		/// Get the luminance of the currently selected color.
		/// </summary>
		public float Luminance
		{
			get { return mLuminance; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Red																																		*
		//*-----------------------------------------------------------------------*
		private int mRed = 0;
		/// <summary>
		/// Get the red level of the current color.
		/// </summary>
		public int Red
		{
			get { return mRed; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Saturation																														*
		//*-----------------------------------------------------------------------*
		private float mSaturation = 0f;
		/// <summary>
		/// Get the saturation channel value of the current color.
		/// </summary>
		public float Saturation
		{
			get { return mSaturation; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
