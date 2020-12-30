//	PropertyCollection.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	PropertyCollection																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of PropertyItem Items.
	/// </summary>
	public class PropertyCollection : List<PropertyItem>
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnPropertyNameChanged																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the PropertyNameChanged event when the name of a property has
		/// changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Property change event arguments.
		/// </param>
		protected virtual void OnPropertyNameChanged(object sender,
			PropertyChangeEventArgs e)
		{
			PropertyNameChanged?.Invoke(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	OnPropertyRequest																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the PropertyRequest event when the value of a property is being
		/// requested.
		/// </summary>
		protected virtual void OnPropertyRequest(PropertyRequestEventArgs e)
		{
			PropertyRequest?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	OnPropertyUpdate																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the PropertyUpdate event when the value of a property is being
		/// sent back to the host.
		/// </summary>
		protected virtual void OnPropertyUpdate(PropertyRequestEventArgs e)
		{
			PropertyUpdate?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnPropertyValueChanged																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the PropertyValueChanged event when the value of a property has
		/// changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Property change event arguments.
		/// </param>
		protected virtual void OnPropertyValueChanged(object sender,
			PropertyChangeEventArgs e)
		{
			PropertyValueChanged?.Invoke(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Indexer																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a property from the collection by its name.
		/// </summary>
		/// <param name="name">
		/// Name of the property to find.
		/// </param>
		public PropertyItem this[string name]
		{
			get
			{
				PropertyRequestEventArgs e = null;
				PropertyItem result = this.FirstOrDefault(x => x.Name == name);

				if(result == null || !result.Static)
				{
					e = new PropertyRequestEventArgs(name);
					OnPropertyRequest(e);
					if(e.Handled)
					{
						//	Property was retrieved.
						if(result == null)
						{
							result = new PropertyItem(this, e.Name, e.Value)
							{
								Static = e.Static
							};
							if(e.Static)
							{
								//	Store the property if requested.
								this.Add(result);
							}
						}
						else
						{
							PropertyItem.SetValue(result, e.Value);
						}
					}
					else
					{
						//	The property did not belong to the host.
						//	Create and maintain a new static property.
						result = new PropertyItem(this, e.Name, e.Value)
						{
							Static = true
						};
						this.Add(result);
					}
				}
				return result;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Add																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add an existing item to the collection, setting the parent collection
		/// if appropriate.
		/// </summary>
		/// <param name="item">
		/// Reference to the item to add.
		/// </param>
		public new void Add(PropertyItem item)
		{
			if(item != null)
			{
				if(item.Parent == null)
				{
					item.Parent = this;
				}
				base.Add(item);
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Add a new item to the collection by member values.
		/// </summary>
		/// <param name="name">
		/// Name of the property to add.
		/// </param>
		/// <param name="value">
		/// Value of the property to add.
		/// </param>
		/// <returns>
		/// Reference to a property item.
		/// </returns>
		public PropertyItem Add(string name, object value)
		{
			PropertyItem result = new PropertyItem(this);
			result.Name = name;
			result.Value = value;
			result.Static = true;
			this.Add(result);
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PropertyNameChanged																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The name has changed on a property in this collection.
		/// </summary>
		public event PropertyChangeEventHandler PropertyNameChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PropertyRequest																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the value of a property is being requested from the host.
		/// </summary>
		public event PropertyRequestEventHandler PropertyRequest;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PropertyUpdate																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the value of a property is being sent to the host.
		/// </summary>
		public event PropertyRequestEventHandler PropertyUpdate;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PropertyValueChanged																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The value has changed on a property in this collection.
		/// </summary>
		public event PropertyChangeEventHandler PropertyValueChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RaisePropertyNameChanged																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raise the PropertyNameChanged event.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Property change event arguments.
		/// </param>
		public void RaisePropertyNameChanged(object sender,
			PropertyChangeEventArgs e)
		{
			OnPropertyNameChanged(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	RaisePropertyUpdate																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raise the PropertyUpdate event for the specified property name.
		/// </summary>
		public void RaisePropertyUpdate(string name, object value)
		{
			PropertyRequestEventArgs e = new PropertyRequestEventArgs(name, value);
			OnPropertyUpdate(e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RaisePropertyValueChanged																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raise the PropertyValueChanged event.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Property change event arguments.
		/// </param>
		public void RaisePropertyValueChanged(object sender,
			PropertyChangeEventArgs e)
		{
			OnPropertyValueChanged(sender, e);
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	PropertyItem																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Generic property with a name and a generic value.
	/// </summary>
	public class PropertyItem
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		////*-----------------------------------------------------------------------*
		////* OnNameChanged																													*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Raises the NameChanged event when the name of this property has
		///// changed.
		///// </summary>
		///// <param name="e">
		///// Property change event arguments.
		///// </param>
		//protected virtual void OnNameChanged(PropertyChangeEventArgs e)
		//{
		//	NameChanged?.Invoke(this, e);
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* OnValueChanged																												*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Raises the ValueChanged event when the value of this property has
		///// changed.
		///// </summary>
		///// <param name="e">
		///// Property change event arguments.
		///// </param>
		//protected virtual void OnValueChanged(PropertyChangeEventArgs e)
		//{
		//	ValueChanged?.Invoke(this, e);
		//}
		////*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the PropertyItem Item.
		/// </summary>
		public PropertyItem()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the PropertyItem Item.
		/// </summary>
		/// <param name="parent">
		/// Reference to the parent collection of which this item is a member.
		/// </param>
		public PropertyItem(PropertyCollection parent)
		{
			mParent = parent;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the PropertyItem Item as a copy of an
		/// original.
		/// </summary>
		/// <param name="original">
		/// Reference to the original item to copy.
		/// </param>
		public PropertyItem(PropertyItem original)
		{
			if(original != null)
			{
				this.mName = original.mName;
				this.mStatic = original.mStatic;
				this.mValue = original.mValue;
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the PropertyItem Item.
		/// </summary>
		public PropertyItem(PropertyCollection parent,
			string name, object value = null)
		{
			mParent = parent;
			mName = name;
			mValue = value;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Name																																	*
		//*-----------------------------------------------------------------------*
		private string mName = "";
		/// <summary>
		/// Get/Set the name of this item.
		/// </summary>
		public string Name
		{
			get { return mName; }
			set
			{
				PropertyChangeEventArgs eaProperty = new PropertyChangeEventArgs();
				string original = mName;

				mName = value;
				eaProperty.Name = mName;
				eaProperty.Property = this;
				eaProperty.ValueBefore = original;
				eaProperty.ValueAfter = value;
				mParent?.RaisePropertyNameChanged(this, eaProperty);
			}
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* NameChanged																														*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Fired when the name of the property has been changed.
		///// </summary>
		///// <remarks>
		///// When processing this event, the ValueBefore and ValueAfter properties
		///// of the event arguments are set to the previous and current Name
		///// value.
		///// </remarks>
		//public event PropertyChangeEventHandler NameChanged;
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Parent																																*
		//*-----------------------------------------------------------------------*
		private PropertyCollection mParent = null;
		/// <summary>
		/// Get/Set a reference to the collection to which this property belongs.
		/// </summary>
		public PropertyCollection Parent
		{
			get { return mParent; }
			set { mParent = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SetValue																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the value of the item without triggering an event.
		/// </summary>
		public static void SetValue(PropertyItem item, object value)
		{
			if(item != null)
			{
				item.mValue = value;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Static																																*
		//*-----------------------------------------------------------------------*
		private bool mStatic = true;
		/// <summary>
		/// Get/Set a value indicating whether the authoritative value of this
		/// item is located in the property collection.
		/// </summary>
		/// <remarks>
		/// If true, this value is the authoritative reference. Otherwise, the
		/// host contains the authoritative value.
		/// </remarks>
		public bool Static
		{
			get { return mStatic; }
			set { mStatic = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ToString																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the string representation of the value of this item.
		/// </summary>
		public override string ToString()
		{
			string result = "";
			if(mValue != null)
			{
				result = mValue.ToString();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Value																																	*
		//*-----------------------------------------------------------------------*
		private object mValue = null;
		/// <summary>
		/// Get/Set the value of this item.
		/// </summary>
		public object Value
		{
			get { return mValue; }
			set
			{
				PropertyChangeEventArgs eaProperty = null;
				object original = mValue;

				mValue = value;
				if(!mStatic && mParent != null)
				{
					mParent?.RaisePropertyUpdate(mName, mValue);
				}
				else
				{
					if(value != original)
					{
						eaProperty = new PropertyChangeEventArgs();
						eaProperty.Name = mName;
						eaProperty.Property = this;
						eaProperty.ValueBefore = original;
						eaProperty.ValueAfter = mValue;
						mParent?.RaisePropertyValueChanged(this, eaProperty);
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* ValueChanged																													*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Fired when the value of the property has been changed.
		///// </summary>
		//public event PropertyChangeEventHandler ValueChanged;
		////*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//* PropertyChangeEventArgs																									*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event arguments used for tracking changes to the name or value of a
	/// property.
	/// </summary>
	public class PropertyChangeEventArgs : EventArgs
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
		/// Create a new instance of the PropertyChangeEventArgs Item.
		/// </summary>
		public PropertyChangeEventArgs()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the PropertyChangeEventArgs Item.
		/// </summary>
		/// <param name="name">
		/// Name of the property being changed.
		/// </param>
		/// <param name="valueBefore">
		/// Value prior to the change.
		/// </param>
		/// <param name="valueAfter">
		/// Value after the change.
		/// </param>
		/// <param name="property">
		/// Reference to the property object receiving the change.
		/// </param>
		public PropertyChangeEventArgs(string name,
			object valueBefore = null, object valueAfter = null,
			PropertyItem property = null)
		{
			mName = name;
			mValueBefore = valueBefore;
			mValueAfter = valueAfter;
			mProperty = property;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Name																																	*
		//*-----------------------------------------------------------------------*
		private string mName = "";
		/// <summary>
		/// Get/Set the name of this item.
		/// </summary>
		public string Name
		{
			get { return mName; }
			set { mName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Property																															*
		//*-----------------------------------------------------------------------*
		private PropertyItem mProperty = null;
		/// <summary>
		/// Get/Set a reference to the property being changed.
		/// </summary>
		public PropertyItem Property
		{
			get { return mProperty; }
			set { mProperty = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ValueAfter																														*
		//*-----------------------------------------------------------------------*
		private object mValueAfter = null;
		/// <summary>
		/// Get/Set the value of this item after the change.
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
		/// Get/Set the value of this item before the change.
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
	//* PropertyChangeEventHandler																							*
	//*-------------------------------------------------------------------------*
	public delegate void PropertyChangeEventHandler(object sender,
		PropertyChangeEventArgs e);
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	PropertyRequestEventArgs																								*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event arguments used for requesting the value of a property.
	/// </summary>
	public class PropertyRequestEventArgs : EventArgs
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
		/// Create a new instance of the PropertyRequestEventArgs Item.
		/// </summary>
		public PropertyRequestEventArgs()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the PropertyRequestEventArgs Item.
		/// </summary>
		public PropertyRequestEventArgs(string name, object value = null)
		{
			mName = name;
			mValue = value;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Handled																																*
		//*-----------------------------------------------------------------------*
		private bool mHandled = false;
		/// <summary>
		/// Get/Set a value indicating whether the event has been handled.
		/// </summary>
		public bool Handled
		{
			get { return mHandled; }
			set { mHandled = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Name																																	*
		//*-----------------------------------------------------------------------*
		private string mName = "";
		/// <summary>
		/// Get/Set the name of this item.
		/// </summary>
		public string Name
		{
			get { return mName; }
			set { mName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Static																																*
		//*-----------------------------------------------------------------------*
		private bool mStatic = false;
		/// <summary>
		/// Get/Set a value indicating whether this value will be static.
		/// </summary>
		/// <remarks>
		/// If true, a handled value will be stored in the local property
		/// collection. Otherwise, a request for the current value will be made
		/// on each subsequent access.
		/// </remarks>
		public bool Static
		{
			get { return mStatic; }
			set { mStatic = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Value																																	*
		//*-----------------------------------------------------------------------*
		private object mValue = null;
		/// <summary>
		/// Get/Set the value of this item.
		/// </summary>
		public object Value
		{
			get { return mValue; }
			set { mValue = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//* PropertyRequestEventHandler																							*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Handler for property request events.
	/// </summary>
	/// <param name="sender">
	/// The object raising this event.
	/// </param>
	/// <param name="e">
	/// Property request event arguments.
	/// </param>
	public delegate void PropertyRequestEventHandler(object sender,
		PropertyRequestEventArgs e);
	//*-------------------------------------------------------------------------*

}
