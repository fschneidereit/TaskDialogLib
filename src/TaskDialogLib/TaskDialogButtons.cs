/***************************************************************************************************
 *
 *  TaskDialog Library
 *  Copyright © 2014 Florian Schneidereit. All Rights Reserved.
 *
 *  File:
 *    TaskDialogButtons.cs
 *  Purpose:
 *    Task dialog buttons enumeration.
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

#endregion

namespace TaskDialogLib
{
	/// <summary>
	/// Specifies constants defining which common buttons to display on a <see cref="TaskDialog"/>.
	/// </summary>
	[Flags]
	public enum TaskDialogButtons
	{
		#region Constants

		/// <summary>
		/// The task dialog contains no buttons.
		/// </summary>
		None = 0,

		/// <summary>
		/// The task dialog contains an OK button.
		/// </summary>
		OK = 1,

		/// <summary>
		/// The task dialog contains a Yes button.
		/// </summary>
		Yes = 2,

		/// <summary>
		/// The task dialog contains a No button.
		/// </summary>
		No = 4,

		/// <summary>
		/// The task dialog contains a Cancel button.
		/// </summary>
		Cancel = 8,

		/// <summary>
		/// The task dialog contains a Retry button.
		/// </summary>
		Retry = 16,

		/// <summary>
		/// The task dialog contains a Close button.
		/// </summary>
		Close = 32,

		#endregion

		#region Constants: Combined Flags

		/// <summary>
		/// The task dialog contains Close and Cancel buttons.
		/// </summary>
		OKCancel = OK | Cancel,

		/// <summary>
		/// The task dialog contains Yes and No buttons.
		/// </summary>
		YesNo = Yes | No,

		/// <summary>
		/// The task dialog contains Yes, No, and Cancel buttons.
		/// </summary>
		YesNoCancel = YesNo | Cancel,

		/// <summary>
		/// The task dialog contains Retry and Cancel buttons.
		/// </summary>
		RetryCancel = Retry | Cancel

		#endregion
	}
}
