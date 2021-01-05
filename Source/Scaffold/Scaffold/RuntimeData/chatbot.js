//	chatbot.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
//	------
//	Chatbot emulator core functionality. This code is pasted into the body of
//	the file being emulated.
//	Paste var chatbotdata = { Nodes: [], Resources: [] }; prior to this line.
var currentNode = null;
var entryID = 1;

//	----------
//	getButtons
//	----------
/**
	* Return the array of buttons associated with a node.
	* @param {object} node Node for which buttons will be retrieved.
	* @returns {array} Array of button (socket) objects.
	*/
function getButtons(node)
{
	var count = 0;
	var index = 0;
	var result = [];
	var socket = null;
	var sockets = null;

	if(node)
	{
		sockets = node.Sockets;
		count = sockets.length;
		for(index = 0; index < count; index ++)
		{
			socket = sockets[index];
			if(socket.SocketMode == "Output")
			{
				result.push(socket);
			}
		}
	}
	return result;
}
//	----------

//	----------
//	getNodeByTicket
//	----------
/**
	* Return the node identified by the ticket.
	* @param {string} ticket Globally unique identification of the node.
	* @returns {object} Node item identified by the provided ticket.
	*/
function getNodeByTicket(ticket)
{
	var count = 0;
	var index = 0;
	var node = null;
	var nodes = chatbotdata.Nodes;
	var result = null;

	if(ticket)
	{
		count = nodes.length;
		// console.log("Count: " + count);
		for(index = 0; index < count; index ++)
		{
			node = nodes[index];
			if(node.Ticket == ticket || socketTicketExists(node, ticket))
			{
				result = node;
				break;
			}
		}
	}
	return result;
}
//	----------

//	----------
//	getPropertyValue
//	----------
/**
	* Return the value of the specified property from the provided object.
	* @param {object} node Object containing the property to read.
	* @param {string} propertyName Name of the property to read.
	* @returns {string} Value of the specified property.
	*/
function getPropertyValue(node, propertyName)
{
	var count = 0;
	var index = 0;
	var property = null;
	var result = "";

	if(node)
	{
		count = node.Properties.length;
		for(index = 0; index < count; index ++)
		{
			property = node.Properties[index];
			if(property.Name == propertyName)
			{
				result = property.Value;
			}
		}
	}
	return result;
}
//	----------

//	----------
// getResourceText
//	----------
/**
	* Return the link resource text referenced in the provided properties collection.
	* @param {array} properties Collection of properties to inspect.
	* @param {string} propertyName Name of the resource property to retrieve.
	* @returns {string} Resource text information, if found. Otherwise, null.
	*/
function getResourceText(properties, propertyName)
{
	var count = 0;
	var index = 0;
	var property = null;
	var resources = [];
	var result = "";
	var ticket = "";

	if(properties)
	{
		count = properties.length;
		for(index = 0; index < count; index ++)
		{
			property = properties[index];
			if(property.Name == propertyName)
			{
				//	Property found.
				ticket = property.Value.toLowerCase();
				break;
			}
		}
		if(ticket)
		{
			resources = chatbotdata.Resources;
			count = resources.length;
			for(index = 0; index < count; index ++)
			{
				resource = resources[index];
				if(resource && resource.Ticket.toLowerCase() == ticket)
				{
					//	Resource found.
					result = getPropertyValue(resource, "Text");
					if(!result && propertyName == "MediaLink")
					{
						//	There was no text property on the resource.
						//	Return the link instead.
						result = resource.Uri;
					}
					break;
				}
			}
		}
	}
	return result;
}
//	----------

//	----------
// getResourceUri
//	----------
/**
	* Return the resource URI referenced in the provided properties collection.
	* @param {array} properties Collection of properties to inspect.
	* @param {string} propertyName Name of the resource property to retrive.
	* @returns {string} Resource URI data, if found. Otherwise, empty string.
	*/
function getResourceUri(properties, propertyName)
{
	var count = 0;
	var index = 0;
	var property = null;
	var resources = [];
	var result = "";
	var ticket = "";

	// console.log(`getResourceUri ${propertyName}`);
	if(properties)
	{
		count = properties.length;
		for(index = 0; index < count; index ++)
		{
			property = properties[index];
			if(property.Name == propertyName)
			{
				//	Property found.
				ticket = property.Value.toLowerCase();
				// console.log(` Property found: ${ticket}`);
				break;
			}
		}
		if(ticket)
		{
			resources = chatbotdata.Resources;
			count = resources.length;
			for(index = 0; index < count; index ++)
			{
				resource = resources[index];
				if(resource && resource.Ticket.toLowerCase() == ticket)
				{
					//	Resource found.
					result = resource.Uri;
					// console.log(` Resource found: ${result}`);
					break;
				}
			}
		}
	}
	return result;
}
//	----------

