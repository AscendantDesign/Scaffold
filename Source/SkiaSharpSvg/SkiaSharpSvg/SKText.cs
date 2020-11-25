//	SKText.cs
//	Copyright (c) 2017 Xamarin, Inc.
//	This file is a part of the library SkiaSharp.Extended.Svg.
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using SkiaSharp;

namespace SkiaSharpSvg
{
	internal class SKText : IEnumerable<SKTextSpan>
	{
		private readonly List<SKTextSpan> spans = new List<SKTextSpan>();

		public SKText(SKPoint location, SKTextAlign textAlign)
		{
			Location = location;
			TextAlign = textAlign;
		}

		public void Append(SKTextSpan span)
		{
			spans.Add(span);
		}

		public SKPoint Location { get; }

		public SKTextAlign TextAlign { get; }

		public float MeasureTextWidth() => spans.Sum(x => x.MeasureTextWidth());

		public IEnumerator<SKTextSpan> GetEnumerator() => spans.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
