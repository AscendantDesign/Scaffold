//	frmHTML.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CefSharp;
using CefSharp.Web;
using CefSharp.WinForms;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	frmHTML																																	*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// HTML5 form for general-duty operations.
	/// </summary>
	public partial class frmHTML : Form
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		private bool mAwaitingNavigationCompleted = true;
		private List<string> mCommands = new List<string>();
		private bool mContextLoaded = false;
		private ChromiumWebBrowser webView = null;

		//*-----------------------------------------------------------------------*
		//* webView_Dispose																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Dispose of the webView control on another thread without waiting.
		/// </summary>
		private void webView_Dispose()
		{
			if(webView != null && !webView.Disposing)
			{
				Task.Run(new Action(webView.Dispose));
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* webView_IsBrowserInitializedChanged																		*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The initialized state of the browser has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void webView_IsBrowserInitializedChanged(object sender,
			EventArgs e)
		{
			ChromiumWebBrowser b = ((ChromiumWebBrowser)sender);
			this.InvokeOnUiThreadIfRequired(() => b.Focus());
			mContextLoaded = true;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* webView_LoadingStateChanged																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The control's loading state has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Loading state changed event arguments.
		/// </param>
		private void webView_LoadingStateChanged(object sender,
			LoadingStateChangedEventArgs e)
		{
			if(!e.IsLoading)
			{
				while(mCommands.Count > 0)
				{
					webView.ExecuteScriptAsync(mCommands[0]);
					mCommands.RemoveAt(0);
				}
				mAwaitingNavigationCompleted = false;
			}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* OnFormClosed																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the FormClosed event when the form has been closed.
		/// </summary>
		/// <param name="e">
		/// Form closed event arguments.
		/// </param>
		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);
			if(webView != null && !webView.Disposing)
			{
				//webView.Dispose();
				webView_Dispose();
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* OnFormClosing																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the FormClosing event when the form is preparing to close.
		/// </summary>
		/// <param name="e">
		/// Form closing event arguments.
		/// </param>
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			this.Hide();
			base.OnFormClosing(e);
			if(!e.Cancel)
			{
				//webView.Stop();
				//webView.Visible = false;
				//webView.Dispose();
			}
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************

		//*-----------------------------------------------------------------------*
		//* _Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the frmHTML form.
		/// </summary>
		public frmHTML()
		{
			InitializeComponent();

			this.menuStrip.Renderer =
				new DarkThemeMenuRenderer(new DarkThemeMenuColorTable());

			mAwaitingNavigationCompleted = true;

			if(!DesignMode)
			{
				HtmlString htmlString =
					new HtmlString(ResourceMain.htmlDefault, true);
				webView = new ChromiumWebBrowser(htmlString);
				pnlHTML.Controls.Add(webView);
				webView.Dock = DockStyle.Fill;
				webView.IsBrowserInitializedChanged +=
					webView_IsBrowserInitializedChanged;
				webView.LoadingStateChanged += webView_LoadingStateChanged;
			}

			Text = "HTML Browser";
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* JavaScriptCommand																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Execute a JavaScript command on the client.
		/// </summary>
		/// <param name="value">
		/// JavaScript command to execute.
		/// </param>
		public void JavaScriptCommand(string value)
		{
			if(mContextLoaded)
			{
				webView.ExecuteScriptAsync(value);
			}
			else
			{
				mCommands.Add(value);
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SetBodyHTML																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set only the body portion of the HTML without modifying
		/// header, styles, and upper script.
		/// </summary>
		/// <param name="html">
		/// The body portion of the HTML.
		/// </param>
		public void SetBodyHTML(string html)
		{
			if(mContextLoaded && !mAwaitingNavigationCompleted)
			{
				webView.ExecuteScriptAsync(
					$"$('body').html(`{html}`);");
			}
			else
			{
				mCommands.Add(
				$"$('body').html(`{html}`);");
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* SetTitle																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Set the document title.
		/// </summary>
		/// <param name="title">
		/// Title text.
		/// </param>
		public void SetTitle(string title)
		{
			if(mContextLoaded && !mAwaitingNavigationCompleted)
			{
				webView.ExecuteScriptAsync(
					$"document.title = '{title}';");
			}
			else
			{
				mCommands.Add(
					$"document.title = '{title}';");
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

}
