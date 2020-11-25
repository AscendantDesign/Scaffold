//	PropertyControl.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using CefSharp;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;

using static Scaffold.ScaffoldUtil;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	PropertyControlCollection																								*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of PropertyControlItem Items.
	/// </summary>
	public class PropertyControlCollection : List<PropertyControlItem>
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

		//	TODO: PropertyControl: Possibly add special formatting for properties.
		//*-----------------------------------------------------------------------*
		//*	Enabled																																*
		//*-----------------------------------------------------------------------*
		private bool mEnabled = true;
		/// <summary>
		/// Get/Set a value indicating whether cross-update functionality is
		/// enabled.
		/// </summary>
		public bool Enabled
		{
			get { return mEnabled; }
			set
			{
				//	When changing state, there will potentially be discrepancies
				//	between the values on the properties vs. controls.
				//	It is expected that both sides have been set during page
				//	configuration and those differences are trivial.
				if(mEnabled = false && value == true)
				{
					foreach(PropertyControlItem propertyControl in this)
					{
						propertyControl.ControlChangedFlag = false;
						propertyControl.PropertyChangedFlag = false;
						propertyControl.UpdatingControl = false;
						propertyControl.UpdatingProperty = false;
					}
				}
				mEnabled = value;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PropertiesTableOnRowChanged																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Content of a row has changed on the properties table.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Data row change event arguments.
		/// </param>
		public void PropertiesTableOnRowChanged(object sender,
			DataRowChangeEventArgs e)
		{
			string name = "";
			PropertyControlItem propertyControl = null;
			string value = "";

			if(e.Row.RowState != DataRowState.Deleted &&
				e.Row.RowState != DataRowState.Detached)
			{
				name = e.Row.Field<string>("Name");
				value = e.Row.Field<string>("Value");
				if(name?.Length > 0)
				{
					//	Find an associated property / control.
					propertyControl = this.FirstOrDefault(x =>
						x.PropertyName.ToLower() == name.ToLower());
					if(propertyControl != null &&
						propertyControl.ControlInstance != null &&
						propertyControl.ControlPropertyName?.Length > 0 &&
						!propertyControl.UpdatingControl &&
						!propertyControl.UpdatingProperty)
					{
						propertyControl.PropertyChangedFlag = true;
						UpdateControl(propertyControl.PropertyName);
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PropertiesTableOnTableNewRow																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A new row has been added to the properties table.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Data table new row event arguments.
		/// </param>
		public void PropertiesTableOnTableNewRow(object sender,
			DataTableNewRowEventArgs e)
		{
			string name = e.Row.Field<string>("Name");
			PropertyControlItem propertyControl = null;
			string value = e.Row.Field<string>("Value");

			if(name?.Length > 0)
			{
				//	Find an associated property / control.
				propertyControl = this.FirstOrDefault(x =>
					x.PropertyName.ToLower() == name.ToLower());
				if(propertyControl != null &&
					propertyControl.ControlInstance != null &&
					propertyControl.ControlPropertyName?.Length > 0 &&
					!propertyControl.UpdatingControl &&
					!propertyControl.UpdatingProperty)
				{
					propertyControl.PropertyChangedFlag = true;
					UpdateControl(propertyControl.PropertyName);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PropertiesTable																												*
		//*-----------------------------------------------------------------------*
		private DataTable mPropertiesTable = new DataTable();
		/// <summary>
		/// Get/Set a reference to the table of properties associated with the
		/// loaded controls.
		/// </summary>
		public DataTable PropertiesTable
		{
			get { return mPropertiesTable; }
			set
			{
				if(mPropertiesTable != null)
				{
					mPropertiesTable.RowChanged -= PropertiesTableOnRowChanged;
					//mPropertiesTable.RowDeleted -= PropertiesTableOnRowDeleted;
					//mPropertiesTable.RowDeleting -= PropertiesTableOnRowDeleting;
					mPropertiesTable.TableNewRow -= PropertiesTableOnTableNewRow;
				}
				mPropertiesTable = value;
				if(mPropertiesTable != null)
				{
					mPropertiesTable.RowChanged += PropertiesTableOnRowChanged;
					//mPropertiesTable.RowDeleted += PropertiesTableOnRowDeleted;
					//mPropertiesTable.RowDeleting += PropertiesTableOnRowDeleting;
					mPropertiesTable.TableNewRow += PropertiesTableOnTableNewRow;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UpdateControl																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the control from the associated property value.
		/// </summary>
		/// <param name="propertyName">
		/// Name of the property for which the associated control will be updated.
		/// </param>
		public void UpdateControl(string propertyName)
		{
			PropertyControlItem propertyControl = null;
			DataRow record = null;
			List<DataRow> records = null;

			if(mEnabled)
			{
				if(propertyName?.Length > 0 && mPropertiesTable != null &&
					mPropertiesTable.Rows.Count > 0)
				{
					records = (from x in mPropertiesTable.Rows.OfType<DataRow>()
										 where x.RowState != DataRowState.Deleted &&
										 x.Field<string>("Name").ToLower() ==
										 propertyName.ToLower()
										 select x).ToList();
					if(records.Count > 0)
					{
						record = records[0];
						propertyControl =
							this.FirstOrDefault(x => x.PropertyName.ToLower() ==
							propertyName.ToLower());
						UpdateControl(record, propertyControl);
					}
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Update the control from the associated property value.
		/// </summary>
		/// <param name="record">
		/// Reference to a data row containing the property name and value.
		/// </param>
		/// <param name="propertyControl">
		/// Reference to the property / control record with a reference to the
		/// control to be updated.
		/// </param>
		public void UpdateControl(DataRow record,
			PropertyControlItem propertyControl)
		{
			char[] dot = new char[] { '.' };
			System.Drawing.Font font = null;
			bool fontTextUsed = false;
			int index = 0;
			List<PropertyInfo> objProperties = null;
			PropertyInfo objProperty = null;
			string[] propChain = null;
			object sourceObject = null;
			string sourceType = "string";
			string sourceValue = "";
			string targetType = "";
			object targetValue = null;
			string tlName = "";

			if(record != null && propertyControl != null &&
				propertyControl.ControlInstance != null &&
				propertyControl.ControlPropertyName?.Length > 0)
			{
				//	The source property is present.
				//	The property / control association is active.
				propertyControl.UpdatingProperty = true;
				sourceValue = record.Field<string>("Value");
				tlName = propertyControl.ControlPropertyName.ToLower();
				switch(tlName)
				{
					case "backcolor":
					case "forecolor":
						//	Color type.
						targetType = "color";
						break;
					case "font.name":
					case "font.sizeinpoints":
						//	Font properties.
						//	We need to create a new font from Name and Size.
						if(propertyControl.ControlInstance is Button button)
						{
							font = button.Font;
							fontTextUsed =
								(button.Text == $"{font.Name} {font.SizeInPoints:0}pt");
							if(tlName == "font.name")
							{
								targetValue = sourceValue;
								if(targetValue != null)
								{
									//	Returning value is a string.
									button.Font =
										new System.Drawing.Font(
											(string)targetValue, font.SizeInPoints);
								}
							}
							else if(tlName == "font.sizeinpoints")
							{
								targetValue =
									TypeConverter.Convert(sourceValue, "string", "single");
								if(targetValue != null)
								{
									//	Returning value is a float.
									button.Font =
										new System.Drawing.Font(
											font.Name, (float)targetValue);
								}
							}
							if(fontTextUsed)
							{
								font = button.Font;
								button.Text = $"{font.Name} {font.SizeInPoints:0}pt";
							}
							targetValue = null;
						}
						targetType = "";
						break;
					case "selectedindex":
						//	Matching item in list.
						targetType = "";
						if(propertyControl.ControlInstance is ComboBox comboBox)
						{
							index = 0;
							foreach(object item in comboBox.Items)
							{
								if(item.ToString().ToLower() == sourceValue.ToLower())
								{
									comboBox.SelectedIndex = index;
									break;
								}
								index++;
							}
						}
						else if(propertyControl.ControlInstance is ListBox listBox)
						{
							index = 0;
							foreach(object item in listBox.Items)
							{
								if(item.ToString().ToLower() == sourceValue.ToLower())
								{
									listBox.SelectedIndex = index;
									break;
								}
								index++;
							}
						}
						break;
					case "text":
					default:
						//	String.
						targetType = "string";
						break;
				}
				if(targetType.Length > 0)
				{
					targetValue =
						TypeConverter.Convert(sourceValue, sourceType, targetType);
				}
				if(targetValue != null)
				{
					propChain = propertyControl.ControlPropertyName.Split(dot);
					sourceObject = propertyControl.ControlInstance;
					index = 1;
					foreach(string propLink in propChain)
					{
						objProperties = sourceObject.GetType().GetProperties().ToList();
						objProperty =
							objProperties.FirstOrDefault(x => x.Name == propLink);
						if(objProperty != null)
						{
							//	Destination property found.
							if(index < propChain.Length)
							{
								sourceObject = objProperty.GetValue(sourceObject);
							}
							else
							{
								try
								{
									objProperty.SetValue(sourceObject, targetValue);
								}
								catch
								{
									Debug.WriteLine(
										"Could not write to property " +
										"{propertyControl.ControlPropertyName}.");
								}
							}
						}
						index++;
					}
				}
				propertyControl.UpdatingProperty = false;
				propertyControl.PropertyChangedFlag = false;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UpdateProperty																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the property from the associated control state.
		/// </summary>
		public void UpdateProperty(string propertyName)
		{
			PropertyControlItem propertyControl = null;
			DataRow record = null;
			List<DataRow> records = null;

			if(mEnabled)
			{
				if(propertyName?.Length > 0 && mPropertiesTable != null &&
					mPropertiesTable.Rows.Count > 0)
				{
					records = (from x in mPropertiesTable.Rows.OfType<DataRow>()
										 where x.Field<string>("Name").ToLower() ==
										 propertyName.ToLower()
										 select x).ToList();
					if(records.Count > 0)
					{
						record = records[0];
						propertyControl =
							this.FirstOrDefault(x => x.PropertyName.ToLower() ==
							propertyName.ToLower());
						UpdateProperty(record, propertyControl);
					}
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Update the property from the associated control value.
		/// </summary>
		/// <param name="record">
		/// Reference to a data row containing the property name and value.
		/// </param>
		/// <param name="propertyControl">
		/// Reference to the property / control record with a reference to the
		/// control to be updated.
		/// </param>
		public void UpdateProperty(DataRow record,
			PropertyControlItem propertyControl)
		{
			char[] dot = new char[] { '.' };
			List<PropertyInfo> objProperties = null;
			PropertyInfo objProperty = null;
			string[] propChain = null;
			object sourceObject = null;
			string sourceType = "";
			object sourceValue = "";
			string targetType = "string";
			object targetValue = null;
			string tlName = "";

			if(record != null && propertyControl != null &&
				propertyControl.ControlInstance != null &&
				propertyControl.ControlPropertyName?.Length > 0)
			{
				//	The source property is present.
				//	The property / control association is active.
				propertyControl.UpdatingControl = true;
				propChain = propertyControl.ControlPropertyName.Split(dot);
				sourceObject = propertyControl.ControlInstance;
				foreach(string propLink in propChain)
				{
					objProperties =
						sourceObject.GetType().GetProperties().ToList();
					objProperty = objProperties.FirstOrDefault(x =>
						x.Name == propLink);
					if(objProperty != null)
					{
						//	Source property found.
						sourceValue = objProperty.GetValue(sourceObject);
						sourceObject = sourceValue;
						if(sourceObject == null)
						{
							break;
						}
					}
				}
				tlName = propertyControl.ControlPropertyName.ToLower();
				switch(tlName)
				{
					case "backcolor":
					case "forecolor":
						//	Color type.
						sourceType = "color";
						break;
					case "font.sizeinpoints":
						//	Floating point number.
						sourceType = "double";
						break;
					case "selectedindex":
						sourceType = "";
						if(propertyControl.ControlInstance is ComboBox comboBox)
						{
							if(comboBox.SelectedIndex > -1)
							{
								targetValue =
									comboBox.Items[comboBox.SelectedIndex].ToString();
							}
						}
						else if(propertyControl.ControlInstance is ListBox listBox)
						{
							if(listBox.SelectedIndex > -1)
							{
								targetValue =
									listBox.Items[listBox.SelectedIndex].ToString();
							}
						}
						break;
					case "font.name":
					case "text":
					default:
						//	String.
						sourceType = "string";
						break;
				}
				if(sourceType.Length > 0)
				{
					targetValue =
						TypeConverter.Convert(sourceValue, sourceType, targetType);
				}
				if(targetValue != null)
				{
					record.SetField<string>("Value", targetValue.ToString());
				}
				propertyControl.UpdatingControl = false;
				propertyControl.ControlChangedFlag = false;
			}
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	PropertyControlItem																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about a property name, associated control, and value
	/// translation.
	/// </summary>
	public class PropertyControlItem
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		private bool mEventBackColorRegistered = false;
		private bool mEventLeaveRegistered = false;
		private bool mEventFontRegistered = false;
		private bool mEventForeColorRegistered = false;
		private bool mEventSelectedIndexRegistered = false;
		private bool mEventTextRegistered = false;

		//*-----------------------------------------------------------------------*
		//*	BindEvents																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Bind the control events.
		/// </summary>
		/// <param name="item">
		/// Reference to the property / control item to be bound.
		/// </param>
		private static void BindEvents(PropertyControlItem item)
		{
			if(item.mControlInstance != null)
			{
				//	Connect the events for the current instance.
				if(item.mControlInstance is Button buttonControl)
				{
					switch(item.mControlPropertyName.ToLower())
					{
						case "backcolor":
							//	Button background color.
							buttonControl.BackColorChanged +=
								item.ControlOnBackColorChanged;
							item.mEventBackColorRegistered = true;
							break;
						case "font.sizeinpoints":
							//	Button font size, in points.
							buttonControl.FontChanged +=
								item.ControlOnFontChanged;
							item.mEventFontRegistered = true;
							break;
						case "font.name":
							//	Button font name.
							buttonControl.FontChanged +=
								item.ControlOnFontChanged;
							item.mEventFontRegistered = true;
							break;
						case "forecolor":
							//	Button text color.
							buttonControl.ForeColorChanged +=
								item.ControlOnForeColorChanged;
							item.mEventForeColorRegistered = true;
							break;
					}
				}
				else if(item.mControlInstance is ComboBox comboBox)
				{
					switch(item.mControlPropertyName.ToLower())
					{
						case "selectedindex":
							//	Selected index in the list.
							if(!item.mEventSelectedIndexRegistered)
							{
								comboBox.SelectedIndexChanged +=
									item.ControlOnSelectedIndexChanged;
								item.mEventSelectedIndexRegistered = true;
							}
							if(!item.mEventLeaveRegistered)
							{
								//	Lost focus.
								comboBox.Leave += item.ControlOnLeave;
								item.mEventLeaveRegistered = true;
							}
							break;
					}
				}
				else if(item.mControlInstance is TextBox textBox)
				{
					switch(item.mControlPropertyName.ToLower())
					{
						case "text":
							//	Lost focus.
							textBox.Leave += item.ControlOnLeave;
							item.mEventLeaveRegistered = true;
							//	Text changed.
							textBox.TextChanged += item.ControlOnTextChanged;
							item.mEventTextRegistered = true;
							break;
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UnbindEvents																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Unbind the control events.
		/// </summary>
		/// <param name="item">
		/// Reference to the item to be unbound.
		/// </param>
		private static void UnbindEvents(PropertyControlItem item)
		{
			if(item.mControlInstance != null)
			{
				//	Disconnect the events for the previous instance.
				if(item.mControlInstance is Button buttonControl)
				{
					switch(item.mControlPropertyName.ToLower())
					{
						case "backcolor":
							//	Button background color.
							if(item.mEventBackColorRegistered)
							{
								buttonControl.BackColorChanged -=
									item.ControlOnBackColorChanged;
								item.mEventBackColorRegistered = false;
							}
							break;
						case "font.sizeinpoints":
							//	Button font size, in points.
							if(item.mEventFontRegistered)
							{
								buttonControl.FontChanged -=
									item.ControlOnFontChanged;
								item.mEventFontRegistered = false;
							}
							break;
						case "font.name":
							//	Button font name.
							if(item.mEventFontRegistered)
							{
								buttonControl.FontChanged -=
									item.ControlOnFontChanged;
								item.mEventFontRegistered = false;
							}
							break;
						case "forecolor":
							//	Button text color.
							if(item.mEventForeColorRegistered)
							{
								buttonControl.ForeColorChanged -=
									item.ControlOnForeColorChanged;
								item.mEventForeColorRegistered = false;
							}
							break;
					}
				}
				else if(item.mControlInstance is ComboBox comboBox)
				{
					switch(item.mControlPropertyName.ToLower())
					{
						case "selectedindex":
							//	Selected item in the list.
							if(item.mEventSelectedIndexRegistered)
							{
								comboBox.SelectedIndexChanged -=
									item.ControlOnSelectedIndexChanged;
								item.mEventSelectedIndexRegistered = false;
							}
							//	Lost focus.
							if(item.mEventLeaveRegistered)
							{
								comboBox.Leave -= item.ControlOnLeave;
								item.mEventLeaveRegistered = false;
							}
							break;
					}
				}
				else if(item.mControlInstance is TextBox textBox)
				{
					switch(item.mControlPropertyName.ToLower())
					{
						case "text":
							//	Lost focus.
							if(item.mEventLeaveRegistered)
							{
								textBox.Leave -= item.ControlOnLeave;
								item.mEventLeaveRegistered = false;
							}
							//	Text changed.
							if(item.mEventTextRegistered)
							{
								textBox.TextChanged -= item.ControlOnTextChanged;
								item.mEventTextRegistered = false;
							}
							break;
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
		/// Create a new instance of the PropertyControlItem Item.
		/// </summary>
		public PropertyControlItem()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the PropertyControlItem Item.
		/// </summary>
		/// <param name="parent">
		/// Reference to the parent collection of which this item is a member.
		/// </param>
		public PropertyControlItem(PropertyControlCollection parent)
		{
			mParent = parent;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the PropertyControlItem Item.
		/// </summary>
		/// <param name="propertyName">
		/// Name of the unique property.
		/// </param>
		/// <param name="controlInstance">
		/// Instance of the associated control.
		/// </param>
		/// <param name="controlPropertyName">
		/// Control editing property.
		/// </param>
		/// <param name="controlFormat">
		/// Optional special formatting to apply on control.
		/// </param>
		public PropertyControlItem(PropertyControlCollection parent,
			string propertyName,
			System.Windows.Forms.Control controlInstance,
			string controlPropertyName = "",
			string controlFormat = "")
		{
			//if(controlPropertyName == "SelectedIndex")
			//{
			//	Debug.WriteLine("Break here...");
			//}
			mParent = parent;
			mPropertyName = propertyName;
			mControlInstance = controlInstance;
			mControlPropertyName = controlPropertyName;
			mControlFormat = controlFormat;
			BindEvents(this);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ControlChangedFlag																										*
		//*-----------------------------------------------------------------------*
		private bool mControlChangedFlag = false;
		/// <summary>
		/// Get/Set an indication as to whether the value of the control property
		/// has been changed.
		/// </summary>
		public bool ControlChangedFlag
		{
			get { return mControlChangedFlag; }
			set { mControlChangedFlag = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ControlFormat																													*
		//*-----------------------------------------------------------------------*
		private string mControlFormat = "";
		/// <summary>
		/// Get/Set the formatting to use when reading and writing to the control.
		/// </summary>
		public string ControlFormat
		{
			get { return mControlFormat; }
			set { mControlFormat = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ControlInstance																												*
		//*-----------------------------------------------------------------------*
		private System.Windows.Forms.Control mControlInstance = null;
		/// <summary>
		/// Get/Set a reference to the associated windows forms control.
		/// </summary>
		public System.Windows.Forms.Control ControlInstance
		{
			get { return mControlInstance; }
			set
			{
				UnbindEvents(this);
				mControlInstance = value;
				BindEvents(this);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ControlOnBackColorChanged																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The background color has changed on the control.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		public void ControlOnBackColorChanged(object sender, EventArgs e)
		{
			if(!mUpdatingProperty)
			{
				mControlChangedFlag = true;
				//	Update the property immediately.
				if(mParent != null)
				{
					mParent.UpdateProperty(mPropertyName);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ControlOnFontChanged																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// One or more aspects of the font have changed on the control.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		public void ControlOnFontChanged(object sender, EventArgs e)
		{
			if(!mUpdatingProperty)
			{
				mControlChangedFlag = true;
				//	Update the property immediately.
				if(mParent != null)
				{
					mParent.UpdateProperty(mPropertyName);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ControlOnBackColorChanged																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The background color has changed on the control.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		public void ControlOnForeColorChanged(object sender, EventArgs e)
		{
			if(!mUpdatingProperty)
			{
				mControlChangedFlag = true;
				if(!mControlInstance.Focused)
				{
					//	If there was no focus on the control, then update the property
					//	immediately.
					if(mParent != null)
					{
						mParent.UpdateProperty(mPropertyName);
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ControlOnLeave																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The control has lost user focus.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		public void ControlOnLeave(object sender, EventArgs e)
		{
			if(!mUpdatingProperty)
			{
				if(mControlChangedFlag)
				{
					if(mParent != null)
					{
						//	Update the parent automatically if linked.
						mParent.UpdateProperty(mPropertyName);
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ControlOnSelectedIndexChanged																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The selected index of the associated control has been changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		public void ControlOnSelectedIndexChanged(object sender, EventArgs e)
		{
			if(!mUpdatingProperty)
			{
				mControlChangedFlag = true;
				//	Update the property immediately.
				if(mParent != null)
				{
					mParent.UpdateProperty(mPropertyName);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ControlOnTextChanged																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The control text has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		public void ControlOnTextChanged(object sender, EventArgs e)
		{
			if(!mUpdatingProperty)
			{
				mControlChangedFlag = true;
				if(!mControlInstance.Focused)
				{
					//	If there was no focus on the control, then update the property
					//	immediately.
					if(mParent != null)
					{
						mParent.UpdateProperty(mPropertyName);
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ControlPropertyName																										*
		//*-----------------------------------------------------------------------*
		private string mControlPropertyName = "";
		/// <summary>
		/// Get/Set the name of the property to work with on the designated
		/// control.
		/// </summary>
		public string ControlPropertyName
		{
			get { return mControlPropertyName; }
			set { mControlPropertyName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetPropertyValue																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the property value to be assigned relative to this control.
		/// </summary>
		/// <param name="propertyControl">
		/// Reference to the property / control item to be updated.
		/// </param>
		/// <returns>
		/// The property value that will be displayed from the current bound
		/// value of this control.
		/// </returns>
		public static string GetPropertyValue(PropertyControlItem propertyControl)
		{
			char[] dot = new char[] { '.' };
			List<PropertyInfo> objProperties = null;
			PropertyInfo objProperty = null;
			string[] propChain = null;
			string result = "";
			object sourceObject = null;
			string sourceType = "";
			object sourceValue = "";
			string targetType = "string";
			string tlName = "";

			if(propertyControl != null &&
				propertyControl.ControlInstance != null &&
				propertyControl.ControlPropertyName?.Length > 0)
			{
				//	TODO: Get multi-level reference, such as Font.Name
				//	The source property is present.
				//	The property / control association is active.
				//if(propertyControl.ControlPropertyName.IndexOf(".") > -1)
				//{
				//	Debug.WriteLine("Break here...");
				//}
				propChain = propertyControl.ControlPropertyName.Split(dot);
				sourceObject = propertyControl.ControlInstance;
				foreach(string propLink in propChain)
				{
					objProperties =
						sourceObject.GetType().GetProperties().ToList();
					objProperty = objProperties.FirstOrDefault(x =>
						x.Name == propLink);
					if(objProperty != null)
					{
						//	Source property found.
						sourceValue = objProperty.GetValue(sourceObject);
						sourceObject = sourceValue;
						if(sourceObject == null)
						{
							break;
						}
					}
				}
				tlName = propertyControl.mControlPropertyName.ToLower();
				switch(tlName)
				{
					case "backcolor":
					case "forecolor":
						//	Color type.
						sourceType = "color";
						targetType = "hexcolor";
						break;
					case "font.sizeinpoints":
						//	Floating point number.
						sourceType = "double";
						break;
					case "selectedindex":
						//	Object.
						sourceType = "";
						if(propertyControl.ControlInstance is ComboBox comboBox)
						{
							if(comboBox.SelectedIndex > -1)
							{
								result =
									comboBox.Items[comboBox.SelectedIndex].ToString();
							}
						}
						else if(propertyControl.ControlInstance is ListBox listBox)
						{
							if(listBox.SelectedIndex > -1)
							{
								result =
									listBox.Items[listBox.SelectedIndex].ToString();
							}
						}
						break;
					case "font.name":
						sourceType = "string";
						break;
					case "text":
					default:
						//	String.
						sourceType = "string";
						break;
				}
				if(sourceType.Length > 0)
				{
					result =
						(string)TypeConverter.Convert(sourceValue, sourceType, targetType);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Parent																																*
		//*-----------------------------------------------------------------------*
		private PropertyControlCollection mParent = null;
		/// <summary>
		/// Get/Set a reference to the collection of which this item is a member.
		/// </summary>
		public PropertyControlCollection Parent
		{
			get { return mParent; }
			set { mParent = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PropertyChangedFlag																										*
		//*-----------------------------------------------------------------------*
		private bool mPropertyChangedFlag = false;
		/// <summary>
		/// Get/Set a value indicating whether the property has changed since the
		/// last control update.
		/// </summary>
		public bool PropertyChangedFlag
		{
			get { return mPropertyChangedFlag; }
			set { mPropertyChangedFlag = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PropertyDataType																											*
		//*-----------------------------------------------------------------------*
		private string mPropertyDataType = "";
		/// <summary>
		/// Get/Set the name of the native type handled by the property.
		/// </summary>
		public string PropertyDataType
		{
			get { return mPropertyDataType; }
			set { mPropertyDataType = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PropertyName																													*
		//*-----------------------------------------------------------------------*
		private string mPropertyName = "";
		/// <summary>
		/// Get/Set the name of the property.
		/// </summary>
		public string PropertyName
		{
			get { return mPropertyName; }
			set { mPropertyName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	UpdatingControl																												*
		//*-----------------------------------------------------------------------*
		private bool mUpdatingControl = false;
		/// <summary>
		/// Get/Set a value indicating whether the control is being updated from
		/// this perspective.
		/// </summary>
		public bool UpdatingControl
		{
			get { return mUpdatingControl; }
			set { mUpdatingControl = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	UpdatingProperty																											*
		//*-----------------------------------------------------------------------*
		private bool mUpdatingProperty = false;
		/// <summary>
		/// Get/Set a value indicating whether the property is being updated from
		/// this perspective.
		/// </summary>
		public bool UpdatingProperty
		{
			get { return mUpdatingProperty; }
			set { mUpdatingProperty = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
