//	IconInfo.cs
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
	//*	IconInfo																																*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Transient information about custom dragging icon.
	/// </summary>
	public struct IconInfo
	{
		/// <summary>
		/// Whether the icon is floating.
		/// </summary>
		public bool fIcon;
		/// <summary>
		/// The X-coordinate of the hotspot.
		/// </summary>
		public int xHotspot;
		/// <summary>
		/// The Y-coordinate of the hotspot.
		/// </summary>
		public int yHotspot;
		/// <summary>
		/// Bitmap handle of the mask.
		/// </summary>
		public IntPtr hbmMask;
		/// <summary>
		/// Bitmap handle of the color.
		/// </summary>
		public IntPtr hbmColor;
	}
	//*-------------------------------------------------------------------------*
}
