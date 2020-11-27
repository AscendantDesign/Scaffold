
namespace Scaffold
{
	partial class frmFind
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
			this.grpMedia = new System.Windows.Forms.GroupBox();
			this.txtMediaLink = new System.Windows.Forms.TextBox();
			this.txtMediaAudio = new System.Windows.Forms.TextBox();
			this.txtMediaVideo = new System.Windows.Forms.TextBox();
			this.txtMediaImage = new System.Windows.Forms.TextBox();
			this.chkMediaLink = new System.Windows.Forms.CheckBox();
			this.chkMediaAudio = new System.Windows.Forms.CheckBox();
			this.chkMediaVideo = new System.Windows.Forms.CheckBox();
			this.chkMediaImage = new System.Windows.Forms.CheckBox();
			this.lblSearch = new System.Windows.Forms.Label();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.lv = new System.Windows.Forms.ListView();
			this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnFind = new System.Windows.Forms.Button();
			this.lbllv = new System.Windows.Forms.Label();
			this.grpMedia.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpMedia
			// 
			this.grpMedia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpMedia.Controls.Add(this.txtMediaLink);
			this.grpMedia.Controls.Add(this.txtMediaAudio);
			this.grpMedia.Controls.Add(this.txtMediaVideo);
			this.grpMedia.Controls.Add(this.txtMediaImage);
			this.grpMedia.Controls.Add(this.chkMediaLink);
			this.grpMedia.Controls.Add(this.chkMediaAudio);
			this.grpMedia.Controls.Add(this.chkMediaVideo);
			this.grpMedia.Controls.Add(this.chkMediaImage);
			this.grpMedia.ForeColor = System.Drawing.Color.Gainsboro;
			this.grpMedia.Location = new System.Drawing.Point(15, 15);
			this.grpMedia.Margin = new System.Windows.Forms.Padding(4);
			this.grpMedia.Name = "grpMedia";
			this.grpMedia.Padding = new System.Windows.Forms.Padding(4);
			this.grpMedia.Size = new System.Drawing.Size(664, 120);
			this.grpMedia.TabIndex = 0;
			this.grpMedia.TabStop = false;
			this.grpMedia.Text = "&Media (filenames are optional)";
			// 
			// txtMediaLink
			// 
			this.txtMediaLink.BackColor = System.Drawing.SystemColors.Control;
			this.txtMediaLink.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.txtMediaLink.Location = new System.Drawing.Point(432, 70);
			this.txtMediaLink.Margin = new System.Windows.Forms.Padding(4);
			this.txtMediaLink.Name = "txtMediaLink";
			this.txtMediaLink.ReadOnly = true;
			this.txtMediaLink.Size = new System.Drawing.Size(205, 27);
			this.txtMediaLink.TabIndex = 7;
			// 
			// txtMediaAudio
			// 
			this.txtMediaAudio.BackColor = System.Drawing.SystemColors.Control;
			this.txtMediaAudio.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.txtMediaAudio.Location = new System.Drawing.Point(115, 70);
			this.txtMediaAudio.Margin = new System.Windows.Forms.Padding(4);
			this.txtMediaAudio.Name = "txtMediaAudio";
			this.txtMediaAudio.ReadOnly = true;
			this.txtMediaAudio.Size = new System.Drawing.Size(205, 27);
			this.txtMediaAudio.TabIndex = 5;
			// 
			// txtMediaVideo
			// 
			this.txtMediaVideo.BackColor = System.Drawing.SystemColors.Control;
			this.txtMediaVideo.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.txtMediaVideo.Location = new System.Drawing.Point(432, 36);
			this.txtMediaVideo.Margin = new System.Windows.Forms.Padding(4);
			this.txtMediaVideo.Name = "txtMediaVideo";
			this.txtMediaVideo.ReadOnly = true;
			this.txtMediaVideo.Size = new System.Drawing.Size(205, 27);
			this.txtMediaVideo.TabIndex = 3;
			// 
			// txtMediaImage
			// 
			this.txtMediaImage.BackColor = System.Drawing.SystemColors.Control;
			this.txtMediaImage.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.txtMediaImage.Location = new System.Drawing.Point(115, 36);
			this.txtMediaImage.Margin = new System.Windows.Forms.Padding(4);
			this.txtMediaImage.Name = "txtMediaImage";
			this.txtMediaImage.ReadOnly = true;
			this.txtMediaImage.Size = new System.Drawing.Size(205, 27);
			this.txtMediaImage.TabIndex = 1;
			// 
			// chkMediaLink
			// 
			this.chkMediaLink.AutoSize = true;
			this.chkMediaLink.Location = new System.Drawing.Point(342, 72);
			this.chkMediaLink.Margin = new System.Windows.Forms.Padding(4);
			this.chkMediaLink.Name = "chkMediaLink";
			this.chkMediaLink.Size = new System.Drawing.Size(62, 24);
			this.chkMediaLink.TabIndex = 6;
			this.chkMediaLink.Text = "&Link";
			this.chkMediaLink.UseVisualStyleBackColor = true;
			this.chkMediaLink.CheckedChanged += new System.EventHandler(this.chkMediaLink_CheckedChanged);
			// 
			// chkMediaAudio
			// 
			this.chkMediaAudio.AutoSize = true;
			this.chkMediaAudio.Location = new System.Drawing.Point(22, 72);
			this.chkMediaAudio.Margin = new System.Windows.Forms.Padding(4);
			this.chkMediaAudio.Name = "chkMediaAudio";
			this.chkMediaAudio.Size = new System.Drawing.Size(73, 24);
			this.chkMediaAudio.TabIndex = 4;
			this.chkMediaAudio.Text = "&Audio";
			this.chkMediaAudio.UseVisualStyleBackColor = true;
			this.chkMediaAudio.CheckedChanged += new System.EventHandler(this.chkMediaAudio_CheckedChanged);
			// 
			// chkMediaVideo
			// 
			this.chkMediaVideo.AutoSize = true;
			this.chkMediaVideo.Location = new System.Drawing.Point(342, 39);
			this.chkMediaVideo.Margin = new System.Windows.Forms.Padding(4);
			this.chkMediaVideo.Name = "chkMediaVideo";
			this.chkMediaVideo.Size = new System.Drawing.Size(73, 24);
			this.chkMediaVideo.TabIndex = 2;
			this.chkMediaVideo.Text = "&Video";
			this.chkMediaVideo.UseVisualStyleBackColor = true;
			this.chkMediaVideo.CheckedChanged += new System.EventHandler(this.chkMediaVideo_CheckedChanged);
			// 
			// chkMediaImage
			// 
			this.chkMediaImage.AutoSize = true;
			this.chkMediaImage.Location = new System.Drawing.Point(22, 39);
			this.chkMediaImage.Margin = new System.Windows.Forms.Padding(4);
			this.chkMediaImage.Name = "chkMediaImage";
			this.chkMediaImage.Size = new System.Drawing.Size(76, 24);
			this.chkMediaImage.TabIndex = 0;
			this.chkMediaImage.Text = "&Image";
			this.chkMediaImage.UseVisualStyleBackColor = true;
			this.chkMediaImage.CheckedChanged += new System.EventHandler(this.chkMediaImage_CheckedChanged);
			// 
			// lblSearch
			// 
			this.lblSearch.AutoSize = true;
			this.lblSearch.Location = new System.Drawing.Point(15, 154);
			this.lblSearch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSearch.Name = "lblSearch";
			this.lblSearch.Size = new System.Drawing.Size(104, 20);
			this.lblSearch.TabIndex = 1;
			this.lblSearch.Text = "Search &Text:";
			// 
			// txtSearch
			// 
			this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSearch.Location = new System.Drawing.Point(132, 150);
			this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(438, 27);
			this.txtSearch.TabIndex = 2;
			// 
			// lv
			// 
			this.lv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colType,
            this.colText});
			this.lv.HideSelection = false;
			this.lv.Location = new System.Drawing.Point(15, 219);
			this.lv.Margin = new System.Windows.Forms.Padding(4);
			this.lv.Name = "lv";
			this.lv.Size = new System.Drawing.Size(663, 193);
			this.lv.TabIndex = 5;
			this.lv.UseCompatibleStateImageBehavior = false;
			this.lv.View = System.Windows.Forms.View.Details;
			// 
			// colType
			// 
			this.colType.Text = "Type";
			this.colType.Width = 120;
			// 
			// colText
			// 
			this.colText.Text = "Text";
			this.colText.Width = 200;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.ForeColor = System.Drawing.Color.Black;
			this.btnCancel.Location = new System.Drawing.Point(470, 420);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(100, 39);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.ForeColor = System.Drawing.Color.Black;
			this.btnOK.Location = new System.Drawing.Point(581, 420);
			this.btnOK.Margin = new System.Windows.Forms.Padding(4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(100, 39);
			this.btnOK.TabIndex = 7;
			this.btnOK.Text = "&OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnFind
			// 
			this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFind.ForeColor = System.Drawing.Color.Black;
			this.btnFind.Location = new System.Drawing.Point(579, 147);
			this.btnFind.Margin = new System.Windows.Forms.Padding(4);
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = new System.Drawing.Size(100, 34);
			this.btnFind.TabIndex = 3;
			this.btnFind.Text = "&Find";
			this.btnFind.UseVisualStyleBackColor = true;
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			// 
			// lbllv
			// 
			this.lbllv.AutoSize = true;
			this.lbllv.ForeColor = System.Drawing.Color.DarkGray;
			this.lbllv.Location = new System.Drawing.Point(33, 195);
			this.lbllv.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbllv.Name = "lbllv";
			this.lbllv.Size = new System.Drawing.Size(478, 20);
			this.lbllv.TabIndex = 4;
			this.lbllv.Text = "Items selected in the following list will be selected in the editor.";
			// 
			// frmFind
			// 
			this.AcceptButton = this.btnFind;
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(694, 472);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnFind);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.lv);
			this.Controls.Add(this.txtSearch);
			this.Controls.Add(this.lbllv);
			this.Controls.Add(this.lblSearch);
			this.Controls.Add(this.grpMedia);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.Gainsboro;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "frmFind";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Find";
			this.grpMedia.ResumeLayout(false);
			this.grpMedia.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox grpMedia;
		private System.Windows.Forms.TextBox txtMediaLink;
		private System.Windows.Forms.TextBox txtMediaAudio;
		private System.Windows.Forms.TextBox txtMediaVideo;
		private System.Windows.Forms.TextBox txtMediaImage;
		private System.Windows.Forms.CheckBox chkMediaLink;
		private System.Windows.Forms.CheckBox chkMediaAudio;
		private System.Windows.Forms.CheckBox chkMediaVideo;
		private System.Windows.Forms.CheckBox chkMediaImage;
		private System.Windows.Forms.Label lblSearch;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.ListView lv;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.ColumnHeader colType;
		private System.Windows.Forms.ColumnHeader colText;
		private System.Windows.Forms.Button btnFind;
		private System.Windows.Forms.Label lbllv;
	}
}
