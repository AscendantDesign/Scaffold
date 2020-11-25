//	SvgAnimationFrame.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

using static SkiaSharpSvg.SvgAnimationUtil;

namespace SkiaSharpSvg
{
	//*-------------------------------------------------------------------------*
	//*	SvgAnimationFrameCollection																							*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of SvgAnimationFrameItem Items.
	/// </summary>
	public class SvgAnimationFrameCollection : List<SvgAnimationFrameItem>
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
		////*	CalculateDiscrete																											*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Calculate the graduating discrete values between the start and end
		///// frames.
		///// </summary>
		///// <param name="frames">
		///// Collection of resolved frames to which the resulting values will be
		///// stored.
		///// </param>
		///// <param name="characterName">
		///// Name of the character being resolved.
		///// </param>
		///// <param name="propertyName">
		///// Name of the property being set.
		///// </param>
		///// <param name="fromFrameIndex">
		///// Starting index to calculate.
		///// </param>
		///// <param name="fromValue">
		///// Starting value to set.
		///// </param>
		///// <param name="toFrameIndex">
		///// Ending index to calculate.
		///// </param>
		///// <param name="toValue">
		///// Ending value to set.
		///// </param>
		///// <param name="interpolationType">
		///// Type of interpolation being calculated.
		///// </param>
		//public static void CalculateDiscrete(
		//	SvgAnimationResolvedFrameCollection frames,
		//	string characterName, string propertyName,
		//	int fromFrameIndex, object fromValue, int toFrameIndex, object toValue,
		//	SvgAnimationInterpolationTypeEnum interpolationType)
		//{
		//	int index = 0;
		//	int indexRel = 0;
		//	double offset = 0d;
		//	object valOffs = null;

