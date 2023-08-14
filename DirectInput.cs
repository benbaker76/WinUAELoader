// Copyright (c) 2008, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Timers;
using DInput = Microsoft.DirectX.DirectInput;

namespace WinUAELoader
{
    public class DirectInput
    {
        public enum DeviceType
        {
            Keyboard,
            Mouse,
            Joystick
        }

        public static string[] GetDInputDeviceName(DeviceType deviceType, bool bFriendlyName)
        {
            List<string> retVal = new List<string>();

            try
            {
                DInput.DeviceList gameControllerList = DInput.Manager.GetDevices(DInput.DeviceClass.All, DInput.EnumDevicesFlags.AllDevices);

                foreach (DInput.DeviceInstance deviceInstance in gameControllerList)
                {
                    string deviceName = String.Format("{0} {1}", Convert.StrRemoveLastInstanceOf(deviceInstance.ProductGuid.ToString(), '-').ToUpper(), Convert.StrRemoveLastInstanceOf(deviceInstance.InstanceGuid.ToString(), '-').ToUpper());
                    string friendlyName = deviceInstance.InstanceName;

                    if (deviceType == DeviceType.Keyboard && deviceInstance.DeviceType == DInput.DeviceType.Keyboard)
                        retVal.Add(bFriendlyName ? friendlyName : deviceName);
                    else if (deviceType == DeviceType.Mouse && deviceInstance.DeviceType == DInput.DeviceType.Mouse)
                        retVal.Add(bFriendlyName ? friendlyName : deviceName);
                    else if (deviceType == DeviceType.Joystick && deviceInstance.DeviceType == DInput.DeviceType.Joystick)
                        retVal.Add(bFriendlyName ? friendlyName : deviceName);
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("GetDInputDeviceName", "DirectInput", ex.Message, ex.StackTrace);
            }

            return retVal.ToArray();
        }
    }
}
