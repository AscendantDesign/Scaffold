//	PanelWindowControl.Designer.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
namespace Scaffold
{
	partial class PanelWindowControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnlTop = new System.Windows.Forms.Panel();
			this.pnlBottom = new System.Windows.Forms.Panel();
			this.pnlLeft = new System.Windows.Forms.Panel();
			this.pnlRight = new System.Windows.Forms.Panel();
			this.pnlCenter = new System.Windows.Forms.Panel();
			this.splitRight = new Scaffold.CollapsibleSplitter();
			this.splitLeft = new Scaffold.CollapsibleSplitter();
			this.splitBottom = new Scaffold.CollapsibleSplitter();
			this.splitTop = new Scaffold.CollapsibleSplitter();
			this.SuspendLayout();
			// 
			// pnlTop
			// 
			this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTop.Location = new System.Drawing.Point(0, 0);
			this.pnlTop.Name = "pnlTop";
			this.pnlTop.Size = new System.Drawing.Size(665, 8);
			this.pnlTop.TabIndex = 0;
			this.pnlTop.SizeChanged += new System.EventHandler(this.pnlTop_SizeChanged);
			// 
			// pnlBottom
			// 
			this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlBottom.Location = new System.Drawing.Point(0, 284);
			this.pnlBottom.Name = "pnlBottom";
			this.pnlBottom.Size = new System.Drawing.Size(665, 8);
			this.pnlBottom.TabIndex = 1;
			this.pnlBottom.SizeChanged += new System.EventHandler(this.pnlBottom_SizeChanged);
			// 
			// pnlLeft
			// 
			this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlLeft.Location = new System.Drawing.Point(0, 66);
			this.pnlLeft.Name = "pnlLeft";
			this.pnlLeft.Size = new System.Drawing.Size(8, 212);
			this.pnlLeft.TabIndex = 4;
			this.pnlLeft.SizeChanged += new System.EventHandler(this.pnlLeft_SizeChanged);
			// 
			// pnlRight
			// 
			this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnlRight.Location = new System.Drawing.Point(477, 66);
			this.pnlRight.Name = "pnlRight";
			this.pnlRight.Size = new System.Drawing.Size(8, 212);
			this.pnlRight.TabIndex = 6;
			this.pnlRight.SizeChanged += new System.EventHandler(this.pnlRight_SizeChanged);
			// 
			// pnlCenter
			// 
			this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlCenter.Location = new System.Drawing.Point(140, 66);
			this.pnlCenter.Name = "pnlCenter";
			this.pnlCenter.Size = new System.Drawing.Size(331, 212);
			this.pnlCenter.TabIndex = 8;
			// 
			// splitRight
			// 
			this.splitRight.AnimationDelay = 20;
			this.splitRight.AnimationStep = 20;
			this.splitRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.splitRight.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
			this.splitRight.ControlToHide = this.pnlRight;
			this.splitRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitRight.ExpandParentForm = false;
			this.splitRight.Location = new System.Drawing.Point(471, 66);
			this.splitRight.Name = "splitRight";
			this.splitRight.TabIndex = 7;
			this.splitRight.TabStop = false;
			this.splitRight.UseAnimations = false;
			this.splitRight.VisualStyle = Scaffold.VisualStyles.DoubleDots;
			this.splitRight.MouseMove += new System.Windows.Forms.MouseEventHandler(this.splitRight_MouseMove);
			// 
			// splitLeft
			// 
			this.splitLeft.AnimationDelay = 20;
			this.splitLeft.AnimationStep = 20;
			this.splitLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.splitLeft.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
			this.splitLeft.ControlToHide = this.pnlLeft;
			this.splitLeft.ExpandParentForm = false;
			this.splitLeft.Location = new System.Drawing.Point(134, 66);
			this.splitLeft.Name = "splitLeft";
			this.splitLeft.TabIndex = 5;
			this.splitLeft.TabStop = false;
			this.splitLeft.UseAnimations = false;
			this.splitLeft.VisualStyle = Scaffold.VisualStyles.DoubleDots;
			this.splitLeft.MouseMove += new System.Windows.Forms.MouseEventHandler(this.splitLeft_MouseMove);
			// 
			// splitBottom
			// 
			this.splitBottom.AnimationDelay = 20;
			this.splitBottom.AnimationStep = 20;
			this.splitBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.splitBottom.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
			this.splitBottom.ControlToHide = this.pnlBottom;
			this.splitBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitBottom.ExpandParentForm = false;
			this.splitBottom.Location = new System.Drawing.Point(0, 278);
			this.splitBottom.Name = "splitBottom";
			this.splitBottom.TabIndex = 3;
			this.splitBottom.TabStop = false;
			this.splitBottom.UseAnimations = false;
			this.splitBottom.VisualStyle = Scaffold.VisualStyles.DoubleDots;
			this.splitBottom.MouseMove += new System.Windows.Forms.MouseEventHandler(this.splitBottom_MouseMove);
			// 
			// splitTop
			// 
			this.splitTop.AnimationDelay = 20;
			this.splitTop.AnimationStep = 20;
			this.splitTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
			this.splitTop.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
			this.splitTop.ControlToHide = this.pnlTop;
			this.splitTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitTop.ExpandParentForm = false;
			this.splitTop.Location = new System.Drawing.Point(0, 60);
			this.splitTop.Name = "splitTop";
			this.splitTop.TabIndex = 2;
			this.splitTop.TabStop = false;
			this.splitTop.UseAnimations = false;
			this.splitTop.VisualStyle = Scaffold.VisualStyles.DoubleDots;
			this.splitTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.splitTop_MouseMove);
			// 
			// PanelWindowControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlCenter);
			this.Controls.Add(this.splitRight);
			this.Controls.Add(this.pnlRight);
			this.Controls.Add(this.splitLeft);
			this.Controls.Add(this.pnlLeft);
			this.Controls.Add(this.splitBottom);
			this.Controls.Add(this.pnlBottom);
			this.Controls.Add(this.splitTop);
			this.Controls.Add(this.pnlTop);
			this.Name = "PanelWindowControl";
			this.Size = new System.Drawing.Size(665, 384);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlTop;
		private System.Windows.Forms.Panel pnlBottom;
		private CollapsibleSplitter splitTop;
		private CollapsibleSplitter splitBottom;
		private System.Windows.Forms.Panel pnlLeft;
		private CollapsibleSplitter splitLeft;
		private System.Windows.Forms.Panel pnlRight;
		private CollapsibleSplitter splitRight;
		private System.Windows.Forms.Panel pnlCenter;
	}
}
