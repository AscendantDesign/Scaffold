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
		private Label lblTitle = null;
		protected MenuStrip menuThemedForm = null;
		private Panel pnlClose = null;
		private Panel pnlForm = null;
		private Panel pnlIcon = null;
		protected Panel pnlMain = null;
		private Panel pnlMaximize = null;
		private Panel pnlMinimize = null;
		private Panel pnlTitle = null;
		protected ToolStripStatusLabel statMessage = null;
		protected StatusStrip statusThemedForm = null;

		//*-----------------------------------------------------------------------*
		//* InitializeComponent																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Initialize the form.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnlTitle = new System.Windows.Forms.Panel();
			this.pnlClose = new System.Windows.Forms.Panel();
			this.pnlMaximize = new System.Windows.Forms.Panel();
			this.pnlMinimize = new System.Windows.Forms.Panel();
			this.lblTitle = new System.Windows.Forms.Label();
			this.pnlIcon = new System.Windows.Forms.Panel();
			this.pnlForm = new System.Windows.Forms.Panel();
			this.statusThemedForm = new System.Windows.Forms.StatusStrip();
			this.statMessage = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuThemedForm = new System.Windows.Forms.MenuStrip();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.pnlTitle.SuspendLayout();
			this.pnlForm.SuspendLayout();
			this.statusThemedForm.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlTitle
			// 
			this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.pnlTitle.Controls.Add(this.pnlClose);
			this.pnlTitle.Controls.Add(this.pnlMaximize);
			this.pnlTitle.Controls.Add(this.pnlMinimize);
			this.pnlTitle.Controls.Add(this.lblTitle);
			this.pnlTitle.Controls.Add(this.pnlIcon);
			this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTitle.Location = new System.Drawing.Point(0, 0);
			this.pnlTitle.Margin = new System.Windows.Forms.Padding(0);
			this.pnlTitle.Name = "pnlTitle";
			this.pnlTitle.Size = new System.Drawing.Size(800, 48);
			this.pnlTitle.TabIndex = 0;
			// 
			// pnlClose
			// 
			this.pnlClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlClose.BackgroundImage = global::Scaffold.ResourceMain.WinControlX0;
			this.pnlClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pnlClose.Location = new System.Drawing.Point(752, 0);
			this.pnlClose.Margin = new System.Windows.Forms.Padding(0);
			this.pnlClose.Name = "pnlClose";
			this.pnlClose.Size = new System.Drawing.Size(48, 48);
			this.pnlClose.TabIndex = 4;
			// 
			// pnlMaximize
			// 
			this.pnlMaximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlMaximize.BackgroundImage = global::Scaffold.ResourceMain.WinControlM0;
			this.pnlMaximize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pnlMaximize.Location = new System.Drawing.Point(704, 0);
			this.pnlMaximize.Margin = new System.Windows.Forms.Padding(0);
			this.pnlMaximize.Name = "pnlMaximize";
			this.pnlMaximize.Size = new System.Drawing.Size(48, 48);
			this.pnlMaximize.TabIndex = 3;
			// 
			// pnlMinimize
			// 
			this.pnlMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlMinimize.BackgroundImage = global::Scaffold.ResourceMain.WinControlH0;
			this.pnlMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pnlMinimize.Location = new System.Drawing.Point(656, 0);
			this.pnlMinimize.Margin = new System.Windows.Forms.Padding(0);
			this.pnlMinimize.Name = "pnlMinimize";
			this.pnlMinimize.Size = new System.Drawing.Size(48, 48);
			this.pnlMinimize.TabIndex = 2;
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.ForeColor = System.Drawing.Color.DarkGray;
			this.lblTitle.Location = new System.Drawing.Point(57, 14);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(85, 20);
			this.lblTitle.TabIndex = 1;
			this.lblTitle.Text = "Form Title";
			// 
			// pnlIcon
			// 
			this.pnlIcon.BackgroundImage = global::Scaffold.ResourceMain.ScaffoldIcon24;
			this.pnlIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pnlIcon.Location = new System.Drawing.Point(12, 12);
			this.pnlIcon.Margin = new System.Windows.Forms.Padding(0);
			this.pnlIcon.Name = "pnlIcon";
			this.pnlIcon.Size = new System.Drawing.Size(24, 24);
			this.pnlIcon.TabIndex = 0;
			// 
			// pnlForm
			// 
			this.pnlForm.Controls.Add(this.pnlMain);
			this.pnlForm.Controls.Add(this.statusThemedForm);
			this.pnlForm.Controls.Add(this.menuThemedForm);
			this.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlForm.Location = new System.Drawing.Point(0, 48);
			this.pnlForm.Name = "pnlForm";
			this.pnlForm.Size = new System.Drawing.Size(800, 432);
			this.pnlForm.TabIndex = 1;
			// 
			// statusThemedForm
			// 
			this.statusThemedForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.statusThemedForm.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusThemedForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statMessage});
			this.statusThemedForm.Location = new System.Drawing.Point(0, 406);
			this.statusThemedForm.Name = "statusThemedForm";
			this.statusThemedForm.Size = new System.Drawing.Size(800, 26);
			this.statusThemedForm.TabIndex = 1;
			this.statusThemedForm.Text = "statusThemedForm";
			// 
			// statMessage
			// 
			this.statMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
			this.statMessage.Name = "statMessage";
			this.statMessage.Size = new System.Drawing.Size(59, 20);
			this.statMessage.Text = "Ready...";
			// 
			// menuThemedForm
			// 
			this.menuThemedForm.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuThemedForm.Location = new System.Drawing.Point(0, 0);
			this.menuThemedForm.Name = "menuThemedForm";
			this.menuThemedForm.Size = new System.Drawing.Size(800, 30);
			this.menuThemedForm.TabIndex = 0;
			this.menuThemedForm.Text = "menuThemedForm";
			// 
			// pnlMain
			// 
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(0, 30);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(800, 376);
			this.pnlMain.TabIndex = 1;
			// 
			// ThemedForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.ClientSize = new System.Drawing.Size(800, 480);
			this.Controls.Add(this.pnlForm);
			this.Controls.Add(this.pnlTitle);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MainMenuStrip = this.menuThemedForm;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "ThemedForm";
			this.Text = "ThemedFormBase";
			this.pnlTitle.ResumeLayout(false);
			this.pnlTitle.PerformLayout();
			this.pnlForm.ResumeLayout(false);
			this.pnlForm.PerformLayout();
			this.statusThemedForm.ResumeLayout(false);
			this.statusThemedForm.PerformLayout();
			this.ResumeLayout(false);

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlMain_Paint																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The main panel is being painted.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Paint event arguments.
		/// </param>
		private void pnlMain_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			Pen pen = new Pen(FromHex("#505050"), 2f);

			g.DrawRectangle(pen, new Rectangle(0, 0, pnlMain.Width, pnlMain.Height));
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
			//FlipbookItemControl item = null;

			base.OnActivated(e);
			lblTitle.ForeColor =
				Scaffold.ScaffoldNodesUtil.FromHex(ResourceMain.colorTitleTextActive);
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
			lblTitle.ForeColor =
				Scaffold.ScaffoldNodesUtil.FromHex(
					ResourceMain.colorTitleTextInactive);
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
			base.OnLeave(e);
			mWindowActive = false;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnPanelCloseMouseClick																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has clicked the window close panel.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		protected virtual void OnPanelCloseMouseClick(object sender,
			MouseEventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Hide();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnPanelCloseMouseEnter																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has entered the area of the window close panel.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected virtual void OnPanelCloseMouseEnter(object sender, EventArgs e)
		{
			pnlClose.BackColor =
				Scaffold.ScaffoldNodesUtil.FromHex(ResourceMain.colorWinControlClose);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnPanelCloseMouseLeave																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has left the area of the window close panel.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected virtual void OnPanelCloseMouseLeave(object sender, EventArgs e)
		{
			pnlClose.BackColor =
				Scaffold.ScaffoldNodesUtil.FromHex(ResourceMain.colorWinControlNormal);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnPanelMaximizeMouseClick																							*
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
		protected virtual void OnPanelMaximizeMouseClick(object sender,
			MouseEventArgs e)
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
		//* OnPanelMaximizeMouseEnter																							*
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
		protected virtual void OnPanelMaximizeMouseEnter(object sender,
			EventArgs e)
		{
			pnlMaximize.BackColor =
				Scaffold.ScaffoldNodesUtil.FromHex(ResourceMain.colorWinControlHover);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnPanelMaximizeMouseLeave																							*
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
		protected virtual void OnPanelMaximizeMouseLeave(object sender,
			EventArgs e)
		{
			pnlMaximize.BackColor =
				Scaffold.ScaffoldNodesUtil.FromHex(ResourceMain.colorWinControlNormal);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnPanelMinimizeMouseClick																							*
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
		protected virtual void OnPanelMinimizeMouseClick(object sender,
			MouseEventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnPanelMinimizeMouseEnter																							*
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
		protected virtual void OnPanelMinimizeMouseEnter(object sender,
			EventArgs e)
		{
			pnlMinimize.BackColor =
				Scaffold.ScaffoldNodesUtil.FromHex(ResourceMain.colorWinControlHover);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnPanelMinimizeMouseLeave																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has left the area of the minimize panel.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected virtual void OnPanelMinimizeMouseLeave(object sender,
			EventArgs e)
		{
			pnlMinimize.BackColor =
				Scaffold.ScaffoldNodesUtil.FromHex(ResourceMain.colorWinControlNormal);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnPanelTitleMouseDown																									*
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
		protected virtual void OnPanelTitleMouseDown(object sender,
			MouseEventArgs e)
		{
			mMouseDown = true;
			mMouseLocation = e.Location;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnPanelTitleMouseMove																									*
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
		protected virtual void OnPanelTitleMouseMove(object sender,
			MouseEventArgs e)
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
		//* OnPanelTitleMouseUp																										*
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
		protected virtual void OnPanelTitleMouseUp(object sender,
			MouseEventArgs e)
		{
			mMouseDown = false;
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
			pnlClose.MouseClick += OnPanelCloseMouseClick;
			pnlClose.MouseEnter += OnPanelCloseMouseEnter;
			pnlClose.MouseLeave += OnPanelCloseMouseLeave;
			pnlMain.Paint += pnlMain_Paint;
			pnlMaximize.MouseClick += OnPanelMaximizeMouseClick;
			pnlMaximize.MouseEnter += OnPanelMaximizeMouseEnter;
			pnlMaximize.MouseLeave += OnPanelMaximizeMouseLeave;
			pnlMinimize.MouseClick += OnPanelMinimizeMouseClick;
			pnlMinimize.MouseEnter += OnPanelMinimizeMouseEnter;
			pnlMinimize.MouseLeave += OnPanelMinimizeMouseLeave;
			pnlTitle.MouseDown += OnPanelTitleMouseDown;
			pnlTitle.MouseMove += OnPanelTitleMouseMove;
			pnlTitle.MouseUp += OnPanelTitleMouseUp;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MainPanel																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get a reference to the main panel on the base form.
		/// </summary>
		public Panel MainPanel
		{
			get { return pnlMain; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Title																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the title text for this form.
		/// </summary>
		public string Title
		{
			get { return lblTitle.Text; }
			set { lblTitle.Text = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
