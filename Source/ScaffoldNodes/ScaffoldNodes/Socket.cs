//	Socket.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.

using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using System.Windows.Forms;

//using static Scaffold.ScaffoldUtil;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	SocketCollection																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of SocketItem Items.
	/// </summary>
	public class SocketCollection : List<SocketItem>
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* BindSocket																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Bind the specified socket's events.
		/// </summary>
		/// <param name="collection">
		/// Reference to the collection receiving the events.
		/// </param>
		/// <param name="socket">
		/// Reference to the socket for which events will be monitored.
		/// </param>
		private static void BindSocket(SocketCollection collection,
			SocketItem socket)
		{
			if(socket != null && collection != null)
			{
				socket.PropertyChanged += collection.socket_PropertyChanged;
				socket.ConnectionAdded += collection.OnSocketConnectionAdded;
				socket.ConnectionDeleted += collection.OnSocketConnectionDeleted;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* socket_PropertyChanged																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A property has been changed on a socket.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket property change event arguments.
		/// </param>
		private void socket_PropertyChanged(object sender,
			SocketPropertyChangeEventArgs e)
		{
			if(e != null)
			{
				e.Node = mParent;
			}
			OnSocketPropertyChanged(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UnbindSocket																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Unbind the specified socket's events.
		/// </summary>
		/// <param name="collection">
		/// Reference to the collection receiving the events.
		/// </param>
		/// <param name="socket">
		/// Reference to the socket for which events will be detached.
		/// </param>
		private static void UnbindSocket(SocketCollection collection,
			SocketItem socket)
		{
			if(socket != null && collection != null)
			{
				socket.PropertyChanged -= collection.socket_PropertyChanged;
				socket.ConnectionAdded -= collection.OnSocketConnectionAdded;
				socket.ConnectionDeleted -= collection.OnSocketConnectionDeleted;
			}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnSocketAdded																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SocketAdded event when a socket has been added.
		/// </summary>
		/// <param name="e">
		/// Socket event arguments.
		/// </param>
		protected virtual void OnSocketAdded(SocketEventArgs e)
		{
			SocketAdded?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSocketConnectionAdded																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SocketConnectionAdded event when a connection has been added
		/// to a socket.
		/// </summary>
		/// <param name="sender">
		/// Reference to the object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket event arguments.
		/// </param>
		protected virtual void OnSocketConnectionAdded(object sender,
			SocketConnectionEventArgs e)
		{
			SocketConnectionAdded?.Invoke(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSocketConnectionDeleted																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SocketConnectionAdded event when a connection has been
		/// deleted from a socket.
		/// </summary>
		/// <param name="sender">
		/// Reference to the object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket event arguments.
		/// </param>
		protected virtual void OnSocketConnectionDeleted(object sender,
			SocketConnectionEventArgs e)
		{
			SocketConnectionDeleted?.Invoke(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSocketDeleted																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SocketDeleted event when a socket has been deleted.
		/// </summary>
		/// <param name="e">
		/// Socket event arguments.
		/// </param>
		protected virtual void OnSocketDeleted(SocketEventArgs e)
		{
			SocketDeleted?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSocketPropertyChanged																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SocketPropertyChanged event when a property value has been
		/// changed on a socket in this collection.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket property change event arguments.
		/// </param>
		protected virtual void OnSocketPropertyChanged(object sender,
			SocketPropertyChangeEventArgs e)
		{
			SocketPropertyChanged?.Invoke(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the SocketCollection Item.
		/// </summary>
		public SocketCollection()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the SocketCollection Item.
		/// </summary>
		public SocketCollection(NodeItem parent)
		{
			mParent = parent;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Add																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add an existing socket to the collection.
		/// </summary>
		/// <param name="item">
		/// Reference to the socket to be added.
		/// </param>
		public new void Add(SocketItem item)
		{
			SocketEventArgs eaSocket = null;

			if(item != null)
			{
				base.Add(item);
				eaSocket = new SocketEventArgs(mParent, item);
				OnSocketAdded(eaSocket);
				BindSocket(this, item);
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Add an existing socket to the collection.
		/// </summary>
		/// <param name="item">
		/// Reference to the socket to be added.
		/// </param>
		/// <param name="attachEvent">
		/// Value indicating whether to attach the property changed event.
		/// </param>
		public void Add(SocketItem item, bool attachEvent)
		{
			SocketEventArgs eaSocket = null;

			if(item != null)
			{
				base.Add(item);
				eaSocket = new SocketEventArgs(mParent, item);
				OnSocketAdded(eaSocket);
				if(attachEvent)
				{
					BindSocket(this, item);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Parent																																*
		//*-----------------------------------------------------------------------*
		private NodeItem mParent = null;
		/// <summary>
		/// Get/Set a reference to the parent node of which this socket is a
		/// member.
		/// </summary>
		public NodeItem Parent
		{
			get { return mParent; }
			set { mParent = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Remove																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Remove the specified socket from the collection.
		/// </summary>
		/// <param name="item">
		/// Reference of the item to be removed.
		/// </param>
		/// <returns>
		/// True if the item was found and removed. Otherwise, false.
		/// </returns>
		public new bool Remove(SocketItem item)
		{
			SocketEventArgs eaSocket = null;
			bool result = false;

			if(item != null)
			{
				UnbindSocket(this, item);
				result = base.Remove(item);
				if(result)
				{
					eaSocket = new SocketEventArgs(mParent, item);
					OnSocketDeleted(eaSocket);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RemoveAll																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Remove all items matching the predicate pattern.
		/// </summary>
		/// <param name="match">
		/// Predicate expression pattern to match.
		/// </param>
		public new void RemoveAll(Predicate<SocketItem> match)
		{
			SocketEventArgs eaSocket = null;
			List<SocketItem> items = null;

			if(match != null)
			{
				items = this.FindAll(match);
				if(items.Count > 0)
				{
					base.RemoveAll(match);
					foreach(SocketItem item in items)
					{
						UnbindSocket(this, item);
						eaSocket = new SocketEventArgs(mParent, item);
						OnSocketDeleted(eaSocket);
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RemoveAt																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Remove the item at the specified ordinal index of the list.
		/// </summary>
		/// <param name="index">
		/// Ordinal index of the item to be removed.
		/// </param>
		public new void RemoveAt(int index)
		{
			SocketEventArgs eaSocket = null;
			SocketItem item = null;

			if(index > -1 && index < this.Count)
			{
				//	Index is valid.
				item = this[index];
				UnbindSocket(this, item);
				base.RemoveAt(index);
				eaSocket = new SocketEventArgs(mParent, item);
				OnSocketDeleted(eaSocket);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RemoveRange																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Remove a range of items from the list.
		/// </summary>
		/// <param name="index">
		/// Ordinal index of the position at which to start removing items.
		/// </param>
		/// <param name="count">
		/// Count of items to remove.
		/// </param>
		public new void RemoveRange(int index, int count)
		{
			SocketEventArgs eaSocket = null;
			SocketItem item = null;

			if(index > -1 && index < this.Count)
			{
				while(count > 0 && index < this.Count)
				{
					item = this[index];
					UnbindSocket(this, item);
					base.RemoveAt(index);
					count--;
					eaSocket = new SocketEventArgs(mParent, item);
					OnSocketDeleted(eaSocket);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SocketAdded																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a socket has been added to a node.
		/// </summary>
		public event SocketEventHandler SocketAdded;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SocketConnectionAdded																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a socket to socket connection has been added.
		/// </summary>
		public event SocketConnectionEventHandler SocketConnectionAdded;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SocketConnectionDeleted																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a socket to socket connection has been deleted.
		/// </summary>
		public event SocketConnectionEventHandler SocketConnectionDeleted;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SocketDeleted																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a socket has been deleted from the collection.
		/// </summary>
		public event SocketEventHandler SocketDeleted;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SocketPropertyChanged																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a property value has changed on a socket.
		/// </summary>
		public event SocketPropertyChangeEventHandler SocketPropertyChanged;
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SocketItem																															*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Display elements of a socket.
	/// </summary>
	public class SocketItem
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* BindConnections																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Bind the connection event handlers on the specified control.
		/// </summary>
		/// <param name="socket">
		/// Reference to a socket to which the event handlers will be bound.
		/// </param>
		private static void BindConnections(SocketItem socket)
		{
			if(socket != null)
			{
				socket.mConnections.SocketAdded += socket.OnConnectionAdded;
				socket.mConnections.SocketDeleted += socket.OnConnectionDeleted;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* BindProperties																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Bind the property event handlers on the specified control.
		/// </summary>
		/// <param name="socket">
		/// Reference to a socket to which the event handlers will be bound.
		/// </param>
		private static void BindProperties(SocketItem socket)
		{
			if(socket != null)
			{
				socket.mProperties.PropertyValueChanged +=
					socket.mProperties_PropertyValueChanged;
				socket.mProperties.PropertyRequest +=
					socket.mProperties_PropertyRequest;
				socket.mProperties.PropertyUpdate += socket.mProperties_PropertyUpdate;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mProperties_PropertyRequest																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The value of a property is being requested.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Property request event arguments.
		/// </param>
		private void mProperties_PropertyRequest(object sender,
			PropertyRequestEventArgs e)
		{
			List<PropertyInfo> objProperties = null;
			PropertyInfo property = null;

			if(!e.Handled)
			{
				objProperties = this.GetType().GetProperties().ToList();
				property = objProperties.FirstOrDefault(x => x.Name == e.Name);
				if(property != null)
				{
					//	Property found.
					e.Value = property.GetValue(this);
					e.Handled = true;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mProperties_PropertyUpdate																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The value of the property is being sent back to the host.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Property request event arguments.
		/// </param>
		private void mProperties_PropertyUpdate(object sender,
			PropertyRequestEventArgs e)
		{
			List<PropertyInfo> objProperties = null;
			PropertyInfo property = null;
			SocketModeEnum socketMode = SocketModeEnum.None;

			if(!e.Handled)
			{
				objProperties = this.GetType().GetProperties().ToList();
				property = objProperties.FirstOrDefault(x => x.Name == e.Name);
				if(property != null)
				{
					//	Property found.
					switch(property.Name)
					{
						case "SocketMode":
							try
							{
								if(e.Value is SocketModeEnum socketModeValue)
								{
									socketMode = socketModeValue;
								}
								else
								{
									Enum.TryParse(e.Value.ToString(), out socketMode);
								}
								property.SetValue(this, socketMode);
							}
							catch { }
							break;
						default:
							try
							{
								property.SetValue(this, e.Value);
							}
							catch { }
							break;
					}
					e.Handled = true;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mProperties_PropertyValueChanged																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The value of a property has changed on the socket properties
		/// collection.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Property change event arguments.
		/// </param>
		private void mProperties_PropertyValueChanged(object sender,
			PropertyChangeEventArgs e)
		{
			SocketPropertyChangeEventArgs eaProperty =
				new SocketPropertyChangeEventArgs();
			eaProperty.PropertyName = e.Name;
			eaProperty.Socket = this;
			eaProperty.ValueBefore = e.ValueBefore;
			eaProperty.ValueAfter = e.ValueAfter;
			OnPropertyChanged(sender, eaProperty);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UnbindConnections																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Unbind the connection event handlers on the specified control.
		/// </summary>
		/// <param name="socket">
		/// Reference to a socket from which the event handlers will be unbound.
		/// </param>
		private static void UnbindConnections(SocketItem socket)
		{
			if(socket != null)
			{
				socket.mConnections.SocketAdded -= socket.OnConnectionAdded;
				socket.mConnections.SocketDeleted -= socket.OnConnectionDeleted;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UnbindProperties																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Unbind the property event handlers on the specified control.
		/// </summary>
		/// <param name="socket">
		/// Reference to a socket from which the event handlers will be unbound.
		/// </param>
		private static void UnbindProperties(SocketItem socket)
		{
			if(socket != null)
			{
				socket.mProperties.PropertyValueChanged -=
					socket.mProperties_PropertyValueChanged;
				socket.mProperties.PropertyRequest -=
					socket.mProperties_PropertyRequest;
				socket.mProperties.PropertyUpdate -= socket.mProperties_PropertyUpdate;
			}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnConnectionAdded																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the ConnectionAdded event when a connection has been added to
		/// this socket.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket event arguments.
		/// </param>
		protected void OnConnectionAdded(object sender, SocketEventArgs e)
		{
			SocketConnectionEventArgs ea = new SocketConnectionEventArgs();

			ea.Socket = this;
			ea.Connection = e.Socket;
			ConnectionAdded?.Invoke(sender, ea);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnConnectionDeleted																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the ConnectionDeleted event when a connection has been removed
		/// from this socket.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket event arguments.
		/// </param>
		protected void OnConnectionDeleted(object sender, SocketEventArgs e)
		{
			SocketConnectionEventArgs ea = new SocketConnectionEventArgs();

			ea.Socket = this;
			ea.Connection = e.Socket;
			ConnectionDeleted?.Invoke(sender, ea);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnPropertyChanged																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the PropertyChanged event when a property on the socket has
		/// changed.
		/// </summary>
		/// <param name="e">
		/// Socket property change event arguments.
		/// </param>
		protected void OnPropertyChanged(object sender,
			SocketPropertyChangeEventArgs e)
		{
			PropertyChanged?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//public static float SocketHeight { get; set; } = 16f;

		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the SocketItem Item.
		/// </summary>
		public SocketItem()
		{
			BindProperties(this);
			BindConnections(this);
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the SocketItem Item.
		/// </summary>
		/// <param name="data">
		/// Reference to an inactive socket item.
		/// </param>
		/// <remarks>
		/// This constructor builds an active socket item from a data-only copy.
		/// </remarks>
		public SocketItem(SocketDataItem data) : this()
		{
			if(data != null)
			{
				this.mHeight = data.Height;
				this.mSocketMode =
					(SocketModeEnum)Enum.Parse(typeof(SocketModeEnum),
					data.SocketMode, true);
				this.mTextBounds = new RectangleF(
					data.TextX, data.TextY, data.TextWidth, data.TextHeight);
				this.mTicket = data.Ticket;
				this.mTitleProperty = data.TitleProperty;
				this.mWidth = data.Width;
				this.mX = data.X;
				this.mY = data.Y;
				//	Properties.
				foreach(NameValueItem item in data.Properties)
				{
					//	The data contains static properties only.
					this.mProperties.Add(item.Name, item.Value);
				}
				//	Connections have to wait until all sockets have been loaded.
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the SocketItem Item.
		/// </summary>
		/// <param name="original">
		/// Reference to the original socket to copy.
		/// </param>
		/// <remarks>
		/// The new item will have a unique name.
		/// </remarks>
		public SocketItem(SocketItem original) : this()
		{
			if(original != null)
			{
				this.mHeight = original.mHeight;
				this.mSocketMode = original.mSocketMode;
				this.mTextBounds = new RectangleF(
					original.mTextBounds.Location, original.mTextBounds.Size);
				this.mTitleProperty = original.TitleProperty;
				this.mWidth = original.mWidth;
				this.mX = original.mX;
				this.mY = original.mY;
				//	Properties.
				foreach(PropertyItem property in original.Properties)
				{
					//	Transfer static properties only.
					if(property.Static)
					{
						this.mProperties.Add(property.Name, property.Value);
					}
				}
				//	Connections.
				foreach(SocketItem connection in original.mConnections)
				{
					this.mConnections.Add(connection, false);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	_Indexer																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the value of the specified property by name.
		/// </summary>
		public PropertyItem this[string name]
		{
			get
			{
				PropertyItem result = mProperties[name];
				return result;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ConnectionAdded																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a connection has been added to this socket.
		/// </summary>
		public event SocketConnectionEventHandler ConnectionAdded;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ConnectionDeleted																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a connection has been deleted from this socket.
		/// </summary>
		public event SocketConnectionEventHandler ConnectionDeleted;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Connections																														*
		//*-----------------------------------------------------------------------*
		private SocketCollection mConnections = new SocketCollection();
		/// <summary>
		/// Get a reference to the collection of sockets to which this socket is
		/// connected.
		/// </summary>
		public SocketCollection Connections
		{
			get { return mConnections; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetBounds																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the bounds of this item on the parent object.
		/// </summary>
		public RectangleF GetBounds()
		{
			RectangleF result = new RectangleF(X, Y, Width, Height);
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetLocation																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the location of this item, relative to its container.
		/// </summary>
		public PointF GetLocation()
		{
			return new PointF(mX, mY);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Height																																*
		//*-----------------------------------------------------------------------*
		private float mHeight = 16f;
		/// <summary>
		/// Get/Set the height of the item.
		/// </summary>
		public float Height
		{
			get { return mHeight; }
			set
			{
				SocketPropertyChangeEventArgs eaProperty =
					new SocketPropertyChangeEventArgs();
				float original = mHeight;
				mHeight = value;
				eaProperty.PropertyName = "Height";
				eaProperty.Socket = this;
				eaProperty.ValueBefore = original;
				eaProperty.ValueAfter = value;
				OnPropertyChanged(this, eaProperty);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Properties																														*
		//*-----------------------------------------------------------------------*
		private PropertyCollection mProperties = new PropertyCollection();
		/// <summary>
		/// Get a reference to the collection of properties on this item.
		/// </summary>
		public PropertyCollection Properties
		{
			get { return mProperties; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PropertyChanged																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a property has changed on the socket.
		/// </summary>
		public event SocketPropertyChangeEventHandler PropertyChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SetConnections																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Replace the connections collection of the specified socket with another
		/// preloaded collection.
		/// </summary>
		/// <param name="socket">
		/// Reference to the socket to be adapted.
		/// </param>
		/// <param name="connections">
		/// Reference to a replacement connections collection.
		/// </param>
		public static void SetConnections(SocketItem socket,
			SocketCollection connections)
		{
			if(socket != null && connections != null)
			{
				UnbindConnections(socket);
				socket.mConnections = connections;
				BindConnections(socket);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SetProperties																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Replace the properties collection of the specified socket with another
		/// preloaded collection.
		/// </summary>
		/// <param name="socket">
		/// Reference to the socket to be adapted.
		/// </param>
		/// <param name="properties">
		/// Reference to a replacement properties collection.
		/// </param>
		public static void SetProperties(SocketItem socket,
			PropertyCollection properties)
		{
			if(socket != null && properties != null)
			{
				UnbindProperties(socket);
				socket.mProperties = properties;
				BindProperties(socket);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SocketMode																														*
		//*-----------------------------------------------------------------------*
		private SocketModeEnum mSocketMode = SocketModeEnum.None;
		/// <summary>
		/// Get/Set the operational mode of this socket.
		/// </summary>
		public SocketModeEnum SocketMode
		{
			get { return mSocketMode; }
			set
			{
				SocketPropertyChangeEventArgs eaProperty =
					new SocketPropertyChangeEventArgs();
				SocketModeEnum original = mSocketMode;
				mSocketMode = value;
				eaProperty.PropertyName = "SocketMode";
				eaProperty.Socket = this;
				eaProperty.ValueBefore = original;
				eaProperty.ValueAfter = value;
				OnPropertyChanged(this, eaProperty);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	TextBounds																														*
		//*-----------------------------------------------------------------------*
		private RectangleF mTextBounds = RectangleF.Empty;
		/// <summary>
		/// Get/Set the text bounds of this socket.
		/// </summary>
		/// <remarks>
		/// The height of this property is also used to determine the vertical
		/// spacing between this socket and the next.
		/// </remarks>
		public RectangleF TextBounds
		{
			get { return mTextBounds; }
			set
			{
				SocketPropertyChangeEventArgs eaProperty =
					new SocketPropertyChangeEventArgs();
				RectangleF original = mTextBounds;
				mTextBounds = value;
				eaProperty.PropertyName = "TextBounds";
				eaProperty.Socket = this;
				eaProperty.ValueBefore = original;
				eaProperty.ValueAfter = value;
				OnPropertyChanged(this, eaProperty);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Ticket																																*
		//*-----------------------------------------------------------------------*
		private string mTicket = Guid.NewGuid().ToString("D");
		/// <summary>
		/// Get/Set the globally unique ID of the socket.
		/// </summary>
		public string Ticket
		{
			get { return mTicket; }
			set
			{
				SocketPropertyChangeEventArgs eaProperty =
					new SocketPropertyChangeEventArgs();
				string original = mTicket;
				mTicket = value;
				eaProperty.PropertyName = "Ticket";
				eaProperty.Socket = this;
				eaProperty.ValueBefore = original;
				eaProperty.ValueAfter = value;
				OnPropertyChanged(this, eaProperty);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	TitleProperty																													*
		//*-----------------------------------------------------------------------*
		private string mTitleProperty = "Ticket";
		/// <summary>
		/// Get/Set the name of the title property for this item.
		/// </summary>
		public string TitleProperty
		{
			get { return mTitleProperty; }
			set
			{
				SocketPropertyChangeEventArgs eaProperty =
					new SocketPropertyChangeEventArgs();
				string original = mTitleProperty;
				mTitleProperty = value;
				eaProperty.PropertyName = "TitleProperty";
				eaProperty.Socket = this;
				eaProperty.ValueBefore = original;
				eaProperty.ValueAfter = value;
				OnPropertyChanged(this, eaProperty);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Width																																	*
		//*-----------------------------------------------------------------------*
		private float mWidth = 16f;
		/// <summary>
		/// Get/Set the width of the item on the parent object.
		/// </summary>
		public float Width
		{
			get { return mWidth; }
			set
			{
				SocketPropertyChangeEventArgs eaProperty =
					new SocketPropertyChangeEventArgs();
				float original = mWidth;
				mWidth = value;
				eaProperty.PropertyName = "Width";
				eaProperty.Socket = this;
				eaProperty.ValueBefore = original;
				eaProperty.ValueAfter = value;
				OnPropertyChanged(this, eaProperty);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	X																																			*
		//*-----------------------------------------------------------------------*
		private float mX = 0f;
		/// <summary>
		/// Get/Set the X-coordinate of the item on the parent object.
		/// </summary>
		public float X
		{
			get { return mX; }
			set
			{
				SocketPropertyChangeEventArgs eaProperty =
					new SocketPropertyChangeEventArgs();
				float original = mX;
				mX = value;
				eaProperty.PropertyName = "X";
				eaProperty.Socket = this;
				eaProperty.ValueBefore = original;
				eaProperty.ValueAfter = value;
				OnPropertyChanged(this, eaProperty);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Y																																			*
		//*-----------------------------------------------------------------------*
		private float mY = 0f;
		/// <summary>
		/// Get/Set the Y-coordinate of the item on the parent object.
		/// </summary>
		public float Y
		{
			get { return mY; }
			set
			{
				SocketPropertyChangeEventArgs eaProperty =
					new SocketPropertyChangeEventArgs();
				float original = mY;
				mY = value;
				eaProperty.PropertyName = "Y";
				eaProperty.Socket = this;
				eaProperty.ValueBefore = original;
				eaProperty.ValueAfter = value;
				OnPropertyChanged(this, eaProperty);
			}
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//* SocketConnectionEventArgs																								*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event arguments for socket to socket connections.
	/// </summary>
	public class SocketConnectionEventArgs : EventArgs
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
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the SocketConnectionEventArgs Item.
		/// </summary>
		public SocketConnectionEventArgs()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the SocketConnectionEventArgs Item.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to which the socket belongs.
		/// </param>
		/// <param name="socket">
		/// Reference of the socket being tracked.
		/// </param>
		public SocketConnectionEventArgs(NodeItem node, SocketItem socket,
			SocketItem connection)
		{
			mNode = node;
			mSocket = socket;
			mConnection = connection;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Connection																														*
		//*-----------------------------------------------------------------------*
		private SocketItem mConnection = null;
		/// <summary>
		/// Get/Set a reference to the connected node socket.
		/// </summary>
		public SocketItem Connection
		{
			get { return mConnection; }
			set { mConnection = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Node																																	*
		//*-----------------------------------------------------------------------*
		private NodeItem mNode = null;
		/// <summary>
		/// Get/Set a reference to the affected node.
		/// </summary>
		public NodeItem Node
		{
			get { return mNode; }
			set { mNode = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Socket																																*
		//*-----------------------------------------------------------------------*
		private SocketItem mSocket = null;
		/// <summary>
		/// Get/Set a reference to the affected node socket.
		/// </summary>
		public SocketItem Socket
		{
			get { return mSocket; }
			set { mSocket = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//* SocketConnectionEventHandler																						*
	//*-------------------------------------------------------------------------*
	public delegate void SocketConnectionEventHandler(object sender,
		SocketConnectionEventArgs e);
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SocketEventArgs																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Handling for socket events on a node.
	/// </summary>
	public class SocketEventArgs : EventArgs
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
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the SocketEventArgs Item.
		/// </summary>
		public SocketEventArgs()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the SocketEventArgs Item.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to which the socket belongs.
		/// </param>
		/// <param name="socket">
		/// Reference of the socket being tracked.
		/// </param>
		public SocketEventArgs(NodeItem node, SocketItem socket)
		{
			mNode = node;
			mSocket = socket;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Node																																	*
		//*-----------------------------------------------------------------------*
		private NodeItem mNode = null;
		/// <summary>
		/// Get/Set a reference to the affected node.
		/// </summary>
		public NodeItem Node
		{
			get { return mNode; }
			set { mNode = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Socket																																*
		//*-----------------------------------------------------------------------*
		private SocketItem mSocket = null;
		/// <summary>
		/// Get/Set a reference to the affected node socket.
		/// </summary>
		public SocketItem Socket
		{
			get { return mSocket; }
			set { mSocket = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//* SocketEventHandler																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event handler for events where node sockets are affected.
	/// </summary>
	/// <param name="sender">
	/// The object raising this event.
	/// </param>
	/// <param name="e">
	/// Node socket event arguments.
	/// </param>
	public delegate void SocketEventHandler(object sender,
		SocketEventArgs e);
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SocketPropertyChangeEventArgs																						*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Handling for property change events on a node socket.
	/// </summary>
	public class SocketPropertyChangeEventArgs : EventArgs
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
		//*	Node																																	*
		//*-----------------------------------------------------------------------*
		private NodeItem mNode = null;
		/// <summary>
		/// Get/Set a reference to the affected node.
		/// </summary>
		public NodeItem Node
		{
			get { return mNode; }
			set { mNode = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PropertyName																													*
		//*-----------------------------------------------------------------------*
		private string mPropertyName = "";
		/// <summary>
		/// Get/Set the name of the property being changed.
		/// </summary>
		public string PropertyName
		{
			get { return mPropertyName; }
			set { mPropertyName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Socket																																*
		//*-----------------------------------------------------------------------*
		private SocketItem mSocket = null;
		/// <summary>
		/// Get/Set a reference to the affected node socket.
		/// </summary>
		public SocketItem Socket
		{
			get { return mSocket; }
			set { mSocket = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ValueAfter																														*
		//*-----------------------------------------------------------------------*
		private object mValueAfter = null;
		/// <summary>
		/// Get/Set the value after the change.
		/// </summary>
		public object ValueAfter
		{
			get { return mValueAfter; }
			set { mValueAfter = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ValueBefore																														*
		//*-----------------------------------------------------------------------*
		private object mValueBefore = null;
		/// <summary>
		/// Get/Set the value before the change.
		/// </summary>
		public object ValueBefore
		{
			get { return mValueBefore; }
			set { mValueBefore = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//* SocketPropertyChangeEventHandler																				*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event handler for events where node socket properties have been changed.
	/// </summary>
	/// <param name="sender">
	/// The object raising this event.
	/// </param>
	/// <param name="e">
	/// Node socket property change event arguments.
	/// </param>
	public delegate void SocketPropertyChangeEventHandler(object sender,
		SocketPropertyChangeEventArgs e);
	//*-------------------------------------------------------------------------*

}
