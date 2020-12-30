//	ScaffoldSlackPackController.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

using static ScaffoldSlackPack.ScaffoldSlackPackUtil;

namespace ScaffoldSlackPack
{
	//*-------------------------------------------------------------------------*
	//*	ScaffoldSlackPackController																							*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Main controller for the Scaffold Slack Pack server service.
	/// </summary>
	[Route("ScaffoldSlackPack")]
	[ApiController]
	public class ScaffoldSlackPackController : ControllerBase
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	ProcessJsonObject																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Process the provided JSON element and potentially return a message
		/// from the operation.
		/// </summary>
		private static string ProcessJsonObject(JsonElement element)
		{
			string appID = "";
			string channelID = "";
			//string entry = "";
			string eventID = "";
			string eventText = "";
			string eventTime = "";
			string eventType = "";
			JsonElement property;
			JsonElement property2;
			string result = "";
			string teamID = "";
			string typeName = "";
			//JsonElement user;
			string userID = "";
			List<string> userList = new List<string>();
			//string username = "";
			//JsonElement.ArrayEnumerator users;

			if(element.TryGetProperty("type", out property))
			{
				typeName = property.GetString();
				switch(typeName)
				{
					case "event_callback":
						//	App ID.
						if(element.TryGetProperty("api_app_id", out property))
						{
							appID = property.GetString();
						}
						//	Event ID.
						if(element.TryGetProperty("event_id", out property))
						{
							eventID = property.GetString();
						}
						//	Event object.
						if(element.TryGetProperty("event", out property))
						{
							//	Event is present.
							if(property.TryGetProperty("type", out property2))
							{
								//	Event type is present.
								eventType = property2.GetString();
							}
							if(property.TryGetProperty("subtype", out property2))
							{
								//	Sub-type is present.
								if(property2.GetString() == "bot_message")
								{
									eventType += ".bot";
								}
							}
							if(property.TryGetProperty("user", out property2))
							{
								//	User ID is present.
								userID = property2.GetString();
							}
							if(property.TryGetProperty("channel", out property2))
							{
								//	Channel ID is present.
								channelID = property2.GetString();
							}
							if(property.TryGetProperty("text", out property2))
							{
								//	Text is present.
								eventText = property2.GetString();
							}
						}
						//	Event time.
						if(element.TryGetProperty("event_time", out property))
						{
							eventTime = property.GetDouble().ToString();
						}
						//	Team ID.
						if(element.TryGetProperty("team_id", out property))
						{
							teamID = property.GetString();
						}
						switch(eventType)
						{
							case "app_home_opened":
								//	The user opened the channel. Send a message.
								_ = SendGreeting(userID);
								break;
							case "message":
								//	The user has sent a message. Process as conversation.
								if(!Regex.IsMatch(eventText,
									@"(?i:(?s:this\s+content\s+can't\s+be\s+displayed))"))
								//if(eventText.ToLower().IndexOf(
								//	"this content can't be displayed") == -1)
								{
									_ = ProcessUserMessage(userID, eventText, true);
								}
								//else
								//{
								//	AppendLog("Received: Event - " +
								//		$"{DateTime.Now.ToString("yyyyMMdd.HHmm")}\r\n" +
								//		$"Previous content could not be displayed.\r\n\r\n");
								//}
								break;
							case "message.bot":
								break;
						}
						AppendLog(
							"Event: " +
							$"{appID}, {eventID}, {eventType}, {eventTime}, " +
							$"{teamID}, {userID}");
						break;
					case "url_verification":
						if(element.TryGetProperty("challenge", out property))
						{
							result = property.GetString();
						}
						break;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		/*/// <param name="body">
		/// Reference to the body-level element uploaded as application/json.
		/// </param> */

		//*-----------------------------------------------------------------------*
		//* PublishPackage																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Publish a Node package for use as a chatbot within Slack.
		/// </summary>
		/// <returns>
		/// Content result "200 OK".
		/// </returns>
		/// <remarks>
		/// The current version expects JSON as raw text in the body.
		/// Data format of the body parameter is that of an extended Nodes file.
		/// In general, the Nodes file format is { Nodes: [], Resources: [] }.
		/// This publication expects the following, slightly different, format.
		/// { Ticket: "", Name: "", Description: "", Nodes: [], Resources: [] }
		/// If the project ticket is already present in the database, the existing
		/// record is overwritten. Otherwise, a new record is created.
		/// </remarks>
		[HttpPost("PublishPackage")]
		public async Task<ContentResult> PublishPackage()
		{
			string content = "";

			using(StreamReader reader =
				new StreamReader(Request.Body, Encoding.UTF8))
			{
				content = await reader.ReadToEndAsync();
			}
			StoreNodeFile(HttpContext.Request, content);
			return Content("");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SlackEvent																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Process a Slack event and return a corresponding result.
		/// </summary>
		[HttpPost("SlackEvent")]
		public async Task<ContentResult> SlackEvent()
		{
			JsonElement body;
			string content = "";
			JsonElement.ArrayEnumerator enumerator;
			JsonElement property;
			string result = "";

			SetBaseUrl(HttpContext.Request);
			using(StreamReader reader =
				new StreamReader(Request.Body, Encoding.UTF8))
			{
				content = await reader.ReadToEndAsync();
			}
			if(content?.Length > 0)
			{
				AppendLog("Received: Event -\r\n" +
					$"{content}\r\n\r\n");
				using(JsonDocument doc = JsonDocument.Parse(content))
				{
					body = doc.RootElement;
					if(body.ValueKind == JsonValueKind.Array)
					{
						//	Process multiple objects.
						enumerator = body.EnumerateArray();
						while(enumerator.MoveNext())
						{
							property = enumerator.Current;
							ProcessJsonObject(property);
						}
					}
					else
					{
						//	Process a single object.
						result = ProcessJsonObject(body);
					}
				}
			}
			return Content(result);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SlackInteraction																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Receive an interaction from a Slack user interface element.
		/// </summary>
		/// <returns>
		/// 200 OK.
		/// </returns>
		[HttpPost("SlackInteraction")]
		public async Task<ContentResult> SlackInteraction()
		{
			string content = "";
			string result = "";

			SetBaseUrl(HttpContext.Request);
			using(StreamReader reader =
				new StreamReader(Request.Body, Encoding.UTF8))
			{
				content = await reader.ReadToEndAsync();
			}
			if(content?.Length > 0)
			{
				result = await ProcessUserInteraction(content);
			}
			return Content(result);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UnpublishPackage																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Unpublish a Node package from use as a chatbot within Slack.
		/// </summary>
		/// <returns>
		/// Content result "200 OK".
		/// </returns>
		/// <remarks>
		/// The current version expects JSON as raw text in the body.
		/// Data format of the body parameter is that of node file descriptor.
		/// This method reads the following format.
		/// { Ticket: "", Name: "", Description: "" }
		/// If the project ticket is not already present in the database, no
		/// action is taken. Otherwise, all related records are deleted.
		/// </remarks>
		[HttpPost("UnpublishPackage")]
		public async Task<ContentResult> UnpublishPackage()
		{
			string content = "";

			using(StreamReader reader =
				new StreamReader(Request.Body, Encoding.UTF8))
			{
				content = await reader.ReadToEndAsync();
			}
			RemoveNodeFile(content);
			return Content("");
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
