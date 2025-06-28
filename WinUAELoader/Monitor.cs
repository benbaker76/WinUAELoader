// Copyright (c) 2008, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WinUAELoader
{
    public class Screen
    {
        public int Width = 0;
        public int Height = 0;

        public Screen(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return String.Format("{0}x{1}", Width, Height);
        }
    }

    public class DisplayMode
    {
        public int Width = 0;
        public int Height = 0;
        public int BitDepth = 0;
        public int Frequency = 0;

        public DisplayMode(int width, int height, int bitDepth, int frequency)
        {
            Width = width;
            Height = height;
            BitDepth = bitDepth;
            Frequency = frequency;
        }

        public override string ToString()
        {
            return String.Format("{0} x {1}, {2} bit, {3} Hz", Width, Height, BitDepth, Frequency);
        }
    }

    public class Monitor
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        [DllImport("user32.dll")]
        private static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

        public static DisplayMode[] EnumDisplaySettings()
        {
            List<DisplayMode> displayMode = new List<DisplayMode>();
            DEVMODE vDevMode = new DEVMODE();

            int i = 0;

            while (EnumDisplaySettings(null, i++, ref vDevMode))
                displayMode.Add(new DisplayMode(vDevMode.dmPelsWidth, vDevMode.dmPelsHeight, vDevMode.dmBitsPerPel, vDevMode.dmDisplayFrequency));

            return displayMode.ToArray();
        }
    }
}