//	----------
// getSocketBySocketMode
//	----------
/**
	* Return the first socket matching the specified mode.
	* @param {object} node Reference to the node containing the sockets to search.
	* @param {string} socketMode Mode name of the socket to find.
	* @returns {object} Reference to the socket object, if found. Otherwise, null.
	*/
function getSocketBySocketMode(node, socketMode)
{
	var count = 0;
	var index = 0;
	var result = null;
	var socket = null;
	var sockets = [];

	// console.log("getSocketBySocketMode...");
	if(node && socketMode)
	{
		sockets = node.Sockets;
		count = sockets.length;
		// console.log(` Socket and mode are present. Count: ${count}`);
		for(index = 0; index < count; index ++)
		{
			socket = sockets[index];
			// console.log(` Socket. Type: ${socket.SocketMode}; Ticket: ${socket.Ticket}`);
			if(socket.SocketMode == socketMode)
			{
				//	Socket found.
				// console.log(" Socket found...");
				result = socket;
				break;
			}
		}
	}
	return result;
}
//	----------

//	----------
//	getTicket
//	----------
/**
	* Return the ticket of the closest item or ancestor having a ticket.
	* @param {object} element HTML element for which to retrieve the ticket attribute.
	* @returns {string} Globally unique identification of the requested ticket.
	*/
function getTicket(element)
{
	var ticket = "";

	if(element)
	{
		ticket = element.getAttribute("ticket");
		if(ticket)
		{
			console.log("Ticket found...");
		}
		if(!ticket)
		{
			if(element.parentElement)
			{
				console.log(`Ticket not found at current level. Going to parent ${element.parentElement.tagName}.`);
				ticket = getTicket(element.parentElement);
			}
			else
			{
				console.log(`No more ancestors. Ticket not found...`);
			}
		}
	}
	return ticket;
}
//	----------

//	----------
//	handleResponse
//	----------
/**
	* Handle the user's response to the last node and set up the
	* next card.
	* @param {object} node Node containing the expected responses.
	* @param {string} ticket Globally unique identification of the response.
	*/
