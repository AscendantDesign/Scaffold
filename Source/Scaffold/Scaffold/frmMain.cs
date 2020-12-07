//	frmMain.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

using static Scaffold.ScaffoldNodesUtil;
using static Scaffold.ScaffoldUtil;
using static SkiaSharpSvg.SvgAnimationUtil;

using SkiaSharpSvg;
using SkiaSharp;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	frmMain																																	*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Main form implementation.
	/// </summary>
	public partial class frmMain : Form
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//	** External Library Functions **
		[DllImport("user32.dll")]
		private static extern IntPtr CreateIconIndirect(ref IconInfo icon);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern bool DestroyIcon(IntPtr handle);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetIconInfo(IntPtr hIcon,
			ref IconInfo pIconInfo);

		//*-----------------------------------------------------------------------*
		//	** Members **
		/// <summary>
		/// On-board HTML control. Chromium compatible.
		/// </summary>
		HTMLControl htmlGeneric = null;
		/// <summary>
		/// Last known starting frame index on the Tools / Animation /
		/// Draw Frame [N] to HTML View function.
		/// </summary>
		private int mAnimationFrameToHtmlFrom = 0;
		/// <summary>
		/// Last known ending frame index on the Tools / Animation /
		/// Draw Frame [N] to HTML View function.
		/// </summary>
		private int mAnimationFrameToHtmlTo = -1;
		/// <summary>
		/// Value indicating whether the [Esc] key was pressed during an operation.
		/// </summary>
		private bool mEscPressed = false;
		///// <summary>
		///// Currently loaded decision tree node file.
		///// </summary>
		//private FileInfo mNodeFile = null;
		/// <summary>
		/// Tracking list for moving nodes.
		/// </summary>
		List<UndoItem> mNodeMoving = new List<UndoItem>();
		/// <summary>
		/// SVG library.
		/// </summary>
		private SKSvg mSVG = null;
		/// <summary>
		/// Currently loaded SVG file.
		/// </summary>
		private FileInfo mSVGFile = null;
		/// <summary>
		/// Dragging tool icon.
		/// </summary>
		private IconInfo mToolIcon;
		/// <summary>
		/// Dragging tool icon handle.
		/// </summary>
		private IntPtr mToolIconHandle;
		/// <summary>
		/// Action undo.
		/// </summary>
		private UndoStack mUndo = null;
		private UndoPack mUndoPack = null;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnDecisionTreeEditor_SelectedChanged																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The selected state of the decision tree editor button has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnDecisionTreeEditor_SelectedChanged(object sender,
			EventArgs e)
		{
			if(btnDecisionTreeEditor.Selected)
			{
				tctlDocument.SelectedTabName = "Decision";
				tctlTools.SelectedTabName = "Decision";
				//mnuFileOpenNode.Visible = true;
				//mnuFileSaveNode.Visible = true;
				//mnuFileSaveNodeAs.Visible = true;
				//mnuFileOpenSVG.Visible = false;
				//mnuFileSaveSVG.Visible = false;
				//mnuFileSaveSVGAs.Visible = false;
				//mnuFileOpenSVG.ShortcutKeys = Keys.None;
				//mnuFileSaveSVG.ShortcutKeys = Keys.None;
				//mnuFileSaveSVGAs.ShortcutKeys = Keys.None;
				//mnuFileOpenNode.ShortcutKeys = Keys.Control | Keys.O;
				//mnuFileSaveNode.ShortcutKeys = Keys.Control | Keys.S;
				//mnuFileSaveNodeAs.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
				this.Text = "Scaffold - Decision Tree Editor";
				mnuWindowDecision.Checked = true;
				mnuWindowSlide.Checked = false;
				mnuWindowHTMLViewer.Checked = false;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnHTML_SelectedChanged																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The selected state of the HTML viewer button has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnHTML_SelectedChanged(object sender, EventArgs e)
		{
			if(btnHTML.Selected)
			{
				tctlDocument.SelectedTabName = "HTML";
				tctlTools.SelectedTabName = "HTML";
				this.Text = "Scaffold - Viewer";
				mnuWindowDecision.Checked = false;
				mnuWindowSlide.Checked = false;
				mnuWindowHTMLViewer.Checked = true;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnSlideEditor_SelectedChanged																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The selected state of the slide editor button has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnSlideEditor_SelectedChanged(object sender, EventArgs e)
		{
			if(btnSlideEditor.Selected)
			{
				tctlDocument.SelectedTabName = "Slide";
				tctlTools.SelectedTabName = "Slide";
				//mnuFileOpenSVG.Visible = true;
				//mnuFileSaveSVG.Visible = true;
				//mnuFileSaveSVGAs.Visible = true;
				//mnuFileOpenNode.Visible = false;
				//mnuFileSaveNode.Visible = false;
				//mnuFileSaveNodeAs.Visible = false;
				//mnuFileOpenNode.ShortcutKeys = Keys.None;
				//mnuFileSaveNode.ShortcutKeys = Keys.None;
				//mnuFileSaveNodeAs.ShortcutKeys = Keys.None;
				//mnuFileOpenSVG.ShortcutKeys = Keys.Control | Keys.O;
				//mnuFileSaveSVG.ShortcutKeys = Keys.Control | Keys.S;
				//mnuFileSaveSVGAs.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
				this.Text = "Scaffold - Slide Editor";
				mnuWindowDecision.Checked = false;
				mnuWindowSlide.Checked = true;
				mnuWindowHTMLViewer.Checked = false;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ChatbotEmulator																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Run the chatbot emulator from an optional starting node.
		/// </summary>
		/// <param name="startNodeTicket">
		/// Global identification of the starting node from which to start. If
		/// blank, the first node of type 'Start' will be selected.
		/// </param>
		private void ChatbotEmulator(string startNodeTicket = "")
		{
			string appPath = System.IO.Path.GetDirectoryName(
				System.Reflection.Assembly.GetEntryAssembly().Location);
			bool bContinue = true;
			StringBuilder builder = new StringBuilder();
			string filename = Path.Combine(Path.GetTempPath(),
				Guid.NewGuid().ToString("D") + ".html");
			ProcessStartInfo processInfo = null;

			if(NodeFileInfo == null)
			{
				if(MessageBox.Show("Please save your node file before continuing.",
					"Chatbot Emulator", MessageBoxButtons.OKCancel) == DialogResult.OK)
				{
					mnuFileSaveNodeAs_Click(null, null);
				}
				else
				{
					bContinue = false;
				}
			}
			if(NodeFileInfo == null)
			{
				bContinue = false;
			}

			if(bContinue)
			{
				//	TODO: Modify chatbot.js to support media on questions and responses.
				statMessage.Text = "Preparing Chatbot Emulator...";
				this.Refresh();

				//	Steps for creating the file.
				//	HTML5 header.
				builder.Append(ResourceMain.htmlHeaderBasic);
				//	Embedded jQuery.
				builder.Append(ResourceMain.htmljQueryEmbedded);
				//	Script element including serialized node file assigned to var
				builder.Append("<script type=\"text/javascript\">");
				//	startNode.
				builder.Append("var startNodeTicket = ");
				if(startNodeTicket?.Length > 0)
				{
					builder.Append($"\"{startNodeTicket}\";");
				}
				else
				{
					builder.Append("\"\";");
				}
				//	chatbotdata.
				builder.Append("var chatbotdata = ");
				builder.Append(
					NodeDataCollection.SerializeData(nodeControl.NodeFile, true,
					NodeFileInfo.DirectoryName));
				builder.Append(";\r\n");
				//	Embedded Chatbot.js.
				builder.Append(
					File.ReadAllText(Path.Combine(appPath, "RuntimeData/chatbot.js")));
				builder.Append("</script>");
				//	Embedded Styles.
				builder.Append("<style type=\"text/css\">");
				builder.Append(File.ReadAllText(
					Path.Combine(appPath, "RuntimeData/chatbotdefault.css")));
				builder.Append("</style>");
				//	Title, /head, body, window div, h1, chatContainer, and chatPanel.
				//	Sample content to close out the file.
				builder.Append(File.ReadAllText(
					Path.Combine(appPath, "RuntimeData/chatbotlowerbody.html")));
				File.WriteAllText(filename, builder.ToString());
				processInfo = new ProcessStartInfo();
				processInfo.FileName = filename;
				processInfo.UseShellExecute = true;
				Process.Start(processInfo);

				GC.Collect();

				statMessage.Text = "Chatbot Emulator started in default browser...";
			}
			else
			{
				statMessage.Text = "Chatbot Emulator cancelled...";
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* frmMain_KeyDown																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The form has received a keypress.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Key event arguments.
		/// </param>
		private void frmMain_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Escape && mToolHandleActive)
			{
				ReleaseToolCursor();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetNodeControlMouseLocation																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the current mouse location relative to the node control window.
		/// </summary>
		/// <returns>
		/// Current relative mouse location over the node control window.
		/// </returns>
		private Point GetNodeControlMouseLocation()
		{
			Point mousePosition = Control.MousePosition;
			PointF controlLocation = PointF.Empty;

			controlLocation = ScaleDrawing(new Point(
				mousePosition.X - this.Location.X -
				panelWindowControl.PanelLeft - nodeControl.Location.X +
				nodeControl.HorizontalScroll.Value,
				mousePosition.Y - this.Location.Y -
				panelWindowControl.PanelTop - nodeControl.Location.Y +
				nodeControl.VerticalScroll.Value - this.MainMenuHeight),
				nodeControl.DrawingScale);
			return Point.Round(controlLocation);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ImageOffset																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Calculate the current image offset location.
		/// </summary>
		/// <param name="col">
		/// X index from upper left.
		/// </param>
		/// <param name="row">
		/// Y index from upper left.
		/// </param>
		/// <param name="margin">
		/// General margin applied to the image.
		/// </param>
		/// <param name="width">
		/// Width of each column.
		/// </param>
		/// <param name="height">
		/// Height of each row.
		/// </param>
		/// <returns>
		/// Image offset, in pixels.
		/// </returns>
		private Point ImageOffset(int col, int row, int margin,
			int width, int height)
		{
			Point result =
				new Point(
					margin + (col * width) + (col * margin),
					margin + (row * height) + (row * margin));
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* InitializeDocumentProperties																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Initialize document properties if name or ticket are blank.
		/// </summary>
		private void InitializeDocumentProperties()
		{
			int index = 0;
			string value = "";

			if(nodeControl.NodeFile.Name.Length == 0)
			{
				if(NodeFileInfo != null)
				{
					value = NodeFileInfo.Name;
					//	Remove extension for default name.
					index = NodeFileInfo.Name.IndexOf('.');
					if(index > -1)
					{
						value = value.Substring(0, index);
					}
					nodeControl.NodeFile.Name = value;
				}
			}
			if(nodeControl.NodeFile.Ticket.Length == 0)
			{
				nodeControl.NodeFile.Ticket = Guid.NewGuid().ToString("D").ToLower();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditNodeAddAudio_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Node / Add Audio to Selected menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditNodeAddAudio_Click(object sender, EventArgs e)
		{
			bool bContinue = true;
			StringBuilder builder = new StringBuilder();
			int count = 0;
			OpenFileDialog dialog = null;
			frmLinkEmbed dialogEmbed = null;
			int existing = 0;
			FileInfo file = null;
			int index = 0;
			NodeItem node = null;
			List<NodeItem> nodes = nodeControl.SelectionQueue;
			string relativeFilename = "";
			string ticket = "";

			if(nodes.Count > 0)
			{
				//	Audio can be added or updated.
				existing = nodes.Count(x =>
					x.Properties.Exists(y => y.Name == "MediaAudio" && y.Value != null));
				if(existing > 0)
				{
					bContinue = MessageBox.Show($"There are {existing} selected nodes that " +
					"already have audio definitions. Do you wish to overwrite those " +
					"entries?", "Add Audio to Selected Nodes",
					MessageBoxButtons.YesNo) == DialogResult.Yes;
				}
				if(bContinue)
				{
					//	Overwrite the media value in each selected node.
					dialog = new OpenFileDialog();
					dialog.CheckFileExists = true;
					dialog.CheckPathExists = true;
					dialog.DereferenceLinks = true;
					dialog.Filter =
						"Common Audio Files " +
						"(*.m4a;*.flac;*.mp3;*.wav;*.wma;*.aac)|" +
						"*.m4a;*.flac;*.mp3;*.wav;*.wma;*.aac|" +
						"MPEG-4 Audio File (*.m4a)|*.m4a|" +
						"Free Lossless Audio Codec (*.flac)|*.flac|" +
						"MPEG-3 Audio File (*.mp3)|*.mp3|" +
						"Waveform Audio File (*.wav)|*.wav|" +
						"Windows Media Audio (*.wma)|*.wma|" +
						"Advanced Audio Coding (*.aac)|*.aac|" +
						"All Files (*.*)|*.*";
					dialog.FilterIndex = 0;
					dialog.Multiselect = false;
					dialog.ShowReadOnly = true;
					dialog.SupportMultiDottedExtensions = true;
					dialog.Title = "Select Audio File";
					dialog.ValidateNames = true;
					if(dialog.ShowDialog() == DialogResult.OK)
					{
						//	File selection complete.
						file = new FileInfo(dialog.FileName);
						relativeFilename = RelativeFilename(NodeFileInfo, file);
						dialogEmbed = new frmLinkEmbed();
						CenterOver(this, dialogEmbed);
						dialogEmbed.LinkFilename = relativeFilename;
						if(dialogEmbed.ShowDialog() == DialogResult.OK)
						{
							//	Prepare the resource.
							relativeFilename = dialogEmbed.LinkFilename;
							//	Assign the resource ticket to each selected node.
							index = 0;
							count = nodes.Count;
							node = nodes[index];
							ticket = CreateAudioResource(node, file,
								relativeFilename, dialogEmbed.Embed);
							if(ticket?.Length > 0)
							{
								//	All remaining selected nodes.
								for(index = 1; index < count; index ++)
								{
									node = nodes[index];
									node.Properties["MediaAudio"].Value = ticket;
									CreateAudioIcon(node);
								}
							}
							statMessage.Text = "Audio Resource Assigned to Nodes...";
						}
						else
						{
							statMessage.Text = "Audio Resource Assignment Cancelled...";
						}
					}
					else
					{
						statMessage.Text = "Select Audio Cancelled...";
					}
				}
				else
				{
					statMessage.Text = "Add Audio Cancelled...";
				}
			}
			else
			{
				//	No nodes are selected.
				MessageBox.Show("Please try selecting one or more nodes before " +
					"adding audio reference.", "Add Audio to Selected Nodes");
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditNodeAddImage_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Node / Add Image to Selected menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private async void mnuEditNodeAddImage_Click(object sender, EventArgs e)
		{
			bool bContinue = true;
			int count = 0;
			OpenFileDialog dialog = null;
			frmLinkEmbed dialogEmbed = null;
			int existing = 0;
			FileInfo file = null;
			int index = 0;
			NodeItem node = null;
			List<NodeItem> nodes = nodeControl.SelectionQueue;
			string relativeFilename = "";
			object thumbnail = null;
			string ticket = "";

			if(nodes.Count > 0)
			{
				//	Image can be added or updated.
				existing = nodes.Count(x =>
					x.Properties.Exists(y => y.Name == "MediaImage" && y.Value != null));
				if(existing > 0)
				{
					bContinue = MessageBox.Show(
					$"There are {existing} selected nodes that " +
					"already have image definitions. Do you wish to overwrite those " +
					"entries?", "Add Image to Selected Nodes",
					MessageBoxButtons.YesNo) == DialogResult.Yes;
				}
				if(bContinue)
				{
					//	Overwrite the media value in each selected node.
					dialog = new OpenFileDialog();
					dialog.CheckFileExists = true;
					dialog.CheckPathExists = true;
					dialog.DereferenceLinks = true;
					dialog.Filter =
						"Common Image Files " +
						"(*.jpg;*.jpeg;*.png;*.bmp;*.webp;*.tiff)|" +
						"*.jpg;*.jpeg;*.png;*.bmp;*.webp;*.tiff|" +
						"Joint Photographic Experts Group (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
						"Portable Network Graphics (*.png)|*.png|" +
						"Windows Bitmap (*.bmp)|*.bmp|" +
						"Web picture format (*.webp)|*.webp|" +
						"Tagged image file format (*.tiff)|*.tiff|" +
						"All Files (*.*)|*.*";
					dialog.FilterIndex = 0;
					dialog.Multiselect = false;
					dialog.ShowReadOnly = true;
					dialog.SupportMultiDottedExtensions = true;
					dialog.Title = "Select Image File";
					dialog.ValidateNames = true;
					if(dialog.ShowDialog() == DialogResult.OK)
					{
						//	File selection complete.
						file = new FileInfo(dialog.FileName);
						relativeFilename = RelativeFilename(NodeFileInfo, file);
						dialogEmbed = new frmLinkEmbed();
						CenterOver(this, dialogEmbed);
						dialogEmbed.LinkFilename = relativeFilename;
						if(dialogEmbed.ShowDialog() == DialogResult.OK)
						{
							//	Prepare the resource.
							relativeFilename = dialogEmbed.LinkFilename;
							//	Assign the resource ticket to each selected node.
							index = 0;
							count = nodes.Count;
							node = nodes[index];
							ticket = CreateImageResource(node, file,
								relativeFilename, dialogEmbed.Embed);
							if(node.Properties.Exists(p => p.Name == "ThumbImage"))
							{
								thumbnail =
									node.Properties.First(p => p.Name == "ThumbImage").Value;
							}
							if(ticket?.Length > 0)
							{
								//	All remaining selected nodes.
								for(index = 1; index < count; index++)
								{
									node = nodes[index];
									node.Properties["MediaImage"].Value = ticket;
									if(thumbnail != null)
									{
										CreateImageThumbnail(node, (Bitmap)thumbnail);
									}
								}
							}
							await Task.Delay(500);
							nodeControl.Invalidate();
							statMessage.Text = "Image Resource Assigned to Nodes...";
						}
						else
						{
							statMessage.Text = "Image Resource Assignment Cancelled...";
						}
					}
					else
					{
						statMessage.Text = "Select Image Cancelled...";
					}
				}
				else
				{
					statMessage.Text = "Add Image Cancelled...";
				}
			}
			else
			{
				//	No nodes are selected.
				MessageBox.Show("Please try selecting one or more nodes before " +
					"adding image reference.", "Add Image to Selected Nodes");
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditNodeAddLink_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Node / Add Link to Selected menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditNodeAddLink_Click(object sender, EventArgs e)
		{
			bool bContinue = true;
			int count = 0;
			frmInputBox dialogInput = null;
			int existing = 0;
			int index = 0;
			NodeItem node = null;
			List<NodeItem> nodes = nodeControl.SelectionQueue;
			string ticket = "";

			if(nodes.Count > 0)
			{
				//	Link can be added or updated.
				existing = nodes.Count(x =>
					x.Properties.Exists(y => y.Name == "MediaLink" && y.Value != null));
				if(existing > 0)
				{
					bContinue = MessageBox.Show(
					$"There are {existing} selected nodes that " +
					"already have link definitions. Do you wish to overwrite those " +
					"entries?", "Add Link to Selected Nodes",
					MessageBoxButtons.YesNo) == DialogResult.Yes;
				}
				if(bContinue)
				{
					//	Overwrite the media value in each selected node.
					dialogInput = new frmInputBox();
					dialogInput.Prompt = "Please enter the link:";
					if(dialogInput.ShowDialog() == DialogResult.OK &&
						dialogInput.Text?.Length > 0)
					{
						//	Assign the resource ticket to each selected node.
						index = 0;
						count = nodes.Count;
						node = nodes[index];
						ticket = CreateLinkResource(node, dialogInput.Text);
						if(ticket?.Length > 0)
						{
							//	All remaining selected nodes.
							for(index = 1; index < count; index++)
							{
								node = nodes[index];
								node.Properties["MediaLink"].Value = ticket;
								CreateLinkIcon(node);
							}
						}
					}
				}
				else
				{
					statMessage.Text = "Add Link Cancelled...";
				}
			}
			else
			{
				//	No nodes are selected.
				MessageBox.Show("Please try selecting one or more nodes before " +
					"adding link reference.", "Add Link to Selected Nodes");
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditNodeAddResources_Click																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Node / Add One Or More Resources To File menu option has
		/// been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditNodeAddResources_Click(object sender, EventArgs e)
		{
			string basePath = "";
			bool bContinue = true;
			bool bEmbed = true;
			OpenFileDialog dialog = null;
			frmLinkEmbed dialogEmbed = null;
			FileInfo file = null;
			string[] filenames = null;
			string mediaType = "";
			string relativeFilename = "";
			ResourceCollection resources = null;

			//	Overwrite the media value in each selected node.
			if(NodeFileInfo == null)
			{
				if(MessageBox.Show("Please save the file first.",
					"Add Resources To File",
					MessageBoxButtons.OKCancel) == DialogResult.OK)
				{
					mnuFileSaveNodeAs_Click(sender, e);
				}
			}
			bContinue = (NodeFileInfo != null);

			if(bContinue)
			{
				dialog = new OpenFileDialog();
				dialog.CheckFileExists = true;
				dialog.CheckPathExists = true;
				dialog.DereferenceLinks = true;
				dialog.Filter =
					"Common Resource Files (" +
					"*.jpg;*.jpeg;*.png;*.bmp;*.webp;*.tiff;" +
					"*.m4a;*.flac;*.mp3;*.wav;*.wma;*.aac;" +
					"*.mp4;*.webm;*.wmv;*.mov;*.avi" +
					")|" +
					"*.jpg;*.jpeg;*.png;*.bmp;*.webp;*.tiff;" +
					"*.m4a;*.flac;*.mp3;*.wav;*.wma;*.aac;" +
					"*.mp4;*.webm;*.wmv;*.mov;*.avi|" +
					"Advanced Audio Coding (*.aac)|*.aac|" +
					"Apple QuickTime Movie (*.mov)|*.mov|" +
					"Audio Video Interleave (*.avi)|*.avi|" +
					"Free Lossless Audio Codec (*.flac)|*.flac|" +
					"Joint Photographic Experts Group (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
					"Moving Pictures Experts Group (*.mp4)|*.mp4|" +
					"MPEG-3 Audio File (*.mp3)|*.mp3|" +
					"MPEG-4 Audio File (*.m4a)|*.m4a|" +
					"Portable Network Graphics (*.png)|*.png|" +
					"Tagged image file format (*.tiff)|*.tiff|" +
					"Waveform Audio File (*.wav)|*.wav|" +
					"Web Movie Format (*.webm)|*.webm|" +
					"Web picture format (*.webp)|*.webp|" +
					"Windows Bitmap (*.bmp)|*.bmp|" +
					"Windows Media Audio (*.wma)|*.wma|" +
					"Windows Media Video (*.wmv)|*.wmv|" +
					"All Files (*.*)|*.*";
				dialog.FilterIndex = 0;
				dialog.Multiselect = true;
				dialog.ShowReadOnly = true;
				dialog.SupportMultiDottedExtensions = true;
				dialog.Title = "Add Resources To File";
				dialog.ValidateNames = true;
				if(dialog.ShowDialog() == DialogResult.OK)
				{
					//	File selection complete.
					filenames = dialog.FileNames;
					if(filenames.Length > 0)
					{
						dialogEmbed = new frmLinkEmbed();
						CenterOver(this, dialogEmbed);
						dialogEmbed.LinkFilename = filenames[0];
						dialogEmbed.CanEditRelativeName = false;
						if(dialogEmbed.ShowDialog() == DialogResult.OK)
						{
							//	Prepare the resource.
							//	Assign the resource ticket to each selected node.
							bEmbed = dialogEmbed.Embed;
						}
						else
						{
							statMessage.Text = "Image Resource Assignment Cancelled...";
							bContinue = false;
						}
					}
					if(bContinue)
					{
						basePath = NodeFileInfo.DirectoryName;
						resources = nodeControl.Resources;
						foreach(string filename in filenames)
						{
							file = new FileInfo(filename);
							relativeFilename = RelativeFilename(NodeFileInfo, file);

							//	Assign the resource ticket to each selected node.
							mediaType = GetMediaTypeName(file);
							switch(mediaType)
							{
								case "MediaAudio":
									CreateAudioResource(file, relativeFilename,
										bEmbed);
									break;
								case "MediaImage":
									CreateImageResource(file, relativeFilename,
										bEmbed);
									break;
								case "MediaVideo":
									CreateVideoResource(file, relativeFilename,
										bEmbed);
									break;
							}
						}
						statMessage.Text = "Resources Added to File...";
					}
				}
				else
				{
					statMessage.Text = "Select Image Cancelled...";
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditNodeAddVideo_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Node / Add Image to Selected menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private async void mnuEditNodeAddVideo_Click(object sender, EventArgs e)
		{
			bool bContinue = true;
			int count = 0;
			OpenFileDialog dialog = null;
			frmLinkEmbed dialogEmbed = null;
			int existing = 0;
			FileInfo file = null;
			int index = 0;
			NodeItem node = null;
			List<NodeItem> nodes = nodeControl.SelectionQueue;
			object thumbnail = null;
			string relativeFilename = "";
			string ticket = "";

			if(nodes.Count > 0)
			{
				//	Video can be added or updated.
				existing = nodes.Count(x =>
					x.Properties.Exists(y => y.Name == "MediaVideo" && y.Value != null));
				if(existing > 0)
				{
					bContinue = MessageBox.Show(
					$"There are {existing} selected nodes that " +
					"already have video definitions. Do you wish to overwrite those " +
					"entries?", "Add Video to Selected Nodes",
					MessageBoxButtons.YesNo) == DialogResult.Yes;
				}
				if(bContinue)
				{
					//	Overwrite the media value in each selected node.
					dialog = new OpenFileDialog();
					dialog.CheckFileExists = true;
					dialog.CheckPathExists = true;
					dialog.DereferenceLinks = true;
					dialog.Filter =
						"Common Video Files " +
						"(*.mp4;*.webm;*.wmv;*.mov;*.avi)|" +
						"*.mp4;*.webm;*.wmv;*.mov;*.avi|" +
						"Moving Pictures Experts Group (*.mp4)|*.mp4|" +
						"Web Movie Format (*.webm)|*.webm|" +
						"Windows Media Video (*.wmv)|*.wmv|" +
						"Apple QuickTime Movie (*.mov)|*.mov|" +
						"Audio Video Interleave (*.avi)|*.avi|" +
						"All Files (*.*)|*.*";
					dialog.FilterIndex = 0;
					dialog.Multiselect = false;
					dialog.ShowReadOnly = true;
					dialog.SupportMultiDottedExtensions = true;
					dialog.Title = "Select Video File";
					dialog.ValidateNames = true;
					if(dialog.ShowDialog() == DialogResult.OK)
					{
						//	File selection complete.
						file = new FileInfo(dialog.FileName);
						relativeFilename = RelativeFilename(NodeFileInfo, file);
						dialogEmbed = new frmLinkEmbed();
						CenterOver(this, dialogEmbed);
						dialogEmbed.LinkFilename = relativeFilename;
						if(dialogEmbed.ShowDialog() == DialogResult.OK)
						{
							//	Prepare the resource.
							relativeFilename = dialogEmbed.LinkFilename;
							//	Assign the resource ticket to each selected node.
							index = 0;
							count = nodes.Count;
							node = nodes[index];
							ticket = CreateVideoResource(node, file,
								relativeFilename, dialogEmbed.Embed);
							if(node.Properties.Exists(p => p.Name == "ThumbVideo"))
							{
								thumbnail =
									node.Properties.First(p => p.Name == "ThumbVideo").Value;
							}
							if(ticket?.Length > 0)
							{
								//	All remaining selected nodes.
								for(index = 1; index < count; index++)
								{
									node = nodes[index];
									node.Properties["MediaVideo"].Value = ticket;
									if(thumbnail != null)
									{
										CreateVideoThumbnail(node, (Bitmap)thumbnail);
									}
								}
							}
							await Task.Delay(500);
							nodeControl.Invalidate();
							statMessage.Text = "Video Resource Assigned to Nodes...";
						}
						else
						{
							statMessage.Text = "Video Resource Assignment Cancelled...";
						}
					}
					else
					{
						statMessage.Text = "Select Video Cancelled...";
					}
				}
				else
				{
					statMessage.Text = "Add Video Cancelled...";
				}
			}
			else
			{
				//	No nodes are selected.
				MessageBox.Show("Please try selecting one or more nodes before " +
					"adding video.", "Add Video to Selected Nodes");
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditAlignBottom_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Align and Distribute / Align Bottom menu option has been
		/// clicked. Align the selected items with a common bottom coordinate.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditAlignBottom_Click(object sender, EventArgs e)
		{
			float bottom = 0f;
			int count = 0;
			int index = 0;
			float offset = 0f;
			NodeItem node = null;

			count = nodeControl.SelectionQueue.Count;
			if(count > 1)
			{
				node = nodeControl.SelectionQueue[0];
				bottom = NodeItem.GetBounds(node).Bottom;
				for(index = 1; index < count; index ++)
				{
					node = nodeControl.SelectionQueue[index];
					offset = bottom - NodeItem.GetBounds(node).Bottom;
					if(offset != 0f)
					{
						node.Y += offset;
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditAlignCenter_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Align and Distribute / Align Center Horizontally menu option
		/// has been clicked. Align the selected items with a common center
		/// coordinate.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditAlignCenter_Click(object sender, EventArgs e)
		{
			RectangleF bounds = RectangleF.Empty;
			float center = 0f;
			int count = 0;
			int index = 0;
			float offset = 0f;
			NodeItem node = null;

			count = nodeControl.SelectionQueue.Count;
			if(count > 1)
			{
				node = nodeControl.SelectionQueue[0];
				bounds = NodeItem.GetBounds(node);
				center = bounds.X + (bounds.Width / 2f);
				for(index = 1; index < count; index++)
				{
					node = nodeControl.SelectionQueue[index];
					bounds = NodeItem.GetBounds(node);
					offset = center - (bounds.X + (bounds.Width / 2f));
					if(offset != 0f)
					{
						node.X += offset;
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditAlignHorizontal_Click																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Align and Distribute / Distribute Horizontally menu option
		/// has been clicked. Space the selected items horizontally at the same
		/// distance.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditAlignHorizontal_Click(object sender, EventArgs e)
		{
			RectangleF bounds = RectangleF.Empty;
			int count = 0;
			int index = 0;
			float offset = 0f;
			NodeItem node = null;
			NodeItem nodePrev = null;
			List<NodeItem> nodes = null;
			float space = 0f;

			count = nodeControl.SelectionQueue.Count;
			if(count > 2)
			{
				//	Local list is ordered by X.
				nodes = nodeControl.SelectionQueue.OrderBy(x => x.X).ToList();
				//	Get the space between the two objects.
				nodePrev = nodes[0];
				node = nodes[1];
				space = node.X - (nodePrev.X + nodePrev.Width);
				nodePrev = node;
				for(index = 2; index < count; index ++)
				{
					node = nodes[index];
					offset = space - (node.X - (nodePrev.X + nodePrev.Width));
					if(offset != 0f)
					{
						node.X += offset;
					}
					nodePrev = node;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditAlignLeft_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Align and Distribute / Align Left menu option has been
		/// clicked. Align the selected items on their left sides.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditAlignLeft_Click(object sender, EventArgs e)
		{
			int count = 0;
			int index = 0;
			float left = 0f;
			NodeItem node = null;

			count = nodeControl.SelectionQueue.Count;
			if(count > 1)
			{
				node = nodeControl.SelectionQueue[0];
				left = node.X;
				for(index = 1; index < count; index++)
				{
					node = nodeControl.SelectionQueue[index];
					node.X = left;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditAlignMiddle_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Align and Distribute / Align Middle Vertically menu option
		/// has been clicked. Align selected items in their vertical centers.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditAlignMiddle_Click(object sender, EventArgs e)
		{
			RectangleF bounds = RectangleF.Empty;
			int count = 0;
			int index = 0;
			float middle = 0f;
			float offset = 0f;
			NodeItem node = null;

			count = nodeControl.SelectionQueue.Count;
			if(count > 1)
			{
				node = nodeControl.SelectionQueue[0];
				bounds = NodeItem.GetBounds(node);
				middle = bounds.Y + (bounds.Height / 2f);
				for(index = 1; index < count; index++)
				{
					node = nodeControl.SelectionQueue[index];
					bounds = NodeItem.GetBounds(node);
					offset = middle - (bounds.Y + (bounds.Height / 2f));
					if(offset != 0f)
					{
						node.Y += offset;
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditAlignRight_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Align and Distribute / Align Right menu option has been
		/// clicked. Align selected items along their right sides.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditAlignRight_Click(object sender, EventArgs e)
		{
			int count = 0;
			int index = 0;
			float offset = 0f;
			NodeItem node = null;
			float right = 0f;

			count = nodeControl.SelectionQueue.Count;
			if(count > 1)
			{
				node = nodeControl.SelectionQueue[0];
				right = NodeItem.GetBounds(node).Right;
				for(index = 1; index < count; index++)
				{
					node = nodeControl.SelectionQueue[index];
					offset = right - NodeItem.GetBounds(node).Right;
					if(offset != 0f)
					{
						node.X += offset;
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditAlignTop_Click																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Align and Distribute / Align Top menu option has been
		/// clicked.
		/// Align selected items along their top sides.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditAlignTop_Click(object sender, EventArgs e)
		{
			int count = 0;
			int index = 0;
			NodeItem node = null;
			float top = 0f;

			count = nodeControl.SelectionQueue.Count;
			if(count > 1)
			{
				node = nodeControl.SelectionQueue[0];
				top = node.Y;
				for(index = 1; index < count; index++)
				{
					node = nodeControl.SelectionQueue[index];
					node.Y = top;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditAlignVertical_Click																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Align and Distribute / Distribute Vertically menu option has
		/// been clicked. Distribute selected items with identical vertical
		/// spacing.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditAlignVertical_Click(object sender, EventArgs e)
		{
			RectangleF bounds = RectangleF.Empty;
			int count = 0;
			int index = 0;
			float offset = 0f;
			NodeItem node = null;
			NodeItem nodePrev = null;
			List<NodeItem> nodes = null;
			float space = 0f;

			count = nodeControl.SelectionQueue.Count;
			if(count > 2)
			{
				//	Local list is ordered by Y.
				nodes = nodeControl.SelectionQueue.OrderBy(y => y.Y).ToList();
				//	Get the space between the two objects.
				nodePrev = nodes[0];
				node = nodes[1];
				space = node.Y - (nodePrev.Y + nodePrev.Height);
				nodePrev = node;
				for(index = 2; index < count; index++)
				{
					node = nodes[index];
					offset = space - (node.Y - (nodePrev.Y + nodePrev.Height));
					if(offset != 0f)
					{
						node.Y += offset;
					}
					nodePrev = node;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditFind_Click																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Find menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditFind_Click(object sender, EventArgs e)
		{
			frmFind form = new frmFind();
			NodeItem node = null;
			List<NodeItem> selected = null;

			if(form.ShowDialog() == DialogResult.OK)
			{
				selected = nodeControl.SelectionQueue.ToList();
				foreach(NodeItem nodeItem in selected)
				{
					nodeItem.Selected = false;
				}
				foreach(ListViewItem listItem in form.SelectedItems)
				{
					node = NodeFileObject.Nodes.
						FirstOrDefault(x => x.Ticket == ((NodeItem)listItem.Tag).Ticket);
					if(node != null)
					{
						node.Selected = true;
					}
				}
				nodeControl.NeedsInvalidate = true;
				//nodeControl.Invalidate();
				//nodeControl.Refresh();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditNodeColor_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Node / Color Selected Background menu option has been
		/// clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditNodeColor_Click(object sender, EventArgs e)
		{
			nodeControl.ChangeSelectedNodesColor();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditNodeColorText_Click																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Node / Color Selected Text menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditNodeColorText_Click(object sender, EventArgs e)
		{
			nodeControl.ChangeSelectedNodesTextColor();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditNodeDuplicate_Click																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Node / Duplicate Selected menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditNodeDuplicate_Click(object sender, EventArgs e)
		{
			nodeControl.DuplicateSelectedNodes();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditNodeRemoveAudio_Click																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Node / Remove Audio From Selected menu option has been
		/// clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditNodeRemoveAudio_Click(object sender, EventArgs e)
		{
			bool bDeleteResource = false;
			int count = 0;
			List<NodeItem> nodes = nodeControl.SelectionQueue;
			PropertyItem property = null;
			ResourceItem resource = null;
			List<string> resourceTickets = new List<string>();

			if(nodes.Count > 0)
			{
				foreach(NodeItem node in nodes)
				{
					if(PropertyExists(node, "MediaAudio"))
					{
						count++;
					}
				}
				if(count > 0)
				{
					if(MessageBox.Show(
						$"Remove audio from {count} selected nodes?",
						"Remove Audio From Selected Nodes", MessageBoxButtons.YesNo) ==
						DialogResult.Yes)
					{
						bDeleteResource = (MessageBox.Show(
							"Also delete associated resource if no longer in use?",
							"Remove Audio From Selected Nodes", MessageBoxButtons.YesNo) ==
							DialogResult.Yes);
						foreach(NodeItem node in nodes)
						{
							property = node.Properties["MediaAudio"];
							if(property != null)
							{
								//	This item has a media reference.
								if(bDeleteResource)
								{
									if(!resourceTickets.Exists(x =>
										x == property.Value.ToString().ToLower()))
									{
										//	Keep a reference to this resource for 
										resourceTickets.Add(property.StringValue().ToLower());
									}
								}
								node.Properties.Remove(property);
							}
							//	Remove the runtime icon.
							property = node.Properties.FirstOrDefault(x =>
								x.Name == "IconAudio");
							if(property != null)
							{
								node.Properties.Remove(property);
							}
						}
						if(bDeleteResource && resourceTickets.Count > 0)
						{
							//	Check all nodes for reference to the specified resource.
							foreach(string ticket in resourceTickets)
							{
								bDeleteResource = true;
								foreach(NodeItem node in nodeControl.Nodes)
								{
									if(node.Properties.Exists(x =>
										x.Name == "MediaAudio" &&
										x.StringValue().ToLower() == ticket))
									{
										//	This resource is still in use.
										bDeleteResource = false;
										break;
									}
								}
								if(bDeleteResource)
								{
									//	Delete this resource from the file resources collection.
									resource = nodeControl.Resources.FirstOrDefault(x =>
										x.Ticket.ToLower() == ticket);
									if(resource != null)
									{
										nodeControl.Resources.Remove(resource);
									}
								}
							}
						}
					}
				}
				else
				{
					MessageBox.Show(
						"There were no audio resources attached to any of the " +
						"selected nodes.", "Remove Audio From Selected Nodes");
				}
				nodeControl.Invalidate();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditNodeRemoveImage_Click																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Node / Remove Image From Selected menu option has been
		/// clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditNodeRemoveImage_Click(object sender, EventArgs e)
		{
			bool bDeleteResource = false;
			int count = 0;
			List<NodeItem> nodes = nodeControl.SelectionQueue;
			PropertyItem property = null;
			ResourceItem resource = null;
			List<string> resourceTickets = new List<string>();

			if(nodes.Count > 0)
			{
				foreach(NodeItem node in nodes)
				{
					if(PropertyExists(node, "MediaImage"))
					{
						count++;
					}
				}
				if(count > 0)
				{
					if(MessageBox.Show(
						$"Remove image from {count} selected nodes?",
						"Remove Image From Selected Nodes", MessageBoxButtons.YesNo) ==
						DialogResult.Yes)
					{
						bDeleteResource = (MessageBox.Show(
							"Also delete associated resource if no longer in use?",
							"Remove Image From Selected Nodes", MessageBoxButtons.YesNo) ==
							DialogResult.Yes);
						foreach(NodeItem node in nodes)
						{
							property = node.Properties["MediaImage"];
							if(property != null)
							{
								//	This item has a media reference.
								if(bDeleteResource)
								{
									if(!resourceTickets.Exists(x =>
										x == property.Value.ToString().ToLower()))
									{
										//	Keep a reference to this resource for 
										resourceTickets.Add(property.StringValue().ToLower());
									}
								}
								node.Properties.Remove(property);
							}
							//	Remove the runtime icon.
							property = node.Properties.FirstOrDefault(x =>
								x.Name == "ThumbImage");
							if(property != null)
							{
								node.Properties.Remove(property);
							}
						}
						if(bDeleteResource && resourceTickets.Count > 0)
						{
							//	Check all nodes for reference to the specified resource.
							foreach(string ticket in resourceTickets)
							{
								bDeleteResource = true;
								foreach(NodeItem node in nodeControl.Nodes)
								{
									if(node.Properties.Exists(x =>
										x.Name == "MediaImage" &&
										x.StringValue().ToLower() == ticket))
									{
										//	This resource is still in use.
										bDeleteResource = false;
										break;
									}
								}
								if(bDeleteResource)
								{
									//	Delete this resource from the file resources collection.
									resource = nodeControl.Resources.FirstOrDefault(x =>
										x.Ticket.ToLower() == ticket);
									if(resource != null)
									{
										nodeControl.Resources.Remove(resource);
									}
								}
							}
						}
					}
				}
				else
				{
					MessageBox.Show(
						"There were no image resources attached to any of the " +
						"selected nodes.", "Remove Image From Selected Nodes");
				}
				nodeControl.Invalidate();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditNodeRemoveLink_Click																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Node / Remove Link From Selected menu option has been
		/// clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditNodeRemoveLink_Click(object sender, EventArgs e)
		{
			bool bDeleteResource = false;
			int count = 0;
			List<NodeItem> nodes = nodeControl.SelectionQueue;
			PropertyItem property = null;
			ResourceItem resource = null;
			List<string> resourceTickets = new List<string>();

			if(nodes.Count > 0)
			{
				foreach(NodeItem node in nodes)
				{
					if(PropertyExists(node, "MediaLink"))
					{
						count++;
					}
				}
				if(count > 0)
				{
					if(MessageBox.Show(
						$"Remove link from {count} selected nodes?",
						"Remove Link From Selected Nodes", MessageBoxButtons.YesNo) ==
						DialogResult.Yes)
					{
						bDeleteResource = (MessageBox.Show(
							"Also delete associated resource if no longer in use?",
							"Remove Link From Selected Nodes", MessageBoxButtons.YesNo) ==
							DialogResult.Yes);
						foreach(NodeItem node in nodes)
						{
							property = node.Properties["MediaLink"];
							if(property != null)
							{
								//	This item has a media reference.
								if(bDeleteResource)
								{
									if(!resourceTickets.Exists(x =>
										x == property.Value.ToString().ToLower()))
									{
										//	Keep a reference to this resource for 
										resourceTickets.Add(property.StringValue().ToLower());
									}
								}
								node.Properties.Remove(property);
							}
							//	Remove the runtime icon.
							property = node.Properties.FirstOrDefault(x =>
								x.Name == "IconLink");
							if(property != null)
							{
								node.Properties.Remove(property);
							}
						}
						if(bDeleteResource && resourceTickets.Count > 0)
						{
							//	Check all nodes for reference to the specified resource.
							foreach(string ticket in resourceTickets)
							{
								bDeleteResource = true;
								foreach(NodeItem node in nodeControl.Nodes)
								{
									if(node.Properties.Exists(x =>
										x.Name == "MediaLink" &&
										x.StringValue().ToLower() == ticket))
									{
										//	This resource is still in use.
										bDeleteResource = false;
										break;
									}
								}
								if(bDeleteResource)
								{
									//	Delete this resource from the file resources collection.
									resource = nodeControl.Resources.FirstOrDefault(x =>
										x.Ticket.ToLower() == ticket);
									if(resource != null)
									{
										nodeControl.Resources.Remove(resource);
									}
								}
							}
						}
					}
				}
				else
				{
					MessageBox.Show(
						"There were no link resources attached to any of the " +
						"selected nodes.", "Remove Link From Selected Nodes");
				}
				nodeControl.Invalidate();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditNodeRemoveVideo_Click																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Node / Remove Video From Selected menu option has been
		/// clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditNodeRemoveVideo_Click(object sender, EventArgs e)
		{
			bool bDeleteResource = false;
			int count = 0;
			List<NodeItem> nodes = nodeControl.SelectionQueue;
			PropertyItem property = null;
			ResourceItem resource = null;
			List<string> resourceTickets = new List<string>();

			if(nodes.Count > 0)
			{
				foreach(NodeItem node in nodes)
				{
					if(PropertyExists(node, "MediaVideo"))
					{
						count++;
					}
				}
				if(count > 0)
				{
					if(MessageBox.Show(
						$"Remove video from {count} selected nodes?",
						"Remove Video From Selected Nodes", MessageBoxButtons.YesNo) ==
						DialogResult.Yes)
					{
						bDeleteResource = (MessageBox.Show(
							"Also delete associated resource if no longer in use?",
							"Remove Video From Selected Nodes", MessageBoxButtons.YesNo) ==
							DialogResult.Yes);
						foreach(NodeItem node in nodes)
						{
							property = node.Properties["MediaVideo"];
							if(property != null)
							{
								//	This item has a media reference.
								if(bDeleteResource)
								{
									if(!resourceTickets.Exists(x =>
										x == property.Value.ToString().ToLower()))
									{
										//	Keep a reference to this resource for 
										resourceTickets.Add(property.StringValue().ToLower());
									}
								}
								node.Properties.Remove(property);
							}
							//	Remove the runtime icon.
							property = node.Properties.FirstOrDefault(x =>
								x.Name == "ThumbVideo");
							if(property != null)
							{
								node.Properties.Remove(property);
							}
						}
						if(bDeleteResource && resourceTickets.Count > 0)
						{
							//	Check all nodes for reference to the specified resource.
							foreach(string ticket in resourceTickets)
							{
								bDeleteResource = true;
								foreach(NodeItem node in nodeControl.Nodes)
								{
									if(node.Properties.Exists(x =>
										x.Name == "MediaVideo" &&
										x.StringValue().ToLower() == ticket))
									{
										//	This resource is still in use.
										bDeleteResource = false;
										break;
									}
								}
								if(bDeleteResource)
								{
									//	Delete this resource from the file resources collection.
									resource = nodeControl.Resources.FirstOrDefault(x =>
										x.Ticket.ToLower() == ticket);
									if(resource != null)
									{
										nodeControl.Resources.Remove(resource);
									}
								}
							}
						}
					}
				}
				else
				{
					MessageBox.Show(
						"There were no video resources attached to any of the " +
						"selected nodes.", "Remove Video From Selected Nodes");
				}
				nodeControl.Invalidate();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditSelectAll_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Select All menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditSelectAll_Click(object sender, EventArgs e)
		{
			foreach(NodeItem node in nodeControl.Nodes)
			{
				node.Selected = true;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditSelectNone_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Select None menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditSelectNone_Click(object sender, EventArgs e)
		{
			foreach(NodeItem node in nodeControl.Nodes)
			{
				node.Selected = false;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditUndo_Click																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Undo menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditUndo_Click(object sender, EventArgs e)
		{
			string actionL = "";
			bool bFound = false;
			char[] comma = new char[] { ',' };
			SocketItem connection = null;
			SocketCollection connections = null;
			int count = 0;
			int index = 0;
			UndoItem item = null;
			NodeItem node = null;
			bool prevEventsEnabled = nodeControl.EventsEnabled;
			string propertyName = "";
			SocketItem socket = null;
			string[] tickets = null;
			string typeL = "";

			while(mUndo != null && mUndo.Count == 0 && mUndoPack.Count > 0)
			{
				mUndo = mUndoPack.Pop();
			}
			nodeControl.EventsEnabled = false;
			while(mUndo.Count > 0)
			{
				item = mUndo.Peek();
				actionL = item.ActionType.ToLower();
				typeL = item.ItemType.ToLower();
				tickets = item.ItemName.Split(comma);
				switch(typeL)
				{
					case "nodeitem":
						if(actionL == "add")
						{
							//	Remove the added node.
							node = nodeControl.Nodes.FirstOrDefault(x =>
								x.Ticket == tickets[0]);
							if(node != null)
							{
								//	Node still exists.
								nodeControl.Nodes.Remove(node);
							}
							bFound = true;
						}
						else if(actionL == "delete")
						{
							//	Restore the deleted node.
							node = new NodeItem();
							if(item.Properties.Exists(x =>
								x.Name == "Delay" && x.Value != null))
							{
								node.Delay = (float)item.Properties["Delay"].Value;
							}
							if(item.Properties.Exists(x =>
								x.Name == "Height" && x.Value != null))
							{
								node.Height = (float)item.Properties["Height"].Value;
							}
							if(item.Properties.Exists(x =>
								x.Name == "NodeColor" && x.Value != null))
							{
								node.NodeColor = (Color)item.Properties["NodeColor"].Value;
							}
							if(item.Properties.Exists(x =>
								x.Name == "NodeTextColor" && x.Value != null))
							{
								node.NodeTextColor =
									(Color)item.Properties["NodeTextColor"].Value;
							}
							if(item.Properties.Exists(x =>
								x.Name == "NodeType" && x.Value != null))
							{
								node.NodeType = (string)item.Properties["NodeType"].Value;
							}
							if(item.Properties.Exists(x =>
								x.Name == "Selected" && x.Value != null))
							{
								node.Selected = (bool)item.Properties["Selected"].Value;
							}
							if(item.Properties.Exists(x =>
								x.Name == "Ticket" && x.Value != null))
							{
								node.Ticket = (string)item.Properties["Ticket"].Value;
							}
							if(item.Properties.Exists(x =>
								x.Name == "TitleHeight" && x.Value != null))
							{
								node.TitleHeight = (float)item.Properties["TitleHeight"].Value;
							}
							if(item.Properties.Exists(x =>
								x.Name == "TitleProperty" && x.Value != null))
							{
								node.TitleProperty =
									(string)item.Properties["TitleProperty"].Value;
							}
							if(item.Properties.Exists(x =>
								x.Name == "Width" && x.Value != null))
							{
								node.Width = (float)item.Properties["Width"].Value;
							}
							if(item.Properties.Exists(x =>
								x.Name == "X" && x.Value != null))
							{
								node.X = (float)item.Properties["X"].Value;
							}
							if(item.Properties.Exists(x =>
								x.Name == "Y" && x.Value != null))
							{
								node.Y = (float)item.Properties["Y"].Value;
							}
							if(item.Properties.Exists(x =>
								x.Name == "ZOrder" && x.Value != null))
							{
								node.ZOrder = (int)item.Properties["ZOrder"].Value;
							}
							if(item.Properties.Exists(x =>
								x.Name == "Properties" && x.Value != null))
							{
								NodeItem.SetProperties(node,
									(PropertyCollection)item.Properties["Properties"].Value);
							}
							if(item.Properties.Exists(x =>
								x.Name == "Sockets" && x.Value != null))
							{
								NodeItem.SetSockets(node,
									(SocketCollection)item.Properties["Sockets"].Value);
							}
							nodeControl.Nodes.Add(node);
							bFound = true;
						}
						else if(actionL == "edit")
						{
							//	Restore the previous value.
							if(item.Properties.Count > 0)
							{
								node = nodeControl.Nodes.FirstOrDefault(x =>
									x.Ticket == tickets[0]);
								if(node != null)
								{
									//	Node still exists.
									propertyName = item.Properties[0].Name;
									node[propertyName].Value =
										item.Properties[$"{propertyName}Before"].Value;
								}
							}
							bFound = true;
						}
						else if(actionL == "move")
						{
							//	Node was moved. There should be X and Y properties in the
							//	item.
							if(item.Properties.Exists(x => x.Name == "X") &&
								item.Properties.Exists(x => x.Name == "Y"))
							{
								//	The X and Y properties are both available.
								node = nodeControl.Nodes.FirstOrDefault(x =>
									x.Ticket == tickets[0]);
								if(node != null)
								{
									//	Node still exists.
									node.X = (float)item.Properties["XBefore"].Value;
									node.Y = (float)item.Properties["YBefore"].Value;
								}
							}
							bFound = true;
						}
						break;
					case "socketconnectionitem":
						if(actionL == "add")
						{
							//	Remove the added socket connection.
							//	This item will have {Node},{Socket} tickets.
							if(tickets.Length > 1)
							{
								node = nodeControl.Nodes.FirstOrDefault(x =>
									x.Ticket == tickets[0]);
								if(node != null)
								{
									//	Node still exists.
									socket = node.Sockets.FirstOrDefault(x =>
										x.Ticket == tickets[1]);
									if(socket != null && item.Properties.Count > 0)
									{
										//	Get the connection.
										connection = socket.Connections.FirstOrDefault(x =>
											x.Ticket == (string)item.Properties[0].Value);
										if(connection != null)
										{
											//	Connection found.
											socket.Connections.Remove(connection);
										}
									}
								}
							}
							bFound = true;
						}
						else if(actionL == "delete")
						{
							//	Restore the deleted socket connection.
							//	This item will have {Node},{Socket} tickets.
							if(tickets.Length > 1 && item.Properties.Count > 0)
							{
								node = nodeControl.Nodes.FirstOrDefault(x =>
									x.Ticket == tickets[0]);
								if(node != null)
								{
									//	Node still exists.
									socket = node.Sockets.FirstOrDefault(x =>
										x.Ticket == tickets[1]);
									if(socket != null && item.Properties.Count > 0)
									{
										//	Socket still exists.
										//	Get the connection.
										node = nodeControl.Nodes.FirstOrDefault(x =>
											x.Sockets.Exists(y =>
											y.Ticket == (string)item.Properties[0].Value));
										if(node != null)
										{
											//	The original connecting socket still exists.
											socket.Connections.Add(node.Sockets.First(x =>
												x.Ticket == (string)item.Properties[0].Value));
										}
									}
								}
							}
							bFound = true;
						}
						break;
					case "socketitem":
						if(actionL == "add")
						{
							//	Remove the added socket.
							//	This instance will have {Node},{Socket} tickets.
							if(tickets.Length > 1)
							{
								node = nodeControl.Nodes.FirstOrDefault(x =>
									x.Ticket == tickets[0]);
								if(node != null)
								{
									//	Node still exists.
									socket = node.Sockets.FirstOrDefault(x =>
										x.Ticket == tickets[1]);
									if(socket != null)
									{
										//	Socket still exists.
										node.Sockets.Remove(socket);
									}
								}
							}
							bFound = true;
						}
						else if(actionL == "delete")
						{
							//	Restore the deleted socket.
							//	This item will have a {Node} ticket.
							//	Socket and connection information is stored in the
							//	properties.
							node = nodeControl.Nodes.FirstOrDefault(x =>
								x.Ticket == tickets[0]);
							if(node != null)
							{
								//	Node still exists.
								//	Restore the deleted node.
								socket = new SocketItem();
								if(item.Properties.Exists(x =>
									x.Name == "Height" && x.Value != null))
								{
									socket.Height = (float)item.Properties["Height"].Value;
								}
								if(item.Properties.Exists(x =>
									x.Name == "SocketMode" && x.Value != null))
								{
									socket.SocketMode =
										(SocketModeEnum)item.Properties["SocketMode"].Value;
								}
								if(item.Properties.Exists(x =>
									x.Name == "TextBounds" && x.Value != null))
								{
									socket.TextBounds =
										(RectangleF)item.Properties["TextBounds"].Value;
								}
								if(item.Properties.Exists(x =>
									x.Name == "Ticket" && x.Value != null))
								{
									socket.Ticket = (string)item.Properties["Ticket"].Value;
								}
								if(item.Properties.Exists(x =>
									x.Name == "TitleProperty" && x.Value != null))
								{
									socket.TitleProperty =
										(string)item.Properties["TitleProperty"].Value;
								}
								if(item.Properties.Exists(x =>
									x.Name == "Width" && x.Value != null))
								{
									socket.Width = (float)item.Properties["Width"].Value;
								}
								if(item.Properties.Exists(x =>
									x.Name == "X" && x.Value != null))
								{
									socket.X = (float)item.Properties["X"].Value;
								}
								if(item.Properties.Exists(x =>
									x.Name == "Y" && x.Value != null))
								{
									socket.Y = (float)item.Properties["Y"].Value;
								}
								if(item.Properties.Exists(x =>
									x.Name == "Properties" && x.Value != null))
								{
									SocketItem.SetProperties(socket,
										(PropertyCollection)item.Properties["Properties"].Value);
								}
								if(item.Properties.Exists(x =>
									x.Name == "Connections" && x.Value != null))
								{
									//	Remove all connections to items that no longer exist
									//	in the structure.
									connections =
										(SocketCollection)item.Properties["Connections"].Value;
									count = connections.Count;
									for(index = 0; index < count; index ++)
									{
										connection = connections[index];
										if(!nodeControl.Nodes.Exists(x =>
											x.Sockets.Exists(y =>
											y.Ticket == connection.Ticket)))
										{
											//	The item is not found.
											connections.Remove(connection);
											count--;
											index--;
										}
									}
									if(connections.Count > 0)
									{
										SocketItem.SetConnections(socket, connections);
									}
								}
								node.Sockets.Add(socket);
							}
							bFound = true;
						}
						else if(actionL == "edit")
						{
							//	Restore the previous value.
							if(item.Properties.Count > 0)
							{
								node = nodeControl.Nodes.FirstOrDefault(x =>
									x.Ticket == tickets[0]);
								if(node != null && tickets.Length > 1)
								{
									//	Node still exists.
									socket = node.Sockets.FirstOrDefault(x =>
										x.Ticket == tickets[1]);
									if(socket != null)
									{
										//	Socket still exists.
										propertyName = item.Properties[0].Name;
										socket[propertyName].Value =
											item.Properties[$"{propertyName}Before"].Value;
									}
								}
							}
							bFound = true;
						}
						break;
				}
				if(!bFound)
				{
					//	Allow plug-ins to process the item.
					//bFound = !mUndo.ProcessItem(item);
				}
				//	Pop after maintenance is finished.
				mUndo.Pop();
			}
			mUndoPack.Pop();
			if(mUndoPack.Count > 0)
			{
				mUndo = mUndoPack.Peek();
			}
			else
			{
				mUndo = null;
			}
			nodeControl.EventsEnabled = prevEventsEnabled;
			nodeControl.Invalidate();
			nodeControl.Refresh();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileConvertPPToHTML_Click																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// File / Convert / PowerPoint to HTML menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuFileConvertPPToHTML_Click(object sender, EventArgs e)
		{
			MessageBox.Show(
				"PowerPoint to HTML conversion is not yet functional.",
				"Convert PowerPoint to HTML");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileConvertPPToTinyLMS_Click																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// File / Convert / PowerPoint to TinyLMS menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuFileConvertPPToTinyLMS_Click(object sender, EventArgs e)
		{
			MessageBox.Show(
				"PowerPoint to TinyLMS conversion is not yet functional.",
				"Convert PowerPoint to TinyLMS");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileDocumentProperties_Click																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The File / Document Properties menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuFileDocumentProperties_Click(object sender, EventArgs e)
		{
			frmDocumentProperties form = new frmDocumentProperties();

			CenterOver(this, form);
			form.DocumentDescription = nodeControl.NodeFile.Description;
			form.DocumentName = nodeControl.NodeFile.Name;
			form.DocumentTicket = nodeControl.NodeFile.Ticket;
			if(form.ShowDialog() == DialogResult.OK)
			{
				nodeControl.NodeFile.Description = form.DocumentDescription;
				nodeControl.NodeFile.Name = form.DocumentName;
				nodeControl.NodeFile.Ticket = form.DocumentTicket;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileExit_Click																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// File / Exit menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuFileExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileExportDecisionTreeToPP_Click																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// File / Export / Decision Tree to PowerPoint menu option has been
		/// clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuFileExportDecisionTreeToPP_Click(object sender,
			EventArgs e)
		{
			OfficeDriver driver = null;
			int page = 0;
			int pg = 0;
			List<SocketItem> sockets = null;
			frmExportDecisionTreeWizard wizard = null;

			if(nodeControl.Nodes.Count > 0)
			{
				//	Start PowerPoint.
				wizard = new frmExportDecisionTreeWizard();
				wizard.Nodes = nodeControl.Nodes;
				CenterOver(this, wizard);
				if(wizard.ShowDialog() == DialogResult.OK)
				{
					//	Wizard has completed. Prepare for export.
					driver = new OfficeDriver();
					page = 0;
					//	Check to see the highest known page reference.
					foreach(NodeItem node in nodeControl.Nodes)
					{
						pg = ToInt(node["StoryPageNumber"].StringValue());
						page = Math.Max(page, pg);
					}
					//	Prepare the automatic node values.
					if(wizard.AutoPlacement)
					{
						foreach(NodeItem node in nodeControl.Nodes)
						{
							pg = ToInt(node["StoryPageNumber"].StringValue());
							//x = ToFloat(node["StoryPageX"].StringValue());
							//y = ToFloat(node["StoryPageY"].StringValue());
							//ph = node["StoryPageHorizontalPlacement"].StringValue();
							//pv = node["StoryPageVerticalPlacement"].StringValue();
							if(pg == 0)
							{
								//	New page for this item.
								//	Each node starts a page.
								page++;
								pg = page;
								node["StoryPageNumber"].Value = pg;
								node["StoryPageX"].Value = wizard.AutoQuestionOffsetX;
								node["StoryPageHorizontalPlacement"].Value =
									wizard.AutoQuestionOffsetXFrom;
								node["StoryPageY"].Value = wizard.AutoQuestionOffsetY;
								node["StoryPageVerticalPlacement"].Value =
									wizard.AutoQuestionOffsetYFrom;
								node["StoryPageWidth"].Value = 512f;
								node["StoryPageFontName"].Value = "Calibri";
								node["StoryPageFontSize"].Value = 14f;
								sockets = node.Sockets.FindAll(s =>
									s.SocketMode == SocketModeEnum.Output);
								foreach(SocketItem socket in sockets)
								{
									pg = ToInt(socket["StoryPageNumber"].StringValue());
									if(pg == 0)
									{
										pg = page;
										socket["StoryPageNumber"].Value = pg;
										socket["StoryPageX"].Value = wizard.AutoAnswerOffsetX;
										socket["StoryPageHorizontalPlacement"].Value =
											wizard.AutoAnswerOffsetXFrom;
										socket["StoryPageY"].Value = wizard.AutoAnswerOffsetY;
										socket["StoryPageVerticalPlacement"].Value =
											wizard.AutoAnswerOffsetYFrom;
										socket["StoryPageWidth"].Value = 256f;
										socket["StoryPageFontName"].Value = "Calibri";
										socket["StoryPageFontSize"].Value = 14f;
									}
									else
									{
										page = Math.Max(page, pg);
									}
								}
							}
						}
					}
					driver.ExportDecisionTreeToPowerPoint(nodeControl.Nodes,
						wizard.CreateFile, wizard.Filename);
				}
			}
			else
			{
				MessageBox.Show(
					"There are no nodes present in the decision tree. " +
					"Please add nodes first.", "Export Decision Tree to PowerPoint");
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileNew_Click																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The File / New menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuFileNew_Click(object sender, EventArgs e)
		{
			if(btnDecisionTreeEditor.Selected)
			{
				mnuFileNewNode_Click(sender, e);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileNewNode_Click																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The File / New Node File menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuFileNewNode_Click(object sender, EventArgs e)
		{
			nodeControl.NodeFile.Clear();
			nodeControl.Invalidate();
			//NodeFileObject = nodeControl.NodeFile;
			//mNodeFile = null;
			NodeFileInfo = null;
			ResourceCollection.BasePath = "";
			this.Text = $"Scaffold - Decision Tree Editor";
			statMessage.Text = "New Node Layout Created...";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileOpen_Click																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The File / Open menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuFileOpen_Click(object sender, EventArgs e)
		{
			if(btnDecisionTreeEditor.Selected)
			{
				mnuFileOpenNode_Click(sender, e);
			}
			else if(btnSlideEditor.Selected)
			{
				mnuFileOpenSVG_Click(sender, e);
			}
			else if(btnHTML.Selected)
			{
				mnuFileOpenHTML_Click(sender, e);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileOpenHTML_Click																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The File / Open HTML File menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuFileOpenHTML_Click(object sender, EventArgs e)
		{
			//htmlGeneric.Navigate(
			//	"file://C:/Users/Daniel/Documents/GitHub/Scaffold/Experiments/ChatbotEmulator/Index.html");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileOpenNode_Click																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// File / Open Node File menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuFileOpenNode_Click(object sender, EventArgs e)
		{
			string content = "";
			OpenFileDialog dialog = new OpenFileDialog();

			statMessage.Text = "Loading Nodes...";
			dialog.Filter = "Node JSON (*.node.json)|*.node.json|" +
				"All Files (*.*)|*.*";
			dialog.FilterIndex = 0;
			dialog.DefaultExt = ".node.json";
			dialog.AddExtension = true;
			dialog.CheckFileExists = true;
			dialog.CheckPathExists = true;
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				//mNodeFile = new FileInfo(dialog.FileName);
				NodeFileInfo = new FileInfo(dialog.FileName);
				ResourceCollection.BasePath = NodeFileInfo.DirectoryName;
				content = File.ReadAllText(dialog.FileName);
				nodeControl.NodeFile.Clear();
				NodeFileObject = nodeControl.NodeFile;
				NodeDataCollection.DeserializeData(nodeControl.NodeFile,
					NodeFileInfo.DirectoryName, content);
				this.Text = $"Scaffold - Decision Tree Editor - {NodeFileInfo.Name}";
				InitializeDocumentProperties();
				statMessage.Text = "Nodes Loaded...";
				mUndoPack.Clear();
				mUndo_StackPushPop(null, null);
				nodeControl.Invalidate();
				nodeControl.Refresh();
				ScrollIntoView();
			}
			else
			{
				statMessage.Text = "Open Cancelled...";
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileOpenSVG_Click																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The File / Open SVG menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Stanard event arguments.
		/// </param>
		private async void mnuFileOpenSVG_Click(object sender, EventArgs e)
		{
			string content = "";
			OpenFileDialog dialog = new OpenFileDialog();
			DirectoryInfo directory = null;
			NameValueItem result = null;

			statMessage.Text = "Loading SVG...";
			dialog.Filter = "SVG (*.svg)|*.svg|" +
				"All Files (*.*)|*.*";
			dialog.FilterIndex = 0;
			dialog.DefaultExt = ".svg";
			dialog.AddExtension = true;
			dialog.CheckFileExists = true;
			dialog.CheckPathExists = true;
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				mSVGFile = new FileInfo(dialog.FileName);
				directory = mSVGFile.Directory;
				content = File.ReadAllText(dialog.FileName);
				result = new NameValueItem();
				await SvgReplaceFileRefWithB64(directory, content, result);
				statProg.Value = 50;
				mSVG.Clear();
				mSVG.LoadFromString(result.Value);
				skControl.Invalidate();
				this.Text = $"Scaffold - Slide Editor - {mSVGFile.Name}";
				statMessage.Text = "SVG Loaded...";
			}
			else
			{
				statMessage.Text = "Open Cancelled...";
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFilePublishSlackChatConversation_Click															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// File / Publish / As Slack Chatbot Conversation menu item has been
		/// clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuFilePublishSlackChatConversation_Click(object sender,
			EventArgs e)
		{
			bool bContinue = true;    //	Flag - continue.
			string content = "";
			frmDocumentProperties formProperties = null;
			//	Runtime.
			string site = "https://ScaffoldSlackPack.azurewebsites.net";

			if(NetworkLocalMode)
			{
				//	Development on IIS Express.
				site = "https://localhost:44328";
			}

			if(NodeFileInfo == null)
			{
				if(MessageBox.Show("Please save your node file before continuing.",
					"Publish to Slack", MessageBoxButtons.OKCancel) == DialogResult.OK)
				{
					mnuFileSaveNodeAs_Click(sender, e);
				}
				else
				{
					bContinue = false;
				}
			}
			if(NodeFileInfo == null)
			{
				bContinue = false;
			}
			if(bContinue)
			{
				statMessage.Text = "Preparing Slack Data...";
				statProg.Value = 25;
				this.Refresh();
				//	Make sure a ticket exists.
				if(nodeControl.NodeFile.Ticket.Length == 0)
				{
					nodeControl.NodeFile.Ticket =
						Guid.NewGuid().ToString("D").ToLower();
				}
				//	View / Edit properties.
				formProperties = new frmDocumentProperties();
				formProperties.DocumentDescription = nodeControl.NodeFile.Description;
				formProperties.DocumentName = nodeControl.NodeFile.Name;
				formProperties.DocumentTicket = nodeControl.NodeFile.Ticket;
				bContinue = (formProperties.ShowDialog() == DialogResult.OK);
			}
			if(bContinue)
			{
				//	Update properties.
				statMessage.Text = $"Uploading Slack Data to {site}...";
				statProg.Value = 50;
				this.Refresh();
				nodeControl.NodeFile.Description = formProperties.DocumentDescription;
				nodeControl.NodeFile.Name = formProperties.DocumentName;
				nodeControl.NodeFile.Ticket = formProperties.DocumentTicket;
				//	Convert the content to stand-alone.
				content = NodeDataCollection.SerializeData(nodeControl.NodeFile, true,
					NodeFileInfo.DirectoryName).Replace('’', '\'');
				using(WebClient webClient = new WebClient())
				{
					//webClient.Headers.Add(
					//	HttpRequestHeader.ContentType, "application/json");
					webClient.Headers.Add(
						HttpRequestHeader.ContentType, "text/plain");
					try
					{
						webClient.UploadString(
							new Uri($"{site}/ScaffoldSlackPack/PublishPackage"),
							"POST", content);
					}
					catch(Exception ex)
					{
						MessageBox.Show(
							$"Error while uploading data: {ex.Message}",
							"Publish to Slack");
						bContinue = false;
					}
				}
			}
			if(bContinue)
			{
				statProg.Value = 100;
				this.Refresh();
				MessageBox.Show("File published to Slack!", "Publish to Slack");
			}

			statMessage.Text = "Ready...";
			statProg.Value = 0;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileSave_Click																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The File / Save menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Stanard event arguments.
		/// </param>
		private void mnuFileSave_Click(object sender, EventArgs e)
		{
			if(btnDecisionTreeEditor.Selected)
			{
				mnuFileSaveNode_Click(sender, e);
			}
			else if(btnSlideEditor.Selected)
			{
				mnuFileSaveSVG_Click(sender, e);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileSaveNode_Click																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// File / Save Node File menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuFileSaveNode_Click(object sender, EventArgs e)
		{
			string content = "";
			if(NodeFileInfo != null)
			{
				statMessage.Text = "Saving Nodes...";
				InitializeDocumentProperties();
				content = NodeDataCollection.SerializeData(nodeControl.NodeFile);
				File.WriteAllText(NodeFileInfo.FullName, content);
				statMessage.Text = "Nodes Saved...";
			}
			else
			{
				mnuFileSaveNodeAs_Click(sender, e);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileSaveSVG_Click																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The File / Save SVG menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Stanard event arguments.
		/// </param>
		private void mnuFileSaveSVG_Click(object sender, EventArgs e)
		{
			if(mSVG != null && mSVG.Document != null && mSVGFile != null)
			{
				//	The SVG file exists and can be saved.
				mSVG.Document.Save(mSVGFile.FullName);
				statMessage.Text = "SVG Saved...";
			}
			else if(mSVG != null && mSVG.Document != null)
			{
				//	The SVG file exists, but no previous file was specified.
				//	Use a File Save As dialog to save the content.
				mnuFileSaveSVGAs_Click(sender, e);
			}
			else
			{
				MessageBox.Show("No SVG file has been loaded...", "Save SVG File");
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileSaveAs_Click																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The File / Save As menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuFileSaveAs_Click(object sender, EventArgs e)
		{
			if(btnDecisionTreeEditor.Selected)
			{
				mnuFileSaveNodeAs_Click(sender, e);
			}
			else
			{
				mnuFileSaveSVGAs_Click(sender, e);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileSaveNodeAs_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// File / Save Node File As ... menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuFileSaveNodeAs_Click(object sender, EventArgs e)
		{
			string content = "";
			SaveFileDialog dialog = new SaveFileDialog();
			FileInfo file = null;
			string relativeFilename = "";

			statMessage.Text = "Saving Nodes...";
			dialog.Filter = "Node JSON (*.node.json)|*.node.json|" +
				"All Files (*.*)|*.*";
			dialog.FilterIndex = 0;
			dialog.DefaultExt = ".node.json";
			dialog.AddExtension = true;
			dialog.CheckPathExists = true;
			dialog.OverwritePrompt = true;
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				//mNodeFile = new FileInfo(dialog.FileName);
				NodeFileInfo = new FileInfo(dialog.FileName);
				//	New version of file gets a new name and ticket.
				NodeFileObject.Ticket = Guid.NewGuid().ToString("D");
				NodeFileObject.Name = "";
				InitializeDocumentProperties();
				//	Re-attempt relative filenames on absolute path linked resources.
				foreach(ResourceItem resourceItem in NodeFileObject.Resources)
				{
					if(resourceItem.RelativeFilename.Length > 1 &&
						(resourceItem.RelativeFilename[1] == ':' ||
						resourceItem.RelativeFilename.StartsWith("//")))
					{
						//	Drive letter or UNC are present.
						//	Resource has an absolute path.
						file = new FileInfo(resourceItem.AbsoluteFilename);
						relativeFilename = RelativeFilename(NodeFileInfo, file);
						if(resourceItem.Uri == resourceItem.RelativeFilename)
						{
							//	The file is a local link with no relative path.
							resourceItem.Uri = relativeFilename;
						}
						resourceItem.RelativeFilename = relativeFilename;
					}
				}
				content = NodeDataCollection.SerializeData(nodeControl.NodeFile);
				File.WriteAllText(dialog.FileName, content);
				ResourceCollection.BasePath = NodeFileInfo.DirectoryName;
				this.Text = $"Scaffold - Decision Tree Editor - {NodeFileInfo.Name}";
				statMessage.Text = "Nodes Saved...";
			}
			else
			{
				statMessage.Text = "Save Cancelled...";
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileSaveSVGAs_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The File / Save SVG As menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Stanard event arguments.
		/// </param>
		private void mnuFileSaveSVGAs_Click(object sender, EventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			DirectoryInfo directory = null;

			if(mSVG != null && mSVG.Document != null)
			{
				statMessage.Text = "Saving SVG...";
				dialog.Filter = "SVG (*.svg)|*.svg|" +
					"All Files (*.*)|*.*";
				dialog.FilterIndex = 0;
				dialog.DefaultExt = ".svg";
				dialog.AddExtension = true;
				dialog.CheckPathExists = true;
				dialog.OverwritePrompt = true;
				if(dialog.ShowDialog() == DialogResult.OK)
				{
					mSVGFile = new FileInfo(dialog.FileName);
					directory = mSVGFile.Directory;
					mSVG.Document.Save(dialog.FileName);
					this.Text = $"Scaffold - Slide Editor - {mSVGFile.Name}";
					statMessage.Text = "SVG Saved...";
				}
				else
				{
					statMessage.Text = "Save Cancelled...";
				}
			}
			else
			{
				MessageBox.Show("No SVG file has been loaded...", "Save SVG File As");
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileUnpublishSlackChatConversation_Click														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// File / Unpublish / Slack Chatbot Conversation menu option has been
		/// clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuFileUnpublishSlackChatConversation_Click(object sender,
			EventArgs e)
		{
			bool bContinue = true;    //	Flag - continue.
			string content = "";
			NodeFileDescriptor descriptor = null;
			frmDocumentProperties formProperties = null;
			//	Runtime.
			string site = "https://ScaffoldSlackPack.azurewebsites.net";

			if(NetworkLocalMode)
			{
				//	Development on IIS Express.
				site = "https://localhost:44328";
			}

			if(NodeFileInfo == null)
			{
				if(MessageBox.Show("Please save your node file before continuing.",
					"Publish to Slack", MessageBoxButtons.OKCancel) == DialogResult.OK)
				{
					mnuFileSaveNodeAs_Click(sender, e);
				}
				else
				{
					bContinue = false;
				}
			}
			if(NodeFileInfo == null)
			{
				bContinue = false;
			}
			if(bContinue)
			{
				statMessage.Text = "Preparing Slack Data...";
				statProg.Value = 25;
				this.Refresh();
				//	Make sure a ticket exists.
				if(nodeControl.NodeFile.Ticket.Length == 0)
				{
					nodeControl.NodeFile.Ticket =
						Guid.NewGuid().ToString("D").ToLower();
				}
				//	View / Edit properties.
				formProperties = new frmDocumentProperties();
				formProperties.DocumentDescription = nodeControl.NodeFile.Description;
				formProperties.DocumentName = nodeControl.NodeFile.Name;
				formProperties.DocumentTicket = nodeControl.NodeFile.Ticket;
				bContinue = (formProperties.ShowDialog() == DialogResult.OK);
			}
			if(bContinue)
			{
				//	Update properties.
				statMessage.Text = $"Unpublishing Slack Data from {site}...";
				statProg.Value = 50;
				this.Refresh();
				descriptor = new NodeFileDescriptor();
				descriptor.Description = formProperties.DocumentDescription;
				descriptor.Name = formProperties.DocumentName;
				descriptor.Ticket = formProperties.DocumentTicket;
				//	Convert the content to stand-alone.
				content = JsonConvert.SerializeObject(descriptor);
				using(WebClient webClient = new WebClient())
				{
					//webClient.Headers.Add(
					//	HttpRequestHeader.ContentType, "application/json");
					webClient.Headers.Add(
						HttpRequestHeader.ContentType, "text/plain");
					try
					{
						webClient.UploadString(
							new Uri($"{site}/ScaffoldSlackPack/UnpublishPackage"),
							"POST", content);
					}
					catch(Exception ex)
					{
						MessageBox.Show(
							$"Error while processing command: {ex.Message}",
							"Unpublish from Slack");
						bContinue = false;
					}
				}
			}
			if(bContinue)
			{
				statProg.Value = 100;
				this.Refresh();
				MessageBox.Show("File unpublished from Slack.",
					"Unpublish from Slack");
			}

			statMessage.Text = "Ready...";
			statProg.Value = 0;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsAnimationFrameNToHTML_Click																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Tools / Animation / Draw Frame [N] to HTML View menu option has been
		/// clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private async void mnuToolsAnimationFrameNToHTML_Click(object sender,
			EventArgs e)
		{
			SvgAnimation animation = null;
			BrowserAttributeItem attribute = null;
			bool bContinue = true;
			//Bitmap bitmap = null;
			BrowserDriver browser = new BrowserDriver(
				htmlGeneric, statMessage, statProg);
			//byte[] buffer = new byte[0];
			SvgAnimationCharacterTrackCollection charTrack = null;
			string content = "";
			OpenFileDialog dialog = new OpenFileDialog();
			double dValue = 0.0;
			BrowserElementItem element = new BrowserElementItem();
			FileInfo file = null;
			int frameIndex = 0;
			int frameMax = 0;
			double height = 0.0;
			frmInputBox inputBox = new frmInputBox();
			List<string> log = new List<string>();
			Dictionary<string, object> properties = null;
			SvgAnimationFrameCollection renderedFrames = null;
			string svgID = "";
			NameValueItem token = null;
			SvgUserUnit userUnit = new SvgUserUnit();
			string[] values = new string[0];
			List<double> viewBox = new List<double>();
			double width = 0.0;
			//double x = 0.0;
			//double y = 0.0;

			btnHTML.Selected = true;
			mEscPressed = false;

			try
			{
				statMessage.Text = "Loading Timeline File...";
				dialog.Filter = "Timeline JSON (*.timeline.json)|*.timeline.json|" +
					"All Files (*.*)|*.*";
				dialog.FilterIndex = 0;
				dialog.DefaultExt = ".timeline.json";
				dialog.AddExtension = true;
				dialog.CheckFileExists = true;
				dialog.CheckPathExists = true;
				dialog.SupportMultiDottedExtensions = true;
				if(dialog.ShowDialog() == DialogResult.OK)
				{
					content = File.ReadAllText(dialog.FileName);
					animation = JsonConvert.DeserializeObject<SvgAnimation>(content);
					//	Check for compiled objects file.
					if(animation.CompiledHTMLFilename?.Length == 0)
					{
						animation.CompiledHTMLFilename = "C:/Temp/Scaffold.html";
						file = new FileInfo(animation.CompiledHTMLFilename);
						if(file.Exists)
						{
							file.Delete();
						}
						file = null;
						GC.Collect();
					}
					if(animation.CompiledHTMLFilename?.Length > 0)
					{
						//	Compiled HTML file was specified.
						file = new FileInfo(animation.CompiledHTMLFilename);
						if(!file.Exists)
						{
							//	A compiled file has been specified, but doesn't exist.
							if(animation.SourceArtFilename?.Length > 0)
							{
								file = new FileInfo(ResolveEnvironment(
									animation.SourceArtFilename));
								if(file.Exists)
								{
									//	A source file was specified.
									statMessage.Text = "Compiling Source File...";
									statProg.Maximum = 100;
									statProg.Value = 0;
									content = File.ReadAllText(file.FullName);
									//	To eliminate need for resolving file locations,
									//	replace all images with onboard base64.
									token = new NameValueItem();
									await SvgReplaceFileRefWithB64(
										file.Directory, content, token, statProg);
									statMessage.Text = "Saving Compiled File...";
									statProg.Value = 0;
									content = ResourceMain.htmlSVGInteractive.
										Replace("{SVGContent}", token.Value);
									File.WriteAllText(animation.CompiledHTMLFilename, content);
									statProg.Value = 100;
									////buffer = Encoding.UTF8.GetBytes(content);
									//////	Convert the entire payload to base64.
									////content = Convert.ToBase64String(buffer);
									//File.WriteAllText(@"C:\Temp\Scaffold.html", content);
									GC.Collect();
									//htmlGeneric.NavigateToString(content);
									statProg.Value = 0;
								}
							}
						}
						file = new FileInfo(animation.CompiledHTMLFilename);
						if(file.Exists)
						{
							statProg.Value = 0;
							statProg.Value = 100;
							await htmlGeneric.Navigate("file:/" +
								animation.CompiledHTMLFilename.Replace("\\", "/"));
							statProg.Value = 0;
							statMessage.Text = "SVG Data Loaded...";
						}
					}

					statMessage.Text = "Configuring User Unit...";
					token = new NameValueItem();
					await browser.GetId("svg", token);
					if(token.Name == "Message")
					{
						svgID = token.Value;
					}

					//	ViewBox.
					await browser.LoadAttributes(svgID, element);
					if(element.Attributes.Count > 0)
					{
						//	viewBox.
						values = element.Attributes["viewBox"].Value.
							Split(new char[] { ' ' });
						foreach(string value in values)
						{
							dValue = 0.0;
							double.TryParse(value, out dValue);
							viewBox.Add(dValue);
						}
						//	width.
						double.TryParse(element.Attributes["width"].Value, out width);
						//	height.
						double.TryParse(element.Attributes["height"].Value, out height);
						//	At this point, we have the view box, width, and height values.
						//	Configure the user unit.
						if(viewBox.Count >= 4)
						{
							userUnit.SetUserUnitX(viewBox[2], width);
							userUnit.SetUserUnitY(viewBox[3], height);
						}
					}
					browser.UserUnit = userUnit;

					#region Experiments
					//Bitmap bitmap =
					//	ControlSnapshot.Snapshot(htmlGeneric.GetInnerControl());
					//bitmap = new Bitmap((int)width, (int)height);
					//htmlGeneric.DrawToBitmap(bitmap);
					//bitmap.Save("C:/Temp/ScaffoldImage.png");

					////	Set the scene.
					//browser.Css("#layerHelpContact", "display", "none");
					//browser.Css("#layerStartIdeas", "display", "inline");

					////	Configure the characters.
					//statMessage.Text = "Configuring character IanWrite1...";
					//await browser.LoadAttributes("IanWrite1", element);
					//if(element.Attributes.Count > 0)
					//{
					//	double.TryParse(element.Attributes["x"].Value, out x);
					//	double.TryParse(element.Attributes["y"].Value, out y);
					//	double.TryParse(element.Attributes["width"].Value, out width);
					//	double.TryParse(element.Attributes["height"].Value, out height);
					//}
					////	Position the character.
					//x = userUnit.ToUserX(2051.390);
					////x = userUnit.ToUserX(1000);
					//y = userUnit.ToUserY(8.571);
					//browser.Attr("#IanWrite1", "x", x);
					//browser.Attr("#IanWrite1", "y", y);
					//browser.Attr("#IanWrite1", "transform",
					//	"rotate(30 " +
					//	$"{x + (width / 2.0)} " +
					//	$"{y + (height / 2.0)}" +
					//	")");

					//statMessage.Text =
					//	"Configuring character ThoughtBubbleConnector1...";
					////	Position the character.
					//browser.Css("#ThoughtBubbleConnector1", "opacity", 0.0);

					//statMessage.Text = "Configuring character ThoughtBubble1...";
					////	Position the character.
					//browser.Css("#ThoughtBubble1", "opacity", 0.0);

					//statMessage.Text = "Configuring character ToyRobots1...";
					////	Position the character.
					//x = userUnit.ToUserX(13.768);
					////x = userUnit.ToUserX(1000);
					//y = userUnit.ToUserY(1036.812);
					//browser.Attr("#ToyRobots1", "x", x);
					//browser.Attr("#ToyRobots1", "y", y);
					#endregion

					//	Discrete Timeline.
					if(animation.Frames.Count > 0)
					{
						frameMax = animation.Frames.Max(x => x.FrameIndex);
						if(mAnimationFrameToHtmlFrom < 0 ||
							mAnimationFrameToHtmlFrom >
							frameMax)
						{
							mAnimationFrameToHtmlFrom = 0;
						}
						if(mAnimationFrameToHtmlTo < 0 ||
							mAnimationFrameToHtmlTo >
							frameMax)
						{
							mAnimationFrameToHtmlTo = frameMax;
						}
						CenterOver(this, inputBox);
						inputBox.Title = "Starting Frame";
						inputBox.Prompt = "Enter the frame at which to start:";
						inputBox.Text = mAnimationFrameToHtmlFrom.ToString();
						if(inputBox.ShowDialog() == DialogResult.OK)
						{
							if(IsInt(inputBox.Text))
							{
								int.TryParse(inputBox.Text, out frameIndex);
							}
							else
							{
								frameIndex = 0;
							}
						}
						else
						{
							bContinue = false;
						}
						if(bContinue)
						{
							if(frameIndex > -1 && frameIndex <= frameMax)
							{
								//	In range.
								mAnimationFrameToHtmlFrom = frameIndex;
							}
							else
							{
								frameIndex = 0;
							}
							if(mAnimationFrameToHtmlTo < frameIndex)
							{
								mAnimationFrameToHtmlTo = frameIndex;
							}
							inputBox.Title = "Ending Frame";
							inputBox.Prompt = "Enter the frame at which to stop:";
							inputBox.Text = mAnimationFrameToHtmlTo.ToString();
							if(inputBox.ShowDialog() == DialogResult.OK)
							{
								if(IsInt(inputBox.Text))
								{
									int.TryParse(inputBox.Text, out mAnimationFrameToHtmlTo);
								}
								else
								{
									mAnimationFrameToHtmlTo = frameMax;
								}
							}
							else
							{
								bContinue = false;
							}
						}

						if(bContinue)
						{
							statMessage.Text = "Preparing animation...";
							await browser.ThreadSleep(100);

							frameMax = mAnimationFrameToHtmlTo;
							renderedFrames =
								SvgAnimationFrameCollection.RenderScenes(
									animation.Frames, animation.Scenes);
							if(renderedFrames.Count > 0)
							{
								//	Prime the log.
								log.Add(
									"Frame\t" +
									"Character\t" +
									"Property\t" +
									"Value");
								//	Start the per-character tracker.
								frameMax = renderedFrames.Max(mx => mx.FrameIndex);
								charTrack = new SvgAnimationCharacterTrackCollection(
									renderedFrames);
								//	Initialize monitored properties.
								foreach(SvgAnimationCharacterTrackItem character in charTrack)
								{
									if(character.HasRotation)
									{
										await browser.LoadAttributes(character.CharacterName,
											element);
										foreach(string monitoredProperty in
											character.MonitoredProperties)
										{
											if(element.Attributes.Exists(mp =>
												mp.Name == monitoredProperty))
											{
												//	A monitored property was found.
												attribute = element.Attributes.First(mp =>
													mp.Name == monitoredProperty);
												dValue = 0.0;
												if(IsDouble(attribute.Value))
												{
													double.TryParse(attribute.Value.ToString(),
														out dValue);
												}
												//	Monitoring is pixel-based.
												switch(monitoredProperty)
												{
													case "x":
													case "width":
														dValue = userUnit.ToPixelX(dValue);
														break;
													case "y":
													case "height":
														dValue = userUnit.ToPixelY(dValue);
														break;
												}
												character.SetMonitoredPropertyValue(
													monitoredProperty, dValue);
											}
										}
									}
								}
								for(frameIndex = 0;
									frameIndex <= mAnimationFrameToHtmlTo; frameIndex++)
								{
									//	Even if starting from a different frame, 0 is used to
									//	initialize everything.
									foreach(SvgAnimationCharacterTrackItem character in charTrack)
									{
										//if(frameIndex == 119 &&
										//	character.CharacterName == "ThoughtBubble1")
										//{
										//	Debug.WriteLine("Break here...");
										//}
										//if(frameIndex == 198 &&
										//	character.CharacterName == "TaskList1")
										//{
										//	Debug.WriteLine("Break here...");
										//}
										//if(frameIndex == 415 &&
										//	character.CharacterName == "IanSit3")
										//{
										//	Debug.WriteLine("Break here...");
										//}
										//if(frameIndex == 1057 &&
										//	character.CharacterName == "IanPoint1")
										//{
										//	Debug.WriteLine("Break here...");
										//}
										character.UpdateBand(frameIndex, 0, frameMax);
										if(frameIndex == mAnimationFrameToHtmlFrom &&
											frameIndex > 0)
										{
											//	At the starting frame, make sure to get all current
											//	properties for all characters.
											properties = character.GetPropertiesImplied(frameIndex);
										}
										else
										{
											//	This character is ready to be positioned.
											properties = character.GetPropertiesAtFrame(frameIndex);
										}
										foreach(KeyValuePair<string, object> property in properties)
										{
											switch(property.Key)
											{
												case "display":
													//	Css.
													//	String.
													browser.Css($"#{character.CharacterName}",
														property.Key, property.Value);
													break;
												case "height":
													//	Attr.
													//	Number.
													browser.Attr($"#{character.CharacterName}",
														property.Key,
														userUnit.ToUserY((double)property.Value));
													break;
												case "opacity":
													//	Css.
													//	Number.
													browser.Css($"#{character.CharacterName}",
														property.Key, property.Value);
													break;
												case "rotation":
													//	Rotation macro. Rotates at center.
													browser.Rotation($"#{character.CharacterName}",
														property.Value,
														userUnit.ToUserX(character.LastX),
														userUnit.ToUserY(character.LastY),
														userUnit.ToUserX(character.LastWidth),
														userUnit.ToUserY(character.LastHeight),
														element);
													break;
												//	Number.
												case "width":
													//	Attr.
													//	Number.
													browser.Attr($"#{character.CharacterName}",
														property.Key,
														userUnit.ToUserX((double)property.Value));
													break;
												case "x":
													//	Attr.
													//	Number.
													browser.Attr($"#{character.CharacterName}",
														property.Key,
														userUnit.ToUserX((double)property.Value));
													break;
												case "y":
													//	Attr.
													//	Number.
													browser.Attr($"#{character.CharacterName}",
														property.Key,
														userUnit.ToUserY((double)property.Value));
													break;
											}
											if(character.HasRotation && character.LastRotation != 0.0)
											{
												if(property.Key == "x" || property.Key == "y" ||
													property.Key == "width" || property.Key == "height")
												{
													//	If the character has rotation, that setting must
													//	also be corrected with any change to location or
													//	scale.
													browser.Rotation($"#{character.CharacterName}",
														character.LastRotation,
														userUnit.ToUserX(character.LastX),
														userUnit.ToUserY(character.LastY),
														userUnit.ToUserX(character.LastWidth),
														userUnit.ToUserY(character.LastHeight),
														element);
												}
											}
											//	Log the action.
											log.Add(
												$"{frameIndex}\t" +
												$"{character.CharacterName}\t" +
												$"{property.Key}\t" +
												$"{property.Value}");
										}
									}
									//	Frame is ready to display for all characters.
									statMessage.Text = $"Frame: {frameIndex}...";
									statProg.Value =
										(int)(((double)frameIndex / (double)frameMax) * 100.0);
									await browser.ThreadSleep(100);
									if(mEscPressed)
									{
										bContinue = false;
										break;
									}
									if(frameIndex == 0 && mAnimationFrameToHtmlFrom > 1)
									{
										frameIndex = mAnimationFrameToHtmlFrom - 1;
									}
								}
							}
						}
					}

					if(bContinue)
					{
						File.WriteAllText(
							"C:/Temp/ScaffoldLog.txt",
							String.Join("\r\n", log.ToArray()));
						statMessage.Text = "Animation Loaded...";
					}
					else
					{
						statMessage.Text = "Animation Cancelled...";
					}

					statProg.Value = 0;
				}
				else
				{
					statMessage.Text = "Open Cancelled...";
				}
			}
			catch(Exception ex)
			{
				Debug.WriteLine(
					$"Error on mnuToolsAnimationFrameToHTML: {ex.Message}");
				if(ex.Message.IndexOf("parsing") > 0)
				{
					MessageBox.Show("Error parsing timeline: " + ex.Message,
						"Frame [N] to HTML");
					statMessage.Text = "Open Cancelled...";
				}
				else if(ex.Message.IndexOf("used by another process") > 0)
				{
					statMessage.Text = "Log file was not written...";
				}
				else
				{
					statMessage.Text = "Animation Completed...";
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsAnimationFrameNToSVG_Click																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Tools / Animation / Draw Frame [N] to SVG View menu option has
		/// been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsAnimationFrameNToSVG_Click(object sender,
			EventArgs e)
		{
			SvgAnimation animation = null;
			bool bContinue = true;
			string content = "";
			OpenFileDialog dialog = new OpenFileDialog();
			int frameIndex = 0;
			int frameMax = 0;
			frmInputBox inputBox = new frmInputBox();
			string[] values = new string[0];

			btnSlideEditor.Selected = true;
			mEscPressed = false;

			statMessage.Text = "Loading Timeline File...";
			dialog.Filter = "Timeline JSON (*.timeline.json)|*.timeline.json|" +
				"All Files (*.*)|*.*";
			dialog.FilterIndex = 0;
			dialog.DefaultExt = ".timeline.json";
			dialog.AddExtension = true;
			dialog.CheckFileExists = true;
			dialog.CheckPathExists = true;
			dialog.SupportMultiDottedExtensions = true;
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				content = File.ReadAllText(dialog.FileName);
				animation = JsonConvert.DeserializeObject<SvgAnimation>(content);

				//	Discrete timeline.
				if(animation.Frames.Count > 0)
				{
					frameMax = animation.Frames.Max(x => x.FrameIndex);
					if(mAnimationFrameToHtmlFrom < 0 ||
						mAnimationFrameToHtmlFrom >
						frameMax)
					{
						mAnimationFrameToHtmlFrom = 0;
					}
					if(mAnimationFrameToHtmlTo < 0 ||
						mAnimationFrameToHtmlTo >
						frameMax)
					{
						mAnimationFrameToHtmlTo = frameMax;
					}
					CenterOver(this, inputBox);
					inputBox.Title = "Starting Frame";
					inputBox.Prompt = "Enter the frame at which to start:";
					inputBox.Text = mAnimationFrameToHtmlFrom.ToString();
					if(inputBox.ShowDialog() == DialogResult.OK)
					{
						if(IsInt(inputBox.Text))
						{
							int.TryParse(inputBox.Text, out frameIndex);
						}
						else
						{
							frameIndex = 0;
						}
					}
					else
					{
						bContinue = false;
					}
					if(bContinue)
					{
						if(frameIndex > -1 && frameIndex <= frameMax)
						{
							//	In range.
							mAnimationFrameToHtmlFrom = frameIndex;
						}
						else
						{
							frameIndex = 0;
						}
						if(mAnimationFrameToHtmlTo < frameIndex)
						{
							mAnimationFrameToHtmlTo = frameIndex;
						}
						inputBox.Title = "Ending Frame";
						inputBox.Prompt = "Enter the frame at which to stop:";
						inputBox.Text = mAnimationFrameToHtmlTo.ToString();
						if(inputBox.ShowDialog() == DialogResult.OK)
						{
							if(IsInt(inputBox.Text))
							{
								int.TryParse(inputBox.Text, out mAnimationFrameToHtmlTo);
							}
							else
							{
								mAnimationFrameToHtmlTo = frameMax;
							}
						}
						else
						{
							bContinue = false;
						}
					}
				}
				else
				{
					bContinue = false;
				}
				if(bContinue)
				{
					//	Continue. Start / End frames have been specified.
					SendAnimationToSVG(animation,
						startFrame: mAnimationFrameToHtmlFrom,
						endFrame: mAnimationFrameToHtmlTo);
				}
				else
				{
					statMessage.Text = "Animation Cancelled...";
				}
				statProg.Value = 0;
			}
			else
			{
				statMessage.Text = "Open Cancelled...";
			}

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsAnimationFrameFlipbook_Click																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Tools / Animation / Frame Switcher menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsAnimationFrameFlipbook_Click(object sender,
			EventArgs e)
		{
			frmFrameFlipbook form = new frmFrameFlipbook();

			form.Show();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsAnimationSaveFrames_Click																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Tools / Animation / Save Animation Frames To Disk menu option has
		/// been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsAnimationSaveFrames_Click(object sender, EventArgs e)
		{
			SvgAnimation animation = null;
			bool bContinue = true;
			StringBuilder builder = new StringBuilder();
			string content = "";
			OpenFileDialog dialog = new OpenFileDialog();
			FileInfo file = null;
			string filePattern = "C:/Temp/Movies/Frames/Frame{FrameIndex:DX}.png";
			frmInputBox inputBox = new frmInputBox();
			string[] values = new string[0];

			btnSlideEditor.Selected = true;
			mEscPressed = false;

			statMessage.Text = "Loading Timeline File...";
			dialog.Filter = "Timeline JSON (*.timeline.json)|*.timeline.json|" +
				"All Files (*.*)|*.*";
			dialog.FilterIndex = 0;
			dialog.DefaultExt = ".timeline.json";
			dialog.AddExtension = true;
			dialog.CheckFileExists = true;
			dialog.CheckPathExists = true;
			dialog.SupportMultiDottedExtensions = true;
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				content = File.ReadAllText(dialog.FileName);
				animation = JsonConvert.DeserializeObject<SvgAnimation>(content);

				//	Output file pattern.
				if(animation.Frames.Count > 0)
				{
					CenterOver(this, inputBox);
					inputBox.Title = "Output File Pattern";
					inputBox.Prompt = "Enter the output file pattern:";
					inputBox.Text = filePattern;
					if(inputBox.ShowDialog() == DialogResult.OK)
					{
						filePattern = inputBox.Text;
					}
					else
					{
						bContinue = false;
					}
				}
				else
				{
					bContinue = false;
				}
				if(bContinue)
				{
					//	Continue. Start / End frames have been specified.
					//	Assure the folder exists.
					if(builder.Length > 0)
					{
						builder.Remove(0, builder.Length);
					}
					builder.Append(filePattern.Replace("{", "_").Replace("}", "_"));
					while(builder.ToString().Count(x => x == ':') > 1)
					{
						builder[builder.ToString().LastIndexOf(':')] = '_';
					}
					file = new FileInfo(builder.ToString());
					if(!file.Directory.Exists)
					{
						file.Directory.Create();
					}
					SendAnimationToSVG(animation,
						saveImages: true, imageFilePattern: filePattern);
				}
				else
				{
					statMessage.Text = "Animation Cancelled...";
				}
				statProg.Value = 0;
			}
			else
			{
				statMessage.Text = "Open Cancelled...";
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsAnimationTimelineFileReport_Click															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Tools / Animation / Timeline File Report menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsAnimationTimelineFileReport_Click(object sender,
			EventArgs e)
		{
			SvgAnimation animation = null;
			string content = "";
			string description = "";
			OpenFileDialog dialog = new OpenFileDialog();
			FileInfo file = null;
			bool found = false;
			StringBuilder line = new StringBuilder();
			List<string> lineDiscreteTimeline = new List<string>();
			StringBuilder listing = new StringBuilder();
			SvgAnimationFrameCollection renderedFrames = null;
			//SvgAnimationResolvedFrameCollection resolvedFrames = null;
			List<string> results = new List<string>();
			string sceneMarkName = "";

			statMessage.Text = "Loading Timeline File...";
			dialog.Filter = "Timeline JSON (*.timeline.json)|*.timeline.json|" +
				"All Files (*.*)|*.*";
			dialog.FilterIndex = 0;
			dialog.DefaultExt = ".timeline.json";
			dialog.AddExtension = true;
			dialog.CheckFileExists = true;
			dialog.CheckPathExists = true;
			dialog.SupportMultiDottedExtensions = true;
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				file = new FileInfo(dialog.FileName);
				content = File.ReadAllText(dialog.FileName);
				animation = JsonConvert.DeserializeObject<SvgAnimation>(content);
				//	Header information.
				listing.Append("<h2>Timeline File Report</h2>");
				listing.Append($"<p>{file.Name}</p>");
				listing.Append($"<p>{animation.Frames.Count} keyframes");
				if(animation.Frames.Count > 0)
				{
					listing.Append($", from {animation.Frames.Min(x => x.FrameIndex)}");
					listing.Append($" to {animation.Frames.Max(x => x.FrameIndex)}.");
				}
				else
				{
					listing.Append("...");
				}
				listing.Append("</p>");
				listing.Append($"{animation.Scenes.Count} scenes");
				if(animation.Scenes.Count > 0)
				{
					listing.Append(" with a total of ");
					listing.Append(
						$"{animation.Scenes.Sum(x => x.Marks.Count())} marks.");
				}
				else
				{
					listing.Append("...");
				}
				listing.Append("</p>");
				listing.Append(ResourceMain.htmlBlankLine);
				//	Scenes and Marks.
				listing.Append("<h3>Scenes and Marks</h3>");
				listing.Append("<ul>");
				foreach(SvgAnimationSceneItem scene in animation.Scenes)
				{
					listing.Append("<li>");
					listing.Append(scene.SceneName);
					if(scene.Marks.Count > 0)
					{
						listing.Append("<ul>");
						foreach(SvgAnimationSceneMarkItem mark in scene.Marks)
						{
							listing.Append($"<li>{mark.MarkName}</li>");
						}
						listing.Append("</ul>");
					}
					listing.Append("</li>");
				}
				listing.Append("</ul>");
				listing.Append(ResourceMain.htmlBlankLine);
				//	Scenes, Marks, and Characters Not Used In A Keyframe.
				listing.Append(
					"<h3>Scenes, Marks, and Characters Not Used In A Keyframe</h3>");
				results.Clear();
				foreach(SvgAnimationSceneItem scene in animation.Scenes)
				{
					foreach(SvgAnimationSceneMarkItem mark in scene.Marks)
					{
						foreach(SvgAnimationSceneMarkCharacterItem setting in
							mark.MarkCharacters)
						{
							found = false;
							sceneMarkName =
								scene.SceneName.ToLower() + "." +
								mark.MarkName.ToLower();
							foreach(SvgAnimationFrameItem frame in animation.Frames)
							{
								if(frame.Elements.Exists(x =>
									x.ElementMark.ToLower() == sceneMarkName &&
									x.ElementID == setting.CharacterName &&
									x.ElementType == SvgAnimationElementTypeEnum.SceneMark))
								{
									found = true;
									break;
								}
							}
							if(!found)
							{
								results.Add($"{setting.CharacterName} in {sceneMarkName}");
							}
						}
					}
				}
				if(results.Count > 0)
				{
					listing.Append("<ul>");
					foreach(string result in results)
					{
						listing.Append($"<li>{result}</li>");
					}
					listing.Append("</ul>");
				}
				else
				{
					listing.Append("<p>None found.</p>");
				}
				listing.Append(ResourceMain.htmlBlankLine);
				//	Keyframes Referencing Non-Existent Marks.
				listing.Append("<h3>Keyframes Referencing Non-Existent Marks</h3>");
				results.Clear();
				foreach(SvgAnimationFrameItem frame in animation.Frames)
				{
					foreach(SvgAnimationFrameElementItem element in frame.Elements)
					{
						if(element.ElementType == SvgAnimationElementTypeEnum.SceneMark)
						{
							//	Scene mark found.
							found = true;
							//	Attempt to find it in the scenes list.
							if(SvgAnimationSceneCollection.FindMark(animation.Scenes,
								element.ElementMark, element.ElementID) == null)
							{
								found = false;
							}
							if(!found)
							{
								results.Add(
									$"{element.ElementID} in {element.ElementMark.ToLower()}");
							}
						}
					}
				}
				if(results.Count > 0)
				{
					listing.Append("<ul>");
					foreach(string result in results)
					{
						listing.Append($"<li>{result}</li>");
					}
					listing.Append("</ul>");
				}
				else
				{
					listing.Append("<p>None found.</p>");
				}
				listing.Append(ResourceMain.htmlBlankLine);
				//	Discrete Timeline.
				listing.Append("<h3>Discrete Timeline</h3>");
				if(animation.Frames.Count > 0)
				{
					results.Clear();
					renderedFrames =
						SvgAnimationFrameCollection.RenderScenes(
							animation.Frames, animation.Scenes);
					if(renderedFrames.Count > 0)
					{
						listing.Append(
							$"<p>{renderedFrames.Sum(x => x.Elements.Count)} " +
							"total entries...</p>");
						listing.Append("<table id='tableDiscrete'>");
						listing.Append("<tr><th>Frame</th>");
						listing.Append("<th>Character</th><th>Property</th>");
						listing.Append("<th>Value</th><th>Style</th>");
						listing.Append("<th>Description</th></tr>");
						foreach(SvgAnimationFrameItem renderedFrame in
							renderedFrames)
						{
							foreach(SvgAnimationFrameElementItem element in
								renderedFrame.Elements)
							{
								foreach(KeyValuePair<string, object> property in
									element.Properties)
								{
									if(line.Length > 0)
									{
										line.Remove(0, line.Length);
									}
									line.Append("$('#tableDiscrete tr:last').after(`");
									line.Append("<tr>");
									line.Append($"<td>{renderedFrame.FrameIndex}</td>");
									line.Append($"<td>{element.ElementID}</td>");
									line.Append($"<td>{property.Key}</td>");
									if(IsDouble(property.Value))
									{
										line.Append($"<td>{property.Value:0.000}</td>");
									}
									else if(IsBool(property.Value))
									{
										line.Append(
											$"<td>{Convert.ToBoolean(property.Value)}</td>");
									}
									else
									{
										line.Append($"<td>{property.Value}</td>");
									}
									line.Append($"<td>{element.InterpolationType}, ");
									line.Append($"{element.InterpolationCount}</td>");
									description = element.Description.Length > 0 ?
										element.Description : renderedFrame.Description;
									line.Append($"<td>{description}</td>");
									line.Append("</tr>");
									line.Append("`);");
									lineDiscreteTimeline.Add(line.ToString());
								}
							}
						}
						listing.Append("</table>");
					}
				}
				else
				{
					listing.Append("None found...");
				}
				listing.Append(ResourceMain.htmlBlankLine);
				//	Load the viewer.
				statMessage.Text = "Animation Loaded...";
				frmHTML form = new frmHTML();
				form.SetTitle("Animation Scenes and Marks");
				form.SetBodyHTML(listing.ToString());
				foreach(string lineItem in lineDiscreteTimeline)
				{
					form.JavaScriptCommand(lineItem);
				}
				form.ShowDialog();
			}
			else
			{
				statMessage.Text = "Open Cancelled...";
			}

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsBase64Raw_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Tools / base64 Data Packing / To Raw String menu option has been
		/// clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsBase64Raw_Click(object sender, EventArgs e)
		{
			//	XXXXX ... XXX
			byte[] buffer = new byte[0];
			StringBuilder builder = new StringBuilder();
			OpenFileDialog dialog = new OpenFileDialog();
			FileInfo file = null;

			dialog.AddExtension = false;
			dialog.CheckFileExists = true;
			dialog.CheckPathExists = true;
			dialog.DereferenceLinks = true;
			dialog.Filter = "All Files (*.*)|*.*";
			dialog.FilterIndex = 0;
			dialog.Multiselect = false;
			dialog.Title = "Read File for base64";
			dialog.ValidateNames = true;
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				file = new FileInfo(dialog.FileName);
				buffer = File.ReadAllBytes(dialog.FileName);
			}
			if(buffer.Length > 0)
			{
				builder.Append(Convert.ToBase64String(buffer));
				Clipboard.SetData(DataFormats.StringFormat, builder.ToString());
				MessageBox.Show(
					$"{file.Name} has been copied to the clipboard as base64.",
					"File to base64 - Raw");
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsBase64SRC_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Tools / base64 Data Packing / To HTML IMG SRC menu option has been
		/// clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsBase64SRC_Click(object sender, EventArgs e)
		{
			//	data:image/png;base64,XXXXX ... XXX
			byte[] buffer = new byte[0];
			StringBuilder builder = new StringBuilder();
			OpenFileDialog dialog = new OpenFileDialog();
			string extension = "";
			FileInfo file = null;

			dialog.AddExtension = false;
			dialog.CheckFileExists = true;
			dialog.CheckPathExists = true;
			dialog.DereferenceLinks = true;
			dialog.Filter = "All Files (*.*)|*.*";
			dialog.FilterIndex = 0;
			dialog.Multiselect = false;
			dialog.Title = "Read File for base64";
			dialog.ValidateNames = true;
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				file = new FileInfo(dialog.FileName);
				extension = file.Extension;
				buffer = File.ReadAllBytes(dialog.FileName);
			}
			if(buffer.Length > 0)
			{
				builder.Append("data:");
				builder.Append(MimeType(extension));
				builder.Append(";base64,");
				builder.Append(Convert.ToBase64String(buffer));
				Clipboard.SetData(DataFormats.StringFormat, builder.ToString());
				MessageBox.Show(
					$"{file.Name} has been copied to the clipboard as base64.",
					"File to base64 - HTML IMG SRC");
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsBase64UClipboard_Click																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Tools / base64 Data Unpacking / From Clipboard menu option has been
		/// clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsBase64UClipboard_Click(object sender, EventArgs e)
		{
			string content = Clipboard.GetText();
			byte[] data = new byte[0];
			SaveFileDialog dialogs = null;

			if(content?.Length > 0)
			{
				dialogs = new SaveFileDialog();
				dialogs.CheckFileExists = false;
				dialogs.CheckPathExists = true;
				dialogs.DereferenceLinks = true;
				dialogs.Filter = "All Files (*.*)|*.*";
				dialogs.FilterIndex = 0;
				dialogs.OverwritePrompt = true;
				dialogs.SupportMultiDottedExtensions = true;
				dialogs.Title = "Save Data File From base64 Conversion";
				if(dialogs.ShowDialog() == DialogResult.OK)
				{
					try
					{
						data = Convert.FromBase64String(content);
					}
					catch { }
					File.WriteAllBytes(dialogs.FileName, data);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsBase64UFile_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Tools / base64 Data Unpacking / From File menu option has been
		/// clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsBase64UFile_Click(object sender, EventArgs e)
		{
			string content = "";
			byte[] data = new byte[0];
			OpenFileDialog dialogo = new OpenFileDialog();
			SaveFileDialog dialogs = new SaveFileDialog();

			dialogo.CheckFileExists = true;
			dialogo.CheckPathExists = true;
			dialogo.DereferenceLinks = true;
			dialogo.Filter = "All Files (*.*)|*.*";
			dialogo.FilterIndex = 0;
			dialogo.Multiselect = false;
			dialogo.SupportMultiDottedExtensions = true;
			dialogo.Title = "Select File Containing base64 String";
			if(dialogo.ShowDialog() == DialogResult.OK)
			{
				content = File.ReadAllText(dialogo.FileName);
				if(content?.Length > 0)
				{
					try
					{
						data = Convert.FromBase64String(content);
					}
					catch { }
					if(data.Length > 0)
					{
						dialogs = new SaveFileDialog();
						dialogs.CheckFileExists = false;
						dialogs.CheckPathExists = true;
						dialogs.DereferenceLinks = true;
						dialogs.Filter = "All Files (*.*)|*.*";
						dialogs.FilterIndex = 0;
						dialogs.OverwritePrompt = true;
						dialogs.SupportMultiDottedExtensions = true;
						dialogs.Title = "Save Resulting Data From base64 Conversion";
						if(dialogs.ShowDialog() == DialogResult.OK)
						{
							File.WriteAllBytes(dialogs.FileName, data);
						}
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsBase64URL_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Tools / base64 Data Packing / To CSS URL menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsBase64URL_Click(object sender, EventArgs e)
		{
			//	url('data:image/png;base64,XXXXX ... XXX')
			byte[] buffer = new byte[0];
			StringBuilder builder = new StringBuilder();
			OpenFileDialog dialog = new OpenFileDialog();
			string extension = "";
			FileInfo file = null;

			dialog.AddExtension = false;
			dialog.CheckFileExists = true;
			dialog.CheckPathExists = true;
			dialog.DereferenceLinks = true;
			dialog.Filter = "All Files (*.*)|*.*";
			dialog.FilterIndex = 0;
			dialog.Multiselect = false;
			dialog.Title = "Read File for base64";
			dialog.ValidateNames = true;
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				file = new FileInfo(dialog.FileName);
				extension = file.Extension;
				buffer = File.ReadAllBytes(dialog.FileName);
			}
			if(buffer.Length > 0)
			{
				builder.Append("url('data:");
				builder.Append(MimeType(extension));
				builder.Append(";base64,");
				builder.Append(Convert.ToBase64String(buffer));
				builder.Append("')");
				Clipboard.SetData(DataFormats.StringFormat, builder.ToString());
				MessageBox.Show(
					$"{file.Name} has been copied to the clipboard as base64.",
					"File to base64 - CSS URL");
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsChatbotEmulateBeginning_Click																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Tools / Chatbot Emulator / Emulate From Beginning menu option has
		/// been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsChatbotEmulateBeginning_Click(object sender,
			EventArgs e)
		{
			ChatbotEmulator();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsChatbotEmulateSelected_Click																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Tools / Chatbot Emulator / Emulate From Selected Node menu option
		/// has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsChatbotEmulateSelected_Click(object sender,
			EventArgs e)
		{
			string start = "";

			if(nodeControl.SelectionQueue.Count > 0)
			{
				start = nodeControl.SelectionQueue[0].Ticket;
			}
			ChatbotEmulator(start);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsClipboardClear_Click																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Tools / Clipboard / Clear menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsClipboardClear_Click(object sender, EventArgs e)
		{
			Clipboard.Clear();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsClipboardSave_Click																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Tools / Clipboard / Save as File... menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsClipboardSave_Click(object sender, EventArgs e)
		{
			byte[] buffer = new byte[0];
			DataObject dataobject = (DataObject)Clipboard.GetDataObject();
			SaveFileDialog dialog = null;
			string[] formats = new string[0];
			BinaryFormatter formatter = null;
			MemoryStream memory = null;
			NamedObjectItem page = null;
			NamedObjectCollection pages = new NamedObjectCollection();

			this.Cursor = Cursors.WaitCursor;
			this.Refresh();
			//	Prepare the data.
			if(dataobject != null)
			{
				formats = dataobject.GetFormats();
			}
			if(formats.Length > 0)
			{
				foreach(string formatName in formats)
				{
					page = new NamedObjectItem();
					page.Name = formatName;
					page.Value = dataobject.GetData(formatName);
					pages.Add(page);
				}
			}
			if(pages.Count > 0)
			{
				//	Each name and entry has a 4 byte length entry.
				memory = new MemoryStream();
				formatter = new BinaryFormatter();
				try
				{
					formatter.Serialize(memory, pages);
				}
				catch(Exception ex)
				{
					memory.Dispose();
					memory = null;
					MessageBox.Show($"Could not serialize clipboard: {ex.Message}",
						"Save Clipboard File");
				}
			}
			//	Store the file.
			if(memory != null)
			{
				dialog = new SaveFileDialog();
				dialog.AddExtension = true;
				dialog.CheckPathExists = true;
				dialog.DefaultExt = ".bin";
				dialog.DereferenceLinks = true;
				dialog.Filter = "Binary files (*.bin)|*.bin|" +
					"All files (*.*)|*.*";
				dialog.FilterIndex = 0;
				dialog.OverwritePrompt = true;
				dialog.Title = "Save Clipboard File As";
				dialog.ValidateNames = true;
				if(dialog.ShowDialog() == DialogResult.OK)
				{
					File.WriteAllBytes(dialog.FileName, memory.ToArray());
				}
				memory.Dispose();
			}
			this.Cursor = Cursors.Default;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsClipboardLoad_Click																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Tools / Clipboard / Load from File... menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsClipboardLoad_Click(object sender, EventArgs e)
		{
			byte[] buffer = new byte[0];
			DataObject dataobject = null;
			OpenFileDialog dialog = null;
			BinaryFormatter formatter = null;
			MemoryStream memory = null;
			NamedObjectCollection pages = null;

			this.Cursor = Cursors.WaitCursor;
			this.Refresh();
			//	Load the file.
			dialog = new OpenFileDialog();
			dialog.AddExtension = true;
			dialog.CheckFileExists = true;
			dialog.CheckPathExists = true;
			dialog.DefaultExt = ".bin";
			dialog.DereferenceLinks = true;
			dialog.Filter = "Binary Files (*.bin)|*.bin|" +
				"All files (*.*)|*.*";
			dialog.FilterIndex = 0;
			dialog.Multiselect = false;
			dialog.SupportMultiDottedExtensions = true;
			dialog.Title = "Load File to Clipboard";
			dialog.ValidateNames = true;
			if(dialog.ShowDialog() == DialogResult.OK)
			{
				buffer = File.ReadAllBytes(dialog.FileName);
			}
			if(buffer.Length > 0)
			{
				try
				{
					memory = new MemoryStream(buffer);
					formatter = new BinaryFormatter();
					pages = (NamedObjectCollection)formatter.Deserialize(memory);
				}
				catch(Exception ex)
				{
					MessageBox.Show($"Could not deserialize clipboard: {ex.Message}",
						"Load Clipboard File");
				}
				finally
				{
					memory.Close();
					memory.Dispose();
				}
			}
			//	Prepare the clipboard.
			if(pages.Count > 0)
			{
				Clipboard.Clear();
				dataobject = new DataObject();
				foreach(NamedObjectItem page in pages)
				{
					dataobject.SetData(page.Name, false, page.Value);
				}
				Clipboard.SetDataObject(dataobject, true);
			}
			this.Cursor = Cursors.Default;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsColorPalette_Click																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Tools / Color Palette menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsColorPalette_Click(object sender, EventArgs e)
		{
			frmColorSelect dialog = new frmColorSelect();
			dialog.Color = FromHex("#204060FF");

			CenterOver(this, dialog);
			dialog.Show();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsControlNodeControl_Click																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Tools / Control Reports / NodeControl Information menu option has
		/// been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsControlNodeControl_Click(object sender, EventArgs e)
		{
			StringBuilder builder = new StringBuilder();
			Point canvasMouse = nodeControl.CanvasMouse;
			//frmHTML form = new frmHTML();
			PointF fractionMouse = nodeControl.GetMouseCenterAbstract();
			Point mouseLocation = GetNodeControlMouseLocation();
			double value = 0.0;

			btnHTML.Selected = true;

			htmlGeneric.JavaScriptCommand("$('body').css('overflow', 'scroll');");
			builder.Append("<div style='margin: 24px'>");
			builder.Append("<h2>NodeControl Information</h2>");
			builder.Append("<style>.right { text-align: right; }</style>");
			builder.Append("<table>");
			builder.Append("<tr><td>View Width:</td><td class='right'>");
			builder.Append($"{nodeControl.Width}</td></tr>");
			builder.Append("<tr><td>View Height:</td><td class='right'>");
			builder.Append($"{nodeControl.Height}</td></tr>");
			builder.Append("<tr><td>Canvas Width:</td><td class='right'>");
			builder.Append($"{nodeControl.CanvasWidth}</td></tr>");
			builder.Append("<tr><td>Canvas Height:</td><td class='right'>");
			builder.Append($"{nodeControl.CanvasHeight}</td></tr>");
			builder.Append("<tr><td>Scroll Horizontal:</td><td class='right'>");
			builder.Append($"{nodeControl.HorizontalScroll.Value}</td></tr>");
			builder.Append("<tr><td>Scroll Vertical:</td><td class='right'>");
			builder.Append($"{nodeControl.VerticalScroll.Value}</td></tr>");
			builder.Append("<tr><td>Local Mouse X:</td><td class='right'>");
			builder.Append($"{mouseLocation.X}</td></tr>");
			builder.Append("<tr><td>Local Mouse Y:</td><td class='right'>");
			builder.Append($"{mouseLocation.Y}</td></tr>");
			builder.Append("<tr><td>Canvas Mouse X:</td><td class='right'>");
			builder.Append($"{canvasMouse.X}</td></tr>");
			builder.Append("<tr><td>Canvas Mouse Y:</td><td class='right'>");
			builder.Append($"{canvasMouse.Y}</td></tr>");
			value = fractionMouse.X * 100.0f;
			builder.Append(
				"<tr><td>Canvas Mouse X Fraction:</td><td class='right'>");
			builder.Append($"{value:0.000}%</td></tr>");
			value = fractionMouse.Y * 100.0f;
			builder.Append(
				"<tr><td>Canvas Mouse Y Fraction:</td><td class='right'>");
			builder.Append($"{value:0.000}%</td></tr>");
			builder.Append("</table>");
			builder.Append("</div>");
			//form.SetBodyHTML(builder.ToString());
			//form.Text = "Node Control Report";
			//form.Show();
			htmlGeneric.SetBodyHTML(builder.ToString());
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsControlNodeMeasurement_Click																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Tools / Control Reports / Node Measurement tool option has been
		/// clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsControlNodeMeasurement_Click(object sender,
			EventArgs e)
		{
			StringBuilder builder = new StringBuilder();
			string text = "";

			btnHTML.Selected = true;

			htmlGeneric.JavaScriptCommand("$('body').css('overflow', 'scroll');");
			builder.Append("<style type='text/css'>");
			builder.Append(".card { ");
			builder.Append("vertical-align: top;");
			builder.Append("display: inline-block;");
			builder.Append("max-width: 256px;");
			builder.Append("background-color: #f0f0f0;");
			builder.Append("color: #3f3f3f;");
			builder.Append("border-radius: 8px;");
			builder.Append("border: 4px solid #7f7f7f;");
			builder.Append("margin: 20px;");
			builder.Append("padding: 8px;");
			builder.Append(" }");
			builder.Append(".card table td, .card table th { ");
			builder.Append("background-color: transparent;");
			builder.Append("color: #3f3f3f;");
			builder.Append(" }");
			builder.Append(".card table tr { border: none; }");
			builder.Append(".container { margin: 24px; }");
			builder.Append(".right { text-align: right; }");
			builder.Append("</style>");
			builder.Append("<div class='container'>");
			builder.Append("<h2>Node Measurement Information</h2>");
			foreach(NodeItem node in nodeControl.Nodes)
			{
				text = node["Question"].StringValue();
				if(text.Length > 20)
				{
					text = text.Substring(0, 17) + "...";
				}
				builder.Append("<div class='card'>");
				builder.Append("<table>");
				builder.Append($"<tr><td>Text:</td><td>{text}</td></tr>");
				builder.Append($"<tr><td>X:</td><td>{node.X:0.000}</td></tr>");
				builder.Append($"<tr><td>Y:</td><td>{node.Y:0.000}</td></tr>");
				builder.Append(
					$"<tr><td>Width:</td><td>{node.Width:0.000}</td></tr>");
				builder.Append(
					$"<tr><td>Height:</td><td>{node.Height:0.000}</td></tr>");
				builder.Append(
					$"<tr><td>TitleHeight:</td><td>{node.TitleHeight:0.000}</td></tr>");
				builder.Append("</table>");
				builder.Append("</div>");
			}
			builder.Append("</div>");
			htmlGeneric.SetBodyHTML(builder.ToString());
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsControlUndo_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Tools / Control Reports / Undo Buffer Contents menu option has
		/// been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsControlUndo_Click(object sender, EventArgs e)
		{
			int count = 0;
			StringBuilder builder = new StringBuilder();

			btnHTML.Selected = true;

			foreach(UndoStack stack in mUndoPack)
			{
				count += stack.Count;
			}

			htmlGeneric.JavaScriptCommand("$('body').css('overflow', 'scroll');");
			builder.Append("<h2>Undo Buffer</h2>");
			builder.Append($"Count of items: {count}");
			builder.Append("<table>");
			builder.Append("<tr>");
			builder.Append("<th>Action</th><th>Item Type</th><th>Name</th>");
			builder.Append("</tr>");
			foreach(UndoStack stack in mUndoPack)
			{
				builder.Append("<tr>");
				builder.Append("<td colspan='3'>Package</td>");
				builder.Append("</tr>");
				foreach(UndoItem item in stack)
				{
					builder.Append("<tr>");
					builder.Append($"<td>{item.ActionType}</td>");
					builder.Append($"<td>{item.ItemType}</td>");
					if(item.Properties.Count > 0)
					{
						builder.Append($"<td>{item.Properties[0].Name}</td>");
					}
					else
					{
						builder.Append("<td>&nbsp;</td>");
					}
					builder.Append("</tr>");
				}
			}
			builder.Append("</table>");
			htmlGeneric.SetBodyHTML(builder.ToString());
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsPPAlignment_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Tools / PowerPoint / Text And Shape Alignment menu option has
		/// been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsPPAlignment_Click(object sender, EventArgs e)
		{
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsPPPlaceholderToTextboxes_Click																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Tools / PowerPoint / Change Content Placeholder To Textboxes menu
		/// option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsPPPlaceholderToTextboxes_Click(object sender,
			EventArgs e)
		{
			OfficeDriver driver = new OfficeDriver();

			driver.EnsurePowerPointRunning(true);
			driver.PlaceholderToTextboxes();

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsPPQuickAnimation_Click																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Tools / PowerPoint / Quick Animation menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsPPQuickAnimation_Click(object sender, EventArgs e)
		{
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsPPRemoveBullet_Click																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Tools / PowerPoint / Remove Bullet From Paragraphs menu option has
		/// been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsPPRemoveBullet_Click(object sender, EventArgs e)
		{
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsResourceGallery_Click																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Tools / Resource Gallery menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsResourceGallery_Click(object sender, EventArgs e)
		{
			frmResourceGallery dialog = new frmResourceGallery();

			//if(NodeFileInfo != null)
			//{
			//	dialog.LoadResources(nodeControl.Resources);
			//}
			dialog.Show();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuViewScrollLayout_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The View / Scroll / Scroll Layout Into View menu option has been
		/// clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuViewScrollLayout_Click(object sender, EventArgs e)
		{
			ScrollIntoView();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuViewScrollNode_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The View / Scroll / Scroll Selected Node Into View menu option has been
		/// clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuViewScrollNode_Click(object sender, EventArgs e)
		{
			if(nodeControl.SelectionQueue.Count > 0)
			{
				//	NOTE: This technique has an immediate response for the user.
				nodeControl.Focus();
				ScrollIntoView(nodeControl.SelectionQueue[0]);
				nodeControl.Invalidate();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuViewZoom100_Click																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// View / Zoom / 100% menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuViewZoom100_Click(object sender, EventArgs e)
		{
			PointF center = nodeControl.GetMouseCenterAbstract();
			Debug.WriteLine(
				$"Queue view center at: {center.X:0.000000}, {center.Y:0.000000}");
			nodeControl.DrawingScale = new SizeF(1f, 1f);
			nodeControl.QueueViewCenter(center);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuViewZoomIn_Click																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// View / Zoom / In menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuViewZoomIn_Click(object sender, EventArgs e)
		{
			PointF center = nodeControl.GetMouseCenterAbstract();
			Debug.WriteLine(
				$"Queue view center at: {center.X:0.000000}, {center.Y:0.000000}");
			nodeControl.DrawingScale = new SizeF(
				nodeControl.DrawingScale.Width * 1.2f,
				nodeControl.DrawingScale.Height * 1.2f);
			nodeControl.QueueViewCenter(center);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuViewZoomOut_Click																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// View / Zoom / Out menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuViewZoomOut_Click(object sender, EventArgs e)
		{
			PointF center = nodeControl.GetMouseCenterAbstract();
			Debug.WriteLine(
				$"Queue view center at: {center.X:0.000000}, {center.Y:0.000000}");
			nodeControl.DrawingScale = new SizeF(
				nodeControl.DrawingScale.Width / 1.2f,
				nodeControl.DrawingScale.Height / 1.2f);
			nodeControl.QueueViewCenter(center);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	mnuWindowHTMLViewer_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Window / HTML Viewer menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuWindowHTMLViewer_Click(object sender, EventArgs e)
		{
			btnHTML.Selected = true;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuWindowDecision_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Window / Decision Tree Editor menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuWindowDecision_Click(object sender, EventArgs e)
		{
			btnDecisionTreeEditor.Selected = true;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuWindowSlide_Click																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Window / Slide Editor menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuWindowSlide_Click(object sender, EventArgs e)
		{
			btnSlideEditor.Selected = true;
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* mUndo_ItemPushPop																											*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// An item has been pushed or popped on the undo stack.
		///// </summary>
		///// <param name="sender">
		///// The object raising this event.
		///// </param>
		///// <param name="e">
		///// Undo item event arguments.
		///// </param>
		//private void mUndo_ItemPushPop(object sender, UndoItemEventArgs e)
		//{
		//	UndoItem item = null;

		//	if(mUndo.Count > 0)
		//	{
		//		item = mUndo.Peek();
		//		mnuEditUndo.Enabled = true;
		//		mnuEditUndo.Text = $"&Undo {item.ItemType} {item.ActionType}";
		//	}
		//	else
		//	{
		//		mnuEditUndo.Text = "&Undo";
		//		mnuEditUndo.Enabled = false;
		//	}
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mUndo_StackPushPop																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// An stack has been pushed or popped on the undo pack.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Undo stack event arguments.
		/// </param>
		private void mUndo_StackPushPop(object sender, UndoStackEventArgs e)
		{
			if(mUndoPack.Count > 0)
			{
				mnuEditUndo.Enabled = true;
				mnuEditUndo.Text = $"&Undo {mUndoPack.GetDescription()}";
			}
			else
			{
				mnuEditUndo.Text = "&Undo";
				mnuEditUndo.Enabled = false;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* nodeControl_Click																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Check to see if a tool is being dropped on the canvas.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void nodeControl_Click(object sender, EventArgs e)
		{
			PointF controlLocation = PointF.Empty;
			Point mousePosition = Control.MousePosition;
			NodeItem node = null;
			SocketItem socket = null;

			if(mToolHandleActive)
			{
				controlLocation = ScaleDrawing(new Point(
					mousePosition.X - this.Location.X -
					panelWindowControl.PanelLeft - nodeControl.Location.X +
					nodeControl.HorizontalScroll.Value - 32,
					mousePosition.Y - this.Location.Y -
					panelWindowControl.PanelTop - nodeControl.Location.Y +
					nodeControl.VerticalScroll.Value - this.MainMenuHeight - 32),
					nodeControl.DrawingScale);
				Verbose(
					$"{mActiveToolType} Tool dropped at {controlLocation}");
				switch(mActiveToolType)
				{
					case ToolTypeEnum.NodeStart:
						node = new NodeItem();
						node.NodeType = "Start";
						node["Question"].Value = "Your question here";
						node["TitleProperty"].Value = "Question";
						node["StoryPageNumber"].Value = "1";
						node.X = controlLocation.X;
						node.Y = controlLocation.Y;
						if(nodeControl.Nodes.Count > 0)
						{
							node.ZOrder = nodeControl.Nodes.Max(x => x.ZOrder) + 1;
						}
						socket = new SocketItem();
						socket.SocketMode = SocketModeEnum.Output;
						socket["Index"].Value = "A";
						socket["Answer"].Value = "Answer A";
						socket["TitleProperty"].Value = "Answer";
						node.Sockets.Add(socket);
						nodeControl.Nodes.Add(node);
						break;
					case ToolTypeEnum.NodeFork:
						node = new NodeItem();
						node.NodeType = "Fork";
						node["Question"].Value = "Your question here";
						node["TitleProperty"].Value = "Question";
						node["StoryPageNumber"].Value = "1";
						node.X = controlLocation.X;
						node.Y = controlLocation.Y;
						if(nodeControl.Nodes.Count > 0)
						{
							node.ZOrder = nodeControl.Nodes.Max(x => x.ZOrder) + 1;
						}
						socket = new SocketItem();
						socket.SocketMode = SocketModeEnum.Input;
						socket["Response"].Value = "Response";
						socket.TitleProperty = "Response";
						node.Sockets.Add(socket);
						socket = new SocketItem();
						socket.SocketMode = SocketModeEnum.Output;
						socket["Index"].Value = "A";
						socket["Answer"].Value = "Answer A";
						socket["TitleProperty"].Value = "Answer";
						node.Sockets.Add(socket);
						nodeControl.Nodes.Add(node);
						break;
					case ToolTypeEnum.NodeDelay:
						node = new NodeItem();
						node.NodeType = "Delay";
						node["Question"].Value = "{Delay}";
						node["TitleProperty"].Value = "Question";
						node["StoryPageNumber"].Value = "1";
						node.X = controlLocation.X;
						node.Y = controlLocation.Y;
						if(nodeControl.Nodes.Count > 0)
						{
							node.ZOrder = nodeControl.Nodes.Max(x => x.ZOrder) + 1;
						}
						socket = new SocketItem();
						socket.SocketMode = SocketModeEnum.Input;
						socket["Response"].Value = "Response";
						socket.TitleProperty = "Response";
						node.Sockets.Add(socket);
						socket = new SocketItem();
						socket.SocketMode = SocketModeEnum.Output;
						socket["Index"].Value = "A";
						socket["Answer"].Value = "Output";
						socket["TitleProperty"].Value = "Answer";
						node.Sockets.Add(socket);
						nodeControl.Nodes.Add(node);
						break;
					case ToolTypeEnum.NodeTermination:
						node = new NodeItem();
						node.NodeType = "Termination";
						node["Question"].Value = "Your statement here";
						node["TitleProperty"].Value = "Question";
						node["StoryPageNumber"].Value = "1";
						node.X = controlLocation.X;
						node.Y = controlLocation.Y;
						if(nodeControl.Nodes.Count > 0)
						{
							node.ZOrder = nodeControl.Nodes.Max(x => x.ZOrder) + 1;
						}
						socket = new SocketItem();
						socket.SocketMode = SocketModeEnum.Input;
						socket["Response"].Value = "Response";
						socket.TitleProperty = "Response";
						node.Sockets.Add(socket);
						nodeControl.Nodes.Add(node);
						break;
				}
				ReleaseToolCursor();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* nodeControl_CursorMessage																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A cursor message as been received from the node control.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Message event arguments.
		/// </param>
		private void nodeControl_CursorMessage(object sender, MessageEventArgs e)
		{
			this.StatusBarCursor.Text = e.Text;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* nodeControl_DisplayNodeResource																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The node control is requesting that a resource be displayed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Node event arguments.
		/// </param>
		private void nodeControl_DisplayNodeResource(object sender,
			NodeEventArgs e)
		{
			StringBuilder builder = new StringBuilder();
			FileInfo file = null;
			string filename = "";
			//Process process = null;
			ProcessStartInfo processInfo = null;
			ResourceItem resource = null;
			ResourceLiveItem resourceLive = null;

			if(e != null && e.Node != null && e.Information?.Length > 0)
			{
				switch(e.Information)
				{
					case "MediaAudio":
						resource = GetResource(e.Node, "MediaAudio");
						if(resource != null)
						{
							//	The audio was found.
							resourceLive =
								ResourceLiveItem.FromResourceItem(resource);
							if(resourceLive != null)
							{
								file = new FileInfo(resource.AbsoluteFilename);
								btnHTML.Selected = true;
								htmlGeneric.JavaScriptCommand(
									"$('body').css('overflow', 'scroll');");
								builder.Append("<div style='margin: 16px;'>");
								builder.Append($"<h2>Audio {file.Name}</h2>");
								builder.Append("<audio controls src='");
								builder.Append(GetDataUri(file.Extension,
									(byte[])resourceLive.Data));
								builder.Append("' autoplay='true' />");
								builder.Append("</div>");
								htmlGeneric.SetBodyHTML(builder.ToString());
							}
						}
						break;
					case "MediaImage":
						resource = GetResource(e.Node, "MediaImage");
						if(resource != null)
						{
							//	The image was found.
							resourceLive =
								ResourceLiveItem.FromResourceItem(resource);
							if(resourceLive != null)
							{
								file = new FileInfo(resource.AbsoluteFilename);
								btnHTML.Selected = true;
								htmlGeneric.JavaScriptCommand(
									"$('body').css('overflow', 'scroll');");
								builder.Append("<div style='margin: 16px;'>");
								builder.Append($"<h2>Image {file.Name}</h2>");
								builder.Append("<img src='");
								builder.Append(GetDataUri(file.Extension,
									(byte[])resourceLive.Data));
								builder.Append("' />");
								builder.Append("</div>");
								htmlGeneric.SetBodyHTML(builder.ToString());
							}
						}
						break;
					case "MediaLink":
						resource = GetResource(e.Node, "MediaLink");
						if(resource != null)
						{
							//	The link was found.
							btnHTML.Selected = true;
							htmlGeneric.JavaScriptCommand(
								"$('body').css('overflow', 'scroll');");
							builder.Append("<div style='margin: 16px;'>");
							builder.Append($"<h2>Link</h2>");
							builder.Append("<a href='");
							builder.Append(resource.Uri);
							builder.Append($"'>{resource.Uri}</a>");
							builder.Append("</div>");
							htmlGeneric.SetBodyHTML(builder.ToString());
						}
						break;
					case "MediaVideo":
						resource = GetResource(e.Node, "MediaVideo");
						if(resource != null)
						{
							//	The video was found.
							resourceLive =
								ResourceLiveItem.FromResourceItem(resource);
							if(resourceLive != null)
							{
								//	Big surprise. CefSharp doesn't support video player.
								file = new FileInfo(resource.AbsoluteFilename);
								processInfo = new ProcessStartInfo();
								processInfo.UseShellExecute = true;
								filename = Guid.NewGuid().ToString("D") + file.Extension;
								filename = Path.Combine(Path.GetTempPath(), filename);
								processInfo.FileName = "\"" + filename + "\"";
								File.WriteAllBytes(filename, (byte[])resourceLive.Data);
								Process.Start(processInfo);
								//await Task.Run(() => process.WaitForExit());
							}
						}
						break;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* nodeControl_EditorMessage																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// An editor message has been received from the node control.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Message event arguments.
		/// </param>
		private void nodeControl_EditorMessage(object sender, MessageEventArgs e)
		{
			this.StatusBarEditor.Text = e.Text;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* nodeControl_NodeAdded																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A node has been added on the node control.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Node event arguments.
		/// </param>
		private void nodeControl_NodeAdded(object sender, NodeEventArgs e)
		{
			UndoItem item = new UndoItem();

			item.ActionType = "Add";
			item.ItemName = e.Node.Ticket;
			item.ItemType = "NodeItem";
			PushUndo(item);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* nodeControl_NodeDeleted																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A node has been deleted on the node control.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Node event arguments.
		/// </param>
		private void nodeControl_NodeDeleted(object sender, NodeEventArgs e)
		{
			SocketItem connection = null;
			UndoItem item = new UndoItem();
			NodeItem node = e.Node;
			SocketItem nodeSocket = null;

			//	Remove any connections to the deleted node.
			foreach(SocketItem socket in node.Sockets)
			{
				while(nodeControl.Nodes.Exists(x =>
					x.Sockets.Exists(y =>
						y.Connections.Exists(z =>
							z.Ticket == socket.Ticket))))
				{
					//	There are connections to this socket.
					node = nodeControl.Nodes.First(x =>
						x.Sockets.Exists(y =>
							y.Connections.Exists(z =>
								z.Ticket == socket.Ticket)));
					nodeSocket = node.Sockets.First(x =>
						x.Connections.Exists(y =>
							y.Ticket == socket.Ticket));
					connection = nodeSocket.Connections.First(x =>
						x.Ticket == socket.Ticket);
					nodeSocket.Connections.Remove(connection);
				}
			}

			item.ActionType = "Delete";
			item.ItemName = e.Node.Ticket;
			item.ItemType = "NodeItem";
			item.Properties["Delay"].Value = e.Node.Delay;
			item.Properties["Height"].Value = e.Node.Height;
			item.Properties["NodeColor"].Value = e.Node.NodeColor;
			item.Properties["NodeTextColor"].Value = e.Node.NodeTextColor;
			item.Properties["NodeType"].Value = e.Node.NodeType;
			item.Properties["Selected"].Value = e.Node.Selected;
			item.Properties["Ticket"].Value = e.Node.Ticket;
			item.Properties["TitleHeight"].Value = e.Node.TitleHeight;
			item.Properties["TitleProperty"].Value = e.Node.TitleProperty;
			item.Properties["Width"].Value = e.Node.Width;
			item.Properties["X"].Value = e.Node.X;
			item.Properties["Y"].Value = e.Node.Y;
			item.Properties["ZOrder"].Value = e.Node.ZOrder;
			item.Properties["Properties"].Value = e.Node.Properties;
			item.Properties["Sockets"].Value = e.Node.Sockets;
			PushUndo(item);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* nodeControl_NodeMoved																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A node has been moved on the node control.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Node event arguments.
		/// </param>
		private void nodeControl_NodeMoved(object sender, NodeEventArgs e)
		{
			UndoItem item =
				mNodeMoving.FirstOrDefault(x => x.ItemName == e.Node.Ticket);

			if(item != null)
			{
				//	A moving item was found.
				mNodeMoving.Remove(item);
				item.Properties["X"].Value = e.Node.X;
				item.Properties["Y"].Value = e.Node.Y;
				item.Properties.Add("XAfter", e.Node.X);
				item.Properties.Add("YAfter", e.Node.Y);
			}
			else
			{
				//	The previous state of this node is unknown.
				item = new UndoItem();
				item.ActionType = "Move";
				item.ItemName = e.Node.Ticket;
				item.ItemType = "NodeItem";
				item.Properties.Add("X", e.Node.X);
				item.Properties.Add("Y", e.Node.Y);
				item.Properties.Add("XBefore", e.Node.X);
				item.Properties.Add("YBefore", e.Node.Y);
				item.Properties.Add("XAfter", e.Node.X);
				item.Properties.Add("YAfter", e.Node.Y);
			}
			PushUndo(item);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* nodeControl_NodeMoving																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A node has started moving on the node control.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Node event arguments.
		/// </param>
		/// <remarks>
		/// This method tracks a node move in progress. The entry will be added
		/// to the undo list if the move is completed with a NodeMoved event.
		/// </remarks>
		private void nodeControl_NodeMoving(object sender, NodeEventArgs e)
		{
			UndoItem item = new UndoItem();

			item.ActionType = "Move";
			item.ItemName = e.Node.Ticket;
			item.ItemType = "NodeItem";
			item.Properties.Add("X", e.Node.X);
			item.Properties.Add("Y", e.Node.Y);
			item.Properties.Add("XBefore", e.Node.X);
			item.Properties.Add("YBefore", e.Node.Y);
			mNodeMoving.Add(item);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* nodeControl_NodePropertyChanged																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A node property value has been changed on a node in the node control.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Node property change event arguments.
		/// </param>
		private void nodeControl_NodePropertyChanged(object sender,
			NodePropertyChangeEventArgs e)
		{
			UndoItem item = new UndoItem();

			item.ActionType = "Edit";
			item.ItemName = e.Node.Ticket;
			item.ItemType = "NodeItem";
			item.Properties.Add(e.PropertyName, e.ValueAfter);
			item.Properties.Add($"{e.PropertyName}Before", e.ValueBefore);
			item.Properties.Add($"{e.PropertyName}After", e.ValueAfter);
			PushUndo(item);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* nodeControl_SelectionChanged																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The selection of nodes has changed on the node control.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Node event arguments.
		/// </param>
		private void nodeControl_SelectionChanged(object sender, NodeEventArgs e)
		{
			if(nodeControl.SelectionQueue.Count > 0)
			{
				//	Items are selected.
				mnuEditAlignBottom.Enabled = true;
				mnuEditAlignCenter.Enabled = true;
				mnuEditAlignHorizontal.Enabled = true;
				mnuEditAlignLeft.Enabled = true;
				mnuEditAlignMiddle.Enabled = true;
				mnuEditAlignRight.Enabled = true;
				mnuEditAlignTop.Enabled = true;
				mnuEditAlignVertical.Enabled = true;
				mnuEditNodeAddAudio.Enabled = true;
				mnuEditNodeAddImage.Enabled = true;
				mnuEditNodeAddLink.Enabled = true;
				mnuEditNodeAddVideo.Enabled = true;
				mnuEditNodeColor.Enabled = true;
				mnuEditNodeColorText.Enabled = true;
				mnuEditNodeDuplicate.Enabled = true;
				mnuEditNodeRemoveAudio.Enabled = true;
				mnuEditNodeRemoveImage.Enabled = true;
				mnuEditNodeRemoveLink.Enabled = true;
				mnuEditNodeRemoveVideo.Enabled = true;
			}
			else
			{
				//	No active selections.
				mnuEditAlignBottom.Enabled = false;
				mnuEditAlignCenter.Enabled = false;
				mnuEditAlignHorizontal.Enabled = false;
				mnuEditAlignLeft.Enabled = false;
				mnuEditAlignMiddle.Enabled = false;
				mnuEditAlignRight.Enabled = false;
				mnuEditAlignTop.Enabled = false;
				mnuEditAlignVertical.Enabled = false;
				mnuEditNodeAddAudio.Enabled = false;
				mnuEditNodeAddImage.Enabled = false;
				mnuEditNodeAddLink.Enabled = false;
				mnuEditNodeAddVideo.Enabled = false;
				mnuEditNodeColor.Enabled = false;
				mnuEditNodeColorText.Enabled = false;
				mnuEditNodeDuplicate.Enabled = false;
				mnuEditNodeRemoveAudio.Enabled = false;
				mnuEditNodeRemoveImage.Enabled = false;
				mnuEditNodeRemoveLink.Enabled = false;
				mnuEditNodeRemoveVideo.Enabled = false;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* nodeControl_SocketAdded																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A socket has been added to a node in the node control.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket event arguments.
		/// </param>
		private void nodeControl_SocketAdded(object sender, SocketEventArgs e)
		{
			UndoItem item = new UndoItem();

			item.ActionType = "Add";
			item.ItemName = $"{e.Node.Ticket},{e.Socket.Ticket}";
			item.ItemType = "SocketItem";
			PushUndo(item);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* nodeControl_SocketConnectionAdded																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A socket to socket connection has been added to a node socket in the
		/// node control.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket event arguments.
		/// </param>
		private void nodeControl_SocketConnectionAdded(object sender,
			SocketConnectionEventArgs e)
		{
			UndoItem item = new UndoItem();

			item.ActionType = "Add";
			item.ItemName = $"{e.Node.Ticket},{e.Socket.Ticket}";
			item.ItemType = "SocketConnectionItem";
			item.Properties.Add("Ticket", e.Connection.Ticket);
			PushUndo(item);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* nodeControl_SocketConnectionDeleted																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A socket to socket connection has been deleted from a node socket in
		/// the node control.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket event arguments.
		/// </param>
		private void nodeControl_SocketConnectionDeleted(object sender,
			SocketConnectionEventArgs e)
		{
			UndoItem item = new UndoItem();

			item.ActionType = "Delete";
			item.ItemName = $"{e.Node.Ticket},{e.Socket.Ticket}";
			item.ItemType = "SocketConnectionItem";
			item.Properties.Add("Ticket", e.Connection.Ticket);
			PushUndo(item);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* nodeControl_SocketDeleted																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A socket has been deleted from a node in the node control.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket event arguments.
		/// </param>
		private void nodeControl_SocketDeleted(object sender, SocketEventArgs e)
		{
			UndoItem item = new UndoItem();

			item.ActionType = "Delete";
			item.ItemName = $"{e.Node.Ticket},{e.Socket.Ticket}";
			item.ItemType = "SocketItem";
			item.Properties[""].Value = e.Socket.Height;
			item.Properties[""].Value = e.Socket.SocketMode;
			item.Properties[""].Value = e.Socket.TextBounds;
			item.Properties[""].Value = e.Socket.Ticket;
			item.Properties[""].Value = e.Socket.TitleProperty;
			item.Properties[""].Value = e.Socket.Width;
			item.Properties[""].Value = e.Socket.X;
			item.Properties[""].Value = e.Socket.Y;
			item.Properties["Properties"].Value = e.Socket.Properties;
			item.Properties["Connections"].Value = e.Socket.Connections;
			PushUndo(item);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* nodeControl_SocketPropertyChanged																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A property value has been changed in a node socket on the node control.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket property change event arguments.
		/// </param>
		private void nodeControl_SocketPropertyChanged(object sender,
			SocketPropertyChangeEventArgs e)
		{
			UndoItem item = new UndoItem();

			item.ActionType = "Edit";
			item.ItemName = $"{e.Node.Ticket},{e.Socket.Ticket}";
			item.ItemType = "SocketItem";
			item.Properties.Add(e.PropertyName, e.ValueAfter);
			item.Properties.Add($"{e.PropertyName}Before", e.ValueBefore);
			item.Properties.Add($"{e.PropertyName}After", e.ValueAfter);
			PushUndo(item);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* noDrop_MouseMove																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse is moving across a no-drop area.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void noDrop_MouseMove(object sender, MouseEventArgs e)
		{
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* noDrop_MouseUp																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Mouse button has been released on a no-drop area.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void noDrop_MouseUp(object sender, MouseEventArgs e)
		{
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlSlideTools_Resize																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The slide editor tools panel has been resized.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlSlideTools_Resize(object sender, EventArgs e)
		{
			if(pnlSlideTools.Width > 128 + 12)
			{
				//	There is enough room for two rows.
				toolSlidePicCursor.Location = ImageOffset(0, 0, 4, 64, 64);
				toolSlidePicRectangle.Location = ImageOffset(0, 1, 4, 64, 64);
				toolSlidePicEllipse.Location = ImageOffset(1, 1, 4, 64, 64);
				toolSlidePicLine.Location = ImageOffset(0, 2, 4, 64, 64);
				toolSlidePicPolyline.Location = ImageOffset(1, 2, 4, 64, 64);
				toolSlidePicText.Location = ImageOffset(0, 3, 4, 64, 64);
			}
			else
			{
				//	Only enough room for one row.
				toolSlidePicCursor.Location = ImageOffset(0, 0, 4, 64, 64);
				toolSlidePicRectangle.Location = ImageOffset(0, 1, 4, 64, 64);
				toolSlidePicEllipse.Location = ImageOffset(0, 2, 4, 64, 64);
				toolSlidePicLine.Location = ImageOffset(0, 3, 4, 64, 64);
				toolSlidePicPolyline.Location = ImageOffset(0, 4, 4, 64, 64);
				toolSlidePicText.Location = ImageOffset(0, 5, 4, 64, 64);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTools_Click																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The user has clicked on the empty space of a tools panel.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlTools_Click(object sender, EventArgs e)
		{
			if(mToolHandleActive)
			{
				//	The tool won't be used.
				ReleaseToolCursor();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PushUndo																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Push the undo item into the current undo stack, adjusting the active
		/// pack if necessary.
		/// </summary>
		/// <param name="item">
		/// Undo item to push.
		/// </param>
		private void PushUndo(UndoItem item)
		{
			bool bCreated = false;

			if(mUndo == null && item != null)
			{
				mUndo = new UndoStack();
				bCreated = true;
			}
			if(item != null)
			{
				if(mUndo.GetAge() > 0.5f)
				{
					if(mUndo.Count == 0)
					{
						mUndo.DateCreated = DateTime.Now;
					}
					else
					{
						mUndo = new UndoStack();
						bCreated = true;
					}
				}
				mUndo.Push(item);
				if(bCreated)
				{
					//	Push a newly created stack to the pack after the item was set
					//	to get accurate description update on response event.
					mUndoPack.Push(mUndo);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ScrollIntoView																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Scroll the objects into view.
		/// </summary>
		/// <param name="node">
		/// Optional reference to a node to be scrolled. If null, the leftmost
		/// node is selected.
		/// </param>
		private void ScrollIntoView(NodeItem node = null)
		{
			float minX = nodeControl.Nodes.Min(x => x.X);
			float minY = 0f;
			NodeItem nodeItem = (node != null ? node :
				nodeControl.Nodes.FirstOrDefault(x => x.X == minX));

			if(nodeItem != null)
			{
				minX = nodeItem.X * nodeControl.DrawingScale.Width;
				minY = nodeItem.Y * nodeControl.DrawingScale.Height;
				//	Convert from pixel to fraction of display.
				minX = minX / (float)nodeControl.CanvasWidth;
				minY = minY / (float)nodeControl.CanvasHeight;
				nodeControl.QueueViewCenter(new PointF(minX, minY));
				//nodeControl.Invalidate();
				//nodeControl.Refresh();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SendAnimationToSVG																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Send an animation to the SVG viewer (Slide Editor).
		/// </summary>
		/// <param name="animation">
		/// Pre-loaded animation timeline object.
		/// </param>
		/// <param name="saveImages">
		/// Value indicating whether to save each image.
		/// </param>
		/// <param name="imageFilePattern">
		/// Path and filename pattern to use when saving images.
		/// {frameIndex:D4} indicates the current frame index with a four digit
		/// decimal number. For example MyImage{frameIndex:D4}.png when frameIndex
		/// is 27 will result in the filename 'MyImage0027.png'. The insert
		/// {frameIndex:DX} will resolve the X to the maximum number of digits
		/// for the current run.
		/// </param>
		/// <param name="startFrame">
		/// User-specified starting frame.
		/// </param>
		/// <param name="endFrame">
		/// User-specified end frame.
		/// </param>
		private async void SendAnimationToSVG(SvgAnimation animation,
			bool saveImages = false, string imageFilePattern = "",
			int startFrame = 0, int endFrame = -1, bool writeLog = false)
		{
			Dictionary<string, string> attributes = null;
			string attributeValue = "";
			bool bContinue = true;
			SvgAnimationCharacterTrackCollection charTrack = null;
			string content = "";
			double dValue = 0.0;
			FileInfo file = null;
			string filename = "";
			string format = "";
			int frameEnd = 0;
			int frameIndex = 0;
			int frameMax = 0;
			List<string> log = new List<string>();
			MatchCollection matches = null;
			string name = "";
			Dictionary<string, object> properties = null;
			SvgAnimationFrameCollection renderedFrames = null;
			NameValueItem token = null;
			SvgUserUnit userUnit = new SvgUserUnit();
			List<double> viewBox = new List<double>();

			mEscPressed = false;

			if(animation.SourceArtFilename?.Length > 0)
			{
				//	Source art was specified.
				file = new FileInfo(ResolveEnvironment(
					animation.SourceArtFilename));
				if(file.Exists)
				{
					//	A source file was specified.
					statMessage.Text = "Rendering source file...";
					statProg.Maximum = 100;
					statProg.Value = 0;
					content = File.ReadAllText(file.FullName);
					//	To eliminate need for resolving file locations,
					//	replace all images with onboard base64.
					statProg.Value = 50;
					token = new NameValueItem();
					await SvgReplaceFileRefWithB64(
						file.Directory, content, token, statProg);
					mSVG.LoadFromString(token.Value);
					statProg.Value = 100;
					GC.Collect();
					statProg.Value = 0;
				}
			}
			//	It is possible that the timeline will use a preloaded SVG.
			skControl.Invalidate();
			mSVG.NeedsInvalidation = false;

			//	ViewBox.
			statMessage.Text = "Configuring User Unit...";
			userUnit.SetUserUnitX(mSVG.ViewBox.Width, mSVG.Width);
			userUnit.SetUserUnitY(mSVG.ViewBox.Height, mSVG.Height);

			if(animation.Frames.Count > 0 && bContinue)
			{
				frameIndex = startFrame;
				frameEnd = (endFrame > -1 ? endFrame :
					animation.Frames.Max(x => x.FrameIndex));

				statMessage.Text = "Preparing animation...";
				await ThreadSleep(100);

				renderedFrames =
					SvgAnimationFrameCollection.RenderScenes(
						animation.Frames, animation.Scenes);
				if(renderedFrames.Count > 0)
				{
					//	Prime the log.
					log.Add(
						"Frame\t" +
						"Character\t" +
						"Property\t" +
						"Value");
					//	Start the per-character tracker.
					frameMax = renderedFrames.Max(mx => mx.FrameIndex);
					charTrack = new SvgAnimationCharacterTrackCollection(
						renderedFrames);
					//	Initialize monitored properties.
					foreach(SvgAnimationCharacterTrackItem character in charTrack)
					{
						if(character.HasRotation)
						{
							attributes =
								mSVG.GetElementAttributes(character.CharacterName);
							foreach(string monitoredProperty in
								character.MonitoredProperties)
							{
								if(attributes.ContainsKey(monitoredProperty))
								{
									//	A monitored property was found.
									attributeValue = attributes[monitoredProperty];
									dValue = 0.0;
									if(IsDouble(attributeValue))
									{
										double.TryParse(attributeValue, out dValue);
									}
									//	Monitoring is pixel-based.
									switch(monitoredProperty)
									{
										case "x":
										case "width":
											dValue = userUnit.ToPixelX(dValue);
											break;
										case "y":
										case "height":
											dValue = userUnit.ToPixelY(dValue);
											break;
									}
									character.SetMonitoredPropertyValue(
										monitoredProperty, dValue);
								}
							}
						}
					}
					for(frameIndex = 0;
						frameIndex <= frameEnd; frameIndex++)
					{
						//	Even if starting from a different frame, 0 is used to
						//	initialize everything.
						foreach(SvgAnimationCharacterTrackItem character in charTrack)
						{
							//if(frameIndex == 119 &&
							//	character.CharacterName == "ThoughtBubble1")
							//{
							//	Debug.WriteLine("Break here...");
							//}
							//if(frameIndex == 198 &&
							//	character.CharacterName == "TaskList1")
							//{
							//	Debug.WriteLine("Break here...");
							//}
							//if(frameIndex == 415 &&
							//	character.CharacterName == "IanSit3")
							//{
							//	Debug.WriteLine("Break here...");
							//}
							//if(frameIndex == 1057 &&
							//	character.CharacterName == "IanPoint1")
							//{
							//	Debug.WriteLine("Break here...");
							//}
							character.UpdateBand(frameIndex, 0, frameMax);
							if(frameIndex == startFrame && frameIndex > 0)
							{
								//	At the starting frame, make sure to get all current
								//	properties for all characters.
								properties = character.GetPropertiesImplied(frameIndex);
							}
							else
							{
								//	This character is ready to be positioned.
								properties = character.GetPropertiesAtFrame(frameIndex);
							}
							foreach(KeyValuePair<string, object> property in properties)
							{
								switch(property.Key)
								{
									case "display":
										//	Css.
										//	String.
										mSVG.SetElementCss(character.CharacterName,
											property.Key, property.Value.ToString());
										break;
									case "height":
										//	Attr.
										//	Number.
										mSVG.SetElementAttr(character.CharacterName,
											property.Key,
											userUnit.ToUserY((double)property.Value).
											ToString());
										break;
									case "opacity":
										//	Css.
										//	Number.
										mSVG.SetElementCss(character.CharacterName,
											property.Key, property.Value.ToString());
										break;
									case "rotation":
										//	Rotation macro. Rotates at center.
										mSVG.SetElementRotation(character.CharacterName,
											property.Value.ToString(),
											(float)userUnit.ToUserX(character.LastX),
											(float)userUnit.ToUserY(character.LastY),
											(float)userUnit.ToUserX(character.LastWidth),
											(float)userUnit.ToUserY(character.LastHeight));
										break;
									//	Number.
									case "width":
										//	Attr.
										//	Number.
										mSVG.SetElementAttr(character.CharacterName,
											property.Key,
											userUnit.ToUserX((double)property.Value).
											ToString());
										break;
									case "x":
										//	Attr.
										//	Number.
										mSVG.SetElementAttr(character.CharacterName,
											property.Key,
											userUnit.ToUserX((double)property.Value).
											ToString());
										break;
									case "y":
										//	Attr.
										//	Number.
										mSVG.SetElementAttr(character.CharacterName,
											property.Key,
											userUnit.ToUserY((double)property.Value).
											ToString());
										break;
								}
								if(character.HasRotation && character.LastRotation != 0.0)
								{
									if(property.Key == "x" || property.Key == "y" ||
										property.Key == "width" || property.Key == "height")
									{
										//	If the character has rotation, that setting must
										//	also be corrected with any change to location or
										//	scale.
										mSVG.SetElementRotation(character.CharacterName,
											character.LastRotation.ToString(),
											(float)userUnit.ToUserX(character.LastX),
											(float)userUnit.ToUserY(character.LastY),
											(float)userUnit.ToUserX(character.LastWidth),
											(float)userUnit.ToUserY(character.LastHeight));
									}
								}
								//	Log the action.
								log.Add(
									$"{frameIndex}\t" +
									$"{character.CharacterName}\t" +
									$"{property.Key}\t" +
									$"{property.Value}");
							}
						}
						//	Frame is ready to display for all characters.
						statMessage.Text = $"Frame: {frameIndex}...";
						statProg.Value =
							(int)(((double)frameIndex / (double)frameMax) * 100.0);
						if(mSVG.NeedsInvalidation)
						{
							//mSVG.Document.Save("C:/Temp/ScaffoldModified.svg");
							mSVG.Refresh();
							skControl.Invalidate();
							skControl.Refresh();
							mSVG.NeedsInvalidation = false;
						}
						if(saveImages)
						{
							filename = imageFilePattern;
							matches = Regex.Matches(imageFilePattern,
								ResourceMain.rxFormatString);
							foreach(Match match in matches)
							{
								name = GetValue(match, "name");
								format = GetValue(match, "format");
								if(name.Length > 0)
								{
									if(format.Length > 0)
									{
										//	Name and format.
										if(format.ToLower() == "dx")
										{
											format = "D" + frameMax.ToString().Length.ToString();
										}
										//	TODO: Create a collection of available variables.
										//	Resolve all variables from a resolve values method.
										switch(name.ToLower())
										{
											case "frameindex":
												filename = filename.Replace(match.Value,
													string.Format("{0:" + format + "}", frameIndex));
												break;
										}
									}
									else
									{
										//	Name only.
										switch(name.ToLower())
										{
											case "frameindex":
												filename = filename.Replace(match.Value,
													frameIndex.ToString());
												break;
										}
									}
								}
							}
							mSVG.SavePicture(filename);
						}
						await ThreadSleep(100);
						if(mEscPressed)
						{
							bContinue = false;
							break;
						}
						if(frameIndex == 0 && startFrame > 1)
						{
							frameIndex = startFrame - 1;
						}
					}
				}
			}

			if(bContinue)
			{
				if(writeLog)
				{
					File.WriteAllText(
						"C:/Temp/ScaffoldLog.txt",
						String.Join("\r\n", log.ToArray()));
				}
				statMessage.Text = "Animation Loaded...";
			}
			else
			{
				statMessage.Text = "Animation Cancelled...";
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* skControl_PaintSurface																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The SkiaSharp control surface is being painted.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// SkiaSharp paint surface event arguments.
		/// </param>
		private void skControl_PaintSurface(object sender,
			SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
		{
			SKCanvas canvas = null;
			float ch = (float)skControl.Height;
			float cw = (float)skControl.Width;
			SKPaint paint = new SKPaint();
			paint.Color = new SKColor(255, 0, 0);
			paint.StrokeWidth = 3;
			paint.IsAntialias = true;

			canvas = e.Surface.Canvas;
			canvas.Clear();


			if(mSVG != null && mSVG.Picture != null)
			{
				if(cw != 0f && ch != 0f &&
					mSVG.CanvasSize.Width != 0f && mSVG.CanvasSize.Height != 0f)
				{
					if(cw < mSVG.CanvasSize.Width ||
						ch < mSVG.CanvasSize.Height)
					{
						//	At least one dimension of the window is smaller than the
						//	image. Scale down.
						if(cw < mSVG.CanvasSize.Width && ch < mSVG.CanvasSize.Height)
						{
							//	Find the most distant scale.
							if(Math.Abs(mSVG.CanvasSize.Width - cw) >
								Math.Abs(mSVG.CanvasSize.Height - ch))
							{
								//	Horizontal distance is greatest.
								canvas.Scale(cw / mSVG.CanvasSize.Width);
							}
							else
							{
								//	Vertical distance is greatest.
								canvas.Scale(ch / mSVG.CanvasSize.Height);
							}
						}
						else if(cw < mSVG.CanvasSize.Width)
						{
							//	Scale down by width.
							canvas.Scale(cw / mSVG.CanvasSize.Width);
						}
						else
						{
							//	Scale down by height.
							canvas.Scale(ch / mSVG.CanvasSize.Height);
						}
					}
					else if(cw > mSVG.CanvasSize.Width && ch > mSVG.CanvasSize.Width)
					{
						//	The entire window is larger than the image.
						//	Scale up.
						if(Math.Abs(mSVG.CanvasSize.Width - cw) <
							Math.Abs(mSVG.CanvasSize.Height - ch))
						{
							//	Horizontal distance is least.
							canvas.Scale(mSVG.CanvasSize.Width / cw);
						}
						else
						{
							//	Vertical distance is least.
							canvas.Scale(mSVG.CanvasSize.Height / ch);
						}
					}
					canvas.DrawPicture(mSVG.Picture);
					GC.Collect();
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* timerDrag_Tick																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The drag monitor timer has elapsed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void timerDrag_Tick(object sender, EventArgs e)
		{
			if(mToolHandleActive && Control.MouseButtons == MouseButtons.None)
			{
				//	Deactivate drag.
				ReleaseToolCursor();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* timerAutoSave_Tick																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The auto-save timer has elapsed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void timerAutoSave_Tick(object sender, EventArgs e)
		{
			//	TODO: Re-enable file autosave.
			//string content = "";

			//Debug.WriteLine("Autosave interval...");
			//if(mSVGFile != null && mSVGFile.Exists &&
			//	mSVG != null && mSVG.Document != null)
			//{
			//	//	The SVG file exists and can be saved.
			//	mSVG.Document.Save(mSVGFile.FullName);
			//	Debug.WriteLine("SVG Autosaved...");
			//}
			//if(mNodeFile != null && mNodeFile.Exists)
			//{
			//	content = NodeDataCollection.SerializeData(nodeControl.NodeFile);
			//	File.WriteAllText(mNodeFile.FullName, content);
			//	Debug.WriteLine("Nodes Autosaved...");
			//}
			//if(!timerAutoSave.Enabled)
			//{
			//	timerAutoSave.Start();
			//}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* toolDecisionPicDelay_Click																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fork tool has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void toolDecisionPicDelay_Click(object sender, EventArgs e)
		{
			Verbose("Delay tool click...");
			if(!mToolHandleActive)
			{
				this.Cursor =
					SetToolCursor(ToolTypeEnum.NodeDelay,
					new Point(0, 0));
			}
			else
			{
				ReleaseToolCursor();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* toolDecisionPicFork_Click																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fork tool has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void toolDecisionPicFork_Click(object sender, EventArgs e)
		{
			Verbose("Fork tool click...");
			if(!mToolHandleActive)
			{
				this.Cursor =
					SetToolCursor(ToolTypeEnum.NodeFork, new Point(0, 0));
			}
			else
			{
				ReleaseToolCursor();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* toolDecisionPicStart_Click																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Click has been received on the Start node tool.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void toolDecisionPicStart_Click(object sender, EventArgs e)
		{
			Verbose("Start tool click...");
			if(!mToolHandleActive)
			{
				this.Cursor =
					SetToolCursor(ToolTypeEnum.NodeStart,
						new Point(0, 0));
			}
			else
			{
				ReleaseToolCursor();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* toolDecisionPicTermination_Click																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Click has been received on the Termination node tool.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void toolDecisionPicTermination_Click(object sender, EventArgs e)
		{
			Verbose("Termination tool click...");
			if(!mToolHandleActive)
			{
				this.Cursor =
					SetToolCursor(
						ToolTypeEnum.NodeTermination,
						new Point(0, 0));
			}
			else
			{
				ReleaseToolCursor();
			}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	OnKeyDown																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the KeyDown event when a key has gone into the depressed
		/// position.
		/// </summary>
		/// <param name="e">
		/// Key event arguments.
		/// </param>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if(e.KeyCode == Keys.Escape)
			{
				mEscPressed = true;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnLoad																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The form has been loaded and is ready to display for the first time.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			panelWindowControl.ActivateAssociations();
			mSVG = new SKSvg();
			//mSVG.Load("C:/Users/Daniel/Documents/GitHub/DOHApp/Experiments/" +
			//	"WebBasedApp/SVGInteractive/Drawings/PageHome.svg");
			//mSVG.Load("C:/Users/Daniel/Documents/AscendantShared/Art/Drawings/" +
			//	"Illustrator/OnlineEventProduction/EventProduction-Animation.svg");
			//List<double> samples = SvgAnimationUtil.EaseBouncePattern(3, 30);
			//Debug.WriteLine(
			//	RelativeFilename(
			//		new FileInfo("C:/Temp/Document.doc"),
			//		new FileInfo("C:/Temp/Image.png")));
			//Debug.WriteLine(
			//	RelativeFilename(
			//		new FileInfo("C:/Temp/Document.doc"),
			//		new FileInfo("C:/Temp/Images/Image.png")));
			//Debug.WriteLine(
			//	RelativeFilename(
			//		new FileInfo("C:/Temp/Docs/InProgress/Document.doc"),
			//		new FileInfo("C:/Temp/Image.png")));
			//Debug.WriteLine(
			//	RelativeFilename(
			//		new FileInfo("C:/Temp/Docs/InProgress/Document.doc"),
			//		new FileInfo("C:/Temp/Images/Masters/Image.png")));
			if(NetworkLocalMode)
			{
				statMessage.Text = "Running in local server mode...";
			}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		public static bool NetworkLocalMode = false;

		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new Instance of the frmMain Item.
		/// </summary>
		public frmMain()
		{
			//List<ToolStripMenuItem> menuItems = new List<ToolStripMenuItem>();

			InitializeComponent();

			this.KeyPreview = true;

			this.menuMain.Renderer =
				new DarkThemeMenuRenderer(new DarkThemeMenuColorTable());

			//	TODO: Implement auto-size in panel window control.
			panelWindowControl.PanelTop =
				btnDecisionTreeEditor.Top + btnDecisionTreeEditor.Height + 4 -
				menuMain.Height;

			skControl.PaintSurface += skControl_PaintSurface;

			if(!this.DesignMode)
			{
				htmlGeneric = new HTMLControl();
				htmlGeneric.Name = "htmlGeneric";
				tpgHTML.Controls.Add(htmlGeneric);
				htmlGeneric.Dock = DockStyle.Fill;
				htmlGeneric.SetBodyHTML("<p>&nbsp;</p>");
			}
			mUndoPack = new UndoPack();
			mUndoPack.StackPopped += mUndo_StackPushPop;
			mUndoPack.StackPushed += mUndo_StackPushPop;
			nodeControl.NodeAdded += nodeControl_NodeAdded;
			nodeControl.NodeDeleted += nodeControl_NodeDeleted;
			nodeControl.NodeMoved += nodeControl_NodeMoved;
			nodeControl.NodeMoving += nodeControl_NodeMoving;
			nodeControl.NodePropertyChanged += nodeControl_NodePropertyChanged;
			nodeControl.SocketAdded += nodeControl_SocketAdded;
			nodeControl.SocketConnectionAdded += nodeControl_SocketConnectionAdded;
			nodeControl.SocketConnectionDeleted +=
				nodeControl_SocketConnectionDeleted;
			nodeControl.SocketDeleted += nodeControl_SocketDeleted;
			nodeControl.SocketPropertyChanged += nodeControl_SocketPropertyChanged;
			nodeControl.DisplayNodeResource += nodeControl_DisplayNodeResource;
			nodeControl.SelectionChanged += nodeControl_SelectionChanged;

			nodeControl_SelectionChanged(null, null);

			//	Menu items.
			mnuEditAlignBottom.Click += mnuEditAlignBottom_Click;
			mnuEditAlignCenter.Click += mnuEditAlignCenter_Click;
			mnuEditAlignHorizontal.Click += mnuEditAlignHorizontal_Click;
			mnuEditAlignLeft.Click += mnuEditAlignLeft_Click;
			mnuEditAlignMiddle.Click += mnuEditAlignMiddle_Click;
			mnuEditAlignRight.Click += mnuEditAlignRight_Click;
			mnuEditAlignTop.Click += mnuEditAlignTop_Click;
			mnuEditAlignVertical.Click += mnuEditAlignVertical_Click;
			mnuEditFind.Click += mnuEditFind_Click;
			mnuEditSelectAll.Click += mnuEditSelectAll_Click;
			mnuEditSelectNone.Click += mnuEditSelectNone_Click;
			mnuEditNodeAddAudio.Click += mnuEditNodeAddAudio_Click;
			mnuEditNodeAddImage.Click += mnuEditNodeAddImage_Click;
			mnuEditNodeAddLink.Click += mnuEditNodeAddLink_Click;
			mnuEditNodeAddResources.Click += mnuEditNodeAddResources_Click;
			mnuEditNodeAddVideo.Click += mnuEditNodeAddVideo_Click;
			mnuEditNodeColor.Click += mnuEditNodeColor_Click;
			mnuEditNodeColorText.Click += mnuEditNodeColorText_Click;
			mnuEditNodeDuplicate.Click += mnuEditNodeDuplicate_Click;
			mnuEditNodeRemoveAudio.Click += mnuEditNodeRemoveAudio_Click;
			mnuEditNodeRemoveImage.Click += mnuEditNodeRemoveImage_Click;
			mnuEditNodeRemoveLink.Click += mnuEditNodeRemoveLink_Click;
			mnuEditNodeRemoveVideo.Click += mnuEditNodeRemoveVideo_Click;
			mnuEditUndo.Click += mnuEditUndo_Click;

			mnuFileConvertPPToHTML.Click += mnuFileConvertPPToHTML_Click;
			mnuFileConvertPPToTinyLMS.Click += mnuFileConvertPPToTinyLMS_Click;
			mnuFileDocumentProperties.Click += mnuFileDocumentProperties_Click;
			mnuFileExit.Click += mnuFileExit_Click;
			mnuFileExportDecisionTreeToPP.Click +=
				mnuFileExportDecisionTreeToPP_Click;
			mnuFileNew.Click += mnuFileNew_Click;
			mnuFileOpen.Click += mnuFileOpen_Click;
			mnuFilePublishSlackChatConversation.Click +=
				mnuFilePublishSlackChatConversation_Click;
			mnuFileSave.Click += mnuFileSave_Click;
			mnuFileSaveAs.Click += mnuFileSaveAs_Click;
			mnuFileUnpublishSlackChatConversation.Click +=
				mnuFileUnpublishSlackChatConversation_Click;

			mnuToolsAnimationFrameFlipbook.Click +=
				mnuToolsAnimationFrameFlipbook_Click;
			mnuToolsAnimationFrameNToHTML.Click +=
				mnuToolsAnimationFrameNToHTML_Click;
			mnuToolsAnimationFrameNToSVG.Click += mnuToolsAnimationFrameNToSVG_Click;
			mnuToolsAnimationSaveFrames.Click += mnuToolsAnimationSaveFrames_Click;
			mnuToolsAnimationTimelineFileReport.Click +=
				mnuToolsAnimationTimelineFileReport_Click;
			mnuToolsBase64Raw.Click += mnuToolsBase64Raw_Click;
			mnuToolsBase64SRC.Click += mnuToolsBase64SRC_Click;
			mnuToolsBase64UClipboard.Click += mnuToolsBase64UClipboard_Click;
			mnuToolsBase64UFile.Click += mnuToolsBase64UFile_Click;
			mnuToolsBase64URL.Click += mnuToolsBase64URL_Click;
			mnuToolsChatbotEmulateBeginning.Click +=
				mnuToolsChatbotEmulateBeginning_Click;
			mnuToolsChatbotEmulateSelected.Click +=
				mnuToolsChatbotEmulateSelected_Click;
			mnuToolsClipboardClear.Click += mnuToolsClipboardClear_Click;
			mnuToolsClipboardLoad.Click += mnuToolsClipboardLoad_Click;
			mnuToolsClipboardSave.Click += mnuToolsClipboardSave_Click;
			mnuToolsColorPalette.Click += mnuToolsColorPalette_Click;
			mnuToolsControlNodeControl.Click += mnuToolsControlNodeControl_Click;
			mnuToolsControlNodeMeasurement.Click +=
				mnuToolsControlNodeMeasurement_Click;
			mnuToolsControlUndo.Click += mnuToolsControlUndo_Click;
			mnuToolsPPAlignment.Click += mnuToolsPPAlignment_Click;
			mnuToolsPPPlaceholderToTextboxes.Click +=
				mnuToolsPPPlaceholderToTextboxes_Click;
			mnuToolsPPQuickAnimation.Click += mnuToolsPPQuickAnimation_Click;
			mnuToolsPPRemoveBullet.Click += mnuToolsPPRemoveBullet_Click;
			mnuToolsResourceGallery.Click += mnuToolsResourceGallery_Click;

			mnuViewScrollLayout.Click += mnuViewScrollLayout_Click;
			mnuViewScrollNode.Click += mnuViewScrollNode_Click;
			mnuViewZoom100.Click += mnuViewZoom100_Click;
			mnuViewZoomIn.Click += mnuViewZoomIn_Click;
			mnuViewZoomOut.Click += mnuViewZoomOut_Click;

			mnuWindowDecision.Click += mnuWindowDecision_Click;
			mnuWindowHTMLViewer.Click += mnuWindowHTMLViewer_Click;
			mnuWindowSlide.Click += mnuWindowSlide_Click;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ActiveToolType																												*
		//*-----------------------------------------------------------------------*
		private ToolTypeEnum mActiveToolType = ToolTypeEnum.None;
		/// <summary>
		/// Get/Set the currently active tool type.
		/// </summary>
		public ToolTypeEnum ActiveToolType
		{
			get { return mActiveToolType; }
			set { mActiveToolType = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MainMenuHeight																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the height of the main menu.
		/// </summary>
		public int MainMenuHeight
		{
			get { return menuMain.Height; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ReleaseToolCursor																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Release the custom cursor to the Windows memory management service.
		/// </summary>
		public void ReleaseToolCursor()
		{
			if(mToolHandleActive)
			{
				DestroyIcon(mToolIconHandle);
				mToolIcon.fIcon = false;
				mToolIcon.hbmColor = (IntPtr)0;
				mToolIcon.hbmMask = (IntPtr)0;
				mToolHandleActive = false;
				this.Cursor = Cursors.Default;
				Verbose("ToolHandle deactivated...");
				switch(mActiveToolType)
				{
					case ToolTypeEnum.NodeStart:
						toolDecisionPicStart.Image = ResourceMain.NodeStart;
						break;
					case ToolTypeEnum.NodeFork:
						toolDecisionPicFork.Image = ResourceMain.NodeFork;
						break;
					case ToolTypeEnum.NodeDelay:
						toolDecisionPicDelay.Image = ResourceMain.NodeDelay;
						break;
					case ToolTypeEnum.NodeTermination:
						toolDecisionPicTermination.Image = ResourceMain.NodeTermination;
						break;
				}
				mActiveToolType = ToolTypeEnum.None;
				Verbose("Tool drag ended...", 2);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SetToolCursor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create the custom cursor for the specified tool drag operation.
		/// </summary>
		/// <param name="toolType">
		/// The identification of the tool type to illustrate.
		/// </param>
		/// <param name="hotspot">
		/// Current cursor hotspot.
		/// </param>
		public Cursor SetToolCursor(ToolTypeEnum toolType,
			Point hotspot)
		{
			Bitmap bitmap = null;
			Cursor cursor = Cursor.Current;

			mActiveToolType = toolType;
			switch(toolType)
			{
				case ToolTypeEnum.NodeStart:
					bitmap = ResourceMain.NodeStart;
					toolDecisionPicStart.Image = ResourceMain.NodeStartHighlight;
					break;
				case ToolTypeEnum.NodeFork:
					bitmap = ResourceMain.NodeFork;
					toolDecisionPicFork.Image = ResourceMain.NodeForkHighlight;
					break;
				case ToolTypeEnum.NodeDelay:
					bitmap = ResourceMain.NodeDelay;
					toolDecisionPicDelay.Image = ResourceMain.NodeDelayHighlight;
					break;
				case ToolTypeEnum.NodeTermination:
					bitmap = ResourceMain.NodeTermination;
					toolDecisionPicTermination.Image =
						ResourceMain.NodeTerminationHighlight;
					break;
			}
			if(bitmap != null)
			{
				mToolIcon = new IconInfo();
				mToolIconHandle = bitmap.GetHicon();
				mToolHandleActive = true;
				GetIconInfo(mToolIconHandle, ref mToolIcon);
				mToolIcon.xHotspot = hotspot.X + (bitmap.Width / 2);
				mToolIcon.yHotspot = hotspot.Y + (bitmap.Height / 2);
				mToolIcon.fIcon = false;
				cursor = new Cursor(CreateIconIndirect(ref mToolIcon));
			}

			return cursor;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	StatusBarCursor																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get a reference to the cursor message status bar.
		/// </summary>
		public ToolStripStatusLabel StatusBarCursor
		{
			get { return statCursor; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	StatusBarEditor																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get a reference to the editor message status bar.
		/// </summary>
		public ToolStripStatusLabel StatusBarEditor
		{
			get { return statEditor; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	StatusBarMessage																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get a reference to the general message status bar.
		/// </summary>
		public ToolStripStatusLabel StatusBarMessage
		{
			get { return statMessage; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ToolHandleActive																											*
		//*-----------------------------------------------------------------------*
		private bool mToolHandleActive = false;
		/// <summary>
		/// Get/Set a value indicating whether the tool handler is active.
		/// </summary>
		public bool ToolHandleActive
		{
			get { return mToolHandleActive; }
			set { mToolHandleActive = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
