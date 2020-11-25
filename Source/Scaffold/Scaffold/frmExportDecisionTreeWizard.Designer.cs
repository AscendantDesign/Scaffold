//	frmExportDecisionTreeWizard.Designer.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
namespace Scaffold
{
	partial class frmExportDecisionTreeWizard
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
			this.tctl = new Scaffold.WizardTabControl();
			this.tpgFile = new System.Windows.Forms.TabPage();
			this.btnFilename = new System.Windows.Forms.Button();
			this.txtFilename = new System.Windows.Forms.TextBox();
			this.lblFilename = new System.Windows.Forms.Label();
			this.optFileOpenExisting = new System.Windows.Forms.RadioButton();
			this.optFileCreateNew = new System.Windows.Forms.RadioButton();
			this.lblFile = new System.Windows.Forms.Label();
			this.tpgPageSettings = new System.Windows.Forms.TabPage();
			this.cmboAnswerOffsetY = new System.Windows.Forms.ComboBox();
			this.txtAnswerOffsetY = new System.Windows.Forms.TextBox();
			this.cmboQuestionOffsetY = new System.Windows.Forms.ComboBox();
			this.txtQuestionOffsetY = new System.Windows.Forms.TextBox();
			this.cmboAnswerOffsetX = new System.Windows.Forms.ComboBox();
			this.cmboQuestionOffsetX = new System.Windows.Forms.ComboBox();
			this.lblAnswerOffsetY = new System.Windows.Forms.Label();
			this.txtAnswerOffsetX = new System.Windows.Forms.TextBox();
			this.lblQuestionOffsetY = new System.Windows.Forms.Label();
			this.lblAnswerOffsetX = new System.Windows.Forms.Label();
			this.txtQuestionOffsetX = new System.Windows.Forms.TextBox();
			this.lblQuestionOffsetX = new System.Windows.Forms.Label();
			this.txtStartingPageNumber = new System.Windows.Forms.TextBox();
			this.lblStartingPageNumber = new System.Windows.Forms.Label();
			this.chkPageSettingsAllow = new System.Windows.Forms.CheckBox();
			this.lblPageSettingsQuestion = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.tpgFinished = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnBack = new System.Windows.Forms.Button();
			this.btnNext = new System.Windows.Forms.Button();
			this.btnFinish = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tctl.SuspendLayout();
			this.tpgFile.SuspendLayout();
			this.tpgPageSettings.SuspendLayout();
			this.tpgFinished.SuspendLayout();
			this.SuspendLayout();
			// 
			// tctl
			// 
			this.tctl.Controls.Add(this.tpgFile);
			this.tctl.Controls.Add(this.tpgPageSettings);
			this.tctl.Controls.Add(this.tpgFinished);
			this.tctl.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tctl.Location = new System.Drawing.Point(15, 15);
			this.tctl.Margin = new System.Windows.Forms.Padding(0);
			this.tctl.Multiline = true;
			this.tctl.Name = "tctl";
			this.tctl.SelectedIndex = 0;
			this.tctl.Size = new System.Drawing.Size(749, 460);
			this.tctl.TabIndex = 0;
			// 
			// tpgFile
			// 
			this.tpgFile.Controls.Add(this.btnFilename);
			this.tpgFile.Controls.Add(this.txtFilename);
			this.tpgFile.Controls.Add(this.lblFilename);
			this.tpgFile.Controls.Add(this.optFileOpenExisting);
			this.tpgFile.Controls.Add(this.optFileCreateNew);
			this.tpgFile.Controls.Add(this.lblFile);
			this.tpgFile.Location = new System.Drawing.Point(4, 30);
			this.tpgFile.Margin = new System.Windows.Forms.Padding(4);
			this.tpgFile.Name = "tpgFile";
			this.tpgFile.Padding = new System.Windows.Forms.Padding(4);
			this.tpgFile.Size = new System.Drawing.Size(741, 426);
			this.tpgFile.TabIndex = 0;
			this.tpgFile.Text = "File";
			this.tpgFile.UseVisualStyleBackColor = true;
			// 
			// btnFilename
			// 
			this.btnFilename.Enabled = false;
			this.btnFilename.Location = new System.Drawing.Point(680, 163);
			this.btnFilename.Name = "btnFilename";
			this.btnFilename.Size = new System.Drawing.Size(42, 34);
			this.btnFilename.TabIndex = 5;
			this.btnFilename.Text = "...";
			this.btnFilename.UseVisualStyleBackColor = true;
			this.btnFilename.Click += new System.EventHandler(this.btnFilename_Click);
			// 
			// txtFilename
			// 
			this.txtFilename.Enabled = false;
			this.txtFilename.Location = new System.Drawing.Point(162, 167);
			this.txtFilename.Name = "txtFilename";
			this.txtFilename.Size = new System.Drawing.Size(512, 28);
			this.txtFilename.TabIndex = 4;
			// 
			// lblFilename
			// 
			this.lblFilename.AutoSize = true;
			this.lblFilename.Enabled = false;
			this.lblFilename.Location = new System.Drawing.Point(73, 170);
			this.lblFilename.Name = "lblFilename";
			this.lblFilename.Size = new System.Drawing.Size(83, 21);
			this.lblFilename.TabIndex = 3;
			this.lblFilename.Text = "Filename:";
			// 
			// optFileOpenExisting
			// 
			this.optFileOpenExisting.AutoSize = true;
			this.optFileOpenExisting.Location = new System.Drawing.Point(48, 132);
			this.optFileOpenExisting.Name = "optFileOpenExisting";
			this.optFileOpenExisting.Size = new System.Drawing.Size(164, 25);
			this.optFileOpenExisting.TabIndex = 2;
			this.optFileOpenExisting.Text = "&Open Existing File";
			this.optFileOpenExisting.UseVisualStyleBackColor = true;
			// 
			// optFileCreateNew
			// 
			this.optFileCreateNew.AutoSize = true;
			this.optFileCreateNew.Checked = true;
			this.optFileCreateNew.Location = new System.Drawing.Point(48, 86);
			this.optFileCreateNew.Name = "optFileCreateNew";
			this.optFileCreateNew.Size = new System.Drawing.Size(149, 25);
			this.optFileCreateNew.TabIndex = 1;
			this.optFileCreateNew.TabStop = true;
			this.optFileCreateNew.Text = "Create &New File";
			this.optFileCreateNew.UseVisualStyleBackColor = true;
			this.optFileCreateNew.CheckedChanged += new System.EventHandler(this.optFile_CheckedChanged);
			// 
			// lblFile
			// 
			this.lblFile.AutoSize = true;
			this.lblFile.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFile.Location = new System.Drawing.Point(8, 28);
			this.lblFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblFile.Name = "lblFile";
			this.lblFile.Size = new System.Drawing.Size(207, 29);
			this.lblFile.TabIndex = 0;
			this.lblFile.Text = "File Information";
			// 
			// tpgPageSettings
			// 
			this.tpgPageSettings.Controls.Add(this.cmboAnswerOffsetY);
			this.tpgPageSettings.Controls.Add(this.txtAnswerOffsetY);
			this.tpgPageSettings.Controls.Add(this.cmboQuestionOffsetY);
			this.tpgPageSettings.Controls.Add(this.txtQuestionOffsetY);
			this.tpgPageSettings.Controls.Add(this.cmboAnswerOffsetX);
			this.tpgPageSettings.Controls.Add(this.cmboQuestionOffsetX);
			this.tpgPageSettings.Controls.Add(this.lblAnswerOffsetY);
			this.tpgPageSettings.Controls.Add(this.txtAnswerOffsetX);
			this.tpgPageSettings.Controls.Add(this.lblQuestionOffsetY);
			this.tpgPageSettings.Controls.Add(this.lblAnswerOffsetX);
			this.tpgPageSettings.Controls.Add(this.txtQuestionOffsetX);
			this.tpgPageSettings.Controls.Add(this.lblQuestionOffsetX);
			this.tpgPageSettings.Controls.Add(this.txtStartingPageNumber);
			this.tpgPageSettings.Controls.Add(this.lblStartingPageNumber);
			this.tpgPageSettings.Controls.Add(this.chkPageSettingsAllow);
			this.tpgPageSettings.Controls.Add(this.lblPageSettingsQuestion);
			this.tpgPageSettings.Controls.Add(this.label3);
			this.tpgPageSettings.Location = new System.Drawing.Point(4, 30);
			this.tpgPageSettings.Name = "tpgPageSettings";
			this.tpgPageSettings.Size = new System.Drawing.Size(741, 426);
			this.tpgPageSettings.TabIndex = 2;
			this.tpgPageSettings.Text = "PageSettings";
			this.tpgPageSettings.UseVisualStyleBackColor = true;
			// 
			// cmboAnswerOffsetY
			// 
			this.cmboAnswerOffsetY.Enabled = false;
			this.cmboAnswerOffsetY.FormattingEnabled = true;
			this.cmboAnswerOffsetY.Items.AddRange(new object[] {
            "From top",
            "From bottom"});
			this.cmboAnswerOffsetY.Location = new System.Drawing.Point(377, 331);
			this.cmboAnswerOffsetY.Name = "cmboAnswerOffsetY";
			this.cmboAnswerOffsetY.Size = new System.Drawing.Size(145, 29);
			this.cmboAnswerOffsetY.TabIndex = 16;
			// 
			// txtAnswerOffsetY
			// 
			this.txtAnswerOffsetY.Enabled = false;
			this.txtAnswerOffsetY.Location = new System.Drawing.Point(257, 331);
			this.txtAnswerOffsetY.Name = "txtAnswerOffsetY";
			this.txtAnswerOffsetY.Size = new System.Drawing.Size(100, 28);
			this.txtAnswerOffsetY.TabIndex = 15;
			this.txtAnswerOffsetY.Text = "10.00";
			// 
			// cmboQuestionOffsetY
			// 
			this.cmboQuestionOffsetY.Enabled = false;
			this.cmboQuestionOffsetY.FormattingEnabled = true;
			this.cmboQuestionOffsetY.Items.AddRange(new object[] {
            "From top",
            "From bottom"});
			this.cmboQuestionOffsetY.Location = new System.Drawing.Point(377, 247);
			this.cmboQuestionOffsetY.Name = "cmboQuestionOffsetY";
			this.cmboQuestionOffsetY.Size = new System.Drawing.Size(145, 29);
			this.cmboQuestionOffsetY.TabIndex = 10;
			// 
			// txtQuestionOffsetY
			// 
			this.txtQuestionOffsetY.Enabled = false;
			this.txtQuestionOffsetY.Location = new System.Drawing.Point(257, 247);
			this.txtQuestionOffsetY.Name = "txtQuestionOffsetY";
			this.txtQuestionOffsetY.Size = new System.Drawing.Size(100, 28);
			this.txtQuestionOffsetY.TabIndex = 9;
			this.txtQuestionOffsetY.Text = "10.00";
			// 
			// cmboAnswerOffsetX
			// 
			this.cmboAnswerOffsetX.Enabled = false;
			this.cmboAnswerOffsetX.FormattingEnabled = true;
			this.cmboAnswerOffsetX.Items.AddRange(new object[] {
            "From left",
            "From right"});
			this.cmboAnswerOffsetX.Location = new System.Drawing.Point(377, 296);
			this.cmboAnswerOffsetX.Name = "cmboAnswerOffsetX";
			this.cmboAnswerOffsetX.Size = new System.Drawing.Size(145, 29);
			this.cmboAnswerOffsetX.TabIndex = 13;
			// 
			// cmboQuestionOffsetX
			// 
			this.cmboQuestionOffsetX.Enabled = false;
			this.cmboQuestionOffsetX.FormattingEnabled = true;
			this.cmboQuestionOffsetX.Items.AddRange(new object[] {
            "From left",
            "From right"});
			this.cmboQuestionOffsetX.Location = new System.Drawing.Point(377, 212);
			this.cmboQuestionOffsetX.Name = "cmboQuestionOffsetX";
			this.cmboQuestionOffsetX.Size = new System.Drawing.Size(145, 29);
			this.cmboQuestionOffsetX.TabIndex = 7;
			// 
			// lblAnswerOffsetY
			// 
			this.lblAnswerOffsetY.AutoSize = true;
			this.lblAnswerOffsetY.Enabled = false;
			this.lblAnswerOffsetY.Location = new System.Drawing.Point(75, 334);
			this.lblAnswerOffsetY.Name = "lblAnswerOffsetY";
			this.lblAnswerOffsetY.Size = new System.Drawing.Size(175, 21);
			this.lblAnswerOffsetY.TabIndex = 14;
			this.lblAnswerOffsetY.Text = "First Answer Offset Y:";
			// 
			// txtAnswerOffsetX
			// 
			this.txtAnswerOffsetX.Enabled = false;
			this.txtAnswerOffsetX.Location = new System.Drawing.Point(257, 296);
			this.txtAnswerOffsetX.Name = "txtAnswerOffsetX";
			this.txtAnswerOffsetX.Size = new System.Drawing.Size(100, 28);
			this.txtAnswerOffsetX.TabIndex = 12;
			this.txtAnswerOffsetX.Text = "10.00";
			// 
			// lblQuestionOffsetY
			// 
			this.lblQuestionOffsetY.AutoSize = true;
			this.lblQuestionOffsetY.Enabled = false;
			this.lblQuestionOffsetY.Location = new System.Drawing.Point(75, 250);
			this.lblQuestionOffsetY.Name = "lblQuestionOffsetY";
			this.lblQuestionOffsetY.Size = new System.Drawing.Size(147, 21);
			this.lblQuestionOffsetY.TabIndex = 8;
			this.lblQuestionOffsetY.Text = "Question Offset Y:";
			// 
			// lblAnswerOffsetX
			// 
			this.lblAnswerOffsetX.AutoSize = true;
			this.lblAnswerOffsetX.Enabled = false;
			this.lblAnswerOffsetX.Location = new System.Drawing.Point(75, 299);
			this.lblAnswerOffsetX.Name = "lblAnswerOffsetX";
			this.lblAnswerOffsetX.Size = new System.Drawing.Size(175, 21);
			this.lblAnswerOffsetX.TabIndex = 11;
			this.lblAnswerOffsetX.Text = "First Answer Offset X:";
			// 
			// txtQuestionOffsetX
			// 
			this.txtQuestionOffsetX.Enabled = false;
			this.txtQuestionOffsetX.Location = new System.Drawing.Point(257, 212);
			this.txtQuestionOffsetX.Name = "txtQuestionOffsetX";
			this.txtQuestionOffsetX.Size = new System.Drawing.Size(100, 28);
			this.txtQuestionOffsetX.TabIndex = 6;
			this.txtQuestionOffsetX.Text = "10.00";
			// 
			// lblQuestionOffsetX
			// 
			this.lblQuestionOffsetX.AutoSize = true;
			this.lblQuestionOffsetX.Enabled = false;
			this.lblQuestionOffsetX.Location = new System.Drawing.Point(75, 215);
			this.lblQuestionOffsetX.Name = "lblQuestionOffsetX";
			this.lblQuestionOffsetX.Size = new System.Drawing.Size(147, 21);
			this.lblQuestionOffsetX.TabIndex = 5;
			this.lblQuestionOffsetX.Text = "Question Offset X:";
			// 
			// txtStartingPageNumber
			// 
			this.txtStartingPageNumber.Enabled = false;
			this.txtStartingPageNumber.Location = new System.Drawing.Point(257, 178);
			this.txtStartingPageNumber.Name = "txtStartingPageNumber";
			this.txtStartingPageNumber.Size = new System.Drawing.Size(100, 28);
			this.txtStartingPageNumber.TabIndex = 4;
			this.txtStartingPageNumber.Text = "1";
			// 
			// lblStartingPageNumber
			// 
			this.lblStartingPageNumber.AutoSize = true;
			this.lblStartingPageNumber.Enabled = false;
			this.lblStartingPageNumber.Location = new System.Drawing.Point(75, 181);
			this.lblStartingPageNumber.Name = "lblStartingPageNumber";
			this.lblStartingPageNumber.Size = new System.Drawing.Size(176, 21);
			this.lblStartingPageNumber.TabIndex = 3;
			this.lblStartingPageNumber.Text = "Starting page number:";
			// 
			// chkPageSettingsAllow
			// 
			this.chkPageSettingsAllow.AutoSize = true;
			this.chkPageSettingsAllow.Location = new System.Drawing.Point(48, 144);
			this.chkPageSettingsAllow.Name = "chkPageSettingsAllow";
			this.chkPageSettingsAllow.Size = new System.Drawing.Size(395, 25);
			this.chkPageSettingsAllow.TabIndex = 2;
			this.chkPageSettingsAllow.Text = "Allow automatic placement of unassigned nodes.";
			this.chkPageSettingsAllow.UseVisualStyleBackColor = true;
			this.chkPageSettingsAllow.CheckedChanged += new System.EventHandler(this.chkPageSettingsAllow_CheckedChanged);
			// 
			// lblPageSettingsQuestion
			// 
			this.lblPageSettingsQuestion.AutoSize = true;
			this.lblPageSettingsQuestion.Location = new System.Drawing.Point(48, 86);
			this.lblPageSettingsQuestion.MaximumSize = new System.Drawing.Size(650, 0);
			this.lblPageSettingsQuestion.Name = "lblPageSettingsQuestion";
			this.lblPageSettingsQuestion.Size = new System.Drawing.Size(645, 42);
			this.lblPageSettingsQuestion.TabIndex = 1;
			this.lblPageSettingsQuestion.Text = "Some of the nodes in your diagram don\'t have storyboard page settings assigned. D" +
    "o you wish to allow them to be placed automatically?";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(8, 28);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(322, 29);
			this.label3.TabIndex = 0;
			this.label3.Text = "Incomplete Page Settings";
			// 
			// tpgFinished
			// 
			this.tpgFinished.Controls.Add(this.label2);
			this.tpgFinished.Controls.Add(this.label1);
			this.tpgFinished.Location = new System.Drawing.Point(4, 30);
			this.tpgFinished.Margin = new System.Windows.Forms.Padding(4);
			this.tpgFinished.Name = "tpgFinished";
			this.tpgFinished.Padding = new System.Windows.Forms.Padding(4);
			this.tpgFinished.Size = new System.Drawing.Size(741, 426);
			this.tpgFinished.TabIndex = 1;
			this.tpgFinished.Text = "Finished";
			this.tpgFinished.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(48, 86);
			this.label2.MaximumSize = new System.Drawing.Size(650, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(638, 42);
			this.label2.TabIndex = 1;
			this.label2.Text = "The wizard has finished collecting information and is ready to complete the proce" +
    "ss. Press [Enter] or click finish to continue your export.";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 28);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(390, 29);
			this.label1.TabIndex = 0;
			this.label1.Text = "Finished Collecting Information";
			// 
			// btnBack
			// 
			this.btnBack.Enabled = false;
			this.btnBack.Location = new System.Drawing.Point(523, 492);
			this.btnBack.Name = "btnBack";
			this.btnBack.Size = new System.Drawing.Size(75, 40);
			this.btnBack.TabIndex = 2;
			this.btnBack.Text = "&Back";
			this.btnBack.UseVisualStyleBackColor = true;
			this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
			// 
			// btnNext
			// 
			this.btnNext.Location = new System.Drawing.Point(604, 492);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(75, 40);
			this.btnNext.TabIndex = 3;
			this.btnNext.Text = "&Next";
			this.btnNext.UseVisualStyleBackColor = true;
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// btnFinish
			// 
			this.btnFinish.Enabled = false;
			this.btnFinish.Location = new System.Drawing.Point(685, 492);
			this.btnFinish.Name = "btnFinish";
			this.btnFinish.Size = new System.Drawing.Size(75, 40);
			this.btnFinish.TabIndex = 4;
			this.btnFinish.Text = "&Finish";
			this.btnFinish.UseVisualStyleBackColor = true;
			this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(442, 492);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 40);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// frmExportDecisionTreeWizard
			// 
			this.AcceptButton = this.btnFinish;
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(779, 562);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnFinish);
			this.Controls.Add(this.btnNext);
			this.Controls.Add(this.btnBack);
			this.Controls.Add(this.tctl);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "frmExportDecisionTreeWizard";
			this.Text = "frmExportDecisionTreeWizard";
			this.tctl.ResumeLayout(false);
			this.tpgFile.ResumeLayout(false);
			this.tpgFile.PerformLayout();
			this.tpgPageSettings.ResumeLayout(false);
			this.tpgPageSettings.PerformLayout();
			this.tpgFinished.ResumeLayout(false);
			this.tpgFinished.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private WizardTabControl tctl;
		private System.Windows.Forms.TabPage tpgFile;
		private System.Windows.Forms.TabPage tpgFinished;
		private System.Windows.Forms.Button btnFilename;
		private System.Windows.Forms.TextBox txtFilename;
		private System.Windows.Forms.Label lblFilename;
		private System.Windows.Forms.RadioButton optFileOpenExisting;
		private System.Windows.Forms.RadioButton optFileCreateNew;
		private System.Windows.Forms.Label lblFile;
		private System.Windows.Forms.Button btnBack;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnNext;
		private System.Windows.Forms.Button btnFinish;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TabPage tpgPageSettings;
		private System.Windows.Forms.ComboBox cmboAnswerOffsetY;
		private System.Windows.Forms.TextBox txtAnswerOffsetY;
		private System.Windows.Forms.ComboBox cmboQuestionOffsetY;
		private System.Windows.Forms.TextBox txtQuestionOffsetY;
		private System.Windows.Forms.ComboBox cmboAnswerOffsetX;
		private System.Windows.Forms.ComboBox cmboQuestionOffsetX;
		private System.Windows.Forms.Label lblAnswerOffsetY;
		private System.Windows.Forms.TextBox txtAnswerOffsetX;
		private System.Windows.Forms.Label lblQuestionOffsetY;
		private System.Windows.Forms.Label lblAnswerOffsetX;
		private System.Windows.Forms.TextBox txtQuestionOffsetX;
		private System.Windows.Forms.Label lblQuestionOffsetX;
		private System.Windows.Forms.TextBox txtStartingPageNumber;
		private System.Windows.Forms.Label lblStartingPageNumber;
		private System.Windows.Forms.CheckBox chkPageSettingsAllow;
		private System.Windows.Forms.Label lblPageSettingsQuestion;
		private System.Windows.Forms.Label label3;
	}
}