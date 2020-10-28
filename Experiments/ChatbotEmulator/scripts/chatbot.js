var chatbotdata = {
	"Nodes":
	[
  {
			"Ticket": "52c728c7-6dc4-4b65-916b-04b5ae909165",
			"TitleProperty": "Question",
			"NodeType": "Start",
			"ZOrder": 72,
			"X": 111.0,
			"Y": 80.0,
			"Width": 228.132813,
			"Height": 294.063751,
			"TitleHeight": 214.355438,
			"Sockets": [
					{
							"Ticket": "22405b07-e335-4844-9747-b6ccb851015f",
							"TitleProperty": "Answer",
							"SocketMode": "Output",
							"X": 331.1328,
							"Y": 307.282532,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 119.0,
							"TextY": 298.355438,
							"TextWidth": 212.132813,
							"TextHeight": 33.8541641,
							"Properties": [
									{
											"Name": "Index",
											"Value": "A"
									},
									{
											"Name": "Answer",
											"Value": "Sure thing, grandma!\r\nI’ll be there."
									}
							],
							"Connections": [
									"7b868a73-0531-4fa7-ab2c-da5b3608e868"
							]
					},
					{
							"Ticket": "d55327bd-3be2-46e0-b183-ffaba9d8bfc6",
							"TitleProperty": "Answer",
							"SocketMode": "Output",
							"X": 331.1328,
							"Y": 345.1367,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 119.0,
							"TextY": 336.2096,
							"TextWidth": 212.132813,
							"TextHeight": 33.8541641,
							"Properties": [
									{
											"Name": "Index",
											"Value": "B"
									},
									{
											"Name": "Answer",
											"Value": "I’d love to, grandma!\r\nShould I wear my mask?"
									}
							],
							"Connections": [
									"3c2db81d-8cef-4cd8-b50a-c0998bc28c4a"
							]
					}
			],
			"Properties": [
					{
							"Name": "Question",
							"Value": "Hi, Jonas! Grandpa is turning 80 next week, and we’re having a huge party to celebrate his birthday. Are you coming?"
					},
					{
							"Name": "StoryPageNumber",
							"Value": "2"
					},
					{
							"Name": "StoryPageX",
							"Value": "0.000"
					},
					{
							"Name": "StoryPageY",
							"Value": "0.000"
					},
					{
							"Name": "StoryShapeType",
							"Value": "Callout"
					},
					{
							"Name": "StoryColorFill",
							"Value": "#FFFFFF"
					},
					{
							"Name": "StoryColorOutline",
							"Value": "#000000"
					},
					{
							"Name": "StoryColorText",
							"Value": "#000000"
					},
					{
							"Name": "StoryFontName",
							"Value": "Tahoma"
					},
					{
							"Name": "StoryFontSize",
							"Value": "10.2"
					}
			]
	},
	{
			"Ticket": "3486a7f0-bb70-431a-91b0-a4b0d3a4b6de",
			"TitleProperty": "Question",
			"NodeType": "Fork",
			"ZOrder": 74,
			"X": 412.192,
			"Y": 250.0,
			"Width": 231.982391,
			"Height": 452.621063,
			"TitleHeight": 335.058533,
			"Sockets": [
					{
							"Ticket": "3c2db81d-8cef-4cd8-b50a-c0998bc28c4a",
							"TitleProperty": "Response",
							"SocketMode": "Input",
							"X": 404.192,
							"Y": 589.9387,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 420.192,
							"TextY": 589.058533,
							"TextWidth": 215.982391,
							"TextHeight": 17.7604141,
							"Properties": [
									{
											"Name": "Response",
											"Value": "Response"
									}
							]
					},
					{
							"Ticket": "d89c134c-d784-45b3-abf7-be7e4b24b834",
							"TitleProperty": "Answer",
							"SocketMode": "Output",
							"X": 636.1744,
							"Y": 619.746033,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 420.192,
							"TextY": 610.819,
							"TextWidth": 215.982391,
							"TextHeight": 33.8541641,
							"Properties": [
									{
											"Name": "Index",
											"Value": "A"
									},
									{
											"Name": "Answer",
											"Value": "Well, maybe it will be alright.\r\nSure, I’ll come."
									}
							],
							"Connections": [
									"7b868a73-0531-4fa7-ab2c-da5b3608e868"
							]
					},
					{
							"Ticket": "5eab4634-f5db-4ae9-8ed9-7eb6ae3723d6",
							"TitleProperty": "Answer",
							"SocketMode": "Output",
							"X": 636.1744,
							"Y": 665.6471,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 420.192,
							"TextY": 648.673157,
							"TextWidth": 215.982391,
							"TextHeight": 49.9479141,
							"Properties": [
									{
											"Name": "Index",
											"Value": "B"
									},
									{
											"Name": "Answer",
											"Value": "Are you sure that’s a good idea? Maybe I can FaceTime with him instead."
									}
							],
							"Connections": [
									"b1810146-0f12-437c-bcbf-557a90fa7569"
							]
					}
			],
			"Properties": [
					{
							"Name": "Question",
							"Value": "Here, let me put you on speakerphone.\r\nNo, we’re not going to be wearing masks. We just want to be together, relax and not think about all this COVID-19 stuff."
					},
					{
							"Name": "StoryPageNumber",
							"Value": "3"
					},
					{
							"Name": "StoryPageX",
							"Value": "0.000"
					},
					{
							"Name": "StoryPageY",
							"Value": "0.000"
					},
					{
							"Name": "StoryShapeType",
							"Value": "Callout"
					},
					{
							"Name": "StoryColorFill",
							"Value": "#FFFFFF"
					},
					{
							"Name": "StoryColorOutline",
							"Value": "#000000"
					},
					{
							"Name": "StoryColorText",
							"Value": "#000000"
					},
					{
							"Name": "StoryFontName",
							"Value": "Tahoma"
					},
					{
							"Name": "StoryFontSize",
							"Value": "10.2"
					}
			]
	},
	{
			"Ticket": "51d254be-35aa-4bea-b7a7-d8a497305e82",
			"TitleProperty": "Question",
			"NodeType": "Fork",
			"ZOrder": 76,
			"X": 714.0,
			"Y": 489.0,
			"Width": 235.538177,
			"Height": 353.678436,
			"TitleHeight": 214.355438,
			"Sockets": [
					{
							"Ticket": "b1810146-0f12-437c-bcbf-557a90fa7569",
							"TitleProperty": "Response",
							"SocketMode": "Input",
							"X": 706.0,
							"Y": 708.235657,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 722.0,
							"TextY": 707.355469,
							"TextWidth": 219.538177,
							"TextHeight": 17.7604141,
							"Properties": [
									{
											"Name": "Response",
											"Value": "Response"
									}
							]
					},
					{
							"Ticket": "9d028396-7312-4193-ad5d-8241b05fbba5",
							"TitleProperty": "Answer",
							"SocketMode": "Output",
							"X": 941.5382,
							"Y": 729.9961,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 722.0,
							"TextY": 729.1159,
							"TextWidth": 219.538177,
							"TextHeight": 17.7604141,
							"Properties": [
									{
											"Name": "Index",
											"Value": "A"
									},
									{
											"Name": "Answer",
											"Value": "I’m really sorry, grandma; I just can’t."
									}
							],
							"Connections": [
									"ef145150-230e-49cf-a768-dcff5b06cfe1"
							]
					},
					{
							"Ticket": "bf424814-3142-47b4-a6a0-06eb07160318",
							"TitleProperty": "Answer",
							"SocketMode": "Output",
							"X": 941.5382,
							"Y": 767.8503,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 722.0,
							"TextY": 750.876343,
							"TextWidth": 219.538177,
							"TextHeight": 49.9479141,
							"Properties": [
									{
											"Name": "Index",
											"Value": "B"
									},
									{
											"Name": "Answer",
											"Value": "I would love to see him, too! But I want to protect both of you.\r\nI don’t want you guys to get sick."
									}
							],
							"Connections": [
									"e64747b8-62e2-4dfe-ba5e-1f420d7eb7d7"
							]
					},
					{
							"Ticket": "083fbfde-7307-4e03-a01e-a9b581c918c6",
							"TitleProperty": "Answer",
							"SocketMode": "Output",
							"X": 941.5382,
							"Y": 813.751343,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 722.0,
							"TextY": 804.8243,
							"TextWidth": 219.538177,
							"TextHeight": 33.8541641,
							"Properties": [
									{
											"Name": "Index",
											"Value": "C"
									},
									{
											"Name": "Answer",
											"Value": "Well, I don’t want to let grandpa down. Ok, I’ll be there."
									}
							],
							"Connections": [
									"7b868a73-0531-4fa7-ab2c-da5b3608e868"
							]
					}
			],
			"Properties": [
					{
							"Name": "Question",
							"Value": "Jonas, it’s no big deal. Look, you know grandpa would love to see you. It would make his day. Please come, won’t you?"
					},
					{
							"Name": "StoryPageNumber",
							"Value": "4"
					},
					{
							"Name": "StoryPageX",
							"Value": "0.000"
					},
					{
							"Name": "StoryPageY",
							"Value": "0.000"
					},
					{
							"Name": "StoryShapeType",
							"Value": "Callout"
					},
					{
							"Name": "StoryColorFill",
							"Value": "#FFFFFF"
					},
					{
							"Name": "StoryColorOutline",
							"Value": "#000000"
					},
					{
							"Name": "StoryColorText",
							"Value": "#000000"
					},
					{
							"Name": "StoryFontName",
							"Value": "Tahoma"
					},
					{
							"Name": "StoryFontSize",
							"Value": "10.2"
					}
			]
	},
	{
			"Ticket": "4bb89d8d-3e25-4a78-96a6-0428c87a08f8",
			"TitleProperty": "Question",
			"NodeType": "Delay",
			"ZOrder": 78,
			"X": 1039.6,
			"Y": 510.6,
			"Width": 228.906219,
			"Height": 201.524719,
			"TitleHeight": 154.003876,
			"Delay": 2.5,
			"Sockets": [
					{
							"Ticket": "ef145150-230e-49cf-a768-dcff5b06cfe1",
							"TitleProperty": "Response",
							"SocketMode": "Input",
							"X": 1031.6,
							"Y": 669.4841,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 1047.6,
							"TextY": 668.6039,
							"TextWidth": 212.906219,
							"TextHeight": 17.7604141,
							"Properties": [
									{
											"Name": "Response",
											"Value": "Response"
									}
							]
					},
					{
							"Ticket": "4ec2e7b0-dc3a-4197-bbb6-b14af2cbf359",
							"TitleProperty": "Answer",
							"SocketMode": "Output",
							"X": 1260.50623,
							"Y": 691.2445,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 1047.6,
							"TextY": 690.3643,
							"TextWidth": 212.906219,
							"TextHeight": 17.7604141,
							"Properties": [
									{
											"Name": "Index",
											"Value": "A"
									},
									{
											"Name": "Answer",
											"Value": "Result"
									}
							],
							"Connections": [
									"15e8c713-8918-4640-9811-dd52aac74223"
							]
					}
			],
			"Properties": [
					{
							"Name": "Question",
							"Value": "I’m sorry, too, Jonas. I guess we’ll just have to have the party without you."
					},
					{
							"Name": "StoryPageNumber",
							"Value": "5"
					},
					{
							"Name": "StoryPageX",
							"Value": "0.000"
					},
					{
							"Name": "StoryPageY",
							"Value": "0.000"
					},
					{
							"Name": "StoryShapeType",
							"Value": "Callout"
					},
					{
							"Name": "StoryColorFill",
							"Value": "#FFFFFF"
					},
					{
							"Name": "StoryColorOutline",
							"Value": "#000000"
					},
					{
							"Name": "StoryColorText",
							"Value": "#000000"
					},
					{
							"Name": "StoryFontName",
							"Value": "Tahoma"
					},
					{
							"Name": "StoryFontSize",
							"Value": "10.2"
					}
			]
	},
	{
			"Ticket": "35c555b8-174c-4203-a242-59ea9efe8895",
			"TitleProperty": "Question",
			"NodeType": "Termination",
			"ZOrder": 80,
			"X": 1329.2,
			"Y": 507.2,
			"Width": 219.835938,
			"Height": 209.940033,
			"TitleHeight": 184.179657,
			"Sockets": [
					{
							"Ticket": "15e8c713-8918-4640-9811-dd52aac74223",
							"TitleProperty": "Response",
							"SocketMode": "Input",
							"X": 1321.2,
							"Y": 696.2598,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 1337.2,
							"TextY": 695.379639,
							"TextWidth": 203.835938,
							"TextHeight": 17.7604141,
							"Properties": [
									{
											"Name": "Response",
											"Value": "Response"
									}
							]
					}
			],
			"Properties": [
					{
							"Name": "Question",
							"Value": "You feel like you did the right thing by not going, but you wish the conversation had gone better."
					},
					{
							"Name": "StoryPageNumber",
							"Value": "5"
					},
					{
							"Name": "StoryPageX",
							"Value": "0.000"
					},
					{
							"Name": "StoryPageY",
							"Value": "0.000"
					},
					{
							"Name": "StoryShapeType",
							"Value": "Callout"
					},
					{
							"Name": "StoryColorFill",
							"Value": "#FFFFFF"
					},
					{
							"Name": "StoryColorOutline",
							"Value": "#000000"
					},
					{
							"Name": "StoryColorText",
							"Value": "#000000"
					},
					{
							"Name": "StoryFontName",
							"Value": "Tahoma"
					},
					{
							"Name": "StoryFontSize",
							"Value": "10.2"
					}
			]
	},
	{
			"Ticket": "e9d82e2c-a085-4ea5-ac5f-74ace979dd45",
			"TitleProperty": "Question",
			"NodeType": "Fork",
			"ZOrder": 115,
			"X": 405.400024,
			"Y": 716.8,
			"Width": 239.171875,
			"Height": 516.996,
			"TitleHeight": 335.058533,
			"Sockets": [
					{
							"Ticket": "e64747b8-62e2-4dfe-ba5e-1f420d7eb7d7",
							"TitleProperty": "Response",
							"SocketMode": "Input",
							"X": 397.400024,
							"Y": 1056.73877,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 413.400024,
							"TextY": 1055.85852,
							"TextWidth": 223.171875,
							"TextHeight": 17.7604141,
							"Properties": [
									{
											"Name": "Response",
											"Value": "Response"
									}
							]
					},
					{
							"Ticket": "6321cb24-e2e0-4988-8fd4-d700b047a540",
							"TitleProperty": "Answer",
							"SocketMode": "Output",
							"X": 636.5719,
							"Y": 1094.5929,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 413.400024,
							"TextY": 1077.6189,
							"TextWidth": 223.171875,
							"TextHeight": 49.9479141,
							"Properties": [
									{
											"Name": "Index",
											"Value": "A"
									},
									{
											"Name": "Answer",
											"Value": "COVID-19 totally different from the flu! Haven’t you been watching the news?"
									}
							],
							"Connections": [
									"bf832b7f-185d-4354-a1f0-c09d8afd3e1d"
							]
					},
					{
							"Ticket": "0d9cec30-6454-4941-97b2-1cf93b6e30ad",
							"TitleProperty": "Answer",
							"SocketMode": "Output",
							"X": 636.5719,
							"Y": 1172.6814,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 413.400024,
							"TextY": 1131.56677,
							"TextWidth": 223.171875,
							"TextHeight": 98.2291641,
							"Properties": [
									{
											"Name": "Index",
											"Value": "B"
									},
									{
											"Name": "Answer",
											"Value": "I’d be happy to show you some resources about COVID-19. I don’t think that’s the problem we’re trying to solve right now, though. Let’s talk about how we can have grandpa’s party safely."
									}
							],
							"Connections": [
									"8274b0de-bb5c-43e8-82e1-8ac0838b212d"
							]
					}
			],
			"Properties": [
					{
							"Name": "Question",
							"Value": "Well, it seems like you’re blowing this all out of proportion. Honestly, I don’t know what all the fuss is about. We have flu season every year, and no one goes crazy over that."
					},
					{
							"Name": "StoryPageNumber",
							"Value": "6"
					},
					{
							"Name": "StoryPageX",
							"Value": "0.000"
					},
					{
							"Name": "StoryPageY",
							"Value": "0.000"
					},
					{
							"Name": "StoryShapeType",
							"Value": "Callout"
					},
					{
							"Name": "StoryColorFill",
							"Value": "#FFFFFF"
					},
					{
							"Name": "StoryColorOutline",
							"Value": "#000000"
					},
					{
							"Name": "StoryColorText",
							"Value": "#000000"
					},
					{
							"Name": "StoryFontName",
							"Value": "Tahoma"
					},
					{
							"Name": "StoryFontSize",
							"Value": "10.2"
					}
			]
	},
	{
			"Ticket": "525f6005-aa26-47a7-9bb6-60ef97d0b022",
			"TitleProperty": "Question",
			"NodeType": "Delay",
			"ZOrder": 84,
			"X": 1013.6,
			"Y": 822.6,
			"Width": 228.979462,
			"Height": 261.876251,
			"TitleHeight": 214.355438,
			"Delay": 2.5,
			"Sockets": [
					{
							"Ticket": "bf832b7f-185d-4354-a1f0-c09d8afd3e1d",
							"TitleProperty": "Response",
							"SocketMode": "Input",
							"X": 1005.6,
							"Y": 1041.83569,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 1021.6,
							"TextY": 1040.95544,
							"TextWidth": 212.979462,
							"TextHeight": 17.7604141,
							"Properties": [
									{
											"Name": "Response",
											"Value": "Response"
									}
							]
					},
					{
							"Ticket": "1ada5d38-d18d-4077-8601-160d0ba787b0",
							"TitleProperty": "Answer",
							"SocketMode": "Output",
							"X": 1234.57947,
							"Y": 1063.59607,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 1021.6,
							"TextY": 1062.71582,
							"TextWidth": 212.979462,
							"TextHeight": 17.7604141,
							"Properties": [
									{
											"Name": "Index",
											"Value": "A"
									},
									{
											"Name": "Answer",
											"Value": "Result"
									}
							],
							"Connections": [
									"2d061010-90c7-4575-88f3-a067e27af402"
							]
					}
			],
			"Properties": [
					{
							"Name": "Question",
							"Value": "Look, I don’t need you to tell me what to think.\r\nI’m sorry, Jonas, but I just think you’re overreacting."
					},
					{
							"Name": "StoryPageNumber",
							"Value": "7"
					},
					{
							"Name": "StoryPageX",
							"Value": "0.000"
					},
					{
							"Name": "StoryPageY",
							"Value": "0.000"
					},
					{
							"Name": "StoryShapeType",
							"Value": "Callout"
					},
					{
							"Name": "StoryColorFill",
							"Value": "#FFFFFF"
					},
					{
							"Name": "StoryColorOutline",
							"Value": "#000000"
					},
					{
							"Name": "StoryColorText",
							"Value": "#000000"
					},
					{
							"Name": "StoryFontName",
							"Value": "Tahoma"
					},
					{
							"Name": "StoryFontSize",
							"Value": "10.2"
					}
			]
	},
	{
			"Ticket": "38700b55-a7ab-4a71-853c-28cc03d0b324",
			"TitleProperty": "Question",
			"NodeType": "Termination",
			"ZOrder": 86,
			"X": 1319.6,
			"Y": 837.6,
			"Width": 221.496552,
			"Height": 240.115875,
			"TitleHeight": 214.355438,
			"Sockets": [
					{
							"Ticket": "2d061010-90c7-4575-88f3-a067e27af402",
							"TitleProperty": "Response",
							"SocketMode": "Input",
							"X": 1311.6,
							"Y": 1056.83569,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 1327.6,
							"TextY": 1055.95544,
							"TextWidth": 205.496552,
							"TextHeight": 17.7604141,
							"Properties": [
									{
											"Name": "Response",
											"Value": "Response"
									}
							]
					}
			],
			"Properties": [
					{
							"Name": "Question",
							"Value": "After the conversation ends, you feel irate and frustrated. You’re not sure if you accomplished anything."
					},
					{
							"Name": "StoryPageNumber",
							"Value": "7"
					},
					{
							"Name": "StoryPageX",
							"Value": "0.000"
					},
					{
							"Name": "StoryPageY",
							"Value": "0.000"
					},
					{
							"Name": "StoryShapeType",
							"Value": "Callout"
					},
					{
							"Name": "StoryColorFill",
							"Value": "#FFFFFF"
					},
					{
							"Name": "StoryColorOutline",
							"Value": "#000000"
					},
					{
							"Name": "StoryColorText",
							"Value": "#000000"
					},
					{
							"Name": "StoryFontName",
							"Value": "Tahoma"
					},
					{
							"Name": "StoryFontSize",
							"Value": "10.2"
					}
			]
	},
	{
			"Ticket": "71d4b91e-75a1-4f1f-8149-2c8229da0fc4",
			"TitleProperty": "Question",
			"NodeType": "Fork",
			"ZOrder": 117,
			"X": 403.2,
			"Y": 1246.16,
			"Width": 239.980469,
			"Height": 191.4661,
			"TitleHeight": 63.47655,
			"Sockets": [
					{
							"Ticket": "8274b0de-bb5c-43e8-82e1-8ac0838b212d",
							"TitleProperty": "Response",
							"SocketMode": "Input",
							"X": 395.2,
							"Y": 1314.51685,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 411.2,
							"TextY": 1313.6366,
							"TextWidth": 223.980469,
							"TextHeight": 17.7604141,
							"Properties": [
									{
											"Name": "Response",
											"Value": "Response"
									}
							]
					},
					{
							"Ticket": "07b1ad7d-2f83-4ac1-a3ff-0cda6026f82f",
							"TitleProperty": "Answer",
							"SocketMode": "Output",
							"X": 635.1805,
							"Y": 1376.5116,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 411.2,
							"TextY": 1335.397,
							"TextWidth": 223.980469,
							"TextHeight": 98.2291641,
							"Properties": [
									{
											"Name": "Index",
											"Value": "A"
									},
									{
											"Name": "Answer",
											"Value": "Well, the only sure way to prevent the disease is to practice social distancing. How about this: I’ll help you set up a Zoom account, then we’ll get everyone together on a video call and have the party that way."
									}
							],
							"Connections": [
									"c859a7ce-23de-4b9e-adae-76502d87b9b8"
							]
					}
			],
			"Properties": [
					{
							"Name": "Question",
							"Value": "OK. What do you have in mind?"
					},
					{
							"Name": "StoryPageNumber",
							"Value": "8"
					},
					{
							"Name": "StoryPageX",
							"Value": "0.000"
					},
					{
							"Name": "StoryPageY",
							"Value": "0.000"
					},
					{
							"Name": "StoryShapeType",
							"Value": "Callout"
					},
					{
							"Name": "StoryColorFill",
							"Value": "#FFFFFF"
					},
					{
							"Name": "StoryColorOutline",
							"Value": "#000000"
					},
					{
							"Name": "StoryColorText",
							"Value": "#000000"
					},
					{
							"Name": "StoryFontName",
							"Value": "Tahoma"
					},
					{
							"Name": "StoryFontSize",
							"Value": "10.2"
					}
			]
	},
	{
			"Ticket": "f49ddba2-109a-4528-9900-63dd212c9f81",
			"TitleProperty": "Question",
			"NodeType": "Fork",
			"ZOrder": 119,
			"X": 398.92,
			"Y": 1443.12,
			"Width": 235.932281,
			"Height": 207.559845,
			"TitleHeight": 63.47655,
			"Sockets": [
					{
							"Ticket": "c859a7ce-23de-4b9e-adae-76502d87b9b8",
							"TitleProperty": "Response",
							"SocketMode": "Input",
							"X": 390.92,
							"Y": 1511.47681,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 406.92,
							"TextY": 1510.59656,
							"TextWidth": 219.932281,
							"TextHeight": 17.7604141,
							"Properties": [
									{
											"Name": "Response",
											"Value": "Response"
									}
							]
					},
					{
							"Ticket": "9dfad6de-e5d2-4fb9-836e-d48706db9531",
							"TitleProperty": "Answer",
							"SocketMode": "Output",
							"X": 626.8523,
							"Y": 1581.51843,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 406.92,
							"TextY": 1532.35693,
							"TextWidth": 219.932281,
							"TextHeight": 114.322914,
							"Properties": [
									{
											"Name": "Index",
											"Value": "A"
									},
									{
											"Name": "Answer",
											"Value": "I know, grandma; this is tough for anyone. But at least you and grandpa will be safe, right? We can still have fun, too. I’ll still have those noisemakers left over from Aunt Vera’s birthday a year ago. I’ll send you some!"
									}
							],
							"Connections": [
									"0d262b0f-40cf-4be6-abf7-ac452a0b868c"
							]
					}
			],
			"Properties": [
					{
							"Name": "Question",
							"Value": "But, Jonas, it just won’t be the same!"
					},
					{
							"Name": "StoryPageNumber",
							"Value": "9"
					},
					{
							"Name": "StoryPageX",
							"Value": "0.000"
					},
					{
							"Name": "StoryPageY",
							"Value": "0.000"
					},
					{
							"Name": "StoryShapeType",
							"Value": "Callout"
					},
					{
							"Name": "StoryColorFill",
							"Value": "#FFFFFF"
					},
					{
							"Name": "StoryColorOutline",
							"Value": "#000000"
					},
					{
							"Name": "StoryColorText",
							"Value": "#000000"
					},
					{
							"Name": "StoryFontName",
							"Value": "Tahoma"
					},
					{
							"Name": "StoryFontSize",
							"Value": "10.2"
					}
			]
	},
	{
			"Ticket": "a4d1b815-73a7-4ddc-8621-fd06e503b1db",
			"TitleProperty": "Question",
			"NodeType": "Fork",
			"ZOrder": 122,
			"X": 405.88,
			"Y": 1663.065,
			"Width": 226.023438,
			"Height": 141.173126,
			"TitleHeight": 93.65233,
			"Sockets": [
					{
							"Ticket": "0d262b0f-40cf-4be6-abf7-ac452a0b868c",
							"TitleProperty": "Response",
							"SocketMode": "Input",
							"X": 397.88,
							"Y": 1761.59753,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 413.88,
							"TextY": 1760.71729,
							"TextWidth": 210.023438,
							"TextHeight": 17.7604141,
							"Properties": [
									{
											"Name": "Response",
											"Value": "Response"
									}
							]
					},
					{
							"Ticket": "de4045ce-9511-49f1-9756-9a7cdd31974e",
							"TitleProperty": "Answer",
							"SocketMode": "Output",
							"X": 623.903442,
							"Y": 1783.35791,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 413.88,
							"TextY": 1782.47766,
							"TextWidth": 210.023438,
							"TextHeight": 17.7604141,
							"Properties": [
									{
											"Name": "Index",
											"Value": "A"
									},
									{
											"Name": "Answer",
											"Value": "Great! Thanks, grandma."
									}
							],
							"Connections": [
									"9d446725-f8f1-43b5-903a-8fc84ee45686"
							]
					}
			],
			"Properties": [
					{
							"Name": "Question",
							"Value": "Alright, Jonas. I’m game. Let’s give it a try."
					},
					{
							"Name": "StoryPageNumber",
							"Value": "10"
					},
					{
							"Name": "StoryPageX",
							"Value": "0.000"
					},
					{
							"Name": "StoryPageY",
							"Value": "0.000"
					},
					{
							"Name": "StoryShapeType",
							"Value": "Callout"
					},
					{
							"Name": "StoryColorFill",
							"Value": "#FFFFFF"
					},
					{
							"Name": "StoryColorOutline",
							"Value": "#000000"
					},
					{
							"Name": "StoryColorText",
							"Value": "#000000"
					},
					{
							"Name": "StoryFontName",
							"Value": "Tahoma"
					},
					{
							"Name": "StoryFontSize",
							"Value": "10.2"
					}
			]
	},
	{
			"Ticket": "0fce893d-e39c-4316-9770-d182b2bc71e5",
			"TitleProperty": "Question",
			"NodeType": "Termination",
			"ZOrder": 106,
			"X": 1168.848,
			"Y": 1142.668,
			"Width": 231.982391,
			"Height": 330.643219,
			"TitleHeight": 304.882751,
			"Sockets": [
					{
							"Ticket": "9d446725-f8f1-43b5-903a-8fc84ee45686",
							"TitleProperty": "Response",
							"SocketMode": "Input",
							"X": 1160.848,
							"Y": 1452.431,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 1176.848,
							"TextY": 1451.55078,
							"TextWidth": 215.982391,
							"TextHeight": 17.7604141,
							"Properties": [
									{
											"Name": "Response",
											"Value": "Response"
									}
							]
					}
			],
			"Properties": [
					{
							"Name": "Question",
							"Value": "You help your grandma set up the party on Zoom, and it goes better than expected. Your grandma asks if you can help her organize other family get-togethers online."
					},
					{
							"Name": "StoryPageNumber",
							"Value": "11"
					},
					{
							"Name": "StoryPageX",
							"Value": "0.000"
					},
					{
							"Name": "StoryPageY",
							"Value": "0.000"
					},
					{
							"Name": "StoryShapeType",
							"Value": "Callout"
					},
					{
							"Name": "StoryColorFill",
							"Value": "#FFFFFF"
					},
					{
							"Name": "StoryColorOutline",
							"Value": "#000000"
					},
					{
							"Name": "StoryColorText",
							"Value": "#000000"
					},
					{
							"Name": "StoryFontName",
							"Value": "Tahoma"
					},
					{
							"Name": "StoryFontSize",
							"Value": "10.2"
					}
			]
	},
	{
			"Ticket": "c2d21196-d9ee-4572-9467-7e859dccdb03",
			"TitleProperty": "Question",
			"NodeType": "Termination",
			"ZOrder": 105,
			"X": 1110.904,
			"Y": 76.36,
			"Width": 229.296844,
			"Height": 390.99472,
			"TitleHeight": 365.2343,
			"Sockets": [
					{
							"Ticket": "7b868a73-0531-4fa7-ab2c-da5b3608e868",
							"TitleProperty": "Response",
							"SocketMode": "Input",
							"X": 1102.904,
							"Y": 446.474518,
							"Width": 16.0,
							"Height": 16.0,
							"TextX": 1118.904,
							"TextY": 445.5943,
							"TextWidth": 213.296844,
							"TextHeight": 17.7604141,
							"Properties": [
									{
											"Name": "Response",
											"Value": "Response"
									}
							]
					}
			],
			"Properties": [
					{
							"Name": "Question",
							"Value": "You attend the party as planned. A few days later, you get tested for COVID-19 and realize you have the virus. Your grandpa comes down with the virus, too, and must be hospitalized."
					},
					{
							"Name": "StoryPageNumber",
							"Value": "12"
					},
					{
							"Name": "StoryPageX",
							"Value": "0.000"
					},
					{
							"Name": "StoryPageY",
							"Value": "0.000"
					},
					{
							"Name": "StoryShapeType",
							"Value": "Callout"
					},
					{
							"Name": "StoryColorFill",
							"Value": "#FFFFFF"
					},
					{
							"Name": "StoryColorOutline",
							"Value": "#000000"
					},
					{
							"Name": "StoryColorText",
							"Value": "#000000"
					},
					{
							"Name": "StoryFontName",
							"Value": "Tahoma"
					},
					{
							"Name": "StoryFontSize",
							"Value": "10.2"
					}
			]
	}
],
	"Resources": []
};
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
	var count = 0;
	var index = 0;

	if(node && ticket)
	{
		currentNode = null;
		buttons = node.Sockets;
		count = buttons.length;
		for(index = 0; index < count; index ++)
		{
			button = buttons[index];
			if(button.Ticket == ticket)
			{
				//	This is the clicked item.
				if(node.NodeType != "Delay")
				{
					//	Add the response.
					$("#chatPanel").append(
						`<div class="hero-card answer">
						<p class="title">${getPropertyValue(button, "Answer")}</p>
						</div>`);
					$("#chatContainer").animate(
						{scrollTop: $("#chatPanel").height()}, "slow").delay(400);
				}
				//	Check for next connections.
				connections = button.Connections;
				if(connections.length > 0)
				{
					currentNode = getNodeByTicket(connections[0]);
				}
				break;
			}
		}
	}
	//	Setup the next question.
	if(currentNode)
	{
		// console.log("Current node defined...");
		$("#chatPanel").append(
			`<div class="hero-card question">
			<p class="title">${getPropertyValue(currentNode, "Question")}</p>
			<div id="buttons${entryID}" class="buttons">
			</div>
			</div>`);
		buttons = getButtons(currentNode);
		count = buttons.length;
		console.log(`Button count: ${count}...`);
		if(currentNode.NodeType != "Delay")
		{
			for(index = 0; index < count; index ++)
			{
				button = buttons[index];
				$(`#buttons${entryID}`).append(
					`<div class="button" ticket="${button.Ticket}">
					${getPropertyValue(button, "Answer")}</div>`);
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
	currentNode.EntryID = entryID;
	entryID ++;
	$("#chatContainer").animate(
		{scrollTop: $("#chatPanel").height()}, "slow");
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
	var count = 0;
	var index = 0;
	var startNode = null;

	$("#chatPanel").empty();
	$("#chatPanel").on("click", ".button", function(e)
	{
		var ticket = e.target.getAttribute("ticket");
		e.preventDefault();
		$(`#buttons${currentNode.EntryID} .button`).attr("class", "button-used");
		handleResponse(currentNode, ticket);
	});
	chatbotdata.Nodes.some(function(e)
	{
		if(e.NodeType == "Start")
		{
			startNode = e;
			return true;
		}
	});
	currentNode = startNode;
	currentNode.EntryID = entryID;
	if(startNode)
	{
		$("#chatPanel").append(
			`<div class="hero-card question">
			<p class="title">${getPropertyValue(startNode, "Question")}</p>
			<div id="buttons${entryID}" class="buttons">
			</div>
			</div>`);
		buttons = getButtons(startNode);
		count = buttons.length;
		if(currentNode.NodeType != "Delay")
		{
			for(index = 0; index < count; index ++)
			{
				button = buttons[index];
				$(`#buttons${entryID}`).append(
					`<div class="button" ticket="${button.Ticket}">
					${getPropertyValue(button, "Answer")}</div>`);
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
