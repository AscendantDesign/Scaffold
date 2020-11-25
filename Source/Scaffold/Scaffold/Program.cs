//	Program.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

using CefSharp;
using CefSharp.WinForms;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	Program																																	*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Main application entry point.
	/// </summary>
	static class Program
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		////*-----------------------------------------------------------------------*
		////* OnAssemblyResolve																											*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Called when assemblies need to be resolved.
		///// </summary>
		///// <param name="sender">
		///// The object raising this event.
		///// </param>
		///// <param name="args">
		///// Resolve event arguments.
		///// </param>
		///// <returns>
		///// Reference to the found assembly.
		///// </returns>
		///// <remarks>
		///// Credit for this method goes to
		///// https://stackoverflow.com/questions/52394485/
		///// how-to-get-cefsharp-to-work-with-configuration-
		///// anycpu-in-vs-common-library
		///// </remarks>
		//private static Assembly OnAssemblyResolve(
		//	object sender, ResolveEventArgs args)
		//{
		//	if(args.Name.StartsWith("CefSharp"))
		//	{
		//		string assemblyName = args.Name.Split(new[] { ',' }, 2)[0] + ".dll";
		//		string architectureSpecificPath =
		//			Path.Combine(
		//				AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
		//				Environment.Is64BitProcess ? "x64" : "x86",
		//				assemblyName);

		//		return File.Exists(architectureSpecificPath)
		//				? Assembly.LoadFile(architectureSpecificPath)
		//				: null;
		//	}
		//	return null;
		//}
		////*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Main																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Main entry point for the application.
		/// </summary>
		[STAThread]
		public static void Main(string[] args)
		{
			////AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//CefSettings cefsettings = new CefSettings()
			//{
			//	BrowserSubprocessPath = Path.Combine(
			//		AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
			//		Environment.Is64BitProcess ? "x64" : "x86",
			//		"CefSharp.BrowserSubprocess.exe")
			//};
			//Cef.Initialize(cefsettings);
			Cef.EnableHighDPISupport();
			//	The following settings are added to support new style of embedded
			//	.NET controls in the browser.
			CefSharpSettings.LegacyJavascriptBindingEnabled = true;
			CefSharpSettings.WcfEnabled = true;
			foreach(string arg in args)
			{
				if(arg.ToLower() == "/local")
				{
					frmMain.NetworkLocalMode = true;
				}
			}
			Application.Run(new frmMain());
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*


}
