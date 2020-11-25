//	ColorSliderChannelEnum.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	ColorSliderChannelEnum																									*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Enumeration of possible channel modes for the color slider.
	/// </summary>
	public enum ColorSliderChannelEnum
	{
		/// <summary>
		/// No mode set or unknown.
		/// </summary>
		None = 0,
		/// <summary>
		/// Hue channel.
		/// </summary>
		Hue,
		/// <summary>
		/// Saturation channel.
		/// </summary>
		Saturation,
		/// <summary>
		/// Lumunance channel.
		/// </summary>
		Luminance,
		/// <summary>
		/// Red channel.
		/// </summary>
		Red,
		/// <summary>
		/// Green channel.
		/// </summary>
		Green,
		/// <summary>
		/// Blue channel.
		/// </summary>
		Blue,
		/// <summary>
		/// Alpha channel.
		/// </summary>
		Alpha
	}
	//*-------------------------------------------------------------------------*
}
