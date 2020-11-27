using System;
using System.Collections.Generic;
using System.Text;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	MediaTypeEnum																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Enumeration of possible media types.
	/// </summary>
	public enum MediaTypeEnum
	{
		/// <summary>
		/// Media type unknown or not defined.
		/// </summary>
		None = 0,
		/// <summary>
		/// Audio resource.
		/// </summary>
		Audio,
		/// <summary>
		/// Image resource.
		/// </summary>
		Image,
		/// <summary>
		/// Link resource.
		/// </summary>
		Link,
		/// <summary>
		/// Video resource.
		/// </summary>
		Video
	}
	//*-------------------------------------------------------------------------*
}
