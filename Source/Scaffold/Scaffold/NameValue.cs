//	NameValue.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	NameValue																																*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Generic name and value.
	/// </summary>
	/// <typeparam name="T">
	/// Commonly used or primitive type.
	/// </typeparam>
	public class NameValue<T>
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
		/// Create a new instance of the NameValue Item.
		/// </summary>
		public NameValue()
		{
			Type type = typeof(T);

			switch(type.Name.ToLower())
			{
				case "byte":
				case "double":
				case "int":
				case "int32":
				case "int64":
				case "float":
				case "single":
					mValue = (T)Convert.ChangeType(0, typeof(T));
					break;
				case "char":
					mValue = (T)Convert.ChangeType('\0', typeof(T));
					break;
				case "string":
					mValue = (T)Convert.ChangeType("", typeof(T));
					break;
			}

		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the NameValue Item.
		/// </summary>
		/// <param name="name">
		/// Name of the entry.
		/// </param>
		/// <param name="value">
		/// Value of the entry.
		/// </param>
		public NameValue(string name, T value)
		{
			mName = name;
			mValue = value;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Name																																	*
		//*-----------------------------------------------------------------------*
		private string mName = "";
		/// <summary>
		/// Get/Set the name of this item.
		/// </summary>
		public string Name
		{
			get { return mName; }
			set { mName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Value																																	*
		//*-----------------------------------------------------------------------*
		private T mValue;
		/// <summary>
		/// Get/Set the value of this object.
		/// </summary>
		public T Value
		{
			get { return mValue; }
			set { mValue = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*
}
