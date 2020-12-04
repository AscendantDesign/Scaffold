using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Scaffold.ScaffoldNodesUtil;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	FrameFlipbookItemControl																								*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Single element in the visible flipbook entry list.
	/// </summary>
	public partial class FlipbookItemControl : UserControl
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* any_Click																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Any object on the item, including the control itself, has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void any_Click(object sender, EventArgs e)
		{
			OnItemClick(e);
			//this.Invalidate();
			//this.Refresh();
			////if(this.Parent != null)
			////{
			////	this.Parent.Invalidate();
			////	this.Parent.Refresh();
			////}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* any_DoubleClick																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Any object on the item, including the control itself, has been
		/// double-clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void any_DoubleClick(object sender, EventArgs e)
		{
			OnItemDoubleClick(e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* picThumb_Resize																												*
		//*-----------------------------------------------------------------------*
		private bool mpicThumbResizeBusy = false;
		/// <summary>
		/// The thumb picturebox has been resized.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void picThumb_Resize(object sender, EventArgs e)
		{
			float ratio = 1.7777f;

			if(!mpicThumbResizeBusy)
			{
				mpicThumbResizeBusy = true;
				if((float)picThumb.Width > ratio)
				{
					picThumb.Size = new Size(picThumb.Width,
						(int)((float)picThumb.Width / ratio));
				}
				mpicThumbResizeBusy = false;
			}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnItemClick																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the ItemClick event when any item on the control, including the
		/// control itself, has been clicked.
		/// </summary>
		/// <param name="e">
		/// The object raising this event.
		/// </param>
		protected virtual void OnItemClick(EventArgs e)
		{
			ItemClick?.Invoke(this, e);
			//if(mFrame != null)
			//{
			//	FlipbookFrameItem.ItemClick(mFrame,
			//		(Control.ModifierKeys & Keys.Control) == Keys.Control,
			//		(Control.ModifierKeys & Keys.Shift) == Keys.Shift);
			//}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnItemDoubleClick																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the ItemDoubleClick event when any item on the control,
		/// including the control itself, has been double-clicked.
		/// </summary>
		/// <param name="e">
		/// The object raising this event.
		/// </param>
		protected virtual void OnItemDoubleClick(EventArgs e)
		{
			ItemDoubleClick?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnPaintBackground																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the PaintBackground event when the background area needs to be
		/// painted.
		/// </summary>
		/// <param name="e">
		/// Paint event arguments.
		/// </param>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			Pen pen = null;

			base.OnPaintBackground(e);
			if(mFrame != null && mFrame.Selected)
			{
				pen = new Pen(FromHex("#f9bb00"), 2f);
				e.Graphics.DrawRectangle(pen,
					new Rectangle(0, 0, this.Width, this.Height));
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
		/// Create a new instance of the FrameFlipbookItemControl Item.
		/// </summary>
		public FlipbookItemControl()
		{
			InitializeComponent();

			lblIndex.Click += any_Click;
			lblIndex.DoubleClick += any_DoubleClick;
			lblName.Click += any_Click;
			lblName.DoubleClick += any_DoubleClick;
			lblTimer.Click += any_Click;
			lblTimer.DoubleClick += any_DoubleClick;
			lblTimerMeasure.Click += any_Click;
			lblTimerMeasure.DoubleClick += any_DoubleClick;
			picIcon.Click += any_Click;
			picIcon.DoubleClick += any_DoubleClick;
			picThumb.Click += any_Click;
			picThumb.DoubleClick += any_DoubleClick;
			picThumb.Resize += picThumb_Resize;

			this.Click += any_Click;
			this.DoubleClick += any_DoubleClick;
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	Filename																															*
		////*-----------------------------------------------------------------------*
		//private string mFilename = "";
		///// <summary>
		///// Get/Set the filename to be handled by this item.
		///// </summary>
		//public string Filename
		//{
		//	get { return mFilename; }
		//	set { mFilename = value; }
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Frame																																	*
		//*-----------------------------------------------------------------------*
		private FlipbookFrameItem mFrame = null;
		/// <summary>
		/// Get/Set a reference to the active frame for this item.
		/// </summary>
		public FlipbookFrameItem Frame
		{
			get { return mFrame; }
			set
			{
				Bitmap bitmap = null;
				string filename = "";

				mFrame = value;
				lblIndex.Text = "";
				lblName.Text = "";
				lblTimer.Text = "";
				picIcon.Image = ResourceMain.Filmstrip32;
				if(value != null)
				{
					lblIndex.Text = value.Index;
					lblName.Text = value.Name;
					lblTimer.Text = value.Timer.ToString();
					switch(value.Action)
					{
						case FlipbookActionTypeEnum.DeleteSpace:
							picIcon.Image = ResourceMain.FilmstripTrash32;
							break;
						case FlipbookActionTypeEnum.Keep:
							picIcon.Image = ResourceMain.FilmstripArrow32;
							break;
						default:
							picIcon.Image = ResourceMain.Filmstrip32;
							break;
					}
					if(value.Index?.Length > 0 &&
						value.Parent != null && value.Parent.Parent != null)
					{
						//	Picture implied. Get thumbnail.
						//	Build the filename.
						filename = Path.Combine(value.Parent.Parent.Folder,
							value.Index);
						try
						{
							using(FileStream bitmapStream = File.OpenRead(filename))
							{
								bitmap = (Bitmap)Bitmap.FromStream(bitmapStream);
							}
						}
						catch(Exception ex)
						{
							Debug.WriteLine(
								$"Error reading thumbnail image: {ex.Message}");
						}
						if(bitmap != null)
						{
							picThumb.Image = CreateThumbnail(bitmap, picThumb.Size, false);
							//picThumb.Image = bitmap;
						}
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ItemClick																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when any control on the item or the item itself have been
		/// clicked.
		/// </summary>
		public event EventHandler ItemClick;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ItemDoubleClick																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when any control on the item or the item itself have been
		/// double-clicked.
		/// </summary>
		public event EventHandler ItemDoubleClick;
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
