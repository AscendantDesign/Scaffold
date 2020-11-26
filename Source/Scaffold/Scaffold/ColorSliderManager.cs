//	ColorSliderManager.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

using static Scaffold.ScaffoldUtil;
using static Scaffold.ScaffoldNodesUtil;

using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Xml.Linq;
using System.Reflection;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	ColorSliderManagerCollection																						*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of ColorSliderManagerItem Items.
	/// </summary>
	public class ColorSliderManagerCollection : List<ColorSliderManagerItem>
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//private ColorToken mColorToken = new ColorToken();

		//*-----------------------------------------------------------------------*
		//* item_ValueChanged																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A value has changed on an item.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void item_ValueChanged(object sender, EventArgs e)
		{
			ColorSliderManagerItem item = (ColorSliderManagerItem)sender;

			switch(item.Channel)
			{
				case ColorSliderChannelEnum.Alpha:
					mColorToken.ModifyChannel(alpha: item.Value);
					break;
				case ColorSliderChannelEnum.Blue:
					mColorToken.ModifyChannel(blue: item.Value);
					break;
				case ColorSliderChannelEnum.Green:
					mColorToken.ModifyChannel(green: item.Value);
					break;
				case ColorSliderChannelEnum.Hue:
					mColorToken.ModifyChannel(hue: ((float)item.Value / 240f) * 360f);
					break;
				case ColorSliderChannelEnum.Luminance:
					mColorToken.ModifyChannel(luminance: (float)item.Value / 240f);
					break;
				case ColorSliderChannelEnum.Red:
					mColorToken.ModifyChannel(red: item.Value);
					break;
				case ColorSliderChannelEnum.Saturation:
					mColorToken.ModifyChannel(saturation: (float)item.Value / 240f);
					break;
			}
			OnValueChanged(item, e);
			//int ca = 0;
			//int cb = 0;
			//int cg = 0;
			//int ch = 0;
			//int cl = 0;
			//int cr = 0;
			//int cs = 0;
			//ColorSliderManagerItem item = (ColorSliderManagerItem)sender;
			//int va = 0;
			//int vb = 0;

			//if(item.Mode == ColorSliderModeEnum.RGB ||
			//	item.Channel == ColorSliderChannelEnum.Alpha)
			//{
			//	cr = mColor.R;
			//	cg = mColor.G;
			//	cb = mColor.B;
			//	ca = mColor.A;
			//	switch(item.Channel)
			//	{
			//		case ColorSliderChannelEnum.Red:
			//			cr = item.Value;
			//			break;
			//		case ColorSliderChannelEnum.Green:
			//			cg = item.Value;
			//			break;
			//		case ColorSliderChannelEnum.Blue:
			//			cb = item.Value;
			//			break;
			//		case ColorSliderChannelEnum.Alpha:
			//			ca = item.Value;
			//			break;
			//	}
			//	Color = Color.FromArgb(ca, cr, cg, cb);
			//}
			//else if(item.Mode == ColorSliderModeEnum.HSL)
			//{
			//	switch(item.Channel)
			//	{
			//		case ColorSliderChannelEnum.Hue:
			//			mColorToken.
			//				ModifyChannel(hue: ((float)item.Value / 240f) * 360f);
			//			break;
			//		case ColorSliderChannelEnum.Saturation:
			//			//cs = item.Value;
			//			//if(cl == 0)
			//			//{
			//			//	cl = cs;
			//			//}
			//			mColorToken.ModifyChannel(saturation: (float)item.Value / 240f);
			//			break;
			//		case ColorSliderChannelEnum.Luminance:
			//			//cl = item.Value;
			//			//if(cs == 0)
			//			//{
			//			//	cs = cl;
			//			//}
			//			mColorToken.ModifyChannel(luminance: (float)item.Value / 240f);
			//			break;
			//	}
			//	//Debug.WriteLine($"Set HSL: {ch:0.0} {cs:0.000} {cl:0.000}");
			//	//vb = ch;
			//	////Color = FromHSL(ch, cs, cl);
			//	//va = (int)((mColor.GetHue() / 360f) * 240f);
			//}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnColorChanged																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the ColorChanged event when color has changed on this item.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Color event arguments.
		/// </param>
		protected virtual void OnColorChanged(object sender, ColorEventArgs e)
		{
			bool cb = mColorBusy;
			if(!cb)
			{
				mColorBusy = true;
			}
			ColorChanged?.Invoke(sender, e);
			if(!cb)
			{
				mColorBusy = false;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnFocusReceived																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the FocusReceived event when a control has received the logical
		/// focus.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected virtual void OnFocusReceived(object sender, EventArgs e)
		{
			FocusReceived?.Invoke(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnValueChanged																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the ValueChanged event when an item's value has been changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected virtual void OnValueChanged(object sender, EventArgs e)
		{
			ValueChanged?.Invoke(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the ColorSliderManagerCollection Item.
		/// </summary>
		public ColorSliderManagerCollection()
		{
			mColorToken.ColorChange += OnColorChanged;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Add																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add an item to the collection by member values.
		/// </summary>
		/// <param name="panel">
		/// Reference to the panel control to add.
		/// </param>
		/// <param name="channel">
		/// Color channel assigned to the slider.
		/// </param>
		/// <param name="mode">
		/// </param>
		/// <returns>
		/// Newly created and added color slider manager item.
		/// </returns>
		public ColorSliderManagerItem Add(Panel panel,
			ColorSliderChannelEnum channel, ColorSliderModeEnum mode)
		{
			ColorSliderManagerItem result = new ColorSliderManagerItem();

			result.Parent = this;
			result.Channel = channel;
			result.Mode = mode;
			this.Add(result);
			//	Only set the control reference after the parent and the collection
			//	to support full iterative link-up when reference is made.
			result.PanelControl = panel;
			result.UpdateValue(mColorToken);
			result.ValueChanged += item_ValueChanged;
			result.FocusReceived += OnFocusReceived;
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Color																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the current selected color.
		/// </summary>
		public System.Drawing.Color Color
		{
			get { return mColorToken.Color; }
			set
			{
				System.Drawing.Color original = mColorToken.Color;
				mColorToken.Color = value;
				if(!mColorBusy)
				{
					mColorBusy = true;
					OnColorChanged(this,
						new ColorEventArgs(original, mColorToken.Color));
					mColorBusy = false;
				}
				foreach(ColorSliderManagerItem slider in this)
				{
					slider.UpdateValue(mColorToken);
				}
				//mColorToken.Color = mColor;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ColorBusy																															*
		//*-----------------------------------------------------------------------*
		private bool mColorBusy = false;
		/// <summary>
		/// Get/Set a value indicating whether the currently selected color is
		/// busy.
		/// </summary>
		public bool ColorBusy
		{
			get { return mColorBusy; }
			set { mColorBusy = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ColorChanged																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the color is being set to a new value.
		/// </summary>
		public event ColorEventHandler ColorChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ColorToken																														*
		//*-----------------------------------------------------------------------*
		private Scaffold.ColorToken mColorToken = new Scaffold.ColorToken();
		/// <summary>
		/// Get a reference to the color token handler for this instance.
		/// </summary>
		public Scaffold.ColorToken ColorToken
		{
			get { return mColorToken; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* FocusReceived																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a slider item has received logical focus.
		/// </summary>
		/// <remarks>
		/// In this version, none of the slider controls are physically focusable.
		/// This event is fired only to notify the host form that all other native
		/// controls have gone out of focus.
		/// </remarks>
		public event EventHandler FocusReceived;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Invalidate																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Invalidate the sliders in this collection.
		/// </summary>
		public void Invalidate()
		{
			foreach(ColorSliderManagerItem item in this)
			{
				item.Invalidate();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SetMode																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the color mode of each of the handlers in the collection.
		/// </summary>
		/// <param name="mode">
		/// </param>
		/// <remarks>
		/// The intention of this method is to switch all of the individual values
		/// and associated displays without changing the color.
		/// </remarks>
		public void SetMode(ColorSliderModeEnum mode)
		{
			int b = 0;
			int g = 0;
			int h = 0;
			int l = 0;
			int r = 0;
			int s = 0;

			mColorBusy = true;
			mColorToken.ColorMode = mode;
			foreach(ColorSliderManagerItem item in this)
			{
				switch(mode)
				{
					case ColorSliderModeEnum.HSL:
						h = (int)((mColorToken.Hue / 360f) * 240f);
						s = (int)(mColorToken.Saturation * 240f);
						l = (int)(mColorToken.Luminance * 240f);
						switch(item.Mode)
						{
							case ColorSliderModeEnum.RGB:
								//	To HSL from RGB.
								item.ValueBusy = true;
								switch(item.Channel)
								{
									case ColorSliderChannelEnum.Red:
										//	Red switches to Hue.
										item.Channel = ColorSliderChannelEnum.Hue;
										item.Value = h;
										break;
									case ColorSliderChannelEnum.Green:
										//	Green switches to Saturation.
										item.Channel = ColorSliderChannelEnum.Saturation;
										item.Value = s;
										break;
									case ColorSliderChannelEnum.Blue:
										//	Blue switches to Luminance.
										item.Channel = ColorSliderChannelEnum.Luminance;
										item.Value = l;
										break;
								}
								item.ValueBusy = false;
								break;
						}
						break;
					case ColorSliderModeEnum.RGB:
						r = mColorToken.Red;
						g = mColorToken.Green;
						b = mColorToken.Blue;
						switch(item.Mode)
						{
							case ColorSliderModeEnum.HSL:
								//	To RGB from HSL.
								item.ValueBusy = true;
								switch(item.Channel)
								{
									case ColorSliderChannelEnum.Hue:
										//	Hue switches to Red.
										item.Channel = ColorSliderChannelEnum.Red;
										item.Value = r;
										break;
									case ColorSliderChannelEnum.Saturation:
										//	Saturation switches to Green.
										item.Channel = ColorSliderChannelEnum.Green;
										item.Value = g;
										break;
									case ColorSliderChannelEnum.Luminance:
										//	Luminance switches to Blue.
										item.Channel = ColorSliderChannelEnum.Blue;
										item.Value = b;
										break;
								}
								item.ValueBusy = false;
								break;
						}
						break;
				}
				item.Mode = mode;
				item.Invalidate();
			}
			mColorBusy = false;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SetValue																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the value of the specified channel.
		/// </summary>
		/// <param name="channel">
		/// Channel to update.
		/// </param>
		/// <param name="value">
		/// Value to set on the channel.
		/// </param>
		/// <param name="allowEvent">
		/// Value indicating whether to allow the ValueChanged event to fire.
		/// </param>
		public void SetValue(ColorSliderChannelEnum channel, int value,
			bool allowEvent = true)
		{
			foreach(ColorSliderManagerItem slider in this)
			{
				if(slider.Channel == channel)
				{
					if(!allowEvent)
					{
						slider.ValueBusy = true;
					}
					slider.Value = value;
					if(!allowEvent)
					{
						slider.ValueBusy = false;
					}
					break;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ValueChanged																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a value has changed on one of the sliders.
		/// </summary>
		public event EventHandler ValueChanged;
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	ColorSliderManagerItem																									*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Tracker and handler for internally drawn color slider.
	/// </summary>
	public class ColorSliderManagerItem
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* mPanelControl_MouseClick																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Mouse has been clicked on the color channel panel.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void mPanelControl_MouseClick(object sender, MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left)
			{
				//	Left button down, user setting the pointer.
				ProcessMouseInput(e);
				OnFocusReceived(this, new EventArgs());
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mPanelControl_MouseMove																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse is moving over the assigned panel.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void mPanelControl_MouseMove(object sender, MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left)
			{
				//	Left button down, user dragging the pointer.
				ProcessMouseInput(e);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mPanelControl_Paint																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The channel panel is being painted.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Paint event arguments.
		/// </param>
		private void mPanelControl_Paint(object sender, PaintEventArgs e)
		{
			//	Full control = Bounds.
			//	Swatch = 0, 8 to Right - 16, Bottom - 8
			//	Pointer = Right - 28, 0
			//		[0, 8], [12, 16], [28, 16], [28, 0], [12, 0]
			int bottom = 0;
			int ca = 0;
			int cb = 0;
			int cg = 0;
			int ch = 0;
			int cl = 0;
			int cr = 0;
			int cs = 0;
			Brush brush = null;
			float chunkH = (float)mPanelControl.Height / 256f;
			Color color = mParent.ColorToken.Color;
			bool drawp = false;
			Graphics g = e.Graphics;
			PointF[] indicator = null;
			Matrix matrix = new Matrix();
			Rectangle panelRect =
				new Rectangle(0, 0, mPanelControl.Width, mPanelControl.Height);
			Pen pen = null;
			int right = 0;
			Rectangle swatchRect =
				new Rectangle(0, 8,
					mPanelControl.Width - 16, mPanelControl.Height - 16);
			Rectangle work = Rectangle.Empty;
			RectangleF workF = RectangleF.Empty;
			float y = 0;

			//	Indicator and position setup.
			//if(mChannel == ColorSliderChannelEnum.Hue)
			//{
			//	Debug.WriteLine("Break here...");
			//}
			indicator = new PointF[]
			{
				new PointF(panelRect.Right - 28, 8),
				new PointF(panelRect.Right - 28 + 12, 15),
				new PointF(panelRect.Right - 2, 15),
				new PointF(panelRect.Right - 2, 0),
				new PointF(panelRect.Right - 28 + 12, 0)
			};
			if(mMode == ColorSliderModeEnum.RGB ||
				mChannel == ColorSliderChannelEnum.Alpha)
			{
				if(mChannel == ColorSliderChannelEnum.Alpha)
				{
					Debug.WriteLine($"Alpha: {mValue}");
				}
				matrix.Translate(0f,
					(float)swatchRect.Height -
					((mValue / 256f) * (float)swatchRect.Height),
					MatrixOrder.Append);
			}
			else if(mMode == ColorSliderModeEnum.HSL)
			{
				matrix.Translate(0f,
					(float)swatchRect.Height -
					((mValue / 240f) * (float)swatchRect.Height),
					MatrixOrder.Append);
			}
			matrix.TransformPoints(indicator);

			g.CompositingMode = CompositingMode.SourceOver;
			g.CompositingQuality = CompositingQuality.HighSpeed;
			g.PixelOffsetMode = PixelOffsetMode.None;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
			g.InterpolationMode =
				System.Drawing.Drawing2D.InterpolationMode.Default;

			//	Clear the area.
			brush = new SolidBrush(this.mPanelControl.Parent.BackColor);
			//brush = new SolidBrush(Color.Black);
			g.FillRectangle(brush, panelRect);
			//	Load the Alpha background.
			if(mChannel == ColorSliderChannelEnum.Alpha)
			{
				//	Draw the background reference patterns.
				work = new Rectangle(swatchRect.X, swatchRect.Y,
					swatchRect.Width / 4, swatchRect.Height);
				pen = new Pen(Color.Black, 2f);
				g.DrawRectangle(pen, swatchRect);
				brush = new SolidBrush(Color.White);
				g.FillRectangle(brush, work);
				work.X += swatchRect.Width / 4;
				brush = new SolidBrush(FromHex("#A0A0A0FF"));
				g.FillRectangle(brush, work);
				work.X += swatchRect.Width / 4;
				brush = new SolidBrush(FromHex("#5F5F5FFF"));
				g.FillRectangle(brush, work);
				work.X += swatchRect.Width / 4;
				brush = new SolidBrush(FromHex("#202020FF"));
				g.FillRectangle(brush, work);
			}
			//	Draw the channel swatch.
			//	When the swatch is shorter than 256,
			//	calculate the level of gradient per pixel.
			//	When the swatch is longer than 256,
			//	calculate the height of each gradient in the area.
			drawp =
				(mMode == ColorSliderModeEnum.RGB && swatchRect.Height < 256) ||
				(mMode == ColorSliderModeEnum.HSL && swatchRect.Height < 240);
			ca = color.A;
			cr = color.R;
			cg = color.G;
			cb = color.B;
			ch = (int)((color.GetHue() / 360f) * 240f);
			cs = (int)(color.GetSaturation() * 240f);
			cl = (int)(color.GetBrightness() * 240f);
			if(drawp)
			{
				//	Draw each pixel from top to bottom.
				bottom = swatchRect.Bottom;
				right = swatchRect.Right;
				switch(mChannel)
				{
					case ColorSliderChannelEnum.Alpha:
						for(y = swatchRect.Y; y < bottom; y++)
						{
							ca = 255 - (int)((y / (float)bottom) * 256f);
							pen = new Pen(Color.FromArgb(ca, cr, cg, cb), 1f);
							g.DrawLine(pen, 0, y, right, y);
						}
						break;
					case ColorSliderChannelEnum.Red:
						for(y = swatchRect.Y; y < bottom; y ++)
						{
							cr = 255 - (int)((y / (float)bottom) * 256f);
							pen = new Pen(Color.FromArgb(255, cr, cg, cb), 1f);
							g.DrawLine(pen, 0, y, right, y);
						}
						break;
					case ColorSliderChannelEnum.Green:
						for(y = swatchRect.Y; y < bottom; y++)
						{
							cg = 255 - (int)((y / (float)bottom) * 256f);
							pen = new Pen(Color.FromArgb(255, cr, cg, cb), 1f);
							g.DrawLine(pen, 0, y, right, y);
						}
						break;
					case ColorSliderChannelEnum.Blue:
						for(y = swatchRect.Y; y < bottom; y++)
						{
							cb = 255 - (int)((y / (float)bottom) * 256f);
							pen = new Pen(Color.FromArgb(255, cr, cg, cb), 1f);
							g.DrawLine(pen, 0, y, right, y);
						}
						break;
					case ColorSliderChannelEnum.Hue:
						for(y = swatchRect.Y; y < bottom; y++)
						{
							ch = 240 - (int)((y / (float)bottom) * 240f);
							//	In this version, draw all of the hues at full blast.
							//pen = new Pen(FromHSL(ch, cs, cl), 1f);
							pen = new Pen(FromHSL(ch, 240, 120), 1f);
							g.DrawLine(pen, 0, y, right, y);
						}
						break;
					case ColorSliderChannelEnum.Saturation:
						for(y = swatchRect.Y; y < bottom; y++)
						{
							cs = 240 - (int)((y / (float)bottom) * 240f);
							pen = new Pen(FromHSL(ch, cs, cl), 1f);
							g.DrawLine(pen, 0, y, right, y);
						}
						break;
					case ColorSliderChannelEnum.Luminance:
						for(y = swatchRect.Y; y < bottom; y++)
						{
							cl = 240 - (int)((y / (float)bottom) * 240f);
							pen = new Pen(FromHSL(ch, cs, cl), 1f);
							g.DrawLine(pen, 0, y, right, y);
						}
						break;
				}
			}
			else
			{
				//	Draw each range from top to bottom.
				switch(mChannel)
				{
					case ColorSliderChannelEnum.Alpha:
						break;
					case ColorSliderChannelEnum.Red:
						break;
					case ColorSliderChannelEnum.Green:
						break;
					case ColorSliderChannelEnum.Blue:
						break;
					case ColorSliderChannelEnum.Hue:
						break;
					case ColorSliderChannelEnum.Saturation:
						break;
					case ColorSliderChannelEnum.Luminance:
						break;
				}
			}
			//	Draw the value indicator.
			brush = Brushes.White;
			pen = Pens.Black;
			g.FillPolygon(brush, indicator);
			g.DrawPolygon(pen, indicator);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ProcessMouseInput																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Process current input from the user's mouse.
		/// </summary>
		/// <param name="e">
		/// Mouse event arguments.
		/// </param>
		private void ProcessMouseInput(MouseEventArgs e)
		{
			float swatchHeight = (float)mPanelControl.Height - 16f;
			float swatchPosition =
				1.0f - (((float)e.Y - 8f) / swatchHeight);

			if(swatchPosition < 0f)
			{
				swatchPosition = 0f;
			}
			if(swatchPosition > 1.0f)
			{
				swatchPosition = 1.0f;
			}
			if(mMode == ColorSliderModeEnum.RGB ||
				mChannel == ColorSliderChannelEnum.Alpha)
			{
				Value = (int)(255f * swatchPosition);
			}
			else if(mMode == ColorSliderModeEnum.HSL)
			{
				Value = (int)(240f * swatchPosition);
			}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnFocusReceived																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the FocusReceived event when a control has received the logical
		/// focus.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected virtual void OnFocusReceived(object sender, EventArgs e)
		{
			FocusReceived?.Invoke(sender, e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnValueChanged																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the ValueChanged event when the value of this channel has
		/// been changed.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected virtual void OnValueChanged(EventArgs e)
		{
			ValueChanged?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	Channel																																*
		//*-----------------------------------------------------------------------*
		private ColorSliderChannelEnum mChannel = ColorSliderChannelEnum.None;
		/// <summary>
		/// Get/Set the channel being operated by this instance.
		/// </summary>
		public ColorSliderChannelEnum Channel
		{
			get { return mChannel; }
			set { mChannel = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* FocusReceived																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when a slider item has received logical focus.
		/// </summary>
		/// <remarks>
		/// In this version, none of the slider controls are physically focusable.
		/// This event is fired only to notify the host form that all other native
		/// controls have gone out of focus.
		/// </remarks>
		public event EventHandler FocusReceived;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Invalidate																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Invalidate the host control.
		/// </summary>
		public void Invalidate()
		{
			if(mPanelControl != null)
			{
				mPanelControl.Invalidate();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Mode																																	*
		//*-----------------------------------------------------------------------*
		private ColorSliderModeEnum mMode = ColorSliderModeEnum.None;
		/// <summary>
		/// Get/Set the color calculation mode currently being handled by this
		/// channel.
		/// </summary>
		public ColorSliderModeEnum Mode
		{
			get { return mMode; }
			set { mMode = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PanelControl																													*
		//*-----------------------------------------------------------------------*
		private Panel mPanelControl = null;
		/// <summary>
		/// Get/Set the reference to the panel control.
		/// </summary>
		public Panel PanelControl
		{
			get { return mPanelControl; }
			set
			{
				if(mPanelControl != null)
				{
					//	Remove event connections.
					mPanelControl.MouseClick -= mPanelControl_MouseClick;
					mPanelControl.MouseMove -= mPanelControl_MouseMove;
					mPanelControl.Paint -= mPanelControl_Paint;
				}
				mPanelControl = value;
				if(mPanelControl != null)
				{
					mPanelControl.MouseClick += mPanelControl_MouseClick;
					mPanelControl.MouseMove += mPanelControl_MouseMove;
					mPanelControl.Paint += mPanelControl_Paint;
					typeof(Panel).InvokeMember("DoubleBuffered",
						BindingFlags.SetProperty |
						BindingFlags.Instance | BindingFlags.NonPublic, null,
						mPanelControl, new object[] { true });
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Parent																																*
		//*-----------------------------------------------------------------------*
		private ColorSliderManagerCollection mParent = null;
		/// <summary>
		/// Get/Set a reference to the collection to which this item belongs.
		/// </summary>
		public ColorSliderManagerCollection Parent
		{
			get { return mParent; }
			set { mParent = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	UpdateValue																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the value of this channel to match the currently selected color.
		/// </summary>
		/// <param name="token">
		/// Color token controlling the current color scheme.
		/// </param>
		public void UpdateValue(ColorToken token)
		{
			mValueBusy = true;
			//	RGB = 0 to 255.
			//	HSL = 0 to 240.
			switch(mChannel)
			{
				case ColorSliderChannelEnum.Alpha:
					mValue = token.Alpha;
					break;
				case ColorSliderChannelEnum.Red:
					mValue = token.Red;
					break;
				case ColorSliderChannelEnum.Green:
					mValue = token.Green;
					break;
				case ColorSliderChannelEnum.Blue:
					mValue = token.Blue;
					break;
				case ColorSliderChannelEnum.Hue:
					mValue = (int)((token.Hue / 360f) * 240f);
					break;
				case ColorSliderChannelEnum.Saturation:
					mValue = (int)(token.Saturation * 240f);
					break;
				case ColorSliderChannelEnum.Luminance:
					mValue = (int)(token.Luminance * 240f);
					break;
			}
			mValueBusy = false;
			Invalidate();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Value																																	*
		//*-----------------------------------------------------------------------*
		private int mValue = 0;
		/// <summary>
		/// Get/Set the current value of this color.
		/// </summary>
		public int Value
		{
			get { return mValue; }
			set
			{
				mValue = value;
				if(!mValueBusy)
				{
					mValueBusy = true;
					OnValueChanged(new EventArgs());
					Invalidate();
					mValueBusy = false;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ValueBusy																															*
		//*-----------------------------------------------------------------------*
		private bool mValueBusy = false;
		/// <summary>
		/// Get/Set a value indicating whether the Value property is currently
		/// being updated by the system.
		/// </summary>
		public bool ValueBusy
		{
			get { return mValueBusy; }
			set { mValueBusy = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ValueChanged																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the value property has been changed.
		/// </summary>
		public event EventHandler ValueChanged;
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
