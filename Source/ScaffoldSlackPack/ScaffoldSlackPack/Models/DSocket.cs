//	DSocket.cs
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
	//*	DSocketCollection																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of DSocketItem Items.
	/// </summary>
	public class DSocketCollection : List<DSocketItem>
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
		/// Load a collection of sockets from the database using the unique ticket
		/// of the parent node item.
		/// </summary>
		/// <param name="nodeItemTicket">
		/// Globally unique identification of the parent node.
		/// </param>
		/// <returns>
		/// Collection of matching sockets.
		/// </returns>
		public static DSocketCollection LoadFromDatabase(string nodeItemTicket)
		{
			SqliteCommand command = null;
			SqliteDataReader reader = null;
			DSocketItem socket = null;
			DSocketCollection sockets = new DSocketCollection();

			using(SqliteConnection connection =
				new SqliteConnection("Data Source = Data/ScaffoldSlackPack.db"))
			{
				connection.Open();
				command = connection.CreateCommand();
				command.CommandText =
					String.Format(ResourceMain.sqlSocketDataSelectNode, nodeItemTicket);
				reader = command.ExecuteReader();
				while(reader.Read())
				{
					socket = new DSocketItem();
					socket.NextNodeItemTicket =
						reader.GetString(reader.GetOrdinal("NextNodeItemTicket"));
					socket.NextSocketItemTicket =
						reader.GetString(reader.GetOrdinal("NextSocketItemTicket"));
					socket.NodeItemTicket =
						reader.GetString(reader.GetOrdinal("NodeItemTicket"));
					socket.SocketImageUrl =
						reader.GetString(reader.GetOrdinal("SocketImageUrl"));
					socket.SocketItemTicket =
						reader.GetString(reader.GetOrdinal("SocketItemTicket"));
					socket.SocketLinkUrl =
						reader.GetString(reader.GetOrdinal("SocketLinkUrl"));
					socket.SocketText =
						reader.GetString(reader.GetOrdinal("SocketText"));
					socket.SocketType =
						reader.GetString(reader.GetOrdinal("SocketType"));
					sockets.Add(socket);
				}
				reader.DisposeAsync();
			}
			return sockets;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UpdateDatabase																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Store the collection of sockets to the database, inserting or updating
		/// on a per-record basis.
		/// </summary>
		/// <param name="sockets">
		/// Collection of sockets to update.
		/// </param>
		public static void UpdateDatabase(DSocketCollection sockets)
		{
			SqliteCommand command = null;
			int id = 0;
			string state = "";

			if(sockets?.Count > 0)
			{
				using(SqliteConnection connection =
					new SqliteConnection("Data Source = Data/ScaffoldSlackPack.db"))
				{
					connection.Open();
					command = connection.CreateCommand();
					foreach(DSocketItem socket in sockets)
					{
						command.Parameters.Clear();
						command.CommandText =
							String.Format(
								ResourceMain.sqlSocketDataSelectID, socket.SocketItemTicket);
						id = ToInteger(command.ExecuteScalar());
						if(id > 0)
						{
							//	Update.
							command.CommandText = ResourceMain.sqlSocketDataUpdate;
							command.Parameters.AddWithValue(
								"$NextNodeItemTicket", socket.NextNodeItemTicket);
							command.Parameters.AddWithValue(
								"$NextSocketItemTicket", socket.NextSocketItemTicket);
							command.Parameters.AddWithValue(
								"$NodeItemTicket", socket.NodeItemTicket);
							command.Parameters.AddWithValue(
								"$SocketImageUrl", socket.SocketImageUrl);
							command.Parameters.AddWithValue(
								"$SocketItemTicket", socket.SocketItemTicket);
							command.Parameters.AddWithValue(
								"$SocketLinkUrl", socket.SocketLinkUrl);
							command.Parameters.AddWithValue(
								"$SocketText", socket.SocketText);
							command.Parameters.AddWithValue(
								"$SocketType", socket.SocketType);
							state = "Update";
						}
						else
						{
							//	Insert.
							command.CommandText = ResourceMain.sqlSocketDataInsert;
							command.Parameters.AddWithValue(
								"$NextNodeItemTicket", socket.NextNodeItemTicket);
							command.Parameters.AddWithValue(
								"$NextSocketItemTicket", socket.NextSocketItemTicket);
							command.Parameters.AddWithValue(
								"$NodeItemTicket", socket.NodeItemTicket);
							command.Parameters.AddWithValue(
								"$SocketImageUrl", socket.SocketImageUrl);
							command.Parameters.AddWithValue(
								"$SocketItemTicket", socket.SocketItemTicket);
							command.Parameters.AddWithValue(
								"$SocketLinkUrl", socket.SocketLinkUrl);
							command.Parameters.AddWithValue(
								"$SocketText", socket.SocketText);
							command.Parameters.AddWithValue(
								"$SocketType", socket.SocketType);
							state = "Update";
						}
						try
						{
							command.ExecuteNonQuery();
						}
						catch(Exception ex)
						{
							Debug.WriteLine(
								$"Store socket data: Error on {state} - {ex.Message}");
						}
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	DSocketItem																															*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about a single socket in the database.
	/// </summary>
	public class DSocketItem
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
		//*	NextNodeItemTicket																										*
		//*-----------------------------------------------------------------------*
		private string mNextNodeItemTicket = "";
		/// <summary>
		/// Get/Set the globally unique identification of the next node to which
		/// navigation will continue if this socket is clicked.
		/// </summary>
		public string NextNodeItemTicket
		{
			get { return mNextNodeItemTicket; }
			set { mNextNodeItemTicket = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NextSocketItemTicket																									*
		//*-----------------------------------------------------------------------*
		private string mNextSocketItemTicket = "";
		/// <summary>
		/// Get/Set the globally unique identification of the response socket to
		/// which a click on this socket will be directed.
		/// </summary>
		public string NextSocketItemTicket
		{
			get { return mNextSocketItemTicket; }
			set { mNextSocketItemTicket = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeItemTicket																												*
		//*-----------------------------------------------------------------------*
		private string mNodeItemTicket = "";
		/// <summary>
		/// Get/Set the globally unique identification of the node of which this
		/// socket is a member.
		/// </summary>
		public string NodeItemTicket
		{
			get { return mNodeItemTicket; }
			set { mNodeItemTicket = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SocketImageUrl																												*
		//*-----------------------------------------------------------------------*
		private string mSocketImageUrl = "";
		/// <summary>
		/// Get/Set the URL of an image to send with this socket.
		/// </summary>
		public string SocketImageUrl
		{
			get { return mSocketImageUrl; }
			set { mSocketImageUrl = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SocketItemTicket																											*
		//*-----------------------------------------------------------------------*
		private string mSocketItemTicket = "";
		/// <summary>
		/// Get/Set the unique identification of the represented socket object.
		/// </summary>
		public string SocketItemTicket
		{
			get { return mSocketItemTicket; }
			set { mSocketItemTicket = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SocketLinkUrl																													*
		//*-----------------------------------------------------------------------*
		private string mSocketLinkUrl = "";
		/// <summary>
		/// Get/Set a link URL to send with this socket.
		/// </summary>
		public string SocketLinkUrl
		{
			get { return mSocketLinkUrl; }
			set { mSocketLinkUrl = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SocketText																														*
		//*-----------------------------------------------------------------------*
		private string mSocketText = "";
		/// <summary>
		/// Get/Set the text to display with this socket.
		/// </summary>
		public string SocketText
		{
			get { return mSocketText; }
			set { mSocketText = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SocketType																														*
		//*-----------------------------------------------------------------------*
		private string mSocketType = "";
		/// <summary>
		/// Get/Set the type of socket represented by this record.
		/// </summary>
		public string SocketType
		{
			get { return mSocketType; }
			set { mSocketType = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
