//	ScaffoldSlackPackUtil.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.

using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using ScaffoldSlackPack.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Scaffold;
using System.Web;

namespace ScaffoldSlackPack
{
	//*-------------------------------------------------------------------------*
	//*	ScaffoldSlackPackUtil																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Utility functionality for Scaffold Slack Pack server.
	/// </summary>
	public static class ScaffoldSlackPackUtil
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
		//* AppendLog																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Append the specified content to the log.
		/// </summary>
		/// <param name="content">
		/// Content to append.
		/// </param>
		public static void AppendLog(string content)
		{
			System.IO.File.AppendAllText("Data/Activity.txt", content);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ClearLog																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Clear the log file.
		/// </summary>
		public static void ClearLog()
		{
			System.IO.File.WriteAllText("Data/Activity.txt",
				$"Created: {DateTime.Now.ToString("yyyyMMdd.HHmm")}");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* CourseCompleted																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the course to completed for the specified user.
		/// </summary>
		/// <param name="slackUserID">
		/// Unique identification of the slack user.
		/// </param>
		/// <param name="courseTicket">
		/// Globally unique identification of the course.
		/// </param>
		public static void CourseCompleted(string slackUserID, string courseTicket)
		{
			bool bFound = false;
			SqliteCommand command = null;
			string progressTicket = "";
			SqliteDataReader reader = null;
			string userTicket = GetUserTicket(slackUserID);

			using(SqliteConnection connection =
				new SqliteConnection("Data Source = Data/ScaffoldSlackPack.db"))
			{
				connection.Open();
				command = connection.CreateCommand();
				command.CommandText =
					String.Format(ResourceMain.sqlUserProgressSelectCourse,
					userTicket, courseTicket);
				reader = command.ExecuteReader();
				if(reader.Read())
				{
					//	Result is present. Update.
					progressTicket =
						reader.GetString(reader.GetOrdinal("UserProgressTicket"));
					bFound = true;
				}
				reader.DisposeAsync();
				if(bFound)
				{
					command.CommandText = String.Format(
						ResourceMain.sqlUserProgressUpdateStatus,
						(int)ConversationStateEnum.Completed, progressTicket);
				}
				else
				{
					command.CommandText = String.Format(
						ResourceMain.sqlUserProgressInsert,
						Guid.NewGuid().ToString("D"),
						userTicket, courseTicket, (int)ConversationStateEnum.Completed, 0);
				}
				command.ExecuteNonQuery();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* CoursePaused																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Pause the specified course.
		/// </summary>
		/// <param name="slackUserID">
		/// Unique identification of the slack user.
		/// </param>
		/// <param name="courseTicket">
		/// Globally unique identification of the course.
		/// </param>
		/// <param name="userLevel">
		/// Level at which the course was paused.
		/// </param>
		public static void CoursePaused(string slackUserID, string courseTicket)
		{
			bool bFound = false;
			SqliteCommand command = null;
			string progressTicket = "";
			SqliteDataReader reader = null;
			string userTicket = GetUserTicket(slackUserID);

			using(SqliteConnection connection =
				new SqliteConnection("Data Source = Data/ScaffoldSlackPack.db"))
			{
				connection.Open();
				command = connection.CreateCommand();
				command.CommandText =
					String.Format(ResourceMain.sqlUserProgressSelectCourse,
					userTicket, courseTicket);
				reader = command.ExecuteReader();
				if(reader.Read())
				{
					//	Result is present. Update.
					progressTicket =
						reader.GetString(reader.GetOrdinal("UserProgressTicket"));
					bFound = true;
				}
				reader.DisposeAsync();
				if(bFound)
				{
					command.CommandText = String.Format(
						ResourceMain.sqlUserProgressUpdateStatus,
						(int)ConversationStateEnum.Paused, progressTicket);
				}
				else
				{
					command.CommandText = String.Format(
						ResourceMain.sqlUserProgressInsert,
						Guid.NewGuid().ToString("D"),
						userTicket, courseTicket, (int)ConversationStateEnum.Paused, 0);
				}
				command.ExecuteNonQuery();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* CourseResumed																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Resume the specified course.
		/// </summary>
		/// <param name="slackUserID">
		/// Unique identification of the slack user.
		/// </param>
		/// <param name="courseTicket">
		/// Globally unique identification of the course.
		/// </param>
		public static void CourseResumed(string slackUserID, string courseTicket)
		{
			bool bFound = false;
			SqliteCommand command = null;
			string progressTicket = "";
			SqliteDataReader reader = null;
			string userTicket = GetUserTicket(slackUserID);

			using(SqliteConnection connection =
				new SqliteConnection("Data Source = Data/ScaffoldSlackPack.db"))
			{
				connection.Open();
				command = connection.CreateCommand();
				command.CommandText =
					String.Format(ResourceMain.sqlUserProgressSelectCourse,
					userTicket, courseTicket);
				reader = command.ExecuteReader();
				if(reader.Read())
				{
					//	Result is present. Update.
					progressTicket =
						reader.GetString(reader.GetOrdinal("UserProgressTicket"));
					bFound = true;
				}
				reader.DisposeAsync();
				if(bFound)
				{
					command.CommandText = String.Format(
						ResourceMain.sqlUserProgressUpdateStatus,
						(int)ConversationStateEnum.Active, progressTicket);
				}
				else
				{
					command.CommandText = String.Format(
						ResourceMain.sqlUserProgressInsert,
						Guid.NewGuid().ToString("D"),
						userTicket, courseTicket, (int)ConversationStateEnum.Active, 0);
				}
				command.ExecuteNonQuery();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* CourseStarted																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Start or restart the specified course for the specified user.
		/// </summary>
		/// <param name="slackUserID">
		/// Unique identification of the slack user.
		/// </param>
		/// <param name="courseTicket">
		/// Globally unique identification of the course.
		/// </param>
		public static void CourseStarted(string slackUserID, string courseTicket)
		{
			bool bFound = false;
			SqliteCommand command = null;
			string progressTicket = "";
			SqliteDataReader reader = null;
			string userTicket = GetUserTicket(slackUserID);

			using(SqliteConnection connection =
				new SqliteConnection("Data Source = Data/ScaffoldSlackPack.db"))
			{
				connection.Open();
				command = connection.CreateCommand();
				command.CommandText =
					String.Format(ResourceMain.sqlUserProgressSelectCourse,
					userTicket, courseTicket);
				reader = command.ExecuteReader();
				if(reader.Read())
				{
					//	Result is present. Update.
					progressTicket =
						reader.GetString(reader.GetOrdinal("UserProgressTicket"));
					bFound = true;
				}
				reader.DisposeAsync();
				if(bFound)
				{
					command.CommandText = String.Format(
						ResourceMain.sqlUserProgressUpdateStatusLevel,
						(int)ConversationStateEnum.Active, 0, progressTicket);
				}
				else
				{
					command.CommandText = String.Format(
						ResourceMain.sqlUserProgressInsert,
						Guid.NewGuid().ToString("D"),
						userTicket, courseTicket, (int)ConversationStateEnum.Active, 0);
				}
				command.ExecuteNonQuery();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* CourseStopped																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the course for the specified user to stopped.
		/// </summary>
		/// <param name="slackUserID">
		/// Unique identification of the slack user.
		/// </param>
		/// <param name="courseTicket">
		/// Globally unique identification of the course.
		/// </param>
		public static void CourseStopped(string slackUserID, string courseTicket)
		{
			bool bFound = false;
			SqliteCommand command = null;
			string progressTicket = "";
			SqliteDataReader reader = null;
			string userTicket = GetUserTicket(slackUserID);

			using(SqliteConnection connection =
				new SqliteConnection("Data Source = Data/ScaffoldSlackPack.db"))
			{
				connection.Open();
				command = connection.CreateCommand();
				command.CommandText =
					String.Format(ResourceMain.sqlUserProgressSelectCourse,
					userTicket, courseTicket);
				reader = command.ExecuteReader();
				if(reader.Read())
				{
					//	Result is present. Update.
					progressTicket =
						reader.GetString(reader.GetOrdinal("UserProgressTicket"));
					bFound = true;
				}
				reader.DisposeAsync();
				if(bFound)
				{
					command.CommandText = String.Format(
						ResourceMain.sqlUserProgressUpdateStatus,
						(int)ConversationStateEnum.None, progressTicket);
				}
				else
				{
					command.CommandText = String.Format(
						ResourceMain.sqlUserProgressInsert,
						Guid.NewGuid().ToString("D"),
						userTicket, courseTicket, (int)ConversationStateEnum.None, 0);
				}
				command.ExecuteNonQuery();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	FirstCharacter																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the first usable character matches
		/// the specified pattern.
		/// </summary>
		/// <param name="source">
		/// Source content to inspect.
		/// </param>
		/// <param name="pattern">
		/// Pattern to test for.
		/// </param>
		/// <returns>
		/// True if the specified pattern occupies the first intelligible
		/// portion of the source.
		/// </returns>
		public static bool FirstCharacter(string source, string pattern)
		{
			Match match = null;
			bool result = false;

			if(source?.Length > 0 &&
				pattern?.Length > 0 && pattern.Length <= source.Length)
			{
				//	Source and pattern are within scope.
				match = Regex.Match(source,
					String.Format(ResourceMain.rxCharPatternFirst, pattern.Length));
				if(match.Success && GetValue(match, "first") == pattern)
				{
					//	The first character pattern was found.
					result = true;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetBaseUrl																														*
		//*-----------------------------------------------------------------------*
		private static string mBaseUrl = "";
		/// <summary>
		/// Return the base URL of this server, without a trailing slash.
		/// </summary>
		/// <returns>
		/// Base URL of the server.
		/// </returns>
		public static string GetBaseUrl()
		{
			////	Runtime.
			//return mBaseUrl;
			//	Development.
			return "https://scaffoldslackpack.azurewebsites.net";
		}
		////*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		///// <summary>
		///// Return the base URL of this server, without a trailing slash.
		///// </summary>
		///// <param name="request">
		///// Active HTTP request instance.
		///// </param>
		///// <returns>
		///// The base URL of the server, not including a file name, folder name, or
		///// trailing slash.
		///// </returns>
		//public static string GetBaseUrl(HttpRequest request)
		//{
		//	string result = "";

		//	if(request != null)
		//	{
		//		result = String.Format("{0}://{1}", request.Scheme, request.Host);
		//	}
		//	return result;
		//}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetBlockAnswer																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the JSON formatted message block answer representing the user's
		/// reaction to the previous question.
		/// </summary>
		/// <param name="slackUserID">
		/// Unique identification of the slack user.
		/// </param>
		/// <param name="nodeItemTicket">
		/// Globally unique identification of the node to load.
		/// </param>
		/// <returns>
		/// Formatted JSON message body for Slack message hook.
		/// </returns>
		public static string GetBlockAnswer(string slackUserID,
			string nodeItemTicket)
		{
			string content = "";

			return content;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetBlockMessage																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a JSON formatted rich text message addressed to the specified
		/// user.
		/// </summary>
		/// <param name="slackUserID">
		/// Unique identification of the slack user.
		/// </param>
		/// <param name="message">
		/// Markdown-formatted content of the message to send.
		/// </param>
		/// <returns>
		/// Rich text message, formatted as serialized JSON, and addressed to the
		/// specified user.
		/// </returns>
		public static string GetBlockMessage(string slackUserID, string message)
		{
			SlackBlockContainer container = null;
			string content = "";
			SlackBlockItemSection section = null;

			container = new SlackBlockContainer();
			container.SlackUserID = slackUserID;
			container.Blocks.Add(new SlackBlockItemDivider());
			section = new SlackBlockItemSection();
			section.Text.TextType = "mrkdwn";
			section.Text.Value = message;
			container.Blocks.Add(section);
			content = JsonConvert.SerializeObject(container);
			return content;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetBlockQuestion																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the formatted question block for the specified node item ticket.
		/// </summary>
		/// <param name="slackUserID">
		/// Unique identification of the slack user.
		/// </param>
		/// <param name="nodeItemTicket">
		/// Globally unique identification of the node to load.
		/// </param>
		/// <returns>
		/// Formatted JSON message body for Slack message hook.
		/// </returns>
		public static string GetBlockQuestion(string slackUserID,
			string nodeItemTicket)
		{
			SlackBlockItemActions actions = null;
			SlackBlockItemButton button = null;
			SlackBlockContainer container = null;
			string content = "";
			DNodeItem dNode = null;
			DSocketCollection dSockets = null;
			SlackBlockItemImage image = null;
			SlackBlockItemSection section = null;
			string text = "";

			if(slackUserID?.Length > 0 && nodeItemTicket?.Length > 0)
			{
				dNode = DNodeItem.LoadFromDatabase(nodeItemTicket);
				if(dNode != null)
				{
					//	Record found.
					dSockets = DSocketCollection.LoadFromDatabase(dNode.NodeItemTicket);
					text = dNode.NodeText;
					container = new SlackBlockContainer();
					container.SlackUserID = slackUserID;
					container.Blocks.Add(new SlackBlockItemDivider());
					if(dNode.NodeImageUrl?.Length > 0)
					{
						image = new SlackBlockItemImage();
						image.AltText = "card image";
						image.ImageURL = $"{GetBaseUrl()}/{dNode.NodeImageUrl}";
						container.Blocks.Add(image);
					}
					section = new SlackBlockItemSection();
					section.Text.TextType = "mrkdwn";
					section.Text.Value = text;
					container.Blocks.Add(section);
					container.Blocks.Add(new SlackBlockItemDivider());
					foreach(DSocketItem dSocket in dSockets)
					{
						if(dSocket.SocketType == "Output")
						{
							section = new SlackBlockItemSection();
							section.Text.TextType = "mrkdwn";
							section.Text.Value = dSocket.SocketText;
							if(dSocket.SocketImageUrl?.Length > 0)
							{
								image = new SlackBlockItemImage();
								image.AltText = "choice image";
								image.ImageURL = $"{GetBaseUrl()}/{dSocket.SocketImageUrl}";
								section.Accessory = image;
							}
							container.Blocks.Add(section);
							actions = new SlackBlockItemActions();
							button = new SlackBlockItemButton();
							button.Text.Value = "Click to select";
							button.ActionID = dSocket.NextNodeItemTicket;
							actions.Elements.Add(button);
							container.Blocks.Add(actions);
							container.Blocks.Add(new SlackBlockItemDivider());
						}
					}
				}
				content = JsonConvert.SerializeObject(container);
			}
			return content;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetBlockQuestionStart																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the formatted question block for the start node on the
		/// specified conversational course.
		/// </summary>
		/// <param name="slackUserID">
		/// Unique identification of the slack user.
		/// </param>
		/// <param name="conversationTicket">
		/// Globally unique identification of the conversation.
		/// </param>
		/// <returns>
		/// Formatted JSON message body for Slack message hook.
		/// </returns>
		public static string GetBlockQuestionStart(string slackUserID,
			string conversationTicket)
		{
			string content = "";
			DNodeItem dNode = null;

			if(slackUserID?.Length > 0 && conversationTicket?.Length > 0)
			{
				dNode = DNodeCollection.LoadStartFromDatabase(conversationTicket);
				if(dNode != null)
				{
					content = GetBlockQuestion(slackUserID, dNode.NodeItemTicket);
				}
			}
			return content;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetCommandNotFoundText																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a message formatted as command not found.
		/// </summary>
		/// <param name="slackUserID">
		/// Unique identification of the slack user.
		/// </param>
		/// <param name="inputMessageText">
		/// Original text of the message input by the user.
		/// </param>
		/// <returns>
		/// Command not found text.
		/// </returns>
		public static async Task<string> GetCommandNotFoundText(
			string slackUserID, string inputMessageText)
		{
			bool bQuestion = false;
			SlackBlockContainer container = null;
			string content = "";
			Match match = null;
			SlackBlockItemSection section = null;
			string username = "";

			username = await GetSlackUsername(slackUserID);
			match = Regex.Match(inputMessageText, ResourceMain.rxCharPatternLast);
			if(match.Success)
			{
				bQuestion = (GetValue(match, "last") == "?");
			}

			container = new SlackBlockContainer();
			container.SlackUserID = slackUserID;
			container.Blocks.Add(new SlackBlockItemDivider());
			section = new SlackBlockItemSection();
			section.Text.TextType = "mrkdwn";
			section.Text.Value = $"Sorry {username}, I don't yet understand " +
				(bQuestion ? "the question ": "") +
				$"_{inputMessageText}_.\n" +
				"Could you rephrase that?";
			container.Blocks.Add(section);
			content = JsonConvert.SerializeObject(container);
			return content;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetCourseList																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a formatted list of courses.
		/// </summary>
		/// <param name="slackUserID">
		/// Unique identification of the slack user.
		/// </param>
		/// <returns>
		/// The list of courses applicable to the specified user.
		/// </returns>
		public static async Task<string> GetCourseList(string slackUserID)
		{
			StringBuilder builder = new StringBuilder();
			SlackBlockContainer container = null;
			string content = "";
			ConversationCourseCollection courses =
				ConversationCourseCollection.LoadFromDatabase(slackUserID);
			SlackBlockItemSection section = null;
			string status = "";
			string username = "";

			if(courses.Count > 0)
			{
				container = new SlackBlockContainer();
				container.SlackUserID = slackUserID;
				builder.Append(
					"The following courses are currently published in Scaffold.\n" +
					"`Name` - *Description*");
				container.Blocks.Add(new SlackBlockItemDivider());
				section = new SlackBlockItemSection();
				section.Text.TextType = "mrkdwn";
				section.Text.Value = builder.ToString();
				container.Blocks.Add(section);
				container.Blocks.Add(new SlackBlockItemDivider());
				section = new SlackBlockItemSection();
				section.Text.TextType = "mrkdwn";
				builder.Remove(0, builder.Length);
				foreach(ConversationCourseItem course in courses)
				{
					switch(course.UserStatus)
					{
						case ConversationStateEnum.Active:
							status = $"In progress - Level:{course.UserLevel}";
							break;
						case ConversationStateEnum.Completed:
							status = "Completed";
							break;
						case ConversationStateEnum.None:
							status = "Not started";
							break;
						case ConversationStateEnum.Paused:
							status = $"Paused - Level:{course.UserLevel}";
							break;
					}
					builder.Append($"`{course.ConversationTitle}` - ({status}) ");
					builder.Append($"{course.ConversationDescription}\n");
				}
				section.Text.Value = builder.ToString();
				container.Blocks.Add(section);
				content = JsonConvert.SerializeObject(container);
			}
			else
			{
				username = await GetSlackUsername(slackUserID);
				builder.Append($"Sorry {username}, " +
					"there are not currently any courses available.\n");
				builder.Append(
					"Please contact your administrator.");
				content = GetBlockMessage(slackUserID, builder.ToString());
			}

			return content;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetGreetingText																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return greeting text for the specified User ID.
		/// </summary>
		/// <param name="slackUserID">
		/// Unique identification of the slack user.
		/// </param>
		/// <returns>
		/// Greeting text block for the specified user.
		/// </returns>
		public static async Task<string> GetGreetingText(string slackUserID)
		{
			SlackBlockContainer container = null;
			string content = "";
			SlackBlockItemSection section = null;
			string username = "";

			username = await GetSlackUsername(slackUserID);
			container = new SlackBlockContainer();
			container.SlackUserID = slackUserID;
			container.Blocks.Add(new SlackBlockItemDivider());
			section = new SlackBlockItemSection();
			section.Text.TextType = "mrkdwn";
			section.Text.Value = $"Hello {username}. What would you like to do?\n" +
				"Following are some available commands:\n" +
				"_list_ - " +
				"List courses available to you. Try typing *list courses*\n" +
				"_start_ - " +
				"Start a course by its name. Try typing " +
				"*start the grandma-d scenario*\n" +
				"_stop_ - " +
				"Stop a course by name. Try typing *stop lesson grandma-d*\n" +
				"_pause_ - " +
				"Pause the course you are currently working on.\n" +
				"_resume_ - " +
				"Resume a course you started in the past. " +
				"Try typing *resume grandma-d*\n";
			container.Blocks.Add(section);
			try
			{
				content = JsonConvert.SerializeObject(container);
			}
			catch(Exception ex)
			{
				Debug.WriteLine("Error on GetGreetingText(slackUserID): " +
					ex.Message);
			}
			return content;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetSlackUsername																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the username corresponding to the provided Slack User ID.
		/// </summary>
		/// <param name="slackUserID">
		/// Unique identification of the slack user.
		/// </param>
		/// <returns>
		/// The appointed name of the user, if found. Otherwise, 'friend'.
		/// </returns>
		public static async Task<string> GetSlackUsername(string slackUserID)
		{
			SqliteCommand command = null;
			string commandText = ResourceMain.sqlUserItemInsertUnique;
			SqliteDataReader reader = null;
			string result = "friend";

			if(slackUserID?.Length > 0)
			{
				using(SqliteConnection connection =
					new SqliteConnection("Data Source = Data/ScaffoldSlackPack.db"))
				{
					connection.Open();
					command = connection.CreateCommand();
					command.CommandText =
						String.Format(ResourceMain.sqlUserItemSelectSlackID, slackUserID);
					reader = command.ExecuteReader();
					if(reader.Read())
					{
						//	Result is present.
						result = reader.GetString(reader.GetOrdinal("SlackName"));
						Debug.WriteLine(
							"User record found at " +
							$"{reader.GetOrdinal("SlackName")}: {result} ...");
					}
					await reader.DisposeAsync();
				}
			}

			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetUserTicket																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the globally unique identification of the user, given the
		/// Slack ID.
		/// </summary>
		/// <param name="slackUserID">
		/// Unique identification of the slack user.
		/// </param>
		/// <returns>
		/// Globally unique identification of the system user record.
		/// </returns>
		public static string GetUserTicket(string slackUserID)
		{
			SqliteCommand command = null;
			string result = "";

			using(SqliteConnection connection =
				new SqliteConnection("Data Source = Data/ScaffoldSlackPack.db"))
			{
				connection.Open();
				command = connection.CreateCommand();
				command.CommandText =
					String.Format(ResourceMain.sqlUserItemSelectTicket, slackUserID);
				result = command.ExecuteScalar().ToString();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetValue																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the value found in the named group of the specified match.
		/// </summary>
		public static string GetValue(Match match, string groupName)
		{
			string result = "";

			if(match != null &&
				match.Groups[groupName] != null &&
				match.Groups[groupName].Value != null)
			{
				result = match.Groups[groupName].Value;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ProcessUserInteraction																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Process user interaction input and any applicable responses.
		/// </summary>
		/// <param name="content">
		/// The content of the interaction received.
		/// </param>
		/// <returns>
		/// Applicable response.
		/// </returns>
		public static async Task<string> ProcessUserInteraction(string content)
		{
			JsonElement body;
			string controlName = "";
			string courseContent = "";
			JsonElement.ArrayEnumerator enumerator;
			string lContent = "";
			JsonElement property;
			JsonElement property2;
			JsonElement property3;
			string result = "";
			string userID = "";
			//	The response pattern is an object with the following properties.
			//	"type":"block_actions",
			//	"user": { id: "{slackUserID}", ... },
			//	"api_app_id":"A01EYSRMA05",
			//	"token":"eg32b9pktGOpTbAHeKc1CH9k",
			//	"container": { ... },
			//	"trigger_id":"1533072218337.1497228267895.229a914bc949b442d3cd6c2b8fe95c63",
			//	"team": { ... },
			//	"channel": { ... },
			//	"message": { ... },
			//	"response_url":"https:\/\/hooks.slack.com\/actions\/{uniquepattern}",
			//	"actions":
			//	[
			//	{
			//		"action_id":"c2d21196-d9ee-4572-9467-7e859dccdb03",
			//		"block_id":"NF1b",
			//		"type":"button",
			//		"action_ts": "1606082019.019127"
			//	}
			//	]
			//	This content is received as UrlEncodedUnicode. We want to
			//	decode it before going any further.
			lContent = content.ToLower();
			if(lContent.IndexOf("%7b") > -1 && lContent.IndexOf("%7d") > -1)
			{
				content = HttpUtility.UrlDecode(content);
			}
			AppendLog("Received: Interaction - " +
				$"{DateTime.Now.ToString("yyyyMMdd.HHmm")}\r\n" +
				$"{content}\r\n\r\n");
			//	Find the first { and discard anything to the left.
			if(content.IndexOf('{') > 0)
			{
				content = content.Substring(content.IndexOf('{'));
			}
			using(JsonDocument doc = JsonDocument.Parse(content))
			{
				body = doc.RootElement;
				if(body.TryGetProperty("user", out property))
				{
					if(property.TryGetProperty("id", out property2))
					{
						userID = property2.GetString();
					}
				}
				if(userID?.Length > 0 &&
					body.TryGetProperty("actions", out property))
				{
					enumerator = property.EnumerateArray();
					while(enumerator.MoveNext())
					{
						controlName = "";
						property2 = enumerator.Current;
						if(property2.TryGetProperty("type", out property3))
						{
							controlName = property3.GetString();
						}
						if(controlName?.Length > 0)
						{
							switch(controlName)
							{
								case "button":
									if(property2.TryGetProperty("action_id", out property3))
									{
										courseContent =
											GetBlockQuestion(userID, property3.GetString());
										if(courseContent?.Length > 0)
										{
											await Task.Run(() => SendMessageBlock(courseContent));
										}
									}
									break;
							}
						}
					}
				}
			}

			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ProcessUserMessage																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Process a user message, including the sending of any response message.
		/// </summary>
		/// <param name="slackUserID">
		/// Unique identification of the slack user.
		/// </param>
		/// <param name="userText">
		/// User's input command text.
		/// </param>
		/// <param name="respondAsync">
		/// Value indicating whether to send an asynchronous response to the
		/// user. If false, no response is sent to the Slack system. In both
		/// cases, the response is returned from this method to allow for testing
		/// and monitoring.
		/// </param>
		/// <returns>
		/// The response generated by the system.
		/// </returns>
		public static async Task<string> ProcessUserMessage(
			string slackUserID, string userText, bool respondAsync = true)
		{
			bool bFound = false;
			string command = "";
			string content = "";
			string courseContent = "";
			int count = 0;
			ConversationCourseCollection courses = null;
			int index = 0;
			Match match = null;
			MatchCollection matches = null;
			List<string> nouns = new List<string>();
			string username = await GetSlackUsername(slackUserID);
			List<string> words = new List<string>();

			if(userText?.Length > 0)
			{
				//	Text is present.
				words.Clear();
				matches = Regex.Matches(userText,
					ResourceMain.rxCommandWordAny);
				foreach(Match matchItem in matches)
				{
					words.Add(GetValue(matchItem, "word").ToLower());
				}
				matches = Regex.Matches(userText, ResourceMain.rxCommandWordNoun);
				foreach(Match matchItem in matches)
				{
					nouns.Add(GetValue(matchItem, "noun").ToLower());
				}
				if(nouns.Count > 0)
				{
					count = words.Count;
					for(index = 0; index < count; index ++)
					{
						if(nouns.Exists(x => x == words[index]))
						{
							words.RemoveAt(index);
							index--;
							count--;
						}
					}
				}
				match = Regex.Match(userText,
					ResourceMain.rxCommandWordCommand);
				if(match.Success)
				{
					//	A command was found.
					command = GetValue(match, "command").ToLower();
					switch(command)
					{
						case "list":
							//	List all courses.
							content = await GetCourseList(slackUserID);
							break;
						case "pause":
							courses =
								ConversationCourseCollection.LoadFromDatabase(slackUserID);
							words.Remove("pause");
							if(words.Count > 0)
							{
								bFound = false;
								foreach(ConversationCourseItem course in courses)
								{
									if(words.Exists(x =>
										x == course.ConversationTitle.ToLower()))
									{
										//	The course was found. Pause it.
										CoursePaused(slackUserID, course.ConversationTicket);
										content = GetBlockMessage(slackUserID,
											$"Course `{course.ConversationTitle}` has been paused " +
											$"at level {course.UserLevel}.");
										bFound = true;
										break;
									}
								}
								if(!bFound)
								{
									if(words.Count > 1)
									{
										content = GetBlockMessage(slackUserID,
											"I didn't find any course names to *pause* in the " +
											$"phrase _{userText}_");
									}
									else
									{
										content = GetBlockMessage(slackUserID,
											$"Sorry {username}, I couldn't find a course named " +
											$"_{words[0]}_");
									}
								}
							}
							else
							{
								content = GetBlockMessage(slackUserID,
									"Which course would you like to *pause*?");
							}
							break;
						case "resume":
							courses =
								ConversationCourseCollection.LoadFromDatabase(slackUserID);
							words.Remove("resume");
							if(words.Count > 0)
							{
								bFound = false;
								foreach(ConversationCourseItem course in courses)
								{
									if(words.Exists(x =>
										x == course.ConversationTitle.ToLower()))
									{
										//	The course was found. Resume it.
										CourseResumed(slackUserID, course.ConversationTicket);
										content = GetBlockMessage(slackUserID,
											$"Course `{course.ConversationTitle}` has been " +
											$"resumed at level {course.UserLevel}.");
										bFound = true;
										break;
									}
								}
								if(!bFound)
								{
									if(words.Count > 1)
									{
										content = GetBlockMessage(slackUserID,
											"I didn't find any course names to *resume* in the " +
											$"phrase _{userText}_");
									}
									else
									{
										content = GetBlockMessage(slackUserID,
											$"Sorry {username}, I couldn't find a course named " +
											$"_{words[0]}_");
									}
								}
							}
							else
							{
								content = GetBlockMessage(slackUserID,
									"Which course would you like to *resume*?");
							}
							break;
						case "start":
							courses =
								ConversationCourseCollection.LoadFromDatabase(slackUserID);
							words.Remove("start");
							if(words.Count > 0)
							{
								bFound = false;
								foreach(ConversationCourseItem course in courses)
								{
									if(words.Exists(x =>
										x == course.ConversationTitle.ToLower()))
									{
										//	The course was found. Start it.
										CourseStarted(slackUserID, course.ConversationTicket);
										content = GetBlockMessage(slackUserID,
											$"Course `{course.ConversationTitle}` has been started " +
											$"from the beginning.");
										courseContent = GetBlockQuestionStart(slackUserID,
											course.ConversationTicket);
										bFound = true;
										break;
									}
								}
								if(!bFound)
								{
									if(words.Count > 1)
									{
										content = GetBlockMessage(slackUserID,
											"I didn't find any course names to *start* in the " +
											$"phrase _{userText}_");
									}
									else
									{
										content = GetBlockMessage(slackUserID,
											$"Sorry {username}, I couldn't find a course named " +
											$"_{words[0]}_");
									}
								}
							}
							else
							{
								content = GetBlockMessage(slackUserID,
									"Which course would you like to *start*?");
							}
							break;
						case "stop":
							courses =
								ConversationCourseCollection.LoadFromDatabase(slackUserID);
							words.Remove("stop");
							if(words.Count > 0)
							{
								bFound = false;
								foreach(ConversationCourseItem course in courses)
								{
									if(words.Exists(x =>
										x == course.ConversationTitle.ToLower()))
									{
										//	The course was found. Stop it.
										CourseStopped(slackUserID, course.ConversationTicket);
										content = GetBlockMessage(slackUserID,
											$"Course `{course.ConversationTitle}` " +
											"has been stopped.");
										bFound = true;
										break;
									}
								}
								if(!bFound)
								{
									if(words.Count > 1)
									{
										content = GetBlockMessage(slackUserID,
											"I didn't find any course names to *stop* in the " +
											$"phrase _{userText}_");
									}
									else
									{
										content = GetBlockMessage(slackUserID,
											$"Sorry {username}, I couldn't find a course named " +
											$"_{words[0]}_");
									}
								}
							}
							else
							{
								content = GetBlockMessage(slackUserID,
									"Which course would you like to *stop*?");
							}
							break;
					}
				}
				else
				{
					//	No command found.
					content = await GetCommandNotFoundText(slackUserID, userText);
				}
			}
			if(respondAsync)
			{
				//	Send a message to the user through message hook.
				if(content?.Length > 0)
				{
					SendMessageBlock(content);
				}
				if(courseContent?.Length > 0)
				{
					SendMessageBlock(courseContent);
				}
			}
			return content;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SendGreeting																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Send a greeting message to the specified Slack user.
		/// </summary>
		/// <param name="slackUserID">
		/// Unique identification of the user to whom the greeting will be sent.
		/// </param>
		public static async Task SendGreeting(string slackUserID)
		{
			string content = "";

			if(slackUserID?.Length > 0)
			{
				content = await GetGreetingText(slackUserID);
				SendMessageBlock(content);
				//using(WebClient webClient = new WebClient())
				//{
				//	webClient.Headers.Add(
				//		HttpRequestHeader.ContentType, "application/json");
				//	try
				//	{
				//		webClient.UploadString(
				//			new Uri($"{ResourceMain.SlackWebHook}"), "POST", content);
				//	}
				//	catch(Exception ex)
				//	{
				//		AppendLog(
				//			$"Received: {DateTime.Now.ToString("yyyyMMdd.HHmm")}\r\n" +
				//			$"Error while sending greeting: {ex.Message}\r\n\r\n");
				//	}
				//}
			}

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SendMessageBlock																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Send a message block to the defined message hook.
		/// </summary>
		/// <param name="slackBlockContent">
		/// Serialized Slack block content.
		/// </param>
		/// <returns>
		/// Reference to an awaitable task.
		/// </returns>
		public static void SendMessageBlock(string slackBlockContent)
		{
			////	This section sends to message hook.
			//using(WebClient webClient = new WebClient())
			//{
			//	webClient.Headers.Add(
			//		HttpRequestHeader.ContentType, "application/json");
			//	_ = Task.Run(() => webClient.UploadString(
			//		new Uri($"{ResourceMain.SlackWebHook}"),
			//		"POST", slackBlockContent));
			//}
			//	This section sends to chat.postMessage.
			using(WebClient webClient = new WebClient())
			{
				webClient.Headers.Add(
					HttpRequestHeader.ContentType, "application/json");
				webClient.Headers.Add(
					HttpRequestHeader.Authorization,
					$"Bearer {ResourceMain.SlackBotToken}");
				_ = Task.Run(() => webClient.UploadString(
					new Uri($"{ResourceMain.SlackMethodChatPostMessage}"),
					"POST", slackBlockContent));
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SetBaseUrl																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the base URL for this session.
		/// </summary>
		/// <param name="request">
		/// Reference to the active request.
		/// </param>
		public static void SetBaseUrl(HttpRequest request)
		{
			if(request != null)
			{
				mBaseUrl = String.Format("{0}://{1}", request.Scheme, request.Host);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* StoreNodeFile																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Store a node file to the database.
		/// </summary>
		/// <param name="content">
		/// Node file content, in serialized JSON format.
		/// </param>
		public static void StoreNodeFile(HttpRequest request, string content)
		{
			bool bFound = false;
			byte[] buffer = null;
			char[] chars = null;
			int count = 0;
			string ext = "";
			System.IO.FileInfo fileInfo = null;
			string filename = "";
			int index = 0;
			NodeFileItem nodeFile = null;
			PropertyItem property = null;
			ResourceItem resource = null;
			string ticket = "";

			if(content?.Length > 0)
			{
				SetBaseUrl(request);
				try
				{
					nodeFile = new NodeFileItem();
					NodeDataCollection.DeserializeData(nodeFile, "", content);
					//	Convert all media to local references.
					//	All resource assets will be transferred to local files in
					//	Media/.
					//	All local references will be direct from the nodes, and
					//	the Resources collection will be emptied.
					//	Entering, all resources except links will be data URIs.
					if(nodeFile.Resources.Count > 0)
					{
						//	Stage 1. Save binary files to Media folder.
						foreach(ResourceItem resourceItem in nodeFile.Resources)
						{
							ticket = resourceItem.Ticket;
							switch(resourceItem.ResourceType)
							{
								case "MediaAudio":
									//	Audio not supported on Slack.
									break;
								case "MediaImage":
									if(resourceItem.Uri.StartsWith("data:"))
									{
										//	The image can be saved.
										chars = resourceItem.Uri.ToCharArray();
										count = chars.Length;
										for(index = 0; index < count; index++)
										{
											if(chars[index] == ',')
											{
												index++;
												count = chars.Length - index;
												bFound = true;
												break;
											}
										}
										if(bFound)
										{
											//	End of data header was found.
											fileInfo =
												new System.IO.FileInfo(resourceItem.RelativeFilename);
											ext = fileInfo.Extension;
											if(ext.StartsWith("."))
											{
												ext = ext.Substring(1);
											}
											filename =
												$"Media/{nodeFile.Ticket}_{ticket}.{ext}";
											buffer =
												Convert.FromBase64CharArray(chars, index, count);
											System.IO.File.WriteAllBytes(
												$"wwwroot/{filename}", buffer);
											buffer = null;
											//resourceItem.Uri =
											//	$"{GetBaseURL(request)}/{filename}";
											resourceItem.Uri =
												$"{filename}";
										}
									}
									break;
								case "MediaLink":
									//	Link won't have Data URI loading.
									break;
								case "MediaVideo":
									//	Video not supported on Slack.
									break;
							}
						}
						//	Stage 2 - Replace media references on nodes to actual links.
						foreach(NodeItem node in nodeFile.Nodes)
						{
							property = node.Properties.FirstOrDefault(x =>
								x.Name == "MediaImage");
							if(property != null)
							{
								//	Image property found.
								resource = nodeFile.Resources.FirstOrDefault(x =>
									x.Ticket == property.StringValue());
								if(resource != null)
								{
									//	Transfer the link directly to the node property.
									property.Value = resource.Uri;
								}
								else
								{
									//	No resource found for that reference.
									property.Name = "MediaImageInvalid";
								}
							}
							foreach(SocketItem socket in node.Sockets)
							{
								property = socket.Properties.FirstOrDefault(x =>
									x.Name == "MediaImage");
								if(property != null)
								{
									//	Image property found.
									resource = nodeFile.Resources.FirstOrDefault(x =>
										x.Ticket == property.StringValue());
									if(resource != null)
									{
										//	Transfer the link directly to the socket property.
										property.Value = resource.Uri;
									}
									else
									{
										//	No resource found for that reference.
										property.Name = "MediaImageInvalid";
									}
								}
							}
						}
					}
					//	After finishing with resource transfer, clear the resource
					//	collection.
					nodeFile.Resources.Clear();
					//	Create discrete conversation, nodes, and sockets in the database.
					ConversationCourseCollection.UpdateDatabase(nodeFile);
					////	Prepare the conversation record header.
					//conversationTicket = nodeFile.Ticket;
					//conversationTitle = nodeFile.Name;
					//conversationDescription = nodeFile.Description;
					////	Re-serialize the updated version of the file.
					//content = NodeDataCollection.SerializeData(nodeFile, false);
				}
				catch(Exception ex)
				{
					Debug.WriteLine($"Error Deserializing: {ex.Message}");
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ToInteger																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the safe integer value of the specified object.
		/// </summary>
		/// <param name="value">
		/// Value to be converted to an integer.
		/// </param>
		/// <returns>
		/// Integer representation of the caller's value.
		/// </returns>
		public static int ToInteger(object value)
		{
			string intermediate = "";
			Match match = null;
			double number = 0d;
			int result = 0;

			if(value != null)
			{
				intermediate = value.ToString();
				if(intermediate.Length > 0)
				{
					match = Regex.Match(intermediate, ResourceMain.rxNumeric);
					if(match.Success)
					{
						intermediate = GetValue(match, "value").Replace(",", "");
						if(intermediate.Length > 0 &&
							double.TryParse(intermediate, out number))
						{
							result = (int)number;
						}
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UpdateSlackUserList																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the Slack user list.
		/// </summary>
		public static void UpdateSlackUserList()
		{
			bool bContinue = true;
			string content = "";
			byte[] data = new byte[0];
			SlackUserList slackUserList = null;

			using(WebClient webClient = new WebClient())
			{
				//webClient.Headers.Add(
				//	HttpRequestHeader.ContentType, "application/json");
				webClient.Headers.Add(
					HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
				try
				{
					data = webClient.DownloadData(new Uri(
						$"{ResourceMain.SlackAPIUsersList}?token=" +
						$"{ResourceMain.SlackTeamToken}&pretty=1"));
				}
				catch(Exception ex)
				{
					AppendLog($"Received: {DateTime.Now.ToString("yyyyMMdd.HHmm")}\r\n" +
						$"Error while uploading data: {ex.Message}\r\n\r\n");
					bContinue = false;
				}
			}
			if(bContinue)
			{
				//	Compare received data with existing data.
				content = Encoding.UTF8.GetString(data);
				slackUserList = JsonConvert.DeserializeObject<SlackUserList>(content);
				if(slackUserList != null)
				{
					UpdateSlackUserList(slackUserList.Members);
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Update the Slack user list.
		/// </summary>
		/// <param name="slackUsers">
		/// Collection of slack users to maintain in the database.
		/// </param>
		public static void UpdateSlackUserList(SlackUserCollection slackUsers)
		{
			UserItem user = null;
			UserCollection users = null;
			UserCollection usersUpdate = new UserCollection();

			if(slackUsers?.Count > 0)
			{
				//	Entries were received.
				users = UserCollection.LoadFromDatabase();
				//	Process inserts.
				usersUpdate.Clear();
				foreach(SlackUserItem slackUser in slackUsers)
				{
					if(!users.Exists(x => x.SlackID == slackUser.ID))
					{
						//	This user needs to be added to the database.
						usersUpdate.Add(new UserItem(slackUser));
					}
				}
				if(usersUpdate.Count > 0)
				{
					UserCollection.InsertIntoDatabase(usersUpdate);
				}
				//	Process updates.
				usersUpdate.Clear();
				foreach(SlackUserItem slackUser in slackUsers)
				{
					user = users.FirstOrDefault(x =>
						x.SlackID == slackUser.ID &&
						x.SlackName != slackUser.Name);
					if(user != null)
					{
						//	Existing user with updated name.
						usersUpdate.Add(new UserItem(slackUser));
					}
				}
				if(usersUpdate.Count > 0)
				{
					UserCollection.UpdateUsernamesOnDatabase(usersUpdate);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadLog																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read the log file.
		/// </summary>
		public static string ReadLog()
		{
			return System.IO.File.ReadAllText("Data/Activity.txt");
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
