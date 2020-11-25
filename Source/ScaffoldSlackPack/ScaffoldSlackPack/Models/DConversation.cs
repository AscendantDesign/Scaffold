//	DConversation.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.

using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScaffoldSlackPack.Models
{
	//*-------------------------------------------------------------------------*
	//*	DConversationCollection																									*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of DConversationItem Items.
	/// </summary>
	public class DConversationCollection : List<DConversationItem>
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


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	DConversationItem																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Individual conversation record in the database.
	/// </summary>
	public class DConversationItem
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
		//* LoadFromDatabase																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Load a conversation record from the database by conversation ticket.
		/// </summary>
		/// <param name="conversationTicket">
		/// Globally unique identification of the conversation to load.
		/// </param>
		/// <returns>
		/// Reference to the object representation of the specified conversation.
		/// </returns>
		public static DConversationItem LoadFromDatabase(string conversationTicket)
		{
			SqliteCommand command = null;
			DConversationItem conversation = null;
			SqliteDataReader reader = null;

			if(conversationTicket?.Length > 0)
			{
				using(SqliteConnection connection =
					new SqliteConnection("Data Source = Data/ScaffoldSlackPack.db"))
				{
					connection.Open();
					command = connection.CreateCommand();
					command.CommandText =
						String.Format(
							ResourceMain.sqlConversationSelect, conversationTicket);
					reader = command.ExecuteReader();
					if(reader.Read())
					{
						conversation = new DConversationItem();
						conversation.ConversationDescription =
							reader.GetString(reader.GetOrdinal("ConversationDescription"));
						conversation.ConversationTicket =
							reader.GetString(reader.GetOrdinal("ConversationTicket"));
						conversation.ConversationTitle =
							reader.GetString(reader.GetOrdinal("ConversationTitle"));
					}
					reader.DisposeAsync();
				}
			}
			return conversation;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ConversationDescription																								*
		//*-----------------------------------------------------------------------*
		private string mConversationDescription = "";
		/// <summary>
		/// Get/Set a brief description of the conversation.
		/// </summary>
		public string ConversationDescription
		{
			get { return mConversationDescription; }
			set { mConversationDescription = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ConversationTicket																										*
		//*-----------------------------------------------------------------------*
		private string mConversationTicket = "";
		/// <summary>
		/// Get/Set the unique identification of this record.
		/// </summary>
		public string ConversationTicket
		{
			get { return mConversationTicket; }
			set { mConversationTicket = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ConversationTitle																											*
		//*-----------------------------------------------------------------------*
		private string mConversationTitle = "";
		/// <summary>
		/// Get/Set the title of this conversation record.
		/// </summary>
		public string ConversationTitle
		{
			get { return mConversationTitle; }
			set { mConversationTitle = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
