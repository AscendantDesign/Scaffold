//	SlackBlock.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace ScaffoldSlackPack.Models
{
	//*-------------------------------------------------------------------------*
	//*	SlackBlockCollection																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of SlackBlockBase Items.
	/// </summary>
	public class SlackBlockCollection : List<SlackBlockItemBase>
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


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SlackBlockContainer																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Container of Slack blocks.
	/// </summary>
	public class SlackBlockContainer
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
		//*	Blocks																																*
		//*-----------------------------------------------------------------------*
		private SlackBlockCollection mBlocks = new SlackBlockCollection();
		/// <summary>
		/// Get a reference to the collection of blocks on this container.
		/// </summary>
		[JsonProperty(PropertyName = "blocks", Order = 2)]
		public SlackBlockCollection Blocks
		{
			get { return mBlocks; }
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* ShouldSerializeSlackChannelID																					*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return a value indicating whether the slack channel property should be
		///// serialized.
		///// </summary>
		///// <returns>
		///// True if the channel property will be added. Otherwise, false.
		///// </returns>
		//public bool ShouldSerializeSlackChannelID()
		//{
		//	return mSlackChannelID?.Length > 0;
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ShouldSerializeSlackUserID																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the slack user property should be
		/// serialized.
		/// </summary>
		/// <returns>
		/// True if the user property will be added. Otherwise, false.
		/// </returns>
		public bool ShouldSerializeSlackUserID()
		{
			return mSlackUserID?.Length > 0;
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	SlackChannelID																												*
		////*-----------------------------------------------------------------------*
		//private string mSlackChannelID = "";
		///// <summary>
		///// Get/Set the Slack channel ID.
		///// </summary>
		//[JsonProperty(PropertyName = "channel", Order = 1)]
		//public string SlackChannelID
		//{
		//	get { return mSlackChannelID; }
		//	set { mSlackChannelID = value; }
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SlackUserID																														*
		//*-----------------------------------------------------------------------*
		private string mSlackUserID = "";
		/// <summary>
		/// Get/Set the Slack user ID.
		/// </summary>
		/// <remarks>
		/// In this version (chat.postMessage send), the channel property is used
		/// as the destination user.
		/// </remarks>
		//[JsonProperty(PropertyName = "user", Order = 0)]
		[JsonProperty(PropertyName = "channel", Order = 0)]
		public string SlackUserID
		{
			get { return mSlackUserID; }
			set { mSlackUserID = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SlackBlockItemActions																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Defines a block actions section.
	/// </summary>
	public class SlackBlockItemActions : SlackBlockItemBase
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
		/// Create a new instance of the SlackBlockItemActions Item.
		/// </summary>
		public SlackBlockItemActions()
		{
			mBlockType = "actions";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Elements																															*
		//*-----------------------------------------------------------------------*
		private SlackBlockCollection mElements = new SlackBlockCollection();
		/// <summary>
		/// Get a reference to the collection of action blocks in this section.
		/// </summary>
		[JsonProperty(PropertyName = "elements", Order = 2)]
		public SlackBlockCollection Elements
		{
			get { return mElements; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SlackBlockItemBase																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Individual Slack Block Kit item base.
	/// </summary>
	public abstract class SlackBlockItemBase
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
		////*-----------------------------------------------------------------------*
		////*	BlockID																																*
		////*-----------------------------------------------------------------------*
		//private string mBlockID = Guid.NewGuid().ToString("D");
		///// <summary>
		///// Get/Set the unique identification of this block.
		///// </summary>
		//[JsonProperty(PropertyName = "block_id", Order = 1)]
		//public string BlockID
		//{
		//	get { return mBlockID; }
		//	set { mBlockID = value; }
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	BlockType																															*
		//*-----------------------------------------------------------------------*
		protected string mBlockType = "";
		/// <summary>
		/// Get/Set the type of block represented by this item.
		/// </summary>
		[JsonProperty(PropertyName = "type", Order = 0)]
		public string BlockType
		{
			get { return mBlockType; }
			set { mBlockType = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SlackBlockItemButton																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Button block element.
	/// </summary>
	public class SlackBlockItemButton : SlackBlockItemBase
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
		/// Create a new instance of the SlackBlockItemButton Item.
		/// </summary>
		public SlackBlockItemButton()
		{
			mBlockType = "button";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ActionID																															*
		//*-----------------------------------------------------------------------*
		private string mActionID = Guid.NewGuid().ToString("D");
		/// <summary>
		/// Get/Set the unique action ID for this item.
		/// </summary>
		[JsonProperty(PropertyName = "action_id", Order = 2)]
		public string ActionID
		{
			get { return mActionID; }
			set { mActionID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Text																																	*
		//*-----------------------------------------------------------------------*
		private SlackBlockTextObject mText = new SlackBlockTextObject();
		/// <summary>
		/// Get a reference to the text object for this item.
		/// </summary>
		[JsonProperty(PropertyName = "text", Order = 3)]
		public SlackBlockTextObject Text
		{
			get { return mText; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SlackBlockItemCheckboxGroup																							*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Group of checkboxes.
	/// </summary>
	public class SlackBlockItemCheckboxGroup : SlackBlockItemBase
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
		/// Create a new instance of the SlackBlockItemCheckboxGroup Item.
		/// </summary>
		public SlackBlockItemCheckboxGroup()
		{
			mBlockType = "checkboxes";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ActionID																															*
		//*-----------------------------------------------------------------------*
		private string mActionID = Guid.NewGuid().ToString("D");
		/// <summary>
		/// Get/Set the unique action ID for this item.
		/// </summary>
		[JsonProperty(PropertyName = "action_id", Order = 2)]
		public string ActionID
		{
			get { return mActionID; }
			set { mActionID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Options																																*
		//*-----------------------------------------------------------------------*
		private SlackBlockOptionCollection mOptions =
			new SlackBlockOptionCollection();
		/// <summary>
		/// Get a reference to the collection of options on this checkbox group.
		/// </summary>
		[JsonProperty(PropertyName = "options", Order = 3)]
		public SlackBlockOptionCollection Options
		{
			get { return mOptions; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SlackBlockItemConversationList																					*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Slack Block Kit conversation list element.
	/// </summary>
	public class SlackBlockItemConversationList : SlackBlockItemBase
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
		/// Create a new instance of the SlackBlockItemConversationList Item.
		/// </summary>
		public SlackBlockItemConversationList()
		{
			mBlockType = "multi_conversations_select";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ActionID																															*
		//*-----------------------------------------------------------------------*
		private string mActionID = Guid.NewGuid().ToString("D");
		/// <summary>
		/// Get/Set the unique action ID for this item.
		/// </summary>
		[JsonProperty(PropertyName = "action_id", Order = 2)]
		public string ActionID
		{
			get { return mActionID; }
			set { mActionID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Placeholder																														*
		//*-----------------------------------------------------------------------*
		private SlackBlockTextObject mPlaceholder = new SlackBlockTextObject
		{
			TextType = "plain_text"
		};
		/// <summary>
		/// Get/Set a reference to the placeholder text object. This value is only
		/// allowed to have a plain_text type.
		/// </summary>
		[JsonProperty(PropertyName = "placeholder", Order = 3)]
		public SlackBlockTextObject Placeholder
		{
			get { return mPlaceholder; }
			set { mPlaceholder = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SlackBlockItemDatePicker																								*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Slack Block Kit date picker element.
	/// </summary>
	public class SlackBlockItemDatePicker : SlackBlockItemBase
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
		/// Create a new instance of the SlackBlockItemDatePicker Item.
		/// </summary>
		public SlackBlockItemDatePicker()
		{
			mBlockType = "datepicker";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ActionID																															*
		//*-----------------------------------------------------------------------*
		private string mActionID = Guid.NewGuid().ToString("D");
		/// <summary>
		/// Get/Set the unique action ID for this item.
		/// </summary>
		[JsonProperty(PropertyName = "action_id", Order = 2)]
		public string ActionID
		{
			get { return mActionID; }
			set { mActionID = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SlackBlockItemDivider																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Slack Block Kit divider element.
	/// </summary>
	public class SlackBlockItemDivider : SlackBlockItemBase
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
		/// Create a new instance of the SlackBlockItemDivider Item.
		/// </summary>
		public SlackBlockItemDivider()
		{
			mBlockType = "divider";
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SlackBlockItemImage																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Slack Block Kit image element.
	/// </summary>
	public class SlackBlockItemImage : SlackBlockItemBase
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
		/// Create a new instance of the SlackBlockItemImage Item.
		/// </summary>
		public SlackBlockItemImage()
		{
			mBlockType = "image";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AltText																																*
		//*-----------------------------------------------------------------------*
		private string mAltText = "";
		/// <summary>
		/// Get/Set the alternate text for the image.
		/// </summary>
		[JsonProperty(PropertyName = "alt_text", Order = 3)]
		public string AltText
		{
			get { return mAltText; }
			set { mAltText = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ImageURL																															*
		//*-----------------------------------------------------------------------*
		private string mImageURL = "";
		/// <summary>
		/// Get/Set the URL to the image.
		/// </summary>
		[JsonProperty(PropertyName = "image_url", Order = 2)]
		public string ImageURL
		{
			get { return mImageURL; }
			set { mImageURL = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SlackBlockItemMultipleSelect																						*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Slack Block Kit multiple select menu element.
	/// </summary>
	public class SlackBlockItemMultipleSelect : SlackBlockItemBase
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
		/// Create a new instance of the SlackBlockItemMultipleSelect Item.
		/// </summary>
		public SlackBlockItemMultipleSelect()
		{
			mBlockType = "multi_static_sleect";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ActionID																															*
		//*-----------------------------------------------------------------------*
		private string mActionID = Guid.NewGuid().ToString("D");
		/// <summary>
		/// Get/Set the unique action ID for this item.
		/// </summary>
		[JsonProperty(PropertyName = "action_id", Order = 2)]
		public string ActionID
		{
			get { return mActionID; }
			set { mActionID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Options																																*
		//*-----------------------------------------------------------------------*
		private SlackBlockOptionCollection mOptions =
			new SlackBlockOptionCollection();
		/// <summary>
		/// Get a reference to the collection of options on this checkbox group.
		/// </summary>
		[JsonProperty(PropertyName = "options", Order = 4)]
		public SlackBlockOptionCollection Options
		{
			get { return mOptions; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Placeholder																														*
		//*-----------------------------------------------------------------------*
		private SlackBlockTextObject mPlaceholder = new SlackBlockTextObject
		{
			TextType = "plain_text"
		};
		/// <summary>
		/// Get/Set a reference to the placeholder text object. This value is only
		/// allowed to have a plain_text type.
		/// </summary>
		[JsonProperty(PropertyName = "placeholder", Order = 3)]
		public SlackBlockTextObject Placeholder
		{
			get { return mPlaceholder; }
			set { mPlaceholder = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SlackBlockItemOverflowMenu																							*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Slack Block Kit overflow menu element.
	/// </summary>
	public class SlackBlockItemOverflowMenu : SlackBlockItemBase
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
		/// Create a new instance of the SlackBlockItemOverflowMenu Item.
		/// </summary>
		public SlackBlockItemOverflowMenu()
		{
			mBlockType = "overflow";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ActionID																															*
		//*-----------------------------------------------------------------------*
		private string mActionID = Guid.NewGuid().ToString("D");
		/// <summary>
		/// Get/Set the unique action ID for this item.
		/// </summary>
		[JsonProperty(PropertyName = "action_id", Order = 2)]
		public string ActionID
		{
			get { return mActionID; }
			set { mActionID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Options																																*
		//*-----------------------------------------------------------------------*
		private SlackBlockOptionCollection mOptions =
			new SlackBlockOptionCollection();
		/// <summary>
		/// Get a reference to the collection of options on this checkbox group.
		/// </summary>
		[JsonProperty(PropertyName = "options", Order = 3)]
		public SlackBlockOptionCollection Options
		{
			get { return mOptions; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SlackBlockItemPlainTextInput																						*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Slack Block Kit plain text input element.
	/// </summary>
	public class SlackBlockItemPlainTextInput : SlackBlockItemBase
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
		/// Create a new instance of the SlackBlockItemPlainTextInput Item.
		/// </summary>
		public SlackBlockItemPlainTextInput()
		{
			mBlockType = "plain_text_input";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ActionID																															*
		//*-----------------------------------------------------------------------*
		private string mActionID = Guid.NewGuid().ToString("D");
		/// <summary>
		/// Get/Set the unique action ID for this item.
		/// </summary>
		[JsonProperty(PropertyName = "action_id", Order = 2)]
		public string ActionID
		{
			get { return mActionID; }
			set { mActionID = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SlackBlockItemPublicChannelList																					*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Slack Block Kit public channel list element.
	/// </summary>
	public class SlackBlockItemPublicChannelList : SlackBlockItemBase
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
		/// Create a new instance of the SlackBlockItemConversationList Item.
		/// </summary>
		public SlackBlockItemPublicChannelList()
		{
			mBlockType = "multi_channels_select";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ActionID																															*
		//*-----------------------------------------------------------------------*
		private string mActionID = Guid.NewGuid().ToString("D");
		/// <summary>
		/// Get/Set the unique action ID for this item.
		/// </summary>
		[JsonProperty(PropertyName = "action_id", Order = 2)]
		public string ActionID
		{
			get { return mActionID; }
			set { mActionID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Placeholder																														*
		//*-----------------------------------------------------------------------*
		private SlackBlockTextObject mPlaceholder = new SlackBlockTextObject
		{
			TextType = "plain_text"
		};
		/// <summary>
		/// Get/Set a reference to the placeholder text object. This value is only
		/// allowed to have a plain_text type.
		/// </summary>
		[JsonProperty(PropertyName = "placeholder", Order = 3)]
		public SlackBlockTextObject Placeholder
		{
			get { return mPlaceholder; }
			set { mPlaceholder = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SlackBlockItemRadioButtonGroup																					*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Slack Block Kit radio button group element.
	/// </summary>
	public class SlackBlockItemRadioButtonGroup : SlackBlockItemBase
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
		/// Create a new instance of the SlackBlockItemRadioButtonGroup Item.
		/// </summary>
		public SlackBlockItemRadioButtonGroup()
		{
			mBlockType = "radio_buttons";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ActionID																															*
		//*-----------------------------------------------------------------------*
		private string mActionID = Guid.NewGuid().ToString("D");
		/// <summary>
		/// Get/Set the unique action ID for this item.
		/// </summary>
		[JsonProperty(PropertyName = "action_id", Order = 2)]
		public string ActionID
		{
			get { return mActionID; }
			set { mActionID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Options																																*
		//*-----------------------------------------------------------------------*
		private SlackBlockOptionCollection mOptions =
			new SlackBlockOptionCollection();
		/// <summary>
		/// Get a reference to the collection of options on this checkbox group.
		/// </summary>
		[JsonProperty(PropertyName = "options", Order = 3)]
		public SlackBlockOptionCollection Options
		{
			get { return mOptions; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SlackBlockItemSection																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Defines a block section.
	/// </summary>
	public class SlackBlockItemSection : SlackBlockItemBase
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
		/// Create a new instance of the SlackBlockItemSection Item.
		/// </summary>
		public SlackBlockItemSection()
		{
			mBlockType = "section";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Accessory																															*
		//*-----------------------------------------------------------------------*
		private SlackBlockItemBase mAccessory = null;
		/// <summary>
		/// Get/Set a reference to an allowable element to use as an accessory on
		/// this section.
		/// </summary>
		[JsonProperty(PropertyName = "accessory", Order = 3)]
		public SlackBlockItemBase Accessory
		{
			get { return mAccessory; }
			set { mAccessory = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Text																																	*
		//*-----------------------------------------------------------------------*
		private SlackBlockTextObject mText = new SlackBlockTextObject();
		/// <summary>
		/// Get/Set a reference to the text block for this section.
		/// </summary>
		[JsonProperty(PropertyName = "text", Order = 2)]
		public SlackBlockTextObject Text
		{
			get { return mText; }
			set { mText = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ShouldSerializeAccessory																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the accessory property should be
		/// serialized.
		/// </summary>
		/// <returns>
		/// True if the accessory property has been set. Otherwise, false.
		/// </returns>
		public bool ShouldSerializeAccessory()
		{
			return (mAccessory != null);
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SlackBlockItemSelectMenu																								*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Slack Block Kit select menu element.
	/// </summary>
	public class SlackBlockItemSelectMenu : SlackBlockItemBase
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
		/// Create a new instance of the SlackBlockItemSelectMenu Item.
		/// </summary>
		public SlackBlockItemSelectMenu()
		{
			mBlockType = "static_select";
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SlackBlockItemTimePicker																								*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Slack Block Kit time picker element.
	/// </summary>
	public class SlackBlockItemTimePicker : SlackBlockItemBase
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
		/// Create a new instance of the SlackBlockItemTimePicker Item.
		/// </summary>
		public SlackBlockItemTimePicker()
		{
			mBlockType = "timepicker";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ActionID																															*
		//*-----------------------------------------------------------------------*
		private string mActionID = Guid.NewGuid().ToString("D");
		/// <summary>
		/// Get/Set the unique action ID for this item.
		/// </summary>
		[JsonProperty(PropertyName = "action_id", Order = 2)]
		public string ActionID
		{
			get { return mActionID; }
			set { mActionID = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SlackBlockItemUserlist																									*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Slack Block Kit userlist element.
	/// </summary>
	public class SlackBlockItemUserlist : SlackBlockItemBase
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
		/// Create a new instance of the SlackBlockItemUserlist Item.
		/// </summary>
		public SlackBlockItemUserlist()
		{
			mBlockType = "multi_users_select";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ActionID																															*
		//*-----------------------------------------------------------------------*
		private string mActionID = Guid.NewGuid().ToString("D");
		/// <summary>
		/// Get/Set the unique action ID for this item.
		/// </summary>
		[JsonProperty(PropertyName = "action_id", Order = 2)]
		public string ActionID
		{
			get { return mActionID; }
			set { mActionID = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Placeholder																														*
		//*-----------------------------------------------------------------------*
		private SlackBlockTextObject mPlaceholder = new SlackBlockTextObject
		{
			TextType = "plain_text"
		};
		/// <summary>
		/// Get/Set a reference to the placeholder text object. This value is only
		/// allowed to have a plain_text type.
		/// </summary>
		[JsonProperty(PropertyName = "placeholder", Order = 3)]
		public SlackBlockTextObject Placeholder
		{
			get { return mPlaceholder; }
			set { mPlaceholder = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SlackBlockOptionCollection																							*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of SlackBlockOptionItem Items.
	/// </summary>
	public class SlackBlockOptionCollection : List<SlackBlockOptionItem>
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


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SlackBlockOptionItem																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Value of a single Slack Block option.
	/// </summary>
	public class SlackBlockOptionItem
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
		//*	Text																																	*
		//*-----------------------------------------------------------------------*
		private SlackBlockTextObject mText = new SlackBlockTextObject();
		/// <summary>
		/// Get a reference to the text object for this item.
		/// </summary>
		[JsonProperty(PropertyName = "text", Order = 0)]
		public SlackBlockTextObject Text
		{
			get { return mText; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Value																																	*
		//*-----------------------------------------------------------------------*
		private string mValue = "";
		/// <summary>
		/// Get/Set the unique value of this item within the group.
		/// </summary>
		[JsonProperty(PropertyName = "value", Order = 1)]
		public string Value
		{
			get { return mValue; }
			set { mValue = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*


	//*-------------------------------------------------------------------------*
	//*	SlackBlockTextObject																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Text as an object with multiple properties.
	/// </summary>
	public class SlackBlockTextObject
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
		//*	TextType																															*
		//*-----------------------------------------------------------------------*
		private string mTextType = "plain_text";
		/// <summary>
		/// Get/Set the type of text to present in this area.
		/// </summary>
		[JsonProperty(PropertyName = "type", Order = 0)]
		public string TextType
		{
			get { return mTextType; }
			set { mTextType = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Value																																	*
		//*-----------------------------------------------------------------------*
		private string mValue = "";
		/// <summary>
		/// Get/Set the text value.
		/// </summary>
		[JsonProperty(PropertyName = "text", Order = 1)]
		public string Value
		{
			get { return mValue; }
			set { mValue = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*




}
