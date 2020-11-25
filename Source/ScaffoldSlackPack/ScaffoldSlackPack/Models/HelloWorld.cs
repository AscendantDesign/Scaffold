//	HelloWorld.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScaffoldSlackPack.Models
{
	//*-------------------------------------------------------------------------*
	//*	HelloWorldItem																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Simple hello world object.
	/// </summary>
	public class HelloWorldItem
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the HelloWorldItem Item.
		/// </summary>
		public HelloWorldItem()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the HelloWorldItem Item.
		/// </summary>
		public HelloWorldItem(string basePath)
		{
			mHello = $"World from {basePath}";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Hello																																	*
		//*-----------------------------------------------------------------------*
		private string mHello = "World";
		/// <summary>
		/// Get the value of hello, which is always world.
		/// </summary>
		public string Hello
		{
			get
			{
				return mHello;
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
