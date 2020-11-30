//	frmDecisionNode.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using Microsoft.Office.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebSockets;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

using static Scaffold.ScaffoldUtil;
using static Scaffold.ScaffoldNodesUtil;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	frmDecisionNode																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Node property editing dialog for question / answer decision nodes.
	/// </summary>
	/// <remarks>
	/// <para>Extended Properties:</para>
	/// <list type="bullet">
	/// <item>StoryPageNumber / txtStoryPageNumber. The page index at which to
	/// place this node.</item>
	/// <item>StoryPageX / txtStoryPageX. The X coordinate upon which to place
	/// the node text.</item>
	/// <item>StoryPageY / txtStoryPageY. The Y coordinate upon which to place
	/// the node text.</item>
	/// <item>StoryShapeType / cmboStoryboardShapeType. The type of shape in
	/// which to place the text.</item>
	/// <item>StoryColorFill / btnStoryFillColor. The fill color of the
	/// shape.</item>
	/// <item>StoryColorOutline / btnStoryLineColor. The outline color of the
	/// shape.</item>
	/// <item>StoryColorText / btnStoryTextColor. The text color on the
	/// shape.</item>
	/// <item>StoryFontName / btnStoryFont.Font. The name of the font on the
	/// shape.</item>
	/// <item>StoryFontSize / btnStoryFont.Font. The size of the font on the
	/// shape.</item>
	/// </list>
	/// </remarks>
	public partial class frmDecisionNode : Form
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		private bool mActivated = false;
		//private bool mOptPerspectiveBusy = false;
		private PropertyControlCollection mPropertyControls = null;

		//*-----------------------------------------------------------------------*
		//* AddMediaQuestionListItem																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add an icon or thumbnail item to the question media listview.
		/// </summary>
		/// <param name="ticket">
		/// Globally unique identification of the resource.
		/// </param>
		private async void AddMediaQuestionListItem(string ticket)
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
							item.Text = GetFilename(resource.AbsoluteFilename);
							item.Tag = resource.Ticket;
							item.Group = lvMediaQuestion.Groups["Audio"];
							lvMediaQuestion.Items.Add(item);
							break;
						case "MediaImage":
							thumbnail = CreateImageThumbnail(resource, 128, 128);
							index = imageListMediaQuestion.Images.Count;
							imageListMediaQuestion.Images.Add(thumbnail);
							item = new ListViewItem(ResourceItem.Filename(resource),
								index);
							item.Tag = resource.Ticket;
							item.Group = lvMediaQuestion.Groups["Image"];
							lvMediaQuestion.Items.Add(item);
							break;
						case "MediaLink":
							item = new ListViewItem(ResourceItem.Filename(resource), 1);
							item.Text = resource.Uri;
							item.Tag = resource.Ticket;
							item.Group = lvMediaQuestion.Groups["Link"];
							lvMediaQuestion.Items.Add(item);
							break;
						case "MediaVideo":
							thumbnail = await CreateVideoThumbnail(resource, 128, 128);
							index = imageListMediaQuestion.Images.Count;
							imageListMediaQuestion.Images.Add(thumbnail);
							item = new ListViewItem(ResourceItem.Filename(resource),
								index);
							item.Tag = resource.Ticket;
							item.Group = lvMediaQuestion.Groups["Video"];
							lvMediaQuestion.Items.Add(item);
							break;
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* AddMediaResponseListItem																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add an icon or thumbnail item to the response media listview.
		/// </summary>
		/// <param name="ticket">
		/// Globally unique identification of the resource.
		/// </param>
		private async void AddMediaResponseListItem(string ticket)
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
							item.Text = GetFilename(resource.AbsoluteFilename);
							item.Tag = resource.Ticket;
							item.Group = lvMediaResponse.Groups["Audio"];
							lvMediaResponse.Items.Add(item);
							break;
						case "MediaImage":
							thumbnail = CreateImageThumbnail(resource, 128, 128);
							index = imageListMediaResponse.Images.Count;
							imageListMediaResponse.Images.Add(thumbnail);
							item = new ListViewItem(ResourceItem.Filename(resource),
								index);
							item.Tag = resource.Ticket;
							item.Group = lvMediaResponse.Groups["Image"];
							lvMediaResponse.Items.Add(item);
							break;
						case "MediaLink":
							item = new ListViewItem(ResourceItem.Filename(resource), 1);
							item.Text = resource.Uri;
							item.Tag = resource.Ticket;
							item.Group = lvMediaResponse.Groups["Link"];
							lvMediaResponse.Items.Add(item);
							break;
						case "MediaVideo":
							thumbnail = await CreateVideoThumbnail(resource, 128, 128);
							index = imageListMediaResponse.Images.Count;
							imageListMediaResponse.Images.Add(thumbnail);
							item = new ListViewItem(ResourceItem.Filename(resource),
								index);
							item.Tag = resource.Ticket;
							item.Group = lvMediaResponse.Groups["Video"];
							lvMediaResponse.Items.Add(item);
							break;
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnAnswerAdd_Click																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add an answer to the list.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnAnswerAdd_Click(object sender, EventArgs e)
		{
			byte[] ascii = null;
			frmSingleAnswer form = new frmSingleAnswer();
			string max = "";
			DataRow record = null;

			CenterOver(this, form);

			mAnswerTable.AcceptChanges();
			//	Get the enumeration.
			foreach(DataRow row in mAnswerTable.Rows)
			{
				if(max == null || max.Length == 0 ||
					String.Compare(row.Field<string>("Index"), max) > 0)
				{
					max = row.Field<string>("Index");
				}
			}
			if(max?.Length > 0)
			{
				ascii = Encoding.ASCII.GetBytes(max.ToCharArray());
				if(ascii.Length > 0)
				{
					if(ascii[0] >= 48 && ascii[0] <= 57)
					{
						//	Numeric.
						max = (Convert.ToInt32(max) + 1).ToString();
					}
					else if(ascii[0] >= 65 && ascii[0] <= 90)
					{
						//	Letters.
						max = Convert.ToChar(ascii[0] + 1).ToString();
					}
				}
			}
			else
			{
				max = "A";
			}
			form.Index = max;
			SocketItem socket = new SocketItem();
			record = mAnswerTable.NewRow();
			record.SetField<string>("Ticket", "");
			record.SetField<string>("Index", max);
			record.SetField<string>("Answer", "");
			record.SetField<PropertyCollection>("Properties",
				socket.Properties);
			form.SetSocketProperties(socket);
			if(form.ShowDialog() == DialogResult.OK)
			{
				//record = mAnswerTable.NewRow();
				record.SetField<string>("Ticket", "");
				record.SetField<string>("Index", form.Index);
				record.SetField<string>("Answer", form.Answer);
				record.SetField<PropertyCollection>("Properties",
					form.SocketProperties);
				mAnswerTable.Rows.Add(record);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnAnswerDelete_Click																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The delete answer button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnAnswerDelete_Click(object sender, EventArgs e)
		{
			if(grdAnswer.SelectedRows.Count > 0 &&
				grdAnswer.SelectedRows[0].Index !=
				grdAnswer.Rows.Count - 1)
			{
				grdAnswer.Rows.RemoveAt(grdAnswer.SelectedRows[0].Index);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnAnswerEdit_Click																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Edit the selected answer.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnAnswerEdit_Click(object sender, EventArgs e)
		{
			frmSingleAnswer form = null;
			PropertyCollection properties = null;
			DataRow record = null;
			SocketItem socket = null;
			string ticket = "";

			if(grdAnswer.SelectedRows.Count > 0 &&
				grdAnswer.SelectedRows[0].Index !=
				grdAnswer.Rows.Count - 1)
			{
				form = new frmSingleAnswer();
				form.StartPosition = FormStartPosition.Manual;
				form.Location = CenterOver(this, form);
				record = ((DataRowView)grdAnswer.SelectedRows[0].DataBoundItem).Row;
				form.Index = record.Field<string>("Index");
				form.Answer = record.Field<string>("Answer");
				ticket = record.Field<string>("Ticket");
				if(ticket?.Length > 0)
				{
					socket = mNode.Sockets.FirstOrDefault(s => s.Ticket == ticket);
				}
				if(socket == null)
				{
					//	The record might be new. Handle all properties through
					//	a socket for type awareness.
					socket = new SocketItem();
					properties = record.Field<PropertyCollection>("Properties");
					SocketItem.SetProperties(socket, properties);
				}
				form.SetSocketProperties(socket);
				if(form.ShowDialog() == DialogResult.OK)
				{
					record.SetField<string>("Index", form.Index);
					record.SetField<string>("Answer", form.Answer);
					grdAnswer.Refresh();
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnCancel_Click																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Cancel button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Hide();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnMediaAddQuestion_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Add Question Media button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnMediaAddQuestion_Click(object sender, EventArgs e)
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
						RemoveMediaQuestionListItem(resourceType);
						if(MediaExists(mNode, resourceType))
						{
							DetachResourceByProperty(mNode, resourceType);
						}
						AttachResource(mNode, ticket);
						AddMediaQuestionListItem(ticket);
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnMediaAddResponse_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Add Response Media button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnMediaAddResponse_Click(object sender, EventArgs e)
		{
			frmResourceGallery dialog = new frmResourceGallery();
			ResourceItem resource = null;
			string resourceType = "";
			SocketItem socket = null;
			string ticket = "";

			socket = mNode.Sockets.FirstOrDefault(x =>
				x.SocketMode == SocketModeEnum.Input);
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
						RemoveMediaResponseListItem(resourceType);
						if(socket != null)
						{
							if(MediaExists(socket, resourceType))
							{
								DetachResourceByProperty(socket, resourceType);
							}
							AttachResource(socket, ticket);
						}
						AddMediaResponseListItem(ticket);
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnMediaDeleteQuestion_Click																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Delete Question Media button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnMediaDeleteQuestion_Click(object sender, EventArgs e)
		{
			int count = lvMediaQuestion.SelectedItems.Count;
			List<ListViewItem> items = new List<ListViewItem>();
			string ticket = "";

			foreach(ListViewItem item in lvMediaQuestion.SelectedItems)
			{
				items.Add(item);
			}
			foreach(ListViewItem item in items)
			{
				ticket = ScaffoldNodesUtil.ToString(item.Tag);
				DetachResourceByTicket(mNode, ticket);
				lvMediaQuestion.Items.Remove(item);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnMediaDeleteResponse_Click																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Delete Response Media button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnMediaDeleteResponse_Click(object sender, EventArgs e)
		{
			int count = lvMediaResponse.SelectedItems.Count;
			List<ListViewItem> items = new List<ListViewItem>();
			SocketItem socket = null;
			string ticket = "";

			socket = mNode.Sockets.FirstOrDefault(x =>
				x.SocketMode == SocketModeEnum.Input);

			foreach(ListViewItem item in lvMediaResponse.SelectedItems)
			{
				items.Add(item);
			}
			foreach(ListViewItem item in items)
			{
				ticket = ScaffoldNodesUtil.ToString(item.Tag);
				DetachResourceByTicket(socket, ticket);
				lvMediaResponse.Items.Remove(item);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnOK_Click																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// OK button was clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnOK_Click(object sender, EventArgs e)
		{
			btnOK.Focus();
			WriteNode();
			this.DialogResult = DialogResult.OK;
			this.Hide();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnPropertiesAdd_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Add Property button has been clicked.
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
		/// The Delete Property button has been clicked.
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
		/// The Edit Property button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnPropertiesEdit_Click(object sender, EventArgs e)
		{
			bool bColor = false;
			bool bFound = false;
			string name = "";
			DataRow record = null;

			if(grdProperties.SelectedRows.Count > 0 &&
				grdProperties.SelectedRows[0].Index !=
				grdProperties.Rows.Count - 1)
			{
				record =
					((DataRowView)grdProperties.SelectedRows[0].DataBoundItem).Row;
				name = record.Field<string>("Name").ToLower();
				switch(name)
				{
					case "height":
					case "nodetype":
					case "question":
					case "storyfontname":
					case "storyfontsize":
					case "storypagehorizontalplacement":
					case "storypagenumber":
					case "storypageverticalplacement":
					case "storypagewidth":
					case "storypagex":
					case "storypagey":
					case "storyshapetype":
					case "ticket":
					case "titleheight":
					case "titleproperty":
					case "width":
					case "x":
					case "y":
					case "zorder":
						bFound = true;
						bColor = false;
						break;
					case "nodecolor":
					case "nodetextcolor":
					case "storycolorfill":
					case "storycoloroutline":
					case "storycolortext":
						bFound = true;
						bColor = true;
						break;
				}
				if(!bFound)
				{
					if(name.IndexOf("color") > -1)
					{
						bFound = true;
						bColor = true;
					}
				}
				if(!bFound)
				{
					if(record.Field<string>("Value").StartsWith("#"))
					{
						bFound = true;
						bColor = true;
					}
				}
				if(bColor)
				{
					PropertiesEditColor(record);
				}
				else
				{
					PropertiesEditValue(record);
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
		//* cmboType_SelectedIndexChanged																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Selected index on the type drop-down has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void cmboType_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch(cmboType.SelectedItem)
			{
				case "Start":
					InputSocketEnabled = false;
					OutputSocketsEnabled = true;
					lblDelay.Visible = false;
					txtDelay.Visible = false;
					lblDelaySec.Visible = false;
					break;
				case "Fork":
					InputSocketEnabled = true;
					OutputSocketsEnabled = true;
					lblDelay.Visible = false;
					txtDelay.Visible = false;
					lblDelaySec.Visible = false;
					break;
				case "Delay":
					InputSocketEnabled = true;
					OutputSocketsEnabled = true;
					lblDelay.Visible = true;
					txtDelay.Visible = true;
					lblDelaySec.Visible = true;
					break;
				case "Termination":
					InputSocketEnabled = true;
					OutputSocketsEnabled = false;
					lblDelay.Visible = false;
					txtDelay.Visible = false;
					lblDelaySec.Visible = false;
					break;
			}
			lblAnswers.Visible =
				grdAnswer.Visible =
				btnAnswerAdd.Visible =
				btnAnswerEdit.Visible =
				btnAnswerDelete.Visible =
				(cmboType.SelectedItem.ToString() != "Termination");
			lblQuestion.Text =
				(cmboType.SelectedItem.ToString() != "Termination" ?
				"&Question:" : "&Statement:");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* grdAnswer_DataBindingComplete																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Data binding of the table to the grid has been completed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Data grid view binding complete event arguments.
		/// </param>
		private void grdAnswer_DataBindingComplete(object sender,
			DataGridViewBindingCompleteEventArgs e)
		{
			grdAnswer.Columns["Ticket"].Visible = false;
			try
			{
				if(grdAnswer.Columns.Contains("Properties"))
				{
					grdAnswer.Columns["Properties"].Visible = false;
				}
			}
			catch { }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* grdAnswer_CellDoubleClick																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// An answers grid cell has been double-clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Data grid view cell event arguments.
		/// </param>
		private void grdAnswer_CellDoubleClick(object sender,
			DataGridViewCellEventArgs e)
		{
			grdAnswer.EndEdit();
			if(btnAnswerEdit.Enabled)
			{
				btnAnswerEdit_Click(null, null);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* grdAnswer_SelectionChanged																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Selection has changed on the grid.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void grdAnswer_SelectionChanged(object sender, EventArgs e)
		{
			btnAnswerEdit.Enabled =
				btnAnswerDelete.Enabled =
				(grdAnswer.SelectedRows.Count > 0 &&
				grdAnswer.SelectedRows[0].Index !=
				grdAnswer.Rows.Count - 1);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* grdProperties_CellBeginEdit																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// In-cell editing is going to take place on a specific cell unless
		/// cancelled.
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
		/// User has double-clicked in a cell.
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

			grdProperties.EndEdit();
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
		/// Cell formatting is taking place on an individual cell in the properties
		/// grid.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		///	Data grid view cell formatting event arguments.
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
		/// The active selection has changed on the properties grid.
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
		/// User is deleting a row in the properties grid.
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
				else
				{
					//e.Cancel = true;
					//record.Table.Rows.Remove(record);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* LoadMediaLists																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Load both media lists.
		/// </summary>
		private void LoadMediaLists()
		{
			SocketItem socket = null;

			//	Clear both lists.
			lvMediaQuestion.Items.Clear();
			lvMediaResponse.Items.Clear();
			//	Prune the images.
			while(imageListMediaQuestion.Images.Count > 2)
			{
				imageListMediaQuestion.Images.RemoveAt(
					imageListMediaQuestion.Images.Count - 1);
			}
			while(imageListMediaResponse.Images.Count > 2)
			{
				imageListMediaResponse.Images.RemoveAt(
					imageListMediaResponse.Images.Count - 1);
			}

			if(mNode != null && mResources != null)
			{
				//	Question list.
				AddMediaListItems(
					lvMediaQuestion, imageListMediaQuestion,
					mResources, mNode.Properties);
				//	Response list.
				socket = mNode.Sockets.FirstOrDefault(x =>
					x.SocketMode == SocketModeEnum.Input);
				if(socket != null)
				{
					AddMediaListItems(
						lvMediaResponse, imageListMediaResponse,
						mResources, socket.Properties);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* lvMediaQuestion_SelectedIndexChanged																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The selected index has changed on the question media listview.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void lvMediaQuestion_SelectedIndexChanged(object sender,
			EventArgs e)
		{
			btnMediaDeleteQuestion.Enabled =
				mnuEditMediaDeleteQuestion.Enabled =
				(lvMediaQuestion.SelectedItems.Count > 0);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* lvMediaResponse_SelectedIndexChanged																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The selected index has changed on the response media listview.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void lvMediaResponse_SelectedIndexChanged(object sender,
			EventArgs e)
		{
			btnMediaDeleteResponse.Enabled =
				mnuEditMediaDeleteResponse.Enabled =
				(lvMediaResponse.SelectedItems.Count > 0);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditMediaAddQuestion_Click																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Media / Question Add menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditMediaAddQuestion_Click(object sender, EventArgs e)
		{
			btnMediaAddQuestion_Click(null, null);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditMediaAddResponse_Click																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Media / Response Add menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditMediaAddResponse_Click(object sender, EventArgs e)
		{
			btnMediaAddResponse_Click(null, null);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditMediaDeleteQuestion_Click																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Media / Question Delete Selected menu option has been
		/// clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditMediaDeleteQuestion_Click(object sender, EventArgs e)
		{
			btnMediaDeleteQuestion_Click(null, null);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditMediaDeleteResponse_Click																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Media / Response Delete Selected menu option has been
		/// clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditMediaDeleteResponse_Click(object sender, EventArgs e)
		{
			btnMediaDeleteResponse_Click(null, null);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuViewMediaPage_Click																								*
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
		private void mnuViewMediaPage_Click(object sender, EventArgs e)
		{
			tctl.SelectedTab = tabMedia;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuViewNodesPage_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The View / Nodes Page menu item has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuViewNodesPage_Click(object sender, EventArgs e)
		{
			tctl.SelectedTab = tabNode;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuViewPropertiesPage_Click																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The View / Properties Page menu item has been clicked.
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
		//* mnuViewStoryboardPage_Click																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The View / Storyboard Page menu item has been clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuViewStoryboardPage_Click(object sender, EventArgs e)
		{
			tctl.SelectedTab = tabStoryboard;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PropertiesEditColor																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Edit the color value on the specified property row.
		/// </summary>
		/// <param name="row">
		/// Reference to the data row to be edited.
		/// </param>
		private void PropertiesEditColor(DataRow row)
		{
			frmColorSelect form = null;

			if(row != null &&
				row.Table.Columns.Contains("Name") &&
				row.Table.Columns.Contains("Value"))
			{
				form = new frmColorSelect();
				form.StartPosition = FormStartPosition.Manual;
				form.Location = CenterOver(this, form);
				form.Text = $"Select Color For [{row.Field<string>("Name")}]";
				if(form.ShowDialog() == DialogResult.OK)
				{
					row.SetField<string>("Value", ToHex(form.Color, true));
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PropertiesEditValue																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Edit the specified property value.
		/// </summary>
		/// <param name="row">
		/// Reference to the data row to be edited.
		/// </param>
		private void PropertiesEditValue(DataRow row)
		{
			frmPropertyAssignment form = null;
			string name = "";
			string originalName = "";
			DataRow record = null;
			List<DataRow> records = null;

			if(row != null &&
				row.Table.Columns.Contains("Name") &&
				row.Table.Columns.Contains("Value"))
			{
				form = new frmPropertyAssignment();
				form.StartPosition = FormStartPosition.Manual;
				form.Location = CenterOver(this, form);
				name = row.Field<string>("Name");
				form.PropertyName = name;
				if(mPropertyControls.Exists(x =>
					x.PropertyName.ToLower() == name.ToLower()))
				{
					//	Permanent property can not be renamed.
					form.PropertyNameReadOnly = true;
				}
				originalName = form.PropertyName;
				form.PropertyValue = row.Field<string>("Value");
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
								row.Table.Rows.Remove(row);
								//	Modify existing record.
								record = records[0];
								record.SetField<string>("Value", form.PropertyValue);
							}
						}
						else
						{
							//	No conflict. Rename the property.
							row.SetField<string>("Name", form.PropertyName);
						}
					}
					row.SetField<string>("Value", form.PropertyValue);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ReadNode																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read the contents of the caller's node to the local dialog.
		/// </summary>
		private void ReadNode()
		{
			string fontName = "";
			float fontSize = 0f;
			int index = 1;
			DataRow row = null;
			bool seriesNumeric = false;
			List<SocketItem> sockets = null;
			object sourceValue = null;
			string text = "";

			mAnswerTable.Rows.Clear();
			mAnswerTable.AcceptChanges();
			mPropertiesTable.Rows.Clear();
			mPropertiesTable.AcceptChanges();

			if(mNode != null)
			{
				//	Read properties from node.
				switch(mNode.NodeType.ToLower())
				{
					case "start":
						InputSocketEnabled = false;
						OutputSocketsEnabled = true;
						cmboType.SelectedItem = "Start";
						break;
					case "fork":
						InputSocketEnabled = true;
						OutputSocketsEnabled = true;
						cmboType.SelectedItem = "Fork";
						break;
					case "delay":
						InputSocketEnabled = true;
						OutputSocketsEnabled = true;
						cmboType.SelectedItem = "Delay";
						break;
					case "termination":
						InputSocketEnabled = true;
						OutputSocketsEnabled = false;
						cmboType.SelectedItem = "Termination";
						break;
				}
				txtX.Text = mNode.X.ToString("0.000");
				txtY.Text = mNode.Y.ToString("0.000");
				txtDelay.Text = mNode.Delay.ToString("0.000");
				if(mNode["Question"].Value == null)
				{
					mNode["Question"].Value = mNode["Ticket"].Value;
					mNode.TitleProperty = "Question";
				}
				txtQuestion.Text = mNode["Question"].Value.ToString();
				//	Extended story properties.
				text = mNode["StoryPageNumber"].StringValue();
				if(text.Length > 0)
				{
					txtStoryPageNumber.Text = text;
				}
				text = mNode["StoryPageWidth"].StringValue();
				if(text.Length > 0)
				{
					txtStoryWidth.Text = text;
				}
				else
				{
					txtStoryWidth.Text = "512";
				}
				text = mNode["StoryPageX"].StringValue();
				if(text.Length > 0)
				{
					txtStoryPageX.Text = text;
				}
				text = mNode["StoryPageHorizontalPlacement"].StringValue();
				if(text.Length > 0)
				{
					cmboStoryFromX.SelectedItem = text;
				}
				else
				{
					cmboStoryFromX.SelectedItem = "Left";
				}
				text = mNode["StoryPageY"].StringValue();
				if(text.Length > 0)
				{
					txtStoryPageY.Text = text;
				}
				text = mNode["StoryPageVerticalPlacement"].StringValue();
				if(text.Length > 0)
				{
					cmboStoryFromY.SelectedItem = text;
				}
				else
				{
					cmboStoryFromY.SelectedItem = "Top";
				}
				text = mNode["StoryShapeType"].StringValue();
				if(text.Length > 0)
				{
					cmboStoryboardShapeType.SelectedItem = text;
				}
				else
				{
					cmboStoryboardShapeType.SelectedIndex = 0;
				}
				text = mNode["StoryColorFill"].StringValue();
				if(text.Length > 0)
				{
					btnStoryFillColor.BackColor = FromHex(text);
				}
				text = mNode["StoryColorOutline"].StringValue();
				if(text.Length > 0)
				{
					btnStoryLineColor.BackColor = FromHex(text);
				}
				text = mNode["StoryColorText"].StringValue();
				if(text.Length > 0)
				{
					btnStoryTextColor.BackColor = FromHex(text);
				}
				fontName = mNode["StoryFontName"].StringValue();
				text = mNode["StoryFontSize"].StringValue();
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
						sourceValue = mNode[propertyControl.PropertyName].Value;
						row.SetField<string>("Value",
							(string)ScaffoldUtil.TypeConverter.Convert(sourceValue,
							GetRelaxedType(sourceValue), "string"));
					}
					mPropertiesTable.Rows.Add(row);
					row.AcceptChanges();
				}
				//	Read non-permanent node properties.
				foreach(PropertyItem property in mNode.Properties)
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
				//	Sockets.
				sockets = mNode.Sockets.FindAll(x =>
					x.SocketMode == SocketModeEnum.Output);
				foreach(SocketItem socket in sockets)
				{
					//	Protect against null access on improperly prepared nodes.
					if(socket["Index"].Value == null)
					{
						if(seriesNumeric)
						{
							socket["Index"].Value = index.ToString();
						}
						else
						{
							socket["Index"].Value =
								(Convert.ToChar(index + 64)).ToString();
						}
					}
					if(socket["Answer"].Value == null)
					{
						socket["Answer"].Value = $"Answer {socket["Index"].Value}";
					}
					row = mAnswerTable.NewRow();
					row.SetField<string>("Ticket", socket.Ticket);
					row.SetField<string>("Index", socket["Index"].StringValue());
					row.SetField<string>("Answer", socket["Answer"].StringValue());
					row.SetField<PropertyCollection>("Properties", socket.Properties);
					mAnswerTable.Rows.Add(row);
					row.AcceptChanges();
					index++;
				}
			}
			LoadMediaLists();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RemoveMediaQuestionListItem																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Remove the question media list item by media type.
		/// </summary>
		/// <param name="resourceType">
		/// The resource type to remove.
		/// </param>
		private void RemoveMediaQuestionListItem(string resourceType)
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
			for(index = 0; index < count; index ++)
			{
				item = lvMediaQuestion.Items[index];
				if(item.Group != null && item.Group.Name == key)
				{
					lvMediaQuestion.Items.Remove(item);
					break;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RemoveMediaResponseListItem																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Remove the question media list item by media type.
		/// </summary>
		/// <param name="resourceType">
		/// The resource type to remove.
		/// </param>
		private void RemoveMediaResponseListItem(string resourceType)
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
				item = lvMediaResponse.Items[index];
				if(item.Group != null && item.Group.Name == key)
				{
					lvMediaResponse.Items.Remove(item);
					break;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* RemoveRow																															*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Remove a row from the table where the cell is equal to the specified
		///// value in the named column.
		///// </summary>
		///// <param name="table">
		///// Reference to the table containing the row to delete.
		///// </param>
		///// <param name="columnName">
		///// Name of the column to match.
		///// </param>
		///// <param name="cellValue">
		///// Value of the cell to match.
		///// </param>
		//private void RemoveRow(DataTable table,
		//	string columnName, string cellValue)
		//{
		//	int count = 0;
		//	int index = 0;
		//	DataRow row = null;

		//	if(table != null && table.Columns.Contains(columnName))
		//	{
		//		count = table.Rows.Count;
		//		for(index = 0; index < count; index ++)
		//		{
		//			row = table.Rows[index];
		//			if(row.Field<string>(columnName) == cellValue)
		//			{
		//				table.Rows.RemoveAt(index);
		//				break;
		//			}
		//		}
		//	}
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	WriteNode																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the properties of the caller's node to reflect changes on this
		/// dialog.
		/// </summary>
		private void WriteNode()
		{
			int count = 0;
			bool found = false;
			int index = 0;
			string name = "";
			PropertyItem property = null;
			SocketItem socket = null;
			SocketItem socketInput = null;
			List<SocketItem> sockets = null;
			List<SocketItem> socketsNew = new List<SocketItem>();

			mAnswerTable.AcceptChanges();
			mPropertiesTable.AcceptChanges();
			if(mNode != null)
			{
				//	Node is present.
				mNode["Question"].Value = txtQuestion.Text;
				mNode.X = ToFloat(txtX.Text);
				mNode.Y = ToFloat(txtY.Text);
				mNode.Delay = ToFloat(txtDelay.Text);
				//	Extended story properties.
				mNode["StoryPageNumber"].Value = txtStoryPageNumber.Text;
				mNode["StoryPageWidth"].Value = txtStoryWidth;
				mNode["StoryPageX"].Value = txtStoryPageX.Text;
				mNode["StoryPageHorizontalPlacement"].Value =
					cmboStoryFromX.SelectedItem;
				mNode["StoryPageY"].Value = txtStoryPageY.Text;
				mNode["StoryPageVerticalPlacement"].Value =
					cmboStoryFromY.SelectedItem;
				mNode["StoryShapeType"].Value = cmboStoryboardShapeType.SelectedItem;
				mNode["StoryColorFill"].Value = ToHex(btnStoryFillColor.BackColor);
				mNode["StoryColorOutline"].Value = ToHex(btnStoryLineColor.BackColor);
				mNode["StoryColorText"].Value = ToHex(btnStoryTextColor.BackColor);
				mNode["StoryFontName"].Value = btnStoryFont.Font.Name;
				mNode["StoryFontSize"].Value = btnStoryFont.Font.SizeInPoints;
				//	All remaining properties, permanent and non-permanent.
				//	Delete properties for deleted rows.
				count = mNode.Properties.Count;
				for(index = 0; index < count; index ++)
				{
					property = mNode.Properties[index];
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
							mNode.Properties.Remove(property);
							index--;
							count--;
						}
					}
				}
				//	Add and update properties.
				foreach(DataRow row in mPropertiesTable.Rows)
				{
					if(row.RowState != DataRowState.Deleted &&
						row.RowState != DataRowState.Detached)
					{
						name = row.Field<string>("Name");
						if(!mPropertyControls.Exists(x =>
							x.PropertyName.ToLower() == name.ToLower() &&
							x.ControlInstance != null))
						{
							//	Convert to implied type so the value will be stored unquoted,
							//	if applicable.
							property = mNode.Properties.FirstOrDefault(x =>
								x.Name.ToLower() == name.ToLower());
							if(property == null)
							{
								//	Create a new property.
								mNode[name].Value = ToImpliedType(row.Field<string>("Value"));
							}
							else if(property.Static)
							{
								//	The property can be updated.
								mNode[name].Value = ScaffoldUtil.TypeConverter.Convert(
									row.Field<string>("Value"),
									"string", GetRelaxedType(mNode[name].Value));
							}
						}
					}
				}
				//	Sockets.
				if(!mInputSocketEnabled)
				{
					//	Input socket is disabled.
					mNode.Sockets.RemoveAll(x => x.SocketMode == SocketModeEnum.Input);
				}
				if(!mOutputSocketsEnabled)
				{
					//	Output sockets are disabled.
					mNode.Sockets.RemoveAll(x => x.SocketMode == SocketModeEnum.Output);
				}
				//	Input sockets.
				if(mInputSocketEnabled)
				{
					//	The input socket is enabled.
					socket = mNode.Sockets.FirstOrDefault(x =>
						x.SocketMode == SocketModeEnum.Input);
					if(socket == null)
					{
						socketInput = new SocketItem();
						socketInput.SocketMode = SocketModeEnum.Input;
						socketInput["Text"].Value = "Response";
						socketInput.TitleProperty = "Text";
					}
				}
				//	Output sockets.
				if(mOutputSocketsEnabled)
				{
					//	Output sockets are enabled. Non-destructive sync.
					foreach(DataRow row in mAnswerTable.Rows)
					{
						found = false;
						if(row.Field<string>("Ticket")?.Length > 0)
						{
							//	This was a pre-existing row.
							socket = mNode.Sockets.FirstOrDefault(x =>
								x.SocketMode == SocketModeEnum.Output &&
								x.Ticket == row.Field<string>("Ticket"));
							if(socket != null)
							{
								//	Match found. Update record.
								socket["Index"].Value = row.Field<string>("Index");
								socket["Answer"].Value = row.Field<string>("Answer");
								found = true;
							}
						}
						if(!found)
						{
							socket = new SocketItem();
							socket.SocketMode = SocketModeEnum.Output;
							socket["Index"].Value = row.Field<string>("Index");
							socket["Answer"].Value = row.Field<string>("Answer");
							if(row.Field<PropertyCollection>("Properties") != null)
							{
								foreach(PropertyItem propertyItem in
									row.Field<PropertyCollection>("Properties"))
								{
									if(propertyItem.Static)
									{
										socket[propertyItem.Name].Value = propertyItem.Value;
									}
								}
							}
							socket.TitleProperty = "Answer";
							socketsNew.Add(socket);
						}
					}
					//	Remove all items from the target collection not present in the
					//	current list.
					sockets = mNode.Sockets.FindAll(x =>
						x.SocketMode == SocketModeEnum.Output);
					foreach(SocketItem socketItem in sockets)
					{
						found = false;
						foreach(DataRow row in mAnswerTable.Rows)
						{
							if(row.Field<string>("Ticket") == socketItem.Ticket)
							{
								found = true;
								break;
							}
						}
						if(!found)
						{
							socketItem.Ticket = "";
						}
					}
					//	Delete non-existent sockets.
					mNode.Sockets.RemoveAll(x => x.Ticket.Length == 0);
					//	Add new input socket.
					if(socketInput != null)
					{
						mNode.Sockets.Insert(0, socketInput);
					}
					//	Add new sockets to the collection.
					if(socketsNew.Count > 0)
					{
						//mNode.Sockets.AddRange(socketsNew);
						foreach(SocketItem socketItem in socketsNew)
						{
							mNode.Sockets.Add(socketItem);
						}
					}
				}
				//	Update node properties.
				mNode.NodeType = cmboType.SelectedItem.ToString();
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
				txtQuestion.Focus();
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
			grdAnswer_SelectionChanged(this, null);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the frmDecisionNode Item.
		/// </summary>
		public frmDecisionNode()
		{
			PropertyControlItem[] propertyControlItems = null;

			InitializeComponent();
			//	Socket properties.
			mAnswerTable.Columns.Add("Index", typeof(string));
			mAnswerTable.Columns.Add("Answer", typeof(string));
			mAnswerTable.Columns.Add("Ticket", typeof(string));
			mAnswerTable.Columns.Add("Properties", typeof(PropertyCollection));
			grdAnswer.DataSource = mAnswerTable;
			//	Node properties.
			mPropertiesTable.Columns.Add("Name", typeof(string));
			mPropertiesTable.Columns.Add("Value", typeof(string));
			grdProperties.DataSource = mPropertiesTable;
			//	Bound property / control associations.
			mPropertyControls = new PropertyControlCollection();
			propertyControlItems = new PropertyControlItem[]
			{
				//	Built-in properties.
				new PropertyControlItem(mPropertyControls, "NodeColor", null),
				new PropertyControlItem(mPropertyControls, "NodeTextColor", null),
				new PropertyControlItem(mPropertyControls, "NodeType",
					cmboType, "SelectedIndex"),
				new PropertyControlItem(mPropertyControls, "Delay", txtDelay, "Text"),
				new PropertyControlItem(mPropertyControls, "Selected", null),
				new PropertyControlItem(mPropertyControls, "Ticket", null),
				new PropertyControlItem(mPropertyControls, "Height", null),
				new PropertyControlItem(mPropertyControls, "TitleHeight", null),
				new PropertyControlItem(mPropertyControls, "TitleProperty", null),
				new PropertyControlItem(mPropertyControls, "Width", null),
				new PropertyControlItem(mPropertyControls, "X", txtX, "Text"),
				new PropertyControlItem(mPropertyControls, "Y", txtY, "Text"),
				new PropertyControlItem(mPropertyControls, "ZOrder", null),
				//	Base properties.
				new PropertyControlItem(
					mPropertyControls, "Question", txtQuestion, "Text"),
				//	Extended story properties.
				new PropertyControlItem(
					mPropertyControls, "StoryPageNumber", txtStoryPageNumber, "Text"),
				new PropertyControlItem(
					mPropertyControls, "StoryPageWidth", txtStoryWidth, "Text"),
				new PropertyControlItem(
					mPropertyControls, "StoryPageX", txtStoryPageX, "Text"),
				new PropertyControlItem(
					mPropertyControls, "StoryPageHorizontalPlacement",
					cmboStoryFromX, "SelectedIndex"),
				new PropertyControlItem(
					mPropertyControls, "StoryPageY", txtStoryPageY, "Text"),
				new PropertyControlItem(
					mPropertyControls, "StoryPageVerticalPlacement",
					cmboStoryFromY, "SelectedIndex"),
				new PropertyControlItem(
					mPropertyControls, "StoryShapeType",
					cmboStoryboardShapeType, "SelectedIndex"),
				new PropertyControlItem(
					mPropertyControls, "StoryColorFill", btnStoryFillColor, "BackColor"),
				new PropertyControlItem(
					mPropertyControls, "StoryColorOutline",
					btnStoryLineColor, "BackColor"),
				new PropertyControlItem(
					mPropertyControls, "StoryColorText", btnStoryTextColor, "BackColor"),
				new PropertyControlItem(
					mPropertyControls, "StoryFontName",
					btnStoryFont, "Font.Name"),
				new PropertyControlItem(
					mPropertyControls, "StoryFontSize",
					btnStoryFont, "Font.SizeInPoints")
			};
			mPropertyControls.AddRange(propertyControlItems);
			mPropertyControls.Enabled = false;
			mPropertyControls.PropertiesTable = mPropertiesTable;

			//	Media page.
			btnMediaDeleteQuestion.Enabled =
				mnuEditMediaDeleteQuestion.Enabled =
				btnMediaDeleteResponse.Enabled =
				mnuEditMediaDeleteResponse.Enabled = false;

			lvMediaQuestion.Groups.Add(new ListViewGroup("Audio", "Audio"));
			lvMediaQuestion.Groups.Add(new ListViewGroup("Image", "Image"));
			lvMediaQuestion.Groups.Add(new ListViewGroup("Link", "Link"));
			lvMediaQuestion.Groups.Add(new ListViewGroup("Video", "Video"));

			lvMediaResponse.Groups.Add(new ListViewGroup("Audio", "Audio"));
			lvMediaResponse.Groups.Add(new ListViewGroup("Image", "Image"));
			lvMediaResponse.Groups.Add(new ListViewGroup("Link", "Link"));
			lvMediaResponse.Groups.Add(new ListViewGroup("Video", "Video"));

			imageListMediaQuestion.Images.Add(ResourceMain.Audio128);
			imageListMediaQuestion.Images.Add(ResourceMain.Link128);

			imageListMediaResponse.Images.Add(ResourceMain.Audio128);
			imageListMediaResponse.Images.Add(ResourceMain.Link128);
			//optMediaPerspective_CheckedChanged(null, null);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AnswerTable																														*
		//*-----------------------------------------------------------------------*
		private DataTable mAnswerTable = new DataTable("Answer");
		/// <summary>
		/// Get a reference to the table of answers.
		/// </summary>
		public DataTable AnswerTable
		{
			get { return mAnswerTable; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	InputSocketEnabled																										*
		//*-----------------------------------------------------------------------*
		private bool mInputSocketEnabled = false;
		/// <summary>
		/// Get/Set a value indicating whether the input socket is enabled.
		/// </summary>
		public bool InputSocketEnabled
		{
			get { return mInputSocketEnabled; }
			set
			{
				SocketItem socket = null;

				mInputSocketEnabled = value;
				lblMediaResponse.Enabled =
					lvMediaResponse.Enabled =
					btnMediaAddResponse.Enabled =
					btnMediaDeleteResponse.Enabled = value;

				if(value)
				{
					//	Verify socket.
					socket = mNode.Sockets.FirstOrDefault(x =>
						x.SocketMode == SocketModeEnum.Input);
					if(socket == null)
					{
						socket = new SocketItem();
						socket.SocketMode = SocketModeEnum.Input;
						socket["Text"].Value = "Response";
						socket.TitleProperty = "Text";
						mNode.Sockets.Add(socket);
					}
				}
				else
				{
					//	Response list not populated.
					lvMediaResponse.Items.Clear();
					//	Verify no socket.
					socket = mNode.Sockets.FirstOrDefault(x =>
						x.SocketMode == SocketModeEnum.Input);
					if(socket != null)
					{
						mNode.Sockets.Remove(socket);
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Node																																	*
		//*-----------------------------------------------------------------------*
		private NodeItem mNode = null;
		/// <summary>
		/// Get a reference to the node to be edited.
		/// </summary>
		public NodeItem Node
		{
			get { return mNode; }
			//set
			//{
			//	mNode = value;
			//	ReadNode();
			//}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	OutputSocketsEnabled																									*
		//*-----------------------------------------------------------------------*
		private bool mOutputSocketsEnabled = false;
		/// <summary>
		/// Get/Set a value indicating whether the output sockets are enabled.
		/// </summary>
		public bool OutputSocketsEnabled
		{
			get { return mOutputSocketsEnabled; }
			set { mOutputSocketsEnabled = value; }
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
		//*	Question																															*
		//*-----------------------------------------------------------------------*
		private string mQuestion = "";
		/// <summary>
		/// Get/Set the question.
		/// </summary>
		public string Question
		{
			get { return mQuestion; }
			set { mQuestion = value; }
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
		//* SetNode																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set a reference to the node and associated resources used in this
		/// instance.
		/// </summary>
		/// <param name="node">
		/// Reference to the node being edited.
		/// </param>
		/// <param name="resources">
		/// Reference to associated resource collection.
		/// </param>
		public void SetNode(NodeItem node, ResourceCollection resources)
		{
			mNode = node;
			mResources = resources;
			ReadNode();
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
