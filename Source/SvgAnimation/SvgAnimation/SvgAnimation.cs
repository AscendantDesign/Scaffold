//	SvgAnimation.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using Newtonsoft.Json;

namespace SkiaSharpSvg
{
	//*-------------------------------------------------------------------------*
	//*	SvgAnimation																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// SVG animation engine.
	/// </summary>
	public class SvgAnimation
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
		////*	BuildTimeline																													*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Build a frame-based timeline from a combination of scenes and frames.
		///// </summary>
		///// <param name="source">
		///// Multipurpose scene and frame information.
		///// </param>
		///// <returns>
		///// New frame-based timeline where all elements have been reduced to SVG
		///// object references.
		///// </returns>
		///// <remarks>
		///// Source frame indexes may occur multiple times in the stack. However,
		///// the output collection has single-referenced indexes and is sorted
		///// by frame index.
		///// </remarks>
		//public static SvgAnimationFrameCollection BuildTimeline(
		//	SvgAnimation source)
		//{
		//	//	TODO: Build timeline in animation class.
		//	List<SvgAnimationSceneMarkCharacterItem> characters;
		//	int frameIndex = 0;
		//	SvgAnimationFrameCollection frames = new SvgAnimationFrameCollection();
		//	List<SvgAnimationFrameItem> frameSet = null;
		//	SvgAnimationSceneMarkItem mark = null;
		//	SvgAnimationFrameElementItem newElement = null;
		//	SvgAnimationFrameElementCollection newElements = null;
		//	SvgAnimationFrameItem newFrame = null;

		//	if(source != null)
		//	{
		//		if(source.Frames.Count > 0)
		//		{
		//			//	Frames are present.
		//			//	Scenes are for reference only.
		//			//	Scene elements not referenced by a frame are not used.
		//			//	Add frames.
		//			foreach(SvgAnimationFrameItem frame in source.Frames)
		//			{
		//				frameIndex = frame.FrameIndex;
		//				if(!frames.Exists(x => x.FrameIndex == frameIndex))
		//				{
		//					//	Only add this frame index if it hasn't already been
		//					//	encountered.
		//					frameSet =
		//						source.Frames.FindAll(x => x.FrameIndex == frameIndex);
		//					if(frameSet?.Count > 0)
		//					{
		//						//	All source frames having the specified frame index.
		//						newFrame = new SvgAnimationFrameItem();
		//						newFrame.FrameIndex = frameIndex;
		//						foreach(SvgAnimationFrameItem frameItem in frameSet)
		//						{
		//							foreach(SvgAnimationFrameElementItem elementItem in
		//								frameItem.Elements)
		//							{
		//								switch(elementItem.ElementType)
		//								{
		//									case SvgAnimationElementTypeEnum.Character:
		//									case SvgAnimationElementTypeEnum.Layer:
		//										//	Character and layer are identified directly.
		//										//	Character is a shape or image. Layer is a group.
		//										newElement =
		//											new SvgAnimationFrameElementItem(elementItem);
		//										newFrame.Elements.Add(newElement);
		//										break;
		//									case SvgAnimationElementTypeEnum.SceneMark:
		//										//	Scene mark needs to be resolved before the element
		//										//	can be added to the frame.
		//										mark =
		//											SvgAnimationSceneCollection.FindMark(
		//												source.mScenes, elementItem.ElementMark);
		//										if(mark != null)
		//										{
		//											//	Find all of the settings with the current
		//											//	character name.
		//											characters = mark.MarkCharacters.FindAll(x =>
		//												x.CharacterName == elementItem.ElementID);
		//											if(characters != null)
		//											{
		//												newElements = SvgAnimationFrameCollection.
		//													MergeMarkSettings(elementItem, characters);
		//												if(newElements?.Count > 0)
		//												{
		//													newFrame.Elements.AddRange(newElements);
		//												}
		//											}
		//										}
		//										break;
		//								}
		//							}
		//						}
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return frames;
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Clone																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a deep copy of the caller's animation data set.
		/// </summary>
		/// <param name="source">
		/// Source animation to copy.
		/// </param>
		/// <returns>
		/// Deep value-wise copy of the caller's animation, if successful.
		/// Otherwise, null.
		/// </returns>
		/// <remarks>
		/// Useful for compiling scene information to the keyframes list while
		/// maintaining an editable version of the original timeline.
		/// </remarks>
		public static SvgAnimation Clone(SvgAnimation source)
		{
			SvgAnimation result = null;

			if(source != null)
			{
				//	Source was presented.
				result = new SvgAnimation();
				result.mDescription = source.mDescription;
				SvgAnimationFrameCollection.Clone(source.mFrames, result.mFrames);
				SvgAnimationSceneCollection.Clone(source.mScenes, result.mScenes);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	CompiledHTMLFilename																									*
		//*-----------------------------------------------------------------------*
		private string mCompiledHTMLFilename = "";
		/// <summary>
		/// Get/Set the name of the compiled HTML filename to be used for
		/// SVG-In-HTML operations.
		/// </summary>
		[JsonProperty(Order = 2)]
		public string CompiledHTMLFilename
		{
			get { return mCompiledHTMLFilename; }
			set { mCompiledHTMLFilename = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Description																														*
		//*-----------------------------------------------------------------------*
		private string mDescription = "";
		/// <summary>
		/// Get/Set a brief description of the animation data set.
		/// </summary>
		[JsonProperty(Order = 0)]
		public string Description
		{
			get { return mDescription; }
			set { mDescription = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Frames																																*
		//*-----------------------------------------------------------------------*
		private SvgAnimationFrameCollection mFrames =
			new SvgAnimationFrameCollection();
		/// <summary>
		/// Get a reference to the collection of frames in this animation.
		/// </summary>
		[JsonProperty(Order = 4)]
		public SvgAnimationFrameCollection Frames
		{
			get { return mFrames; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Scenes																																*
		//*-----------------------------------------------------------------------*
		private SvgAnimationSceneCollection mScenes =
			new SvgAnimationSceneCollection();
		/// <summary>
		/// Get a reference to the collection of scenes in this animation.
		/// </summary>
		[JsonProperty(Order = 3)]
		public SvgAnimationSceneCollection Scenes
		{
			get { return mScenes; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SourceArtFilename																											*
		//*-----------------------------------------------------------------------*
		private string mSourceArtFilename = "";
		/// <summary>
		/// Get/Set the path and filename of the source art file.
		/// </summary>
		/// <remarks>
		/// This refers to the source SVG file whose elements will be compiled,
		/// displayed, and used in animation.
		/// </remarks>
		[JsonProperty(Order = 1)]
		public string SourceArtFilename
		{
			get { return mSourceArtFilename; }
			set { mSourceArtFilename = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
