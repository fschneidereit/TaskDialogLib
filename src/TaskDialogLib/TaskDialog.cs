/***************************************************************************************************
 *
 *  TaskDialog Library
 *  Copyright © 2014 Florian Schneidereit. All Rights Reserved.
 *
 *  File:
 *    TaskDialog.cs
 *  Purpose:
 *    Task dialog dependency object.
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
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;

#endregion

namespace TaskDialogLib
{
	/// <summary>
	/// Represents a dialog box that can be used to display information and receive simple input
	/// from the user.
	/// </summary>
	/// <remarks>
	/// Like a <see cref="MessageBox"/>, a task dialog is formatted by the operating system
	/// according to properties you set. However, it offers many more features than a message box. 
	/// </remarks>
	[DefaultProperty("Content")]
	[ContentProperty("Content")]
	public class TaskDialog : DependencyObject
	{
		#region Fields

		readonly TaskDialogElementCollection<TaskDialogButton> buttons;
		readonly TaskDialogElementCollection<TaskDialogRadioButton> radioButtons;

		Boolean activated;
		Boolean allowCancellation;
		TaskDialogButtonStyle buttonStyle;
		Boolean canMinimize;
		TaskDialogButtons commonButtons;
		Int32 defaultButton;
		TaskDialogIcon defaultIcon;
		Int32 defaultRadioButton;
		TaskDialogState defaultState;
		Boolean enableHyperlinks;
		Boolean enableTimer;
		Boolean expanded;
		Boolean expandFooter;
		IntPtr handle;
		Boolean initialized;
		TaskDialogProgressBar progressBar;
		Window owner;
		Boolean sizeToContent;
		Boolean useDefaultIcon;
		Boolean verificationChecked;

		IconHandle iconHandle;
		IconHandle footerIconHandle;

		EventHandler activatedEventHandler;
		EventHandler closedEventHandler;
		EventHandler collapsedEventHandler;
		EventHandler expandedEventHandler;
		EventHandler helpEventHandler;
		EventHandler initializedEventHandler;
		EventHandler<TaskDialogTimerEventArgs> timerEventHandler;
		EventHandler verificationChangedEventHandler;

		#endregion

		#region Fields: Dependency Properties

		/// <summary>
		/// Identifies the <see cref="CollapsedControlText"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty CollapsedControlTextProperty =
			DependencyProperty.Register("CollapsedControlText", typeof(String), typeof(TaskDialog),
										new PropertyMetadata(null));

		/// <summary>
		/// Identifies the <see cref="Content"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty ContentProperty =
			DependencyProperty.Register("Content", typeof(Object), typeof(TaskDialog),
										new PropertyMetadata(null, ContentPropertyChanged));

		/// <summary>
		/// Identifies the <see cref="ExpandedControlText"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty ExpandedControlTextProperty =
			DependencyProperty.Register("ExpandedControlText", typeof(String), typeof(TaskDialog),
										new PropertyMetadata(null));

		/// <summary>
		/// Identifies the <see cref="ExpandedInformation"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty ExpandedInformationProperty =
			DependencyProperty.Register("ExpandedInformation", typeof(Object), typeof(TaskDialog),
										new PropertyMetadata(null, ExpandedInformationPropertyChanged));

		/// <summary>
		/// Identifies the <see cref="Footer"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty FooterProperty =
			DependencyProperty.Register("Footer", typeof(Object), typeof(TaskDialog),
										new PropertyMetadata(null, FooterPropertyChanged));

		/// <summary>
		/// Identifies the <see cref="FooterIcon"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty FooterIconProperty =
			DependencyProperty.Register("FooterIcon", typeof(ImageSource), typeof(TaskDialog),
										new PropertyMetadata(null, FooterIconPropertyChanged));

		/// <summary>
		/// Identifies the <see cref="Icon"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty IconProperty =
			DependencyProperty.Register("Icon", typeof(ImageSource), typeof(TaskDialog),
										new PropertyMetadata(null, IconPropertyChanged));

		/// <summary>
		/// Identifies the <see cref="Instruction"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty InstructionProperty =
			DependencyProperty.Register("Instruction", typeof(String), typeof(TaskDialog),
										new PropertyMetadata(null, InstructionPropertyChanged));

		/// <summary>
		/// Identifies the <see cref="Title"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty TitleProperty =
			DependencyProperty.Register("Title", typeof(String), typeof(TaskDialog),
										new PropertyMetadata(null));

		/// <summary>
		/// Identifies the <see cref=" VerificationText"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty VerificationTextProperty =
			DependencyProperty.Register("VerificationText", typeof(String), typeof(TaskDialog),
										new PropertyMetadata(null));

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TaskDialog class.
		/// </summary>
		public TaskDialog()
		{
			// Default initialization
			buttons = new TaskDialogElementCollection<TaskDialogButton>();
			radioButtons = new TaskDialogElementCollection<TaskDialogRadioButton>();
			useDefaultIcon = true;
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when this <see cref="TaskDialog"/> is activated. This event coincides with
		/// cases where the value of the <see cref="IsActivated" /> property changes from False
		/// to True.
		/// </summary>
		public event EventHandler Activated {
			add {
				activatedEventHandler =
					(EventHandler)Delegate.Combine(activatedEventHandler, value);
			}
			remove {
				activatedEventHandler =
					(EventHandler)Delegate.Remove(activatedEventHandler, value);
			}
		}

		/// <summary>
		/// Occurs when this <see cref="TaskDialog"/> is closed.
		/// </summary>
		public event EventHandler Closed {
			add {
				closedEventHandler =
					(EventHandler)Delegate.Combine(closedEventHandler, value);
			}
			remove {
				closedEventHandler =
					(EventHandler)Delegate.Remove(closedEventHandler, value);
			}
		}

		/// <summary>
		/// Occurs when the <see cref="State"/> of this <see cref="TaskDialog"/> changes from
		/// Expanded to Collapsed.
		/// </summary>
		public event EventHandler Collapsed {
			add {
				collapsedEventHandler = 
					(EventHandler)Delegate.Combine(collapsedEventHandler, value);
			}
			remove {
				collapsedEventHandler =
					(EventHandler)Delegate.Remove(collapsedEventHandler, value);
			}
		}

		/// <summary>
		/// Occurs when the <see cref="State"/> of this <see cref="TaskDialog"/> changes from
		/// Collapsed to Expanded.
		/// </summary>
		public event EventHandler Expanded {
			add {
				expandedEventHandler =
					(EventHandler)Delegate.Combine(expandedEventHandler, value);
			}
			remove {
				expandedEventHandler =
					(EventHandler)Delegate.Remove(expandedEventHandler, value);
			}
		}

		/// <summary>
		/// Occurs when this <see cref="TaskDialog"/> is activated and the F1 key is pressed.
		/// </summary>
		public event EventHandler Help {
			add {
				helpEventHandler =
					(EventHandler)Delegate.Combine(helpEventHandler, value);
			}
			remove {
				helpEventHandler =
					(EventHandler)Delegate.Remove(helpEventHandler, value);
			}
		}

		/// <summary>
		/// Occurs when this <see cref="TaskDialog"/> is initialized. This event coincides with
		/// cases where the value of the <see cref="IsInitialized" /> property changes from False
		/// to True.
		/// </summary>
		public event EventHandler Initialized {
			add {
				initializedEventHandler =
					(EventHandler)Delegate.Combine(initializedEventHandler, value);
			}
			remove {
				initializedEventHandler =
					(EventHandler)Delegate.Remove(initializedEventHandler, value);
			}
		}

		/// <summary>
		/// Occurs approximately every 200 milliseconds when this <see cref="TaskDialog"/> is
		/// activated and the <see cref="EnableTimer"/> property is set to True.
		/// </summary>
		public event EventHandler<TaskDialogTimerEventArgs> Timer {
			add {
				timerEventHandler =
					(EventHandler<TaskDialogTimerEventArgs>)Delegate.Combine(timerEventHandler,
																			 value);
			}
			remove {
				timerEventHandler =
					(EventHandler<TaskDialogTimerEventArgs>)Delegate.Remove(timerEventHandler,
																			value);
			}
		}

		/// <summary>
		/// Occurs when the state of the verification checkbox on this <see cref="TaskDialog"/>
		/// changes. This event coincides with cases where the value of the 
		/// <see cref="IsVerificationChecked"/> property changes.
		/// </summary>
		public event EventHandler VerificationChanged {
			add {
				verificationChangedEventHandler =
					(EventHandler)Delegate.Combine(verificationChangedEventHandler, value);
			}
			remove {
				verificationChangedEventHandler =
					(EventHandler)Delegate.Remove(verificationChangedEventHandler, value);
			}
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets whether this <see cref="TaskDialog"/> can be closed using Alt-F4, Escape,
		/// and the title bar's close button even if no cancel button is specified in either the
		/// <see cref="CommonButtons"/> property or <see cref="Buttons"/> collection.
		/// </summary>
		[DefaultValue(false)]
		public Boolean AllowCancellation {
			get { return allowCancellation; }
			set {
				VerifyPropertyWriteAccess("AllowCancellation");
				allowCancellation = value;
			}
		}

		/// <summary>
		/// Gets a <see cref="TaskDialogElementCollection&lt;TaskDialogButton&gt;"/> containing the
		/// custom buttons of this <see cref="TaskDialog"/>.
		/// </summary>
		public TaskDialogElementCollection<TaskDialogButton> Buttons {
			get { return buttons; }
		}

		/// <summary>
		/// Gets or sets the <see cref="TaskDialogButtonStyle"/> of the custom buttons of this
		/// <see cref="TaskDialog"/>.
		/// </summary>
		/// <remarks>
		/// This property has no effect on this <see cref="TaskDialog"/> if no custom buttons are
		/// defined.
		/// </remarks>
		[DefaultValue(TaskDialogButtonStyle.Normal)]
		public TaskDialogButtonStyle ButtonStyle {
			get { return buttonStyle; }
			set {
				VerifyPropertyWriteAccess("ButtonStyle");
				buttonStyle = value;
			}
		}

		/// <summary>
		/// Gets or sets whether this <see cref="TaskDialog"/> can be minimized.
		/// </summary>
		[DefaultValue(false)]
		public Boolean CanMinimize {
			get { return canMinimize; }
			set {
				VerifyPropertyWriteAccess("CanMinimize");
				canMinimize = value;
			}
		}

		/// <summary>
		/// Gets or sets the text that is displayed next to the expando when the <see cref="State"/>
		/// if this <see cref="TaskDialog"/> is Collapsed.
		/// </summary>
		/// <remarks>
		/// The value of this property has no effect on this <see cref="TaskDialog"/> if the
		/// <see cref="ExpandedInformation"/> property has no content.
		/// </remarks>
		public String CollapsedControlText {
			get { return GetValue(CollapsedControlTextProperty) as String; }
			set {
				VerifyPropertyWriteAccess("CollapsedControlText");
				SetValue(CollapsedControlTextProperty, value); ;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="TaskDialogButtons"/> that are visible on this
		/// <see cref="TaskDialog"/>.
		/// </summary>
		[DefaultValue(TaskDialogButtons.None)]
		public TaskDialogButtons CommonButtons {
			get { return commonButtons; }
			set {
				VerifyPropertyWriteAccess("CommonButtons");
				commonButtons = value;
			}
		}

		/// <summary>
		/// Gets or sets the content to display on this <see cref="TaskDialog"/>.
		/// </summary>
		public Object Content {
			get { return GetValue(ContentProperty); }
			set { SetValue(ContentProperty, value); }
		}

		/// <summary>
		/// Gets or sets the default button of this <see cref="TaskDialog"/>.
		/// </summary>
		/// <remarks>
		/// The value of this property represents either an index into the <see cref="Buttons"/>
		/// collection, or a <see cref="TaskDialogButtons"/> enumeration value of a common button
		/// defined in the <see cref="CommonButtons"/> property.
		/// property.
		/// </remarks>
		[DefaultValue(0)]
		public Int32 DefaultButton
		{
			get { return defaultButton; }
			set {
				// Verify state
				VerifyPropertyWriteAccess("DefaultButton");

				// Verify value
				if (value < 0) {
					throw new ArgumentOutOfRangeException("value");
				}

				defaultButton = value;
			}
		}

		/// <summary>
		/// Gets or sets the default icon of this <see cref="TaskDialog"/>.
		/// </summary>
		/// <remarks>
		/// The value of this property has no effect on this <see cref="TaskDialog"/> if the
		/// <see cref="UseDefaultIcon"/> property is set to False.
		/// </remarks>
		[DefaultValue(TaskDialogIcon.None)]
		public TaskDialogIcon DefaultIcon {
			get { return defaultIcon; }
			set { defaultIcon = value; }
		}

		/// <summary>
		/// Gets or sets the default radio button of this <see cref="TaskDialog"/>.
		/// </summary>
		/// <remarks>
		/// The value of this property has no effect on this <see cref="TaskDialog"/> if the
		/// <see cref="RadioButtons"/> collection is empty.
		/// </remarks>
		[DefaultValue(0)]
		public Int32 DefaultRadioButton {
			get { return defaultRadioButton; }
			set {
				// Verify state
				VerifyPropertyWriteAccess("DefaultRadioButton");

				// Verify value
				if (value < -1) {
					throw new ArgumentOutOfRangeException("value");
				}

				defaultRadioButton = value;
			}
		}

		/// <summary>
		/// Gets or sets the default <see cref="TaskDialogState"/> of this <see cref="TaskDialog"/>.
		/// </summary>
		[DefaultValue(TaskDialogState.Collapsed)]
		public TaskDialogState DefaultState
		{
			get { return defaultState; }
			set {
				VerifyPropertyWriteAccess("DefaultState");
				defaultState = value;
			}
		}

		/// <summary>
		/// Gets or sets whether hyperlink processing is enabled for the <see cref="Content"/>,
		/// <see cref="ExpandedInformation"/>, and <see cref="Footer"/> properties.
		/// </summary>
		[DefaultValue(false)]
		public Boolean EnableHyperlinks {
			get { return enableHyperlinks; }
			set {
				VerifyPropertyWriteAccess("EnableHyperlinks");
				enableHyperlinks = value;
			}
		}

		/// <summary>
		/// Gets or sets whether the <see cref="Timer"/> event should be raised when this
		/// <see cref="TaskDialog"/> is activated.
		/// </summary>
		[DefaultValue(false)]
		public Boolean EnableTimer {
			get { return enableTimer; }
			set {
				VerifyPropertyWriteAccess("EnableTimer");
				enableTimer = value;
			}
		}

		/// <summary>
		/// Gets or sets the text that is displayed next to the expando when the <see cref="State"/>
		/// if this <see cref="TaskDialog"/> is Expanded.
		/// </summary>
		/// <remarks>
		/// The value of this property has no effect on this <see cref="TaskDialog"/> if the
		/// <see cref="ExpandedInformation"/> property has no content.
		/// </remarks>
		public String ExpandedControlText {
			get { return GetValue(ExpandedControlTextProperty) as String; }
			set {
				VerifyPropertyWriteAccess("ExpandedControlText");
				SetValue(ExpandedControlTextProperty, value);
			}
		}

		/// <summary>
		/// Gets or sets the expanded information text for this <see cref="TaskDialog"/>.
		/// </summary>
		/// <remarks>
		/// The expanded information section will only be visible on this <see cref="TaskDialog"/>
		/// if the value of this property is not null.
		/// </remarks>
		public Object ExpandedInformation {
			get { return GetValue(ExpandedInformationProperty); }
			set { SetValue(ExpandedInformationProperty, value); }
		}

		/// <summary>
		/// Gets or sets whether the <see cref="ExpandedInformation"/> text should be placed after
		/// the footer of this <see cref="TaskDialog"/> rather than in the content area.
		/// </summary>
		/// <remarks>
		/// The value of this property has no effect on this <see cref="TaskDialog"/> if the value
		/// of the <see cref="ExpandedInformation"/> property is null.
		/// </remarks>
		[DefaultValue(false)]
		public Boolean ExpandFooter
		{
			get { return expandFooter; }
			set {
				VerifyPropertyWriteAccess("ExpandFooter");
				expandFooter = value;
			}
		}

		/// <summary>
		/// Gets or sets the footer text of this <see cref="TaskDialog"/>.
		/// </summary>
		/// <remarks>
		/// The footer will only be visible on this <see cref="TaskDialog"/> if the value of this
		/// property is not null.
		/// </remarks>
		public Object Footer {
			get { return GetValue(FooterProperty); }
			set { SetValue(FooterProperty, value); }
		}

		/// <summary>
		/// Gets or sets the icon that is displyed next to the footer text of this
		/// <see cref="TaskDialog"/>.
		/// </summary>
		/// <remarks>
		/// The value of this property has no effect on this <see cref="TaskDialog"/> if the value
		/// of the <see cref="Footer"/> property is null.
		/// </remarks>
		public ImageSource FooterIcon {
			get { return GetValue(FooterIconProperty) as ImageSource; }
			set { SetValue(FooterIconProperty, value); }
		}

		internal IntPtr Handle {
			get { return handle; }
			set { handle = value; }
		}

		/// <summary>
		/// Determines whether this <see cref="TaskDialog"/> has a progress bar.
		/// </summary>
		public Boolean HasProgressBar {
			get { return progressBar != null; }
		}

		/// <summary>
		/// Gets or sets the icon of this <see cref="TaskDialog"/>.
		/// </summary>
		/// <remarks>
		/// The value of this property has no effect on this <see cref="TaskDialog"/> if the
		/// <see cref="UseDefaultIcon"/> property is set to True.
		/// </remarks>
		public ImageSource Icon {
			get { return GetValue(IconProperty) as ImageSource; }
			set { SetValue(IconProperty, value); }
		}

		/// <summary>
		/// Gets or sets the instruction text of this <see cref="TaskDialog"/>.
		/// </summary>
		public String Instruction {
			get { return GetValue(InstructionProperty) as String; }
			set { SetValue(InstructionProperty, value); }
		}

		/// <summary>
		/// Determines whether this <see cref="TaskDialog"/> is activated.
		/// </summary>
		public Boolean IsActivated {
			get { return activated; }
			private set { activated = value;  }
		}

		/// <summary>
		/// Determines whether this <see cref="TaskDialog"/> is expanded.
		/// </summary>
		public Boolean IsExpanded {
			get { return expanded; }
			private set { expanded = value; }
		}

		/// <summary>
		/// Determines whether this <see cref="TaskDialog"/> is initialized.
		/// </summary>
		public Boolean IsInitialized {
			get { return initialized; }
			private set { initialized = value; }
		}

		/// <summary>
		/// Determines whether the verficiation checkbox of this <see cref="TaskDialog"/> is
		/// checked.
		/// </summary>
		/// <remarks>
		/// The value of this property has no effect on this <see cref="TaskDialog"/> if the value
		/// of the <see cref="VerificationText"/> property is null.
		/// </remarks>
		[DefaultValue(false)]
		public Boolean IsVerificationChecked {
			get { return verificationChecked; }
			set {
				VerifyPropertyWriteAccess("VerificationChecked");
				verificationChecked = value;
			}
		}

		internal Window Owner {
			get { return owner; }
			set { owner = value; }
		}

		/// <summary>
		/// Gets or sets the <see cref="TaskDialogProgressBar"/> of this <see cref="TaskDialog"/>.
		/// </summary>
		public TaskDialogProgressBar ProgressBar {
			get { return progressBar; }
			set {
				VerifyPropertyWriteAccess("ProgressBar");
				progressBar = value;

				if (progressBar != null) {
					progressBar.Owner = this;
				}
			}
		}

		/// <summary>
		/// Gets a <see cref="TaskDialogElementCollection&lt;TaskDialogRadioButton&gt;"/> that
		/// contains the custom radio buttons of this <see cref="TaskDialog"/>.
		/// </summary>
		public TaskDialogElementCollection<TaskDialogRadioButton> RadioButtons {
			get { return radioButtons; }
		}

		/// <summary>
		/// Gets or sets whether the size of this <see cref="TaskDialog"/> is determined by its
		/// contents.
		/// </summary>
		[DefaultValue(false)]
		public Boolean SizeToContent {
			get { return sizeToContent; }
			set {
				VerifyPropertyWriteAccess("SizeToContent");
				sizeToContent = value;
			}
		}

		/// <summary>
		/// Determines whether this <see cref="TaskDialog"/> is collapsed or expanded.
		/// </summary>
		public TaskDialogState State {
			get { return IsExpanded ? TaskDialogState.Expanded : TaskDialogState.Collapsed; }
		}

		/// <summary>
		/// Gets or sets the title text of this <see cref="TaskDialog"/>.
		/// </summary>
		public String Title {
			get { return GetValue(TitleProperty) as String; }
			set { SetValue(TitleProperty, value); }
		}

		/// <summary>
		/// Gets or sets whether this <see cref="TaskDialog"/> should use the icon specified in the
		/// <see cref="DefaultIcon"/> property as its icon.
		/// </summary>
		[DefaultValue(true)]
		public Boolean UseDefaultIcon {
			get { return useDefaultIcon; }
			set { useDefaultIcon = value; }
		}

		/// <summary>
		/// Gets or sets the text that is displayed next to the verification checkbox on this
		/// <see cref="TaskDialog"/>.
		/// </summary>
		/// <remarks>
		/// The verification checkbox will only be visible on this <see cref="TaskDialog"/> if the
		/// value of this property is not null.
		/// </remarks>
		public String VerificationText {
			get { return GetValue(VerificationTextProperty) as String; }
			set { SetValue(VerificationTextProperty, value); }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Displays this <see cref="TaskDialog"/> and returns when it is closed.
		/// </summary>
		/// <returns>A <see cref="TaskDialogResult"/> value that represents result data.</returns>
		public TaskDialogResult Show()
		{
			// Validate state
			if (IsInitialized) {
				throw new InvalidOperationException("The current instance is already initialized.");
			}

			// Check if Common Controls version 6 is present (declared via manifest)
			NativeMethods.DLLVERSIONINFO dvi = new NativeMethods.DLLVERSIONINFO();
			dvi.cbSize = (UInt32)Marshal.SizeOf(typeof(NativeMethods.DLLVERSIONINFO));

			// Call DllGetVersion on common controls
			Int32 hr = NativeMethods.DllGetVersion(ref dvi);

			// Handle method call exceptions
			if (hr != NativeMethods.S_OK) {
				Marshal.ThrowExceptionForHR(hr);
			}

			// Check for major version 6 or greater
			if (!(dvi.dwMajorVersion >= 6)) {
				throw new NotSupportedException("Missing application manifest dependency "+
					                            "declaration for Microsoft Common Controls " +
					                            "version 6.");
			}

			// Initialize task dialog configuration
			NativeMethods.TASKDIALOGCONFIG taskDialogConfig = new NativeMethods.TASKDIALOGCONFIG();
			taskDialogConfig.cbSize = (UInt32)Marshal.SizeOf(typeof(NativeMethods.TASKDIALOGCONFIG));
			taskDialogConfig.hInstance = NativeMethods.GetModuleHandle();
			taskDialogConfig.pfCallback = TaskDialogProc;

			Debug.Assert(taskDialogConfig.hInstance != IntPtr.Zero);

			// Check if the task dialog has an owner window
			if (Owner != null) {
				WindowInteropHelper wih = new WindowInteropHelper(Owner);
				
				if (wih.Handle == IntPtr.Zero) {
					// Error: The owner window has either not been initialized or is closed.
					throw new InvalidOperationException("The owner window has either not been " +
						                                "initialized or is closed.");
				}

				taskDialogConfig.hWndParent = wih.Handle;
			}

			// Initialize flags
			if (AllowCancellation) {
				taskDialogConfig.dwFlags |=
					NativeMethods.TASKDIALOG_FLAGS.TDF_ALLOW_DIALOG_CANCELLATION;
			}

			switch (ButtonStyle) {
				case TaskDialogButtonStyle.CommandLinks:
					taskDialogConfig.dwFlags |=
						NativeMethods.TASKDIALOG_FLAGS.TDF_USE_COMMAND_LINKS;
					break;
				case TaskDialogButtonStyle.CommandLinksNoIcon:
					taskDialogConfig.dwFlags |=
						NativeMethods.TASKDIALOG_FLAGS.TDF_USE_COMMAND_LINKS_NO_ICON;
					break;
			}

			if (CanMinimize) {
				taskDialogConfig.dwFlags |=
					NativeMethods.TASKDIALOG_FLAGS.TDF_CAN_BE_MINIMIZED;
			}

			if (DefaultRadioButton == -1) {
				taskDialogConfig.dwFlags |=
					NativeMethods.TASKDIALOG_FLAGS.TDF_NO_DEFAULT_RADIO_BUTTON;
			}

			switch (DefaultState) {
				case TaskDialogState.Expanded:
					taskDialogConfig.dwFlags |=
						NativeMethods.TASKDIALOG_FLAGS.TDF_EXPANDED_BY_DEFAULT;
					break;
			}

			if (EnableHyperlinks) {
				taskDialogConfig.dwFlags |=
					NativeMethods.TASKDIALOG_FLAGS.TDF_ENABLE_HYPERLINKS;
			}

			if (EnableTimer) {
				taskDialogConfig.dwFlags |=
					NativeMethods.TASKDIALOG_FLAGS.TDF_CALLBACK_TIMER;
			}

			if (ExpandFooter) {
				taskDialogConfig.dwFlags |=
					NativeMethods.TASKDIALOG_FLAGS.TDF_EXPAND_FOOTER_AREA;
			}

			if (HasProgressBar) {
				taskDialogConfig.dwFlags |=
					NativeMethods.TASKDIALOG_FLAGS.TDF_SHOW_PROGRESS_BAR;

				switch (ProgressBar.Style) {
					case TaskDialogProgressBarStyle.Marquee:
						taskDialogConfig.dwFlags |=
							NativeMethods.TASKDIALOG_FLAGS.TDF_SHOW_MARQUEE_PROGRESS_BAR;
						break;
				}
			}

			if (IsVerificationChecked) {
				taskDialogConfig.dwFlags |=
					NativeMethods.TASKDIALOG_FLAGS.TDF_VERIFICATION_FLAG_CHECKED;
			}

			if (SizeToContent) {
				taskDialogConfig.dwFlags |=
					NativeMethods.TASKDIALOG_FLAGS.TDF_SIZE_TO_CONTENT;
			}

			// Prepare formatted elements, if necessary
			TaskDialogText taskDialogText;

			if (Content is TaskDialogText) {
				taskDialogText = (TaskDialogText)Content;
				foreach (TaskDialogTextElement element in taskDialogText.Contents) {
					element.Owner = this;
				}
			}

			if (ExpandedInformation is TaskDialogText) {
				taskDialogText = (TaskDialogText)ExpandedInformation;
				foreach (TaskDialogTextElement element in taskDialogText.Contents) {
					element.Owner = this;
				}
			}

			if (Footer is TaskDialogText) {
				taskDialogText = (TaskDialogText)Footer;
				foreach (TaskDialogTextElement element in taskDialogText.Contents) {
					element.Owner = this;
				}
			}

			// Initialize strings
			taskDialogConfig.pszWindowTitle = Title;
			taskDialogConfig.pszMainInstruction = Instruction;

			if (Content != null) {
				taskDialogConfig.pszContent = Content.ToString();
			}

			if (ExpandedInformation != null) {
				taskDialogConfig.pszExpandedInformation = ExpandedInformation.ToString();
			}

			if (Footer != null) {
				taskDialogConfig.pszFooter = Footer.ToString();
			}

			taskDialogConfig.pszCollapsedControlText = CollapsedControlText;
			taskDialogConfig.pszExpandedControlText = ExpandedControlText;
			taskDialogConfig.pszVerificationText = VerificationText;

			// Initialize common buttons
			taskDialogConfig.dwCommonButtons =
				(NativeMethods.TASKDIALOG_COMMON_BUTTON_FLAGS)CommonButtons;

			// Initialize icons
			if (UseDefaultIcon) {
				taskDialogConfig.hMainIcon = (IntPtr)(UInt16)DefaultIcon;
			} else if (Icon != null) {
				if (iconHandle != null) {
					if (!iconHandle.IsClosed) {
						iconHandle.Close();
					}
				}
				iconHandle = IconHandle.Create(Icon);
				taskDialogConfig.hMainIcon = iconHandle.Value;
				taskDialogConfig.dwFlags |=
					NativeMethods.TASKDIALOG_FLAGS.TDF_USE_HICON_MAIN;
			}

			if (FooterIcon != null) {
				if (footerIconHandle != null) {
					if (!footerIconHandle.IsClosed) {
						footerIconHandle.Close();
					}
				}
				footerIconHandle = IconHandle.Create(FooterIcon);
				taskDialogConfig.hFooterIcon = footerIconHandle.Value;
			}

			taskDialogConfig.dwFlags |= NativeMethods.TASKDIALOG_FLAGS.TDF_USE_HICON_FOOTER;

			// Initialize buttons
			IntPtr buttonPtr = IntPtr.Zero;

			if (Buttons.Count != 0) {
				// Allocate unmanaged memory for the button array
				buttonPtr = Marshal.AllocHGlobal(
					Marshal.SizeOf(typeof(NativeMethods.TASKDIALOG_BUTTON)) * Buttons.Count);

				IntPtr offset = buttonPtr;
				Int32 buttonID = 0;

				// Copy radio button data from managed list to unmanaged button array
				foreach (TaskDialogButton button in Buttons) {
					// Set owner
					button.Owner = this;

					// Create and initialize native wrapper structure
					NativeMethods.TASKDIALOG_BUTTON btn = new NativeMethods.TASKDIALOG_BUTTON();
					btn.nButtonID = buttonID;
					btn.pszButtonText = button.ToString();

					// Marshal to unmanaged memory
					Marshal.StructureToPtr(btn, offset, false);

					// Adjust offset and button ID
					offset = (IntPtr)((Int32)offset + Marshal.SizeOf(btn));
					buttonID++;
				}

				// Set button count and assign pointer to unmanaged button array
				taskDialogConfig.cButtons = (UInt32)Buttons.Count;
				taskDialogConfig.pButtons = buttonPtr;
			}

			taskDialogConfig.nDefaultButton = DefaultButton;

			// Initialize radio buttons
			IntPtr radioButtonPtr = IntPtr.Zero;

			if (RadioButtons.Count != 0) {
				// Allocate unmanaged memory for the radio button array
				radioButtonPtr = Marshal.AllocHGlobal(
					Marshal.SizeOf(typeof(NativeMethods.TASKDIALOG_BUTTON)) * RadioButtons.Count);

				IntPtr offset = radioButtonPtr;
				Int32 radioButtonID = 0;

				// Copy radio button data from managed list to unmanaged button array
				foreach (TaskDialogRadioButton radioButton in RadioButtons) {
					// Set owner
					radioButton.Owner = this;

					// Create and initialize native wrapper structure
					NativeMethods.TASKDIALOG_BUTTON btn = new NativeMethods.TASKDIALOG_BUTTON();
					btn.nButtonID = radioButtonID;
					btn.pszButtonText = radioButton.Title;

					// Marshal to unmanaged memory
					Marshal.StructureToPtr(btn, offset, false);

					// Adjust offset and radio button ID
					offset = (IntPtr)((Int32)offset + Marshal.SizeOf(btn));
					radioButtonID++;
				}

				// Set radio button count and assign pointer to unmanaged radio button array
				taskDialogConfig.cRadioButtons = (UInt32)RadioButtons.Count;
				taskDialogConfig.pRadioButtons = radioButtonPtr;
			}

			taskDialogConfig.nDefaultRadioButton = DefaultRadioButton;

			// Present task dialog
			Int32 selectedButton;
			Int32 selectedRadioButton;
			Boolean verificationChecked;
			
			hr = NativeMethods.TaskDialogIndirect(taskDialogConfig,
												  out selectedButton, out selectedRadioButton,
												  out verificationChecked);
			try { 
				// Check HRESULT for errors
				if (hr != NativeMethods.S_OK) {
					Marshal.ThrowExceptionForHR(hr);
				}
			} finally {
				// Clean up resources
				if (radioButtonPtr != IntPtr.Zero) {
					Marshal.FreeHGlobal(radioButtonPtr);
				}

				if (buttonPtr != IntPtr.Zero) {
					Marshal.FreeHGlobal(buttonPtr);
				}

				if (footerIconHandle != null) {
					if (!footerIconHandle.IsClosed) {
						footerIconHandle.Close();
					}
				}

				if (iconHandle != null) {
					if (!iconHandle.IsClosed) {
						iconHandle.Close();
					}
				}
			}
			
			return new TaskDialogResult(selectedButton, selectedRadioButton, verificationChecked);
		}

		/// <summary>
		/// Displays this <see cref="TaskDialog"/> in front of the specified window and returns
		/// when it is closed.
		/// </summary>
		/// <param name="owner">A <see cref="Window"/> that represents the owner of this task
		/// dialog.</param>
		/// <returns>A <see cref="TaskDialogResult"/> value that represents result data.</returns>
		public TaskDialogResult ShowModal(Window owner)
		{
			// Validate argument
			if (owner == null) {
				throw new ArgumentNullException("owner");
			}

			// Set owner and show the task dialog
			Owner = owner;
			return Show();
		}

		#endregion

		#region Methods: Implementation

		void InitializeElements()
		{
			// Initialize buttons
			foreach (TaskDialogButton button in Buttons) {
				if (!button.IsEnabled) {
					button.SetButtonEnabled(button.IsEnabled);
				}
				if (button.ElevationRequired) {
					button.SetElevationRequired(button.ElevationRequired);
				}
			}

			// Initialize radio buttons
			Int32 radioButtonID = 0;

			foreach (TaskDialogRadioButton radioButton in RadioButtons) {
				if (!radioButton.IsEnabled) {
					radioButton.SetButtonEnabled(radioButton.IsEnabled);
				}
				if (radioButtonID == DefaultRadioButton) {
					radioButton.IsChecked = true;
				}
				radioButtonID++;
			}

			// Initialize progress bar
			if (HasProgressBar) {
				ProgressBar.SetProgressBarStyle(ProgressBar.Style);

				switch (ProgressBar.Style) {
					case TaskDialogProgressBarStyle.Normal:
						ProgressBar.SetProgressBarRange(ProgressBar.Minimum, ProgressBar.Maximum);
						ProgressBar.SetProgressBarValue(ProgressBar.Value);
						ProgressBar.SetProgressBarState(ProgressBar.State);
						break;
					case TaskDialogProgressBarStyle.Marquee:
						ProgressBar.SetProgressBarMarquee(ProgressBar.RunMarquee,
													      ProgressBar.MarqueeInterval);
						break;
				}
			}
		}

		void SetElementText(NativeMethods.TASKDIALOG_ELEMENTS element, String text)
		{
			// Allocate unmanaged string
			IntPtr stringPtr = Marshal.StringToHGlobalUni(text);

			// Send TDM_SET_ELEMENT_TEXT message to set the element text
			NativeMethods.SendMessage(
				Handle, (UInt32)NativeMethods.TASKDIALOG_MESSAGES.TDM_SET_ELEMENT_TEXT,
				new UIntPtr((UInt32)element), stringPtr);

			// Free resources
			Marshal.FreeHGlobal(stringPtr);
		}

		void UpdateIcon(NativeMethods.TASKDIALOG_ICON_ELEMENTS iconElement, ImageSource imageSource)
		{
			IconHandle newHandle = IconHandle.Invalid;

			// Create replacement handle first
			switch (iconElement) {
				case NativeMethods.TASKDIALOG_ICON_ELEMENTS.TDIE_ICON_MAIN:
					newHandle = IconHandle.Create(imageSource);
					break;
				case NativeMethods.TASKDIALOG_ICON_ELEMENTS.TDIE_ICON_FOOTER:
					newHandle = IconHandle.Create(imageSource);
					break;
			}

			// Send TDM_UPDATE_ICON message to update the icon
			NativeMethods.SendMessage(
				Handle, (UInt32)NativeMethods.TASKDIALOG_MESSAGES.TDM_UPDATE_ICON,
				new UIntPtr((UInt32)iconElement), newHandle.Value);

			// Release previous icon handle and replace with new handle
			switch (iconElement) {
				case NativeMethods.TASKDIALOG_ICON_ELEMENTS.TDIE_ICON_MAIN:
					iconHandle.Close();
					iconHandle = newHandle;
					break;
				case NativeMethods.TASKDIALOG_ICON_ELEMENTS.TDIE_ICON_FOOTER:
					footerIconHandle.Close();
					footerIconHandle = newHandle;
					break;
			}
		}

		void VerifyPropertyWriteAccess(String propertyName)
		{
			// Allow property write access only on inactive state
			if (IsInitialized) {
				throw new InvalidOperationException(
					String.Format("The property '{0}' cannot be set when the current instance is " +
				                  "in an initialized state.", propertyName));
			}
		}

		#endregion

		#region Methods: Dependency Property Callbacks

		static void ContentPropertyChanged(DependencyObject source,
										   DependencyPropertyChangedEventArgs e)
		{
			TaskDialog taskDialog = source as TaskDialog;

			if (taskDialog != null) {
				if (taskDialog.IsActivated) {
					taskDialog.SetElementText(
						NativeMethods.TASKDIALOG_ELEMENTS.TDE_CONTENT,
						(String)e.NewValue);
				}
			}
		}

		static void ExpandedInformationPropertyChanged(DependencyObject source,
													   DependencyPropertyChangedEventArgs e)
		{
			TaskDialog taskDialog = source as TaskDialog;

			if (taskDialog != null) {
				if (taskDialog.IsActivated) {
					taskDialog.SetElementText(
						NativeMethods.TASKDIALOG_ELEMENTS.TDE_EXPANDED_INFORMATION,
						(String)e.NewValue);
				}
			}
		}

		static void FooterPropertyChanged(DependencyObject source,
										  DependencyPropertyChangedEventArgs e)
		{
			TaskDialog taskDialog = source as TaskDialog;

			if (taskDialog != null) {
				if (taskDialog.IsActivated) {
					taskDialog.SetElementText(
						NativeMethods.TASKDIALOG_ELEMENTS.TDE_FOOTER,
						(String)e.NewValue);
				}
			}
		}

		static void FooterIconPropertyChanged(DependencyObject source,
			                                  DependencyPropertyChangedEventArgs e)
		{
			TaskDialog taskDialog = source as TaskDialog;

			if (taskDialog != null) {
				if (taskDialog.IsActivated) {
					taskDialog.UpdateIcon(
						NativeMethods.TASKDIALOG_ICON_ELEMENTS.TDIE_ICON_FOOTER,
						(ImageSource)e.NewValue);
				}
			}
		}

		static void IconPropertyChanged(DependencyObject source,
										DependencyPropertyChangedEventArgs e)
		{
			TaskDialog taskDialog = source as TaskDialog;

			if (taskDialog != null) {
				if (taskDialog.IsActivated) {
					if (!taskDialog.UseDefaultIcon) {
						taskDialog.UpdateIcon(
							NativeMethods.TASKDIALOG_ICON_ELEMENTS.TDIE_ICON_MAIN,
							(ImageSource)e.NewValue);
					}
				}
			}
		}

		static void InstructionPropertyChanged(DependencyObject source,
											   DependencyPropertyChangedEventArgs e)
		{
			TaskDialog taskDialog = source as TaskDialog;

			if (taskDialog != null) {
				if (taskDialog.IsActivated) {
					taskDialog.SetElementText(
						NativeMethods.TASKDIALOG_ELEMENTS.TDE_MAIN_INSTRUCTION,
						(String)e.NewValue);
				}
			}
		}

		#endregion

		#region Methods: Event Handler

		/// <summary>
		/// Raises the <see cref="Activated"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected virtual void OnActivated(EventArgs e)
		{
			EventHandler handler = activatedEventHandler;

			if (handler != null) {
				handler(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Closed"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected virtual void OnClosed(EventArgs e)
		{
			EventHandler handler = closedEventHandler;

			if (handler != null) {
				handler(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Collapsed"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected virtual void OnCollapsed(EventArgs e)
		{
			EventHandler handler = collapsedEventHandler;

			if (handler != null) {
				handler(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Expanded"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected virtual void OnExpanded(EventArgs e)
		{
			EventHandler handler = expandedEventHandler;

			if (handler != null) {
				handler(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Help"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected virtual void OnHelp(EventArgs e)
		{
			EventHandler handler = helpEventHandler;

			if (handler != null) {
				handler(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Initialized"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected virtual void OnInitialized(EventArgs e)
		{
			EventHandler handler = initializedEventHandler;

			if (handler != null) {
				handler(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="Timer"/> event.
		/// </summary>
		/// <param name="e">An <see cref="TaskDialogTimerEventArgs"/> object that contains the
		/// timer event data.</param>
		protected virtual void OnTimer(TaskDialogTimerEventArgs e)
		{
			EventHandler<TaskDialogTimerEventArgs> handler = timerEventHandler;

			if (handler != null) {
				handler(this, e);
			}
		}

		/// <summary>
		/// Raises the <see cref="VerificationChanged"/> event.
		/// </summary>
		/// <param name="e">An <see cref="EventArgs"/> object that contains the event data.</param>
		protected virtual void OnVerificationChanged(EventArgs e)
		{
			EventHandler handler = verificationChangedEventHandler;

			if (handler != null) {
				handler(this, e);
			}
		}
		
		#endregion

		#region Methods: Static

		/// <summary>
		/// Displays a <see cref="TaskDialog"/>.
		/// </summary>
		/// <param name="instruction">A <see cref="String"/> that specifies the instruction text
		/// to display.</param>
		/// <returns>A <see cref="TaskDialogResult"/> value that specifies which common button
		/// was selected by the user.</returns>
		public static TaskDialogResult Show(String instruction)
		{
			return Show(instruction, null, null, TaskDialogButtons.None, TaskDialogIcon.None);
		}

		/// <summary>
		/// Displays a <see cref="TaskDialog"/>.
		/// </summary>
		/// <param name="instruction">A <see cref="String"/> that specifies the instruction text
		/// to display.</param>
		/// <param name="content">A <see cref="String"/> that specifies the content text to
		/// display.</param>
		/// <returns>A <see cref="TaskDialogResult"/> value that specifies which common button
		/// was selected by the user.</returns>
		public static TaskDialogResult Show(String instruction, String content)
		{
			return Show(instruction, content, null, TaskDialogButtons.None, TaskDialogIcon.None);
		}

		/// <summary>
		/// Displays a <see cref="TaskDialog"/>.
		/// </summary>
		/// <param name="instruction">A <see cref="String"/> that specifies the instruction text
		/// to display.</param>
		/// <param name="content">A <see cref="String"/> that specifies the content text to
		/// display.</param>
		/// <param name="title">A <see cref="String"/> that specifies the title bar text of the
		/// task dialog.</param>
		/// <returns>A <see cref="TaskDialogResult"/> value that specifies which common button
		/// was selected by the user.</returns>
		public static TaskDialogResult Show(String instruction, String content, String title)
		{
			return Show(instruction, content, title, TaskDialogButtons.None, TaskDialogIcon.None);
		}

		/// <summary>
		/// Displays a <see cref="TaskDialog"/>.
		/// </summary>
		/// <param name="instruction">A <see cref="String"/> that specifies the instruction text
		/// to display.</param>
		/// <param name="content">A <see cref="String"/> that specifies the content text to
		/// display.</param>
		/// <param name="title">A <see cref="String"/> that specifies the title bar text of the
		/// task dialog.</param>
		/// <param name="buttons">A <see cref="TaskDialogButtons"/> value that specifies which
		/// buttons to display.</param>
		/// <returns>A <see cref="TaskDialogResult"/> value that specifies which common button
		/// was selected by the user.</returns>
		public static TaskDialogResult Show(String instruction, String content, String title,
			                                TaskDialogButtons buttons)
		{
			return Show(instruction, content, title, buttons, TaskDialogIcon.None);
		}

		/// <summary>
		/// Displays a <see cref="TaskDialog"/>.
		/// </summary>
		/// <param name="instruction">A <see cref="String"/> that specifies the instruction text
		/// to display.</param>
		/// <param name="content">A <see cref="String"/> that specifies the content text to
		/// display.</param>
		/// <param name="title">A <see cref="String"/> that specifies the title bar text of the
		/// task dialog.</param>
		/// <param name="buttons">A <see cref="TaskDialogButtons"/> value that specifies which
		/// buttons to display.</param>
		/// <param name="icon">A <see cref="TaskDialogIcon"/> value that specifies the icon to
		/// display.</param>
		/// <returns>A <see cref="TaskDialogResult"/> value that specifies which common button
		/// was selected by the user.</returns>
		public static TaskDialogResult Show(String instruction, String content, String title,
			                                TaskDialogButtons buttons, TaskDialogIcon icon)
		{
			// Initialize task dialog instance
			TaskDialog taskDialog = new TaskDialog();
			taskDialog.Title = title;
			taskDialog.Instruction = instruction;
			taskDialog.Content = content;
			taskDialog.CommonButtons = buttons;
			taskDialog.DefaultIcon = icon;

			// Show instance and return result
			return taskDialog.Show();
		}

		/// <summary>
		/// Displays a <see cref="TaskDialog"/> in front of the specified window.
		/// </summary>
		/// <param name="owner">A <see cref="Window"/> that represents the owner window of the
		/// task dialog.</param>
		/// <param name="instruction">A <see cref="String"/> that specifies the instruction text
		/// to display.</param>
		/// <returns>A <see cref="TaskDialogResult"/> value that specifies which common button
		/// was selected by the user.</returns>
		public static TaskDialogResult ShowModal(Window owner, String instruction)
		{
			return ShowModal(owner, instruction, null, null, TaskDialogButtons.None,
				              TaskDialogIcon.None);
		}

		/// <summary>
		/// Displays a <see cref="TaskDialog"/> in front of the specified window.
		/// </summary>
		/// <param name="owner">A <see cref="Window"/> that represents the owner window of the
		/// task dialog.</param>
		/// <param name="instruction">A <see cref="String"/> that specifies the instruction text
		/// to display.</param>
		/// <param name="content">A <see cref="String"/> that specifies the content text to
		/// display.</param>
		/// <returns>A <see cref="TaskDialogResult"/> value that specifies which common button
		/// was selected by the user.</returns>
		public static TaskDialogResult ShowModal(Window owner, String instruction, String content)
		{
			return ShowModal(owner, instruction, content, null, TaskDialogButtons.None,
							  TaskDialogIcon.None);
		}

		/// <summary>
		/// Displays a <see cref="TaskDialog"/> in front of the specified window.
		/// </summary>
		/// <param name="owner">A <see cref="Window"/> that represents the owner window of the
		/// task dialog.</param>
		/// <param name="instruction">A <see cref="String"/> that specifies the instruction text
		/// to display.</param>
		/// <param name="content">A <see cref="String"/> that specifies the content text to
		/// display.</param>
		/// <param name="title">A <see cref="String"/> that specifies the title bar text of the
		/// task dialog.</param>
		/// <returns>A <see cref="TaskDialogResult"/> value that specifies which common button
		/// was selected by the user.</returns>
		public static TaskDialogResult ShowModal(Window owner, String instruction, String content,
			                                     String title)
		{
			return ShowModal(owner, instruction, content, title, TaskDialogButtons.None,
							  TaskDialogIcon.None);
		}

		/// <summary>
		/// Displays a <see cref="TaskDialog"/> in front of the specified window.
		/// </summary>
		/// <param name="owner">A <see cref="Window"/> that represents the owner window of the
		/// task dialog.</param>
		/// <param name="instruction">A <see cref="String"/> that specifies the instruction text
		/// to display.</param>
		/// <param name="content">A <see cref="String"/> that specifies the content text to
		/// display.</param>
		/// <param name="title">A <see cref="String"/> that specifies the title bar text of the
		/// task dialog.</param>
		/// <param name="buttons">A <see cref="TaskDialogButtons"/> value that specifies which
		/// buttons to display.</param>
		/// <returns>A <see cref="TaskDialogResult"/> value that specifies which common button
		/// was selected by the user.</returns>
		public static TaskDialogResult ShowModal(Window owner, String instruction, String content,
			                                     String title, TaskDialogButtons buttons)
		{
			return ShowModal(owner, instruction, content, title, buttons, TaskDialogIcon.None);
		}

		/// <summary>
		/// Displays a <see cref="TaskDialog"/> in front of the specified window.
		/// </summary>
		/// <param name="owner">A <see cref="Window"/> that represents the owner window of the
		/// task dialog.</param>
		/// <param name="instruction">A <see cref="String"/> that specifies the instruction text
		/// to display.</param>
		/// <param name="content">A <see cref="String"/> that specifies the content text to
		/// display.</param>
		/// <param name="title">A <see cref="String"/> that specifies the title bar text of the
		/// task dialog.</param>
		/// <param name="buttons">A <see cref="TaskDialogButtons"/> value that specifies which
		/// buttons to display.</param>
		/// <param name="icon">A <see cref="TaskDialogIcon"/> value that specifies the icon to
		/// display.</param>
		/// <returns>A <see cref="TaskDialogResult"/> value that specifies which common button
		/// was selected by the user.</returns>
		public static TaskDialogResult ShowModal(Window owner, String instruction, String content,
			                                     String title, TaskDialogButtons buttons,
			                                     TaskDialogIcon icon)
		{
			// Validate arguments
			if (owner == null) {
				throw new ArgumentNullException("owner");
			}

			// Initialize task dialog instance
			TaskDialog taskDialog = new TaskDialog();
			taskDialog.Title = title;
			taskDialog.Instruction = instruction;
			taskDialog.Content = content;
			taskDialog.CommonButtons = buttons;
			taskDialog.DefaultIcon = icon;

			// Show instance and return result
			return taskDialog.ShowModal(owner);
		}

		#endregion

		#region Methods: TaskDialog Callback Procedure

		Int32 TaskDialogProc(IntPtr hwnd, UInt32 uNotification, UIntPtr wParam, IntPtr lParam,
			                 IntPtr dwRefData)
		{
			Int32 result = NativeMethods.S_OK;

			// Cast notification value
			NativeMethods.TASKDIALOG_NOTIFICATIONS notification =
				(NativeMethods.TASKDIALOG_NOTIFICATIONS)uNotification;

			// Handle notification
			switch (notification) {
				case NativeMethods.TASKDIALOG_NOTIFICATIONS.TDN_BUTTON_CLICKED:
					// Get button ID
					Int32 buttonID = (Int32)wParam;

					// Lookup button instance from collection, if necessary
					if (Buttons.Count != 0 && buttonID < Buttons.Count) {
						TaskDialogButton button = Buttons[buttonID];

						// Raise Click event on button
						button.RaiseClickEvent();

						// Does the button prevent the dialog from closing?
						if (button.PreventClose) {
							// Note: According to MSDN, one has to return E_FAIL to prevent the task
							// dialog from closing, but actually you have to return S_FALSE as using
							// E_FAIL will cause the task dialog function to fail.
							result = NativeMethods.S_FALSE;
						}
					}

					break;

				case NativeMethods.TASKDIALOG_NOTIFICATIONS.TDN_CREATED:
					// Set IsActivated property
					IsActivated = true;

					// Raise Activated event
					OnActivated(EventArgs.Empty);

					break;

				case NativeMethods.TASKDIALOG_NOTIFICATIONS.TDN_DESTROYED:
					// Remove handle reference
					Handle = IntPtr.Zero;

					// Mark as inactive and uninitialized
					IsActivated = false;
					IsInitialized = false;

					// Raise Closed event
					OnClosed(EventArgs.Empty);

					break;

				case NativeMethods.TASKDIALOG_NOTIFICATIONS.TDN_DIALOG_CONSTRUCTED:
					// Set Handle property
					Handle = hwnd;

					// Initialize elements and set IsInitialized property
					InitializeElements();
					IsInitialized = true;

					// Raise Initialized event
					OnInitialized(EventArgs.Empty);

					break;

				case NativeMethods.TASKDIALOG_NOTIFICATIONS.TDN_EXPANDO_BUTTON_CLICKED:
					// Set IsExpanded property
					IsExpanded = ((Int32)wParam != 0);

					// Raise Expanded/Collapsed event according to expansion state
					if (expanded) {
						OnExpanded(EventArgs.Empty);
					} else {
						OnCollapsed(EventArgs.Empty);
					}

					break;

				case NativeMethods.TASKDIALOG_NOTIFICATIONS.TDN_HELP:
					// Raise Help event
					OnHelp(EventArgs.Empty);

					break;

				case NativeMethods.TASKDIALOG_NOTIFICATIONS.TDN_HYPERLINK_CLICKED:
					// Marshal URL string
					String url = Marshal.PtrToStringUni(lParam);

					// Find corresponding link and raise its Click event
					Boolean eventRaised = false;
					TaskDialogText taskDialogText;
					TaskDialogLink taskDialogLink;

					if (Content is TaskDialogText) {
						// Check Content for event source
						taskDialogText = (TaskDialogText)Content;
						foreach (TaskDialogTextElement element in taskDialogText.Contents) {
							if (element is TaskDialogLink) {
								taskDialogLink = (TaskDialogLink)element;
								if (url.Equals(taskDialogLink.Uri.ToString())) {
									taskDialogLink.RaiseClickEvent();
									eventRaised = true;
									break;
								}
							}
						}
						if (eventRaised) {
							break;
						}
					}

					if (ExpandedInformation is TaskDialogText) {
						// Check ExpandedInformation for event source
						taskDialogText = (TaskDialogText)ExpandedInformation;
						foreach (TaskDialogTextElement element in taskDialogText.Contents) {
							if (element is TaskDialogLink) {
								taskDialogLink = (TaskDialogLink)element;
								if (url.Equals(taskDialogLink.Uri.ToString())) {
									taskDialogLink.RaiseClickEvent();
									eventRaised = true;
									break;
								}
							}
						}
						if (eventRaised) {
							break;
						}
					}

					if (Footer is TaskDialogText) {
						// Check Footer for event source
						taskDialogText = (TaskDialogText)Footer;
						foreach (TaskDialogTextElement element in taskDialogText.Contents) {
							if (element is TaskDialogLink) {
								taskDialogLink = (TaskDialogLink)element;
								if (url.Equals(taskDialogLink.Uri.ToString())) {
									taskDialogLink.RaiseClickEvent();
									break;
								}
							}
						}
					}

					break;

				case NativeMethods.TASKDIALOG_NOTIFICATIONS.TDN_RADIO_BUTTON_CLICKED:
					// Get radio button ID
					Int32 radioButtonID = (Int32)wParam;

					// Lookup radio button instance from collection
					TaskDialogRadioButton radioButton = RadioButtons[radioButtonID];

					// Update IsChecked property
					foreach (TaskDialogRadioButton radioBtn in RadioButtons) {
						// Remove check state from all but the clicked radio button
						if (radioButton != radioBtn) {
							radioBtn.IsChecked = false;
						}
					}

					radioButton.IsChecked = true;

					// Raise Click event on radio button
					radioButton.RaiseClickEvent();

					break;

				case NativeMethods.TASKDIALOG_NOTIFICATIONS.TDN_TIMER:
					// Create and initialize event arguments object
					Int32 tickCount = (Int32)wParam;
					TaskDialogTimerEventArgs timerEventArgs = new TaskDialogTimerEventArgs(tickCount);

					// Raise Timer event
					OnTimer(timerEventArgs);

					// Check reset property of event arguments
					// If set to true by the callee, the timer will be reset
					if (timerEventArgs.Reset) {
						// Reset indicated by S_FALSE result
						result = NativeMethods.S_FALSE;
					}

					break;

				case NativeMethods.TASKDIALOG_NOTIFICATIONS.TDN_VERIFICATION_CLICKED:
					// Check verification property
					verificationChecked = ((Int32)wParam != 0);

					// Raise VerificationChanged event
					OnVerificationChanged(EventArgs.Empty);

					break;
			}

			return result;
		}

		#endregion
	}
}
