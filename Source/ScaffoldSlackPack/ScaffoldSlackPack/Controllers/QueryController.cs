//	QueryController.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;

using ScaffoldSlackPack.Models;
using static ScaffoldSlackPack.ScaffoldSlackPackUtil;

namespace ScaffoldSlackPack.Controllers
{
	//*-------------------------------------------------------------------------*
	//*	QueryController																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Manual query interface.
	/// </summary>
	[Route("Query")]
	[ApiController]
	public class QueryController : Controller
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
		//* Inject																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Inject data from the client into the server, allowing emulation of
		/// data being received from external sources.
		/// </summary>
		/// <returns>
		/// Response text.
		/// </returns>
		[Route("Inject")]
		public async Task<ContentResult> Inject()
		{
			string content = "";
			RequestItem request = null;
			List<string> parameters = new List<string>();
			string result = "";
			SlackUserList slackUserList = null;

			using(StreamReader reader =
				new StreamReader(Request.Body, Encoding.UTF8))
			{
				content = await reader.ReadToEndAsync();
			}
			Debug.WriteLine($"Client injection content: {content}");
			if(content?.Length > 0)
			{
				request = JsonConvert.DeserializeObject<RequestItem>(content);
			}
			if(request != null)
			{
				switch(request.Request)
				{
					case "delete.conversation":
						//	Remove the course from the database.
						if(request.Data?.Length > 0)
						{
							result = RemoveNodeFile(request.Data.Replace("&dquote;", "\""));
						}
						break;
					case "get.block.question":
						//	Return the formatted question message for a node.
						parameters = request.Data.Split(',').ToList();
						if(parameters.Count > 1)
						{
							result = GetBlockQuestion(
								parameters[0].Trim(), parameters[1].Trim());
						}
						else
						{
							result = "Get block question reqires two parameters: " +
								"slackUserID, nodeItemTicket";
						}
						break;
					case "get.command.not.found":
						//	Get the message for command not found.
						parameters = request.Data.Split(',').ToList();
						if(parameters.Count > 1)
						{
							result = await GetCommandNotFoundText(
								parameters[0].Trim(), parameters[1].Trim());
						}
						else
						{
							result = "Command not found reqires two parameters: " +
								"slackUserID, inputMessageText";
						}
						break;
					case "get.conversation.list":
						//	Get conversational course list.
						result = await GetCourseList(request.Data);
						break;
					case "get.greeting.text":
						//	Get greeting text block.
						result = await GetGreetingText(request.Data);
						break;
					case "get.username":
						//	Lookup username.
						result = await GetSlackUsername(request.Data);
						break;
					case "post.interaction.response":
						//	Interaction response data received.
						result = await ProcessUserInteraction(request.Data);
						break;
					case "post.user.message":
						//	Post a message from the user.
						parameters = request.Data.Split(',').ToList();
						if(parameters.Count > 1)
						{
							result = await ProcessUserMessage(
								parameters[0], parameters[1], false);
						}
						else
						{
							result = "User message reqires two parameters: " +
								"slackUserID, inputMessageText";
						}
						break;
					case "send.greeting":
						//	Send a greeting to the specified individual.
						await SendGreeting(request.Data);
						break;
					case "users.list":
						//	Check on members in the users list.
						if(request.Data?.Length > 0)
						{
							slackUserList =
								JsonConvert.DeserializeObject<SlackUserList>(
									request.Data.Replace("&dquote;", "\""));
							if(slackUserList != null)
							{
								UpdateSlackUserList(slackUserList.Members);
							}
						}
						break;
				}
			}
			return Content(result);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Submit																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Submit a query as clear text in the post body and receive a JSON
		/// resultset.
		/// </summary>
		[Route("Submit")]
		public async Task<QueryRecordCollection> Submit()
		{
			SqliteCommand command = null;
			string content = "";
			int count = 0;
			SqliteDataReader dataReader = null;
			int index = 0;
			Dictionary<string, string> record = null;
			//QueryRecordItem record = null;
			RequestItem request = null;
			QueryRecordCollection result = new QueryRecordCollection();

			using(StreamReader reader =
				new StreamReader(Request.Body, Encoding.UTF8))
			{
				content = await reader.ReadToEndAsync();
			}
			Debug.WriteLine($"Client query content: {content}");
			if(content?.Length > 0)
			{
				request = JsonConvert.DeserializeObject<RequestItem>(content);
				if(request != null)
				{
					content = request.Request;
					Debug.WriteLine($"Client query: {content}");
					if(content.Length > 0)
					{
						using(SqliteConnection connection =
							new SqliteConnection("Data Source = Data/ScaffoldSlackPack.db"))
						{
							connection.Open();
							command = connection.CreateCommand();
							command.CommandText = content;
							try
							{
								dataReader = command.ExecuteReader();
								while(dataReader.Read())
								{
									record = new Dictionary<string, string>();
									result.Add(record);
									count = dataReader.FieldCount;
									for(index = 0; index < count; index ++)
									{
										record.Add(
											dataReader.GetName(index),
											dataReader.GetValue(index).ToString());
									}
								}
							}
							catch(Exception ex)
							{
								Debug.WriteLine($"Query: Error on Select - {ex.Message}");
							}
						}
					}
				}
			}

			return result;
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
