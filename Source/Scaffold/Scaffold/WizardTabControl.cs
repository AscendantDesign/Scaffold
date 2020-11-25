//	WizardTabControl.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	WizardTabControl																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Tab control with header during design time and no header during run time.
	/// </summary>
	/// <remarks>
	/// The approach is necessary to eliminate gray borders, which appear on a
	/// normal tab control when the header is hidden.
	/// </remarks>
	public partial class WizardTabControl : TabControl
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* WndProc																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Windows message processing pump.
		/// </summary>
		/// <param name="m">
		/// Reference to the Windows message to process.
		/// </param>
		protected override void WndProc(ref Message m)
		{
			if(m.Msg == 0x1328 && (!this.DesignMode || !mShowTabs))
			{
				m.Result = new IntPtr(1);
			}
			else
			{
				base.WndProc(ref m);
			}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the TabControlNoHeader Item.
		/// </summary>
		public WizardTabControl()
		{
			if(!this.DesignMode || !mShowTabs)
			{
				this.Multiline = true;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ShowTabs																															*
		//*-----------------------------------------------------------------------*
		private bool mShowTabs = true;
		/// <summary>
		/// Get/Set a value indicating whether to view tabs in design mode.
		/// </summary>
		public bool ShowTabs
		{
			get { return mShowTabs; }
			set
			{
				mShowTabs = value;
				this.Invalidate();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SelectedTabIndex																											*
		//*-----------------------------------------------------------------------*
		private int mSelectedTabIndex = 0;
		/// <summary>
		/// Get/Set the index of the currently selected tab page.
		/// </summary>
		public int SelectedTabIndex
		{
			get { return mSelectedTabIndex; }
			set
			{
				mSelectedTabIndex = value;
				if(this.TabPages.Count > mSelectedTabIndex)
				{
					this.SelectedIndex = mSelectedTabIndex;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SelectedTabName																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the name of the selected tab.
		/// </summary>
		public string SelectedTabName
		{
			get { return this.SelectedTab.Text; }
			set
			{
				int count = this.TabCount;
				int index = 0;
				TabPage tab = null;


				for(index = 0; index < count; index ++)
				{
					tab = this.TabPages[index];
					if(tab.Text == value)
					{
						this.SelectedIndex = index;
						break;
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

}
