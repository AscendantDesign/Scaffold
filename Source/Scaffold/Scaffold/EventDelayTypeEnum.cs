//	EventDelayTypeEnum.cs
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
	//*	EventDelayTypeEnum																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Enumeration of event delay types.
	/// </summary>
	public enum EventDelayTypeEnum
	{
		/// <summary>
		/// No delay specified or unknown.
		/// </summary>
		None = 0,
		/// <summary>
		/// Continue after click.
		/// </summary>
		AfterClick,
		/// <summary>
		/// Continue after delay time.
		/// </summary>
		AfterDelay,
		/// <summary>
		/// Continue immediately.
		/// </summary>
		Immediately
	}
	//*-------------------------------------------------------------------------*
}
