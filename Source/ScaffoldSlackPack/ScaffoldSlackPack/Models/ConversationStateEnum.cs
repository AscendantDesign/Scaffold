//	ConversationStateEnum.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScaffoldSlackPack.Models
{
	//*-------------------------------------------------------------------------*
	//*	ConversationStateEnum																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Enumeration of possible conversation states.
	/// </summary>
	public enum ConversationStateEnum
	{
		/// <summary>
		/// Conversation unknown or undefined.
		/// </summary>
		None = 0,
		/// <summary>
		/// A conversation is currently underway.
		/// </summary>
		Active,
		/// <summary>
		/// The conversation is paused.
		/// </summary>
		Paused,
		/// <summary>
		/// The conversation has been completed.
		/// </summary>
		Completed
	}
	//*-------------------------------------------------------------------------*
}
