//	AlignmentReferenceEnum.cs
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
	//*	AlignmentReferenceEnum																									*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Enumeration of possible alignment reference types.
	/// </summary>
	public enum AlignmentReferenceEnum
	{
		/// <summary>
		/// Alignment not specified or unknown.
		/// </summary>
		None = 0,
		/// <summary>
		/// Alignment will occur on an anchor object.
		/// </summary>
		Anchor,
		/// <summary>
		/// Alignment will be made using a specified left coordinate.
		/// </summary>
		LeftCoord,
		/// <summary>
		/// Alignment will be made using a specified top coordinate.
		/// </summary>
		TopCoord
	}
	//*-------------------------------------------------------------------------*
}
