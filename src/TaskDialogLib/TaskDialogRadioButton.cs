/***************************************************************************************************
 *
 *  Flatcode Task Dialog Library
 *  Copyright © 2014 Flatcode.net. All Rights Reserved.
 *
 *  File:
 *    TaskDialogRadioButton.cs
 *  Purpose:
 *    Task dialog radio button object.
 *  Authors:
 *    Florian Schneidereit <florian.schneidereit@outlook.com>
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a copy of this software
 *  and associated documentation files (the "Software"), to deal in the Software without
 *  restriction, including without limitation the rights to use, copy, modify, merge, publish,
 *  distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in all copies or
 *  substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
 *  BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 *  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 *  DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 *
 **************************************************************************************************/

#region Using Directives

using System;
using System.Windows;

#endregion

namespace Flatcode.Presentation
{
	/// <summary>
	/// Represents a radio button of a <see cref="TaskDialog"/>.
	/// </summary>
	public class TaskDialogRadioButton : TaskDialogButtonBase
	{
		#region Fields: Dependency Properties

		static readonly DependencyPropertyKey IsCheckedPropertyKey =
			DependencyProperty.RegisterReadOnly("IsChecked", typeof(Boolean),
												typeof(TaskDialogRadioButton),
												new PropertyMetadata(false));

		/// <summary>
		/// Identifies the <see cref="IsChecked"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty IsCheckedProperty =
			IsCheckedPropertyKey.DependencyProperty;

		#endregion

		#region Properties

		/// <summary>
		/// Determines whether this <see cref="TaskDialogRadioButton"/> is checked.
		/// </summary>
		public Boolean IsChecked {
			get { return (Boolean)GetValue(IsCheckedProperty); }
			internal set { SetValue(IsCheckedPropertyKey, value); }
		}

		#endregion

		#region Methods: Implementation

		internal override void SetButtonEnabled(bool enabled)
		{
			Int32 buttonID = Owner.RadioButtons.IndexOf(this);

			// Send TDM_ENABLE_BUTTON message to enable/disable the button
			NativeMethods.SendMessage(
				Owner.Handle, (UInt32)NativeMethods.TASKDIALOG_MESSAGES.TDM_ENABLE_RADIO_BUTTON,
				new UIntPtr((UInt32)buttonID), new IntPtr(enabled ? 1 : 0));
		}

		#endregion
	}
}
