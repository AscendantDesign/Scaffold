//	frmSingleAnswer.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Scaffold.ScaffoldUtil;
using static Scaffold.ScaffoldNodesUtil;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	frmSingleAnswer																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Input form for accepting the text of a single answer.
	/// </summary>
	public partial class frmSingleAnswer : Form
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		private bool mActivated = false;
		private PropertyControlCollection mPropertyControls = null;

		//*-----------------------------------------------------------------------*
		//* AddMediaListItem																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add an icon or thumbnail item to the media listview.
		/// </summary>
		/// <param name="ticket">
		/// Globally unique identification of the resource.
		/// </param>
		private async void AddMediaListItem(string ticket)
		{
			int index = 0;
			ListViewItem item = null;
			ResourceItem resource = null;
			Bitmap thumbnail = null;

			if(ticket?.Length > 0)
			{
				resource = mResources.FirstOrDefault(x =>
					x.Ticket.ToLower() == ticket.ToLower());
				if(resource != null)
				{
					switch(resource.ResourceType)
					{
						case "MediaAudio":
							item = new ListViewItem(ResourceItem.Filename(resource), 0);
							item.Tag = resource.Ticket;
							item.Group = lvMedia.Groups["Audio"];
							lvMedia.Items.Add(item);
							break;
						case "MediaImage":
							thumbnail = CreateImageThumbnail(resource, 128, 128);
							index = imageListMedia.Images.Count;
							imageListMedia.Images.Add(thumbnail);
							item = new ListViewItem(ResourceItem.Filename(resource),
								index);
							item.Tag = resource.Ticket;
							item.Group = lvMedia.Groups["Image"];
							lvMedia.Items.Add(item);
							break;
						case "MediaLink":
							item = new ListViewItem(ResourceItem.Filename(resource), 1);
							item.Tag = resource.Ticket;
							item.Group = lvMedia.Groups["Link"];
							lvMedia.Items.Add(item);
							break;
						case "MediaVideo":
							thumbnail = await CreateVideoThumbnail(resource, 128, 128);
							index = imageListMedia.Images.Count;
							imageListMedia.Images.Add(thumbnail);
							item = new ListViewItem(ResourceItem.Filename(resource),
								index);
							item.Tag = resource.Ticket;
							item.Group = lvMedia.Groups["Video"];
							lvMedia.Items.Add(item);
							break;
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	btnCancel_Click																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Cancel button has been clicked.
		/// </summary>
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Hide();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	btnOK_Click																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// OK button has been clicked.
		/// </summary>
		private void btnOK_Click(object sender, EventArgs e)
		{
			if(!btnOK.Focused)
			{
				btnOK.Focus();
			}
			WriteSocket();
			this.DialogResult = DialogResult.OK;
			this.Hide();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnMediaAdd_Click																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Add Media button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnMediaAdd_Click(object sender, EventArgs e)
		{
			frmResourceGallery dialog = new frmResourceGallery();
			ResourceItem resource = null;
			string resourceType = "";
			string ticket = "";

			dialog.LoadResources(mResources);
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				ticket = dialog.GetSelectedTicket();
				if(ticket?.Length > 0)
				{
					resource = mResources.FirstOrDefault(x =>
						x.Ticket.ToLower() == ticket);
					if(resource != null)
					{
						//	A resource is being added or updated.
						resourceType = resource.ResourceType;
						RemoveMediaListItem(resourceType);
						if(MediaExists(mSocketProperties, mResources, resourceType))
						{
							DetachResourceByProperty(mSocketProperties, resourceType);
						}
						AttachResource(mSocketProperties, mResources, ticket);
						AddMediaListItem(ticket);
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnMediaDelete_Click																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Delete Media button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnMediaDelete_Click(object sender, EventArgs e)
		{
			int count = lvMedia.SelectedItems.Count;
			List<ListViewItem> items = new List<ListViewItem>();
			string ticket = "";

			foreach(ListViewItem item in lvMedia.SelectedItems)
			{
				items.Add(item);
			}
			foreach(ListViewItem item in items)
			{
				ticket = ScaffoldUtil.ToString(item.Tag);
				DetachResourceByTicket(mSocketProperties, ticket);
				lvMedia.Items.Remove(item);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnPropertiesAdd_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Add button associated with the properties grid has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnPropertiesAdd_Click(object sender, EventArgs e)
		{
			frmPropertyAssignment form = new frmPropertyAssignment();
			DataRow record = null;
			List<DataRow> records = null;

			CenterOver(this, form);

			mPropertiesTable.AcceptChanges();
			if(form.ShowDialog() == DialogResult.OK)
			{
				records = (from x in mPropertiesTable.Rows.OfType<DataRow>()
									 where x.Field<string>("Name").ToLower() ==
									 form.PropertyName.ToLower()
									 select x).ToList();
				if(records.Count > 0)
				{
					//	Property name must be unique.
					if(MessageBox.Show($"The property [{form.PropertyName}] already " +
						"exists. Do you wish to overwrite the value?",
						"Add Property", MessageBoxButtons.YesNo) == DialogResult.Yes)
					{
						//	Modify existing record.
						record = records[0];
						record.SetField<string>("Value", form.PropertyValue);
					}
				}
				else
				{
					//	Create new record.
					record = mPropertiesTable.NewRow();
					record.SetField<string>("Name", form.PropertyName);
					record.SetField<string>("Value", form.PropertyValue);
					mPropertiesTable.Rows.Add(record);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnPropertiesDelete_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Delete button associated with the properties grid has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnPropertiesDelete_Click(object sender, EventArgs e)
		{
			if(grdProperties.SelectedRows.Count > 0 &&
				grdProperties.SelectedRows[0].Index !=
				grdProperties.Rows.Count - 1)
			{
				if(MessageBox.Show(
					"Delete property " +
					$"[{grdProperties.SelectedRows[0].Cells["Name"].Value}]?",
					"Delete Property", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					grdProperties.Rows.RemoveAt(grdProperties.SelectedRows[0].Index);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnPropertiesEdit_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit button associated with the properties grid has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnPropertiesEdit_Click(object sender, EventArgs e)
		{
			frmPropertyAssignment form = null;
			string name = "";
			string originalName = "";
			DataRow record = null;
			List<DataRow> records = null;

			if(grdProperties.SelectedRows.Count > 0 &&
				grdProperties.SelectedRows[0].Index !=
				grdProperties.Rows.Count - 1)
			{
				form = new frmPropertyAssignment();
				form.StartPosition = FormStartPosition.Manual;
				form.Location = CenterOver(this, form);
				record =
					((DataRowView)grdProperties.SelectedRows[0].DataBoundItem).Row;
				name = record.Field<string>("Name");
				form.PropertyName = name;
				if(mPropertyControls.Exists(x =>
					x.PropertyName.ToLower() == name.ToLower()))
				{
					//	Permanent property can not be renamed.
					form.PropertyNameReadOnly = true;
				}
				originalName = form.PropertyName;
				form.PropertyValue = record.Field<string>("Value");
				if(form.ShowDialog() == DialogResult.OK)
				{
					if(form.PropertyName != originalName)
					{
						//	Check to see if we are going to be overwriting an existing
						//	name.
						records = (from x in mPropertiesTable.Rows.OfType<DataRow>()
											 where x.Field<string>("Name").ToLower() ==
											 form.PropertyName.ToLower()
											 select x).ToList();
						if(records.Count > 0)
						{
							//	Property name must be unique.
							if(MessageBox.Show(
								$"The property [{form.PropertyName}] already " +
								"exists. Do you wish to overwrite the target value?",
								"Edit Property", MessageBoxButtons.YesNo) == DialogResult.Yes)
							{
								//	Delete previous record.
								record.Delete();
								//	Modify existing record.
								record = records[0];
								record.SetField<string>("Value", form.PropertyValue);
							}
						}
						else
						{
							//	No conflict. Rename the property.
							record.SetField<string>("Name", form.PropertyName);
						}
					}
					record.SetField<string>("Value", form.PropertyValue);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnStoryFillColor_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Fill Color button has been clicked on the Story tab.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnStoryFillColor_Click(object sender, EventArgs e)
		{
			frmColorSelect dialog = new frmColorSelect();

			CenterOver(this, dialog);
			dialog.Color = btnStoryFillColor.BackColor;
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				btnStoryFillColor.BackColor = dialog.Color;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnStoryFont_Click																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Font button has been clicked on the Story tab.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnStoryFont_Click(object sender, EventArgs e)
		{
			FontDialog dialog = new FontDialog();

			dialog.Font = btnStoryFont.Font;
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				btnStoryFont.Font = dialog.Font;
				btnStoryFont.Text = $"{dialog.Font.Name} {dialog.Font.SizeInPoints}pt";
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnStoryLineColor_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Line Color button has been clicked on the Story tab.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnStoryLineColor_Click(object sender, EventArgs e)
		{
			frmColorSelect dialog = new frmColorSelect();

			CenterOver(this, dialog);
			dialog.Color = btnStoryLineColor.BackColor;
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				btnStoryLineColor.BackColor = dialog.Color;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnStoryTextColor_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Text Color button has been clicked on the Story tab.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnStoryTextColor_Click(object sender, EventArgs e)
		{
			frmColorSelect dialog = new frmColorSelect();

			CenterOver(this, dialog);
			dialog.Color = btnStoryTextColor.BackColor;
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				btnStoryTextColor.BackColor = dialog.Color;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* grdProperties_CellBeginEdit																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The user is beginning an edit within a cell.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Data grid view cell cancel event arguments.
		/// </param>
		private void grdProperties_CellBeginEdit(object sender,
			DataGridViewCellCancelEventArgs e)
		{
			DataGridViewRow gridRow = null;
			string name = "";
			int nameIndex = 0;
			DataRow record = null;

			//	Assuming columns can be rearranged,
			//	find the index of the Name column.
			nameIndex = 0;
			foreach(DataGridViewColumn gridColumn in grdProperties.Columns)
			{
				if(gridColumn.Name == "Name")
				{
					break;
				}
				nameIndex++;
			}
			if(e.ColumnIndex == nameIndex)
			{
				gridRow = grdProperties.Rows[e.RowIndex];
				if(gridRow.DataBoundItem != null)
				{
					record = ((DataRowView)gridRow.DataBoundItem).Row;
					if(record != null)
					{
						name = record.Field<string>("Name");
						if(name?.Length > 0)
						{
							if(mPropertyControls.Exists(x =>
								x.PropertyName.ToLower() == name.ToLower()))
							{
								//	Permanent property.
								e.Cancel = true;
							}
						}
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* grdProperties_CellDoubleClick																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A property grid cell has been double-clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Data grid view cell event arguments.
		/// </param>
		private void grdProperties_CellDoubleClick(object sender,
			DataGridViewCellEventArgs e)
		{
			DataGridViewRow gridRow = null;

			gridRow = grdProperties.Rows[e.RowIndex];
			if(gridRow.DataBoundItem != null)
			{
				if(btnPropertiesEdit.Enabled)
				{
					btnPropertiesEdit_Click(null, null);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* grdProperties_CellFormatting																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A cell is going to be formatted for display.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Data grid view cell formatting event arguments.
		/// </param>
		private void grdProperties_CellFormatting(object sender,
			DataGridViewCellFormattingEventArgs e)
		{
			DataGridViewRow gridRow = null;
			string name = "";
			int nameIndex = 0;
			DataRow record = null;

			//	Assuming columns can be rearranged,
			//	find the index of the Name column.
			nameIndex = 0;
			foreach(DataGridViewColumn gridColumn in grdProperties.Columns)
			{
				if(gridColumn.Name == "Name")
				{
					break;
				}
				nameIndex++;
			}
			if(e.ColumnIndex == nameIndex)
			{
				gridRow = grdProperties.Rows[e.RowIndex];
				if(gridRow.DataBoundItem != null)
				{
					record = ((DataRowView)gridRow.DataBoundItem).Row;
					if(record != null)
					{
						name = record.Field<string>("Name");
						if(name?.Length > 0)
						{
							if(mPropertyControls.Exists(x =>
								x.PropertyName.ToLower() == name.ToLower()))
							{
								//	Permanent property.
								e.CellStyle.BackColor = SystemColors.Control;
							}
						}
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* grdProperties_DataBindingComplete																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Data binding has been completed on the properties grid.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Data grid view binding complete event arguments.
		/// </param>
		private void grdProperties_DataBindingComplete(object sender,
			DataGridViewBindingCompleteEventArgs e)
		{
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* grdProperties_SelectionChanged																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The current selection has changed on the properties grid.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void grdProperties_SelectionChanged(object sender, EventArgs e)
		{
			DataRow record = null;

			btnPropertiesEdit.Enabled =
				btnPropertiesDelete.Enabled =
				(grdProperties.SelectedRows.Count > 0 &&
				grdProperties.SelectedRows[0].Index !=
				grdProperties.Rows.Count - 1);
			if(btnPropertiesDelete.Enabled)
			{
				record =
					((DataRowView)grdProperties.SelectedRows[0].DataBoundItem).Row;
				if(record != null)
				{
					if(mPropertyControls.Exists(x =>
						x.PropertyName.ToLower() ==
						record.Field<string>("Name").ToLower()))
					{
						//	This property is permanent and shouldn't be deleted.
						btnPropertiesDelete.Enabled = false;
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* grdProperties_UserDeletingRow																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The user will delete a row on the properties grid unless the event is
		/// cancelled.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Data grid view row cancel event arguments.
		/// </param>
		private void grdProperties_UserDeletingRow(object sender,
			DataGridViewRowCancelEventArgs e)
		{
			string name = "";
			DataRow record = ((DataRowView)e.Row.DataBoundItem).Row;

			if(record != null)
			{
				name = record.Field<string>("Name");
				if(mPropertyControls.Exists(x =>
					x.PropertyName.ToLower() == name.ToLower()))
				{
					//	This property is permanent and shouldn't be deleted
					e.Cancel = true;
					MessageBox.Show(
						$"Property [{name}] is permanent and cannot be deleted.",
						"Delete Property");
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* lvMedia_SelectedIndexChanged																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The selected index of the media list view has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void lvMedia_SelectedIndexChanged(object sender, EventArgs e)
		{
			btnMediaDelete.Enabled =
				mnuEditMediaDelete.Enabled =
				(lvMedia.SelectedItems.Count > 0);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditMediaAdd_Click																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Media / Add menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditMediaAdd_Click(object sender, EventArgs e)
		{
			btnMediaAdd_Click(null, null);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditMediaDelete_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Media / Delete Selected menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditMediaDelete_Click(object sender, EventArgs e)
		{
			btnMediaDelete_Click(null, null);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuViewMedia_Click																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The View / Media Page menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuViewMedia_Click(object sender, EventArgs e)
		{
			tctl.SelectedTab = tabMedia;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuViewPropertiesPage_Click																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The View / Properties Page menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuViewPropertiesPage_Click(object sender, EventArgs e)
		{
			tctl.SelectedTab = tabProperties;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuViewSocketPage_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The View / Socket Page menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuViewSocketPage_Click(object sender, EventArgs e)
		{
			tctl.SelectedTab = tabSocket;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuViewStoryboardPage_Click																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The View Storyboard Page menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuViewStoryboardPage_Click(object sender, EventArgs e)
		{
			tctl.SelectedTab = tabStoryboard;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ReadSocket																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read the contents of the caller's property collection to the local
		/// dialog.
		/// </summary>
		private void ReadSocket()
		{
			string fontName = "";
			float fontSize = 0f;
			DataRow row = null;
			object sourceValue = null;
			string text = "";

			mPropertiesTable.Rows.Clear();
			mPropertiesTable.AcceptChanges();

			if(mSocketProperties != null)
			{
				//	Extended story properties.
				text = mSocketProperties["StoryPageNumber"].StringValue();
				if(text.Length > 0)
				{
					txtStoryPageNumber.Text = text;
				}
				text = mSocketProperties["StoryPageWidth"].StringValue();
				if(text.Length > 0)
				{
					txtStoryWidth.Text = text;
				}
				else
				{
					txtStoryWidth.Text = "256";
				}
				text = mSocketProperties["StoryPageX"].StringValue();
				if(text.Length > 0)
				{
					txtStoryPageX.Text = text;
				}
				text = mSocketProperties["StoryPageHorizontalPlacement"].StringValue();
				if(text.Length > 0)
				{
					cmboStoryFromX.SelectedItem = text;
				}
				else
				{
					cmboStoryFromX.SelectedItem = "Right";
				}
				text = mSocketProperties["StoryPageY"].StringValue();
				if(text.Length > 0)
				{
					txtStoryPageY.Text = text;
				}
				text = mSocketProperties["StoryPageVerticalPlacement"].StringValue();
				if(text.Length > 0)
				{
					cmboStoryFromY.SelectedItem = text;
				}
				else
				{
					cmboStoryFromY.SelectedItem = "Top";
				}
				text = mSocketProperties["StoryShapeType"].StringValue();
				if(text.Length > 0)
				{
					cmboStoryboardShapeType.SelectedItem = text;
				}
				text = mSocketProperties["StoryColorFill"].StringValue();
				if(text.Length > 0)
				{
					btnStoryFillColor.BackColor = FromHex(text);
				}
				text = mSocketProperties["StoryColorOutline"].StringValue();
				if(text.Length > 0)
				{
					btnStoryLineColor.BackColor = FromHex(text);
				}
				text = mSocketProperties["StoryColorText"].StringValue();
				if(text.Length > 0)
				{
					btnStoryTextColor.BackColor = FromHex(text);
				}
				fontName = mSocketProperties["StoryFontName"].StringValue();
				text = mSocketProperties["StoryFontSize"].StringValue();
				if(text.Length > 0)
				{
					float.TryParse(text, out fontSize);
				}
				if(fontName.Length > 0 && fontSize > 0f)
				{
					btnStoryFont.Font = new Font(fontName, fontSize);
				}
				//	Read permanent editable properties.
				foreach(PropertyControlItem propertyControl in mPropertyControls)
				{
					row = mPropertiesTable.NewRow();
					row.SetField<string>("Name", propertyControl.PropertyName);
					if(propertyControl.ControlInstance != null)
					{
						//if(propertyControl.ControlPropertyName == "SelectedIndex")
						//{
						//	Debug.WriteLine("Break here...");
						//}
						//	A control is assigned to the property.
						//	Write the control value to the property.
						row.SetField<string>("Value",
							PropertyControlItem.GetPropertyValue(propertyControl));
					}
					else
					{
						sourceValue =
							mSocketProperties[propertyControl.PropertyName].Value;
						row.SetField<string>("Value",
							(string)ScaffoldUtil.TypeConverter.Convert(sourceValue,
							GetRelaxedType(sourceValue), "string"));
					}
					mPropertiesTable.Rows.Add(row);
					row.AcceptChanges();
				}
				//	Read non-permanent node properties.
				foreach(PropertyItem property in mSocketProperties)
				{
					if(property.Static && !CommonMediaTypes.Contains(property.Name))
					{
						if(!mPropertyControls.Exists(x =>
							x.PropertyName.ToLower() == property.Name.ToLower()))
						{
							//	Add any other non-permanent node property.
							row = mPropertiesTable.NewRow();
							row.SetField<string>("Name", property.Name);
							row.SetField<string>("Value",
								property.Value == null ? "" :
								property.Value.ToString());
							mPropertiesTable.Rows.Add(row);
							row.AcceptChanges();
						}
					}
				}
				//	Properties, controls, and styles have been loaded.
				mPropertyControls.Enabled = true;
			}
			AddMediaListItems(lvMedia, imageListMedia,
				mResources, mSocketProperties);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RemoveMediaListItem																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Remove the media list item by media type.
		/// </summary>
		/// <param name="resourceType">
		/// The resource type to remove.
		/// </param>
		private void RemoveMediaListItem(string resourceType)
		{
			int count = 0;
			int index = 0;
			ListViewItem item = null;
			string key = "";

			switch(resourceType)
			{
				case "MediaAudio":
					key = "Audio";
					break;
				case "MediaImage":
					key = "Image";
					break;
				case "MediaLink":
					key = "Link";
					break;
				case "MediaVideo":
					key = "Video";
					break;
			}
			for(index = 0; index < count; index++)
			{
				item = lvMedia.Items[index];
				if(item.Group != null && item.Group.Name == key)
				{
					lvMedia.Items.Remove(item);
					break;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	WriteSocket																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the properties of the caller's node to reflect changes on this
		/// dialog.
		/// </summary>
		private void WriteSocket()
		{
			int count = 0;
			int index = 0;
			string name = "";
			PropertyItem property = null;

			mPropertiesTable.AcceptChanges();
			if(mSocketProperties != null)
			{
				//	Property collection is present.
				//	Extended story properties.
				mSocketProperties["StoryPageNumber"].Value = txtStoryPageNumber.Text;
				mSocketProperties["StoryPageWidth"].Value = txtStoryWidth.Text;
				mSocketProperties["StoryPageX"].Value = txtStoryPageX.Text;
				mSocketProperties["StoryPageHorizontalPlacement"].Value =
					cmboStoryFromX.SelectedItem;
				mSocketProperties["StoryPageY"].Value = txtStoryPageY.Text;
				mSocketProperties["StoryPageVerticalPlacement"].Value =
					cmboStoryFromY.SelectedItem;
				mSocketProperties["StoryShapeType"].Value =
					cmboStoryboardShapeType.SelectedItem;
				mSocketProperties["StoryColorFill"].Value =
					ToHex(btnStoryFillColor.BackColor);
				mSocketProperties["StoryColorOutline"].Value =
					ToHex(btnStoryLineColor.BackColor);
				mSocketProperties["StoryColorText"].Value =
					ToHex(btnStoryTextColor.BackColor);
				mSocketProperties["StoryFontName"].Value = btnStoryFont.Font.Name;
				mSocketProperties["StoryFontSize"].Value =
					btnStoryFont.Font.SizeInPoints;
				//	Delete properties for deleted rows.
				count = mSocketProperties.Count;
				for(index = 0; index < count; index ++)
				{
					property = mSocketProperties[index];
					if(property.Static && !CommonMediaTypes.Contains(property.Name))
					{
						//	Actual property.
						if(mPropertiesTable.Select($"Name='{property.Name}'", "",
							DataViewRowState.Added |
							DataViewRowState.CurrentRows |
							DataViewRowState.ModifiedCurrent |
							DataViewRowState.ModifiedOriginal |
							DataViewRowState.OriginalRows |
							DataViewRowState.Unchanged).Length == 0)
						{
							//	The property has been deleted from the table.
							mSocketProperties.Remove(property);
							index--;
							count--;
						}
					}
				}
				//	Add and update properties.
				//	All remaining properties, permanent and non-permanent.
				foreach(DataRow row in mPropertiesTable.Rows)
				{
					name = row.Field<string>("Name");
					if(!mPropertyControls.Exists(x =>
						x.PropertyName.ToLower() == name.ToLower() &&
						x.ControlInstance != null))
					{
						//	Convert to implied type so the value will be stored unquoted,
						//	if applicable.
						if(mSocketProperties[name] != null)
						{
							//	Update existing property.
							mSocketProperties[name].Value =
								ScaffoldUtil.TypeConverter.Convert(
								row.Field<string>("Value"),
								"string", GetRelaxedType(mSocketProperties[name].Value));
						}
						else
						{
							//	Create new property.
							mSocketProperties[name].Value =
								ToImpliedType(row.Field<string>("Value"));
						}
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnActivated																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the Activated event when the form has been displayed.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			if(!mActivated)
			{
				txtAnswer.Focus();
				mActivated = true;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnLoad																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the Load event when the form has loaded and is ready to display
		/// for the first time.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			cmboStoryboardShapeType.SelectedIndex = 0;
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the frmSingleAnswer Item.
		/// </summary>
		public frmSingleAnswer()
		{
			PropertyControlItem[] propertyControlItems = null;

			InitializeComponent();
			//	Node properties.
			mPropertiesTable.Columns.Add("Name", typeof(string));
			mPropertiesTable.Columns.Add("Value", typeof(string));
			grdProperties.DataSource = mPropertiesTable;
			//	Bound property / control associations.
			mPropertyControls = new PropertyControlCollection();
			propertyControlItems = new PropertyControlItem[]
			{
				//	Built-in properties.
				new PropertyControlItem(mPropertyControls, "Ticket", null),
				new PropertyControlItem(mPropertyControls, "TitleProperty", null),
				new PropertyControlItem(mPropertyControls, "SocketMode", null),
				new PropertyControlItem(mPropertyControls, "X", null),
				new PropertyControlItem(mPropertyControls, "Y", null),
				new PropertyControlItem(mPropertyControls, "Width", null),
				new PropertyControlItem(mPropertyControls, "Height", null),
				new PropertyControlItem(mPropertyControls, "TextX", null),
				new PropertyControlItem(mPropertyControls, "TextY", null),
				new PropertyControlItem(mPropertyControls, "TextWidth", null),
				new PropertyControlItem(mPropertyControls, "TextHeight", null),
				//	Base properties.
				new PropertyControlItem(mPropertyControls, "Index",
					txtIndex, "Text"),
				new PropertyControlItem(mPropertyControls, "Answer",
					txtAnswer, "Text"),
				//	Extended story properties.
				new PropertyControlItem(mPropertyControls, "StoryPageNumber",
					txtStoryPageNumber, "Text"),
				new PropertyControlItem(mPropertyControls, "StoryPageWidth",
					txtStoryWidth, "Text"),
				new PropertyControlItem(mPropertyControls, "StoryPageX",
					txtStoryPageX, "Text"),
				new PropertyControlItem(mPropertyControls,
					"StoryPageHorizontalPlacement",
					cmboStoryFromX, "SelectedIndex"),
				new PropertyControlItem(mPropertyControls, "StoryPageY",
					txtStoryPageY, "Text"),
				new PropertyControlItem(mPropertyControls,
				"StoryPageVerticalPlacement",
				cmboStoryFromY, "SelectedIndex"),
				new PropertyControlItem(mPropertyControls, "StoryShapeType",
					cmboStoryboardShapeType, "SelectedIndex"),
				new PropertyControlItem(mPropertyControls, "StoryColorFill",
					btnStoryFillColor, "BackColor"),
				new PropertyControlItem(mPropertyControls, "StoryColorOutline",
					btnStoryLineColor, "BackColor"),
				new PropertyControlItem(mPropertyControls, "StoryColorText",
					btnStoryTextColor, "BackColor"),
				new PropertyControlItem(mPropertyControls, "StoryFontName",
					btnStoryFont, "Font.Name"),
				new PropertyControlItem(mPropertyControls, "StoryFontSize",
					btnStoryFont, "Font.SizeInPoints")
			};
			mPropertyControls.AddRange(propertyControlItems);
			mPropertyControls.Enabled = false;
			mPropertyControls.PropertiesTable = mPropertiesTable;

			//	Media page.
			btnMediaDelete.Enabled =
				mnuEditMediaDelete.Enabled = false;

			lvMedia.Groups.Add(new ListViewGroup("Audio", "Audio"));
			lvMedia.Groups.Add(new ListViewGroup("Image", "Image"));
			lvMedia.Groups.Add(new ListViewGroup("Link", "Link"));
			lvMedia.Groups.Add(new ListViewGroup("Video", "Video"));

			imageListMedia.Images.Add(ResourceMain.Audio128);
			imageListMedia.Images.Add(ResourceMain.Link128);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Answer																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the answer text.
		/// </summary>
		public string Answer
		{
			get { return txtAnswer.Text; }
			set { txtAnswer.Text = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Index																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the index of this answer.
		/// </summary>
		public string Index
		{
			get { return txtIndex.Text; }
			set { txtIndex.Text = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PropertiesTable																												*
		//*-----------------------------------------------------------------------*
		private DataTable mPropertiesTable = new DataTable("Properties");
		/// <summary>
		/// Get a reference to the table of properties.
		/// </summary>
		public DataTable PropertiesTable
		{
			get { return mPropertiesTable; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Resources																															*
		//*-----------------------------------------------------------------------*
		private ResourceCollection mResources = null;
		/// <summary>
		/// Get a reference to the active collection of resources.
		/// </summary>
		public ResourceCollection Resources
		{
			get { return mResources; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SetSocketProperties																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set a reference to the socket properties and associated resources being
		/// used in this session.
		/// </summary>
		/// <param name="properties">
		/// Reference to the property collection being edited.
		/// </param>
		/// <param name="resources">
		/// Reference to the collection of resources available to this socket.
		/// </param>
		public void SetSocketProperties(PropertyCollection properties,
			ResourceCollection resources)
		{
			mSocketProperties = properties;
			mResources = resources;
			ReadSocket();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SocketProperties																											*
		//*-----------------------------------------------------------------------*
		private PropertyCollection mSocketProperties = new PropertyCollection();
		/// <summary>
		/// Get a reference to the properties collection associated with this
		/// answer.
		/// </summary>
		public PropertyCollection SocketProperties
		{
			get { return mSocketProperties; }
			//set
			//{
			//	mSocketProperties = value;
			//	ReadSocket();
			//}
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

}
