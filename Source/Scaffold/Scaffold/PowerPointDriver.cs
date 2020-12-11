//	OfficeDriver.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

using static Scaffold.ScaffoldNodesUtil;
using static Scaffold.ScaffoldUtil;
using System.Drawing;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	PowerPointDriver																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Microsoft PowerPoint namespace isolation and functionality.
	/// </summary>
	public class PowerPointDriver
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		private PowerPoint.Application mPowerPoint = null;
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	ActivePresentation																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get a reference to the active presentation.
		/// </summary>
		public Presentation ActivePresentation
		{
			get
			{
				Presentation result = null;

				if(mPowerPoint != null)
				{
					try
					{
						result = mPowerPoint.ActivePresentation;
					}
					catch { }
				}
				return result;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ActiveSlideIndex																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the slide index of the active slide in the active window.
		/// </summary>
		public int ActiveSlideIndex
		{
			get
			{
				int result = 0;
				if(mPowerPoint != null)
				{
					try
					{
						result = mPowerPoint.ActiveWindow.View.Slide.SlideIndex;
					}
					catch { }
				}
				return result;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* AddTextbox																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add a simple textbox to the specified slide.
		/// </summary>
		/// <param name="slide">
		/// Reference to the slide that will receive the new textbox.
		/// </param>
		/// <param name="text">
		/// Text content to add.
		/// </param>
		/// <param name="location">
		/// The location of the object on the slide.
		/// </param>
		/// <param name="size">
		/// Size of the object.
		/// </param>
		/// <returns>
		/// Reference to the newly created text shape.
		/// </returns>
		public static PowerPoint.Shape AddTextbox(Slide slide, string text,
			PointF location, SizeF size)
		{
			PowerPoint.Shape result = null;

			try
			{
				if(slide != null && text?.Length > 0)
				{
					result = slide.Shapes.AddTextbox(
						MsoTextOrientation.msoTextOrientationHorizontal,
						location.X, location.Y,
						size.Width, size.Height);
					result.TextFrame.TextRange.Text = text;
				}
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* AlignContent																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Align and/or distribute all of the named shapes in the specified
		/// slide.
		/// </summary>
		/// <param name="slideIndex">
		/// Index of the slide containing the shapes to update.
		/// </param>
		/// <param name="alignmentReference">
		/// Coordinate reference type for alignment operation.
		/// </param>
		/// <param name="alignmentReferenceValue">
		/// Value associated with the coordinate reference type.
		/// </param>
		/// <param name="alignmentType">
		/// Type of alignment to perform. AlignmentTypeEnum.None if none.
		/// </param>
		/// <param name="distributionType">
		/// TYpe of distribution to perform. DistributionTypeEnum.None if none.
		/// </param>
		/// <param name="shapeNames">
		/// Collection of names of shapes to be updated by this operation.
		/// </param>
		public void AlignContent(int slideIndex,
			AlignmentReferenceEnum alignmentReference,
			string alignmentReferenceValue, AlignmentTypeEnum alignmentType,
			DistributionTypeEnum distributionType, List<string> shapeNames)
		{
			float coord = 0f;
			float coordMax = 0f;
			float coordMin = 0f;
			int count = 0;
			float delta = 0f;
			List<NameValue<float>> floatList = new List<NameValue<float>>();
			int index = 0;
			PowerPoint.Shape shape = null;
			Slide slide = null;

			if(slideIndex != 0 &&
				alignmentReference != AlignmentReferenceEnum.None &&
				alignmentReferenceValue?.Length > 0 &&
				(alignmentType != AlignmentTypeEnum.None ||
				distributionType != DistributionTypeEnum.None) &&
				shapeNames?.Count > 0)
			{
				//	Parameters are legal.
				slide = GetSlideBySlideIndex(ActivePresentation, slideIndex);
				if(slide != null)
				{
					//	Slide found.
					switch(alignmentReference)
					{
						case AlignmentReferenceEnum.Anchor:
							shape = GetShape(slide, alignmentReferenceValue);
							if(shape != null)
							{
								switch(alignmentType)
								{
									case AlignmentTypeEnum.Top:
									case AlignmentTypeEnum.Middle:
									case AlignmentTypeEnum.Bottom:
										try
										{
											coord = shape.Top;
										}
										catch { }
										break;
									case AlignmentTypeEnum.Left:
									case AlignmentTypeEnum.Center:
									case AlignmentTypeEnum.Right:
										try
										{
											coord = shape.Left;
										}
										catch { }
										break;
								}
							}
							break;
						case AlignmentReferenceEnum.LeftCoord:
						case AlignmentReferenceEnum.TopCoord:
							coord = ToFloat(alignmentReferenceValue);
							break;
					}
					//	Align the shapes.
					if(alignmentType != AlignmentTypeEnum.None)
					{
						switch(alignmentType)
						{
							case AlignmentTypeEnum.Bottom:
								foreach(string shapeName in shapeNames)
								{
									shape = GetShape(slide, shapeName);
									if(shape != null)
									{
										try
										{
											shape.Top = coord - shape.Height;
										}
										catch { }
									}
								}
								break;
							case AlignmentTypeEnum.Center:
								foreach(string shapeName in shapeNames)
								{
									shape = GetShape(slide, shapeName);
									if(shape != null)
									{
										try
										{
											shape.Left = coord - (shape.Width / 2f);
										}
										catch { }
									}
								}
								break;
							case AlignmentTypeEnum.Left:
								foreach(string shapeName in shapeNames)
								{
									shape = GetShape(slide, shapeName);
									if(shape != null)
									{
										try
										{
											shape.Left = coord;
										}
										catch { }
									}
								}
								break;
							case AlignmentTypeEnum.Middle:
								foreach(string shapeName in shapeNames)
								{
									shape = GetShape(slide, shapeName);
									if(shape != null)
									{
										try
										{
											shape.Top = coord - (shape.Height / 2f);
										}
										catch { }
									}
								}
								break;
							case AlignmentTypeEnum.Right:
								foreach(string shapeName in shapeNames)
								{
									shape = GetShape(slide, shapeName);
									if(shape != null)
									{
										try
										{
											shape.Left = coord - shape.Width;
										}
										catch { }
									}
								}
								break;
							case AlignmentTypeEnum.Top:
								foreach(string shapeName in shapeNames)
								{
									shape = GetShape(slide, shapeName);
									if(shape != null)
									{
										try
										{
											shape.Top = coord;
										}
										catch { }
									}
								}
								break;
						}
					}
					//	Distribute the shapes.
					if(distributionType != DistributionTypeEnum.None)
					{
						floatList.Clear();
						foreach(string shapeName in shapeNames)
						{
							shape = GetShape(slide, shapeName);
							if(shape != null)
							{
								switch(distributionType)
								{
									case DistributionTypeEnum.Horizontal:
										floatList.Add(
											new NameValue<float>(shapeName, shape.Left));
										break;
									case DistributionTypeEnum.Vertical:
										floatList.Add(
											new NameValue<float>(shapeName, shape.Top));
										break;
								}
							}
						}
						if(floatList.Count > 1)
						{
							floatList.Sort((x, y) => x.Value.CompareTo(y.Value));
							coordMin = floatList[0].Value;
							coordMax = floatList[floatList.Count - 1].Value;
							delta = ((coordMax - coordMin) / (float)(shapeNames.Count - 1));
							count = floatList.Count;
							for(index = 0; index < count; index++)
							{
								shape = GetShape(slide, floatList[index].Name);
								if(shape != null)
								{
									switch(distributionType)
									{
										case DistributionTypeEnum.Horizontal:
											shape.Left = coordMin + (delta * (float)index);
											break;
										case DistributionTypeEnum.Vertical:
											shape.Top = coordMin + (delta * (float)index);
											break;
									}
								}
							}
						}
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	EnsurePowerPointRunning																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Ensure the PowerPoint application is running.
		/// </summary>
		/// <param name="visible">
		/// Value indicating whether to make the display visible.
		/// </param>
		public string EnsurePowerPointRunning(bool visible)
		{
			string name = "";
			string result = "OK";

			try
			{
				name = mPowerPoint.Name;
			}
			catch
			{
				mPowerPoint = new PowerPoint.Application();
			}
			try
			{
				//	Hiding the application window is not allowed.
				//	Instead, set the window state accordingly.
				mPowerPoint.Visible = MsoTriState.msoTrue;
				if(visible)
				{
					mPowerPoint.WindowState = PowerPoint.PpWindowState.ppWindowNormal;
				}
				else
				{
					mPowerPoint.WindowState = PowerPoint.PpWindowState.ppWindowMinimized;
				}
			}
			catch(Exception ex)
			{
				result = $"Error connecting to PowerPoint: {ex.Message}";
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ExportDecisionTreeToPowerPoint																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Export the caller's decision tree to PowerPoint, using the storyboard
		/// options provided.
		/// </summary>
		public void ExportDecisionTreeToPowerPoint(NodeCollection nodes,
			bool createFile = true, string filename = "")
		{
			string message = "";
			Presentation objPres = null;
			//Microsoft.Office.Interop.PowerPoint.Shape objShape = null;
			Microsoft.Office.Interop.PowerPoint.ShapeRange objShapeRange = null;
			Slide objSlide = null;
			float offset = 0f;
			int page = 0;
			string ph = "";
			string pv = "";
			Size size = Size.Empty;
			float slideHeight = 0f;
			float slideWidth = 0f;
			List<SocketItem> sockets = null;
			string text = "";
			int width = 0;
			float x = 0f;
			float y = 0f;

			message = EnsurePowerPointRunning(false);
			if(message == "OK")
			{
				page = nodes.Max(n => ToInt(n["StoryPageNumber"].StringValue()));
				if(createFile)
				{
					//	A new file will be created.
					objPres = mPowerPoint.Presentations.Add(MsoTriState.msoTrue);
				}
				else
				{
					//	An existing file will be created.
					objPres = mPowerPoint.Presentations.Open(filename);
				}
				//	Add all of the slides.
				while(objPres.Slides.Count < page)
				{
					objPres.Slides.Add(objPres.Slides.Count + 1,
						PpSlideLayout.ppLayoutBlank);
				}
				//	Place configured elements.
				slideWidth = objPres.PageSetup.SlideWidth;
				slideHeight = objPres.PageSetup.SlideHeight;
				ClipboardLoadFromResource("CaptionLeft");
				if(Clipboard.GetDataObject().GetFormats().Length != 14)
				{
					//	Sometimes, the item is not loaded on the first try.
					ClipboardLoadFromResource("CaptionLeft");
				}
				foreach(NodeItem node in nodes)
				{
					page = ToInt(node["StoryPageNumber"].StringValue());
					ph = node["StoryPageHorizontalPlacement"].StringValue();
					pv = node["StoryPageVerticalPlacement"].StringValue();
					x = ToFloat(node["StoryPageX"].StringValue());
					y = ToFloat(node["StoryPageY"].StringValue());
					text = node["Question"].StringValue();
					width = ToInt(node["StoryPageWidth"].StringValue());
					objSlide = objPres.Slides[page];
					objShapeRange = objSlide.Shapes.Paste();
					size = MeasureString(
							text,
							node["StoryPageFontName"].StringValue(),
							ToFloat(node["StoryPageFontSize"].StringValue()),
							width - 20);
					size.Width = Math.Max(size.Width, 64);
					objShapeRange.Width = size.Width;
					objShapeRange.Height = size.Height + 20;
					if(ph == "From right")
					{
						//	Right aligned.
						objShapeRange.Left = slideWidth - x;
					}
					else
					{
						//	Left aligned = default.
						objShapeRange.Left = x;
					}
					if(pv == "From bottom")
					{
						//	Bottom aligned.
						objShapeRange.Top = slideHeight - y;
					}
					else
					{
						//	Top aligned = default.
						objShapeRange.Top = y;
					}
					objShapeRange.TextFrame.TextRange.Text = text;
				}
				ClipboardLoadFromResource("CaptionRight");
				if(Clipboard.GetDataObject().GetFormats().Length != 14)
				{
					//	Sometimes, the item is not loaded on the first try.
					ClipboardLoadFromResource("CaptionRight");
				}
				foreach(NodeItem node in nodes)
				{
					offset = 0f;
					sockets = node.Sockets.FindAll(s =>
						s.SocketMode == SocketModeEnum.Output);
					foreach(SocketItem socket in sockets)
					{
						page = ToInt(socket["StoryPageNumber"].StringValue());
						if(page == 0)
						{
							page = ToInt(node["StoryPageNumber"].StringValue());
						}
						ph = socket["StoryPageHorizontalPlacement"].StringValue();
						pv = socket["StoryPageVerticalPlacement"].StringValue();
						x = ToFloat(socket["StoryPageX"].StringValue());
						y = ToFloat(socket["StoryPageY"].StringValue());
						text = socket["Answer"].StringValue();
						width = ToInt(socket["StoryPageWidth"].StringValue());
						objSlide = objPres.Slides[page];
						objShapeRange = objSlide.Shapes.Paste();
						size = MeasureString(
								text,
								socket["StoryPageFontName"].StringValue(),
								ToFloat(socket["StoryPageFontSize"].StringValue()),
								width - 20);
						size.Width = Math.Max(size.Width, 64);
						objShapeRange.Width = size.Width;
						objShapeRange.Height = size.Height + 20;
						if(ph == "From right")
						{
							//	Right aligned.
							objShapeRange.Left = slideWidth - size.Width - x;
						}
						else
						{
							//	Left aligned = default.
							objShapeRange.Left = x;
						}
						if(pv == "From bottom")
						{
							//	Bottom aligned.
							objShapeRange.Top = slideHeight - size.Height - (y + offset);
						}
						else
						{
							//	Top aligned = default.
							objShapeRange.Top = y + offset;
						}
						offset += (y + size.Height + 32);
						objShapeRange.TextFrame.TextRange.Text = text;
					}
				}
			}
			else
			{
				MessageBox.Show(message, "Export Decision Tree to PowerPoint");
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetFontName																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the name of the font in the specified shape.
		/// </summary>
		/// <param name="shape">
		/// Reference to the shape to inspect.
		/// </param>
		/// <returns>
		/// Name of the font found on the shape, if found. Otherwise, an empty
		/// string.
		/// </returns>
		public static string GetFontName(PowerPoint.Shape shape)
		{
			string result = "";

			try
			{
				if(shape != null && shape.HasTextFrame == MsoTriState.msoTrue)
				{
					result = shape.TextFrame.TextRange.Font.Name;
				}
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetFontSize																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the size of the font in the specified shape.
		/// </summary>
		/// <param name="shape">
		/// Reference to the shape to inspect.
		/// </param>
		/// <returns>
		/// Size of the font found on the shape, in points, if found. Otherwise,
		/// 0.
		/// </returns>
		public static float GetFontSize(PowerPoint.Shape shape)
		{
			float result = 0f;

			try
			{
				if(shape != null && shape.HasTextFrame == MsoTriState.msoTrue)
				{
					result = shape.TextFrame.TextRange.Font.Size;
				}
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetHeight																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the height of the specified shape.
		/// </summary>
		/// <param name="shape">
		/// Reference to the shape to measure.
		/// </param>
		/// <returns>
		/// Height of the specified shape.
		/// </returns>
		public static float GetHeight(PowerPoint.Shape shape)
		{
			float result = 0f;

			try
			{
				if(shape != null)
				{
					result = shape.Height;
				}
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetLocation																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the location of the provided shape.
		/// </summary>
		/// <param name="shape">
		/// Reference to the shape to be inspected.
		/// </param>
		/// <returns>
		/// A floating Point structure where X and Y have been set to the
		/// location of the caller's shape.
		/// </returns>
		public static PointF GetLocation(PowerPoint.Shape shape)
		{
			PointF result = PointF.Empty;

			try
			{
				if(shape != null)
				{
					result = new PointF(shape.Left, shape.Top);
				}
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetPlaceholderContainedType																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the placeholder contained type for the specified shape.
		/// </summary>
		/// <param name="shape">
		/// Reference to the shape to inspect.
		/// </param>
		/// <returns>
		/// Shape type for the placeholder contained type, or null.
		/// </returns>
		public static MsoShapeType? GetPlaceholderContainedType(
			PowerPoint.Shape shape)
		{
			MsoShapeType? result = null;

			try
			{
				if(shape != null)
				{
					result = shape.PlaceholderFormat.ContainedType;
				}
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetPlaceholderType																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the placeholder type for the specified shape.
		/// </summary>
		/// <param name="shape">
		/// Reference to the shape to inspect.
		/// </param>
		/// <returns>
		/// Type of placeholder, or null.
		/// </returns>
		public static PpPlaceholderType? GetPlaceholderType(PowerPoint.Shape shape)
		{
			PpPlaceholderType? result = null;

			try
			{
				if(shape != null)
				{
					result = shape.PlaceholderFormat.Type;
				}
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetShape																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the shape referenced by ordinal index.
		/// </summary>
		/// <param name="slide">
		/// Reference to the slide containing the shape.
		/// </param>
		/// <param name="shapeIndex">
		/// Ordinal index of the shape within the collection.
		/// </param>
		/// <returns>
		/// Reference to the referenced shape, if found. Otherwise, null.
		/// </returns>
		public static PowerPoint.Shape GetShape(Slide slide, int shapeIndex)
		{
			PowerPoint.Shape result = null;

			try
			{
				if(slide != null && shapeIndex > 0 && shapeIndex <= slide.Shapes.Count)
				{
					result = slide.Shapes[shapeIndex];
				}
			}
			catch { }
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the shape referenced by name.
		/// </summary>
		/// <param name="slide">
		/// Reference to the slide containing the shape.
		/// </param>
		/// <param name="shapeName">
		/// Name of the shape to find.
		/// </param>
		/// <returns>
		/// Reference to the referenced shape, if found. Otherwise, null.
		/// </returns>
		public static PowerPoint.Shape GetShape(Slide slide, string shapeName)
		{
			PowerPoint.Shape result = null;

			try
			{
				if(slide != null && shapeName?.Length > 0)
				{
					result = slide.Shapes[shapeName];
				}
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetSlideBySlideIndex																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a reference to the slide having the specified SlideIndex value.
		/// </summary>
		/// <param name="presentation">
		/// Reference to the presentation containing the slides to inspect.
		/// </param>
		/// <param name="slideIndex">
		/// SlideIndex value to match.
		/// </param>
		/// <returns>
		/// Reference to the slide having the specified SlideIndex property
		/// value, if found. Otherwise, null;
		/// </returns>
		public static Slide GetSlideBySlideIndex(Presentation presentation,
			int slideIndex)
		{
			Slide result = null;

			try
			{
				if(presentation != null && presentation.Slides.Count > 0)
				{
					foreach(Slide slide in presentation.Slides)
					{
						if(slide.SlideIndex == slideIndex)
						{
							result = slide;
							break;
						}
					}
				}
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetSlideIndicesInScope																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the index values of all slides in the specified scope.
		/// </summary>
		/// <param name="presentation">
		/// Reference to the presentation containing the slides to inspect.
		/// </param>
		/// <param name="slideScope">
		/// Scope of slides to identify.
		/// </param>
		/// <param name="slideScopeValue">
		/// Value to use in the case that slideScope is SlideScopeEnum.Custom.
		/// </param>
		/// <returns>
		/// List of integer values identifying the indexes of the specified slides.
		/// </returns>
		public static List<int> GetSlideIndicesInScope(Presentation presentation,
			SlideScopeEnum slideScope, string slideScopeValue)
		{
			List<int> result = new List<int>();
			int slideIndex = 0;

			if(presentation != null && presentation.Slides.Count > 0)
			{
				switch(slideScope)
				{
					case SlideScopeEnum.All:
						foreach(Slide slideItem in presentation.Slides)
						{
							try
							{
								result.Add(slideItem.SlideIndex);
							}
							catch { }
						}
						break;
					case SlideScopeEnum.Current:
						try
						{
							slideIndex =
								presentation.Application.ActiveWindow.View.Slide.SlideIndex;
						}
						catch { }
						if(slideIndex != 0)
						{
							result.Add(slideIndex);
						}
						break;
					case SlideScopeEnum.Custom:
						//	In this version, custom can only be a certain number.
						slideIndex = ToInt(slideScopeValue);
						if(slideIndex != 0)
						{
							foreach(Slide slideItem in presentation.Slides)
							{
								try
								{
									if(slideItem.SlideIndex == slideIndex)
									{
										result.Add(slideIndex);
										break;
									}
								}
								catch { }
							}
						}
						break;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetText																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the text content of the specified object.
		/// </summary>
		/// <param name="shape">
		/// Reference to the shape to inspect.
		/// </param>
		/// <returns>
		/// The text associated with the object.
		/// </returns>
		public static string GetText(PowerPoint.Shape shape)
		{
			string result = "";

			try
			{
				if(shape != null)
				{
					result = shape.TextFrame.TextRange.Text;
				}
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetTextHeight																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the height of a single line of text, in points.
		/// </summary>
		/// <param name="shape">
		/// Reference to the shape to inspect.
		/// </param>
		/// <returns>
		/// Text height of the shape, in points.
		/// </returns>
		public static float GetTextHeight(PowerPoint.Shape shape)
		{
			float result = 0f;

			try
			{
				if(shape != null && shape.HasTextFrame == MsoTriState.msoTrue)
				{
					result = shape.TextFrame.TextRange.Font.Size;
				}
			}
			catch { }
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the height of the specified text, given a starting boundary.
		/// </summary>
		/// <param name="text">
		/// Text to measure.
		/// </param>
		/// <param name="fontName">
		/// Name of the font to measure with.
		/// </param>
		/// <param name="fontSize">
		/// Size of the font to measure with.
		/// </param>
		/// <param name="boundaryWidth">
		/// Boundary width. If the height of bounarySize is non-zero, the
		/// return value will be equal to bounarySize.Height.
		/// </param>
		/// <param name="boundaryHeight">
		/// </param>
		/// <returns>
		/// Either boundarySize.Height or the dynamically caculated height
		/// based on the combination of bounarySize.Width and the total
		/// size of the text.
		/// </returns>
		public static float GetTextHeight(string text, string fontName,
			float fontSize, float boundaryWidth, float boundaryHeight)
		{
			float result = 0f;

			if(boundaryHeight != 0f)
			{
				result = boundaryHeight;
			}
			else
			{
				result = MeasureString(text, fontName, fontSize,
					PointsToPixels(boundaryWidth)).Height;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetTextLines																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the text as a list of individual trimmed lines.
		/// </summary>
		/// <param name="shape">
		/// Reference to the shape to be read.
		/// </param>
		/// <returns>
		/// List of string items where each entry represents one trimmed line of
		/// the original get. If no text was found, an empty list is returned.
		/// </returns>
		public static List<string> GetTextLines(PowerPoint.Shape shape)
		{
			string line = "";
			MatchCollection matches = null;
			List<string> result = new List<string>();
			string text = "";

			if(shape != null)
			{
				text = GetText(shape);
				if(text.Length > 0)
				{
					matches = Regex.Matches(text, ResourceMain.rxTextLine);
					foreach(Match match in matches)
					{
						line = GetValue(match, "line");
						if(line?.Length > 0)
						{
							result.Add(line);
						}
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetWidth																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the width of the provided shape.
		/// </summary>
		/// <param name="shape">
		/// Reference to the shape to inspect.
		/// </param>
		/// <returns>
		/// Width of the specified shape, if found. Otherwise, 0.
		/// </returns>
		public static float GetWidth(PowerPoint.Shape shape)
		{
			float result = 0f;

			try
			{
				if(shape != null)
				{
					result = shape.Width;
				}
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PlaceholderToTextboxes																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert placeholder content text to individual textboxes.
		/// </summary>
		/// <param name="slideScope">
		/// Specification of slides upon which to operate.
		/// </param>
		/// <param name="placeholderOnly">
		/// Value indicating whether only the content placeholder will be
		/// targeted. If false, all multiline textboxes are targeted.
		/// </param>
		/// <param name="slideScopeText">
		/// Optional scope text to include when slideScope is Custom.
		/// </param>
		/// <returns>
		/// Counts of elements affected.
		/// </returns>
		public Dictionary<string, int> PlaceholderToTextboxes(
			SlideScopeEnum slideScope, bool placeholderOnly = true,
			string slideScopeText = "")
		{
			bool bContinue = false;
			int continueMax = 5;
			int continueIndex = 0;
			int count = 0;
			int countedSlide = 0;
			int countShape = 0;
			int countSlide = 0;
			string fontName = "";
			float fontSize = 0f;
			int index = 0;
			List<string> lines = null;
			PointF location = PointF.Empty;
			Presentation presentation = ActivePresentation;
			Dictionary<string, int> result = new Dictionary<string, int>();
			PowerPoint.Shape shape = null;
			PowerPoint.Shape shapeNew = null;
			SizeF size = SizeF.Empty;
			SizeF sizeRef = SizeF.Empty;
			Slide slide = null;
			List<int> slideIndices = null;

			if(presentation != null && presentation.Slides.Count > 0)
			{
				slideIndices = GetSlideIndicesInScope(presentation,
					slideScope, slideScopeText);
				foreach(int slideIndexItem in slideIndices)
				{
					//	Convert on each specified slide.
					slide = GetSlideBySlideIndex(presentation, slideIndexItem);
					if(slide != null)
					{
						count = slide.Shapes.Count;
						if(placeholderOnly)
						{
							//	Replace only the content placeholder.
							//	Shapes are 1-based.
							bContinue = true;
							continueIndex = 0;
							while(bContinue && continueIndex < continueMax)
							{
								bContinue = false;
								count = slide.Shapes.Count;
								for(index = 1; index <= count; index++)
								{
									shape = GetShape(slide, index);
									if(shape != null && ShapeIsContentPlaceholder(shape) &&
										!ShapeHasChart(shape) &&
										!ShapeHasDiagram(shape) && !ShapeHasDiagramNode(shape) &&
										!ShapeHasInkXml(shape) && !ShapeHasSectionZoom(shape) &&
										!ShapeHasSmartArt(shape) && !ShapeHasTable(shape))
									{
										//	Content placeholder shape found.
										if(countedSlide != slide.SlideIndex)
										{
											countSlide++;
											countedSlide = slide.SlideIndex;
										}
										if(ShapeHasText(shape))
										{
											countShape++;
											fontName = GetFontName(shape);
											fontSize = GetFontSize(shape);
											location = GetLocation(shape);
											sizeRef = new SizeF(GetWidth(shape), fontSize);
											lines = GetTextLines(shape);
											foreach(string line in lines)
											{
												size =
													new SizeF(sizeRef.Width,
													GetTextHeight(line, fontName, fontSize,
													sizeRef.Width, 0f));
												shapeNew = AddTextbox(slide, line, location, size);
												SetFont(shapeNew, fontName, fontSize);
												location = new PointF(
													location.X,
													location.Y + size.Height);
											}
										}
										try
										{
											shape.Delete();
											bContinue = true;
										}
										catch { }
									}
								}
								continueIndex++;
							}
						}
						else
						{
							//	Replace all multiline textboxes.
							bContinue = true;
							continueIndex = 0;
							while(bContinue && continueIndex < continueMax)
							{
								bContinue = false;
								count = slide.Shapes.Count;
								for(index = 1; index <= count; index++)
								{
									shape = GetShape(slide, index);
									if(shape != null &&
										(GetPlaceholderContainedType(shape) == null &&
										GetPlaceholderType(shape) == null &&
										ShapeHasText(shape)) ||
										(ShapeIsContentPlaceholder(shape) &&
										!ShapeHasChart(shape) &&
										!ShapeHasDiagram(shape) && !ShapeHasDiagramNode(shape) &&
										!ShapeHasInkXml(shape) && !ShapeHasSectionZoom(shape) &&
										!ShapeHasSmartArt(shape) && !ShapeHasTable(shape)))
									{
										//	Multiline or content placeholder found.
										lines = GetTextLines(shape);
										if(lines.Count > 1 || ShapeIsContentPlaceholder(shape))
										{
											if(countedSlide != slide.SlideIndex)
											{
												countSlide++;
												countedSlide = slide.SlideIndex;
											}
											countShape++;
											if(lines.Count > 0)
											{
												fontName = GetFontName(shape);
												fontSize = GetFontSize(shape);
												location = GetLocation(shape);
												sizeRef = new SizeF(GetWidth(shape), fontSize);
												foreach(string line in lines)
												{
													size =
														new SizeF(sizeRef.Width,
														GetTextHeight(line, fontName, fontSize,
														sizeRef.Width, 0f));
													shapeNew = AddTextbox(slide, line, location, size);
													SetFont(shapeNew, fontName, fontSize);
													location = new PointF(
														location.X,
														location.Y + size.Height);
												}
											}
											try
											{
												shape.Delete();
												bContinue = true;
											}
											catch { }
										}
									}
								}
								continueIndex++;
							}
						}
					}
				}
			}
			result.Add("Slides", countSlide);
			result.Add("Shapes", countShape);
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RemoveBullet																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Remove bullet formatting from the specified areas of the presentation.
		/// </summary>
		/// <param name="slideScope">
		/// Scope of slides to handle.
		/// </param>
		/// <param name="slideScopeText">
		/// An index value to process when slideScope is SlideScopeEnum.Custom.
		/// </param>
		/// <param name="selectedOnly">
		/// Handle only selected items when true. Handle all items with bullet
		/// formatting when false.
		/// </param>
		/// <param name="selectedShapeNames">
		/// List of shape names to update when selectedOnly is true.
		/// </param>
		public Dictionary<string, int> RemoveBullet(
			SlideScopeEnum slideScope, string slideScopeText,
			bool selectedOnly = false, List<string> selectedShapeNames = null)
		{
			int countedSlide = 0;
			int countShape = 0;
			int countSlide = 0;
			Dictionary<string, int> counts = new Dictionary<string, int>();
			Presentation presentation = null;
			Slide slide = null;
			List<int> slideIndices = null;

			if(slideScope != SlideScopeEnum.None &&
				(slideScope != SlideScopeEnum.Custom || slideScopeText?.Length > 0) &&
				(selectedOnly == false || selectedShapeNames.Count > 0))
			{
				presentation = ActivePresentation;
				if(presentation != null && presentation.Slides.Count > 0)
				{
					slideIndices = GetSlideIndicesInScope(presentation,
						slideScope, slideScopeText);
					foreach(int slideIndexItem in slideIndices)
					{
						slide = GetSlideBySlideIndex(presentation, slideIndexItem);
						if(slide != null)
						{
							foreach(PowerPoint.Shape shape in slide.Shapes)
							{
								try
								{
									if(shape.HasTextFrame == MsoTriState.msoTrue &&
										shape.TextFrame.TextRange.ParagraphFormat.Bullet.
										Visible == MsoTriState.msoTrue)
									{
										if(!selectedOnly ||
											selectedShapeNames.Exists(x => x == shape.Name))
										{
											//	Remove the bullet formatting on this item.
											if(countedSlide != slideIndexItem)
											{
												countSlide++;
												countedSlide = slideIndexItem;
											}
											shape.TextFrame.TextRange.ParagraphFormat.Bullet.
												Visible = MsoTriState.msoFalse;
											countShape++;
										}
									}
								}
								catch { }
							}
						}
					}
				}
			}
			counts.Add("Slides", countSlide);
			counts.Add("Shapes", countShape);
			return counts;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SetFont																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the text font on the specified shape.
		/// </summary>
		/// <param name="shape">
		/// Reference to the shape upon which the font will be set.
		/// </param>
		/// <param name="fontName">
		/// Name of the font to apply.
		/// </param>
		/// <param name="fontSize">
		/// Size of the font to apply.
		/// </param>
		public static void SetFont(PowerPoint.Shape shape, string fontName,
			float fontSize)
		{
			try
			{
				if(shape != null && shape.HasTextFrame == MsoTriState.msoTrue)
				{
					shape.TextFrame.TextRange.Font.Name = fontName;
					shape.TextFrame.TextRange.Font.Size = fontSize;
				}
			}
			catch { }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ShapeHasChart																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified shape contains a chart.
		/// </summary>
		/// <param name="shape">
		/// Reference to the shape to inspect.
		/// </param>
		/// <returns>
		/// Value indicating whether the shape contains a chart.
		/// </returns>
		public static bool ShapeHasChart(PowerPoint.Shape shape)
		{
			bool result = false;
			try
			{
				if(shape != null)
				{
					result = (shape.HasChart == MsoTriState.msoTrue);
				}
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ShapeHasDiagram																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified shape contains a
		/// diagram.
		/// </summary>
		/// <param name="shape">
		/// Reference to the shape to inspect.
		/// </param>
		/// <returns>
		/// Value indicating whether the specified shape has a diagram.
		/// </returns>
		public static bool ShapeHasDiagram(PowerPoint.Shape shape)
		{
			bool result = false;
			try
			{
				if(shape != null)
				{
					result = (shape.HasDiagram == MsoTriState.msoTrue);
				}
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ShapeHasDiagramNode																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified shape contains a
		/// diagram node.
		/// </summary>
		/// <param name="shape">
		/// Reference to the shape to inspect.
		/// </param>
		/// <returns>
		/// Value indicating whether the shape contains a diagram node.
		/// </returns>
		public static bool ShapeHasDiagramNode(PowerPoint.Shape shape)
		{
			bool result = false;
			try
			{
				if(shape != null)
				{
					result = (shape.HasDiagramNode == MsoTriState.msoTrue);
				}
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ShapeHasInkXml																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified shape contains Ink xml.
		/// </summary>
		/// <param name="shape">
		/// Reference to the shape to inspect.
		/// </param>
		/// <returns>
		/// Value indicating whether the specified shape contains Ink xml data.
		/// </returns>
		public static bool ShapeHasInkXml(PowerPoint.Shape shape)
		{
			return false;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ShapeHasSectionZoom																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified shape contains a
		/// section zoom.
		/// </summary>
		/// <param name="shape">
		/// Reference to the shape to inspect.
		/// </param>
		/// <returns>
		/// Value indicating whether the shape contains a section zoom.
		/// </returns>
		public static bool ShapeHasSectionZoom(PowerPoint.Shape shape)
		{
			return false;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ShapeHasSmartArt																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified shape contains
		/// SmartArt.
		/// </summary>
		/// <param name="shape">
		/// Reference to the shape to inspect.
		/// </param>
		/// <returns>
		/// Value indicating whether the shape contains SmartArt.
		/// </returns>
		public static bool ShapeHasSmartArt(PowerPoint.Shape shape)
		{
			bool result = false;
			try
			{
				if(shape != null)
				{
					result = (shape.HasSmartArt == MsoTriState.msoTrue);
				}
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ShapeHasTable																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the shape contains a table.
		/// </summary>
		/// <param name="shape">
		/// Reference to the shape to inspect.
		/// </param>
		/// <returns>
		/// Value indicating whether the shape contains a table.
		/// </returns>
		public static bool ShapeHasTable(PowerPoint.Shape shape)
		{
			bool result = false;
			try
			{
				if(shape != null)
				{
					result = (shape.HasTable == MsoTriState.msoTrue);
				}
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ShapeHasText																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified shape has text.
		/// </summary>
		/// <param name="shape">
		/// Reference to the shape to inspect.
		/// </param>
		/// <returns>
		/// True if the shape has text. Otherwise, false.
		/// </returns>
		public static bool ShapeHasText(PowerPoint.Shape shape)
		{
			bool result = false;

			try
			{
				if(shape != null)
				{
					result = (shape.HasTextFrame == MsoTriState.msoTrue &&
						shape.TextFrame != null && shape.TextFrame.TextRange != null &&
						shape.TextFrame.TextRange.Text?.Length > 0);
				}
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ShapeIsContentPlaceholder																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified shape is a content
		/// placeholder.
		/// </summary>
		/// <param name="shape">
		/// Reference to the shape to inspect.
		/// </param>
		/// <returns>
		/// True if the shape is specifically a content placeholder. Otherwise,
		/// false.
		/// </returns>
		public static bool ShapeIsContentPlaceholder(PowerPoint.Shape shape)
		{
			bool result = false;

			try
			{
				if(shape != null)
				{
					result =
						shape.PlaceholderFormat.ContainedType ==
							MsoShapeType.msoAutoShape &&
						shape.PlaceholderFormat.Type ==
							PpPlaceholderType.ppPlaceholderObject;
				}
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SlideCount																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the count of slides in the active presentation.
		/// </summary>
		public int SlideCount
		{
			get
			{
				Presentation presentation = ActivePresentation;
				int result = 0;

				if(presentation != null)
				{
					result = presentation.Slides.Count;
				}
				return result;
			}
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
