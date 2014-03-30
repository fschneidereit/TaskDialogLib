/***************************************************************************************************
 *
 *  TaskDialog Library
 *  Copyright © 2014 Florian Schneidereit. All Rights Reserved.
 *
 *  File:
 *    TaskDialogProgressBarState.cs
 *  Purpose:
 *    Task dialog progress bar state enumeration.
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
	/// Specifies constants defining the state of a <see cref="TaskDialogProgressBar"/>.
	/// </summary>
	public enum TaskDialogProgressBarState
	{
		/// <summary>
		/// Indicates that the progress bar is in a normal state.
		/// </summary>
		Normal = 1,

		/// <summary>
		/// Indicates that the progress bar is in an error state.
		/// </summary>
		Error = 2,

		/// <summary>
		/// Indicates that the progress bar is in a paused state.
		/// </summary>
		Paused = 3
	}
}
