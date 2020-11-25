//	SvgAnimationResolver.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using static SkiaSharpSvg.SvgAnimationUtil;

namespace SkiaSharpSvg
{
	//*-------------------------------------------------------------------------*
	//*	SvgAnimationResolver																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Tween and easing value band resolver for SVG animation.
	/// </summary>
	public class SvgAnimationResolver
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		private double dValueLast = 0.0;
		private double dValueMid = 0.0;
		private double dValuePrev = 0.0;
		private double dValueNext = 0.0;

		private SvgAnimationBandCollection mBand = null;
		private SvgAnimationFrameElementItem mElementHi = null;
		private SvgAnimationFrameElementItem mElementLo = null;
		private SvgAnimationFrameItem mFrameHi = null;
		private SvgAnimationFrameItem mFrameLo = null;
		private int mIndex = 0;

		private int mIndexEnd = 0;
		private int mIndexMax = 0;
		private int mIndexMin = 0;

		private SvgAnimationInterpolationTypeEnum mInterNext =
			SvgAnimationInterpolationTypeEnum.None;
		private SvgAnimationInterpolationTypeEnum mInterPrev =
			SvgAnimationInterpolationTypeEnum.None;

		private SvgAnimationFrameCollection mKeyFrames = null;

		private string mPropertyName = "";

		private object mValueLast = null;
		private object mValueNext = null;
		private object mValuePrev = null;

