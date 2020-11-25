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
						item = new ListViewItem(ResourceItem.Filename(resource), 0);
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
		//* frmResourceGallery_Activated																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The form has been displayed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void frmResourceGallery_Activated(object sender, EventArgs e)
		{
			mnuFileLoadFromFileSystem.Enabled =
				(NodeFileInfo != null && NodeFileObject != null);
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

			bContinue = (NodeFileInfo != null && NodeFileObject != null);
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
						basePath = NodeFileInfo.DirectoryName;
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
									CreateAudioResource(file, relativeFilename,
										bEmbed, resources);
									break;
								case "MediaImage":
									CreateImageResource(file, relativeFilename,
										bEmbed, resources);
									break;
								case "MediaVideo":
									CreateVideoResource(file, relativeFilename,
										bEmbed, resources);
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
			this.LoadResources(NodeFileObject.Resources);
			this.Refresh();
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
				result = ScaffoldUtil.ToString(
					lvResourceGallery.SelectedItems[0].Tag).ToLower();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	LoadResources																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Load the resources to display.
		/// </summary>
		/// <param name="resources">
		/// Reference to the collection of resources present in the file.
		/// </param>
		public async void LoadResources(ResourceCollection resources)
		{
			Bitmap thumbnail = null;

			mMediaActive = -1;
			mResources = resources;

			imageListImage.Images.Clear();
			imageListVideo.Images.Clear();
			//	Load image lists.
			if(resources != null)
			{
				foreach(ResourceItem resource in resources)
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
