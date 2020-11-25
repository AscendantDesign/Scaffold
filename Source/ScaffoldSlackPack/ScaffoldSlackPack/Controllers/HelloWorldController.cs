//	HelloWorldController.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ScaffoldSlackPack.Models;

namespace ScaffoldSlackPack
{
	//*-------------------------------------------------------------------------*
	//*	HelloWorldController																										*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Simple hello world controller that allows confirmation the service is
	/// available.
	/// </summary>
	[Route("HelloWorld")]
	[ApiController]
	[Produces("application/json")]
	public class HelloWorldController : ControllerBase
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
		//*	Get																																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a hello world JSON object.
		/// </summary>
		public IActionResult Get()
		{
			HttpRequest request = HttpContext.Request;

			return Ok(new HelloWorldItem(
				String.Format("{0}://{1}/{2}",
				request.Scheme, request.Host, request.PathBase)));
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*



}
