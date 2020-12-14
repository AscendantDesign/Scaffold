//	NodeControl.cs
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
using System.Drawing.Drawing2D;

using static Scaffold.ScaffoldUtil;
using static Scaffold.ScaffoldNodesUtil;

using System.IO;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Web.UI.WebControls;
using OpenTK.Graphics.OpenGL;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	NodeControl																															*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Node editor user control.
	/// </summary>
	[ToolboxBitmap(typeof(NodeControl), "nodeeditor")]
	public partial class NodeControl : UserControl
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		private NodeItem mClickedNodeLast = null;
		private PointF mDragConnectionEnd = PointF.Empty;
		private PointF mDragConnectionStart = PointF.Empty;
		private SocketItem mDragSocket = null;
		private SocketItem mDragTarget = null;
		//private NodeItem mDragSocketNode = null;
		private Point mMouseCanvasLocation = Point.Empty;
		private bool mMouseDown = false;
		private Point mMousePositionLast = Point.Empty;
		private List<NodeEventArgs> mNodesMoving = new List<NodeEventArgs>();
		private PointF mSelectionEnd = PointF.Empty;
		private PointF mSelectionStart = PointF.Empty;
		private Timer mTimer = new Timer();
		private int mScrollHorizontal = 0;
		private int mScrollVertical = 0;

		//*-----------------------------------------------------------------------*
		//* BindNodes																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Bind events to the nodes collection.
		/// </summary>
		/// <param name="nodes">
		/// Reference to a nodes collection with events to be bound.
		/// </param>
		private void BindNodes(NodeCollection nodes)
		{
			nodes.NodeAdded += OnNodeAdded;
			nodes.NodeDeleted += OnNodeDeleted;
			nodes.NodePropertyChanged += OnNodePropertyChanged;
			nodes.SocketAdded += OnSocketAdded;
			nodes.SocketConnectionAdded += OnSocketConnectionAdded;
			nodes.SocketConnectionDeleted += OnSocketConnectionDeleted;
			nodes.SocketDeleted += OnSocketDeleted;
			nodes.SocketPropertyChanged += OnSocketPropertyChanged;
			nodes.SelectionChanged += OnSelectionChanged;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* BindResources																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Bind events to the resources collection.
		/// </summary>
		/// <param name="resources">
		/// Reference to a collection of resources with events to be bound.
		/// </param>
		private void BindResources(ResourceCollection resources)
		{

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DeleteSelectedNodes																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Delete all selected nodes.
		/// </summary>
		private void DeleteSelectedNodes()
		{
			//NodeEventArgs eaNode = null;
			//List<NodeItem> nodes = null;

			if(mNodes.Exists(x => x.Selected))
			{
				//	There are selected nodes.
				////	Fire the NodeDeleting event for each one.
				//nodes = mNodes.FindAll(x => x.Selected);
				//foreach(NodeItem node in nodes)
				//{
				//	eaNode = new NodeEventArgs(node);
				//	OnNodeDeleting(eaNode);
				//}
				//	Delete the selected nodes.
				mNodes.RemoveAll(x => x.Selected);
			}
			pnlView.Invalidate();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DrawConnection																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Draw the bezier connection, originating at from: and ending at to:.
		/// </summary>
		/// <param name="g">
		/// Reference to an active graphics object.
		/// </param>
		/// <param name="pen">
		/// Reference to the active pen.
		/// </param>
		/// <param name="from">
		/// Starting coordinate of the connection line.
		/// </param>
		/// <param name="to">
		/// Ending coordinate of the connection line.
		/// </param>
		private static void DrawConnection(Graphics g, Pen pen,
			PointF from, PointF to)
		{
			PointF a = PointF.Empty;
			float amount = 0f;
			PointF b = PointF.Empty;
			float d = 0f;
			int i = 0;
			const int interpolation = 48;
			float lx = 0f;
			PointF[] points = null;

			if(g != null && pen != null &&
				from != null && to != null && from != to)
			{
				//g.InterpolationMode = InterpolationMode.HighQualityBilinear;
				//g.SmoothingMode = SmoothingMode.HighQuality;

				points = new PointF[interpolation];
				for(i = 0; i < interpolation; i++)
				{
					amount = i / (float)(interpolation - 1);

					lx = LinearInterpolate(from.X, to.X, amount);
					d = Math.Min(Math.Abs(from.X - to.X), 100);
					a = new PointF(
						(float)ScaleValues(amount, 0d, 1d, from.X, from.X + d), from.Y);
					b = new PointF(
						(float)ScaleValues(amount, 0d, 1d, to.X - d, to.X), to.Y);

					var bas = Saturate(ScaleValues(amount, 0.1, 0.9, 0d, 1d));
					var cos = Math.Cos(bas * Math.PI);
					if(cos < 0)
					{
						cos = -Math.Pow(-cos, 0.2);
					}
					else
					{
						cos = Math.Pow(cos, 0.2);
					}
					amount = (float)cos * -0.5f + 0.5f;

					var f = LinearInterpolate(a, b, amount);
					points[i] = f;
				}
				g.DrawLines(pen, points);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DrawNode																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Draw the visual node on the caller's graphics context.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to be drawn.
		/// </param>
		/// <param name="g">
		/// Active graphics context.
		/// </param>
		/// <param name="mouseLocation">
		/// Location of the mouse relative to the parent container.
		/// </param>
		/// <param name="mouseButtons">
		/// Buttons currently pressed.
		/// </param>
		private void DrawNode(NodeItem node, Graphics g, Point mouseLocation,
			MouseButtons mouseButtons)
		{
			Bitmap bitmap = null;
			Brush brush = null;
			RectangleF caption = RectangleF.Empty;
			RectangleF feedrect = RectangleF.Empty;
			Font font = null;
			RectangleF image = RectangleF.Empty;
			GraphicsPath path = null;
			Pen pen = null;
			PropertyItem property = null;
			RectangleF rect = RectangleF.Empty;
			SizeF size = SizeF.Empty;
			bool toolTipActive = false;

			Verbose("    Draw node...", 2);
			if(node != null && g != null)
			{
				//	Node bounding box.
				Verbose("    Node bounding box...", 2);
				rect = new RectangleF(node.X, node.Y, node.Width, node.Height);
				feedrect = rect;
				feedrect.Inflate(10, 10);

				caption = new RectangleF(NodeItem.GetLocation(node),
					NodeItem.GetTitleSize(node));
				toolTipActive = caption.Contains(mouseLocation);

				//	Node shape.
				Verbose("    Node shape...", 2);
				path = RoundedRectangle(Rectangle.Round(rect), 6);

				//	Node background.
				Verbose("    Node background...", 2);
				if(node.NodeColor == Color.Empty)
				{
					brush = new SolidBrush(mNodeBackColor);
				}
				else
				{
					brush = new SolidBrush(node.NodeColor);
				}
				g.FillPath(brush, path);

				//	Node outline.
				Verbose("    Node outline...", 2);
				if(node.Selected)
				{
					pen = new Pen(mNodeSelectColor);
				}
				else
				{
					pen = new Pen(mNodeLineColor);
				}
				pen.Width = mNodeLineWidth;
				g.DrawPath(pen, path);

				//	Name.
				Verbose("    Node name...", 2);
				font = new Font(this.Font.FontFamily, mTitleFontSize);
				if(node.NodeTextColor == Color.Empty)
				{
					brush = new SolidBrush(mTitleTextColor);
				}
				else
				{
					brush = new SolidBrush(node.NodeTextColor);
				}
				g.DrawString(node[node.TitleProperty].Value.ToString(),
					font, brush,
					new RectangleF(node.X + 3f, node.Y + 3f,
					node.Width - 6f, node.TitleHeight));

				//	Thumbnails and Icons.
				if(MediaExists(node))
				{
					//	At least one media resource is defined.
					//	In this version, the thumbnail image will have been pre-scaled.
					if(node.Properties.Exists(p => p.Name == "ThumbImage"))
					{
						bitmap = (Bitmap)node.Properties.FirstOrDefault(p =>
							p.Name == "ThumbImage").Value;
						size = GetScaledSize(bitmap.Size, width: node.Width - 8f);
						//image = new RectangleF(
						//	(node.X + (node.Width / 2f) - ((float)bitmap.Width / 2f)),
						//	caption.Bottom + 2f,
						//	bitmap.Width, bitmap.Height);
						image = new RectangleF(
							node.X + 4f, caption.Bottom + 2f,
							size.Width, size.Height);
						g.DrawImage(bitmap, image);
					}
					else if(node.Properties.Exists(p => p.Name == "ThumbVideo"))
					{
						bitmap = (Bitmap)node.Properties.FirstOrDefault(p =>
							p.Name == "ThumbVideo").Value;
						size = GetScaledSize(bitmap.Size, width: node.Width - 8f);
						//image = new RectangleF(
						//	node.X + (node.Width / 2f) - ((float)bitmap.Width / 2f),
						//	caption.Bottom + 2f,
						//	bitmap.Width, bitmap.Height);
						image = new RectangleF(
							node.X + 4f, caption.Bottom + 2f,
							size.Width, size.Height);
						g.DrawImage(bitmap, image);
					}
					if(node.Properties.Exists(p => p.Name == "IconAudio"))
					{
						bitmap = (Bitmap)node.Properties.FirstOrDefault(p =>
							p.Name == "IconAudio").Value;
						image = new RectangleF(node.X + 6f, caption.Bottom + 6f,
							32f, 32f);
						g.DrawImage(bitmap, image);
					}
					if(node.Properties.Exists(p => p.Name == "IconLink"))
					{
						property = node.Properties.FirstOrDefault(p =>
							p.Name == "IconLink");
						if(property != null)
						{
							bitmap = (Bitmap)node.Properties.FirstOrDefault(p =>
								p.Name == "IconLink").Value;
							image = new RectangleF(node.X + 6f, caption.Bottom + 42f,
								32f, 32f);
							g.DrawImage(bitmap, image);
						}
					}
				}

				//	Sheet number.
				Verbose("    Sheet number...", 2);
				font = new Font(this.Font.FontFamily, mTitleFontSize + 2f);
				brush = new SolidBrush(FromHex("#F977E6"));
				g.DrawString(ToInt(node["StoryPageNumber"].StringValue()).ToString(),
					font, brush, node.X + node.Width + 16f, node.Y - 16f);

				//	Sockets.
				Verbose("    Draw node sockets...", 2);
				DrawSockets(node, g, mouseLocation, mouseButtons);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DrawNodes																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Draw all of the nodes in the tree.
		/// </summary>
		/// <param name="g">
		/// Reference to the active graphics context.
		/// </param>
		/// <param name="mouseLocation">
		/// Last known mouse location.
		/// </param>
		/// <param name="mouseButtons">
		/// Last known mouse button state.
		/// </param>
		private void DrawNodes(Graphics g, Point mouseLocation,
			MouseButtons mouseButtons)
		{
			Pen connectionPen = new Pen(mSocketConnectionColor);
			PointF destination = PointF.Empty;
			//float headerHeight = 0f;
			//int index = 0;
			List<NodeItem> orderedNodes = null;
			PointF origin = PointF.Empty;
			RectangleF rect = RectangleF.Empty;
			List<SocketItem> socketConnections = null;
			List<SocketItem> sockets = null;

			//	Draw all connections.
			connectionPen.Width = (float)mSocketConnectionLineWidth;
			foreach(NodeItem node in mNodes)
			{
				sockets = node.Sockets.FindAll(x =>
					x.SocketMode == SocketModeEnum.Output).ToList();
				foreach(SocketItem socketOrigin in sockets)
				{
					//	Every output to every input.
					Verbose("  Socket origin...", 3);
					origin = socketOrigin.GetLocation() +
						new SizeF(socketOrigin.Width / 2f, socketOrigin.Height / 2f);
					socketConnections = socketOrigin.Connections;
					foreach(SocketItem socketDestination in socketConnections)
					{
						Verbose("  Socket destination...", 3);
						destination = socketDestination.GetLocation() +
							new SizeF(socketDestination.Width / 2f,
							socketDestination.Height / 2f);
						Verbose("  Draw connection...", 3);
						DrawConnection(g, connectionPen, origin, destination);
					}
				}
			}

			//	Draw the actual nodes, by Z-order, back to front.
			Verbose("  Draw ordered nodes...", 3);
			orderedNodes = Nodes.OrderBy(x => x.ZOrder).ToList();
			foreach(NodeItem node in orderedNodes)
			{
				DrawNode(node, g, mouseLocation, mouseButtons);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DrawSockets																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Draw the sockets on the caller's node.
		/// </summary>
		/// <param name="node">
		/// Reference to the node for which the sockets will be drawn.
		/// </param>
		/// <param name="g">
		/// Reference to an active graphics context.
		/// </param>
		/// <param name="mouseLocation">
		/// Location of the mouse.
		/// </param>
		/// <param name="mouseButtons">
		/// Current mouse button state.
		/// </param>
		private void DrawSockets(NodeItem node, Graphics g,
			Point mouseLocation, MouseButtons mouseButtons)
		{
			Font font = null;
			Brush highlightBrush = null;
			Bitmap icon = null;
			RectangleF iconArea = RectangleF.Empty;
			SizeF iconSize = SizeF.Empty;
			int index = 0;
			Brush normalBrush = null;
			PropertyItem property = null;
			RectangleF rect = RectangleF.Empty;
			StringFormat sf = null;
			List<SocketItem> sockets = null;
			Brush textBrush = null;
			RectangleF textArea = RectangleF.Empty;
			bool toolTipActive = false;

			if(node != null && g != null)
			{
				//	Direct reference.
				if(node.NodeTextColor == Color.Empty)
				{
					textBrush = normalBrush = new SolidBrush(mSocketTextColor);
				}
				else
				{
					textBrush = normalBrush = new SolidBrush(node.NodeTextColor);
				}
				highlightBrush = Brushes.Blue;

				//	Get the inputs.
				sockets = node.Sockets.FindAll(x =>
					x.SocketMode == SocketModeEnum.Input);
				index = 0;
				font = new Font(this.Font.FontFamily, mSocketFontSize);
				foreach(SocketItem socket in sockets)
				{
					//	Each input socket.
					icon = null;
					iconArea = RectangleF.Empty;
					iconSize = SizeF.Empty;
					property = socket.Properties.FirstOrDefault(p => p.Name == "Icon");
					if(property != null)
					{
						icon = (Bitmap)property.Value;
						iconSize = icon.Size;
						if(iconSize.Width != 0f)
						{
							iconArea = new RectangleF(
								socket.TextBounds.X,
								socket.TextBounds.Y,
								iconSize.Width,
								iconSize.Height);
							textArea = new RectangleF(
								socket.TextBounds.X + iconSize.Width + 2,
								socket.TextBounds.Y,
								socket.TextBounds.Width,
								socket.TextBounds.Height);
						}
					}
					else
					{
						textArea = socket.TextBounds;
					}
					rect = socket.GetBounds();
					toolTipActive = rect.Contains(
						ScaleDrawing(mouseLocation, mDrawingScale,
						this.HorizontalScroll.Value,
						this.VerticalScroll.Value));
					if(toolTipActive)
					{
						rect.Inflate(4f, 4f);
						textBrush = highlightBrush;
					}
					else
					{
						textBrush = normalBrush;
					}

					//g.SmoothingMode = SmoothingMode.HighSpeed;
					//g.InterpolationMode = InterpolationMode.Low;

					sf = new StringFormat();
					sf.Alignment = StringAlignment.Near;
					sf.LineAlignment = StringAlignment.Center;
					//	Text bounds includes the width of any icon.
					g.DrawString(socket[socket.TitleProperty].Value.ToString(),
						font, textBrush, textArea, sf);

					if(icon != null)
					{
						g.DrawImage(icon, iconArea);
					}

					//g.InterpolationMode = InterpolationMode.HighQualityBilinear;
					//g.SmoothingMode = SmoothingMode.HighQuality;

					g.DrawImage(ResourceMain.Socket, rect);
					index++;
				}

				//	Get the outputs.
				sockets = node.Sockets.FindAll(x =>
					x.SocketMode == SocketModeEnum.Output);
				index = 0;
				foreach(SocketItem socket in sockets)
				{
					icon = null;
					iconArea = RectangleF.Empty;
					iconSize = SizeF.Empty;
					property = socket.Properties.FirstOrDefault(p => p.Name == "Icon");
					if(property != null)
					{
						icon = (Bitmap)property.Value;
						iconSize = icon.Size;
						if(iconSize.Width != 0f)
						{
							iconArea = new RectangleF(
								socket.TextBounds.X + socket.TextBounds.Width -
									(iconSize.Width + 2f),
								socket.TextBounds.Y,
								iconSize.Width,
								iconSize.Height);
							textArea = new RectangleF(
								socket.TextBounds.X,
								socket.TextBounds.Y,
								socket.TextBounds.Width - (iconSize.Width + 2),
								socket.TextBounds.Height);
						}
					}
					else
					{
						textArea = socket.TextBounds;
					}
					rect = socket.GetBounds();
					toolTipActive = rect.Contains(
						ScaleDrawing(mouseLocation, mDrawingScale,
						this.HorizontalScroll.Value,
						this.VerticalScroll.Value));
					if(toolTipActive)
					{
						rect.Inflate(4f, 4f);
						textBrush = highlightBrush;
					}
					else
					{
						textBrush = normalBrush;
					}

					//g.SmoothingMode = SmoothingMode.HighSpeed;
					//g.InterpolationMode = InterpolationMode.Low;

					sf = new StringFormat();
					sf.Alignment = StringAlignment.Far;
					sf.LineAlignment = StringAlignment.Center;
					g.DrawString(socket[socket.TitleProperty].Value.ToString(),
						font, textBrush, textArea, sf);

					if(icon != null)
					{
						g.DrawImage(icon, iconArea);
					}

					//g.InterpolationMode = InterpolationMode.HighQualityBilinear;
					//g.SmoothingMode = SmoothingMode.HighQuality;

					g.DrawImage(ResourceMain.Socket, rect);
					index++;
				}

			}
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* MeasureString																													*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return the resulting size of a string that will be limited to the
		///// specified width when drawn.
		///// </summary>
		///// <param name="g">
		///// Reference to an active graphics object.
		///// </param>
		///// <param name="text">
		///// Text to measure.
		///// </param>
		///// <param name="font">
		///// Reference to the selected font for the specified text.
		///// </param>
		///// <param name="maxWidth">
		///// Maximum allowable width of a single line.
		///// </param>
		///// <returns>
		///// Resulting size of the string to draw.
		///// </returns>
		//private SizeF MeasureString(Graphics g, string text, Font font,
		//	int maxWidth)
		//{
		//	bool bFound = false;
		//	Color color = Color.Empty;
		//	Graphics gr = null;
		//	Bitmap bitmap = null;
		//	int h = 0;
		//	SizeF result = new SizeF(0f, 0f);
		//	RectangleF textArea = RectangleF.Empty;
		//	SizeF textSize = SizeF.Empty;
		//	int w = 0;
		//	int x = 0;
		//	int y = 0;

		//	if(text.Length > 20)
		//	{
		//		Debug.WriteLine("Break here...");
		//	}
		//	if(g != null && text?.Length > 0 && maxWidth > 0f)
		//	{
		//		//	Get the measured text size.
		//		textSize = g.MeasureString(text, font, maxWidth);
		//		//	Draw the text.
		//		bitmap = new Bitmap((int)textSize.Width, (int)textSize.Height);
		//		gr = Graphics.FromImage(bitmap);
		//		textArea = new RectangleF(new PointF(0f, 0f), textSize);
		//		gr.FillRectangle(Brushes.White, textArea);
		//		gr.DrawString(text, font, Brushes.Black, textArea);
		//		//	Check for boundaries.
		//		w = (int)textArea.Width;
		//		h = (int)textArea.Height;
		//		//	Check width.
		//		bFound = false;
		//		for(x = w - 1; x > -1; x--)
		//		{
		//			//	Test right to left.
		//			for(y = 0; y < h; y++)
		//			{
		//				//	Top to bottom.
		//				color = bitmap.GetPixel(x, y);
		//				if(color.R != 255 || color.G != 255 || color.B != 255)
		//				{
		//					//	Right-edge found.
		//					result.Width = x + 1;
		//					bFound = true;
		//					break;
		//				}
		//			}
		//			if(bFound)
		//			{
		//				break;
		//			}
		//		}
		//		//	Check height.
		//		bFound = false;
		//		for(y = h - 1; y > -1; y--)
		//		{
		//			//	Text bottom to top.
		//			for(x = 0; x < w; x++)
		//			{
		//				//	Left to right.
		//				color = bitmap.GetPixel(x, y);
		//				if(color.R != 255 || color.G != 255 || color.B != 255)
		//				{
		//					//	Bottom edge found.
		//					result.Height = y + 1;
		//					bFound = true;
		//					break;
		//				}
		//			}
		//			if(bFound)
		//			{
		//				break;
		//			}
		//		}
		//	}
		//	//if(bitmap != null)
		//	//{
		//	//	bitmap.Save("C:/Temp/ScaffoldMeasureText.png");
		//	//}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* MeasureView																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Measure the current state of the logical view.
		/// </summary>
		/// <param name="g">
		/// Reference to the active graphics context.
		/// </param>
		private void MeasureView(Graphics g)
		{
			Bitmap bitmap = null;
			RectangleF bounds = new RectangleF(0f, 0f,
				(float)this.Width - (float)this.AutoScrollMargin.Width,
				(float)this.Height - (float)this.AutoScrollMargin.Height);
			Font fontSocket = null;
			Font fontTitle = null;
			float headerHeight = 0f;
			int height = 0;
			float hMax = 0;
			float hMin = 0;
			float hVal = 0;
			SizeF iconSize = SizeF.Empty;
			int index = 0;
			float lastSocketY = 0f;
			float lastTextHeight = 0f;
			float maxHeight = 0f;
			float maxWidth = 0f;
			PropertyItem property = null;
			//float ratio = 1f;
			RectangleF rect = RectangleF.Empty;
			float socketMaxWidth = 16f;
			SizeF socketTextSize = SizeF.Empty;
			float socketVSpacing = 4f;
			string text = "";
			SizeF textSize = SizeF.Empty;
			SizeF titleTextSize = SizeF.Empty;
			float vMax = 0;
			float vMin = 0;
			float vVal = 0;
			int width = 0;

			//	Calculate all sizes and locations.
			//	Measure the font a little larger than needed.
			fontTitle = new Font(this.Font.FontFamily, mTitleFontSize + 1);
			foreach(NodeItem node in mNodes)
			{
				//	Caption.
				text = node[node.TitleProperty].StringValue();
				if(mNodeMaxWidth > 0)
				{
					//	Fit title within node bounds.
					textSize = g.MeasureString(text, fontTitle, mNodeMaxWidth - 24);
					titleTextSize = textSize;
				}
				else
				{
					//	Allow title to take unlimited width.
					textSize = g.MeasureString(text, fontTitle);
					titleTextSize =
						SizeF.Add(textSize, new SizeF(24f, 0f));
				}
				Verbose(
					$"MeasureView: Measured text size {textSize} for [{text}].", 3);
				node.Width = Math.Max(node.Width,
					titleTextSize.Width);
				headerHeight = node.TitleHeight = titleTextSize.Height;

				//	Main node thumbnails and icons.
				if(MediaExists(node))
				{
					//	Thumbnails and icons will already exist here...
					//	At least one media resource is defined. Measure for the
					//	thumbnail and icons.
					node.IconHeight = 4;    //	Start with the margins.
					if(node.Properties.Exists(p => p.Name == "ThumbImage"))
					{
						//	In this version, the image will have been pre-scaled.
						//	The height of this item helps to determine the height of
						//	the area.
						bitmap = (Bitmap)node.Properties.FirstOrDefault(p =>
							p.Name == "ThumbImage").Value;
						if(bitmap.Height > 72)
						{
							node.IconHeight += bitmap.Height;
						}
						else
						{
							node.IconHeight += 72;
						}
					}
					else if(node.Properties.Exists(p => p.Name == "ThumbVideo"))
					{
						//	In this version, the image will have been pre-scaled.
						//	The height of this item helps to determine the height of
						//	the area.
						bitmap = (Bitmap)node.Properties.FirstOrDefault(p =>
							p.Name == "ThumbVideo").Value;
						if(bitmap.Height > 72)
						{
							node.IconHeight += bitmap.Height;
						}
						else
						{
							node.IconHeight += 72;
						}
					}
					//	Icons are placed over the top of the thumbnails.
					else if(node.Properties.Exists(p => p.Name == "IconLink"))
					{
						//	Link icon is at 40, 4 from thumbnail.
						node.IconHeight += 72;
					}
					else if(node.Properties.Exists(p => p.Name == "IconAudio"))
					{
						//	Audio icon is at 4, 4 from thumbnail.
						node.IconHeight += 38;
					}
				}
				else
				{
					node.IconHeight = 0;
				}

				//	Sockets.
				index = 0;
				//	In this version, sockets are placed in absolute coordinates.
				if(node.Sockets.Count > 0)
				{
					socketMaxWidth =
						Math.Max(socketMaxWidth, node.Sockets.Max(s => s.Width));
				}
				lastSocketY = node.Y;
				lastTextHeight = headerHeight + node.IconHeight;
				fontSocket = new Font(this.Font.FontFamily, mSocketFontSize);
				foreach(SocketItem socket in node.Sockets)
				{
					iconSize = SizeF.Empty;
					property = socket.Properties.FirstOrDefault(p => p.Name == "Icon");
					if(property != null && property.Value != null)
					{
						//	An icon is present.
						iconSize = ((Bitmap)property.Value).Size;
					}
					if(mNodeMaxWidth > 0)
					{
						//	Fit title within node bounds.
						socketTextSize =
							g.MeasureString(socket[socket.TitleProperty].StringValue(),
							fontSocket,
							mNodeMaxWidth - (int)socket.Width - (int)iconSize.Width);
					}
					else
					{
						//	Allow socket to take unlimited width.
						socketTextSize =
							g.MeasureString(socket[socket.TitleProperty].StringValue(),
							fontSocket);
						socketTextSize.Width += (socket.Width + iconSize.Width);
					}
					//	Keep in mind that the width of the socket might push the width of
					//	the node.
					node.Width = Math.Max(node.Width, socketTextSize.Width);
					//	The socket text bounds cover the entire width of the node.
					socket.TextBounds =
						new RectangleF(
							node.X + (socket.Width / 2f),
							lastSocketY + lastTextHeight + socketVSpacing,
							node.Width - socket.Width,
							Math.Max(socketTextSize.Height, iconSize.Height));
					socket.Y = socket.TextBounds.Y + (socket.TextBounds.Height / 2f) -
						(socket.Height / 2f);
					if(socket.SocketMode == SocketModeEnum.Input)
					{
						//	Inputs are on the left side.
						socket.X = node.X - (socket.Width / 2f);
					}
					else
					{
						//	Outputs are on the right side.
						socket.X = node.X + node.Width - (socket.Width / 2f);
					}
					lastSocketY = socket.TextBounds.Y;
					lastTextHeight = socket.TextBounds.Height;
					index++;
				}
				//	Calculate final node height.
				node.Height =
					lastSocketY + lastTextHeight + socketVSpacing - node.Y;

				//	Get the node bounds with sockets.
				rect = new RectangleF(
					node.X, node.Y,
					node.Width + socketMaxWidth, node.Height);
				bounds = RectangleF.Union(bounds,
					ScaleDrawing(rect, mDrawingScale));
				maxWidth = Math.Max(maxWidth, rect.Width);
				maxHeight = Math.Max(maxHeight, rect.Height);
			}

			width = (int)bounds.Width;
			height = (int)bounds.Height;
			if(width != pnlView.Width ||
				height != pnlView.Height)
			{
				pnlView.Size = new Size(width, height);
			}

			Verbose(
				$"View: {new Rectangle(pnlView.Location, pnlView.Size)}; " +
				$"Bounds: {bounds}.; " +
				$"H: {hMin} < " +
				$"{hVal} < " +
				$"{hMax}; " +
				$"V: {vMin} < " +
				$"{vVal} < " +
				$"{vMax}", 2);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mTimer_Tick																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Timer has elapsed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mTimer_Tick(object sender, EventArgs e)
		{
			if(!DesignMode)
			{
				//	Only process when not in design mode.
				mTimer.Stop();
				//	Force the scroll bars to their expected values.
				//	When zooming, these values might be invalid.
				if(this.HorizontalScroll.Value != mScrollHorizontal)
				{
					try
					{
						this.HorizontalScroll.Value = mScrollHorizontal;
					}
					catch { }
					QueueInvalidate();
				}
				if(this.VerticalScroll.Value != mScrollVertical)
				{
					try
					{
						this.VerticalScroll.Value = mScrollVertical;
					}
					catch { }
					QueueInvalidate();
				}
				if(mNeedsInvalidate)
				{
					pnlView.Invalidate();
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlView_Click																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		///	The view has received a click.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlView_Click(object sender, EventArgs e)
		{
			OnClick(e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlView_MouseClick																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has been clicked on the canvas view.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlView_MouseClick(object sender, MouseEventArgs e)
		{
			//NodeItem node = null;

			mMousePositionLast = PointToScreen(e.Location);

			//	Get the first node in view under the mouse.
			mClickedNodeLast = mNodes.OrderByDescending(x => x.ZOrder).
				FirstOrDefault(x => NodeItem.GetBounds(x).Contains(
					ScaleDrawing(e.Location, mDrawingScale)));

			if(e.Button == MouseButtons.Right)
			{
				//	Right click.
				//	Display context menu.
				var context = new ContextMenuStrip();
				context.Items.Add("&Edit", null, ((o, args) =>
				{
					//	Display the node edit dialog.
					if(mClickedNodeLast != null)
					{
						frmDecisionNode frm = new frmDecisionNode();
						frm.SetNode(mClickedNodeLast);
						frm.ShowDialog();
						QueueInvalidate();
					}
				}));
				context.Show(MousePosition);
			}
			OnMouseClick(e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlView_MouseDoubleClick																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A mouse button has been double-clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			mMousePositionLast = PointToScreen(e.Location);

			//	Get the first node in view under the mouse.
			mClickedNodeLast = mNodes.OrderByDescending(x => x.ZOrder).
				FirstOrDefault(x => NodeItem.GetBounds(x).Contains(
					ScaleDrawing(e.Location, mDrawingScale)));

			if(mClickedNodeLast != null)
			{
				frmDecisionNode frm = new frmDecisionNode();
				frm.SetNode(mClickedNodeLast);
				frm.ShowDialog();
				QueueInvalidate();
			}
			OnMouseDoubleClick(e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlView_MouseDown																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A mouse button is depressed on the canvas view.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlView_MouseDown(object sender, MouseEventArgs e)
		{
			SocketItem connection = null;
			//NodeEventArgs eaNode = null;
			NodeItem node = null;
			NodeItem nodeWhole = null;
			SocketItem socket = null;

			Verbose("Mouse down...");
			if(e.Button == MouseButtons.Left)
			{
				mSelectionStart = PointF.Empty;
				Focus();
				//	On [Shift][Mouse], additional nodes can be selected.
				//	On normal [Mouse] on a selected item, all previously
				//		selected items go into move mode.
				//	On normal [Mouse] on a non-selected item, all previously
				//		selected items get unselected and the new item goes into
				//		move mode.
				//	On [Ctrl][Mouse], the selection state of the current item is
				//		toggled, and no other items are changed.

				//	Get the first node in view under the mouse.
				node = mNodes.OrderByDescending(x => x.ZOrder).
					FirstOrDefault(x =>
					new RectangleF(new PointF(x.X, x.Y),
					NodeItem.GetTitleSize(x)).Contains(
						ScaleDrawing(e.Location, mDrawingScale,
						0, 0)));
				//	Process selection state.
				if(node != null)
				{
					//	Clicking a node header or icons.

					if(node.Selected)
					{
						//	Clicking on pre-selected node.
						Verbose(" Preselected node...", 2);
						if((ModifierKeys & Keys.Control) == Keys.Control)
						{
							//	Toggle selection state for this item.
							node.Selected = !node.Selected;
						}
						else
						{
							//	The node is already selected. We can drag all selected items.
						}
					}
					else
					{
						//	Clicking on non-selected node.
						if((ModifierKeys & Keys.Control) == Keys.Control)
						{
							node.Selected = !node.Selected;
						}
						else
						{
							if((ModifierKeys & Keys.Shift) != Keys.Shift)
							{
								//	Deselect all nodes except this one.
								//	Be careful not to set the selected property on all
								//	nodes, to avoid property change notifications.
								mNodes.Where(w => w != node && w.Selected).
									ToList().ForEach(x => x.Selected = false);
							}
							node.Selected = true;
						}
					}
				}
				else
				{
					//	Icons and thumbnails.
					node = mNodes.OrderByDescending(x => x.ZOrder).
						FirstOrDefault(x =>
						NodeItem.GetIconArea(x).Contains(
							ScaleDrawing(e.Location, mDrawingScale,
							0, 0)));
					if(node != null)
					{
						mClickedNodeLast = node;
						////	The user has clicked in the icon and thumbnail area.
						//if(NodeItem.GetIconArea(node, "MediaAudio").Contains(
						//	ScaleDrawing(e.Location, mDrawingScale, 0, 0)))
						//{
						//	//	This is either a click on the audio icon or the
						//	//	image or video thumbnail.
						//	if(MediaExists(node, mResources, "MediaAudio"))
						//	{
						//		//	Audio was clicked.
						//		eaNode = new NodeEventArgs(node, "MediaAudio");
						//		OnDisplayNodeResource(eaNode);
						//	}
						//	else if(MediaExists(node, mResources, "MediaImage"))
						//	{
						//		//	Image thumbnail was clicked.
						//		eaNode = new NodeEventArgs(node, "MediaImage");
						//		OnDisplayNodeResource(eaNode);
						//	}
						//	else if(MediaExists(node, mResources, "MediaVideo"))
						//	{
						//		//	Video thumbnail was clicked.
						//		eaNode = new NodeEventArgs(node, "MediaVideo");
						//		OnDisplayNodeResource(eaNode);
						//	}
						//}
						//else if(NodeItem.GetIconArea(node, "MediaLink").Contains(
						//	ScaleDrawing(e.Location, mDrawingScale, 0, 0)))
						//{
						//	//	This is either a click on the link icon or the image or
						//	//	video thumbnail.
						//	if(MediaExists(node, mResources, "MediaLink"))
						//	{
						//		//	Link was clicked.
						//		eaNode = new NodeEventArgs(node, "MediaLink");
						//		OnDisplayNodeResource(eaNode);
						//	}
						//	else if(MediaExists(node, mResources, "MediaImage"))
						//	{
						//		//	Image thumbnail was clicked.
						//		eaNode = new NodeEventArgs(node, "MediaImage");
						//		OnDisplayNodeResource(eaNode);
						//	}
						//	else if(MediaExists(node, mResources, "MediaVideo"))
						//	{
						//		//	Video thumbnail was clicked.
						//		eaNode = new NodeEventArgs(node, "MediaVideo");
						//		OnDisplayNodeResource(eaNode);
						//	}
						//}
						//else
						//{
						//	//	This is either an image or video thumbnail click.
						//	if(MediaExists(node, mResources, "MediaImage"))
						//	{
						//		//	Image thumbnail was clicked.
						//		eaNode = new NodeEventArgs(node, "MediaImage");
						//		OnDisplayNodeResource(eaNode);
						//	}
						//	else if(MediaExists(node, mResources, "MediaVideo"))
						//	{
						//		//	Video thumbnail was clicked.
						//		eaNode = new NodeEventArgs(node, "MediaVideo");
						//		OnDisplayNodeResource(eaNode);
						//	}
						//}
					}
					node = null;
				}
				if(node != null && !mMouseDown)
				{
					Verbose($"Node selected: {node.Selected}", 2);
					if(node.Selected)
					{
						node.ZOrder = mNodes.Max(x => x.ZOrder) + 1;
					}
					mMouseDown = true;
					mMousePositionLast = PointToScreen(e.Location);
					Refresh();
				}
				if(node == null && !mMouseDown)
				{
					//	User is clicking on an area not occupied by a node header.
					//	Check the entire node for click.
					nodeWhole =
						mNodes.OrderByDescending(x => x.ZOrder).FirstOrDefault(x =>
						RectangleF.Inflate(NodeItem.GetBounds(x),
						x.MaxSocketWidth() * 2f, 0f).
						Contains(ScaleDrawing(e.Location, mDrawingScale,
							0, 0)));
					if(nodeWhole != null)
					{
						//	Mouse is depressed within the node body, plus sockets.
						node = nodeWhole;
						socket = nodeWhole.Sockets.FirstOrDefault(x =>
							x.GetBounds().Contains(ScaleDrawing(e.Location, mDrawingScale,
							0, 0)));
						if(socket != null)
						{
							//	Mouse is depressed on a socket.
							mDragTarget = null;
							if((ModifierKeys & Keys.Control) == Keys.Control &&
								socket.SocketMode == SocketModeEnum.Input)
							{
								//	[Ctrl][Click] on socket.
								//	Move an existing connection.
								connection = mNodes.FirstOutputSocket(socket);
								if(connection != null)
								{
									//	A connection exists to this socket.
									//	Move the existing connection.
									mDragSocket = connection;
									mDragTarget = socket;
									//mDragSocketNode = nodeWhole;
									OnEditorMessage("Moving connection...");
								}
							}
							else if(socket.SocketMode == SocketModeEnum.Output)
							{
								//	Normal click on an output socket.
								//	Build new connection.
								mDragSocket = socket;
								//mDragSocketNode = nodeWhole;
								OnEditorMessage("New connection in progress...");
							}
							else
							{
								mDragSocket = null;
								//mDragSocketNode = null;
							}
							mDragConnectionStart = ScaleDrawing(e.Location, mDrawingScale,
								0, 0);
							mDragConnectionEnd = ScaleDrawing(e.Location, mDrawingScale,
								0, 0);
							mMouseDown = true;
							mMousePositionLast = PointToScreen(e.Location);
						}
					}
					else
					{
						//	Node not clicked.
						//	Start selection.
						Verbose(" Selected started...", 2);
						mSelectionStart = mSelectionEnd =
							ScaleDrawing(e.Location, mDrawingScale,
							0, 0);
					}
				}
			}
			if(mDragSocket != null && mNodes.Exists(x => x.Selected))
			{
				mNodes.Where(x => x.Selected).ToList().
					ForEach(x => x.Selected = false);
			}
			QueueInvalidate();
			OnMouseDown(e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlView_MouseMove																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse is moving over the canvas view.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlView_MouseMove(object sender, MouseEventArgs e)
		{
			RectangleF bound = RectangleF.Empty;
			PointF center = PointF.Empty;
			float dragX = 0f;
			float dragY = 0f;
			NodeEventArgs eaNode = null;
			Point em = Point.Empty;
			NodeItem n = null;

			OnCursorMessage($"{e.Location}");
			mMouseCanvasLocation = e.Location;
			//	Selection area active?
			em = PointToScreen(e.Location);
			if(mSelectionStart != PointF.Empty)
			{
				mSelectionEnd = ScaleDrawing(e.Location, mDrawingScale,
					0, 0);
			}
			if(mMouseDown)
			{
				//	Check to see if the mouse is still down.
				if(e.Button == MouseButtons.None)
				{
					mMouseDown = false;
				}
			}
			if(mMouseDown)
			{
				//	Move selected items.
				foreach(NodeItem node in mNodes.Where(x => x.Selected))
				{
					//	TODO: Convert change in position to screen on demand.
					dragX = ScaleDrawing(em.X, mDrawingScale.Width) -
						ScaleDrawing(mMousePositionLast.X, mDrawingScale.Width);
					dragY = ScaleDrawing(em.Y, mDrawingScale.Height) -
						ScaleDrawing(mMousePositionLast.Y, mDrawingScale.Height);

					eaNode = mNodesMoving.FirstOrDefault(x =>
						x.Node.Ticket == node.Ticket);
					if(eaNode == null)
					{
						//	Moving event hasn't yet been fired.
						eaNode = new NodeEventArgs(node);
						mNodesMoving.Add(eaNode);
						OnNodeMoving(eaNode);
					}
					node.X += dragX;
					node.Y += dragY;
					OnEditorMessage(
						$"Node Drag - X:{dragX:0.000}, Y:{dragY:0.000}");
				}
				if(mNodes.Exists(x => x.Selected))
				{
					n = mNodes.FirstOrDefault(x => x.Selected);
					bound = new RectangleF(n.X, n.Y, n.Width, n.Height);
					foreach(NodeItem node in mNodes.Where(x => x.Selected))
					{
						bound = RectangleF.Union(bound,
							new RectangleF(node.X, node.Y, node.Width, node.Height));
					}
				}
				pnlView.Invalidate();
				//	Process connection drag.
				if(mDragSocket != null)
				{
					center = new PointF(
						mDragSocket.X + mDragSocket.Width / 2f,
						mDragSocket.Y + mDragSocket.Height / 2f);
					if(mDragSocket.SocketMode == SocketModeEnum.Input)
					{
						//	Convert mouse location to scale on demand.
						mDragConnectionStart.X +=
							ScaleDrawing(em.X, mDrawingScale.Width) -
							ScaleDrawing(mMousePositionLast.X, mDrawingScale.Width);
						mDragConnectionStart.Y +=
							ScaleDrawing(em.Y, mDrawingScale.Height) -
							ScaleDrawing(mMousePositionLast.Y, mDrawingScale.Height);
						mDragConnectionEnd = center;
					}
					else
					{
						//	Convert mouse location to scale on demand.
						mDragConnectionStart = center;
						mDragConnectionEnd.X +=
							ScaleDrawing(em.X, mDrawingScale.Width) -
							ScaleDrawing(mMousePositionLast.X, mDrawingScale.Width);
						mDragConnectionEnd.Y +=
							ScaleDrawing(em.Y, mDrawingScale.Height) -
							ScaleDrawing(mMousePositionLast.Y, mDrawingScale.Height);
					}
				}
				mMousePositionLast = em;
			}
			else if(em.X - mMousePositionLast.X != 0 ||
				em.Y - mMousePositionLast.Y != 0)
			{
				Verbose($"Move: {e.Location}", 2);
				em = Point.Round(ScaleDrawing(e.Location, mDrawingScale));
				OnEditorMessage($"Effective - {em}");
			}
			QueueInvalidate();
			OnMouseMove(e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlView_MouseUp																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A mouse button has been released over the canvas view.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void pnlView_MouseUp(object sender, MouseEventArgs e)
		{
			NodeEventArgs eaNode = null;
			NodeItem nodeWhole = null;
			RectangleF rect = RectangleF.Empty;
			SocketItem socket = null;

			Verbose("Mouse up...");
			//	Update selection.
			if(mSelectionStart != PointF.Empty)
			{
				rect = RectangleFromPoints(mSelectionStart, mSelectionEnd);
				mNodes.ForEach(x => x.Selected =
					rect.Contains(NodeItem.GetBounds(x)));
				mSelectionStart = PointF.Empty;
			}
			if(mDragSocket != null)
			{
				//	Socket being dragged.
				Verbose("Drag socket present...", 2);
				//	Check whole node status, including sockets.
				nodeWhole = mNodes.OrderByDescending(x => x.ZOrder).
					FirstOrDefault(x =>
					RectangleF.Inflate(NodeItem.GetBounds(x), x.MaxSocketWidth(), 0f).
					Contains(ScaleDrawing(e.Location, mDrawingScale,
						0, 0)));
				if(nodeWhole != null)
				{
					//	Node tested positive for hit test.
					socket = nodeWhole.Sockets.FirstOrDefault(x =>
						x.SocketMode == SocketModeEnum.Input &&
						x.GetBounds().Contains(ScaleDrawing(e.Location, mDrawingScale,
							0, 0)));
					if(socket != null)
					{
						//	Mouse was above socket.
						//	The user is dropping the mouse on an input node.
						if(mDragSocket.SocketMode == SocketModeEnum.Output)
						{
							//	Output socket is in tow.
							if(!mDragSocket.Connections.Exists(x =>
								x.Ticket == socket.Ticket))
							{
								//	Only add the connection if unique.
								mDragSocket.Connections.Add(socket);
								OnEditorMessage("Connection created...");
							}
							else
							{
								//	Print a message on the status bar.
								OnEditorMessage("Connection already exists...");
							}
						}
					}
					else
					{
						//	No drop point.
						//	Delete the connection that was being dragged.
						if((ModifierKeys & Keys.Control) == Keys.Control)
						{
							//	[Ctrl][Mouse]. Move existing connection.
							if(mDragSocket.Connections.Count > 0 && mDragTarget != null)
							{
								mDragSocket.Connections.RemoveAll(x => x.Ticket == mDragTarget.Ticket);
								//mDragSocket.Connections.RemoveAt(0);
								OnEditorMessage("Connection deleted...");
							}
							else
							{
								OnEditorMessage("Connection dropped...");
							}
						}
						else
						{
							OnEditorMessage("Connection dropped...");
						}
					}
				}
				else
				{
					//	No drop point.
					//	Delete the connection that was being dragged.
					if((ModifierKeys & Keys.Control) == Keys.Control)
					{
						//	[Ctrl][Mouse]. Move existing connection.
						if(mDragSocket.Connections.Count > 0)
						{
							mDragSocket.Connections.RemoveAt(0);
							OnEditorMessage("Connection deleted...");
						}
						else
						{
							OnEditorMessage("Connection dropped...");
						}
					}
					else
					{
						OnEditorMessage("Connection dropped...");
					}
				}
			}
			else
			{
				//	Check for dragged nodes out of bounds.
				Verbose("Drag socket not present...", 2);
				foreach(NodeItem node in mNodes.Where(x => x.Selected))
				{
					if(node.X < 0f || node.Y < 0f)
					{
						OnEditorMessage(
							$"Moving node {node.Ticket} back in bounds.");
						if(node.X < 0f)
						{
							node.X = 0f;
						}
						if(node.Y < 0f)
						{
							node.Y = 0f;
						}
					}
				}
				//	Stop moving any moving nodes.
				while(mNodesMoving.Count > 0)
				{
					eaNode = mNodesMoving[0];
					OnNodeMoved(eaNode);
					mNodesMoving.RemoveAt(0);
				}
			}
			mDragSocket = null;
			mMouseDown = false;
			mMousePositionLast = PointToScreen(e.Location);
			QueueInvalidate();
			OnMouseUp(e);
			Verbose("End of Mouse up...", 2);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlView_Paint																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The canvas view is being painted.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Paint event arguments.
		/// </param>
		private void pnlView_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			Pen pen = null;
			bool prevEventsEnabled = mEventsEnabled;
			Rectangle rect = Rectangle.Empty;
			SizeF translate = SizeF.Empty;
			int valueX = 0;
			int valueY = 0;

			mEventsEnabled = false;
			Verbose("Paint start...", 3);
			Verbose(" Measure view...", 3);
			MeasureView(g);

			g.SmoothingMode = SmoothingMode.HighQuality;
			g.InterpolationMode = InterpolationMode.HighQualityBilinear;
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

			//	Background color.
			Verbose(" Fill background...", 3);
			g.FillRectangle(new SolidBrush(BackColor), g.ClipBounds);

			//	Pan.
			//if(HorizontalScrollValue != 0 || VerticalScrollValue != 0)
			//{
			//	translate = new SizeF(
			//		(float)(0 - (HorizontalScrollValue * mDrawingScale.Width)),
			//		(float)(0 - (VerticalScrollValue * mDrawingScale.Height)));
			//	g.TranslateTransform(translate.Width, translate.Height);
			//	Verbose($"G Translate: {translate}", 3);
			//}

			//	Scale.
			Verbose(" Scale...", 3);
			if(mDrawingScale.Width != 0f && mDrawingScale.Height != 0f)
			{
				g.ScaleTransform(mDrawingScale.Width, mDrawingScale.Height);
			}

			//	Nodes and connections.
			Verbose(" Draw nodes and connections...", 3);
			DrawNodes(g, PointToClient(Control.MousePosition), MouseButtons);
			//DrawNodes(g, Control.MousePosition, MouseButtons);

			//	Active drag.
			Verbose(" Process active drag...", 3);
			if(mDragSocket != null)
			{
				pen = new Pen(mSocketDragColor, mSocketConnectionLineWidth);
				DrawConnection(g, pen,
					mDragConnectionStart, mDragConnectionEnd);
			}

			//	Active selection.
			Verbose(" Process active selection...", 3);
			if(mSelectionStart != PointF.Empty)
			{
				rect = Rectangle.Round(
					RectangleFromPoints(mSelectionStart, mSelectionEnd));
				Verbose($" Painting selection: {rect}", 3);
				//	TODO: Set selection colors from control.
				g.FillRectangle(
					new SolidBrush(Color.FromArgb(50, Color.CornflowerBlue)), rect);
				g.DrawRectangle(
					new Pen(Color.DodgerBlue), rect);
			}

			//	Process requested screen center.
			if(mViewCenterFraction != Point.Empty)
			{
				//	TODO: Scroll correction needs to be evened out.
				valueX = 0;
				if(pnlView.Width > this.Width)
				{
					valueX = (int)(mViewCenterFraction.X *
						((float)pnlView.Width - (float)this.Width));
					Debug.WriteLine(
						$"Scroll X to: {valueX} of {pnlView.Width - this.Width}");
					if(valueX >= this.HorizontalScroll.Minimum &&
						valueX <= this.HorizontalScroll.Maximum)
					{
						//this.HorizontalScroll.Value = valueX;
						mScrollHorizontal = valueX;
					}
				}
				valueY = 0;
				if(pnlView.Height > this.Height)
				{
					valueY = (int)(mViewCenterFraction.Y *
						((float)pnlView.Height - (float)this.Height));
					Debug.WriteLine(
						$"Scroll Y to: {valueY} of {pnlView.Height - this.Height}");
					if(valueY >= this.VerticalScroll.Minimum &&
						valueY <= this.VerticalScroll.Maximum)
					{
						//this.VerticalScroll.Value = valueY;
						mScrollVertical = valueY;
					}
				}
				mViewCenterFraction = Point.Empty;
			}

			//QueueInvalidate();
			OnPaint(e);
			Verbose("Paint end...", 3);
			mEventsEnabled = prevEventsEnabled;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlView_Resize																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The panel has been resized.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlView_Resize(object sender, EventArgs e)
		{
			QueueInvalidate();
			OnResize(e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* QueueInvalidate																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Self-invalidate after a brief delay.
		/// </summary>
		private void QueueInvalidate()
		{
			mNeedsInvalidate = true;
			if(!mTimer.Enabled)
			{
				mTimer.Start();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UnbindNodes																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Unbind events from the nodes collection.
		/// </summary>
		/// <param name="nodes">
		/// Reference to a nodes collection with events to be unbound.
		/// </param>
		private void UnbindNodes(NodeCollection nodes)
		{
			nodes.NodeAdded -= OnNodeAdded;
			nodes.NodeDeleted -= OnNodeDeleted;
			nodes.NodePropertyChanged -= OnNodePropertyChanged;
			nodes.SocketAdded -= OnSocketAdded;
			nodes.SocketConnectionAdded -= OnSocketConnectionAdded;
			nodes.SocketConnectionDeleted -= OnSocketConnectionDeleted;
			nodes.SocketDeleted -= OnSocketDeleted;
			nodes.SocketPropertyChanged -= OnSocketPropertyChanged;
			nodes.SelectionChanged -= OnSelectionChanged;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UnbindResources																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Unbind events from the resources collection.
		/// </summary>
		/// <param name="resources">
		/// Reference to a collection of resources with events to be unbound.
		/// </param>
		private void UnbindResources(ResourceCollection resources)
		{

		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnBackColorChanged																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the BackColorChanged event when the background color is changed.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			pnlView.BackColor = this.BackColor;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnCursorMessage																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the CursorMessage event when the cursor is moving.
		/// </summary>
		/// <param name="text">
		/// Text to display in the cursor status.
		/// </param>
		protected virtual void OnCursorMessage(string text)
		{
			CursorMessage?.Invoke(this, new MessageEventArgs(text));
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnDisplayNodeResource																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the DisplayNodeResource event so the host will allow the user
		/// to interact with the specified resource.
		/// </summary>
		/// <param name="e">
		/// Reference to a NodeEventArgs containing the resource property name
		/// in its Information property.
		/// </param>
		protected virtual void OnDisplayNodeResource(NodeEventArgs e)
		{
			DisplayNodeResource?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnEditorMessage																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the EditorMessage event when a new message is being sent to the
		/// host control.
		/// </summary>
		/// <param name="message">
		/// The message to inspect or display.
		/// </param>
		protected virtual void OnEditorMessage(string message)
		{
			EditorMessage?.Invoke(this, new MessageEventArgs(message));
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnInvalidated																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the Invalidated event when the control has been invalidated.
		/// </summary>
		/// <param name="e">
		/// Invalidate event arguments.
		/// </param>
		protected override void OnInvalidated(InvalidateEventArgs e)
		{
			base.OnInvalidated(e);
			pnlView.Invalidate();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	OnKeyDown																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the KeyDown event when a keyboard key has been depressed.
		/// </summary>
		/// <param name="e">
		/// </param>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			NodeEventArgs eaNode = null;

			base.OnKeyDown(e);
			switch(e.KeyCode)
			{
				case Keys.Delete:
					DeleteSelectedNodes();
					break;
				case Keys.Escape:
					if(mNodesMoving.Count > 0)
					{
						//	Cancel move.
						while(mNodesMoving.Count > 0)
						{
							eaNode = mNodesMoving[0];
							eaNode.Node.X = eaNode.OriginalNode.X;
							eaNode.Node.Y = eaNode.OriginalNode.Y;
							mNodesMoving.RemoveAt(0);
						}
						mMouseDown = false;
					}
					break;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnMouseWheel																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fires the MouseWheel event when the mouse wheel has been used.
		/// </summary>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			PointF center = PointF.Empty;
			float val = 0f;
			int scrollAmount = 0;

			//	Don't call base on this feature, because it doesn't ever behave
			//	as expected in WinForms.
			//base.OnMouseWheel(e);
			Verbose($"Wheel. Delta: {e.Delta}");
			if((ModifierKeys & Keys.Control) == Keys.Control)
			{
				//	Control wheel. Scroll in and out.
				//	[Ctrl][WheelForward] = Zoom In.
				//	[Ctrl][WheelBacward] = Zoom Out.
				if(e.Delta >= 0)
				{
					val = (float)e.Delta / 100f;
				}
				else
				{
					val = 1f / (Math.Abs((float)e.Delta) / 100f);
				}
				center = GetMouseCenterAbstract();
				mDrawingScale = new SizeF(
					mDrawingScale.Width * val, mDrawingScale.Height * val);
				OnEditorMessage("Zoom: " +
					$"X:{mDrawingScale.Width:0.00}, Y:{mDrawingScale.Height:0.00}");
				QueueViewCenter(center);
				QueueInvalidate();
			}
			else if((ModifierKeys & Keys.Shift) == Keys.Shift)
			{
				//	Shift wheel. Scroll left and right.
				//	[Shift][WheelForward] = Scroll left.
				//	[Shift][WheelBackward] = Scroll right.
				scrollAmount = Math.Max(0,
					(int)((float)this.HorizontalScroll.SmallChange *
							(mDrawingScale.Width * 10f)));
				if(e.Delta > 0 &&
					this.HorizontalScroll.Value - scrollAmount >
					this.HorizontalScroll.Minimum)
				{
					//	Wheel forward.
					mScrollHorizontal = this.HorizontalScroll.Value - scrollAmount;
					this.HorizontalScroll.Value = mScrollHorizontal;
				}
				else if(e.Delta < 0 &&
					this.HorizontalScroll.Value + scrollAmount <
					this.HorizontalScroll.Maximum)
				{
					//	Wheel backward.
					mScrollHorizontal = this.HorizontalScroll.Value + scrollAmount;
					this.HorizontalScroll.Value = mScrollHorizontal;
				}
			}
			else
			{
				//	No modification. Scroll up and down.
				//	[WheelForward] = Scroll up.
				//	[WheelBackward] = Scroll down.
				scrollAmount = Math.Max(0,
					(int)((float)this.VerticalScroll.SmallChange *
							(mDrawingScale.Height * 10f)));
				if(e.Delta > 0 &&
					this.VerticalScroll.Value - scrollAmount >
					this.VerticalScroll.Minimum)
				{
					//	Wheel forward.
					mScrollVertical = this.VerticalScroll.Value - scrollAmount;
					this.VerticalScroll.Value = mScrollVertical;
				}
				else if(e.Delta < 0 &&
					this.VerticalScroll.Value + scrollAmount <
					this.VerticalScroll.Maximum)
				{
					//	Wheel backward.
					mScrollVertical = this.VerticalScroll.Value + scrollAmount;
					this.VerticalScroll.Value = mScrollVertical;
				}
			}
			QueueInvalidate();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnNodeAdded																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the NodeAdded event when a node has been added.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Node event arguments.
		/// </param>
		protected virtual void OnNodeAdded(object sender, NodeEventArgs e)
		{
			if(mEventsEnabled)
			{
				NodeAdded?.Invoke(sender, e);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnNodeDeleted																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the NodeDeleted event when a node has been deleted.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Node event arguments.
		/// </param>
		protected virtual void OnNodeDeleted(object sender, NodeEventArgs e)
		{
			if(mEventsEnabled)
			{
				NodeDeleted?.Invoke(sender, e);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnNodeMoved																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the NodeMoved event when a node has been moved.
		/// </summary>
		/// <param name="e">
		/// Node event arguments.
		/// </param>
		protected virtual void OnNodeMoved(NodeEventArgs e)
		{
			if(mEventsEnabled)
			{
				NodeMoved?.Invoke(this, e);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnNodeMoving																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the NodeMoving event when a node has started moving.
		/// </summary>
		/// <param name="e">
		/// Node event arguments.
		/// </param>
		protected virtual void OnNodeMoving(NodeEventArgs e)
		{
			if(mEventsEnabled)
			{
				NodeMoving?.Invoke(this, e);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnNodePropertyChanged																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the NodePropertyChanged event when the value of a property has
		/// been changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Node property change event arguments.
		/// </param>
		protected virtual void OnNodePropertyChanged(object sender,
			NodePropertyChangeEventArgs e)
		{
			if(mEventsEnabled)
			{
				NodePropertyChanged?.Invoke(this, e);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnResize																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the Resize event when the control has been resized.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			QueueInvalidate();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSelectionChanged																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SelectionChanged event when the Selected property of a node
		/// has been changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Node event arguments.
		/// </param>
		protected virtual void OnSelectionChanged(object sender, NodeEventArgs e)
		{
			//	Update the queue before passing the event out to subscribers.
			NodeItem node = null;

			if(e.Node != null)
			{
				if(e.Node.Selected)
				{
					//	This item will be added if not already a member.
					if(!mSelectionQueue.Exists(x => x.Ticket == e.Node.Ticket))
					{
						mSelectionQueue.Add(e.Node);
					}
				}
				else
				{
					//	This item will be removed if present.
					node =
						mSelectionQueue.FirstOrDefault(x => x.Ticket == e.Node.Ticket);
					if(node != null)
					{
						mSelectionQueue.Remove(node);
					}
				}
			}
			SelectionChanged?.Invoke(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnScroll																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the Scroll event when the control has been scrolled.
		/// </summary>
		/// <param name="se">
		/// Scroll event arguments.
		/// </param>
		protected override void OnScroll(ScrollEventArgs se)
		{
			base.OnScroll(se);
			if(se.ScrollOrientation == ScrollOrientation.HorizontalScroll)
			{
				mScrollHorizontal = HorizontalScroll.Value;
			}
			else if(se.ScrollOrientation == ScrollOrientation.VerticalScroll)
			{
				mScrollVertical = VerticalScroll.Value;
			}
			QueueInvalidate();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSocketAdded																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SocketAdded event when a socket has been added.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket event arguments.
		/// </param>
		protected virtual void OnSocketAdded(object sender, SocketEventArgs e)
		{
			if(mEventsEnabled)
			{
				SocketAdded?.Invoke(sender, e);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSocketConnectionAdded																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SocketConnectionAdded event when a connection has been added
		/// to a node socket.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket event arguments.
		/// </param>
		protected virtual void OnSocketConnectionAdded(object sender,
			SocketConnectionEventArgs e)
		{
			if(mEventsEnabled)
			{
				SocketConnectionAdded?.Invoke(sender, e);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSocketConnectionDeleted																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SocketConnectionDeleted event when a connection has been
		/// deleted from a node socket.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket event arguments.
		/// </param>
		protected virtual void OnSocketConnectionDeleted(object sender,
			SocketConnectionEventArgs e)
		{
			if(mEventsEnabled)
			{
				SocketConnectionDeleted?.Invoke(sender, e);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSocketDeleted																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SocketDeleted event when a socket has been deleted.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket event arguments.
		/// </param>
		protected virtual void OnSocketDeleted(object sender, SocketEventArgs e)
		{
			if(mEventsEnabled)
			{
				SocketDeleted?.Invoke(this, e);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnSocketPropertyChanged																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SocketPropertyChange event when the value of a property
		/// has been changed on the node socket.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Socket property change event arguments.
		/// </param>
		protected virtual void OnSocketPropertyChanged(object sender,
			SocketPropertyChangeEventArgs e)
		{
			if(mEventsEnabled)
			{
				SocketPropertyChanged?.Invoke(this, e);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* WndProc																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Windows message pump monitor.
		/// </summary>
		/// <param name="m">
		/// Windows message.
		/// </param>
		protected override void WndProc(ref Message m)
		{
			if(m.Msg != 7)
			{
				//	Only process if not SetFocus.
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
		/// Create a new instance of the NodeControl Item.
		/// </summary>
		public NodeControl()
		{
			InitializeComponent();
			mTimer.Interval = 100;
			mTimer.Tick += mTimer_Tick;
			mTimer.Start();
			SetStyle(ControlStyles.Selectable, true);
			DoubleBuffered = true;
			typeof(System.Windows.Forms.Panel).InvokeMember("DoubleBuffered",
				BindingFlags.SetProperty | BindingFlags.Instance |
				BindingFlags.NonPublic, null, pnlView, new object[] { true });
			//NodeFile = new NodeFileItem();
			BindNodes(mNodes);
			BindResources(mResources);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	CanvasHeight																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the height of the active drawing canvas under the window.
		/// </summary>
		public int CanvasHeight
		{
			get { return pnlView.Height; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	CanvasMouse																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get a reference to the last known canvas-oriented mouse position.
		/// </summary>
		public Point CanvasMouse
		{
			get { return mMouseCanvasLocation; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	CanvasWidth																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the width of the active drawing canvas under the window.
		/// </summary>
		public int CanvasWidth
		{
			get { return pnlView.Width; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ChangeSelectedNodesColor																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Allow the user to change the color of the selected nodes.
		/// </summary>
		public void ChangeSelectedNodesColor()
		{
			frmColorSelect form = new frmColorSelect();

			form.StartPosition = FormStartPosition.Manual;
			form.Location = CenterOver(this.ParentForm, form);
			if(form.ShowDialog() == DialogResult.OK)
			{
				foreach(NodeItem node in mNodes.Where(x => x.Selected))
				{
					node.NodeColor = form.Color;
				}
			}
			Refresh();
			QueueInvalidate();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ChangeSelectedNodesTextColor																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Allow the user to change the text color on the selected nodes.
		/// </summary>
		public void ChangeSelectedNodesTextColor()
		{
			frmColorSelect form = new frmColorSelect();

			form.StartPosition = FormStartPosition.Manual;
			form.Location = CenterOver(this.ParentForm, form);
			if(form.ShowDialog() == DialogResult.OK)
			{
				foreach(NodeItem node in mNodes.Where(x => x.Selected))
				{
					node.NodeTextColor = form.Color;
				}
			}
			Refresh();
			QueueInvalidate();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* CursorMessage																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a cursor message is being sent to the host control.
		/// </summary>
		public event MessageEventHandler CursorMessage;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* DisplayNodeResource																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a request is made to access a media resource on a node.
		/// </summary>
		public event NodeEventHandler DisplayNodeResource;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DuplicateSelectedNodes																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Duplicate the selected nodes.
		/// </summary>
		public void DuplicateSelectedNodes()
		{
			List<NodeItem> cloned = new List<NodeItem>();
			int count = 0;
			NodeItem nodeNew = null;

			count = mNodes.Count(x => x.Selected);
			foreach(NodeItem node in mNodes.Where(x => x.Selected))
			{
				nodeNew = new NodeItem(node);
				nodeNew.X += 40;
				nodeNew.Y += 40;
				UpdateThumbnails(nodeNew);
				cloned.Add(nodeNew);
			}
			mNodes.ForEach(x => x.Selected = false);
			cloned.ForEach(x => x.Selected = false);
			cloned.ForEach(x => x.Sockets.ForEach(y => y.Connections.Clear()));
			//	TODO: Allow node groups to be added using AddRange method.
			//	Don't use addrange for event-sensitive nodes until a suitable
			//	override is found that can accomodate wiring of each item without
			//	potential for override holes.
			foreach(NodeItem nodeItem in cloned)
			{
				mNodes.Add(nodeItem);
			}
			//	Reselect all of the new nodes after the events have been wired.
			cloned.ForEach(x => x.Selected = true);
			pnlView.Invalidate();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DrawingScale																													*
		//*-----------------------------------------------------------------------*
		private SizeF mDrawingScale = new SizeF(1f, 1f);
		/// <summary>
		/// Get/Set the current drawing scale of this control.
		/// </summary>
		/// <remarks>
		/// Any zooming in and out are done with the DrawingScale properties and
		/// methods.
		/// Do not use the Scale method. That is reserved for DPI-independent
		/// scaling on devices and monitors.
		/// </remarks>
		public SizeF DrawingScale
		{
			get { return mDrawingScale; }
			set
			{
				mDrawingScale = value;
				QueueInvalidate();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* EditorMessage																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when an editor message is being sent to the host control.
		/// </summary>
		public event MessageEventHandler EditorMessage;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	EventsEnabled																													*
		//*-----------------------------------------------------------------------*
		private bool mEventsEnabled = true;
		/// <summary>
		/// Get/Set a value indicating whether events are enabled on this control.
		/// </summary>
		public bool EventsEnabled
		{
			get { return mEventsEnabled; }
			set
			{
				mEventsEnabled = value;
				if(value)
				{
					mNeedsInvalidate = true;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetMouseCenterAbstract																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the current abstract position of the mouse over the canvas as a
		/// decimal fraction.
		/// </summary>
		public PointF GetMouseCenterAbstract()
		{
			PointF result = new PointF(
				((float)mMouseCanvasLocation.X / (float)pnlView.Width),
				((float)mMouseCanvasLocation.Y / (float)pnlView.Height));
			return result;
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* GetSelectedNodes																											*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return a list of nodes currently selected.
		///// </summary>
		///// <returns>
		///// List of nodes currently selected. If there are no selected nodes, an
		///// empty list is returned.
		///// </returns>
		//public List<NodeItem> GetSelectedNodes()
		//{
		//	List<NodeItem> result = new List<NodeItem>();

		//	foreach(NodeItem node in mNodes)
		//	{
		//		if(node.Selected)
		//		{
		//			result.Add(node);
		//		}
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NeedsInvalidate																												*
		//*-----------------------------------------------------------------------*
		private bool mNeedsInvalidate = false;
		/// <summary>
		/// Get/Set a value indicating whether the control display needs to be
		/// invalidated.
		/// </summary>
		public bool NeedsInvalidate
		{
			get { return mNeedsInvalidate; }
			set { mNeedsInvalidate = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* NodeAdded																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a node has been added.
		/// </summary>
		public event NodeEventHandler NodeAdded;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeBackColor																													*
		//*-----------------------------------------------------------------------*
		private Color mNodeBackColor = FromHex("#5D9BEC");
		/// <summary>
		/// Get/Set the default color for node fill.
		/// </summary>
		public Color NodeBackColor
		{
			get { return mNodeBackColor; }
			set { mNodeBackColor = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* NodeDeleted																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a node has been deleted.
		/// </summary>
		public event NodeEventHandler NodeDeleted;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeFile																															*
		//*-----------------------------------------------------------------------*
		private NodeFileItem mNodeFile = NodeFileObject;
		/// <summary>
		/// Get/Set a reference to the data structure for the layout.
		/// </summary>
		public NodeFileItem NodeFile
		{
			get { return mNodeFile; }
			//set
			//{
			//	if(mNodes != null)
			//	{
			//		UnbindNodes(mNodes);
			//	}
			//	if(mResources != null)
			//	{
			//		UnbindResources(mResources);
			//	}
			//	mNodes = null;
			//	mResources = null;
			//	mNodeFile = value;
			//	if(value == null)
			//	{
			//		mNodeFile = new NodeFileItem();
			//		NodeFileObject = mNodeFile;
			//	}
			//	mNodes = mNodeFile.Nodes;
			//	BindNodes(mNodes);
			//	mResources = mNodeFile.Resources;
			//	BindResources(mResources);
			//}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeLineColor																													*
		//*-----------------------------------------------------------------------*
		private Color mNodeLineColor = FromHex("#004D95");
		/// <summary>
		/// Get/Set the default color for node outline.
		/// </summary>
		public Color NodeLineColor
		{
			get { return mNodeLineColor; }
			set { mNodeLineColor = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeLineWidth																													*
		//*-----------------------------------------------------------------------*
		private int mNodeLineWidth = 4;
		/// <summary>
		/// Get/Set the node line width.
		/// </summary>
		public int NodeLineWidth
		{
			get { return mNodeLineWidth; }
			set { mNodeLineWidth = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeMaxWidth																													*
		//*-----------------------------------------------------------------------*
		private int mNodeMaxWidth = 256;
		/// <summary>
		/// Get/Set the maximum width of the node.
		/// </summary>
		public int NodeMaxWidth
		{
			get { return mNodeMaxWidth; }
			set { mNodeMaxWidth = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* NodeMoved																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the node has been moved.
		/// </summary>
		public event NodeEventHandler NodeMoved;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* NodeMoving																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the node is going to be moved.
		/// </summary>
		public event NodeEventHandler NodeMoving;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* NodePropertyChanged																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a property value has been changed on a node.
		/// </summary>
		public event NodePropertyChangeEventHandler NodePropertyChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Nodes																																	*
		//*-----------------------------------------------------------------------*
		private NodeCollection mNodes = NodeFileObject.Nodes;
		/// <summary>
		/// Get a reference to the collection of nodes on this canvas.
		/// </summary>
		public NodeCollection Nodes
		{
			get { return mNodes; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeSelectColor																												*
		//*-----------------------------------------------------------------------*
		private Color mNodeSelectColor = FromHex("#F9AD18");
		/// <summary>
		/// Get/Set the default color for node outline.
		/// </summary>
		public Color NodeSelectColor
		{
			get { return mNodeSelectColor; }
			set { mNodeSelectColor = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	QueueViewCenter																												*
		//*-----------------------------------------------------------------------*
		private PointF mViewCenterFraction = PointF.Empty;
		/// <summary>
		/// Queue a view center decimal fraction coordinate to center on next time
		/// the view is stable.
		/// </summary>
		/// <param name="centerPoint">
		/// Logical point on the canvas to center in the view.
		/// </param>
		public void QueueViewCenter(PointF centerFraction)
		{
			mViewCenterFraction = centerFraction;
			QueueInvalidate();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Resources																															*
		//*-----------------------------------------------------------------------*
		private ResourceCollection mResources = NodeFileObject.Resources;
		/// <summary>
		/// Get a reference to the collection of resources in the loaded layout.
		/// </summary>
		public ResourceCollection Resources
		{
			get { return mResources; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SelectionChanged																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the Selected property of a node has changed.
		/// </summary>
		public event NodeEventHandler SelectionChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SelectionQueue																												*
		//*-----------------------------------------------------------------------*
		private List<NodeItem> mSelectionQueue = new List<NodeItem>();
		/// <summary>
		/// Get a reference to the ordered queue of selected items.
		/// </summary>
		/// <remarks>
		/// Item[0] is the first item currently selected.
		/// </remarks>
		public List<NodeItem> SelectionQueue
		{
			get { return mSelectionQueue; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SetDrawingScale																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Scale the drawing evenly on both axis.
		/// </summary>
		/// <param name="scale">
		/// </param>
		public void SetDrawingScale(float scale)
		{
			mDrawingScale = new SizeF(scale, scale);
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Scale the drawing on separate factors per axis.
		/// </summary>
		/// <param name="xScale">
		/// Horizontal scale.
		/// </param>
		/// <param name="yScale">
		/// Vertical scale.
		/// </param>
		public void SetDrawingScale(float xScale, float yScale)
		{
			mDrawingScale = new SizeF(xScale, yScale);
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Scale the drawing on separate factors per axis.
		/// </summary>
		/// <param name="scale">
		/// Scaling factor.
		/// </param>
		public void SetDrawingScale(SizeF scale)
		{
			mDrawingScale = scale;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SocketAdded																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a socket has been added to a node.
		/// </summary>
		public event SocketEventHandler SocketAdded;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SocketConnectionAdded																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a connection has been added to a node socket.
		/// </summary>
		public event SocketConnectionEventHandler SocketConnectionAdded;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SocketConnectionColor																									*
		//*-----------------------------------------------------------------------*
		private Color mSocketConnectionColor = Color.White;
		/// <summary>
		/// Get/Set the color of socket to socket connections.
		/// </summary>
		public Color SocketConnectionColor
		{
			get { return mSocketConnectionColor; }
			set { mSocketConnectionColor = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SocketConnectionDeleted																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a connection has been deleted from a node socket.
		/// </summary>
		public event SocketConnectionEventHandler SocketConnectionDeleted;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SocketConnectionLineWidth																							*
		//*-----------------------------------------------------------------------*
		private int mSocketConnectionLineWidth = 4;
		/// <summary>
		/// Get/Set the line width of connection lines between sockets.
		/// </summary>
		public int SocketConnectionLineWidth
		{
			get { return mSocketConnectionLineWidth; }
			set { mSocketConnectionLineWidth = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SocketDeleted																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a socket has been deleted from the node.
		/// </summary>
		public event SocketEventHandler SocketDeleted;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SocketDragColor																												*
		//*-----------------------------------------------------------------------*
		private Color mSocketDragColor = Color.White;
		/// <summary>
		/// Get/Set the color of in-drag socket candidate lines.
		/// </summary>
		public Color SocketDragColor
		{
			get { return mSocketDragColor; }
			set { mSocketDragColor = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SocketFontSize																												*
		//*-----------------------------------------------------------------------*
		private float mSocketFontSize = 8f;
		/// <summary>
		/// Get/Set the font size of the socket, in points.
		/// </summary>
		public float SocketFontSize
		{
			get { return mSocketFontSize; }
			set { mSocketFontSize = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SocketPropertyChanged																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when there has been a change on a node socket property.
		/// </summary>
		public event SocketPropertyChangeEventHandler SocketPropertyChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SocketTextColor																												*
		//*-----------------------------------------------------------------------*
		private Color mSocketTextColor = Color.White;
		/// <summary>
		/// Get/Set the color of socket text.
		/// </summary>
		public Color SocketTextColor
		{
			get { return mSocketTextColor; }
			set { mSocketTextColor = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	TitleFontSize																													*
		//*-----------------------------------------------------------------------*
		private float mTitleFontSize = 8f;
		/// <summary>
		/// Get/Set the size of the node title text, in points.
		/// </summary>
		public float TitleFontSize
		{
			get { return mTitleFontSize; }
			set { mTitleFontSize = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	TitleTextColor																												*
		//*-----------------------------------------------------------------------*
		private Color mTitleTextColor = Color.White;
		/// <summary>
		/// Get/Set the text color of the title.
		/// </summary>
		public Color TitleTextColor
		{
			get { return mTitleTextColor; }
			set { mTitleTextColor = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*


}
