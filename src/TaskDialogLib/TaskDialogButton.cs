/***************************************************************************************************
 *
 *  Flatcode Task Dialog Library
 *  Copyright © 2014 Flatcode.net. All Rights Reserved.
 *
 *  File:
 *    TaskDialogButton.cs
 *  Purpose:
 *    Task dialog button object.
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

namespace Flatcode.Presentation
{
	/// <summary>
	/// Represents a custom button of a <see cref="TaskDialog"/>.
	/// </summary>
	public class TaskDialogButton : TaskDialogButtonBase
	{
		#region Fields

		Boolean elevationRequired;
		Boolean preventClose;

		#endregion

		#region Fields: Dependency Properties

		/// <summary>
		/// Indentifies the <see cref="Description"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty DescriptionProperty =
			DependencyProperty.Register("Description", typeof(Object), typeof(TaskDialogButton),
										new PropertyMetadata(null));

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets whether administrative permissions are required for the action that is
		/// performed by this <see cref="TaskDialogButton"/>.
		/// </summary>
		public Boolean ElevationRequired {
			get { return elevationRequired; }
			set {
				elevationRequired = value;

				if (Owner != null) {
					if (Owner.IsActivated) {
						SetElevationRequired(elevationRequired);
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the description text for this <see cref="TaskDialogButton"/>.
		/// </summary>
		/// <remarks>
		/// The value of this property has no effect if the <see cref="TaskDialogButtonStyle"/> of
		/// the owner task dialog is set to Normal.
		/// </remarks>
		public Object Description {
			get { return GetValue(DescriptionProperty) as Object; }
			set { SetValue(DescriptionProperty, value); }
		}

		/// <summary>
		/// Gets or sets whether this <see cref="TaskDialogButton"/> prevents the task dialog from
		/// closing when clicked.
		/// </summary>
		public Boolean PreventClose {
			get { return preventClose; }
			set { preventClose = value; }
		}

		#endregion

		#region Methods: Implementation

		internal override void SetButtonEnabled(Boolean enabled)
		{
			Int32 buttonID = Owner.Buttons.IndexOf(this);

			// Send TDM_ENABLE_BUTTON message to enable/disable the button
			NativeMethods.SendMessage(
				Owner.Handle, (UInt32)NativeMethods.TASKDIALOG_MESSAGES.TDM_ENABLE_BUTTON,
				new UIntPtr((UInt32)buttonID), new IntPtr(enabled ? 1 : 0));
		}

		internal void SetElevationRequired(Boolean required)
		{
			Int32 buttonID = Owner.Buttons.IndexOf(this);

			// Send TDM_SET_BUTTON_ELEVATION_REQUIRED_STATE message to set the state
			NativeMethods.SendMessage(
				Owner.Handle,
				(UInt32)NativeMethods.TASKDIALOG_MESSAGES.TDM_SET_BUTTON_ELEVATION_REQUIRED_STATE,
				new UIntPtr((UInt32)buttonID), new IntPtr(required ? 1 : 0));
		}

		#endregion

		#region Methods: Overridden

		/// <summary>
		/// Returns a string that represents this <see cref="TaskDialogButton"/>.
		/// </summary>
		/// <returns>A <see cref="String"/> that represents the current instance.
		/// </returns>
		public override String ToString()
		{
			if (Owner != null) {
				if (Owner.ButtonStyle != TaskDialogButtonStyle.Normal) {
					// Handle command link button style
					return String.Format("{0}\n{1}", Title, Description);
				}
			}

			return base.ToString();
		}

		#endregion
	}
}
