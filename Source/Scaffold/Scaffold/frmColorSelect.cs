//	frmColorSelect.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Scaffold.ScaffoldUtil;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	frmSelectColor																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Color selection dialog.
	/// </summary>
	public partial class frmColorSelect : Form
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		private Control mFocusControl = null;
		private bool mOriginalColorSet = false;
		private ColorSliderManagerCollection mSliders =
			new ColorSliderManagerCollection();
		private bool mTextBusy = false;
		private bool mTextHexChanged = false;

		//*-----------------------------------------------------------------------*
		//* btnCancel_Click																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Cancel button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Hide();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnOK_Click																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// OK button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnOK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			this.Hide();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	lblBasicColor_Click																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The user has clicked on one of the basic color labels.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void lblBasicColor_Click(object sender, EventArgs e)
		{
			Label source = (Label)sender;
			this.Color = source.BackColor;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	lblBasicColor_MouseEnter																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has entered one of the basic color entries.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void lblBasicColor_MouseEnter(object sender, EventArgs e)
		{
			Label source = (Label)sender;
			tip.SetToolTip(source, ToHex(source.BackColor));
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mSliders_ColorChanged																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The color has changed in the sliders control collection.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Color event arguments.
		/// </param>
		private void mSliders_ColorChanged(object sender, ColorEventArgs e)
		{
			Color = e.NewValue;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* mSliders_FocusReceived																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// One of the slider controls has received logical focus.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mSliders_FocusReceived(object sender, EventArgs e)
		{
			TextBox control = null;
			if(mFocusControl is TextBox)
			{
				control = (TextBox)mFocusControl;
			}
			if(control != null)
			{
				switch(control.Name)
				{
					case "txtAlpha":
						mTextBusy = true;
						txtAlpha.Text = mSliders.ColorToken.Alpha.ToString();
						mTextBusy = false;
						break;
					case "txtBlue":
						mTextBusy = true;
						txtBlue.Text = mSliders.ColorToken.Blue.ToString();
						mTextBusy = false;
						break;
					case "txtGreen":
						mTextBusy = true;
						txtGreen.Text = mSliders.ColorToken.Green.ToString();
						mTextBusy = false;
						break;
					case "txtHue":
						mTextBusy = true;
						txtHue.Text = mSliders.ColorToken.Hue.ToString("0.0");
						mTextBusy = false;
						break;
					case "txtLuminance":
						mTextBusy = true;
						txtLuminance.Text =
							mSliders.ColorToken.Luminance.ToString("0.000");
						mTextBusy = false;
						break;
					case "txtRed":
						mTextBusy = true;
						txtRed.Text = mSliders.ColorToken.Red.ToString();
						mTextBusy = false;
						break;
					case "txtSaturation":
						mTextBusy = true;
						txtSaturation.Text =
							mSliders.ColorToken.Saturation.ToString("0.000");
						mTextBusy = false;
						break;
				}
			}
			mFocusControl = null;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* optColorModeHSL_CheckedChanged																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state of the color mode has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void optColorModeHSL_CheckedChanged(object sender, EventArgs e)
		{
			if(optColorModeHSL.Checked && mColorMode != ColorSliderModeEnum.HSL)
			{
				ColorMode = ColorSliderModeEnum.HSL;
			}
			else if(optColorModeRGB.Checked && mColorMode != ColorSliderModeEnum.RGB)
			{
				ColorMode = ColorSliderModeEnum.RGB;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* optColorModeRGB_CheckedChanged																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state of the color mode has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void optColorModeRGB_CheckedChanged(object sender, EventArgs e)
		{
			if(optColorModeHSL.Checked && mColorMode != ColorSliderModeEnum.HSL)
			{
				ColorMode = ColorSliderModeEnum.HSL;
			}
			else if(optColorModeRGB.Checked && mColorMode != ColorSliderModeEnum.RGB)
			{
				ColorMode = ColorSliderModeEnum.RGB;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtAlpha_Enter																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The alpha textbox has received focus.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtAlpha_Enter(object sender, EventArgs e)
		{
			mFocusControl = txtAlpha;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtAlpha_Leave																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The alpha textbox has lost focus.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtAlpha_Leave(object sender, EventArgs e)
		{

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtAlpha_TextChanged																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The alpha channel text has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtAlpha_TextChanged(object sender, EventArgs e)
		{
			if(!mTextBusy)
			{
				try
				{
					//mSliders.ColorToken.ModifyChannel(alpha: int.Parse(txtAlpha.Text));
					mSliders.SetValue(
						ColorSliderChannelEnum.Alpha, int.Parse(txtAlpha.Text));
				}
				catch { }
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtBlue_Enter																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The blue channel textbox has received focus.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtBlue_Enter(object sender, EventArgs e)
		{
			mFocusControl = txtBlue;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtBlue_Leave																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The blue channel textbox has lost focus.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtBlue_Leave(object sender, EventArgs e)
		{

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtBlue_TextChanged																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The blue channel text has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtBlue_TextChanged(object sender, EventArgs e)
		{
			if(!mTextBusy)
			{
				try
				{
					//mSliders.ColorToken.ModifyChannel(blue: int.Parse(txtBlue.Text));
					mSliders.SetValue(
						ColorSliderChannelEnum.Blue, int.Parse(txtBlue.Text));
				}
				catch { }
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtGreen_Enter																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The green channel textbox has received focus.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtGreen_Enter(object sender, EventArgs e)
		{
			mFocusControl = txtGreen;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtGreen_Leave																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The green channel textbox has lost focus.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtGreen_Leave(object sender, EventArgs e)
		{

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtGreen_TextChanged																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The green channel text has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtGreen_TextChanged(object sender, EventArgs e)
		{
			if(!mTextBusy)
			{
				try
				{
					//mSliders.ColorToken.ModifyChannel(green: int.Parse(txtGreen.Text));
					mSliders.SetValue(
						ColorSliderChannelEnum.Green, int.Parse(txtGreen.Text));
				}
				catch { }
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtHex_Leave																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Hex textbox has lost the focus.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtHex_Leave(object sender, EventArgs e)
		{
			if(mTextHexChanged && !mColorBusy)
			{
				this.Color = FromHex(txtHex.Text);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtHex_TextChanged																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The text in the hex textbox has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtHex_TextChanged(object sender, EventArgs e)
		{
			if(!mColorBusy)
			{
				mTextHexChanged = true;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtHue_Enter																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The hue textbox has received focus.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtHue_Enter(object sender, EventArgs e)
		{
			mFocusControl = txtHue;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtHue_Leave																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The hue textbox has lost focus.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtHue_Leave(object sender, EventArgs e)
		{

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtHue_TextChanged																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The hue channel text has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtHue_TextChanged(object sender, EventArgs e)
		{
			if(!mTextBusy)
			{
				try
				{
					//mSliders.ColorToken.ModifyChannel(hue: int.Parse(txtHue.Text));
					mSliders.SetValue(ColorSliderChannelEnum.Hue,
						(int)((float.Parse(txtHue.Text) / 360f) * 240f));
				}
				catch { }
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtLuminance_Enter																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The luminance textbox has received focus.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtLuminance_Enter(object sender, EventArgs e)
		{
			mFocusControl = txtLuminance;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtLuminance_Leave																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The luminance textbox has lost focus.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtLuminance_Leave(object sender, EventArgs e)
		{

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtLuminance_TextChanged																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The luminance channel text has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtLuminance_TextChanged(object sender, EventArgs e)
		{
			if(!mTextBusy)
			{
				try
				{
					//mSliders.ColorToken.
					//	ModifyChannel(luminance: int.Parse(txtLuminance.Text));
					mSliders.SetValue(ColorSliderChannelEnum.Luminance,
						(int)(float.Parse(txtLuminance.Text) * 240f));
				}
				catch { }
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtRed_Enter																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The red channel textbox has received focus.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtRed_Enter(object sender, EventArgs e)
		{
			mFocusControl = txtRed;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtRed_Leave																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The red channel textbox has received focus.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtRed_Leave(object sender, EventArgs e)
		{

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtRed_TextChanged																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The red channel text has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtRed_TextChanged(object sender, EventArgs e)
		{
			if(!mTextBusy)
			{
				try
				{
					//mSliders.ColorToken.ModifyChannel(red: int.Parse(txtRed.Text));
					mSliders.SetValue(
						ColorSliderChannelEnum.Red, int.Parse(txtRed.Text));
				}
				catch { }
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtSaturation_Enter																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The saturation textbox has received focus.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtSaturation_Enter(object sender, EventArgs e)
		{
			mFocusControl = txtSaturation;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtSaturation_Leave																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The saturation textbox has lost focus.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtSaturation_Leave(object sender, EventArgs e)
		{

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtSaturation_TextChanged																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The saturation channel text has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtSaturation_TextChanged(object sender, EventArgs e)
		{
			if(!mTextBusy)
			{
				try
				{
					//mSliders.ColorToken.
					//	ModifyChannel(saturation: int.Parse(txtSaturation.Text));
					mSliders.SetValue(ColorSliderChannelEnum.Saturation,
						(int)(float.Parse(txtSaturation.Text) * 240f));
				}
				catch { }
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UpdateHex																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the Hex text value.
		/// </summary>
		private void UpdateHex()
		{
			mTextBusy = true;
			if(mFocusControl != txtHex)
			{
				txtHex.Text = $"#{mColor.R:X2}{mColor.G:X2}{mColor.B:X2}{mColor.A:X2}";
			}
			mTextBusy = false;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	UpdateHSL																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the Hue, Saturation, Luminance control values.
		/// </summary>
		private void UpdateHSL()
		{
			mTextBusy = true;
			if(mFocusControl != txtHue)
			{
				txtHue.Text = mColor.GetHue().ToString("0.0");
			}
			if(mFocusControl != txtSaturation)
			{
				txtSaturation.Text = mColor.GetSaturation().ToString("0.000");
			}
			if(mFocusControl != txtLuminance)
			{
				txtLuminance.Text = mColor.GetBrightness().ToString("0.000");
			}
			mTextBusy = false;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	UpdateRGB																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the Red, Green, Blue control values.
		/// </summary>
		private void UpdateRGB()
		{
			mTextBusy = true;
			if(mFocusControl != txtRed)
			{
				txtRed.Text = mColor.R.ToString();
			}
			if(mFocusControl != txtGreen)
			{
				txtGreen.Text = mColor.G.ToString();
			}
			if(mFocusControl != txtBlue)
			{
				txtBlue.Text = mColor.B.ToString();
			}
			if(mFocusControl != txtAlpha)
			{
				txtAlpha.Text = mColor.A.ToString();
			}
			mTextBusy = false;
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnActivated																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the Activated event when the form has been displayed.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			if(!mOriginalColorSet)
			{
				//	If no default color has been set, use black.
				Color = FromHex("#000000FF");
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
		/// Create a new instance of the frmSelectColor Item.
		/// </summary>
		public frmColorSelect()
		{
			InitializeComponent();
			this.DoubleBuffered = true;
			ColorMode = ColorSliderModeEnum.HSL;
			//	Hover info for basic colors.
			foreach(Label label in tblLayoutBasic.Controls)
			{
				label.Click += lblBasicColor_Click;
				label.MouseEnter += lblBasicColor_MouseEnter;
			}
			//	Hover info for recent colors.
			foreach(Label label in tblLayoutRecent.Controls)
			{
				label.Click += lblBasicColor_Click;
				label.MouseEnter += lblBasicColor_MouseEnter;
			}
			//	Hover info for selected colors.
			foreach(Label label in tblLayoutSelected.Controls)
			{
				label.Click += lblBasicColor_Click;
				label.MouseEnter += lblBasicColor_MouseEnter;
			}
			//	Slider controls.
			mSliders.Color = lblColorSelected.BackColor;
			mSliders.Add(
				ctlHR, ColorSliderChannelEnum.Hue, ColorSliderModeEnum.HSL);
			mSliders.Add(
				ctlSG, ColorSliderChannelEnum.Saturation, ColorSliderModeEnum.HSL);
			mSliders.Add(
				ctlLB, ColorSliderChannelEnum.Luminance, ColorSliderModeEnum.HSL);
			mSliders.Add(
				ctlAlpha, ColorSliderChannelEnum.Alpha, ColorSliderModeEnum.HSL);
			mSliders.ColorChanged += mSliders_ColorChanged;
			mSliders.FocusReceived += mSliders_FocusReceived;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Color																																	*
		//*-----------------------------------------------------------------------*
		private System.Drawing.Color mColor = System.Drawing.Color.White;
		private bool mColorBusy = false;
		/// <summary>
		/// Get/Set the selected color.
		/// </summary>
		public System.Drawing.Color Color
		{
			get { return mColor; }
			set
			{
				Color original = mColor;
				mColor = value;
				if(!mColorBusy)
				{
					mColorBusy = true;
					UpdateHex();
					UpdateRGB();
					UpdateHSL();
					mColorBusy = false;
				}
				lblColorSelected.BackColor = value;
				if(!mOriginalColorSet)
				{
					lblColorOriginal.BackColor = value;
					mOriginalColorSet = true;
				}
				if(!mSliders.ColorBusy)
				{
					mSliders.ColorBusy = true;
					mSliders.Color = value;
					mSliders.ColorBusy = false;
				}
				else
				{
					mSliders.Invalidate();
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ColorMode																															*
		//*-----------------------------------------------------------------------*
		private ColorSliderModeEnum mColorMode = ColorSliderModeEnum.HSL;
		/// <summary>
		/// Get/Set the current color mode of this dialog.
		/// </summary>
		public ColorSliderModeEnum ColorMode
		{
			get { return mColorMode; }
			set
			{
				mColorMode = value;
				mFocusControl = null;
				mSliders.SetMode(mColorMode);
				if(mColorMode == ColorSliderModeEnum.HSL)
				{
					lblHR.Text = "H";
					lblSG.Text = "S";
					lblLB.Text = "L";
					txtHue.ReadOnly = false;
					txtHue.BackColor = SystemColors.Window;
					txtSaturation.ReadOnly = false;
					txtSaturation.BackColor = SystemColors.Window;
					txtLuminance.ReadOnly = false;
					txtLuminance.BackColor = SystemColors.Window;
					txtRed.ReadOnly = true;
					txtRed.BackColor = SystemColors.Control;
					txtGreen.ReadOnly = true;
					txtGreen.BackColor = SystemColors.Control;
					txtBlue.ReadOnly = true;
					txtBlue.BackColor = SystemColors.Control;
				}
				else if(mColorMode == ColorSliderModeEnum.RGB)
				{
					lblHR.Text = "R";
					lblSG.Text = "G";
					lblLB.Text = "B";
					txtRed.ReadOnly = false;
					txtRed.BackColor = SystemColors.Window;
					txtGreen.ReadOnly = false;
					txtGreen.BackColor = SystemColors.Window;
					txtBlue.ReadOnly = false;
					txtBlue.BackColor = SystemColors.Window;
					txtHue.ReadOnly = true;
					txtHue.BackColor = SystemColors.Control;
					txtSaturation.ReadOnly = true;
					txtSaturation.BackColor = SystemColors.Control;
					txtLuminance.ReadOnly = true;
					txtLuminance.BackColor = SystemColors.Control;
				}
			}
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

}