function handleResponse(node, ticket)
{
	var button = null;
	var buttons = [];
	var connections = [];
	var content = "";
	var count = 0;
	var hasAudio = false;
	var hasAudioResponse = false;
	var hasImage = false;
	var hasImageResponse = false;
	var hasLink = false;
	var hasLinkResponse = false;
	var hasVideo = false;
	var hasVideoResponse = false;
	var index = 0;
	var nextNode = null;
	var response = null;

	if(node != null)
	{
		console.log("Node not null.");
	}
	if(ticket)
	{
		console.log(`Ticket: ${ticket}`);
	}

	if(node && ticket)
	{
		console.log(`Node not null. Ticket: ${ticket}`);
		currentNode = null;
		buttons = node.Sockets;
		count = buttons.length;
		for(index = 0; index < count; index ++)
		{
			button = buttons[index];
			if(button.Ticket == ticket)
			{
				//	This is the clicked item.
				//	*** Build Response Item ***
				console.log("Clicked item found...");
				//	Get media status.
				hasAudio = hasResource(button.Properties, "MediaAudio");
				hasImage = hasResource(button.Properties, "MediaImage");
				hasLink = hasResource(button.Properties, "MediaLink");
				hasVideo = hasResource(button.Properties, "MediaVideo");
				//	Check for next connections.
				connections = button.Connections;
				if(connections.length > 0)
				{
					currentNode = getNodeByTicket(connections[0]);
				}
				if(currentNode)
				{
					//	Response node on next question has been found.
					response = getSocketBySocketMode(currentNode, "Input");
					if(response)
					{
						// console.log(`Response found. ${response.Ticket}`);
						hasAudioResponse = hasResource(response.Properties, "MediaAudio");
						hasImageResponse = hasResource(response.Properties, "MediaImage");
						hasLinkResponse = hasResource(response.Properties, "MediaLink");
						hasVideoResponse = hasResource(response.Properties, "MediaVideo");
						// console.log(
						// 	`Audio: ${hasAudioResponse}, ` +
						// 	`Image: ${hasImageResponse}, ` +
						// 	`Link: ${hasLinkResponse}, ` +
						// 	`Video: ${hasVideoResponse}`);
					}
				}
				if(node.NodeType != "Delay")
				{
					//	Container.
					content = `<div class="hero-card answer">` +
					//	Title area.
					`<p class="title">${getPropertyValue(button, "Answer")}</p>` +
					//	Image area.
					(hasImage ?
						`<div class="image">
						<img src="${getResourceUri(button.Properties, "MediaImage")}" />
						</div>` :
						(hasImageResponse ?
							`<div class="image">
							<img src="${getResourceUri(response.Properties, "MediaImage")}" />
							</div>` : "")) +
					//	Video area.
					(hasVideo ?
						`<div class="video">
						<video controls>
						<source src="${getResourceUri(button.Properties, "MediaVideo")}">
						</video></div>` :
						(hasVideoResponse ?
							`<div class="video">
							<video controls>
							<source src="${getResourceUri(response.Properties, "MediaVideo")}">
							</video></div>` : "")) +
					//	Audio area.
					(hasAudio ?
						`<div class="audio">
						<audio controls>
						<source src="${getResourceUri(button.Properties, "MediaAudio")}">
						</audio></div>` :
						(hasAudioResponse ?
							`<div class="audio">
							<audio controls>
							<source src="${getResourceUri(response.Properties, "MediaAudio")}">
							</audio></div>` : "")) +
					//	Link area.
					(hasLink ?
						`<div class="link">
						<a href="${getResourceUri(button.Properties, "MediaLink")}">
						${getResourceText(button.Properties, "MediaLink")}</a></div>` :
						(hasLinkResponse ?
							`<div class="link">
							<a href="${getResourceUri(response.Properties, "MediaLink")}">
							${getResourceText(response.Properties, "MediaLink")}
							</a></div>` : "")) +
					//	Container.
					`</div>`;

					// console.log(content);

					//	Add the response.
					$("#chatPanel").append(content);
					$("#chatContainer").animate(
						{scrollTop: $("#chatPanel").height()}, "slow").delay(400);
				}
				//	*** ~Build response item ***
				break;
			}
		}
	}
	//	*** Build next question ***
	if(currentNode)
	{
		// console.log("Current node defined...");
		//	Get media status.
		hasAudio = hasResource(currentNode.Properties, "MediaAudio");
		hasImage = hasResource(currentNode.Properties, "MediaImage");
		hasLink = hasResource(currentNode.Properties, "MediaLink");
		hasVideo = hasResource(currentNode.Properties, "MediaVideo");
		//	Build the card.
		//	Container.
		content = `<div class="hero-card question">` +
		//	Title area.
		`<p class="title">${getPropertyValue(currentNode, "Question")}</p>` +
		//	Image area.
		(hasImage ?
			`<div class="image">
			<img src="${getResourceUri(currentNode.Properties, "MediaImage")}" />
			</div>` : "") +
		//	Video area.
		(hasVideo ?
			`<div class="video">
			<video controls>
			<source src="${getResourceUri(currentNode.Properties, "MediaVideo")}">
			</video></div>` : "") +
		//	Audio area.
		(hasAudio ?
			`<div class="audio">
			<audio controls>
			<source src="${getResourceUri(currentNode.Properties, "MediaAudio")}">
			</video></div>` : "") +
		//	Link area.
		(hasLink ?
			`<div class="link">
			<a href="${getResourceUri(currentNode.Properties, "MediaLink")}">
			${getResourceText(currentNode.Properties, "MediaLink")}</a></div>` : "") +
		//	Buttons area.
		`<div id="buttons${entryID}" class="buttons">
		</div>` +
		//	Container.
		`</div>`;
		$("#chatPanel").append(content);
		//	Buttons on next question.
		buttons = getButtons(currentNode);
		count = buttons.length;
		nextNode = null;
		// console.log(`Button count: ${count}...`);
		if(currentNode.NodeType != "Delay")
		{
			for(index = 0; index < count; index ++)
			{
				hasAudioResponse = false;
				hasImageResponse = false;
				hasLinkResponse = false;
				hasVideoResponse = false;
				button = buttons[index];
				hasAudio = hasResource(button.Properties, "MediaAudio");
				hasImage = hasResource(button.Properties, "MediaImage");
				hasLink = hasResource(button.Properties, "MediaLink");
				hasVideo = hasResource(button.Properties, "MediaVideo");
				connections = button.Connections;
				if(connections && connections.length > 0)
				{
					nextNode = getNodeByTicket(connections[0]);
				}
				if(nextNode)
				{
					//	Response node on next question has been found.
					response = getSocketBySocketMode(nextNode, "Input");
					if(response)
					{
						// console.log(`Response found. ${response.Ticket}`);
						hasAudioResponse = hasResource(response.Properties, "MediaAudio");
						hasImageResponse = hasResource(response.Properties, "MediaImage");
						hasLinkResponse = hasResource(response.Properties, "MediaLink");
						hasVideoResponse = hasResource(response.Properties, "MediaVideo");
						// console.log(
						// 	`Audio: ${hasAudioResponse}, ` +
						// 	`Image: ${hasImageResponse}, ` +
						// 	`Link: ${hasLinkResponse}, ` +
						// 	`Video: ${hasVideoResponse}`);
					}
				}
				$(`#buttons${entryID}`).append(
					`<div class="button" ticket="${button.Ticket}">` +
					//	Image area.
					(hasImage ?
						`<div class="image">
						<img src="${getResourceUri(button.Properties, "MediaImage")}" />
						</div>` :
						(hasImageResponse ?
							`<div class="image">
							<img src="${getResourceUri(response.Properties, "MediaImage")}" />
							</div>` : "")) +
						//	Text area.
					`<div class="text">` +
					`${getPropertyValue(button, "Answer")}</div></div>`);
			}
		}
		else
		{
			//	Wait for the specified delay time then click each of the buttons.
			setTimeout(function()
			{
				var ticket = "";
				for(index = 0; index < count; index ++)
				{
					button = buttons[index];
					ticket = button.Ticket;
					handleResponse(currentNode, ticket);
				}
			}, currentNode.Delay * 1000);
		}
		$("#chatContainer").animate(
			{scrollTop: $("#chatPanel").height()}, "slow");
	}
	else
	{
		// console.log("Current node not defined...");
	}
	//	*** ~Build next question ***
	if(currentNode)
	{
		currentNode.EntryID = entryID;
	}
	entryID ++;
	$("#chatContainer").animate(
		{scrollTop: $("#chatPanel").height()}, "slow");
}
//	----------

