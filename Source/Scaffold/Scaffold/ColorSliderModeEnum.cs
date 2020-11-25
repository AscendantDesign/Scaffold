//	ColorSliderModeEnum.cs
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
	//*	ColorSliderModeEnum																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Enumeration of color slider modes.
	/// </summary>
	public enum ColorSliderModeEnum
	{
		/// <summary>
		/// No mode defined or unknown.
		/// </summary>
		None = 0,
		/// <summary>
		/// Red / Green / Blue.
		/// </summary>
		RGB,
		/// <summary>
		/// Hue / Saturation / Light.
		/// </summary>
		HSL
	}
	//*-------------------------------------------------------------------------*
}
