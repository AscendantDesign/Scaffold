
namespace Scaffold
{
	partial class frmFrameFlipbook
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFrameFlipbook));
			this.pnlTitle = new System.Windows.Forms.Panel();
			this.pnlClose = new System.Windows.Forms.Panel();
			this.picClose = new System.Windows.Forms.PictureBox();
			this.pnlMaximize = new System.Windows.Forms.Panel();
			this.picMaximize = new System.Windows.Forms.PictureBox();
			this.pnlMinimize = new System.Windows.Forms.Panel();
			this.picMinimize = new System.Windows.Forms.PictureBox();
			this.lblTitle = new System.Windows.Forms.Label();
			this.picIcon = new System.Windows.Forms.PictureBox();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.lvThumbs = new Manina.Windows.Forms.ImageListView();
			this.picImage = new System.Windows.Forms.PictureBox();
			this.spltMain = new System.Windows.Forms.Splitter();
			this.pnlControls = new System.Windows.Forms.Panel();
			this.pnlTransportNext = new System.Windows.Forms.Panel();
			this.pnlTransportPause = new System.Windows.Forms.Panel();
			this.pnlTransportStop = new System.Windows.Forms.Panel();
			this.pnlTransportPlay = new System.Windows.Forms.Panel();
			this.pnlTransportBack = new System.Windows.Forms.Panel();
			this.pnlFlow = new Scaffold.FlipbookListControl();
			this.statusFrameSwitch = new System.Windows.Forms.StatusStrip();
			this.statMessage = new System.Windows.Forms.ToolStripStatusLabel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.menuFrameSwitch = new System.Windows.Forms.MenuStrip();
			this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileClose = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditCaptureFrame = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewThumbsize = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewThumbsize96 = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewThumbsize128 = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewThumbsize256 = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewThumbsize512 = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuViewThumbnails = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewImage = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTransport = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTransportBack = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTransportNext = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTransportStop = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTransportPause = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTransportPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditRunCommands = new System.Windows.Forms.ToolStripMenuItem();
			this.pnlTitle.SuspendLayout();
			this.pnlClose.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
			this.pnlMaximize.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picMaximize)).BeginInit();
			this.pnlMinimize.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picMinimize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
			this.pnlMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
			this.pnlControls.SuspendLayout();
			this.statusFrameSwitch.SuspendLayout();
			this.menuFrameSwitch.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlTitle
			// 
			this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.pnlTitle.Controls.Add(this.pnlClose);
			this.pnlTitle.Controls.Add(this.pnlMaximize);
			this.pnlTitle.Controls.Add(this.pnlMinimize);
			this.pnlTitle.Controls.Add(this.lblTitle);
			this.pnlTitle.Controls.Add(this.picIcon);
			this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTitle.Location = new System.Drawing.Point(0, 0);
			this.pnlTitle.Name = "pnlTitle";
			this.pnlTitle.Size = new System.Drawing.Size(1000, 48);
			this.pnlTitle.TabIndex = 0;
			// 
			// pnlClose
			// 
			this.pnlClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlClose.Controls.Add(this.picClose);
			this.pnlClose.Location = new System.Drawing.Point(949, 0);
			this.pnlClose.Name = "pnlClose";
			this.pnlClose.Size = new System.Drawing.Size(48, 48);
			this.pnlClose.TabIndex = 2;
			// 
			// picClose
			// 
			this.picClose.Image = global::Scaffold.ResourceMain.WinControlX0;
			this.picClose.Location = new System.Drawing.Point(12, 12);
			this.picClose.Name = "picClose";
			this.picClose.Size = new System.Drawing.Size(24, 24);
			this.picClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.picClose.TabIndex = 0;
			this.picClose.TabStop = false;
			// 
			// pnlMaximize
			// 
			this.pnlMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlMaximize.Controls.Add(this.picMaximize);
			this.pnlMaximize.Location = new System.Drawing.Point(904, 0);
			this.pnlMaximize.Name = "pnlMaximize";
			this.pnlMaximize.Size = new System.Drawing.Size(48, 48);
			this.pnlMaximize.TabIndex = 2;
			// 
			// picMaximize
			// 
			this.picMaximize.Image = global::Scaffold.ResourceMain.WinControlM0;
			this.picMaximize.Location = new System.Drawing.Point(12, 12);
			this.picMaximize.Name = "picMaximize";
			this.picMaximize.Size = new System.Drawing.Size(24, 24);
			this.picMaximize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.picMaximize.TabIndex = 0;
			this.picMaximize.TabStop = false;
			// 
			// pnlMinimize
			// 
			this.pnlMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlMinimize.Controls.Add(this.picMinimize);
			this.pnlMinimize.Location = new System.Drawing.Point(850, 0);
			this.pnlMinimize.Name = "pnlMinimize";
			this.pnlMinimize.Size = new System.Drawing.Size(48, 48);
			this.pnlMinimize.TabIndex = 2;
			// 
			// picMinimize
			// 
			this.picMinimize.Image = global::Scaffold.ResourceMain.WinControlH0;
			this.picMinimize.Location = new System.Drawing.Point(12, 12);
			this.picMinimize.Name = "picMinimize";
			this.picMinimize.Size = new System.Drawing.Size(24, 24);
			this.picMinimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.picMinimize.TabIndex = 0;
			this.picMinimize.TabStop = false;
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitle.ForeColor = System.Drawing.Color.DarkGray;
			this.lblTitle.Location = new System.Drawing.Point(57, 14);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(124, 20);
			this.lblTitle.TabIndex = 1;
			this.lblTitle.Text = "Frame Flipbook";
			// 
			// picIcon
			// 
			this.picIcon.Image = global::Scaffold.ResourceMain.ScaffoldIcon24;
			this.picIcon.Location = new System.Drawing.Point(12, 12);
			this.picIcon.Name = "picIcon";
			this.picIcon.Size = new System.Drawing.Size(24, 24);
			this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.picIcon.TabIndex = 0;
			this.picIcon.TabStop = false;
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.lvThumbs);
			this.pnlMain.Controls.Add(this.picImage);
			this.pnlMain.Controls.Add(this.spltMain);
			this.pnlMain.Controls.Add(this.pnlControls);
			this.pnlMain.Controls.Add(this.statusFrameSwitch);
			this.pnlMain.Controls.Add(this.panel5);
			this.pnlMain.Controls.Add(this.menuFrameSwitch);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(0, 48);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(1000, 514);
			this.pnlMain.TabIndex = 1;
			// 
			// lvThumbs
			// 
			this.lvThumbs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.lvThumbs.CacheLimit = "10MB";
			this.lvThumbs.Colors = new Manina.Windows.Forms.ImageListViewColor(resources.GetString("lvThumbs.Colors"));
			this.lvThumbs.Location = new System.Drawing.Point(369, 87);
			this.lvThumbs.Name = "lvThumbs";
			this.lvThumbs.PersistentCacheDirectory = "";
			this.lvThumbs.PersistentCacheSize = ((long)(100));
			this.lvThumbs.Size = new System.Drawing.Size(556, 376);
			this.lvThumbs.TabIndex = 3;
			this.lvThumbs.UseWIC = true;
			// 
			// picImage
			// 
			this.picImage.BackColor = System.Drawing.Color.Black;
			this.picImage.Location = new System.Drawing.Point(328, 50);
			this.picImage.Name = "picImage";
			this.picImage.Size = new System.Drawing.Size(357, 140);
			this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picImage.TabIndex = 4;
			this.picImage.TabStop = false;
			this.picImage.Visible = false;
			// 
			// spltMain
			// 
			this.spltMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.spltMain.Location = new System.Drawing.Point(300, 30);
			this.spltMain.Name = "spltMain";
			this.spltMain.Size = new System.Drawing.Size(6, 458);
			this.spltMain.TabIndex = 2;
			this.spltMain.TabStop = false;
			// 
			// pnlControls
			// 
			this.pnlControls.Controls.Add(this.pnlTransportNext);
			this.pnlControls.Controls.Add(this.pnlTransportPause);
			this.pnlControls.Controls.Add(this.pnlTransportStop);
			this.pnlControls.Controls.Add(this.pnlTransportPlay);
			this.pnlControls.Controls.Add(this.pnlTransportBack);
			this.pnlControls.Controls.Add(this.pnlFlow);
			this.pnlControls.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlControls.Location = new System.Drawing.Point(0, 30);
			this.pnlControls.Name = "pnlControls";
			this.pnlControls.Size = new System.Drawing.Size(300, 458);
			this.pnlControls.TabIndex = 1;
			// 
			// pnlTransportNext
			// 
			this.pnlTransportNext.BackgroundImage = global::Scaffold.ResourceMain.TransportNext32;
			this.pnlTransportNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pnlTransportNext.Location = new System.Drawing.Point(171, 3);
			this.pnlTransportNext.Name = "pnlTransportNext";
			this.pnlTransportNext.Size = new System.Drawing.Size(36, 36);
			this.pnlTransportNext.TabIndex = 1;
			// 
			// pnlTransportPause
			// 
			this.pnlTransportPause.BackgroundImage = global::Scaffold.ResourceMain.TransportPause32;
			this.pnlTransportPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pnlTransportPause.Location = new System.Drawing.Point(87, 3);
			this.pnlTransportPause.Name = "pnlTransportPause";
			this.pnlTransportPause.Size = new System.Drawing.Size(36, 36);
			this.pnlTransportPause.TabIndex = 1;
			// 
			// pnlTransportStop
			// 
			this.pnlTransportStop.BackgroundImage = global::Scaffold.ResourceMain.TransportStop32;
			this.pnlTransportStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pnlTransportStop.Location = new System.Drawing.Point(45, 3);
			this.pnlTransportStop.Name = "pnlTransportStop";
			this.pnlTransportStop.Size = new System.Drawing.Size(36, 36);
			this.pnlTransportStop.TabIndex = 1;
			// 
			// pnlTransportPlay
			// 
			this.pnlTransportPlay.BackgroundImage = global::Scaffold.ResourceMain.TransportPlay32;
			this.pnlTransportPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pnlTransportPlay.Location = new System.Drawing.Point(129, 3);
			this.pnlTransportPlay.Name = "pnlTransportPlay";
			this.pnlTransportPlay.Size = new System.Drawing.Size(36, 36);
			this.pnlTransportPlay.TabIndex = 1;
			// 
			// pnlTransportBack
			// 
			this.pnlTransportBack.BackgroundImage = global::Scaffold.ResourceMain.TransportBack32;
			this.pnlTransportBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pnlTransportBack.Location = new System.Drawing.Point(3, 3);
			this.pnlTransportBack.Name = "pnlTransportBack";
			this.pnlTransportBack.Size = new System.Drawing.Size(36, 36);
			this.pnlTransportBack.TabIndex = 1;
			// 
			// pnlFlow
			// 
			this.pnlFlow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlFlow.AutoScroll = true;
			this.pnlFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.pnlFlow.Frames = null;
			this.pnlFlow.Location = new System.Drawing.Point(3, 45);
			this.pnlFlow.Name = "pnlFlow";
			this.pnlFlow.Size = new System.Drawing.Size(294, 410);
			this.pnlFlow.TabIndex = 0;
			this.pnlFlow.WrapContents = false;
			// 
			// statusFrameSwitch
			// 
			this.statusFrameSwitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.statusFrameSwitch.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusFrameSwitch.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statMessage});
			this.statusFrameSwitch.Location = new System.Drawing.Point(0, 488);
			this.statusFrameSwitch.Name = "statusFrameSwitch";
			this.statusFrameSwitch.Size = new System.Drawing.Size(1000, 26);
			this.statusFrameSwitch.TabIndex = 6;
			this.statusFrameSwitch.Text = "statusStrip1";
			// 
			// statMessage
			// 
			this.statMessage.ForeColor = System.Drawing.Color.Gainsboro;
			this.statMessage.Name = "statMessage";
			this.statMessage.Size = new System.Drawing.Size(59, 20);
			this.statMessage.Text = "Ready...";
			// 
			// panel5
			// 
			this.panel5.BackColor = System.Drawing.Color.DarkGray;
			this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel5.Location = new System.Drawing.Point(0, 28);
			this.panel5.Margin = new System.Windows.Forms.Padding(0);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(1000, 2);
			this.panel5.TabIndex = 5;
			// 
			// menuFrameSwitch
			// 
			this.menuFrameSwitch.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuFrameSwitch.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuView,
            this.mnuTransport});
			this.menuFrameSwitch.Location = new System.Drawing.Point(0, 0);
			this.menuFrameSwitch.Name = "menuFrameSwitch";
			this.menuFrameSwitch.Size = new System.Drawing.Size(1000, 28);
			this.menuFrameSwitch.TabIndex = 0;
			this.menuFrameSwitch.Text = "menuStrip1";
			// 
			// mnuFile
			// 
			this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileOpen,
            this.mnuFileClose});
			this.mnuFile.Name = "mnuFile";
			this.mnuFile.Size = new System.Drawing.Size(46, 24);
			this.mnuFile.Text = "&File";
			// 
			// mnuFileOpen
			// 
			this.mnuFileOpen.Name = "mnuFileOpen";
			this.mnuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.mnuFileOpen.Size = new System.Drawing.Size(270, 26);
			this.mnuFileOpen.Text = "&Open Flipbook File";
			// 
			// mnuFileClose
			// 
			this.mnuFileClose.Name = "mnuFileClose";
			this.mnuFileClose.Size = new System.Drawing.Size(270, 26);
			this.mnuFileClose.Text = "&Close";
			// 
			// mnuEdit
			// 
			this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditCaptureFrame,
            this.mnuEditRunCommands});
			this.mnuEdit.Name = "mnuEdit";
			this.mnuEdit.Size = new System.Drawing.Size(49, 24);
			this.mnuEdit.Text = "&Edit";
			// 
			// mnuEditCaptureFrame
			// 
			this.mnuEditCaptureFrame.Name = "mnuEditCaptureFrame";
			this.mnuEditCaptureFrame.Size = new System.Drawing.Size(226, 26);
			this.mnuEditCaptureFrame.Text = "&Capture Frame";
			// 
			// mnuView
			// 
			this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewThumbsize,
            this.mnuViewSep1,
            this.mnuViewThumbnails,
            this.mnuViewImage});
			this.mnuView.Name = "mnuView";
			this.mnuView.Size = new System.Drawing.Size(55, 24);
			this.mnuView.Text = "&View";
			// 
			// mnuViewThumbsize
			// 
			this.mnuViewThumbsize.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewThumbsize96,
            this.mnuViewThumbsize128,
            this.mnuViewThumbsize256,
            this.mnuViewThumbsize512});
			this.mnuViewThumbsize.Name = "mnuViewThumbsize";
			this.mnuViewThumbsize.Size = new System.Drawing.Size(213, 26);
			this.mnuViewThumbsize.Text = "Thumbnail &Size";
			// 
			// mnuViewThumbsize96
			// 
			this.mnuViewThumbsize96.Name = "mnuViewThumbsize96";
			this.mnuViewThumbsize96.Size = new System.Drawing.Size(116, 26);
			this.mnuViewThumbsize96.Text = "&96";
			// 
			// mnuViewThumbsize128
			// 
			this.mnuViewThumbsize128.Name = "mnuViewThumbsize128";
			this.mnuViewThumbsize128.Size = new System.Drawing.Size(116, 26);
			this.mnuViewThumbsize128.Text = "&128";
			// 
			// mnuViewThumbsize256
			// 
			this.mnuViewThumbsize256.Checked = true;
			this.mnuViewThumbsize256.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mnuViewThumbsize256.Name = "mnuViewThumbsize256";
			this.mnuViewThumbsize256.Size = new System.Drawing.Size(116, 26);
			this.mnuViewThumbsize256.Text = "&256";
			// 
			// mnuViewThumbsize512
			// 
			this.mnuViewThumbsize512.Name = "mnuViewThumbsize512";
			this.mnuViewThumbsize512.Size = new System.Drawing.Size(116, 26);
			this.mnuViewThumbsize512.Text = "&512";
			// 
			// mnuViewSep1
			// 
			this.mnuViewSep1.Name = "mnuViewSep1";
			this.mnuViewSep1.Size = new System.Drawing.Size(210, 6);
			// 
			// mnuViewThumbnails
			// 
			this.mnuViewThumbnails.Checked = true;
			this.mnuViewThumbnails.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mnuViewThumbnails.Name = "mnuViewThumbnails";
			this.mnuViewThumbnails.ShortcutKeys = System.Windows.Forms.Keys.F8;
			this.mnuViewThumbnails.Size = new System.Drawing.Size(213, 26);
			this.mnuViewThumbnails.Text = "Thumbnail &List";
			// 
			// mnuViewImage
			// 
			this.mnuViewImage.Name = "mnuViewImage";
			this.mnuViewImage.ShortcutKeys = System.Windows.Forms.Keys.F9;
			this.mnuViewImage.Size = new System.Drawing.Size(213, 26);
			this.mnuViewImage.Text = "Image &Preview";
			// 
			// mnuTransport
			// 
			this.mnuTransport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTransportBack,
            this.mnuTransportNext,
            this.mnuTransportStop,
            this.mnuTransportPause,
            this.mnuTransportPlay});
			this.mnuTransport.Name = "mnuTransport";
			this.mnuTransport.Size = new System.Drawing.Size(85, 24);
			this.mnuTransport.Text = "T&ransport";
			// 
			// mnuTransportBack
			// 
			this.mnuTransportBack.Name = "mnuTransportBack";
			this.mnuTransportBack.ShortcutKeys = System.Windows.Forms.Keys.F4;
			this.mnuTransportBack.Size = new System.Drawing.Size(182, 26);
			this.mnuTransportBack.Text = "Step &Back";
			// 
			// mnuTransportNext
			// 
			this.mnuTransportNext.Name = "mnuTransportNext";
			this.mnuTransportNext.ShortcutKeys = System.Windows.Forms.Keys.F6;
			this.mnuTransportNext.Size = new System.Drawing.Size(182, 26);
			this.mnuTransportNext.Text = "Step &Next";
			// 
			// mnuTransportStop
			// 
			this.mnuTransportStop.Checked = true;
			this.mnuTransportStop.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mnuTransportStop.Name = "mnuTransportStop";
			this.mnuTransportStop.ShortcutKeys = System.Windows.Forms.Keys.F7;
			this.mnuTransportStop.Size = new System.Drawing.Size(182, 26);
			this.mnuTransportStop.Text = "&Stop";
			// 
			// mnuTransportPause
			// 
			this.mnuTransportPause.Name = "mnuTransportPause";
			this.mnuTransportPause.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F5)));
			this.mnuTransportPause.Size = new System.Drawing.Size(182, 26);
			this.mnuTransportPause.Text = "Pa&use";
			// 
			// mnuTransportPlay
			// 
			this.mnuTransportPlay.Name = "mnuTransportPlay";
			this.mnuTransportPlay.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.mnuTransportPlay.Size = new System.Drawing.Size(182, 26);
			this.mnuTransportPlay.Text = "&Play";
			// 
			// mnuEditRunCommands
			// 
			this.mnuEditRunCommands.Name = "mnuEditRunCommands";
			this.mnuEditRunCommands.Size = new System.Drawing.Size(226, 26);
			this.mnuEditRunCommands.Text = "Run Edit &Commands";
			this.mnuEditRunCommands.ToolTipText = "Run user-defined edit commands on each of the keyframes";
			// 
			// frmFrameFlipbook
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.ClientSize = new System.Drawing.Size(1000, 562);
			this.Controls.Add(this.pnlMain);
			this.Controls.Add(this.pnlTitle);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "frmFrameFlipbook";
			this.Text = "frmFrameSwitch";
			this.pnlTitle.ResumeLayout(false);
			this.pnlTitle.PerformLayout();
			this.pnlClose.ResumeLayout(false);
			this.pnlClose.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
			this.pnlMaximize.ResumeLayout(false);
			this.pnlMaximize.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picMaximize)).EndInit();
			this.pnlMinimize.ResumeLayout(false);
			this.pnlMinimize.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picMinimize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
			this.pnlMain.ResumeLayout(false);
			this.pnlMain.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
			this.pnlControls.ResumeLayout(false);
			this.statusFrameSwitch.ResumeLayout(false);
			this.statusFrameSwitch.PerformLayout();
			this.menuFrameSwitch.ResumeLayout(false);
			this.menuFrameSwitch.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlTitle;
		private System.Windows.Forms.PictureBox picIcon;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Panel pnlClose;
		private System.Windows.Forms.PictureBox picClose;
		private System.Windows.Forms.Panel pnlMaximize;
		private System.Windows.Forms.PictureBox picMaximize;
		private System.Windows.Forms.Panel pnlMinimize;
		private System.Windows.Forms.PictureBox picMinimize;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.MenuStrip menuFrameSwitch;
		private System.Windows.Forms.ToolStripMenuItem mnuFile;
		private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
		private System.Windows.Forms.Splitter spltMain;
		private System.Windows.Forms.Panel pnlControls;
		private Manina.Windows.Forms.ImageListView lvThumbs;
		private System.Windows.Forms.ToolStripMenuItem mnuFileClose;
		private System.Windows.Forms.ToolStripMenuItem mnuView;
		private System.Windows.Forms.ToolStripMenuItem mnuViewThumbsize;
		private System.Windows.Forms.ToolStripMenuItem mnuViewThumbsize96;
		private System.Windows.Forms.ToolStripMenuItem mnuViewThumbsize128;
		private System.Windows.Forms.ToolStripMenuItem mnuViewThumbsize256;
		private System.Windows.Forms.ToolStripMenuItem mnuViewThumbsize512;
		private FlipbookListControl pnlFlow;
		private System.Windows.Forms.ToolStripMenuItem mnuEdit;
		private System.Windows.Forms.ToolStripMenuItem mnuEditCaptureFrame;
		private System.Windows.Forms.ToolStripMenuItem mnuViewImage;
		private System.Windows.Forms.Panel pnlTransportNext;
		private System.Windows.Forms.Panel pnlTransportStop;
		private System.Windows.Forms.Panel pnlTransportPlay;
		private System.Windows.Forms.Panel pnlTransportBack;
		private System.Windows.Forms.Panel pnlTransportPause;
		private System.Windows.Forms.ToolStripMenuItem mnuTransport;
		private System.Windows.Forms.ToolStripMenuItem mnuTransportBack;
		private System.Windows.Forms.ToolStripMenuItem mnuTransportNext;
		private System.Windows.Forms.ToolStripMenuItem mnuTransportStop;
		private System.Windows.Forms.ToolStripMenuItem mnuTransportPause;
		private System.Windows.Forms.ToolStripMenuItem mnuTransportPlay;
		private System.Windows.Forms.PictureBox picImage;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.ToolStripSeparator mnuViewSep1;
		private System.Windows.Forms.ToolStripMenuItem mnuViewThumbnails;
		private System.Windows.Forms.StatusStrip statusFrameSwitch;
		private System.Windows.Forms.ToolStripStatusLabel statMessage;
		private System.Windows.Forms.ToolStripMenuItem mnuEditRunCommands;
	}
}
