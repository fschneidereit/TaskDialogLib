/***************************************************************************************************
 *
 *  Flatcode Task Dialog Library
 *  Copyright © 2014 Flatcode.net. All Rights Reserved.
 *
 *  File:
 *    TaskDialogIcon.cs
 *  Purpose:
 *    Task dialog default icon enumeration.
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
	/// Specifies constants defining which icon to display on a <see cref="TaskDialog"/>.
	/// </summary>
	public enum TaskDialogIcon
	{
		/// <summary>
		/// The task dialog contains no icon.
		/// </summary>
		None = 0,

		/// <summary>
		/// The task dialog contains a warning sign.
		/// </summary>
		Warning = -1,

		/// <summary>
		/// The task dialog contains an error sign.
		/// </summary>
		Error = -2,

		/// <summary>
		/// The task dialog contains an information sign.
		/// </summary>
		Information = -3,

		/// <summary>
		/// The task dialog contains a shield sign.
		/// </summary>
		Shield = -4
	}
}
