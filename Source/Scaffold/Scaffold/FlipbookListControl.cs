using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	FlipbookListControl																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// List control containing flipbook keyframe items.
	/// </summary>
	public class FlipbookListControl : FlowLayoutPanel
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* frame_SelectedChanged																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Selected property has changed on an attached frame.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void frame_SelectedChanged(object sender, EventArgs e)
		{
			FlipbookFrameItem frame = null;
			FlipbookItemControl item = null;

			if(sender != null)
			{
				frame = (FlipbookFrameItem)sender;
				foreach(FlipbookItemControl listItem in this.Controls)
				{
					if(listItem.Frame == frame)
					{
						item = listItem;
						break;
					}
				}
				if(item != null)
				{
					item.Invalidate();
					item.Refresh();
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* listItem_ItemClick																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A list item has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void listItem_ItemClick(object sender, EventArgs e)
		{
			FlipbookFrameItem frame = null;
			FlipbookItemControl item = null;
			//	Select the item according to modifier key states.
			if(sender != null)
			{
				item = (FlipbookItemControl)sender;
				frame = item.Frame;
				FlipbookFrameItem.ItemClick(frame,
					(Control.ModifierKeys & Keys.Control) == Keys.Control,
					(Control.ModifierKeys & Keys.Shift) == Keys.Shift);
			}
			OnItemClick(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* listItem_ItemDoubleClick																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// An item in the list has received a double-click.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void listItem_ItemDoubleClick(object sender, EventArgs e)
		{
			//	Open the item editor dialog.
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnItemClick																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the ItemClick event when an item has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected virtual void OnItemClick(object sender, EventArgs e)
		{
			ItemClick?.Invoke(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	Clear																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Clear the items from the list.
		/// </summary>
		public void Clear()
		{
			while(this.Controls.Count > 0)
			{
				this.Controls.RemoveAt(0);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	FirstSelectedItem																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get a reference to the first selected item in the list.
		/// </summary>
		public FlipbookItemControl FirstSelectedItem
		{
			get
			{
				FlipbookItemControl result = null;

				foreach(FlipbookItemControl item in this.Controls)
				{
					if(item.Frame.Selected)
					{
						result = item;
						break;
					}
				}
				return result;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Frames																																*
		//*-----------------------------------------------------------------------*
		private FlipbookFrameCollection mFrames = null;
		/// <summary>
		/// Get/Set a reference to the collection of frames to be associated with
		/// this control.
		/// </summary>
		public FlipbookFrameCollection Frames
		{
			get { return mFrames; }
			set
			{
				FlipbookItemControl listItem = null;

				mFrames = value;
				this.Clear();
				if(mFrames != null)
				{
					this.FlowDirection = FlowDirection.TopDown;
					foreach(FlipbookFrameItem frame in mFrames)
					{
						listItem = new FlipbookItemControl();
						listItem.Frame = frame;
						listItem.Frame.SelectedChanged += frame_SelectedChanged;
						listItem.ItemClick += listItem_ItemClick;
						listItem.ItemDoubleClick += listItem_ItemDoubleClick;
						this.Controls.Add(listItem);
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ItemClick																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when an item in the list has been clicked.
		/// </summary>
		public event EventHandler ItemClick;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ScrollToItem																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Scroll the item associated with the specified frame into view.
		/// </summary>
		/// <param name="frame">
		/// Reference to the frame to be made visible.
		/// </param>
		public void ScrollToItem(FlipbookFrameItem frame)
		{
			if(frame != null)
			{
				foreach(FlipbookItemControl listItem in this.Controls)
				{
					if(listItem.Frame == frame)
					{
						ScrollToItem(listItem);
						break;
					}
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Scroll the specified item into view.
		/// </summary>
		/// <param name="item">
		/// Reference to the item to be visible.
		/// </param>
		public void ScrollToItem(FlipbookItemControl item)
		{
			Point location = Point.Empty;

			if(item != null)
			{
				location = item.Location - new Size(this.AutoScrollPosition);
				location -= new Size(item.Margin.Left, item.Margin.Top);
				this.AutoScrollPosition = location;
				//item.Focus();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SelectFirst																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Select the first item in the list.
		/// </summary>
		public void SelectFirst()
		{
			FlipbookItemControl item = null;

			if(this.Controls.Count > 0)
			{
				item = (FlipbookItemControl)this.Controls[0];
				foreach(FlipbookItemControl listItem in this.Controls)
				{
					if(listItem.Frame.Selected)
					{
						listItem.Frame.Selected = false;
					}
				}
				item.Frame.Selected = true;
				ScrollToItem(item);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SelectItem																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Select an item in the list by its filename.
		/// </summary>
		/// <param name="filename">
		/// Name of the file to select.
		/// </param>
		public void SelectItem(string filename)
		{
			bool bFound = false;
			foreach(FlipbookItemControl item in this.Controls)
			{
				if(item.Frame.Index == filename)
				{
					item.Frame.Selected = true;
					//ScrollToControl(item);
					ScrollToItem(item);
					bFound = true;
					break;
				}
			}
			if(bFound)
			{
				foreach(FlipbookItemControl item in this.Controls)
				{
					if(item.Frame.Index != filename && item.Frame.Selected)
					{
						//	Deselect any other item when keyframe has been found.
						item.Frame.Selected = false;
					}
				}
				//ScrollToItem(item);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SelectNext																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Select the next item in the list.
		/// </summary>
		public void SelectNext()
		{
			bool bFound = false;
			int count = 0;
			int index = 0;
			FlipbookFrameItem frame = null;

			if(mFrames != null)
			{
				count = mFrames.Count;
				for(index = 0; index < count; index++)
				{
					frame = mFrames[index];
					if(frame.Selected)
					{
						//	A selected frame was found.
						bFound = true;
						frame.Selected = false;
						if(index < count - 1)
						{
							frame = mFrames[index + 1];
							frame.Selected = true;
							ScrollToItem(frame);
						}
						break;
					}
				}
				if(!bFound && count > 0)
				{
					//	No selected frame found. Select the first item.
					mFrames[0].Selected = true;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SelectPrevious																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Select the previous item in the list.
		/// </summary>
		public void SelectPrevious()
		{
			bool bFound = false;
			int count = 0;
			int index = 0;
			FlipbookFrameItem frame = null;

			if(mFrames != null)
			{
				count = mFrames.Count;
				for(index = count - 1; index > -1; index--)
				{
					frame = mFrames[index];
					if(frame.Selected)
					{
						//	A selected frame was found.
						bFound = true;
						frame.Selected = false;
						if(index > 0)
						{
							frame = mFrames[index - 1];
							frame.Selected = true;
							ScrollToItem(frame);
						}
						break;
					}
				}
				if(!bFound && count > 0)
				{
					//	No selected frame found. Select the last item.
					mFrames[count - 1].Selected = true;
				}
			}
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
