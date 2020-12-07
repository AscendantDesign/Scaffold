//	OfficeDriver.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

using static Scaffold.ScaffoldNodesUtil;
using static Scaffold.ScaffoldUtil;
using System.Drawing;
using System.Diagnostics;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	OfficeDriver																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Office namespace isolation and functionality.
	/// </summary>
	public class OfficeDriver
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
		//*	PlaceholderToTextboxes																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert placeholder content text to individual textboxes.
		/// </summary>
		public void PlaceholderToTextboxes()
		{
			Presentation presentation = mPowerPoint.ActivePresentation;
			int slideIndex = 12;

			if(presentation != null && presentation.Slides.Count > 0)
			{
				foreach(Slide slide in presentation.Slides)
				{
					if(slide.SlideIndex == slideIndex)
					{
						foreach(PowerPoint.Shape shape in slide.Shapes)
						{
							//Debug.WriteLine(shape.MediaFormat.ToString());
						}
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
