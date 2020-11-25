//	MessageEvent.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	MessageEventArgs																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Event arguments for handling an inter process message.
	/// </summary>
	public class MessageEventArgs
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
		/// Create a new instance of the MessageEventArgs Item.
		/// </summary>
		public MessageEventArgs()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the MessageEventArgs Item.
		/// </summary>
		/// <param name="text">
		/// The text of the message.
		/// </param>
		public MessageEventArgs(string text)
		{
			mText = text;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Text																																	*
		//*-----------------------------------------------------------------------*
		private string mText = "";
		/// <summary>
		/// Get/Set the text of the message.
		/// </summary>
		public string Text
		{
			get { return mText; }
			set { mText = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//* MessageEventHandler																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Message event handler.
	/// </summary>
	/// <param name="sender">
	/// The object raising the event.
	/// </param>
	/// <param name="e">
	/// Message event arguments.
	/// </param>
	public delegate void MessageEventHandler(object sender, MessageEventArgs e);
	//*-------------------------------------------------------------------------*

}
