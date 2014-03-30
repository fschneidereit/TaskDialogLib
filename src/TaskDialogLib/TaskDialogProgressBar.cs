/***************************************************************************************************
 *
 *  TaskDialog Library
 *  Copyright © 2014 Florian Schneidereit. All Rights Reserved.
 *
 *  File:
 *    TaskDialogProgressBar.cs
 *  Purpose:
 *    Task dialog progress bar object.
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
	/// Represents the progress bar of a <see cref="TaskDialog"/>.
	/// </summary>
	public class TaskDialogProgressBar : TaskDialogElement
	{
		#region Fields

		Int32 marqueeInterval;
		TaskDialogProgressBarStyle style;

		#endregion

		#region Fields: Dependency Properties

		/// <summary>
		/// Identifies the <see cref="Maximum"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty MaximumProperty =
			DependencyProperty.Register("Maximum", typeof(Int32), typeof(TaskDialogProgressBar),
										new PropertyMetadata(100, MaximumPropertyChanged,
											                      CoerceMaximumProperty),
																  ValidateInt32TypeProperty);

		/// <summary>
		/// Identifies the <see cref="Minimum"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty MinimumProperty =
			DependencyProperty.Register("Minimum", typeof(Int32), typeof(TaskDialogProgressBar),
										new PropertyMetadata(0, MinimumPropertyChanged,
											                    CoerceMinimumProperty),
																ValidateInt32TypeProperty);

		/// <summary>
		/// Identifies the <see cref="RunMarquee"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty RunMarqueeProperty =
			DependencyProperty.Register("RunMarquee", typeof(Boolean), typeof(TaskDialogProgressBar),
										new PropertyMetadata(false, RunMarqueePropertyChanged));

		/// <summary>
		/// Identifies the <see cref="State"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty StateProperty =
			DependencyProperty.Register("State", typeof(TaskDialogProgressBarState),
										typeof(TaskDialogProgressBar),
										new PropertyMetadata(TaskDialogProgressBarState.Normal,
											                 StatePropertyChanged),
															 ValidateStateProperty);

		/// <summary>
		/// Identifies the <see cref="Value"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register("Value", typeof(Int32), typeof(TaskDialogProgressBar),
										new PropertyMetadata(0, ValuePropertyChanged,
											                    CoerceValueProperty),
																ValidateInt32TypeProperty);

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the marquee update interval of this <see cref="TaskDialogProgressBar"/>.
		/// </summary>
		/// <remarks>
		/// The value of this property has no effect on this <see cref="TaskDialogProgressBar"/> if
		/// the value of the <see cref="Style"/> property is not set to Marquee.
		/// </remarks>
		public Int32 MarqueeInterval {
			get { return marqueeInterval; }
			set {
				marqueeInterval = value;

				if (Owner != null) {
					if (Owner.IsActivated) {
						SetProgressBarMarquee(RunMarquee, marqueeInterval);
					}
				}
			}
		}

		/// <summary>
		/// Gets or set the maximum value of this <see cref="TaskDialogProgressBar"/>.
		/// </summary>
		public Int32 Maximum {
			get { return (Int32)GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}

		/// <summary>
		/// Gets or sets the minimum value of this <see cref="TaskDialogProgressBar"/>.
		/// </summary>
		public Int32 Minimum {
			get { return (Int32)GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}

		/// <summary>
		/// Gets or sets wheter the marquee of this <see cref="TaskDialogProgressBar"/> is running.
		/// </summary>
		public Boolean RunMarquee {
			get { return (Boolean)GetValue(RunMarqueeProperty); }
			set { SetValue(RunMarqueeProperty, value); }
		}

		/// <summary>
		/// Gets or sets the state of this <see cref="TaskDialogProgressBar"/>.
		/// </summary>
		public TaskDialogProgressBarState State {
			get { return (TaskDialogProgressBarState)GetValue(StateProperty); }
			set { SetValue(StateProperty, value); }
		}

		/// <summary>
		/// Gets or sets the style of this <see cref="TaskDialogProgressBar"/>.
		/// </summary>
		public TaskDialogProgressBarStyle Style {
			get { return style; }
			set {
				style = value;

				if (Owner != null) {
					if (Owner.IsActivated) {
						SetProgressBarStyle(style);
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the value of this <see cref="TaskDialogProgressBar"/>.
		/// </summary>
		public Int32 Value {
			get { return (Int32)GetValue(ValueProperty); }
			set { SetValue(ValueProperty, value); }
		}

		#endregion

		#region Methods: Dependency Property Callbacks

		static Object CoerceMaximumProperty(DependencyObject source, Object value)
		{
			TaskDialogProgressBar progressBar = source as TaskDialogProgressBar;
			Int32 maximum = (Int32)value;

			if (progressBar != null) {
				// Maximum cannot be smaller than or equal minimum
				if (maximum <= progressBar.Minimum) {
					maximum = progressBar.Minimum + 1;
				}
			}

			// Maximum cannot be greater than Int16.MaxValue
			// Restricted by the underlying native API
			if (maximum > Int16.MaxValue) {
				maximum = Int16.MaxValue;
			}

			return maximum;
		}

		static Object CoerceMinimumProperty(DependencyObject source, Object value)
		{
			TaskDialogProgressBar progressBar = source as TaskDialogProgressBar;
			Int32 minimum = (Int32)value;

			if (progressBar != null) {
				// Minimum cannot be larger than or equal maximum
				if (minimum >= progressBar.Maximum) {
					minimum = progressBar.Minimum + 1;
				}
			}

			// Minimum cannot be smaller than Int16.MinValue
			// Restricted by the underlying native API
			if (minimum < Int16.MinValue) {
				minimum = Int16.MinValue;
			}

			return minimum;
		}

		static Object CoerceValueProperty(DependencyObject source, Object value)
		{
			TaskDialogProgressBar progressBar = source as TaskDialogProgressBar;
			Int32 v = (Int32)value;

			if (progressBar != null) {
				// Value cannot be smaller than the current minimum
				if (v < progressBar.Minimum) {
					v = progressBar.Minimum;
				}

				// Value cannot be larger than the current maximum
				if (v > progressBar.Maximum) {
					v = progressBar.Maximum;
				}
			}

			return v;
		}

		static void MaximumPropertyChanged(DependencyObject source,
			                               DependencyPropertyChangedEventArgs e)
		{
			TaskDialogProgressBar progressBar = source as TaskDialogProgressBar;
			Int32 maximum = (Int32)e.NewValue;

			if (progressBar != null) {
				// Get progress bar owner task dialog
				TaskDialog owner = progressBar.Owner;
				Int32 minimum = progressBar.Minimum;

				// Adjust Value property, if necessary
				if (progressBar.Value > maximum) {
					progressBar.Value = maximum;
				}

				if (owner != null) {
					// Send update message only if the owner task dialog has a progress bar and
					// is activated
					if (owner.HasProgressBar && owner.IsActivated) {
						progressBar.SetProgressBarRange(minimum, maximum);
					}
				}
			}
		}

		static void MinimumPropertyChanged(DependencyObject source,
										   DependencyPropertyChangedEventArgs e)
		{
			TaskDialogProgressBar progressBar = source as TaskDialogProgressBar;
			Int32 minimum = (Int32)e.NewValue;

			if (progressBar != null) {
				// Get progress bar owner task dialog
				TaskDialog owner = progressBar.Owner;
				Int32 maximum = progressBar.Maximum;

				// Adjust Value property, if necessary
				if (progressBar.Value < minimum) {
					progressBar.Value = minimum;
				}

				if (owner != null) {
					// Send update message only if the owner task dialog has a progress bar and
					// is activated
					if (owner.HasProgressBar && owner.IsActivated) {
						progressBar.SetProgressBarRange(minimum, maximum);
					}
				}
			}
		}

		static void RunMarqueePropertyChanged(DependencyObject source,
			                                  DependencyPropertyChangedEventArgs e)
		{
			TaskDialogProgressBar progressBar = source as TaskDialogProgressBar;
			Boolean runMarquee = (Boolean)e.NewValue;

			if (progressBar != null) {
				// Get progress bar owner task dialog
				TaskDialog owner = progressBar.Owner;

				if (owner != null) {
					// Send update message only if the owner task dialog has a progress bar and
					// is activated
					if (owner.HasProgressBar && owner.IsActivated) {
						progressBar.SetProgressBarMarquee(runMarquee, progressBar.MarqueeInterval);
					}
				}
			}
		}

		static void StatePropertyChanged(DependencyObject source,
			                             DependencyPropertyChangedEventArgs e)
		{
			TaskDialogProgressBar progressBar = source as TaskDialogProgressBar;
			TaskDialogProgressBarState state = (TaskDialogProgressBarState)e.NewValue;

			if (progressBar != null) {
				// Get progress bar owner task dialog
				TaskDialog owner = progressBar.Owner;

				if (owner != null) {
					// Send update message only if the owner task dialog has a progress bar and
					// is activated
					if (owner.HasProgressBar && owner.IsActivated) {
						progressBar.SetProgressBarState(state);
					}
				}
			}
		}

		static void ValuePropertyChanged(DependencyObject source,
										 DependencyPropertyChangedEventArgs e)
		{
			TaskDialogProgressBar progressBar = source as TaskDialogProgressBar;
			Int32 value = (Int32)e.NewValue;

			if (progressBar != null) {
				// Get progress bar owner task dialog
				TaskDialog owner = progressBar.Owner;
				
				if (owner != null) {
					// Send update message only if the owner task dialog has a progress bar and
					// is activated
					if (owner.HasProgressBar && owner.IsActivated) {
						progressBar.SetProgressBarValue(value);
					}
				}
			}
		}

		static Boolean ValidateInt32TypeProperty(Object value)
		{
			return value is Int32;
		}

		static Boolean ValidateStateProperty(Object value)
		{
			TaskDialogProgressBarState state = (TaskDialogProgressBarState)value;
			return (state == TaskDialogProgressBarState.Normal) ||
				   (state == TaskDialogProgressBarState.Error) ||
				   (state == TaskDialogProgressBarState.Paused);
		}

		#endregion

		#region Methods: Implementation

		internal void SetProgressBarMarquee(Boolean display, Int32 interval)
		{
			NativeMethods.SendMessage(
				Owner.Handle, (UInt32)NativeMethods.TASKDIALOG_MESSAGES.TDM_SET_PROGRESS_BAR_MARQUEE,
				new UIntPtr((UInt32)(display ? 1 : 0)), new IntPtr(interval));
		}

		internal void SetProgressBarRange(Int32 minimum, Int32 maximum)
		{
			Int32 range = unchecked((Int32)((((UInt16)maximum) << 16) + ((UInt16)minimum)));

			NativeMethods.SendMessage(
				Owner.Handle, (UInt32)NativeMethods.TASKDIALOG_MESSAGES.TDM_SET_PROGRESS_BAR_RANGE,
				UIntPtr.Zero, new IntPtr(range));
		}

		internal void SetProgressBarState(TaskDialogProgressBarState state)
		{
			NativeMethods.SendMessage(
				Owner.Handle, (UInt32)NativeMethods.TASKDIALOG_MESSAGES.TDM_SET_PROGRESS_BAR_STATE,
				new UIntPtr((UInt32)state), IntPtr.Zero);
		}

		internal void SetProgressBarStyle(TaskDialogProgressBarStyle style)
		{
			NativeMethods.SendMessage(
				Owner.Handle, (UInt32)NativeMethods.TASKDIALOG_MESSAGES.TDM_SET_MARQUEE_PROGRESS_BAR,
				new UIntPtr((UInt32)style), IntPtr.Zero);
		}

		internal void SetProgressBarValue(Int32 value)
		{
			NativeMethods.SendMessage(
				Owner.Handle, (UInt32)NativeMethods.TASKDIALOG_MESSAGES.TDM_SET_PROGRESS_BAR_POS,
				new UIntPtr((UInt32)value), IntPtr.Zero);
		}

		#endregion
	}
}
