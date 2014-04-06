/***************************************************************************************************
 *
 *  TaskDialog Library
 *  Copyright © 2014 Florian Schneidereit. All Rights Reserved.
 *
 *  File:
 *    IconHandle.cs
 *  Purpose:
 *    Icon handle object.
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
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

#endregion

namespace TaskDialogLib
{
	sealed class IconHandle : SafeHandle
	{
		public static readonly IconHandle Invalid = new IconHandle();

		#region Constructors

		IconHandle() : base(IntPtr.Zero, true)
		{
		}

		IconHandle(IntPtr handle) : this()
		{
			this.handle = handle;
		}

		#endregion

		#region Properties

		public IntPtr Value {
			get { return handle; }
		}

		#endregion

		#region Properties: Overridden

		public override Boolean IsInvalid {
			get { return handle == IntPtr.Zero; }
		}

		#endregion

		#region Methods: Overridden

		protected override Boolean ReleaseHandle()
		{
			return NativeMethods.DestroyIcon(handle);
		}

		#endregion

		#region Methods: Static

		public static IconHandle Create(ImageSource imageSource)
		{
			IconHandle result = IconHandle.Invalid;

			if (imageSource != null) {
				// Initialize bitmap rectangle and try to cast image source to bitmap frame
				Rect bitmapRect = new Rect(new Size(imageSource.Width, imageSource.Height));
				BitmapFrame bitmapFrame = imageSource as BitmapFrame;

				if (bitmapFrame == null || !(bitmapFrame.Decoder is IconBitmapDecoder)) {
					// Create drawing visual
					DrawingVisual drawingVisual = new DrawingVisual();

					// Open drawing context and draw the image to the visual
					using (DrawingContext drawingContext = drawingVisual.RenderOpen()) {
						// Render the image and close drawing context
						drawingContext.DrawImage(imageSource, bitmapRect);
					}

					// Create render target bitmap and render the visual
					RenderTargetBitmap renderTargetBitmap =
						new RenderTargetBitmap((Int32)bitmapRect.Width, (Int32)bitmapRect.Height,
											   96.0d, 96.0d, PixelFormats.Pbgra32);

					renderTargetBitmap.Render(drawingVisual);
					renderTargetBitmap.Freeze();

					// Create bitmap frame from the render target bitmap
					bitmapFrame = BitmapFrame.Create(renderTargetBitmap);
				}

				// Copy pixels from bitmap frame to pixel array
				Int32 stride = (bitmapFrame.Format.BitsPerPixel * bitmapFrame.PixelWidth + 31) / 32 * 4;
				Byte[] pixelArray = new Byte[stride * bitmapFrame.PixelHeight];
				bitmapFrame.CopyPixels(pixelArray, stride, 0);

				// Mask & icon bitmap handles
				IntPtr maskBitmapPtr = IntPtr.Zero;
				IntPtr iconBitmapPtr = IntPtr.Zero;

				// Initialize BITMAPINFO/BITMAPINFOHEADER structure
				NativeMethods.BITMAPINFO bmi = new NativeMethods.BITMAPINFO();
				bmi.bmiHeader.biSize = (UInt32)Marshal.SizeOf(typeof(NativeMethods.BITMAPINFOHEADER));
				bmi.bmiHeader.biWidth = (Int32)bitmapRect.Width;
				bmi.bmiHeader.biHeight -= ((Int32)bitmapRect.Height);
				bmi.bmiHeader.biPlanes = 1;
				bmi.bmiHeader.biBitCount = (UInt16)bitmapFrame.Format.BitsPerPixel;

				// DIB handle
				IntPtr dibPtr = IntPtr.Zero;

				// Create icon bitmap DIB section
				iconBitmapPtr = NativeMethods.CreateDIBSection(IntPtr.Zero, ref bmi, 0, out dibPtr,
															   IntPtr.Zero, 0);

				// Validate icon bitmap handle
				if (iconBitmapPtr != IntPtr.Zero && dibPtr != IntPtr.Zero) {
					// Copy pixels to unmanaged memory
					Marshal.Copy(pixelArray, 0, dibPtr, pixelArray.Length);

					// Create mask bitmap
					maskBitmapPtr = NativeMethods.CreateBitmap((Int32)bitmapRect.Width,
															   (Int32)bitmapRect.Height,
															   1, 1, IntPtr.Zero);

					if (maskBitmapPtr != IntPtr.Zero) {
						// Create and initialize ICONINFO structure
						NativeMethods.ICONINFO iconInfo = new NativeMethods.ICONINFO();
						iconInfo.fIcon = true;
						iconInfo.hbmMask = maskBitmapPtr;
						iconInfo.hbmColor = iconBitmapPtr;

						// Create icon handle
						IntPtr iconPtr = NativeMethods.CreateIconIndirect(ref iconInfo);

						if (iconPtr != IntPtr.Zero) {
							result = new IconHandle(iconPtr);
						}
					}
				}

				// Clean up resources
				if (maskBitmapPtr != IntPtr.Zero) {
					NativeMethods.DeleteObject(maskBitmapPtr);
				}

				if (iconBitmapPtr != IntPtr.Zero) {
					NativeMethods.DeleteObject(iconBitmapPtr);
				}
			}

			// Return result
			return result;
		}

		#endregion
	}
}
