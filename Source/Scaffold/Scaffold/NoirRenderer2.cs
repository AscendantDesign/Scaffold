//	NoirRenderer2.cs
//	This file Copyright(c) 2020. Ozgur Ozcitak.
//	This file is part of the AscendantDesign/Scaffold project, and
//	is distributed under the Apache License 2.0 License.
//	The greater Scaffold project is distributed under the MIT License.
//	Please see the LICENSE file in this project.
using Manina.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
	public class NoirRenderer2 : ImageListView.ImageListViewRenderer
	{
		// Returns item size for the given view mode.
		public override Size MeasureItem(View view)
		{
			if(view == View.Thumbnails)
			{
				Size itemPadding = new Size(4, 4);
				Size sz = ImageListView.ThumbnailSize +
									new Size(
										ImageListView.Margin.Left + ImageListView.Margin.Right,
										ImageListView.Margin.Top + ImageListView.Margin.Bottom) +
									itemPadding + itemPadding;
				return sz;
			}
			else
				return base.MeasureItem(view);
		}
		// Draws the background of the control.
		public override void DrawBackground(Graphics g, Rectangle bounds)
		{
			if(ImageListView.View == View.Thumbnails)
				g.Clear(Color.FromArgb(32, 32, 32));
			else
				base.DrawBackground(g, bounds);
		}
		// Draws the specified item on the given graphics.
		public override void DrawItem(Graphics g, ImageListViewItem item,
				ItemState state, Rectangle bounds)
		{
			if(ImageListView.View == View.Thumbnails)
			{
				// Black background
				using(Brush b = new SolidBrush(Color.Black))
				{
					Utility.FillRoundedRectangle(g, b, bounds, 4);
				}
				// Background of selected items
				if((state & ItemState.Selected) == ItemState.Selected)
				{
					using(Brush b = new SolidBrush(Color.FromArgb(128,
															 SystemColors.Highlight)))
					{
						Utility.FillRoundedRectangle(g, b, bounds, 4);
					}
				}
				// Gradient background
				using(Brush b = new LinearGradientBrush(
						bounds,
						Color.Transparent,
						Color.FromArgb(96, SystemColors.Highlight),
						LinearGradientMode.Vertical))
				{
					Utility.FillRoundedRectangle(g, b, bounds, 4);
				}
				// Light overlay for hovered items
				if((state & ItemState.Hovered) == ItemState.Hovered)
				{
					using(Brush b =
								 new SolidBrush(Color.FromArgb(32, SystemColors.Highlight)))
					{
						Utility.FillRoundedRectangle(g, b, bounds, 4);
					}
				}
				// Border
				using(Pen p = new Pen(Color.FromArgb(128, SystemColors.Highlight)))
				{
					Utility.DrawRoundedRectangle(g, p, bounds.X, bounds.Y, bounds.Width - 1,
																 bounds.Height - 1, 4);
				}
				// Image
				Image img = item.ThumbnailImage;
				if(img != null)
				{
					int x = bounds.Left + (bounds.Width - img.Width) / 2;
					int y = bounds.Top + (bounds.Height - img.Height) / 2;
					g.DrawImageUnscaled(item.ThumbnailImage, x, y);
					// Image border
					using(Pen p = new Pen(Color.FromArgb(128, SystemColors.Highlight)))
					{
						g.DrawRectangle(p, x, y, img.Width - 1, img.Height - 1);
					}
				}
			}
			else
				base.DrawItem(g, item, state, bounds);
		}
		// Draws the selection rectangle.
		public override void DrawSelectionRectangle(Graphics g, Rectangle selection)
		{
			using(Brush b = new HatchBrush(
					HatchStyle.DarkDownwardDiagonal,
					Color.FromArgb(128, Color.Black),
					Color.FromArgb(128, SystemColors.Highlight)))
			{
				g.FillRectangle(b, selection);
			}
			using(Pen p = new Pen(SystemColors.Highlight))
			{
				g.DrawRectangle(p, selection.X, selection.Y,
						selection.Width, selection.Height);
			}
		}
	}
}
