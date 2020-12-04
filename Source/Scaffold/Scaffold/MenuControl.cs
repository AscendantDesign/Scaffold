using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	MenuControlCollection																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of MenuControlItem Items.
	/// </summary>
	public class MenuControlCollection : List<MenuControlItem>
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	MenuControlItem																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about a menu item and an associated windows control.
	/// </summary>
	public class MenuControlItem
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
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
		/// Create a new instance of the MenuControlItem Item.
		/// </summary>
		public MenuControlItem()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the MenuControlItem Item.
		/// </summary>
		public MenuControlItem(ToolStripMenuItem menu, Control control)
		{
			if(menu != null)
			{
				mMenuItem = menu;
			}
			if(control != null)
			{
				mControlItem = control;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ControlItem																														*
		//*-----------------------------------------------------------------------*
		private Control mControlItem = null;
		/// <summary>
		/// Get/Set a reference to the control for this instance.
		/// </summary>
		public Control ControlItem
		{
			get { return mControlItem; }
			set { mControlItem = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MenuItem																															*
		//*-----------------------------------------------------------------------*
		private ToolStripMenuItem mMenuItem = null;
		/// <summary>
		/// Get/Set a reference to the menu item for this instance.
		/// </summary>
		public ToolStripMenuItem MenuItem
		{
			get { return mMenuItem; }
			set { mMenuItem = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
