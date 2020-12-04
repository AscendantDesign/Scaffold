
namespace Scaffold
{
	partial class FlipbookItemControl
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
			this.picIcon = new System.Windows.Forms.PictureBox();
			this.lblIndex = new System.Windows.Forms.Label();
			this.lblName = new System.Windows.Forms.Label();
			this.lblTimer = new System.Windows.Forms.Label();
			this.lblTimerMeasure = new System.Windows.Forms.Label();
			this.picThumb = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picThumb)).BeginInit();
			this.SuspendLayout();
			// 
			// picIcon
			// 
			this.picIcon.Image = global::Scaffold.ResourceMain.FilmstripArrow32;
			this.picIcon.Location = new System.Drawing.Point(3, 3);
			this.picIcon.Name = "picIcon";
			this.picIcon.Size = new System.Drawing.Size(32, 32);
			this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.picIcon.TabIndex = 0;
			this.picIcon.TabStop = false;
			// 
			// lblIndex
			// 
			this.lblIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblIndex.AutoEllipsis = true;
			this.lblIndex.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblIndex.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.lblIndex.Location = new System.Drawing.Point(41, 3);
			this.lblIndex.Name = "lblIndex";
			this.lblIndex.Size = new System.Drawing.Size(246, 21);
			this.lblIndex.TabIndex = 1;
			this.lblIndex.Text = "Frame-100762";
			// 
			// lblName
			// 
			this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblName.AutoEllipsis = true;
			this.lblName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblName.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.lblName.Location = new System.Drawing.Point(41, 24);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(118, 22);
			this.lblName.TabIndex = 1;
			this.lblName.Text = "Frame-100762";
			// 
			// lblTimer
			// 
			this.lblTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTimer.AutoEllipsis = true;
			this.lblTimer.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTimer.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.lblTimer.Location = new System.Drawing.Point(165, 24);
			this.lblTimer.Name = "lblTimer";
			this.lblTimer.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblTimer.Size = new System.Drawing.Size(73, 22);
			this.lblTimer.TabIndex = 1;
			this.lblTimer.Text = "0";
			this.lblTimer.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblTimerMeasure
			// 
			this.lblTimerMeasure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTimerMeasure.AutoSize = true;
			this.lblTimerMeasure.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTimerMeasure.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.lblTimerMeasure.Location = new System.Drawing.Point(244, 24);
			this.lblTimerMeasure.Name = "lblTimerMeasure";
			this.lblTimerMeasure.Size = new System.Drawing.Size(28, 18);
			this.lblTimerMeasure.TabIndex = 1;
			this.lblTimerMeasure.Text = "ms";
			// 
			// picThumb
			// 
			this.picThumb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.picThumb.Location = new System.Drawing.Point(16, 64);
			this.picThumb.Name = "picThumb";
			this.picThumb.Size = new System.Drawing.Size(256, 144);
			this.picThumb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picThumb.TabIndex = 2;
			this.picThumb.TabStop = false;
			// 
			// FlipbookItemControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.Controls.Add(this.picThumb);
			this.Controls.Add(this.lblTimerMeasure);
			this.Controls.Add(this.lblTimer);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.lblIndex);
			this.Controls.Add(this.picIcon);
			this.Name = "FlipbookItemControl";
			this.Size = new System.Drawing.Size(290, 221);
			((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picThumb)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox picIcon;
		private System.Windows.Forms.Label lblIndex;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblTimer;
		private System.Windows.Forms.Label lblTimerMeasure;
		private System.Windows.Forms.PictureBox picThumb;
	}
}
