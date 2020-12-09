using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	frm{FormName}																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// {FormDescription}.
	/// </summary>
	public class frm{FormName} : ThemedForm
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		//*-----------------------------------------------------------------------*
		//* InitializeComponent																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Initialize the form.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources =
				new System.ComponentModel.
				ComponentResourceManager(typeof(frm{FormName}));
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

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the frm{FormName} Item.
		/// </summary>
		public frm{FormName}()
		{
			InitializeComponent();
			this.StartPosition = FormStartPosition.CenterParent;
			this.Title = "Form Title";
			this.statusThemedForm.Visible = false;

		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
