//	SKSvg.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
//	------
//	Based on SkiaSharp.Extended.Svg Copyright (c) 2017 Xamarin, Inc.,
//	also licensed and distributed under the MIT License.
#define NOTPORTABLE
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

using SkiaSharp;

namespace SkiaSharpSvg
{
	//*-------------------------------------------------------------------------*
	//*	SKSvg																																		*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// The main SVG rendering functionality.
	/// </summary>
	public class SKSvg
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		private static readonly IFormatProvider iCulture =
			CultureInfo.InvariantCulture;

#if PORTABLE
		//	Use reflection to try finding a method that supports a 
		//	file path and an XmlParserContext...
		private static readonly MethodInfo mCreateReaderMethod;
#endif

		private const float mDefaultPPI = 150f;
		private readonly Dictionary<string, XElement> mDefSection =
			new Dictionary<string, XElement>();
		//private const bool mErrorOnUnsupportedElementFound = false;

		private static readonly Regex mRxClipPathUrl =
			new Regex(@"url\s*\(\s*#([^\)]+)\)");
		private static readonly Regex mRxFillUrl =
			new Regex(@"url\s*\(\s*#([^\)]+)\)");
		private static readonly Regex mRxKeyValue =
			new Regex(@"\s*([\w-]+)\s*:\s*(.*)");
		private static readonly Regex mRxPercent =
			new Regex("%");
		private static readonly Regex mRxUnit =
			new Regex("px|pt|em|ex|pc|cm|mm|in");
		private static readonly Regex mRxWhitespace =
			new Regex(@"\s{2,}");

		private static readonly char[] mWhitespace =
			new char[] { ' ', '\t', '\n', '\r' };

		private readonly XmlReaderSettings mXmlReaderSettings =
			new XmlReaderSettings()
			{
				DtdProcessing = DtdProcessing.Ignore,
				IgnoreComments = true,
			};

		private static readonly XNamespace xlink =
			"http://www.w3.org/1999/xlink";
		private static readonly XNamespace svg =
			"http://www.w3.org/2000/svg";

