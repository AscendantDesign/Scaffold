//	frmFrameFlipbook.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Manina.Windows.Forms;

using AnimatedGif;
using Newtonsoft.Json;

using static Scaffold.ScaffoldNodesUtil;
using static Scaffold.ScaffoldUtil;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	frmFrameSwitch																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Image frame switching and stitching form.
	/// </summary>
	public partial class frmFrameFlipbook : Form
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		private FileInfo[] mFileList = new FileInfo[0];
		private FlipbookFile mFlipbook = null;
		private FileInfo mFrameFile = null;
		private ToolStripMenuItem[] mMenusThumbsize = null;
		private MenuControlItem[] mMenusTransport = null;
		private ToolStripMenuItem[] mMenusViewMode = null;
		private bool mMouseDown = false;
		private Point mMouseLocation = Point.Empty;
		private bool mWindowActive = false;

		private delegate void SafeCallDelegate(string text);

		//*-----------------------------------------------------------------------*
		//* FillThumbnailList																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Asynchronously fill the thumbnail list.
		/// </summary>
		/// <returns>
		/// Reference to an active task.
		/// </returns>
		private async Task FillThumbnailList()
		{
			await Task.Run(() =>
			{
				foreach(FileInfo fileItem in mFileList)
				{
					SafeAddFilename(fileItem.FullName);
				}
			});
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetFileIndex																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the index of the file in the loaded files list.
		/// </summary>
		/// <param name="filename">
		/// Name of the file to search for.
		/// </param>
		/// <returns>
		/// Index of the file within the files scope list. -1 if not found.
		/// </returns>
		private int GetFileIndex(string filename)
		{
			int index = 0;
			string nametl = "";
			int result = -1;

			if(filename?.Length > 0)
			{
				nametl = filename.ToLower();
				foreach(FileInfo file in mFileList)
				{
					if(file.Name.ToLower() == nametl)
					{
						result = index;
						break;
					}
					index++;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* lvThumbs_SelectionChanged																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The selection on the thumbnail list view has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void lvThumbs_SelectionChanged(object sender, EventArgs e)
		{
			string filename = "";

			if(!mItemSelectionBusy)
			{
				//	Selection is not being driven by the keyframe list.
				mItemSelectionBusy = true;
				if(lvThumbs.SelectedItems.Count > 0)
				{
					filename = GetFilename(lvThumbs.SelectedItems[0].FileName);
					pnlFlow.SelectItem(filename);
				}
				mItemSelectionBusy = false;
			}
			//if(picImage.Visible)
			//{
				//	Display the image from the current list view thumbnail.
				if(lvThumbs.SelectedItems.Count > 0)
				{
					picImage.ImageLocation = lvThumbs.SelectedItems[0].FileName;
					statMessage.Text = GetFilename(lvThumbs.SelectedItems[0].FileName);
				}
			//}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditCaptureFrame_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Capture Frame menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditCaptureFrame_Click(object sender, EventArgs e)
		{
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditRunCommands_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Run Edit Commands menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditRunCommands_Click(object sender, EventArgs e)
		{
			bool bKeep = false;
			Bitmap bitmap = null;
			Brush brush = null;
			int commandCount = 0;
			string commandName = "";
			int count = 0;
			FlipbookFrameItem frame = null;
			FlipbookFrameItem frameNext = null;
			string fullFilename = "";
			int index = 0;
			int indexBegin = 0;
			int indexEnd = 0;
			int indexFile = 0;
			Match match = null;
			string[] parameters = null;
			string paramText = "";
			string path = "";
			Rectangle rect = Rectangle.Empty;

			if(mFlipbook != null)
			{
				path = mFlipbook.Folder;
				count = mFlipbook.Frames.Count;
				for(index = 0; index < count; index ++)
				{
					frame = mFlipbook.Frames[index];
					if(frame.EditCommands.Count > 0)
					{
						//	This frame.
						statMessage.Text = $"Editing {frame.Index}...";
						statusFrameSwitch.Refresh();
						indexFile = GetFileIndex(frame.Index);
						bKeep = (frame.Action == FlipbookActionTypeEnum.Keep);
						if(bKeep)
						{
							indexBegin = indexFile;
							if(mFlipbook.Frames.Count > index + 1)
							{
								frameNext = mFlipbook.Frames[index + 1];
								indexEnd = GetFileIndex(frameNext.Index);
								if(indexEnd > indexBegin)
								{
									indexEnd--;
								}
							}
						}
						else
						{
							indexBegin = indexEnd = indexFile;
						}
						for(indexFile = indexBegin;
							indexFile > -1 && indexFile <= indexEnd; indexFile ++)
						{
							bitmap = null;
							//fullFilename = Path.Combine(path, frame.Index);
							fullFilename = mFileList[indexFile].FullName;
							using(FileStream stream = File.OpenRead(fullFilename))
							{
								bitmap = (Bitmap)Bitmap.FromStream(stream);
							}
							if(bitmap != null)
							{
								//	The image is loaded.
								using(Graphics g = Graphics.FromImage(bitmap))
								{
									g.CompositingQuality =
										System.Drawing.Drawing2D.
										CompositingQuality.HighQuality;
									g.InterpolationMode =
										System.Drawing.Drawing2D.InterpolationMode.High;
									g.SmoothingMode =
										System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
									foreach(string command in frame.EditCommands)
									{
										commandCount++;
										parameters = null;
										match = Regex.Match(command, ResourceMain.rxCommandParams);
										if(match.Success)
										{
											commandName = GetValue(match, "command").ToLower();
											paramText = GetValue(match, "params");
											if(paramText.Length > 0)
											{
												parameters = paramText.Split(',');
											}
											//	Process the command.
											switch(commandName)
											{
												case "fillcolor":
													//	FillColor is {color}, {X}, {Y}, {W}, {H}
													if(parameters.Length >= 5)
													{
														brush =
															new SolidBrush(FromHex(parameters[0].Trim()));
														rect = new Rectangle(
															ToInt(parameters[1].Trim()),
															ToInt(parameters[2].Trim()),
															ToInt(parameters[3].Trim()),
															ToInt(parameters[4].Trim()));
														g.FillRectangle(brush, rect);
													}
													break;
											}
										}
									}
									bitmap.Save(fullFilename);
									bitmap.Dispose();
									bitmap = null;
								}
							}
						}
					}
				}
			}
			statMessage.Text = $"{commandCount} editing commands were run...";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileClose_Click																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The File / Close menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuFileClose_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Hide();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	mnuFileExportGIF_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Export an animated GIF file from the current animation.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuFileExportGIF_Click(object sender, EventArgs e)
		{
			Bitmap bitmap = null;
			bool bKeep = false;
			SaveFileDialog dialog = null;
			int fileIndex = 0;
			string filePath = "";
			FlipbookFrameItem frame = null;
			FlipbookFrameItem frameNext = null;
			int frameCount = 0;
			int frameIndex = 0;
			string fullFilename = "";
			int indexBegin = 0;
			int indexEnd = 0;
			Size targetSize = Size.Empty;

			if(mFlipbook != null && mFlipbook.Frames.Count > 0)
			{
				dialog = new SaveFileDialog();
				dialog.AddExtension = true;
				dialog.CheckPathExists = true;
				dialog.DefaultExt = ".gif";
				dialog.DereferenceLinks = true;
				dialog.Filter = "GIF Files (*.gif)|*.gif|" +
					"All Files (*.*)|*.gif";
				dialog.FilterIndex = 0;
				dialog.OverwritePrompt = true;
				dialog.SupportMultiDottedExtensions = true;
				dialog.Title = "Save GIF File As";
				dialog.ValidateNames = true;
				if(dialog.ShowDialog() == DialogResult.OK)
				{
					targetSize = new Size(mFlipbook.OutputWidth, mFlipbook.OutputHeight);
					using(AnimatedGifCreator gif =
						AnimatedGif.AnimatedGif.Create(dialog.FileName, 100))
					{
						filePath = mFlipbook.Folder;
						frameCount = mFlipbook.Frames.Count;
						for(frameIndex = 0; frameIndex < frameCount; frameIndex++)
						{
							frame = mFlipbook.Frames[frameIndex];
							statMessage.Text = $"Exporting frame {frame.Index}...";
							statusFrameSwitch.Refresh();
							fileIndex = GetFileIndex(frame.Index);
							bKeep = (frame.Action == FlipbookActionTypeEnum.Keep);
							if(bKeep)
							{
								indexBegin = fileIndex;
								if(mFlipbook.Frames.Count > frameIndex + 1)
								{
									frameNext = mFlipbook.Frames[frameIndex + 1];
									indexEnd = GetFileIndex(frameNext.Index);
									if(indexEnd > indexBegin)
									{
										indexEnd--;
									}
								}
							}
							else
							{
								indexBegin = indexEnd = fileIndex;
							}
							for(fileIndex = indexBegin;
								fileIndex > -1 && fileIndex <= indexEnd; fileIndex++)
							{
								bitmap = null;
								//fullFilename = Path.Combine(path, frame.Index);
								fullFilename = mFileList[fileIndex].FullName;
								using(FileStream stream = File.OpenRead(fullFilename))
								{
									bitmap = (Bitmap)Bitmap.FromStream(stream);
								}
								if(bitmap != null)
								{
									//	The image is loaded.
									bitmap = CreateThumbnail(bitmap, targetSize, false);
									gif.AddFrame(bitmap,
										frame.Timer != 0 ? frame.Timer : mFlipbook.DefaultTimer,
										GifQuality.Bit8);
								}
							}
						}
					}
				}
			}
			else
			{
				MessageBox.Show("There are no frames to save.", "Export GIF");
			}
			statMessage.Text = "Export to GIF finished...";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileOpen_Click																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The File / Open Frame Switch File menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuFileOpen_Click(object sender, EventArgs e)
		{
			//Bitmap bitmap = null;
			string content = "";
			OpenFileDialog dialog = new OpenFileDialog();
			DirectoryInfo dir = null;
			//FileInfo[] files = null;
			//FlipbookItemControl listItem = null;
			//string name = "";
			//Bitmap thumbnail = null;

			lvThumbs.Items.Clear();
			pnlFlow.Clear();

			dialog.Filter = "Frame Flipbook JSON " +
				"(*.flipbook.json)|*.flipbook.json|" +
				"All Files (*.*)|*.*";
			dialog.FilterIndex = 0;
			dialog.DefaultExt = ".flipbook.json";
			dialog.AddExtension = true;
			dialog.CheckFileExists = true;
			dialog.CheckPathExists = true;
			dialog.SupportMultiDottedExtensions = true;
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				mFrameFile = new FileInfo(dialog.FileName);
				content = File.ReadAllText(dialog.FileName);
				mFlipbook =
					JsonConvert.DeserializeObject<FlipbookFile>(content);
				mFlipbook.Frames.SetParentOnChildren();
				this.Text = $"Frame Flipbook - {mFrameFile.Name}";
				//	Fill the thumbnails list.
				if(mFlipbook.Folder?.Length > 0)
				{
					dir = new DirectoryInfo(mFlipbook.Folder);
					if(dir.Exists)
					{
						//	The folder exists.
						if(mFlipbook.Filter?.Length > 0)
						{
							//	Get all files of the specified extension.
							mFileList = dir.GetFiles(mFlipbook.Filter);
							_ = FillThumbnailList();
						}
						else
						{
							//	Retrieve each file independently.
						}
					}
				}
				//	Fill the list control with the current entries.
				pnlFlow.Frames = mFlipbook.Frames;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuTransport_CheckedChanged																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state of an item on the transport menu has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuTransport_CheckedChanged(object sender, EventArgs e)
		{
			foreach(MenuControlItem menuItem in mMenusTransport)
			{
				switch(menuItem.MenuItem.Name)
				{
					case "mnuTransportBack":
					case "mnuTransportNext":
						break;
					case "mnuTransportPause":
					case "mnuTransportPlay":
					case "mnuTransportStop":
						if(menuItem.MenuItem.Checked)
						{
							((Panel)menuItem.ControlItem).BackColor =
								FromHex(ResourceMain.colorBackgroundActive);
						}
						else
						{
							((Panel)menuItem.ControlItem).BackColor =
								FromHex(ResourceMain.colorBackground);
						}
						break;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuTransportBack_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Transport / Step Back menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuTransportBack_Click(object sender, EventArgs e)
		{
			pnlFlow.SelectPrevious();
			pnlFlow_ItemClick(pnlFlow.FirstSelectedItem, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuTransportNext_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Transport / Step Next menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuTransportNext_Click(object sender, EventArgs e)
		{
			pnlFlow.SelectNext();
			pnlFlow_ItemClick(pnlFlow.FirstSelectedItem, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuTransportPause_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Transport / Pause menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuTransportPause_Click(object sender, EventArgs e)
		{
			if(mnuTransportPlay.Checked)
			{
				mnuTransportPlay.Checked = false;
				mnuTransportPause.Checked = true;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuTransportPlay_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Transport / Play menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuTransportPlay_Click(object sender, EventArgs e)
		{
			bool fromStop = false;

			if(!mnuTransportPlay.Checked)
			{
				fromStop = !mnuTransportPause.Checked;
				foreach(MenuControlItem menuItem in mMenusTransport)
				{
					menuItem.MenuItem.Checked = false;
				}
				mnuTransportPlay.Checked = true;
				if(fromStop)
				{
					pnlFlow.SelectFirst();
				}
				mnuViewImage_Click(null, null);
				_ = PlayAnimation();
				//await PlayAnimation();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuTransportStop_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Transport / Stop menu optioin has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuTransportStop_Click(object sender, EventArgs e)
		{
			if(!mnuTransportStop.Checked)
			{
				foreach(MenuControlItem menuItem in mMenusTransport)
				{
					menuItem.MenuItem.Checked = false;
				}
				mnuTransportStop.Checked = true;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuViewImage_Click																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The View / Image Preview menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuViewImage_Click(object sender, EventArgs e)
		{
			lvThumbs.Dock = DockStyle.None;
			lvThumbs.Visible = false;
			picImage.Visible = true;
			picImage.Dock = DockStyle.Fill;
			mnuViewThumbnails.Checked = false;
			mnuViewImage.Checked = true;
			this.Invalidate();
			this.Refresh();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuViewMode_CheckedChanged																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state of one of the View / mode members has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuViewMode_CheckedChanged(object sender, EventArgs e)
		{
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuViewThumbnails_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The View / Thumbnail List menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuViewThumbnails_Click(object sender, EventArgs e)
		{
			picImage.Dock = DockStyle.None;
			picImage.Visible = false;
			lvThumbs.Visible = true;
			lvThumbs.Dock = DockStyle.Fill;
			mnuViewImage.Checked = false;
			mnuViewThumbnails.Checked = true;
			this.Invalidate();
			this.Refresh();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuViewThumbsize128_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The View / Thumbnail Size / 128 menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuViewThumbsize128_Click(object sender, EventArgs e)
		{
			lvThumbs.ThumbnailSize = new Size(128, 128);
			lvThumbs.Invalidate();
			lvThumbs.Refresh();
			foreach(ToolStripMenuItem menuItem in mMenusThumbsize)
			{
				if(menuItem.Checked)
				{
					menuItem.Checked = false;
				}
			}
			mnuViewThumbsize128.Checked = true;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuViewThumbsize256_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The View / Thumbnail Size / 256 menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuViewThumbsize256_Click(object sender, EventArgs e)
		{
			lvThumbs.ThumbnailSize = new Size(256, 256);
			lvThumbs.Invalidate();
			lvThumbs.Refresh();
			foreach(ToolStripMenuItem menuItem in mMenusThumbsize)
			{
				if(menuItem.Checked)
				{
					menuItem.Checked = false;
				}
			}
			mnuViewThumbsize256.Checked = true;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuViewThumbsize512_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The View / Thumbnail Size / 512 menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuViewThumbsize512_Click(object sender, EventArgs e)
		{
			lvThumbs.ThumbnailSize = new Size(512, 512);
			lvThumbs.Invalidate();
			lvThumbs.Refresh();
			foreach(ToolStripMenuItem menuItem in mMenusThumbsize)
			{
				if(menuItem.Checked)
				{
					menuItem.Checked = false;
				}
			}
			mnuViewThumbsize512.Checked = true;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuViewThumbsize96_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The View / Thumbnail Size / 96 menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuViewThumbsize96_Click(object sender, EventArgs e)
		{
			lvThumbs.ThumbnailSize = new Size(96, 96);
			lvThumbs.Invalidate();
			lvThumbs.Refresh();
			foreach(ToolStripMenuItem menuItem in mMenusThumbsize)
			{
				if(menuItem.Checked)
				{
					menuItem.Checked = false;
				}
			}
			mnuViewThumbsize96.Checked = true;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	mnuViewThumbsize_CheckedChanged																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state of a thumnail size menu member has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuViewThumbsize_CheckedChanged(object sender, EventArgs e)
		{
			
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PlayAnimation																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Play the animation off of the main UI thread.
		/// </summary>
		/// <returns>
		/// Reference to an awaitable task.
		/// </returns>
		private async Task PlayAnimation()
		{
			bool bKeep = false;
			FlipbookFrameItem frame = null;
			FlipbookFrameItem frameNext = null;
			int frameCount = 0;
			int frameIndex = 0;
			int indexBegin = 0;
			int indexEnd = 0;
			int indexFile = 0;

			if(mFlipbook != null)
			{
				frameCount = mFlipbook.Frames.Count;
				while(true)
				{
					//	Continuous.
					for(frameIndex = 0; frameIndex < frameCount; frameIndex++)
					{
						frame = mFlipbook.Frames[frameIndex];
						SafeMessageWrite($"Playing frames {frame.Index}...");
						indexFile = GetFileIndex(frame.Index);
						bKeep = (frame.Action == FlipbookActionTypeEnum.Keep);
						if(bKeep)
						{
							indexBegin = indexFile;
							if(mFlipbook.Frames.Count > frameIndex + 1)
							{
								frameNext = mFlipbook.Frames[frameIndex + 1];
								indexEnd = GetFileIndex(frameNext.Index);
								if(indexEnd > indexBegin)
								{
									indexEnd--;
								}
							}
						}
						else
						{
							indexBegin = indexEnd = indexFile;
						}
						for(indexFile = indexBegin;
							indexFile > -1 && indexFile <= indexEnd; indexFile++)
						{
							SafePictureImageLocation(mFileList[indexFile].FullName);
							//	Wait for timer.
							if(frame.Timer > 0)
							{
								await Task.Delay(frame.Timer);
							}
							else if(mFlipbook.DefaultTimer > 0)
							{
								await Task.Delay(mFlipbook.DefaultTimer);
							}
							if(!mnuTransportPlay.Checked)
							{
								break;
							}
						}
						if(!mnuTransportPlay.Checked)
						{
							break;
						}
					}
					if(!mnuTransportPlay.Checked)
					{
						break;
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	pnlClose_MouseClick																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The close panel has received a mouse click.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlClose_MouseClick(object sender, MouseEventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Hide();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlClose_MouseEnter																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has entered the close panel.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlClose_MouseEnter(object sender, EventArgs e)
		{
			pnlClose.BackColor = FromHex(ResourceMain.colorWinControlClose);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlClose_MouseLeave																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has left the close panel.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlClose_MouseLeave(object sender, EventArgs e)
		{
			pnlClose.BackColor = FromHex(ResourceMain.colorWinControlNormal);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlFlow_ItemClick																											*
		//*-----------------------------------------------------------------------*
		private bool mItemSelectionBusy = false;
		/// <summary>
		/// A keyframe list item has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlFlow_ItemClick(object sender, EventArgs e)
		{
			int count = 0;
			FileInfo file = null;
			string fullName = "";
			int index = 0;
			FlipbookItemControl item = null;
			string name = "";

			if(sender != null)
			{
				if(!mItemSelectionBusy)
				{
					mItemSelectionBusy = true;
					item = (FlipbookItemControl)sender;
					if(item.Frame.Selected)
					{
						name = item.Frame.Index;
						count = mFileList.Length;
						for(index = 0; index < count; index++)
						{
							file = mFileList[index];
							if(file.Name == name)
							{
								//	Item found.
								fullName = file.FullName;
								lvThumbs.EnsureVisible(index);
								while(lvThumbs.SelectedItems.Count > 0)
								{
									lvThumbs.SelectedItems[0].Selected = false;
								}
								foreach(ImageListViewItem viewItem in lvThumbs.Items)
								{
									if(viewItem.FileName == fullName)
									{
										viewItem.Selected = true;
										break;
									}
								}
								break;
							}
						}
					}
					mItemSelectionBusy = false;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlMaximize_MouseClick																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The maximize panel has received a mouse click.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlMaximize_MouseClick(object sender, MouseEventArgs e)
		{
			if(this.WindowState == FormWindowState.Maximized)
			{
				this.WindowState = FormWindowState.Normal;
			}
			else
			{
				this.WindowState = FormWindowState.Maximized;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlMaximize_MouseEnter																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has entered the maximize panel.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlMaximize_MouseEnter(object sender, EventArgs e)
		{
			pnlMaximize.BackColor = FromHex(ResourceMain.colorWinControlHover);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlMaximize_MouseLeave																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has left the maximize panel.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlMaximize_MouseLeave(object sender, EventArgs e)
		{
			pnlMaximize.BackColor = FromHex(ResourceMain.colorWinControlNormal);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlMinimize_MouseClick																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The minimize panel has received a mouse click.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlMinimize_MouseClick(object sender, MouseEventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlMinimize_MouseEnter																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has entered the minimize panel.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlMinimize_MouseEnter(object sender, EventArgs e)
		{
			pnlMinimize.BackColor = FromHex(ResourceMain.colorWinControlHover);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlMinimize_MouseLeave																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has left the minimize panel.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlMinimize_MouseLeave(object sender, EventArgs e)
		{
			pnlMinimize.BackColor = FromHex(ResourceMain.colorWinControlNormal);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTitle_MouseDown																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A mouse button has been depressed on the title bar.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTitle_MouseDown(object sender, MouseEventArgs e)
		{
			mMouseDown = true;
			mMouseLocation = e.Location;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTitle_MouseMove																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse is moving over the title bar.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTitle_MouseMove(object sender, MouseEventArgs e)
		{
			if(mMouseDown)
			{
				this.Location =
					new Point(
						this.Location.X + (e.X - mMouseLocation.X),
						this.Location.Y + (e.Y - mMouseLocation.Y));
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTitle_MouseUp																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has been released on the title bar.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTitle_MouseUp(object sender, MouseEventArgs e)
		{
			mMouseDown = false;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTransportBack_MouseClick																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has been clicked over the Back button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTransportBack_MouseClick(object sender, MouseEventArgs e)
		{
			mnuTransportBack_Click(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTransportBack_MouseEnter																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has entered the area of the Back toolbar button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTransportBack_MouseEnter(object sender, EventArgs e)
		{
			pnlTransportBack.BackColor =
				FromHex(ResourceMain.colorBackgroundHighlight);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTransportBack_MouseLeave																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has left the area of the Back toolbar button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTransportBack_MouseLeave(object sender, EventArgs e)
		{
			pnlTransportBack.BackColor = FromHex(ResourceMain.colorBackground);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTransportNext_MouseClick																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has been clicked over the Next button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTransportNext_MouseClick(object sender, MouseEventArgs e)
		{
			mnuTransportNext_Click(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTransportNext_MouseEnter																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has entered the area of the Next toolbar button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTransportNext_MouseEnter(object sender, EventArgs e)
		{
			pnlTransportNext.BackColor =
				FromHex(ResourceMain.colorBackgroundHighlight);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTransportNext_MouseLeave																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has left the area of the Next toolbar button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTransportNext_MouseLeave(object sender, EventArgs e)
		{
			pnlTransportNext.BackColor = FromHex(ResourceMain.colorBackground);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTransportPause_MouseClick																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has been clicked over the Pause button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTransportPause_MouseClick(object sender, MouseEventArgs e)
		{
			mnuTransportPause_Click(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTransportPause_MouseEnter																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has entered the area of the Pause toolbar button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTransportPause_MouseEnter(object sender, EventArgs e)
		{
			if(mnuTransportPause.Checked)
			{
				pnlTransportPause.BackColor =
					FromHex(ResourceMain.colorBackgroundActive);
			}
			else
			{
				pnlTransportPause.BackColor =
					FromHex(ResourceMain.colorBackgroundHighlight);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTransportPause_MouseLeave																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has left the area of the Pause toolbar button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTransportPause_MouseLeave(object sender, EventArgs e)
		{
			if(mnuTransportPause.Checked)
			{
				pnlTransportPause.BackColor =
					FromHex(ResourceMain.colorBackgroundActive);
			}
			else
			{
				pnlTransportPause.BackColor = FromHex(ResourceMain.colorBackground);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTransportPlay_MouseClick																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has been clicked over the Play button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTransportPlay_MouseClick(object sender, MouseEventArgs e)
		{
			mnuTransportPlay_Click(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTransportPlay_MouseEnter																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has entered the area of the Play toolbar button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTransportPlay_MouseEnter(object sender, EventArgs e)
		{
			if(mnuTransportPlay.Checked)
			{
				pnlTransportPlay.BackColor =
					FromHex(ResourceMain.colorBackgroundActive);
			}
			else
			{
				pnlTransportPlay.BackColor =
					FromHex(ResourceMain.colorBackgroundHighlight);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTransportPlay_MouseLeave																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has left the area of the Play toolbar button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTransportPlay_MouseLeave(object sender, EventArgs e)
		{
			if(mnuTransportPlay.Checked)
			{
				pnlTransportPlay.BackColor =
					FromHex(ResourceMain.colorBackgroundActive);
			}
			else
			{
				pnlTransportPlay.BackColor = FromHex(ResourceMain.colorBackground);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTransportStop_MouseClick																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has been clicked over the Stop button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTransportStop_MouseClick(object sender, MouseEventArgs e)
		{
			mnuTransportStop_Click(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTransportStop_MouseEnter																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has entered the area of the Stop toolbar button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTransportStop_MouseEnter(object sender, EventArgs e)
		{
			if(mnuTransportStop.Checked)
			{
				pnlTransportStop.BackColor =
					FromHex(ResourceMain.colorBackgroundActive);
			}
			else
			{
				pnlTransportStop.BackColor =
					FromHex(ResourceMain.colorBackgroundHighlight);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTransportStop_MouseLeave																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has left the area of the Stop toolbar button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTransportStop_MouseLeave(object sender, EventArgs e)
		{
			if(mnuTransportStop.Checked)
			{
				pnlTransportStop.BackColor =
					FromHex(ResourceMain.colorBackgroundActive);
			}
			else
			{
				pnlTransportStop.BackColor = FromHex(ResourceMain.colorBackground);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SafeAddFilename																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add a filename to the thumbnails gallery using UI thread safety.
		/// </summary>
		/// <param name="filename">
		/// Full path and filename of the item to add.
		/// </param>
		private void SafeAddFilename(string filename)
		{
			if(lvThumbs.InvokeRequired)
			{
				var d = new SafeCallDelegate(SafeAddFilename);
				lvThumbs.Invoke(d, new object[] { filename });
			}
			else
			{
				lvThumbs.Items.Add(filename);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SafeMessageWrite																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the status message using UI thread safety.
		/// </summary>
		/// <param name="message">
		/// Text of the message to display.
		/// </param>
		private void SafeMessageWrite(string message)
		{
			if(statusFrameSwitch.InvokeRequired)
			{
				var d = new SafeCallDelegate(SafeMessageWrite);
				statusFrameSwitch.Invoke(d, new object[] { message });
			}
			else
			{
				statusFrameSwitch.Items[0].Text = message;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SafePictureImageLocation																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the image location of the preview picture using UI thread safety.
		/// </summary>
		/// <param name="filename">
		/// Full path and filename of the image to set.
		/// </param>
		private void SafePictureImageLocation(string filename)
		{
			if(picImage.InvokeRequired)
			{
				var d = new SafeCallDelegate(SafePictureImageLocation);
				picImage.Invoke(d, new object[] { filename });
			}
			else
			{
				picImage.ImageLocation = filename;
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
		/// Raises the Activated event when the form has been activated.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnActivated(EventArgs e)
		{
			FlipbookItemControl item = null;

			base.OnActivated(e);
			lblTitle.ForeColor = FromHex(ResourceMain.colorTitleTextActive);
			picMinimize.Image = ResourceMain.WinControlH1;
			if(this.WindowState == FormWindowState.Maximized)
			{
				picMaximize.Image = ResourceMain.WinControlN1;
			}
			else
			{
				picMaximize.Image = ResourceMain.WinControlM1;
			}
			picClose.Image = ResourceMain.WinControlX1;
			item = pnlFlow.FirstSelectedItem;
			if(item != null)
			{
				pnlFlow.ScrollToItem(item);
			}
			mWindowActive = true;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnDeactivate																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the Deactivate event when the form is going to be deactivated.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnDeactivate(EventArgs e)
		{
			base.OnDeactivate(e);
			base.OnLeave(e);
			lblTitle.ForeColor = FromHex(ResourceMain.colorTitleTextInactive);
			picMinimize.Image = ResourceMain.WinControlH0;
			if(this.WindowState == FormWindowState.Maximized)
			{
				picMaximize.Image = ResourceMain.WinControlN0;
			}
			else
			{
				picMaximize.Image = ResourceMain.WinControlM0;
			}
			picClose.Image = ResourceMain.WinControlX0;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnEnter																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the Enter event when the form has received focus.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnLeave																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the Leave event when the form has lost focus.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnLeave(EventArgs e)
		{
			mWindowActive = false;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSizeChanged																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SizeChanged event when the size of the form has changed.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			if(this.WindowState == FormWindowState.Maximized)
			{
				if(mWindowActive)
				{
					picMaximize.Image = ResourceMain.WinControlN1;
				}
				else
				{
					picMaximize.Image = ResourceMain.WinControlN0;
				}
			}
			else
			{
				if(mWindowActive)
				{
					picMaximize.Image = ResourceMain.WinControlM1;
				}
				else
				{
					picMaximize.Image = ResourceMain.WinControlM0;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the frmFrameSwitch Item.
		/// </summary>
		public frmFrameFlipbook()
		{
			InitializeComponent();

			this.menuFrameSwitch.Renderer =
				new DarkThemeMenuRenderer(new DarkThemeMenuColorTable());
			//this.lvThumbs.SetRenderer(
			//	new Manina.Windows.Forms.ImageListViewRenderers.NoirRenderer());
			this.lvThumbs.SetRenderer(new NoirRenderer2());
			this.lvThumbs.ThumbnailSize = new Size(512, 512);

			mMenusThumbsize = new ToolStripMenuItem[]
			{
				mnuViewThumbsize96,
				mnuViewThumbsize128,
				mnuViewThumbsize256,
				mnuViewThumbsize512
			};

			mMenusTransport = new MenuControlItem[]
			{
				new MenuControlItem(mnuTransportBack, pnlTransportBack),
				new MenuControlItem(mnuTransportNext, pnlTransportNext),
				new MenuControlItem(mnuTransportPause, pnlTransportPause),
				new MenuControlItem(mnuTransportPlay, pnlTransportPlay),
				new MenuControlItem(mnuTransportStop, pnlTransportStop)
			};

			mMenusViewMode = new ToolStripMenuItem[]
			{
				mnuViewThumbnails,
				mnuViewImage
			};

			//	Form controls.
			pnlClose.MouseClick += pnlClose_MouseClick;
			pnlClose.MouseEnter += pnlClose_MouseEnter;
			pnlClose.MouseLeave += pnlClose_MouseLeave;
			picClose.MouseClick += pnlClose_MouseClick;
			picClose.MouseEnter += pnlClose_MouseEnter;
			picClose.MouseLeave += pnlClose_MouseLeave;
			pnlMaximize.MouseClick += pnlMaximize_MouseClick;
			pnlMaximize.MouseEnter += pnlMaximize_MouseEnter;
			pnlMaximize.MouseLeave += pnlMaximize_MouseLeave;
			picMaximize.MouseClick += pnlMaximize_MouseClick;
			picMaximize.MouseEnter += pnlMaximize_MouseEnter;
			picMaximize.MouseLeave += pnlMaximize_MouseLeave;
			pnlMinimize.MouseClick += pnlMinimize_MouseClick;
			pnlMinimize.MouseEnter += pnlMinimize_MouseEnter;
			pnlMinimize.MouseLeave += pnlMinimize_MouseLeave;
			picMinimize.MouseClick += pnlMinimize_MouseClick;
			picMinimize.MouseEnter += pnlMinimize_MouseEnter;
			picMinimize.MouseLeave += pnlMinimize_MouseLeave;
			pnlTitle.MouseDown += pnlTitle_MouseDown;
			pnlTitle.MouseMove += pnlTitle_MouseMove;
			pnlTitle.MouseUp += pnlTitle_MouseUp;

			pnlTransportBack.MouseClick += pnlTransportBack_MouseClick;
			pnlTransportBack.MouseEnter += pnlTransportBack_MouseEnter;
			pnlTransportBack.MouseLeave += pnlTransportBack_MouseLeave;
			pnlTransportNext.MouseClick += pnlTransportNext_MouseClick;
			pnlTransportNext.MouseEnter += pnlTransportNext_MouseEnter;
			pnlTransportNext.MouseLeave += pnlTransportNext_MouseLeave;
			pnlTransportPause.MouseClick += pnlTransportPause_MouseClick;
			pnlTransportPause.MouseEnter += pnlTransportPause_MouseEnter;
			pnlTransportPause.MouseLeave += pnlTransportPause_MouseLeave;
			pnlTransportPlay.MouseClick += pnlTransportPlay_MouseClick;
			pnlTransportPlay.MouseEnter += pnlTransportPlay_MouseEnter;
			pnlTransportPlay.MouseLeave += pnlTransportPlay_MouseLeave;
			pnlTransportStop.MouseClick += pnlTransportStop_MouseClick;
			pnlTransportStop.MouseEnter += pnlTransportStop_MouseEnter;
			pnlTransportStop.MouseLeave += pnlTransportStop_MouseLeave;

			mnuEditRunCommands.Click += mnuEditRunCommands_Click;
			mnuFileOpen.Click += mnuFileOpen_Click;
			mnuFileExportGIF.Click += mnuFileExportGIF_Click;
			mnuFileClose.Click += mnuFileClose_Click;
			mnuEditCaptureFrame.Click += mnuEditCaptureFrame_Click;
			mnuViewThumbsize96.Click += mnuViewThumbsize96_Click;
			mnuViewThumbsize128.Click += mnuViewThumbsize128_Click;
			mnuViewThumbsize256.Click += mnuViewThumbsize256_Click;
			mnuViewThumbsize512.Click += mnuViewThumbsize512_Click;
			mnuViewThumbnails.Click += mnuViewThumbnails_Click;
			mnuViewImage.Click += mnuViewImage_Click;
			mnuTransportBack.Click += mnuTransportBack_Click;
			mnuTransportNext.Click += mnuTransportNext_Click;
			mnuTransportStop.Click += mnuTransportStop_Click;
			mnuTransportPause.Click += mnuTransportPause_Click;
			mnuTransportPlay.Click += mnuTransportPlay_Click;

			mnuTransportBack.CheckedChanged += mnuTransport_CheckedChanged;
			mnuTransportNext.CheckedChanged += mnuTransport_CheckedChanged;
			mnuTransportPause.CheckedChanged += mnuTransport_CheckedChanged;
			mnuTransportPlay.CheckedChanged += mnuTransport_CheckedChanged;
			mnuTransportStop.CheckedChanged += mnuTransport_CheckedChanged;

			mnuViewThumbsize96.CheckedChanged += mnuViewThumbsize_CheckedChanged;
			mnuViewThumbsize128.CheckedChanged += mnuViewThumbsize_CheckedChanged;
			mnuViewThumbsize256.CheckedChanged += mnuViewThumbsize_CheckedChanged;
			mnuViewThumbsize512.CheckedChanged += mnuViewThumbsize_CheckedChanged;

			mnuViewThumbnails.CheckedChanged += mnuViewMode_CheckedChanged;
			mnuViewImage.CheckedChanged += mnuViewMode_CheckedChanged;

			pnlFlow.ItemClick += pnlFlow_ItemClick;
			lvThumbs.SelectionChanged += lvThumbs_SelectionChanged;
			lvThumbs.Dock = DockStyle.Fill;
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

}
