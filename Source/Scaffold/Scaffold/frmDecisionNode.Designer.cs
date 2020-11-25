namespace Scaffold
{
	partial class frmDecisionNode
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.tctl = new System.Windows.Forms.TabControl();
			this.tabNode = new System.Windows.Forms.TabPage();
			this.grpType = new System.Windows.Forms.GroupBox();
			this.txtDelay = new System.Windows.Forms.TextBox();
			this.lblDelaySec = new System.Windows.Forms.Label();
			this.lblDelay = new System.Windows.Forms.Label();
			this.cmboType = new System.Windows.Forms.ComboBox();
			this.btnAnswerEdit = new System.Windows.Forms.Button();
			this.btnAnswerDelete = new System.Windows.Forms.Button();
			this.btnAnswerAdd = new System.Windows.Forms.Button();
			this.grdAnswer = new System.Windows.Forms.DataGridView();
			this.lblAnswers = new System.Windows.Forms.Label();
			this.grpLocation = new System.Windows.Forms.GroupBox();
			this.txtY = new System.Windows.Forms.TextBox();
			this.lblY = new System.Windows.Forms.Label();
			this.txtX = new System.Windows.Forms.TextBox();
			this.lblX = new System.Windows.Forms.Label();
			this.txtQuestion = new System.Windows.Forms.TextBox();
			this.lblQuestion = new System.Windows.Forms.Label();
			this.tabMedia = new System.Windows.Forms.TabPage();
			this.lblMediaQuestion = new System.Windows.Forms.Label();
			this.lblMediaResponse = new System.Windows.Forms.Label();
			this.lblMediaPerspective = new System.Windows.Forms.Label();
			this.btnMediaDeleteQuestion = new System.Windows.Forms.Button();
			this.btnMediaDeleteResponse = new System.Windows.Forms.Button();
			this.btnMediaAddQuestion = new System.Windows.Forms.Button();
			this.btnMediaAddResponse = new System.Windows.Forms.Button();
			this.lvMediaQuestion = new System.Windows.Forms.ListView();
			this.imageListMediaQuestion = new System.Windows.Forms.ImageList(this.components);
			this.lvMediaResponse = new System.Windows.Forms.ListView();
			this.imageListMediaResponse = new System.Windows.Forms.ImageList(this.components);
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
			this.txtStoryPageNumber = new System.Windows.Forms.TextBox();
			this.lblStoryPageNumber = new System.Windows.Forms.Label();
			this.txtStoryPageY = new System.Windows.Forms.TextBox();
			this.lblStoryPageY = new System.Windows.Forms.Label();
			this.txtStoryPageX = new System.Windows.Forms.TextBox();
			this.lblStoryFromY = new System.Windows.Forms.Label();
			this.lblStoryFromX = new System.Windows.Forms.Label();
			this.lblStoryPageX = new System.Windows.Forms.Label();
			this.tabProperties = new System.Windows.Forms.TabPage();
			this.btnPropertiesEdit = new System.Windows.Forms.Button();
			this.btnPropertiesDelete = new System.Windows.Forms.Button();
			this.btnPropertiesAdd = new System.Windows.Forms.Button();
			this.grdProperties = new System.Windows.Forms.DataGridView();
			this.menuDecisionNode = new System.Windows.Forms.MenuStrip();
			this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditMedia = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditMediaAddResponse = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditMediaDeleteResponse = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditMediaSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuEditMediaAddQuestion = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEditMediaDeleteQuestion = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewNodesPage = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewMediaPage = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewStoryboardPage = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuViewPropertiesPage = new System.Windows.Forms.ToolStripMenuItem();
			this.tctl.SuspendLayout();
			this.tabNode.SuspendLayout();
			this.grpType.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdAnswer)).BeginInit();
			this.grpLocation.SuspendLayout();
			this.tabMedia.SuspendLayout();
			this.tabStoryboard.SuspendLayout();
			this.grpStoryColors.SuspendLayout();
			this.grpStoryPageLocation.SuspendLayout();
			this.tabProperties.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdProperties)).BeginInit();
			this.menuDecisionNode.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(326, 541);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(108, 47);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(442, 541);
			this.btnOK.Margin = new System.Windows.Forms.Padding(4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(108, 47);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "&OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// tctl
			// 
			this.tctl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tctl.Controls.Add(this.tabNode);
			this.tctl.Controls.Add(this.tabMedia);
			this.tctl.Controls.Add(this.tabStoryboard);
			this.tctl.Controls.Add(this.tabProperties);
			this.tctl.Location = new System.Drawing.Point(12, 40);
			this.tctl.Name = "tctl";
			this.tctl.SelectedIndex = 0;
			this.tctl.Size = new System.Drawing.Size(539, 494);
			this.tctl.TabIndex = 0;
			// 
			// tabNode
			// 
			this.tabNode.Controls.Add(this.grpType);
			this.tabNode.Controls.Add(this.btnAnswerEdit);
			this.tabNode.Controls.Add(this.btnAnswerDelete);
			this.tabNode.Controls.Add(this.btnAnswerAdd);
			this.tabNode.Controls.Add(this.grdAnswer);
			this.tabNode.Controls.Add(this.lblAnswers);
			this.tabNode.Controls.Add(this.grpLocation);
			this.tabNode.Controls.Add(this.txtQuestion);
			this.tabNode.Controls.Add(this.lblQuestion);
			this.tabNode.Location = new System.Drawing.Point(4, 30);
			this.tabNode.Name = "tabNode";
			this.tabNode.Padding = new System.Windows.Forms.Padding(3);
			this.tabNode.Size = new System.Drawing.Size(531, 460);
			this.tabNode.TabIndex = 0;
			this.tabNode.Text = "Node";
			this.tabNode.UseVisualStyleBackColor = true;
			// 
			// grpType
			// 
			this.grpType.Controls.Add(this.txtDelay);
			this.grpType.Controls.Add(this.lblDelaySec);
			this.grpType.Controls.Add(this.lblDelay);
			this.grpType.Controls.Add(this.cmboType);
			this.grpType.Location = new System.Drawing.Point(333, 5);
			this.grpType.Name = "grpType";
			this.grpType.Size = new System.Drawing.Size(185, 96);
			this.grpType.TabIndex = 1;
			this.grpType.TabStop = false;
			this.grpType.Text = "Node &Type";
			// 
			// txtDelay
			// 
			this.txtDelay.Location = new System.Drawing.Point(66, 62);
			this.txtDelay.Margin = new System.Windows.Forms.Padding(4);
			this.txtDelay.Name = "txtDelay";
			this.txtDelay.Size = new System.Drawing.Size(61, 28);
			this.txtDelay.TabIndex = 2;
			this.txtDelay.Text = "0";
			this.txtDelay.Visible = false;
			// 
			// lblDelaySec
			// 
			this.lblDelaySec.AutoSize = true;
			this.lblDelaySec.Location = new System.Drawing.Point(128, 65);
			this.lblDelaySec.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDelaySec.Name = "lblDelaySec";
			this.lblDelaySec.Size = new System.Drawing.Size(35, 21);
			this.lblDelaySec.TabIndex = 3;
			this.lblDelaySec.Text = "sec";
			this.lblDelaySec.Visible = false;
			// 
			// lblDelay
			// 
			this.lblDelay.AutoSize = true;
			this.lblDelay.Location = new System.Drawing.Point(7, 65);
			this.lblDelay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDelay.Name = "lblDelay";
			this.lblDelay.Size = new System.Drawing.Size(58, 21);
			this.lblDelay.TabIndex = 1;
			this.lblDelay.Text = "De&lay:";
			this.lblDelay.Visible = false;
			// 
			// cmboType
			// 
			this.cmboType.FormattingEnabled = true;
			this.cmboType.Items.AddRange(new object[] {
            "Start",
            "Fork",
            "Delay",
            "Termination"});
			this.cmboType.Location = new System.Drawing.Point(5, 26);
			this.cmboType.Name = "cmboType";
			this.cmboType.Size = new System.Drawing.Size(175, 29);
			this.cmboType.TabIndex = 0;
			this.cmboType.SelectedIndexChanged += new System.EventHandler(this.cmboType_SelectedIndexChanged);
			// 
			// btnAnswerEdit
			// 
			this.btnAnswerEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAnswerEdit.Location = new System.Drawing.Point(411, 286);
			this.btnAnswerEdit.Margin = new System.Windows.Forms.Padding(4);
			this.btnAnswerEdit.Name = "btnAnswerEdit";
			this.btnAnswerEdit.Size = new System.Drawing.Size(108, 47);
			this.btnAnswerEdit.TabIndex = 7;
			this.btnAnswerEdit.Text = "&Edit Answer";
			this.btnAnswerEdit.UseVisualStyleBackColor = true;
			this.btnAnswerEdit.Click += new System.EventHandler(this.btnAnswerEdit_Click);
			// 
			// btnAnswerDelete
			// 
			this.btnAnswerDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAnswerDelete.Location = new System.Drawing.Point(412, 342);
			this.btnAnswerDelete.Margin = new System.Windows.Forms.Padding(4);
			this.btnAnswerDelete.Name = "btnAnswerDelete";
			this.btnAnswerDelete.Size = new System.Drawing.Size(108, 47);
			this.btnAnswerDelete.TabIndex = 8;
			this.btnAnswerDelete.Text = "&Delete";
			this.btnAnswerDelete.UseVisualStyleBackColor = true;
			this.btnAnswerDelete.Click += new System.EventHandler(this.btnAnswerDelete_Click);
			// 
			// btnAnswerAdd
			// 
			this.btnAnswerAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAnswerAdd.Location = new System.Drawing.Point(412, 230);
			this.btnAnswerAdd.Margin = new System.Windows.Forms.Padding(4);
			this.btnAnswerAdd.Name = "btnAnswerAdd";
			this.btnAnswerAdd.Size = new System.Drawing.Size(108, 47);
			this.btnAnswerAdd.TabIndex = 6;
			this.btnAnswerAdd.Text = "Add A&nswer";
			this.btnAnswerAdd.UseVisualStyleBackColor = true;
			this.btnAnswerAdd.Click += new System.EventHandler(this.btnAnswerAdd_Click);
			// 
			// grdAnswer
			// 
			this.grdAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grdAnswer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.grdAnswer.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
			this.grdAnswer.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.Navy;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.grdAnswer.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
			this.grdAnswer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdAnswer.Location = new System.Drawing.Point(7, 230);
			this.grdAnswer.Margin = new System.Windows.Forms.Padding(4);
			this.grdAnswer.MultiSelect = false;
			this.grdAnswer.Name = "grdAnswer";
			this.grdAnswer.RowHeadersVisible = false;
			this.grdAnswer.RowHeadersWidth = 51;
			this.grdAnswer.RowTemplate.Height = 24;
			this.grdAnswer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.grdAnswer.Size = new System.Drawing.Size(397, 221);
			this.grdAnswer.TabIndex = 5;
			this.grdAnswer.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdAnswer_CellDoubleClick);
			this.grdAnswer.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.grdAnswer_DataBindingComplete);
			this.grdAnswer.SelectionChanged += new System.EventHandler(this.grdAnswer_SelectionChanged);
			// 
			// lblAnswers
			// 
			this.lblAnswers.AutoSize = true;
			this.lblAnswers.Location = new System.Drawing.Point(7, 204);
			this.lblAnswers.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblAnswers.Name = "lblAnswers";
			this.lblAnswers.Size = new System.Drawing.Size(80, 21);
			this.lblAnswers.TabIndex = 4;
			this.lblAnswers.Text = "&Answers:";
			// 
			// grpLocation
			// 
			this.grpLocation.Controls.Add(this.txtY);
			this.grpLocation.Controls.Add(this.lblY);
			this.grpLocation.Controls.Add(this.txtX);
			this.grpLocation.Controls.Add(this.lblX);
			this.grpLocation.Location = new System.Drawing.Point(11, 5);
			this.grpLocation.Margin = new System.Windows.Forms.Padding(4);
			this.grpLocation.Name = "grpLocation";
			this.grpLocation.Padding = new System.Windows.Forms.Padding(4);
			this.grpLocation.Size = new System.Drawing.Size(316, 96);
			this.grpLocation.TabIndex = 0;
			this.grpLocation.TabStop = false;
			this.grpLocation.Text = "Editor Location";
			// 
			// txtY
			// 
			this.txtY.Location = new System.Drawing.Point(191, 26);
			this.txtY.Margin = new System.Windows.Forms.Padding(4);
			this.txtY.Name = "txtY";
			this.txtY.Size = new System.Drawing.Size(112, 28);
			this.txtY.TabIndex = 3;
			this.txtY.Text = "0.000";
			// 
			// lblY
			// 
			this.lblY.AutoSize = true;
			this.lblY.Location = new System.Drawing.Point(160, 29);
			this.lblY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblY.Name = "lblY";
			this.lblY.Size = new System.Drawing.Size(26, 21);
			this.lblY.TabIndex = 2;
			this.lblY.Text = "&Y:";
			// 
			// txtX
			// 
			this.txtX.Location = new System.Drawing.Point(37, 26);
			this.txtX.Margin = new System.Windows.Forms.Padding(4);
			this.txtX.Name = "txtX";
			this.txtX.Size = new System.Drawing.Size(112, 28);
			this.txtX.TabIndex = 1;
			this.txtX.Text = "0.000";
			// 
			// lblX
			// 
			this.lblX.AutoSize = true;
			this.lblX.Location = new System.Drawing.Point(7, 29);
			this.lblX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblX.Name = "lblX";
			this.lblX.Size = new System.Drawing.Size(26, 21);
			this.lblX.TabIndex = 0;
			this.lblX.Text = "&X:";
			// 
			// txtQuestion
			// 
			this.txtQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtQuestion.Location = new System.Drawing.Point(92, 109);
			this.txtQuestion.Margin = new System.Windows.Forms.Padding(4);
			this.txtQuestion.Multiline = true;
			this.txtQuestion.Name = "txtQuestion";
			this.txtQuestion.Size = new System.Drawing.Size(427, 77);
			this.txtQuestion.TabIndex = 3;
			// 
			// lblQuestion
			// 
			this.lblQuestion.AutoSize = true;
			this.lblQuestion.Location = new System.Drawing.Point(7, 113);
			this.lblQuestion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblQuestion.Name = "lblQuestion";
			this.lblQuestion.Size = new System.Drawing.Size(82, 21);
			this.lblQuestion.TabIndex = 2;
			this.lblQuestion.Text = "&Question:";
			// 
			// tabMedia
			// 
			this.tabMedia.Controls.Add(this.lblMediaQuestion);
			this.tabMedia.Controls.Add(this.lblMediaResponse);
			this.tabMedia.Controls.Add(this.lblMediaPerspective);
			this.tabMedia.Controls.Add(this.btnMediaDeleteQuestion);
			this.tabMedia.Controls.Add(this.btnMediaDeleteResponse);
			this.tabMedia.Controls.Add(this.btnMediaAddQuestion);
			this.tabMedia.Controls.Add(this.btnMediaAddResponse);
			this.tabMedia.Controls.Add(this.lvMediaQuestion);
			this.tabMedia.Controls.Add(this.lvMediaResponse);
			this.tabMedia.Location = new System.Drawing.Point(4, 30);
			this.tabMedia.Name = "tabMedia";
			this.tabMedia.Size = new System.Drawing.Size(531, 460);
			this.tabMedia.TabIndex = 3;
			this.tabMedia.Text = "Media";
			this.tabMedia.UseVisualStyleBackColor = true;
			// 
			// lblMediaQuestion
			// 
			this.lblMediaQuestion.AutoSize = true;
			this.lblMediaQuestion.Location = new System.Drawing.Point(285, 49);
			this.lblMediaQuestion.Name = "lblMediaQuestion";
			this.lblMediaQuestion.Size = new System.Drawing.Size(214, 21);
			this.lblMediaQuestion.TabIndex = 5;
			this.lblMediaQuestion.Text = "Node / Question (outgoing)";
			// 
			// lblMediaResponse
			// 
			this.lblMediaResponse.AutoSize = true;
			this.lblMediaResponse.Location = new System.Drawing.Point(20, 49);
			this.lblMediaResponse.Name = "lblMediaResponse";
			this.lblMediaResponse.Size = new System.Drawing.Size(220, 21);
			this.lblMediaResponse.TabIndex = 1;
			this.lblMediaResponse.Text = "Response (incoming socket)";
			// 
			// lblMediaPerspective
			// 
			this.lblMediaPerspective.AutoSize = true;
			this.lblMediaPerspective.Location = new System.Drawing.Point(12, 24);
			this.lblMediaPerspective.Name = "lblMediaPerspective";
			this.lblMediaPerspective.Size = new System.Drawing.Size(101, 21);
			this.lblMediaPerspective.TabIndex = 0;
			this.lblMediaPerspective.Text = "Perspective:";
			// 
			// btnMediaDeleteQuestion
			// 
			this.btnMediaDeleteQuestion.Enabled = false;
			this.btnMediaDeleteQuestion.Location = new System.Drawing.Point(397, 400);
			this.btnMediaDeleteQuestion.Name = "btnMediaDeleteQuestion";
			this.btnMediaDeleteQuestion.Size = new System.Drawing.Size(122, 47);
			this.btnMediaDeleteQuestion.TabIndex = 8;
			this.btnMediaDeleteQuestion.Text = "&Delete Media";
			this.btnMediaDeleteQuestion.UseVisualStyleBackColor = true;
			this.btnMediaDeleteQuestion.Click += new System.EventHandler(this.btnMediaDeleteQuestion_Click);
			// 
			// btnMediaDeleteResponse
			// 
			this.btnMediaDeleteResponse.Enabled = false;
			this.btnMediaDeleteResponse.Location = new System.Drawing.Point(136, 400);
			this.btnMediaDeleteResponse.Name = "btnMediaDeleteResponse";
			this.btnMediaDeleteResponse.Size = new System.Drawing.Size(122, 47);
			this.btnMediaDeleteResponse.TabIndex = 4;
			this.btnMediaDeleteResponse.Text = "&Delete Media";
			this.btnMediaDeleteResponse.UseVisualStyleBackColor = true;
			this.btnMediaDeleteResponse.Click += new System.EventHandler(this.btnMediaDeleteResponse_Click);
			// 
			// btnMediaAddQuestion
			// 
			this.btnMediaAddQuestion.Location = new System.Drawing.Point(269, 400);
			this.btnMediaAddQuestion.Name = "btnMediaAddQuestion";
			this.btnMediaAddQuestion.Size = new System.Drawing.Size(122, 47);
			this.btnMediaAddQuestion.TabIndex = 7;
			this.btnMediaAddQuestion.Text = "Add &Media";
			this.btnMediaAddQuestion.UseVisualStyleBackColor = true;
			this.btnMediaAddQuestion.Click += new System.EventHandler(this.btnMediaAddQuestion_Click);
			// 
			// btnMediaAddResponse
			// 
			this.btnMediaAddResponse.Location = new System.Drawing.Point(8, 400);
			this.btnMediaAddResponse.Name = "btnMediaAddResponse";
			this.btnMediaAddResponse.Size = new System.Drawing.Size(122, 47);
			this.btnMediaAddResponse.TabIndex = 3;
			this.btnMediaAddResponse.Text = "Add &Media";
			this.btnMediaAddResponse.UseVisualStyleBackColor = true;
			this.btnMediaAddResponse.Click += new System.EventHandler(this.btnMediaAddResponse_Click);
			// 
			// lvMediaQuestion
			// 
			this.lvMediaQuestion.HideSelection = false;
			this.lvMediaQuestion.LargeImageList = this.imageListMediaQuestion;
			this.lvMediaQuestion.Location = new System.Drawing.Point(269, 73);
			this.lvMediaQuestion.Name = "lvMediaQuestion";
			this.lvMediaQuestion.Size = new System.Drawing.Size(249, 321);
			this.lvMediaQuestion.TabIndex = 6;
			this.lvMediaQuestion.UseCompatibleStateImageBehavior = false;
			this.lvMediaQuestion.SelectedIndexChanged += new System.EventHandler(this.lvMediaQuestion_SelectedIndexChanged);
			// 
			// imageListMediaQuestion
			// 
			this.imageListMediaQuestion.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageListMediaQuestion.ImageSize = new System.Drawing.Size(128, 128);
			this.imageListMediaQuestion.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// lvMediaResponse
			// 
			this.lvMediaResponse.HideSelection = false;
			this.lvMediaResponse.LargeImageList = this.imageListMediaResponse;
			this.lvMediaResponse.Location = new System.Drawing.Point(8, 73);
			this.lvMediaResponse.Name = "lvMediaResponse";
			this.lvMediaResponse.Size = new System.Drawing.Size(249, 321);
			this.lvMediaResponse.TabIndex = 2;
			this.lvMediaResponse.UseCompatibleStateImageBehavior = false;
			this.lvMediaResponse.SelectedIndexChanged += new System.EventHandler(this.lvMediaResponse_SelectedIndexChanged);
			// 
			// imageListMediaResponse
			// 
			this.imageListMediaResponse.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageListMediaResponse.ImageSize = new System.Drawing.Size(128, 128);
			this.imageListMediaResponse.TransparentColor = System.Drawing.Color.Transparent;
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
			this.tabStoryboard.Size = new System.Drawing.Size(531, 460);
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
			this.grpStoryPageLocation.Controls.Add(this.txtStoryPageNumber);
			this.grpStoryPageLocation.Controls.Add(this.lblStoryPageNumber);
			this.grpStoryPageLocation.Controls.Add(this.txtStoryPageY);
			this.grpStoryPageLocation.Controls.Add(this.lblStoryPageY);
			this.grpStoryPageLocation.Controls.Add(this.txtStoryPageX);
			this.grpStoryPageLocation.Controls.Add(this.lblStoryFromY);
			this.grpStoryPageLocation.Controls.Add(this.lblStoryFromX);
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
			this.tabProperties.Size = new System.Drawing.Size(531, 460);
			this.tabProperties.TabIndex = 2;
			this.tabProperties.Text = "Properties";
			this.tabProperties.ToolTipText = "View and edit all the properties of the node.";
			this.tabProperties.UseVisualStyleBackColor = true;
			// 
			// btnPropertiesEdit
			// 
			this.btnPropertiesEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPropertiesEdit.Location = new System.Drawing.Point(408, 68);
			this.btnPropertiesEdit.Margin = new System.Windows.Forms.Padding(4);
			this.btnPropertiesEdit.Name = "btnPropertiesEdit";
			this.btnPropertiesEdit.Size = new System.Drawing.Size(108, 47);
			this.btnPropertiesEdit.TabIndex = 2;
			this.btnPropertiesEdit.Text = "&Edit";
			this.btnPropertiesEdit.UseVisualStyleBackColor = true;
			this.btnPropertiesEdit.Click += new System.EventHandler(this.btnPropertiesEdit_Click);
			// 
			// btnPropertiesDelete
			// 
			this.btnPropertiesDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPropertiesDelete.Location = new System.Drawing.Point(409, 124);
			this.btnPropertiesDelete.Margin = new System.Windows.Forms.Padding(4);
			this.btnPropertiesDelete.Name = "btnPropertiesDelete";
			this.btnPropertiesDelete.Size = new System.Drawing.Size(108, 47);
			this.btnPropertiesDelete.TabIndex = 3;
			this.btnPropertiesDelete.Text = "&Delete";
			this.btnPropertiesDelete.UseVisualStyleBackColor = true;
			this.btnPropertiesDelete.Click += new System.EventHandler(this.btnPropertiesDelete_Click);
			// 
			// btnPropertiesAdd
			// 
			this.btnPropertiesAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPropertiesAdd.Location = new System.Drawing.Point(409, 12);
			this.btnPropertiesAdd.Margin = new System.Windows.Forms.Padding(4);
			this.btnPropertiesAdd.Name = "btnPropertiesAdd";
			this.btnPropertiesAdd.Size = new System.Drawing.Size(108, 47);
			this.btnPropertiesAdd.TabIndex = 1;
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
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.Color.Navy;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.grdProperties.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.grdProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdProperties.Location = new System.Drawing.Point(4, 12);
			this.grdProperties.Margin = new System.Windows.Forms.Padding(4);
			this.grdProperties.MultiSelect = false;
			this.grdProperties.Name = "grdProperties";
			this.grdProperties.RowHeadersVisible = false;
			this.grdProperties.RowHeadersWidth = 51;
			this.grdProperties.RowTemplate.Height = 24;
			this.grdProperties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.grdProperties.Size = new System.Drawing.Size(397, 426);
			this.grdProperties.TabIndex = 0;
			this.grdProperties.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.grdProperties_CellBeginEdit);
			this.grdProperties.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdProperties_CellDoubleClick);
			this.grdProperties.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grdProperties_CellFormatting);
			this.grdProperties.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.grdProperties_DataBindingComplete);
			this.grdProperties.SelectionChanged += new System.EventHandler(this.grdProperties_SelectionChanged);
			this.grdProperties.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.grdProperties_UserDeletingRow);
			// 
			// menuDecisionNode
			// 
			this.menuDecisionNode.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuDecisionNode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEdit,
            this.mnuView});
			this.menuDecisionNode.Location = new System.Drawing.Point(0, 0);
			this.menuDecisionNode.Name = "menuDecisionNode";
			this.menuDecisionNode.Size = new System.Drawing.Size(563, 28);
			this.menuDecisionNode.TabIndex = 3;
			this.menuDecisionNode.Text = "menuStrip1";
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
            this.mnuEditMediaAddResponse,
            this.mnuEditMediaDeleteResponse,
            this.mnuEditMediaSep1,
            this.mnuEditMediaAddQuestion,
            this.mnuEditMediaDeleteQuestion});
			this.mnuEditMedia.Name = "mnuEditMedia";
			this.mnuEditMedia.Size = new System.Drawing.Size(134, 26);
			this.mnuEditMedia.Text = "&Media";
			// 
			// mnuEditMediaAddResponse
			// 
			this.mnuEditMediaAddResponse.Name = "mnuEditMediaAddResponse";
			this.mnuEditMediaAddResponse.Size = new System.Drawing.Size(264, 26);
			this.mnuEditMediaAddResponse.Text = "&Response Add";
			this.mnuEditMediaAddResponse.Click += new System.EventHandler(this.mnuEditMediaAddResponse_Click);
			// 
			// mnuEditMediaDeleteResponse
			// 
			this.mnuEditMediaDeleteResponse.Name = "mnuEditMediaDeleteResponse";
			this.mnuEditMediaDeleteResponse.Size = new System.Drawing.Size(264, 26);
			this.mnuEditMediaDeleteResponse.Text = "R&esponse Delete Selected";
			this.mnuEditMediaDeleteResponse.Click += new System.EventHandler(this.mnuEditMediaDeleteResponse_Click);
			// 
			// mnuEditMediaSep1
			// 
			this.mnuEditMediaSep1.Name = "mnuEditMediaSep1";
			this.mnuEditMediaSep1.Size = new System.Drawing.Size(261, 6);
			// 
			// mnuEditMediaAddQuestion
			// 
			this.mnuEditMediaAddQuestion.Name = "mnuEditMediaAddQuestion";
			this.mnuEditMediaAddQuestion.Size = new System.Drawing.Size(264, 26);
			this.mnuEditMediaAddQuestion.Text = "Question &Add";
			this.mnuEditMediaAddQuestion.Click += new System.EventHandler(this.mnuEditMediaAddQuestion_Click);
			// 
			// mnuEditMediaDeleteQuestion
			// 
			this.mnuEditMediaDeleteQuestion.Name = "mnuEditMediaDeleteQuestion";
			this.mnuEditMediaDeleteQuestion.Size = new System.Drawing.Size(264, 26);
			this.mnuEditMediaDeleteQuestion.Text = "Question &Delete Selected";
			this.mnuEditMediaDeleteQuestion.Click += new System.EventHandler(this.mnuEditMediaDeleteQuestion_Click);
			// 
			// mnuView
			// 
			this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewNodesPage,
            this.mnuViewMediaPage,
            this.mnuViewStoryboardPage,
            this.mnuViewPropertiesPage});
			this.mnuView.Name = "mnuView";
			this.mnuView.Size = new System.Drawing.Size(55, 24);
			this.mnuView.Text = "&View";
			// 
			// mnuViewNodesPage
			// 
			this.mnuViewNodesPage.Name = "mnuViewNodesPage";
			this.mnuViewNodesPage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
			this.mnuViewNodesPage.Size = new System.Drawing.Size(252, 26);
			this.mnuViewNodesPage.Text = "&Nodes Page";
			this.mnuViewNodesPage.Click += new System.EventHandler(this.mnuViewNodesPage_Click);
			// 
			// mnuViewMediaPage
			// 
			this.mnuViewMediaPage.Name = "mnuViewMediaPage";
			this.mnuViewMediaPage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
			this.mnuViewMediaPage.Size = new System.Drawing.Size(252, 26);
			this.mnuViewMediaPage.Text = "&Media Page";
			this.mnuViewMediaPage.Click += new System.EventHandler(this.mnuViewMediaPage_Click);
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
			// frmDecisionNode
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(563, 604);
			this.Controls.Add(this.tctl);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.menuDecisionNode);
			this.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "frmDecisionNode";
			this.Text = "Decision Properties";
			this.tctl.ResumeLayout(false);
			this.tabNode.ResumeLayout(false);
			this.tabNode.PerformLayout();
			this.grpType.ResumeLayout(false);
			this.grpType.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdAnswer)).EndInit();
			this.grpLocation.ResumeLayout(false);
			this.grpLocation.PerformLayout();
			this.tabMedia.ResumeLayout(false);
			this.tabMedia.PerformLayout();
			this.tabStoryboard.ResumeLayout(false);
			this.tabStoryboard.PerformLayout();
			this.grpStoryColors.ResumeLayout(false);
			this.grpStoryColors.PerformLayout();
			this.grpStoryPageLocation.ResumeLayout(false);
			this.grpStoryPageLocation.PerformLayout();
			this.tabProperties.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdProperties)).EndInit();
			this.menuDecisionNode.ResumeLayout(false);
			this.menuDecisionNode.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TabControl tctl;
		private System.Windows.Forms.TabPage tabNode;
		private System.Windows.Forms.GroupBox grpType;
		private System.Windows.Forms.ComboBox cmboType;
		private System.Windows.Forms.Button btnAnswerEdit;
		private System.Windows.Forms.Button btnAnswerDelete;
		private System.Windows.Forms.Button btnAnswerAdd;
		private System.Windows.Forms.DataGridView grdAnswer;
		private System.Windows.Forms.Label lblAnswers;
		private System.Windows.Forms.GroupBox grpLocation;
		private System.Windows.Forms.TextBox txtY;
		private System.Windows.Forms.Label lblY;
		private System.Windows.Forms.TextBox txtX;
		private System.Windows.Forms.Label lblX;
		private System.Windows.Forms.TextBox txtQuestion;
		private System.Windows.Forms.Label lblQuestion;
		private System.Windows.Forms.TabPage tabStoryboard;
		private System.Windows.Forms.MenuStrip menuDecisionNode;
		private System.Windows.Forms.ToolStripMenuItem mnuView;
		private System.Windows.Forms.ToolStripMenuItem mnuViewNodesPage;
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
		private System.Windows.Forms.Button btnPropertiesEdit;
		private System.Windows.Forms.Button btnPropertiesDelete;
		private System.Windows.Forms.Button btnPropertiesAdd;
		private System.Windows.Forms.DataGridView grdProperties;
		private System.Windows.Forms.ToolStripMenuItem mnuViewPropertiesPage;
		private System.Windows.Forms.ComboBox cmboStoryFromY;
		private System.Windows.Forms.ComboBox cmboStoryFromX;
		private System.Windows.Forms.Label lblStoryFromY;
		private System.Windows.Forms.Label lblStoryFromX;
		private System.Windows.Forms.TextBox txtStoryWidth;
		private System.Windows.Forms.Label lblStoryWidth;
		private System.Windows.Forms.TextBox txtDelay;
		private System.Windows.Forms.Label lblDelaySec;
		private System.Windows.Forms.Label lblDelay;
		private System.Windows.Forms.TabPage tabMedia;
		private System.Windows.Forms.Label lblMediaPerspective;
		private System.Windows.Forms.Button btnMediaDeleteResponse;
		private System.Windows.Forms.Button btnMediaAddResponse;
		private System.Windows.Forms.ListView lvMediaResponse;
		private System.Windows.Forms.ImageList imageListMediaResponse;
		private System.Windows.Forms.ToolStripMenuItem mnuEdit;
		private System.Windows.Forms.ToolStripMenuItem mnuEditMedia;
		private System.Windows.Forms.ToolStripMenuItem mnuEditMediaAddResponse;
		private System.Windows.Forms.ToolStripMenuItem mnuEditMediaDeleteResponse;
		private System.Windows.Forms.ToolStripMenuItem mnuViewMediaPage;
		private System.Windows.Forms.Label lblMediaQuestion;
		private System.Windows.Forms.Label lblMediaResponse;
		private System.Windows.Forms.Button btnMediaDeleteQuestion;
		private System.Windows.Forms.Button btnMediaAddQuestion;
		private System.Windows.Forms.ListView lvMediaQuestion;
		private System.Windows.Forms.ToolStripSeparator mnuEditMediaSep1;
		private System.Windows.Forms.ToolStripMenuItem mnuEditMediaAddQuestion;
		private System.Windows.Forms.ToolStripMenuItem mnuEditMediaDeleteQuestion;
		private System.Windows.Forms.ImageList imageListMediaQuestion;
	}
}