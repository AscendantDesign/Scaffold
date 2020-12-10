//	ScaffoldUtil.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Scaffold.ScaffoldNodesUtil;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	ScaffoldUtil																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Utilities and functionality for the scaffold application.
	/// </summary>
	public class ScaffoldUtil
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		[DllImport("shlwapi.dll")]
		private static extern int ColorHLSToRGB(int H, int L, int S);

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* AddMediaListItems																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Populate the specified media list with representations of the media
		/// properties in the collection.
		/// </summary>
		/// <param name="listControl">
		/// Reference to the target list view control.
		/// </param>
		/// <param name="imageControl">
		/// Reference to the associated icons and thumbnails image list.
		/// </param>
		/// <param name="resources">
		/// Collection of resources providing the media.
		/// </param>
		/// <param name="properties">
		/// Collection of properties to inspect for media entries.
		/// </param>
		public static async void AddMediaListItems(ListView listControl,
			ImageList imageControl, ResourceCollection resources,
			PropertyCollection properties)
		{
			int index = 0;
			ListViewItem item = null;
			ResourceItem resource = null;
			Bitmap thumbnail = null;

			if(MediaExists(properties))
			{
				resource = GetResource(properties, "MediaAudio");
				if(resource != null)
				{
					item = new ListViewItem(ResourceItem.Filename(resource), 0);
					item.Text = GetFilename(resource.AbsoluteFilename);
					item.Tag = resource.Ticket;
					item.Group = listControl.Groups["Audio"];
					listControl.Items.Add(item);
				}
				resource = GetResource(properties, "MediaImage");
				if(resource != null)
				{
					thumbnail = CreateImageThumbnail(resource, 128, 128);
					index = imageControl.Images.Count;
					imageControl.Images.Add(thumbnail);
					item = new ListViewItem(ResourceItem.Filename(resource),
						index);
					item.Tag = resource.Ticket;
					item.Group = listControl.Groups["Image"];
					listControl.Items.Add(item);
				}
				resource = GetResource(properties, "MediaLink");
				if(resource != null)
				{
					item = new ListViewItem(ResourceItem.Filename(resource), 1);
					item.Text = resource.Uri;
					item.Tag = resource.Ticket;
					item.Group = listControl.Groups["Link"];
					listControl.Items.Add(item);
				}
				resource = GetResource(properties, "MediaVideo");
				if(resource != null)
				{
					thumbnail = await CreateVideoThumbnail(resource, 128, 128);
					index = imageControl.Images.Count;
					imageControl.Images.Add(thumbnail);
					item = new ListViewItem(ResourceItem.Filename(resource),
						index);
					item.Tag = resource.Ticket;
					item.Group = listControl.Groups["Video"];
					listControl.Items.Add(item);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	CenterOver																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Center the top form over the base form.
		/// </summary>
		/// <param name="baseForm">
		/// Reference to the form whose reference location will be used.
		/// </param>
		/// <param name="topForm">
		/// Reference to the form being placed in front of the other.
		/// </param>
		/// <returns>
		/// Reference to the starting location of the form to display.
		/// </returns>
		public static Point CenterOver(Form baseForm, Form topForm)
		{
			Point center = Point.Empty;
			Point result = Point.Empty;

			if(baseForm != null && topForm != null)
			{
				//	Both forms have dimensions.
				result = new Point(
					(baseForm.Width / 2),
					(baseForm.Height / 2));
				result = new Point(
					result.X - (topForm.Width / 2),
					result.Y - (topForm.Height / 2));
				result = baseForm.PointToScreen(result);
				topForm.StartPosition = FormStartPosition.Manual;
				if(result.X < 0)
				{
					result.X = 0;
				}
				if(result.Y < 0)
				{
					result.Y = 0;
				}
				if(result.X + topForm.Width > Screen.PrimaryScreen.Bounds.Width)
				{
					result.X = Screen.PrimaryScreen.Bounds.Width - topForm.Width;
				}
				if(result.Y + topForm.Height > Screen.PrimaryScreen.Bounds.Height)
				{
					result.Y = Screen.PrimaryScreen.Bounds.Height - topForm.Height;
				}
				topForm.Location = result;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ClipboardLoadFromResource																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Load the clipboard from an onboard binary resource.
		/// </summary>
		/// <param name="resourceName">
		/// Name of the resource to load.
		/// </param>
		/// <returns>
		/// True if the clipboard was loaded. Otherwise, false.
		/// </returns>
		public static bool ClipboardLoadFromResource(string resourceName)
		{
			byte[] buffer = new byte[0];
			DataObject dataobject = null;
			BinaryFormatter formatter = null;
			MemoryStream memory = null;
			NamedObjectCollection pages = null;
			PropertyInfo property = null;
			bool result = false;
			Type type = typeof(ResourceMain);

			property = type.GetProperty(resourceName,
				BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
			try
			{
				buffer = (byte[])property.GetValue(null, null);
				if(buffer.Length > 0)
				{
					try
					{
						memory = new MemoryStream(buffer);
						formatter = new BinaryFormatter();
						pages = (NamedObjectCollection)formatter.Deserialize(memory);
					}
					catch(Exception ex)
					{
						Console.WriteLine($"Could not deserialize clipboard: {ex.Message}",
							"Load Clipboard File");
					}
					finally
					{
						memory.Close();
						memory.Dispose();
					}
				}
				//	Prepare the clipboard.
				if(pages.Count > 0)
				{
					Clipboard.Clear();
					dataobject = new DataObject();
					foreach(NamedObjectItem page in pages)
					{
						dataobject.SetData(page.Name, false, page.Value);
					}
					Clipboard.SetDataObject(dataobject, true);
				}
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* DeScaleDrawing																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Scale the coordinate to the specified factor and return the new
		/// coordinate to the caller.
		/// </summary>
		/// <param name="origin">
		/// Original point to scale.
		/// </param>
		/// <param name="scale">
		/// Scale factor.
		/// </param>
		/// <returns>
		/// Scaled coordinate.
		/// </returns>
		public static PointF DeScaleDrawing(Point origin, SizeF scale,
			int scrollHorizontal = 0, int scrollVertical = 0)
		{
			PointF orig = new PointF((float)origin.X, (float)origin.Y);
			PointF result = new PointF(0f, 0f);

			if(scale.Width != 0f)
			{
				result.X =
					((orig.X * scale.Width) + (float)scrollHorizontal);
			}
			if(scale.Height != 0f)
			{
				result.Y =
					((orig.Y * scale.Height) + (float)scrollVertical);
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Scale the rectangle to the specified factor and return the new area to
		/// the caller.
		/// </summary>
		/// <param name="rectangle">
		/// Rectangle to inspect.
		/// </param>
		/// <param name="scale">
		/// X and Y scale factors.
		/// </param>
		/// <returns>
		/// New rectangle as a result of the original rectangle scaled by the
		/// specified factors.
		/// </returns>
		public static RectangleF DeScaleDrawing(RectangleF rectangle, SizeF scale)
		{
			RectangleF result = new RectangleF(
				rectangle.X / scale.Width,
				rectangle.Y / scale.Height,
				rectangle.Width / scale.Width,
				rectangle.Height / scale.Height);
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DrawRoundedRectangle																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Draw the outline of a rounded rectangle.
		/// </summary>
		/// <param name="graphics">
		/// Reference to the active graphics context.
		/// </param>
		/// <param name="pen">
		/// Reference to the active pen.
		/// </param>
		/// <param name="bounds">
		/// Outer bounds of the rectangle.
		/// </param>
		/// <param name="cornerRadius">
		/// Corner rounding radius.
		/// </param>
		public static void DrawRoundedRectangle(Graphics graphics, Pen pen,
			Rectangle bounds, int cornerRadius)
		{
			if(graphics != null && pen != null)
			{
				using(GraphicsPath path = RoundedRectangle(bounds, cornerRadius))
				{
					graphics.DrawPath(pen, path);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	FillRoundedRectangle																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fill the shape of a rounded rectangle.
		/// </summary>
		/// <param name="graphics">
		/// Reference to the active graphics context to use.
		/// </param>
		/// <param name="brush">
		/// Reference to the fill brush to use.
		/// </param>
		/// <param name="bounds">
		/// Outer bounds of the rectangle.
		/// </param>
		/// <param name="cornerRadius">
		/// Corner rounding radius.
		/// </param>
		public static void FillRoundedRectangle(Graphics graphics,
			Brush brush, Rectangle bounds, int cornerRadius)
		{
			if(graphics != null && brush != null)
			{
				using(GraphicsPath path = RoundedRectangle(bounds, cornerRadius))
				{
					graphics.FillPath(brush, path);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* FromHSL																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the system color from the caller's HSL value.
		/// </summary>
		/// <param name="hue">
		/// The Hue level to convert, from 0 to 360.
		/// </param>
		/// <param name="saturation">
		/// The Saturation to convert, from 0 to 1.
		/// </param>
		/// <param name="luminance">
		/// The Luminance to convert, from 0 to 1. 0 is black and 1 is white.
		/// </param>
		/// <param name="alpha">
		/// The alpha level to apply.
		/// </param>
		/// <returns>
		/// System.Drawing.Color representing the RGB value of the caller's color.
		/// </returns>
		public static Color FromHSL(float hue, float saturation, float luminance,
			int alpha = 255)
		{
			int vHue = (int)((hue / 360f) * 240f);
			int vLum = (int)(saturation * 240f);
			int vSat = (int)(luminance * 240f);
			return FromHSL(vHue, vLum, vSat, alpha);
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the system color from the caller's HSL value.
		/// </summary>
		/// <param name="hue">
		/// The Hue level to convert, from 0 to 240.
		/// </param>
		/// <param name="saturation">
		/// The Saturation to convert, from 0 to 240.
		/// </param>
		/// <param name="luminance">
		/// The Luminance to convert, from 0 to 240. 0 is black and 240 is white.
		/// </param>
		/// <param name="alpha">
		/// The Alpha level to apply.
		/// </param>
		/// <returns>
		/// System.Drawing.Color representing the RGB value of the caller's color.
		/// </returns>
		public static Color FromHSL(int hue, int saturation, int luminance,
			int alpha = 255)
		{
			Color result = ColorTranslator.FromWin32(
				ColorHLSToRGB(hue, luminance, saturation));
			if(alpha < 255)
			{
				result = Color.FromArgb(alpha, result);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetCharacterMatchCount																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the number of characters matching in the two strings from
		/// index 0 onward.
		/// </summary>
		/// <param name="value1">
		/// Left value to compare.
		/// </param>
		/// <param name="value2">
		/// Right value to compare.
		/// </param>
		/// <returns>
		/// Count of characters matching in the two strings from the beginning
		/// of the string. Otherwise, 0.
		/// </returns>
		public static int GetCharacterMatchCount(string value1, string value2)
		{
			char[] chars1 = null;
			char[] chars2 = null;
			int count = 0;
			int index = 0;
			int result = 0;

			if(value1?.Length > 0 && value2?.Length > 0)
			{
				chars1 = value1.ToCharArray();
				chars2 = value2.ToCharArray();
				count = Math.Min(chars1.Length, chars2.Length);
				for(index = 0; index < count; index ++)
				{
					if(chars1[index] == chars2[index])
					{
						result++;
					}
					else
					{
						break;
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetFilename																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return just the filename from the full path.
		/// </summary>
		/// <param name="pathName">
		/// Full path.
		/// </param>
		/// <returns>
		/// Just the filename of the specified value.
		/// </returns>
		public static string GetFilename(string pathName)
		{
			string fullpath = pathName;
			int index = 0;
			string[] levels = null;
			string result = "";

			if(fullpath?.Length > 0)
			{
				//	Remove values to the right of the url parameter line.
				index = fullpath.IndexOf("?");
				if(index > -1)
				{
					fullpath = fullpath.Substring(0, index);
				}
				//	Switch all slashes to forward.
				if(fullpath.IndexOf("\\") > -1)
				{
					fullpath = fullpath.Replace("\\", "/");
				}
				levels = fullpath.Split('/');
				if(levels.Length > 0)
				{
					result = levels[levels.Length - 1];
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetMediaTypeName																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the media type name for the supplied file extension.
		/// </summary>
		/// <param name="file">
		/// Reference to information about a file.
		/// </param>
		/// <returns>
		/// Media type name for the provided file.
		/// </returns>
		public static string GetMediaTypeName(FileInfo file)
		{
			string extension = "";
			string result = "";

			if(file != null)
			{
				extension = file.Extension.ToLower();
				if(extension.StartsWith("."))
				{
					extension = extension.Substring(1);
				}
				switch(extension)
				{
					case "aac":
					case "flac":
					case "m4a":
					case "mp3":
					case "wav":
					case "wma":
						result = "MediaAudio";
						break;
					case "bmp":
					case "gif":
					case "jpeg":
					case "jpg":
					case "png":
					case "tif":
					case "tiff":
					case "webp":
						result = "MediaImage";
						break;
					case "avi":
					case "mov":
					case "mp4":
					case "webm":
					case "wmv":
						result = "MediaVideo";
						break;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetRelaxedType																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the relaxed type name from the specified object type.
		/// </summary>
		/// <param name="value">
		/// Reference to the object for which a relaxed type will be found.
		/// </param>
		/// <returns>
		/// The name of the relaxed type to be used for this value.
		/// </returns>
		public static string GetRelaxedType(object value)
		{
			string result = "string";

			if(value != null)
			{
				if(value is bool)
				{
					result = "bool";
				}
				else if(value is Color)
				{
					result = "color";
				}
				else if(value is DateTime)
				{
					result = "datetime";
				}
				else if(value is Guid)
				{
					result = "guid";
				}
				else if(value is float)
				{
					result = "single";
				}
				else if(value is int ||
					(IsNumeric(value) && !IsNumericFloatingPoint(value)))
				{
					result = "int";
				}
				else if(value is double || IsNumericFloatingPoint(value))
				{
					result = "double";
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* InchesToPoints																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the number of points corresponding to the specified number of
		/// inches.
		/// </summary>
		/// <param name="value">
		/// Value, in inches.
		/// </param>
		/// <returns>
		/// Value, in points.
		/// </returns>
		public static float InchesToPoints(float value)
		{
			return value * 72f;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* InsideOf																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the text found between the left and right pattern edges.
		/// </summary>
		/// <param name="value">
		/// The value to inspect.
		/// </param>
		/// <param name="leftPattern">
		/// The pattern establishing the left boundary of the content.
		/// </param>
		/// <param name="rightPattern">
		/// The pattern establishing the right boundary of the content.
		/// </param>
		/// <returns>
		/// If the left and right patterns were both found, then the content
		/// between those patterns. If only the left pattern was found and the
		/// right pattern was not found to the right of it, then the content to
		/// the right of the left pattern. If the left pattern was not found and
		/// the right pattern was found, then the content to the left of the
		/// right pattern. Otherwise, an empty string.
		/// </returns>
		public static string InsideOf(string value,
			string leftPattern, string rightPattern)
		{
			int leftIndex = 0;
			string result = "";
			int rightIndex = 0;
			
			if(value?.Length > 0)
			{
				if(leftPattern?.Length > 0 && value.IndexOf(leftPattern) > -1)
				{
					//	Left pattern found.
					leftIndex = value.IndexOf(leftPattern);
					if(rightPattern?.Length > 0 &&
						value.IndexOf(rightPattern, leftIndex + 1) > -1)
					{
						//	Right pattern found.
						rightIndex =
							value.IndexOf(rightPattern, leftIndex + leftPattern.Length);
						if(rightIndex > leftIndex + leftPattern.Length)
						{
							result = value.Substring(leftIndex + leftPattern.Length,
								rightIndex - (leftIndex + leftPattern.Length));
						}
					}
					else
					{
						//	Right pattern not found.
						result = value.Substring(leftIndex + leftPattern.Length);
					}
				}
				else if(rightPattern?.Length > 0 && value.IndexOf(rightPattern) > -1)
				{
					//	Left pattern not found. Right pattern found.
					rightIndex = value.IndexOf(rightPattern);
					result = value.Substring(0, rightIndex);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* LimitLength																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the length-limited version of the caller's string.
		/// </summary>
		/// <param name="value">
		/// The value to limit.
		/// </param>
		/// <param name="length">
		/// The maximum allowable length.
		/// </param>
		/// <param name="ellipsis">
		/// Value indicating whether ellipsis will be used.
		/// </param>
		/// <returns>
		/// Caller's string, limited to the specified length.
		/// </returns>
		public static string LimitLength(string value, int length,
			bool ellipsis = true)
		{
			int sourceLength = length;
			StringBuilder result = new StringBuilder();

			if(value?.Length > 0 && length > 0)
			{
				if(value.Length > length)
				{
					if(ellipsis)
					{
						if(length > 3)
						{
							sourceLength = length - 3;
						}
						else
						{
							sourceLength = 0;
						}
					}
					if(sourceLength > 0)
					{
						result.Append(value.Substring(0, sourceLength));
					}
					if(ellipsis)
					{
						if(length > 3)
						{
							result.Append("...");
						}
						else
						{
							result.Append(new string('.', length));
						}
					}
				}
				else
				{
					result.Append(value);
				}
			}
			return result.ToString();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* LinearInterpolate																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the linear interpolation of the specified amount between values
		/// a and b.
		/// </summary>
		/// <param name="a">
		/// First value.
		/// </param>
		/// <param name="b">
		/// Second value.
		/// </param>
		/// <param name="amount">
		/// The amount to interpolate.
		/// </param>
		/// <returns>
		/// The linearly interpolated difference between a and b.
		/// </returns>
		public static float LinearInterpolate(float a, float b, float amount)
		{
			return a * (1f - amount) + b * amount;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the linear interpolation between points a and b.
		/// </summary>
		/// <param name="a">
		/// Point a.
		/// </param>
		/// <param name="b">
		/// Point b.
		/// </param>
		/// <param name="amount">
		/// The amount to interpolate.
		/// </param>
		/// <returns>
		/// The linearly interpolated difference between points a and b.
		/// </returns>
		public static PointF LinearInterpolate(PointF a, PointF b, float amount)
		{
			PointF result = new PointF();

			result.X = a.X * (1f - amount) + b.X * amount;
			result.Y = a.Y * (1f - amount) + b.Y * amount;

			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MeasureString																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the size of the caller's string.
		/// </summary>
		/// <param name="text">
		/// Text to measure.
		/// </param>
		/// <param name="fontName">
		/// Name of the font.
		/// </param>
		/// <param name="fontSize">
		/// Size of the font.
		/// </param>
		/// <param name="maxWidth">
		/// Optional maximum width of the string.
		/// </param>
		/// <returns>
		/// </returns>
		public static Size MeasureString(string text,
			string fontName, float fontSize, int maxWidth = 0)
		{
			Graphics g = Graphics.FromHwnd(IntPtr.Zero);
			Size result = Size.Empty;

			if(text?.Length > 0 && fontName?.Length > 0 && fontSize > 0f)
			{
				g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
				if(maxWidth > 0f)
				{
					//	Word wrap.
					result =
						Size.Round(
							g.MeasureString(text, new Font(fontName, fontSize), maxWidth,
							StringFormat.GenericTypographic));
				}
				else
				{
					//	Single line.
					result =
						Size.Round(
							g.MeasureString(text, new Font(fontName, fontSize)));
				}
			}
			g.Dispose();
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PointsToInches																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the number of inches corresponding to the specified number of
		/// points.
		/// </summary>
		/// <param name="value">
		/// Value, in points.
		/// </param>
		/// <returns>
		/// Value, in inches.
		/// </returns>
		public static float PointsToInches(float value)
		{
			return value / 72f;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PointsToPixels																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the pixel size occupied by the specified number of points.
		/// </summary>
		/// <param name="pointSize">
		/// Size to convert, in points.
		/// </param>
		/// <returns>
		/// Count of pixels occupying the caller's point space.
		/// </returns>
		public static int PointsToPixels(float pointSize)
		{
			int result = 0;

			result = (int)(pointSize * 1.333333f);
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ProgressUpdate																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update progress bar value through asynchronous invoke.
		/// </summary>
		/// <param name="progress">
		/// Reference to the progress bar to update.
		/// </param>
		/// <param name="value">
		/// Value to place on the progress bar.
		/// </param>
		public static void ProgressUpdate(ToolStripProgressBar progress, int value)
		{
			if(progress != null && progress.GetCurrentParent().Parent.InvokeRequired)
			{
				progress.GetCurrentParent().Parent.BeginInvoke(
					new MethodInvoker(delegate { progress.Value = value; }));
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	RectangleFromPoints																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a rectangle from two points.
		/// </summary>
		/// <param name="a">
		/// Upper left or lower right point.
		/// </param>
		/// <param name="b">
		/// Contrasting point.
		/// </param>
		/// <returns>
		/// Reference to a floating point rectangle of the area described by
		/// a and b.
		/// </returns>
		public static RectangleF RectangleFromPoints(PointF a, PointF b)
		{
			RectangleF result = RectangleF.Empty;
			float x1 = a.X;
			float x2 = b.X;
			float y1 = a.Y;
			float y2 = b.Y;

			result = new RectangleF(
				Math.Min(x1, x2), Math.Min(y1, y2),
				Math.Abs(x2 - x1), Math.Abs(y2 - y1));
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RelativeFilename																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the relative filename from the logical difference between
		/// two paths.
		/// </summary>
		/// <param name="baseFile">
		/// The base file or domain name from which a relative path can be built.
		/// </param>
		/// <param name="relativeFile">
		/// An external file to be referenced with a relative path.
		/// </param>
		/// <returns>
		/// Relative path and filename of the specified relative file, if
		/// feasible. Otherwise, the full path of the relative file.
		/// </returns>
		public static string RelativeFilename(FileInfo baseFile,
			FileInfo relativeFile)
		{
			StringBuilder builder = null;
			int count = 0;
			int index = 0;
			string[] levels = null;
			string pathBase = "";
			string pathOffset = "";
			string pathRel = "";
			int prefixLength = 0;
			string result = "";
			char[] slash = new char[] { '/' };

			if(relativeFile != null)
			{
				if(baseFile != null)
				{
					pathBase = baseFile.Directory.FullName.ToLower().Replace('\\', '/');
					pathRel =
						relativeFile.Directory.FullName.ToLower().Replace('\\', '/');
					if(pathBase == pathRel)
					{
						//	Same folder. The filename is all that is needed.
						result = relativeFile.Name;
					}
					else
					{
						prefixLength = GetCharacterMatchCount(pathBase, pathRel);
						if(prefixLength == 0)
						{
							//	No path in common.
							result = relativeFile.FullName.Replace('\\', '/');
						}
						else if(prefixLength == pathBase.Length)
						{
							//	The relative file is inward from the base.
							result = Path.Combine(
								relativeFile.Directory.FullName.
								Substring(prefixLength),
								relativeFile.Name).Replace('\\', '/');
							if(result.StartsWith("/"))
							{
								result = result.Substring(1);
							}
						}
						else if(prefixLength == pathRel.Length)
						{
							//	The relative file is back from the base.
							pathOffset = baseFile.Directory.FullName.
								Substring(prefixLength).Replace('\\', '/');
							if(pathOffset.StartsWith("/"))
							{
								pathOffset = pathOffset.Substring(1);
							}
							if(pathOffset.EndsWith("/"))
							{
								pathOffset = pathOffset.Substring(0, pathOffset.Length - 1);
							}
							builder = new StringBuilder();
							levels = pathOffset.Split(slash);
							count = levels.Length;
							for(index = 0; index < count; index ++)
							{
								builder.Append("../");
							}
							builder.Append(relativeFile.Name);
							result = builder.ToString();
						}
						else
						{
							//	The relative file is diagonal to the base.
							//	Start by getting the number of directories back.
							pathOffset = baseFile.Directory.FullName.
								Substring(prefixLength).Replace('\\', '/');
							if(pathOffset.StartsWith("/"))
							{
								pathOffset = pathOffset.Substring(1);
							}
							if(pathOffset.EndsWith("/"))
							{
								pathOffset = pathOffset.Substring(0, pathOffset.Length - 1);
							}
							builder = new StringBuilder();
							levels = pathOffset.Split(slash);
							count = levels.Length;
							for(index = 0; index < count; index++)
							{
								builder.Append("../");
							}
							//	Continue by adding in the number of directories forward.
							pathOffset = relativeFile.Directory.FullName.
								Substring(prefixLength).Replace('\\', '/');
							if(pathOffset.StartsWith("/"))
							{
								pathOffset = pathOffset.Substring(1);
							}
							if(pathOffset.EndsWith("/"))
							{
								pathOffset = pathOffset.Substring(0, pathOffset.Length - 1);
							}
							if(builder[builder.Length - 1] != '/')
							{
								builder.Append("/");
							}
							builder.Append(pathOffset);
							builder.Append("/");
							builder.Append(relativeFile.Name);
							result = builder.ToString();
						}
					}
				}
				else
				{
					result = relativeFile.FullName.Replace('\\', '/');
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RemoveExtension																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the portion of the filename without the extension.
		/// </summary>
		/// <param name="filename">
		/// Name of the file to inspect.
		/// </param>
		/// <returns>
		/// Main body of the filename without the extension.
		/// </returns>
		public static string RemoveExtension(string filename)
		{
			string result = "";

			if(filename?.Length > 0)
			{
				if(filename.IndexOf('.') > -1)
				{
					result = filename.Substring(0, filename.IndexOf('.'));
				}
				else
				{
					result = filename;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ResolveEnvironment																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a string value where all environment variable references have
		/// been resolved.
		/// </summary>
		/// <param name="value">
		/// String value potentially containing environment variables wrapped in
		/// percent signs.
		/// </param>
		/// <returns>
		/// String value where all environment variables have been replaced with
		/// their local values.
		/// </returns>
		public static string ResolveEnvironment(string value)
		{
			string env = "";
			MatchCollection matches = null;
			string setting = "";
			string result = value;

			matches = Regex.Matches(value, "%(?<f>[^%]+?)%");
			foreach(Match match in matches)
			{
				setting = GetValue(match, "f");
				if(setting.Length > 0)
				{
					env = Environment.GetEnvironmentVariable(setting);
					if(env?.Length > 0)
					{
						result = result.Replace($"%{setting}%", env);
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RoundedRectangle																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a graphics path representing a rounded rectangle.
		/// </summary>
		/// <param name="bounds">
		/// Area of the outer bounds of the rectangle.
		/// </param>
		/// <param name="radius">
		/// Corner rounding radius.
		/// </param>
		/// <returns>
		/// Reference to a graphics path composed of the strokes of a rounded
		/// rectangle.
		/// </returns>
		public static GraphicsPath RoundedRectangle(Rectangle bounds, int radius)
		{
			int diameter = radius * 2;
			Size size = new Size(diameter, diameter);
			Rectangle arc = new Rectangle(bounds.Location, size);
			GraphicsPath path = new GraphicsPath();

			if(radius == 0)
			{
				path.AddRectangle(bounds);
				return path;
			}

			// Top left arc  
			path.AddArc(arc, 180, 90);

			// Top right arc  
			arc.X = bounds.Right - diameter;
			path.AddArc(arc, 270, 90);

			// Bottom right arc  
			arc.Y = bounds.Bottom - diameter;
			path.AddArc(arc, 0, 90);

			// Bottom left arc 
			arc.X = bounds.Left;
			path.AddArc(arc, 90, 90);

			path.CloseFigure();
			return path;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Saturate																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Saturate any value outside of a positive decimal fraction.
		/// </summary>
		/// <param name="value">
		/// Decimal fraction to inspect.
		/// </param>
		/// <returns>
		/// Caller's value, saturated to 0 if the original value was less than 0
		/// and saturated to 1 if the original value was more than 1.
		/// </returns>
		public static double Saturate(double value)
		{
			double result = value;

			if(value < 0d)
			{
				result = 0d;
			}
			else if(value > 1d)
			{
				result = 1d;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ScaleValues																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the scale of the caller's values.
		/// </summary>
		/// <param name="x">
		/// Value to be scaled.
		/// </param>
		/// <param name="a">
		/// Difference from absolute.
		/// </param>
		/// <param name="b">
		/// Reference size.
		/// </param>
		/// <param name="c">
		/// Set size.
		/// </param>
		/// <param name="d">
		/// Set total.
		/// </param>
		/// <returns>
		/// X scaled by reference values.
		/// </returns>
		public static double ScaleValues(double x, double a, double b,
			double c, double d)
		{
			double s = (x - a) / (b - a);
			return s * (d - c) + c;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ScaleDrawing																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Translate the caller's physical coordinate to the scaled view.
		/// </summary>
		/// <param name="origin">
		/// The physical coordinate being translated.
		/// </param>
		/// <param name="scale">
		/// The current scaling factor.
		/// </param>
		/// <param name="scroll">
		/// The current scroll value in the specified axis.
		/// </param>
		/// <returns>
		/// </returns>
		/// <remarks>
		/// Scrollbar values are assumed to exist in the scaled domain, since
		/// they serve to move the scaled view on the physical viewport.
		/// </remarks>
		public static float ScaleDrawing(int origin, float scale, int scroll = 0)
		{
			float result = 0f;

			if(scale != 0f)
			{
				result = ((float)origin * (1f / scale)) + (float)scroll;
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Translate the caller's physical point to the scaled view.
		/// </summary>
		/// <param name="origin">
		/// The physical point being translated.
		/// </param>
		/// <param name="scale">
		/// The current scaling factor.
		/// </param>
		/// <param name="scrollHorizontal">
		/// The current horizontal scroll value.
		/// </param>
		/// <param name="scrollVertical">
		/// The current vertical scroll value.
		/// </param>
		/// <returns>
		/// A scaled version of the caller's point.
		/// </returns>
		/// <remarks>
		/// Scrollbar values are assumed to exist in the scaled domain, since
		/// they serve to move the scaled view on the physical viewport.
		/// </remarks>
		public static PointF ScaleDrawing(Point origin, SizeF scale,
			int scrollHorizontal = 0, int scrollVertical = 0)
		{
			PointF orig = new PointF((float)origin.X, (float)origin.Y);
			PointF result = new PointF(0f, 0f);

			if(scale.Width != 0f)
			{
				result.X =
					(orig.X + (float)scrollHorizontal) * (1f / scale.Width);
				//Debug.WriteLine(
				//	$"Scale H with {scrollHorizontal} From {orig.X} to {result.X}");
			}
			if(scale.Height != 0f)
			{
				result.Y =
					(orig.Y + (float)scrollVertical) * (1f / scale.Height);
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Scale the rectangle to the specified factor and return the new area to
		/// the caller.
		/// </summary>
		/// <param name="rectangle">
		/// Rectangle to inspect.
		/// </param>
		/// <param name="scale">
		/// X and Y scale factors.
		/// </param>
		/// <returns>
		/// New rectangle as a result of the original rectangle scaled by the
		/// specified factors.
		/// </returns>`
		public static RectangleF ScaleDrawing(RectangleF rectangle, SizeF scale)
		{
			RectangleF result = new RectangleF(
				rectangle.X * scale.Width,
				rectangle.Y * scale.Height,
				rectangle.Width * scale.Width,
				rectangle.Height * scale.Height);
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SvgReplaceFileRefWithB64																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Replace all file references in the SVG content with base64 embedded
		/// values.
		/// </summary>
		/// <param name="directory">
		/// Working directory serving as the base of the current content.
		/// </param>
		/// <param name="rawContent">
		/// SVG content.
		/// </param>
		/// <param name="progress">
		/// Reference to an optional progress bar to display the current progress.
		/// </param>
		/// <returns>
		/// SVG string value where all external references have been fully
		/// embedded.
		/// </returns>
		public static async Task SvgReplaceFileRefWithB64(DirectoryInfo directory,
			string rawContent, NameValueItem namedResult,
			ToolStripProgressBar progress = null)
		{
			Action replaceInstances = () =>
			{
				byte[] buffer = new byte[0];  //	File content.
				StringBuilder builder = new StringBuilder();  //	base64 content.
				double count = 0;
				FileInfo file = null;
				double index = 0;
				string inner = "";
				MatchCollection matches = null;
				List<string> processedLinks = new List<string>();
				string result = rawContent;

				if(directory != null && rawContent?.Length > 0)
				{
					matches = Regex.Matches(rawContent, ResourceMain.rxSVGHREFFind);
					count = (double)matches.Count;
					index = 0.0;
					foreach(Match match in matches)
					{
						//	External reference found.
						if(builder.Length > 0)
						{
							builder.Remove(0, builder.Length);
						}
						inner = GetValue(match, "inner");
						if(inner.Length > 0 && !processedLinks.Exists(x => x == inner))
						{
							file = new FileInfo(Path.Combine(directory.FullName, inner));
							if(file.Exists)
							{
								//	The file is available.
								buffer = File.ReadAllBytes(file.FullName);
								builder.Append("data:");
								builder.Append(MimeType(file.Extension));
								builder.Append(";base64,");
								builder.Append(Convert.ToBase64String(buffer));
							}
							result = result.Replace(GetValue(match, "outer"),
								$"xlink:href=\"{builder}\"");
							processedLinks.Add(inner);
							GC.Collect();
						}
						if(progress != null && count != 0.0)
						{
							ProgressUpdate(progress, (int)(index / count));
						}
						index++;
					}
				}
				namedResult.Name = "OK";
				namedResult.Value = result;
			};
			await Task.Run(replaceInstances);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ThreadSleep																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Continue to process the UI while waiting on the calling thread for
		/// the specified number of milliseconds.
		/// </summary>
		/// <param name="milliseconds">
		/// Number of milliseconds to wait.
		/// </param>
		/// <returns>
		/// Reference to an active task.
		/// </returns>
		public static async Task ThreadSleep(int milliseconds)
		{
			await Task.Delay(milliseconds);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ToImpliedType																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value that is either string, numeric, or boolean.
		/// </summary>
		/// <param name="value">
		/// Value to inspect.
		/// </param>
		/// <returns>
		/// Value in a native type corresponding to string, numeric, or boolean.
		/// </returns>
		/// <remarks>
		/// This method is generally intended for use with JSON output.
		/// </remarks>
		public static object ToImpliedType(string value)
		{
			object result = null;

			if(value != null)
			{
				if(IsNumeric(value))
				{
					if(IsNumericFloatingPoint(value))
					{
						result = ToFloat(value);
					}
					else
					{
						result = ToInt(value);
					}
				}
				else if(IsBoolean(value))
				{
					result = ToBoolean(value);
				}
				else
				{
					result = value;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	TypeConverter																													*
		//*-----------------------------------------------------------------------*
		private static RelaxedTypeConverterCollection mTypeConverter =
			new RelaxedTypeConverterCollection();
		/// <summary>
		/// Get a reference to the universal relaxed type converter.
		/// </summary>
		public static RelaxedTypeConverterCollection TypeConverter
		{
			get { return mTypeConverter; }
		}
		//*-----------------------------------------------------------------------*

#if DEBUG && VERBOSE
		//*-----------------------------------------------------------------------*
		//* Verbose																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Send a message to the verbose text output.
		/// </summary>
		/// <param name="value">
		/// Message to display.
		/// </param>
		/// <param name="level">
		/// Verbose level to display.
		/// </param>
		public static void Verbose(string value, int level = 1)
		{
#if V3
			if(level <= 3)
			{
				Debug.WriteLine(value);
			}
#elif V2
			if(level <= 2)
			{
				Debug.WriteLine(value);
			}
#else
			if(level == 1)
			{
				Debug.WriteLine(value);
			}
#endif
		}
		//*-----------------------------------------------------------------------*
#else
			//*-----------------------------------------------------------------------*
			//* Verbose																																*
			//*-----------------------------------------------------------------------*
			/// <summary>
			/// Send a message to the verbose text output.
			/// </summary>
			/// <param name="value">
			/// Message to display.
			/// </param>
			/// <param name="level">
			/// Verbose level to display.
			/// </param>
			public static void Verbose(string value, int level = 1) { }
		//*-----------------------------------------------------------------------*
#endif

	}
	//*-------------------------------------------------------------------------*
}
