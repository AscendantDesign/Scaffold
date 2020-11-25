//	ConversationCourse.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.

using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Scaffold;
using static ScaffoldSlackPack.ScaffoldSlackPackUtil;
using System.Diagnostics;

namespace ScaffoldSlackPack.Models
{
	//*-------------------------------------------------------------------------*
	//*	ConversationCourseCollection																						*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of conversational course Items.
	/// </summary>
	public class ConversationCourseCollection : List<ConversationCourseItem>
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
		//*	LoadFromDatabase																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a collection of courses, loaded from the database.
		/// </summary>
		/// <returns>
		/// Collection of courses.
		/// </returns>
		public static ConversationCourseCollection LoadFromDatabase()
		{
			SqliteCommand command = null;
			ConversationCourseItem course = null;
			ConversationCourseCollection courses =
				new ConversationCourseCollection();
			SqliteDataReader reader = null;

			using(SqliteConnection connection =
				new SqliteConnection("Data Source = Data/ScaffoldSlackPack.db"))
			{
				connection.Open();
				command = connection.CreateCommand();
				command.CommandText = ResourceMain.sqlConversationSelectCatalogAll;
				reader = command.ExecuteReader();
				while(reader.Read())
				{
					course = new ConversationCourseItem()
					{
						RowID = reader.GetInt32(0),
						ConversationTicket =
							reader.GetString(reader.GetOrdinal("ConversationTicket")),
						ConversationTitle =
							reader.GetString(reader.GetOrdinal("ConversationTitle")),
						ConversationDescription =
							reader.GetString(reader.GetOrdinal("ConversationDescription"))
					};
					courses.Add(course);
				}
				reader.DisposeAsync();
			}
			return courses;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a collection of courses, loaded from the database, including
		/// current status and level for the specified user.
		/// </summary>
		/// <param name="slackUserID">
		/// Unique identification of the slack user.
		/// </param>
		/// <returns>
		/// Collection of courses where user status and level have been filled.
		/// </returns>
		public static ConversationCourseCollection LoadFromDatabase(
			string slackUserID)
		{
			SqliteCommand command = null;
			ConversationCourseItem course = null;
			ConversationCourseCollection courses = LoadFromDatabase();
			SqliteDataReader reader = null;
			string userTicket = GetUserTicket(slackUserID);

			using(SqliteConnection connection =
				new SqliteConnection("Data Source = Data/ScaffoldSlackPack.db"))
			{
				connection.Open();
				command = connection.CreateCommand();
				command.CommandText =
					String.Format(ResourceMain.sqlUserProgressSelectUserAll, userTicket);
				reader = command.ExecuteReader();
				while(reader.Read())
				{
					course = courses.FirstOrDefault(x =>
						x.ConversationTicket ==
						reader.GetString(reader.GetOrdinal("ConversationTicket")));
					if(course != null)
					{
						course.UserLevel = reader.GetInt32(reader.GetOrdinal("UserLevel"));
						course.UserStatus = (ConversationStateEnum)reader.GetInt32(
							reader.GetOrdinal("ConversationState"));
					}
				}
				reader.DisposeAsync();
			}
			return courses;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UpdateDatabase																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the local database from nodes and sockets presented on the
		/// standard decision tree node file.
		/// </summary>
		/// <param name="nodeFile">
		/// Reference to a decision tree node file containing objects to store.
		/// </param>
		/// <remarks>
		/// In this version the modified node structure is not stored in the
		/// database. If it is needed in a later version, it can be re-added
		/// to the ConversationData column.
		/// </remarks>
		public static void UpdateDatabase(NodeFileItem nodeFile)
		{
			SqliteCommand command = null;
			string connectionTicket = "";
			DNodeItem dNode = null;
			DNodeCollection dNodes = new DNodeCollection();
			DSocketItem dSocket = null;
			DSocketItem dSocketNext = null;
			DSocketCollection dSockets = new DSocketCollection();
			int id = 0;
			NodeItem nodeFound = null;
			string state = "";

			if(nodeFile != null && nodeFile.Ticket?.Length > 0)
			{
				////	Re-serialize the optimized version of the file.
				//content = NodeDataCollection.SerializeData(nodeFile, false);
				//	Expected type was received.
				using(SqliteConnection connection =
					new SqliteConnection("Data Source = Data/ScaffoldSlackPack.db"))
				{
					connection.Open();
					command = connection.CreateCommand();
					command.CommandText = String.Format(
						ResourceMain.sqlConversationSelectIDForTicket, nodeFile.Ticket);
					try
					{
						id = ToInteger(command.ExecuteScalar());
					}
					catch(Exception ex)
					{
						Debug.WriteLine($"Publish: Error on Select - {ex.Message}");
					}
					if(id > 0)
					{
						//	File already exists.
						command.CommandText =
							String.Format(ResourceMain.sqlConversationUpdate, id);
						command.Parameters.AddWithValue(
							"$ConversationTitle", nodeFile.Name);
						command.Parameters.AddWithValue(
							"$ConversationDescription", nodeFile.Description);
						state = "Update";
					}
					else
					{
						//	File is new.
						command.CommandText = ResourceMain.sqlConversationInsert;
						command.Parameters.AddWithValue(
							"$ConversationTicket", nodeFile.Ticket);
						command.Parameters.AddWithValue(
							"$ConversationTitle", nodeFile.Name);
						command.Parameters.AddWithValue(
							"$ConversationDescription", nodeFile.Description);
						state = "Insert";
					}
					try
					{
						command.ExecuteNonQuery();
					}
					catch(Exception ex)
					{
						Debug.WriteLine(
							$"Publish: Error on {state} - {ex.Message}");
					}
				}
				foreach(NodeItem node in nodeFile.Nodes)
				{
					if(node.Ticket?.Length > 0)
					{
						dNode = new DNodeItem();
						dNode.ConversationTicket = nodeFile.Ticket;
						dNode.NodeDelay = node.Delay;
						dNode.NodeImageUrl = node.Properties["MediaImage"].StringValue();
						dNode.NodeItemTicket = node.Ticket;
						dNode.NodeLinkUrl = node.Properties["MediaLink"].StringValue();
						dNode.NodeText = node.Properties[node.TitleProperty].StringValue();
						dNode.NodeType = node.NodeType;
						dNodes.Add(dNode);
						foreach(SocketItem socket in node.Sockets)
						{
							dSocket = new DSocketItem();
							if(socket.Connections.Count > 0)
							{
								connectionTicket = socket.Connections[0].Ticket;
								nodeFound = nodeFile.Nodes.FirstOrDefault(x =>
									x.Sockets.FirstOrDefault(y =>
										y.Ticket == connectionTicket) != null);
								if(nodeFound != null)
								{
									dSocket.NextNodeItemTicket = nodeFound.Ticket;
								}
								dSocket.NextSocketItemTicket = connectionTicket;
							}
							dSocket.NodeItemTicket = node.Ticket;
							dSocket.SocketImageUrl =
								socket.Properties["MediaImage"].StringValue();
							dSocket.SocketItemTicket = socket.Ticket;
							dSocket.SocketLinkUrl =
								socket.Properties["MediaLink"].StringValue();
							dSocket.SocketText =
								socket.Properties[socket.TitleProperty].StringValue();
							dSocket.SocketType = socket.SocketMode.ToString();
							dSockets.Add(dSocket);
						}
						//	For every output socket where no local media was assigned,
						//	use the following response media, if present.
						foreach(DSocketItem dSocketItem in dSockets)
						{
							if(dSocketItem.SocketImageUrl.Length == 0)
							{
								dSocketNext = dSockets.FirstOrDefault(x =>
									x.SocketItemTicket == dSocketItem.NextSocketItemTicket);
								if(dSocketNext != null &&
									dSocketNext.SocketImageUrl?.Length > 0)
								{
									dSocketItem.SocketImageUrl = dSocketNext.SocketImageUrl;
								}
							}
						}
					}
				}
				if(dNodes.Count > 0)
				{
					DNodeCollection.UpdateDatabase(dNodes);
				}
				if(dSockets.Count > 0)
				{
					DSocketCollection.UpdateDatabase(dSockets);
				}
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	ConversationCourseItem																									*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about a single conversational course.
	/// </summary>
	public class ConversationCourseItem
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
		//*	ConversationDescription																								*
		//*-----------------------------------------------------------------------*
		private string mConversationDescription = "";
		/// <summary>
		/// Get/Set a brief description of the item.
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
		private string mConversationTicket = Guid.NewGuid().ToString("D");
		/// <summary>
		/// Get/Set the globally unique identification of this item.
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
		/// Get/Set the user-readable name of this item.
		/// </summary>
		public string ConversationTitle
		{
			get { return mConversationTitle; }
			set { mConversationTitle = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	RowID																																	*
		//*-----------------------------------------------------------------------*
		private int mRowID = 0;
		/// <summary>
		/// Get/Set the locally unique identification of this item.
		/// </summary>
		public int RowID
		{
			get { return mRowID; }
			set { mRowID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	UserLevel																															*
		//*-----------------------------------------------------------------------*
		private int mUserLevel = 0;
		/// <summary>
		/// Get/Set the user's current level on this course.
		/// </summary>
		public int UserLevel
		{
			get { return mUserLevel; }
			set { mUserLevel = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	UserStatus																														*
		//*-----------------------------------------------------------------------*
		private ConversationStateEnum mUserStatus = ConversationStateEnum.None;
		/// <summary>
		/// Get/Set the user's current conversation status.
		/// </summary>
		public ConversationStateEnum UserStatus
		{
			get { return mUserStatus; }
			set { mUserStatus = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