//	----------
// hasResource
//	----------
/**
	* Return a value indicating whether the provided collection contains
	* the specified media reference.
	* @param {array} properties Collection of properties to search.
	* @param {string} propertyName Name of the property to test for.
	* @returns {boolean} True if the resource reference property was found.
	* Otherwise, false.
	*/
function hasResource(properties, propertyName)
{
	var count = 0;
	var index = 0;
	var property = null;
	var result = false;

	// console.log(`hasResource ${propertyName}`);
	if(properties)
	{
		count = properties.length;
		for(index = 0; index < count; index ++)
		{
			property = properties[index];
			// console.log(` Property. Name: ${property.Name}`);
			if(property.Name == propertyName)
			{
				if(property.Value)
				{
					result = true;
				}
				break;
			}
		}
	}
	return result;
}
//	----------

//	----------
//	socketTicketExists
//	----------
/**
	* Return a value indicating whether the specified ticket identifies any of
	* the sockets on the provided node.
	* @param {object} node Node containing the sockets to inspect.
	* @param {string} ticket Globally unique identification to find.
	* @returns {boolean} True if the ticket identifies any of the sockets of
	* this node. Otherwise, false.
	*/
function socketTicketExists(node, ticket)
{
	var count = 0;
	var index = 0;
	var result = false;
	var socket = null;
	var sockets = [];

	if(node && ticket)
	{
		sockets = node.Sockets;
		count = sockets.length;
		for(index = 0; index < count; index ++)
		{
			socket = sockets[index];
			if(socket.Ticket == ticket)
			{
				result = true;
				break;
			}
		}
	}
	return result;
}
//	----------

