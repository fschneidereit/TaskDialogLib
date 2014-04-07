/***************************************************************************************************
 *
 *  Flatcode Task Dialog Library
 *  Copyright © 2014 Flatcode.net. All Rights Reserved.
 *
 *  File:
 *    TaskDialogTimerEventArgs.cs
 *  Purpose:
 *    Task dialog timer event arguments object.
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
	/// Represents timer event data for a <see cref="TaskDialog"/>.
	/// </summary>
	public sealed class TaskDialogTimerEventArgs : EventArgs
	{
		#region Fields

		readonly Int32 tickCount;
		Boolean reset;

		#endregion

		#region Constructors

		internal TaskDialogTimerEventArgs(Int32 tickCount)
		{
			// Initialize instance
			this.tickCount = tickCount;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets whether the tick count should be reset after event handling is complete.
		/// </summary>
		public Boolean Reset {
			get { return reset; }
			set { reset = value; }
		}

		/// <summary>
		/// Gets the tick count, in milliseconds, since the last reset. If the tick count has never
		/// been reset, this is the amount of time since the task dialog has been activated.
		/// </summary>
		public Int32 TickCount {
			get { return tickCount; }
		}

		#endregion
	}
}
