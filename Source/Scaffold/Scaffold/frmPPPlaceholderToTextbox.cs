using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Scaffold.ScaffoldNodesUtil;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	frmPPPlaceholderToTextbox																								*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Convert PowerPoint Content Placeholder To Textboxes Input Form.
	/// </summary>
	public partial class frmPPPlaceholderToTextbox : Form
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		/// <summary>
		/// Value indicating whether the mouse is currently pressed on the form.
		/// </summary>
		private bool mMouseDown = false;
		private Point mMouseLocation = Point.Empty;
		private bool mWindowActive = false;

		//*-----------------------------------------------------------------------*
		//* pnlClose_MouseClick																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has clicked the window close button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlClose_MouseClick(object sender, MouseEventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Hide();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlClose_MouseEnter																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has entered the area of the window close button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlClose_MouseEnter(object sender, EventArgs e)
		{
			pnlClose.BackColor = FromHex(ResourceMain.colorWinControlClose);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlClose_MouseLeave																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has left the area of the window close button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlClose_MouseLeave(object sender, EventArgs e)
		{
			pnlClose.BackColor = FromHex(ResourceMain.colorWinControlNormal);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlMaximize_MouseClick																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has clicked the window maximize button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlMaximize_MouseClick(object sender, MouseEventArgs e)
		{
			if(this.WindowState == FormWindowState.Maximized)
			{
				this.WindowState = FormWindowState.Normal;
			}
			else
			{
				this.WindowState = FormWindowState.Maximized;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlMaximize_MouseEnter																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has entered the area of the maximize button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlMaximize_MouseEnter(object sender, EventArgs e)
		{
			pnlMaximize.BackColor = FromHex(ResourceMain.colorWinControlHover);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlMaximize_MouseLeave																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has left the area of the maximize button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlMaximize_MouseLeave(object sender, EventArgs e)
		{
			pnlMaximize.BackColor = FromHex(ResourceMain.colorWinControlNormal);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlMinimize_MouseClick																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has clicked the minimize button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlMinimize_MouseClick(object sender, MouseEventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlMinimize_MouseEnter																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has entered the area of the minimize button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlMinimize_MouseEnter(object sender, EventArgs e)
		{
			pnlMinimize.BackColor = FromHex(ResourceMain.colorWinControlHover);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlMinimize_MouseLeave																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has left the area of the minimize button.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlMinimize_MouseLeave(object sender, EventArgs e)
		{
			pnlMinimize.BackColor = FromHex(ResourceMain.colorWinControlNormal);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTitle_MouseDown																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A mouse button has been depressed on the title bar.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTitle_MouseDown(object sender, MouseEventArgs e)
		{
			mMouseDown = true;
			mMouseLocation = e.Location;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTitle_MouseMove																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has been moved on the title bar.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTitle_MouseMove(object sender, MouseEventArgs e)
		{
			if(mMouseDown)
			{
				this.Location =
					new Point(
						this.Location.X + (e.X - mMouseLocation.X),
						this.Location.Y + (e.Y - mMouseLocation.Y));
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTitle_MouseUp																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A mouse button has been released on the title bar.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlTitle_MouseUp(object sender, MouseEventArgs e)
		{
			mMouseDown = false;
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnActivated																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the Activated event when the form has been activated.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnActivated(EventArgs e)
		{
			FlipbookItemControl item = null;

			base.OnActivated(e);
			lblTitle.ForeColor = FromHex(ResourceMain.colorTitleTextActive);
			pnlMinimize.BackgroundImage = ResourceMain.WinControlH1;
			if(this.WindowState == FormWindowState.Maximized)
			{
				pnlMaximize.BackgroundImage = ResourceMain.WinControlN1;
			}
			else
			{
				pnlMaximize.BackgroundImage = ResourceMain.WinControlM1;
			}
			pnlClose.BackgroundImage = ResourceMain.WinControlX1;
			mWindowActive = true;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnDeactivate																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the Deactivate event when the form is going to be deactivated.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnDeactivate(EventArgs e)
		{
			base.OnDeactivate(e);
			base.OnLeave(e);
			lblTitle.ForeColor = FromHex(ResourceMain.colorTitleTextInactive);
			pnlMinimize.BackgroundImage = ResourceMain.WinControlH0;
			if(this.WindowState == FormWindowState.Maximized)
			{
				pnlMaximize.BackgroundImage = ResourceMain.WinControlN0;
			}
			else
			{
				pnlMaximize.BackgroundImage = ResourceMain.WinControlM0;
			}
			pnlClose.BackgroundImage = ResourceMain.WinControlX0;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnEnter																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the Enter event when the form has received focus.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnLeave																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the Leave event when the form has lost focus.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnLeave(EventArgs e)
		{
			mWindowActive = false;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSizeChanged																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SizeChanged event when the size of the form has changed.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			if(this.WindowState == FormWindowState.Maximized)
			{
				if(mWindowActive)
				{
					pnlMaximize.BackgroundImage = ResourceMain.WinControlN1;
				}
				else
				{
					pnlMaximize.BackgroundImage = ResourceMain.WinControlN0;
				}
			}
			else
			{
				if(mWindowActive)
				{
					pnlMaximize.BackgroundImage = ResourceMain.WinControlM1;
				}
				else
				{
					pnlMaximize.BackgroundImage = ResourceMain.WinControlM0;
				}
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
		/// Create a new instance of the frmPPPlaceholderToTextbox Item.
		/// </summary>
		public frmPPPlaceholderToTextbox()
		{
			InitializeComponent();

			this.menuPlaceholderToTextbox.Renderer =
				new DarkThemeMenuRenderer(new DarkThemeMenuColorTable());

			//	Form controls.
			pnlClose.MouseClick += pnlClose_MouseClick;
			pnlClose.MouseEnter += pnlClose_MouseEnter;
			pnlClose.MouseLeave += pnlClose_MouseLeave;
			pnlMaximize.MouseClick += pnlMaximize_MouseClick;
			pnlMaximize.MouseEnter += pnlMaximize_MouseEnter;
			pnlMaximize.MouseLeave += pnlMaximize_MouseLeave;
			pnlMinimize.MouseClick += pnlMinimize_MouseClick;
			pnlMinimize.MouseEnter += pnlMinimize_MouseEnter;
			pnlMinimize.MouseLeave += pnlMinimize_MouseLeave;
			pnlTitle.MouseDown += pnlTitle_MouseDown;
			pnlTitle.MouseMove += pnlTitle_MouseMove;
			pnlTitle.MouseUp += pnlTitle_MouseUp;
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

}
