//	SvgAnimationSceneMarkCharacter.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace SkiaSharpSvg
{
	//*-------------------------------------------------------------------------*
	//*	SvgAnimationSceneMarkCharacterCollection																*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of SvgAnimationSceneMarkCharacterItem Items.
	/// </summary>
	public class SvgAnimationSceneMarkCharacterCollection :
		List<SvgAnimationSceneMarkCharacterItem>
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
		//*	_Indexer																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the mark setting from the collection by character name.
		/// </summary>
		public SvgAnimationSceneMarkCharacterItem this[string characterName]
		{
			get
			{
				SvgAnimationSceneMarkCharacterItem result =
					this.FirstOrDefault(x => x.CharacterName == characterName);
				return result;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Clone																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Clone the values of the source collection into the target collection.
		/// </summary>
		/// <param name="source">
		/// </param>
		/// <param name="target">
		/// </param>
		public static void Clone(SvgAnimationSceneMarkCharacterCollection source,
			SvgAnimationSceneMarkCharacterCollection target)
		{
			if(target != null)
			{
				target.Clear();
			}
			if(source?.Count > 0 && target != null)
			{
				foreach(SvgAnimationSceneMarkCharacterItem item in source)
				{
					target.Add(new SvgAnimationSceneMarkCharacterItem(item));
				}
			}
		}
		//*-----------------------------------------------------------------------*



	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SvgAnimationSceneMarkCharacterItem																			*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Individual mark setting within an animation scene.
	/// </summary>
	public class SvgAnimationSceneMarkCharacterItem
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
		/// Create a new instance of the SvgAnimationSceneMarkCharacterItem Item.
		/// </summary>
		public SvgAnimationSceneMarkCharacterItem()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the SvgAnimationSceneMarkCharacterItem Item.
		/// </summary>
		/// <param name="source">
		/// </param>
		public SvgAnimationSceneMarkCharacterItem(
			SvgAnimationSceneMarkCharacterItem source)
		{
			if(source != null)
			{
				mCharacterName = source.mCharacterName;
				mDescription = source.mDescription;
				SvgAnimationInterpolationCollection.Clone(
					source.mInterpolations, mInterpolations);
				//SvgAnimationPropertyCollection.Clone(
				//	source.mProperties, mProperties);
				foreach(KeyValuePair<string, object> entry in source.mProperties)
				{
					mProperties.Add(entry.Key, entry.Value);
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	CharacterName																													*
		//*-----------------------------------------------------------------------*
		private string mCharacterName = "";
		/// <summary>
		/// Get/Set the name of the affected character.
		/// </summary>
		[JsonProperty(Order = 1)]
		public string CharacterName
		{
			get { return mCharacterName; }
			set { mCharacterName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Description																														*
		//*-----------------------------------------------------------------------*
		private string mDescription = "";
		/// <summary>
		/// Get/Set a brief description of this setting.
		/// </summary>
		[JsonProperty(Order = 0)]
		public string Description
		{
			get { return mDescription; }
			set { mDescription = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Interpolations																												*
		//*-----------------------------------------------------------------------*
		private SvgAnimationInterpolationCollection mInterpolations =
			new SvgAnimationInterpolationCollection();
		/// <summary>
		/// Get a reference to the collection of interpolation tasks enhancing this
		/// setting.
		/// </summary>
		[JsonProperty(Order = 3)]
		public SvgAnimationInterpolationCollection Interpolations
		{
			get { return mInterpolations; }
			set { mInterpolations = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Properties																														*
		//*-----------------------------------------------------------------------*
		private Dictionary<string, object> mProperties =
			new Dictionary<string, object>();
		/// <summary>
		/// Get a reference to the collection of properties for this character at
		/// this mark.
		/// </summary>
		[JsonProperty(Order = 2)]
		public Dictionary<string, object> Properties
		{
			get { return mProperties; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
