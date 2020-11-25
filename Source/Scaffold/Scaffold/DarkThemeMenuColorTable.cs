//	DarkThemeMenuColorTable.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
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
	//*	DarkThemeMenuColorTable																									*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Menu render color table for dark theme.
	/// </summary>
	public class DarkThemeMenuColorTable : ProfessionalColorTable
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	ImageMarginGradientBegin																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the background color of the image margin at the left side of the
		/// strip.
		/// </summary>
		public override Color ImageMarginGradientBegin
		{
			get { return ColorTranslator.FromHtml("#333333"); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ImageMarginGradientEnd																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the background color of the image margin at the left side of the
		/// strip.
		/// </summary>
		public override Color ImageMarginGradientEnd
		{
			get { return ColorTranslator.FromHtml("#333333"); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ImageMarginGradientMiddle																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the background color of the image margin at the left side of the
		/// strip.
		/// </summary>
		public override Color ImageMarginGradientMiddle
		{
			get { return ColorTranslator.FromHtml("#333333"); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MenuBorder																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the color of the menu border.
		/// </summary>
		public override Color MenuBorder
		{
			get { return ColorTranslator.FromHtml("#222222"); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MenuItemBorder																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the color of an individual menu item.
		/// </summary>
		public override Color MenuItemBorder
		{
			get { return ColorTranslator.FromHtml("#222222"); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MenuItemPressedGradientBegin																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the menu item selected color.
		/// </summary>
		public override Color MenuItemPressedGradientBegin
		{
			get { return ColorTranslator.FromHtml("#dcdcdc"); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MenuItemPressedGradientEnd																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the menu item selected color.
		/// </summary>
		public override Color MenuItemPressedGradientEnd
		{
			get { return ColorTranslator.FromHtml("#dcdcdc"); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MenuItemPressedGradientMiddle																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the menu item selected color.
		/// </summary>
		public override Color MenuItemPressedGradientMiddle
		{
			get { return ColorTranslator.FromHtml("#dcdcdc"); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MenuItemSelected																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the menu item selected color.
		/// </summary>
		public override Color MenuItemSelected
		{
			get { return ColorTranslator.FromHtml("#dcdcdc"); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MenuItemSelectedGradientBegin																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the background color of a selected menu item.
		/// </summary>
		public override Color MenuItemSelectedGradientBegin
		{
			get { return ColorTranslator.FromHtml("#dcdcdc"); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MenuItemSelectedGradientEnd																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the background color of a selected menu item.
		/// </summary>
		public override Color MenuItemSelectedGradientEnd
		{
			get { return ColorTranslator.FromHtml("#dcdcdc"); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MenuStripGradientBegin																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the background color of the menu strip.
		/// </summary>
		public override Color MenuStripGradientBegin
		{
			get { return ColorTranslator.FromHtml("#333333"); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MenuStripGradientEnd																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the background color of the menu strip.
		/// </summary>
		public override Color MenuStripGradientEnd
		{
			get { return ColorTranslator.FromHtml("#333333"); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SeparatorDark																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the menu item dark separator color.
		/// </summary>
		public override Color SeparatorDark
		{
			get { return ColorTranslator.FromHtml("#dcdcdc"); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SeparatorLight																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the menu item light separator color.
		/// </summary>
		public override Color SeparatorLight
		{
			get { return ColorTranslator.FromHtml("#333333"); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ToolStripDropDownBackground																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the background color of dropdown toolstrips.
		/// </summary>
		public override Color ToolStripDropDownBackground
		{
			get { return ColorTranslator.FromHtml("#333333"); }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	DarkThemeMenuRenderer																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Dark theme menu rendering manager.
	/// </summary>
	public class DarkThemeMenuRenderer : ToolStripProfessionalRenderer
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	OnRenderArrow																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the RenderArrow event to render the arrow character on menus
		/// and toolstrips.
		/// </summary>
		/// <param name="e">
		/// </param>
		protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
		{
			//base.OnRenderArrow(e);
			ToolStripMenuItem item = (ToolStripMenuItem)e.Item;
			if(item != null)
			{
				if(item.Selected)
				{
					e.ArrowColor = ColorTranslator.FromHtml("#333333");
				}
				else
				{
					e.ArrowColor = ColorTranslator.FromHtml("#dcdcdc");
				}
			}
			base.OnRenderArrow(e);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	OnRenderMenuItemBackground																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the RenderMenuItemBackground event to render background color
		/// on menu items and toolstrips.
		/// </summary>
		/// <param name="e">
		/// </param>
		protected override void OnRenderMenuItemBackground(
			ToolStripItemRenderEventArgs e)
		{
			//base.OnRenderMenuItemBackground(e);
			Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
			Color c = Color.Empty;

			if(e.Item.Selected)
			{
				c = ColorTranslator.FromHtml("#dcdcdc");
				e.Item.ForeColor = ColorTranslator.FromHtml("#333333");
			}
			else
			{
				c = ColorTranslator.FromHtml("#333333");
				e.Item.ForeColor = ColorTranslator.FromHtml("#dcdcdc");
			}
			using(SolidBrush brush = new SolidBrush(c))
			{
				e.Graphics.FillRectangle(brush, rc);
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
		/// Create a new instance of the DarkThemeMenuRenderer Item.
		/// </summary>
		public DarkThemeMenuRenderer() : base()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the DarkThemeMenuRenderer Item.
		/// </summary>
		public DarkThemeMenuRenderer(
			ProfessionalColorTable professionalColorTable) :
			base(professionalColorTable)
		{
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
