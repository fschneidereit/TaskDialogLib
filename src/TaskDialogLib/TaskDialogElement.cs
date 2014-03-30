/***************************************************************************************************
 *
 *  TaskDialog Library
 *  Copyright © 2014 Florian Schneidereit. All Rights Reserved.
 *
 *  File:
 *    TaskDialogElement.cs
 *  Purpose:
 *    Task dialog element dependency object.
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

#endregion

namespace TaskDialogLib
{
	/// <summary>
	/// Represents the base class for all task dialog elements.
	/// </summary>
	public abstract class TaskDialogElement : DependencyObject
	{
		#region Fields

		WeakReference<TaskDialog> ownerReference;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskDialogElement"/> class.
		/// </summary>
		protected TaskDialogElement()
		{
			ownerReference = null;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the owner <see cref="TaskDialog"/> of this <see cref="TaskDialogElement"/>.
		/// </summary>
		protected internal TaskDialog Owner {
			get {
				TaskDialog owner;

				if (ownerReference != null) {
					if (ownerReference.TryGetTarget(out owner)) {
						return owner;
					}
				}
				
				return null;
			}
			set {
				if (ownerReference == null) {
					ownerReference = new WeakReference<TaskDialog>(value);
				} else {
					ownerReference.SetTarget(value);
				}
			}
		}

		#endregion
	}
}
