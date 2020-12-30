//	Node.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

using static Scaffold.ScaffoldNodesUtil;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	NodeCollection																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of NodeItem Items.
	/// </summary>
	public class NodeCollection : List<NodeItem>
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* BindEvents																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Bind node events to this collection.
		/// </summary>
		/// <param name="item">
		/// Node whose events will be relayed by the collection.
		/// </param>
		private void BindEvents(NodeItem item)
		{
			if(item != null)
			{
				item.PropertyChanged += OnNodePropertyChanged;
				item.SelectedChanged += OnSelectionChanged;
				item.SocketAdded += OnSocketAdded;
				item.SocketConnectionAdded += OnSocketConnectionAdded;
				item.SocketConnectionDeleted += OnSocketConnectionDeleted;
				item.SocketDeleted += OnSocketDeleted;
				item.SocketPropertyChanged += OnSocketPropertyChanged;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UnbindEvents																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Unbind node events from this collection.
		/// </summary>
		/// <param name="item">
		/// Node whose events are being relayed by the collection.
		/// </param>
		private void UnbindEvents(NodeItem item)
		{
			if(item != null)
			{
				item.PropertyChanged -= OnNodePropertyChanged;
				item.SelectedChanged -= OnSelectionChanged;
				item.SocketAdded -= OnSocketAdded;
				item.SocketConnectionAdded -= OnSocketConnectionAdded;
				item.SocketConnectionDeleted -= OnSocketConnectionDeleted;
				item.SocketDeleted -= OnSocketDeleted;
				item.SocketPropertyChanged -= OnSocketPropertyChanged;
			}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnNodeAdded																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the NodeAdded event when a node has been added.
		/// </summary>
		/// <param name="e">
		/// Node event arguments.
		/// </param>
		protected virtual void OnNodeAdded(NodeEventArgs e)
		{
			NodeAdded?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnNodeDeleted																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the NodeDeleted event when a node has been deleted from the
		/// collection.
		/// </summary>
		/// <param name="e">
		/// Node event arguments.
		/// </param>
		protected virtual void OnNodeDeleted(NodeEventArgs e)
		{
			NodeDeleted?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnNodePropertyChanged																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the NodePropertyChanged event when the value of a node property
		/// has been changed.
		/// </summary>
		/// <param name="e">
		/// Node property change event arguments.
		/// </param>
		protected virtual void OnNodePropertyChanged(object sender,
			NodePropertyChangeEventArgs e)
		{
			NodePropertyChanged?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSelectionChanged																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SelectionChanged event when the Selected property on a node
		/// has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Node event arguments.
		/// </param>
		protected virtual void OnSelectionChanged(object sender, NodeEventArgs e)
		{
			SelectionChanged?.Invoke(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSocketAdded																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SocketAdded event when a socket has been added to the
		/// collection.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket event arguments.
		/// </param>
		protected virtual void OnSocketAdded(object sender, SocketEventArgs e)
		{
			SocketAdded?.Invoke(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSocketConnectionAdded																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SocketConnectionAdded event when a connection has been added
		/// to a socket in the collection.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
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
		/// Raises the SocketConnectionDeleted event when a connection has been
		/// deleted from a socket in the collection.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
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
		/// Raises the SocketDeleted event when a socket has been deleted from
		/// the collection.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket event arguments.
		/// </param>
		protected virtual void OnSocketDeleted(object sender, SocketEventArgs e)
		{
			SocketDeleted?.Invoke(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSocketPropertyChanged																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SocketPropertyChanged event when a property value on a
		/// socket has been changed.
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
		//* Add																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add an existing node item to the collection.
		/// </summary>
		/// <param name="item">
		/// Reference to the node item to be added.
		/// </param>
		public new void Add(NodeItem item)
		{
			NodeEventArgs eaNode = null;

			if(item != null)
			{
				base.Add(item);
				eaNode = new NodeEventArgs(item);
				OnNodeAdded(eaNode);
				BindEvents(item);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	FirstOutputSocket																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the first output socket connected to the specified input socket.
		/// </summary>
		/// <param name="inputSocket">
		/// Input socket to match in the output socket's Connections collection.
		/// </param>
		public SocketItem FirstOutputSocket(SocketItem inputSocket)
		{
			SocketItem result = null;
			List<SocketItem> sockets = null;

			foreach(NodeItem node in this)
			{
				sockets =
					node.Sockets.FindAll(x => x.SocketMode == SocketModeEnum.Output);
				foreach(SocketItem socket in sockets)
				{
					if(socket.Connections.Exists(x => x.Ticket == inputSocket.Ticket))
					{
						result = socket;
						break;
					}
				}
				if(result != null)
				{
					break;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetSocketByTicket																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the socket item having the specified globally unique ID.
		/// </summary>
		/// <param name="ticket">
		/// Ticket value.
		/// </param>
		/// <returns>
		/// Socket item with the specified globally unique identification, if
		/// found. Otherwise, null;
		/// </returns>
		public SocketItem GetSocketByTicket(string ticket)
		{
			SocketItem result = null;

			if(ticket?.Length > 0)
			{
				foreach(NodeItem node in this)
				{
					foreach(SocketItem socket in node.Sockets)
					{
						if(socket.Ticket == ticket)
						{
							result = socket;
							break;
						}
					}
					if(result != null)
					{
						break;
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* NodeAdded																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a node has been added to the collection.
		/// </summary>
		public event NodeEventHandler NodeAdded;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* NodeDeleted																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a node has been deleted from the collection.
		/// </summary>
		public event NodeEventHandler NodeDeleted;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* NodePropertyChanged																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the property value of a node has been changed.
		/// </summary>
		public event NodePropertyChangeEventHandler NodePropertyChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Remove																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Remove the specified node from the collection.
		/// </summary>
		/// <param name="item">
		/// Reference of the item to be removed.
		/// </param>
		/// <returns>
		/// True if the item was found and removed. Otherwise, false.
		/// </returns>
		public new bool Remove(NodeItem item)
		{
			NodeEventArgs eaNode = null;
			bool result = false;

			if(item != null)
			{
				UnbindEvents(item);
				result = base.Remove(item);
				if(result)
				{
					eaNode = new NodeEventArgs(item);
					OnNodeDeleted(eaNode);
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
		public new void RemoveAll(Predicate<NodeItem> match)
		{
			NodeEventArgs eaNode = null;
			List<NodeItem> items = null;

			if(match != null)
			{
				items = this.FindAll(match);
				if(items.Count > 0)
				{
					base.RemoveAll(match);
					foreach(NodeItem item in items)
					{
						UnbindEvents(item);
						eaNode = new NodeEventArgs(item);
						OnNodeDeleted(eaNode);
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
			NodeEventArgs eaNode = null;
			NodeItem item = null;

			if(index > -1 && index < this.Count)
			{
				//	Index is valid.
				item = this[index];
				UnbindEvents(item);
				base.RemoveAt(index);
				eaNode = new NodeEventArgs(item);
				OnNodeDeleted(eaNode);
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
			NodeEventArgs eaNode = null;
			NodeItem item = null;

			if(index > -1 && index < this.Count)
			{
				while(count > 0 && index < this.Count)
				{
					item = this[index];
					UnbindEvents(item);
					base.RemoveAt(index);
					count--;
					eaNode = new NodeEventArgs(item);
					OnNodeDeleted(eaNode);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SelectionChanged																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the Selected property of a node has changed.
		/// </summary>
		public event NodeEventHandler SelectionChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SocketAdded																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a socket has been added to the sockets collection.
		/// </summary>
		public event SocketEventHandler SocketAdded;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SocketConnectionAdded																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a connection has been added to a socket in the sockets
		/// collection.
		/// </summary>
		public event SocketConnectionEventHandler SocketConnectionAdded;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SocketConnectionDeleted																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a connection has been deleted from a socket in the sockets
		/// collection.
		/// </summary>
		public event SocketConnectionEventHandler SocketConnectionDeleted;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SocketDeleted																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a socket has been deleted from the sockets collection.
		/// </summary>
		public event SocketEventHandler SocketDeleted;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SocketPropertyChanged																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the property value on a socket in the sockets collection has
		/// been changed.
		/// </summary>
		public event SocketPropertyChangeEventHandler SocketPropertyChanged;
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	NodeFileDescriptor																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Description and top-level information about a node file.
	/// </summary>
	public class NodeFileDescriptor
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
		public string Name
		{
			get { return mName; }
			set { mName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Ticket																																*
		//*-----------------------------------------------------------------------*
		private string mTicket = Guid.NewGuid().ToString("D");
		/// <summary>
		/// Get/Set the globally unique identification of this file.
		/// </summary>
		public string Ticket
		{
			get { return mTicket; }
			set { mTicket = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*


	//*-------------------------------------------------------------------------*
	//*	NodeFileItem																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Decision tree file structure.
	/// </summary>
	public class NodeFileItem : NodeFileDescriptor
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
		//*	Clear																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Clear the contents of this file.
		/// </summary>
		public void Clear()
		{
			this.Description = "";
			this.Name = "";
			this.Ticket = Guid.NewGuid().ToString("D");
			mNodes.Clear();
			mResources.Clear();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Nodes																																	*
		//*-----------------------------------------------------------------------*
		private NodeCollection mNodes = new NodeCollection();
		/// <summary>
		/// Get a reference to the collection of nodes in this file.
		/// </summary>
		public NodeCollection Nodes
		{
			get { return mNodes; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Resources																															*
		//*-----------------------------------------------------------------------*
		private ResourceCollection mResources = new ResourceCollection();
		/// <summary>
		/// Get a reference to the collection of universal single instance
		/// resources in the file.
		/// </summary>
		public ResourceCollection Resources
		{
			get { return mResources; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	NodeItem																																*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Display elements of a node.
	/// </summary>
	public class NodeItem
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* BindProperties																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Bind the property collection events to the specified node.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to be bound to property events.
		/// </param>
		private static void BindProperties(NodeItem node)
		{
			if(node != null)
			{
				node.mProperties.PropertyRequest +=
					node.mProperties_PropertyRequest;
				node.mProperties.PropertyUpdate +=
					node.mProperties_PropertyUpdate;
				node.mProperties.PropertyValueChanged +=
					node.mProperties_PropertyValueChanged;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* BindSockets																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Bind socket collection events to the specified node.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to receive socket events.
		/// </param>
		private static void BindSockets(NodeItem node)
		{
			if(node != null)
			{
				node.mSockets.SocketAdded += node.OnSocketAdded;
				node.mSockets.SocketConnectionAdded += node.OnSocketConnectionAdded;
				node.mSockets.SocketConnectionDeleted +=
					node.OnSocketConnectionDeleted;
				node.mSockets.SocketDeleted += node.OnSocketDeleted;
				node.mSockets.SocketPropertyChanged += node.OnSocketPropertyChanged;
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

			if(!e.Handled)
			{
				objProperties = this.GetType().GetProperties().ToList();
				property = objProperties.FirstOrDefault(x => x.Name == e.Name);
				if(property != null)
				{
					//	Property found.
					try
					{
						property.SetValue(this, e.Value);
					}
					catch { }
					e.Handled = true;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mProperties_PropertyValueChanged																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The value of a property in the Properties collection has been changed.
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
			NodePropertyChangeEventArgs eaProperty =
				new NodePropertyChangeEventArgs();
			eaProperty.PropertyName = e.Name;
			eaProperty.Node = this;
			eaProperty.ValueBefore = e.ValueBefore;
			eaProperty.ValueAfter = e.ValueAfter;
			OnPropertyChanged(sender, eaProperty);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UnbindProperties																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Unbind the property collection events to the specified node.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to be unbound from property events.
		/// </param>
		private static void UnbindProperties(NodeItem node)
		{
			if(node != null)
			{
				node.mProperties.PropertyRequest -=
					node.mProperties_PropertyRequest;
				node.mProperties.PropertyUpdate -=
					node.mProperties_PropertyUpdate;
				node.mProperties.PropertyValueChanged -=
					node.mProperties_PropertyValueChanged;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UnbindSockets																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Unbind socket collection events from the specified node.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to dismiss socket events.
		/// </param>
		private static void UnbindSockets(NodeItem node)
		{
			if(node != null)
			{
				node.mSockets.SocketAdded -= node.OnSocketAdded;
				node.mSockets.SocketConnectionAdded -= node.OnSocketConnectionAdded;
				node.mSockets.SocketConnectionDeleted -=
					node.OnSocketConnectionDeleted;
				node.mSockets.SocketDeleted -= node.OnSocketDeleted;
				node.mSockets.SocketPropertyChanged -= node.OnSocketPropertyChanged;
			}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnPropertyChanged																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the PropertyChanged event when the value of a property has been
		/// changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Node property change event arguments.
		/// </param>
		protected virtual void OnPropertyChanged(object sender,
			NodePropertyChangeEventArgs e)
		{
			PropertyChanged?.Invoke(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSelectedChanged																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SelectedChanged event when the Selected property of the
		/// node has been changed.
		/// </summary>
		/// <param name="e">
		/// Node event arguments.
		/// </param>
		protected virtual void OnSelectedChanged(NodeEventArgs e)
		{
			SelectedChanged?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSocketAdded																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SocketAdded event when a socket has been added to the
		/// collection.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket event arguments.
		/// </param>
		protected virtual void OnSocketAdded(object sender, SocketEventArgs e)
		{
			e.Node = this;
			SocketAdded?.Invoke(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSocketConnectionAdded																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SocketConnectionAdded event when a connection has been added
		/// to a socket in the collection.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket event arguments.
		/// </param>
		protected virtual void OnSocketConnectionAdded(object sender,
			SocketConnectionEventArgs e)
		{
			e.Node = this;
			SocketConnectionAdded?.Invoke(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSocketConnectionDeleted																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SocketConnectionDeleted event when a connection has been
		/// deleted from a socket in the collection.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket event arguments.
		/// </param>
		protected virtual void OnSocketConnectionDeleted(object sender,
			SocketConnectionEventArgs e)
		{
			e.Node = this;
			SocketConnectionDeleted?.Invoke(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSocketDeleted																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SocketDeleted event when a socket has been deleted from
		/// the collection.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket event arguments.
		/// </param>
		protected virtual void OnSocketDeleted(object sender, SocketEventArgs e)
		{
			e.Node = this;
			SocketDeleted?.Invoke(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSocketPropertyChanged																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SocketPropertyChanged event when a property value on a
		/// socket has been changed.
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
			e.Node = this;
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
		/// Create a new instance of the NodeItem Item.
		/// </summary>
		public NodeItem()
		{
			BindProperties(this);
			mSockets = new SocketCollection(this);
			BindSockets(this);
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the NodeItem Item.
		/// </summary>
		/// <param name="data">
		/// Reference to an inactive node item.
		/// </param>
		/// <remarks>
		/// This constructor builds an active node item from a data-only copy.
		/// </remarks>
		public NodeItem(NodeDataItem data) : this()
		{
			if(data != null)
			{
				this.mHeight = data.Height;
				this.mNodeColor = FromHex(data.NodeColor);
				this.mNodeTextColor = FromHex(data.NodeTextColor);
				this.mNodeType = data.NodeType;
				this.mTicket = data.Ticket;
				this.mTitleHeight = data.TitleHeight;
				this.mTitleProperty = data.TitleProperty;
				this.mWidth = data.Width;
				this.mX = data.X;
				this.mY = data.Y;
				this.mZOrder = data.ZOrder;
				this.mDelay = data.Delay;
				//	Restore properties.
				foreach(NameValueItem item in data.Properties)
				{
					//	All properties in this collection are static only.
					mProperties.Add(item.Name, item.Value);
				}
				//	Restore socket definitions.
				foreach(SocketDataItem socket in data.Sockets)
				{
					mSockets.Add(new SocketItem(socket));
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the NodeItem Item.
		/// </summary>
		public NodeItem(NodeItem original) : this()
		{
			if(original != null)
			{
				this.mHeight = original.mHeight;
				this.mNodeColor = original.mNodeColor;
				this.mNodeTextColor = original.mNodeTextColor;
				this.mNodeType = original.mNodeType;
				this.mSelected = original.mSelected;
				this.mTitleHeight = original.TitleHeight;
				this.mTitleProperty = original.TitleProperty;
				this.mWidth = original.mWidth;
				this.mX = original.mX;
				this.mY = original.mY;
				this.mZOrder = original.mZOrder;
				this.mDelay = original.mDelay;
				//	Properties.
				foreach(PropertyItem property in original.Properties)
				{
					//	Only static properties are transferred.
					if(property.Static)
					{
						this.mProperties.Add(property.Name, property.Value);
					}
				}
				//	Sockets.
				foreach(SocketItem socket in original.mSockets)
				{
					this.mSockets.Add(new SocketItem(socket));
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
		//*	Delay																																	*
		//*-----------------------------------------------------------------------*
		private float mDelay = 0f;
		/// <summary>
		/// Get/Set the number of seconds to delay before continuing.
		/// </summary>
		public float Delay
		{
			get { return mDelay; }
			set
			{
				NodePropertyChangeEventArgs eaProperty = null;
				float original = mDelay;

				mDelay = value;
				if(original != value)
				{
					eaProperty = new NodePropertyChangeEventArgs();
					eaProperty.Node = this;
					eaProperty.PropertyName = "Delay";
					eaProperty.ValueBefore = original;
					eaProperty.ValueAfter = value;
					OnPropertyChanged(this, eaProperty);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetBounds																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the boundaries of this item on the parent object.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to be bounded.
		/// </param>
		public static RectangleF GetBounds(NodeItem node)
		{
			RectangleF result = RectangleF.Empty;

			if(node != null)
			{
				result = new RectangleF(node.X, node.Y, node.Width, node.Height);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetIconArea																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the area of the node set aside for icons and thumbnails.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to measure.
		/// </param>
		/// <param name="propertyName">
		/// Name of a specific property area to test for.
		/// </param>
		/// <returns>
		/// Reference to a floating point rectangle representing the icon area, if
		/// icons or thumbnails are present. Otherwise, an empty rectangle.
		/// </returns>
		public static RectangleF GetIconArea(NodeItem node,
			string propertyName = "")
		{
			RectangleF result = RectangleF.Empty;
			SizeF title = SizeF.Empty;

			if(node != null && node.mIconHeight > 0)
			{
				title = GetTitleSize(node);
				switch(propertyName)
				{
					case "MediaAudio":
						result = new RectangleF(node.mX + 4f, node.mY + title.Height + 4f,
							32f, 32f);
						break;
					case "MediaLink":
						result = new RectangleF(node.mX + 4f, node.mY + title.Height + 36f,
							32f, 32f);
						break;
					case "MediaImage":
					case "MediaVideo":
					default:
						result = new RectangleF(node.mX, node.mY + title.Height,
							node.Width, node.IconHeight);
						break;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetLocation																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the location of this item, relative to its container.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to be inspected.
		/// </param>
		public static PointF GetLocation(NodeItem node)
		{
			PointF result = PointF.Empty;

			if(node != null)
			{
				result = new PointF(node.mX, node.mY);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetTitleSize																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the size of the node title area.
		/// </summary>
		public static SizeF GetTitleSize(NodeItem node)
		{
			SizeF result = SizeF.Empty;

			if(node != null)
			{
				result = new SizeF(node.Width, node.TitleHeight);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetPropertyNames																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a list of all property names on the node; permanent and dynamic.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to be inspected.
		/// </param>
		/// <returns>
		/// List of all permanent and dymamic property names that can be read
		/// publicly.
		/// </returns>
		public static List<string> GetPropertyNames(NodeItem node)
		{
			PropertyInfo[] properties = null;
			List<string> result = new List<string>();

			if(node != null)
			{
				properties = typeof(NodeItem).GetProperties(
					BindingFlags.Public | BindingFlags.Instance);
				foreach(PropertyInfo property in properties)
				{
					if(property.Name != "Item")
					{
						result.Add(property.Name);
					}
				}
				foreach(PropertyItem property in node.mProperties)
				{
					if(!result.Exists(x => x == property.Name))
					{
						result.Add(property.Name);
					}
				}
			}
			return result;
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
		public float Height
		{
			get { return mHeight; }
			set
			{
				NodePropertyChangeEventArgs eaProperty = null;
				float original = mHeight;

				mHeight = value;
				if(original != mHeight)
				{
					eaProperty = new NodePropertyChangeEventArgs();
					eaProperty.Node = this;
					eaProperty.PropertyName = "Height";
					eaProperty.ValueBefore = original;
					eaProperty.ValueAfter = value;
					OnPropertyChanged(this, eaProperty);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	IconHeight																														*
		//*-----------------------------------------------------------------------*
		private float mIconHeight = 0f;
		/// <summary>
		/// Get/Set the height of the node's icon section.
		/// </summary>
		public float IconHeight
		{
			get { return mIconHeight; }
			set
			{
				//NodePropertyChangeEventArgs eaProperty =
				//	new NodePropertyChangeEventArgs();
				//float original = mIconHeight;

				mIconHeight = value;
				//eaProperty.Node = this;
				//eaProperty.PropertyName = "IconHeight";
				//eaProperty.ValueBefore = original;
				//eaProperty.ValueAfter = value;
				//OnPropertyChanged(this, eaProperty);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MaxSocketWidth																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the maximum width of the sockets in this node.
		/// </summary>
		/// <returns>
		/// Width of the widest socket in the Sockets collection.
		/// </returns>
		public float MaxSocketWidth()
		{
			float result = 2f;

			if(mSockets.Count > 0)
			{
				result = mSockets.Max(x => x.Width);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeColor																															*
		//*-----------------------------------------------------------------------*
		private Color mNodeColor = Color.Empty;
		/// <summary>
		/// Get/Set the node color.
		/// </summary>
		public Color NodeColor
		{
			get { return mNodeColor; }
			set
			{
				NodePropertyChangeEventArgs eaProperty = null;
				Color original = mNodeColor;

				mNodeColor = value;
				if(original != value)
				{
					eaProperty = new NodePropertyChangeEventArgs();
					eaProperty.Node = this;
					eaProperty.PropertyName = "NodeColor";
					eaProperty.ValueBefore = original;
					eaProperty.ValueAfter = value;
					OnPropertyChanged(this, eaProperty);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeTextColor																													*
		//*-----------------------------------------------------------------------*
		private Color mNodeTextColor = Color.Empty;
		/// <summary>
		/// Get/Set the node text color.
		/// </summary>
		public Color NodeTextColor
		{
			get { return mNodeTextColor; }
			set
			{
				NodePropertyChangeEventArgs eaProperty = null;
				Color original = mNodeTextColor;

				mNodeTextColor = value;
				if(original.ToString() != value.ToString())
				{
					eaProperty = new NodePropertyChangeEventArgs();
					eaProperty.Node = this;
					eaProperty.PropertyName = "NodeTextColor";
					eaProperty.ValueBefore = original;
					eaProperty.ValueAfter = value;
					OnPropertyChanged(this, eaProperty);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeType																															*
		//*-----------------------------------------------------------------------*
		private string mNodeType = "Start";
		/// <summary>
		/// Get/Set the registered node type of this node.
		/// </summary>
		public string NodeType
		{
			get { return mNodeType; }
			set
			{
				NodePropertyChangeEventArgs eaProperty = null;
				string original = mNodeType;

				mNodeType = value;
				if(original != value)
				{
					eaProperty = new NodePropertyChangeEventArgs();
					eaProperty.Node = this;
					eaProperty.PropertyName = "NodeType";
					eaProperty.ValueBefore = original;
					eaProperty.ValueAfter = value;
					OnPropertyChanged(this, eaProperty);
				}
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
		/// Fired when the value of a property has been changed.
		/// </summary>
		public event NodePropertyChangeEventHandler PropertyChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Selected																															*
		//*-----------------------------------------------------------------------*
		private bool mSelected = false;
		/// <summary>
		/// Get/Set a value indicating whether this node is selected.
		/// </summary>
		public bool Selected
		{
			get { return mSelected; }
			set
			{
				NodePropertyChangeEventArgs eaProperty = null;
				bool original = mSelected;

				mSelected = value;
				if(original != value)
				{
					eaProperty = new NodePropertyChangeEventArgs();
					eaProperty.Node = this;
					eaProperty.PropertyName = "Selected";
					eaProperty.ValueBefore = original;
					eaProperty.ValueAfter = value;
					OnPropertyChanged(this, eaProperty);
					OnSelectedChanged(new NodeEventArgs(this));
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SelectedChanged																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the selected property of the node has changed.
		/// </summary>
		public event NodeEventHandler SelectedChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SetProperties																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the properties collection for the indicated node.
		/// </summary>
		/// <param name="node">
		/// Reference to the node that will be receiving the new properties
		/// collection.
		/// </param>
		/// <param name="properties">
		/// Reference to the existing collection of properties to attach to the
		/// node.
		/// </param>
		public static void SetProperties(NodeItem node,
			PropertyCollection properties)
		{
			if(node != null && properties != null)
			{
				UnbindProperties(node);
				node.mProperties = properties;
				BindProperties(node);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SetSockets																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the sockets collection for the specified node.
		/// </summary>
		/// <param name="node">
		/// Reference to the node that will be receiving the new sockets
		/// collection.
		/// </param>
		/// <param name="sockets">
		/// Reference to the existing collection of sockets to attach to the
		/// node.
		/// </param>
		public static void SetSockets(NodeItem node,
			SocketCollection sockets)
		{
			if(node != null && sockets != null)
			{
				UnbindSockets(node);
				//	Transfer the parent reference.
				sockets.Parent = node;
				node.mSockets = sockets;
				BindSockets(node);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SocketAdded																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a socket has been added to the sockets collection.
		/// </summary>
		public event SocketEventHandler SocketAdded;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SocketConnectionAdded																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a connection has been added to a socket in the sockets
		/// collection.
		/// </summary>
		public event SocketConnectionEventHandler SocketConnectionAdded;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SocketConnectionDeleted																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a connection has been deleted from a socket in the sockets
		/// collection.
		/// </summary>
		public event SocketConnectionEventHandler SocketConnectionDeleted;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SocketDeleted																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a socket has been deleted from the sockets collection.
		/// </summary>
		public event SocketEventHandler SocketDeleted;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SocketPropertyChanged																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the property value on a socket in the sockets collection has
		/// been changed.
		/// </summary>
		public event SocketPropertyChangeEventHandler SocketPropertyChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Sockets																																*
		//*-----------------------------------------------------------------------*
		private SocketCollection mSockets = null;
		/// <summary>
		/// Get a reference to the collection of sockets on this node.
		/// </summary>
		public SocketCollection Sockets
		{
			get { return mSockets; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Ticket																																*
		//*-----------------------------------------------------------------------*
		private string mTicket = Guid.NewGuid().ToString("D");
		/// <summary>
		/// Get/Set the globally unique identification of the node in the tree.
		/// </summary>
		public string Ticket
		{
			get { return mTicket; }
			set
			{
				NodePropertyChangeEventArgs eaProperty = null;
				string original = mTicket;

				mTicket = value;
				if(original != value)
				{
					eaProperty = new NodePropertyChangeEventArgs();
					eaProperty.Node = this;
					eaProperty.PropertyName = "Ticket";
					eaProperty.ValueBefore = original;
					eaProperty.ValueAfter = value;
					OnPropertyChanged(this, eaProperty);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	TitleHeight																														*
		//*-----------------------------------------------------------------------*
		private float mTitleHeight = 20f;
		/// <summary>
		/// Get/Set the height of the title of this node.
		/// </summary>
		public float TitleHeight
		{
			get { return mTitleHeight; }
			set
			{
				NodePropertyChangeEventArgs eaProperty = null;
				float original = mTitleHeight;

				mTitleHeight = value;
				if(original != value)
				{
					eaProperty = new NodePropertyChangeEventArgs();
					eaProperty.Node = this;
					eaProperty.PropertyName = "TitleHeight";
					eaProperty.ValueBefore = original;
					eaProperty.ValueAfter = value;
					OnPropertyChanged(this, eaProperty);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	TitleProperty																													*
		//*-----------------------------------------------------------------------*
		private string mTitleProperty = "Ticket";
		/// <summary>
		/// Get/Set the property name to be used as the node title.
		/// </summary>
		public string TitleProperty
		{
			get { return mTitleProperty; }
			set
			{
				NodePropertyChangeEventArgs eaProperty = null;
				string original = mTitleProperty;

				mTitleProperty = value;
				if(original != null)
				{
					eaProperty = new NodePropertyChangeEventArgs();
					eaProperty.Node = this;
					eaProperty.PropertyName = "TitleProperty";
					eaProperty.ValueBefore = original;
					eaProperty.ValueAfter = value;
					OnPropertyChanged(this, eaProperty);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Width																																	*
		//*-----------------------------------------------------------------------*
		private float mWidth = 150f;
		/// <summary>
		/// Get/Set the width of this node.
		/// </summary>
		public float Width
		{
			get { return mWidth; }
			set
			{
				NodePropertyChangeEventArgs eaProperty = null;
				float original = mWidth;

				mWidth = value;
				if(original != value)
				{
					eaProperty = new NodePropertyChangeEventArgs();
					eaProperty.Node = this;
					eaProperty.PropertyName = "Width";
					eaProperty.ValueBefore = original;
					eaProperty.ValueAfter = value;
					OnPropertyChanged(this, eaProperty);
				}
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
				NodePropertyChangeEventArgs eaProperty = null;
				float original = mX;

				mX = value;
				if(original != value)
				{
					eaProperty = new NodePropertyChangeEventArgs();
					eaProperty.Node = this;
					eaProperty.PropertyName = "X";
					eaProperty.ValueBefore = original;
					eaProperty.ValueAfter = value;
					OnPropertyChanged(this, eaProperty);
				}
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
				NodePropertyChangeEventArgs eaProperty = null;
				float original = mY;

				mY = value;
				if(original != value)
				{
					eaProperty = new NodePropertyChangeEventArgs();
					eaProperty.Node = this;
					eaProperty.PropertyName = "Y";
					eaProperty.ValueBefore = original;
					eaProperty.ValueAfter = value;
					OnPropertyChanged(this, eaProperty);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ZOrder																																*
		//*-----------------------------------------------------------------------*
		private int mZOrder = 0;
		/// <summary>
		/// Get/Set the Z-order of this node within the collection.
		/// </summary>
		public int ZOrder
		{
			get { return mZOrder; }
			set
			{
				NodePropertyChangeEventArgs eaProperty = null;
				int original = mZOrder;

				mZOrder = value;
				if(original != value)
				{
					eaProperty = new NodePropertyChangeEventArgs();
					eaProperty.Node = this;
					eaProperty.PropertyName = "ZOrder";
					eaProperty.ValueBefore = original;
					eaProperty.ValueAfter = value;
					OnPropertyChanged(this, eaProperty);
				}
			}
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	NodeEventArgs																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event arguments suitable for passing information about a node.
	/// </summary>
	public class NodeEventArgs : EventArgs
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
		/// Create a new instance of the NodeItemEventArgs Item.
		/// </summary>
		public NodeEventArgs()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the NodeItemEventArgs Item.
		/// </summary>
		/// <param name="node">
		/// Reference to the node receiving the action.
		/// </param>
		/// <param name="information">
		/// Optional information field value.
		/// </param>
		public NodeEventArgs(NodeItem node, string information = "")
		{
			Node = node;
			mInformation = information;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Information																														*
		//*-----------------------------------------------------------------------*
		private string mInformation = "";
		/// <summary>
		/// Get/Set additional parameter information for the event.
		/// </summary>
		public string Information
		{
			get { return mInformation; }
			set { mInformation = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Node																																	*
		//*-----------------------------------------------------------------------*
		private NodeItem mNode = null;
		/// <summary>
		/// Get/Set a reference to the node receiving the action.
		/// </summary>
		public NodeItem Node
		{
			get { return mNode; }
			set
			{
				mNode = value;
				mOriginalNode = new NodeItem(value);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	OriginalNode																													*
		//*-----------------------------------------------------------------------*
		private NodeItem mOriginalNode = null;
		/// <summary>
		/// Get/Set a reference to the memberwise copy of the node in its original
		/// condition.
		/// </summary>
		/// <remarks>
		/// This value is set automatically when assigning the Node property, but
		/// can be overridden via the local set accessor.
		/// </remarks>
		public NodeItem OriginalNode
		{
			get { return mOriginalNode; }
			set
			{
				mOriginalNode = new NodeItem(value);
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//* NodeEventHandler																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// General event handler for node changes.
	/// </summary>
	/// <param name="sender">
	/// The object raising this event.
	/// </param>
	/// <param name="e">
	/// Node event arguments.
	/// </param>
	public delegate void NodeEventHandler(object sender, NodeEventArgs e);
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	NodePropertyChangeEventArgs																							*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Handling for property change events on a node.
	/// </summary>
	public class NodePropertyChangeEventArgs : EventArgs
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
		/// Get/Set the property name.
		/// </summary>
		public string PropertyName
		{
			get { return mPropertyName; }
			set { mPropertyName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ValueAfter																														*
		//*-----------------------------------------------------------------------*
		private object mValueAfter = null;
		/// <summary>
		/// Get/Set the value after the event.
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
		/// Get/Set the value before the event.
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
	//* NodePropertyChangeEventHandler																					*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event handler for events where node properties have been changed.
	/// </summary>
	/// <param name="sender">
	/// The object raising this event.
	/// </param>
	/// <param name="e">
	/// Node property change event arguments.
	/// </param>
	public delegate void NodePropertyChangeEventHandler(object sender,
		NodePropertyChangeEventArgs e);
	//*-------------------------------------------------------------------------*

}
