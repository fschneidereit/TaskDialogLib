/***************************************************************************************************
 *
 *  Flatcode Task Dialog Library
 *  Copyright © 2014 Flatcode.net. All Rights Reserved.
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

namespace Flatcode.Presentation
{
	/// <summary>
	/// Represents the result of a <see cref="TaskDialog"/> user interaction.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct TaskDialogResult : IEquatable<TaskDialogResult>
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
		/// Gets either the zero-based index of the <see cref="TaskDialogButton"/> or a value that
		/// indicates which <see cref="TaskDialogButtons"/> common button was selected.
		/// </summary>
		public Int32 SelectedButton {
			get { return selectedButton; }
		}

		/// <summary>
		/// Gets the zero-based index of the <see cref="TaskDialogRadioButton"/> that was selected.
		/// </summary>
		public Int32 SelectedRadioButton {
			get { return selectedRadioButton; }
		}

		/// <summary>
		/// Determines whether the verification checkbox of the <see cref="TaskDialog"/> was in a
		/// checked state.
		/// </summary>
		public Boolean VerificationChecked {
			get { return verificationChecked; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Determines whether the specified <see cref="TaskDialogResult"/> is equal to the current
		/// instance.
		/// </summary>
		/// <param name="obj">The <see cref="TaskDialogResult"/> to compare with the current
		/// instance.</param>
		/// <returns>True if <paramref name="obj"/> is equal to the current instance; otherwise,
		/// False.</returns>
		public Boolean Equals(TaskDialogResult obj)
		{
			return Equals(this, obj);
		}

		#endregion

		#region Methods: Overridden

		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is a <see cref="TaskDialogResult"/>
		/// value and equal to the current instance.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare with the current instance.
		/// </param>
		/// <returns>True if <paramref name="obj"/> is equal to the current instance; otherwise,
		/// False.</returns>
		public override Boolean Equals(Object obj)
		{
			if (obj != null) {
				if (obj is TaskDialogResult) {
					return Equals((TaskDialogResult)obj);
				}
			}

			return false;
		}

		/// <summary>
		/// Generates a hash code for this <see cref="TaskDialogResult"/>.
		/// </summary>
		/// <returns>A <see cref="Int32"/> that represents the hash code of this instance.</returns>
		public override Int32 GetHashCode()
		{
			return SelectedButton.GetHashCode() ^
				   SelectedRadioButton.GetHashCode() ^
				   VerificationChecked.GetHashCode();
		}

		/// <summary>
		/// Returns a string representation of this <see cref="TaskDialogResult"/>.
		/// </summary>
		/// <returns>A <see cref="String"/> that represents this instance.</returns>
		public override String ToString()
		{
			return String.Format("TaskDialogResult {{ SelectedButton = {0}, " +
								 "SelectedRadioButton = {1}, VerificationChecked = {2} }}",
								 SelectedButton, SelectedRadioButton, VerificationChecked);
		}

		#endregion

		#region Methods: Static

		/// <summary>
		/// Determines whether two <see cref="TaskDialogResult"/> instances are equal.
		/// </summary>
		/// <param name="objA">The first <see cref="TaskDialogResult"/> instance to compare.</param>
		/// <param name="objB">The second <see cref="TaskDialogResult"/> instance to compare.</param>
		/// <returns>True if <paramref name="objA"/> and <paramref name="objB"/> represent the
		/// same values; otherwise, False.</returns>
		public static Boolean Equals(TaskDialogResult objA, TaskDialogResult objB)
		{
			return objA == objB;
		}

		#endregion

		#region Operators

		/// <summary>
		/// Determines whether two <see cref="TaskDialogResult"/> instances are equal.
		/// </summary>
		/// <param name="objA">The first <see cref="TaskDialogResult"/> instance to compare.</param>
		/// <param name="objB">The second <see cref="TaskDialogResult"/> instance to compare.</param>
		/// <returns>True if <paramref name="objA"/> and <paramref name="objB"/> represent the
		/// same values; otherwise, False.</returns>
		public static Boolean operator ==(TaskDialogResult objA, TaskDialogResult objB)
		{
			return objA.SelectedButton == objB.SelectedButton &&
				   objA.SelectedRadioButton == objB.SelectedRadioButton &&
				   objA.VerificationChecked == objB.VerificationChecked;
		}

		/// <summary>
		/// Determines whether two <see cref="TaskDialogResult"/> instances are not equal.
		/// </summary>
		/// <param name="objA">The first <see cref="TaskDialogResult"/> instance to compare.</param>
		/// <param name="objB">The second <see cref="TaskDialogResult"/> instance to compare.</param>
		/// <returns>True if <paramref name="objA"/> and <paramref name="objB"/> do not represent
		/// the same values; otherwise, False.</returns>
		public static Boolean operator !=(TaskDialogResult objA, TaskDialogResult objB)
		{
			return !(objA == objB);
		}

		#endregion
	}
}
