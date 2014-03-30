/***************************************************************************************************
 *
 *  TaskDialog Library
 *  Copyright © 2014 Florian Schneidereit. All Rights Reserved.
 *
 *  File:
 *    TaskDialogText.cs
 *  Purpose:
 *    Task dialog text dependency object.
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
using System.Windows;
using System.Windows.Markup;

#endregion

namespace TaskDialogLib
{
	/// <summary>
	/// Represents formatted content of a <see cref="TaskDialog"/>.
	/// </summary>
	[ContentProperty("Contents")]
	public sealed class TaskDialogText : DependencyObject
	{
		#region Fields

		readonly TaskDialogTextElementCollection contents;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TaskDialogText class.
		/// </summary>
		public TaskDialogText()
		{
			contents = new TaskDialogTextElementCollection();
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets a <see cref="TaskDialogTextElementCollection"/> that represents the contents of
		/// this <see cref="TaskDialogText"/>.
		/// </summary>
		public TaskDialogTextElementCollection Contents {
			get { return contents; }
		}

		#endregion

		#region Methods: Overridden

		/// <summary>
		/// Returns a string that represents this <see cref="TaskDialogText"/>.
		/// </summary>
		/// <returns>A <see cref="String"/> that represent the current instance.</returns>
		public override String ToString()
		{
			return Contents.ToString();
		}

		#endregion
	}
}
