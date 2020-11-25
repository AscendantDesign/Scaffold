//	SKLine.cs
//	Copyright (c) 2017 Xamarin, Inc.
//	This file is a part of the library SkiaSharp.Extended.Svg.
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using SkiaSharp;

namespace SkiaSharpSvg
{
	internal struct SKLine
	{
		public SKLine(SKPoint p1, SKPoint p2)
		{
			P1 = p1;
			P2 = p2;
		}

		public SKPoint P1 { get; }

		public SKPoint P2 { get; }
	}
}
