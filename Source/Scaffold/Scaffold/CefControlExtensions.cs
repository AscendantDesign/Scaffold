﻿//	CefControlExtensions.cs
//	Copyright © 2010-2015 The CefSharp Authors.
//	All rights reserved. Use of this source code is governed by a
//	BSD-style license that can be found in the LICENSE file at
//	https://github.com/cefsharp/CefSharp
using System;
using System.Windows.Forms;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//* CefControlExtensions																										*
	//*-------------------------------------------------------------------------*
  /// <summary>
  /// Control extensions for CefSharp controls.
  /// </summary>
  /// <remarks>
  /// This class Copyright © 2010-2015 The CefSharp Authors. All rights
  /// reserved. Use of this source code is governed by a BSD-style license
  /// that can be found in the LICENSE file at
  /// https://github.com/cefsharp/CefSharp
  /// </remarks>
  public static class CefControlExtensions
  {
		//*-----------------------------------------------------------------------*
		//* InvokeOnUiThreadIfRequired																						*
		//*-----------------------------------------------------------------------*
    /// <summary>
    /// Executes the Action asynchronously on the UI thread, does not block
    /// execution on the calling thread.
    /// </summary>
    /// <param name="control">
    /// The control for which the update is required.
    /// </param>
    /// <param name="action">action to be performed on the control</param>
    public static void InvokeOnUiThreadIfRequired(this Control control,
      Action action)
    {
      //  If you are planning on using a similar function in your own code
      //  then please be sure to have a quick read over
      //  https://stackoverflow.com/questions/1874728/
      //  avoid-calling-invoke-when-the-control-is-disposed
      //  No action
      if(control.Disposing || control.IsDisposed || !control.IsHandleCreated)
      {
        return;
      }

      if(control.InvokeRequired)
      {
        control.BeginInvoke(action);
      }
      else
      {
        action.Invoke();
      }
    }
		//*-----------------------------------------------------------------------*
  }
	//*-------------------------------------------------------------------------*
}
