//	DistributionTypeEnum.cs
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
	//*	DistributionTypeEnum																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Enumeration of possible distribution types.
	/// </summary>
	public enum DistributionTypeEnum
	{
		/// <summary>
		/// No distribution type specified or unknown.
		/// </summary>
		None = 0,
		/// <summary>
		/// Evenly distribute the spaces between the specified elements along
		/// a horizontal line.
		/// </summary>
		Horizontal,
		/// <summary>
		/// Evenly distribute the spaces between the specified elements along
		/// a vertical line.
		/// </summary>
		Vertical
	}
	//*-------------------------------------------------------------------------*
}
