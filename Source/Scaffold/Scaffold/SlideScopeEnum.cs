using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	SlideScopeEnum																													*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Enumeration of available slide counting scopes.
	/// </summary>
	public enum SlideScopeEnum
	{
		/// <summary>
		/// No slides defined or unknown.
		/// </summary>
		None,
		/// <summary>
		/// All slides.
		/// </summary>
		All,
		/// <summary>
		/// The currently selected slide only.
		/// </summary>
		Current,
		/// <summary>
		/// The slide indexes and ranges specified in a separate variable.
		/// </summary>
		Custom
	}
	//*-------------------------------------------------------------------------*
}
