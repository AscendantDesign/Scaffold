//	DisplayLogController.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using static ScaffoldSlackPack.ScaffoldSlackPackUtil;

namespace ScaffoldSlackPack.Controllers
{
	//*-------------------------------------------------------------------------*
	//*	DisplayLogController																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Log display and interaction functionality.
	/// </summary>
	[Route("Log")]
	[ApiController]
	public class DisplayLogController : ControllerBase
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
		//*	Clear																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Clear the log.
		/// </summary>
		[Route("Clear")]
		public IActionResult Clear()
		{
			ClearLog();
			return Ok();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Display																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the content of the log as clear text.
		/// </summary>
		[Route("Display")]
		public ContentResult Display()
		{
			return Content(ReadLog());
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*


}
