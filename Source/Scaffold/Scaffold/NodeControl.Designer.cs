//	NodeControl.Designer.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
namespace Scaffold
{
	partial class NodeControl
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
			this.pnlView = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// pnlView
			// 
			this.pnlView.Location = new System.Drawing.Point(0, 0);
			this.pnlView.Name = "pnlView";
			this.pnlView.Size = new System.Drawing.Size(200, 100);
			this.pnlView.TabIndex = 0;
			this.pnlView.Click += new System.EventHandler(this.pnlView_Click);
			this.pnlView.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlView_Paint);
			this.pnlView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlView_MouseClick);
			this.pnlView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pnlView_MouseDoubleClick);
			this.pnlView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlView_MouseDown);
			this.pnlView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlView_MouseMove);
			this.pnlView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlView_MouseUp);
			this.pnlView.Resize += new System.EventHandler(this.pnlView_Resize);
			// 
			// NodeControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.AutoScrollMargin = new System.Drawing.Size(21, 21);
			this.AutoScrollMinSize = new System.Drawing.Size(32, 32);
			this.Controls.Add(this.pnlView);
			this.Name = "NodeControl";
			this.Size = new System.Drawing.Size(512, 384);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlView;
	}
}
