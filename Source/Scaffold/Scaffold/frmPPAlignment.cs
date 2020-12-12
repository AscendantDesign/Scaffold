//	frmPPAlignment.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Office.Interop.PowerPoint;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

using static Scaffold.ScaffoldNodesUtil;
using static Scaffold.ScaffoldUtil;
using System.Diagnostics;

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
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
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
		private CheckBox chkDistribute;
		private CheckBox chkAlign;
		private ImageList ilShapes;
		private GroupBox grpReference;
		private RadioButton optReferenceAnchor;
		private TextBox txtAnchor;
		private RadioButton optReferenceTop;
		private RadioButton optReferenceLeft;
		private TextBox txtReferenceTop;
		private TextBox txtReferenceLeft;
		private NumericUpDown txtSlide;
		private Label lblReferenceTop;
		private Label lblReferenceLeft;
		private Panel pnlAlignCenter;
		private Panel pnlAlignMiddle;
		private RadioButton optAlignCenter;
		private RadioButton optAlignMiddle;


		RadioButton[] mGroupAlign = null;
		RadioButton[] mGroupDistribute = null;
		string mSelectionAnchor = "";

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
			btnOK.Focus();
			this.DialogResult = DialogResult.OK;
			this.Hide();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* chkAlign_CheckedChanged																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Align checkbox checked state has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void chkAlign_CheckedChanged(object sender, EventArgs e)
		{
			UpdateOKEnabled();
			if(chkAlign.Checked)
			{
				grpAlign.Enabled = true;
				grpAlign.ForeColor = FromHex(ResourceMain.colorTextNormal);
				//foreach(RadioButton opt in mGroupAlign)
				//{
				//	opt.ForeColor = FromHex(ResourceMain.colorTextNormal);
				//}
			}
			else
			{
				grpAlign.Enabled = false;
				grpAlign.ForeColor = FromHex(ResourceMain.colorTextDisabled);
				//foreach(RadioButton opt in mGroupAlign)
				//{
				//	opt.ForeColor = FromHex(ResourceMain.colorTextDisabled);
				//}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* chkDistribute_CheckedChanged																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Distribute checkbox checked state has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void chkDistribute_CheckedChanged(object sender, EventArgs e)
		{
			UpdateOKEnabled();
			if(chkDistribute.Checked)
			{
				grpDistribute.Enabled = true;
				grpDistribute.ForeColor = FromHex(ResourceMain.colorTextNormal);
				//foreach(RadioButton opt in mGroupDistribute)
				//{
				//	opt.ForeColor = FromHex(ResourceMain.colorTextNormal);
				//}
			}
			else
			{
				grpDistribute.Enabled = false;
				grpDistribute.ForeColor = FromHex(ResourceMain.colorTextDisabled);
				//foreach(RadioButton opt in mGroupDistribute)
				//{
				//	opt.ForeColor = FromHex(ResourceMain.colorTextDisabled);
				//}
			}
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
			this.components = new System.ComponentModel.Container();
			this.lblSlide = new System.Windows.Forms.Label();
			this.lblShapes = new System.Windows.Forms.Label();
			this.lvShapes = new System.Windows.Forms.ListView();
			this.ilShapes = new System.Windows.Forms.ImageList(this.components);
			this.btnCancel = new System.Windows.Forms.Button();
			this.grpAlign = new System.Windows.Forms.GroupBox();
			this.pnlAlignCenter = new System.Windows.Forms.Panel();
			this.pnlAlignBottom = new System.Windows.Forms.Panel();
			this.pnlAlignMiddle = new System.Windows.Forms.Panel();
			this.pnlAlignRight = new System.Windows.Forms.Panel();
			this.pnlAlignTop = new System.Windows.Forms.Panel();
			this.pnlAlignLeft = new System.Windows.Forms.Panel();
			this.optAlignCenter = new System.Windows.Forms.RadioButton();
			this.optAlignBottom = new System.Windows.Forms.RadioButton();
			this.optAlignMiddle = new System.Windows.Forms.RadioButton();
			this.optAlignRight = new System.Windows.Forms.RadioButton();
			this.optAlignTop = new System.Windows.Forms.RadioButton();
			this.optAlignLeft = new System.Windows.Forms.RadioButton();
			this.grpDistribute = new System.Windows.Forms.GroupBox();
			this.pnlDistributeHorz = new System.Windows.Forms.Panel();
			this.pnlDistributeVert = new System.Windows.Forms.Panel();
			this.optDistributeHorz = new System.Windows.Forms.RadioButton();
			this.optDistributeVert = new System.Windows.Forms.RadioButton();
			this.btnOK = new System.Windows.Forms.Button();
			this.chkAlign = new System.Windows.Forms.CheckBox();
			this.chkDistribute = new System.Windows.Forms.CheckBox();
			this.grpReference = new System.Windows.Forms.GroupBox();
			this.lblReferenceTop = new System.Windows.Forms.Label();
			this.lblReferenceLeft = new System.Windows.Forms.Label();
			this.optReferenceTop = new System.Windows.Forms.RadioButton();
			this.optReferenceLeft = new System.Windows.Forms.RadioButton();
			this.optReferenceAnchor = new System.Windows.Forms.RadioButton();
			this.txtReferenceTop = new System.Windows.Forms.TextBox();
			this.txtReferenceLeft = new System.Windows.Forms.TextBox();
			this.txtAnchor = new System.Windows.Forms.TextBox();
			this.txtSlide = new System.Windows.Forms.NumericUpDown();
			this.pnlMain.SuspendLayout();
			this.grpAlign.SuspendLayout();
			this.grpDistribute.SuspendLayout();
			this.grpReference.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtSlide)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.txtSlide);
			this.pnlMain.Controls.Add(this.chkDistribute);
			this.pnlMain.Controls.Add(this.chkAlign);
			this.pnlMain.Controls.Add(this.grpDistribute);
			this.pnlMain.Controls.Add(this.grpReference);
			this.pnlMain.Controls.Add(this.grpAlign);
			this.pnlMain.Controls.Add(this.btnOK);
			this.pnlMain.Controls.Add(this.btnCancel);
			this.pnlMain.Controls.Add(this.lvShapes);
			this.pnlMain.Controls.Add(this.lblShapes);
			this.pnlMain.Controls.Add(this.lblSlide);
			this.pnlMain.Size = new System.Drawing.Size(710, 498);
			this.pnlMain.Controls.SetChildIndex(this.lblSlide, 0);
			this.pnlMain.Controls.SetChildIndex(this.lblShapes, 0);
			this.pnlMain.Controls.SetChildIndex(this.lvShapes, 0);
			this.pnlMain.Controls.SetChildIndex(this.btnCancel, 0);
			this.pnlMain.Controls.SetChildIndex(this.btnOK, 0);
			this.pnlMain.Controls.SetChildIndex(this.grpAlign, 0);
			this.pnlMain.Controls.SetChildIndex(this.grpReference, 0);
			this.pnlMain.Controls.SetChildIndex(this.grpDistribute, 0);
			this.pnlMain.Controls.SetChildIndex(this.chkAlign, 0);
			this.pnlMain.Controls.SetChildIndex(this.chkDistribute, 0);
			this.pnlMain.Controls.SetChildIndex(this.txtSlide, 0);
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
			this.lvShapes.BackColor = System.Drawing.Color.White;
			this.lvShapes.ForeColor = System.Drawing.Color.Black;
			this.lvShapes.HideSelection = false;
			this.lvShapes.Location = new System.Drawing.Point(12, 74);
			this.lvShapes.Name = "lvShapes";
			this.lvShapes.Size = new System.Drawing.Size(297, 339);
			this.lvShapes.SmallImageList = this.ilShapes;
			this.lvShapes.TabIndex = 3;
			this.lvShapes.UseCompatibleStateImageBehavior = false;
			this.lvShapes.View = System.Windows.Forms.View.List;
			// 
			// ilShapes
			// 
			this.ilShapes.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.ilShapes.ImageSize = new System.Drawing.Size(24, 24);
			this.ilShapes.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(511, 441);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(88, 45);
			this.btnCancel.TabIndex = 9;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// grpAlign
			// 
			this.grpAlign.Controls.Add(this.pnlAlignCenter);
			this.grpAlign.Controls.Add(this.pnlAlignBottom);
			this.grpAlign.Controls.Add(this.pnlAlignMiddle);
			this.grpAlign.Controls.Add(this.pnlAlignRight);
			this.grpAlign.Controls.Add(this.pnlAlignTop);
			this.grpAlign.Controls.Add(this.pnlAlignLeft);
			this.grpAlign.Controls.Add(this.optAlignCenter);
			this.grpAlign.Controls.Add(this.optAlignBottom);
			this.grpAlign.Controls.Add(this.optAlignMiddle);
			this.grpAlign.Controls.Add(this.optAlignRight);
			this.grpAlign.Controls.Add(this.optAlignTop);
			this.grpAlign.Controls.Add(this.optAlignLeft);
			this.grpAlign.Enabled = false;
			this.grpAlign.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
			this.grpAlign.Location = new System.Drawing.Point(334, 175);
			this.grpAlign.Name = "grpAlign";
			this.grpAlign.Size = new System.Drawing.Size(357, 104);
			this.grpAlign.TabIndex = 6;
			this.grpAlign.TabStop = false;
			// 
			// pnlAlignCenter
			// 
			this.pnlAlignCenter.BackgroundImage = global::Scaffold.ResourceMain.AlignCenterGray32;
			this.pnlAlignCenter.Location = new System.Drawing.Point(103, 54);
			this.pnlAlignCenter.Margin = new System.Windows.Forms.Padding(0);
			this.pnlAlignCenter.Name = "pnlAlignCenter";
			this.pnlAlignCenter.Size = new System.Drawing.Size(32, 32);
			this.pnlAlignCenter.TabIndex = 8;
			// 
			// pnlAlignBottom
			// 
			this.pnlAlignBottom.BackgroundImage = global::Scaffold.ResourceMain.AlignBottomGray32;
			this.pnlAlignBottom.Location = new System.Drawing.Point(224, 16);
			this.pnlAlignBottom.Margin = new System.Windows.Forms.Padding(0);
			this.pnlAlignBottom.Name = "pnlAlignBottom";
			this.pnlAlignBottom.Size = new System.Drawing.Size(32, 32);
			this.pnlAlignBottom.TabIndex = 4;
			// 
			// pnlAlignMiddle
			// 
			this.pnlAlignMiddle.BackgroundImage = global::Scaffold.ResourceMain.AlignMiddleGray32;
			this.pnlAlignMiddle.Location = new System.Drawing.Point(103, 16);
			this.pnlAlignMiddle.Margin = new System.Windows.Forms.Padding(0);
			this.pnlAlignMiddle.Name = "pnlAlignMiddle";
			this.pnlAlignMiddle.Size = new System.Drawing.Size(32, 32);
			this.pnlAlignMiddle.TabIndex = 2;
			// 
			// pnlAlignRight
			// 
			this.pnlAlignRight.BackgroundImage = global::Scaffold.ResourceMain.AlignRightGray32;
			this.pnlAlignRight.Location = new System.Drawing.Point(224, 54);
			this.pnlAlignRight.Margin = new System.Windows.Forms.Padding(0);
			this.pnlAlignRight.Name = "pnlAlignRight";
			this.pnlAlignRight.Size = new System.Drawing.Size(32, 32);
			this.pnlAlignRight.TabIndex = 10;
			// 
			// pnlAlignTop
			// 
			this.pnlAlignTop.BackgroundImage = global::Scaffold.ResourceMain.AlignTopGray32;
			this.pnlAlignTop.Location = new System.Drawing.Point(4, 16);
			this.pnlAlignTop.Margin = new System.Windows.Forms.Padding(0);
			this.pnlAlignTop.Name = "pnlAlignTop";
			this.pnlAlignTop.Size = new System.Drawing.Size(32, 32);
			this.pnlAlignTop.TabIndex = 0;
			// 
			// pnlAlignLeft
			// 
			this.pnlAlignLeft.BackgroundImage = global::Scaffold.ResourceMain.AlignLeft32;
			this.pnlAlignLeft.Location = new System.Drawing.Point(4, 54);
			this.pnlAlignLeft.Margin = new System.Windows.Forms.Padding(0);
			this.pnlAlignLeft.Name = "pnlAlignLeft";
			this.pnlAlignLeft.Size = new System.Drawing.Size(32, 32);
			this.pnlAlignLeft.TabIndex = 6;
			// 
			// optAlignCenter
			// 
			this.optAlignCenter.AutoSize = true;
			this.optAlignCenter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.optAlignCenter.Location = new System.Drawing.Point(139, 58);
			this.optAlignCenter.Name = "optAlignCenter";
			this.optAlignCenter.Size = new System.Drawing.Size(80, 24);
			this.optAlignCenter.TabIndex = 9;
			this.optAlignCenter.TabStop = true;
			this.optAlignCenter.Text = "Cente&r";
			this.optAlignCenter.UseVisualStyleBackColor = false;
			// 
			// optAlignBottom
			// 
			this.optAlignBottom.AutoSize = true;
			this.optAlignBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.optAlignBottom.Location = new System.Drawing.Point(260, 20);
			this.optAlignBottom.Name = "optAlignBottom";
			this.optAlignBottom.Size = new System.Drawing.Size(84, 24);
			this.optAlignBottom.TabIndex = 5;
			this.optAlignBottom.TabStop = true;
			this.optAlignBottom.Text = "&Bottom";
			this.optAlignBottom.UseVisualStyleBackColor = false;
			// 
			// optAlignMiddle
			// 
			this.optAlignMiddle.AutoSize = true;
			this.optAlignMiddle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.optAlignMiddle.Location = new System.Drawing.Point(139, 20);
			this.optAlignMiddle.Name = "optAlignMiddle";
			this.optAlignMiddle.Size = new System.Drawing.Size(79, 24);
			this.optAlignMiddle.TabIndex = 3;
			this.optAlignMiddle.TabStop = true;
			this.optAlignMiddle.Text = "&Middle";
			this.optAlignMiddle.UseVisualStyleBackColor = false;
			// 
			// optAlignRight
			// 
			this.optAlignRight.AutoSize = true;
			this.optAlignRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.optAlignRight.Location = new System.Drawing.Point(260, 58);
			this.optAlignRight.Name = "optAlignRight";
			this.optAlignRight.Size = new System.Drawing.Size(69, 24);
			this.optAlignRight.TabIndex = 11;
			this.optAlignRight.TabStop = true;
			this.optAlignRight.Text = "R&ight";
			this.optAlignRight.UseVisualStyleBackColor = false;
			// 
			// optAlignTop
			// 
			this.optAlignTop.AutoSize = true;
			this.optAlignTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.optAlignTop.Location = new System.Drawing.Point(40, 20);
			this.optAlignTop.Name = "optAlignTop";
			this.optAlignTop.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.optAlignTop.Size = new System.Drawing.Size(58, 24);
			this.optAlignTop.TabIndex = 1;
			this.optAlignTop.TabStop = true;
			this.optAlignTop.Text = "To&p";
			this.optAlignTop.UseVisualStyleBackColor = true;
			// 
			// optAlignLeft
			// 
			this.optAlignLeft.AutoSize = true;
			this.optAlignLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.optAlignLeft.Checked = true;
			this.optAlignLeft.Location = new System.Drawing.Point(40, 58);
			this.optAlignLeft.Name = "optAlignLeft";
			this.optAlignLeft.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.optAlignLeft.Size = new System.Drawing.Size(59, 24);
			this.optAlignLeft.TabIndex = 7;
			this.optAlignLeft.TabStop = true;
			this.optAlignLeft.Text = "L&eft";
			this.optAlignLeft.UseVisualStyleBackColor = true;
			// 
			// grpDistribute
			// 
			this.grpDistribute.Controls.Add(this.pnlDistributeHorz);
			this.grpDistribute.Controls.Add(this.pnlDistributeVert);
			this.grpDistribute.Controls.Add(this.optDistributeHorz);
			this.grpDistribute.Controls.Add(this.optDistributeVert);
			this.grpDistribute.Enabled = false;
			this.grpDistribute.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
			this.grpDistribute.Location = new System.Drawing.Point(334, 311);
			this.grpDistribute.Name = "grpDistribute";
			this.grpDistribute.Size = new System.Drawing.Size(152, 102);
			this.grpDistribute.TabIndex = 8;
			this.grpDistribute.TabStop = false;
			// 
			// pnlDistributeHorz
			// 
			this.pnlDistributeHorz.BackgroundImage = global::Scaffold.ResourceMain.DistributeHorzGray32;
			this.pnlDistributeHorz.Location = new System.Drawing.Point(4, 16);
			this.pnlDistributeHorz.Margin = new System.Windows.Forms.Padding(0);
			this.pnlDistributeHorz.Name = "pnlDistributeHorz";
			this.pnlDistributeHorz.Size = new System.Drawing.Size(32, 32);
			this.pnlDistributeHorz.TabIndex = 0;
			// 
			// pnlDistributeVert
			// 
			this.pnlDistributeVert.BackgroundImage = global::Scaffold.ResourceMain.DistributeVert32;
			this.pnlDistributeVert.Location = new System.Drawing.Point(4, 54);
			this.pnlDistributeVert.Margin = new System.Windows.Forms.Padding(0);
			this.pnlDistributeVert.Name = "pnlDistributeVert";
			this.pnlDistributeVert.Size = new System.Drawing.Size(32, 32);
			this.pnlDistributeVert.TabIndex = 2;
			// 
			// optDistributeHorz
			// 
			this.optDistributeHorz.AutoSize = true;
			this.optDistributeHorz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.optDistributeHorz.Location = new System.Drawing.Point(40, 20);
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
			this.optDistributeVert.Location = new System.Drawing.Point(40, 58);
			this.optDistributeVert.Name = "optDistributeVert";
			this.optDistributeVert.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.optDistributeVert.Size = new System.Drawing.Size(87, 24);
			this.optDistributeVert.TabIndex = 3;
			this.optDistributeVert.TabStop = true;
			this.optDistributeVert.Text = "&Vertical";
			this.optDistributeVert.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Enabled = false;
			this.btnOK.Location = new System.Drawing.Point(605, 441);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(88, 45);
			this.btnOK.TabIndex = 10;
			this.btnOK.Text = "&OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// chkAlign
			// 
			this.chkAlign.AutoSize = true;
			this.chkAlign.ForeColor = System.Drawing.Color.Gainsboro;
			this.chkAlign.Location = new System.Drawing.Point(337, 156);
			this.chkAlign.Name = "chkAlign";
			this.chkAlign.Size = new System.Drawing.Size(68, 24);
			this.chkAlign.TabIndex = 5;
			this.chkAlign.Text = "&Align";
			this.chkAlign.UseVisualStyleBackColor = true;
			// 
			// chkDistribute
			// 
			this.chkDistribute.AutoSize = true;
			this.chkDistribute.ForeColor = System.Drawing.Color.Gainsboro;
			this.chkDistribute.Location = new System.Drawing.Point(334, 292);
			this.chkDistribute.Name = "chkDistribute";
			this.chkDistribute.Size = new System.Drawing.Size(104, 24);
			this.chkDistribute.TabIndex = 7;
			this.chkDistribute.Text = "&Distribute";
			this.chkDistribute.UseVisualStyleBackColor = true;
			// 
			// grpReference
			// 
			this.grpReference.Controls.Add(this.lblReferenceTop);
			this.grpReference.Controls.Add(this.lblReferenceLeft);
			this.grpReference.Controls.Add(this.optReferenceTop);
			this.grpReference.Controls.Add(this.optReferenceLeft);
			this.grpReference.Controls.Add(this.optReferenceAnchor);
			this.grpReference.Controls.Add(this.txtReferenceTop);
			this.grpReference.Controls.Add(this.txtReferenceLeft);
			this.grpReference.Controls.Add(this.txtAnchor);
			this.grpReference.ForeColor = System.Drawing.Color.Gainsboro;
			this.grpReference.Location = new System.Drawing.Point(334, 3);
			this.grpReference.Name = "grpReference";
			this.grpReference.Size = new System.Drawing.Size(308, 137);
			this.grpReference.TabIndex = 4;
			this.grpReference.TabStop = false;
			this.grpReference.Text = "&Reference Point";
			// 
			// lblReferenceTop
			// 
			this.lblReferenceTop.AutoSize = true;
			this.lblReferenceTop.Location = new System.Drawing.Point(245, 98);
			this.lblReferenceTop.Name = "lblReferenceTop";
			this.lblReferenceTop.Size = new System.Drawing.Size(26, 20);
			this.lblReferenceTop.TabIndex = 7;
			this.lblReferenceTop.Text = "in.";
			// 
			// lblReferenceLeft
			// 
			this.lblReferenceLeft.AutoSize = true;
			this.lblReferenceLeft.Location = new System.Drawing.Point(245, 68);
			this.lblReferenceLeft.Name = "lblReferenceLeft";
			this.lblReferenceLeft.Size = new System.Drawing.Size(26, 20);
			this.lblReferenceLeft.TabIndex = 4;
			this.lblReferenceLeft.Text = "in.";
			// 
			// optReferenceTop
			// 
			this.optReferenceTop.AutoSize = true;
			this.optReferenceTop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.optReferenceTop.ForeColor = System.Drawing.Color.Gainsboro;
			this.optReferenceTop.Location = new System.Drawing.Point(19, 96);
			this.optReferenceTop.Name = "optReferenceTop";
			this.optReferenceTop.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.optReferenceTop.Size = new System.Drawing.Size(58, 24);
			this.optReferenceTop.TabIndex = 5;
			this.optReferenceTop.Text = "&Top";
			this.optReferenceTop.UseVisualStyleBackColor = true;
			// 
			// optReferenceLeft
			// 
			this.optReferenceLeft.AutoSize = true;
			this.optReferenceLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.optReferenceLeft.ForeColor = System.Drawing.Color.Gainsboro;
			this.optReferenceLeft.Location = new System.Drawing.Point(19, 63);
			this.optReferenceLeft.Name = "optReferenceLeft";
			this.optReferenceLeft.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.optReferenceLeft.Size = new System.Drawing.Size(59, 24);
			this.optReferenceLeft.TabIndex = 2;
			this.optReferenceLeft.Text = "&Left";
			this.optReferenceLeft.UseVisualStyleBackColor = true;
			// 
			// optReferenceAnchor
			// 
			this.optReferenceAnchor.AutoSize = true;
			this.optReferenceAnchor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.optReferenceAnchor.Checked = true;
			this.optReferenceAnchor.ForeColor = System.Drawing.Color.Gainsboro;
			this.optReferenceAnchor.Location = new System.Drawing.Point(19, 30);
			this.optReferenceAnchor.Name = "optReferenceAnchor";
			this.optReferenceAnchor.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.optReferenceAnchor.Size = new System.Drawing.Size(83, 24);
			this.optReferenceAnchor.TabIndex = 0;
			this.optReferenceAnchor.TabStop = true;
			this.optReferenceAnchor.Text = "A&nchor";
			this.optReferenceAnchor.UseVisualStyleBackColor = true;
			// 
			// txtReferenceTop
			// 
			this.txtReferenceTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.txtReferenceTop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
			this.txtReferenceTop.Location = new System.Drawing.Point(108, 94);
			this.txtReferenceTop.Name = "txtReferenceTop";
			this.txtReferenceTop.ReadOnly = true;
			this.txtReferenceTop.Size = new System.Drawing.Size(131, 27);
			this.txtReferenceTop.TabIndex = 6;
			this.txtReferenceTop.TabStop = false;
			this.txtReferenceTop.Text = "0.0";
			// 
			// txtReferenceLeft
			// 
			this.txtReferenceLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.txtReferenceLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
			this.txtReferenceLeft.Location = new System.Drawing.Point(108, 61);
			this.txtReferenceLeft.Name = "txtReferenceLeft";
			this.txtReferenceLeft.ReadOnly = true;
			this.txtReferenceLeft.Size = new System.Drawing.Size(131, 27);
			this.txtReferenceLeft.TabIndex = 3;
			this.txtReferenceLeft.TabStop = false;
			this.txtReferenceLeft.Text = "0.0";
			// 
			// txtAnchor
			// 
			this.txtAnchor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.txtAnchor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
			this.txtAnchor.Location = new System.Drawing.Point(108, 28);
			this.txtAnchor.Name = "txtAnchor";
			this.txtAnchor.ReadOnly = true;
			this.txtAnchor.Size = new System.Drawing.Size(181, 27);
			this.txtAnchor.TabIndex = 1;
			this.txtAnchor.TabStop = false;
			// 
			// txtSlide
			// 
			this.txtSlide.Location = new System.Drawing.Point(69, 8);
			this.txtSlide.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.txtSlide.Name = "txtSlide";
			this.txtSlide.Size = new System.Drawing.Size(111, 27);
			this.txtSlide.TabIndex = 1;
			this.txtSlide.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// frmPPAlignment
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(710, 546);
			this.Name = "frmPPAlignment";
			this.pnlMain.ResumeLayout(false);
			this.pnlMain.PerformLayout();
			this.grpAlign.ResumeLayout(false);
			this.grpAlign.PerformLayout();
			this.grpDistribute.ResumeLayout(false);
			this.grpDistribute.PerformLayout();
			this.grpReference.ResumeLayout(false);
			this.grpReference.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtSlide)).EndInit();
			this.ResumeLayout(false);

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* lvShapes_ItemSelectionChanged																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Item selection has changed in the shapes list view.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// ListView item selection changed event arguments.
		/// </param>
		private void lvShapes_ItemSelectionChanged(object sender,
			ListViewItemSelectionChangedEventArgs e)
		{
			UpdateOKEnabled();
			if(lvShapes.SelectedItems.Count == 0)
			{
				mSelectionAnchor = "";
			}
			else if(lvShapes.SelectedItems.Count == 1)
			{
				mSelectionAnchor = (string)lvShapes.SelectedItems[0].Tag;
				txtAnchor.Text = lvShapes.SelectedItems[0].Text;
			}
			else
			{
				//	Multiple items selected.
				if(mSelectionAnchor.Length == 0)
				{
					//	Set the anchor.
					mSelectionAnchor = (string)lvShapes.SelectedItems[0].Tag;
					txtAnchor.Text = lvShapes.SelectedItems[0].Text;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* optAlignBottom_CheckedChanged																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state on the bottom alignment radio button has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void optAlignBottom_CheckedChanged(object sender, EventArgs e)
		{
			pnlAlignBottom.BackgroundImage =
				(optAlignBottom.Checked ?
				ResourceMain.AlignBottom32 :
				ResourceMain.AlignBottomGray32);
			if(optAlignBottom.Checked && optReferenceLeft.Checked)
			{
				optReferenceTop.Checked = true;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* optAlignCenter_CheckedChanged																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state on the center alignment radio button has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void optAlignCenter_CheckedChanged(object sender, EventArgs e)
		{
			pnlAlignCenter.BackgroundImage =
				(optAlignCenter.Checked ?
				ResourceMain.AlignCenter32 :
				ResourceMain.AlignCenterGray32);
			if(optAlignCenter.Checked && optReferenceTop.Checked)
			{
				optReferenceLeft.Checked = true;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* optAlignLeft_CheckedChanged																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state on the left alignment radio button has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void optAlignLeft_CheckedChanged(object sender, EventArgs e)
		{
			pnlAlignLeft.BackgroundImage =
				(optAlignLeft.Checked ?
				ResourceMain.AlignLeft32 :
				ResourceMain.AlignLeftGray32);
			if(optAlignLeft.Checked && optReferenceTop.Checked)
			{
				optReferenceLeft.Checked = true;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* optAlignMiddle_CheckedChanged																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state on the middle alignment radio button has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void optAlignMiddle_CheckedChanged(object sender, EventArgs e)
		{
			pnlAlignMiddle.BackgroundImage =
				(optAlignMiddle.Checked ?
				ResourceMain.AlignMiddle32 :
				ResourceMain.AlignMiddleGray32);
			if(optAlignMiddle.Checked && optReferenceLeft.Checked)
			{
				optReferenceTop.Checked = true;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* optAlignRight_CheckedChanged																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state on the right alignment radio button has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void optAlignRight_CheckedChanged(object sender, EventArgs e)
		{
			pnlAlignRight.BackgroundImage =
				(optAlignRight.Checked ?
				ResourceMain.AlignRight32 :
				ResourceMain.AlignRightGray32);
			if(optAlignRight.Checked && optReferenceTop.Checked)
			{
				optReferenceLeft.Checked = true;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* optAlignTop_CheckedChanged																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state on the top alignment radio button has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void optAlignTop_CheckedChanged(object sender, EventArgs e)
		{
			pnlAlignTop.BackgroundImage =
				(optAlignTop.Checked ?
				ResourceMain.AlignTop32 :
				ResourceMain.AlignTopGray32);
			if(optAlignTop.Checked && optReferenceLeft.Checked)
			{
				optReferenceTop.Checked = true;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* optDistributeHorz_CheckedChanged																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state on the horizontal distribution radio button has
		/// changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void optDistributeHorz_CheckedChanged(object sender, EventArgs e)
		{
			pnlDistributeHorz.BackgroundImage =
				(optDistributeHorz.Checked ?
				ResourceMain.DistributeHorz32 :
				ResourceMain.DistributeHorzGray32);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* optDistributeVert_CheckedChanged																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state on the vertical distribution radio button has
		/// changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void optDistributeVert_CheckedChanged(object sender, EventArgs e)
		{
			pnlDistributeVert.BackgroundImage =
				(optDistributeVert.Checked ?
				ResourceMain.DistributeVert32 :
				ResourceMain.DistributeVertGray32);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* optReferenceLeft_CheckedChanged																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state of the reference left radio button has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void optReferenceLeft_CheckedChanged(object sender, EventArgs e)
		{
			if(optReferenceLeft.Checked)
			{
				txtReferenceLeft.ReadOnly = false;
				txtReferenceLeft.TabStop = true;
				txtReferenceLeft.BackColor =
					FromHex(ResourceMain.colorBackgroundWriting);
				txtReferenceLeft.ForeColor =
					FromHex(ResourceMain.colorTextWriting);
			}
			else
			{
				txtReferenceLeft.ReadOnly = true;
				txtReferenceLeft.TabStop = false;
				txtReferenceLeft.BackColor =
					FromHex(ResourceMain.colorBackgroundWritingDisabled);
				txtReferenceLeft.ForeColor =
					FromHex(ResourceMain.colorTextWritingDisabled);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* optReferenceTop_CheckedChanged																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state of the reference top radio button has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void optReferenceTop_CheckedChanged(object sender, EventArgs e)
		{
			if(optReferenceTop.Checked)
			{
				txtReferenceTop.ReadOnly = false;
				txtReferenceTop.TabStop = true;
				txtReferenceTop.BackColor =
					FromHex(ResourceMain.colorBackgroundWriting);
				txtReferenceTop.ForeColor =
					FromHex(ResourceMain.colorTextWriting);
			}
			else
			{
				txtReferenceTop.ReadOnly = true;
				txtReferenceTop.TabStop = false;
				txtReferenceTop.BackColor =
					FromHex(ResourceMain.colorBackgroundWritingDisabled);
				txtReferenceTop.ForeColor =
					FromHex(ResourceMain.colorTextWritingDisabled);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtSlide_TextChanged																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The value of the slide number textbox has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtSlide_TextChanged(object sender, EventArgs e)
		{
			ListViewItem item = null;
			string name = "";
			int slideIndex = ToInt(txtSlide.Text);
			Shapes shapes = null;
			Slide slide = null;

			mSelectionAnchor = "";

			lvShapes.Items.Clear();
			if(mDriver != null)
			{
				if(slideIndex > 0)
				{
					slide = PowerPointDriver.GetSlideBySlideIndex(
						mDriver.ActivePresentation, slideIndex);
					if(slide != null)
					{
						//	Slide found.
						txtSlide.BackColor =
							FromHex(ResourceMain.colorBackgroundWriting);
						shapes = slide.Shapes;
						foreach(PowerPoint.Shape shape in shapes)
						{
							name = LimitLength(PowerPointDriver.GetText(shape), 32);
							if(name == null || name.Length == 0)
							{
								name = shape.Name;
							}
							item = lvShapes.Items.Add(name, shape.Type.ToString());
							item.Tag = shape.Name;
						}
					}
					else
					{
						//	Slide not found.
						txtSlide.BackColor =
							FromHex(ResourceMain.colorBackgroundWritingError);
					}
				}
				else
				{
					//	Non-numeric index.
					txtSlide.BackColor =
						FromHex(ResourceMain.colorBackgroundWritingError);
				}
			}
			else
			{
				//	Driver not assigned.
				txtSlide.BackColor = FromHex(ResourceMain.colorBackgroundWritingError);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UpdateOKEnabled																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the OK button enabled state based upon current conditions on
		/// the form.
		/// </summary>
		private void UpdateOKEnabled()
		{
			btnOK.Enabled =
				(lvShapes.SelectedItems.Count > 0 &&
				(chkAlign.Checked || chkDistribute.Checked));
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

		//*-----------------------------------------------------------------------*
		//*	OnLoad																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the Load event when the form has loaded and is ready to display
		/// for the first time.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if(mDriver != null)
			{
				txtSlide.Value = mDriver.ActiveSlideIndex;
			}
			foreach(RadioButton opt in mGroupAlign)
			{
				opt.ForeColor = FromHex(ResourceMain.colorTextDisabled);
			}
			foreach(RadioButton opt in mGroupDistribute)
			{
				opt.ForeColor = FromHex(ResourceMain.colorTextDisabled);
			}
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
			this.menuThemedForm.Visible = false;
			this.statusThemedForm.Visible = false;

			btnCancel.Click += btnCancel_Click;
			btnOK.Click += btnOK_Click;
			chkAlign.CheckedChanged += chkAlign_CheckedChanged;
			chkDistribute.CheckedChanged += chkDistribute_CheckedChanged;
			lvShapes.ItemSelectionChanged += lvShapes_ItemSelectionChanged;
			optAlignBottom.CheckedChanged += optAlignBottom_CheckedChanged;
			optAlignCenter.CheckedChanged += optAlignCenter_CheckedChanged;
			optAlignLeft.CheckedChanged += optAlignLeft_CheckedChanged;
			optAlignMiddle.CheckedChanged += optAlignMiddle_CheckedChanged;
			optAlignRight.CheckedChanged += optAlignRight_CheckedChanged;
			optAlignTop.CheckedChanged += optAlignTop_CheckedChanged;
			optDistributeHorz.CheckedChanged += optDistributeHorz_CheckedChanged;
			optDistributeVert.CheckedChanged += optDistributeVert_CheckedChanged;
			optReferenceLeft.CheckedChanged += optReferenceLeft_CheckedChanged;
			optReferenceTop.CheckedChanged += optReferenceTop_CheckedChanged;
			txtSlide.TextChanged += txtSlide_TextChanged;

			//	Option handlers.
			mGroupAlign = new RadioButton[]
			{
				optAlignTop,
				optAlignMiddle,
				optAlignBottom,
				optAlignLeft,
				optAlignCenter,
				optAlignRight
			};
			mGroupDistribute = new RadioButton[]
			{
				optDistributeHorz,
				optDistributeVert
			};

			//	List shapes.
			LoadImageListMsoShapeType(ilShapes);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AlignmentReference																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the coordinate reference for this alignment.
		/// </summary>
		public AlignmentReferenceEnum AlignmentReference
		{
			get
			{
				AlignmentReferenceEnum result = AlignmentReferenceEnum.None;
				if(optReferenceAnchor.Checked)
				{
					result = AlignmentReferenceEnum.Anchor;
				}
				else if(optReferenceLeft.Checked)
				{
					result = AlignmentReferenceEnum.LeftCoord;
				}
				else if(optReferenceTop.Checked)
				{
					result = AlignmentReferenceEnum.TopCoord;
				}
				return result;
			}
			set
			{
				switch(value)
				{
					case AlignmentReferenceEnum.Anchor:
						optReferenceAnchor.Checked = true;
						break;
					case AlignmentReferenceEnum.LeftCoord:
						optReferenceLeft.Checked = true;
						break;
					case AlignmentReferenceEnum.TopCoord:
						optReferenceTop.Checked = true;
						break;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AlignmentReferenceValue																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the value associated to the selected coordinate alignment
		/// reference.
		/// </summary>
		public string AlignmentReferenceValue
		{
			get
			{
				string result = "";
				if(optReferenceAnchor.Checked)
				{
					result = mSelectionAnchor;
				}
				else if(optReferenceLeft.Checked)
				{
					result =
						InchesToPoints(ToFloat(txtReferenceLeft.Text)).ToString();
				}
				else if(optReferenceTop.Checked)
				{
					result =
						InchesToPoints(ToFloat(txtReferenceTop.Text)).ToString();
				}
				return result;
			}
			set
			{
				if(optReferenceAnchor.Checked)
				{
					mSelectionAnchor = txtAnchor.Text = value;
				}
				else if(optReferenceLeft.Checked)
				{
					txtReferenceLeft.Text = PointsToInches(ToFloat(value)).ToString();
				}
				else if(optReferenceTop.Checked)
				{
					txtReferenceTop.Text = PointsToInches(ToFloat(value)).ToString();
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AlignmentType																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the selected alignment type.
		/// </summary>
		public AlignmentTypeEnum AlignmentType
		{
			get
			{
				AlignmentTypeEnum result = AlignmentTypeEnum.None;

				if(chkAlign.Checked)
				{
					if(optAlignBottom.Checked)
					{
						result = AlignmentTypeEnum.Bottom;
					}
					else if(optAlignCenter.Checked)
					{
						result = AlignmentTypeEnum.Center;
					}
					else if(optAlignLeft.Checked)
					{
						result = AlignmentTypeEnum.Left;
					}
					else if(optAlignMiddle.Checked)
					{
						result = AlignmentTypeEnum.Middle;
					}
					else if(optAlignRight.Checked)
					{
						result = AlignmentTypeEnum.Right;
					}
					else if(optAlignTop.Checked)
					{
						result = AlignmentTypeEnum.Top;
					}
				}
				return result;
			}
			set
			{
				if(value == AlignmentTypeEnum.None)
				{
					chkAlign.Checked = false;
				}
				else
				{
					chkAlign.Checked = true;
					switch(value)
					{
						case AlignmentTypeEnum.Bottom:
							optAlignBottom.Checked = true;
							break;
						case AlignmentTypeEnum.Center:
							optAlignCenter.Checked = true;
							break;
						case AlignmentTypeEnum.Left:
							optAlignLeft.Checked = true;
							break;
						case AlignmentTypeEnum.Middle:
							optAlignMiddle.Checked = true;
							break;
						case AlignmentTypeEnum.Right:
							optAlignRight.Checked = true;
							break;
						case AlignmentTypeEnum.Top:
							optAlignTop.Checked = true;
							break;
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DistributionType																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the selected distribution type.
		/// </summary>
		public DistributionTypeEnum DistributionType
		{
			get
			{
				DistributionTypeEnum result = DistributionTypeEnum.None;

				if(chkDistribute.Checked)
				{
					if(optDistributeHorz.Checked)
					{
						result = DistributionTypeEnum.Horizontal;
					}
					else if(optDistributeVert.Checked)
					{
						result = DistributionTypeEnum.Vertical;
					}
				}
				return result;
			}
			set
			{
				if(value == DistributionTypeEnum.None)
				{
					chkDistribute.Checked = false;
				}
				else
				{
					chkDistribute.Checked = true;
					switch(value)
					{
						case DistributionTypeEnum.Horizontal:
							optDistributeHorz.Checked = true;
							break;
						case DistributionTypeEnum.Vertical:
							optDistributeVert.Checked = true;
							break;
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Driver																																*
		//*-----------------------------------------------------------------------*
		private PowerPointDriver mDriver = null;
		/// <summary>
		/// Get/Set a reference to the active PowerPoint office driver for this
		/// instance.
		/// </summary>
		public PowerPointDriver Driver
		{
			get { return mDriver; }
			set
			{
				mDriver = value;
				if(mDriver != null)
				{
					txtSlide.Maximum = mDriver.SlideCount;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SelectedShapeNames																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get a reference to the list of selected object names.
		/// </summary>
		public List<string> SelectedShapeNames
		{
			get
			{
				List<string> result = new List<string>();

				foreach(ListViewItem listItem in lvShapes.SelectedItems)
				{
					result.Add((string)listItem.Tag);
				}
				return result;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SlideIndex																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the selected slide index.
		/// </summary>
		public int SlideIndex
		{
			get { return Convert.ToInt32(txtSlide.Value); }
			set { txtSlide.Value = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}

