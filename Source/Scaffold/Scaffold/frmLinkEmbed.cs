//	frmLinkEmbed.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	frmLinkEmbed																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Link or embed resource form.
	/// </summary>
	public partial class frmLinkEmbed : Form
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
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
			DialogResult = DialogResult.Cancel;
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
			if(!btnOK.Focused)
			{
				btnOK.Focus();
			}
			DialogResult = DialogResult.OK;
			this.Hide();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* frmLinkEmbed_Activated																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The form has been activated.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void frmLinkEmbed_Activated(object sender, EventArgs e)
		{
			if(optEmbed.Checked)
			{
				optEmbed.Focus();
			}
			else if(optLink.Checked)
			{
				optLink.Focus();
			}
			else
			{
				btnCancel.Focus();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* opt_CheckedChanged																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state of an option button has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void opt_CheckedChanged(object sender, EventArgs e)
		{
			if(optEmbed.Checked)
			{
				lblEmbed.ForeColor = Color.Black;
				lblLink.ForeColor = Color.Gray;
				lblLinkFilename.ForeColor = Color.Gray;
				txtLinkFilename.BackColor = SystemColors.Control;
				txtLinkFilename.ForeColor = Color.Gray;
				txtLinkFilename.ReadOnly = true;
				txtLinkFilename.TabStop = false;
				btnOK.Enabled = (txtLinkFilename.Text.Length > 0);
				optLink.TabStop = true;
				optEmbed.TabStop = false;
			}
			else if(optLink.Checked)
			{
				if(mCanEditRelativeName)
				{
					lblLinkFilename.ForeColor = Color.Black;
					txtLinkFilename.BackColor = SystemColors.Window;
					txtLinkFilename.ForeColor = Color.Black;
					txtLinkFilename.ReadOnly = false;
					txtLinkFilename.TabStop = true;
				}
				else
				{
					lblLinkFilename.ForeColor = Color.Gray;
					txtLinkFilename.BackColor = SystemColors.Control;
					txtLinkFilename.ForeColor = Color.Gray;
					txtLinkFilename.ReadOnly = true;
					txtLinkFilename.TabStop = false;
				}
				lblLink.ForeColor = Color.Black;
				lblEmbed.ForeColor = Color.Gray;
				btnOK.Enabled = true;
				optEmbed.TabStop = true;
				optLink.TabStop = false;
			}
			else
			{
				btnOK.Enabled = false;
			}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnLoad																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the Load event when the form has been loaded and is ready to
		/// display.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			opt_CheckedChanged(null, null);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the frmLinkEmbed Item.
		/// </summary>
		public frmLinkEmbed()
		{
			InitializeComponent();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	CanEditRelativeName																										*
		//*-----------------------------------------------------------------------*
		private bool mCanEditRelativeName = true;
		/// <summary>
		/// Get/Set a value indicating whether the user can edit the relative
		/// filename.
		/// </summary>
		public bool CanEditRelativeName
		{
			get { return mCanEditRelativeName; }
			set { mCanEditRelativeName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Embed																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set a value indicating whether the user chooses to embed the
		/// resource.
		/// </summary>
		/// <remarks>
		/// If false, the resource will be linked.
		/// </remarks>
		public bool Embed
		{
			get { return optEmbed.Checked; }
			set
			{
				if(value)
				{
					optEmbed.Checked = true;
				}
				else
				{
					optLink.Checked = true;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	LinkFilename																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the filename of the linked resource.
		/// </summary>
		public string LinkFilename
		{
			get { return txtLinkFilename.Text; }
			set { txtLinkFilename.Text = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
