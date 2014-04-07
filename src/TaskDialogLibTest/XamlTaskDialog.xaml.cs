/***************************************************************************************************
 *
 *  Flatcode Task Dialog Library Test Application
 *  Copyright © 2014 Flatcode.net. All Rights Reserved.
 *
 *  File:
 *    XamlTaskDialog.xaml.cs
 *  Purpose:
 *    Test task dialog XAML code-behind.
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

#region Using Directives: TaskDialogLib

using Flatcode.Presentation;

#endregion

namespace TaskDialogLibTest
{
	public partial class XamlTaskDialog : TaskDialog
	{
		#region Constructors

		public XamlTaskDialog()
		{
			InitializeComponent();
		}

		#endregion

		#region Methods: Event Handler

		void Link1_Click(object sender, EventArgs e)
		{
			TaskDialogLink link = sender as TaskDialogLink;

			if (link != null) {
				MessageBox.Show(String.Format("Uri = {0}", link.Uri), "Link Clicked!");
			}
		}

		void Button1_Click(object sender, EventArgs e)
		{
			TaskDialogButton button = sender as TaskDialogButton;

			if (button != null) {
				MessageBox.Show("Button 1 was clicked.", "Button Clicked!");
			}
		}

		#endregion
	}
}
