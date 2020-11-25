#define DONTUSECONTROLLOG
//	PanelWindowControl.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	PanelWindowControl																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Panel window control with predefined collapsible / expandable panes.
	/// </summary>
	public partial class PanelWindowControl : UserControl
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		private PanelWindowContent mContentBottom = null;
		private PanelWindowContent mContentCenter = null;
		private PanelWindowContent mContentLeft = null;
		private PanelWindowContent mContentRight = null;
		private PanelWindowContent mContentTop = null;
		private bool mControlBottom = false;
		private bool mControlLeft = false;
		private bool mControlRight = false;
		private bool mControlTop = false;
		//private NamedStringCatalog mGenericLists = new NamedStringCatalog();
		private bool mMouseBottom = false;
		private bool mMouseLeft = false;
		private bool mMouseRight = false;
		private bool mMouseTop = false;
		//private const int WM_SETREDRAW = 0x000B;

		//*-----------------------------------------------------------------------*
		//* ControlLog																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Write debugging and tracing information to the local log for this
		/// control.
		/// </summary>
		/// <param name="value">
		/// Entry to write.
		/// </param>
		private void ControlLog(string value)
		{
#if USECONTROLLOG
			File.AppendAllText("C:/Temp/PanelWindowControl.txt",
				DateTime.Now.ToString("yyyyMMdd.HHmm") + " - " + value + "\r\n");
#endif
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlBottom_SizeChanged																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The bottom panel size has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlBottom_SizeChanged(object sender, EventArgs e)
		{
			if(mMouseBottom || mControlBottom)
			{
				//	Update the reference height.
				mPanelBottom = pnlBottom.Height;
				if(mMouseBottom)
				{
					mMouseBottom = false;
				}
			}
			else
			{
				//	Revert to known height.
				pnlBottom.Height = mPanelBottom;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlLeft_SizeChanged																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The left panel size has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlLeft_SizeChanged(object sender, EventArgs e)
		{
			if(mMouseLeft || mControlLeft)
			{
				//	Update the reference width.
				mPanelLeft = pnlLeft.Width;
				if(mMouseLeft)
				{
					mMouseLeft = false;
				}
			}
			else
			{
				//	Revert to known width.
				pnlLeft.Width = mPanelLeft;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlRight_SizeChanged																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The right panel size has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlRight_SizeChanged(object sender, EventArgs e)
		{
			if(mMouseRight || mControlRight)
			{
				//	Update the reference width.
				mPanelRight = pnlRight.Width;
				if(mMouseRight)
				{
					mMouseRight = false;
				}
			}
			else
			{
				//	Revert to known width.
				pnlRight.Width = mPanelRight;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* pnlTop_SizeChanged																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The top panel size has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void pnlTop_SizeChanged(object sender, EventArgs e)
		{
			if(mMouseTop || mControlTop)
			{
				//	Update the reference height.
				mPanelTop = pnlTop.Height;
				if(mMouseTop)
				{
					mMouseTop = false;
				}
			}
			else
			{
				//	Revert to known height.
				pnlTop.Height = mPanelTop;
			}
		}
		//*-----------------------------------------------------------------------*

		//public static void ResumeDraw(Control control)
		//{
		//	// Create a C "true" boolean as an IntPtr
		//	IntPtr wparam = new IntPtr(1);
		//	Message msgResumeUpdate =
		//		Message.Create(control.Handle, WM_SETREDRAW, wparam, IntPtr.Zero);

		//	NativeWindow window = NativeWindow.FromHandle(control.Handle);
		//	window.DefWndProc(ref msgResumeUpdate);

		//	control.Invalidate();
		//}

		//*-----------------------------------------------------------------------*
		//*	splitBottom_MouseMove																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has moved on the bottom splitter.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void splitBottom_MouseMove(object sender, MouseEventArgs e)
		{
			if((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				mMouseBottom = true;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* splitLeft_MouseMove																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has moved on the left splitter.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void splitLeft_MouseMove(object sender, MouseEventArgs e)
		{
			if((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				mMouseLeft = true;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* splitRight_MouseMove																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has moved on the right splitter.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void splitRight_MouseMove(object sender, MouseEventArgs e)
		{
			if((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				mMouseRight = true;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* splitTop_MouseMove																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The mouse has moved on the top splitter.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void splitTop_MouseMove(object sender, MouseEventArgs e)
		{
			if((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				mMouseTop = true;
			}
		}
		//*-----------------------------------------------------------------------*

		//public static void SuspendDraw(Control control)
		//{
		//	Message msgSuspendUpdate =
		//		Message.Create(control.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);

		//	NativeWindow window = NativeWindow.FromHandle(control.Handle);
		//	window.DefWndProc(ref msgSuspendUpdate);
		//}

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	OnBackColorChanged																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the BackColorChanged event when the control background color
		/// has been changed.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			pnlTop.BackColor =
				pnlBottom.BackColor =
				pnlLeft.BackColor =
				pnlRight.BackColor =
				pnlCenter.BackColor = this.BackColor;
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the PanelWindowControl Item.
		/// </summary>
		public PanelWindowControl()
		{
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			InitializeComponent();
			ControlLog("Initialized...");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ActivateAssociations																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Activate all of the associated controls on the form.
		/// </summary>
		public void ActivateAssociations()
		{
			List<PanelWindowContent> contents = new List<PanelWindowContent>();
			Control control = null;
			DockStyle dock = DockStyle.None;
			Form form = null;
			Control[] matches = null;
			Control parent = null;

			if(this.Parent != null && this.Parent.FindForm() != null)
			{
				ControlLog("Activating associations...");
				form = this.Parent.FindForm();
				if(mContentBottom != null)
				{
					contents.Add(mContentBottom);
				}
				if(mContentCenter != null)
				{
					contents.Add(mContentCenter);
				}
				if(mContentLeft != null)
				{
					contents.Add(mContentLeft);
				}
				if(mContentRight != null)
				{
					contents.Add(mContentRight);
				}
				if(mContentTop != null)
				{
					contents.Add(mContentTop);
				}
				foreach(PanelWindowContent content in contents)
				{
					//	Check for associations on each panel.
					ControlLog($" Checking for associations on {content.Name}");
					foreach(PanelWindowContentControl controldef in content.Controls)
					{
						control = null;
						matches = form.Controls.Find(controldef.Name, true);
						if(matches?.Length > 0)
						{
							control = matches[0];
						}
						if(control != null)
						{
							ControlLog(" Windows control found...");
							if(control.Dock != DockStyle.None)
							{
								dock = control.Dock;
								control.Dock = DockStyle.None;
							}
							parent = control.Parent;
							parent.Controls.Remove(control);
							switch(content.Name)
							{
								case "Bottom":
									ControlLog(" Adding to bottom...");
									pnlBottom.Controls.Add(control);
									control.Location =
										new Point(
											control.Left - this.Left,
											control.Top - pnlBottom.Top - this.Top);
									content.MaxExtent =
										Math.Max(control.Top + control.Height, content.MaxExtent);
									break;
								case "Center":
									ControlLog(" Adding to center...");
									pnlCenter.Controls.Add(control);
									control.Location =
										new Point(
											control.Left - pnlCenter.Left - this.Left,
											control.Top - pnlCenter.Top - this.Top);
									break;
								case "Left":
									ControlLog(" Adding to left...");
									pnlLeft.Controls.Add(control);
									control.Location =
										new Point(
											control.Left - this.Left,
											control.Top - pnlLeft.Top - this.Top);
									content.MaxExtent =
										Math.Max(control.Left + control.Width, content.MaxExtent);
									break;
								case "Right":
									ControlLog(" Adding to right...");
									pnlRight.Controls.Add(control);
									control.Location =
										new Point(
											control.Left - pnlRight.Left - this.Left,
											control.Top - pnlRight.Top - this.Top);
									content.MaxExtent =
										Math.Max(control.Left + control.Width, content.MaxExtent);
									break;
								case "Top":
									ControlLog(" Adding to top...");
									//SuspendDraw(pnlTop);
									pnlTop.Controls.Add(control);
									control.Location =
										new Point(control.Left, control.Top - this.Top);
									content.MaxExtent =
										Math.Max(control.Top + control.Height, content.MaxExtent);
									//ResumeDraw(pnlTop);
									break;
							}
							switch(controldef.Dock)
							{
								case "Bottom":
									control.Dock = DockStyle.Bottom;
									break;
								case "Fill":
									control.Dock = DockStyle.Fill;
									break;
								case "Left":
									control.Dock = DockStyle.Left;
									break;
								case "None":
									control.Dock = DockStyle.None;
									break;
								case "Right":
									control.Dock = DockStyle.Right;
									break;
								case "Top":
									control.Dock = DockStyle.Top;
									break;
							}
						}
					}
					if(content.AutoSize)
					{
						//	Set the panel size according to the max X+Width or Y+Height.
						switch(content.Name)
						{
							case "Bottom":
								PanelBottom = content.MaxExtent + content.Margin;
								break;
							case "Left":
								PanelLeft = content.MaxExtent + content.Margin;
								break;
							case "Right":
								PanelRight = content.MaxExtent + content.Margin;
								break;
							case "Top":
								PanelTop = content.MaxExtent + content.Margin;
								break;
						}
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	AddBottomAssociation																									*
		////*-----------------------------------------------------------------------*
		//private Control mAddBottomAssociation = null;
		///// <summary>
		///// Get/Set a reference to a control to associate with the bottom panel at
		///// design time.
		///// </summary>
		//[DesignOnly(true)]
		//[ParenthesizePropertyName(true)]
		//public Control AddBottomAssociation
		//{
		//	get { return mAddBottomAssociation; }
		//	set
		//	{
		//		List<string> list = null;

		//		mAddBottomAssociation = value;
		//		if(value != null)
		//		{
		//			list = mGenericLists.FirstOrDefault(x =>
		//				x.Exists(y => y == value.Name));
		//			if(list != null)
		//			{
		//				list.Remove(value.Name);
		//			}
		//			mGenericLists["Bottom"].Add(value.Name);
		//			mAssociationListBottom = mGenericLists["Bottom"].ToString();
		//		}
		//	}
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	AddLeftAssociation																										*
		////*-----------------------------------------------------------------------*
		//private Control mAddLeftAssociation = null;
		///// <summary>
		///// Get/Set a reference to a control to associate with the left panel at
		///// design time.
		///// </summary>
		//[DesignOnly(true)]
		//[ParenthesizePropertyName(true)]
		//public Control AddLeftAssociation
		//{
		//	get { return mAddLeftAssociation; }
		//	set
		//	{
		//		List<string> list = null;

		//		mAddLeftAssociation = value;
		//		if(value != null)
		//		{
		//			list = mGenericLists.FirstOrDefault(x =>
		//				x.Exists(y => y == value.Name));
		//			if(list != null)
		//			{
		//				list.Remove(value.Name);
		//			}
		//			mGenericLists["Left"].Add(value.Name);
		//			mAssociationListLeft = mGenericLists["Left"].ToString();
		//		}
		//	}
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	AddRightAssociation																										*
		////*-----------------------------------------------------------------------*
		//private Control mAddRightAssociation = null;
		///// <summary>
		///// Get/Set a reference to a control to associate with the right panel at
		///// design time.
		///// </summary>
		//[DesignOnly(true)]
		//[ParenthesizePropertyName(true)]
		//public Control AddRightAssociation
		//{
		//	get { return mAddRightAssociation; }
		//	set
		//	{
		//		List<string> list = null;

		//		mAddRightAssociation = value;
		//		if(value != null)
		//		{
		//			list = mGenericLists.FirstOrDefault(x =>
		//				x.Exists(y => y == value.Name));
		//			if(list != null)
		//			{
		//				list.Remove(value.Name);
		//			}
		//			mGenericLists["Right"].Add(value.Name);
		//			mAssociationListRight = mGenericLists["Right"].ToString();
		//		}
		//	}
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	AddTopAssociation																											*
		////*-----------------------------------------------------------------------*
		//private Control mAddTopAssociation = null;
		///// <summary>
		///// Get/Set a reference to a control to associate with the top panel at
		///// design time.
		///// </summary>
		//[DesignOnly(true)]
		//[ParenthesizePropertyName(true)]
		//public Control AddTopAssociation
		//{
		//	get { return mAddTopAssociation; }
		//	set
		//	{
		//		List<string> list = null;

		//		mAddTopAssociation = value;
		//		if(value != null)
		//		{
		//			list = mGenericLists.FirstOrDefault(x =>
		//				x.Exists(y => y == value.Name));
		//			if(list != null)
		//			{
		//				list.Remove(value.Name);
		//			}
		//			mGenericLists["Top"].Add(value.Name);
		//			mAssociationListTop = mGenericLists["Top"].ToString();
		//		}
		//	}
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AssociationListBottom																									*
		//*-----------------------------------------------------------------------*
		private string mAssociationListBottom = "";
		/// <summary>
		/// Get/Set the current list of control names associated with the
		/// bottom panel.
		/// </summary>
		[Editor(
			typeof(MultilineStringEditor),
			typeof(System.Drawing.Design.UITypeEditor))]
		public string AssociationListBottom
		{
			get { return mAssociationListBottom; }
			set
			{
				//int count = 0;
				//int index = 0;
				//NamedStringCollection list = null;
				//List<string> names = null;

				ControlLog($"AssociationListBottom Set: {value}...");
				//	In the string property, maintain name lists only.
				//	This strategy supports the use of the AddTopAssociation property.
				if(value?.Length > 0)
				{
					mContentBottom = PanelWindowContent.Deserialize(value);
					//names = value.Split(new char[] { ';', ',' }).
					//	Select(x => x.Trim()).ToList();
					//list = mGenericLists["Bottom"];
					//if(list != null)
					//{
					//	ControlLog(" Bottom list found...");
					//}
					//count = list.Count;
					////	Remove deleted items.
					//for(index = 0; index < count; index++)
					//{
					//	if(!names.Exists(x => x == list[index]))
					//	{
					//		list.RemoveAt(index);
					//		index--;
					//		count--;
					//	}
					//}
					////	Add new items.
					//foreach(string name in names)
					//{
					//	if(!mGenericLists.Exists(x => x.Exists(y => y == name)))
					//	{
					//		ControlLog($" Add {name} to control list...");
					//		mGenericLists["Bottom"].Add(name);
					//	}
					//}
				}
				else
				{
					mContentBottom = null;
				}
				mAssociationListBottom = value;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AssociationListCenter																									*
		//*-----------------------------------------------------------------------*
		private string mAssociationListCenter = "";
		/// <summary>
		/// Get/Set the current list of control names associated with the
		/// center panel.
		/// </summary>
		[Editor(
			typeof(MultilineStringEditor),
			typeof(System.Drawing.Design.UITypeEditor))]
		public string AssociationListCenter
		{
			get { return mAssociationListCenter; }
			set
			{
				//int count = 0;
				//int index = 0;
				//NamedStringCollection list = null;
				//List<string> names = null;

				ControlLog($"AssociationListCenter Set: {value}...");
				//	In the string property, maintain name lists only.
				//	This strategy supports the use of the AddTopAssociation property.
				if(value?.Length > 0)
				{
					mContentCenter = PanelWindowContent.Deserialize(value);
					//names = value.Split(new char[] { ';', ',' }).
					//	Select(x => x.Trim()).ToList();
					//list = mGenericLists["Center"];
					//if(list != null)
					//{
					//	ControlLog(" Center list found...");
					//}
					//count = list.Count;
					////	Remove deleted items.
					//for(index = 0; index < count; index++)
					//{
					//	if(!names.Exists(x => x == list[index]))
					//	{
					//		list.RemoveAt(index);
					//		index--;
					//		count--;
					//	}
					//}
					////	Add new items.
					//foreach(string name in names)
					//{
					//	if(!mGenericLists.Exists(x => x.Exists(y => y == name)))
					//	{
					//		ControlLog($" Add {name} to control list...");
					//		mGenericLists["Center"].Add(name);
					//	}
					//}
				}
				else
				{
					mContentCenter = null;
				}
				mAssociationListCenter = value;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AssociationListLeft																										*
		//*-----------------------------------------------------------------------*
		private string mAssociationListLeft = "";
		/// <summary>
		/// Get/Set the current list of control names associated with the
		/// left panel.
		/// </summary>
		[Editor(
			typeof(MultilineStringEditor),
			typeof(System.Drawing.Design.UITypeEditor))]
		public string AssociationListLeft
		{
			get { return mAssociationListLeft; }
			set
			{
				//int count = 0;
				//int index = 0;
				//NamedStringCollection list = null;
				//List<string> names = null;

				ControlLog($"AssociationListLeft Set: {value}...");
				//	In the string property, maintain name lists only.
				//	This strategy supports the use of the AddLeftAssociation property.
				if(value?.Length > 0)
				{
					mContentLeft = PanelWindowContent.Deserialize(value);
					//names = value.Split(new char[] { ';', ',' }).
					//	Select(x => x.Trim()).ToList();
					//list = mGenericLists["Left"];
					//if(list != null)
					//{
					//	ControlLog(" Left list found...");
					//}
					//count = list.Count;
					////	Remove deleted items.
					//for(index = 0; index < count; index++)
					//{
					//	if(!names.Exists(x => x == list[index]))
					//	{
					//		list.RemoveAt(index);
					//		index--;
					//		count--;
					//	}
					//}
					////	Add new items.
					//foreach(string name in names)
					//{
					//	if(!mGenericLists.Exists(x => x.Exists(y => y == name)))
					//	{
					//		ControlLog($" Add {name} to control list...");
					//		mGenericLists["Left"].Add(name);
					//	}
					//}
				}
				else
				{
					mContentLeft = null;
				}
				mAssociationListLeft = value;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AssociationListRight																									*
		//*-----------------------------------------------------------------------*
		private string mAssociationListRight = "";
		/// <summary>
		/// Get/Set the current list of control names associated with the
		/// right panel.
		/// </summary>
		[Editor(
			typeof(MultilineStringEditor),
			typeof(System.Drawing.Design.UITypeEditor))]
		public string AssociationListRight
		{
			get { return mAssociationListRight; }
			set
			{
				//int count = 0;
				//int index = 0;
				//NamedStringCollection list = null;
				//List<string> names = null;

				ControlLog($"AssociationListRight Set: {value}...");
				//	In the string property, maintain name lists only.
				//	This strategy supports the use of the AddLeftAssociation property.
				if(value?.Length > 0)
				{
					mContentRight = PanelWindowContent.Deserialize(value);
					//names = value.Split(new char[] { ';', ',' }).
					//	Select(x => x.Trim()).ToList();
					//list = mGenericLists["Right"];
					//if(list != null)
					//{
					//	ControlLog(" Right list found...");
					//}
					//count = list.Count;
					////	Remove deleted items.
					//for(index = 0; index < count; index++)
					//{
					//	if(!names.Exists(x => x == list[index]))
					//	{
					//		list.RemoveAt(index);
					//		index--;
					//		count--;
					//	}
					//}
					////	Add new items.
					//foreach(string name in names)
					//{
					//	if(!mGenericLists.Exists(x => x.Exists(y => y == name)))
					//	{
					//		ControlLog($" Add {name} to control list...");
					//		mGenericLists["Right"].Add(name);
					//	}
					//}
				}
				else
				{
					mContentRight = null;
				}
				mAssociationListRight = value;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AssociationListTop																										*
		//*-----------------------------------------------------------------------*
		private string mAssociationListTop = "";
		/// <summary>
		/// Get/Set the current list of control names associated with the
		/// top panel.
		/// </summary>
		[Editor(
			typeof(MultilineStringEditor),
			typeof(System.Drawing.Design.UITypeEditor))]
		public string AssociationListTop
		{
			get { return mAssociationListTop; }
			set
			{
				//int count = 0;
				//int index = 0;
				//NamedStringCollection list = null;
				//List<string> names = null;

				ControlLog($"AssociationListTop Set: {value}...");
				//	In the string property, maintain name lists only.
				//	This strategy supports the use of the AddTopAssociation property.
				if(value?.Length > 0)
				{
					mContentTop = PanelWindowContent.Deserialize(value);
					//names = value.Split(new char[] { ';', ',' }).
					//	Select(x => x.Trim()).ToList();
					//list = mGenericLists["Top"];
					//if(list != null)
					//{
					//	ControlLog(" Top list found...");
					//}
					//count = list.Count;
					////	Remove deleted items.
					//for(index = 0; index < count; index++)
					//{
					//	if(!names.Exists(x => x == list[index]))
					//	{
					//		list.RemoveAt(index);
					//		index--;
					//		count--;
					//	}
					//}
					////	Add new items.
					//foreach(string name in names)
					//{
					//	if(!mGenericLists.Exists(x => x.Exists(y => y == name)))
					//	{
					//		ControlLog($" Add {name} to control list...");
					//		mGenericLists["Top"].Add(name);
					//	}
					//}
				}
				else
				{
					mContentTop = null;
				}
				mAssociationListTop = value;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PanelBottom																														*
		//*-----------------------------------------------------------------------*
		private int mPanelBottom = 10;
		/// <summary>
		/// Get/Set the opened height of the bottom panel.
		/// </summary>
		public int PanelBottom
		{
			get { return mPanelBottom; }
			set
			{
				mControlBottom = true;
				pnlBottom.Height = value;
				mControlBottom = false;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PanelLeft																															*
		//*-----------------------------------------------------------------------*
		private int mPanelLeft = 100;
		/// <summary>
		/// Get/Set the open width of the left panel.
		/// </summary>
		public int PanelLeft
		{
			get { return mPanelLeft; }
			set
			{
				mControlLeft = true;
				pnlLeft.Width = value;
				mControlLeft = false;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PanelRight																														*
		//*-----------------------------------------------------------------------*
		private int mPanelRight = 100;
		/// <summary>
		/// Get/Set the open width of the right panel.
		/// </summary>
		public int PanelRight
		{
			get { return mPanelRight; }
			set
			{
				mControlRight = true;
				pnlRight.Width = value;
				mControlRight = false;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PanelTop																															*
		//*-----------------------------------------------------------------------*
		private int mPanelTop = 32;
		/// <summary>
		/// Get/Set the opened height of the top panel.
		/// </summary>
		public int PanelTop
		{
			get { return mPanelTop; }
			set
			{
				mControlTop = true;
				pnlTop.Height = value;
				mControlTop = false;
			}
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	RemoveBottomAssociation																								*
		////*-----------------------------------------------------------------------*
		//private Control mRemoveBottomAssociation = null;
		///// <summary>
		///// Get/Set a reference to a control to de-associate with the bottom panel
		///// at design time.
		///// </summary>
		//[DesignOnly(true)]
		//[ParenthesizePropertyName(true)]
		//public Control RemoveBottomAssociation
		//{
		//	get { return mRemoveBottomAssociation; }
		//	set
		//	{
		//		mRemoveBottomAssociation = value;
		//		if(value != null)
		//		{
		//			if(mGenericLists["Bottom"].Exists(x => x == value.Name))
		//			{
		//				mGenericLists["Bottom"].Remove(value.Name);
		//				mAssociationListBottom = mGenericLists["Bottom"].ToString();
		//			}
		//		}
		//	}
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	RemoveLeftAssociation																									*
		////*-----------------------------------------------------------------------*
		//private Control mRemoveLeftAssociation = null;
		///// <summary>
		///// Get/Set a reference to a control to de-associate with the left panel
		///// at design time.
		///// </summary>
		//[DesignOnly(true)]
		//[ParenthesizePropertyName(true)]
		//public Control RemoveLeftAssociation
		//{
		//	get { return mRemoveLeftAssociation; }
		//	set
		//	{
		//		mRemoveLeftAssociation = value;
		//		if(value != null)
		//		{
		//			if(mGenericLists["Left"].Exists(x => x == value.Name))
		//			{
		//				mGenericLists["Left"].Remove(value.Name);
		//				mAssociationListLeft = mGenericLists["Left"].ToString();
		//			}
		//		}
		//	}
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	RemoveRightAssociation																								*
		////*-----------------------------------------------------------------------*
		//private Control mRemoveRightAssociation = null;
		///// <summary>
		///// Get/Set a reference to a control to de-associate with the right panel
		///// at design time.
		///// </summary>
		//[DesignOnly(true)]
		//[ParenthesizePropertyName(true)]
		//public Control RemoveRightAssociation
		//{
		//	get { return mRemoveRightAssociation; }
		//	set
		//	{
		//		mRemoveRightAssociation = value;
		//		if(value != null)
		//		{
		//			if(mGenericLists["Right"].Exists(x => x == value.Name))
		//			{
		//				mGenericLists["Right"].Remove(value.Name);
		//				mAssociationListRight = mGenericLists["Right"].ToString();
		//			}
		//		}
		//	}
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	RemoveTopAssociation																									*
		////*-----------------------------------------------------------------------*
		//private Control mRemoveTopAssociation = null;
		///// <summary>
		///// Get/Set a reference to a control to de-associate with the top panel at
		///// design time.
		///// </summary>
		//[DesignOnly(true)]
		//[ParenthesizePropertyName(true)]
		//public Control RemoveTopAssociation
		//{
		//	get { return mRemoveTopAssociation; }
		//	set
		//	{
		//		mRemoveTopAssociation = value;
		//		if(value != null)
		//		{
		//			if(mGenericLists["Top"].Exists(x => x == value.Name))
		//			{
		//				mGenericLists["Top"].Remove(value.Name);
		//				mAssociationListTop = mGenericLists["Top"].ToString();
		//			}
		//		}
		//	}
		//}
		////*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*


}
