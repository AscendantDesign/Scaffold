//	frmFind.cs
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

using static Scaffold.ScaffoldNodesUtil;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	frmFind																																	*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Find form.
	/// </summary>
	public partial class frmFind : Form
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* btnCancel_Click																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Cancel button has been clicked.
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
		//* btnFind_Click																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Find button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnFind_Click(object sender, EventArgs e)
		{
			bool found = false;
			string lText = "";
			ListViewItem lvItem = null;
			bool match = false;

			if(NodeFileObject != null)
			{
				foreach(NodeItem node in NodeFileObject.Nodes)
				{
					found = false;
					match = false;
					if(chkMediaAudio.Checked)
					{
						match = true;
						found =
							ResourceFound(node, MediaTypeEnum.Audio, txtMediaAudio.Text);
					}
					if(match == found && chkMediaImage.Checked)
					{
						match = true;
						found =
							ResourceFound(node, MediaTypeEnum.Image, txtMediaImage.Text);
					}
					if(match == found && chkMediaLink.Checked)
					{
						match = true;
						found =
							ResourceFound(node, MediaTypeEnum.Link, txtMediaLink.Text);
					}
					if(match == found && chkMediaVideo.Checked)
					{
						match = true;
						found =
							ResourceFound(node, MediaTypeEnum.Video, txtMediaVideo.Text);
					}
					if(match == found)
					{
						match = true;
						if(txtSearch.Text.Length > 0)
						{
							found = false;
							lText = txtSearch.Text.ToLower();
							if(node.Properties[node.TitleProperty].ToString().ToLower().
								IndexOf(lText) > -1)
							{
								found = true;
							}
							if(!found)
							{
								foreach(SocketItem socket in node.Sockets)
								{
									if(socket.Properties[socket.TitleProperty].ToString().
										ToLower().IndexOf(lText) > -1)
									{
										found = true;
										break;
									}
								}
							}
						}
						else
						{
							//	When no text is specified, all nodes match if the options
							//	were matched.
							found = true;
						}
					}
					if(found)
					{
						//	TODO: !1. Stopped here.
						//	Place this item in the list.
						lvItem = new ListViewItem(new string[]
						{
							"Node",
							node.Properties[node.TitleProperty].ToString()
						});
						lvItem.Tag = node;
						lv.Items.Add(lvItem);
					}
				}
			}
			else
			{
				MessageBox.Show("Couldn't find the Node collection from the Find " +
					"dialog.\r\n" +
					"Please contact your Scaffold administrator.",
					"Find Click", MessageBoxButtons.OK);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnOK_Click																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The OK button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Hide();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* chkMediaAudio_CheckedChanged																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Media / Audio checkbox state has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void chkMediaAudio_CheckedChanged(object sender, EventArgs e)
		{
			if(chkMediaAudio.Checked)
			{
				ControlEnable(txtMediaAudio);
			}
			else
			{
				ControlDisable(txtMediaAudio);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* chkMediaImage_CheckedChanged																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Media / Image checkbox state has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void chkMediaImage_CheckedChanged(object sender, EventArgs e)
		{
			if(chkMediaImage.Checked)
			{
				ControlEnable(txtMediaImage);
			}
			else
			{
				ControlDisable(txtMediaImage);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* chkMediaLink_CheckedChanged																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Media / Link checkbox state has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void chkMediaLink_CheckedChanged(object sender, EventArgs e)
		{
			if(chkMediaLink.Checked)
			{
				ControlEnable(txtMediaLink);
			}
			else
			{
				ControlDisable(txtMediaLink);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	chkMediaVideo_CheckedChanged																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Media / Video checkbox state has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void chkMediaVideo_CheckedChanged(object sender, EventArgs e)
		{
			if(chkMediaVideo.Checked)
			{
				ControlEnable(txtMediaVideo);
			}
			else
			{
				ControlDisable(txtMediaVideo);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ControlEnable																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Enable the specified control for user interaction.
		/// </summary>
		/// <param name="control">
		/// Reference to the control to enable.
		/// </param>
		private static void ControlEnable(TextBox control)
		{
			control.BackColor = System.Drawing.SystemColors.Window;
			control.ForeColor = System.Drawing.SystemColors.WindowText;
			control.ReadOnly = false;
			control.TabStop = true;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ControlDisable																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Disable the specified control from user interaction.
		/// </summary>
		/// <param name="control">
		/// Reference to the control to disable.
		/// </param>
		private static void ControlDisable(TextBox control)
		{
			control.BackColor = System.Drawing.SystemColors.Control;
			control.ForeColor = System.Drawing.SystemColors.ControlDark;
			control.ReadOnly = true;
			control.TabStop = false;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ResourceFound																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified resource type is found.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to find.
		/// </param>
		/// <param name="mediaType">
		/// Type of media to find.
		/// </param>
		/// <param name="text">
		/// Optional text to match if the media type is found.
		/// </param>
		/// <returns>
		/// True if the resource of the specified type is found. Otherwise, false.
		/// </returns>
		private static bool ResourceFound(NodeItem node, MediaTypeEnum mediaType,
			string text = "")
		{
			ResourceItem resource = null;
			bool result = false;

			if(text.Length > 0)
			{
				resource = GetResource(node, mediaType);
				if(resource != null)
				{
					if(resource.AbsoluteFilename.ToLower().IndexOf(text.ToLower()) > -1)
					{
						result = true;
					}
				}
				if(!result)
				{
					//	If not found on the node, check all sockets.
					foreach(SocketItem socket in node.Sockets)
					{
						resource = GetResource(socket, mediaType);
						if(resource != null)
						{
							if(resource.AbsoluteFilename.ToLower().
								IndexOf(text.ToLower()) > -1)
							{
								result = true;
							}
							break;
						}
					}
				}
			}
			else
			{
				//	Blank text.
				if(MediaExists(node, mediaType))
				{
					result = true;
				}
				else
				{
					//	Check the sockets.
					foreach(SocketItem socket in node.Sockets)
					{
						if(MediaExists(socket, mediaType))
						{
							result = true;
							break;
						}
					}
				}
			}
			return result;
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
		/// Create a new instance of the frmFind Item.
		/// </summary>
		public frmFind()
		{
			InitializeComponent();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SelectedItems																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get a reference to the collection of selected items.
		/// </summary>
		public List<ListViewItem> SelectedItems
		{
			get
			{
				List<ListViewItem> result = new List<ListViewItem>();

				foreach(ListViewItem item in lv.SelectedItems)
				{
					result.Add(item);
				}
				return result;
			}
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

}
