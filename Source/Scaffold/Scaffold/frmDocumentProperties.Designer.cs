//	frmDocumentProperties.Designer.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
namespace Scaffold
{
	partial class frmDocumentProperties
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
			this.lblTicket = new System.Windows.Forms.Label();
			this.txtTicket = new System.Windows.Forms.TextBox();
			this.lblName = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblDescription = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblTicket
			// 
			this.lblTicket.AutoSize = true;
			this.lblTicket.Location = new System.Drawing.Point(15, 11);
			this.lblTicket.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTicket.Name = "lblTicket";
			this.lblTicket.Size = new System.Drawing.Size(181, 20);
			this.lblTicket.TabIndex = 0;
			this.lblTicket.Text = "Globally Unique &Ticket:";
			// 
			// txtTicket
			// 
			this.txtTicket.Location = new System.Drawing.Point(203, 8);
			this.txtTicket.Name = "txtTicket";
			this.txtTicket.ReadOnly = true;
			this.txtTicket.Size = new System.Drawing.Size(424, 27);
			this.txtTicket.TabIndex = 1;
			this.txtTicket.TabStop = false;
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Location = new System.Drawing.Point(15, 44);
			this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(103, 20);
			this.lblName.TabIndex = 2;
			this.lblName.Text = "Short &Name:";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(203, 41);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(424, 27);
			this.txtName.TabIndex = 3;
			// 
			// lblDescription
			// 
			this.lblDescription.AutoSize = true;
			this.lblDescription.Location = new System.Drawing.Point(15, 77);
			this.lblDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(100, 20);
			this.lblDescription.TabIndex = 4;
			this.lblDescription.Text = "&Description:";
			// 
			// txtDescription
			// 
			this.txtDescription.Location = new System.Drawing.Point(203, 74);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtDescription.Size = new System.Drawing.Size(424, 100);
			this.txtDescription.TabIndex = 5;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.ForeColor = System.Drawing.SystemColors.WindowText;
			this.btnCancel.Location = new System.Drawing.Point(429, 191);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 35);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.ForeColor = System.Drawing.SystemColors.WindowText;
			this.btnOK.Location = new System.Drawing.Point(531, 191);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(96, 35);
			this.btnOK.TabIndex = 6;
			this.btnOK.Text = "&OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// frmDocumentProperties
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(651, 249);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.lblDescription);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.txtTicket);
			this.Controls.Add(this.lblTicket);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.Gainsboro;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmDocumentProperties";
			this.Text = "Document Properties";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblTicket;
		private System.Windows.Forms.TextBox txtTicket;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
	}
}