//	frmPPRemoveBullet.cs
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

using Microsoft.Office.Interop.PowerPoint;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

using static Scaffold.ScaffoldNodesUtil;
using static Scaffold.ScaffoldUtil;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	frmPPRemoveBullet																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// PowerPoint Content Placeholder To Textboxes user input form.
	/// </summary>
	public class frmPPRemoveBullet : ThemedForm
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		private System.Windows.Forms.GroupBox grpScope;
		private System.Windows.Forms.NumericUpDown txtScopeIndex;
		private System.Windows.Forms.RadioButton optScopeIndex;
		private System.Windows.Forms.RadioButton optScopeCurrent;
		private System.Windows.Forms.RadioButton optScopeAll;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox grpResolution;
		private System.Windows.Forms.RadioButton optObjectSelected;
		private ListView lvShapes;
		private ImageList ilShapes;
		private System.Windows.Forms.RadioButton optObjectAll;

		//*-----------------------------------------------------------------------*
		//* btnCancel_Click																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The cancel button has been clicked.
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
			this.DialogResult = DialogResult.OK;
			this.Hide();
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
			this.grpScope = new System.Windows.Forms.GroupBox();
			this.txtScopeIndex = new System.Windows.Forms.NumericUpDown();
			this.optScopeIndex = new System.Windows.Forms.RadioButton();
			this.optScopeCurrent = new System.Windows.Forms.RadioButton();
			this.optScopeAll = new System.Windows.Forms.RadioButton();
			this.grpResolution = new System.Windows.Forms.GroupBox();
			this.lvShapes = new System.Windows.Forms.ListView();
			this.ilShapes = new System.Windows.Forms.ImageList(this.components);
			this.optObjectSelected = new System.Windows.Forms.RadioButton();
			this.optObjectAll = new System.Windows.Forms.RadioButton();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.pnlMain.SuspendLayout();
			this.grpScope.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtScopeIndex)).BeginInit();
			this.grpResolution.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.btnOK);
			this.pnlMain.Controls.Add(this.btnCancel);
			this.pnlMain.Controls.Add(this.grpResolution);
			this.pnlMain.Controls.Add(this.grpScope);
			this.pnlMain.Size = new System.Drawing.Size(617, 301);
			this.pnlMain.Controls.SetChildIndex(this.grpScope, 0);
			this.pnlMain.Controls.SetChildIndex(this.grpResolution, 0);
			this.pnlMain.Controls.SetChildIndex(this.btnCancel, 0);
			this.pnlMain.Controls.SetChildIndex(this.btnOK, 0);
			// 
			// grpScope
			// 
			this.grpScope.Controls.Add(this.txtScopeIndex);
			this.grpScope.Controls.Add(this.optScopeIndex);
			this.grpScope.Controls.Add(this.optScopeCurrent);
			this.grpScope.Controls.Add(this.optScopeAll);
			this.grpScope.ForeColor = System.Drawing.Color.Gainsboro;
			this.grpScope.Location = new System.Drawing.Point(12, 13);
			this.grpScope.Name = "grpScope";
			this.grpScope.Size = new System.Drawing.Size(244, 130);
			this.grpScope.TabIndex = 0;
			this.grpScope.TabStop = false;
			this.grpScope.Text = "Conversion Scope";
			// 
			// txtScopeIndex
			// 
			this.txtScopeIndex.BackColor = System.Drawing.SystemColors.Control;
			this.txtScopeIndex.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.txtScopeIndex.Location = new System.Drawing.Point(162, 85);
			this.txtScopeIndex.Minimum = new decimal(new int[] {
						1,
						0,
						0,
						0});
			this.txtScopeIndex.Name = "txtScopeIndex";
			this.txtScopeIndex.ReadOnly = true;
			this.txtScopeIndex.Size = new System.Drawing.Size(66, 27);
			this.txtScopeIndex.TabIndex = 3;
			this.txtScopeIndex.TabStop = false;
			this.txtScopeIndex.Value = new decimal(new int[] {
						1,
						0,
						0,
						0});
			// 
			// optScopeIndex
			// 
			this.optScopeIndex.AutoSize = true;
			this.optScopeIndex.Location = new System.Drawing.Point(25, 86);
			this.optScopeIndex.Name = "optScopeIndex";
			this.optScopeIndex.Size = new System.Drawing.Size(131, 24);
			this.optScopeIndex.TabIndex = 2;
			this.optScopeIndex.Text = "Slide &Number";
			this.optScopeIndex.UseVisualStyleBackColor = true;
			// 
			// optScopeCurrent
			// 
			this.optScopeCurrent.AutoSize = true;
			this.optScopeCurrent.Checked = true;
			this.optScopeCurrent.Location = new System.Drawing.Point(25, 56);
			this.optScopeCurrent.Name = "optScopeCurrent";
			this.optScopeCurrent.Size = new System.Drawing.Size(167, 24);
			this.optScopeCurrent.TabIndex = 1;
			this.optScopeCurrent.TabStop = true;
			this.optScopeCurrent.Text = "&Current Slide Only";
			this.optScopeCurrent.UseVisualStyleBackColor = true;
			// 
			// optScopeAll
			// 
			this.optScopeAll.AutoSize = true;
			this.optScopeAll.Location = new System.Drawing.Point(25, 26);
			this.optScopeAll.Name = "optScopeAll";
			this.optScopeAll.Size = new System.Drawing.Size(100, 24);
			this.optScopeAll.TabIndex = 0;
			this.optScopeAll.Text = "&All Slides";
			this.optScopeAll.UseVisualStyleBackColor = true;
			// 
			// grpResolution
			// 
			this.grpResolution.Controls.Add(this.lvShapes);
			this.grpResolution.Controls.Add(this.optObjectSelected);
			this.grpResolution.Controls.Add(this.optObjectAll);
			this.grpResolution.ForeColor = System.Drawing.Color.Gainsboro;
			this.grpResolution.Location = new System.Drawing.Point(262, 13);
			this.grpResolution.Name = "grpResolution";
			this.grpResolution.Size = new System.Drawing.Size(343, 220);
			this.grpResolution.TabIndex = 1;
			this.grpResolution.TabStop = false;
			this.grpResolution.Text = "Objects";
			// 
			// lvShapes
			// 
			this.lvShapes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
						| System.Windows.Forms.AnchorStyles.Left) 
						| System.Windows.Forms.AnchorStyles.Right)));
			this.lvShapes.HideSelection = false;
			this.lvShapes.Location = new System.Drawing.Point(25, 86);
			this.lvShapes.Name = "lvShapes";
			this.lvShapes.Size = new System.Drawing.Size(293, 115);
			this.lvShapes.SmallImageList = this.ilShapes;
			this.lvShapes.TabIndex = 2;
			this.lvShapes.UseCompatibleStateImageBehavior = false;
			this.lvShapes.View = System.Windows.Forms.View.List;
			// 
			// ilShapes
			// 
			this.ilShapes.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.ilShapes.ImageSize = new System.Drawing.Size(24, 24);
			this.ilShapes.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// optObjectSelected
			// 
			this.optObjectSelected.AutoSize = true;
			this.optObjectSelected.Location = new System.Drawing.Point(25, 56);
			this.optObjectSelected.Name = "optObjectSelected";
			this.optObjectSelected.Size = new System.Drawing.Size(195, 24);
			this.optObjectSelected.TabIndex = 1;
			this.optObjectSelected.Text = "Selected Shapes &Only";
			this.optObjectSelected.UseVisualStyleBackColor = true;
			// 
			// optObjectAll
			// 
			this.optObjectAll.AutoSize = true;
			this.optObjectAll.Checked = true;
			this.optObjectAll.Location = new System.Drawing.Point(25, 26);
			this.optObjectAll.Name = "optObjectAll";
			this.optObjectAll.Size = new System.Drawing.Size(182, 24);
			this.optObjectAll.TabIndex = 0;
			this.optObjectAll.TabStop = true;
			this.optObjectAll.Text = "All Text With &Bullets";
			this.optObjectAll.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(415, 239);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(92, 39);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(513, 239);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(92, 39);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "&OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// frmPPRemoveBullet
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(617, 349);
			this.Name = "frmPPRemoveBullet";
			this.pnlMain.ResumeLayout(false);
			this.pnlMain.PerformLayout();
			this.grpScope.ResumeLayout(false);
			this.grpScope.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtScopeIndex)).EndInit();
			this.grpResolution.ResumeLayout(false);
			this.grpResolution.PerformLayout();
			this.ResumeLayout(false);

		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* lvShapes_ItemSelectionChanged																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The selection has changed on the shapes list.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// List view selection changed event arguments.
		/// </param>
		private void lvShapes_ItemSelectionChanged(object sender,
			ListViewItemSelectionChangedEventArgs e)
		{
			UpdateOKEnabled();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* optObjectSelected_CheckedChanged																			*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state of the Selected Shapes Only option has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void optObjectSelected_CheckedChanged(object sender, EventArgs e)
		{
			lvShapes.Enabled = optObjectSelected.Checked;
			UpdateOKEnabled();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* optScopeCurrent_CheckedChanged																				*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state has changed on the Current Slide Only option.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void optScopeCurrent_CheckedChanged(object sender, EventArgs e)
		{
			if(optScopeCurrent.Checked && mDriver != null)
			{
				txtScopeIndex.Value = mDriver.ActiveSlideIndex;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* optScopeIndex_CheckedChanged																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state of the option control has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void optScopeIndex_CheckedChanged(object sender, EventArgs e)
		{
			if(optScopeIndex.Checked)
			{
				txtScopeIndex.ReadOnly = false;
				txtScopeIndex.TabStop = true;
				txtScopeIndex.BackColor = FromHex(ResourceMain.colorBackgroundWriting);
				txtScopeIndex.ForeColor = FromHex(ResourceMain.colorTextWriting);
			}
			else
			{
				txtScopeIndex.ReadOnly = true;
				txtScopeIndex.TabStop = false;
				txtScopeIndex.BackColor =
					FromHex(ResourceMain.colorBackgroundWritingDisabled);
				txtScopeIndex.ForeColor =
					FromHex(ResourceMain.colorTextWritingDisabled);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* txtScopeIndex_ValueChanged																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The value of the slide index textbox has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void txtScopeIndex_ValueChanged(object sender, EventArgs e)
		{
			ListViewItem item = null;
			string name = "";
			Shapes shapes = null;
			PowerPoint.Slide slide = null;
			int slideIndex = (int)txtScopeIndex.Value;

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
						if(optScopeIndex.Checked)
						{
							txtScopeIndex.BackColor =
								FromHex(ResourceMain.colorBackgroundWriting);
						}
						else
						{
							txtScopeIndex.BackColor =
								FromHex(ResourceMain.colorBackgroundWritingDisabled);
						}
						shapes = slide.Shapes;
						foreach(PowerPoint.Shape shape in shapes)
						{
							name = LimitLength(PowerPointDriver.GetText(shape), 24);
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
						txtScopeIndex.BackColor =
							FromHex(ResourceMain.colorBackgroundWritingError);
					}
				}
				else
				{
					//	Non-numeric index.
					txtScopeIndex.BackColor =
						FromHex(ResourceMain.colorBackgroundWritingError);
				}
			}
			else
			{
				//	Driver not assigned.
				txtScopeIndex.BackColor =
					FromHex(ResourceMain.colorBackgroundWritingError);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UpdateOKEnabled																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the enabled state of the OK button.
		/// </summary>
		private void UpdateOKEnabled()
		{
			btnOK.Enabled = (optObjectAll.Checked ||
				lvShapes.SelectedItems.Count > 0);
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
		//* OnLoad																																*
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
			UpdateOKEnabled();
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the frmPPRemoveBullet Item.
		/// </summary>
		public frmPPRemoveBullet()
		{
			InitializeComponent();
			this.StartPosition = FormStartPosition.CenterParent;
			this.Title = "Remove Bullet Point";
			this.menuThemedForm.Visible = false;
			this.statusThemedForm.Visible = false;

			lvShapes.ItemSelectionChanged += lvShapes_ItemSelectionChanged;
			optObjectSelected.CheckedChanged += optObjectSelected_CheckedChanged;
			optScopeCurrent.CheckedChanged += optScopeCurrent_CheckedChanged;
			optScopeIndex.CheckedChanged += optScopeIndex_CheckedChanged;
			btnCancel.Click += btnCancel_Click;
			btnOK.Click += btnOK_Click;
			txtScopeIndex.ValueChanged += txtScopeIndex_ValueChanged;

			//	List shapes.
			LoadImageListMsoShapeType(ilShapes);
			lvShapes.Enabled = false;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Driver																																*
		//*-----------------------------------------------------------------------*
		private PowerPointDriver mDriver = null;
		/// <summary>
		/// Get/Set a reference to the PowerPoint driver.
		/// </summary>
		public PowerPointDriver Driver
		{
			get { return mDriver; }
			set
			{
				mDriver = value;
				if(mDriver != null)
				{
					txtScopeIndex.Maximum = mDriver.SlideCount;
					txtScopeIndex.Value = mDriver.ActiveSlideIndex;
				}
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
		//*	SelectedOnly																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get a value indicating whether to handle only the selected items.
		/// </summary>
		/// <remarks>
		/// If false, all objects with bullet formatting will be converted.
		/// </remarks>
		public bool SelectedOnly
		{
			get { return optObjectSelected.Checked; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SlideScope																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the scope of slides to which this conversion will apply.
		/// </summary>
		public SlideScopeEnum SlideScope
		{
			get
			{
				SlideScopeEnum result = SlideScopeEnum.None;
				if(optScopeAll.Checked)
				{
					result = SlideScopeEnum.All;
				}
				else if(optScopeCurrent.Checked)
				{
					result = SlideScopeEnum.Current;
				}
				else if(optScopeIndex.Checked)
				{
					result = SlideScopeEnum.Custom;
				}
				return result;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SlideScopeText																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the custom text for the selected slide scope.
		/// </summary>
		public string SlideScopeText
		{
			get { return txtScopeIndex.Text; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*


}
