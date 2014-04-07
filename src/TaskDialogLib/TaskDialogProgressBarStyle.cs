/***************************************************************************************************
 *
 *  Flatcode Task Dialog Library
 *  Copyright © 2014 Flatcode.net. All Rights Reserved.
 *
 *  File:
 *    TaskDialogProgressBarMode.cs
 *  Purpose:
 *    Task dialog progress bar mode enumeration.
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

namespace Flatcode.Presentation
{
	/// <summary>
	/// Specifies constants defining the style of a <see cref="TaskDialogProgressBar"/>.
	/// </summary>
	public enum TaskDialogProgressBarStyle
	{
		/// <summary>
		/// Indicates that the progress bar has a normal style.
		/// </summary>
		Normal = 0,

		/// <summary>
		/// Indicates that the progress bar has a marquee style.
		/// </summary>
		Marquee = 1
	}
}
