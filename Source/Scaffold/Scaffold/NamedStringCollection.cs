//	NamedStringCollection.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	NamedStringCatalog																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of NamedStringCollection Items.
	/// </summary>
	public class NamedStringCatalog : List<NamedStringCollection>
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
		//*	_Indexer																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get the item with the specified name from within the collection.
		/// </summary>
		public NamedStringCollection this[string name]
		{
			get
			{
				NamedStringCollection result =
					this.FirstOrDefault(x => x.Name == name);
				return result;
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	NamedStringCollection																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of string Items.
	/// </summary>
	public class NamedStringCollection : List<string>
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
		//*	Name																																	*
		//*-----------------------------------------------------------------------*
		private string mName = "";
		/// <summary>
		/// Get/Set the name of this list.
		/// </summary>
		public string Name
		{
			get { return mName; }
			set { mName = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ToString																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the string representation of this list.
		/// </summary>
		public override string ToString()
		{
			StringBuilder result = new StringBuilder();

			foreach(string item in this)
			{
				if(result.Length > 0 && item.Length > 0)
				{
					result.Append("; ");
				}
				result.Append(item);
			}
			return result.ToString();
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
