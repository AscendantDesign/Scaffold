//	SvgAnimationCharacterTrack.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Schema;
using static SkiaSharpSvg.SvgAnimationUtil;

namespace SkiaSharpSvg
{
	//*-------------------------------------------------------------------------*
	//*	SvgAnimationCharacterTrackCollection																		*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of SvgAnimationCharacterTrackItem Items.
	/// </summary>
	public class SvgAnimationCharacterTrackCollection :
		List<SvgAnimationCharacterTrackItem>
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
		/// Create a new instance of the SvgAnimationCharacterTrackCollection Item.
		/// </summary>
		public SvgAnimationCharacterTrackCollection()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the SvgAnimationCharacterTrackCollection Item.
		/// </summary>
		/// <param name="sourceFrames">
		/// </param>
		public SvgAnimationCharacterTrackCollection(
			SvgAnimationFrameCollection sourceFrames)
		{
			SvgAnimationFrameItem targetFrame = null;
			SvgAnimationCharacterTrackItem tracker = null;

			if(sourceFrames != null)
			{
				mLastFrameIndex = sourceFrames.Max(x => x.FrameIndex);
				foreach(SvgAnimationFrameItem sourceFrame in sourceFrames)
				{
					foreach(SvgAnimationFrameElementItem sourceElement in
						sourceFrame.Elements)
					{
						if(tracker == null ||
							tracker.CharacterName != sourceElement.ElementID)
						{
							tracker = this.FirstOrDefault(x =>
								x.CharacterName == sourceElement.ElementID);
							if(tracker == null)
							{
								//	Create new tracker and frame.
								tracker = new SvgAnimationCharacterTrackItem()
								{
									CharacterName = sourceElement.ElementID
								};
								this.Add(tracker);
								targetFrame = new SvgAnimationFrameItem()
								{
									FrameIndex = sourceFrame.FrameIndex
								};
								tracker.Frames.Add(targetFrame);
								targetFrame.Elements.Add(sourceElement);
							}
							else
							{
								//	Existing tracker found. Lookup frame.
								//	Add the new element.
								targetFrame = tracker.Frames.FirstOrDefault(x =>
									x.FrameIndex == sourceFrame.FrameIndex);
								if(targetFrame == null)
								{
									targetFrame = new SvgAnimationFrameItem()
									{
										FrameIndex = sourceFrame.FrameIndex
									};
									tracker.Frames.Add(targetFrame);
								}
								targetFrame.Elements.Add(sourceElement);
							}
						}
						else
						{
							//	Same character and tracker as prior.
							//	New element. Possibly new frame.
							if(targetFrame == null ||
								targetFrame.FrameIndex != sourceFrame.FrameIndex)
							{
								targetFrame = tracker.Frames.FirstOrDefault(x =>
									x.FrameIndex == sourceFrame.FrameIndex);
							}
							if(targetFrame == null)
							{
								targetFrame = new SvgAnimationFrameItem()
								{
									FrameIndex = sourceFrame.FrameIndex
								};
								tracker.Frames.Add(targetFrame);
							}
							targetFrame.Elements.Add(sourceElement);
						}
						//	Properties are already added as a child of element.
					}
				}
			}
			//	Initialize property bands.
			foreach(SvgAnimationCharacterTrackItem character in this)
			{
				foreach(SvgAnimationFrameItem frame in character.Frames)
				{
					foreach(SvgAnimationFrameElementItem element in frame.Elements)
					{
						foreach(KeyValuePair<string, object> property in
							element.Properties)
						{
							if(!character.PropertyBands.Exists(x =>
								x.PropertyName == property.Key))
							{
								character.PropertyBands.Add(new SvgAnimationBandCollection()
								{
									PropertyName = property.Key
								});
							}
							if(!character.HasRotation && property.Key == "rotation")
							{
								character.HasRotation = true;
							}
						}
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	FirstFrameIndex																												*
		//*-----------------------------------------------------------------------*
		private int mFirstFrameIndex = 0;
		/// <summary>
		/// Get/Set the frame index of the first known frame.
		/// </summary>
		public int FirstFrameIndex
		{
			get { return mFirstFrameIndex; }
			set { mFirstFrameIndex = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	LastFrameIndex																												*
		//*-----------------------------------------------------------------------*
		private int mLastFrameIndex = 0;
		/// <summary>
		/// Get/Set the frame index of the last known frame.
		/// </summary>
		public int LastFrameIndex
		{
			get { return mLastFrameIndex; }
			set { mLastFrameIndex = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SetFrameIndex																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the current frame index, calculating current tweening bands within
		/// range.
		/// </summary>
		public void SetFrameIndex(int frameIndex)
		{
			foreach(SvgAnimationCharacterTrackItem track in this)
			{
				track.UpdateBand(frameIndex, mFirstFrameIndex, mLastFrameIndex);
			}
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	SvgAnimationCharacterTrackItem																					*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about a character / property combination with its associated
	/// frame information associated.
	/// </summary>
	public class SvgAnimationCharacterTrackItem
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
		//*	CharacterName																													*
		//*-----------------------------------------------------------------------*
		private string mCharacterName = "";
		/// <summary>
		/// Get/Set the name of the character.
		/// </summary>
		public string CharacterName
		{
			get { return mCharacterName; }
			set { mCharacterName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Frames																																*
		//*-----------------------------------------------------------------------*
		private SvgAnimationFrameCollection mFrames =
			new SvgAnimationFrameCollection();
		/// <summary>
		/// Get a reference to the collection of frames referencing this character.
		/// </summary>
		public SvgAnimationFrameCollection Frames
		{
			get { return mFrames; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetPropertiesAtFrame																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the list of properties at the specified frame index.
		/// </summary>
		/// <param name="frameIndex">
		/// The frame index for which to report the current property values.
		/// </param>
		/// <remarks>
		/// This method assumes that the property bands have been updated around
		/// the specified frame index.
		/// </remarks>
		public Dictionary<string, object> GetPropertiesAtFrame(int frameIndex)
		{
			Dictionary<string, object> properties = new Dictionary<string, object>();

			foreach(SvgAnimationBandCollection bands in mPropertyBands)
			{
				//	Each property.
				foreach(SvgAnimationBandItem band in bands)
				{
					//	Each value per frame.
					if(band.FrameIndex == frameIndex)
					{
						//	The property was found for the specified frame index.
						properties.Add(bands.PropertyName, band.PropertyValue);
						break;
					}
				}
			}
			foreach(KeyValuePair<string, object> property in properties)
			{
				if(mMonitoredProperties.Contains(property.Key))
				{
					//	Monitored property. Set the last known value.
					SetMonitoredPropertyValue(property.Key, property.Value);
				}
			}
			return properties;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetPropertiesImplied																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the list of implied property values at the specified frame
		/// index.
		/// </summary>
		/// <param name="frameIndex">
		/// The frame index for which to report the current property values.
		/// </param>
		/// <remarks>
		/// This method assumes that the property bands have been updated around
		/// the specified frame index. If a property assignment doesn't exist
		/// at this frame, the last-known assignment is used.
		/// </remarks>
		public Dictionary<string, object> GetPropertiesImplied(int frameIndex)
		{
			Dictionary<string, object> properties = new Dictionary<string, object>();

			foreach(SvgAnimationBandCollection bands in mPropertyBands)
			{
				//	Each property.
				foreach(SvgAnimationBandItem band in bands)
				{
					//	Each value per frame.
					if(band.FrameIndex <= frameIndex)
					{
						//	If the property was found for the specified frame
						//	index or earlier, then that value is added.
						if(properties.ContainsKey(bands.PropertyName))
						{
							properties[bands.PropertyName] = band.PropertyValue;
						}
						else
						{
							properties.Add(bands.PropertyName, band.PropertyValue);
						}
						break;
					}
				}
			}
			foreach(KeyValuePair<string, object> property in properties)
			{
				if(mMonitoredProperties.Contains(property.Key))
				{
					//	Monitored property. Set the last known value.
					SetMonitoredPropertyValue(property.Key, property.Value);
				}
			}
			return properties;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	HasRotation																														*
		//*-----------------------------------------------------------------------*
		private bool mHasRotation = false;
		/// <summary>
		/// Get/Set a value indicating whether the character has rotation
		/// specification.
		/// </summary>
		/// <remarks>
		/// Because SVG has no specific rotation property and the transform rotate
		/// method also requires the center point of rotation and the center point
		/// is not updated implicitly when the x, y, width, or height values are
		/// changed, the transform=rotate(r cx cy) value must be updated any time
		/// the character is moved or scaled, in addition to having its own
		/// value changed independently at any time.
		/// </remarks>
		public bool HasRotation
		{
			get { return mHasRotation; }
			set { mHasRotation = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	LastHeight																														*
		//*-----------------------------------------------------------------------*
		private double mLastHeight = 0.0;
		/// <summary>
		/// Get/Set the last known value of the height property.
		/// </summary>
		public double LastHeight
		{
			get { return mLastHeight; }
			set { mLastHeight = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	LastRotation																													*
		//*-----------------------------------------------------------------------*
		private double mLastRotation = 0.0;
		/// <summary>
		/// Get/Set the last known value of the rotation property.
		/// </summary>
		public double LastRotation
		{
			get { return mLastRotation; }
			set { mLastRotation = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	LastWidth																															*
		//*-----------------------------------------------------------------------*
		private double mLastWidth = 0.0;
		/// <summary>
		/// Get/Set the last known value of the width property.
		/// </summary>
		public double LastWidth
		{
			get { return mLastWidth; }
			set { mLastWidth = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	LastX																																	*
		//*-----------------------------------------------------------------------*
		private double mLastX = 0.0;
		/// <summary>
		/// Get/Set the last known value of the x property.
		/// </summary>
		public double LastX
		{
			get { return mLastX; }
			set { mLastX = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	LastY																																	*
		//*-----------------------------------------------------------------------*
		private double mLastY = 0.0;
		/// <summary>
		/// Get/Set the last known value of the y property.
		/// </summary>
		public double LastY
		{
			get { return mLastY; }
			set { mLastY = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MonitoredProperties																										*
		//*-----------------------------------------------------------------------*
		private string[] mMonitoredProperties =
			new string[] { "x", "y", "width", "height", "rotation" };
		/// <summary>
		/// Get/Set a reference to the array of monitored property names for this
		/// character instance.
		/// </summary>
		public string[] MonitoredProperties
		{
			get { return mMonitoredProperties; }
			set { mMonitoredProperties = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	PropertyBands																													*
		//*-----------------------------------------------------------------------*
		private SvgAnimationBandCatalog mPropertyBands =
			new SvgAnimationBandCatalog();
		/// <summary>
		/// Get a reference to the collection of per-frame property name / value
		/// bands currently resolved.
		/// </summary>
		public SvgAnimationBandCatalog PropertyBands
		{
			get { return mPropertyBands; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	SetMonitoredPropertyValue																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the value of the specified monitored property, if handled.
		/// </summary>
		/// <param name="propertyName">
		/// Name of the property to set.
		/// </param>
		/// <param name="propertyValue">
		/// Value of the property.
		/// </param>
		public void SetMonitoredPropertyValue(string propertyName,
			object propertyValue)
		{
			switch(propertyName)
			{
				case "height":
					if(IsDouble(propertyValue))
					{
						double.TryParse(propertyValue.ToString(), out mLastHeight);
					}
					break;
				case "rotation":
					if(IsDouble(propertyValue))
					{
						double.TryParse(propertyValue.ToString(), out mLastRotation);
					}
					break;
				case "width":
					if(IsDouble(propertyValue))
					{
						double.TryParse(propertyValue.ToString(), out mLastWidth);
					}
					break;
				case "x":
					if(IsDouble(propertyValue))
					{
						double.TryParse(propertyValue.ToString(), out mLastX);
					}
					break;
				case "y":
					if(IsDouble(propertyValue))
					{
						double.TryParse(propertyValue.ToString(), out mLastY);
					}
					break;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	UpdateBand																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update the tweening band for properties on this character according to
		/// the current frame index.
		/// </summary>
		/// <param name="frameIndex">
		/// </param>
		/// <param name="frameMin">
		/// </param>
		/// <param name="frameMax">
		/// </param>
		/// <remarks>
		/// Each property on each character is independent. When the element is
		/// to the left side of the frame, 'out' counting values are preferred,
		/// followed by any other. When the element is to the right side of the
		/// frame, 'in' counting values are preferred, followed by all others.
		/// </remarks>
		public void UpdateBand(int frameIndex, int frameMin, int frameMax)
		{
			SvgAnimationResolver resolver = new SvgAnimationResolver();

			foreach(SvgAnimationBandCollection band in mPropertyBands)
			{
				resolver.UpdateBand(mFrames, band, frameIndex, frameMax);
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
