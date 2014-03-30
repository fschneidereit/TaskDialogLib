/***************************************************************************************************
 *
 *  TaskDialog Library
 *  Copyright © 2014 Florian Schneidereit. All Rights Reserved.
 *
 *  File:
 *    TaskDialogResult.cs
 *  Purpose:
 *    Task dialog result structure.
 *  Authors:
 *    Florian Schneidereit <florian.schneidereit@outlook.com>
 *
 *  This library is free software; you can redistribute it and/or modify it under the terms of
 *  the GNU Lesser General Public License as published by the Free Software Foundation; either
 *  version 2.1 of the License, or (at your option) any later version.
 *
 *  This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 *  without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 *  See the GNU Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public License along with this
 *  library; if not, write to the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
 *  Boston, MA  02110-1301  USA
 *
 **************************************************************************************************/

#region Using Directives

using System;
using System.Runtime.InteropServices;

#endregion

namespace TaskDialogLib
{
	/// <summary>
	/// Represents the result of a <see cref="TaskDialog"/>.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct TaskDialogResult
	{
		internal static readonly TaskDialogResult Empty;

		#region Fields

		readonly Int32 selectedButton;
		readonly Int32 selectedRadioButton;
		readonly Boolean verificationChecked;

		#endregion

		#region Constructors

		internal TaskDialogResult(Int32 selectedButton) :
			this(selectedButton, 0, false)
		{
		}

		internal TaskDialogResult(Int32 selectedButton, Int32 selectedRadioButton) :
			this(selectedButton, selectedRadioButton, false)
		{
		}

		internal TaskDialogResult(Int32 selectedButton, Int32 selectedRadioButton,
			                      Boolean verificationChecked)
		{
			// Initialize instance
			this.selectedButton = selectedButton;
			this.selectedRadioButton = selectedRadioButton;
			this.verificationChecked = verificationChecked;
		}

		#endregion

		#region Properties

		/// <summary>
		/// 
		/// </summary>
		public Int32 SelectedButton {
			get { return selectedButton; }
		}

		/// <summary>
		/// 
		/// </summary>
		public Int32 SelectedRadioButton {
			get { return selectedRadioButton; }
		}

		/// <summary>
		/// 
		/// </summary>
		public Boolean VerificationChecked {
			get { return verificationChecked; }
		}

		#endregion

		#region Methods: Overridden

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public override String ToString()
		{
			return String.Format("TaskDialogResult {{ SelectedButton = {0}, " +
								 "SelectedRadioButton = {1}, VerificationChecked = {2} }}",
								 SelectedButton, SelectedRadioButton, VerificationChecked);
		}

		#endregion
	}
}