		//*-----------------------------------------------------------------------*
		//* CreatePaint																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a new paint object, initialized with commonly used values.
		/// </summary>
		/// <param name="stroke">
		/// Value indicating whether this is a stroke-based object.
		/// </param>
		/// <returns>
		/// Reference to the newly created paint object.
		/// </returns>
		private SKPaint CreatePaint(bool stroke = false)
		{
			return new SKPaint
			{
				IsAntialias = true,
				IsStroke = stroke,
				Color = SKColors.Black
			};
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* CreateSvgXmlContext																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return an XML parser context that has been primed for use with SVG
		/// compliant data.
		/// </summary>
		/// <returns>
		/// Reference to an XML parser context ready to use with SVG.
		/// </returns>
		private static XmlParserContext CreateSvgXmlContext()
		{
			var table = new NameTable();
			var manager = new XmlNamespaceManager(table);
			manager.AddNamespace(string.Empty, svg.NamespaceName);
			manager.AddNamespace("xlink", xlink.NamespaceName);
			return new XmlParserContext(null, manager, null, XmlSpace.None);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetString																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the string value of the specified style attribute.
		/// </summary>
		/// <param name="style">
		/// Collection of style attributes.
		/// </param>
		/// <param name="name">
		/// Name of the attribute to retrieve.
		/// </param>
		/// <param name="defaultValue">
		/// Default value to return if the specified attribute is not found.
		/// </param>
		/// <returns>
		/// Value of the specified attribute, if found. Otherwise,
		/// caller-specified default value.
		/// </returns>
		private string GetString(Dictionary<string, string> style,
			string name, string defaultValue = "")
		{
			if(style.TryGetValue(name, out string v))
			{
				return v;
			}
			return defaultValue;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* HasSvgNamespace																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the provided xname has an svg:
		/// namespace.
		/// </summary>
		/// <param name="name">
		/// Reference to the xname object to inspect.
		/// </param>
		/// <returns>
		/// True if an svg: namespace was found. Otherwise, false.
		/// </returns>
		private static bool HasSvgNamespace(XName name)
		{
			return
				string.IsNullOrEmpty(name.Namespace?.NamespaceName) ||
				name.Namespace == svg ||
				name.Namespace == xlink;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* LoadElements																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Load the SVG elements onto the provided canvas.
		/// </summary>
		/// <param name="elements">
		/// Collection of elements to transfer.
		/// </param>
		/// <param name="canvas">
		/// Reference to the active drawing canvas.
		/// </param>
		/// <param name="stroke">
		/// Active pen stroke.
		/// </param>
		/// <param name="fill">
		/// Active bucket fill.
		/// </param>
		private void LoadElements(IEnumerable<XElement> elements,
			SKCanvas canvas, SKPaint stroke, SKPaint fill)
		{
			foreach(var e in elements)
			{
				ReadElement(e, canvas, stroke, fill);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* LogOrThrow																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Log a message or throw an exception, depending upon the setting of
		/// the ThrowOnUnsupportedElement property.
		/// </summary>
		/// <param name="message">
		/// Message to deliver.
		/// </param>
		private void LogOrThrow(string message)
		{
			if(mThrowOnUnsupportedElement)
			{
				throw new NotSupportedException(message);
			}
			else
			{
				Debug.WriteLine(message);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadBaselineShift																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read the baseline shift, in user units.
		/// </summary>
		/// <param name="element">
		/// Reference to an XML element containing a baseline-shift attribute.
		/// </param>
		/// <returns>
		/// Baseline shift amount, in user units.
		/// </returns>
		private float ReadBaselineShift(XElement element)
		{
			string value = null;
			if(element != null)
			{
				var attrib = element.Attribute("baseline-shift");
				if(attrib != null && !string.IsNullOrWhiteSpace(attrib.Value))
					value = attrib.Value;
				else
				{
					var style = element.Attribute("style");
					if(style != null && !string.IsNullOrWhiteSpace(style.Value))
					{
						value = GetString(ReadStyle(style.Value), "baseline-shift");
					}
				}
			}

			return ReadNumber(value);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadCircle																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read and return a circle.
		/// </summary>
		/// <param name="element">
		/// XML element containing the description of a circle.
		/// </param>
		/// <returns>
		/// Reference to a circle object.
		/// </returns>
		private SKCircle ReadCircle(XElement element)
		{
			var cx = ReadNumber(element.Attribute("cx"));
			var cy = ReadNumber(element.Attribute("cy"));
			var rr = ReadNumber(element.Attribute("r"));

			return new SKCircle(new SKPoint(cx, cy), rr);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadClipPath																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a vector polygon representation of the provided raw clips.
		/// </summary>
		/// <param name="raw">
		/// Raw clip path information.
		/// </param>
		/// <returns>
		/// Reference to a vector polygon representing the specified clip paths.
		/// </returns>
		private SKPath ReadClipPath(string raw)
		{
			if(string.IsNullOrWhiteSpace(raw))
			{
				return null;
			}

			SKPath result = null;
			var read = false;
			var urlM = mRxClipPathUrl.Match(raw);
			if(urlM.Success)
			{
				var id = urlM.Groups[1].Value.Trim();

				if(mDefSection.TryGetValue(id, out XElement defE))
				{
					result = ReadClipPathDefinition(defE);
					if(result != null)
					{
						read = true;
					}
				}
				else
				{
					LogOrThrow($"Invalid clip-path url reference: {id}");
				}
			}

			if(!read)
			{
				LogOrThrow($"Unsupported clip-path: {raw}");
			}

			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadClipPathDefinition																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a clip path definition vector polygon.
		/// </summary>
		/// <param name="element">
		/// Reference to an XML element containing the clip path definition.
		/// </param>
		/// <returns>
		/// Reference to a vector polygon containing clip path definition.
		/// </returns>
		private SKPath ReadClipPathDefinition(XElement element)
		{
			if(element.Name.LocalName != "clipPath" || !element.HasElements)
			{
				return null;
			}

			var result = new SKPath();

			foreach(var ce in element.Elements())
			{
				var path = ReadElement(ce);
				if(path != null)
				{
					result.AddPath(path);
				}

				else
				{
					LogOrThrow(
						"SVG element " +
						$"'{ce.Name.LocalName}' is not supported in clipPath.");
				}
			}

			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadDefinition																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the child def element from the specified element.
		/// </summary>
		/// <param name="element">
		/// Reference to an XML element containing a def child element.
		/// </param>
		/// <returns>
		/// Reference to an XML element representing a def SVG element.
		/// </returns>
		private XElement ReadDefinition(XElement element)
		{
			var union = new XElement(element.Name);
			union.Add(element.Elements());
			union.Add(element.Attributes());

			var child = ReadHref(element);
			if(child != null)
			{
				union.Add(child.Elements());
				union.Add(
					child.Attributes().Where(a => union.Attribute(a.Name) == null));
			}

			return union;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadElement																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read and process an SVG element.
		/// </summary>
		/// <param name="element">
		/// XML element to process.
		/// </param>
		/// <returns>
		/// Reference to information about a vector polygon.
		/// </returns>
		private SKPath ReadElement(XElement element)
		{
			var path = new SKPath();

			var elementName = element.Name.LocalName;
			switch(elementName)
			{
				case "rect":
					var rect = ReadRoundedRect(element);
					if(rect.IsRounded)
					{
						//path.AddRoundedRect(rect.Rect,
						//	rect.RadiusX, rect.RadiusY);
						path.AddRoundRect(rect.Rect, rect.RadiusX, rect.RadiusY);
					}
					else
					{
						path.AddRect(rect.Rect);
					}
					break;
				case "ellipse":
					var oval = ReadOval(element);
					path.AddOval(oval.BoundingRect);
					break;
				case "circle":
					var circle = ReadCircle(element);
					path.AddCircle(circle.Center.X, circle.Center.Y, circle.Radius);
					break;
				case "path":
					var d = element.Attribute("d")?.Value;
					if(!string.IsNullOrWhiteSpace(d))
					{
						path.Dispose();
						path = SKPath.ParseSvgPathData(d);
					}
					break;
				case "polygon":
				case "polyline":
					var close = elementName == "polygon";
					var p = element.Attribute("points")?.Value;
					if(!string.IsNullOrWhiteSpace(p))
					{
						p = "M" + p;
						if(close)
							p += " Z";
						path.Dispose();
						path = SKPath.ParseSvgPathData(p);
					}
					break;
				case "line":
					var line = ReadLine(element);
					path.MoveTo(line.P1);
					path.LineTo(line.P2);
					break;
				default:
					path.Dispose();
					path = null;
					break;
			}

			return path;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Read and process an SVG element.
		/// </summary>
		/// <param name="element">
		/// XML element to process.
		/// </param>
		/// <param name="canvas">
		/// Reference to the active drawing canvas.
		/// </param>
		/// <param name="stroke">
		/// Active pen stroke.
		/// </param>
		/// <param name="fill">
		/// Active bucket fill.
		/// </param>
		private void ReadElement(XElement element,
			SKCanvas canvas, SKPaint stroke, SKPaint fill)
		{
			//	SVG element.
			string elementName = element.Name.LocalName;
			bool isGroup = (elementName == "g");
			byte opacity = 1;
			SKPaint opacityPaint = null;
			float styleOpacity = 1.0f;

			SKPaint paint = new SKPaint
			{
				FilterQuality = SKFilterQuality.High,
				IsAntialias = true,
				IsDither = true,
				IsEmbeddedBitmapText = true
			};

			//	Read style.
			Dictionary<string, string> style =
				ReadPaints(element, ref stroke, ref fill, isGroup);

			if(style.ContainsKey("display"))
			{
				//	Any element set to style="display:none" should be ignored.
				if(style["display"] == "none")
				{
					return;
				}
			}
			styleOpacity = ReadOpacity(style);
			if(styleOpacity == 0.0f)
			{
				//	Any element set to style="opacity:0" should be ignored.
				return;
			}

			//	No unconditional returns beyond this point.
			//	Transform matrix.
			SKMatrix transform =
				ReadTransform(element.Attribute("transform")?.Value ?? string.Empty);
			if(!transform.IsIdentity)
			{
				canvas.Save();
				canvas.Concat(ref transform);
			}

			// Clip-path.
			SKPath clipPath =
				ReadClipPath(element.Attribute("clip-path")?.Value ?? string.Empty);
			if(clipPath != null)
			{
				canvas.ClipPath(clipPath);
			}

			if(styleOpacity != 1.0f)
			{
				opacity = (byte)(255 * styleOpacity);
				opacityPaint = new SKPaint
				{
					Color = SKColors.Black.WithAlpha(opacity)
				};

				//	Apply the opacity.
				canvas.SaveLayer(opacityPaint);
			}

			//	Parse elements.
			Debug.WriteLine($"Parse {elementName}");
			//if(elementName == "text")
			//{
			//	return;
			//}
			switch(elementName)
			{
				case "image":
					var image = ReadImage(element);
					if(image.Bytes != null)
					{
						using(var bitmap = SKBitmap.Decode(image.Bytes))
						{
							if(bitmap != null)
							{
								canvas.DrawBitmap(bitmap, image.Rect, paint);
							}
						}
					}
					break;
				case "text":
					if(stroke != null || fill != null)
					{
						var spans = ReadText(element, stroke?.Clone(), fill?.Clone());
						if(spans.Any())
						{
							canvas.DrawText(spans);
						}
					}
					break;
				case "rect":
				case "ellipse":
				case "circle":
				case "path":
				case "polygon":
				case "polyline":
				case "line":
					if(stroke != null || fill != null)
					{
						var elementPath = ReadElement(element);
						if(elementPath != null)
						{
							if(fill != null)
							{
								canvas.DrawPath(elementPath, fill);
							}
							if(stroke != null)
							{
								canvas.DrawPath(elementPath, stroke);
							}
						}
					}
					break;
				case "g":
					//	In SVG, groups play roles both as layers and as logical
					//	grouping sections on a layer.
					//	They can be translated, transformed, rotated, hidden,
					//	and changed in opacity, like any other visual object.
					if(element.HasElements)
					{
						//// get current group opacity
						//if(styleOpacity > 0f)
						//{
							////	Anything in the group will only be visible if
							////	it is not totally hidden.
							//if(styleOpacity != 1.0f)
							//{
							//	opacity = (byte)(255 * styleOpacity);
							//	opacityPaint = new SKPaint
							//	{
							//		Color = SKColors.Black.WithAlpha(opacity)
							//	};

							//	//	Apply the opacity.
							//	canvas.SaveLayer(opacityPaint);
							//}

							foreach(var gElement in element.Elements())
							{
								ReadElement(gElement, canvas, stroke?.Clone(), fill?.Clone());
							}

							//// restore state
							//if(styleOpacity != 1.0f)
							//{
							//	canvas.Restore();
							//}
						//}
					}
					break;
				case "use":
					if(element.HasAttributes)
					{
						var href = ReadHref(element);
						if(href != null)
						{
							// TODO: copy/process other attributes

							var x = ReadNumber(element.Attribute("x"));
							var y = ReadNumber(element.Attribute("y"));
							//var useTransform = SKMatrix.MakeTranslation(x, y);
							var useTransform = SKMatrix.CreateTranslation(x, y);

							canvas.Save();
							canvas.Concat(ref useTransform);

							ReadElement(href, canvas, stroke?.Clone(), fill?.Clone());

							canvas.Restore();
						}
					}
					break;
				case "switch":
					if(element.HasElements)
					{
						foreach(var ee in element.Elements())
						{
							var requiredFeatures = ee.Attribute("requiredFeatures");
							var requiredExtensions = ee.Attribute("requiredExtensions");
							var systemLanguage = ee.Attribute("systemLanguage");

							// TODO: evaluate requiredFeatures, requiredExtensions and systemLanguage
							var isVisible =
								requiredFeatures == null &&
								requiredExtensions == null &&
								systemLanguage == null;

							if(isVisible)
							{
								ReadElement(ee, canvas, stroke?.Clone(), fill?.Clone());
							}
						}
					}
					break;
				case "defs":
				case "title":
				case "desc":
				case "description":
					// already read earlier
					break;
				default:
					LogOrThrow($"SVG element '{elementName}' is not supported");
					break;
			}
			//	Restore general opacity state.
			if(styleOpacity != 1.0f)
			{
				canvas.Restore();
			}

			if(!transform.IsIdentity)
			{
				//	Restore matrix from transform.
				canvas.Restore();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadFontAttributes																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read the font attributes, and set the paint properties accordinly.
		/// </summary>
		/// <param name="element">
		/// XML element containing font attribute descriptions.
		/// </param>
		/// <param name="paint">
		/// Reference to the paint object to update.
		/// </param>
		private void ReadFontAttributes(XElement element, SKPaint paint)
		{
			string fillColor = "#000000";
			SKColor fillColorK = SKColor.Empty;
			Dictionary<string, string> fontStyle = ReadStyle(element);
			SKFontStyleSlant fSlantStyle = SKFontStyleSlant.Upright;
			int fweight = 0;
			int fwidth = 0;

			//	Font family.
			if(!fontStyle.TryGetValue("font-family", out string ffamily) ||
				string.IsNullOrWhiteSpace(ffamily))
			{
				ffamily = paint.Typeface?.FamilyName;
			}
			//	Font weight.
			fweight = ReadFontWeight(fontStyle,
				paint.Typeface?.FontWeight ?? (int)SKFontStyleWeight.Normal);
			//	Font width.
			fwidth = ReadFontWidth(fontStyle,
				paint.Typeface?.FontWidth ?? (int)SKFontStyleWidth.Normal);
			//	Font slant.
			fSlantStyle = ReadFontStyle(fontStyle,
				paint.Typeface?.FontSlant ?? SKFontStyleSlant.Upright);

			paint.Typeface = SKTypeface.FromFamilyName(
				ffamily, fweight, fwidth, fSlantStyle);

			//	Font size.
			if(fontStyle.TryGetValue("font-size", out string fsize) &&
				!string.IsNullOrWhiteSpace(fsize))
			{
				paint.TextSize = ReadNumber(fsize);
			}

			//	Fill color.
			if(fontStyle.TryGetValue("fill", out fillColor))
			{
				SKColor.TryParse(fillColor, out fillColorK);
				if(fillColorK != SKColor.Empty)
				{
					paint.Color = fillColorK;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadFontStyle																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read the font style attributes, and use it to prepare a font style
		/// object.
		/// </summary>
		/// <param name="fontStyle">
		/// List of font style attributes from XML.
		/// </param>
		/// <param name="defaultStyle">
		/// Default prepared font style.
		/// </param>
		/// <returns>
		/// Reference to a font style object.
		/// </returns>
		private static SKFontStyleSlant ReadFontStyle(
			Dictionary<string, string> fontStyle,
			SKFontStyleSlant defaultStyle = SKFontStyleSlant.Upright)
		{
			SKFontStyleSlant style = defaultStyle;

			if(fontStyle.TryGetValue("font-style", out string fstyle) &&
				!string.IsNullOrWhiteSpace(fstyle))
			{
				switch(fstyle)
				{
					case "italic":
						style = SKFontStyleSlant.Italic;
						break;
					case "oblique":
						style = SKFontStyleSlant.Oblique;
						break;
					case "normal":
						style = SKFontStyleSlant.Upright;
						break;
					default:
						style = defaultStyle;
						break;
				}
			}

			return style;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadFontWeight																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read and return the weight of the font.
		/// </summary>
		/// <param name="fontStyle">
		/// Collection of font style attributes to check.
		/// </param>
		/// <param name="defaultWeight">
		/// Default weight of the font, as an integer representation of the
		/// SKFontStyleWeight enumeration.
		/// </param>
		/// <returns>
		/// Weight of the font, as an integer representation of the
		/// SKFontStyleWeight enumeration.
		/// </returns>
		private int ReadFontWeight(Dictionary<string, string> fontStyle,
			int defaultWeight = (int)SKFontStyleWeight.Normal)
		{
			var weight = defaultWeight;

			if(fontStyle.TryGetValue("font-weight", out string fweight) &&
				!string.IsNullOrWhiteSpace(fweight) &&
				!int.TryParse(fweight, out weight))
			{
				switch(fweight)
				{
					case "normal":
						weight = (int)SKFontStyleWeight.Normal;
						break;
					case "bold":
						weight = (int)SKFontStyleWeight.Bold;
						break;
					case "bolder":
						weight = weight + 100;
						break;
					case "lighter":
						weight = weight - 100;
						break;
					default:
						weight = defaultWeight;
						break;
				}
			}

			return Math.Min(Math.Max((int)SKFontStyleWeight.Thin, weight),
				(int)SKFontStyleWeight.ExtraBlack);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadFontWidth																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read and return the font width, as an integer representation of the
		/// SKFontStyleWidth enumeration.
		/// </summary>
		/// <param name="fontStyle">
		/// Collection of font style attributes to read.
		/// </param>
		/// <param name="defaultWidth">
		/// Default font width, as an integer representation of the
		/// SKFontStyleWidth enumeration.
		/// </param>
		/// <returns>
		/// Font width, as an integer representation of the SKFontStyleWidth
		/// enumeration.
		/// </returns>
		private int ReadFontWidth(Dictionary<string, string> fontStyle,
			int defaultWidth = (int)SKFontStyleWidth.Normal)
		{
			var width = defaultWidth;
			if(fontStyle.TryGetValue("font-stretch", out string fwidth) &&
				!string.IsNullOrWhiteSpace(fwidth) && !int.TryParse(fwidth, out width))
			{
				switch(fwidth)
				{
					case "ultra-condensed":
						width = (int)SKFontStyleWidth.UltraCondensed;
						break;
					case "extra-condensed":
						width = (int)SKFontStyleWidth.ExtraCondensed;
						break;
					case "condensed":
						width = (int)SKFontStyleWidth.Condensed;
						break;
					case "semi-condensed":
						width = (int)SKFontStyleWidth.SemiCondensed;
						break;
					case "normal":
						width = (int)SKFontStyleWidth.Normal;
						break;
					case "semi-expanded":
						width = (int)SKFontStyleWidth.SemiExpanded;
						break;
					case "expanded":
						width = (int)SKFontStyleWidth.Expanded;
						break;
					case "extra-expanded":
						width = (int)SKFontStyleWidth.ExtraExpanded;
						break;
					case "ultra-expanded":
						width = (int)SKFontStyleWidth.UltraExpanded;
						break;
					case "wider":
						width = width + 1;
						break;
					case "narrower":
						width = width - 1;
						break;
					default:
						width = defaultWidth;
						break;
				}
			}

			return Math.Min(Math.Max((int)SKFontStyleWidth.UltraCondensed, width),
				(int)SKFontStyleWidth.UltraExpanded);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadGradient																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read the specified gradient and return it as a shader.
		/// </summary>
		/// <param name="element">
		/// Reference to an XML element containing def information.
		/// </param>
		/// <returns>
		/// Reference to a shader object.
		/// </returns>
		private SKShader ReadGradient(XElement element)
		{
			switch(element.Name.LocalName)
			{
				case "linearGradient":
					return ReadLinearGradient(element);
				case "radialGradient":
					return ReadRadialGradient(element);
			}
			return null;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadHref																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read the href child element from the specified element.
		/// </summary>
		/// <param name="element">
		/// Reference to an XML element containing a child with an href attribute.
		/// </param>
		/// <returns>
		/// Reference to an XML element with an href attribute.
		/// </returns>
		private XElement ReadHref(XElement element)
		{
			XElement child = null;
			string href = "";

			href = ReadHrefString(element);
			if(href?.Length > 0)
			{
				href = href.Substring(1);
			}
			if(string.IsNullOrEmpty(href) ||
				!mDefSection.TryGetValue(href, out child))
			{
				child = null;
			}
			return child;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadHrefString																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read the value of the href value in the specified element.
		/// </summary>
		/// <param name="element">
		/// Reference to an XML element containing an href attribute.
		/// </param>
		/// <returns>
		/// Value of the attribute element.
		/// </returns>
		private static string ReadHrefString(XElement element)
		{
			return (element.Attribute("href") ??
				element.Attribute(xlink + "href"))?.Value;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadImage																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read and return an SVG image.
		/// </summary>
		/// <param name="element">
		/// XML element containing the image directives.
		/// </param>
		/// <returns>
		/// Reference to an SVG image.
		/// </returns>
		private SKSvgImage ReadImage(XElement element)
		{
			var x = ReadNumber(element.Attribute("x"));
			var y = ReadNumber(element.Attribute("y"));
			var width = ReadNumber(element.Attribute("width"));
			var height = ReadNumber(element.Attribute("height"));
			var rect = SKRect.Create(x, y, width, height);

			byte[] bytes = null;

			var uri = ReadHrefString(element);
			if(uri != null)
			{
				if(uri.StartsWith("data:"))
				{
					bytes = ReadUriBytes(uri);
				}
				else
				{
					//	TODO: Read the image using any supported HTTP path method.
					//LogOrThrow($"Remote images are not supported");
					//	Create the 
				}
			}

			return new SKSvgImage(rect, uri, bytes);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadLine																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read and return a line.
		/// </summary>
		/// <param name="element">
		/// XML element containing the description of a line.
		/// </param>
		/// <returns>
		/// Reference to a line object.
		/// </returns>
		private SKLine ReadLine(XElement element)
		{
			var x1 = ReadNumber(element.Attribute("x1"));
			var x2 = ReadNumber(element.Attribute("x2"));
			var y1 = ReadNumber(element.Attribute("y1"));
			var y2 = ReadNumber(element.Attribute("y2"));

			return new SKLine(new SKPoint(x1, y1), new SKPoint(x2, y2));
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadLinearGradient																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read a linear gradient and return it as a shader.
		/// </summary>
		/// <param name="element">
		/// Reference to an XML element containing linear gradient description.
		/// </param>
		/// <returns>
		/// Reference to a shader object representing a linear gradient.
		/// </returns>
		private SKShader ReadLinearGradient(XElement element)
		{
			var startX = ReadNumber(element.Attribute("x1"));
			var startY = ReadNumber(element.Attribute("y1"));
			var endX = ReadNumber(element.Attribute("x2"));
			var endY = ReadNumber(element.Attribute("y2"));
			//var absolute =
			//	element.Attribute("gradientUnits")?.Value == "userSpaceOnUse";
			var tileMode = ReadSpreadMethod(element);
			var stops = ReadStops(element);

			// TODO: check gradientTransform attribute
			// TODO: use absolute

			return SKShader.CreateLinearGradient(
				new SKPoint(startX, startY),
				new SKPoint(endX, endY),
				stops.Values.ToArray(),
				stops.Keys.ToArray(),
				tileMode);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadNumber																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read the numeric representation of the specified style name.
		/// </summary>
		/// <param name="style">
		/// Collection of style attributes.
		/// </param>
		/// <param name="key">
		/// Name of the style to read.
		/// </param>
		/// <param name="defaultValue">
		/// Default value to use if the attribute was not found.
		/// </param>
		/// <returns>
		/// Float value equal to that found in the specified attribute, if found.
		/// Otherwise, the value of the defaultValue parameter.
		/// </returns>
		private float ReadNumber(Dictionary<string, string> style,
			string key, float defaultValue)
		{
			float value = defaultValue;
			if(style.TryGetValue(key, out string strValue))
			{
				value = ReadNumber(strValue);
			}
			return value;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the binary numeric representation of the specified string.
		/// </summary>
		/// <param name="raw">
		/// Raw numeric value, expressed as a string.
		/// </param>
		/// <returns>
		/// Floating point binary numeric value.
		/// </returns>
		private float ReadNumber(string raw)
		{
			if(string.IsNullOrWhiteSpace(raw))
				return 0;

			var s = raw.Trim();
			var m = 1.0f;

			if(mRxUnit.IsMatch(s))
			{
				if(s.EndsWith("in", StringComparison.Ordinal))
				{
					m = PixelsPerInch;
				}
				else if(s.EndsWith("cm", StringComparison.Ordinal))
				{
					m = PixelsPerInch / 2.54f;
				}
				else if(s.EndsWith("mm", StringComparison.Ordinal))
				{
					m = PixelsPerInch / 25.4f;
				}
				else if(s.EndsWith("pt", StringComparison.Ordinal))
				{
					m = PixelsPerInch / 72.0f;
				}
				else if(s.EndsWith("pc", StringComparison.Ordinal))
				{
					m = PixelsPerInch / 6.0f;
				}
				s = s.Substring(0, s.Length - 2);
			}
			else if(mRxPercent.IsMatch(s))
			{
				s = s.Substring(0, s.Length - 1);
				m = 0.01f;
			}

			if(!float.TryParse(s, NumberStyles.Float, iCulture, out float v))
			{
				v = 0;
			}

			return m * v;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Read the numeric value found in the value of the provided attribute.
		/// </summary>
		/// <param name="attribute">
		/// Reference to an XML attribute for which the value will be read.
		/// </param>
		/// <returns>
		/// Floating point binary numeric value.
		/// </returns>
		private float ReadNumber(XAttribute attribute) =>
			ReadNumber(attribute?.Value);
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadOpacity																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the current opacity level found in the provided style
		/// attributes.
		/// </summary>
		/// <param name="style">
		/// Reference to a collection of style attributes containing opacity for
		/// the level in focus.
		/// </param>
		/// <returns>
		/// An opacity value between 0.0 and 1.0.
		/// </returns>
		private float ReadOpacity(Dictionary<string, string> style)
		{
			float result = Math.Min(
				Math.Max(0.0f, ReadNumber(style, "opacity", 1.0f)), 1.0f);

			//	display:none is now handled properly in ReadElement
			//if(result > 0f)
			//{
			//	if(style.ContainsKey("display"))
			//	{
			//		//	Check for display: none.
			//		if(style["display"] == "none")
			//		{
			//			result = 0f;
			//		}
			//	}
			//}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadOptionalNumber																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the numeric value of the attribute if the attribute was present.
		/// </summary>
		/// <param name="attribute">
		/// Reference to an XML attribute potentially containing a numeric value.
		/// </param>
		/// <returns>
		/// A binary floating point number if attribute was present.
		/// Otherwise, null.
		/// </returns>
		private float? ReadOptionalNumber(XAttribute attribute) =>
			attribute == null ? (float?)null : ReadNumber(attribute.Value);
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadOval																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read and return an oval.
		/// </summary>
		/// <param name="element">
		/// XML element containing an oval description.
		/// </param>
		/// <returns>
		/// Reference to an oval object.
		/// </returns>
		private SKOval ReadOval(XElement element)
		{
			var cx = ReadNumber(element.Attribute("cx"));
			var cy = ReadNumber(element.Attribute("cy"));
			var rx = ReadNumber(element.Attribute("rx"));
			var ry = ReadNumber(element.Attribute("ry"));

			return new SKOval(new SKPoint(cx, cy), rx, ry);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadPaints																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read paint descriptions to set the current pen stroke and bucket fill
		/// styles.
		/// </summary>
		/// <param name="style">
		/// Collection of attributes to read.
		/// </param>
		/// <param name="strokePaint">
		/// Reference to the active pen stroke object to initialize.
		/// </param>
		/// <param name="fillPaint">
		/// Reference to the active bucket fill object to initialize.
		/// </param>
		/// <param name="isGroup">
		/// Value indicating whether the element is a group. If true,
		/// opacity is ignored.
		/// </param>
		private void ReadPaints(Dictionary<string, string> style,
			ref SKPaint strokePaint, ref SKPaint fillPaint, bool isGroup)
		{
			// get current element opacity, but ignore for groups (special case)
			float elementOpacity = isGroup ? 1.0f : ReadOpacity(style);

			// stroke
			var stroke = GetString(style, "stroke").Trim();
			if(stroke.Equals("none", StringComparison.OrdinalIgnoreCase))
			{
				strokePaint = null;
			}
			else
			{
				if(string.IsNullOrEmpty(stroke))
				{
					// no change
				}
				else
				{
					if(strokePaint == null)
						strokePaint = CreatePaint(true);

					if(ColorHelper.TryParse(stroke, out SKColor color))
					{
						// preserve alpha
						if(color.Alpha == 255)
							strokePaint.Color = color.WithAlpha(strokePaint.Color.Alpha);
						else
							strokePaint.Color = color;
					}
				}

				// stroke attributes
				var strokeDashArray = GetString(style, "stroke-dasharray");
				if(!string.IsNullOrWhiteSpace(strokeDashArray))
				{
					if("none".Equals(strokeDashArray, StringComparison.OrdinalIgnoreCase))
					{
						// remove any dash
						if(strokePaint != null)
							strokePaint.PathEffect = null;
					}
					else
					{
						if(strokePaint == null)
							strokePaint = CreatePaint(true);

						// get the dash
						var dashesStrings = strokeDashArray.Split(new[] { ' ', ',' },
							StringSplitOptions.RemoveEmptyEntries);
						var dashes = dashesStrings.Select(ReadNumber).ToArray();
						if(dashesStrings.Length % 2 == 1)
							dashes = dashes.Concat(dashes).ToArray();
						// get the offset
						var strokeDashOffset = ReadNumber(style, "stroke-dashoffset", 0);
						// set the effect
						strokePaint.PathEffect =
							SKPathEffect.CreateDash(dashes.ToArray(), strokeDashOffset);
					}
				}

				var strokeWidth = GetString(style, "stroke-width");
				if(!string.IsNullOrWhiteSpace(strokeWidth))
				{
					if(strokePaint == null)
						strokePaint = CreatePaint(true);
					strokePaint.StrokeWidth = ReadNumber(strokeWidth);
				}

				var strokeOpacity = GetString(style, "stroke-opacity");
				if(!string.IsNullOrWhiteSpace(strokeOpacity))
				{
					if(strokePaint == null)
						strokePaint = CreatePaint(true);
					strokePaint.Color = strokePaint.Color.WithAlpha(
						(byte)(ReadNumber(strokeOpacity) * 255));
				}

				if(strokePaint != null)
				{
					strokePaint.Color = strokePaint.Color.WithAlpha(
						(byte)(strokePaint.Color.Alpha * elementOpacity));
				}
			}

			// fill
			var fill = GetString(style, "fill").Trim();
			if(fill.Equals("none", StringComparison.OrdinalIgnoreCase))
			{
				fillPaint = null;
			}
			else
			{
				if(string.IsNullOrEmpty(fill))
				{
					// no change
				}
				else
				{
					if(fillPaint == null)
						fillPaint = CreatePaint();

					if(ColorHelper.TryParse(fill, out SKColor color))
					{
						// preserve alpha
						if(color.Alpha == 255)
							fillPaint.Color = color.WithAlpha(fillPaint.Color.Alpha);
						else
							fillPaint.Color = color;
					}
					else
					{
						var read = false;
						var urlM = mRxFillUrl.Match(fill);
						if(urlM.Success)
						{
							var id = urlM.Groups[1].Value.Trim();

							if(mDefSection.TryGetValue(id, out XElement defE))
							{
								var gradientShader = ReadGradient(defE);
								if(gradientShader != null)
								{
									// TODO: multiple shaders

									fillPaint.Shader = gradientShader;
									read = true;
								}
								// else try another type (eg: image)
							}
							else
							{
								LogOrThrow($"Invalid fill url reference: {id}");
							}
						}

						if(!read)
						{
							LogOrThrow($"Unsupported fill: {fill}");
						}
					}
				}

				// fill attributes
				var fillOpacity = GetString(style, "fill-opacity");
				if(!string.IsNullOrWhiteSpace(fillOpacity))
				{
					if(fillPaint == null)
						fillPaint = CreatePaint();

					fillPaint.Color = fillPaint.Color.WithAlpha(
						(byte)(ReadNumber(fillOpacity) * 255));
				}

				if(fillPaint != null)
				{
					fillPaint.Color = fillPaint.Color.WithAlpha(
						(byte)(fillPaint.Color.Alpha * elementOpacity));
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Read paint descriptions to set the current pen stroke and bucket fill
		/// styles.
		/// </summary>
		/// <param name="element">
		/// Reference to the XML element to inspect.
		/// </param>
		/// <param name="strokePaint">
		/// Reference to the active pen stroke object to initialize.
		/// </param>
		/// <param name="fillPaint">
		/// Reference to the active bucket fill object to initialize.
		/// </param>
		/// <param name="isGroup">
		/// Value indicating whether the element is a group. If true,
		/// opacity is ignored.
		/// </param>
		/// <returns>
		/// Reference to a collection of attributes found in the XML element.
		/// </returns>
		private Dictionary<string, string> ReadPaints(XElement element,
			ref SKPaint strokePaint, ref SKPaint fillPaint, bool isGroup)
		{
			var style = ReadStyle(element);
			ReadPaints(style, ref strokePaint, ref fillPaint, isGroup);
			return style;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadRadialGradient																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read the specified radial gradient and return it as a shader.
		/// </summary>
		/// <param name="element">
		/// Reference to an XML element containing the description of the gradient.
		/// </param>
		/// <returns>
		/// Reference to a shader object with the specified gradient value.
		/// </returns>
		private SKShader ReadRadialGradient(XElement element)
		{
			var centerX = ReadNumber(element.Attribute("cx"));
			var centerY = ReadNumber(element.Attribute("cy"));
			//var focusX = ReadOptionalNumber(element.Attribute("fx")) ?? centerX;
			//var focusY = ReadOptionalNumber(element.Attribute("fy")) ?? centerY;
			var radius = ReadNumber(element.Attribute("r"));
			//var absolute =
			//	element.Attribute("gradientUnits")?.Value == "userSpaceOnUse";
			var tileMode = ReadSpreadMethod(element);
			var stops = ReadStops(element);

			// TODO: check gradientTransform attribute
			// TODO: use absolute

			return SKShader.CreateRadialGradient(
				new SKPoint(centerX, centerY),
				radius,
				stops.Values.ToArray(),
				stops.Keys.ToArray(),
				tileMode);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadRectangle																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read and return a rectangle from a raw description.
		/// </summary>
		/// <param name="raw">
		/// Raw description of the rectangle.
		/// </param>
		/// <returns>
		/// Reference to a rectangle object.
		/// </returns>
		private SKRect ReadRectangle(string raw)
		{
			var r = new SKRect();
			var p = raw.Split(mWhitespace, StringSplitOptions.RemoveEmptyEntries);
			if(p.Length > 0)
				r.Left = ReadNumber(p[0]);
			if(p.Length > 1)
				r.Top = ReadNumber(p[1]);
			if(p.Length > 2)
				r.Right = r.Left + ReadNumber(p[2]);
			if(p.Length > 3)
				r.Bottom = r.Top + ReadNumber(p[3]);
			return r;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadRoundedRect																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read and return a rounded rectangle.
		/// </summary>
		/// <param name="element">
		/// XML element containing the description of a rounded rectangle.
		/// </param>
		/// <returns>
		/// Reference to a rounded rectangle object.
		/// </returns>
		private SKRoundedRect ReadRoundedRect(XElement element)
		{
			var x = ReadNumber(element.Attribute("x"));
			var y = ReadNumber(element.Attribute("y"));
			var width = ReadNumber(element.Attribute("width"));
			var height = ReadNumber(element.Attribute("height"));
			var rx = ReadOptionalNumber(element.Attribute("rx"));
			var ry = ReadOptionalNumber(element.Attribute("ry"));
			var rect = SKRect.Create(x, y, width, height);

			return new SKRoundedRect(rect, rx ?? ry ?? 0, ry ?? rx ?? 0);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadSpreadMethod																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return an SKShaderTileMode enumeration value representing the type
		/// of spread method found in the caller's element.
		/// </summary>
		/// <param name="element">
		/// Reference to an XML element containing a spreadMethod attribute.
		/// </param>
		/// <returns>
		/// SKShaderTileMode enumeration value representing the observed spread
		/// method.
		/// </returns>
		private static SKShaderTileMode ReadSpreadMethod(XElement element)
		{
			var repeat = element.Attribute("spreadMethod")?.Value;
			switch(repeat)
			{
				case "reflect":
					return SKShaderTileMode.Mirror;
				case "repeat":
					return SKShaderTileMode.Repeat;
				case "pad":
				default:
					return SKShaderTileMode.Clamp;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadStops																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a collection of stop values found in the provided element.
		/// </summary>
		/// <param name="element">
		/// Reference to an XML element containing stop values.
		/// </param>
		/// <returns>
		/// Reference to a collection of stop values found in the element.
		/// </returns>
		private SortedDictionary<float, SKColor> ReadStops(XElement element)
		{
			var stops = new SortedDictionary<float, SKColor>();

			var ns = element.Name.Namespace;
			foreach(var se in element.Elements(ns + "stop"))
			{
				var style = ReadStyle(se);

				var offset = ReadNumber(style["offset"]);
				var color = SKColors.Black;
				byte alpha = 255;

				if(style.TryGetValue("stop-color", out string stopColor))
				{
					// preserve alpha
					if(ColorHelper.TryParse(stopColor, out color) && color.Alpha == 255)
					{
						alpha = color.Alpha;
					}
				}

				if(style.TryGetValue("stop-opacity", out string stopOpacity))
				{
					alpha = (byte)(ReadNumber(stopOpacity) * 255);
				}

				color = color.WithAlpha(alpha);
				stops[offset] = color;
			}

			return stops;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadStyle																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a collection containing the attributes of the specified style
		/// source.
		/// </summary>
		/// <param name="style">
		/// Source style.
		/// </param>
		/// <returns>
		/// Reference to a collection of style attributes.
		/// </returns>
		private Dictionary<string, string> ReadStyle(string style)
		{
			var d = new Dictionary<string, string>();
			var kvs = style.Split(new[] { ';' },
				StringSplitOptions.RemoveEmptyEntries);
			foreach(var kv in kvs)
			{
				var m = mRxKeyValue.Match(kv);
				if(m.Success)
				{
					var k = m.Groups[1].Value;
					var v = m.Groups[2].Value;
					d[k] = v;
				}
			}
			return d;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a collection containing the attributes of the specified
		/// XML element.
		/// </summary>
		/// <param name="element">
		/// Reference to an XML element containing attributes.
		/// </param>
		/// <returns>
		/// Reference to a collection of style attributes.
		/// </returns>
		private Dictionary<string, string> ReadStyle(XElement element)
		{
			// Get style from local attributes.
			Dictionary<string, string> dic =
				element.Attributes().Where(a => HasSvgNamespace(a.Name)).
				ToDictionary(k => k.Name.LocalName, v => v.Value);

			string style = element.Attribute("style")?.Value;
			if(!string.IsNullOrWhiteSpace(style))
			{
				// get from stlye attribute
				Dictionary<string, string> styleDic = ReadStyle(style);

				// overwrite
				foreach(KeyValuePair<string, string> pair in styleDic)
				{
					dic[pair.Key] = pair.Value;
				}
			}

			return dic;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadText																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read and return text.
		/// </summary>
		/// <param name="e">
		/// Reference to an XML element containing the description of text.
		/// </param>
		/// <param name="stroke">
		/// Reference to the active pen stroke.
		/// </param>
		/// <param name="fill">
		/// Reference to the active bucket fill.
		/// </param>
		/// <returns>
		/// Reference to a text object.
		/// </returns>
		private SKText ReadText(XElement e, SKPaint stroke, SKPaint fill)
		{
			// TODO: stroke

			float x = ReadNumber(e.Attribute("x"));
			float y = ReadNumber(e.Attribute("y"));
			SKPoint xy = new SKPoint(x, y);
			SKTextAlign textAlign = ReadTextAlignment(e);
			float baselineShift = ReadBaselineShift(e);

			ReadFontAttributes(e, fill);

			return ReadTextSpans(e, xy, textAlign, baselineShift, stroke, fill);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadTextAlignment																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read the text alignment found in the specified element.
		/// </summary>
		/// <param name="element">
		/// Reference to an XML element containing text aligment description.
		/// </param>
		/// <returns>
		/// Reference to a text alignment object.
		/// </returns>
		private SKTextAlign ReadTextAlignment(XElement element)
		{
			string value = null;
			if(element != null)
			{
				var attrib = element.Attribute("text-anchor");
				if(attrib != null && !string.IsNullOrWhiteSpace(attrib.Value))
					value = attrib.Value;
				else
				{
					var style = element.Attribute("style");
					if(style != null && !string.IsNullOrWhiteSpace(style.Value))
					{
						value = GetString(ReadStyle(style.Value), "text-anchor");
					}
				}
			}

			switch(value)
			{
				case "end":
					return SKTextAlign.Right;
				case "middle":
					return SKTextAlign.Center;
				default:
					return SKTextAlign.Left;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadTextSpans																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read and return the contents of text spans.
		/// </summary>
		/// <param name="e">
		/// Reference to an XML element containing the description of text span or
		/// spans.
		/// </param>
		/// <param name="xy">
		/// Reference to the X, Y point at which the span is based.
		/// </param>
		/// <param name="textAlign">
		/// Reference to the alignment type for the resulting text.
		/// </param>
		/// <param name="baselineShift">
		/// Amount to shift the baseline, in user units.
		/// </param>
		/// <param name="stroke">
		/// Reference to the active pen stroke.
		/// </param>
		/// <param name="fill">
		/// Reference to the active bucket fill.
		/// </param>
		/// <returns>
		/// Reference to a text object built from the specified spans.
		/// </returns>
		private SKText ReadTextSpans(XElement e, SKPoint xy,
			SKTextAlign textAlign, float baselineShift, SKPaint stroke, SKPaint fill)
		{
			string text = null;
			SKPaint spanFill = null;
			SKText spans = new SKText(xy, textAlign);
			SKText spansi = null;
			float? x = null;
			float? y = null;
			SKPoint xyNew = SKPoint.Empty;

			//	textAlign is used for all spans within the <text> element.
			//	If different textAligns would be needed, it is necessary to use
			//	several <text> elements instead of <tspan> elements
			float currentBaselineShift = baselineShift;
			fill.TextAlign = SKTextAlign.Left;  // fixed alignment for all spans

			XNode[] nodes = e.Nodes().ToArray();
			for(int i = 0; i < nodes.Length; i++)
			{
				var c = nodes[i];
				bool isFirst = i == 0;
				bool isLast = i == nodes.Length - 1;

				if(c.NodeType == XmlNodeType.Text)
				{
					// TODO: check for preserve whitespace

					string[] textSegments =
						((XText)c).Value.Split(new[] { '\n', '\r' },
						StringSplitOptions.RemoveEmptyEntries);
					int count = textSegments.Length;
					if(count > 0)
					{
						if(isFirst)
						{
							textSegments[0] = textSegments[0].TrimStart();
						}
						if(isLast)
						{
							textSegments[count - 1] = textSegments[count - 1].TrimEnd();
						}
						text = mRxWhitespace.Replace(string.Concat(textSegments), " ");

						spans.Append(new SKTextSpan(text, fill.Clone(),
							baselineShift: currentBaselineShift));
					}
				}
				else if(c.NodeType == XmlNodeType.Element)
				{
					XElement ce = (XElement)c;
					if(ce.Name.LocalName == "tspan")
					{
						//	The current span may want to change the cursor position
						//	and styles.
						x = ReadOptionalNumber(ce.Attribute("x"));
						y = ReadOptionalNumber(ce.Attribute("y"));
						text = ce.Value; //.Trim();

						xyNew = new SKPoint(
							x != null ? (float)x : xy.X, y != null ? (float)y : xy.Y);

						spanFill = fill.Clone();
						ReadFontAttributes(ce, spanFill);

						//	Don't read text-anchor from tspans.
						//	Only use enclosing text-anchor from text element.
						currentBaselineShift = ReadBaselineShift(ce);
						if(ce.HasElements)
						{
							spansi = ReadTextSpans(ce, xyNew,
								textAlign, currentBaselineShift, stroke, fill);
							if(spansi != null)
							{
								foreach(SKTextSpan spani in spansi)
								{
									spans.Append(spani);
								}
							}
						}
						else
						{
							spans.Append(
								new SKTextSpan(text, spanFill, xyNew.X, xyNew.Y,
									currentBaselineShift));
						}
					}
				}
			}
			return spans;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadTransform																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a matrix containing the details of the caller's SVG transform
		/// description.
		/// </summary>
		/// <param name="raw">
		/// Raw transform attribute description.
		/// </param>
		/// <returns>
		/// Reference to a matrix initialized to calculated the specified
		/// transform.
		/// </returns>
		private SKMatrix ReadTransform(string raw)
		{
			//var t = SKMatrix.MakeIdentity();
			var t = SKMatrix.CreateIdentity();

			if(string.IsNullOrWhiteSpace(raw))
			{
				return t;
			}

			var calls = raw.Trim().Split(new[] { ')' },
				StringSplitOptions.RemoveEmptyEntries);
			foreach(var c in calls)
			{
				var args = c.Split(new[] { '(', ',', ' ', '\t', '\r', '\n' },
					StringSplitOptions.RemoveEmptyEntries);
				//var nt = SKMatrix.MakeIdentity();
				var nt = SKMatrix.CreateIdentity();
				switch(args[0])
				{
					case "matrix":
						if(args.Length == 7)
						{
							nt.Values = new float[]
							{
								ReadNumber(args[1]), ReadNumber(args[3]), ReadNumber(args[5]),
								ReadNumber(args[2]), ReadNumber(args[4]), ReadNumber(args[6]),
								0, 0, 1
							};
						}
						else
						{
							LogOrThrow(
								"Matrices are expected to have 6 elements. " +
								$"This one has {args.Length - 1}");
						}
						break;
					case "translate":
						//	TODO: Override local x,y on translate.
						//	In SVG, translate overrides local x, y instead of
						//	augmenting it.
						if(args.Length >= 3)
						{
							//nt = SKMatrix.MakeTranslation(
							//	ReadNumber(args[1]), ReadNumber(args[2]));
							nt = SKMatrix.CreateTranslation(
								ReadNumber(args[1]), ReadNumber(args[2]));
						}
						else if(args.Length >= 2)
						{
							//nt = SKMatrix.MakeTranslation(ReadNumber(args[1]), 0);
							nt = SKMatrix.CreateTranslation(ReadNumber(args[1]), 0);
						}
						break;
					case "scale":
						if(args.Length >= 3)
						{
							//nt = SKMatrix.MakeScale(
							//	ReadNumber(args[1]), ReadNumber(args[2]));
							nt = SKMatrix.CreateScale(
								ReadNumber(args[1]), ReadNumber(args[2]));
						}
						else if(args.Length >= 2)
						{
							var sx = ReadNumber(args[1]);
							//nt = SKMatrix.MakeScale(sx, sx);
							nt = SKMatrix.CreateScale(sx, sx);
						}
						break;
					case "rotate":
						var a = ReadNumber(args[1]);
						if(args.Length >= 4)
						{
							var x = ReadNumber(args[2]);
							var y = ReadNumber(args[3]);
							//var t1 = SKMatrix.MakeTranslation(x, y);
							var t1 = SKMatrix.CreateTranslation(x, y);
							//var t2 = SKMatrix.MakeRotationDegrees(a);
							var t2 = SKMatrix.CreateRotationDegrees(a);
							//var t3 = SKMatrix.MakeTranslation(-x, -y);
							var t3 = SKMatrix.CreateTranslation(-x, -y);
							SKMatrix.Concat(ref nt, ref t1, ref t2);
							SKMatrix.Concat(ref nt, ref nt, ref t3);
						}
						else
						{
							//nt = SKMatrix.MakeRotationDegrees(a);
							nt = SKMatrix.CreateRotationDegrees(a);
						}
						break;
					default:
						LogOrThrow($"Can't transform {args[0]}");
						break;
				}
				SKMatrix.Concat(ref t, ref t, ref nt);
			}

			return t;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ReadUriBytes																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Read a data URI into a byte buffer.
		/// </summary>
		/// <param name="uri">
		/// Data URI string containing formatted base64 data.
		/// </param>
		/// <returns>
		/// Reference to a byte array containing the binary data.
		/// </returns>
		private byte[] ReadUriBytes(string uri)
		{
			if(!string.IsNullOrEmpty(uri))
			{
				var offset = uri.IndexOf(",");
				if(offset != -1 && offset - 1 < uri.Length)
				{
					uri = uri.Substring(offset + 1);
					return Convert.FromBase64String(uri);
				}
			}

			return null;
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
#if PORTABLE
		/// <summary>
		/// Create a new instance of the SKSvg Item.
		/// </summary>
		static SKSvg()
		{
			//	Try to find a method with the signature
			//	'Create(string, XmlReaderSettings, XmlParserContext)'
			mCreateReaderMethod = typeof(XmlReader).GetRuntimeMethod(
				nameof(XmlReader.Create),
				new[] {
					typeof(string),
					typeof(XmlReaderSettings),
					typeof(XmlParserContext)
				});
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
#endif
		/// <summary>
		/// Create a new instance of the SKSvg object.
		/// </summary>
		public SKSvg() : this(mDefaultPPI, SKSize.Empty)
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the SKSvg object.
		/// </summary>
		/// <param name="pixelsPerInch">
		/// Number of pixels per inch to use when rendering physical.
		/// </param>
		public SKSvg(float pixelsPerInch) : this(pixelsPerInch, SKSize.Empty)
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the SKSvg object.
		/// </summary>
		/// <param name="canvasSize">
		/// Starting size of the image canvas.
		/// </param>
		public SKSvg(SKSize canvasSize) : this(mDefaultPPI, canvasSize)
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the SKSvg object.
		/// </summary>
		/// <param name="pixelsPerInch">
		/// Number of pixels per inch to use when rendering physical.
		/// </param>
		/// <param name="canvasSize">
		/// Starting size of the image canvas.
		/// </param>
		public SKSvg(float pixelsPerInch, SKSize canvasSize)
		{
			CanvasSize = canvasSize;
			PixelsPerInch = pixelsPerInch;
			//ThrowOnUnsupportedElement = mErrorOnUnsupportedElementFound;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	BaseFolder																														*
		//*-----------------------------------------------------------------------*
		private string mBaseFolder = "";
		/// <summary>
		/// Get/Set the name of the base folder or domain name from which this SVG
		/// is loaded.
		/// </summary>
		/// <remarks>
		/// <para>This property provides absolute reference for any elements that
		/// require remote and relative linking.</para>
		/// <para>When Load(string) is called for a filename, this value is set
		/// automatically.</para>
		/// </remarks>
		public string BaseFolder
		{
			get { return mBaseFolder; }
			set { mBaseFolder = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	CanvasSize																														*
		//*-----------------------------------------------------------------------*
		private SKSize mCanvasSize = new SKSize();
		/// <summary>
		/// Get/Set a reference to the canvas size within which the view is
		/// clipped.
		/// </summary>
		public SKSize CanvasSize
		{
			get { return mCanvasSize; }
			set { mCanvasSize = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Clear																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Clear the contents of this control.
		/// </summary>
		public void Clear()
		{
			mPicture = null;
			mDocument = null;
			mIsDirty = false;
			mNeedsInvalidation = false;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Description																														*
		//*-----------------------------------------------------------------------*
		private string mDescription = "";
		/// <summary>
		/// Get/Set the description of the loaded flie.
		/// </summary>
		public string Description
		{
			get { return mDescription; }
			set { mDescription = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Document																															*
		//*-----------------------------------------------------------------------*
		private XDocument mDocument = null;
		/// <summary>
		/// Get/Set a reference to the active XML document representing the
		/// loaded SVG file.
		/// </summary>
		public XDocument Document
		{
			get { return mDocument; }
			set { mDocument = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetElementAttributes																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the attributes for the specified element ID.
		/// </summary>
		/// <param name="elementID">
		/// Unique ID of the element within the document.
		/// </param>
		/// <returns>
		/// Reference to a collection of attributes for the specified element.
		/// If the element or attributes were not found, the collection is empty.
		/// </returns>
		public Dictionary<string, string> GetElementAttributes(string elementID)
		{
			XElement element = null;
			List<XElement> elements = null;
			Dictionary<string, string> result = new Dictionary<string, string>();

			if(mDocument != null && elementID?.Length > 0)
			{
				elements = mDocument.Descendants().
					Where(x => (string)x.Attribute("id") == elementID).ToList();
				if(elements?.Count > 0)
				{
					element = elements[0];
				}
				if(element != null)
				{
					//	Element found.
					foreach(XAttribute attribute in element.Attributes())
					{
						result.Add(attribute.Name.LocalName, attribute.Value);
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Height																																*
		//*-----------------------------------------------------------------------*
		private float mHeight = 0F;
		/// <summary>
		/// Get/Set the specified image height for this document.
		/// </summary>
		public float Height
		{
			get { return mHeight; }
			set { mHeight = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	IsDirty																																*
		//*-----------------------------------------------------------------------*
		private bool mIsDirty = false;
		/// <summary>
		/// Get/Set a value indicating whether the data in this document has been
		/// changed since the last time it was saved.
		/// </summary>
		public bool IsDirty
		{
			get { return mIsDirty; }
			set { mIsDirty = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Load																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Load and return a vector picture from a generic stream.
		/// </summary>
		/// <param name="stream">
		/// Generic stream assumed to contain XML data adhering to the SVG
		/// specification.
		/// </param>
		/// <returns>
		/// Reference to a vector-based picture.
		/// </returns>
		public SKPicture Load(Stream stream)
		{
			using(var reader = XmlReader.Create(
				stream, mXmlReaderSettings, CreateSvgXmlContext()))
			{
				return Load(reader);
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Load and return a vector picture from the path and filename of an SVG
		/// file.
		/// </summary>
		/// <param name="filename">
		/// Name of an SVG file to load.
		/// </param>
		/// <returns>
		/// Reference to a vector-based picture.
		/// </returns>
		/// <remarks>
		/// This method sets the value of the BaseFolder property.
		/// </remarks>
		public SKPicture Load(string filename)
		{
#if PORTABLE
			//	PCL does not have the ability to read a file and use a context.
			if(mCreateReaderMethod == null)
			{
				return Load(XDocument.Load(filename));
			}

			// we know that there we can access the method via reflection
			var args = new object[]
			{
				filename, mXmlReaderSettings, CreateSvgXmlContext()
			};
			using(var reader = (XmlReader)mCreateReaderMethod.Invoke(null, args))
			{
				return Load(reader);
			}
#else
			using(var stream = File.OpenRead(filename))
			{
				return Load(stream);
			}
#endif
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Load and return a vector picture from an SVG compliant XML stream.
		/// </summary>
		/// <param name="document">
		/// Reference to an XML document to process.
		/// </param>
		/// <returns>
		/// Reference to a vector-based picture.
		/// </returns>
		public SKPicture Load(XDocument document)
		{
			var svg = document.Root;
			var ns = svg.Name.Namespace;

			mDocument = document;

			// find the defs (gradients) - and follow all hrefs
			foreach(var d in svg.Descendants())
			{
				var id = d.Attribute("id")?.Value?.Trim();
				if(!string.IsNullOrEmpty(id))
					mDefSection[id] = ReadDefinition(d);
			}

			Version = svg.Attribute("version")?.Value;
			Title = svg.Element(ns + "title")?.Value;
			Description =
				svg.Element(ns + "desc")?.Value ??
				svg.Element(ns + "description")?.Value;

			// TODO: parse the "preserveAspectRatio" values properly
			var preserveAspectRatio =
				svg.Attribute("preserveAspectRatio")?.Value;

			// get the SVG dimensions
			var viewBoxA = svg.Attribute("viewBox") ?? svg.Attribute("viewPort");
			if(viewBoxA != null)
			{
				ViewBox = ReadRectangle(viewBoxA.Value);
			}

			if(CanvasSize.IsEmpty)
			{
				// get the user dimensions
				var widthA = svg.Attribute("width");
				var heightA = svg.Attribute("height");
				var width = ReadNumber(widthA);
				var height = ReadNumber(heightA);
				var size = new SKSize(width, height);

				mWidth = width;
				mHeight = height;

				if(widthA == null)
				{
					size.Width = ViewBox.Width;
				}
				else if(widthA.Value.Contains("%"))
				{
					size.Width *= ViewBox.Width;
				}
				if(heightA == null)
				{
					size.Height = ViewBox.Height;
				}
				else if(heightA != null && heightA.Value.Contains("%"))
				{
					size.Height *= ViewBox.Height;
				}

				// set the property
				CanvasSize = size;
			}

			// create the picture from the elements
			using(var recorder = new SKPictureRecorder())
			{
				using(var canvas = recorder.BeginRecording(SKRect.Create(CanvasSize)))
				{
					//	If there is no viewbox, then we don't do anything, otherwise
					//	scale the SVG dimensions to fit inside the user dimensions
					if(!ViewBox.IsEmpty &&
						(ViewBox.Width != CanvasSize.Width ||
						ViewBox.Height != CanvasSize.Height))
					{
						if(preserveAspectRatio == "none")
						{
							canvas.Scale(
								CanvasSize.Width / ViewBox.Width,
								CanvasSize.Height / ViewBox.Height);
						}
						else
						{
							// TODO: just center scale for now
							var scale = Math.Min(
								CanvasSize.Width / ViewBox.Width,
								CanvasSize.Height / ViewBox.Height);
							var centered = SKRect.Create(CanvasSize).AspectFit(ViewBox.Size);
							canvas.Translate(centered.Left, centered.Top);
							canvas.Scale(scale, scale);
						}
					}

					// translate the canvas by the viewBox origin
					canvas.Translate(-ViewBox.Left, -ViewBox.Top);

					// if the viewbox was specified, then crop to that
					if(!ViewBox.IsEmpty)
					{
						canvas.ClipRect(ViewBox);
					}

					// read style
					SKPaint stroke = null;
					SKPaint fill = CreatePaint();
					var style = ReadPaints(svg, ref stroke, ref fill, true);

					// read elements
					LoadElements(svg.Elements(), canvas, stroke, fill);

					Picture = recorder.EndRecording();
				}
			}

			return Picture;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Load and return a vector picture from an SVG compliant XML stream.
		/// </summary>
		/// <param name="reader">
		/// Reference to an XML reader, configured to read the SVG data.
		/// </param>
		/// <returns>
		/// Reference to a vector-based picture.
		/// </returns>
		public SKPicture Load(XmlReader reader)
		{
			return Load(XDocument.Load(reader));
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* LoadFromString																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Load the SVG from string content.
		/// </summary>
		/// <param name="content">
		/// Fully qualified SVG file content.
		/// </param>
		/// <returns>
		/// Reference to a vector picture object.
		/// </returns>
		public SKPicture LoadFromString(string content)
		{
			XmlReader reader = XmlReader.Create(new StringReader(content));
			return Load(reader);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NeedsInvalidation																											*
		//*-----------------------------------------------------------------------*
		private bool mNeedsInvalidation = false;
		/// <summary>
		/// Get/Set a value indicating whether any associated controls require
		/// invalidation.
		/// </summary>
		public bool NeedsInvalidation
		{
			get { return mNeedsInvalidation; }
			set { mNeedsInvalidation = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Picture																																*
		//*-----------------------------------------------------------------------*
		private SKPicture mPicture = null;
		/// <summary>
		/// Get/Set a reference to the picture rendered from the SVG data.
		/// </summary>
		public SKPicture Picture
		{
			get { return mPicture; }
			set { mPicture = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PixelsPerInch																													*
		//*-----------------------------------------------------------------------*
		private float mPixelsPerInch = 0;
		/// <summary>
		/// Get/Set the resolution of physical renders, in pixels per inch.
		/// </summary>
		public float PixelsPerInch
		{
			get { return mPixelsPerInch; }
			set { mPixelsPerInch = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Refresh																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Refresh the picture by re-reading the local document.
		/// </summary>
		/// <returns>
		/// Reference to a vector picture object.
		/// </returns>
		public SKPicture Refresh()
		{
			if(mDocument != null)
			{
				mPicture = Load(mDocument);
			}
			return mPicture;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SavePicture																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Save the current picture to the specified file.
		/// </summary>
		/// <param name="filename">
		/// Name of the file to which the picture will be written.
		/// </param>
		public void SavePicture(string filename)
		{
			SKBitmap bitmap = null;
			SKCanvas canvas = null;
			SKData data = null;
			FileInfo file = null;

			if(mPicture != null && filename?.Length > 0)
			{
				file = new FileInfo(filename);
				bitmap = new SKBitmap((int)mWidth, (int)mHeight);
				canvas = new SKCanvas(bitmap);
				//	Draw picture full size.
				canvas.DrawPicture(mPicture);
				using(Stream s = File.OpenWrite(filename))
				{
					switch(file.Extension.ToLower())
					{
						case ".bmp":
							data = SKImage.FromBitmap(bitmap).Encode(
								SKEncodedImageFormat.Bmp, 100);
							break;
						case ".jpg":
						case ".jpeg":
							data = SKImage.FromBitmap(bitmap).Encode(
								SKEncodedImageFormat.Jpeg, 90);
							break;
						case ".png":
							data = SKImage.FromBitmap(bitmap).Encode(
								SKEncodedImageFormat.Png, 100);
							break;
					}
					if(data != null)
					{
						data.SaveTo(s);
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SetElementAttr																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the value of an element's attribute.
		/// </summary>
		/// <param name="elementID">
		/// Unique ID of the element within the document.
		/// </param>
		/// <param name="attributeName">
		/// Name of the attribute to set.
		/// </param>
		/// <param name="attributeValue">
		///	Value of the attribute.
		/// </param>
		public void SetElementAttr(string elementID,
			string attributeName, string attributeValue)
		{
			IEnumerable<XAttribute> attributes = null;
			bool bFound = false;
			XElement element = null;
			List<XElement> elements = null;
			XAttribute newAttribute = null;

			if(mDocument != null && elementID?.Length > 0)
			{
				elements = mDocument.Descendants().
					Where(x => (string)x.Attribute("id") == elementID).ToList();
				if(elements?.Count > 0)
				{
					element = elements[0];
				}
				if(element != null)
				{
					//	Element found.
					attributes = element.Attributes();
					foreach(XAttribute attribute in attributes)
					{
						if(attribute.Name.LocalName == attributeName)
						{
							attribute.Value = attributeValue;
							bFound = true;
							mIsDirty = true;
							mNeedsInvalidation = true;
							break;
						}
					}
					if(!bFound)
					{
						newAttribute = new XAttribute(attributeName, attributeValue);
						element.Add(newAttribute);
						mIsDirty = true;
						mNeedsInvalidation = true;
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SetElementCss																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the value of an element's css attribute.
		/// </summary>
		/// <param name="elementID">
		/// Unique ID of the element within the document.
		/// </param>
		/// <param name="attributeName">
		/// Name of the CSS attribute to set.
		/// </param>
		/// <param name="attributeValue">
		///	Value of the CSS attribute.
		/// </param>
		public void SetElementCss(string elementID,
			string attributeName, string attributeValue)
		{
			IEnumerable<XAttribute> attributes = null;
			bool bFound = false;
			StringBuilder builder = null;
			XElement element = null;
			List<XElement> elements = null;
			Dictionary<string, string> entries = null;
			string name = "";
			XAttribute newAttribute = null;
			string value = "";
			string[] values = null;

			if(mDocument != null && elementID?.Length > 0)
			{
				elements = mDocument.Descendants().
					Where(x => (string)x.Attribute("id") == elementID).ToList();
				if(elements?.Count > 0)
				{
					element = elements[0];
				}
				if(element != null)
				{
					//	Element found.
					attributes = element.Attributes();
					foreach(XAttribute attribute in attributes)
					{
						if(attribute.Name.LocalName == "style")
						{
							//	Style found.
							bFound = true;
							builder = new StringBuilder();
							entries = new Dictionary<string, string>();
							if(attribute.Value?.Length > 0)
							{
								values = attribute.Value.Split(new char[] { ';' });
								if(values?.Length > 0)
								{
									foreach(string nameValue in values)
									{
										name = "";
										value = "";
										if(nameValue.IndexOf(':') >= 0)
										{
											//	Name and value present.
											name = nameValue.
												Substring(0, nameValue.IndexOf(':')).Trim();
											value = nameValue.
												Substring(nameValue.IndexOf(':') + 1).Trim();
										}
										else
										{
											//	Name only.
											name = nameValue;
											value = null;
										}
										entries.Add(name, value);
									}
								}
							}
							if(entries.ContainsKey(attributeName))
							{
								//	The style is present.
								entries[attributeName] = attributeValue;
							}
							else
							{
								//	The style needs to be added.
								entries.Add(attributeName, attributeValue);
							}
							foreach(KeyValuePair<string, string> entry in entries)
							{
								if(builder.Length > 0)
								{
									builder.Append(";");
								}
								if(entry.Value == null)
								{
									//	If the value is null, then not name:value.
									builder.Append(entry.Key);
								}
								else
								{
									builder.Append($"{entry.Key}:{entry.Value}");
								}
							}
							attribute.Value = builder.ToString();
							mIsDirty = true;
							mNeedsInvalidation = true;
							break;
						}
					}
					if(!bFound)
					{
						//	If the style attribute was not found, add it.
						if(attributeValue == null)
						{
							newAttribute = new XAttribute("style", $"{attributeName};");
						}
						else
						{
							newAttribute =
								new XAttribute("style", $"{attributeName}:{attributeValue};");
						}
						element.Add(newAttribute);
						mIsDirty = true;
						mNeedsInvalidation = true;
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SetElementRotation																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the rotation translation for the specified element.
		/// </summary>
		/// <param name="elementID">
		/// Unique ID of the element to locate.
		/// </param>
		/// <param name="rotation">
		/// Rotation amount.
		/// </param>
		/// <param name="xRef">
		/// X coordinate reference of the object.
		/// </param>
		/// <param name="yRef">
		/// Y coordinate reference of the object.
		/// </param>
		/// <param name="widthRef">
		/// Width of the object.
		/// </param>
		/// <param name="heightRef">
		/// Height of the object.
		/// </param>
		/// <remarks>
		/// SVG has a locked dependency relation between its location, size, and
		/// rotation. Whenever location or size are changed, the rotation must be
		/// updated.
		/// </remarks>
		public void SetElementRotation(string elementID, string rotation,
			float xRef, float yRef, float widthRef, float heightRef)
		{
			SetElementAttr(elementID, "transform",
				$"rotate({rotation} " +
					$"{xRef + (widthRef / 2.0)} " +
					$"{yRef + (heightRef / 2.0)}" +
					")");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ThrowOnUnsupportedElement																							*
		//*-----------------------------------------------------------------------*
		private bool mThrowOnUnsupportedElement = false;
		/// <summary>
		/// Get/Set a value indicating whether to throw an exception when an
		/// unsupported element is encountered.
		/// </summary>
		public bool ThrowOnUnsupportedElement
		{
			get { return mThrowOnUnsupportedElement; }
			set { mThrowOnUnsupportedElement = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Title																																	*
		//*-----------------------------------------------------------------------*
		private string mTitle = "";
		/// <summary>
		/// Get/Set the title of the loaded file.
		/// </summary>
		public string Title
		{
			get { return mTitle; }
			set { mTitle = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Version																																*
		//*-----------------------------------------------------------------------*
		private string mVersion = "";
		/// <summary>
		/// Get/Set the version of the loaded file.
		/// </summary>
		public string Version
		{
			get { return mVersion; }
			set { mVersion = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ViewBox																																*
		//*-----------------------------------------------------------------------*
		private SKRect mViewBox = new SKRect();
		/// <summary>
		/// Get/Set a reference to the viewbox controlling the aspect and user unit
		/// representation of this data.
		/// </summary>
		public SKRect ViewBox
		{
			get { return mViewBox; }
			set { mViewBox = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Width																																	*
		//*-----------------------------------------------------------------------*
		private float mWidth = 0F;
		/// <summary>
		/// Get/Set the specified image width for this document.
		/// </summary>
		public float Width
		{
			get { return mWidth; }
			set { mWidth = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
