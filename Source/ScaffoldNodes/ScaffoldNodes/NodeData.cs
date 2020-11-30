//	NodeData.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

using static Scaffold.ScaffoldNodesUtil;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	NodeDataCollection																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of NodeDataItem Items.
	/// </summary>
	/// <remarks>
	/// NodeDataX objects serve as an intermediate platform between raw
	/// deserialization and fully relational node objects. All serialization and
	/// deserialization of nodes is done through associated NodeData family
	/// objects.
	/// </remarks>
	public class NodeDataCollection : List<NodeDataItem>
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		////*-----------------------------------------------------------------------*
		////* PostThumbnail																													*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Post a thumbnail of the provided image to the specified node property.
		///// </summary>
		///// <param name="node">
		///// Reference to the node to update.
		///// </param>
		///// <param name="propertyName">
		///// Name of the node property to update.
		///// </param>
		///// <param name="bitmap">
		///// Reference to the image to receive thumbnail.
		///// </param>
		///// <param name="thumbWidth">
		///// Maximum width of the thumbnail.
		///// </param>
		///// <remarks>
		///// The property created here is non-permanent, and will not be
		///// saved with other node properties.
		///// </remarks>
		//private static void PostThumbnail(NodeItem node, string propertyName,
		//	Bitmap bitmap, float thumbWidth)
		//{
		//	float height = 0f;
		//	float scale = 0f;
		//	Bitmap thumbnail = null;
		//	float width = 0f;

		//	if(bitmap != null)
		//	{
		//		width = (float)bitmap.Width;
		//		height = (float)bitmap.Height;
		//		if(width != 0f && height != 0f)
		//		{
		//			//	A thumbnail will be generated.
		//			if(width > thumbWidth)
		//			{
		//				scale = width / thumbWidth;
		//			}
		//			else
		//			{
		//				scale = 1f;
		//			}
		//			thumbnail = new Bitmap(
		//				(int)(width / scale), (int)(height / scale));
		//			using(Graphics g = Graphics.FromImage(thumbnail))
		//			{
		//				g.CompositingQuality =
		//					System.Drawing.Drawing2D.
		//					CompositingQuality.HighQuality;
		//				g.InterpolationMode =
		//					System.Drawing.Drawing2D.InterpolationMode.High;
		//				g.SmoothingMode =
		//					System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
		//				g.DrawImage(bitmap,
		//					new RectangleF(0f, 0f, width, height));
		//			}
		//			node.Properties.Add(new PropertyItem()
		//			{
		//				Name = propertyName,
		//				Value = thumbnail,
		//				Static = false
		//			});
		//		}
		//	}
		//}
		////*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************

		////*-----------------------------------------------------------------------*
		////*	DeserializeData																												*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Deserialize serialized JSON content to the provided collection.
		///// </summary>
		///// <param name="nodes">
		///// </param>
		///// <param name="content">
		///// </param>
		//public static void DeserializeData(NodeCollection nodes, string content)
		//{
		//	SocketItem connection = null;
		//	NodeDataCollection data =
		//		JsonConvert.DeserializeObject<NodeDataCollection>(content);
		//	SocketItem socket = null;

		//	if(data.Count > 0)
		//	{
		//		//	All data except connections.
		//		foreach(NodeDataItem item in data)
		//		{
		//			nodes.Add(new NodeItem(item));
		//		}
		//		//	Activate all connections.
		//		foreach(NodeDataItem item in data)
		//		{
		//			foreach(SocketDataItem itemSocket in item.Sockets)
		//			{
		//				if(itemSocket.Connections.Count > 0)
		//				{
		//					//	Get the source socket.
		//					socket = nodes.GetSocketByTicket(itemSocket.Ticket);
		//				}
		//				foreach(string itemConnection in itemSocket.Connections)
		//				{
		//					//	Get the target socket.
		//					connection = nodes.GetSocketByTicket(itemConnection);
		//					if(connection != null)
		//					{
		//						socket.Connections.Add(connection);
		//					}
		//				}
		//			}
		//		}
		//	}
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DeserializeData																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Deserialize serialized JSON content to the provided node file item.
		/// </summary>
		/// <param name="file">
		/// Reference to the node file item that will be populated.
		/// </param>
		/// <param name="basePath">
		/// Current base path for relative references.
		/// </param>
		/// <param name="content">
		/// JSON node file content.
		/// </param>
		public static void DeserializeData(NodeFileItem file, string basePath,
			string content)
		{
			//Bitmap bitmap = null;
			SocketItem connection = null;
			//IConversionResult conversionResult = null;
			//string dataPath = "";
			//string extension = "";
			//float height = 0f;
			Match match = null;
			NodeFileDataItem nodeFile = null;
			NodeDataCollection nodeData = null;
			NodeCollection nodes = null;
			//ResourceItem resource = null;
			//ResourceLiveItem resourceLive = null;
			ResourceCollection resources = null;
			ResourceCollection resourceData = null;
			//float scale = 0f;
			SocketItem socket = null;
			//FileInfo thumbFile = null;
			//Bitmap thumbnail = null;
			//string thumbPath = "";
			//float thumbWidth = 200f;
			//float width = 0f;

			if(file != null && content?.Length > 0)
			{
				nodes = file.Nodes;
				resources = file.Resources;
				match = Regex.Match(content, ResourceLib.rxJSONStartArray);
				if(match.Success)
				{
					//	File is an array.
					//	Version a schema. Node structure only.
					nodeData =
						JsonConvert.DeserializeObject<NodeDataCollection>(content);
				}
				else
				{
					//	File is an object.
					//	Version b schema. Node and universal single instance resources.
					nodeFile =
						JsonConvert.DeserializeObject<NodeFileDataItem>(content);
					nodeData = nodeFile.Nodes;
				}
				if(nodeData?.Count > 0)
				{
					//	All data except connections.
					foreach(NodeDataItem item in nodeData)
					{
						nodes.Add(new NodeItem(item));
					}
					//	Activate all connections.
					foreach(NodeDataItem item in nodeData)
					{
						foreach(SocketDataItem itemSocket in item.Sockets)
						{
							if(itemSocket.Connections.Count > 0)
							{
								//	Get the source socket.
								socket = nodes.GetSocketByTicket(itemSocket.Ticket);
							}
							foreach(string itemConnection in itemSocket.Connections)
							{
								//	Get the target socket.
								connection = nodes.GetSocketByTicket(itemConnection);
								if(connection != null)
								{
									socket.Connections.Add(connection);
								}
							}
						}
					}
				}
				if(nodeFile != null)
				{
					//	Pointers to data resources can just be reassigned to the
					//	operational collection.
					file.Description = nodeFile.Description;
					file.Name = nodeFile.Name;
					file.Ticket = nodeFile.Ticket;
					resourceData = nodeFile.Resources;
					foreach(ResourceItem resourceItem in resourceData)
					{
						resources.Add(resourceItem);
					}
				}
				//	Create thumbnails and icons for nodes with media.
				foreach(NodeItem node in file.Nodes)
				{
					if(MediaExists(node))
					{
						//	Media are attached to this node.
						if(MediaExists(node, "MediaImage"))
						{
							//	Image is attached.
							CreateImageThumbnail(node);
							//resource =
							//	NodeItem.GetResource(node, file.Resources, "MediaImage");
							//if(resource != null)
							//{
							//	resourceLive = ResourceLiveItem.FromResourceItem(resource);
							//	if(resourceLive.Data is byte[] rData && rData.Length > 0)
							//	{
							//		try
							//		{
							//			using(MemoryStream stream = new MemoryStream(rData))
							//			{
							//				bitmap = new Bitmap(stream);
							//			}
							//		}
							//		catch { }
							//	}
							//	PostThumbnail(node, "ThumbImage", bitmap, thumbWidth);
							//}
						}
						if(MediaExists(node, "MediaVideo"))
						{
							//	Video is attached.
							CreateVideoThumbnail(node);
							//resource =
							//	NodeItem.GetResource(node, file.Resources, "MediaVideo");
							//if(resource != null)
							//{
							//	resourceLive = ResourceLiveItem.FromResourceItem(resource);
							//	if(resourceLive.Data is byte[] rData && rData.Length > 0)
							//	{
							//		try
							//		{
							//			extension = MimeTypeExtension(resourceLive.MimeType);
							//			dataPath = Path.Combine(Path.GetTempPath(),
							//				Guid.NewGuid().ToString("D") + "." + extension);
							//			File.WriteAllBytes(dataPath, rData);
							//			thumbPath = Path.Combine(Path.GetTempPath(),
							//				Guid.NewGuid().ToString("D") + ".png");
							//			conversionResult = await Conversion.Snapshot(
							//				dataPath, thumbPath, TimeSpan.FromSeconds(0)).Start();
							//			thumbFile = new FileInfo(thumbPath);
							//			if(thumbFile.Exists)
							//			{
							//				//	Thumbnail file was created.
							//				bitmap = (Bitmap)Bitmap.FromFile(thumbPath);
							//				PostThumbnail(node, "ThumbVideo", bitmap, thumbWidth);
							//			}
							//		}
							//		catch { }
							//	}
							//}
						}
						if(MediaExists(node, "MediaAudio"))
						{
							//	Audio file is attached.
							CreateAudioIcon(node);
							////	Create Icon.
							//PostThumbnail(node, "IconAudio", ResourceMain.Audio, 32f);
						}
						if(MediaExists(node, "MediaLink"))
						{
							//	Link is attached.
							CreateLinkIcon(node);
							////	Create Icon.
							//PostThumbnail(node, "IconLink", ResourceMain.Link, 32f);
						}
						foreach(SocketItem socketItem in node.Sockets)
						{
							//	Sockets only receive one thumbnail per object.
							//	If the item is present, it is named 'Icon'.
							if(MediaExists(socket, "MediaImage"))
							{
								CreateImageThumbnail(socket);
							}
							else if(MediaExists(socket, "MediaVideo"))
							{
								CreateVideoThumbnail(socket);
							}
							else if(MediaExists(socket, "MediaAudio"))
							{
								CreateAudioIcon(socket);
							}
							else if(MediaExists(socket, "MediaLink"))
							{
								CreateLinkIcon(socket);
							}
						}
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SerializeData																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Serialize node data.
		/// </summary>
		/// <param name="file">
		/// Collection of in-service nodes and resources to serialize.
		/// </param>
		/// <param name="embedResources">
		/// Value indicating whether to embed all media resources with Data URI.
		/// <para>If true, all linked resources are converted locally to embedded.
		/// Any resources that already had embedded data will be copied by
		/// reference to the local collection.</para>
		/// <para>If false, all resources are copied by reference to the local
		/// collection. Whatever resources were already embedded will remain that
		/// way.</para>
		/// </param>
		/// <param name="basePath">
		/// Base path of the actively open file. Used for resolving relative paths.
		/// </param>
		/// <returns>
		/// Serialized string data, in JSON format.
		/// </returns>
		public static string SerializeData(NodeFileItem file,
			bool embedResources = false, string basePath = "")
		{
			NodeFileDataItem data = null;
			NodeCollection nodes = null;
			ResourceCollection resources = null;
			string result = "";

			if(file != null)
			{
				data = new NodeFileDataItem();
				data.Description = file.Description;
				data.Name = file.Name;
				data.Ticket = file.Ticket;
				nodes = file.Nodes;
				resources = file.Resources;
				foreach(NodeItem node in nodes)
				{
					data.Nodes.Add(new NodeDataItem(node));
				}
				if(embedResources)
				{
					foreach(ResourceItem resource in resources)
					{
						data.Resources.Add(ResourceItem.Embed(resource));
					}
				}
				else
				{
					foreach(ResourceItem resource in resources)
					{
						data.Resources.Add(resource);
					}
				}
				result = JsonConvert.SerializeObject(data, Formatting.Indented);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	NodeDataItem																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Non-active node handler for loading and storing information.
	/// </summary>
	public class NodeDataItem
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	TransferValues																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Transfer the values of the active source node to the data-only target
		/// node.
		/// </summary>
		/// <param name="source">
		/// Reference to the active source node to transfer.
		/// </param>
		/// <param name="target">
		/// Reference to the data node to configure.
		/// </param>
		private static void TransferValues(NodeItem source, NodeDataItem target)
		{
			if(source != null && target != null)
			{
				target.mHeight = source.Height;
				target.mTicket = source.Ticket;
				target.mNodeColor = ToHex(source.NodeColor);
				target.mNodeTextColor = ToHex(source.NodeTextColor);
				target.mNodeType = source.NodeType.ToString();
				target.mTitleHeight = source.TitleHeight;
				target.mTitleProperty = source.TitleProperty;
				target.mWidth = source.Width;
				target.mX = source.X;
				target.mY = source.Y;
				target.mZOrder = source.ZOrder;
				target.mDelay = source.Delay;
				foreach(PropertyItem property in source.Properties)
				{
					if(property.Static)
					{
						target.Properties.Add(property.Name, property.StringValue());
					}
				}
				foreach(SocketItem socket in source.Sockets)
				{
					target.Sockets.Add(new SocketDataItem(socket));
				}
			}
		}
		//*-----------------------------------------------------------------------*

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
		/// Create a new instance of the NodeDataItem Item.
		/// </summary>
		public NodeDataItem()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the NodeDataItem Item.
		/// </summary>
		/// <param name="node">
		/// Reference to the active node to be used for filling this node's
		/// properties.
		/// </param>
		public NodeDataItem(NodeItem node)
		{
			TransferValues(node, this);
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	ComponentPadding																											*
		////*-----------------------------------------------------------------------*
		//private float mComponentPadding = 2f;
		///// <summary>
		///// Get/Set the padding between components.
		///// </summary>
		//public float ComponentPadding
		//{
		//	get { return mComponentPadding; }
		//	set { mComponentPadding = value; }
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Convert																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert the specified active node to inactive node and return a
		/// reference to the new value.
		/// </summary>
		public static NodeDataItem Convert(NodeItem node)
		{
			NodeDataItem result = new NodeDataItem();
			TransferValues(node, result);
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Delay																																	*
		//*-----------------------------------------------------------------------*
		private float mDelay = 0f;
		/// <summary>
		/// Get/Set the number of seconds to delay before continuing.
		/// </summary>
		[JsonProperty(Order = 11)]
		public float Delay
		{
			get { return mDelay; }
			set { mDelay = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Height																																*
		//*-----------------------------------------------------------------------*
		private float mHeight = 0f;
		/// <summary>
		/// Get/Set the height of the item on the parent object.
		/// </summary>
		/// <remarks>
		/// When internal height is 0, the result is calculated each time.
		/// </remarks>
		[JsonProperty(Order = 7)]
		public float Height
		{
			get { return mHeight; }
			set { mHeight = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeColor																															*
		//*-----------------------------------------------------------------------*
		private string mNodeColor = "";
		/// <summary>
		/// Get/Set the node color.
		/// </summary>
		[JsonProperty(Order = 9)]
		public string NodeColor
		{
			get { return mNodeColor; }
			set { mNodeColor = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeTextColor																													*
		//*-----------------------------------------------------------------------*
		private string mNodeTextColor = "";
		/// <summary>
		/// Get/Set the node text color.
		/// </summary>
		[JsonProperty(Order = 10)]
		public string NodeTextColor
		{
			get { return mNodeTextColor; }
			set { mNodeTextColor = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeType																															*
		//*-----------------------------------------------------------------------*
		private string mNodeType = "Start";
		/// <summary>
		/// Get/Set the registered node type of this node.
		/// </summary>
		[JsonProperty(Order = 2)]
		public string NodeType
		{
			get { return mNodeType; }
			set { mNodeType = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Properties																														*
		//*-----------------------------------------------------------------------*
		private NameValueCollection mProperties = new NameValueCollection();
		/// <summary>
		/// Get a reference to the collection of properties on this item.
		/// </summary>
		[JsonProperty(Order = 13)]
		public NameValueCollection Properties
		{
			get { return mProperties; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ShouldSerializeNodeColor																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the NodeColor property should be
		/// serialized.
		/// </summary>
		/// <returns>
		/// True if the NodeColor.Length > 0. Otherwise, false.
		/// </returns>
		public bool ShouldSerializeNodeColor()
		{
			return mNodeColor.Length > 0;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ShouldSerializeNodeTextColor																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the NodeTextColor property should be
		/// serialized.
		/// </summary>
		/// <returns>
		/// True if the NodeTextColor.Length > 0. Otherwise, false.
		/// </returns>
		public bool ShouldSerializeNodeTextColor()
		{
			return mNodeTextColor.Length > 0;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ShouldSerializeProperties																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the Properties property should be
		/// serialized.
		/// </summary>
		/// <returns>
		/// True if Properties.Count > 0. Otherwise, false.
		/// </returns>
		public bool ShouldSerializeProperties()
		{
			return mProperties.Count > 0;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ShouldSerializeSockets																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the Sockets property should be
		/// serialized.
		/// </summary>
		/// <returns>
		/// True if Sockets.Count > 0. Otherwise, false.
		/// </returns>
		public bool ShouldSerializeSockets()
		{
			return mSockets.Count > 0;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Sockets																																*
		//*-----------------------------------------------------------------------*
		private SocketDataCollection mSockets = new SocketDataCollection();
		/// <summary>
		/// Get a reference to the collection of sockets on this node.
		/// </summary>
		[JsonProperty(Order = 12)]
		public SocketDataCollection Sockets
		{
			get { return mSockets; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Ticket																																*
		//*-----------------------------------------------------------------------*
		private string mTicket = "";
		/// <summary>
		/// Get/Set the globally unique identification of the node in the tree.
		/// </summary>
		[JsonProperty(Order = 0)]
		public string Ticket
		{
			get { return mTicket; }
			set { mTicket = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	TitleHeight																														*
		//*-----------------------------------------------------------------------*
		private float mTitleHeight = 20f;
		/// <summary>
		/// Get/Set the height of the title of this node.
		/// </summary>
		[JsonProperty(Order = 8)]
		public float TitleHeight
		{
			get { return mTitleHeight; }
			set { mTitleHeight = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	TitleProperty																													*
		//*-----------------------------------------------------------------------*
		private string mTitleProperty = "Ticket";
		/// <summary>
		/// Get/Set the property name to be used as the node title.
		/// </summary>
		[JsonProperty(Order = 1)]
		public string TitleProperty
		{
			get { return mTitleProperty; }
			set { mTitleProperty = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Width																																	*
		//*-----------------------------------------------------------------------*
		private float mWidth = 150f;
		/// <summary>
		/// Get/Set the width of this node.
		/// </summary>
		[JsonProperty(Order = 6)]
		public float Width
		{
			get { return mWidth; }
			set { mWidth = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	X																																			*
		//*-----------------------------------------------------------------------*
		private float mX = 0f;
		/// <summary>
		/// Get/Set the X-coordinate of the item on the parent object.
		/// </summary>
		[JsonProperty(Order = 4)]
		public float X
		{
			get { return mX; }
			set { mX = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Y																																			*
		//*-----------------------------------------------------------------------*
		private float mY = 0f;
		/// <summary>
		/// Get/Set the Y-coordinate of the item on the parent object.
		/// </summary>
		[JsonProperty(Order = 5)]
		public float Y
		{
			get { return mY; }
			set { mY = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ZOrder																																*
		//*-----------------------------------------------------------------------*
		private int mZOrder = 0;
		/// <summary>
		/// Get/Set the Z-order of this node within the collection.
		/// </summary>
		[JsonProperty(Order = 3)]
		public int ZOrder
		{
			get { return mZOrder; }
			set { mZOrder = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	NodeFileDataItem																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// File container for node data schema version b.
	/// </summary>
	public class NodeFileDataItem
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
		//*	Description																														*
		//*-----------------------------------------------------------------------*
		private string mDescription = "";
		/// <summary>
		/// Get/Set a brief description of this document.
		/// </summary>
		[JsonProperty(Order = 2)]
		public string Description
		{
			get { return mDescription; }
			set { mDescription = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Name																																	*
		//*-----------------------------------------------------------------------*
		private string mName = "";
		/// <summary>
		/// Get/Set the official name of this document.
		/// </summary>
		[JsonProperty(Order = 1)]
		public string Name
		{
			get { return mName; }
			set { mName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Nodes																																	*
		//*-----------------------------------------------------------------------*
		private NodeDataCollection mNodes = new NodeDataCollection();
		/// <summary>
		/// Get a reference to the collection of data nodes on this file.
		/// </summary>
		[JsonProperty(Order = 3)]
		public NodeDataCollection Nodes
		{
			get { return mNodes; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Resources																															*
		//*-----------------------------------------------------------------------*
		private ResourceCollection mResources = new ResourceCollection();
		/// <summary>
		/// Get a reference to the universal single instance resources on this
		/// file.
		/// </summary>
		[JsonProperty(Order = 4)]
		public ResourceCollection Resources
		{
			get { return mResources; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Ticket																																*
		//*-----------------------------------------------------------------------*
		private string mTicket = Guid.NewGuid().ToString("D");
		/// <summary>
		/// Get/Set the globally unique identification of this file.
		/// </summary>
		[JsonProperty(Order = 0)]
		public string Ticket
		{
			get { return mTicket; }
			set { mTicket = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
