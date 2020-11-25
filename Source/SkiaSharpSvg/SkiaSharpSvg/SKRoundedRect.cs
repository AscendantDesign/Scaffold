//	SKRoundedRect.cs
//	Copyright (c) 2017 Xamarin, Inc.
//	This file is a part of the library SkiaSharp.Extended.Svg.
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using SkiaSharp;

namespace SkiaSharpSvg
{
	internal struct SKRoundedRect
	{
		public SKRoundedRect(SKRect rect, float rx, float ry)
		{
			Rect = rect;
			RadiusX = rx;
			RadiusY = ry;
		}

		public SKRect Rect { get; }

		public float RadiusX { get; }

		public float RadiusY { get; }

		public bool IsRounded => RadiusX > 0 || RadiusY > 0;
	}
}
