//	frmPPAnimation.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Office.Interop.PowerPoint;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

using static Scaffold.ScaffoldNodesUtil;
using static Scaffold.ScaffoldUtil;
using System.Diagnostics;

//	TODO: !1 - Stopped here.
//	TODO: Return the values to the caller and call the PowerPoint driver.
namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	frmPPAnimation																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// User input for the PowerPoint / Text And Shape Alignment function..
	/// </summary>
	public class frmPPAnimation : ThemedForm
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private Label lblSlide;
		private Button btnCancel;
		private ListView lvShapes;
		private Label lblShapes;
		private Button btnOK;
		private ImageList ilShapes;
		private GroupBox grpSequence;
		private NumericUpDown txtSlide;
		private CheckBox chkEffectExit;
		private TextBox txtEffectDuration;
		private TextBox txtNextDelay;
		private TextBox txtStartDelay;
		private ComboBox cmboEffect;
		private Label lblEffectDurationMeasure;
		private ComboBox cmboNext;
		private ComboBox cmboStartType;
		private Label lblNextDelay;
		private Label lblStartDelay;
		private Label lblEffect;
		private Label lblEffectDuration;
		private Label lblPerItem;
		private Label lblNext;
		private Label label1;

		//*-----------------------------------------------------------------------*
		//* btnCancel_Click																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The Cancel button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Hide();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnOK_Click																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The OK button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnOK_Click(object sender, EventArgs e)
		{
			btnOK.Focus();
			this.DialogResult = DialogResult.OK;
			this.Hide();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* cmboNext_SelectedIndexChanged																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The selected index has changed on the next item event drop down list.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void cmboNext_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(cmboNext.SelectedItem != null)
			{
				txtNextDelay.Visible = lblNextDelay.Visible =
					(((string)cmboNext.SelectedItem) == "After delay");
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* cmboStartType_SelectedIndexChanged																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The selected index has changed on the start event drop down list.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void cmboStartType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(cmboStartType.SelectedItem != null)
			{
				txtStartDelay.Visible = lblStartDelay.Visible =
					(((string)cmboStartType.SelectedItem) == "After delay");
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* InitializeComponent																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Initialize the form.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.lblSlide = new System.Windows.Forms.Label();
			this.lblShapes = new System.Windows.Forms.Label();
			this.lvShapes = new System.Windows.Forms.ListView();
			this.ilShapes = new System.Windows.Forms.ImageList(this.components);
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.grpSequence = new System.Windows.Forms.GroupBox();
			this.chkEffectExit = new System.Windows.Forms.CheckBox();
			this.txtEffectDuration = new System.Windows.Forms.TextBox();
			this.txtNextDelay = new System.Windows.Forms.TextBox();
			this.txtStartDelay = new System.Windows.Forms.TextBox();
			this.cmboEffect = new System.Windows.Forms.ComboBox();
			this.lblEffectDurationMeasure = new System.Windows.Forms.Label();
			this.cmboNext = new System.Windows.Forms.ComboBox();
			this.cmboStartType = new System.Windows.Forms.ComboBox();
			this.lblNextDelay = new System.Windows.Forms.Label();
			this.lblStartDelay = new System.Windows.Forms.Label();
			this.lblEffect = new System.Windows.Forms.Label();
			this.lblEffectDuration = new System.Windows.Forms.Label();
			this.lblPerItem = new System.Windows.Forms.Label();
			this.lblNext = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtSlide = new System.Windows.Forms.NumericUpDown();
			this.pnlMain.SuspendLayout();
			this.grpSequence.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtSlide)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.txtSlide);
			this.pnlMain.Controls.Add(this.grpSequence);
			this.pnlMain.Controls.Add(this.btnOK);
			this.pnlMain.Controls.Add(this.btnCancel);
			this.pnlMain.Controls.Add(this.lvShapes);
			this.pnlMain.Controls.Add(this.lblShapes);
			this.pnlMain.Controls.Add(this.lblSlide);
			this.pnlMain.Size = new System.Drawing.Size(726, 419);
			// 
			// lblSlide
			// 
			this.lblSlide.AutoSize = true;
			this.lblSlide.ForeColor = System.Drawing.Color.Gainsboro;
			this.lblSlide.Location = new System.Drawing.Point(12, 10);
			this.lblSlide.Name = "lblSlide";
			this.lblSlide.Size = new System.Drawing.Size(51, 20);
			this.lblSlide.TabIndex = 0;
			this.lblSlide.Text = "&Slide:";
			// 
			// lblShapes
			// 
			this.lblShapes.AutoSize = true;
			this.lblShapes.ForeColor = System.Drawing.Color.Gainsboro;
			this.lblShapes.Location = new System.Drawing.Point(12, 50);
			this.lblShapes.Name = "lblShapes";
			this.lblShapes.Size = new System.Drawing.Size(70, 20);
			this.lblShapes.TabIndex = 2;
			this.lblShapes.Text = "S&hapes:";
			// 
			// lvShapes
			// 
			this.lvShapes.BackColor = System.Drawing.Color.White;
			this.lvShapes.ForeColor = System.Drawing.Color.Black;
			this.lvShapes.HideSelection = false;
			this.lvShapes.Location = new System.Drawing.Point(12, 74);
			this.lvShapes.Name = "lvShapes";
			this.lvShapes.Size = new System.Drawing.Size(297, 339);
			this.lvShapes.SmallImageList = this.ilShapes;
			this.lvShapes.TabIndex = 3;
			this.lvShapes.UseCompatibleStateImageBehavior = false;
			this.lvShapes.View = System.Windows.Forms.View.List;
			// 
			// ilShapes
			// 
			this.ilShapes.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.ilShapes.ImageSize = new System.Drawing.Size(24, 24);
			this.ilShapes.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(527, 356);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(88, 45);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Enabled = false;
			this.btnOK.Location = new System.Drawing.Point(621, 356);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(88, 45);
			this.btnOK.TabIndex = 6;
			this.btnOK.Text = "&OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// grpSequence
			// 
			this.grpSequence.Controls.Add(this.chkEffectExit);
			this.grpSequence.Controls.Add(this.txtEffectDuration);
			this.grpSequence.Controls.Add(this.txtNextDelay);
			this.grpSequence.Controls.Add(this.txtStartDelay);
			this.grpSequence.Controls.Add(this.cmboEffect);
			this.grpSequence.Controls.Add(this.lblEffectDurationMeasure);
			this.grpSequence.Controls.Add(this.cmboNext);
			this.grpSequence.Controls.Add(this.cmboStartType);
			this.grpSequence.Controls.Add(this.lblNextDelay);
			this.grpSequence.Controls.Add(this.lblStartDelay);
			this.grpSequence.Controls.Add(this.lblEffect);
			this.grpSequence.Controls.Add(this.lblEffectDuration);
			this.grpSequence.Controls.Add(this.lblPerItem);
			this.grpSequence.Controls.Add(this.lblNext);
			this.grpSequence.Controls.Add(this.label1);
			this.grpSequence.ForeColor = System.Drawing.Color.Gainsboro;
			this.grpSequence.Location = new System.Drawing.Point(334, 50);
			this.grpSequence.Name = "grpSequence";
			this.grpSequence.Size = new System.Drawing.Size(375, 225);
			this.grpSequence.TabIndex = 4;
			this.grpSequence.TabStop = false;
			this.grpSequence.Text = "Animation Sequence";
			// 
			// chkEffectExit
			// 
			this.chkEffectExit.AutoSize = true;
			this.chkEffectExit.Location = new System.Drawing.Point(275, 101);
			this.chkEffectExit.Name = "chkEffectExit";
			this.chkEffectExit.Size = new System.Drawing.Size(59, 24);
			this.chkEffectExit.TabIndex = 7;
			this.chkEffectExit.Text = "Exit";
			this.chkEffectExit.UseVisualStyleBackColor = true;
			// 
			// txtEffectDuration
			// 
			this.txtEffectDuration.Location = new System.Drawing.Point(268, 140);
			this.txtEffectDuration.Name = "txtEffectDuration";
			this.txtEffectDuration.Size = new System.Drawing.Size(53, 27);
			this.txtEffectDuration.TabIndex = 9;
			this.txtEffectDuration.Text = "0.5";
			// 
			// txtNextDelay
			// 
			this.txtNextDelay.Location = new System.Drawing.Point(268, 185);
			this.txtNextDelay.Name = "txtNextDelay";
			this.txtNextDelay.Size = new System.Drawing.Size(53, 27);
			this.txtNextDelay.TabIndex = 13;
			this.txtNextDelay.Text = "0.5";
			// 
			// txtStartDelay
			// 
			this.txtStartDelay.Location = new System.Drawing.Point(268, 29);
			this.txtStartDelay.Name = "txtStartDelay";
			this.txtStartDelay.Size = new System.Drawing.Size(53, 27);
			this.txtStartDelay.TabIndex = 2;
			this.txtStartDelay.Text = "1.0";
			// 
			// cmboEffect
			// 
			this.cmboEffect.FormattingEnabled = true;
			this.cmboEffect.Items.AddRange(new object[] {
            "Appear",
            "Bounce",
            "Fade",
            "Flash Once",
            "Float Down",
            "Float Up",
            "Fly Down",
            "Fly Down And Left",
            "Fly Down And Right",
            "Fly Left",
            "Fly Right",
            "Fly Up",
            "Fly Up And Left",
            "Fly Up And Right",
            "Split Horizontal In",
            "Split Horizontal Out",
            "Split Vertical In",
            "Split Vertical Out",
            "Teeter",
            "Wipe Down",
            "Wipe Left",
            "Wipe Right",
            "Wipe Up"});
			this.cmboEffect.Location = new System.Drawing.Point(100, 99);
			this.cmboEffect.Name = "cmboEffect";
			this.cmboEffect.Size = new System.Drawing.Size(162, 28);
			this.cmboEffect.TabIndex = 6;
			// 
			// lblEffectDurationMeasure
			// 
			this.lblEffectDurationMeasure.AutoSize = true;
			this.lblEffectDurationMeasure.ForeColor = System.Drawing.Color.Gainsboro;
			this.lblEffectDurationMeasure.Location = new System.Drawing.Point(327, 143);
			this.lblEffectDurationMeasure.Name = "lblEffectDurationMeasure";
			this.lblEffectDurationMeasure.Size = new System.Drawing.Size(36, 20);
			this.lblEffectDurationMeasure.TabIndex = 10;
			this.lblEffectDurationMeasure.Text = "sec";
			// 
			// cmboNext
			// 
			this.cmboNext.FormattingEnabled = true;
			this.cmboNext.Items.AddRange(new object[] {
            "After click",
            "After delay",
            "Immediately"});
			this.cmboNext.Location = new System.Drawing.Point(100, 184);
			this.cmboNext.Name = "cmboNext";
			this.cmboNext.Size = new System.Drawing.Size(162, 28);
			this.cmboNext.TabIndex = 12;
			// 
			// cmboStartType
			// 
			this.cmboStartType.FormattingEnabled = true;
			this.cmboStartType.Items.AddRange(new object[] {
            "After click",
            "After delay",
            "Immediately"});
			this.cmboStartType.Location = new System.Drawing.Point(100, 28);
			this.cmboStartType.Name = "cmboStartType";
			this.cmboStartType.Size = new System.Drawing.Size(162, 28);
			this.cmboStartType.TabIndex = 1;
			// 
			// lblNextDelay
			// 
			this.lblNextDelay.AutoSize = true;
			this.lblNextDelay.ForeColor = System.Drawing.Color.Gainsboro;
			this.lblNextDelay.Location = new System.Drawing.Point(327, 188);
			this.lblNextDelay.Name = "lblNextDelay";
			this.lblNextDelay.Size = new System.Drawing.Size(36, 20);
			this.lblNextDelay.TabIndex = 14;
			this.lblNextDelay.Text = "sec";
			// 
			// lblStartDelay
			// 
			this.lblStartDelay.AutoSize = true;
			this.lblStartDelay.ForeColor = System.Drawing.Color.Gainsboro;
			this.lblStartDelay.Location = new System.Drawing.Point(327, 32);
			this.lblStartDelay.Name = "lblStartDelay";
			this.lblStartDelay.Size = new System.Drawing.Size(36, 20);
			this.lblStartDelay.TabIndex = 3;
			this.lblStartDelay.Text = "sec";
			// 
			// lblEffect
			// 
			this.lblEffect.AutoSize = true;
			this.lblEffect.ForeColor = System.Drawing.Color.Gainsboro;
			this.lblEffect.Location = new System.Drawing.Point(15, 102);
			this.lblEffect.Name = "lblEffect";
			this.lblEffect.Size = new System.Drawing.Size(58, 20);
			this.lblEffect.TabIndex = 5;
			this.lblEffect.Text = "Effect:";
			// 
			// lblEffectDuration
			// 
			this.lblEffectDuration.AutoSize = true;
			this.lblEffectDuration.ForeColor = System.Drawing.Color.Gainsboro;
			this.lblEffectDuration.Location = new System.Drawing.Point(15, 142);
			this.lblEffectDuration.Name = "lblEffectDuration";
			this.lblEffectDuration.Size = new System.Drawing.Size(127, 20);
			this.lblEffectDuration.TabIndex = 8;
			this.lblEffectDuration.Text = "Effect Duration:";
			// 
			// lblPerItem
			// 
			this.lblPerItem.AutoSize = true;
			this.lblPerItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPerItem.ForeColor = System.Drawing.Color.Gainsboro;
			this.lblPerItem.Location = new System.Drawing.Point(15, 67);
			this.lblPerItem.Name = "lblPerItem";
			this.lblPerItem.Size = new System.Drawing.Size(197, 20);
			this.lblPerItem.TabIndex = 4;
			this.lblPerItem.Text = "Per Item                         ";
			// 
			// lblNext
			// 
			this.lblNext.AutoSize = true;
			this.lblNext.ForeColor = System.Drawing.Color.Gainsboro;
			this.lblNext.Location = new System.Drawing.Point(15, 187);
			this.lblNext.Name = "lblNext";
			this.lblNext.Size = new System.Drawing.Size(85, 20);
			this.lblNext.TabIndex = 11;
			this.lblNext.Text = "Next Item:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.Gainsboro;
			this.label1.Location = new System.Drawing.Point(15, 31);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(50, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "S&tart:";
			// 
			// txtSlide
			// 
			this.txtSlide.Location = new System.Drawing.Point(69, 8);
			this.txtSlide.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.txtSlide.Name = "txtSlide";
			this.txtSlide.Size = new System.Drawing.Size(111, 27);
			this.txtSlide.TabIndex = 1;
			this.txtSlide.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// frmPPAnimation
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(726, 517);
			this.Name = "frmPPAnimation";
			this.pnlMain.ResumeLayout(false);
			this.pnlMain.PerformLayout();
			this.grpSequence.ResumeLayout(false);
			this.grpSequence.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtSlide)).EndInit();
			this.ResumeLayout(false);

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* lvShapes_ItemSelectionChanged																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Item selection has changed in the shapes list view.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// ListView item selection changed event arguments.
		/// </param>
		private void lvShapes_ItemSelectionChanged(object sender,
			ListViewItemSelectionChangedEventArgs e)
		{
			UpdateOKEnabled();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtSlide_TextChanged																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The value of the slide number textbox has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtSlide_TextChanged(object sender, EventArgs e)
		{
			ListViewItem item = null;
			string name = "";
			int slideIndex = ToInt(txtSlide.Text);
			Shapes shapes = null;
			Slide slide = null;

			lvShapes.Items.Clear();
			if(mDriver != null)
			{
				if(slideIndex > 0)
				{
					slide = PowerPointDriver.GetSlideBySlideIndex(
						mDriver.ActivePresentation, slideIndex);
					if(slide != null)
					{
						//	Slide found.
						txtSlide.BackColor =
							FromHex(ResourceMain.colorBackgroundWriting);
						shapes = slide.Shapes;
						foreach(PowerPoint.Shape shape in shapes)
						{
							name = LimitLength(PowerPointDriver.GetText(shape), 32);
							if(name == null || name.Length == 0)
							{
								name = shape.Name;
							}
							item = lvShapes.Items.Add(name, shape.Type.ToString());
							item.Tag = shape.Name;
						}
					}
					else
					{
						//	Slide not found.
						txtSlide.BackColor =
							FromHex(ResourceMain.colorBackgroundWritingError);
					}
				}
				else
				{
					//	Non-numeric index.
					txtSlide.BackColor =
						FromHex(ResourceMain.colorBackgroundWritingError);
				}
			}
			else
			{
				//	Driver not assigned.
				txtSlide.BackColor = FromHex(ResourceMain.colorBackgroundWritingError);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UpdateOKEnabled																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the OK button enabled state based upon current conditions on
		/// the form.
		/// </summary>
		private void UpdateOKEnabled()
		{
			btnOK.Enabled = (lvShapes.SelectedItems.Count > 0);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* Dispose																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">
		/// True if managed resources should be disposed. Otherwise, false.
		/// </param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	OnLoad																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the Load event when the form has loaded and is ready to display
		/// for the first time.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			cmboStartType.SelectedItem = "After delay";
			cmboEffect.SelectedItem = "Appear";
			cmboNext.SelectedItem = "After delay";
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the frmPPPlaceholderToTextbox Item.
		/// </summary>
		public frmPPAnimation()
		{
			InitializeComponent();
			this.StartPosition = FormStartPosition.CenterParent;
			this.Title = "Quick Animation";
			this.menuThemedForm.Visible = false;
			this.statusThemedForm.Visible = false;

			btnCancel.Click += btnCancel_Click;
			btnOK.Click += btnOK_Click;
			cmboStartType.SelectedIndexChanged += cmboStartType_SelectedIndexChanged;
			cmboNext.SelectedIndexChanged += cmboNext_SelectedIndexChanged;
			lvShapes.ItemSelectionChanged += lvShapes_ItemSelectionChanged;
			txtSlide.TextChanged += txtSlide_TextChanged;

			//	List shapes.
			LoadImageListMsoShapeType(ilShapes);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Direction																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the direction of the effect in this instance.
		/// </summary>
		public MsoAnimDirection Direction
		{
			get
			{
				MsoAnimDirection result = MsoAnimDirection.msoAnimDirectionNone;
				string value = "";

				if(cmboEffect.SelectedItem != null)
				{
					value = (string)cmboEffect.SelectedItem;
				}
				switch(value)
				{
					case "Appear":
						break;
					case "Bounce":
						result = MsoAnimDirection.msoAnimDirectionUp;
						break;
					case "Fade":
						break;
					case "Flash Once":
						break;
					case "Float Down":
						result = MsoAnimDirection.msoAnimDirectionDown;
						break;
					case "Float Up":
						result = MsoAnimDirection.msoAnimDirectionUp;
						break;
					case "Fly Down":
						result = MsoAnimDirection.msoAnimDirectionDown;
						break;
					case "Fly Down And Left":
						result = MsoAnimDirection.msoAnimDirectionDownLeft;
						break;
					case "Fly Down And Right":
						result = MsoAnimDirection.msoAnimDirectionDownRight;
						break;
					case "Fly Left":
						result = MsoAnimDirection.msoAnimDirectionLeft;
						break;
					case "Fly Right":
						result = MsoAnimDirection.msoAnimDirectionRight;
						break;
					case "Fly Up":
						result = MsoAnimDirection.msoAnimDirectionUp;
						break;
					case "Fly Up And Left":
						result = MsoAnimDirection.msoAnimDirectionUpLeft;
						break;
					case "Fly Up And Right":
						result = MsoAnimDirection.msoAnimDirectionUpRight;
						break;
					case "Split Horizontal In":
						result = MsoAnimDirection.msoAnimDirectionIn;
						break;
					case "Split Horizontal Out":
						result = MsoAnimDirection.msoAnimDirectionOut;
						break;
					case "Split Vertical In":
						result = MsoAnimDirection.msoAnimDirectionIn;
						break;
					case "Split Vertical Out":
						result = MsoAnimDirection.msoAnimDirectionOut;
						break;
					case "Teeter":
						break;
					case "Wipe Down":
						result = MsoAnimDirection.msoAnimDirectionDown;
						break;
					case "Wipe Left":
						result = MsoAnimDirection.msoAnimDirectionLeft;
						break;
					case "Wipe Right":
						result = MsoAnimDirection.msoAnimDirectionRight;
						break;
					case "Wipe Up":
						result = MsoAnimDirection.msoAnimDirectionUp;
						break;
				}
				return result;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Driver																																*
		//*-----------------------------------------------------------------------*
		private PowerPointDriver mDriver = null;
		/// <summary>
		/// Get/Set a reference to the active PowerPoint office driver for this
		/// instance.
		/// </summary>
		public PowerPointDriver Driver
		{
			get { return mDriver; }
			set
			{
				mDriver = value;
				if(mDriver != null)
				{
					txtSlide.Maximum = mDriver.SlideCount;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Effect																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the selected effect.
		/// </summary>
		public MsoAnimEffect Effect
		{
			get
			{
				MsoAnimEffect result = MsoAnimEffect.msoAnimEffectAppear;
				string value = "";

				if(cmboEffect.SelectedItem != null)
				{
					value = (string)cmboEffect.SelectedItem;
				}
				switch(value)
				{
					case "Appear":
						result = MsoAnimEffect.msoAnimEffectAppear;
						break;
					case "Bounce":
						result = MsoAnimEffect.msoAnimEffectBounce;
						break;
					case "Fade":
						result = MsoAnimEffect.msoAnimEffectFade;
						break;
					case "Flash Once":
						result = MsoAnimEffect.msoAnimEffectFlashOnce;
						break;
					case "Float Down":
					case "Float Up":
						result = MsoAnimEffect.msoAnimEffectFloat;
						break;
					case "Fly Down":
					case "Fly Down And Left":
					case "Fly Down And Right":
					case "Fly Left":
					case "Fly Right":
					case "Fly Up":
					case "Fly Up And Left":
					case "Fly Up And Right":
						result = MsoAnimEffect.msoAnimEffectFly;
						break;
					case "Split Horizontal In":
					case "Split Horizontal Out":
					case "Split Vertical In":
					case "Split Vertical Out":
						result = MsoAnimEffect.msoAnimEffectSplit;
						break;
					case "Teeter":
						result = MsoAnimEffect.msoAnimEffectTeeter;
						break;
					case "Wipe Down":
					case "Wipe Left":
					case "Wipe Right":
					case "Wipe Up":
						result = MsoAnimEffect.msoAnimEffectWipe;
						break;
				}
				return result;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	EffectDuration																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the duration of the item effect.
		/// </summary>
		public float EffectDuration
		{
			get { return ToFloat(txtEffectDuration.Text); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	EffectExit																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get a value indicating whether the item effect is for exit.
		/// </summary>
		public bool EffectExit
		{
			get
			{
				return chkEffectExit.Checked;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NextDelayTime																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the delay time for the next item delay.
		/// </summary>
		public float NextDelayTime
		{
			get { return ToFloat(txtNextDelay.Text); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NextDelayType																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the delay type used at the next item of the sequence.
		/// </summary>
		public EventDelayTypeEnum NextDelayType
		{
			get
			{
				EventDelayTypeEnum result = EventDelayTypeEnum.None;
				string value = "";

				if(cmboStartType.SelectedItem != null)
				{
					value = (string)cmboNext.SelectedItem;
				}
				switch(value)
				{
					case "After click":
						result = EventDelayTypeEnum.AfterClick;
						break;
					case "After delay":
						result = EventDelayTypeEnum.AfterDelay;
						break;
					case "Immediately":
						result = EventDelayTypeEnum.Immediately;
						break;
				}
				return result;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SelectedShapeNames																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get a reference to the list of selected object names.
		/// </summary>
		public List<string> SelectedShapeNames
		{
			get
			{
				List<string> result = new List<string>();

				foreach(ListViewItem listItem in lvShapes.SelectedItems)
				{
					result.Add((string)listItem.Tag);
				}
				return result;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SlideIndex																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the selected slide index.
		/// </summary>
		public int SlideIndex
		{
			get { return Convert.ToInt32(txtSlide.Value); }
			set { txtSlide.Value = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	StartDelayTime																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the delay time for the start delay.
		/// </summary>
		public float StartDelayTime
		{
			get { return ToFloat(txtStartDelay.Text); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	StartDelayType																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the delay type used at the start of the sequence.
		/// </summary>
		public EventDelayTypeEnum StartDelayType
		{
			get
			{
				EventDelayTypeEnum result = EventDelayTypeEnum.None;
				string value = "";

				if(cmboStartType.SelectedItem != null)
				{
					value = (string)cmboStartType.SelectedItem;
				}
				switch(value)
				{
					case "After click":
						result = EventDelayTypeEnum.AfterClick;
						break;
					case "After delay":
						result = EventDelayTypeEnum.AfterDelay;
						break;
					case "Immediately":
						result = EventDelayTypeEnum.Immediately;
						break;
				}
				return result;
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}

