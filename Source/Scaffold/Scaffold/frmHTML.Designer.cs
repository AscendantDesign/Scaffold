//	frmHTML.Designer.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
namespace Scaffold
{
			//this.menuStrip.Renderer = new DarkThemeMenuRenderer(new DarkThemeMenuColorTable());
	partial class frmHTML
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
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.statMessage = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.pnlHTML = new System.Windows.Forms.Panel();
			this.statusStrip.SuspendLayout();
			this.pnlHTML.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Renderer = new DarkThemeMenuRenderer(new DarkThemeMenuColorTable());
			this.menuStrip.Size = new System.Drawing.Size(800, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip1";
			// 
			// statusStrip
			// 
			this.statusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statMessage});
			this.statusStrip.Location = new System.Drawing.Point(0, 424);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(800, 26);
			this.statusStrip.TabIndex = 1;
			this.statusStrip.Text = "statusStrip1";
			// 
			// statMessage
			// 
			this.statMessage.ForeColor = System.Drawing.Color.Gainsboro;
			this.statMessage.Name = "statMessage";
			this.statMessage.Size = new System.Drawing.Size(59, 20);
			this.statMessage.Text = "Ready...";
			// 
			// toolStrip
			// 
			this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.toolStrip.Location = new System.Drawing.Point(0, 24);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(800, 25);
			this.toolStrip.TabIndex = 2;
			this.toolStrip.Text = "toolStrip1";
			this.toolStrip.Visible = false;
			// 
			// pnlHTML
			// 
			this.pnlHTML.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlHTML.Location = new System.Drawing.Point(0, 24);
			this.pnlHTML.Name = "pnlHTML";
			this.pnlHTML.Size = new System.Drawing.Size(800, 400);
			this.pnlHTML.TabIndex = 3;
			// 
			// frmHTML
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.pnlHTML);
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.menuStrip);
			this.MainMenuStrip = this.menuStrip;
			this.Name = "frmHTML";
			this.Text = "frmHTML";
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.pnlHTML.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripStatusLabel statMessage;
		private System.Windows.Forms.Panel pnlHTML;
	}
}