//	frmSingleAnswer.Designer.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
namespace Scaffold
{
	partial class frmSingleAnswer
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tctl = new System.Windows.Forms.TabControl();
			this.tabSocket = new System.Windows.Forms.TabPage();
			this.txtAnswer = new System.Windows.Forms.TextBox();
			this.lblAnswer = new System.Windows.Forms.Label();
			this.txtIndex = new System.Windows.Forms.TextBox();
			this.lblIndex = new System.Windows.Forms.Label();
			this.tabMedia = new System.Windows.Forms.TabPage();
			this.btnMediaDelete = new System.Windows.Forms.Button();
			this.btnMediaAdd = new System.Windows.Forms.Button();
			this.lvMedia = new System.Windows.Forms.ListView();
			this.imageListMedia = new System.Windows.Forms.ImageList(this.components);
			this.tabStoryboard = new System.Windows.Forms.TabPage();
			this.btnStoryFont = new System.Windows.Forms.Button();
			this.lblStoryFont = new System.Windows.Forms.Label();
			this.grpStoryColors = new System.Windows.Forms.GroupBox();
			this.btnStoryTextColor = new System.Windows.Forms.Button();
			this.lblStoryTextColor = new System.Windows.Forms.Label();
			this.btnStoryLineColor = new System.Windows.Forms.Button();
			this.lblStoryLineColor = new System.Windows.Forms.Label();
			this.btnStoryFillColor = new System.Windows.Forms.Button();
			this.lblStoryFillColor = new System.Windows.Forms.Label();
			this.cmboStoryboardShapeType = new System.Windows.Forms.ComboBox();
			this.lblStoryboardShapeType = new System.Windows.Forms.Label();
			this.grpStoryPageLocation = new System.Windows.Forms.GroupBox();
			this.cmboStoryFromY = new System.Windows.Forms.ComboBox();
			this.cmboStoryFromX = new System.Windows.Forms.ComboBox();
			this.txtStoryWidth = new System.Windows.Forms.TextBox();
			this.lblStoryWidth = new System.Windows.Forms.Label();
			this.lblStoryFromY = new System.Windows.Forms.Label();
			this.lblStoryFromX = new System.Windows.Forms.Label();
			this.txtStoryPageNumber = new System.Windows.Forms.TextBox();
			this.lblStoryPageNumber = new System.Windows.Forms.Label();
			this.txtStoryPageY = new System.Windows.Forms.TextBox();
			this.lblStoryPageY = new System.Windows.Forms.Label();
			this.txtStoryPageX = new System.Windows.Forms.TextBox();
			this.lblStoryPageX = new System.Windows.Forms.Label();
			this.tabProperties = new System.Windows.Forms.TabPage();
			this.btnPropertiesEdit = new System.Windows.Forms.Button();
			this.btnPropertiesDelete = new System.Windows.Forms.Button();
			this.btnPropertiesAdd = new System.Windows.Forms.Button();
			this.grdProperties = new System.Windows.Forms.DataGridView();
			this.mnuSingleAnswer = new System.Windows.Forms.MenuStrip();
			this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditMedia = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditMediaAdd = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditMediaDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewSocketPage = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewMedia = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewStoryboardPage = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewPropertiesPage = new System.Windows.Forms.ToolStripMenuItem();
			this.tctl.SuspendLayout();
			this.tabSocket.SuspendLayout();
			this.tabMedia.SuspendLayout();
			this.tabStoryboard.SuspendLayout();
			this.grpStoryColors.SuspendLayout();
			this.grpStoryPageLocation.SuspendLayout();
			this.tabProperties.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdProperties)).BeginInit();
			this.mnuSingleAnswer.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(466, 444);
			this.btnOK.Margin = new System.Windows.Forms.Padding(4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(85, 54);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "&OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(371, 444);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(85, 54);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// tctl
			// 
			this.tctl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tctl.Controls.Add(this.tabSocket);
			this.tctl.Controls.Add(this.tabMedia);
			this.tctl.Controls.Add(this.tabStoryboard);
			this.tctl.Controls.Add(this.tabProperties);
			this.tctl.Location = new System.Drawing.Point(12, 31);
			this.tctl.Name = "tctl";
			this.tctl.SelectedIndex = 0;
			this.tctl.Size = new System.Drawing.Size(539, 406);
			this.tctl.TabIndex = 0;
			// 
			// tabSocket
			// 
			this.tabSocket.Controls.Add(this.txtAnswer);
			this.tabSocket.Controls.Add(this.lblAnswer);
			this.tabSocket.Controls.Add(this.txtIndex);
			this.tabSocket.Controls.Add(this.lblIndex);
			this.tabSocket.Location = new System.Drawing.Point(4, 30);
			this.tabSocket.Name = "tabSocket";
			this.tabSocket.Padding = new System.Windows.Forms.Padding(3);
			this.tabSocket.Size = new System.Drawing.Size(531, 372);
			this.tabSocket.TabIndex = 0;
			this.tabSocket.Text = "Socket";
			this.tabSocket.UseVisualStyleBackColor = true;
			// 
			// txtAnswer
			// 
			this.txtAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAnswer.Location = new System.Drawing.Point(17, 88);
			this.txtAnswer.Margin = new System.Windows.Forms.Padding(4);
			this.txtAnswer.Multiline = true;
			this.txtAnswer.Name = "txtAnswer";
			this.txtAnswer.Size = new System.Drawing.Size(498, 277);
			this.txtAnswer.TabIndex = 3;
			// 
			// lblAnswer
			// 
			this.lblAnswer.AutoSize = true;
			this.lblAnswer.Location = new System.Drawing.Point(13, 61);
			this.lblAnswer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblAnswer.Name = "lblAnswer";
			this.lblAnswer.Size = new System.Drawing.Size(110, 21);
			this.lblAnswer.TabIndex = 2;
			this.lblAnswer.Text = "&Answer Text:";
			// 
			// txtIndex
			// 
			this.txtIndex.Location = new System.Drawing.Point(71, 17);
			this.txtIndex.Margin = new System.Windows.Forms.Padding(4);
			this.txtIndex.Name = "txtIndex";
			this.txtIndex.Size = new System.Drawing.Size(112, 28);
			this.txtIndex.TabIndex = 1;
			// 
			// lblIndex
			// 
			this.lblIndex.AutoSize = true;
			this.lblIndex.Location = new System.Drawing.Point(13, 21);
			this.lblIndex.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblIndex.Name = "lblIndex";
			this.lblIndex.Size = new System.Drawing.Size(57, 21);
			this.lblIndex.TabIndex = 0;
			this.lblIndex.Text = "&Index:";
			// 
			// tabMedia
			// 
			this.tabMedia.Controls.Add(this.btnMediaDelete);
			this.tabMedia.Controls.Add(this.btnMediaAdd);
			this.tabMedia.Controls.Add(this.lvMedia);
			this.tabMedia.Location = new System.Drawing.Point(4, 30);
			this.tabMedia.Name = "tabMedia";
			this.tabMedia.Size = new System.Drawing.Size(531, 372);
			this.tabMedia.TabIndex = 3;
			this.tabMedia.Text = "Media";
			this.tabMedia.UseVisualStyleBackColor = true;
			// 
			// btnMediaDelete
			// 
			this.btnMediaDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMediaDelete.Enabled = false;
			this.btnMediaDelete.Location = new System.Drawing.Point(401, 311);
			this.btnMediaDelete.Name = "btnMediaDelete";
			this.btnMediaDelete.Size = new System.Drawing.Size(122, 47);
			this.btnMediaDelete.TabIndex = 2;
			this.btnMediaDelete.Text = "&Delete Media";
			this.btnMediaDelete.UseVisualStyleBackColor = true;
			this.btnMediaDelete.Click += new System.EventHandler(this.btnMediaDelete_Click);
			// 
			// btnMediaAdd
			// 
			this.btnMediaAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMediaAdd.Location = new System.Drawing.Point(273, 311);
			this.btnMediaAdd.Name = "btnMediaAdd";
			this.btnMediaAdd.Size = new System.Drawing.Size(122, 47);
			this.btnMediaAdd.TabIndex = 1;
			this.btnMediaAdd.Text = "Add &Media";
			this.btnMediaAdd.UseVisualStyleBackColor = true;
			this.btnMediaAdd.Click += new System.EventHandler(this.btnMediaAdd_Click);
			// 
			// lvMedia
			// 
			this.lvMedia.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvMedia.HideSelection = false;
			this.lvMedia.LargeImageList = this.imageListMedia;
			this.lvMedia.Location = new System.Drawing.Point(8, 24);
			this.lvMedia.Name = "lvMedia";
			this.lvMedia.Size = new System.Drawing.Size(515, 281);
			this.lvMedia.TabIndex = 0;
			this.lvMedia.UseCompatibleStateImageBehavior = false;
			this.lvMedia.SelectedIndexChanged += new System.EventHandler(this.lvMedia_SelectedIndexChanged);
			// 
			// imageListMedia
			// 
			this.imageListMedia.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageListMedia.ImageSize = new System.Drawing.Size(128, 128);
			this.imageListMedia.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tabStoryboard
			// 
			this.tabStoryboard.Controls.Add(this.btnStoryFont);
			this.tabStoryboard.Controls.Add(this.lblStoryFont);
			this.tabStoryboard.Controls.Add(this.grpStoryColors);
			this.tabStoryboard.Controls.Add(this.cmboStoryboardShapeType);
			this.tabStoryboard.Controls.Add(this.lblStoryboardShapeType);
			this.tabStoryboard.Controls.Add(this.grpStoryPageLocation);
			this.tabStoryboard.Location = new System.Drawing.Point(4, 30);
			this.tabStoryboard.Name = "tabStoryboard";
			this.tabStoryboard.Padding = new System.Windows.Forms.Padding(3);
			this.tabStoryboard.Size = new System.Drawing.Size(531, 372);
			this.tabStoryboard.TabIndex = 1;
			this.tabStoryboard.Text = "Storyboard";
			this.tabStoryboard.UseVisualStyleBackColor = true;
			// 
			// btnStoryFont
			// 
			this.btnStoryFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStoryFont.AutoSize = true;
			this.btnStoryFont.Location = new System.Drawing.Point(71, 324);
			this.btnStoryFont.Name = "btnStoryFont";
			this.btnStoryFont.Size = new System.Drawing.Size(453, 37);
			this.btnStoryFont.TabIndex = 5;
			this.btnStoryFont.Text = "Tahoma 10pt";
			this.btnStoryFont.UseVisualStyleBackColor = true;
			this.btnStoryFont.Click += new System.EventHandler(this.btnStoryFont_Click);
			// 
			// lblStoryFont
			// 
			this.lblStoryFont.AutoSize = true;
			this.lblStoryFont.Location = new System.Drawing.Point(11, 332);
			this.lblStoryFont.Name = "lblStoryFont";
			this.lblStoryFont.Size = new System.Drawing.Size(49, 21);
			this.lblStoryFont.TabIndex = 4;
			this.lblStoryFont.Text = "F&ont:";
			// 
			// grpStoryColors
			// 
			this.grpStoryColors.Controls.Add(this.btnStoryTextColor);
			this.grpStoryColors.Controls.Add(this.lblStoryTextColor);
			this.grpStoryColors.Controls.Add(this.btnStoryLineColor);
			this.grpStoryColors.Controls.Add(this.lblStoryLineColor);
			this.grpStoryColors.Controls.Add(this.btnStoryFillColor);
			this.grpStoryColors.Controls.Add(this.lblStoryFillColor);
			this.grpStoryColors.Location = new System.Drawing.Point(11, 219);
			this.grpStoryColors.Margin = new System.Windows.Forms.Padding(4);
			this.grpStoryColors.Name = "grpStoryColors";
			this.grpStoryColors.Padding = new System.Windows.Forms.Padding(4);
			this.grpStoryColors.Size = new System.Drawing.Size(513, 96);
			this.grpStoryColors.TabIndex = 3;
			this.grpStoryColors.TabStop = false;
			this.grpStoryColors.Text = "Colors";
			// 
			// btnStoryTextColor
			// 
			this.btnStoryTextColor.BackColor = System.Drawing.Color.Black;
			this.btnStoryTextColor.Location = new System.Drawing.Point(380, 28);
			this.btnStoryTextColor.Name = "btnStoryTextColor";
			this.btnStoryTextColor.Size = new System.Drawing.Size(75, 32);
			this.btnStoryTextColor.TabIndex = 5;
			this.btnStoryTextColor.UseVisualStyleBackColor = false;
			this.btnStoryTextColor.Click += new System.EventHandler(this.btnStoryTextColor_Click);
			// 
			// lblStoryTextColor
			// 
			this.lblStoryTextColor.AutoSize = true;
			this.lblStoryTextColor.Location = new System.Drawing.Point(328, 34);
			this.lblStoryTextColor.Name = "lblStoryTextColor";
			this.lblStoryTextColor.Size = new System.Drawing.Size(49, 21);
			this.lblStoryTextColor.TabIndex = 4;
			this.lblStoryTextColor.Text = "&Text:";
			// 
			// btnStoryLineColor
			// 
			this.btnStoryLineColor.BackColor = System.Drawing.Color.Black;
			this.btnStoryLineColor.Location = new System.Drawing.Point(220, 28);
			this.btnStoryLineColor.Name = "btnStoryLineColor";
			this.btnStoryLineColor.Size = new System.Drawing.Size(75, 32);
			this.btnStoryLineColor.TabIndex = 3;
			this.btnStoryLineColor.UseVisualStyleBackColor = false;
			this.btnStoryLineColor.Click += new System.EventHandler(this.btnStoryLineColor_Click);
			// 
			// lblStoryLineColor
			// 
			this.lblStoryLineColor.AutoSize = true;
			this.lblStoryLineColor.Location = new System.Drawing.Point(168, 34);
			this.lblStoryLineColor.Name = "lblStoryLineColor";
			this.lblStoryLineColor.Size = new System.Drawing.Size(46, 21);
			this.lblStoryLineColor.TabIndex = 2;
			this.lblStoryLineColor.Text = "Li&ne:";
			// 
			// btnStoryFillColor
			// 
			this.btnStoryFillColor.BackColor = System.Drawing.Color.White;
			this.btnStoryFillColor.Location = new System.Drawing.Point(60, 28);
			this.btnStoryFillColor.Name = "btnStoryFillColor";
			this.btnStoryFillColor.Size = new System.Drawing.Size(75, 32);
			this.btnStoryFillColor.TabIndex = 1;
			this.btnStoryFillColor.UseVisualStyleBackColor = false;
			this.btnStoryFillColor.Click += new System.EventHandler(this.btnStoryFillColor_Click);
			// 
			// lblStoryFillColor
			// 
			this.lblStoryFillColor.AutoSize = true;
			this.lblStoryFillColor.Location = new System.Drawing.Point(17, 34);
			this.lblStoryFillColor.Name = "lblStoryFillColor";
			this.lblStoryFillColor.Size = new System.Drawing.Size(37, 21);
			this.lblStoryFillColor.TabIndex = 0;
			this.lblStoryFillColor.Text = "&Fill:";
			// 
			// cmboStoryboardShapeType
			// 
			this.cmboStoryboardShapeType.FormattingEnabled = true;
			this.cmboStoryboardShapeType.Items.AddRange(new object[] {
            "Callout",
            "Rectangle"});
			this.cmboStoryboardShapeType.Location = new System.Drawing.Point(115, 18);
			this.cmboStoryboardShapeType.Name = "cmboStoryboardShapeType";
			this.cmboStoryboardShapeType.Size = new System.Drawing.Size(409, 29);
			this.cmboStoryboardShapeType.TabIndex = 1;
			// 
			// lblStoryboardShapeType
			// 
			this.lblStoryboardShapeType.AutoSize = true;
			this.lblStoryboardShapeType.Location = new System.Drawing.Point(7, 21);
			this.lblStoryboardShapeType.Name = "lblStoryboardShapeType";
			this.lblStoryboardShapeType.Size = new System.Drawing.Size(102, 21);
			this.lblStoryboardShapeType.TabIndex = 0;
			this.lblStoryboardShapeType.Text = "Shape &Type:";
			// 
			// grpStoryPageLocation
			// 
			this.grpStoryPageLocation.Controls.Add(this.cmboStoryFromY);
			this.grpStoryPageLocation.Controls.Add(this.cmboStoryFromX);
			this.grpStoryPageLocation.Controls.Add(this.txtStoryWidth);
			this.grpStoryPageLocation.Controls.Add(this.lblStoryWidth);
			this.grpStoryPageLocation.Controls.Add(this.lblStoryFromY);
			this.grpStoryPageLocation.Controls.Add(this.lblStoryFromX);
			this.grpStoryPageLocation.Controls.Add(this.txtStoryPageNumber);
			this.grpStoryPageLocation.Controls.Add(this.lblStoryPageNumber);
			this.grpStoryPageLocation.Controls.Add(this.txtStoryPageY);
			this.grpStoryPageLocation.Controls.Add(this.lblStoryPageY);
			this.grpStoryPageLocation.Controls.Add(this.txtStoryPageX);
			this.grpStoryPageLocation.Controls.Add(this.lblStoryPageX);
			this.grpStoryPageLocation.Location = new System.Drawing.Point(11, 64);
			this.grpStoryPageLocation.Margin = new System.Windows.Forms.Padding(4);
			this.grpStoryPageLocation.Name = "grpStoryPageLocation";
			this.grpStoryPageLocation.Padding = new System.Windows.Forms.Padding(4);
			this.grpStoryPageLocation.Size = new System.Drawing.Size(513, 145);
			this.grpStoryPageLocation.TabIndex = 2;
			this.grpStoryPageLocation.TabStop = false;
			this.grpStoryPageLocation.Text = "Storyboard Page Location";
			// 
			// cmboStoryFromY
			// 
			this.cmboStoryFromY.FormattingEnabled = true;
			this.cmboStoryFromY.Items.AddRange(new object[] {
            "Top",
            "Bottom"});
			this.cmboStoryFromY.Location = new System.Drawing.Point(272, 105);
			this.cmboStoryFromY.Name = "cmboStoryFromY";
			this.cmboStoryFromY.Size = new System.Drawing.Size(234, 29);
			this.cmboStoryFromY.TabIndex = 11;
			// 
			// cmboStoryFromX
			// 
			this.cmboStoryFromX.FormattingEnabled = true;
			this.cmboStoryFromX.Items.AddRange(new object[] {
            "Left",
            "Right"});
			this.cmboStoryFromX.Location = new System.Drawing.Point(272, 69);
			this.cmboStoryFromX.Name = "cmboStoryFromX";
			this.cmboStoryFromX.Size = new System.Drawing.Size(234, 29);
			this.cmboStoryFromX.TabIndex = 7;
			// 
			// txtStoryWidth
			// 
			this.txtStoryWidth.Location = new System.Drawing.Point(272, 34);
			this.txtStoryWidth.Name = "txtStoryWidth";
			this.txtStoryWidth.Size = new System.Drawing.Size(112, 28);
			this.txtStoryWidth.TabIndex = 3;
			this.txtStoryWidth.Text = "512";
			// 
			// lblStoryWidth
			// 
			this.lblStoryWidth.AutoSize = true;
			this.lblStoryWidth.Location = new System.Drawing.Point(212, 37);
			this.lblStoryWidth.Name = "lblStoryWidth";
			this.lblStoryWidth.Size = new System.Drawing.Size(59, 21);
			this.lblStoryWidth.TabIndex = 2;
			this.lblStoryWidth.Text = "&Width:";
			// 
			// lblStoryFromY
			// 
			this.lblStoryFromY.AutoSize = true;
			this.lblStoryFromY.Location = new System.Drawing.Point(212, 108);
			this.lblStoryFromY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblStoryFromY.Name = "lblStoryFromY";
			this.lblStoryFromY.Size = new System.Drawing.Size(54, 21);
			this.lblStoryFromY.TabIndex = 10;
			this.lblStoryFromY.Text = "Fro&m:";
			// 
			// lblStoryFromX
			// 
			this.lblStoryFromX.AutoSize = true;
			this.lblStoryFromX.Location = new System.Drawing.Point(212, 72);
			this.lblStoryFromX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblStoryFromX.Name = "lblStoryFromX";
			this.lblStoryFromX.Size = new System.Drawing.Size(54, 21);
			this.lblStoryFromX.TabIndex = 6;
			this.lblStoryFromX.Text = "F&rom:";
			// 
			// txtStoryPageNumber
			// 
			this.txtStoryPageNumber.Location = new System.Drawing.Point(79, 34);
			this.txtStoryPageNumber.Name = "txtStoryPageNumber";
			this.txtStoryPageNumber.Size = new System.Drawing.Size(112, 28);
			this.txtStoryPageNumber.TabIndex = 1;
			this.txtStoryPageNumber.Text = "1";
			// 
			// lblStoryPageNumber
			// 
			this.lblStoryPageNumber.AutoSize = true;
			this.lblStoryPageNumber.Location = new System.Drawing.Point(21, 37);
			this.lblStoryPageNumber.Name = "lblStoryPageNumber";
			this.lblStoryPageNumber.Size = new System.Drawing.Size(52, 21);
			this.lblStoryPageNumber.TabIndex = 0;
			this.lblStoryPageNumber.Text = "&Page:";
			// 
			// txtStoryPageY
			// 
			this.txtStoryPageY.Location = new System.Drawing.Point(79, 105);
			this.txtStoryPageY.Margin = new System.Windows.Forms.Padding(4);
			this.txtStoryPageY.Name = "txtStoryPageY";
			this.txtStoryPageY.Size = new System.Drawing.Size(112, 28);
			this.txtStoryPageY.TabIndex = 9;
			this.txtStoryPageY.Text = "0.000";
			// 
			// lblStoryPageY
			// 
			this.lblStoryPageY.AutoSize = true;
			this.lblStoryPageY.Location = new System.Drawing.Point(21, 108);
			this.lblStoryPageY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblStoryPageY.Name = "lblStoryPageY";
			this.lblStoryPageY.Size = new System.Drawing.Size(26, 21);
			this.lblStoryPageY.TabIndex = 8;
			this.lblStoryPageY.Text = "&Y:";
			// 
			// txtStoryPageX
			// 
			this.txtStoryPageX.Location = new System.Drawing.Point(79, 69);
			this.txtStoryPageX.Margin = new System.Windows.Forms.Padding(4);
			this.txtStoryPageX.Name = "txtStoryPageX";
			this.txtStoryPageX.Size = new System.Drawing.Size(112, 28);
			this.txtStoryPageX.TabIndex = 5;
			this.txtStoryPageX.Text = "0.000";
			// 
			// lblStoryPageX
			// 
			this.lblStoryPageX.AutoSize = true;
			this.lblStoryPageX.Location = new System.Drawing.Point(21, 72);
			this.lblStoryPageX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblStoryPageX.Name = "lblStoryPageX";
			this.lblStoryPageX.Size = new System.Drawing.Size(26, 21);
			this.lblStoryPageX.TabIndex = 4;
			this.lblStoryPageX.Text = "&X:";
			// 
			// tabProperties
			// 
			this.tabProperties.Controls.Add(this.btnPropertiesEdit);
			this.tabProperties.Controls.Add(this.btnPropertiesDelete);
			this.tabProperties.Controls.Add(this.btnPropertiesAdd);
			this.tabProperties.Controls.Add(this.grdProperties);
			this.tabProperties.Location = new System.Drawing.Point(4, 30);
			this.tabProperties.Name = "tabProperties";
			this.tabProperties.Size = new System.Drawing.Size(531, 372);
			this.tabProperties.TabIndex = 2;
			this.tabProperties.Text = "Properties";
			this.tabProperties.UseVisualStyleBackColor = true;
			// 
			// btnPropertiesEdit
			// 
			this.btnPropertiesEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPropertiesEdit.Location = new System.Drawing.Point(408, 72);
			this.btnPropertiesEdit.Margin = new System.Windows.Forms.Padding(4);
			this.btnPropertiesEdit.Name = "btnPropertiesEdit";
			this.btnPropertiesEdit.Size = new System.Drawing.Size(108, 47);
			this.btnPropertiesEdit.TabIndex = 6;
			this.btnPropertiesEdit.Text = "&Edit";
			this.btnPropertiesEdit.UseVisualStyleBackColor = true;
			this.btnPropertiesEdit.Click += new System.EventHandler(this.btnPropertiesEdit_Click);
			// 
			// btnPropertiesDelete
			// 
			this.btnPropertiesDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPropertiesDelete.Location = new System.Drawing.Point(409, 128);
			this.btnPropertiesDelete.Margin = new System.Windows.Forms.Padding(4);
			this.btnPropertiesDelete.Name = "btnPropertiesDelete";
			this.btnPropertiesDelete.Size = new System.Drawing.Size(108, 47);
			this.btnPropertiesDelete.TabIndex = 7;
			this.btnPropertiesDelete.Text = "&Delete";
			this.btnPropertiesDelete.UseVisualStyleBackColor = true;
			this.btnPropertiesDelete.Click += new System.EventHandler(this.btnPropertiesDelete_Click);
			// 
			// btnPropertiesAdd
			// 
			this.btnPropertiesAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPropertiesAdd.Location = new System.Drawing.Point(409, 16);
			this.btnPropertiesAdd.Margin = new System.Windows.Forms.Padding(4);
			this.btnPropertiesAdd.Name = "btnPropertiesAdd";
			this.btnPropertiesAdd.Size = new System.Drawing.Size(108, 47);
			this.btnPropertiesAdd.TabIndex = 5;
			this.btnPropertiesAdd.Text = "&Add";
			this.btnPropertiesAdd.UseVisualStyleBackColor = true;
			this.btnPropertiesAdd.Click += new System.EventHandler(this.btnPropertiesAdd_Click);
			// 
			// grdProperties
			// 
			this.grdProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grdProperties.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.grdProperties.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
			this.grdProperties.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.Navy;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.grdProperties.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.grdProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdProperties.Location = new System.Drawing.Point(4, 16);
			this.grdProperties.Margin = new System.Windows.Forms.Padding(4);
			this.grdProperties.MultiSelect = false;
			this.grdProperties.Name = "grdProperties";
			this.grdProperties.RowHeadersVisible = false;
			this.grdProperties.RowHeadersWidth = 51;
			this.grdProperties.RowTemplate.Height = 24;
			this.grdProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.grdProperties.Size = new System.Drawing.Size(397, 296);
			this.grdProperties.TabIndex = 4;
			this.grdProperties.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.grdProperties_CellBeginEdit);
			this.grdProperties.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdProperties_CellDoubleClick);
			this.grdProperties.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grdProperties_CellFormatting);
			this.grdProperties.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.grdProperties_DataBindingComplete);
			this.grdProperties.SelectionChanged += new System.EventHandler(this.grdProperties_SelectionChanged);
			this.grdProperties.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.grdProperties_UserDeletingRow);
			// 
			// mnuSingleAnswer
			// 
			this.mnuSingleAnswer.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.mnuSingleAnswer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEdit,
            this.mnuView});
			this.mnuSingleAnswer.Location = new System.Drawing.Point(0, 0);
			this.mnuSingleAnswer.Name = "mnuSingleAnswer";
			this.mnuSingleAnswer.Size = new System.Drawing.Size(563, 28);
			this.mnuSingleAnswer.TabIndex = 7;
			this.mnuSingleAnswer.Text = "menuStrip1";
			// 
			// mnuEdit
			// 
			this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditMedia});
			this.mnuEdit.Name = "mnuEdit";
			this.mnuEdit.Size = new System.Drawing.Size(49, 24);
			this.mnuEdit.Text = "&Edit";
			// 
			// mnuEditMedia
			// 
			this.mnuEditMedia.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditMediaAdd,
            this.mnuEditMediaDelete});
			this.mnuEditMedia.Name = "mnuEditMedia";
			this.mnuEditMedia.Size = new System.Drawing.Size(134, 26);
			this.mnuEditMedia.Text = "&Media";
			// 
			// mnuEditMediaAdd
			// 
			this.mnuEditMediaAdd.Name = "mnuEditMediaAdd";
			this.mnuEditMediaAdd.Size = new System.Drawing.Size(197, 26);
			this.mnuEditMediaAdd.Text = "&Add";
			this.mnuEditMediaAdd.Click += new System.EventHandler(this.mnuEditMediaAdd_Click);
			// 
			// mnuEditMediaDelete
			// 
			this.mnuEditMediaDelete.Name = "mnuEditMediaDelete";
			this.mnuEditMediaDelete.Size = new System.Drawing.Size(197, 26);
			this.mnuEditMediaDelete.Text = "&Delete Selected";
			this.mnuEditMediaDelete.Click += new System.EventHandler(this.mnuEditMediaDelete_Click);
			// 
			// mnuView
			// 
			this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewSocketPage,
            this.mnuViewMedia,
            this.mnuViewStoryboardPage,
            this.mnuViewPropertiesPage});
			this.mnuView.Name = "mnuView";
			this.mnuView.Size = new System.Drawing.Size(55, 24);
			this.mnuView.Text = "&View";
			// 
			// mnuViewSocketPage
			// 
			this.mnuViewSocketPage.Name = "mnuViewSocketPage";
			this.mnuViewSocketPage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
			this.mnuViewSocketPage.Size = new System.Drawing.Size(252, 26);
			this.mnuViewSocketPage.Text = "S&ocket Page";
			this.mnuViewSocketPage.Click += new System.EventHandler(this.mnuViewSocketPage_Click);
			// 
			// mnuViewMedia
			// 
			this.mnuViewMedia.Name = "mnuViewMedia";
			this.mnuViewMedia.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
			this.mnuViewMedia.Size = new System.Drawing.Size(252, 26);
			this.mnuViewMedia.Text = "&Media";
			this.mnuViewMedia.Click += new System.EventHandler(this.mnuViewMedia_Click);
			// 
			// mnuViewStoryboardPage
			// 
			this.mnuViewStoryboardPage.Name = "mnuViewStoryboardPage";
			this.mnuViewStoryboardPage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
			this.mnuViewStoryboardPage.Size = new System.Drawing.Size(252, 26);
			this.mnuViewStoryboardPage.Text = "&Storyboard Page";
			this.mnuViewStoryboardPage.Click += new System.EventHandler(this.mnuViewStoryboardPage_Click);
			// 
			// mnuViewPropertiesPage
			// 
			this.mnuViewPropertiesPage.Name = "mnuViewPropertiesPage";
			this.mnuViewPropertiesPage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
			this.mnuViewPropertiesPage.Size = new System.Drawing.Size(252, 26);
			this.mnuViewPropertiesPage.Text = "&Properties Page";
			this.mnuViewPropertiesPage.Click += new System.EventHandler(this.mnuViewPropertiesPage_Click);
			// 
			// frmSingleAnswer
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(563, 507);
			this.Controls.Add(this.tctl);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.mnuSingleAnswer);
			this.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MainMenuStrip = this.mnuSingleAnswer;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "frmSingleAnswer";
			this.Text = "Answer";
			this.tctl.ResumeLayout(false);
			this.tabSocket.ResumeLayout(false);
			this.tabSocket.PerformLayout();
			this.tabMedia.ResumeLayout(false);
			this.tabStoryboard.ResumeLayout(false);
			this.tabStoryboard.PerformLayout();
			this.grpStoryColors.ResumeLayout(false);
			this.grpStoryColors.PerformLayout();
			this.grpStoryPageLocation.ResumeLayout(false);
			this.grpStoryPageLocation.PerformLayout();
			this.tabProperties.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdProperties)).EndInit();
			this.mnuSingleAnswer.ResumeLayout(false);
			this.mnuSingleAnswer.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TabControl tctl;
		private System.Windows.Forms.TabPage tabSocket;
		private System.Windows.Forms.TextBox txtAnswer;
		private System.Windows.Forms.Label lblAnswer;
		private System.Windows.Forms.TextBox txtIndex;
		private System.Windows.Forms.Label lblIndex;
		private System.Windows.Forms.TabPage tabStoryboard;
		private System.Windows.Forms.MenuStrip mnuSingleAnswer;
		private System.Windows.Forms.ToolStripMenuItem mnuView;
		private System.Windows.Forms.ToolStripMenuItem mnuViewSocketPage;
		private System.Windows.Forms.ToolStripMenuItem mnuViewStoryboardPage;
		private System.Windows.Forms.Button btnStoryFont;
		private System.Windows.Forms.Label lblStoryFont;
		private System.Windows.Forms.GroupBox grpStoryColors;
		private System.Windows.Forms.Button btnStoryTextColor;
		private System.Windows.Forms.Label lblStoryTextColor;
		private System.Windows.Forms.Button btnStoryLineColor;
		private System.Windows.Forms.Label lblStoryLineColor;
		private System.Windows.Forms.Button btnStoryFillColor;
		private System.Windows.Forms.Label lblStoryFillColor;
		private System.Windows.Forms.ComboBox cmboStoryboardShapeType;
		private System.Windows.Forms.Label lblStoryboardShapeType;
		private System.Windows.Forms.GroupBox grpStoryPageLocation;
		private System.Windows.Forms.TextBox txtStoryPageNumber;
		private System.Windows.Forms.Label lblStoryPageNumber;
		private System.Windows.Forms.TextBox txtStoryPageY;
		private System.Windows.Forms.Label lblStoryPageY;
		private System.Windows.Forms.TextBox txtStoryPageX;
		private System.Windows.Forms.Label lblStoryPageX;
		private System.Windows.Forms.TabPage tabProperties;
		private System.Windows.Forms.ToolStripMenuItem mnuViewPropertiesPage;
		private System.Windows.Forms.Button btnPropertiesEdit;
		private System.Windows.Forms.Button btnPropertiesDelete;
		private System.Windows.Forms.Button btnPropertiesAdd;
		private System.Windows.Forms.DataGridView grdProperties;
		private System.Windows.Forms.ComboBox cmboStoryFromY;
		private System.Windows.Forms.ComboBox cmboStoryFromX;
		private System.Windows.Forms.TextBox txtStoryWidth;
		private System.Windows.Forms.Label lblStoryWidth;
		private System.Windows.Forms.Label lblStoryFromY;
		private System.Windows.Forms.Label lblStoryFromX;
		private System.Windows.Forms.ImageList imageListMedia;
		private System.Windows.Forms.TabPage tabMedia;
		private System.Windows.Forms.Button btnMediaDelete;
		private System.Windows.Forms.Button btnMediaAdd;
		private System.Windows.Forms.ListView lvMedia;
		private System.Windows.Forms.ToolStripMenuItem mnuEdit;
		private System.Windows.Forms.ToolStripMenuItem mnuEditMedia;
		private System.Windows.Forms.ToolStripMenuItem mnuEditMediaAdd;
		private System.Windows.Forms.ToolStripMenuItem mnuEditMediaDelete;
		private System.Windows.Forms.ToolStripMenuItem mnuViewMedia;
	}
}