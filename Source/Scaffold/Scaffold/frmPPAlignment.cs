using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Office.Interop.PowerPoint;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

using static Scaffold.ScaffoldNodesUtil;
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

		RadioButton[] mGroupAlign = null;
		RadioButton[] mGroupDistribute = null;
		string mSelectionAnchor = "";
		private ImageList ilShapes;
		private GroupBox grpReference;
		private RadioButton radioButton1;
		private TextBox txtAnchor;
		private RadioButton radioButton3;
		private RadioButton radioButton2;
		private TextBox textBox2;
		private TextBox textBox1;
		private NumericUpDown txtSlide;
		private Label label2;
		private Label label1;
		List<string> mSelectionList = new List<string>();

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
			if(chkAlign.Checked)
			{
				//grpAlign.Enabled = true;
				grpAlign.ForeColor = FromHex(ResourceMain.colorTextNormal);
				foreach(RadioButton opt in mGroupAlign)
				{
					opt.ForeColor = FromHex(ResourceMain.colorTextNormal);
				}
			}
			else
			{
				//grpAlign.Enabled = false;
				grpAlign.ForeColor = FromHex(ResourceMain.colorTextDisabled);
				foreach(RadioButton opt in mGroupAlign)
				{
					opt.ForeColor = FromHex(ResourceMain.colorTextDisabled);
				}
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
			if(chkDistribute.Checked)
			{
				//grpDistribute.Enabled = true;
				grpDistribute.ForeColor = FromHex(ResourceMain.colorTextNormal);
				foreach(RadioButton opt in mGroupDistribute)
				{
					opt.ForeColor = FromHex(ResourceMain.colorTextNormal);
				}
			}
			else
			{
				//grpDistribute.Enabled = false;
				grpDistribute.ForeColor = FromHex(ResourceMain.colorTextDisabled);
				foreach(RadioButton opt in mGroupDistribute)
				{
					opt.ForeColor = FromHex(ResourceMain.colorTextDisabled);
				}
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
			this.pnlAlignBottom = new System.Windows.Forms.Panel();
			this.pnlAlignRight = new System.Windows.Forms.Panel();
			this.pnlAlignTop = new System.Windows.Forms.Panel();
			this.pnlAlignLeft = new System.Windows.Forms.Panel();
			this.optAlignBottom = new System.Windows.Forms.RadioButton();
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
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
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
			this.pnlMain.Size = new System.Drawing.Size(602, 416);
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
			this.lvShapes.Size = new System.Drawing.Size(242, 273);
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
			this.btnCancel.Location = new System.Drawing.Point(403, 359);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(88, 45);
			this.btnCancel.TabIndex = 9;
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
			this.grpAlign.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
			this.grpAlign.Location = new System.Drawing.Point(277, 175);
			this.grpAlign.Name = "grpAlign";
			this.grpAlign.Size = new System.Drawing.Size(141, 172);
			this.grpAlign.TabIndex = 6;
			this.grpAlign.TabStop = false;
			// 
			// pnlAlignBottom
			// 
			this.pnlAlignBottom.BackgroundImage = global::Scaffold.ResourceMain.AlignBottom32;
			this.pnlAlignBottom.Location = new System.Drawing.Point(4, 130);
			this.pnlAlignBottom.Margin = new System.Windows.Forms.Padding(0);
			this.pnlAlignBottom.Name = "pnlAlignBottom";
			this.pnlAlignBottom.Size = new System.Drawing.Size(32, 32);
			this.pnlAlignBottom.TabIndex = 6;
			// 
			// pnlAlignRight
			// 
			this.pnlAlignRight.BackgroundImage = global::Scaffold.ResourceMain.AlignRight32;
			this.pnlAlignRight.Location = new System.Drawing.Point(4, 92);
			this.pnlAlignRight.Margin = new System.Windows.Forms.Padding(0);
			this.pnlAlignRight.Name = "pnlAlignRight";
			this.pnlAlignRight.Size = new System.Drawing.Size(32, 32);
			this.pnlAlignRight.TabIndex = 4;
			// 
			// pnlAlignTop
			// 
			this.pnlAlignTop.BackgroundImage = global::Scaffold.ResourceMain.AlignTop32;
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
			this.pnlAlignLeft.TabIndex = 2;
			// 
			// optAlignBottom
			// 
			this.optAlignBottom.AutoSize = true;
			this.optAlignBottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.optAlignBottom.Location = new System.Drawing.Point(40, 133);
			this.optAlignBottom.Name = "optAlignBottom";
			this.optAlignBottom.Size = new System.Drawing.Size(84, 24);
			this.optAlignBottom.TabIndex = 7;
			this.optAlignBottom.TabStop = true;
			this.optAlignBottom.Text = "&Bottom";
			this.optAlignBottom.UseVisualStyleBackColor = false;
			// 
			// optAlignRight
			// 
			this.optAlignRight.AutoSize = true;
			this.optAlignRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.optAlignRight.Location = new System.Drawing.Point(40, 95);
			this.optAlignRight.Name = "optAlignRight";
			this.optAlignRight.Size = new System.Drawing.Size(69, 24);
			this.optAlignRight.TabIndex = 5;
			this.optAlignRight.TabStop = true;
			this.optAlignRight.Text = "&Right";
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
			this.optAlignLeft.TabIndex = 3;
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
			this.grpDistribute.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
			this.grpDistribute.Location = new System.Drawing.Point(433, 175);
			this.grpDistribute.Name = "grpDistribute";
			this.grpDistribute.Size = new System.Drawing.Size(152, 102);
			this.grpDistribute.TabIndex = 8;
			this.grpDistribute.TabStop = false;
			// 
			// pnlDistributeHorz
			// 
			this.pnlDistributeHorz.BackgroundImage = global::Scaffold.ResourceMain.DistributeHorz32;
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
			this.btnOK.Location = new System.Drawing.Point(497, 359);
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
			this.chkAlign.Location = new System.Drawing.Point(280, 156);
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
			this.chkDistribute.Location = new System.Drawing.Point(433, 156);
			this.chkDistribute.Name = "chkDistribute";
			this.chkDistribute.Size = new System.Drawing.Size(104, 24);
			this.chkDistribute.TabIndex = 7;
			this.chkDistribute.Text = "&Distribute";
			this.chkDistribute.UseVisualStyleBackColor = true;
			// 
			// grpReference
			// 
			this.grpReference.Controls.Add(this.label2);
			this.grpReference.Controls.Add(this.label1);
			this.grpReference.Controls.Add(this.radioButton3);
			this.grpReference.Controls.Add(this.radioButton2);
			this.grpReference.Controls.Add(this.radioButton1);
			this.grpReference.Controls.Add(this.textBox2);
			this.grpReference.Controls.Add(this.textBox1);
			this.grpReference.Controls.Add(this.txtAnchor);
			this.grpReference.ForeColor = System.Drawing.Color.Gainsboro;
			this.grpReference.Location = new System.Drawing.Point(277, 3);
			this.grpReference.Name = "grpReference";
			this.grpReference.Size = new System.Drawing.Size(308, 137);
			this.grpReference.TabIndex = 4;
			this.grpReference.TabStop = false;
			this.grpReference.Text = "&Reference Point";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(245, 98);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(26, 20);
			this.label2.TabIndex = 6;
			this.label2.Text = "in.";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(245, 68);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(26, 20);
			this.label1.TabIndex = 6;
			this.label1.Text = "in.";
			// 
			// radioButton3
			// 
			this.radioButton3.AutoSize = true;
			this.radioButton3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.radioButton3.ForeColor = System.Drawing.Color.Gainsboro;
			this.radioButton3.Location = new System.Drawing.Point(19, 96);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.radioButton3.Size = new System.Drawing.Size(58, 24);
			this.radioButton3.TabIndex = 4;
			this.radioButton3.Text = "&Top";
			this.radioButton3.UseVisualStyleBackColor = true;
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.radioButton2.ForeColor = System.Drawing.Color.Gainsboro;
			this.radioButton2.Location = new System.Drawing.Point(19, 63);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.radioButton2.Size = new System.Drawing.Size(59, 24);
			this.radioButton2.TabIndex = 2;
			this.radioButton2.Text = "&Left";
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.radioButton1.Checked = true;
			this.radioButton1.ForeColor = System.Drawing.Color.Gainsboro;
			this.radioButton1.Location = new System.Drawing.Point(19, 30);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.radioButton1.Size = new System.Drawing.Size(83, 24);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "A&nchor";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// textBox2
			// 
			this.textBox2.BackColor = System.Drawing.Color.White;
			this.textBox2.ForeColor = System.Drawing.Color.Black;
			this.textBox2.Location = new System.Drawing.Point(108, 94);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(131, 27);
			this.textBox2.TabIndex = 5;
			this.textBox2.TabStop = false;
			this.textBox2.Text = "0.0";
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.Color.White;
			this.textBox1.ForeColor = System.Drawing.Color.Black;
			this.textBox1.Location = new System.Drawing.Point(108, 61);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(131, 27);
			this.textBox1.TabIndex = 3;
			this.textBox1.TabStop = false;
			this.textBox1.Text = "0.0";
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
			this.ClientSize = new System.Drawing.Size(602, 464);
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
			mSelectionList.Clear();
			if(lvShapes.SelectedItems.Count == 0)
			{
				mSelectionAnchor = "";
			}
			else if(lvShapes.SelectedItems.Count == 1)
			{
				mSelectionAnchor = lvShapes.SelectedItems[0].Text;
				mSelectionList.Add(mSelectionAnchor);
			}
			else
			{
				//	Multiple items selected.
				if(mSelectionAnchor.Length == 0)
				{
					//	Set the anchor.
					mSelectionAnchor = lvShapes.SelectedItems[0].Text;
				}
				foreach(ListViewItem listItem in lvShapes.SelectedItems)
				{
					mSelectionList.Add(listItem.Text);
				}
			}
			txtAnchor.Text = mSelectionAnchor;
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
			//OnShapeListRequest(new ShapeListEventArgs(ToInt(txtSlide.Text)));
			int slideIndex = ToInt(txtSlide.Text);
			Shapes shapes = null;
			Slide slide = null;

			mSelectionAnchor = "";
			mSelectionList.Clear();

			lvShapes.Items.Clear();
			if(mDriver != null)
			{
				if(slideIndex > 0)
				{
					slide = OfficeDriver.GetSlideBySlideIndex(
						mDriver.ActivePresentation, slideIndex);
					if(slide != null)
					{
						//	Slide found.
						txtSlide.BackColor =
							FromHex(ResourceMain.colorBackgroundWriting);
						shapes = slide.Shapes;
						foreach(PowerPoint.Shape shape in shapes)
						{
							Debug.WriteLine(shape.Type.ToString());
							lvShapes.Items.Add(shape.Name, shape.Type.ToString());
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
				//txtSlide.Text = mDriver.ActiveSlideIndex.ToString();
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

		////*-----------------------------------------------------------------------*
		////* OnShapeListRequest																										*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Raises the ShapeListRequest event when a new list of shapes is being
		///// requested for the currently selected slide.
		///// </summary>
		///// <param name="e">
		///// Reference to a shape list event arguments object containing a valid
		///// 1-base slide index for the current presentation.
		///// </param>
		//protected virtual void OnShapeListRequest(ShapeListEventArgs e)
		//{
		//	bool bTypes = false;
		//	int index = 0;
		//	string typeName = "";

		//	ShapeListRequest?.Invoke(this, e);
		//	if(e != null)
		//	{
		//		lvShapes.Items.Clear();
		//		if(e.SlideIndexValid)
		//		{
		//			txtSlide.BackColor = FromHex(ResourceMain.colorBackgroundWriting);
		//		}
		//		else
		//		{
		//			txtSlide.BackColor =
		//				FromHex(ResourceMain.colorBackgroundWritingError);
		//		}
		//		if(e.Handled)
		//		{
		//			//	The event was handled.
		//			bTypes = (e.Names.Count == e.ShapeTypes.Count);
		//			foreach(string name in e.Names)
		//			{
		//				if(bTypes)
		//				{
		//					//	Name and type image.
		//					typeName = e.ShapeTypes[index];
		//					lvShapes.Items.Add(name, typeName);
		//				}
		//				else
		//				{
		//					//	Names only.
		//					lvShapes.Items.Add(name);
		//				}
		//				index++;
		//			}
		//		}
		//	}
		//}
		////*-----------------------------------------------------------------------*

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
			//txtSlide.TextChanged += txtSlide_TextChanged;

			mGroupAlign = new RadioButton[]
			{
				optAlignTop,
				optAlignLeft,
				optAlignRight,
				optAlignBottom
			};
			mGroupDistribute = new RadioButton[]
			{
				optDistributeHorz,
				optDistributeVert
			};

			ilShapes.Images.Add("msoAutoShape", ResourceMain.msoAutoShape32);
			ilShapes.Images.Add("msoCallout", ResourceMain.msoCallout32);
			ilShapes.Images.Add("msoCanvas", ResourceMain.msoCanvas32);
			ilShapes.Images.Add("msoChart", ResourceMain.msoChart32);
			ilShapes.Images.Add("msoComment", ResourceMain.msoComment32);
			ilShapes.Images.Add("msoContentApp", ResourceMain.msoContentApp32);
			ilShapes.Images.Add("msoDiagram", ResourceMain.msoDiagram32);
			ilShapes.Images.Add("msoEmbeddedOLEObject",
				ResourceMain.msoEmbeddedOLEObject32);
			ilShapes.Images.Add("msoFormControl", ResourceMain.msoFormControl32);
			ilShapes.Images.Add("msoFreeform", ResourceMain.msoFreeform32);
			ilShapes.Images.Add("msoGroup", ResourceMain.msoGroup32);
			ilShapes.Images.Add("msoInk", ResourceMain.msoInk32);
			ilShapes.Images.Add("msoInkComment", ResourceMain.msoInkComment32);
			ilShapes.Images.Add("msoLinkedOLEObject",
				ResourceMain.msoLinkedOLEObject32);
			ilShapes.Images.Add("msoLinkedPicture", ResourceMain.msoLinkedPicture32);
			ilShapes.Images.Add("msoMedia", ResourceMain.msoMedia32);
			ilShapes.Images.Add("msoOLEControlObject",
				ResourceMain.msoOLEControlObject32);
			ilShapes.Images.Add("msoPicture", ResourceMain.msoPicture32);
			ilShapes.Images.Add("msoPlaceholder", ResourceMain.msoPlaceholder32);
			ilShapes.Images.Add("msoScriptAnchor", ResourceMain.msoScriptAnchor32);
			ilShapes.Images.Add("msoShapeTypeMixed",
				ResourceMain.msoShapeTypeMixed32);
			ilShapes.Images.Add("msoSlicer", ResourceMain.msoSlicer32);
			ilShapes.Images.Add("msoSmartArt", ResourceMain.msoSmartArt32);
			ilShapes.Images.Add("msoTable", ResourceMain.msoTable32);
			ilShapes.Images.Add("msoTextBox", ResourceMain.msoTextBox32);
			ilShapes.Images.Add("msoTextEffect", ResourceMain.msoTextEffect32);
			ilShapes.Images.Add("msoWebVideo", ResourceMain.msoWebVideo32);

		}
		//*-----------------------------------------------------------------------*

		//	TODO: !1 - Stopped here.
		//	TODO: Re-wire form with new controls.

		//*-----------------------------------------------------------------------*
		//*	Driver																																*
		//*-----------------------------------------------------------------------*
		private OfficeDriver mDriver = null;
		/// <summary>
		/// Get/Set a reference to the active PowerPoint office driver for this
		/// instance.
		/// </summary>
		public OfficeDriver Driver
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

		////*-----------------------------------------------------------------------*
		////* ShapeListRequest																											*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Fired when the shape list is being requested for a selected sheet
		///// index.
		///// </summary>
		//public event ShapeListEventHandler ShapeListRequest;
		////*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}

