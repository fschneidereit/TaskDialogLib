/***************************************************************************************************
 *
 *  Flatcode Task Dialog Library
 *  Copyright © 2014 Flatcode.net. All Rights Reserved.
 *
 *  File:
 *    NativeMethods.cs
 *  Purpose:
 *    Native API wrappers.
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
using System.Runtime.InteropServices;

#endregion

namespace Flatcode.Presentation
{
	static class NativeMethods
	{
		#region Structures

		[StructLayout(LayoutKind.Sequential)]
		public struct BITMAPINFO
		{
			public BITMAPINFOHEADER bmiHeader;

			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
			public RGBQUAD[] bmiColors;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct BITMAPINFOHEADER
		{
			public UInt32 biSize;
			public Int32 biWidth;
			public Int32 biHeight;
			public UInt16 biPlanes;
			public UInt16 biBitCount;
			public UInt32 biCompression;
			public UInt32 biSizeImage;
			public Int32 biXPelsPerMeter;
			public Int32 biYPelsPerMeter;
			public UInt32 biClrUsed;
			public UInt32 biClrImportant;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct DLLVERSIONINFO
		{
			public UInt32 cbSize;
			public UInt32 dwMajorVersion;
			public UInt32 dwMinorVersion;
			public UInt32 dwBuildNumber;
			public UInt32 dwPlatformID;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct ICONINFO
		{
			public Boolean fIcon;
			public UInt32 xHotspot;
			public UInt32 yHotspot;
			public IntPtr hbmMask;
			public IntPtr hbmColor;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct RGBQUAD
		{
			public Byte rgbBlue;
			public Byte rgbGreen;
			public Byte rgbRed;
			public Byte rgbReserved;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
		public class TASKDIALOGCONFIG
		{
			public UInt32 cbSize;
			public IntPtr hWndParent;
			public IntPtr hInstance;
			public TASKDIALOG_FLAGS dwFlags;
			public TASKDIALOG_COMMON_BUTTON_FLAGS dwCommonButtons;

			[MarshalAs(UnmanagedType.LPWStr)]
			public String pszWindowTitle;

			public IntPtr hMainIcon;

			[MarshalAs(UnmanagedType.LPWStr)]
			public String pszMainInstruction;
			[MarshalAs(UnmanagedType.LPWStr)]
			public String pszContent;

			public UInt32 cButtons;
			public IntPtr pButtons;
			public Int32 nDefaultButton;
			public UInt32 cRadioButtons;
			public IntPtr pRadioButtons;
			public Int32 nDefaultRadioButton;

			[MarshalAs(UnmanagedType.LPWStr)]
			public String pszVerificationText;
			[MarshalAs(UnmanagedType.LPWStr)]
			public String pszExpandedInformation;
			[MarshalAs(UnmanagedType.LPWStr)]
			public String pszExpandedControlText;
			[MarshalAs(UnmanagedType.LPWStr)]
			public String pszCollapsedControlText;

			public IntPtr hFooterIcon;

			[MarshalAs(UnmanagedType.LPWStr)]
			public String pszFooter;

			public TaskDialogCallbackProc pfCallback;
			public IntPtr lpCallbackData;
			public UInt32 cxWidth;
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
		public struct TASKDIALOG_BUTTON
		{
			public Int32 nButtonID;

			[MarshalAs(UnmanagedType.LPWStr)]
			public String pszButtonText;
		}

		#endregion

		#region Enumerations

		[Flags]
		public enum TASKDIALOG_COMMON_BUTTON_FLAGS : uint
		{
			TDCBF_OK_BUTTON = 0x0001,
			TDCBF_YES_BUTTON = 0x0002,
			TDCBF_NO_BUTTON = 0x0004,
			TDCBF_CANCEL_BUTTON = 0x0008,
			TDCBF_RETRY_BUTTON = 0x0010,
			TDCBF_CLOSE_BUTTON = 0x0020 
		}

		public enum TASKDIALOG_ELEMENTS : uint
		{
			TDE_CONTENT = 0,
			TDE_EXPANDED_INFORMATION = 1,
			TDE_FOOTER = 2,
			TDE_MAIN_INSTRUCTION = 3
		}

		[Flags]
		public enum TASKDIALOG_FLAGS : uint
		{
			TDF_ENABLE_HYPERLINKS = 0x00000001,
			TDF_USE_HICON_MAIN = 0x00000002,
			TDF_USE_HICON_FOOTER = 0x00000004,
			TDF_ALLOW_DIALOG_CANCELLATION = 0x00000008,
			TDF_USE_COMMAND_LINKS = 0x00000010,
			TDF_USE_COMMAND_LINKS_NO_ICON = 0x00000020,
			TDF_EXPAND_FOOTER_AREA = 0x00000040,
			TDF_EXPANDED_BY_DEFAULT = 0x00000080,
			TDF_VERIFICATION_FLAG_CHECKED = 0x00000100,
			TDF_SHOW_PROGRESS_BAR = 0x00000200,
			TDF_SHOW_MARQUEE_PROGRESS_BAR = 0x00000400,
			TDF_CALLBACK_TIMER = 0x00000800,
			TDF_POSITION_RELATIVE_TO_WINDOW = 0x00001000,
			TDF_RTL_LAYOUT = 0x00002000,
			TDF_NO_DEFAULT_RADIO_BUTTON = 0x00004000,
			TDF_CAN_BE_MINIMIZED = 0x00008000,
			TDF_NO_SET_FOREGROUND = 0x00010000,
			TDF_SIZE_TO_CONTENT = 0x01000000
		}

		public enum TASKDIALOG_ICON_ELEMENTS : uint
		{
			TDIE_ICON_MAIN = 0,
			TDIE_ICON_FOOTER = 1
		}

		public enum TASKDIALOG_MESSAGES : uint
		{
			TDM_NAVIGATE_PAGE = WM_USER + 101,
			TDM_CLICK_BUTTON = WM_USER + 102,
			TDM_SET_MARQUEE_PROGRESS_BAR = WM_USER + 103,
			TDM_SET_PROGRESS_BAR_STATE = WM_USER + 104,
			TDM_SET_PROGRESS_BAR_RANGE = WM_USER + 105,
			TDM_SET_PROGRESS_BAR_POS = WM_USER + 106,
			TDM_SET_PROGRESS_BAR_MARQUEE = WM_USER + 107,
			TDM_SET_ELEMENT_TEXT = WM_USER + 108,
			TDM_CLICK_RADIO_BUTTON = WM_USER + 110,
			TDM_ENABLE_BUTTON = WM_USER + 111,
			TDM_ENABLE_RADIO_BUTTON = WM_USER + 112,
			TDM_CLICK_VERIFICATION = WM_USER + 113,
			TDM_UPDATE_ELEMENT_TEXT = WM_USER + 114,
			TDM_SET_BUTTON_ELEVATION_REQUIRED_STATE = WM_USER + 115,
			TDM_UPDATE_ICON = WM_USER + 116
		}

		public enum TASKDIALOG_NOTIFICATIONS : uint
		{
			TDN_CREATED = 0,
			TDN_NAVIGATED = 1,
			TDN_BUTTON_CLICKED = 2,
			TDN_HYPERLINK_CLICKED = 3,
			TDN_TIMER = 4,
			TDN_DESTROYED = 5,
			TDN_RADIO_BUTTON_CLICKED = 6,
			TDN_DIALOG_CONSTRUCTED = 7,
			TDN_VERIFICATION_CLICKED = 8,
			TDN_HELP = 9,
			TDN_EXPANDO_BUTTON_CLICKED = 10 
		}

		#endregion

		#region Delegates

		[return: MarshalAs(UnmanagedType.Error)]
		public delegate Int32 TaskDialogCallbackProc(
			IntPtr hwnd, UInt32 uNotification, UIntPtr wParam, IntPtr lParam, IntPtr dwRefData);

		#endregion

		#region Constants

		// Common HRESULT values
		public const Int32 S_OK = 0;
		public const Int32 S_FALSE = 1;
		public const Int32 E_FAIL = unchecked((Int32)0x80004005);

		// Dialog box command IDs
		public const Int32 IDOK = 1;
		public const Int32 IDCANCEL = 2;
		public const Int32 IDRETRY = 4;
		public const Int32 IDYES = 6;
		public const Int32 IDNO = 7;
		public const Int32 IDCLOSE = 8;

		// Progress bar states
		public const Int32 PBST_NORMAL = 0x0001;
		public const Int32 PBST_ERROR = 0x0002;
		public const Int32 PBST_PAUSED = 0x0003;

		// Predefined icon values
		public const UInt16 TD_WARNING_ICON = 65535;
		public const UInt16 TD_ERROR_ICON = 65534;
		public const UInt16 TD_INFORMATION_ICON = 65533;
		public const UInt16 TD_SHIELD_ICON = 65532;

		// Window messages
		public const UInt32 WM_USER = 0x0400;

		#endregion

		#region Methods

		[DllImport(
			ExternDll.Gdi32,
			CallingConvention = CallingConvention.Winapi,
			CharSet = CharSet.Auto)]
		public static extern IntPtr CreateBitmap(
			[In] Int32 nWidth,
			[In] Int32 nHeight,
			[In] UInt32 cPlanes,
			[In] UInt32 cBitsPerPel,
			[In, Optional] IntPtr lpvBits);

		[DllImport(
			ExternDll.Gdi32,
			CallingConvention = CallingConvention.Winapi,
			CharSet = CharSet.Auto)]
		public static extern IntPtr CreateDIBSection(
			[In, Optional] IntPtr hdc,
			[In] ref BITMAPINFO pbmi,
			[In] UInt32 usage,
			[Out] out IntPtr ppvBits,
			[In, Optional] IntPtr hSection,
			[In] UInt32 dwOffset);

		[DllImport(
			ExternDll.User32,
			CallingConvention = CallingConvention.Winapi,
			CharSet = CharSet.Auto,
			SetLastError = true)]
		public static extern IntPtr CreateIconIndirect(
			[In] ref ICONINFO piconinfo);

		[DllImport(
			ExternDll.Gdi32,
			CallingConvention = CallingConvention.Winapi,
			CharSet = CharSet.Auto)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean DeleteObject(
			[In] IntPtr hObject);

		[DllImport(
			ExternDll.User32,
			CallingConvention = CallingConvention.Winapi,
			CharSet = CharSet.Auto,
			SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern Boolean DestroyIcon(
			[In] IntPtr hIcon);

		[DllImport(
			ExternDll.ComCtl32,
			CallingConvention = CallingConvention.StdCall,
			CharSet = CharSet.Auto,
			PreserveSig = true)]
		[return: MarshalAs(UnmanagedType.Error)]
		public static extern Int32 DllGetVersion(
			ref DLLVERSIONINFO pdvi);

		[DllImport(
			ExternDll.Kernel32,
			CallingConvention = CallingConvention.Winapi,
			CharSet = CharSet.Auto,
			SetLastError = true)]
		public static extern IntPtr GetModuleHandle(
			[In, Optional] String lpModuleName);

		[DllImport(
			ExternDll.User32,
			CallingConvention = CallingConvention.Winapi,
			CharSet = CharSet.Auto,
			SetLastError = true)]
		public static extern IntPtr SendMessage(
			[In] IntPtr hWnd,
			[In] UInt32 Msg,
			[In] UIntPtr wParam,
			[In] IntPtr lParam);

		[DllImport(
			ExternDll.ComCtl32,
			CallingConvention = CallingConvention.Winapi,
			CharSet = CharSet.Auto,
			PreserveSig = true)]
		[return: MarshalAs(UnmanagedType.Error)]
		public static extern Int32 TaskDialogIndirect(
			[In] TASKDIALOGCONFIG pTaskConfig,
			[Out, Optional] out Int32 pnButton,
			[Out, Optional] out Int32 pnRadioButton,
			[Out, Optional, MarshalAs(UnmanagedType.Bool)] out Boolean pfVerificationFlagChecked);

		#endregion
	}
}
