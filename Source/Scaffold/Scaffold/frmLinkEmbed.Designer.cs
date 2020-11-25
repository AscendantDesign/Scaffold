//	frmLinkEmbed.Designer.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
namespace Scaffold
{
	partial class frmLinkEmbed
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
			this.lblDescription = new System.Windows.Forms.Label();
			this.lblLink = new System.Windows.Forms.Label();
			this.lblEmbed = new System.Windows.Forms.Label();
			this.optLink = new System.Windows.Forms.RadioButton();
			this.lblLinkFilename = new System.Windows.Forms.Label();
			this.txtLinkFilename = new System.Windows.Forms.TextBox();
			this.optEmbed = new System.Windows.Forms.RadioButton();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblDescription
			// 
			this.lblDescription.AutoSize = true;
			this.lblDescription.Location = new System.Drawing.Point(13, 19);
			this.lblDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(359, 20);
			this.lblDescription.TabIndex = 0;
			this.lblDescription.Text = "You can choose to link or embed this resource.";
			// 
			// lblLink
			// 
			this.lblLink.ForeColor = System.Drawing.Color.Gray;
			this.lblLink.Location = new System.Drawing.Point(39, 101);
			this.lblLink.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblLink.Name = "lblLink";
			this.lblLink.Size = new System.Drawing.Size(427, 70);
			this.lblLink.TabIndex = 2;
			this.lblLink.Text = "If you link to the resource, this document\'s data file will be much smaller in si" +
    "ze, but external files will have to be copied with the document.";
			// 
			// lblEmbed
			// 
			this.lblEmbed.Location = new System.Drawing.Point(39, 267);
			this.lblEmbed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblEmbed.Name = "lblEmbed";
			this.lblEmbed.Size = new System.Drawing.Size(427, 65);
			this.lblEmbed.TabIndex = 6;
			this.lblEmbed.Text = "If you embed the resource, your document can be copied to any destination and use" +
    "d anywhere without external files, but the data file might be very large.";
			// 
			// optLink
			// 
			this.optLink.AutoSize = true;
			this.optLink.Location = new System.Drawing.Point(17, 74);
			this.optLink.Name = "optLink";
			this.optLink.Size = new System.Drawing.Size(223, 24);
			this.optLink.TabIndex = 1;
			this.optLink.TabStop = true;
			this.optLink.Text = "&Link to External Resource";
			this.optLink.UseVisualStyleBackColor = true;
			this.optLink.CheckedChanged += new System.EventHandler(this.opt_CheckedChanged);
			// 
			// lblLinkFilename
			// 
			this.lblLinkFilename.AutoSize = true;
			this.lblLinkFilename.ForeColor = System.Drawing.Color.Gray;
			this.lblLinkFilename.Location = new System.Drawing.Point(39, 185);
			this.lblLinkFilename.Name = "lblLinkFilename";
			this.lblLinkFilename.Size = new System.Drawing.Size(118, 20);
			this.lblLinkFilename.TabIndex = 3;
			this.lblLinkFilename.Text = "Link &Filename:";
			// 
			// txtLinkFilename
			// 
			this.txtLinkFilename.BackColor = System.Drawing.SystemColors.Control;
			this.txtLinkFilename.ForeColor = System.Drawing.Color.Gray;
			this.txtLinkFilename.Location = new System.Drawing.Point(163, 182);
			this.txtLinkFilename.Name = "txtLinkFilename";
			this.txtLinkFilename.ReadOnly = true;
			this.txtLinkFilename.Size = new System.Drawing.Size(303, 27);
			this.txtLinkFilename.TabIndex = 4;
			this.txtLinkFilename.TabStop = false;
			// 
			// optEmbed
			// 
			this.optEmbed.AutoSize = true;
			this.optEmbed.Checked = true;
			this.optEmbed.Location = new System.Drawing.Point(17, 240);
			this.optEmbed.Name = "optEmbed";
			this.optEmbed.Size = new System.Drawing.Size(159, 24);
			this.optEmbed.TabIndex = 5;
			this.optEmbed.TabStop = true;
			this.optEmbed.Text = "&Embed Resource";
			this.optEmbed.UseVisualStyleBackColor = true;
			this.optEmbed.CheckedChanged += new System.EventHandler(this.opt_CheckedChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(310, 360);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 40);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(391, 360);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 40);
			this.btnOK.TabIndex = 8;
			this.btnOK.Text = "&OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// frmLinkEmbed
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(479, 412);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.txtLinkFilename);
			this.Controls.Add(this.lblLinkFilename);
			this.Controls.Add(this.optEmbed);
			this.Controls.Add(this.optLink);
			this.Controls.Add(this.lblEmbed);
			this.Controls.Add(this.lblLink);
			this.Controls.Add(this.lblDescription);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmLinkEmbed";
			this.Text = "Link or Embed";
			this.Activated += new System.EventHandler(this.frmLinkEmbed_Activated);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.Label lblLink;
		private System.Windows.Forms.Label lblEmbed;
		private System.Windows.Forms.RadioButton optLink;
		private System.Windows.Forms.Label lblLinkFilename;
		private System.Windows.Forms.TextBox txtLinkFilename;
		private System.Windows.Forms.RadioButton optEmbed;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
	}
}