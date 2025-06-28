// Copyright (c) 2008, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace WinUAELoader
{
    class RawInput
    {
        public enum RawDeviceType
        {
            Keyboard,
            Mouse,
            HID
        };

        private const int RIDI_DEVICENAME = 0x20000007;
        private const int RIDI_DEVICEINFO = 0x2000000b;

        private const int RIM_TYPEMOUSE = 0;
        private const int RIM_TYPEKEYBOARD = 1;
        private const int RIM_TYPEHID = 2;

        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWINPUTDEVICELIST
        {
            public IntPtr hDevice;
            [MarshalAs(UnmanagedType.U4)]
            public int dwType;
        }

        [DllImport("User32.dll")]
        private extern static uint GetRawInputDeviceList(IntPtr pRawInputDeviceList, ref uint uiNumDevices, uint cbSize);
        
        [DllImport("User32.dll")]
        private extern static uint GetRawInputDeviceInfo(IntPtr hDevice, uint uiCommand, IntPtr pData, ref uint pcbSize);

        public static string[] GetRawInputDeviceName(RawDeviceType rawDeviceType, bool bFriendlyName)
        {
            List<string> retList = new List<string>();

            try
            {
                uint deviceCount = 0;
                int dwSize = (Marshal.SizeOf(typeof(RAWINPUTDEVICELIST)));

                if (GetRawInputDeviceList(IntPtr.Zero, ref deviceCount, (uint)dwSize) == 0)
                {
                    IntPtr pRawInputDeviceList = Marshal.AllocHGlobal((int)(dwSize * deviceCount));

                    GetRawInputDeviceList(pRawInputDeviceList, ref deviceCount, (uint)dwSize);

                    for (int i = 0; i < deviceCount; i++)
                    {
                        RAWINPUTDEVICELIST rid = (RAWINPUTDEVICELIST)Marshal.PtrToStructure(new IntPtr((pRawInputDeviceList.ToInt32() + (dwSize * i))), typeof(RAWINPUTDEVICELIST));

                        uint pcbSize = 0;

                        GetRawInputDeviceInfo(rid.hDevice, RIDI_DEVICENAME, IntPtr.Zero, ref pcbSize);

                        IntPtr pData = Marshal.AllocHGlobal((int)pcbSize);
                        string deviceName, friendlyName;

                        GetRawInputDeviceInfo(rid.hDevice, RIDI_DEVICENAME, pData, ref pcbSize);
                        deviceName = (string)Marshal.PtrToStringAnsi(pData);

                        string item = deviceName.Substring(4);
                        string[] split = item.Split('#');
                        string id1 = split[0];    // ACPI (Class code)
                        string id2 = split[1];    // PNP0303 (SubClass code)
                        string id3 = split[2];    // 3&13c0b0c5&0 (Protocol code)

                        RegistryKey RegKey = Registry.LocalMachine.OpenSubKey(String.Format(@"System\CurrentControlSet\Enum\{0}\{1}\{2}", id1, id2, id3));
                        friendlyName = (string)RegKey.GetValue("DeviceDesc", RegistryValueKind.String);
                        RegKey.Close();

                        if (!deviceName.ToUpper().Contains("ROOT"))
                        {
                            if (rid.dwType == RIM_TYPEKEYBOARD && rawDeviceType == RawDeviceType.Keyboard)
                                retList.Add(bFriendlyName ? friendlyName : deviceName);
                            else if (rid.dwType == RIM_TYPEMOUSE && rawDeviceType == RawDeviceType.Mouse)
                                retList.Add(bFriendlyName ? friendlyName : deviceName);
                            else if (rid.dwType == RIM_TYPEHID && rawDeviceType == RawDeviceType.HID)
                                retList.Add(bFriendlyName ? friendlyName : deviceName);
                        }

                        Marshal.FreeHGlobal(pData);
                    }

                    Marshal.FreeHGlobal(pRawInputDeviceList);
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("GetRawInputDeviceName", "RawInput", ex.Message, ex.StackTrace);
            }

            return retList.ToArray();
        }
    }
}
