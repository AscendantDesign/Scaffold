using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	frmPPAlignment																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// User input for the PowerPoint / Text And Shape Alignment function..
	/// </summary>
	public class frmPPAlignment : ThemedForm
	{
		//	TODO: !1 - Stopped here.
		//	TODO: Finish designing alignment form.
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		private TextBox txtSlide;
		private Label lblSlide;
		private Button btnCancel;
		private ListView lvShapes;
		private Label lblShapes;
		private GroupBox grpDistribute;
		private Panel pnlDistributeHorz;
		private Panel pnlDistributeVert;
		private RadioButton optDistributeHorz;
		private RadioButton optDistributeVert;
		private GroupBox grpAlign;
		private Panel pnlAlignBottom;
		private Panel pnlAlignRight;
		private Panel pnlAlignTop;
		private Panel pnlAlignLeft;
		private RadioButton optAlignBottom;
		private RadioButton optAlignRight;
		private RadioButton optAlignTop;
		private RadioButton optAlignLeft;
		private Button btnOK;
		private TextBox txtAnchor;
		private Label lblAnchor;

		//*-----------------------------------------------------------------------*
		//* InitializeComponent																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Initialize the form.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblSlide = new System.Windows.Forms.Label();
			this.txtSlide = new System.Windows.Forms.TextBox();
			this.lblShapes = new System.Windows.Forms.Label();
			this.lvShapes = new System.Windows.Forms.ListView();
			this.btnCancel = new System.Windows.Forms.Button();
			this.grpAlign = new System.Windows.Forms.GroupBox();
			this.optAlignLeft = new System.Windows.Forms.RadioButton();
			this.pnlAlignLeft = new System.Windows.Forms.Panel();
			this.optAlignTop = new System.Windows.Forms.RadioButton();
			this.pnlAlignTop = new System.Windows.Forms.Panel();
			this.optAlignRight = new System.Windows.Forms.RadioButton();
			this.pnlAlignRight = new System.Windows.Forms.Panel();
			this.optAlignBottom = new System.Windows.Forms.RadioButton();
			this.pnlAlignBottom = new System.Windows.Forms.Panel();
			this.grpDistribute = new System.Windows.Forms.GroupBox();
			this.pnlDistributeHorz = new System.Windows.Forms.Panel();
			this.pnlDistributeVert = new System.Windows.Forms.Panel();
			this.optDistributeHorz = new System.Windows.Forms.RadioButton();
			this.optDistributeVert = new System.Windows.Forms.RadioButton();
			this.lblAnchor = new System.Windows.Forms.Label();
			this.txtAnchor = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.pnlMain.SuspendLayout();
			this.grpAlign.SuspendLayout();
			this.grpDistribute.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.grpDistribute);
			this.pnlMain.Controls.Add(this.grpAlign);
			this.pnlMain.Controls.Add(this.btnOK);
			this.pnlMain.Controls.Add(this.btnCancel);
			this.pnlMain.Controls.Add(this.lvShapes);
			this.pnlMain.Controls.Add(this.lblShapes);
			this.pnlMain.Controls.Add(this.txtAnchor);
			this.pnlMain.Controls.Add(this.txtSlide);
			this.pnlMain.Controls.Add(this.lblAnchor);
			this.pnlMain.Controls.Add(this.lblSlide);
			this.pnlMain.Size = new System.Drawing.Size(533, 418);
			this.pnlMain.Controls.SetChildIndex(this.lblSlide, 0);
			this.pnlMain.Controls.SetChildIndex(this.lblAnchor, 0);
			this.pnlMain.Controls.SetChildIndex(this.txtSlide, 0);
			this.pnlMain.Controls.SetChildIndex(this.txtAnchor, 0);
			this.pnlMain.Controls.SetChildIndex(this.lblShapes, 0);
			this.pnlMain.Controls.SetChildIndex(this.lvShapes, 0);
			this.pnlMain.Controls.SetChildIndex(this.btnCancel, 0);
			this.pnlMain.Controls.SetChildIndex(this.btnOK, 0);
			this.pnlMain.Controls.SetChildIndex(this.grpAlign, 0);
			this.pnlMain.Controls.SetChildIndex(this.grpDistribute, 0);
			// 
			// lblSlide
			// 
			this.lblSlide.AutoSize = true;
			this.lblSlide.ForeColor = System.Drawing.Color.Gainsboro;
			this.lblSlide.Location = new System.Drawing.Point(12, 10);
			this.lblSlide.Name = "lblSlide";
			this.lblSlide.Size = new System.Drawing.Size(51, 20);
			this.lblSlide.TabIndex = 0;
			this.lblSlide.Text = "&Slide:";
			// 
			// txtSlide
			// 
			this.txtSlide.Location = new System.Drawing.Point(69, 7);
			this.txtSlide.Name = "txtSlide";
			this.txtSlide.Size = new System.Drawing.Size(73, 27);
			this.txtSlide.TabIndex = 1;
			this.txtSlide.Text = "0";
			// 
			// lblShapes
			// 
			this.lblShapes.AutoSize = true;
			this.lblShapes.ForeColor = System.Drawing.Color.Gainsboro;
			this.lblShapes.Location = new System.Drawing.Point(12, 50);
			this.lblShapes.Name = "lblShapes";
			this.lblShapes.Size = new System.Drawing.Size(70, 20);
			this.lblShapes.TabIndex = 2;
			this.lblShapes.Text = "S&hapes:";
			// 
			// lvShapes
			// 
			this.lvShapes.HideSelection = false;
			this.lvShapes.Location = new System.Drawing.Point(12, 74);
			this.lvShapes.Name = "lvShapes";
			this.lvShapes.Size = new System.Drawing.Size(242, 234);
			this.lvShapes.TabIndex = 3;
			this.lvShapes.UseCompatibleStateImageBehavior = false;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(338, 358);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(88, 45);
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// grpAlign
			// 
			this.grpAlign.Controls.Add(this.pnlAlignBottom);
			this.grpAlign.Controls.Add(this.pnlAlignRight);
			this.grpAlign.Controls.Add(this.pnlAlignTop);
			this.grpAlign.Controls.Add(this.pnlAlignLeft);
			this.grpAlign.Controls.Add(this.optAlignBottom);
			this.grpAlign.Controls.Add(this.optAlignRight);
			this.grpAlign.Controls.Add(this.optAlignTop);
			this.grpAlign.Controls.Add(this.optAlignLeft);
			this.grpAlign.ForeColor = System.Drawing.Color.Gainsboro;
			this.grpAlign.Location = new System.Drawing.Point(285, 38);
			this.grpAlign.Name = "grpAlign";
			this.grpAlign.Size = new System.Drawing.Size(236, 132);
			this.grpAlign.TabIndex = 6;
			this.grpAlign.TabStop = false;
			this.grpAlign.Text = "&Align";
			// 
			// optAlignLeft
			// 
			this.optAlignLeft.AutoSize = true;
			this.optAlignLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.optAlignLeft.Checked = true;
			this.optAlignLeft.Location = new System.Drawing.Point(38, 60);
			this.optAlignLeft.Name = "optAlignLeft";
			this.optAlignLeft.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.optAlignLeft.Size = new System.Drawing.Size(59, 24);
			this.optAlignLeft.TabIndex = 1;
			this.optAlignLeft.TabStop = true;
			this.optAlignLeft.Text = "&Left";
			this.optAlignLeft.UseVisualStyleBackColor = true;
			// 
			// pnlAlignLeft
			// 
			this.pnlAlignLeft.BackgroundImage = global::Scaffold.ResourceMain.AlignLeft32;
			this.pnlAlignLeft.Location = new System.Drawing.Point(3, 56);
			this.pnlAlignLeft.Margin = new System.Windows.Forms.Padding(0);
			this.pnlAlignLeft.Name = "pnlAlignLeft";
			this.pnlAlignLeft.Size = new System.Drawing.Size(32, 32);
			this.pnlAlignLeft.TabIndex = 0;
			// 
			// optAlignTop
			// 
			this.optAlignTop.AutoSize = true;
			this.optAlignTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.optAlignTop.Location = new System.Drawing.Point(123, 16);
			this.optAlignTop.Name = "optAlignTop";
			this.optAlignTop.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.optAlignTop.Size = new System.Drawing.Size(58, 24);
			this.optAlignTop.TabIndex = 3;
			this.optAlignTop.TabStop = true;
			this.optAlignTop.Text = "&Top";
			this.optAlignTop.UseVisualStyleBackColor = true;
			// 
			// pnlAlignTop
			// 
			this.pnlAlignTop.BackgroundImage = global::Scaffold.ResourceMain.AlignTop32;
			this.pnlAlignTop.Location = new System.Drawing.Point(88, 12);
			this.pnlAlignTop.Margin = new System.Windows.Forms.Padding(0);
			this.pnlAlignTop.Name = "pnlAlignTop";
			this.pnlAlignTop.Size = new System.Drawing.Size(32, 32);
			this.pnlAlignTop.TabIndex = 2;
			// 
			// optAlignRight
			// 
			this.optAlignRight.AutoSize = true;
			this.optAlignRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.optAlignRight.Location = new System.Drawing.Point(131, 60);
			this.optAlignRight.Name = "optAlignRight";
			this.optAlignRight.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.optAlignRight.Size = new System.Drawing.Size(69, 24);
			this.optAlignRight.TabIndex = 4;
			this.optAlignRight.TabStop = true;
			this.optAlignRight.Text = "&Right";
			this.optAlignRight.UseVisualStyleBackColor = false;
			// 
			// pnlAlignRight
			// 
			this.pnlAlignRight.BackgroundImage = global::Scaffold.ResourceMain.AlignRight32;
			this.pnlAlignRight.Location = new System.Drawing.Point(203, 56);
			this.pnlAlignRight.Margin = new System.Windows.Forms.Padding(0);
			this.pnlAlignRight.Name = "pnlAlignRight";
			this.pnlAlignRight.Size = new System.Drawing.Size(32, 32);
			this.pnlAlignRight.TabIndex = 5;
			// 
			// optAlignBottom
			// 
			this.optAlignBottom.AutoSize = true;
			this.optAlignBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.optAlignBottom.Location = new System.Drawing.Point(37, 100);
			this.optAlignBottom.Name = "optAlignBottom";
			this.optAlignBottom.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.optAlignBottom.Size = new System.Drawing.Size(84, 24);
			this.optAlignBottom.TabIndex = 6;
			this.optAlignBottom.TabStop = true;
			this.optAlignBottom.Text = "&Bottom";
			this.optAlignBottom.UseVisualStyleBackColor = false;
			// 
			// pnlAlignBottom
			// 
			this.pnlAlignBottom.BackgroundImage = global::Scaffold.ResourceMain.AlignBottom32;
			this.pnlAlignBottom.Location = new System.Drawing.Point(123, 96);
			this.pnlAlignBottom.Margin = new System.Windows.Forms.Padding(0);
			this.pnlAlignBottom.Name = "pnlAlignBottom";
			this.pnlAlignBottom.Size = new System.Drawing.Size(32, 32);
			this.pnlAlignBottom.TabIndex = 7;
			// 
			// grpDistribute
			// 
			this.grpDistribute.Controls.Add(this.pnlDistributeHorz);
			this.grpDistribute.Controls.Add(this.pnlDistributeVert);
			this.grpDistribute.Controls.Add(this.optDistributeHorz);
			this.grpDistribute.Controls.Add(this.optDistributeVert);
			this.grpDistribute.ForeColor = System.Drawing.Color.Gainsboro;
			this.grpDistribute.Location = new System.Drawing.Point(285, 176);
			this.grpDistribute.Name = "grpDistribute";
			this.grpDistribute.Size = new System.Drawing.Size(236, 132);
			this.grpDistribute.TabIndex = 7;
			this.grpDistribute.TabStop = false;
			this.grpDistribute.Text = "&Distribute";
			// 
			// pnlDistributeHorz
			// 
			this.pnlDistributeHorz.BackgroundImage = global::Scaffold.ResourceMain.DistributeHorz32;
			this.pnlDistributeHorz.Location = new System.Drawing.Point(41, 32);
			this.pnlDistributeHorz.Margin = new System.Windows.Forms.Padding(0);
			this.pnlDistributeHorz.Name = "pnlDistributeHorz";
			this.pnlDistributeHorz.Size = new System.Drawing.Size(32, 32);
			this.pnlDistributeHorz.TabIndex = 0;
			// 
			// pnlDistributeVert
			// 
			this.pnlDistributeVert.BackgroundImage = global::Scaffold.ResourceMain.DistributeVert32;
			this.pnlDistributeVert.Location = new System.Drawing.Point(41, 74);
			this.pnlDistributeVert.Margin = new System.Windows.Forms.Padding(0);
			this.pnlDistributeVert.Name = "pnlDistributeVert";
			this.pnlDistributeVert.Size = new System.Drawing.Size(32, 32);
			this.pnlDistributeVert.TabIndex = 2;
			// 
			// optDistributeHorz
			// 
			this.optDistributeHorz.AutoSize = true;
			this.optDistributeHorz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.optDistributeHorz.Location = new System.Drawing.Point(105, 39);
			this.optDistributeHorz.Name = "optDistributeHorz";
			this.optDistributeHorz.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.optDistributeHorz.Size = new System.Drawing.Size(107, 24);
			this.optDistributeHorz.TabIndex = 1;
			this.optDistributeHorz.Text = "&Horizontal";
			this.optDistributeHorz.UseVisualStyleBackColor = true;
			// 
			// optDistributeVert
			// 
			this.optDistributeVert.AutoSize = true;
			this.optDistributeVert.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.optDistributeVert.Checked = true;
			this.optDistributeVert.Location = new System.Drawing.Point(105, 78);
			this.optDistributeVert.Name = "optDistributeVert";
			this.optDistributeVert.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.optDistributeVert.Size = new System.Drawing.Size(87, 24);
			this.optDistributeVert.TabIndex = 3;
			this.optDistributeVert.TabStop = true;
			this.optDistributeVert.Text = "&Vertical";
			this.optDistributeVert.UseVisualStyleBackColor = true;
			// 
			// lblAnchor
			// 
			this.lblAnchor.AutoSize = true;
			this.lblAnchor.ForeColor = System.Drawing.Color.Gainsboro;
			this.lblAnchor.Location = new System.Drawing.Point(12, 320);
			this.lblAnchor.Name = "lblAnchor";
			this.lblAnchor.Size = new System.Drawing.Size(67, 20);
			this.lblAnchor.TabIndex = 4;
			this.lblAnchor.Text = "Anchor:";
			// 
			// txtAnchor
			// 
			this.txtAnchor.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.txtAnchor.Location = new System.Drawing.Point(85, 317);
			this.txtAnchor.Name = "txtAnchor";
			this.txtAnchor.ReadOnly = true;
			this.txtAnchor.Size = new System.Drawing.Size(169, 27);
			this.txtAnchor.TabIndex = 5;
			this.txtAnchor.TabStop = false;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(432, 358);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(88, 45);
			this.btnOK.TabIndex = 9;
			this.btnOK.Text = "&OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// frmPPAlignment
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(533, 466);
			this.Name = "frmPPAlignment";
			this.pnlMain.ResumeLayout(false);
			this.pnlMain.PerformLayout();
			this.grpAlign.ResumeLayout(false);
			this.grpAlign.PerformLayout();
			this.grpDistribute.ResumeLayout(false);
			this.grpDistribute.PerformLayout();
			this.ResumeLayout(false);

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
		public frmPPAlignment()
		{
			InitializeComponent();
			this.StartPosition = FormStartPosition.CenterParent;
			this.Title = "Text And Shape Alignment";
			this.statusThemedForm.Visible = false;

		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}

