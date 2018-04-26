/***************************************************************************************************
 *
 *  Flatcode Task Dialog Library
 *  Copyright © 2014 Flatcode.net. All Rights Reserved.
 *
 *  File:
 *    TaskDialogTextElementCollection.cs
 *  Purpose:
 *    Task dialog text element dependency object.
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
using System.Text;
using System.Windows.Markup;

#endregion

namespace Flatcode.Presentation
{
	/// <summary>
	/// Represents a collection of <see cref="TaskDialogTextElement"/> objects.
	/// </summary>
	[ContentWrapper(typeof(TaskDialogRun))]
	[WhitespaceSignificantCollection]
	public class TaskDialogTextElementCollection :
		TaskDialogElementCollection<TaskDialogTextElement>
	{
		#region Methods

		/// <summary>
		/// Adds a <see cref="TaskDialogLink"/> instance to this
		/// <see cref="TaskDialogTextElementCollection"/>.
		/// </summary>
		/// <param name="item">The <see cref="TaskDialogLink"/> to be added.</param>
		public void AddLink(TaskDialogLink item)
		{
			Add(item);
		}

		/// <summary>
		/// Adds a <see cref="TaskDialogRun"/> instance to this
		/// <see cref="TaskDialogTextElementCollection"/>.
		/// </summary>
		/// <param name="item">The <see cref="TaskDialogRun"/> to be added.</param>
		public void AddRun(TaskDialogRun item)
		{
			Add(item);
		}

		#endregion

		#region Methods: Overridden

		/// <summary>
		/// This method is implementation-specific and not intended to be used from third-party
		/// code.
		/// </summary>
		/// <param name="item">This argument is implementation-specific and not intended to be used
		/// from third-party code.</param>
		/// <returns>The return value is implementation-specific and not intended to be used from
		/// third-party code.</returns>
		protected internal override Int32 AddInternal(Object item)
		{
			if (item is String s) {
                /*
                 * XAML will coalesce all whitespace into a single space. If
                 * there are no non-whitespace characters, ignore this part.
                 */
                if (s == " ")
                    return -1;

                // if there _is_ a non-whitespace character, create a new Run.
				item = new TaskDialogRun(s);
			}

			return base.AddInternal(item);
		}

		/// <summary>
		/// Returns a string that represents this <see cref="TaskDialogTextElementCollection"/>.
		/// </summary>
		/// <returns>A <see cref="String"/> that represents the current instance.</returns>
		public override String ToString()
		{
			StringBuilder sb = new StringBuilder();

			foreach (TaskDialogTextElement item in this) {
				sb.Append(item.ToString());
			}

			return sb.ToString();
		}

		#endregion
	}
}
