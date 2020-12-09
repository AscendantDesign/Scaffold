using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	frmPPPlaceholderToTextbox																								*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// PowerPoint Content Placeholder To Textboxes user input form.
	/// </summary>
	public class frmPPPlaceholderToTextbox : ThemedForm
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		private System.Windows.Forms.GroupBox grpScope;
		private System.Windows.Forms.TextBox txtScopeIndex;
		private System.Windows.Forms.RadioButton optScopeIndex;
		private System.Windows.Forms.RadioButton optScopeCurrent;
		private System.Windows.Forms.RadioButton optScopeAll;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox grpResolution;
		private System.Windows.Forms.RadioButton optResolutionPlaceholder;
		private System.Windows.Forms.RadioButton optResolutionAllText;

		//*-----------------------------------------------------------------------*
		//* btnCancel_Click																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The cancel button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnCancel_Click(object sender, EventArgs e)
		{
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
			this.DialogResult = DialogResult.OK;
			this.Hide();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* InitializeComponent																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Initialize the form.
		/// </summary>
		private void InitializeComponent()
		{
			this.grpScope = new System.Windows.Forms.GroupBox();
			this.txtScopeIndex = new System.Windows.Forms.TextBox();
			this.optScopeIndex = new System.Windows.Forms.RadioButton();
			this.optScopeCurrent = new System.Windows.Forms.RadioButton();
			this.optScopeAll = new System.Windows.Forms.RadioButton();
			this.grpResolution = new System.Windows.Forms.GroupBox();
			this.optResolutionPlaceholder = new System.Windows.Forms.RadioButton();
			this.optResolutionAllText = new System.Windows.Forms.RadioButton();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.pnlMain.SuspendLayout();
			this.grpScope.SuspendLayout();
			this.grpResolution.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.btnOK);
			this.pnlMain.Controls.Add(this.btnCancel);
			this.pnlMain.Controls.Add(this.grpResolution);
			this.pnlMain.Controls.Add(this.grpScope);
			this.pnlMain.Size = new System.Drawing.Size(540, 211);
			this.pnlMain.Controls.SetChildIndex(this.grpScope, 0);
			this.pnlMain.Controls.SetChildIndex(this.grpResolution, 0);
			this.pnlMain.Controls.SetChildIndex(this.btnCancel, 0);
			this.pnlMain.Controls.SetChildIndex(this.btnOK, 0);
			// 
			// grpScope
			// 
			this.grpScope.Controls.Add(this.txtScopeIndex);
			this.grpScope.Controls.Add(this.optScopeIndex);
			this.grpScope.Controls.Add(this.optScopeCurrent);
			this.grpScope.Controls.Add(this.optScopeAll);
			this.grpScope.ForeColor = System.Drawing.Color.Gainsboro;
			this.grpScope.Location = new System.Drawing.Point(12, 13);
			this.grpScope.Name = "grpScope";
			this.grpScope.Size = new System.Drawing.Size(244, 130);
			this.grpScope.TabIndex = 0;
			this.grpScope.TabStop = false;
			this.grpScope.Text = "Conversion Scope";
			// 
			// txtScopeIndex
			// 
			this.txtScopeIndex.BackColor = System.Drawing.SystemColors.Control;
			this.txtScopeIndex.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.txtScopeIndex.Location = new System.Drawing.Point(162, 85);
			this.txtScopeIndex.Name = "txtScopeIndex";
			this.txtScopeIndex.ReadOnly = true;
			this.txtScopeIndex.Size = new System.Drawing.Size(66, 27);
			this.txtScopeIndex.TabIndex = 3;
			this.txtScopeIndex.TabStop = false;
			this.txtScopeIndex.Text = "0";
			// 
			// optScopeIndex
			// 
			this.optScopeIndex.AutoSize = true;
			this.optScopeIndex.Location = new System.Drawing.Point(25, 86);
			this.optScopeIndex.Name = "optScopeIndex";
			this.optScopeIndex.Size = new System.Drawing.Size(131, 24);
			this.optScopeIndex.TabIndex = 2;
			this.optScopeIndex.Text = "Slide &Number";
			this.optScopeIndex.UseVisualStyleBackColor = true;
			// 
			// optScopeCurrent
			// 
			this.optScopeCurrent.AutoSize = true;
			this.optScopeCurrent.Checked = true;
			this.optScopeCurrent.Location = new System.Drawing.Point(25, 56);
			this.optScopeCurrent.Name = "optScopeCurrent";
			this.optScopeCurrent.Size = new System.Drawing.Size(167, 24);
			this.optScopeCurrent.TabIndex = 1;
			this.optScopeCurrent.TabStop = true;
			this.optScopeCurrent.Text = "&Current Slide Only";
			this.optScopeCurrent.UseVisualStyleBackColor = true;
			// 
			// optScopeAll
			// 
			this.optScopeAll.AutoSize = true;
			this.optScopeAll.Location = new System.Drawing.Point(25, 26);
			this.optScopeAll.Name = "optScopeAll";
			this.optScopeAll.Size = new System.Drawing.Size(100, 24);
			this.optScopeAll.TabIndex = 0;
			this.optScopeAll.Text = "&All Slides";
			this.optScopeAll.UseVisualStyleBackColor = true;
			// 
			// grpResolution
			// 
			this.grpResolution.Controls.Add(this.optResolutionPlaceholder);
			this.grpResolution.Controls.Add(this.optResolutionAllText);
			this.grpResolution.ForeColor = System.Drawing.Color.Gainsboro;
			this.grpResolution.Location = new System.Drawing.Point(262, 13);
			this.grpResolution.Name = "grpResolution";
			this.grpResolution.Size = new System.Drawing.Size(255, 130);
			this.grpResolution.TabIndex = 1;
			this.grpResolution.TabStop = false;
			this.grpResolution.Text = "Conversion Resolution";
			// 
			// optResolutionPlaceholder
			// 
			this.optResolutionPlaceholder.AutoSize = true;
			this.optResolutionPlaceholder.Checked = true;
			this.optResolutionPlaceholder.Location = new System.Drawing.Point(25, 56);
			this.optResolutionPlaceholder.Name = "optResolutionPlaceholder";
			this.optResolutionPlaceholder.Size = new System.Drawing.Size(220, 24);
			this.optResolutionPlaceholder.TabIndex = 1;
			this.optResolutionPlaceholder.TabStop = true;
			this.optResolutionPlaceholder.Text = "Content Placeholder &Only";
			this.optResolutionPlaceholder.UseVisualStyleBackColor = true;
			// 
			// optResolutionAllText
			// 
			this.optResolutionAllText.AutoSize = true;
			this.optResolutionAllText.Location = new System.Drawing.Point(25, 26);
			this.optResolutionAllText.Name = "optResolutionAllText";
			this.optResolutionAllText.Size = new System.Drawing.Size(153, 24);
			this.optResolutionAllText.TabIndex = 0;
			this.optResolutionAllText.Text = "All &Multiline Text";
			this.optResolutionAllText.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(327, 149);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(92, 39);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(425, 149);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(92, 39);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "&OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// frmPPPlaceholderToTextbox
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(540, 259);
			this.Name = "frmPPPlaceholderToTextbox";
			this.pnlMain.ResumeLayout(false);
			this.pnlMain.PerformLayout();
			this.grpScope.ResumeLayout(false);
			this.grpScope.PerformLayout();
			this.grpResolution.ResumeLayout(false);
			this.grpResolution.PerformLayout();
			this.ResumeLayout(false);

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* optScopeIndex_CheckedChanged																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state of the option control has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void optScopeIndex_CheckedChanged(object sender, EventArgs e)
		{
			if(optScopeIndex.Checked)
			{
				txtScopeIndex.ReadOnly = false;
				txtScopeIndex.TabStop = true;
				txtScopeIndex.BackColor = SystemColors.Window;
				txtScopeIndex.ForeColor = SystemColors.ControlText;
			}
			else
			{
				txtScopeIndex.ReadOnly = true;
				txtScopeIndex.TabStop = false;
				txtScopeIndex.BackColor = SystemColors.Control;
				txtScopeIndex.ForeColor = SystemColors.ControlDark;
			}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* Dispose																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">
		/// True if managed resources should be disposed. Otherwise, false.
		/// </param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the frmPPPlaceholderToTextbox Item.
		/// </summary>
		public frmPPPlaceholderToTextbox()
		{
			InitializeComponent();
			this.StartPosition = FormStartPosition.CenterParent;
			this.Title = "Content Placeholder to Textboxes";
			this.statusThemedForm.Visible = false;

			optScopeIndex.CheckedChanged += optScopeIndex_CheckedChanged;
			btnCancel.Click += btnCancel_Click;
			btnOK.Click += btnOK_Click;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PlaceholderOnly																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get a value indicating whether to handle only the content placeholder.
		/// </summary>
		/// <remarks>
		/// If false, all multi-line textboxes will be converted to individual
		/// single line textboxes.
		/// </remarks>
		public bool PlaceholderOnly
		{
			get { return optResolutionPlaceholder.Checked; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SlideScope																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the scope of slides to which this conversion will apply.
		/// </summary>
		public SlideScopeEnum SlideScope
		{
			get
			{
				SlideScopeEnum result = SlideScopeEnum.None;
				if(optScopeAll.Checked)
				{
					result = SlideScopeEnum.All;
				}
				else if(optScopeCurrent.Checked)
				{
					result = SlideScopeEnum.Current;
				}
				else if(optScopeIndex.Checked)
				{
					result = SlideScopeEnum.Custom;
				}
				return result;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SlideScopeText																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the custom text for the selected slide scope.
		/// </summary>
		public string SlideScopeText
		{
			get { return txtScopeIndex.Text; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*


}
