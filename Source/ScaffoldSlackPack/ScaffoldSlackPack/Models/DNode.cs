//	DNode.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.

using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using static ScaffoldSlackPack.ScaffoldSlackPackUtil;

namespace ScaffoldSlackPack.Models
{
	//*-------------------------------------------------------------------------*
	//*	DNodeCollection																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of DNodeItem Items.
	/// </summary>
	public class DNodeCollection : List<DNodeItem>
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
		//* LoadStartFromDatabase																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Load the start node from the database using the unique ticket
		/// of the parent file item.
		/// </summary>
		/// <param name="conversationTicket">
		/// Globally unique identification of the parent file.
		/// </param>
		/// <returns>
		/// Collection of matching sockets.
		/// </returns>
		public static DNodeItem LoadStartFromDatabase(
			string conversationTicket)
		{
			SqliteCommand command = null;
			DNodeItem node = null;
			SqliteDataReader reader = null;

			using(SqliteConnection connection =
				new SqliteConnection("Data Source = Data/ScaffoldSlackPack.db"))
			{
				connection.Open();
				command = connection.CreateCommand();
				command.CommandText =
					String.Format(
						ResourceMain.sqlNodeDataSelectStart, conversationTicket);
				reader = command.ExecuteReader();
				if(reader.Read())
				{
					node = new DNodeItem();
					node.ConversationTicket =
						reader.GetString(reader.GetOrdinal("ConversationTicket"));
					node.NodeDelay =
						reader.GetInt32(reader.GetOrdinal("NodeDelay"));
					node.NodeImageUrl =
						reader.GetString(reader.GetOrdinal("NodeImageUrl"));
					node.NodeItemTicket =
						reader.GetString(reader.GetOrdinal("NodeItemTicket"));
					node.NodeLinkUrl =
						reader.GetString(reader.GetOrdinal("NodeLinkUrl"));
					node.NodeText =
						reader.GetString(reader.GetOrdinal("NodeText"));
					node.NodeType =
						reader.GetString(reader.GetOrdinal("NodeType"));
				}
				reader.DisposeAsync();
			}
			return node;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UpdateDatabase																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Store the collection of nodes to the database, inserting or updating
		/// on a per-record basis.
		/// </summary>
		/// <param name="nodes">
		/// Collection of nodes to update.
		/// </param>
		public static void UpdateDatabase(DNodeCollection nodes)
		{
			SqliteCommand command = null;
			int id = 0;
			string state = "";

			if(nodes?.Count > 0)
			{
				using(SqliteConnection connection =
					new SqliteConnection("Data Source = Data/ScaffoldSlackPack.db"))
				{
					connection.Open();
					command = connection.CreateCommand();
					foreach(DNodeItem node in nodes)
					{
						command.Parameters.Clear();
						command.CommandText =
							String.Format(
								ResourceMain.sqlNodeDataSelectID, node.NodeItemTicket);
						id = ToInteger(command.ExecuteScalar());
						if(id > 0)
						{
							//	Update.
							command.CommandText = ResourceMain.sqlNodeDataUpdate;
							command.Parameters.AddWithValue(
								"$ConversationTicket", node.ConversationTicket);
							command.Parameters.AddWithValue(
								"$NodeDelay", node.NodeDelay);
							command.Parameters.AddWithValue(
								"$NodeImageUrl", node.NodeImageUrl);
							command.Parameters.AddWithValue(
								"$NodeItemTicket", node.NodeItemTicket);
							command.Parameters.AddWithValue(
								"$NodeLinkUrl", node.NodeLinkUrl);
							command.Parameters.AddWithValue(
								"$NodeText", node.NodeText);
							command.Parameters.AddWithValue(
								"$NodeType", node.NodeType);
							state = "Update";
						}
						else
						{
							//	Insert.
							command.CommandText = ResourceMain.sqlNodeDataInsert;
							command.Parameters.AddWithValue(
								"$ConversationTicket", node.ConversationTicket);
							command.Parameters.AddWithValue(
								"$NodeDelay", node.NodeDelay);
							command.Parameters.AddWithValue(
								"$NodeImageUrl", node.NodeImageUrl);
							command.Parameters.AddWithValue(
								"$NodeItemTicket", node.NodeItemTicket);
							command.Parameters.AddWithValue(
								"$NodeLinkUrl", node.NodeLinkUrl);
							command.Parameters.AddWithValue(
								"$NodeText", node.NodeText);
							command.Parameters.AddWithValue(
								"$NodeType", node.NodeType);
							state = "Update";
						}
						try
						{
							command.ExecuteNonQuery();
						}
						catch(Exception ex)
						{
							Debug.WriteLine(
								$"Store node data: Error on {state} - {ex.Message}");
						}
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	DNodeItem																																*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about a single node in the database.
	/// </summary>
	public class DNodeItem
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
		//*	ConversationTicket																										*
		//*-----------------------------------------------------------------------*
		private string mConversationTicket = "";
		/// <summary>
		/// Get/Set the globally unique identification of the parent node file.
		/// </summary>
		public string ConversationTicket
		{
			get { return mConversationTicket; }
			set { mConversationTicket = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* LoadFromDatabase																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Load a node record from the database using the unique node ticket.
		/// </summary>
		/// <param name="nodeItemTicket">
		/// Globally unique identification of the node.
		/// </param>
		/// <returns>
		/// Database representation of the specified node.
		/// </returns>
		public static DNodeItem LoadFromDatabase(string nodeItemTicket)
		{
			SqliteCommand command = null;
			DNodeItem node = null;
			SqliteDataReader reader = null;

			using(SqliteConnection connection =
				new SqliteConnection("Data Source = Data/ScaffoldSlackPack.db"))
			{
				connection.Open();
				command = connection.CreateCommand();
				command.CommandText =
					String.Format(
						ResourceMain.sqlNodeDataSelect, nodeItemTicket);
				reader = command.ExecuteReader();
				if(reader.Read())
				{
					node = new DNodeItem();
					node.ConversationTicket =
						reader.GetString(reader.GetOrdinal("ConversationTicket"));
					node.NodeDelay =
						reader.GetInt32(reader.GetOrdinal("NodeDelay"));
					node.NodeImageUrl =
						reader.GetString(reader.GetOrdinal("NodeImageUrl"));
					node.NodeItemTicket =
						reader.GetString(reader.GetOrdinal("NodeItemTicket"));
					node.NodeLinkUrl =
						reader.GetString(reader.GetOrdinal("NodeLinkUrl"));
					node.NodeText =
						reader.GetString(reader.GetOrdinal("NodeText"));
					node.NodeType =
						reader.GetString(reader.GetOrdinal("NodeType"));
				}
				reader.DisposeAsync();
			}
			return node;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeDelay																															*
		//*-----------------------------------------------------------------------*
		private float mNodeDelay = 0f;
		/// <summary>
		/// Get/Set the node delay, in seconds.
		/// </summary>
		public float NodeDelay
		{
			get { return mNodeDelay; }
			set { mNodeDelay = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeImageUrl																													*
		//*-----------------------------------------------------------------------*
		private string mNodeImageUrl = "";
		/// <summary>
		/// Get/Set the URL of an image to display with the question.
		/// </summary>
		public string NodeImageUrl
		{
			get { return mNodeImageUrl; }
			set { mNodeImageUrl = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeItemTicket																												*
		//*-----------------------------------------------------------------------*
		private string mNodeItemTicket = "";
		/// <summary>
		/// Get/Set the globally unique identification of the node object.
		/// </summary>
		public string NodeItemTicket
		{
			get { return mNodeItemTicket; }
			set { mNodeItemTicket = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeLinkUrl																														*
		//*-----------------------------------------------------------------------*
		private string mNodeLinkUrl = "";
		/// <summary>
		/// Get/Set a link URL to display with the question.
		/// </summary>
		public string NodeLinkUrl
		{
			get { return mNodeLinkUrl; }
			set { mNodeLinkUrl = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeText																															*
		//*-----------------------------------------------------------------------*
		private string mNodeText = "";
		/// <summary>
		/// Get/Set the node or question text.
		/// </summary>
		public string NodeText
		{
			get { return mNodeText; }
			set { mNodeText = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeType																															*
		//*-----------------------------------------------------------------------*
		private string mNodeType = "";
		/// <summary>
		/// Get/Set the type of node represented.
		/// </summary>
		public string NodeType
		{
			get { return mNodeType; }
			set { mNodeType = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*


}
