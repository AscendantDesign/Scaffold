//	HTMLControl.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

using CefSharp;
using CefSharp.WinForms;
using CefSharp.Web;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	HTMLBuffer																															*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Buffer object for receiving information from the client environment.
	/// </summary>
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	public class HTMLBuffer
	{
		//*-----------------------------------------------------------------------*
		//*	Name																																	*
		//*-----------------------------------------------------------------------*
		private string mName = "";
		/// <summary>
		/// Get/Set the name.
		/// </summary>
		public string Name
		{
			get { return mName; }
			set
			{
				bool bChanged = (mName != value);
				mName = value;
				if(bChanged)
				{
					NameChanged?.Invoke(this, new EventArgs());
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* NameChanged																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the name of this item has changed.
		/// </summary>
		public event EventHandler NameChanged;
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Value																																	*
		//*-----------------------------------------------------------------------*
		private string mValue = "";
		/// <summary>
		/// Get/Set the value.
		/// </summary>
		public string Value
		{
			get { return mValue; }
			set
			{
				bool bChanged = (mValue != value);
				mValue = value;
				if(bChanged)
				{
					ValueChanged?.Invoke(this, new EventArgs());
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ValueChanged																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Fired when the value of this item has changed.
		/// </summary>
		public event EventHandler ValueChanged;
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	HTMLControl																															*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Pre-initialized HTML5 control.
	/// </summary>
	public partial class HTMLControl : UserControl
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		private List<string> mCommands = new List<string>();
		private bool mContextLoaded = false;
		private HTMLBuffer mEmbeddedBuffer = null;
		private ChromiumWebBrowser webView = null;

		//*-----------------------------------------------------------------------*
		//* ExecuteScriptSync																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Execute the outstanding commands synchronously.
		/// </summary>
		private void ExecuteScriptSync()
		{
			while(mCommands.Count > 0)
			{
				webView.ExecuteScriptAsync(mCommands[0]);
				mCommands.RemoveAt(0);
			}
			//JavaScriptCommand(
			//	"chrome.webview.hostObjects.htmlBuffer.Value = " +
			//	"'Hello?';");
			//Debug.WriteLine(
			//	$"Embedded buffer: {mEmbeddedBuffer.Name}, {mEmbeddedBuffer.Value}");
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	mEmbeddedBuffer_ValueChanged																					*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Called when the value of the embedded buffer has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void mEmbeddedBuffer_ValueChanged(object sender, EventArgs e)
		{
			mMessages.Add(mEmbeddedBuffer.Value);
			Debug.WriteLine("HTMLControl web message received...");
			mAwaitingWebMessageReceived = false;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* webView_AddressChanged																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The active address has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Address changed event arguments.
		/// </param>
		private void webView_AddressChanged(object sender,
			AddressChangedEventArgs e)
		{
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* webView_ConsoleMessage																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A console message has been received.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Console message event arguments.
		/// </param>
		private void webView_ConsoleMessage(object sender,
			ConsoleMessageEventArgs e)
		{
			Debug.WriteLine(
				$"Line: {e.Line}, Source: {e.Source}, Message: {e.Message}");
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
			Debug.WriteLine("HTMLControl Initialized...");
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
				ExecuteScriptSync();
				mAwaitingNavigationCompleted = false;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* webView_StatusMessage																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// A status message has been received.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Status message event arguments.
		/// </param>
		private void webView_StatusMessage(object sender, StatusMessageEventArgs e)
		{
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* webView_TitleChanged																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// The title of the document has changed.
		/// </summary>
		/// <param name="sender">
		/// The object raising this event.
		/// </param>
		/// <param name="e">
		/// Title changed event arguments.
		/// </param>
		private void webView_TitleChanged(object sender, TitleChangedEventArgs e)
		{
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* webView_WebMessageReceived																						*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Raises the MessageReceived event when a message has been received from
		/// the browser control.
		/// </summary>
		/// <param name="sender">
		/// Object raising this event.
		/// </param>
		/// <param name="e">
		/// Standard event arguments.
		/// </param>
		private void webView_WebMessageReceived(object sender, EventArgs e)
		{
			Debug.WriteLine($"HTMLControl: {mMessages[mMessages.Count - 1]}");
			mAwaitingWebMessageReceived = false;
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* Dispose																																*
		//*-----------------------------------------------------------------------*
		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">
		/// True if managed resources should be disposed; otherwise, false.
		/// </param>
		protected override void Dispose(bool disposing)
		{
			if(webView != null)
			{
				webView.Dispose();
				webView = null;
			}
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
		//*-----------------------------------------------------------------------*

		//*************************************************************************
		//*	Public																																*
		//*************************************************************************

		//*-----------------------------------------------------------------------*
		//* _Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the HTMLControl.
		/// </summary>
		public HTMLControl()
		{
			InitializeComponent();

			mAwaitingNavigationCompleted = true;

			if(!DesignMode)
			{
				mEmbeddedBuffer = new HTMLBuffer();
				mEmbeddedBuffer.ValueChanged += mEmbeddedBuffer_ValueChanged;
				HtmlString htmlString = new HtmlString(
					ResourceMain.htmlSVGInteractive, true);
				webView = new ChromiumWebBrowser(htmlString);
				////	The following was the original method for embedding a control.
				//webView.RegisterJsObject("EmbeddedBuffer", mEmbeddedBuffer);
				//	The following is the 'new' method for embedding a control.
				//	The only difference between this and the async injection is that
				//	the async sets isAsync: true.
				webView.JavascriptObjectRepository.Register(
					name: "embeddedBuffer", objectToBind: mEmbeddedBuffer,
					isAsync: false, options: BindingOptions.DefaultBinder);

				this.Controls.Add(webView);
				webView.Dock = DockStyle.Fill;

				webView.IsBrowserInitializedChanged +=
					webView_IsBrowserInitializedChanged;
				webView.LoadingStateChanged += webView_LoadingStateChanged;
				webView.ConsoleMessage += webView_ConsoleMessage;
				webView.StatusMessage += webView_StatusMessage;
				webView.TitleChanged += webView_TitleChanged;
				webView.AddressChanged += webView_AddressChanged;

			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AwaitingNavigationCompleted																						*
		//*-----------------------------------------------------------------------*
		private bool mAwaitingNavigationCompleted = false;
		/// <summary>
		/// Get/Set a value indicating whether the control is currently awaiting
		/// the NavigationCompleted event.
		/// </summary>
		public bool AwaitingNavigationCompleted
		{
			get { return mAwaitingNavigationCompleted; }
			set { mAwaitingNavigationCompleted = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AwaitingWebMessageReceived																						*
		//*-----------------------------------------------------------------------*
		private bool mAwaitingWebMessageReceived = false;
		/// <summary>
		/// Get/Set a value indicating whether the host is waiting for a web
		/// message to be received.
		/// </summary>
		public bool AwaitingWebMessageReceived
		{
			get { return mAwaitingWebMessageReceived; }
			set { mAwaitingWebMessageReceived = value; }
		}
		//*-----------------------------------------------------------------------*

		//	DrawToBitmap delivers only a small, black square.
		////*-----------------------------------------------------------------------*
		////* DrawToBitmap																													*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Draw the current browser window to the caller-provided bitmap.
		///// </summary>
		///// <param name="bitmap">
		///// Reference to a pre-constructed bitmap to which the window will be
		///// drawn.
		///// </param>
		//public void DrawToBitmap(Bitmap bitmap)
		//{
		//	if(bitmap != null)
		//	{
		//		webView.DrawToBitmap(bitmap,
		//			new Rectangle(0, 0, bitmap.Width, bitmap.Height));
		//	}
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetInnerControl																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a reference to the inner webview control.
		/// </summary>
		/// <returns>
		/// Reference to the inner webview control.
		/// </returns>
		public ChromiumWebBrowser GetInnerControl()
		{
			return webView;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	GetWebMessage																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Wait for the next available web message to be received.
		/// </summary>
		/// <param name="namedValue">
		/// Reference to a token name / value combination for which the Value
		/// property will be loaded with the next available message from the
		/// client control.
		/// </param>
		public async Task GetWebMessage(NameValueItem namedValue)
		{
			int index = 0;
			int maxCount = 10;

			while(index < maxCount && mAwaitingWebMessageReceived)
			{
				await Task.Delay(1000);
				index++;
			}
			if(!mAwaitingWebMessageReceived && mMessages.Count > 0)
			{
				if(namedValue != null)
				{
					namedValue.Name = "Message";
					namedValue.Value = mMessages[0];
					mMessages.RemoveAt(0);
				}
			}
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
		//*	Messages																															*
		//*-----------------------------------------------------------------------*
		private List<string> mMessages = new List<string>();
		/// <summary>
		/// Get a reference to the collection of messages received from the client.
		/// </summary>
		public List<string> Messages
		{
			get { return mMessages; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Navigate																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Navigate to the specified URL.
		/// </summary>
		/// <param name="url">
		/// URL to navigate to.
		/// </param>
		/// <returns>
		/// True if the the page navigation was complete before returning.
		/// Otherwise, false.
		/// </returns>
		public async Task Navigate(string url)
		{
			int index = 0;
			int maxCount = 10;

			mAwaitingNavigationCompleted = true;
			webView.Load(url);
			while(index < maxCount && mAwaitingNavigationCompleted)
			{
				await Task.Delay(1000);
				index++;
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* NavigateAsync																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Navigate to the specified URL.
		/// </summary>
		/// <param name="url">
		/// URL to navigate to.
		/// </param>
		/// <returns>
		/// True if the the page navigation was complete before returning.
		/// Otherwise, false.
		/// </returns>
		public void NavigateAsync(string url)
		{
			mAwaitingNavigationCompleted = true;
			webView.Load(url);
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NavigateToString																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Supply the HTML content for the document and render it, waiting for
		/// the page load to be complete before returning.
		/// </summary>
		/// <param name="htmlContent">
		/// Content to load.
		/// </param>
		/// <returns>
		/// True if the the page navigation was complete before returning.
		/// Otherwise, false.
		/// </returns>
		public async Task NavigateToString(string htmlContent)
		{
			int index = 0;
			int maxCount = 10;

			mAwaitingNavigationCompleted = true;
			//webView.LoadHtml(htmlContent, "www.example.com");
			webView.LoadHtml(htmlContent, true);
			while(index < maxCount && mAwaitingNavigationCompleted)
			{
				await Task.Delay(1000);
				index++;
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
			if(mContextLoaded)
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
			if(mContextLoaded)
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