		//	if(frames?.Count > toFrameIndex && characterName?.Length > 0 &&
		//		propertyName != null)
		//	{
		//		switch(interpolationType)
		//		{
		//			case SvgAnimationInterpolationTypeEnum.BounceIn:
		//			case SvgAnimationInterpolationTypeEnum.BounceOut:
		//				//	Both bounce methods bounce from point A to point B.
		//				break;
		//			case SvgAnimationInterpolationTypeEnum.CountIn:
		//			case SvgAnimationInterpolationTypeEnum.CountOut:
		//			case SvgAnimationInterpolationTypeEnum.LinearIn:
		//			case SvgAnimationInterpolationTypeEnum.LinearOut:
		//			case SvgAnimationInterpolationTypeEnum.None:
		//				//	CountIn / CountOut have already been limited to the number
		//				//	of steps to take. When entering this method, they are the
		//				//	same as LinearIn / LinearOut.
		//				//	None is treated as LinearOut.
		//				if(IsDouble(fromValue) && IsDouble(toValue) &&
		//					toFrameIndex > fromFrameIndex)
		//				{
		//					frames[fromFrameIndex].Entries.
		//						AddUnique(characterName, propertyName).
		//						PropertyValue = (double)fromValue;
		//					offset = ((double)toValue - (double)fromValue) /
		//						(double)(toFrameIndex - fromFrameIndex);
		//					for(index = fromFrameIndex + 1, indexRel = 1;
		//						index < toFrameIndex; index++, indexRel++)
		//					{
		//						frames[index].Entries.AddUnique(characterName, propertyName).
		//							PropertyValue = (double)toValue + (offset * (double)index);
		//					}
		//					frames[toFrameIndex].Entries.
		//						AddUnique(characterName, propertyName).
		//						PropertyValue = (double)toValue;
		//				}
		//				else if(IsBool(fromValue) && IsBool(toValue))
		//				{
		//					//	When the values are bool, the effect is the same as
		//					//	immediate.
		//					for(index = fromFrameIndex; index < toFrameIndex; index++)
		//					{
		//						frames[index].Entries.AddUnique(characterName, propertyName).
		//							PropertyValue = fromValue;
		//					}
		//					frames[toFrameIndex].Entries.
		//						AddUnique(characterName, propertyName).PropertyValue = toValue;
		//				}
		//				else if(IsString(fromValue) && IsString(toValue))
		//				{
		//					//	When the values are string, the effect is the same as
		//					//	immediate.
		//					for(index = fromFrameIndex; index < toFrameIndex; index++)
		//					{
		//						frames[index].Entries.AddUnique(characterName, propertyName).
		//							PropertyValue = fromValue;
		//					}
		//					frames[toFrameIndex].Entries.
		//						AddUnique(characterName, propertyName).PropertyValue = toValue;
		//				}
		//				break;
		//			case SvgAnimationInterpolationTypeEnum.Immediate:
		//				//	The immediate interpolation is treated as a sudden toggle
		//				//	at the last frame of the range.
		//				for(index = fromFrameIndex; index < toFrameIndex; index ++)
		//				{
		//					frames[index].Entries.AddUnique(characterName, propertyName).
		//						PropertyValue = fromValue;
		//				}
		//				frames[toFrameIndex].Entries.
		//					AddUnique(characterName, propertyName).PropertyValue = toValue;
		//				break;
		//		}
		//	}
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Clone																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Clone the contents of the source collection into the target collection.
		/// </summary>
		/// <param name="source">
		/// </param>
		/// <param name="target">
		/// </param>
		public static void Clone(SvgAnimationFrameCollection source,
			SvgAnimationFrameCollection target)
		{
			if(target != null)
			{
				target.Clear();
			}
			if(source?.Count > 0 && target != null)
			{
				foreach(SvgAnimationFrameItem item in source)
				{
					target.Add(new SvgAnimationFrameItem(item));
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetIndex																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a frame by its frame index.
		/// </summary>
		/// <param name="frameIndex">
		/// Frame index of the frame to retrieve.
		/// </param>
		/// <returns>
		/// Reference to the first animation frame having the specified frame
		/// index, if found. Otherwise, null.
		/// </returns>
		public SvgAnimationFrameItem GetIndex(int frameIndex)
		{
			SvgAnimationFrameItem result =
				this.FirstOrDefault(x => x.FrameIndex == frameIndex);
			return result;
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	MergeMarkSettings																											*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Merge one or more mark settings and their properties into one or more
		///// frame elements, organized by interpolation type.
		///// </summary>
		///// <param name="element">
		///// Reference to the current element being filled.
		///// </param>
		///// <param name="settings">
		///// Scene mark settings to be resolved as frame elements.
		///// </param>
		///// <returns>
		///// Reference to a collection of frame elements that can be appended into
		///// the current frame.
		///// </returns>
		//public static SvgAnimationFrameElementCollection MergeMarkSettings(
		//	SvgAnimationFrameElementItem element,
		//	List<SvgAnimationSceneMarkCharacterItem> settings)
		//{
		//	string directionName = "";
		//	SvgAnimationInterpolationCollection interpolations = null;
		//	SvgAnimationFrameElementItem newElement = null;
		//	string propertyName = "";
		//	SvgAnimationFrameElementCollection result =
		//		new SvgAnimationFrameElementCollection();


		//	if(element != null && settings?.Count > 0)
		//	{
		//		if(element.InterpolationType == SvgAnimationInterpolationTypeEnum.None)
		//		{
		//			//	If the local interpolation wasn't defined, then the
		//			//	interpolations on the mark setting can be used.
		//			//	This condition will need a different element for each set of
		//			//	properties in an interpolation / count combination.
		//			interpolations = new SvgAnimationInterpolationCollection();
		//			foreach(SvgAnimationSceneMarkCharacterItem setting in settings)
		//			{
		//				foreach(SvgAnimationInterpolationItem interpol in setting.Interpolations)
		//				{
		//					if(!interpolations.Exists(x =>
		//						x.InterpolationType == interpol.InterpolationType &&
		//						x.InterpolationCount == interpol.InterpolationCount))
		//					{
		//						//	Type and count combination not found. Add a combo.
		//						interpolations.Add(new SvgAnimationInterpolationItem()
		//						{
		//							InterpolationType = interpol.InterpolationType,
		//							InterpolationCount = interpol.InterpolationCount
		//						});
		//					}
		//				}
		//			}
		//			//	At this point, we know all of the interpolation combinations for
		//			//	this character. There will be one element per interpolation type.
		//			foreach(SvgAnimationInterpolationItem interpol in interpolations)
		//			{
		//				result.Add(new SvgAnimationFrameElementItem()
		//				{
		//					InterpolationType = interpol.InterpolationType,
		//					InterpolationCount = interpol.InterpolationCount
		//				});
		//			}
		//			//	Set the properties associated with each of the interpolation
		//			//	combinations.
		//			foreach(SvgAnimationSceneMarkCharacterItem setting in settings)
		//			{
		//				foreach(KeyValuePair<string, object> property in
		//					setting.Properties)
		//				{
		//					//	Each property reference.
		//					propertyName = property.Key.ToLower();
		//					if(propertyName.EndsWith(".in") || propertyName.EndsWith(".out"))
		//					{
		//						//	Directional interpolations.
		//						directionName = property.Key.
		//							Substring(property.Key.LastIndexOf(".") + 1).ToLower();
		//						propertyName = property.Key.
		//							Substring(0, property.Key.LastIndexOf("."));
		//						if(directionName == "in")
		//						{
		//							//	This value only applies on incoming interpolations.
		//							foreach(SvgAnimationInterpolationItem interpol in
		//								setting.Interpolations)
		//							{
		//								newElement = null;
		//								if(interpol.InterpolationProperty == propertyName)
		//								{
		//									switch(interpol.InterpolationType)
		//									{
		//										case SvgAnimationInterpolationTypeEnum.BounceIn:
		//										case SvgAnimationInterpolationTypeEnum.CountIn:
		//										case SvgAnimationInterpolationTypeEnum.Immediate:
		//										case SvgAnimationInterpolationTypeEnum.LinearIn:
		//											newElement = result.FirstOrDefault(x =>
		//												x.InterpolationType ==
		//												interpol.InterpolationType &&
		//												x.InterpolationCount ==
		//												interpol.InterpolationCount);
		//											if(newElement != null)
		//											{
		//												newElement.Properties[propertyName] =
		//													property.Value;
		//											}
		//											break;
		//									}
		//								}
		//							}
		//						}
		//						else if(directionName == "out")
		//						{
		//							//	This value only applies on outgoing interpolations.
		//							foreach(SvgAnimationInterpolationItem interpol in
		//								setting.Interpolations)
		//							{
		//								newElement = null;
		//								if(interpol.InterpolationProperty == propertyName)
		//								{
		//									switch(interpol.InterpolationType)
		//									{
		//										case SvgAnimationInterpolationTypeEnum.BounceOut:
		//										case SvgAnimationInterpolationTypeEnum.CountOut:
		//										case SvgAnimationInterpolationTypeEnum.Immediate:
		//										case SvgAnimationInterpolationTypeEnum.LinearOut:
		//											newElement = result.FirstOrDefault(x =>
		//												x.InterpolationType ==
		//												interpol.InterpolationType &&
		//												x.InterpolationCount ==
		//												interpol.InterpolationCount);
		//											if(newElement != null)
		//											{
		//												newElement.Properties[propertyName] =
		//													property.Value;
		//											}
		//											break;
		//									}
		//								}
		//							}
		//						}
		//					}
		//					else
		//					{
		//						//	Singular. Valid with incoming or outgoing changes.
		//						propertyName = property.Key;
		//						foreach(SvgAnimationInterpolationItem interpol in
		//							setting.Interpolations)
		//						{
		//							newElement = null;
		//							if(interpol.InterpolationProperty == propertyName)
		//							{
		//								newElement = result.FirstOrDefault(x =>
		//									x.InterpolationType ==
		//									interpol.InterpolationType &&
		//									x.InterpolationCount ==
		//									interpol.InterpolationCount);
		//								if(newElement != null)
		//								{
		//									newElement.Properties[propertyName] =
		//										property.Value;
		//								}
		//							}
		//						}
		//					}
		//				}
		//			}
		//		}
		//		else
		//		{
		//			//	If the local interpolation is defined, any mark setting
		//			//	interpolations are overridden.
		//			newElement = new SvgAnimationFrameElementItem();
		//			newElement.InterpolationType = element.InterpolationType;
		//			newElement.InterpolationCount = element.InterpolationCount;
		//			newElement.Description = element.Description;
		//			newElement.ElementID = element.ElementID;
		//			newElement.ElementType = SvgAnimationElementTypeEnum.Character;
		//			foreach(SvgAnimationSceneMarkCharacterItem setting in settings)
		//			{
		//				foreach(KeyValuePair<string, object> property in
		//					setting.Properties)
		//				{
		//					newElement.Properties[property.Key] = property.Value;
		//				}
		//			}
		//			result.Add(newElement);
		//		}
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RenderScenes																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Render all scene and mark attributes to standard keyframes.
		/// </summary>
		/// <param name="keyFrames">
		/// Reference to the collection of the existing keyframe domain
		/// in which to place scene information.
		/// </param>
		/// <param name="scenes">
		/// Reference to the collection of scenes containing attributes to
		/// merge to the keyframe domain.
		/// </param>
		/// <returns>
		/// Reference to a new collection containing the merged keyframe and
		/// scene information.
		/// </returns>
		public static SvgAnimationFrameCollection RenderScenes(
			SvgAnimationFrameCollection keyFrames,
			SvgAnimationSceneCollection scenes)
		{
			SvgAnimationFrameElementItem element = null;
			int elementCount = 0;
			string elementDescription = "";
			string elementID = "";
			int elementIndex = 0;
			bool elementReused = false;
			SvgAnimationFrameItem frame = null;
			int frameCount = 0;
			int frameIndex = 0;
			SvgAnimationFrameCollection frames = new SvgAnimationFrameCollection();
			int interCount = 0;
			int interIndex = 0;
			SvgAnimationInterpolationItem interpol = null;
			int interpolationCount = 0;
			SvgAnimationInterpolationTypeEnum interpolationType =
				SvgAnimationInterpolationTypeEnum.None;
			SvgAnimationSceneMarkItem mark = null;
			SvgAnimationSceneMarkCharacterItem setting = null;
			List<string> usedProperties = new List<string>();

			if(keyFrames?.Count > 0)
			{
				frames.AddRange(keyFrames);
			}
			//	At this point, the frames collection is the main reference for the
			//	method.
			if(scenes?.Count > 0)
			{
				frameCount = frames.Count;
				for(frameIndex = 0; frameIndex < frameCount; frameIndex ++)
				{
					frame = frames[frameIndex];
					elementCount = frame.Elements.Count;
					for(elementIndex = 0; elementIndex < elementCount; elementIndex ++)
					{
						element = frame.Elements[elementIndex];
						//	Set the default interpolations for new elements of this type.
						elementDescription = element.Description;
						elementID = element.ElementID;
						interpolationType = element.InterpolationType;
						interpolationCount = element.InterpolationCount;
						if(element.ElementType == SvgAnimationElementTypeEnum.SceneMark)
						{
							//	Resolve a scene / mark.
							mark = SvgAnimationSceneCollection.FindMark(scenes,
								element.ElementMark, element.ElementID);
							if(mark != null)
							{
								//	The mark was found for this reference.
								setting = mark.MarkCharacters[element.ElementID];
								if(setting != null)
								{
									elementReused = false;
									usedProperties.Clear();
									element.ElementType = SvgAnimationElementTypeEnum.Character;
									interCount = setting.Interpolations.Count;
									if(interCount > 0)
									{
										interpol = setting.Interpolations[0];
										if(SvgAnimationFrameElementItem.
											AssignElementProperty(element, setting, 0))
										{
											//	Mark the property as used so it isn't added during
											//	clean up.
											usedProperties.Add(interpol.InterpolationProperty);
											elementReused = true;
										}
										//	If there are multiple interpolations for this
										//	character frame, add each of them as additional
										//	elements.
										for(interIndex = 1; interIndex < interCount;
											interIndex++)
										{
											interpol = setting.Interpolations[interIndex];
											elementIndex++;
											elementCount++;
											element = new SvgAnimationFrameElementItem()
											{
												ElementID = elementID,
												Description = elementDescription,
												InterpolationCount = interpol.InterpolationCount,
												InterpolationType = interpol.InterpolationType
											};
											frame.Elements.Insert(elementIndex, element);
											if(SvgAnimationFrameElementItem.
												AssignElementProperty(element, setting, interIndex))
											{
												usedProperties.Add(interpol.InterpolationProperty);
											}
										}
									}
									//	Any interpolations have been taken care of.
									//	Add the remaining properties that didn't have
									//	interpolation values assigned.
									foreach(KeyValuePair<string, object> property in
										setting.Properties)
									{
										if(!usedProperties.Exists(x => x == property.Key))
										{
											//	Add this property directly.
											if(elementReused)
											{
												elementIndex++;
												elementCount++;
												element = new SvgAnimationFrameElementItem()
												{
													ElementID = elementID,
													Description = elementDescription,
													InterpolationCount = interpolationCount,
													InterpolationType = interpolationType
												};
												frame.Elements.Insert(elementIndex, element);
											}
											else
											{
												elementReused = true;
											}
											SvgAnimationFrameElementItem.AssignElementProperty(
												element, property);
										}
									}
								}
								else
								{
									//	Character setting not found. Remove this reference.
									frame.Elements.RemoveAt(elementIndex);
									elementIndex--;
									elementCount--;
								}
							}
							else
							{
								//	Mark not found. Remove this reference.
								frame.Elements.RemoveAt(elementIndex);
								elementIndex--;
								elementCount--;
							}
						}
					}
				}
			}
			return frames;
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	ResolveFrames																													*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Resolve all of the implicit relations between keyframes, setting
		///// absolute property values for each frame in the series.
		///// </summary>
		///// <param name="keyFrames">
		///// Reference to a collection of animation keyframes to be resolved.
		///// </param>
		///// <param name="scenes">
		///// Reference to a collection of scenes containing indirect information.
		///// </param>
		///// <param name="expand">
		///// Optional value indicating whether to expand the frames into
		///// incremental values for production.
		///// </param>
		///// <returns>
		///// New collection of animation keyframes where all effects have been
		///// resolved to individual property values.
		///// </returns>
		///// <remarks>
		///// In the completed process, all frame elements will be of type
		///// Immediate.
		///// </remarks>
		//public static SvgAnimationResolvedFrameCollection ResolveFrames(
		//	SvgAnimationFrameCollection keyFrames,
		//	SvgAnimationSceneCollection scenes, bool expand = true)
		//{
		//	SvgAnimationBandItem charProp = null;
		//	SvgAnimationBandCollection charProps =
		//		new SvgAnimationBandCollection();
		//	int count = 0;
		//	SvgAnimationResolvedFrameCollection frames =
		//		new SvgAnimationResolvedFrameCollection();
		//	int index = 0;
		//	SvgAnimationSceneMarkItem mark = null;
		//	int max = 0;
		//	int mid = 0;
		//	object midValue = null;
		//	int min = 0;
		//	SvgAnimationResolvedFrameItem newFrame = null;
		//	SvgTrackingFrameElementItem next = null;
		//	SvgTrackingFrameElementItem prev = null;
		//	SvgAnimationSceneMarkCharacterItem setting = null;
		//	object value = null;

		//	if(keyFrames?.Count > 0)
		//	{
		//		//	Create the basic timeline.
		//		if(expand)
		//		{
		//			//	Frames are to be expanded for every sequential value.
		//			//min = keyFrames.Min(x => x.FrameIndex);
		//			min = 0;
		//			max = keyFrames.Max(x => x.FrameIndex);
		//			for(index = min; index <= max; index++)
		//			{
		//				frames.Add(new SvgAnimationResolvedFrameItem()
		//				{
		//					FrameIndex = index
		//				});
		//			}
		//		}
		//		else
		//		{
		//			//	Only the keyframe positions will be resolved.
		//			foreach(SvgAnimationFrameItem keyFrame in keyFrames)
		//			{
		//				frames.Add(new SvgAnimationResolvedFrameItem()
		//				{
		//					FrameIndex = keyFrame.FrameIndex
		//				});
		//			}
		//		}
		//		foreach(SvgAnimationFrameItem keyFrame in keyFrames)
		//		{
		//			newFrame = frames.GetIndex(keyFrame.FrameIndex);
		//			//	Transfer the frame description.
		//			if(keyFrame.Description.Length > 0)
		//			{
		//				newFrame.Description = keyFrame.Description;
		//			}
		//			//	Collect information about each character / layer property.
		//			foreach(SvgAnimationFrameElementItem element in keyFrame.Elements)
		//			{
		//				if(element.ElementType != SvgAnimationElementTypeEnum.SceneMark)
		//				{
		//					//	Frame is not a scene mark reference.
		//					foreach(KeyValuePair<string, object> property in
		//						element.Properties)
		//					{
		//						if(!charProps.Exists(x =>
		//							x.CharacterName == element.ElementID &&
		//							x.PropertyName == property.Key))
		//						{
		//							charProps.Add(
		//								element.ElementID, property.Key, property.Value);
		//						}
		//					}
		//				}
		//				else
		//				{
		//					//	Scene mark references have their properties defined in the
		//					//	scene.
		//					mark = scenes.FindMark(element.ElementMark, element.ElementID);
		//					if(mark != null)
		//					{
		//						//	Mark was found for the specified character.
		//						setting = mark.MarkCharacters.FirstOrDefault(x =>
		//							x.CharacterName == element.ElementID);
		//						if(setting != null)
		//						{
		//							//	Add the properties from this setting to the
		//							//	character / properties list.
		//							foreach(KeyValuePair<string, object> property in
		//								setting.Properties)
		//							{
		//								if(!charProps.Exists(x =>
		//									x.CharacterName == element.ElementID &&
		//									x.PropertyName == property.Key))
		//								{
		//									charProps.Add(
		//										element.ElementID, property.Key, property.Value);
		//								}
		//							}
		//						}
		//					}
		//				}
		//			}
		//		}
		//		//	Here, we have a discrete set of sequentially numbered
		//		//	frames, optional descriptions in key locations, and a master
		//		//	list of unique properties per character.
		//		//	Add all of the applicable unique keyframe references to each
		//		//	character / property.
		//		foreach(SvgAnimationFrameItem keyFrame in keyFrames)
		//		{
		//			foreach(SvgAnimationFrameElementItem element in keyFrame.Elements)
		//			{
		//				if(element.ElementType != SvgAnimationElementTypeEnum.SceneMark)
		//				{
		//					//	The element is a direct reference of the character, property,
		//					//	and value.
		//					foreach(KeyValuePair<string, object> property in
		//						element.Properties)
		//					{
		//						charProp = charProps[element.ElementID, property.Key];
		//						if(charProp != null)
		//						{
		//							charProp.Frames.Add(keyFrame, element, property);
		//						}
		//					}
		//				}
		//				else
		//				{
		//					//	The element is a scene mark. All scene mark properties
		//					//	are found at the reference location.
		//					mark = scenes.FindMark(element.ElementMark, element.ElementID);
		//					if(mark != null)
		//					{
		//						//	Mark was found for the specified character.
		//						setting = mark.MarkCharacters.FirstOrDefault(x =>
		//							x.CharacterName == element.ElementID);
		//						if(setting != null)
		//						{
		//							//	Add the properties from this setting to the
		//							//	frame reference list.
		//							foreach(KeyValuePair<string, object> property in
		//								setting.Properties)
		//							{
		//								charProp = charProps[element.ElementID, property.Key];
		//								if(charProp != null)
		//								{
		//									charProp.Frames.Add(keyFrame, element, property);
		//								}
		//							}
		//						}
		//					}
		//				}
		//			}
		//		}
		//		if(expand)
		//		{
		//			////	At this stage, all character / property combinations are directly
		//			////	associated with their own frames of interest.
		//			////	Resolve all of the frame values for each property on each
		//			////	character.
		//			//foreach(SvgAnimationBandItem charItem in charProps)
		//			//{
		//			//	if(charItem.Frames.Count > 0)
		//			//	{
		//			//		//	Preset values prior to the first keyframe.
		//			//		if(charItem.Frames.Min(x => x.FrameIndex) > 0)
		//			//		{
		//			//			value = charItem.Frames[0].PropertyValue;
		//			//			for(index = 0; index < min; index++)
		//			//			{
		//			//				//	Add static property value up to minimum keyframe index.
		//			//				frames[index].Entries.
		//			//					AddUnique(charItem.CharacterName, charItem.PropertyName).
		//			//					PropertyValue = value;
		//			//			}
		//			//		}
		//			//		//	Calculate the per-frame values between every two keyframes.
		//			//		count = charItem.Frames.Count - 1;
		//			//		for(index = 0; index < count; index++)
		//			//		{
		//			//			prev = charItem.Frames[index];
		//			//			next = charItem.Frames[index + 1];
		//			//			//	!1. Create discrete values between two keyframes.
		//			//			if(next.InterpolationType ==
		//			//				SvgAnimationInterpolationTypeEnum.Immediate)
		//			//			{
		//			//				//	When the right frame is immediate, all values
		//			//				//	from left to right - 1 are equal to left.
		//			//				value = prev.PropertyValue;
		//			//				for(index = prev.FrameIndex;
		//			//					index < next.FrameIndex; index++)
		//			//				{
		//			//					frames[index].Entries.
		//			//						AddUnique(charItem.CharacterName, charItem.PropertyName).
		//			//						PropertyValue = value;
		//			//				}
		//			//			}
		//			//			else if(next.InterpolationType ==
		//			//				SvgAnimationInterpolationTypeEnum.BounceIn)
		//			//			{
		//			//				//	When the right frame is bounce in, half of the range
		//			//				//	between left and right is used for bounce if the
		//			//				//	left interpolation is unmeasured.
		//			//				//	If measured, the bounce in begins after the last
		//			//				//	frame of the previous measurement.
		//			//				switch(prev.InterpolationType)
		//			//				{
		//			//					//	Unmeasured left styles.
		//			//					//	Bounce occupies entire distance.
		//			//					case SvgAnimationInterpolationTypeEnum.BounceOut:
		//			//					case SvgAnimationInterpolationTypeEnum.LinearOut:
		//			//					case SvgAnimationInterpolationTypeEnum.None:
		//			//						//	Get the frame between the two.
		//			//						mid = next.FrameIndex -
		//			//							((next.FrameIndex - prev.FrameIndex) / 2);
		//			//						//	Get the value in the middle.
		//			//						if(IsDouble(prev.PropertyValue))
		//			//						{
		//			//							midValue = (double)prev.PropertyValue +
		//			//								(((double)next.PropertyValue -
		//			//								(double)prev.PropertyValue) / 2.0);
		//			//						}
		//			//						if(mid >= prev.FrameIndex && mid <= next.FrameIndex)
		//			//						{
		//			//							CalculateDiscrete(frames,
		//			//								charItem.CharacterName, charItem.PropertyName,
		//			//								prev.FrameIndex, prev.PropertyValue,
		//			//								mid, midValue, prev.InterpolationType);
		//			//						}
		//			//						break;
		//			//					//	Measured left styles.
		//			//					//	Bounce occupies distance after last measured frame.
		//			//					case SvgAnimationInterpolationTypeEnum.CountOut:
		//			//					case SvgAnimationInterpolationTypeEnum.Immediate:
		//			//						break;
		//			//				}
		//			//			}
		//			//		}
		//			//		//	Postset values following the last keyframe.
		//			//		if(charItem.Frames.Max(x => x.FrameIndex) < max)
		//			//		{
		//			//			value = charItem.Frames.Last().PropertyValue;
		//			//			for(index = charItem.Frames.Last().FrameIndex;
		//			//				index < max; index++)
		//			//			{
		//			//				//	Add static property value up to maximum keyframe index.
		//			//				frames[index].Entries.
		//			//					AddUnique(charItem.CharacterName, charItem.PropertyName).
		//			//					PropertyValue = value;
		//			//			}
		//			//		}
		//			//	}
		//			//}
		//		}
		//		else
		//		{
		//			//	When not expanding, just add character / property references
		//			//	to the entries collection of each keyframe.
		//			foreach(SvgAnimationBandItem charItem in charProps)
		//			{
		//				if(charItem.Frames.Count > 0)
		//				{
		//					foreach(SvgTrackingFrameElementItem frame in charItem.Frames)
		//					{
		//						//newFrame = frames.GetIndex(frame.FrameIndex);
		//						//charProp = newFrame.Entries.
		//						//	AddUnique(charItem.CharacterName, charItem.PropertyName);
		//						//charProp.PropertyValue = value;
		//						//if(charProp.Frames.Count == 0)
		//						//{
		//						//	charProp.Frames.Add(frame);
		//						//}
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return frames;
		//}
		////*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SvgAnimationFrameItem																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about an individual frame.
	/// </summary>
	public class SvgAnimationFrameItem
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
		/// Create a new instance of the SvgAnimationFrameItem Item.
		/// </summary>
		public SvgAnimationFrameItem()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the SvgAnimationFrameItem Item.
		/// </summary>
		/// <param name="source">
		/// </param>
		public SvgAnimationFrameItem(SvgAnimationFrameItem source)
		{
			if(source != null)
			{
				mDescription = source.mDescription;
				SvgAnimationFrameElementCollection.Clone(source.mElements, mElements);
				mFrameIndex = source.mFrameIndex;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Description																														*
		//*-----------------------------------------------------------------------*
		private string mDescription = "";
		/// <summary>
		/// Get/Set a brief description of this frame.
		/// </summary>
		[JsonProperty(Order = 0)]
		public string Description
		{
			get { return mDescription; }
			set { mDescription = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Elements																															*
		//*-----------------------------------------------------------------------*
		private SvgAnimationFrameElementCollection mElements =
			new SvgAnimationFrameElementCollection();
		/// <summary>
		/// Get a reference to the collection of element settings for this frame.
		/// </summary>
		[JsonProperty(Order = 2)]
		public SvgAnimationFrameElementCollection Elements
		{
			get { return mElements; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	FrameIndex																														*
		//*-----------------------------------------------------------------------*
		private int mFrameIndex = 0;
		/// <summary>
		/// Get/Set the frame index of this item.
		/// </summary>
		[JsonProperty(Order = 1)]
		public int FrameIndex
		{
			get { return mFrameIndex; }
			set { mFrameIndex = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