//	----------
//	document.ready
//	----------
$(document).ready(function()
{
	var button = null;
	var buttons = [];
	var connections = [];
	var content = "";
	var count = 0;
	var hasAudio = false;
	var hasAudioResponse = false;
	var hasImage = false;
	var hasImageResponse = false;
	var hasLink = false;
	var hasLinkResponse = false;
	var hasVideo = false;
	var hasVideoResponse = false;
	var index = 0;
	var mediaList = [];
	var nextNode = null;
	var response = null;
	var startNode = null;

	//	Process.Start no longer supports URL query string.
	// urlParams = new URLSearchParams(window.location.search);
	$("#chatPanel").empty();
	$("#chatPanel").on("click", ".button", function(e)
	{
		// var ticket = e.target.getAttribute("ticket");
		var ticket = getTicket(e.target);
		e.preventDefault();
		$(`#buttons${currentNode.EntryID} .button`).attr("class", "button-used");
		handleResponse(currentNode, ticket);
	});
	if(startNodeTicket)
	{
		//	Start from the specified ticket.
		chatbotdata.Nodes.some(function(e)
		{
			if(e.Ticket == startNodeTicket)
			{
				startNode = e;
				return true;
			}
		});
	}
	else
	{
		//	No start node specified.
		chatbotdata.Nodes.some(function(e)
		{
			if(e.NodeType == "Start")
			{
				startNode = e;
				return true;
			}
		});
	}
	currentNode = startNode;
	currentNode.EntryID = entryID;
	if(startNode)
	{
		console.log(`Starting node: ${startNode.Ticket}`);
		//	Get media status.
		mediaCount = 0;
		hasAudio = hasResource(currentNode.Properties, "MediaAudio");
		hasImage = hasResource(currentNode.Properties, "MediaImage");
		hasLink = hasResource(currentNode.Properties, "MediaLink");
		hasVideo = hasResource(currentNode.Properties, "MediaVideo");
		if(hasAudio)
		{
			mediaList.push("Audio");
		}
		if(hasImage)
		{
			mediaList.push("Image");
		}
		if(hasLink)
		{
			mediaList.push("Link");
		}
		if(hasVideo)
		{
			mediaList.push("Video");
		}
		if(mediaList.length == 0)
		{
			mediaList.push("no");
		}
		console.log(`Start node has ${mediaList.join(", ")} media.`);
		//	Build the card.
		//	Container.
		content = `<div class="hero-card question">` +
		//	Title area.
		`<p class="title">${getPropertyValue(startNode, "Question")}</p>` +
		//	Image area.
		(hasImage ?
			`<div class="image">
			<img src="${getResourceUri(currentNode.Properties, "MediaImage")}" />
			</div>` : "") +
		//	Video area.
		(hasVideo ?
			`<div class="video">
			<video controls>
			<source src="${getResourceUri(currentNode.Properties, "MediaVideo")}">
			</video></div>` : "") +
		//	Audio area.
		(hasAudio ?
			`<div class="audio">
			<audio controls>
			<source src="${getResourceUri(currentNode.Properties, "MediaAudio")}">
			</video></div>` : "") +
		//	Link area.
		(hasLink ?
			`<div class="link">
			<a href="${getResourceUri(currentNode.Properties, "MediaLink")}"
			target="_blank">
			${getResourceText(currentNode.Properties, "MediaLink")}</a></div>` : "") +
		//	Buttons area.
		`<div id="buttons${entryID}" class="buttons">
		</div>` +
		//	Container.
		`</div>`;

		$("#chatPanel").append(content);
		buttons = getButtons(startNode);
		count = buttons.length;
		if(currentNode.NodeType != "Delay")
		{
			for(index = 0; index < count; index ++)
			{
				hasAudioResponse = false;
				hasImageResponse = false;
				hasLinkResponse = false;
				hasVideoResponse = false;
				button = buttons[index];
				hasAudio = hasResource(button.Properties, "MediaAudio");
				hasImage = hasResource(button.Properties, "MediaImage");
				hasLink = hasResource(button.Properties, "MediaLink");
				hasVideo = hasResource(button.Properties, "MediaVideo");
				connections = button.Connections;
				if(connections.length > 0)
				{
					nextNode = getNodeByTicket(connections[0]);
				}
				if(nextNode)
				{
					//	Response node on next question has been found.
					response = getSocketBySocketMode(nextNode, "Input")
					if(response)
					{
						hasAudioResponse = hasResource(response.Properties, "MediaAudio");
						hasImageResponse = hasResource(response.Properties, "MediaImage");
						hasLinkResponse = hasResource(response.Properties, "MediaLink");
						hasVideoResponse = hasResource(response.Properties, "MediaVideo");
					}
				}

/*
				Original.
				$(`#buttons${entryID}`).append(
					`<div class="button" ticket="${button.Ticket}">
					${getPropertyValue(button, "Answer")}</div>`);
*/
				$(`#buttons${entryID}`).append(
					`<div class="button" ticket="${button.Ticket}">` +
					//	Image area.
					(hasImage ?
						`<div class="image">
						<img src="${getResourceUri(button.Properties, "MediaImage")}" />
						</div>` :
						(hasImageResponse ?
							`<div class="image">
							<img src="${getResourceUri(response.Properties, "MediaImage")}" />
							</div>` : "")) +
						//	Text area.
					`<div class="text">` +
					`${getPropertyValue(button, "Answer")}</div></div>`);

			}
		}
		else
		{
			//	Wait for the specified delay time then click each of the buttons.
			setTimeout(function()
			{
				var ticket = "";
				for(index = 0; index < count; index ++)
				{
					button = buttons[index];
					ticket = button.Ticket;
					handleResponse(currentNode, ticket);
				}
			}, currentNode.Delay * 1000);
		}
	}
	entryID ++;
});
//	----------
