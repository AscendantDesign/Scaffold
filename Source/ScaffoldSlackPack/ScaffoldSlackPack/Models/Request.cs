﻿//	Request.cs
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
	//*	RequestItem																															*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Container for a single request.
	/// </summary>
	public class RequestItem
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
		//*	Data																																	*
		//*-----------------------------------------------------------------------*
		private string mData = "";
		/// <summary>
		/// Get/Set the data value.
		/// </summary>
		public string Data
		{
			get { return mData; }
			set { mData = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Request																																*
		//*-----------------------------------------------------------------------*
		private string mRequest = "";
		/// <summary>
		/// Get/Set the request value.
		/// </summary>
		public string Request
		{
			get { return mRequest; }
			set { mRequest = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
