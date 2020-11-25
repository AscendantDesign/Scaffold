Scaffold Project Source
=======================

Each of the following subfolders contains a separate related project that contributes to the entire effort.

 - [**Scaffold**](Scaffold). The Scaffold desktop application.
 - [**ScaffoldNodes**](ScaffoldNodes). Shared class library containing node object handling.
 - [**ScaffoldSlackPack**](ScaffoldSlackPack). Web server for communicating with Slack app and Slack bot.
 - [**SkiaSharpSvg**](SkiaSharpSvg). Dedicated SVG renderer for SkiaSharp and select portions of [SkiaSharp.Extended.Svg by Xamarin](https://github.com/mono/SkiaSharp.Extended).
 - [**SvgAnimation**](SvgAnimation). Dedicated animation library for SkiaSharp.<br /><br />

## Dependencies
Following are the inter-solution dependencies.

 - Scaffold.
   - ScaffoldNodes.
   - SkiaSharpSvg.
   - SvgAnimation.<br /><br />

 - ScaffoldSlackPack.
   - ScaffoldNodes.

For NuGet and other external dependencies, see [Scaffold project dependency graph](https://github.com/AscendantDesign/Scaffold/network/dependencies).

