//	frmResourceGallery.cs
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
using System.Threading.Tasks;
using System.Windows.Forms;

using static Scaffold.ScaffoldUtil;
using static Scaffold.ScaffoldNodesUtil;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	frmResourceGallery																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Resource gallery form.
	/// </summary>
	public partial class frmResourceGallery : Form
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		private Color mColorDisabledBack = Color.FromArgb(255, 51, 51, 51);
		private Color mColorDisabledFore = Color.FromArgb(255, 200, 200, 200);
		private Color mColorEnabledBack = Color.FromArgb(255, 250, 250, 250);
		private Color mColorEnabledFore = Color.FromArgb(255, 0, 0, 120);

		/// <summary>
		/// Index of the active selected media type.
		/// </summary>
		private int mMediaActive = 0;
		/// <summary>
		/// Labeled media types.
		/// </summary>
		private string[] mMediaType = new string[]
		{
			"Audio",
			"Image",
			"Link",
			"Video"
		};

		private ResourceCollection mResources = null;

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
			if(!btnCancel.Focused)
			{
				btnCancel.Focus();
			}
			this.DialogResult = DialogResult.Cancel;
			this.Hide();
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
			if(!btnOK.Focused)
			{
				btnOK.Focus();
			}
			this.DialogResult = DialogResult.OK;
			this.Hide();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* buttonAudio_Click																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Audio media type button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void buttonAudio_Click(object sender, EventArgs e)
		{
			ListViewItem item = null;
			List<ResourceItem> resources = null;

			if(mMediaActive != 0)
			{
				pnlButtonImage.BackColor = mColorDisabledBack;
				lblButtonImage.ForeColor = mColorDisabledFore;
				pnlButtonLink.BackColor = mColorDisabledBack;
				lblButtonLink.ForeColor = mColorDisabledFore;
				pnlButtonVideo.BackColor = mColorDisabledBack;
				lblButtonVideo.ForeColor = mColorDisabledFore;
				pnlButtonAudio.BackColor = mColorEnabledBack;
				lblButtonAudio.ForeColor = mColorEnabledFore;
				lvResourceGallery.Items.Clear();
				lvResourceGallery.LargeImageList = imageListAudio;

				if(mResources != null)
				{
					resources = mResources.FindAll(x => x.ResourceType == "MediaAudio");
					foreach(ResourceItem resource in resources)
					{
						item = new ListViewItem(ResourceItem.Filename(resource), 0);
						item.Tag = resource.Ticket;
						lvResourceGallery.Items.Add(item);
					}
				}
				mMediaActive = 0;
				this.Text = $"Resources - {mMediaType[mMediaActive]}";
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* buttonImage_Click																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Image media type button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void buttonImage_Click(object sender, EventArgs e)
		{
			int index = 0;
			ListViewItem item = null;
			List<ResourceItem> resources = null;

			if(mMediaActive != 1)
			{
				pnlButtonAudio.BackColor = mColorDisabledBack;
				lblButtonAudio.ForeColor = mColorDisabledFore;
				pnlButtonLink.BackColor = mColorDisabledBack;
				lblButtonLink.ForeColor = mColorDisabledFore;
				pnlButtonVideo.BackColor = mColorDisabledBack;
				lblButtonVideo.ForeColor = mColorDisabledFore;
				pnlButtonImage.BackColor = mColorEnabledBack;
				lblButtonImage.ForeColor = mColorEnabledFore;
				lvResourceGallery.Items.Clear();
				lvResourceGallery.LargeImageList = imageListImage;

				if(mResources != null)
				{
					resources = mResources.FindAll(x => x.ResourceType == "MediaImage");
					foreach(ResourceItem resource in resources)
					{
						item = new ListViewItem(ResourceItem.Filename(resource), index ++);
						item.Tag = resource.Ticket;
						lvResourceGallery.Items.Add(item);
					}
				}
				mMediaActive = 1;
				this.Text = $"Resources - {mMediaType[mMediaActive]}";
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* buttonLink_Click																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Link media type button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void buttonLink_Click(object sender, EventArgs e)
		{
			ListViewItem item = null;
			List<ResourceItem> resources = null;

			if(mMediaActive != 2)
			{
				pnlButtonAudio.BackColor = mColorDisabledBack;
				lblButtonAudio.ForeColor = mColorDisabledFore;
				pnlButtonImage.BackColor = mColorDisabledBack;
				lblButtonImage.ForeColor = mColorDisabledFore;
				pnlButtonVideo.BackColor = mColorDisabledBack;
				lblButtonVideo.ForeColor = mColorDisabledFore;
				pnlButtonLink.BackColor = mColorEnabledBack;
				lblButtonLink.ForeColor = mColorEnabledFore;
				lvResourceGallery.Items.Clear();
				lvResourceGallery.LargeImageList = imageListLink;

				if(mResources != null)
				{
					resources = mResources.FindAll(x => x.ResourceType == "MediaLink");
					foreach(ResourceItem resource in resources)
					{
						item = new ListViewItem(resource.Uri, 0);
						item.Tag = resource.Ticket;
						lvResourceGallery.Items.Add(item);
					}
				}
				mMediaActive = 2;
				this.Text = $"Resources - {mMediaType[mMediaActive]}";
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* buttonVideo_Click																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Video media type button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void buttonVideo_Click(object sender, EventArgs e)
		{
			int index = 0;
			ListViewItem item = null;
			List<ResourceItem> resources = null;

			if(mMediaActive != 3)
			{
				pnlButtonAudio.BackColor = mColorDisabledBack;
				lblButtonAudio.ForeColor = mColorDisabledFore;
				pnlButtonImage.BackColor = mColorDisabledBack;
				lblButtonImage.ForeColor = mColorDisabledFore;
				pnlButtonLink.BackColor = mColorDisabledBack;
				lblButtonLink.ForeColor = mColorDisabledFore;
				pnlButtonVideo.BackColor = mColorEnabledBack;
				lblButtonVideo.ForeColor = mColorEnabledFore;
				lvResourceGallery.Items.Clear();
				lvResourceGallery.LargeImageList = imageListVideo;

				if(mResources != null)
				{
					resources = mResources.FindAll(x => x.ResourceType == "MediaVideo");
					foreach(ResourceItem resource in resources)
					{
						item = new ListViewItem(ResourceItem.Filename(resource), index++);
						item.Tag = resource.Ticket;
						lvResourceGallery.Items.Add(item);
					}
				}
				mMediaActive = 3;
				this.Text = $"Resources - {mMediaType[mMediaActive]}";
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* lvResourceGallery_DoubleClick																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The list view control has been double-clicked. If there is a selected
		/// item, emulate OK click.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void lvResourceGallery_DoubleClick(object sender, EventArgs e)
		{
			if(lvResourceGallery.SelectedItems.Count > 0)
			{
				btnOK_Click(sender, e);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* lvResourceGallery_SelectedIndexChanged																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The selected index has changed on the list view control.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void lvResourceGallery_SelectedIndexChanged(object sender,
			EventArgs e)
		{
			mnuEditRemoveSelected.Enabled =
				(lvResourceGallery.SelectedItems.Count > 0);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuEditRemoveSelected_Click																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Edit / Remove Selected Resources menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuEditRemoveSelected_Click(object sender, EventArgs e)
		{
			bool bContinue = false;
			int count = 0;
			int index = 0;
			ListViewItem item = null;
			int nodeCount = 0;
			List<NodeItem> nodes = null;
			PropertyItem property = null;
			List<ListViewItem> selectedItems = null;
			ResourceItem resource = null;
			string ticket = "";
			string sentence = "";
			string[] sentences = new string[]
			{
				"There is {0} object using this resource. " +
				"Do you wish to delete it anyway?",
				"There are {0} objects using this resource. " +
				"Do you wish to delete it anyway?",
				"There is {0} object using these resources. " +
				"Do you wish to delete them anyway?",
				"There are {0} objects using these resources. " +
				"Do you wish to delete them anyway?"
			};

			nodes = NodeFileObject.Nodes;
			count = lvResourceGallery.SelectedItems.Count;
			for(index = 0; index < count; index ++)
			{
				item = lvResourceGallery.SelectedItems[index];
				ticket = (string)item.Tag;
				resource =
					NodeFileObject.Resources.FirstOrDefault(x => x.Ticket == ticket);
				if(resource != null)
				{
					nodeCount += nodes.Count(x =>
						x.Properties.Exists(y =>
							y.Name == resource.ResourceType &&
							y.ToString() == ticket));
					foreach(NodeItem nodeItem in nodes)
					{
						nodeCount += nodeItem.Sockets.Count(x =>
							x.Properties.Exists(y =>
								y.Name == resource.ResourceType &&
								y.ToString() == ticket));
					}
				}
			}
			if(count > 0)
			{
				if(nodeCount > 0)
				{
					if(count == 1 && nodeCount == 1)
					{
						sentence = sentences[0];
					}
					else if(count == 1)
					{
						sentence = sentences[1];
					}
					else if(nodeCount == 1)
					{
						sentence = sentences[2];
					}
					else
					{
						sentence = sentences[3];
					}
					bContinue = (MessageBox.Show(String.Format(sentence, nodeCount),
						"Remove Selected Nodes", MessageBoxButtons.YesNo) ==
						DialogResult.Yes);
				}
				else
				{
					bContinue = true;
				}
			}
			if(bContinue)
			{
				for(index = 0; index < count; index++)
				{
					item = lvResourceGallery.SelectedItems[index];
					ticket = (string)item.Tag;
					resource =
						NodeFileObject.Resources.FirstOrDefault(x => x.Ticket == ticket);
					//	Remove references from all nodes and sockets.
					if(nodeCount > 0)
					{
						foreach(NodeItem nodeItem in nodes)
						{
							property = nodeItem.Properties.FirstOrDefault(x =>
								x.Name == resource.ResourceType &&
								x.ToString() == ticket);
							if(property != null)
							{
								nodeItem.Properties.Remove(property);
							}
							foreach(SocketItem socketItem in nodeItem.Sockets)
							{
								property = socketItem.Properties.FirstOrDefault(x =>
									x.Name == resource.ResourceType &&
									x.ToString() == ticket);
								if(property != null)
								{
									socketItem.Properties.Remove(property);
								}
							}
						}
					}
					//	Remove the actual resource.
					NodeFileObject.Resources.Remove(resource);
				}
				//	Remove the items from the list.
				foreach(ListViewItem listItem in lvResourceGallery.SelectedItems)
				{
					selectedItems.Add(listItem);
				}
				foreach(ListViewItem listItem in selectedItems)
				{
					lvResourceGallery.Items.Remove(listItem);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuFileLoadFromFileSystem_Click																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// File / Load From File System menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuFileLoadFromFileSystem_Click(object sender, EventArgs e)
		{
			//string basePath = "";
			bool bContinue = true;
			bool bEmbed = true;
			OpenFileDialog dialog = null;
			frmLinkEmbed dialogEmbed = null;
			FileInfo file = null;
			string[] filenames = null;
			string mediaType = "";
			string relativeFilename = "";
			ResourceCollection resources = null;

			bContinue = (NodeFileObject != null);
			if(bContinue)
			{
				dialog = new OpenFileDialog();
				dialog.CheckFileExists = true;
				dialog.CheckPathExists = true;
				dialog.DereferenceLinks = true;
				dialog.Filter =
					"Common Resource Files (" +
					"*.jpg;*.jpeg;*.png;*.bmp;*.webp;*.tiff;*.gif;" +
					"*.m4a;*.flac;*.mp3;*.wav;*.wma;*.aac;" +
					"*.mp4;*.webm;*.wmv;*.mov;*.avi" +
					")|" +
					"*.jpg;*.jpeg;*.png;*.bmp;*.webp;*.tiff;*.gif;" +
					"*.m4a;*.flac;*.mp3;*.wav;*.wma;*.aac;" +
					"*.mp4;*.webm;*.wmv;*.mov;*.avi|" +
					"Advanced Audio Coding (*.aac)|*.aac|" +
					"Apple QuickTime Movie (*.mov)|*.mov|" +
					"Audio Video Interleave (*.avi)|*.avi|" +
					"Free Lossless Audio Codec (*.flac)|*.flac|" +
					"Graphics Interchange Format (*.gif)|*.gif|" +
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
						file = new FileInfo(filenames[0]);
						relativeFilename = RelativeFilename(NodeFileInfo, file);
						dialogEmbed.LinkFilename = relativeFilename;
						dialogEmbed.CanEditRelativeName = false;
						dialogEmbed.Embed = false;
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
						//basePath = NodeFileInfo?.DirectoryName;
						resources = NodeFileObject.Resources;
						foreach(string filename in filenames)
						{
							file = new FileInfo(filename);
							relativeFilename = RelativeFilename(NodeFileInfo, file);

							//	Assign the resource ticket to each selected node.
							mediaType = GetMediaTypeName(file);
							switch(mediaType)
							{
								case "MediaAudio":
									CreateAudioResource(file, relativeFilename, bEmbed);
									break;
								case "MediaImage":
									CreateImageResource(file, relativeFilename, bEmbed);
									break;
								case "MediaVideo":
									CreateVideoResource(file, relativeFilename, bEmbed);
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
			LoadResources();
			Refresh();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mnuToolsEmbedAll_Click																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Tools / Embed All Resources menu option has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mnuToolsEmbedAll_Click(object sender, EventArgs e)
		{

		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnActivated																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the Activated event whenever the form has been displayed.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			mnuFileLoadFromFileSystem.Enabled = NodeFileObject != null;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	OnLoad																																*
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
			LoadResources();
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the frmResourceGallery Item.
		/// </summary>
		public frmResourceGallery()
		{
			InitializeComponent();

			if(!DesignMode)
			{
				this.menuResourceGallery.Renderer =
					new DarkThemeMenuRenderer(new DarkThemeMenuColorTable());
				imageListAudio.Images.Add(ResourceMain.Audio128);
				imageListLink.Images.Add(ResourceMain.Link128);
			}

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetSelectedTicket																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the globally unique identification of the selected resource.
		/// </summary>
		/// <returns>
		/// The Ticket property value of the selected resource, if a selection is
		/// active. Otherwise, an empty string.
		/// </returns>
		public string GetSelectedTicket()
		{
			string result = "";

			if(lvResourceGallery.SelectedItems?.Count > 0)
			{
				result = ScaffoldNodesUtil.ToString(
					lvResourceGallery.SelectedItems[0].Tag).ToLower();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//	//*-----------------------------------------------------------------------*
		//	//*	LoadResources																													*
		//	//*-----------------------------------------------------------------------*
		//	/// <summary>
		//	/// Load the resources to display.
		//	/// </summary>
		//	/// <param name="resources">
		//	/// Reference to the collection of resources present in the file.
		//	/// </param>
		//	public async void LoadResources(ResourceCollection resources)
		//	{
		//		Bitmap thumbnail = null;

		//		mMediaActive = -1;
		//		mResources = resources;

		//		imageListImage.Images.Clear();
		//		imageListVideo.Images.Clear();
		//		//	Load image lists.
		//		if(resources != null)
		//		{
		//			foreach(ResourceItem resource in resources)
		//			{
		//				switch(resource.ResourceType)
		//				{
		//					case "MediaAudio":
		//						//	All audio and links share a single image.
		//						if(mMediaActive == -1)
		//						{
		//							mMediaActive = 0;
		//						}
		//						break;
		//					case "MediaImage":
		//						thumbnail = CreateImageThumbnail(resource, 128, 128);
		//						imageListImage.Images.Add(thumbnail);
		//						if(mMediaActive == -1)
		//						{
		//							mMediaActive = 1;
		//						}
		//						break;
		//					case "MediaLink":
		//						if(mMediaActive == -1)
		//						{
		//							mMediaActive = 2;
		//						}
		//						break;
		//					case "MediaVideo":
		//						thumbnail = await CreateVideoThumbnail(resource, 128, 128);
		//						imageListVideo.Images.Add(thumbnail);
		//						if(mMediaActive == -1)
		//						{
		//							mMediaActive = 3;
		//						}
		//						break;
		//				}
		//			}
		//		}
		//		//	Activate the list for the selected tab.
		//		switch(mMediaActive)
		//		{
		//			case 0:
		//				mMediaActive = 1;
		//				buttonAudio_Click(null, null);
		//				break;
		//			case 1:
		//				mMediaActive = 2;
		//				buttonImage_Click(null, null);
		//				break;
		//			case 2:
		//				mMediaActive = 3;
		//				buttonLink_Click(null, null);
		//				break;
		//			case 3:
		//				mMediaActive = 4;
		//				buttonVideo_Click(null, null);
		//				break;
		//		}
		//	}
		//	//*-----------------------------------------------------------------------*
		//}
		////*-------------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	LoadResources																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Load the resources to display.
		/// </summary>
		public async void LoadResources()
		{
			Bitmap thumbnail = null;

			mMediaActive = -1;
			mResources = NodeFileObject.Resources;

			imageListImage.Images.Clear();
			imageListVideo.Images.Clear();
			//	Load image lists.
			if(mResources != null)
			{
				foreach(ResourceItem resource in mResources)
				{
					switch(resource.ResourceType)
					{
						case "MediaAudio":
							//	All audio and links share a single image.
							if(mMediaActive == -1)
							{
								mMediaActive = 0;
							}
							break;
						case "MediaImage":
							thumbnail = CreateImageThumbnail(resource, 128, 128);
							imageListImage.Images.Add(thumbnail);
							if(mMediaActive == -1)
							{
								mMediaActive = 1;
							}
							break;
						case "MediaLink":
							if(mMediaActive == -1)
							{
								mMediaActive = 2;
							}
							break;
						case "MediaVideo":
							thumbnail = await CreateVideoThumbnail(resource, 128, 128);
							imageListVideo.Images.Add(thumbnail);
							if(mMediaActive == -1)
							{
								mMediaActive = 3;
							}
							break;
					}
				}
			}
			//	Activate the list for the selected tab.
			switch(mMediaActive)
			{
				case 0:
					mMediaActive = 1;
					buttonAudio_Click(null, null);
					break;
				case 1:
					mMediaActive = 2;
					buttonImage_Click(null, null);
					break;
				case 2:
					mMediaActive = 3;
					buttonLink_Click(null, null);
					break;
				case 3:
					mMediaActive = 4;
					buttonVideo_Click(null, null);
					break;
			}
		}
		//*-----------------------------------------------------------------------*
	}
	//*-------------------------------------------------------------------------*

}
