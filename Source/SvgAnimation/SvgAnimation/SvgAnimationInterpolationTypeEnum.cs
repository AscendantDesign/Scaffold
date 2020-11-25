//	SvgAnimationInterpolationTypeEnum.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharpSvg
{
	//*-------------------------------------------------------------------------*
	//*	SvgAnimationInterpolationTypeEnum																				*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Enumeration of possible interpolation types.
	/// </summary>
	/// <remarks>
	/// In this version, LinearIn and LinearOut are measured types equal to
	/// CountIn and CountOut, respectively.
	/// </remarks>
	public enum SvgAnimationInterpolationTypeEnum
	{
		/// <summary>
		/// No interpolation type defined or unknown.
		/// </summary>
		None,
		/// <summary>
		/// Unmeasured linear motion between two frames.
		/// </summary>
		Linear,
		/// <summary>
		/// Linear motion in reference to the previous keyframe.
		/// </summary>
		LinearIn,
		/// <summary>
		/// Linear motion in reference to the next keyframe.
		/// </summary>
		LinearOut,
		/// <summary>
		/// Count of frames prior to the current keyframe.
		/// </summary>
		CountIn,
		/// <summary>
		/// Count of frames following the current keyframe.
		/// </summary>
		CountOut,
		/// <summary>
		/// Unmeasured bounce physics between two frames.
		/// </summary>
		Bounce,
		/// <summary>
		/// The item bounces into position.
		/// </summary>
		BounceIn,
		/// <summary>
		/// The item bounces out of position.
		/// </summary>
		BounceOut,
		/// <summary>
		/// The change occurs immediately.
		/// </summary>
		Immediate
	}
	//*-------------------------------------------------------------------------*
}
