//	SocketData.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
//using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	SocketDataCollection																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of SocketDataItem Items.
	/// </summary>
	public class SocketDataCollection : List<SocketDataItem>
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


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SocketDataItem																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Non-active socket data for storing and loading data.
	/// </summary>
	public class SocketDataItem
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	TransferValues																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Transfer values from the active source to the data-only target.
		/// </summary>
		/// <param name="source">
		/// Reference to the source item.
		/// </param>
		/// <param name="target">
		/// Reference to the target item.
		/// </param>
		private static void TransferValues(SocketItem source,
			SocketDataItem target)
		{
			if(source != null && target != null)
			{
				target.mHeight = source.Height;
				target.mSocketMode = source.SocketMode.ToString();
				target.mTextHeight = source.TextBounds.Height;
				target.mTextWidth = source.TextBounds.Width;
				target.mTextX = source.TextBounds.X;
				target.mTextY = source.TextBounds.Y;
				target.mTicket = source.Ticket;
				target.mTitleProperty = source.TitleProperty;
				target.mWidth = source.Width;
				target.mX = source.X;
				target.mY = source.Y;
				foreach(SocketItem connection in source.Connections)
				{
					target.mConnections.Add(connection.Ticket);
				}
				foreach(PropertyItem property in source.Properties)
				{
					if(property.Static)
					{
						target.mProperties.Add(property.Name, property.StringValue());
					}
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
		/// Create a new instance of the SocketDataItem Item.
		/// </summary>
		public SocketDataItem()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the SocketDataItem Item.
		/// </summary>
		public SocketDataItem(SocketItem socket)
		{
			TransferValues(socket, this);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Connections																														*
		//*-----------------------------------------------------------------------*
		private List<string> mConnections = new List<string>();
		/// <summary>
		/// Get a reference to the collection of sockets to which this socket is
		/// connected.
		/// </summary>
		/// <remarks>
		/// This list contains the names of the input sockets at the targets.
		/// </remarks>
		[JsonProperty(Order = 12)]
		public List<string> Connections
		{
			get { return mConnections; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Height																																*
		//*-----------------------------------------------------------------------*
		private float mHeight = 16f;
		/// <summary>
		/// Get/Set the height of the item.
		/// </summary>
		[JsonProperty(Order = 6)]
		public float Height
		{
			get { return mHeight; }
			set { mHeight = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Properties																														*
		//*-----------------------------------------------------------------------*
		private NameValueCollection mProperties = new NameValueCollection();
		/// <summary>
		/// Get a reference to the collection of properties on this item.
		/// </summary>
		[JsonProperty(Order = 11)]
		public NameValueCollection Properties
		{
			get { return mProperties; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ShouldSerializeConnections																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the Connections property should be
		/// serialized.
		/// </summary>
		/// <returns>
		/// True if Connections.Count > 0. Otherwise, false.
		/// </returns>
		public bool ShouldSerializeConnections()
		{
			bool result = mConnections.Count > 0;
			return result;
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
			bool result = mProperties.Count > 0;
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SocketMode																														*
		//*-----------------------------------------------------------------------*
		private string mSocketMode = "None";
		/// <summary>
		/// Get/Set the operational mode of this socket.
		/// </summary>
		[JsonProperty(Order = 2)]
		public string SocketMode
		{
			get { return mSocketMode; }
			set { mSocketMode = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	TextHeight																														*
		//*-----------------------------------------------------------------------*
		private float mTextHeight = 0f;
		/// <summary>
		/// Get/Set the height of the text.
		/// </summary>
		[JsonProperty(Order = 10)]
		public float TextHeight
		{
			get { return mTextHeight; }
			set { mTextHeight = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	TextWidth																															*
		//*-----------------------------------------------------------------------*
		private float mTextWidth = 0f;
		/// <summary>
		/// Get/Set the width of the text.
		/// </summary>
		[JsonProperty(Order = 9)]
		public float TextWidth
		{
			get { return mTextWidth; }
			set { mTextWidth = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	TextX																																	*
		//*-----------------------------------------------------------------------*
		private float mTextX = 0f;
		/// <summary>
		/// Get/Set the X coordinate of the text.
		/// </summary>
		[JsonProperty(Order = 7)]
		public float TextX
		{
			get { return mTextX; }
			set { mTextX = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	TextY																																	*
		//*-----------------------------------------------------------------------*
		private float mTextY = 0f;
		/// <summary>
		/// Get/Set the Y coordinate of the text.
		/// </summary>
		[JsonProperty(Order = 8)]
		public float TextY
		{
			get { return mTextY; }
			set { mTextY = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Ticket																																*
		//*-----------------------------------------------------------------------*
		private string mTicket = "";
		/// <summary>
		/// Get/Set the globally unique ID of the socket.
		/// </summary>
		[JsonProperty(Order = 0)]
		public string Ticket
		{
			get { return mTicket; }
			set { mTicket = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	TitleProperty																													*
		//*-----------------------------------------------------------------------*
		private string mTitleProperty = "Name";
		/// <summary>
		/// Get/Set the name of the title property for this item.
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
		private float mWidth = 16f;
		/// <summary>
		/// Get/Set the width of the item on the parent object.
		/// </summary>
		[JsonProperty(Order = 5)]
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
		[JsonProperty(Order = 3)]
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
		[JsonProperty(Order = 4)]
		public float Y
		{
			get { return mY; }
			set { mY = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
