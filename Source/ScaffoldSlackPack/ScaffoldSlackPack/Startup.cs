//	Startup.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using ScaffoldSlackPack.Models;

namespace ScaffoldSlackPack
{
	//*-------------------------------------------------------------------------*
	//*	Startup																																	*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Priming and configuration functionality for starting up the web service.
	/// </summary>
	public class Startup
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
		/// Create a new instance of the Startup Item.
		/// </summary>
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Configuration																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Get a reference to the configuration map for this instance.
		/// </summary>
		public IConfiguration Configuration { get; }
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Configure																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app">
		/// Application builder interface.
		/// </param>
		/// <param name="env">
		/// Web host environment interface.
		/// </param>
		/// <remarks>
		/// This method is called by the runtime.
		/// </remarks>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if(env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ConfigureServices																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Add services to the container.
		/// </summary>
		/// <remarks>
		/// This method is called by the runtime.
		/// </remarks>
		public void ConfigureServices(IServiceCollection services)
		{
			string content = System.IO.File.ReadAllText("slackserverkeys.user.json");

			//	This line uses System.Text.Json.
			services.AddControllers();
			//	This line uses Newtonsoft.Json.
			//services.AddControllers().AddNewtonsoftJson();

			ScaffoldSlackPackUtil.SlackTokens =
				JsonConvert.DeserializeObject<SlackServerKeys>(content);
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
