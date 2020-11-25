//	SocketModeEnum.cs
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
	//*	SocketModeEnum																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Enumeration of possible socket types.
	/// </summary>
	public enum SocketModeEnum
	{
		/// <summary>
		/// No type assigned or unknown.
		/// </summary>
		None = 0,
		/// <summary>
		/// Input direction.
		/// </summary>
		Input,
		/// <summary>
		/// Output direction.
		/// </summary>
		Output
	}
	//*-------------------------------------------------------------------------*
}
