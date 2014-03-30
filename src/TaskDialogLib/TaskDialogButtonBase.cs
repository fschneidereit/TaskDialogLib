/***************************************************************************************************
 *
 *  TaskDialog Library
 *  Copyright © 2014 Florian Schneidereit. All Rights Reserved.
 *
 *  File:
 *    TaskDialogButtonBase.cs
 *  Purpose:
 *    Task dialog button base object.
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
	/// Represents the base class for all task dialog buttons.
	/// </summary>
	[ContentProperty("Title")]
	public abstract class TaskDialogButtonBase : TaskDialogElement
	{
		#region Fields

		EventHandler clickEventHandler;

		#endregion

		#region Fields: Dependency Properties

		/// <summary>
		/// Identifies the <see cref="IsEnabled"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty IsEnabledProperty =
			DependencyProperty.Register("IsEnabled", typeof(Boolean), typeof(TaskDialogButtonBase),
										new PropertyMetadata(true, IsEnabledPropertyChanged));

		/// <summary>
		/// Identifies the <see cref="Title"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty TitleProperty =
			DependencyProperty.Register("Title", typeof(String), typeof(TaskDialogButtonBase),
										new PropertyMetadata(null));

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="TaskDialogButtonBase"/> class.
		/// </summary>
		protected TaskDialogButtonBase()
		{
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when this <see cref="TaskDialogButtonBase"/> is clicked.
		/// </summary>
		public event EventHandler Click {
			add { clickEventHandler = (EventHandler)Delegate.Combine(clickEventHandler, value);	}
			remove { clickEventHandler = (EventHandler)Delegate.Remove(clickEventHandler, value); }
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets whether this <see cref="TaskDialogButtonBase"/> is enabled.
		/// </summary>
		public Boolean IsEnabled {
			get { return (Boolean)GetValue(IsEnabledProperty); }
			set { SetValue(IsEnabledProperty, value); }
		}

		/// <summary>
		/// Gets or sets the title of this <see cref="TaskDialogButtonBase"/>.
		/// </summary>
		public String Title {
			get { return GetValue(TitleProperty) as String; }
			set { SetValue(TitleProperty, value); }
		}

		#endregion

		#region Methods: Dependency Property Callbacks

		static void IsEnabledPropertyChanged(DependencyObject source,
			                                 DependencyPropertyChangedEventArgs e)
		{
			TaskDialogButtonBase buttonBase = source as TaskDialogButtonBase;

			if (buttonBase != null) {
				if (buttonBase.Owner != null) {
					if (buttonBase.Owner.IsActivated) {
						buttonBase.SetButtonEnabled((Boolean)e.NewValue);
					}
				}
			}
		}

		#endregion

		#region Methods: Implementation

		internal void RaiseClickEvent()
		{
			OnClick(EventArgs.Empty);
		}

		internal abstract void SetButtonEnabled(Boolean enabled);

		#endregion

		#region Methods: Event Handler

		/// <summary>
		/// Raises the <see cref="Click"/> event.
		/// </summary>
		/// <param name="e">A <see cref="EventArgs"/> object that contains the event data.</param>
		protected virtual void OnClick(EventArgs e)
		{
			EventHandler handler = clickEventHandler;

			if (handler != null) {
				handler(this, e);
			}
		}

		#endregion

		#region Methods: Overridden

		/// <summary>
		/// Returns a string that represents this <see cref="TaskDialogButtonBase"/>.
		/// </summary>
		/// <returns>A <see cref="String"/> that represents the current instance.</returns>
		public override String ToString()
		{
			return Title;
		}

		#endregion
	}
}
