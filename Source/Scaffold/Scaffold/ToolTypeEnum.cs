//	ToolTypeEnum.cs
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
	//*	ToolTypeEnum																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Enumeration of tool types currently available.
	/// </summary>
	public enum ToolTypeEnum
	{
		/// <summary>
		/// No tool selected, unknown, or undefined.
		/// </summary>
		None = 0,
		/// <summary>
		/// Start node.
		/// </summary>
		NodeStart,
		/// <summary>
		/// Fork node.
		/// </summary>
		NodeFork,
		/// <summary>
		/// Delay and continue node.
		/// </summary>
		NodeDelay,
		/// <summary>
		/// Termination node.
		/// </summary>
		NodeTermination
	}
	//*-------------------------------------------------------------------------*
}
