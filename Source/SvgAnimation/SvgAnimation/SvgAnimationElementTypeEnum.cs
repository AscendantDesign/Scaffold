//	SvgAnimationElementTypeEnum.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharpSvg
{
	//*-------------------------------------------------------------------------*
	//*	SvgAnimationElementTypeEnum																							*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Enumeration of possible element types.
	/// </summary>
	public enum SvgAnimationElementTypeEnum
	{
		/// <summary>
		/// Element type undefined or unknown.
		/// </summary>
		None = 0,
		/// <summary>
		/// Element is a character within the source file.
		/// </summary>
		Character,
		/// <summary>
		/// Element is a layer within the source file.
		/// </summary>
		Layer,
		/// <summary>
		/// Element is a mark in a scene within the local Scenes collection.
		/// </summary>
		SceneMark
	}
	//*-------------------------------------------------------------------------*
}
