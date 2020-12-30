//	frmExportDecisionTreeWizard.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static Scaffold.ScaffoldNodesUtil;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	frmExportDecisionTreeWizard																							*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Wizard form for exporting decision tree to PowerPoint.
	/// </summary>
	public partial class frmExportDecisionTreeWizard : Form
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* btnBack_Click																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Back button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnBack_Click(object sender, EventArgs e)
		{
			if(tctl.SelectedIndex > 0)
			{
				tctl.SelectedIndex--;
			}
			UpdateTabStatus();
		}
		//*-----------------------------------------------------------------------*

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
			this.DialogResult = DialogResult.Cancel;
			this.Hide();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnFilename_Click																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The filename helper button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnFilename_Click(object sender, EventArgs e)
		{
			bool bContinue = true;
			OpenFileDialog dialog = new OpenFileDialog();
			FileInfo file = null;

			dialog.Filter =
				"PowerPoint Presentations " +
				"(*.pptx;*.ppt;*.pptm)|*.pptx;*.ppt;*.pptm)|" +
				"All Files (*.*)|*.*";
			if(txtFilename.Text.Length > 0)
			{
				//	Existing filename.
				bContinue = false;
				file = new FileInfo(txtFilename.Text);
				if(file.Exists)
				{
					dialog.InitialDirectory = file.Directory.FullName;
					dialog.FileName = file.Name;
					bContinue = true;
				}
				else
				{
					MessageBox.Show($"The file {file.FullName} was not found.",
						"Specify Filename");
					dialog.FileName = file.Name;
					bContinue = true;
				}
			}
			else
			{
				//	Blank filename.
				bContinue = true;
			}
			if(bContinue)
			{
				dialog.Title = "PowerPoint Filename";
				dialog.AddExtension = true;
				dialog.DefaultExt = ".pptx";
				dialog.CheckFileExists = true;
				dialog.CheckPathExists = true;
				dialog.DereferenceLinks = true;
				dialog.FilterIndex = 0;
				dialog.Multiselect = false;
				if(dialog.ShowDialog() == DialogResult.OK)
				{
					txtFilename.Text = dialog.FileName;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnFinish_Click																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Finish button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnFinish_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Hide();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* btnNext_Click																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Next button has been clicked.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void btnNext_Click(object sender, EventArgs e)
		{
			bool bEmpty = false;
			int page;
			//string posX = "From left";
			//string posY = "From top";
			List<SocketItem> sockets = null;
			//float x = 0f;
			//float y = 0f;

			if(tctl.SelectedIndex < tctl.TabCount - 1)
			{
				tctl.SelectedIndex++;
				if(tctl.SelectedTab.Name == "tpgPageSettings")
				{
					//	Check to see if any nodes or sockets have empty page information.
					foreach(NodeItem node in mNodes)
					{
						page = ToInt(node["StoryPageNumber"].ToString());
						if(page == 0)
						{
							//	Empty page information found.
							bEmpty = true;
							break;
						}
						else
						{
							sockets = node.Sockets.FindAll(s =>
								s.SocketMode == SocketModeEnum.Output);
							foreach(SocketItem socket in sockets)
							{
								page = ToInt(socket["StoryPageNumber"].ToString());
								if(page == 0)
								{
									//	Empty page information found.
									bEmpty = true;
									break;
								}
							}
							if(bEmpty)
							{
								break;
							}
						}
					}
					if(!bEmpty)
					{
						//	No empty page information found. Skip page.
						tctl.SelectedIndex++;
					}
				}
			}
			UpdateTabStatus();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* chkPageSettingsAllow_CheckedChanged																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The state of the page settings adjustment allow checkbox has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void chkPageSettingsAllow_CheckedChanged(object sender,
			EventArgs e)
		{
			lblStartingPageNumber.Enabled =
				txtStartingPageNumber.Enabled =
				lblQuestionOffsetX.Enabled =
				txtQuestionOffsetX.Enabled =
				cmboQuestionOffsetX.Enabled =
				lblQuestionOffsetY.Enabled =
				txtQuestionOffsetY.Enabled =
				cmboQuestionOffsetY.Enabled =
				lblAnswerOffsetX.Enabled =
				txtAnswerOffsetX.Enabled =
				cmboAnswerOffsetX.Enabled =
				lblAnswerOffsetY.Enabled =
				txtAnswerOffsetY.Enabled =
				cmboAnswerOffsetY.Enabled =
				chkPageSettingsAllow.Checked;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* optFile_CheckedChanged																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The checked state has changed on a file option.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void optFile_CheckedChanged(object sender, EventArgs e)
		{
			lblFilename.Enabled =
				txtFilename.Enabled =
				btnFilename.Enabled = optFileOpenExisting.Checked;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UpdateTabStatus																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update form controls based upon current tab status.
		/// </summary>
		private void UpdateTabStatus()
		{
			btnCancel.Enabled = true;
			btnBack.Enabled = (tctl.SelectedIndex > 0);
			btnNext.Enabled = (tctl.SelectedIndex < tctl.TabCount - 1);
			btnFinish.Enabled = (tctl.SelectedIndex == tctl.TabCount - 1);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnBackColorChanged																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the BackColorChanged event when the background color of the form
		/// is changed.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			foreach(TabPage page in tctl.TabPages)
			{
				page.BackColor = this.BackColor;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnLoad																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the Load event when the form has loaded and is ready to display.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnLoad(EventArgs e)
		{
			//Size originalSize = tctl.Size;
			//tctl.Appearance = TabAppearance.FlatButtons;
			//tctl.ItemSize = new Size(0, 0);
			//tctl.SizeMode = TabSizeMode.Fixed;
			//tctl.Height += (tctl.Size.Height - 1);
			base.OnLoad(e);
			cmboQuestionOffsetX.SelectedItem = "From left";
			cmboQuestionOffsetY.SelectedItem = "From top";
			cmboAnswerOffsetX.SelectedItem = "From right";
			cmboAnswerOffsetY.SelectedItem = "From top";
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the frmExportDecisionTreeWizard Item.
		/// </summary>
		public frmExportDecisionTreeWizard()
		{
			InitializeComponent();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AutoAnswerOffsetX																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the automatic answer offset X coordinate.
		/// </summary>
		public float AutoAnswerOffsetX
		{
			get { return ToFloat(txtAnswerOffsetX.Text); }
			set { txtAnswerOffsetX.Text = value.ToString("0.000"); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AutoAnswerOffsetXFrom																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the automatic answer offset orientation for the X
		/// coordinate.
		/// </summary>
		public string AutoAnswerOffsetXFrom
		{
			get { return cmboAnswerOffsetX.SelectedItem.ToString(); }
			set { cmboAnswerOffsetX.SelectedItem = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AutoAnswerOffsetY																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the automatic answer offset Y coordinate.
		/// </summary>
		public float AutoAnswerOffsetY
		{
			get { return ToFloat(txtAnswerOffsetY.Text); }
			set { txtAnswerOffsetY.Text = value.ToString("0.000"); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AutoAnswerOffsetYFrom																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the automatic answer offset orientation for the Y
		/// coordinate.
		/// </summary>
		public string AutoAnswerOffsetYFrom
		{
			get { return cmboAnswerOffsetY.SelectedItem.ToString(); }
			set { cmboAnswerOffsetY.SelectedItem = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AutoPlacement																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set a value indicating whether automatic placement of unregistered
		/// storyboard items is allowed.
		/// </summary>
		public bool AutoPlacement
		{
			get { return chkPageSettingsAllow.Checked; }
			set { chkPageSettingsAllow.Checked = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AutoQuestionOffsetX																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the automatic question offset X coordinate.
		/// </summary>
		public float AutoQuestionOffsetX
		{
			get { return ToFloat(txtQuestionOffsetX.Text); }
			set { txtQuestionOffsetX.Text = value.ToString("0.000"); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AutoQuestionOffsetXFrom																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the automatic question offset orientation for the X
		/// coordinate.
		/// </summary>
		public string AutoQuestionOffsetXFrom
		{
			get { return cmboQuestionOffsetX.SelectedItem.ToString(); }
			set { cmboQuestionOffsetX.SelectedItem = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AutoQuestionOffsetY																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the automatic question offset Y coordinate.
		/// </summary>
		public float AutoQuestionOffsetY
		{
			get { return ToFloat(txtQuestionOffsetY.Text); }
			set { txtQuestionOffsetY.Text = value.ToString(); }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AutoQuestionOffsetYFrom																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the automatic question offset orientation for the Y
		/// coordinate.
		/// </summary>
		public string AutoQuestionOffsetYFrom
		{
			get { return cmboQuestionOffsetY.SelectedItem.ToString(); }
			set { cmboQuestionOffsetY.SelectedItem = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	CreateFile																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set a value indicating whether a new file will be created for this
		/// export.
		/// </summary>
		public bool CreateFile
		{
			get { return optFileCreateNew.Checked; }
			set
			{
				if(value)
				{
					optFileCreateNew.Checked = true;
				}
				else
				{
					optFileOpenExisting.Checked = true;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Filename																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get/Set the established filename for this session.
		/// </summary>
		public string Filename
		{
			get { return txtFilename.Text; }
			set { txtFilename.Text = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Nodes																																	*
		//*-----------------------------------------------------------------------*
		private NodeCollection mNodes = null;
		/// <summary>
		/// Get/Set a reference to the collection of nodes being exported.
		/// </summary>
		public NodeCollection Nodes
		{
			get { return mNodes; }
			set { mNodes = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
