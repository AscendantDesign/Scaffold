//	LabelButtonControl.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//* LabelButtonControlCollection																						*
	//*-------------------------------------------------------------------------*
	public partial class LabelButtonControlCollection : Component
	{
		//*-----------------------------------------------------------------------*
		//*	Controls																															*
		//*-----------------------------------------------------------------------*
		private List<LabelButtonControl> mControls =
			new List<LabelButtonControl>();
		/// <summary>
		/// Get a reference to the collection of label button controls managed in
		/// this collection.
		/// </summary>
		public List<LabelButtonControl> Controls
		{
			get { return mControls; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Selecting																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Notifiy the manager a particular control is selecting.
		/// </summary>
		/// <param name="control">
		/// Reference to the control being selected.
		/// </param>
		/// <returns>
		/// Value indicating success of the operation.
		/// </returns>
		public bool Selecting(LabelButtonControl control)
		{
			bool result = true;

			if(mSelectionMode == SelectionMode.One)
			{
				//	Deselect all others.
				mControls.FindAll(x => x != control && x.Selected).
					ForEach(x => x.Selected = false);
			}
			else if(mSelectionMode == SelectionMode.None)
			{
				//	Unselect this item.
				result = false;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SelectedItem																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get a reference to the first selected item in the collection.
		/// </summary>
		public LabelButtonControl SelectedItem
		{
			get
			{
				LabelButtonControl result =
					mControls.FirstOrDefault(x => x.Selected == true);
				return result;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SelectionMode																													*
		//*-----------------------------------------------------------------------*
		private System.Windows.Forms.SelectionMode mSelectionMode =
			System.Windows.Forms.SelectionMode.One;
		/// <summary>
		/// Get/Set the selection mode allowed for buttons in this group.
		/// </summary>
		public System.Windows.Forms.SelectionMode SelectionMode
		{
			get { return mSelectionMode; }
			set { mSelectionMode = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	LabelButtonControl																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Label button able to behave like a disconnected tab button.
	/// </summary>
	public partial class LabelButtonControl : Control
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//private Color mCurrentBackColor;
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnPaint																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the Paint event when the button needs to be painted.
		/// </summary>
		/// <param name="pevent">
		/// Paint event arguments.
		/// </param>
		protected override void OnPaint(PaintEventArgs pevent)
		{
			//base.OnPaint(pevent);

			//pevent.Graphics.FillRectangle(new SolidBrush(mCurrentBackColor), 0, 0, Width, Height);

			//TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;

			//TextRenderer.DrawText(pevent.Graphics, Text, Font, new Point(Width + 3, Height / 2), ForeColor, flags);

			//base.OnPaint(pevent);
			Rectangle bar =
				new Rectangle(0, this.Height - 6, this.Width, 6);
			Brush brush = null;
			Color color = Color.Black;
			TextFormatFlags flags =
				TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
			Graphics g = pevent.Graphics;

			//	Fill with background.
			brush = new SolidBrush(this.BackColor);
			g.FillRectangle(brush, 0, 0, this.Width, this.Height);
			//	Draw the bar.
			if(this.mSelected)
			{
				brush = new SolidBrush(mSelectedBarColor);
				g.FillRectangle(brush, bar);
				color = mSelectedTextColor;
			}
			else
			{
				color = mNormalColor;
			}
			TextRenderer.DrawText(pevent.Graphics, this.Text, this.Font,
				new Point(Width + 3, Height / 2), color, flags);
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*-----------------------------------------------------------------------*
		//protected override void OnMouseEnter(EventArgs e)

		//{
		//	base.OnMouseEnter(e);
		//	CurrentBackColor = onHoverBackColor;
		//	Invalidate();
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnMouseDown																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the MouseDown event when a mouse button has been depressed.
		/// </summary>
		/// <param name="mevent">
		/// Mouse event arguments.
		/// </param>
		protected override void OnMouseDown(MouseEventArgs mevent)
		{
			base.OnMouseDown(mevent);
			//mCurrentBackColor = Color.RoyalBlue;
			if(mManager != null)
			{
				//	This button has a manager.
				if(mSelected &&
					mManager.SelectionMode == SelectionMode.MultiExtended &&
					(Control.ModifierKeys & Keys.Control) == Keys.Control)
				{
					//	The item is selected, multi-extended is active, and [Ctrl] is
					//	pressed. Toggle the item.
					mSelected = !mSelected;
				}
				else if(!mSelected)
				{
					//	Straight click. Select this one only.
					foreach(LabelButtonControl control in mManager.Controls)
					{
						if(control != this && control.Selected)
						{
							control.Selected = false;
						}
					}
					this.Selected = true;
				}
			}
			else
			{
				//	The button doesn't have a manager. Just toggle.
				mSelected = !mSelected;
			}
			Invalidate();
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*-----------------------------------------------------------------------*
		//protected override void OnMouseLeave(EventArgs e)
		//{
		//	base.OnMouseLeave(e);
		//	CurrentBackColor = BackColor;
		//	Invalidate();
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	OnSelectedChanged																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SelectedChanged event when the value of the Selected
		/// property has changed.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected virtual void OnSelectedChanged(EventArgs e)
		{
			SelectedChanged?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the LabelButtonControl Item.
		/// </summary>
		public LabelButtonControl() : base()
		{
			this.BackColor = Color.Black;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Manager																																*
		//*-----------------------------------------------------------------------*
		private LabelButtonControlCollection mManager = null;
		/// <summary>
		/// Get/Set a reference to the label button control collection managing
		/// this button.
		/// </summary>
		public LabelButtonControlCollection Manager
		{
			get { return mManager; }
			set
			{
				mManager = value;
				if(mManager != null &&
					mManager.Controls.FirstOrDefault(x => x == this) == null)
				{
					//	Add this control to the manager collection.
					mManager.Controls.Add(this);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NormalColor																														*
		//*-----------------------------------------------------------------------*
		private Color mNormalColor = Color.Black;
		/// <summary>
		/// Get/Set the foreground color to use when not selected.
		/// </summary>
		public Color NormalColor
		{
			get { return mNormalColor; }
			set { mNormalColor = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Selected																															*
		//*-----------------------------------------------------------------------*
		private bool mSelected = false;
		/// <summary>
		/// Get/Set a value indicating whether this button is selected.
		/// </summary>
		public bool Selected
		{
			get { return mSelected; }
			set
			{
				bool oldValue = mSelected;

				mSelected = value;
				if(mManager != null && mSelected)
				{
					mManager.Selecting(this);
				}
				if(oldValue != value)
				{
					OnSelectedChanged(new EventArgs());
					Invalidate();
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SelectedChanged																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the value of the Selected property has changed.
		/// </summary>
		public event EventHandler SelectedChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SelectedBarColor																											*
		//*-----------------------------------------------------------------------*
		private Color mSelectedBarColor = Color.Black;
		/// <summary>
		/// Get/Set the text color to use when selected.
		/// </summary>
		public Color SelectedBarColor
		{
			get { return mSelectedBarColor; }
			set { mSelectedBarColor = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SelectedTextColor																											*
		//*-----------------------------------------------------------------------*
		private Color mSelectedTextColor = Color.Black;
		/// <summary>
		/// Get/Set the text color to use when selected.
		/// </summary>
		public Color SelectedTextColor
		{
			get { return mSelectedTextColor; }
			set { mSelectedTextColor = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
