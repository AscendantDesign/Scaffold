//	SvgAnimationUtil.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SkiaSharpSvg
{
	//*-------------------------------------------------------------------------*
	//*	SvgAnimationUtil																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Utility functionality for the SVG animation system.
	/// </summary>
	public class SvgAnimationUtil
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		private static double BounceSpeed(double energy)
		{
			//	E = 0.5m v^2, s = |sqrt(2E/m)|
			return Math.Sqrt(2d * energy);
		}
		private static double BounceTime(double height)
		{
			//	2x the half-bounce time measured at the peak.
			return 2d * Math.Sqrt(2d * height);
		}

		private static double EnergyToHeight(double energy)
		{
			return energy;		//	h = E/mg.
		}

		private static double HeightToEnergy(double height)
		{
			return height;		//	E = mgh.
		}

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* Clamp																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Clamp the caller's value to be within the allowable range.
		/// </summary>
		/// <param name="value">
		/// The proposed value.
		/// </param>
		/// <param name="lower">
		/// Lower limit.
		/// </param>
		/// <param name="upper">
		/// Upper limit.
		/// </param>
		/// <returns>
		/// The proposed value, adjusted to remain within upper and lower limits.
		/// </returns>
		public static double Clamp(double value, double lower, double upper)
		{
			return Math.Max(Math.Min(value, upper), lower);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* EaseBouncePattern																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the pattern of sample values from 1.0 to 0.0 representing a
		/// bounce.
		/// </summary>
		/// <param name="sampleCount">
		/// Number of samples to fill.
		/// </param>
		/// <param name="bounceCount">
		/// Number of bounces to execute.
		/// </param>
		/// <param name="threshold">
		/// Optional threshold. Default is 1%.
		/// </param>
		/// <returns>
		/// List of sample double prevision values between 1.0 and 0.0, where 1.0
		/// is the initial sample, the bounce physics samples occupy the middle
		/// range from 0.999999x and 0.000000x, and the 0.0 is the last sample.
		/// </returns>
		/// <remarks>
		/// Credit for bounce physics goes to William Silversmith for
		/// his article at
		/// https://medium.com/hackernoon/the-bounce-factory-3498de1e5262 and
		/// supporting demo at
		/// https://gist.githubusercontent.com/william-silversmith/
		/// cdf9f0a7fd1a3e06040b/raw/8ca0453d6d9425aa205c14cda6b89f1e03959f7b/
		/// bouncefactory.js
		/// </remarks>
		/// <see cref="https://medium.com/hackernoon/the-bounce-factory-3498de1e5262"/>
		/// <seealso cref="https://gist.githubusercontent.com/william-silversmith/cdf9f0a7fd1a3e06040b/raw/8ca0453d6d9425aa205c14cda6b89f1e03959f7b/bouncefactory.js"/>
		public static List<double> EaseBouncePattern(int sampleCount,
			int bounceCount,
			double threshold = 0.01)
		{
			double coordinate = 0.0;
			TimeEnergy bouncePoint = null;
			List<TimeEnergy> criticalPoints = new List<TimeEnergy>();
			double duration = 0.0;
			double elasticity = Math.Pow(threshold, 1.0 / bounceCount);
			double height = 1.0;
			double increment = 1.0;
			int index = 0;
			double position = 0.0;
			double potential = HeightToEnergy(height);
			List<double> result = new List<double>();
			double time = 0.0;
			double timeAdj = 0.0;
			double velocity = 0.0;

			//	The critical points are the points where the object contacts the
			//	ground. The following technique places the critical point on the
			//	timeline preceeding this arc, and assumes that the inital position
			//	of 1.0 is at the peak halfway though the first arc.
			criticalPoints.Add(new TimeEnergy()
			{
				Time = 0.0 - BounceTime(height) / 2.0,
				Energy = potential
			});
			criticalPoints.Add(new TimeEnergy()
			{
				Time = BounceTime(height) / 2.0,
				Energy = potential * elasticity
			});

			potential *= elasticity;
			height = EnergyToHeight(potential);
			time = criticalPoints[1].Time;
			for(index = 1; index < bounceCount; index++)
			{
				time += BounceTime(height);
				potential *= elasticity;	//	Remove energy after each bounce.

				criticalPoints.Add(new TimeEnergy()
				{
					Time = time,
					Energy = potential
				});
				height = EnergyToHeight(potential);
			}
			duration = time;

			//	The pattern is broken into sampleCount separate samples.
			//	Sample[0] is 1.0.
			//	Sample[sampleCount - 1] is 0.0.
			result.Add(1.0);
			if(sampleCount > 2)
			{
				increment = 1.0 / sampleCount;
				for(position = 1.0; position < (double)(sampleCount - 1); position++)
				{
					time = position * increment;
					timeAdj = time * duration;
					if(timeAdj == 0.0)
					{
						result.Add(0.0);
					}
					else if(timeAdj > duration)
					{
						result.Add(1.0);
					}
					else
					{
						//	Find the current bounce point.
						for(index = 0; index < criticalPoints.Count; index ++)
						{
							if(criticalPoints[index].Time > timeAdj)
							{
								break;
							}
						}
						bouncePoint = criticalPoints[index - 1];
						//	If bouncing from a bounce point, the time is reset due to
						//	discontinuity.
						timeAdj -= bouncePoint.Time;
						velocity = BounceSpeed(bouncePoint.Energy);
						//	Finish by projecting the current point from the most recent
						//	bounce point to the current time.
						coordinate =
							(velocity * timeAdj) + (-0.5 * Math.Pow(timeAdj, 2.0));
						result.Add(coordinate);
					}
				}
			}
			result.Add(0.0);
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* EaseLinear																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a linear easing pattern from 1.0 to 0.0.
		/// </summary>
		/// <param name="sampleCount">
		/// Count of samples to use for the linear range.
		/// </param>
		/// <returns>
		/// List of multipliers from 1.0 to 0.0.
		/// </returns>
		public static List<double> EaseLinear(int sampleCount)
		{
			double count = (double)(sampleCount - 1);
			double index = 0;
			List<double> result = new List<double>();

			if(sampleCount > 1)
			{
				for(index = 0.0; index < count; index++)
				{
					result.Add(1.0 - (index / count));
				}
			}
			result.Add(0.0);
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* EasingPattern																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the easing pattern associated with the specified interpolation
		/// pattern.
		/// </summary>
		/// <param name="interpolation">
		/// Interpolation pattern to consider.
		/// </param>
		/// <returns>
		/// The easing pattern closely associated with the specified interpolation
		/// pattern.
		/// </returns>
		public static SvgAnimationEasingTypeEnum EasingPattern(
			SvgAnimationInterpolationTypeEnum interpolation)
		{
			SvgAnimationEasingTypeEnum result = SvgAnimationEasingTypeEnum.None;

			switch(interpolation)
			{
				case SvgAnimationInterpolationTypeEnum.Bounce:
				case SvgAnimationInterpolationTypeEnum.BounceIn:
				case SvgAnimationInterpolationTypeEnum.BounceOut:
					result = SvgAnimationEasingTypeEnum.Bounce;
					break;
				case SvgAnimationInterpolationTypeEnum.CountIn:
				case SvgAnimationInterpolationTypeEnum.CountOut:
				case SvgAnimationInterpolationTypeEnum.Linear:
				case SvgAnimationInterpolationTypeEnum.LinearIn:
				case SvgAnimationInterpolationTypeEnum.LinearOut:
					result = SvgAnimationEasingTypeEnum.Linear;
					break;
				case SvgAnimationInterpolationTypeEnum.Immediate:
				default:
					result = SvgAnimationEasingTypeEnum.None;
					break;
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the easing pattern associated with the specified primary
		/// interpolation pattern.
		/// </summary>
		/// <param name="interpolation1">
		/// First interpolation pattern to consider.
		/// </param>
		/// <param name="interpolation2">
		/// First interpolation pattern to consider.
		/// </param>
		/// <returns>
		/// The easing pattern closely associated with the specified interpolation
		/// patterns.
		/// </returns>
		public static SvgAnimationEasingTypeEnum EasingPattern(
			SvgAnimationInterpolationTypeEnum interpolation1,
			SvgAnimationInterpolationTypeEnum interpolation2)
		{
			SvgAnimationEasingTypeEnum result = SvgAnimationEasingTypeEnum.None;

			if(interpolation1 == SvgAnimationInterpolationTypeEnum.None)
			{
				result = EasingPattern(interpolation2);
			}
			else
			{
				result = EasingPattern(interpolation1);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	IsBool																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the caller's value can be converted
		/// to boolean.
		/// </summary>
		/// <param name="value">
		/// </param>
		/// <returns>
		/// </returns>
		public static bool IsBool(object value)
		{
			string comparison = "";
			bool result = false;

			if(value?.ToString().Length > 0)
			{
				comparison = value.ToString().ToLower();
				switch(comparison)
				{
					case "0":
					case "1":
					case "false":
					case "true":
					case "no":
					case "yes":
						result = true;
						break;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	IsDouble																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the caller's value can be treated as
		/// a double-precision floating point number.
		/// </summary>
		/// <param name="value">
		/// </param>
		/// <returns>
		/// </returns>
		public static bool IsDouble(object value)
		{
			string comparison = "";
			bool result = false;

			if(value?.ToString().Length > 0)
			{
				comparison = value.ToString();
				if(comparison.Length == Regex.Match(comparison,
					@"^-{0,1}[0-9]*\.{0,1}[0-9]*$").Length)
				{
					result = true;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsInbound																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified interpolation type is
		/// inbound.
		/// </summary>
		/// <param name="interpolationType">
		/// Interpolation type to inspect.
		/// </param>
		/// <returns>
		/// True if the specified interpolation type is inbound. Otherwise, false.
		/// </returns>
		public static bool IsInbound(
			SvgAnimationInterpolationTypeEnum interpolationType)
		{
			bool result = false;
			switch(interpolationType)
			{
				case SvgAnimationInterpolationTypeEnum.Bounce:
				case SvgAnimationInterpolationTypeEnum.BounceIn:
				case SvgAnimationInterpolationTypeEnum.CountIn:
				case SvgAnimationInterpolationTypeEnum.Immediate:
				case SvgAnimationInterpolationTypeEnum.Linear:
				case SvgAnimationInterpolationTypeEnum.LinearIn:
				case SvgAnimationInterpolationTypeEnum.None:
					result = true;
					break;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsInboundRight																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified interpolation type is
		/// inbound to the right side only.
		/// </summary>
		/// <param name="interpolationType">
		/// Interpolation type to inspect.
		/// </param>
		/// <returns>
		/// True if the specified interpolation type is inbound. Otherwise, false.
		/// </returns>
		public static bool IsInboundRight(
			SvgAnimationInterpolationTypeEnum interpolationType)
		{
			bool result = false;
			switch(interpolationType)
			{
				case SvgAnimationInterpolationTypeEnum.BounceIn:
				case SvgAnimationInterpolationTypeEnum.CountIn:
				case SvgAnimationInterpolationTypeEnum.LinearIn:
					result = true;
					break;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	IsInt																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the caller's value can be treated as
		/// an integer.
		/// </summary>
		/// <param name="value">
		/// </param>
		/// <returns>
		/// </returns>
		public static bool IsInt(object value)
		{
			string comparison = "";
			bool result = false;

			if(value?.ToString().Length > 0)
			{
				comparison = value.ToString();
				if(comparison.Length == Regex.Match(comparison,
					@"^-{0,1}[0-9]+$").Length)
				{
					result = true;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsMeasured																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified interpolation type is
		/// measured.
		/// </summary>
		/// <param name="interpolationType">
		/// Interpolation type to inspect.
		/// </param>
		/// <param name="comparisonValue">
		/// Reference to a value to be used for measurement comparison.
		/// </param>
		/// <returns>
		/// True if the specified interpolation type is measured. Otherwise, false.
		/// </returns>
		public static bool IsMeasured(
			SvgAnimationInterpolationTypeEnum interpolationType,
			object comparisonValue)
		{
			bool result = IsDouble(comparisonValue);

			if(result)
			{
				result = false;
				switch(interpolationType)
				{
					case SvgAnimationInterpolationTypeEnum.BounceIn:
					case SvgAnimationInterpolationTypeEnum.BounceOut:
					case SvgAnimationInterpolationTypeEnum.CountIn:
					case SvgAnimationInterpolationTypeEnum.CountOut:
					case SvgAnimationInterpolationTypeEnum.LinearIn:
					case SvgAnimationInterpolationTypeEnum.LinearOut:
						result = true;
						break;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsOutbound																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified interpolation type is
		/// outbound.
		/// </summary>
		/// <param name="interpolationType">
		/// Interpolation type to inspect.
		/// </param>
		/// <returns>
		/// True if the specified interpolation type is outbound. Otherwise, false.
		/// </returns>
		public static bool IsOutbound(
			SvgAnimationInterpolationTypeEnum interpolationType)
		{
			bool result = false;
			switch(interpolationType)
			{
				case SvgAnimationInterpolationTypeEnum.BounceOut:
				case SvgAnimationInterpolationTypeEnum.CountOut:
				case SvgAnimationInterpolationTypeEnum.LinearOut:
				case SvgAnimationInterpolationTypeEnum.None:
					result = true;
					break;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	IsString																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the caller's value can be treated as
		/// a string.
		/// </summary>
		/// <param name="value">
		/// </param>
		/// <returns>
		/// </returns>
		public static bool IsString(object value)
		{
			bool result = false;

			if(value != null)
			{
				result = true;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsUnmeasured																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified interpolation type is
		/// unmeasured.
		/// </summary>
		/// <param name="interpolationType">
		/// Interpolation type to inspect.
		/// </param>
		/// <returns>
		/// True if the specified interpolation type is unmeasured. Otherwise,
		/// false.
		/// </returns>
		public static bool IsUnmeasured(
			SvgAnimationInterpolationTypeEnum interpolationType)
		{
			bool result = false;
			switch(interpolationType)
			{
				case SvgAnimationInterpolationTypeEnum.Bounce:
				case SvgAnimationInterpolationTypeEnum.Linear:
				case SvgAnimationInterpolationTypeEnum.None:
					result = true;
					break;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
