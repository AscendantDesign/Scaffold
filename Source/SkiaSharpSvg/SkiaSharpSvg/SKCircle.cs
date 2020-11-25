//	SKCircle.cs
//	Copyright (c) 2017 Xamarin, Inc.
//	This file is a part of the library SkiaSharp.Extended.Svg.
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using SkiaSharp;

namespace SkiaSharpSvg
{
	internal struct SKCircle
	{
		public SKCircle(SKPoint center, float radius)
		{
			Center = center;
			Radius = radius;
		}

		public SKPoint Center { get; }

		public float Radius { get; }
	}
}
