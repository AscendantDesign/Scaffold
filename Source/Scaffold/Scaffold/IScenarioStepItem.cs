//	IScenarioStepItem.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	IScenarioStepItem																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Non-linear view model node.
	/// </summary>
	public interface IScenarioStepItem
	{
		ScenarioStepItem Step { get; set; }
	}
	//*-------------------------------------------------------------------------*
}
