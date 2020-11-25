//	frmInputBox.cs
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
	//*	frmInputBox																															*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// User input dialog.
	/// </summary>
	public partial class frmInputBox : Form
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* btnCancel_Click																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Cancel button has been clicked.
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
			this.DialogResult = DialogResult.OK;
			this.Hide();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* frmInputBox_Activated																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The form has been activated. Reselect the text value.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void frmInputBox_Activated(object sender, EventArgs e)
		{
			btnOK.Focus();
			txtValue.Focus();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtValue_Enter																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Textbox received the focus.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtValue_Enter(object sender, EventArgs e)
		{
			txtValue.SelectionStart = 0;
			txtValue.SelectionLength = txtValue.Text.Length;
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the frmInputBox Item.
		/// </summary>
		public frmInputBox()
		{
			InitializeComponent();
			base.Text = "User Input";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Prompt																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the text prompt on the dialog.
		/// </summary>
		public string Prompt
		{
			get { return lblPrompt.Text; }
			set { lblPrompt.Text = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Text																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the user text.
		/// </summary>
		public new string Text
		{
			get { return txtValue.Text; }
			set { txtValue.Text = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Title																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the title of the dialog.
		/// </summary>
		public string Title
		{
			get { return base.Text; }
			set { base.Text = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

}
