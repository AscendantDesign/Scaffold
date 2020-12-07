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
	//*	ThemedForm																															*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Convert PowerPoint Content Placeholder To Textboxes Input Form.
	/// </summary>
	public partial class ThemedForm : Form
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Value indicating whether the mouse is currently pressed on the form.
		/// </summary>
		protected bool mMouseDown = false;
		protected Point mMouseLocation = Point.Empty;
		protected bool mWindowActive = false;

		//	Local controls.
		protected Label lblTitle = null;
		protected MenuStrip menuThemedForm = null;
		protected Panel pnlClose = null;
		protected Panel pnlIcon = null;
		protected Panel pnlMain = null;
		protected Panel pnlMaximize = null;
		protected Panel pnlMinimize = null;
		protected Panel pnlTitle = null;
		protected ToolStripStatusLabel statMessage = null;
		protected StatusStrip statusThemedForm = null;

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
		protected virtual void pnlClose_MouseClick(object sender, MouseEventArgs e)
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
		protected virtual void pnlClose_MouseEnter(object sender, EventArgs e)
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
		protected virtual void pnlClose_MouseLeave(object sender, EventArgs e)
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
		protected virtual void pnlMaximize_MouseClick(object sender, MouseEventArgs e)
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
		protected virtual void pnlMaximize_MouseEnter(object sender, EventArgs e)
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
		protected virtual void pnlMaximize_MouseLeave(object sender, EventArgs e)
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
		protected virtual void pnlMinimize_MouseClick(object sender, MouseEventArgs e)
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
		protected virtual void pnlMinimize_MouseEnter(object sender, EventArgs e)
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
		protected virtual void pnlMinimize_MouseLeave(object sender, EventArgs e)
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
		protected virtual void pnlTitle_MouseDown(object sender, MouseEventArgs e)
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
		protected virtual void pnlTitle_MouseMove(object sender, MouseEventArgs e)
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
		protected virtual void pnlTitle_MouseUp(object sender, MouseEventArgs e)
		{
			mMouseDown = false;
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
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
		//* InitializeComponent																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Initialize the form.
		/// </summary>
		protected virtual void InitializeComponent()
		{
			//	TODO: !1 - Stopped here.
			//	TODO: Working on themed form base.
			pnlTitle = new System.Windows.Forms.Panel();
			pnlClose = new System.Windows.Forms.Panel();
			pnlMaximize = new System.Windows.Forms.Panel();
			pnlMinimize = new System.Windows.Forms.Panel();
			lblTitle = new System.Windows.Forms.Label();
			pnlIcon = new System.Windows.Forms.Panel();
			pnlMain = new System.Windows.Forms.Panel();
			menuThemedForm = new System.Windows.Forms.MenuStrip();
			statusThemedForm = new System.Windows.Forms.StatusStrip();
			statMessage = new System.Windows.Forms.ToolStripStatusLabel();
			pnlTitle.SuspendLayout();
			pnlMain.SuspendLayout();
			statusThemedForm.SuspendLayout();
			SuspendLayout();
			// 
			// pnlTitle
			// 
			pnlTitle.BackColor = FromHex(ResourceMain.colorTitleBackgroundNormal);
			pnlTitle.Controls.Add(pnlClose);
			pnlTitle.Controls.Add(pnlMaximize);
			pnlTitle.Controls.Add(pnlMinimize);
			pnlTitle.Controls.Add(lblTitle);
			pnlTitle.Controls.Add(pnlIcon);
			pnlTitle.Dock = DockStyle.Top;
			pnlTitle.Location = new Point(0, 0);
			pnlTitle.Margin = new Padding(0);
			pnlTitle.Name = "pnlTitle";
			pnlTitle.Size = new Size(800, 48);
			pnlTitle.TabIndex = 0;
			// 
			// pnlClose
			// 
			pnlClose.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			pnlClose.BackgroundImage = global::Scaffold.ResourceMain.WinControlX0;
			pnlClose.BackgroundImageLayout = ImageLayout.Center;
			pnlClose.Location = new Point(752, 0);
			pnlClose.Margin = new Padding(0);
			pnlClose.Name = "pnlClose";
			pnlClose.Size = new Size(48, 48);
			pnlClose.TabIndex = 4;
			// 
			// pnlMaximize
			// 
			pnlMaximize.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			pnlMaximize.BackgroundImage = global::Scaffold.ResourceMain.WinControlM0;
			pnlMaximize.BackgroundImageLayout = ImageLayout.Center;
			pnlMaximize.Location = new Point(704, 0);
			pnlMaximize.Margin = new Padding(0);
			pnlMaximize.Name = "pnlMaximize";
			pnlMaximize.Size = new Size(48, 48);
			pnlMaximize.TabIndex = 3;
			// 
			// pnlMinimize
			// 
			pnlMinimize.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			pnlMinimize.BackgroundImage = global::Scaffold.ResourceMain.WinControlH0;
			pnlMinimize.BackgroundImageLayout = ImageLayout.Center;
			pnlMinimize.Location = new Point(656, 0);
			pnlMinimize.Margin = new Padding(0);
			pnlMinimize.Name = "pnlMinimize";
			pnlMinimize.Size = new Size(48, 48);
			pnlMinimize.TabIndex = 2;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.ForeColor = Color.DarkGray;
			lblTitle.Location = new Point(57, 14);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(202, 20);
			lblTitle.TabIndex = 1;
			lblTitle.Text = "Form Title";
			// 
			// pnlIcon
			// 
			pnlIcon.BackgroundImage = global::Scaffold.ResourceMain.ScaffoldIcon24;
			pnlIcon.BackgroundImageLayout = ImageLayout.Center;
			pnlIcon.Location = new Point(12, 12);
			pnlIcon.Margin = new Padding(0);
			pnlIcon.Name = "pnlIcon";
			pnlIcon.Size = new Size(24, 24);
			pnlIcon.TabIndex = 0;
			// 
			// pnlMain
			// 
			pnlMain.Controls.Add(statusThemedForm);
			pnlMain.Controls.Add(menuThemedForm);
			pnlMain.Dock = DockStyle.Fill;
			pnlMain.Location = new Point(0, 48);
			pnlMain.Name = "pnlMain";
			pnlMain.Size = new Size(800, 432);
			pnlMain.TabIndex = 1;
			// 
			// menuThemedForm
			// 
			menuThemedForm.ImageScalingSize = new Size(20, 20);
			menuThemedForm.Location = new Point(0, 0);
			menuThemedForm.Name = "menuThemedForm";
			menuThemedForm.Size = new Size(800, 24);
			menuThemedForm.TabIndex = 0;
			menuThemedForm.Text = "menuThemedForm";
			// 
			// statusThemedForm
			// 
			statusThemedForm.BackColor = FromHex(ResourceMain.colorBackground);
			statusThemedForm.ImageScalingSize = new Size(20, 20);
			statusThemedForm.Items.AddRange(new ToolStripItem[] {
				statMessage});
			statusThemedForm.Location = new Point(0, 406);
			statusThemedForm.Name = "statusThemedForm";
			statusThemedForm.Size = new Size(800, 26);
			statusThemedForm.TabIndex = 1;
			statusThemedForm.Text = "statusThemedForm";
			// 
			// statMessage
			// 
			statMessage.ForeColor = FromHex(ResourceMain.colorTextNormal);
			statMessage.Name = "statMessage";
			statMessage.Size = new Size(59, 20);
			statMessage.Text = "Ready...";
			// 
			// ThemedFormBase
			// 
			AutoScaleDimensions = new SizeF(10F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = FromHex(ResourceMain.colorBackground);
			ClientSize = new System.Drawing.Size(800, 480);
			Controls.Add(pnlMain);
			Controls.Add(pnlTitle);
			Font = new Font("Microsoft Sans Serif", 10.2F,
				FontStyle.Regular, GraphicsUnit.Point, (byte)0);
			FormBorderStyle = FormBorderStyle.None;
			MainMenuStrip = menuThemedForm;
			Margin = new Padding(4);
			Name = "ThemedFormBase";
			Text = "ThemedFormBase";
			pnlTitle.ResumeLayout(false);
			pnlTitle.PerformLayout();
			pnlMain.ResumeLayout(false);
			pnlMain.PerformLayout();
			statusThemedForm.ResumeLayout(false);
			statusThemedForm.PerformLayout();
			ResumeLayout(false);
		}
		//*-----------------------------------------------------------------------*

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
		/// Create a new instance of the ThemedForm Item.
		/// </summary>
		public ThemedForm()
		{
			InitializeComponent();

			this.menuThemedForm.Renderer =
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