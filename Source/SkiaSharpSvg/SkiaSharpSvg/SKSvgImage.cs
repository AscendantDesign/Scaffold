//	SKSvgImage.cs
//	Copyright (c) 2017 Xamarin, Inc.
//	This file is a part of the library SkiaSharp.Extended.Svg.
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using SkiaSharp;

namespace SkiaSharpSvg
{
	internal struct SKSvgImage
	{
		public SKSvgImage(SKRect rect, string uri, byte[] bytes = null)
		{
			Rect = rect;
			Uri = uri;
			Bytes = bytes;
		}

		public SKRect Rect { get; }

		public string Uri { get; }

		public byte[] Bytes { get; }
	}
}
