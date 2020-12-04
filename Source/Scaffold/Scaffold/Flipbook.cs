using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	FlipbookActionTypeEnum																									*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Enumeration of possible action types for a frame flipbook frame.
	/// </summary>
	public enum FlipbookActionTypeEnum
	{
		/// <summary>
		/// No action defined or unknown.
		/// </summary>
		None = 0,
		/// <summary>
		/// Keep all frames between this and the next frame definitions.
		/// </summary>
		Keep,
		/// <summary>
		/// Delete all frames between this and the next frame definitions.
		/// </summary>
		DeleteSpace
	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	FlipbookFrameCollection																									*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of FlipbookFrameItem Items.
	/// </summary>
	public class FlipbookFrameCollection : List<FlipbookFrameItem>
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
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the FlipbookFrameCollection Item.
		/// </summary>
		public FlipbookFrameCollection()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the FlipbookFrameCollection Item.
		/// </summary>
		/// <param name="parent">
		/// Reference to the parent file.
		/// </param>
		public FlipbookFrameCollection(FlipbookFile parent)
		{
			mParent = parent;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Add																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add an existing item to the list.
		/// </summary>
		/// <param name="item">
		/// Reference to the item to be added.
		/// </param>
		public new void Add(FlipbookFrameItem item)
		{
			if(item != null)
			{
				if(item.Parent == null)
				{
					item.Parent = this;
				}
				base.Add(item);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Parent																																*
		//*-----------------------------------------------------------------------*
		private FlipbookFile mParent = null;
		/// <summary>
		/// Get/Set a reference to the file of which this frame is a member.
		/// </summary>
		public FlipbookFile Parent
		{
			get { return mParent; }
			set { mParent = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SetParentOnChildren																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the parent property on all child items to this.
		/// </summary>
		public void SetParentOnChildren()
		{
			foreach(FlipbookFrameItem frameItem in this)
			{
				frameItem.Parent = this;
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	FlipbookFile																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// General information about a frame flipbook file.
	/// </summary>
	public class FlipbookFile
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
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the FlipbookFile Item.
		/// </summary>
		public FlipbookFile()
		{
			mFrames = new FlipbookFrameCollection(this);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DefaultTimer																													*
		//*-----------------------------------------------------------------------*
		private int mDefaultTimer = 0;
		/// <summary>
		/// Get/Set the default timer value to use if a keyframe timer is not set.
		/// </summary>
		public int DefaultTimer
		{
			get { return mDefaultTimer; }
			set { mDefaultTimer = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Filter																																*
		//*-----------------------------------------------------------------------*
		private string mFilter = "";
		/// <summary>
		/// Get/Set the filter used for loading the working files from the current
		/// folder.
		/// </summary>
		[JsonProperty(Order = 1)]
		public string Filter
		{
			get { return mFilter; }
			set { mFilter = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Folder																																*
		//*-----------------------------------------------------------------------*
		private string mFolder = "";
		/// <summary>
		/// Get/Set the name of the folder containing the source frame files.
		/// </summary>
		[JsonProperty(Order = 0)]
		public string Folder
		{
			get { return mFolder; }
			set { mFolder = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Frames																																*
		//*-----------------------------------------------------------------------*
		private FlipbookFrameCollection mFrames = null;
		/// <summary>
		/// Get a reference to the collection of frames in this file.
		/// </summary>
		[JsonProperty(Order = 2)]
		public FlipbookFrameCollection Frames
		{
			get { return mFrames; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	FlipbookFrameItem																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Individual frame definition in a frame flipbook.
	/// </summary>
	public class FlipbookFrameItem
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnSelectedChanged																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the SelectedChanged event when the value of the Selected
		/// property has changed.
		/// </summary>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		protected virtual void OnSelectedChanged(EventArgs e)
		{
			SelectedChanged?.Invoke(this, e);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	Action																																*
		//*-----------------------------------------------------------------------*
		private FlipbookActionTypeEnum mAction =
			FlipbookActionTypeEnum.None;
		/// <summary>
		/// Get/Set the action type for this frame.
		/// </summary>
		[JsonProperty(Order = 2)]
		public FlipbookActionTypeEnum Action
		{
			get { return mAction; }
			set { mAction = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	EditCommands																													*
		//*-----------------------------------------------------------------------*
		private List<string> mEditCommands = new List<string>();
		/// <summary>
		/// Get a reference to the list of commands to be used to edit the source
		/// images.
		/// </summary>
		public List<string> EditCommands
		{
			get { return mEditCommands; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Index																																	*
		//*-----------------------------------------------------------------------*
		private string mIndex = "";
		/// <summary>
		/// Get/Set the index of the file. In most cases, this is a numerical
		/// filename without an extension.
		/// </summary>
		[JsonProperty(Order = 0)]
		public string Index
		{
			get { return mIndex; }
			set { mIndex = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ItemClick																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the selection state of the item and any related items, according
		/// to the specified control and shift properties.
		/// </summary>
		/// <param name="frame">
		/// Reference to the frame being clicked.
		/// </param>
		/// <param name="ctrl">
		/// Virtual state of the control button.
		/// </param>
		/// <param name="shift">
		/// Virtual state of the shift button.
		/// </param>
		public static void ItemClick(FlipbookFrameItem frame,
			bool ctrl, bool shift)
		{
			int count = 0;
			int index = 0;
			int indexPrev = 0;
			int indexThis = 0;
			FlipbookFrameItem item = null;
			FlipbookFrameCollection parent = null;

			if(frame != null)
			{
				//	The frame is legitimate.
				parent = frame.mParent;
				if(!ctrl && !shift)
				{
					frame.Selected = true;
					if(parent != null)
					{
						//	This and no other items are selected.
						foreach(FlipbookFrameItem frameItem in parent)
						{
							if(frameItem != frame)
							{
								frameItem.Selected = false;
							}
						}
					}
				}
				else if(!ctrl && shift)
				{
					//	[Shift]
					frame.Selected = true;
					if(parent != null)
					{
						//	Selected from the previously selected item to this item.
						count = parent.Count;
						for(index = 0; index < count; index++)
						{
							item = parent[index];
							if(item == frame)
							{
								indexThis = index;
								break;
							}
							else if(item.mSelected)
							{
								indexPrev = index;
							}
						}
						if(indexThis > 0 && indexPrev >= 0 && indexPrev < indexThis)
						{
							for(index = indexPrev; index <= indexThis; index++)
							{
								parent[index].Selected = true;
							}
						}
					}
				}
				else if(ctrl && !shift)
				{
					//	[Ctrl].
					frame.Selected = !frame.Selected;
				}
				else
				{
					//	[Ctrl][Shift].
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Name																																	*
		//*-----------------------------------------------------------------------*
		private string mName = "";
		/// <summary>
		/// Get/Set the status name at this frame.
		/// </summary>
		[JsonProperty(Order = 1)]
		public string Name
		{
			get { return mName; }
			set { mName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Parent																																*
		//*-----------------------------------------------------------------------*
		private FlipbookFrameCollection mParent = null;
		/// <summary>
		/// Get/Set a reference to the collection to which this item belongs.
		/// </summary>
		public FlipbookFrameCollection Parent
		{
			get { return mParent; }
			set { mParent = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Selected																															*
		//*-----------------------------------------------------------------------*
		private bool mSelected = false;
		/// <summary>
		/// Get/Set a value indicating whether this item is selected.
		/// </summary>
		public bool Selected
		{
			get { return mSelected; }
			set
			{
				bool changed = (mSelected != value);

				mSelected = value;
				if(changed)
				{
					OnSelectedChanged(new EventArgs());
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SelectedChanged																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the value of the Selected property has changed.
		/// </summary>
		public event EventHandler SelectedChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Timer																																	*
		//*-----------------------------------------------------------------------*
		private int mTimer = 0;
		/// <summary>
		/// Get/Set time amount of time to rest on this frame before moving to the
		/// next.
		/// </summary>
		[JsonProperty(Order = 3)]
		public int Timer
		{
			get { return mTimer; }
			set { mTimer = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*


}
