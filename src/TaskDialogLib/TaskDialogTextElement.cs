/***************************************************************************************************
 *
 *  TaskDialog Library
 *  Copyright © 2014 Florian Schneidereit. All Rights Reserved.
 *
 *  File:
 *    TaskDialogTextElement.cs
 *  Purpose:
 *    Task dialog text element object.
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
	/// Represents the base class for all task dialog text elements.
	/// </summary>
	[ContentProperty("Text")]
	public abstract class TaskDialogTextElement : TaskDialogElement
	{
		#region Fields: Dependency Properties

		/// <summary>
		/// Identifies the <see cref="Text"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register("Text", typeof(String), typeof(TaskDialogTextElement),
			                            new PropertyMetadata(null));

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TaskDialogTextElement class.
		/// </summary>
		protected TaskDialogTextElement()
		{
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the text of this <see cref="TaskDialogTextElement"/>.
		/// </summary>
		public String Text {
			get { return GetValue(TextProperty) as String; }
			set { SetValue(TextProperty, value); }
		}

		#endregion

		#region Methods: Overridden

		/// <summary>
		/// Returns a string that represents this <see cref="TaskDialogTextElement"/>.
		/// </summary>
		/// <returns>A <see cref="String"/> that represents the current instance.</returns>
		public override String ToString()
		{
			return Text;
		}

		#endregion
	}
}
