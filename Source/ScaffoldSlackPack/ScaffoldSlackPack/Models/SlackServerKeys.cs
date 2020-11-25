//	SlackServerKeys.cs
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
	//*	SlackServerKeys																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Token values needed by Slack for server-to-server interaction.
	/// </summary>
	public class SlackServerKeys
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	SlackBotToken																													*
		//*-----------------------------------------------------------------------*
		private string mSlackBotToken = "";
		/// <summary>
		/// Get/Set the active Slack bot token for the app.
		/// </summary>
		public string SlackBotToken
		{
			get { return mSlackBotToken; }
			set { mSlackBotToken = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SlackTeamToken																												*
		//*-----------------------------------------------------------------------*
		private string mSlackTeamToken = "";
		/// <summary>
		/// Get/Set the active Slack team token for the app.
		/// </summary>
		public string SlackTeamToken
		{
			get { return mSlackTeamToken; }
			set { mSlackTeamToken = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

}
