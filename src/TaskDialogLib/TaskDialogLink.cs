/***************************************************************************************************
 *
 *  TaskDialog Library
 *  Copyright © 2014 Florian Schneidereit. All Rights Reserved.
 *
 *  File:
 *    TaskDialogLink.cs
 *  Purpose:
 *    Task dialog link object.
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
	/// Represents an inline hyperlink on a <see cref="TaskDialog"/>.
	/// </summary>
	public class TaskDialogLink : TaskDialogTextElement
	{
		#region Fields

		EventHandler clickEventHandler;

		#endregion

		#region Fields: Dependency Properties

		/// <summary>
		/// Identifies the <see cref="Uri"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty UriProperty =
			DependencyProperty.Register("Uri", typeof(Uri), typeof(TaskDialogLink),
										new PropertyMetadata(null));

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TaskDialogLink class.
		/// </summary>
		public TaskDialogLink()
		{
		}

		/// <summary>
		/// Initializes a new instance of the TaskDialogLink class with the specified text.
		/// </summary>
		/// <param name="text">A <see cref="String"/> that represents the link text.</param>
		public TaskDialogLink(String text)
		{
			// Initialize instance
			Text = text;
		}

		/// <summary>
		/// Initializes a new instance of the TaskDialogLink class with the specified text and URI.
		/// </summary>
		/// <param name="text">A <see cref="String"/> that represent the link text.</param>
		/// <param name="uri">A <see cref="Uri"/> that represents the link URI.</param>
		public TaskDialogLink(String text, Uri uri)
		{
			// Initialize instance
			Text = text;
			Uri = uri;
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when this <see cref="TaskDialogLink"/> is clicked.
		/// </summary>
		public event EventHandler Click {
			add { clickEventHandler = (EventHandler)Delegate.Combine(clickEventHandler, value); }
			remove { clickEventHandler = (EventHandler)Delegate.Remove(clickEventHandler, value); }
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the URI associated with this <see cref="TaskDialogLink"/>.
		/// </summary>
		public Uri Uri {
			get { return GetValue(UriProperty) as Uri; }
			set { SetValue(UriProperty, value); }
		}

		#endregion

		#region Methods

		internal void RaiseClickEvent()
		{
			OnClick(EventArgs.Empty);
		}

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
		/// Returns a string that represents this <see cref="TaskDialogLink"/>.
		/// </summary>
		/// <returns>A <see cref="String"/> that represents the current instance.</returns>
		public override String ToString()
		{
			if (Owner != null) {
				if (Owner.EnableHyperlinks) {
					return String.Format("<A HREF=\"{0}\">{1}</A>", Uri, Text);
				}
			}

			return base.ToString();
		}

		#endregion
	}
}
