//	SvgAnimationScene.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using Newtonsoft.Json;

namespace SkiaSharpSvg
{
	//*-------------------------------------------------------------------------*
	//*	SvgAnimationSceneCollection																							*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of SvgAnimationSceneItem Items.
	/// </summary>
	public class SvgAnimationSceneCollection : List<SvgAnimationSceneItem>
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
		//*	Clone																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fill the target collection with a full clone of the source collection.
		/// </summary>
		/// <param name="source">
		/// </param>
		/// <param name="target">
		/// </param>
		public static void Clone(SvgAnimationSceneCollection source,
			SvgAnimationSceneCollection target)
		{
			if(target != null)
			{
				target.Clear();
			}
			if(source?.Count > 0 && target != null)
			{
				foreach(SvgAnimationSceneItem item in source)
				{
					target.Add(new SvgAnimationSceneItem(item));
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	FindMark																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Given the scene.mark name in dotted notation, find and return the
		/// scene mark.
		/// </summary>
		/// <param name="scenes">
		/// Reference to a collection of scenes to be searched.
		/// </param>
		/// <param name="dottedName">
		/// A scene.mark name, expressed with a dot between the scene and mark
		/// names. If no dot is presented, the name is assumed to be that of the
		/// marker. All of the marks of each scene are searched until a matching
		/// mark name is found.
		/// </param>
		/// <returns>
		/// Reference to a scene mark, if found. Otherwise null.
		/// </returns>
		public static SvgAnimationSceneMarkItem FindMark(
			SvgAnimationSceneCollection scenes, string dottedName)
		{
			SvgAnimationSceneMarkItem result = null;
			SvgAnimationSceneItem scene = null;
			string[] values = null;

			if(scenes != null)
			{
				if(dottedName?.Length > 0)
				{
					if(dottedName.IndexOf('.') >= 0)
					{
						//	Scene.Mark syntax provided.
						values = dottedName.ToLower().Split(new char[] { '.' });
						if(values.Length > 1)
						{
							scene = scenes.FirstOrDefault(x =>
								x.SceneName.ToLower() == values[0]);
							if(scene != null)
							{
								result =
									scene.Marks.FirstOrDefault(x =>
									x.MarkName.ToLower() == values[1]);
							}
						}
					}
					else
					{
						//	No dot. Search for mark name.
						foreach(SvgAnimationSceneItem sceneItem in scenes)
						{
							result =
								sceneItem.Marks.FirstOrDefault(x =>
								x.MarkName.ToLower() == dottedName.ToLower());
							if(result != null)
							{
								break;
							}
						}
					}
				}
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Given the scene.mark name in dotted notation, find and return the
		/// scene mark.
		/// </summary>
		/// <param name="scenes">
		/// Reference to a collection of scenes to be searched.
		/// </param>
		/// <param name="dottedName">
		/// A scene.mark name, expressed with a dot between the scene and mark
		/// names. If no dot is presented, the name is assumed to be that of the
		/// marker. All of the marks of each scene are searched until a matching
		/// mark name is found.
		/// </param>
		/// <param name="characterName">
		/// Name of the character to be found in the specified scene and mark.
		/// </param>
		/// <returns>
		/// Reference to a scene mark, if found. Otherwise null.
		/// </returns>
		public static SvgAnimationSceneMarkItem FindMark(
			SvgAnimationSceneCollection scenes, string dottedName,
			string characterName)
		{
			SvgAnimationSceneMarkItem result = null;
			SvgAnimationSceneItem scene = null;
			string[] values = null;

			if(scenes != null)
			{
				if(dottedName?.Length > 0)
				{
					if(dottedName.IndexOf('.') >= 0)
					{
						//	Scene.Mark syntax provided.
						values = dottedName.ToLower().Split(new char[] { '.' });
						if(values.Length > 1)
						{
							scene = scenes.FirstOrDefault(x =>
								x.SceneName.ToLower() == values[0]);
							if(scene != null)
							{
								result =
									scene.Marks.FirstOrDefault(x =>
									x.MarkName.ToLower() == values[1] &&
									x.MarkCharacters.FirstOrDefault(y =>
										y.CharacterName == characterName) != null);
							}
						}
					}
					else
					{
						//	No dot. Search for mark name.
						foreach(SvgAnimationSceneItem sceneItem in scenes)
						{
							result =
								sceneItem.Marks.FirstOrDefault(x =>
								x.MarkName.ToLower() == dottedName.ToLower() &&
								x.MarkCharacters.FirstOrDefault(y =>
									y.CharacterName == characterName) != null);
							if(result != null)
							{
								break;
							}
						}
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*



	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SvgAnimationSceneItem																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Individual scene definition.
	/// </summary>
	public class SvgAnimationSceneItem
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
		/// Create a new instance of the SvgAnimationSceneItem Item.
		/// </summary>
		public SvgAnimationSceneItem()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the SvgAnimationSceneItem Item.
		/// </summary>
		/// <param name="source">
		/// </param>
		public SvgAnimationSceneItem(SvgAnimationSceneItem source)
		{
			if(source != null)
			{
				mDescription = source.mDescription;
				SvgAnimationSceneMarkCollection.Clone(source.mMarks, mMarks);
				mSceneName = source.mSceneName;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Description																														*
		//*-----------------------------------------------------------------------*
		private string mDescription = "";
		/// <summary>
		/// Get/Set a brief description of this scene.
		/// </summary>
		[JsonProperty(Order = 0)]
		public string Description
		{
			get { return mDescription; }
			set { mDescription = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Marks																																	*
		//*-----------------------------------------------------------------------*
		private SvgAnimationSceneMarkCollection mMarks =
			new SvgAnimationSceneMarkCollection();
		/// <summary>
		/// Get a reference to the collection of marks in this scene.
		/// </summary>
		[JsonProperty(Order = 2)]
		public SvgAnimationSceneMarkCollection Marks
		{
			get { return mMarks; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SceneName																															*
		//*-----------------------------------------------------------------------*
		private string mSceneName = "";
		/// <summary>
		/// Get/Set the name of the scene.
		/// </summary>
		[JsonProperty(Order = 1)]
		public string SceneName
		{
			get { return mSceneName; }
			set { mSceneName = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
