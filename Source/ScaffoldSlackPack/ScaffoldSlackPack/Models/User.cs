//	User.cs
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
	//*	UserCollection																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of UserItem Items.
	/// </summary>
	public class UserCollection : List<UserItem>
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
		//* InsertIntoDatabase																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Insert a collection of user records into the database.
		/// </summary>
		/// <param name="users">
		/// Collection of users to insert.
		/// </param>
		public static void InsertIntoDatabase(UserCollection users)
		{
			SqliteCommand command = null;
			string commandText = ResourceMain.sqlUserItemInsertUnique;

			if(users?.Count > 0)
			{
				using(SqliteConnection connection =
					new SqliteConnection("Data Source = Data/ScaffoldSlackPack.db"))
				{
					connection.Open();
					command = connection.CreateCommand();
					foreach(UserItem user in users)
					{
						command.CommandText = String.Format(commandText,
							user.UserItemTicket, user.SlackID, user.SlackName);
						command.ExecuteNonQuery();
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	LoadFromDatabase																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read the user collection from the active database.
		/// </summary>
		/// <returns>
		/// Collection of UserItem records read from the database.
		/// </returns>
		public static UserCollection LoadFromDatabase()
		{
			SqliteCommand command = null;
			SqliteDataReader reader = null;
			UserItem user = null;
			UserCollection users = new UserCollection();

			using(SqliteConnection connection =
				new SqliteConnection("Data Source = Data/ScaffoldSlackPack.db"))
			{
				connection.Open();
				command = connection.CreateCommand();
				command.CommandText = ResourceMain.sqlUserItemSelectAll;
				reader = command.ExecuteReader();
				while(reader.Read())
				{
					user = new UserItem();
					user.RowID = reader.GetInt32(0);
					user.UserItemTicket =
						reader.GetString(reader.GetOrdinal("UserItemTicket"));
					user.SlackID = reader.GetString(reader.GetOrdinal("SlackID"));
					user.SlackName = reader.GetString(reader.GetOrdinal("SlackName"));
					users.Add(user);
				}
				reader.DisposeAsync();
			}
			return users;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UpdateUsernamesOnDatabase																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the username value in the database from a collection of user
		/// records.
		/// </summary>
		/// <param name="users">
		/// Collection of users to update.
		/// </param>
		public static void UpdateUsernamesOnDatabase(UserCollection users)
		{
			SqliteCommand command = null;
			string commandText = ResourceMain.sqlUserItemUpdateSlackNameFromSlackID;

			if(users?.Count > 0)
			{
				using(SqliteConnection connection =
					new SqliteConnection("Data Source = Data/ScaffoldSlackPack.db"))
				{
					connection.Open();
					command = connection.CreateCommand();
					foreach(UserItem user in users)
					{
						command.CommandText = String.Format(commandText,
							user.SlackName, user.SlackID);
						command.ExecuteNonQuery();
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*



	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	UserItem																																*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about the system user.
	/// </summary>
	public class UserItem
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
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the UserItem Item.
		/// </summary>
		public UserItem()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the UserItem Item.
		/// </summary>
		/// <param name="slackUser">
		/// Reference to a slack user record.
		/// </param>
		public UserItem(SlackUserItem slackUser)
		{
			mSlackID = slackUser.ID;
			mSlackName = slackUser.Name;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	RowID																																	*
		//*-----------------------------------------------------------------------*
		private int mRowID = 0;
		/// <summary>
		/// Get/Set the locally unique row ID.
		/// </summary>
		public int RowID
		{
			get { return mRowID; }
			set { mRowID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SlackID																																*
		//*-----------------------------------------------------------------------*
		private string mSlackID = "";
		/// <summary>
		/// Get/Set the user's Slack ID.
		/// </summary>
		public string SlackID
		{
			get { return mSlackID; }
			set { mSlackID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SlackName																															*
		//*-----------------------------------------------------------------------*
		private string mSlackName = "";
		/// <summary>
		/// Get/Set the user's Slack name.
		/// </summary>
		public string SlackName
		{
			get { return mSlackName; }
			set { mSlackName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	UserItemTicket																												*
		//*-----------------------------------------------------------------------*
		private string mUserItemTicket = Guid.NewGuid().ToString("D");
		/// <summary>
		/// Get/Set the globally unique identification of this record.
		/// </summary>
		public string UserItemTicket
		{
			get { return mUserItemTicket; }
			set { mUserItemTicket = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*


}
