//	AlignmentTypeEnum.cs
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
	//*	AlignmentTypeEnum																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Enumeration of possible alignment types.
	/// </summary>
	public enum AlignmentTypeEnum
	{
		/// <summary>
		/// No alignment specified or unknown.
		/// </summary>
		None = 0,
		/// <summary>
		/// Align all specified elements to a single top location.
		/// </summary>
		Top,
		/// <summary>
		/// Align the vertical middle of all specified elements on a single
		/// location.
		/// </summary>
		Middle,
		/// <summary>
		/// Align the specified elements to a single bottom location.
		/// </summary>
		Bottom,
		/// <summary>
		/// Align the left coordinate of all specified elements to a single
		/// location.
		/// </summary>
		Left,
		/// <summary>
		/// Align the horizontal centers of all specified elements on a single
		/// location.
		/// </summary>
		Center,
		/// <summary>
		/// Align the right coordinate of all specified elements to a single
		/// location.
		/// </summary>
		Right
	}
	//*-------------------------------------------------------------------------*
}