		//*-----------------------------------------------------------------------*
		//* AddLinearCount																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add a linear gradent of samples starting at value1 and ending at value2
		/// that span the count range starting at the specified index.
		/// </summary>
		/// <param name="valueLo">
		/// Left value in the sample chain.
		/// </param>
		/// <param name="valueHi">
		/// Right value in the sample chain.
		/// </param>
		/// <param name="startIndex">
		/// The frame index at which to start.
		/// </param>
		/// <param name="count">
		/// Count of frames to cover.
		/// </param>
		/// <returns>
		/// Next index at which to process frames.
		/// </returns>
		private int AddLinearCount(double valueLo, double valueHi,
			int startIndex, int count)
		{
			double difference = valueHi - valueLo;
			int index = startIndex;
			int result = startIndex + count;
			List<double> samples = (count > 0 ? EaseLinear(count) : null);

			if(samples != null)
			{
				foreach(double sample in samples)
				{
					dValueLast = valueHi - (difference * sample);
					mValueLast = dValueLast;
					mBand.Add(new SvgAnimationBandItem()
					{
						FrameIndex = index,
						PropertyValue = dValueLast
					});
					index++;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* AddLinearRange																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add a linear gradent of samples starting at value1 and ending at value2
		/// that span the count range starting at the specified index.
		/// </summary>
		/// <param name="valueLo">
		/// Left value in the sample chain.
		/// </param>
		/// <param name="valueHi">
		/// Right value in the sample chain.
		/// </param>
		/// <param name="startIndex">
		/// The frame index at which to start.
		/// </param>
		/// <param name="endIndex">
		/// The frame index at which to end.
		/// </param>
		/// <returns>
		/// Next index at which to process frames.
		/// </returns>
		private int AddLinearRange(double valueLo, double valueHi,
			int startIndex, int endIndex)
		{
			int count = endIndex - startIndex + 1;
			return AddLinearCount(valueLo, valueHi, startIndex, count);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* AddPatternCount																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add a pattern of values in the range of valueLo to valueHi, starting
		/// at one frame index and continuing for a specified count.
		/// </summary>
		/// <param name="easing">
		/// Easing pattern to use.
		/// </param>
		/// <param name="valueLo">
		/// Left side value.
		/// </param>
		/// <param name="valueHi">
		/// Right side value.
		/// </param>
		/// <param name="startIndex">
		/// Frame index at which to start.
		/// </param>
		/// <param name="count">
		/// Number of frames to apply.
		/// </param>
		/// <returns>
		/// Next available frame index.
		/// </returns>
		private int AddPatternCount(SvgAnimationEasingTypeEnum easing,
			double valueLo, double valueHi,
			int startIndex, int count)
		{
			double difference = valueHi - valueLo;
			int index = startIndex;
			int result = startIndex + count;
			List<double> samples = null;

			switch(easing)
			{
				case SvgAnimationEasingTypeEnum.Bounce:
					samples = EaseBouncePattern(count, 2);
					break;
				case SvgAnimationEasingTypeEnum.Linear:
				case SvgAnimationEasingTypeEnum.None:
					samples = EaseLinear(count);
					break;
			}
			if(samples != null)
			{
				foreach(double sample in samples)
				{
					dValueLast = valueHi - (difference * sample);
					mValueLast = dValueLast;
					mBand.Add(new SvgAnimationBandItem()
					{
						FrameIndex = index,
						PropertyValue = dValueLast
					});
					index++;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* AddPatternRange																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add a pattern of values in the range of valueLo to valueHi, starting
		/// at one frame index and continuing for a specified count.
		/// </summary>
		/// <param name="easing">
		/// Easing pattern to use.
		/// </param>
		/// <param name="valueLo">
		/// Left side value.
		/// </param>
		/// <param name="valueHi">
		/// Right side value.
		/// </param>
		/// <param name="startIndex">
		/// Frame index at which to start.
		/// </param>
		/// <param name="endIndex">
		/// Frame index at which to end the process. This frame is included.
		/// </param>
		/// <returns>
		/// Next available frame index.
		/// </returns>
		private int AddPatternRange(SvgAnimationEasingTypeEnum easing,
			double valueLo, double valueHi,
			int startIndex, int endIndex)
		{
			int count = endIndex - startIndex + 1;
			return AddPatternCount(easing, valueLo, valueHi, startIndex, count);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* AddValueRange																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add a single value for each index in the specified range.
		/// </summary>
		/// <param name="value">
		/// The value to add to the band collection for this character property.
		/// </param>
		/// <param name="startIndex">
		/// Starting frame index to record.
		/// </param>
		/// <param name="endIndex">
		/// Ending frame index to record.
		/// </param>
		/// <remarks>
		/// In this version, only the first and last instances of a single value
		/// are added. In the time between two equal values, that value is implied
		/// statically.
		/// </remarks>
		private int AddValueRange(object value, int startIndex, int endIndex)
		{
			int result = endIndex + 1;
			if(startIndex < endIndex)
			{
				mBand.Add(new SvgAnimationBandItem()
				{
					FrameIndex = startIndex,
					PropertyValue = value
				});
			}
			mBand.Add(new SvgAnimationBandItem()
			{
				FrameIndex = endIndex,
				PropertyValue = value
			});
			mValueLast = value;
			if(IsDouble(value))
			{
				double.TryParse(mValueLast.ToString(), out dValueLast);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* AssignLeftRightIndex																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Assign the left (min) and right (max) frame indexes for the current
		/// property band surrounding the specified frame index.
		/// </summary>
		/// <param name="frameIndex">
		/// Current frame index to process.
		/// </param>
		/// <param name="frameMax">
		/// Maximum frame number for animation.
		/// </param>
		private void AssignLeftRightIndex(int frameIndex, int frameMax)
		{
			List<SvgAnimationFrameItem> frames = null;

			frames = mKeyFrames.FindAll(x => x.FrameIndex < frameIndex &&
				x.Elements.Exists(y => y.Properties.ContainsKey(mPropertyName)));
			if(frames.Count > 0)
			{
				mIndex = frames.Max(x => x.FrameIndex);
			}
			else
			{
				mIndex = 0;
			}
			mFrameLo = frames.FirstOrDefault(x => x.FrameIndex == mIndex);
			if(mFrameLo != null)
			{
				mIndexMin = mFrameLo.FrameIndex;
			}
			else
			{
				//	If a left edge was not found, use the left boundary.
				mIndexMin = 0;
			}
			frames = mKeyFrames.FindAll(x => x.FrameIndex >= frameIndex &&
				x.Elements.Exists(y => y.Properties.ContainsKey(mPropertyName)));
			if(frames.Count > 0)
			{
				mIndex = frames.Min(x => x.FrameIndex);
			}
			else
			{
				mIndex = 0;
			}
			mFrameHi = frames.FirstOrDefault(x => x.FrameIndex == mIndex);
			if(mFrameHi != null)
			{
				mIndexMax = mFrameHi.FrameIndex;
			}
			else
			{
				//	If a right edge was not found, use the right boundary.
				mIndexMax = frameMax;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetLeftIndex																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the left index of the specified element, given its interpolation
		/// type and count.
		/// </summary>
		/// <param name="frameIndex">
		/// The frame index of the current element.
		/// </param>
		/// <param name="element">
		/// Reference to the element to inspect.
		/// </param>
		/// <returns>
		/// Frame index of the left side of the element's scope of influence.
		/// </returns>
		private int GetLeftIndex(int frameIndex,
			SvgAnimationFrameElementItem element)
		{
			int result = 0;

			if(element != null)
			{
				switch(element.InterpolationType)
				{
					case SvgAnimationInterpolationTypeEnum.BounceIn:
					case SvgAnimationInterpolationTypeEnum.CountIn:
						result = frameIndex - element.InterpolationCount + 1;
						break;
					case SvgAnimationInterpolationTypeEnum.Immediate:
						result = frameIndex;
						break;
					default:
						result = frameIndex;
						break;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetPreviousOutputValue																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value previous to the specified frame index, preferring an
		/// output interpolation type, if available, but accepting an input
		/// interpolation type as an alternative.
		/// </summary>
		/// <param name="frameIndex">
		/// Frame at which to make the comparison.
		/// </param>
		/// <returns>
		/// The property value specified most recently previous to the specified
		/// frame index.
		/// </returns>
		private object GetPreviousOutputValue(int frameIndex)
		{
			SvgAnimationFrameElementItem element = null;
			SvgAnimationFrameItem frame = null;
			int frameMax = 0;
			List<SvgAnimationFrameItem> frames = null;
			object result = null;

			if(mPropertyName?.Length > 0 && mKeyFrames?.Count > 0 && frameIndex > 0)
			{
				frames = mKeyFrames.FindAll(x => x.FrameIndex < frameIndex &&
					x.Elements.Exists(y => y.Properties.ContainsKey(mPropertyName)));
				if(frames.Count > 0)
				{
					frameMax = frames.Max(x => x.FrameIndex);
					frame = frames.FirstOrDefault(x => x.FrameIndex == frameMax);
					if(frame != null)
					{
						element = frame.Elements.FirstOrDefault(x =>
							x.Properties.ContainsKey(mPropertyName));
						//	Refine search for output interpolation type.
						if(element != null && !IsOutbound(element.InterpolationType) &&
							frame.Elements.Exists(x =>
								x.Properties.ContainsKey(mPropertyName) &&
								IsOutbound(x.InterpolationType)))
						{
							//	Prefer an outgoing item.
							element = frame.Elements.FirstOrDefault(x =>
								x.Properties.ContainsKey(mPropertyName) &&
								IsOutbound(x.InterpolationType));
						}
						if(element != null)
						{
							//	Assign the property value.
							result = element.Properties[mPropertyName];
						}
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetRightIndex																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the right index of the specified element, given its
		/// interpolation type and count.
		/// </summary>
		/// <param name="frameIndex">
		/// The frame index of the current element.
		/// </param>
		/// <param name="element">
		/// Reference to the element to inspect.
		/// </param>
		/// <returns>
		/// Frame index of the right side of the element's scope of influence.
		/// </returns>
		private int GetRightIndex(int frameIndex,
			SvgAnimationFrameElementItem element)
		{
			int result = 0;

			if(element != null)
			{
				switch(element.InterpolationType)
				{
					case SvgAnimationInterpolationTypeEnum.BounceOut:
					case SvgAnimationInterpolationTypeEnum.CountOut:
						result = frameIndex + element.InterpolationCount;
						break;
					case SvgAnimationInterpolationTypeEnum.Immediate:
						result = frameIndex;
						break;
					default:
						result = frameIndex;
						break;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* HasPreviousValue																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the current property has value
		/// previous to the specified frame index.
		/// </summary>
		/// <param name="frameIndex">
		/// Frame at which to make the comparison.
		/// </param>
		/// <returns>
		/// True if the current working property has a previously specified value.
		/// Otherwise, false.
		/// </returns>
		private bool HasPreviousValue(int frameIndex)
		{
			List<SvgAnimationFrameItem> frames = null;
			bool result = false;

			if(mPropertyName?.Length > 0 && mKeyFrames?.Count > 0 && frameIndex > 0)
			{
				frames = mKeyFrames.FindAll(x => x.FrameIndex < frameIndex &&
					x.Elements.Exists(y => y.Properties.ContainsKey(mPropertyName)));
				result = frames.Count > 0;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Interpolate																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Interpolate between the left and right edges.
		/// </summary>
		private void Interpolate()
		{
			double dValue = 0.0;
			SvgAnimationFrameElementItem prevRef = null;
			object prevValue = null;

			if(mInterNext == SvgAnimationInterpolationTypeEnum.BounceIn)
			{
				Debug.WriteLine("Break here...");
			}

			if(IsOutbound(mInterPrev) && IsMeasured(mInterPrev, mValuePrev))
			{
				//	Previous item is measured.
				//	On a left-side count-out, we prefer to start with
				//	the last known incoming value at that frame.
				prevRef = mFrameLo.Elements.FirstOrDefault(x =>
					IsInbound(x.InterpolationType) &&
					x.Properties.ContainsKey(mPropertyName));
				if(prevRef == null)
				{
					//	An incoming value was not found. Reuse the existing
					//	value.
					prevRef = mElementLo;
				}
				prevValue = prevRef.Properties[mPropertyName];
				if(IsDouble(prevValue))
				{
					double.TryParse(prevValue.ToString(), out dValue);
				}
				mIndex = mIndexMin;
				//	Count from A to A+C1.
				mIndex = AddLinearCount(dValue, dValuePrev,
					mIndex,
					(mElementLo.InterpolationType ==
					SvgAnimationInterpolationTypeEnum.Immediate ? 1 :
					mElementLo.InterpolationCount));
				if(IsInbound(mInterNext) && IsMeasured(mInterNext, mValueNext))
				{
					//	Next item is measured.
					//	Measured, Measured.
					//	Pattern from A+C1+1 to B-C2-1.
					//	Count from B-C2 to B.
					//	First count already completed.
					//	Second count.
					mIndexEnd = GetLeftIndex(mFrameHi.FrameIndex, mElementHi);
					AddValueRange(dValueLast, mIndex, mIndexEnd - 1);
					//	Third count.
					AddLinearRange(dValueLast, dValueNext, mIndexEnd, mIndexMax);
				}
				else
				{
					//	Next item is unmeasured.
					//	Measured, Unmeasured.
					//	Pattern from A+C+1 to B.
					//	First count already completed.
					//	Second count.
					mIndexEnd = mIndexMax;
					AddPatternRange(EasingPattern(mInterNext),
						dValueLast, dValueNext, mIndex, mIndexMax);
				}
			}
			else
			{
				//	Previous item is unmeasured.
				if(IsInbound(mInterNext) && IsMeasured(mInterNext, mValueNext))
				{
					//	Next item is measured.
					//	Unmeasured, Measured.
					//	Pattern from A to B-C-1.
					//	Count from B-C to B.
					//	First count.
					if(EasingPattern(mInterPrev) != SvgAnimationEasingTypeEnum.None &&
						!IsInboundRight(mInterPrev))
					{
						mIndex = AddPatternRange(EasingPattern(mInterPrev),
							dValuePrev, dValueNext, mIndexMin,
							GetLeftIndex(mIndexMax, mElementHi) - 1);
					}
					else
					{
						//	No easing pattern. This is a single value until the
						//	count starts.
						mIndex = AddValueRange(mValuePrev, mIndexMin,
							GetLeftIndex(mIndexMax, mElementHi) - 1);
					}
					//	Second count.
					if(mInterNext != SvgAnimationInterpolationTypeEnum.Immediate)
					{
						AddPatternRange(EasingPattern(mInterNext),
							dValueLast, dValueNext, mIndex, mIndexMax);
						////	Linear easing pattern.
						//AddLinearRange(dValueLast, dValueNext, mIndex, mIndexMax);
					}
					else
					{
						//	Immediate. No pattern.
						AddValueRange(mValueNext, mIndex, mIndex);
					}
				}
				else
				{
					//	Next item is unmeasured.
					//	Unmeasured, Unmeasured.
					//	Pattern from A to B, if single pattern, otherwise:
					//	Pattern from A to A+(B-A/2)
					//	Pattern from A+(B-A/2)+1 to B.
					//	Check to see if this is a single pattern.
					//	A pattern is said to be singular if both of the endpoints have
					//	the same easing pattern.
					if(mInterPrev == SvgAnimationInterpolationTypeEnum.None ||
						mInterNext == SvgAnimationInterpolationTypeEnum.None ||
						EasingPattern(mInterPrev) == EasingPattern(mInterNext))
					{
						//	Singular pattern.
						if((EasingPattern(mInterPrev, mInterNext) ==
							SvgAnimationEasingTypeEnum.None &&
							(!IsDouble(mValuePrev) || !IsDouble(mValueNext) ||
							dValuePrev == dValueNext)) ||
							IsOutbound(mInterNext))
						{
							if(IsOutbound(mInterNext) && mInterNext !=
								SvgAnimationInterpolationTypeEnum.None)
							{
								//	Nodes are facing away. set the current value at
								//	both sides.
								mIndex = AddValueRange(mValuePrev, mIndexMin, mIndexMax);
							}
							else if(IsDouble(mValuePrev) && IsDouble(mValueNext) &&
								mInterPrev == SvgAnimationInterpolationTypeEnum.None &&
								mInterNext == SvgAnimationInterpolationTypeEnum.None)
							{
								//	Both nodes unknown. Linear gradient from left to right.
								mIndex = AddLinearRange(dValuePrev, dValueNext,
									mIndexMin, mIndexMax);
							}
							else
							{
								//	No pattern. Treated the same as immediate.
								mIndex = AddValueRange(mValuePrev, mIndexMin, mIndexMax - 1);
								AddValueRange(mValueNext, mIndexMax, mIndexMax);
							}
						}
						else
						{
							//	Easing from end to end.
							AddPatternRange(EasingPattern(mInterPrev, mInterNext),
								dValuePrev, dValueNext, mIndexMin, mIndexMax);
						}
					}
					else
					{
						//	Multiple patterns.
						//	First count.
						dValueMid = dValueNext - ((dValueNext - dValuePrev) / 2.0);
						mIndex = AddPatternCount(EasingPattern(mInterPrev),
							dValuePrev, dValueMid, mIndexMin, (mIndexMax - mIndexMin) / 2);
						//	Second count.
						AddPatternRange(EasingPattern(mInterNext),
							dValueMid, dValueNext, mIndex, mIndexMax);
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************

		//*-----------------------------------------------------------------------*
		//*	UpdateBand																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the specified band with the selected frame index.
		/// </summary>
		/// <param name="keyFrames">
		/// Reference to the collection of keyframes driving this animation.
		/// </param>
		/// <param name="band">
		/// The character / property discrete animation band to inspect.
		/// </param>
		/// <param name="frameIndex">
		/// The current frame index focus.
		/// </param>
		/// <param name="frameMax">
		/// Maximum known frame index.
		/// </param>
		/// <remarks>
		/// If the edges of the band match the keyframe edges for the current
		/// frame index, the band collection is unchanged.
		/// </remarks>
		public void UpdateBand(SvgAnimationFrameCollection keyFrames,
			SvgAnimationBandCollection band, int frameIndex, int frameMax)
		{
			//List<SvgAnimationFrameItem> frames = null;

			mFrameLo = mFrameHi = null;
			mElementLo = mElementHi = null;
			mValuePrev = mValueNext = null;

			mBand = band;
			mPropertyName = (mBand != null ? mBand.PropertyName : "");
			mKeyFrames = keyFrames;
			AssignLeftRightIndex(frameIndex, frameMax);
			//	Left and right edges are known.
			if(mBand.Count == 0 ||
				mIndexMin != mBand.Min(x => x.FrameIndex) ||
				mIndexMax != mBand.Max(x => x.FrameIndex))
			{
				//	Edges of the band don't match. Create a new series for this
				//	character / property.
				mBand.Clear();
				if(mFrameLo != null)
				{
					//	Select the left edge element and value.
					dValuePrev = 0.0;
					mValuePrev = null;
					mElementLo = mFrameLo.Elements.FirstOrDefault(x =>
						x.Properties.ContainsKey(mPropertyName));
					//	Attempt to refine the filter for left element.
					//	Optimally limit to outbound on the current frame.
					if(mElementLo != null && !IsOutbound(mElementLo.InterpolationType) &&
						mFrameLo.Elements.Exists(x =>
							x.Properties.ContainsKey(mPropertyName) &&
							IsOutbound(x.InterpolationType)))
					{
						//	Prefer an outgoing left item.
						mElementLo = mFrameLo.Elements.FirstOrDefault(x =>
							x.Properties.ContainsKey(mPropertyName) &&
							IsOutbound(x.InterpolationType));
					}
					if(mElementLo != null)
					{
						mValuePrev = mElementLo.Properties[mPropertyName];
						if(IsDouble(mValuePrev))
						{
							double.TryParse(mValuePrev.ToString(), out dValuePrev);
						}
					}
				}
				if(mFrameHi != null)
				{
					//	Select the right edge element and value.
					dValueNext = 0.0;
					mValueNext = null;
					mElementHi = mFrameHi.Elements.FirstOrDefault(x =>
						x.Properties.ContainsKey(mPropertyName));
					//	Attempt to refine the filter for right element,
					//	optimally limiting to inbound on the current frame.
					if(mElementHi != null && !IsInbound(mElementHi.InterpolationType) &&
						mFrameHi.Elements.Exists(x =>
							x.Properties.ContainsKey(mPropertyName) &&
							IsInbound(x.InterpolationType)))
					{
						//	Prefer an incoming previous item.
						mElementHi = mFrameHi.Elements.FirstOrDefault(x =>
							x.Properties.ContainsKey(mPropertyName) &&
							IsInbound(x.InterpolationType));
					}
					if(mElementHi != null)
					{
						mValueNext = mElementHi.Properties[mPropertyName];
						if(IsDouble(mValueNext))
						{
							double.TryParse(mValueNext.ToString(), out dValueNext);
						}
					}
				}
				else if(mElementLo != null)
				{
					//	The current left frame is the last known frame.
					if(IsOutbound(mElementLo.InterpolationType) &&
						IsMeasured(mElementLo.InterpolationType, mValuePrev) &&
						HasPreviousValue(mFrameLo.FrameIndex))
					{
						//	If this is an outbound counter and a previous
						//	value is known, trade this value with next and
						//	set the last-known value as previous.
						dValueNext = 0.0;
						mValueNext = mValuePrev;
						if(IsDouble(mValueNext))
						{
							double.TryParse(mValueNext.ToString(), out dValueNext);
						}
						dValuePrev = 0.0;
						mValuePrev = GetPreviousOutputValue(mFrameLo.FrameIndex);
						if(IsDouble(mValuePrev))
						{
							double.TryParse(mValuePrev.ToString(), out dValuePrev);
						}
					}
				}
				if(mFrameLo == null && mFrameHi != null)
				{
					//	The current pointer is to the left of the minimum keyframe for
					//	this character property.
					//	Build the band from zero to FrameHi.
					AddValueRange(mValueNext, mIndexMin, mIndexMax);
				}
				else if(mFrameLo != null && mFrameHi == null)
				{
					//	The current pointer is to the right of the maximum keyframe for
					//	this character property.
					//	Build the band from FrameLo to FrameMax.
					if(IsOutbound(mElementLo.InterpolationType) &&
						IsMeasured(mElementLo.InterpolationType, mValuePrev) &&
						mValueNext != null)
					{
						mIndex = AddPatternRange(
							EasingPattern(mElementLo.InterpolationType),
							dValuePrev, dValueNext, mIndexMin,
							GetRightIndex(mIndexMin, mElementLo));
						if(mIndex < mIndexMax)
						{
							//	Finish out the pattern with fill.
							AddValueRange(dValueNext, mIndex, mIndexMax);
						}
					}
					else
					{
						AddValueRange(mValuePrev, mIndexMin, mIndexMax);
					}
				}
				else if(mFrameLo != null && mFrameHi != null &&
					mElementLo != null && mElementHi != null)
				{
					//	The current pointer is situated between the lo and hi frames.
					//	In this context, the prev value is less than the current frame,
					//	except on frame zero, and the next value is always greater than
					//	or equal to the current frame.
					mInterPrev = mElementLo.InterpolationType;
					mInterNext = mElementHi.InterpolationType;
					//	Run interpolations.
					Interpolate();
				}
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
