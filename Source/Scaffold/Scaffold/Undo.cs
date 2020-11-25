//	Undo.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
	////*-------------------------------------------------------------------------*
	////*	UndoCarton																															*
	////*-------------------------------------------------------------------------*
	///// <summary>
	///// Stack of UndoPack Items.
	///// </summary>
	//public class UndoCarton : Stack<UndoPack>
	//{
	//	//*************************************************************************
	//	//*	Private																																*
	//	//*************************************************************************
	//	//*************************************************************************
	//	//*	Protected																															*
	//	//*************************************************************************
	//	//*-----------------------------------------------------------------------*
	//	//* OnPackPopped																													*
	//	//*-----------------------------------------------------------------------*
	//	/// <summary>
	//	/// Raises the PackPopped event when an undo pack has been popped from the
	//	/// carton.
	//	/// </summary>
	//	/// <param name="e">
	//	/// Undo pack event arguments.
	//	/// </param>
	//	protected virtual void OnPackPopped(UndoPackEventArgs e)
	//	{
	//		PackPopped?.Invoke(this, e);
	//	}
	//	//*-----------------------------------------------------------------------*

	//	//*-----------------------------------------------------------------------*
	//	//* OnPackPushed																													*
	//	//*-----------------------------------------------------------------------*
	//	/// <summary>
	//	/// Raises the PackPushed event when an undo pack has been pushed to the
	//	/// carton.
	//	/// </summary>
	//	/// <param name="e">
	//	/// Undo pack event arguments.
	//	/// </param>
	//	protected virtual void OnPackPushed(UndoPackEventArgs e)
	//	{
	//		PackPushed?.Invoke(this, e);
	//	}
	//	//*-----------------------------------------------------------------------*

	//	//*************************************************************************
	//	//*	Public																																*
	//	//*************************************************************************

	//	public string GetDescription()
	//	{
	//		string result = "";

	//		if(this.Count > 0 &&
	//			this.Peek().Count > 0 && this.Peek().Peek().Count > 0)
	//		{
	//			//	The carton contains a pack, and most recent pack contains at least
	//			//	one stack with at least one item.
	//		}
	//		return result;
	//	}

	//	//*-----------------------------------------------------------------------*
	//	//* PackPopped																														*
	//	//*-----------------------------------------------------------------------*
	//	/// <summary>
	//	/// Fired when an undo pack has been popped from the carton.
	//	/// </summary>
	//	public event UndoPackEventHandler PackPopped;
	//	//*-----------------------------------------------------------------------*

	//	//*-----------------------------------------------------------------------*
	//	//* PackPushed																														*
	//	//*-----------------------------------------------------------------------*
	//	/// <summary>
	//	/// Fired when an undo pack has been pushed into the carton.
	//	/// </summary>
	//	public event UndoPackEventHandler PackPushed;
	//	//*-----------------------------------------------------------------------*

	//}
	////*-------------------------------------------------------------------------*

	////*-------------------------------------------------------------------------*
	////*	UndoCartonEventArgs																											*
	////*-------------------------------------------------------------------------*
	///// <summary>
	///// Event arguments for undo catalog events.
	///// </summary>
	//public class UndoCartonEventArgs : EventArgs
	//{
	//	//*************************************************************************
	//	//*	Private																																*
	//	//*************************************************************************
	//	//*************************************************************************
	//	//*	Protected																															*
	//	//*************************************************************************
	//	//*************************************************************************
	//	//*	Public																																*
	//	//*************************************************************************

	//}
	////*-------------------------------------------------------------------------*

	////*-------------------------------------------------------------------------*
	////* UndoCartonEventHandler																									*
	////*-------------------------------------------------------------------------*
	///// <summary>
	///// Event handler for undo catalogs.
	///// </summary>
	///// <param name="sender">
	///// The object raising this event.
	///// </param>
	///// <param name="e">
	///// Undo pack event arguments.
	///// </param>
	//public delegate void UndoCartonEventHandler(object sender,
	//	UndoCartonEventArgs e);
	////*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	UndoItem																																*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Single action point representing a state that can be recovered.
	/// </summary>
	public class UndoItem
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
		//*	ActionType																														*
		//*-----------------------------------------------------------------------*
		private string mActionType = "";
		/// <summary>
		/// Get/Set the type of action performed on this item.
		/// </summary>
		public string ActionType
		{
			get { return mActionType; }
			set { mActionType = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ItemName																															*
		//*-----------------------------------------------------------------------*
		private string mItemName = "";
		/// <summary>
		/// Get/Set the name or ID of the item receiving the action.
		/// </summary>
		public string ItemName
		{
			get { return mItemName; }
			set { mItemName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ItemType																															*
		//*-----------------------------------------------------------------------*
		private string mItemType = "";
		/// <summary>
		/// Get/Set the formal type name of the item receiving the change.
		/// </summary>
		public string ItemType
		{
			get { return mItemType; }
			set { mItemType = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Properties																														*
		//*-----------------------------------------------------------------------*
		private PropertyCollection mProperties = new PropertyCollection();
		/// <summary>
		/// Get a reference to the collection of property values being maintained
		/// for this action.
		/// </summary>
		public PropertyCollection Properties
		{
			get { return mProperties; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	UndoItemEventArgs																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Arguments for events involving undo items and the undo stack.
	/// </summary>
	public class UndoItemEventArgs : EventArgs
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
		/// Create a new instance of the UndoItemEventArgs Item.
		/// </summary>
		public UndoItemEventArgs()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the UndoItemEventArgs Item.
		/// </summary>
		/// <param name="item">
		/// Reference to the affected undo item.
		/// </param>
		public UndoItemEventArgs(UndoItem item)
		{
			mItem = item;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Item																																	*
		//*-----------------------------------------------------------------------*
		private UndoItem mItem = null;
		/// <summary>
		/// Get/Set a reference to the affected undo item.
		/// </summary>
		public UndoItem Item
		{
			get { return mItem; }
			set { mItem = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//* UndoItemEventHandler																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event handler for undo items.
	/// </summary>
	/// <param name="sender">
	/// The object raising this event.
	/// </param>
	/// <param name="e">
	/// Undo item event arguments.
	/// </param>
	public delegate void UndoItemEventHandler(object sender,
		UndoItemEventArgs e);
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	UndoPack																																*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Stack of UndoStacks.
	/// </summary>
	public class UndoPack : Stack<UndoStack>
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnStackPopped																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the StackPopped event when a stack has been popped from the
		/// pack.
		/// </summary>
		/// <param name="e">
		/// Undo stack event arguments.
		/// </param>
		protected virtual void OnStackPopped(UndoStackEventArgs e)
		{
			StackPopped?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnStackPushed																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the StackPushed event when a stack has been pushed to the pack.
		/// </summary>
		/// <param name="e">
		/// Undo stack event arguments.
		/// </param>
		protected virtual void OnStackPushed(UndoStackEventArgs e)
		{
			StackPushed?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************

		//*-----------------------------------------------------------------------*
		//*	GetDescription																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the most identifiable description of nearest items in the stack.
		/// </summary>
		/// <returns>
		/// A description of the most recent item of the most recent stack, if
		/// found. Otherwise, an empty string.
		/// </returns>
		public string GetDescription()
		{
			bool bFound = false;
			UndoItem item = null;
			List<UndoItem> items = null;
			string result = "";

			foreach(UndoStack stack in this)
			{
				//	Check each stack for items. We need the first populated stack.
				if(stack.Count > 0)
				{
					//	This stack has items.
					items = stack.ToList();
					item = items[items.Count - 1];
					result = $"{item.ItemType} {item.ActionType}";
					bFound = true;
				}
				if(bFound)
				{
					break;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Pop																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Remove and return the stack at the top of the pack.
		/// </summary>
		/// <returns>
		/// Reference to the popped undo stack.
		/// </returns>
		public new UndoStack Pop()
		{
			UndoStackEventArgs eaStack = null;
			UndoStack result = null;

			if(this.Count > 0)
			{
				result = base.Pop();
				eaStack = new UndoStackEventArgs(result);
				OnStackPopped(eaStack);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Push																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Push an existing stack to the pack.
		/// </summary>
		/// <param name="item">
		/// Reference to the undo stack to be pushed.
		/// </param>
		public new void Push(UndoStack stack)
		{
			UndoStackEventArgs eaStack = null;

			if(stack != null)
			{
				base.Push(stack);
				eaStack = new UndoStackEventArgs(stack);
				OnStackPushed(eaStack);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* StackPopped																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a stack has been popped from the pack.
		/// </summary>
		public event UndoStackEventHandler StackPopped;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* StackPushed																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a stack has been pushed to the pack.
		/// </summary>
		public event UndoStackEventHandler StackPushed;
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	UndoPackEventArgs																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event arguments for undo pack events.
	/// </summary>
	public class UndoPackEventArgs : EventArgs
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
		/// Create a new instance of the UndoPackEventArgs Item.
		/// </summary>
		public UndoPackEventArgs()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the UndoPackEventArgs Item.
		/// </summary>
		public UndoPackEventArgs(UndoPack pack)
		{
			mPack = pack;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Pack																																	*
		//*-----------------------------------------------------------------------*
		private UndoPack mPack = null;
		/// <summary>
		/// Get/Set a reference to the affected undo pack.
		/// </summary>
		public UndoPack Pack
		{
			get { return mPack; }
			set { mPack = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//* UndoPackEventHandler																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event handler for undo packs.
	/// </summary>
	/// <param name="sender">
	/// The object raising this event.
	/// </param>
	/// <param name="e">
	/// Undo pack event arguments.
	/// </param>
	public delegate void UndoPackEventHandler(object sender,
		UndoPackEventArgs e);
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	UndoStack																																*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of UndoItem Items.
	/// </summary>
	public class UndoStack : Stack<UndoItem>
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnItemPopped																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the ItemPopped event when an item has been popped from the
		/// stack.
		/// </summary>
		/// <param name="e">
		/// Undo item event arguments.
		/// </param>
		protected virtual void OnItemPopped(UndoItemEventArgs e)
		{
			ItemPopped?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnItemPushed																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the ItemPushed event when an item has been pushed to the stack.
		/// </summary>
		/// <param name="e">
		/// Undo item event arguments.
		/// </param>
		protected virtual void OnItemPushed(UndoItemEventArgs e)
		{
			ItemPushed?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	DateCreated																														*
		//*-----------------------------------------------------------------------*
		private DateTime mDateCreated = DateTime.Now;
		/// <summary>
		/// Get/Set the date and time at which this stack was created.
		/// </summary>
		public DateTime DateCreated
		{
			get { return mDateCreated; }
			set { mDateCreated = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetAge																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the age of this item, in seconds.
		/// </summary>
		public float GetAge()
		{
			TimeSpan duration = DateTime.Now - mDateCreated;
			return (float)duration.TotalSeconds;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Pop																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Remove and return the item at the top of the stack.
		/// </summary>
		/// <returns>
		/// Reference to the popped undo item.
		/// </returns>
		public new UndoItem Pop()
		{
			UndoItemEventArgs eaItem = null;
			UndoItem result = null;

			if(this.Count > 0)
			{
				result = base.Pop();
				eaItem = new UndoItemEventArgs(result);
				OnItemPopped(eaItem);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Push																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Push an existing item to the stack.
		/// </summary>
		/// <param name="item">
		/// Reference to the undo item to be pushed.
		/// </param>
		public new void Push(UndoItem item)
		{
			UndoItemEventArgs eaItem = null;

			if(item != null)
			{
				base.Push(item);
				eaItem = new UndoItemEventArgs(item);
				OnItemPushed(eaItem);
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Push an item to the stack by member values.
		/// </summary>
		/// <param name="itemType">
		/// Formal type name of the item.
		/// </param>
		/// <param name="itemName">
		/// Name or ID of the item to recover.
		/// </param>
		/// <param name="actionType">
		/// Action type to be reversed.
		/// </param>
		public UndoItem Push(string itemType, string itemName, string actionType)
		{
			UndoItemEventArgs eaItem = null;
			UndoItem result = new UndoItem();

			result.ActionType = actionType;
			result.ItemName = itemName;
			result.ItemType = itemType;
			this.Push(result);
			eaItem = new UndoItemEventArgs(result);
			OnItemPushed(eaItem);
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ItemPopped																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when an item is popped from the stack.
		/// </summary>
		public event UndoItemEventHandler ItemPopped;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ItemPushed																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when an item is pushed to the stack.
		/// </summary>
		public event UndoItemEventHandler ItemPushed;
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	UndoStackEventArgs																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event arguments for undo stack events.
	/// </summary>
	public class UndoStackEventArgs : EventArgs
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
		/// Create a new instance of the UndoStackEventArgs Item.
		/// </summary>
		public UndoStackEventArgs()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the UndoStackEventArgs Item.
		/// </summary>
		public UndoStackEventArgs(UndoStack stack)
		{
			mStack = stack;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Stack																																	*
		//*-----------------------------------------------------------------------*
		private UndoStack mStack = null;
		/// <summary>
		/// Get/Set a reference to the affected undo stack.
		/// </summary>
		public UndoStack Stack
		{
			get { return mStack; }
			set { mStack = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//* UndoStackEventHandler																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event handler for undo stacks.
	/// </summary>
	/// <param name="sender">
	/// The object raising this event.
	/// </param>
	/// <param name="e">
	/// Undo stack event arguments.
	/// </param>
	public delegate void UndoStackEventHandler(object sender,
		UndoStackEventArgs e);
	//*-------------------------------------------------------------------------*

}
