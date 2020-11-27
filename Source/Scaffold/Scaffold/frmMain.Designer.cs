//	frmMain.Designer.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System.Windows.Forms;

namespace Scaffold
{
	partial class frmMain
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
			Scaffold.NodeFileItem nodeFileItem1 = new Scaffold.NodeFileItem();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.menuMain = new System.Windows.Forms.MenuStrip();
			this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFileConvert = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileConvertPPToHTML = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileConvertPPToTinyLMS = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileExport = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileExportDecisionTreeToPP = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFilePublish = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFilePublishSlackChatConversation = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFileDocumentProperties = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFileSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditUndo = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuEditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditSelectNone = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuEditNode = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditNodeColor = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditNodeColorText = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditNodeDuplicate = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditNodeSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuEditNodeAddAudio = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditNodeAddImage = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditNodeAddLink = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditNodeAddVideo = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditNodeAddResources = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditNodeSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuEditNodeRemoveAudio = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditNodeRemoveImage = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditNodeRemoveLink = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditNodeRemoveVideo = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditAlign = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditAlignLeft = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditAlignCenter = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditAlignRight = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditAlignTop = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditAlignMiddle = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditAlignBottom = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditAlignSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuEditAlignHorizontal = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditAlignVertical = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewZoom = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewZoomIn = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewZoomOut = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewZoom100 = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewScroll = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewScrollLayout = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewScrollNode = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsColorPalette = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsChatbotEmulator = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsChatbotEmulateBeginning = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsChatbotEmulateSelected = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsResourceGallery = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsAnimation = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsAnimationTimelineFileReport = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsAnimationFrameNToHTML = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsAnimationFrameNToSVG = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsAnimationSaveFrames = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuToolsClipboard = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsClipboardSave = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsClipboardLoad = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsClipboardSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuToolsClipboardClear = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsControl = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsControlUndo = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsControlNodeControl = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsControlNodeMeasurement = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsBase64 = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsBase64SRC = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsBase64URL = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsBase64Raw = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsBase64U = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsBase64UClipboard = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuToolsBase64UFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindow = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindowDecision = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindowSlide = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWindowHTMLViewer = new System.Windows.Forms.ToolStripMenuItem();
			this.statusMain = new System.Windows.Forms.StatusStrip();
			this.statMessage = new System.Windows.Forms.ToolStripStatusLabel();
			this.statProg = new System.Windows.Forms.ToolStripProgressBar();
			this.statSep1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.statCursor = new System.Windows.Forms.ToolStripStatusLabel();
			this.statSep2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.statEditor = new System.Windows.Forms.ToolStripStatusLabel();
			this.timerDrag = new System.Windows.Forms.Timer(this.components);
			this.tctlDocument = new Scaffold.WizardTabControl();
			this.tpgDecisionTreeEditorDocument = new System.Windows.Forms.TabPage();
			this.nodeControl = new Scaffold.NodeControl();
			this.tpgSlideEditorDocument = new System.Windows.Forms.TabPage();
			this.skControl = new SkiaSharp.Views.Desktop.SKControl();
			this.tpgHTML = new System.Windows.Forms.TabPage();
			this.tctlTools = new Scaffold.WizardTabControl();
			this.tpgDecisionTreeEditorTools = new System.Windows.Forms.TabPage();
			this.pnlDecisionTools = new System.Windows.Forms.Panel();
			this.toolDecisionPicTermination = new System.Windows.Forms.PictureBox();
			this.toolDecisionPicDelay = new System.Windows.Forms.PictureBox();
			this.toolDecisionPicFork = new System.Windows.Forms.PictureBox();
			this.toolDecisionPicStart = new System.Windows.Forms.PictureBox();
			this.tpgSlideEditorTools = new System.Windows.Forms.TabPage();
			this.pnlSlideTools = new System.Windows.Forms.Panel();
			this.toolSlidePicText = new System.Windows.Forms.PictureBox();
			this.toolSlidePicPolyline = new System.Windows.Forms.PictureBox();
			this.toolSlidePicLine = new System.Windows.Forms.PictureBox();
			this.toolSlidePicEllipse = new System.Windows.Forms.PictureBox();
			this.toolSlidePicRectangle = new System.Windows.Forms.PictureBox();
			this.toolSlidePicCursor = new System.Windows.Forms.PictureBox();
			this.tpgHTMLTools = new System.Windows.Forms.TabPage();
			this.btnHTML = new Scaffold.LabelButtonControl();
			this.mainFormButtonCollection = new Scaffold.LabelButtonControlCollection();
			this.btnSlideEditor = new Scaffold.LabelButtonControl();
			this.btnDecisionTreeEditor = new Scaffold.LabelButtonControl();
			this.panelWindowControl = new Scaffold.PanelWindowControl();
			this.timerAutoSave = new System.Windows.Forms.Timer(this.components);
			this.mnuEditFind = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.menuMain.SuspendLayout();
			this.statusMain.SuspendLayout();
			this.tctlDocument.SuspendLayout();
			this.tpgDecisionTreeEditorDocument.SuspendLayout();
			this.tpgSlideEditorDocument.SuspendLayout();
			this.tctlTools.SuspendLayout();
			this.tpgDecisionTreeEditorTools.SuspendLayout();
			this.pnlDecisionTools.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.toolDecisionPicTermination)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.toolDecisionPicDelay)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.toolDecisionPicFork)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.toolDecisionPicStart)).BeginInit();
			this.tpgSlideEditorTools.SuspendLayout();
			this.pnlSlideTools.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.toolSlidePicText)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.toolSlidePicPolyline)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.toolSlidePicLine)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.toolSlidePicEllipse)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.toolSlidePicRectangle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.toolSlidePicCursor)).BeginInit();
			this.SuspendLayout();
			// 
			// menuMain
			// 
			this.menuMain.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuView,
            this.mnuTools,
            this.mnuWindow});
			this.menuMain.Location = new System.Drawing.Point(0, 0);
			this.menuMain.Name = "menuMain";
			this.menuMain.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
			this.menuMain.Size = new System.Drawing.Size(900, 28);
			this.menuMain.TabIndex = 0;
			this.menuMain.Text = "menuMain";
			// 
			// mnuFile
			// 
			this.mnuFile.BackColor = System.Drawing.SystemColors.Control;
			this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileOpen,
            this.mnuFileSave,
            this.mnuFileSaveAs,
            this.mnuFileNew,
            this.mnuFileSep1,
            this.mnuFileConvert,
            this.mnuFileExport,
            this.mnuFilePublish,
            this.mnuFileSep2,
            this.mnuFileDocumentProperties,
            this.mnuFileSep3,
            this.mnuFileExit});
			this.mnuFile.ForeColor = System.Drawing.Color.Black;
			this.mnuFile.Name = "mnuFile";
			this.mnuFile.Size = new System.Drawing.Size(46, 24);
			this.mnuFile.Text = "&File";
			// 
			// mnuFileOpen
			// 
			this.mnuFileOpen.Name = "mnuFileOpen";
			this.mnuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.mnuFileOpen.Size = new System.Drawing.Size(273, 26);
			this.mnuFileOpen.Text = "&Open File";
			this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
			// 
			// mnuFileSave
			// 
			this.mnuFileSave.Name = "mnuFileSave";
			this.mnuFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.mnuFileSave.Size = new System.Drawing.Size(273, 26);
			this.mnuFileSave.Text = "&Save File";
			this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
			// 
			// mnuFileSaveAs
			// 
			this.mnuFileSaveAs.Name = "mnuFileSaveAs";
			this.mnuFileSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.mnuFileSaveAs.Size = new System.Drawing.Size(273, 26);
			this.mnuFileSaveAs.Text = "Save File &As ...";
			this.mnuFileSaveAs.Click += new System.EventHandler(this.mnuFileSaveAs_Click);
			// 
			// mnuFileNew
			// 
			this.mnuFileNew.Name = "mnuFileNew";
			this.mnuFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.mnuFileNew.Size = new System.Drawing.Size(273, 26);
			this.mnuFileNew.Text = "&New";
			this.mnuFileNew.Click += new System.EventHandler(this.mnuFileNew_Click);
			// 
			// mnuFileSep1
			// 
			this.mnuFileSep1.Name = "mnuFileSep1";
			this.mnuFileSep1.Size = new System.Drawing.Size(270, 6);
			// 
			// mnuFileConvert
			// 
			this.mnuFileConvert.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileConvertPPToHTML,
            this.mnuFileConvertPPToTinyLMS});
			this.mnuFileConvert.Name = "mnuFileConvert";
			this.mnuFileConvert.Size = new System.Drawing.Size(273, 26);
			this.mnuFileConvert.Text = "&Convert";
			// 
			// mnuFileConvertPPToHTML
			// 
			this.mnuFileConvertPPToHTML.Name = "mnuFileConvertPPToHTML";
			this.mnuFileConvertPPToHTML.Size = new System.Drawing.Size(242, 26);
			this.mnuFileConvertPPToHTML.Text = "PowerPoint to &HTML";
			this.mnuFileConvertPPToHTML.Click += new System.EventHandler(this.mnuFileConvertPPToHTML_Click);
			// 
			// mnuFileConvertPPToTinyLMS
			// 
			this.mnuFileConvertPPToTinyLMS.Name = "mnuFileConvertPPToTinyLMS";
			this.mnuFileConvertPPToTinyLMS.Size = new System.Drawing.Size(242, 26);
			this.mnuFileConvertPPToTinyLMS.Text = "PowerPoint to &TinyLMS";
			this.mnuFileConvertPPToTinyLMS.Click += new System.EventHandler(this.mnuFileConvertPPToTinyLMS_Click);
			// 
			// mnuFileExport
			// 
			this.mnuFileExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExportDecisionTreeToPP});
			this.mnuFileExport.Name = "mnuFileExport";
			this.mnuFileExport.Size = new System.Drawing.Size(273, 26);
			this.mnuFileExport.Text = "&Export";
			// 
			// mnuFileExportDecisionTreeToPP
			// 
			this.mnuFileExportDecisionTreeToPP.Name = "mnuFileExportDecisionTreeToPP";
			this.mnuFileExportDecisionTreeToPP.Size = new System.Drawing.Size(276, 26);
			this.mnuFileExportDecisionTreeToPP.Text = "Decision Tree to &PowerPoint";
			this.mnuFileExportDecisionTreeToPP.Click += new System.EventHandler(this.mnuFileExportDecisionTreeToPP_Click);
			// 
			// mnuFilePublish
			// 
			this.mnuFilePublish.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFilePublishSlackChatConversation});
			this.mnuFilePublish.Name = "mnuFilePublish";
			this.mnuFilePublish.Size = new System.Drawing.Size(273, 26);
			this.mnuFilePublish.Text = "&Publish";
			// 
			// mnuFilePublishSlackChatConversation
			// 
			this.mnuFilePublishSlackChatConversation.Name = "mnuFilePublishSlackChatConversation";
			this.mnuFilePublishSlackChatConversation.Size = new System.Drawing.Size(293, 26);
			this.mnuFilePublishSlackChatConversation.Text = "As &Slack Chatbot Conversation";
			this.mnuFilePublishSlackChatConversation.Click += new System.EventHandler(this.mnuFilePublishSlackChatConversation_Click);
			// 
			// mnuFileSep2
			// 
			this.mnuFileSep2.Name = "mnuFileSep2";
			this.mnuFileSep2.Size = new System.Drawing.Size(270, 6);
			// 
			// mnuFileDocumentProperties
			// 
			this.mnuFileDocumentProperties.Name = "mnuFileDocumentProperties";
			this.mnuFileDocumentProperties.Size = new System.Drawing.Size(273, 26);
			this.mnuFileDocumentProperties.Text = "&Document Properties";
			this.mnuFileDocumentProperties.Click += new System.EventHandler(this.mnuFileDocumentProperties_Click);
			// 
			// mnuFileSep3
			// 
			this.mnuFileSep3.Name = "mnuFileSep3";
			this.mnuFileSep3.Size = new System.Drawing.Size(270, 6);
			// 
			// mnuFileExit
			// 
			this.mnuFileExit.Name = "mnuFileExit";
			this.mnuFileExit.Size = new System.Drawing.Size(273, 26);
			this.mnuFileExit.Text = "E&xit";
			this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
			// 
			// mnuEdit
			// 
			this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditUndo,
            this.mnuEditSep1,
            this.mnuEditSelectAll,
            this.mnuEditSelectNone,
            this.mnuEditSep2,
            this.mnuEditFind,
            this.mnuEditSep3,
            this.mnuEditNode,
            this.mnuEditAlign});
			this.mnuEdit.Name = "mnuEdit";
			this.mnuEdit.Size = new System.Drawing.Size(49, 24);
			this.mnuEdit.Text = "&Edit";
			// 
			// mnuEditUndo
			// 
			this.mnuEditUndo.Enabled = false;
			this.mnuEditUndo.Name = "mnuEditUndo";
			this.mnuEditUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.mnuEditUndo.Size = new System.Drawing.Size(264, 26);
			this.mnuEditUndo.Text = "&Undo";
			this.mnuEditUndo.Click += new System.EventHandler(this.mnuEditUndo_Click);
			// 
			// mnuEditSep1
			// 
			this.mnuEditSep1.Name = "mnuEditSep1";
			this.mnuEditSep1.Size = new System.Drawing.Size(261, 6);
			// 
			// mnuEditSelectAll
			// 
			this.mnuEditSelectAll.Name = "mnuEditSelectAll";
			this.mnuEditSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.mnuEditSelectAll.Size = new System.Drawing.Size(264, 26);
			this.mnuEditSelectAll.Text = "Select &All";
			this.mnuEditSelectAll.Click += new System.EventHandler(this.mnuEditSelectAll_Click);
			// 
			// mnuEditSelectNone
			// 
			this.mnuEditSelectNone.Name = "mnuEditSelectNone";
			this.mnuEditSelectNone.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.A)));
			this.mnuEditSelectNone.Size = new System.Drawing.Size(264, 26);
			this.mnuEditSelectNone.Text = "Select N&one";
			this.mnuEditSelectNone.Click += new System.EventHandler(this.mnuEditSelectNone_Click);
			// 
			// mnuEditSep2
			// 
			this.mnuEditSep2.Name = "mnuEditSep2";
			this.mnuEditSep2.Size = new System.Drawing.Size(261, 6);
			// 
			// mnuEditNode
			// 
			this.mnuEditNode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditNodeColor,
            this.mnuEditNodeColorText,
            this.mnuEditNodeDuplicate,
            this.mnuEditNodeSep1,
            this.mnuEditNodeAddAudio,
            this.mnuEditNodeAddImage,
            this.mnuEditNodeAddLink,
            this.mnuEditNodeAddVideo,
            this.mnuEditNodeAddResources,
            this.mnuEditNodeSep2,
            this.mnuEditNodeRemoveAudio,
            this.mnuEditNodeRemoveImage,
            this.mnuEditNodeRemoveLink,
            this.mnuEditNodeRemoveVideo});
			this.mnuEditNode.Name = "mnuEditNode";
			this.mnuEditNode.Size = new System.Drawing.Size(264, 26);
			this.mnuEditNode.Text = "&Node";
			// 
			// mnuEditNodeColor
			// 
			this.mnuEditNodeColor.Name = "mnuEditNodeColor";
			this.mnuEditNodeColor.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
			this.mnuEditNodeColor.Size = new System.Drawing.Size(410, 26);
			this.mnuEditNodeColor.Text = "&Color Selected Background";
			this.mnuEditNodeColor.Click += new System.EventHandler(this.mnuEditNodeColor_Click);
			// 
			// mnuEditNodeColorText
			// 
			this.mnuEditNodeColorText.Name = "mnuEditNodeColorText";
			this.mnuEditNodeColorText.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
			this.mnuEditNodeColorText.Size = new System.Drawing.Size(410, 26);
			this.mnuEditNodeColorText.Text = "Color Selected &Text";
			this.mnuEditNodeColorText.Click += new System.EventHandler(this.mnuEditNodeColorText_Click);
			// 
			// mnuEditNodeDuplicate
			// 
			this.mnuEditNodeDuplicate.Name = "mnuEditNodeDuplicate";
			this.mnuEditNodeDuplicate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
			this.mnuEditNodeDuplicate.Size = new System.Drawing.Size(410, 26);
			this.mnuEditNodeDuplicate.Text = "&Duplicate Selected";
			this.mnuEditNodeDuplicate.Click += new System.EventHandler(this.mnuEditNodeDuplicate_Click);
			// 
			// mnuEditNodeSep1
			// 
			this.mnuEditNodeSep1.Name = "mnuEditNodeSep1";
			this.mnuEditNodeSep1.Size = new System.Drawing.Size(407, 6);
			// 
			// mnuEditNodeAddAudio
			// 
			this.mnuEditNodeAddAudio.Name = "mnuEditNodeAddAudio";
			this.mnuEditNodeAddAudio.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.A)));
			this.mnuEditNodeAddAudio.Size = new System.Drawing.Size(410, 26);
			this.mnuEditNodeAddAudio.Text = "Add &Audio To Selected";
			this.mnuEditNodeAddAudio.Click += new System.EventHandler(this.mnuEditNodeAddAudio_Click);
			// 
			// mnuEditNodeAddImage
			// 
			this.mnuEditNodeAddImage.Name = "mnuEditNodeAddImage";
			this.mnuEditNodeAddImage.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.I)));
			this.mnuEditNodeAddImage.Size = new System.Drawing.Size(410, 26);
			this.mnuEditNodeAddImage.Text = "Add &Image To Selected";
			this.mnuEditNodeAddImage.Click += new System.EventHandler(this.mnuEditNodeAddImage_Click);
			// 
			// mnuEditNodeAddLink
			// 
			this.mnuEditNodeAddLink.Name = "mnuEditNodeAddLink";
			this.mnuEditNodeAddLink.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.L)));
			this.mnuEditNodeAddLink.Size = new System.Drawing.Size(410, 26);
			this.mnuEditNodeAddLink.Text = "Add &Link To Selected";
			this.mnuEditNodeAddLink.Click += new System.EventHandler(this.mnuEditNodeAddLink_Click);
			// 
			// mnuEditNodeAddVideo
			// 
			this.mnuEditNodeAddVideo.Name = "mnuEditNodeAddVideo";
			this.mnuEditNodeAddVideo.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.V)));
			this.mnuEditNodeAddVideo.Size = new System.Drawing.Size(410, 26);
			this.mnuEditNodeAddVideo.Text = "Add &Video To Selected";
			this.mnuEditNodeAddVideo.Click += new System.EventHandler(this.mnuEditNodeAddVideo_Click);
			// 
			// mnuEditNodeAddResources
			// 
			this.mnuEditNodeAddResources.Name = "mnuEditNodeAddResources";
			this.mnuEditNodeAddResources.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.R)));
			this.mnuEditNodeAddResources.Size = new System.Drawing.Size(410, 26);
			this.mnuEditNodeAddResources.Text = "Add One Or More &Resouces To File";
			this.mnuEditNodeAddResources.Click += new System.EventHandler(this.mnuEditNodeAddResources_Click);
			// 
			// mnuEditNodeSep2
			// 
			this.mnuEditNodeSep2.Name = "mnuEditNodeSep2";
			this.mnuEditNodeSep2.Size = new System.Drawing.Size(407, 6);
			// 
			// mnuEditNodeRemoveAudio
			// 
			this.mnuEditNodeRemoveAudio.Name = "mnuEditNodeRemoveAudio";
			this.mnuEditNodeRemoveAudio.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.A)));
			this.mnuEditNodeRemoveAudio.Size = new System.Drawing.Size(410, 26);
			this.mnuEditNodeRemoveAudio.Text = "Remove A&udio From Selected";
			this.mnuEditNodeRemoveAudio.Click += new System.EventHandler(this.mnuEditNodeRemoveAudio_Click);
			// 
			// mnuEditNodeRemoveImage
			// 
			this.mnuEditNodeRemoveImage.Name = "mnuEditNodeRemoveImage";
			this.mnuEditNodeRemoveImage.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.I)));
			this.mnuEditNodeRemoveImage.Size = new System.Drawing.Size(410, 26);
			this.mnuEditNodeRemoveImage.Text = "Remove I&mage From Selected";
			this.mnuEditNodeRemoveImage.Click += new System.EventHandler(this.mnuEditNodeRemoveImage_Click);
			// 
			// mnuEditNodeRemoveLink
			// 
			this.mnuEditNodeRemoveLink.Name = "mnuEditNodeRemoveLink";
			this.mnuEditNodeRemoveLink.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.L)));
			this.mnuEditNodeRemoveLink.Size = new System.Drawing.Size(410, 26);
			this.mnuEditNodeRemoveLink.Text = "Remove Li&nk From Selected";
			this.mnuEditNodeRemoveLink.Click += new System.EventHandler(this.mnuEditNodeRemoveLink_Click);
			// 
			// mnuEditNodeRemoveVideo
			// 
			this.mnuEditNodeRemoveVideo.Name = "mnuEditNodeRemoveVideo";
			this.mnuEditNodeRemoveVideo.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.V)));
			this.mnuEditNodeRemoveVideo.Size = new System.Drawing.Size(410, 26);
			this.mnuEditNodeRemoveVideo.Text = "Remove Vid&eo From Selected";
			this.mnuEditNodeRemoveVideo.Click += new System.EventHandler(this.mnuEditNodeRemoveVideo_Click);
			// 
			// mnuEditAlign
			// 
			this.mnuEditAlign.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditAlignLeft,
            this.mnuEditAlignCenter,
            this.mnuEditAlignRight,
            this.mnuEditAlignTop,
            this.mnuEditAlignMiddle,
            this.mnuEditAlignBottom,
            this.mnuEditAlignSep1,
            this.mnuEditAlignHorizontal,
            this.mnuEditAlignVertical});
			this.mnuEditAlign.Name = "mnuEditAlign";
			this.mnuEditAlign.Size = new System.Drawing.Size(264, 26);
			this.mnuEditAlign.Text = "Align and &Distribute";
			// 
			// mnuEditAlignLeft
			// 
			this.mnuEditAlignLeft.Name = "mnuEditAlignLeft";
			this.mnuEditAlignLeft.Size = new System.Drawing.Size(259, 26);
			this.mnuEditAlignLeft.Text = "Align &Left";
			this.mnuEditAlignLeft.Click += new System.EventHandler(this.mnuEditAlignLeft_Click);
			// 
			// mnuEditAlignCenter
			// 
			this.mnuEditAlignCenter.Name = "mnuEditAlignCenter";
			this.mnuEditAlignCenter.Size = new System.Drawing.Size(259, 26);
			this.mnuEditAlignCenter.Text = "Align &Center Horizontally";
			this.mnuEditAlignCenter.Click += new System.EventHandler(this.mnuEditAlignCenter_Click);
			// 
			// mnuEditAlignRight
			// 
			this.mnuEditAlignRight.Name = "mnuEditAlignRight";
			this.mnuEditAlignRight.Size = new System.Drawing.Size(259, 26);
			this.mnuEditAlignRight.Text = "Align &Right";
			this.mnuEditAlignRight.Click += new System.EventHandler(this.mnuEditAlignRight_Click);
			// 
			// mnuEditAlignTop
			// 
			this.mnuEditAlignTop.Name = "mnuEditAlignTop";
			this.mnuEditAlignTop.Size = new System.Drawing.Size(259, 26);
			this.mnuEditAlignTop.Text = "Align &Top";
			this.mnuEditAlignTop.Click += new System.EventHandler(this.mnuEditAlignTop_Click);
			// 
			// mnuEditAlignMiddle
			// 
			this.mnuEditAlignMiddle.Name = "mnuEditAlignMiddle";
			this.mnuEditAlignMiddle.Size = new System.Drawing.Size(259, 26);
			this.mnuEditAlignMiddle.Text = "Align &Middle Vertically";
			this.mnuEditAlignMiddle.Click += new System.EventHandler(this.mnuEditAlignMiddle_Click);
			// 
			// mnuEditAlignBottom
			// 
			this.mnuEditAlignBottom.Name = "mnuEditAlignBottom";
			this.mnuEditAlignBottom.Size = new System.Drawing.Size(259, 26);
			this.mnuEditAlignBottom.Text = "Align &Bottom";
			this.mnuEditAlignBottom.Click += new System.EventHandler(this.mnuEditAlignBottom_Click);
			// 
			// mnuEditAlignSep1
			// 
			this.mnuEditAlignSep1.Name = "mnuEditAlignSep1";
			this.mnuEditAlignSep1.Size = new System.Drawing.Size(256, 6);
			// 
			// mnuEditAlignHorizontal
			// 
			this.mnuEditAlignHorizontal.Name = "mnuEditAlignHorizontal";
			this.mnuEditAlignHorizontal.Size = new System.Drawing.Size(259, 26);
			this.mnuEditAlignHorizontal.Text = "Distribute &Horizontally";
			this.mnuEditAlignHorizontal.Click += new System.EventHandler(this.mnuEditAlignHorizontal_Click);
			// 
			// mnuEditAlignVertical
			// 
			this.mnuEditAlignVertical.Name = "mnuEditAlignVertical";
			this.mnuEditAlignVertical.Size = new System.Drawing.Size(259, 26);
			this.mnuEditAlignVertical.Text = "Distribute &Vertically";
			this.mnuEditAlignVertical.Click += new System.EventHandler(this.mnuEditAlignVertical_Click);
			// 
			// mnuView
			// 
			this.mnuView.BackColor = System.Drawing.SystemColors.Control;
			this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewZoom,
            this.mnuViewScroll});
			this.mnuView.ForeColor = System.Drawing.Color.Black;
			this.mnuView.Name = "mnuView";
			this.mnuView.Size = new System.Drawing.Size(55, 24);
			this.mnuView.Text = "&View";
			// 
			// mnuViewZoom
			// 
			this.mnuViewZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewZoomIn,
            this.mnuViewZoomOut,
            this.mnuViewZoom100});
			this.mnuViewZoom.Name = "mnuViewZoom";
			this.mnuViewZoom.Size = new System.Drawing.Size(132, 26);
			this.mnuViewZoom.Text = "&Zoom";
			// 
			// mnuViewZoomIn
			// 
			this.mnuViewZoomIn.Name = "mnuViewZoomIn";
			this.mnuViewZoomIn.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.I)));
			this.mnuViewZoomIn.Size = new System.Drawing.Size(219, 26);
			this.mnuViewZoomIn.Text = "&In";
			this.mnuViewZoomIn.Click += new System.EventHandler(this.mnuViewZoomIn_Click);
			// 
			// mnuViewZoomOut
			// 
			this.mnuViewZoomOut.Name = "mnuViewZoomOut";
			this.mnuViewZoomOut.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
			this.mnuViewZoomOut.Size = new System.Drawing.Size(219, 26);
			this.mnuViewZoomOut.Text = "&Out";
			this.mnuViewZoomOut.Click += new System.EventHandler(this.mnuViewZoomOut_Click);
			// 
			// mnuViewZoom100
			// 
			this.mnuViewZoom100.Name = "mnuViewZoom100";
			this.mnuViewZoom100.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
			this.mnuViewZoom100.Size = new System.Drawing.Size(219, 26);
			this.mnuViewZoom100.Text = "&100%";
			this.mnuViewZoom100.Click += new System.EventHandler(this.mnuViewZoom100_Click);
			// 
			// mnuViewScroll
			// 
			this.mnuViewScroll.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewScrollLayout,
            this.mnuViewScrollNode});
			this.mnuViewScroll.Name = "mnuViewScroll";
			this.mnuViewScroll.Size = new System.Drawing.Size(132, 26);
			this.mnuViewScroll.Text = "&Scroll";
			// 
			// mnuViewScrollLayout
			// 
			this.mnuViewScrollLayout.Name = "mnuViewScrollLayout";
			this.mnuViewScrollLayout.Size = new System.Drawing.Size(297, 26);
			this.mnuViewScrollLayout.Text = "Scroll &Layout Into View";
			this.mnuViewScrollLayout.Click += new System.EventHandler(this.mnuViewScrollLayout_Click);
			// 
			// mnuViewScrollNode
			// 
			this.mnuViewScrollNode.Name = "mnuViewScrollNode";
			this.mnuViewScrollNode.Size = new System.Drawing.Size(297, 26);
			this.mnuViewScrollNode.Text = "Scroll Selected &Node Into View";
			this.mnuViewScrollNode.Click += new System.EventHandler(this.mnuViewScrollNode_Click);
			// 
			// mnuTools
			// 
			this.mnuTools.BackColor = System.Drawing.SystemColors.Control;
			this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsColorPalette,
            this.mnuToolsChatbotEmulator,
            this.mnuToolsResourceGallery,
            this.mnuToolsAnimation,
            this.toolStripMenuItem1,
            this.mnuToolsClipboard,
            this.mnuToolsControl,
            this.mnuToolsBase64,
            this.mnuToolsBase64U});
			this.mnuTools.ForeColor = System.Drawing.Color.Black;
			this.mnuTools.Name = "mnuTools";
			this.mnuTools.Size = new System.Drawing.Size(58, 24);
			this.mnuTools.Text = "&Tools";
			// 
			// mnuToolsColorPalette
			// 
			this.mnuToolsColorPalette.ForeColor = System.Drawing.Color.Black;
			this.mnuToolsColorPalette.Name = "mnuToolsColorPalette";
			this.mnuToolsColorPalette.Size = new System.Drawing.Size(249, 26);
			this.mnuToolsColorPalette.Text = "&Color Palette";
			this.mnuToolsColorPalette.Click += new System.EventHandler(this.mnuToolsColorPalette_Click);
			// 
			// mnuToolsChatbotEmulator
			// 
			this.mnuToolsChatbotEmulator.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsChatbotEmulateBeginning,
            this.mnuToolsChatbotEmulateSelected});
			this.mnuToolsChatbotEmulator.Name = "mnuToolsChatbotEmulator";
			this.mnuToolsChatbotEmulator.Size = new System.Drawing.Size(249, 26);
			this.mnuToolsChatbotEmulator.Text = "Chatbot &Emulator";
			// 
			// mnuToolsChatbotEmulateBeginning
			// 
			this.mnuToolsChatbotEmulateBeginning.Name = "mnuToolsChatbotEmulateBeginning";
			this.mnuToolsChatbotEmulateBeginning.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
			this.mnuToolsChatbotEmulateBeginning.Size = new System.Drawing.Size(305, 26);
			this.mnuToolsChatbotEmulateBeginning.Text = "Emulate From &Beginning";
			this.mnuToolsChatbotEmulateBeginning.Click += new System.EventHandler(this.mnuToolsChatbotEmulateBeginning_Click);
			// 
			// mnuToolsChatbotEmulateSelected
			// 
			this.mnuToolsChatbotEmulateSelected.Name = "mnuToolsChatbotEmulateSelected";
			this.mnuToolsChatbotEmulateSelected.Size = new System.Drawing.Size(305, 26);
			this.mnuToolsChatbotEmulateSelected.Text = "Emulate From &Selected Node";
			this.mnuToolsChatbotEmulateSelected.Click += new System.EventHandler(this.mnuToolsChatbotEmulateSelected_Click);
			// 
			// mnuToolsResourceGallery
			// 
			this.mnuToolsResourceGallery.Name = "mnuToolsResourceGallery";
			this.mnuToolsResourceGallery.Size = new System.Drawing.Size(249, 26);
			this.mnuToolsResourceGallery.Text = "&Resource Gallery";
			this.mnuToolsResourceGallery.Click += new System.EventHandler(this.mnuToolsResourceGallery_Click);
			// 
			// mnuToolsAnimation
			// 
			this.mnuToolsAnimation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsAnimationTimelineFileReport,
            this.mnuToolsAnimationFrameNToHTML,
            this.mnuToolsAnimationFrameNToSVG,
            this.mnuToolsAnimationSaveFrames});
			this.mnuToolsAnimation.ForeColor = System.Drawing.Color.Black;
			this.mnuToolsAnimation.Name = "mnuToolsAnimation";
			this.mnuToolsAnimation.Size = new System.Drawing.Size(249, 26);
			this.mnuToolsAnimation.Text = "&Animation";
			// 
			// mnuToolsAnimationTimelineFileReport
			// 
			this.mnuToolsAnimationTimelineFileReport.ForeColor = System.Drawing.Color.Black;
			this.mnuToolsAnimationTimelineFileReport.Name = "mnuToolsAnimationTimelineFileReport";
			this.mnuToolsAnimationTimelineFileReport.Size = new System.Drawing.Size(297, 26);
			this.mnuToolsAnimationTimelineFileReport.Text = "Timeline File &Report";
			this.mnuToolsAnimationTimelineFileReport.ToolTipText = "Read a Timeline.JSON file and display a report.";
			this.mnuToolsAnimationTimelineFileReport.Click += new System.EventHandler(this.mnuToolsAnimationTimelineFileReport_Click);
			// 
			// mnuToolsAnimationFrameNToHTML
			// 
			this.mnuToolsAnimationFrameNToHTML.Name = "mnuToolsAnimationFrameNToHTML";
			this.mnuToolsAnimationFrameNToHTML.Size = new System.Drawing.Size(297, 26);
			this.mnuToolsAnimationFrameNToHTML.Text = "Draw &Frame [N] to HTML View";
			this.mnuToolsAnimationFrameNToHTML.Click += new System.EventHandler(this.mnuToolsAnimationFrameNToHTML_Click);
			// 
			// mnuToolsAnimationFrameNToSVG
			// 
			this.mnuToolsAnimationFrameNToSVG.Name = "mnuToolsAnimationFrameNToSVG";
			this.mnuToolsAnimationFrameNToSVG.Size = new System.Drawing.Size(297, 26);
			this.mnuToolsAnimationFrameNToSVG.Text = "Draw Frame [N] to &SVG View";
			this.mnuToolsAnimationFrameNToSVG.Click += new System.EventHandler(this.mnuToolsAnimationFrameNToSVG_Click);
			// 
			// mnuToolsAnimationSaveFrames
			// 
			this.mnuToolsAnimationSaveFrames.Name = "mnuToolsAnimationSaveFrames";
			this.mnuToolsAnimationSaveFrames.Size = new System.Drawing.Size(297, 26);
			this.mnuToolsAnimationSaveFrames.Text = "Save &Animation Frames to Disk";
			this.mnuToolsAnimationSaveFrames.Click += new System.EventHandler(this.mnuToolsAnimationSaveFrames_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(246, 6);
			// 
			// mnuToolsClipboard
			// 
			this.mnuToolsClipboard.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsClipboardSave,
            this.mnuToolsClipboardLoad,
            this.mnuToolsClipboardSep1,
            this.mnuToolsClipboardClear});
			this.mnuToolsClipboard.ForeColor = System.Drawing.Color.Black;
			this.mnuToolsClipboard.Name = "mnuToolsClipboard";
			this.mnuToolsClipboard.Size = new System.Drawing.Size(249, 26);
			this.mnuToolsClipboard.Text = "C&lipboard";
			// 
			// mnuToolsClipboardSave
			// 
			this.mnuToolsClipboardSave.Name = "mnuToolsClipboardSave";
			this.mnuToolsClipboardSave.Size = new System.Drawing.Size(199, 26);
			this.mnuToolsClipboardSave.Text = "&Save As File...";
			this.mnuToolsClipboardSave.Click += new System.EventHandler(this.mnuToolsClipboardSave_Click);
			// 
			// mnuToolsClipboardLoad
			// 
			this.mnuToolsClipboardLoad.Name = "mnuToolsClipboardLoad";
			this.mnuToolsClipboardLoad.Size = new System.Drawing.Size(199, 26);
			this.mnuToolsClipboardLoad.Text = "&Load From File...";
			this.mnuToolsClipboardLoad.Click += new System.EventHandler(this.mnuToolsClipboardLoad_Click);
			// 
			// mnuToolsClipboardSep1
			// 
			this.mnuToolsClipboardSep1.Name = "mnuToolsClipboardSep1";
			this.mnuToolsClipboardSep1.Size = new System.Drawing.Size(196, 6);
			// 
			// mnuToolsClipboardClear
			// 
			this.mnuToolsClipboardClear.Name = "mnuToolsClipboardClear";
			this.mnuToolsClipboardClear.Size = new System.Drawing.Size(199, 26);
			this.mnuToolsClipboardClear.Text = "&Clear";
			this.mnuToolsClipboardClear.Click += new System.EventHandler(this.mnuToolsClipboardClear_Click);
			// 
			// mnuToolsControl
			// 
			this.mnuToolsControl.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsControlUndo,
            this.mnuToolsControlNodeControl,
            this.mnuToolsControlNodeMeasurement});
			this.mnuToolsControl.Name = "mnuToolsControl";
			this.mnuToolsControl.Size = new System.Drawing.Size(249, 26);
			this.mnuToolsControl.Text = "C&ontrol Reports";
			// 
			// mnuToolsControlUndo
			// 
			this.mnuToolsControlUndo.Name = "mnuToolsControlUndo";
			this.mnuToolsControlUndo.Size = new System.Drawing.Size(260, 26);
			this.mnuToolsControlUndo.Text = "&Undo Buffer Contents";
			this.mnuToolsControlUndo.Click += new System.EventHandler(this.mnuToolsControlUndo_Click);
			// 
			// mnuToolsControlNodeControl
			// 
			this.mnuToolsControlNodeControl.Name = "mnuToolsControlNodeControl";
			this.mnuToolsControlNodeControl.Size = new System.Drawing.Size(260, 26);
			this.mnuToolsControlNodeControl.Text = "Node&Control Information";
			this.mnuToolsControlNodeControl.Click += new System.EventHandler(this.mnuToolsControlNodeControl_Click);
			// 
			// mnuToolsControlNodeMeasurement
			// 
			this.mnuToolsControlNodeMeasurement.Name = "mnuToolsControlNodeMeasurement";
			this.mnuToolsControlNodeMeasurement.Size = new System.Drawing.Size(260, 26);
			this.mnuToolsControlNodeMeasurement.Text = "&Node Measurements";
			this.mnuToolsControlNodeMeasurement.Click += new System.EventHandler(this.mnuToolsControlNodeMeasurement_Click);
			// 
			// mnuToolsBase64
			// 
			this.mnuToolsBase64.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsBase64SRC,
            this.mnuToolsBase64URL,
            this.mnuToolsBase64Raw});
			this.mnuToolsBase64.ForeColor = System.Drawing.Color.Black;
			this.mnuToolsBase64.Name = "mnuToolsBase64";
			this.mnuToolsBase64.Size = new System.Drawing.Size(249, 26);
			this.mnuToolsBase64.Text = "base64 Data &Packing";
			// 
			// mnuToolsBase64SRC
			// 
			this.mnuToolsBase64SRC.Name = "mnuToolsBase64SRC";
			this.mnuToolsBase64SRC.Size = new System.Drawing.Size(212, 26);
			this.mnuToolsBase64SRC.Text = "To HTML IMG &SRC";
			this.mnuToolsBase64SRC.Click += new System.EventHandler(this.mnuToolsBase64SRC_Click);
			// 
			// mnuToolsBase64URL
			// 
			this.mnuToolsBase64URL.Name = "mnuToolsBase64URL";
			this.mnuToolsBase64URL.Size = new System.Drawing.Size(212, 26);
			this.mnuToolsBase64URL.Text = "To CSS &URL";
			this.mnuToolsBase64URL.Click += new System.EventHandler(this.mnuToolsBase64URL_Click);
			// 
			// mnuToolsBase64Raw
			// 
			this.mnuToolsBase64Raw.Name = "mnuToolsBase64Raw";
			this.mnuToolsBase64Raw.Size = new System.Drawing.Size(212, 26);
			this.mnuToolsBase64Raw.Text = "To &Raw String";
			this.mnuToolsBase64Raw.Click += new System.EventHandler(this.mnuToolsBase64Raw_Click);
			// 
			// mnuToolsBase64U
			// 
			this.mnuToolsBase64U.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsBase64UClipboard,
            this.mnuToolsBase64UFile});
			this.mnuToolsBase64U.Name = "mnuToolsBase64U";
			this.mnuToolsBase64U.Size = new System.Drawing.Size(249, 26);
			this.mnuToolsBase64U.Text = "base64 Data &Unpacking";
			// 
			// mnuToolsBase64UClipboard
			// 
			this.mnuToolsBase64UClipboard.Name = "mnuToolsBase64UClipboard";
			this.mnuToolsBase64UClipboard.Size = new System.Drawing.Size(196, 26);
			this.mnuToolsBase64UClipboard.Text = "From &Clipboard";
			this.mnuToolsBase64UClipboard.Click += new System.EventHandler(this.mnuToolsBase64UClipboard_Click);
			// 
			// mnuToolsBase64UFile
			// 
			this.mnuToolsBase64UFile.Name = "mnuToolsBase64UFile";
			this.mnuToolsBase64UFile.Size = new System.Drawing.Size(196, 26);
			this.mnuToolsBase64UFile.Text = "From &File";
			this.mnuToolsBase64UFile.Click += new System.EventHandler(this.mnuToolsBase64UFile_Click);
			// 
			// mnuWindow
			// 
			this.mnuWindow.BackColor = System.Drawing.SystemColors.Control;
			this.mnuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuWindowDecision,
            this.mnuWindowSlide,
            this.mnuWindowHTMLViewer});
			this.mnuWindow.ForeColor = System.Drawing.Color.Black;
			this.mnuWindow.Name = "mnuWindow";
			this.mnuWindow.Size = new System.Drawing.Size(78, 24);
			this.mnuWindow.Text = "&Window";
			// 
			// mnuWindowDecision
			// 
			this.mnuWindowDecision.Checked = true;
			this.mnuWindowDecision.CheckState = System.Windows.Forms.CheckState.Checked;
			this.mnuWindowDecision.Name = "mnuWindowDecision";
			this.mnuWindowDecision.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
			this.mnuWindowDecision.Size = new System.Drawing.Size(231, 26);
			this.mnuWindowDecision.Text = "&Decision Tree";
			this.mnuWindowDecision.Click += new System.EventHandler(this.mnuWindowDecision_Click);
			// 
			// mnuWindowSlide
			// 
			this.mnuWindowSlide.Name = "mnuWindowSlide";
			this.mnuWindowSlide.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
			this.mnuWindowSlide.Size = new System.Drawing.Size(231, 26);
			this.mnuWindowSlide.Text = "&Slide Editor";
			this.mnuWindowSlide.Click += new System.EventHandler(this.mnuWindowSlide_Click);
			// 
			// mnuWindowHTMLViewer
			// 
			this.mnuWindowHTMLViewer.Name = "mnuWindowHTMLViewer";
			this.mnuWindowHTMLViewer.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
			this.mnuWindowHTMLViewer.Size = new System.Drawing.Size(231, 26);
			this.mnuWindowHTMLViewer.Text = "&HTML Viewer";
			this.mnuWindowHTMLViewer.Click += new System.EventHandler(this.mnuWindowHTMLViewer_Click);
			// 
			// statusMain
			// 
			this.statusMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.statusMain.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statMessage,
            this.statProg,
            this.statSep1,
            this.statCursor,
            this.statSep2,
            this.statEditor});
			this.statusMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.statusMain.Location = new System.Drawing.Point(0, 564);
			this.statusMain.Name = "statusMain";
			this.statusMain.Size = new System.Drawing.Size(900, 26);
			this.statusMain.TabIndex = 1;
			this.statusMain.Text = "statusMain";
			// 
			// statMessage
			// 
			this.statMessage.ForeColor = System.Drawing.Color.Gainsboro;
			this.statMessage.Name = "statMessage";
			this.statMessage.Size = new System.Drawing.Size(59, 20);
			this.statMessage.Text = "Ready...";
			// 
			// statProg
			// 
			this.statProg.Name = "statProg";
			this.statProg.Size = new System.Drawing.Size(100, 15);
			// 
			// statSep1
			// 
			this.statSep1.Name = "statSep1";
			this.statSep1.Size = new System.Drawing.Size(13, 20);
			this.statSep1.Text = " ";
			// 
			// statCursor
			// 
			this.statCursor.ForeColor = System.Drawing.Color.Gainsboro;
			this.statCursor.Name = "statCursor";
			this.statCursor.Size = new System.Drawing.Size(55, 20);
			this.statCursor.Text = "X:0, Y:0";
			// 
			// statSep2
			// 
			this.statSep2.Name = "statSep2";
			this.statSep2.Size = new System.Drawing.Size(13, 20);
			this.statSep2.Text = " ";
			// 
			// statEditor
			// 
			this.statEditor.ForeColor = System.Drawing.Color.Gainsboro;
			this.statEditor.Name = "statEditor";
			this.statEditor.Size = new System.Drawing.Size(55, 20);
			this.statEditor.Spring = true;
			this.statEditor.Text = "X:0, Y:0";
			// 
			// timerDrag
			// 
			this.timerDrag.Interval = 1000;
			this.timerDrag.Tick += new System.EventHandler(this.timerDrag_Tick);
			// 
			// tctlDocument
			// 
			this.tctlDocument.Controls.Add(this.tpgDecisionTreeEditorDocument);
			this.tctlDocument.Controls.Add(this.tpgSlideEditorDocument);
			this.tctlDocument.Controls.Add(this.tpgHTML);
			this.tctlDocument.Location = new System.Drawing.Point(213, 66);
			this.tctlDocument.Margin = new System.Windows.Forms.Padding(0);
			this.tctlDocument.Multiline = true;
			this.tctlDocument.Name = "tctlDocument";
			this.tctlDocument.Padding = new System.Drawing.Point(0, 0);
			this.tctlDocument.SelectedIndex = 0;
			this.tctlDocument.SelectedTabIndex = 0;
			this.tctlDocument.SelectedTabName = "Decision";
			this.tctlDocument.ShowTabs = true;
			this.tctlDocument.Size = new System.Drawing.Size(501, 382);
			this.tctlDocument.TabIndex = 13;
			// 
			// tpgDecisionTreeEditorDocument
			// 
			this.tpgDecisionTreeEditorDocument.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.tpgDecisionTreeEditorDocument.Controls.Add(this.nodeControl);
			this.tpgDecisionTreeEditorDocument.Location = new System.Drawing.Point(4, 30);
			this.tpgDecisionTreeEditorDocument.Margin = new System.Windows.Forms.Padding(0);
			this.tpgDecisionTreeEditorDocument.Name = "tpgDecisionTreeEditorDocument";
			this.tpgDecisionTreeEditorDocument.Size = new System.Drawing.Size(493, 348);
			this.tpgDecisionTreeEditorDocument.TabIndex = 0;
			this.tpgDecisionTreeEditorDocument.Text = "Decision";
			// 
			// nodeControl
			// 
			this.nodeControl.AutoScroll = true;
			this.nodeControl.AutoScrollMargin = new System.Drawing.Size(21, 21);
			this.nodeControl.AutoScrollMinSize = new System.Drawing.Size(32, 32);
			this.nodeControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.nodeControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nodeControl.DrawingScale = new System.Drawing.SizeF(1F, 1F);
			this.nodeControl.EventsEnabled = true;
			this.nodeControl.ForeColor = System.Drawing.Color.White;
			this.nodeControl.Location = new System.Drawing.Point(0, 0);
			this.nodeControl.Margin = new System.Windows.Forms.Padding(0);
			this.nodeControl.Name = "nodeControl";
			this.nodeControl.NeedsInvalidate = true;
			this.nodeControl.NodeBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(155)))), ((int)(((byte)(236)))));
			nodeFileItem1.Description = "";
			nodeFileItem1.Name = "";
			nodeFileItem1.Ticket = "633c447a-e1c7-4e83-b54a-2845dfa5a5b4";
			this.nodeControl.NodeFile = nodeFileItem1;
			this.nodeControl.NodeLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(77)))), ((int)(((byte)(149)))));
			this.nodeControl.NodeLineWidth = 4;
			this.nodeControl.NodeMaxWidth = 256;
			this.nodeControl.NodeSelectColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(173)))), ((int)(((byte)(24)))));
			this.nodeControl.Size = new System.Drawing.Size(493, 348);
			this.nodeControl.SocketConnectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(227)))), ((int)(((byte)(24)))));
			this.nodeControl.SocketConnectionLineWidth = 4;
			this.nodeControl.SocketDragColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(103)))), ((int)(((byte)(107)))));
			this.nodeControl.SocketFontSize = 8F;
			this.nodeControl.SocketTextColor = System.Drawing.Color.White;
			this.nodeControl.TabIndex = 6;
			this.nodeControl.TitleFontSize = 12F;
			this.nodeControl.TitleTextColor = System.Drawing.Color.White;
			this.nodeControl.CursorMessage += new Scaffold.MessageEventHandler(this.nodeControl_CursorMessage);
			this.nodeControl.EditorMessage += new Scaffold.MessageEventHandler(this.nodeControl_EditorMessage);
			this.nodeControl.Click += new System.EventHandler(this.nodeControl_Click);
			// 
			// tpgSlideEditorDocument
			// 
			this.tpgSlideEditorDocument.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.tpgSlideEditorDocument.Controls.Add(this.skControl);
			this.tpgSlideEditorDocument.Location = new System.Drawing.Point(4, 30);
			this.tpgSlideEditorDocument.Margin = new System.Windows.Forms.Padding(0);
			this.tpgSlideEditorDocument.Name = "tpgSlideEditorDocument";
			this.tpgSlideEditorDocument.Size = new System.Drawing.Size(493, 348);
			this.tpgSlideEditorDocument.TabIndex = 1;
			this.tpgSlideEditorDocument.Text = "Slide";
			// 
			// skControl
			// 
			this.skControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.skControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.skControl.Location = new System.Drawing.Point(0, 0);
			this.skControl.Margin = new System.Windows.Forms.Padding(0);
			this.skControl.Name = "skControl";
			this.skControl.Size = new System.Drawing.Size(493, 348);
			this.skControl.TabIndex = 0;
			this.skControl.Text = "skControl";
			// 
			// tpgHTML
			// 
			this.tpgHTML.Location = new System.Drawing.Point(4, 30);
			this.tpgHTML.Margin = new System.Windows.Forms.Padding(0);
			this.tpgHTML.Name = "tpgHTML";
			this.tpgHTML.Size = new System.Drawing.Size(493, 348);
			this.tpgHTML.TabIndex = 2;
			this.tpgHTML.Text = "HTML";
			this.tpgHTML.UseVisualStyleBackColor = true;
			// 
			// tctlTools
			// 
			this.tctlTools.Controls.Add(this.tpgDecisionTreeEditorTools);
			this.tctlTools.Controls.Add(this.tpgSlideEditorTools);
			this.tctlTools.Controls.Add(this.tpgHTMLTools);
			this.tctlTools.Location = new System.Drawing.Point(0, 66);
			this.tctlTools.Margin = new System.Windows.Forms.Padding(0);
			this.tctlTools.Multiline = true;
			this.tctlTools.Name = "tctlTools";
			this.tctlTools.Padding = new System.Drawing.Point(0, 0);
			this.tctlTools.SelectedIndex = 0;
			this.tctlTools.SelectedTabIndex = 0;
			this.tctlTools.SelectedTabName = "Decision";
			this.tctlTools.ShowTabs = true;
			this.tctlTools.Size = new System.Drawing.Size(197, 389);
			this.tctlTools.TabIndex = 12;
			// 
			// tpgDecisionTreeEditorTools
			// 
			this.tpgDecisionTreeEditorTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.tpgDecisionTreeEditorTools.Controls.Add(this.pnlDecisionTools);
			this.tpgDecisionTreeEditorTools.Location = new System.Drawing.Point(4, 56);
			this.tpgDecisionTreeEditorTools.Margin = new System.Windows.Forms.Padding(0);
			this.tpgDecisionTreeEditorTools.Name = "tpgDecisionTreeEditorTools";
			this.tpgDecisionTreeEditorTools.Size = new System.Drawing.Size(189, 329);
			this.tpgDecisionTreeEditorTools.TabIndex = 0;
			this.tpgDecisionTreeEditorTools.Text = "Decision";
			// 
			// pnlDecisionTools
			// 
			this.pnlDecisionTools.AutoScroll = true;
			this.pnlDecisionTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.pnlDecisionTools.Controls.Add(this.toolDecisionPicTermination);
			this.pnlDecisionTools.Controls.Add(this.toolDecisionPicDelay);
			this.pnlDecisionTools.Controls.Add(this.toolDecisionPicFork);
			this.pnlDecisionTools.Controls.Add(this.toolDecisionPicStart);
			this.pnlDecisionTools.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlDecisionTools.Location = new System.Drawing.Point(0, 0);
			this.pnlDecisionTools.Margin = new System.Windows.Forms.Padding(0);
			this.pnlDecisionTools.Name = "pnlDecisionTools";
			this.pnlDecisionTools.Size = new System.Drawing.Size(189, 329);
			this.pnlDecisionTools.TabIndex = 4;
			this.pnlDecisionTools.Click += new System.EventHandler(this.pnlTools_Click);
			// 
			// toolDecisionPicTermination
			// 
			this.toolDecisionPicTermination.Image = global::Scaffold.ResourceMain.NodeTermination;
			this.toolDecisionPicTermination.Location = new System.Drawing.Point(9, 329);
			this.toolDecisionPicTermination.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.toolDecisionPicTermination.Name = "toolDecisionPicTermination";
			this.toolDecisionPicTermination.Size = new System.Drawing.Size(163, 97);
			this.toolDecisionPicTermination.TabIndex = 0;
			this.toolDecisionPicTermination.TabStop = false;
			this.toolDecisionPicTermination.Click += new System.EventHandler(this.toolDecisionPicTermination_Click);
			// 
			// toolDecisionPicDelay
			// 
			this.toolDecisionPicDelay.Image = global::Scaffold.ResourceMain.NodeDelay;
			this.toolDecisionPicDelay.Location = new System.Drawing.Point(9, 228);
			this.toolDecisionPicDelay.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.toolDecisionPicDelay.Name = "toolDecisionPicDelay";
			this.toolDecisionPicDelay.Size = new System.Drawing.Size(163, 97);
			this.toolDecisionPicDelay.TabIndex = 0;
			this.toolDecisionPicDelay.TabStop = false;
			this.toolDecisionPicDelay.Click += new System.EventHandler(this.toolDecisionPicDelay_Click);
			// 
			// toolDecisionPicFork
			// 
			this.toolDecisionPicFork.Image = global::Scaffold.ResourceMain.NodeFork;
			this.toolDecisionPicFork.Location = new System.Drawing.Point(9, 127);
			this.toolDecisionPicFork.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.toolDecisionPicFork.Name = "toolDecisionPicFork";
			this.toolDecisionPicFork.Size = new System.Drawing.Size(163, 97);
			this.toolDecisionPicFork.TabIndex = 0;
			this.toolDecisionPicFork.TabStop = false;
			this.toolDecisionPicFork.Click += new System.EventHandler(this.toolDecisionPicFork_Click);
			// 
			// toolDecisionPicStart
			// 
			this.toolDecisionPicStart.Image = global::Scaffold.ResourceMain.NodeStart;
			this.toolDecisionPicStart.Location = new System.Drawing.Point(9, 26);
			this.toolDecisionPicStart.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.toolDecisionPicStart.Name = "toolDecisionPicStart";
			this.toolDecisionPicStart.Size = new System.Drawing.Size(163, 97);
			this.toolDecisionPicStart.TabIndex = 0;
			this.toolDecisionPicStart.TabStop = false;
			this.toolDecisionPicStart.Click += new System.EventHandler(this.toolDecisionPicStart_Click);
			// 
			// tpgSlideEditorTools
			// 
			this.tpgSlideEditorTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.tpgSlideEditorTools.Controls.Add(this.pnlSlideTools);
			this.tpgSlideEditorTools.Location = new System.Drawing.Point(4, 56);
			this.tpgSlideEditorTools.Margin = new System.Windows.Forms.Padding(0);
			this.tpgSlideEditorTools.Name = "tpgSlideEditorTools";
			this.tpgSlideEditorTools.Size = new System.Drawing.Size(189, 329);
			this.tpgSlideEditorTools.TabIndex = 1;
			this.tpgSlideEditorTools.Text = "Slide";
			// 
			// pnlSlideTools
			// 
			this.pnlSlideTools.AutoScroll = true;
			this.pnlSlideTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.pnlSlideTools.Controls.Add(this.toolSlidePicText);
			this.pnlSlideTools.Controls.Add(this.toolSlidePicPolyline);
			this.pnlSlideTools.Controls.Add(this.toolSlidePicLine);
			this.pnlSlideTools.Controls.Add(this.toolSlidePicEllipse);
			this.pnlSlideTools.Controls.Add(this.toolSlidePicRectangle);
			this.pnlSlideTools.Controls.Add(this.toolSlidePicCursor);
			this.pnlSlideTools.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlSlideTools.Location = new System.Drawing.Point(0, 0);
			this.pnlSlideTools.Margin = new System.Windows.Forms.Padding(0);
			this.pnlSlideTools.Name = "pnlSlideTools";
			this.pnlSlideTools.Size = new System.Drawing.Size(189, 329);
			this.pnlSlideTools.TabIndex = 5;
			this.pnlSlideTools.Resize += new System.EventHandler(this.pnlSlideTools_Resize);
			// 
			// toolSlidePicText
			// 
			this.toolSlidePicText.Image = global::Scaffold.ResourceMain.DrawToolText;
			this.toolSlidePicText.Location = new System.Drawing.Point(4, 206);
			this.toolSlidePicText.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.toolSlidePicText.Name = "toolSlidePicText";
			this.toolSlidePicText.Size = new System.Drawing.Size(64, 64);
			this.toolSlidePicText.TabIndex = 1;
			this.toolSlidePicText.TabStop = false;
			// 
			// toolSlidePicPolyline
			// 
			this.toolSlidePicPolyline.Image = global::Scaffold.ResourceMain.DrawToolPolyline;
			this.toolSlidePicPolyline.Location = new System.Drawing.Point(76, 138);
			this.toolSlidePicPolyline.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.toolSlidePicPolyline.Name = "toolSlidePicPolyline";
			this.toolSlidePicPolyline.Size = new System.Drawing.Size(64, 64);
			this.toolSlidePicPolyline.TabIndex = 1;
			this.toolSlidePicPolyline.TabStop = false;
			// 
			// toolSlidePicLine
			// 
			this.toolSlidePicLine.Image = global::Scaffold.ResourceMain.DrawToolLine;
			this.toolSlidePicLine.Location = new System.Drawing.Point(4, 138);
			this.toolSlidePicLine.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.toolSlidePicLine.Name = "toolSlidePicLine";
			this.toolSlidePicLine.Size = new System.Drawing.Size(64, 64);
			this.toolSlidePicLine.TabIndex = 1;
			this.toolSlidePicLine.TabStop = false;
			// 
			// toolSlidePicEllipse
			// 
			this.toolSlidePicEllipse.Image = global::Scaffold.ResourceMain.DrawToolCircle;
			this.toolSlidePicEllipse.Location = new System.Drawing.Point(76, 70);
			this.toolSlidePicEllipse.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.toolSlidePicEllipse.Name = "toolSlidePicEllipse";
			this.toolSlidePicEllipse.Size = new System.Drawing.Size(64, 64);
			this.toolSlidePicEllipse.TabIndex = 1;
			this.toolSlidePicEllipse.TabStop = false;
			// 
			// toolSlidePicRectangle
			// 
			this.toolSlidePicRectangle.Image = global::Scaffold.ResourceMain.DrawToolRectangle;
			this.toolSlidePicRectangle.Location = new System.Drawing.Point(4, 70);
			this.toolSlidePicRectangle.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.toolSlidePicRectangle.Name = "toolSlidePicRectangle";
			this.toolSlidePicRectangle.Size = new System.Drawing.Size(64, 64);
			this.toolSlidePicRectangle.TabIndex = 1;
			this.toolSlidePicRectangle.TabStop = false;
			// 
			// toolSlidePicCursor
			// 
			this.toolSlidePicCursor.Image = global::Scaffold.ResourceMain.DrawToolCursor;
			this.toolSlidePicCursor.Location = new System.Drawing.Point(4, 2);
			this.toolSlidePicCursor.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.toolSlidePicCursor.Name = "toolSlidePicCursor";
			this.toolSlidePicCursor.Size = new System.Drawing.Size(64, 64);
			this.toolSlidePicCursor.TabIndex = 1;
			this.toolSlidePicCursor.TabStop = false;
			// 
			// tpgHTMLTools
			// 
			this.tpgHTMLTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.tpgHTMLTools.Location = new System.Drawing.Point(4, 56);
			this.tpgHTMLTools.Margin = new System.Windows.Forms.Padding(0);
			this.tpgHTMLTools.Name = "tpgHTMLTools";
			this.tpgHTMLTools.Size = new System.Drawing.Size(189, 329);
			this.tpgHTMLTools.TabIndex = 2;
			this.tpgHTMLTools.Text = "HTML";
			// 
			// btnHTML
			// 
			this.btnHTML.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.btnHTML.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnHTML.Location = new System.Drawing.Point(225, 30);
			this.btnHTML.Manager = this.mainFormButtonCollection;
			this.btnHTML.Name = "btnHTML";
			this.btnHTML.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
			this.btnHTML.Selected = false;
			this.btnHTML.SelectedBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.btnHTML.SelectedTextColor = System.Drawing.Color.White;
			this.btnHTML.Size = new System.Drawing.Size(48, 30);
			this.btnHTML.TabIndex = 9;
			this.btnHTML.Text = "HTML";
			this.btnHTML.SelectedChanged += new System.EventHandler(this.btnHTML_SelectedChanged);
			// 
			// mainFormButtonCollection
			// 
			this.mainFormButtonCollection.SelectionMode = System.Windows.Forms.SelectionMode.One;
			// 
			// btnSlideEditor
			// 
			this.btnSlideEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.btnSlideEditor.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSlideEditor.Location = new System.Drawing.Point(130, 30);
			this.btnSlideEditor.Manager = this.mainFormButtonCollection;
			this.btnSlideEditor.Name = "btnSlideEditor";
			this.btnSlideEditor.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
			this.btnSlideEditor.Selected = false;
			this.btnSlideEditor.SelectedBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.btnSlideEditor.SelectedTextColor = System.Drawing.Color.White;
			this.btnSlideEditor.Size = new System.Drawing.Size(89, 30);
			this.btnSlideEditor.TabIndex = 9;
			this.btnSlideEditor.Text = "Slide Editor";
			this.btnSlideEditor.SelectedChanged += new System.EventHandler(this.btnSlideEditor_SelectedChanged);
			// 
			// btnDecisionTreeEditor
			// 
			this.btnDecisionTreeEditor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.btnDecisionTreeEditor.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnDecisionTreeEditor.ForeColor = System.Drawing.Color.Gainsboro;
			this.btnDecisionTreeEditor.Location = new System.Drawing.Point(12, 30);
			this.btnDecisionTreeEditor.Manager = this.mainFormButtonCollection;
			this.btnDecisionTreeEditor.Name = "btnDecisionTreeEditor";
			this.btnDecisionTreeEditor.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
			this.btnDecisionTreeEditor.Selected = true;
			this.btnDecisionTreeEditor.SelectedBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.btnDecisionTreeEditor.SelectedTextColor = System.Drawing.Color.White;
			this.btnDecisionTreeEditor.Size = new System.Drawing.Size(102, 30);
			this.btnDecisionTreeEditor.TabIndex = 9;
			this.btnDecisionTreeEditor.Text = "Decision Tree";
			this.btnDecisionTreeEditor.SelectedChanged += new System.EventHandler(this.btnDecisionTreeEditor_SelectedChanged);
			// 
			// panelWindowControl
			// 
			this.panelWindowControl.AssociationListBottom = "";
			this.panelWindowControl.AssociationListCenter = "{\"Name\":\"Center\",\"AutoSize\":false,\"Controls\":[{\"Name\":\"tctlDocument\",\"Dock\":\"Fill" +
    "\"}]}";
			this.panelWindowControl.AssociationListLeft = "{\"Name\":\"Left\",\"AutoSize\":true,\"Margin\":6,\"Controls\":[{\"Name\":\"tctlTools\",\"Dock\":" +
    "\"Fill\"}]}";
			this.panelWindowControl.AssociationListRight = "";
			this.panelWindowControl.AssociationListTop = resources.GetString("panelWindowControl.AssociationListTop");
			this.panelWindowControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.panelWindowControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelWindowControl.Location = new System.Drawing.Point(0, 28);
			this.panelWindowControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.panelWindowControl.Name = "panelWindowControl";
			this.panelWindowControl.PanelBottom = 100;
			this.panelWindowControl.PanelLeft = 200;
			this.panelWindowControl.PanelRight = 100;
			this.panelWindowControl.PanelTop = 32;
			this.panelWindowControl.Size = new System.Drawing.Size(900, 536);
			this.panelWindowControl.TabIndex = 11;
			// 
			// timerAutoSave
			// 
			this.timerAutoSave.Enabled = true;
			this.timerAutoSave.Interval = 300000;
			this.timerAutoSave.Tick += new System.EventHandler(this.timerAutoSave_Tick);
			// 
			// mnuEditFind
			// 
			this.mnuEditFind.Name = "mnuEditFind";
			this.mnuEditFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
			this.mnuEditFind.Size = new System.Drawing.Size(264, 26);
			this.mnuEditFind.Text = "&Find";
			this.mnuEditFind.Click += new System.EventHandler(this.mnuEditFind_Click);
			// 
			// mnuEditSep3
			// 
			this.mnuEditSep3.Name = "mnuEditSep3";
			this.mnuEditSep3.Size = new System.Drawing.Size(261, 6);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(900, 590);
			this.Controls.Add(this.tctlDocument);
			this.Controls.Add(this.tctlTools);
			this.Controls.Add(this.btnHTML);
			this.Controls.Add(this.btnSlideEditor);
			this.Controls.Add(this.btnDecisionTreeEditor);
			this.Controls.Add(this.panelWindowControl);
			this.Controls.Add(this.statusMain);
			this.Controls.Add(this.menuMain);
			this.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuMain;
			this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
			this.Name = "frmMain";
			this.Text = "Scaffold - Decision Tree Editor";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.noDrop_MouseMove);
			this.menuMain.ResumeLayout(false);
			this.menuMain.PerformLayout();
			this.statusMain.ResumeLayout(false);
			this.statusMain.PerformLayout();
			this.tctlDocument.ResumeLayout(false);
			this.tpgDecisionTreeEditorDocument.ResumeLayout(false);
			this.tpgSlideEditorDocument.ResumeLayout(false);
			this.tctlTools.ResumeLayout(false);
			this.tpgDecisionTreeEditorTools.ResumeLayout(false);
			this.pnlDecisionTools.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.toolDecisionPicTermination)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.toolDecisionPicDelay)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.toolDecisionPicFork)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.toolDecisionPicStart)).EndInit();
			this.tpgSlideEditorTools.ResumeLayout(false);
			this.pnlSlideTools.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.toolSlidePicText)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.toolSlidePicPolyline)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.toolSlidePicLine)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.toolSlidePicEllipse)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.toolSlidePicRectangle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.toolSlidePicCursor)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private System.Windows.Forms.MenuStrip menuMain;
		private System.Windows.Forms.ToolStripMenuItem mnuFile;
		private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
		private System.Windows.Forms.StatusStrip statusMain;
		private System.Windows.Forms.ToolStripStatusLabel statMessage;
		private System.Windows.Forms.ToolStripMenuItem mnuFileConvert;
		private System.Windows.Forms.ToolStripMenuItem mnuFileConvertPPToHTML;
		private System.Windows.Forms.ToolStripMenuItem mnuFileConvertPPToTinyLMS;
		private System.Windows.Forms.ToolStripMenuItem mnuFileExport;
		private System.Windows.Forms.ToolStripMenuItem mnuFileExportDecisionTreeToPP;
		private System.Windows.Forms.ToolStripSeparator mnuFileSep1;
		private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
		private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
		private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAs;
		private System.Windows.Forms.ToolStripSeparator mnuFileSep2;
		private System.Windows.Forms.ToolStripMenuItem mnuView;
		private System.Windows.Forms.ToolStripMenuItem mnuViewZoom;
		private System.Windows.Forms.ToolStripMenuItem mnuViewZoomIn;
		private System.Windows.Forms.ToolStripMenuItem mnuViewZoomOut;
		private System.Windows.Forms.ToolStripMenuItem mnuViewZoom100;
		private System.Windows.Forms.ToolStripMenuItem mnuTools;
		private System.Windows.Forms.ToolStripMenuItem mnuToolsColorPalette;
		private System.Windows.Forms.ToolStripMenuItem mnuToolsClipboard;
		private System.Windows.Forms.ToolStripMenuItem mnuToolsClipboardSave;
		private System.Windows.Forms.ToolStripMenuItem mnuToolsClipboardLoad;
		private System.Windows.Forms.ToolStripSeparator mnuToolsClipboardSep1;
		private System.Windows.Forms.ToolStripMenuItem mnuToolsClipboardClear;
		private System.Windows.Forms.ToolStripMenuItem mnuToolsBase64;
		private System.Windows.Forms.ToolStripMenuItem mnuToolsBase64SRC;
		private System.Windows.Forms.ToolStripMenuItem mnuToolsBase64URL;
		private System.Windows.Forms.ToolStripMenuItem mnuToolsBase64Raw;
		private System.Windows.Forms.ToolStripStatusLabel statSep1;
		private System.Windows.Forms.ToolStripStatusLabel statCursor;
		private System.Windows.Forms.ToolStripStatusLabel statSep2;
		private System.Windows.Forms.ToolStripStatusLabel statEditor;
		private LabelButtonControl btnDecisionTreeEditor;
		private LabelButtonControl btnSlideEditor;
		private LabelButtonControlCollection mainFormButtonCollection;
		private PanelWindowControl panelWindowControl;
		private WizardTabControl tctlTools;
		private TabPage tpgDecisionTreeEditorTools;
		private TabPage tpgSlideEditorTools;
		private Panel pnlDecisionTools;
		private PictureBox toolDecisionPicTermination;
		private PictureBox toolDecisionPicFork;
		private PictureBox toolDecisionPicStart;
		private Timer timerDrag;
		private WizardTabControl tctlDocument;
		private TabPage tpgDecisionTreeEditorDocument;
		private TabPage tpgSlideEditorDocument;
		private NodeControl nodeControl;
		private SkiaSharp.Views.Desktop.SKControl skControl;
		private ToolStripMenuItem mnuWindow;
		private ToolStripMenuItem mnuWindowDecision;
		private ToolStripMenuItem mnuWindowSlide;
		private Panel pnlSlideTools;
		private PictureBox toolSlidePicText;
		private PictureBox toolSlidePicPolyline;
		private PictureBox toolSlidePicLine;
		private PictureBox toolSlidePicEllipse;
		private PictureBox toolSlidePicRectangle;
		private PictureBox toolSlidePicCursor;
		private ToolStripSeparator toolStripMenuItem1;
		private ToolStripMenuItem mnuToolsAnimation;
		private ToolStripMenuItem mnuToolsAnimationTimelineFileReport;
		private TabPage tpgHTMLTools;
		private TabPage tpgHTML;
		private LabelButtonControl btnHTML;
		private ToolStripMenuItem mnuWindowHTMLViewer;
		private ToolStripMenuItem mnuToolsAnimationFrameNToHTML;
		private ToolStripMenuItem mnuToolsBase64U;
		private ToolStripMenuItem mnuToolsBase64UClipboard;
		private ToolStripMenuItem mnuToolsBase64UFile;
		private ToolStripProgressBar statProg;
		private ToolStripMenuItem mnuToolsAnimationFrameNToSVG;
		private ToolStripMenuItem mnuToolsAnimationSaveFrames;
		private ToolStripMenuItem mnuEdit;
		private PictureBox toolDecisionPicDelay;
		private ToolStripMenuItem mnuEditNode;
		private ToolStripMenuItem mnuEditNodeColor;
		private ToolStripMenuItem mnuEditNodeDuplicate;
		private ToolStripMenuItem mnuEditNodeColorText;
		private Timer timerAutoSave;
		private ToolStripMenuItem mnuEditUndo;
		private ToolStripMenuItem mnuToolsControl;
		private ToolStripMenuItem mnuToolsControlUndo;
		private ToolStripMenuItem mnuEditSelectAll;
		private ToolStripSeparator mnuEditSep1;
		private ToolStripSeparator mnuEditNodeSep1;
		private ToolStripMenuItem mnuEditNodeAddAudio;
		private ToolStripMenuItem mnuEditNodeAddImage;
		private ToolStripMenuItem mnuEditNodeAddLink;
		private ToolStripMenuItem mnuEditNodeAddVideo;
		private ToolStripMenuItem mnuEditSelectNone;
		private ToolStripSeparator mnuEditSep2;
		private ToolStripMenuItem mnuFileNew;
		private ToolStripMenuItem mnuToolsControlNodeControl;
		private ToolStripMenuItem mnuToolsControlNodeMeasurement;
		private ToolStripSeparator mnuEditNodeSep2;
		private ToolStripMenuItem mnuEditNodeRemoveAudio;
		private ToolStripMenuItem mnuEditNodeRemoveImage;
		private ToolStripMenuItem mnuEditNodeRemoveLink;
		private ToolStripMenuItem mnuEditNodeRemoveVideo;
		private ToolStripMenuItem mnuEditAlign;
		private ToolStripMenuItem mnuEditAlignLeft;
		private ToolStripMenuItem mnuEditAlignCenter;
		private ToolStripMenuItem mnuEditAlignRight;
		private ToolStripMenuItem mnuEditAlignTop;
		private ToolStripMenuItem mnuEditAlignMiddle;
		private ToolStripMenuItem mnuEditAlignBottom;
		private ToolStripSeparator mnuEditAlignSep1;
		private ToolStripMenuItem mnuEditAlignHorizontal;
		private ToolStripMenuItem mnuEditAlignVertical;
		private ToolStripMenuItem mnuToolsChatbotEmulator;
		private ToolStripMenuItem mnuEditNodeAddResources;
		private ToolStripMenuItem mnuToolsResourceGallery;
		private ToolStripMenuItem mnuFilePublish;
		private ToolStripMenuItem mnuFilePublishSlackChatConversation;
		private ToolStripMenuItem mnuFileDocumentProperties;
		private ToolStripSeparator mnuFileSep3;
		private ToolStripMenuItem mnuToolsChatbotEmulateBeginning;
		private ToolStripMenuItem mnuToolsChatbotEmulateSelected;
		private ToolStripMenuItem mnuViewScroll;
		private ToolStripMenuItem mnuViewScrollLayout;
		private ToolStripMenuItem mnuViewScrollNode;
		private ToolStripMenuItem mnuEditFind;
		private ToolStripSeparator mnuEditSep3;
	}
}

