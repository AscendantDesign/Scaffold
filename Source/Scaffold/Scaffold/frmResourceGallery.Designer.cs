//	frmResourceGallery.Designer.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
namespace Scaffold
{
	partial class frmResourceGallery
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.menuResourceGallery = new System.Windows.Forms.MenuStrip();
			this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileLoadFromFileSystem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFileClose = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsEmbedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindow = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindowAudio = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindowImage = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindowLink = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindowVideo = new System.Windows.Forms.ToolStripMenuItem();
			this.statusResourceGallery = new System.Windows.Forms.StatusStrip();
			this.statMessage = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlResourceGallery = new System.Windows.Forms.Panel();
			this.pnlForm = new System.Windows.Forms.Panel();
			this.lvResourceGallery = new System.Windows.Forms.ListView();
			this.imageListAudio = new System.Windows.Forms.ImageList(this.components);
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.pnlSelector = new System.Windows.Forms.Panel();
			this.pnlButtonVideo = new System.Windows.Forms.Panel();
			this.lblButtonVideo = new System.Windows.Forms.Label();
			this.pnlButtonLink = new System.Windows.Forms.Panel();
			this.lblButtonLink = new System.Windows.Forms.Label();
			this.pnlButtonImage = new System.Windows.Forms.Panel();
			this.lblButtonImage = new System.Windows.Forms.Label();
			this.pnlButtonAudio = new System.Windows.Forms.Panel();
			this.lblButtonAudio = new System.Windows.Forms.Label();
			this.pnlTopBuffer = new System.Windows.Forms.Panel();
			this.imageListImage = new System.Windows.Forms.ImageList(this.components);
			this.imageListLink = new System.Windows.Forms.ImageList(this.components);
			this.imageListVideo = new System.Windows.Forms.ImageList(this.components);
			this.menuResourceGallery.SuspendLayout();
			this.statusResourceGallery.SuspendLayout();
			this.pnlResourceGallery.SuspendLayout();
			this.pnlForm.SuspendLayout();
			this.pnlSelector.SuspendLayout();
			this.pnlButtonVideo.SuspendLayout();
			this.pnlButtonLink.SuspendLayout();
			this.pnlButtonImage.SuspendLayout();
			this.pnlButtonAudio.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuResourceGallery
			// 
			this.menuResourceGallery.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuResourceGallery.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuTools,
            this.mnuWindow});
			this.menuResourceGallery.Location = new System.Drawing.Point(0, 0);
			this.menuResourceGallery.Name = "menuResourceGallery";
			this.menuResourceGallery.Padding = new System.Windows.Forms.Padding(10, 2, 0, 2);
			this.menuResourceGallery.Size = new System.Drawing.Size(915, 28);
			this.menuResourceGallery.TabIndex = 0;
			this.menuResourceGallery.Text = "menuStrip1";
			// 
			// mnuFile
			// 
			this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileLoadFromFileSystem,
            this.toolStripMenuItem1,
            this.mnuFileClose});
			this.mnuFile.Name = "mnuFile";
			this.mnuFile.Size = new System.Drawing.Size(46, 24);
			this.mnuFile.Text = "&File";
			// 
			// mnuFileLoadFromFileSystem
			// 
			this.mnuFileLoadFromFileSystem.Enabled = false;
			this.mnuFileLoadFromFileSystem.Name = "mnuFileLoadFromFileSystem";
			this.mnuFileLoadFromFileSystem.Size = new System.Drawing.Size(241, 26);
			this.mnuFileLoadFromFileSystem.Text = "&Load From File System";
			this.mnuFileLoadFromFileSystem.ToolTipText = "Load one or more resources from the local file system.";
			this.mnuFileLoadFromFileSystem.Click += new System.EventHandler(this.mnuFileLoadFromFileSystem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(238, 6);
			// 
			// mnuFileClose
			// 
			this.mnuFileClose.Name = "mnuFileClose";
			this.mnuFileClose.Size = new System.Drawing.Size(241, 26);
			this.mnuFileClose.Text = "&Close";
			// 
			// mnuTools
			// 
			this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsEmbedAll});
			this.mnuTools.Name = "mnuTools";
			this.mnuTools.Size = new System.Drawing.Size(58, 24);
			this.mnuTools.Text = "&Tools";
			// 
			// mnuToolsEmbedAll
			// 
			this.mnuToolsEmbedAll.Name = "mnuToolsEmbedAll";
			this.mnuToolsEmbedAll.Size = new System.Drawing.Size(231, 26);
			this.mnuToolsEmbedAll.Text = "&Embed All Resources";
			this.mnuToolsEmbedAll.ToolTipText = "Load all linked resource files into this data file.";
			this.mnuToolsEmbedAll.Click += new System.EventHandler(this.mnuToolsEmbedAll_Click);
			// 
			// mnuWindow
			// 
			this.mnuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuWindowAudio,
            this.mnuWindowImage,
            this.mnuWindowLink,
            this.mnuWindowVideo});
			this.mnuWindow.Name = "mnuWindow";
			this.mnuWindow.Size = new System.Drawing.Size(78, 24);
			this.mnuWindow.Text = "&Window";
			// 
			// mnuWindowAudio
			// 
			this.mnuWindowAudio.Name = "mnuWindowAudio";
			this.mnuWindowAudio.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
			this.mnuWindowAudio.Size = new System.Drawing.Size(238, 26);
			this.mnuWindowAudio.Text = "&Audio Media";
			// 
			// mnuWindowImage
			// 
			this.mnuWindowImage.Name = "mnuWindowImage";
			this.mnuWindowImage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
			this.mnuWindowImage.Size = new System.Drawing.Size(238, 26);
			this.mnuWindowImage.Text = "&Image Media";
			// 
			// mnuWindowLink
			// 
			this.mnuWindowLink.Name = "mnuWindowLink";
			this.mnuWindowLink.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
			this.mnuWindowLink.Size = new System.Drawing.Size(238, 26);
			this.mnuWindowLink.Text = "&Link Resources";
			// 
			// mnuWindowVideo
			// 
			this.mnuWindowVideo.Name = "mnuWindowVideo";
			this.mnuWindowVideo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
			this.mnuWindowVideo.Size = new System.Drawing.Size(238, 26);
			this.mnuWindowVideo.Text = "&Video Media";
			// 
			// statusResourceGallery
			// 
			this.statusResourceGallery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.statusResourceGallery.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusResourceGallery.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statMessage});
			this.statusResourceGallery.Location = new System.Drawing.Point(0, 453);
			this.statusResourceGallery.Name = "statusResourceGallery";
			this.statusResourceGallery.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
			this.statusResourceGallery.Size = new System.Drawing.Size(915, 26);
			this.statusResourceGallery.TabIndex = 1;
			this.statusResourceGallery.Text = "statusStrip1";
			// 
			// statMessage
			// 
			this.statMessage.ForeColor = System.Drawing.Color.Gainsboro;
			this.statMessage.Name = "statMessage";
			this.statMessage.Size = new System.Drawing.Size(59, 20);
			this.statMessage.Text = "Ready...";
			// 
			// pnlResourceGallery
			// 
			this.pnlResourceGallery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.pnlResourceGallery.Controls.Add(this.pnlForm);
			this.pnlResourceGallery.Controls.Add(this.pnlSelector);
			this.pnlResourceGallery.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlResourceGallery.Location = new System.Drawing.Point(0, 28);
			this.pnlResourceGallery.Margin = new System.Windows.Forms.Padding(4);
			this.pnlResourceGallery.Name = "pnlResourceGallery";
			this.pnlResourceGallery.Size = new System.Drawing.Size(915, 425);
			this.pnlResourceGallery.TabIndex = 2;
			// 
			// pnlForm
			// 
			this.pnlForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.pnlForm.Controls.Add(this.lvResourceGallery);
			this.pnlForm.Controls.Add(this.btnOK);
			this.pnlForm.Controls.Add(this.btnCancel);
			this.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlForm.Location = new System.Drawing.Point(205, 0);
			this.pnlForm.Margin = new System.Windows.Forms.Padding(0);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Size = new System.Drawing.Size(710, 425);
			this.pnlForm.TabIndex = 1;
			// 
			// lvResourceGallery
			// 
			this.lvResourceGallery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvResourceGallery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.lvResourceGallery.ForeColor = System.Drawing.Color.Gainsboro;
			this.lvResourceGallery.HideSelection = false;
			this.lvResourceGallery.LargeImageList = this.imageListAudio;
			this.lvResourceGallery.Location = new System.Drawing.Point(20, 20);
			this.lvResourceGallery.Name = "lvResourceGallery";
			this.lvResourceGallery.Size = new System.Drawing.Size(670, 349);
			this.lvResourceGallery.TabIndex = 2;
			this.lvResourceGallery.UseCompatibleStateImageBehavior = false;
			this.lvResourceGallery.DoubleClick += new System.EventHandler(this.lvResourceGallery_DoubleClick);
			// 
			// imageListAudio
			// 
			this.imageListAudio.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageListAudio.ImageSize = new System.Drawing.Size(128, 128);
			this.imageListAudio.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(596, 377);
			this.btnOK.Margin = new System.Windows.Forms.Padding(4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(94, 35);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "&OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(494, 377);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(94, 35);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// pnlSelector
			// 
			this.pnlSelector.Controls.Add(this.pnlButtonVideo);
			this.pnlSelector.Controls.Add(this.pnlButtonLink);
			this.pnlSelector.Controls.Add(this.pnlButtonImage);
			this.pnlSelector.Controls.Add(this.pnlButtonAudio);
			this.pnlSelector.Controls.Add(this.pnlTopBuffer);
			this.pnlSelector.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlSelector.Location = new System.Drawing.Point(0, 0);
			this.pnlSelector.Margin = new System.Windows.Forms.Padding(4);
			this.pnlSelector.Name = "pnlSelector";
			this.pnlSelector.Size = new System.Drawing.Size(205, 425);
			this.pnlSelector.TabIndex = 0;
			// 
			// pnlButtonVideo
			// 
			this.pnlButtonVideo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.pnlButtonVideo.Controls.Add(this.lblButtonVideo);
			this.pnlButtonVideo.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pnlButtonVideo.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlButtonVideo.Location = new System.Drawing.Point(0, 159);
			this.pnlButtonVideo.Margin = new System.Windows.Forms.Padding(0);
			this.pnlButtonVideo.Name = "pnlButtonVideo";
			this.pnlButtonVideo.Size = new System.Drawing.Size(205, 49);
			this.pnlButtonVideo.TabIndex = 0;
			this.pnlButtonVideo.Click += new System.EventHandler(this.buttonVideo_Click);
			// 
			// lblButtonVideo
			// 
			this.lblButtonVideo.AutoSize = true;
			this.lblButtonVideo.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lblButtonVideo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblButtonVideo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
			this.lblButtonVideo.Location = new System.Drawing.Point(12, 12);
			this.lblButtonVideo.Name = "lblButtonVideo";
			this.lblButtonVideo.Size = new System.Drawing.Size(63, 25);
			this.lblButtonVideo.TabIndex = 2;
			this.lblButtonVideo.Text = "Video";
			this.lblButtonVideo.Click += new System.EventHandler(this.buttonVideo_Click);
			// 
			// pnlButtonLink
			// 
			this.pnlButtonLink.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.pnlButtonLink.Controls.Add(this.lblButtonLink);
			this.pnlButtonLink.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pnlButtonLink.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlButtonLink.Location = new System.Drawing.Point(0, 110);
			this.pnlButtonLink.Margin = new System.Windows.Forms.Padding(0);
			this.pnlButtonLink.Name = "pnlButtonLink";
			this.pnlButtonLink.Size = new System.Drawing.Size(205, 49);
			this.pnlButtonLink.TabIndex = 0;
			this.pnlButtonLink.Click += new System.EventHandler(this.buttonLink_Click);
			// 
			// lblButtonLink
			// 
			this.lblButtonLink.AutoSize = true;
			this.lblButtonLink.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lblButtonLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblButtonLink.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
			this.lblButtonLink.Location = new System.Drawing.Point(12, 12);
			this.lblButtonLink.Name = "lblButtonLink";
			this.lblButtonLink.Size = new System.Drawing.Size(48, 25);
			this.lblButtonLink.TabIndex = 2;
			this.lblButtonLink.Text = "Link";
			this.lblButtonLink.Click += new System.EventHandler(this.buttonLink_Click);
			// 
			// pnlButtonImage
			// 
			this.pnlButtonImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.pnlButtonImage.Controls.Add(this.lblButtonImage);
			this.pnlButtonImage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pnlButtonImage.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlButtonImage.Location = new System.Drawing.Point(0, 61);
			this.pnlButtonImage.Margin = new System.Windows.Forms.Padding(0);
			this.pnlButtonImage.Name = "pnlButtonImage";
			this.pnlButtonImage.Size = new System.Drawing.Size(205, 49);
			this.pnlButtonImage.TabIndex = 0;
			this.pnlButtonImage.Click += new System.EventHandler(this.buttonImage_Click);
			// 
			// lblButtonImage
			// 
			this.lblButtonImage.AutoSize = true;
			this.lblButtonImage.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lblButtonImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblButtonImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
			this.lblButtonImage.Location = new System.Drawing.Point(12, 12);
			this.lblButtonImage.Name = "lblButtonImage";
			this.lblButtonImage.Size = new System.Drawing.Size(66, 25);
			this.lblButtonImage.TabIndex = 1;
			this.lblButtonImage.Text = "Image";
			this.lblButtonImage.Click += new System.EventHandler(this.buttonImage_Click);
			// 
			// pnlButtonAudio
			// 
			this.pnlButtonAudio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.pnlButtonAudio.Controls.Add(this.lblButtonAudio);
			this.pnlButtonAudio.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pnlButtonAudio.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlButtonAudio.Location = new System.Drawing.Point(0, 12);
			this.pnlButtonAudio.Margin = new System.Windows.Forms.Padding(0);
			this.pnlButtonAudio.Name = "pnlButtonAudio";
			this.pnlButtonAudio.Size = new System.Drawing.Size(205, 49);
			this.pnlButtonAudio.TabIndex = 0;
			this.pnlButtonAudio.Click += new System.EventHandler(this.buttonAudio_Click);
			// 
			// lblButtonAudio
			// 
			this.lblButtonAudio.AutoSize = true;
			this.lblButtonAudio.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lblButtonAudio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblButtonAudio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(120)))));
			this.lblButtonAudio.Location = new System.Drawing.Point(12, 12);
			this.lblButtonAudio.Name = "lblButtonAudio";
			this.lblButtonAudio.Size = new System.Drawing.Size(63, 25);
			this.lblButtonAudio.TabIndex = 0;
			this.lblButtonAudio.Text = "Audio";
			this.lblButtonAudio.Click += new System.EventHandler(this.buttonAudio_Click);
			// 
			// pnlTopBuffer
			// 
			this.pnlTopBuffer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.pnlTopBuffer.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTopBuffer.Location = new System.Drawing.Point(0, 0);
			this.pnlTopBuffer.Margin = new System.Windows.Forms.Padding(4);
			this.pnlTopBuffer.Name = "pnlTopBuffer";
			this.pnlTopBuffer.Size = new System.Drawing.Size(205, 12);
			this.pnlTopBuffer.TabIndex = 0;
			// 
			// imageListImage
			// 
			this.imageListImage.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageListImage.ImageSize = new System.Drawing.Size(128, 128);
			this.imageListImage.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// imageListLink
			// 
			this.imageListLink.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageListLink.ImageSize = new System.Drawing.Size(128, 128);
			this.imageListLink.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// imageListVideo
			// 
			this.imageListVideo.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageListVideo.ImageSize = new System.Drawing.Size(128, 128);
			this.imageListVideo.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// frmResourceGallery
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(915, 479);
			this.Controls.Add(this.pnlResourceGallery);
			this.Controls.Add(this.statusResourceGallery);
			this.Controls.Add(this.menuResourceGallery);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MainMenuStrip = this.menuResourceGallery;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "frmResourceGallery";
			this.Text = "Resources";
			this.Activated += new System.EventHandler(this.frmResourceGallery_Activated);
			this.menuResourceGallery.ResumeLayout(false);
			this.menuResourceGallery.PerformLayout();
			this.statusResourceGallery.ResumeLayout(false);
			this.statusResourceGallery.PerformLayout();
			this.pnlResourceGallery.ResumeLayout(false);
			this.pnlForm.ResumeLayout(false);
			this.pnlSelector.ResumeLayout(false);
			this.pnlButtonVideo.ResumeLayout(false);
			this.pnlButtonVideo.PerformLayout();
			this.pnlButtonLink.ResumeLayout(false);
			this.pnlButtonLink.PerformLayout();
			this.pnlButtonImage.ResumeLayout(false);
			this.pnlButtonImage.PerformLayout();
			this.pnlButtonAudio.ResumeLayout(false);
			this.pnlButtonAudio.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuResourceGallery;
		private System.Windows.Forms.StatusStrip statusResourceGallery;
		private System.Windows.Forms.Panel pnlResourceGallery;
		private System.Windows.Forms.ToolStripMenuItem mnuFile;
		private System.Windows.Forms.ToolStripMenuItem mnuFileClose;
		private System.Windows.Forms.ToolStripMenuItem mnuWindow;
		private System.Windows.Forms.ToolStripMenuItem mnuWindowAudio;
		private System.Windows.Forms.ToolStripMenuItem mnuWindowImage;
		private System.Windows.Forms.ToolStripMenuItem mnuWindowLink;
		private System.Windows.Forms.ToolStripMenuItem mnuWindowVideo;
		private System.Windows.Forms.Panel pnlSelector;
		private System.Windows.Forms.Panel pnlButtonVideo;
		private System.Windows.Forms.Panel pnlButtonLink;
		private System.Windows.Forms.Panel pnlButtonImage;
		private System.Windows.Forms.Panel pnlButtonAudio;
		private System.Windows.Forms.Panel pnlTopBuffer;
		private System.Windows.Forms.Panel pnlForm;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblButtonVideo;
		private System.Windows.Forms.Label lblButtonLink;
		private System.Windows.Forms.Label lblButtonImage;
		private System.Windows.Forms.Label lblButtonAudio;
		private System.Windows.Forms.ListView lvResourceGallery;
		private System.Windows.Forms.ImageList imageListAudio;
		private System.Windows.Forms.ImageList imageListImage;
		private System.Windows.Forms.ImageList imageListLink;
		private System.Windows.Forms.ImageList imageListVideo;
		private System.Windows.Forms.ToolStripMenuItem mnuFileLoadFromFileSystem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem mnuTools;
		private System.Windows.Forms.ToolStripMenuItem mnuToolsEmbedAll;
		private System.Windows.Forms.ToolStripStatusLabel statMessage;
	}
}