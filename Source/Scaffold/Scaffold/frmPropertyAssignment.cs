//	frmPropertyAssignment.cs
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
	//*	frmPropertyAssignment																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Property assignment form.
	/// </summary>
	public partial class frmPropertyAssignment : Form
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
		//* frmPropertyAssignment_Activated																				*
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
		private void frmPropertyAssignment_Activated(object sender, EventArgs e)
		{
			if(txtValue.ReadOnly)
			{
				txtName.Focus();
			}
			else if(txtName.ReadOnly)
			{
				txtValue.Focus();
			}
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
		/// Create a new instance of the frmPropertyAssignment Item.
		/// </summary>
		public frmPropertyAssignment()
		{
			InitializeComponent();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PropertyName																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the property name.
		/// </summary>
		public string PropertyName
		{
			get { return txtName.Text; }
			set { txtName.Text = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PropertyNameReadOnly																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set a value indicating whether the property name will be read-only
		/// during this session.
		/// </summary>
		public bool PropertyNameReadOnly
		{
			get { return txtName.ReadOnly; }
			set
			{
				txtName.ReadOnly = value;
				txtName.BackColor =
					(value ? SystemColors.Control : SystemColors.Window);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PropertyValue																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the property value.
		/// </summary>
		public string PropertyValue
		{
			get { return txtValue.Text; }
			set { txtValue.Text = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PropertyValueReadOnly																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set a value indicating whether the property value will be
		/// read-only during this session.
		/// </summary>
		public bool PropertyValueReadOnly
		{
			get { return txtValue.ReadOnly; }
			set
			{
				txtValue.ReadOnly = value;
				txtValue.BackColor =
					(value ? SystemColors.Control : SystemColors.Window);
			}
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

}
