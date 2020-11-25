//	SKOval.cs
//	Copyright (c) 2017 Xamarin, Inc.
//	This file is a part of the library SkiaSharp.Extended.Svg.
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using SkiaSharp;

namespace SkiaSharpSvg
{
	internal struct SKOval
	{
		public SKOval(SKPoint center, float rx, float ry)
		{
			Center = center;
			RadiusX = rx;
			RadiusY = ry;
		}

		public SKPoint Center { get; }

		public float RadiusX { get; }

		public float RadiusY { get; }

		public SKRect BoundingRect => new SKRect(Center.X - RadiusX, Center.Y - RadiusY, Center.X + RadiusX, Center.Y + RadiusY);
	}
}
