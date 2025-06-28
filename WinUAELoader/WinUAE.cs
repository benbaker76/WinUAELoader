// Copyright (c) 2008, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WinUAELoader
{
    enum PalNTSCType : int
    {
        PAL = 0,
        BOTH,
        NTSC
    }

    enum InputType : int
    {
        Mouse,
        Joystick1,
        Joystick2,
        KeyboardA,
        KeyboardB,
        KeyboardC,
        XArcadeLeft,
        XArcadeRight
    }

    enum ControlType : int
    {
        JoyPort2 = 0,
        JoyPort1,
        Keyboard,
        PaddlePort2,
        PaddlePort1,
        Mouse,
        Lightpen,
        KoalaPad,
        LightGun
    }

    enum CLIHideType : int
    {
        Default = 0,
        Background,
        All
    }

    class InputNode
    {
        public string Name = null;
        public uint Input = 0;
        public uint NewInput = (uint)Keys.None;

        public InputNode(string name, uint input, uint newInput)
        {
            Name = name;
            Input = input;
            NewInput = newInput;
        }
    }

    class UAENode
    {
        public string Name = null;
        public string Value = null;

        public UAENode(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

    class WinUAE
    {
        public string[] WindowedResolutions =
        {
            "320x256",
            "640x512",
            "720x576"
        };

        /* public string[] FullScreenResolutions =
        {
            "640x480",
            "800x600"
        }; */

        public string[] FullScreen =
        {
            "Windowed",
            "Fullscreen",
            "Full-window"
        };

        public string[] VSync =
        {
            "-",
            "Low latency VSync",
            "Low latency, 50/60Hz",
            "Legacy VSync",
            "Legacy VS, 50/60Hz"
        };

        public string[] Resolution =
        {
            "Lores",
            "Hires (normal)",
            "SuperHires"
        };

        public string[] LineMode =
        {
            "Normal",
            "Double",
            "Scanlines"
        };

        public string[] Control =
        {
            "JoyPort2",
            "JoyPort1",
            "Keyboard",
            "PaddlePort2",
            "PaddlePort1",
            "Mouse",
            "Lightpen",
            "KoalaPad",
            "LightGun"
        };

        public string[] Input =
        {
            "Mouse",
            "Joystick 1",
            "Joystick 2",
            "Keyboard A",
            "Keyboard B",
            "Keyboard C",
            "X-Arcade Left",
            "X-Arcade Right"
        };

        public InputNode[] Mouse =
        {
            new InputNode("Mouse Up", (uint) Keys.None, (uint) Keys.R),
            new InputNode("Mouse Down", (uint) Keys.None, (uint) Keys.F),
            new InputNode("Mouse Left", (uint) Keys.None, (uint) Keys.D),
            new InputNode("Mouse Right", (uint) Keys.None, (uint) Keys.G),
            new InputNode("Button 1", (uint) Keys.None, (uint) Keys.A),
            new InputNode("Button 2", (uint) Keys.None, (uint) Keys.S)
        };

        public InputNode[] KeyboardA =
        {
            new InputNode("Joy Up", (uint) Keys.NumPad8, (uint) Keys.R),
            new InputNode("Joy Down", (uint) Keys.NumPad2, (uint) Keys.F),
            new InputNode("Joy Left", (uint) Keys.NumPad4, (uint) Keys.D),
            new InputNode("Joy Right", (uint) Keys.NumPad6, (uint) Keys.G),
            new InputNode("Button 1", (uint) Keys.NumPad5, (uint) Keys.A)
        };

        public InputNode[] KeyboardB =
        {
            new InputNode("Joy Up", (uint) Keys.Up, (uint) Keys.None),
            new InputNode("Joy Down", (uint) Keys.Down, (uint) Keys.None),
            new InputNode("Joy Left", (uint) Keys.Left, (uint) Keys.None),
            new InputNode("Joy Right", (uint) Keys.Right, (uint) Keys.None),
            new InputNode("Button 1", (uint) Keys.RControlKey, (uint) Keys.LControlKey)
        };

        public InputNode[] KeyboardC =
        {
            new InputNode("Joy Up", (uint) Keys.T, (uint) Keys.None),
            new InputNode("Joy Down", (uint) Keys.B, (uint) Keys.None),
            new InputNode("Joy Left", (uint) Keys.F, (uint) Keys.None),
            new InputNode("Joy Right", (uint) Keys.H, (uint) Keys.None),
            new InputNode("Button 1", (uint) Keys.LMenu, (uint) Keys.None)
        };

        public InputNode[] XArcadeLeft =
        {
            new InputNode("F1", (uint) Keys.D1, (uint) Keys.None),
            new InputNode("Joy Up", (uint) Keys.NumPad8, (uint) Keys.Up),
            new InputNode("Joy Down", (uint) Keys.NumPad2, (uint) Keys.Down),
            new InputNode("Joy Left", (uint) Keys.NumPad4, (uint) Keys.Left),
            new InputNode("Joy Right", (uint) Keys.NumPad6, (uint) Keys.Right),
            new InputNode("Button 1", (uint) Keys.LControlKey, (uint) Keys.None),
            new InputNode("Button 2", (uint) Keys.LMenu, (uint) Keys.None),
            new InputNode("Button 3", (uint) Keys.Space, (uint) Keys.None),
            new InputNode("Space Bar", (uint) Keys.LShiftKey, (uint) Keys.None),
            new InputNode("3", (uint) Keys.Z, (uint) Keys.None),
            new InputNode("4", (uint) Keys.X, (uint) Keys.None),
            new InputNode("1", (uint) Keys.C, (uint) Keys.None),
            new InputNode("2", (uint) Keys.D5, (uint) Keys.None),
            new InputNode("Left Alt", (uint) Keys.D3, (uint) Keys.V)
        };

        public InputNode[] XArcadeRight =
        {
            new InputNode("F2", (uint) Keys.D2, (uint) Keys.None),
            new InputNode("Joy Up", (uint) Keys.R, (uint) Keys.None),
            new InputNode("Joy Down", (uint) Keys.F, (uint) Keys.None),
            new InputNode("Joy Left", (uint) Keys.D, (uint) Keys.None),
            new InputNode("Joy Right", (uint) Keys.G, (uint) Keys.None),
            new InputNode("Button 1", (uint) Keys.A, (uint) Keys.None),
            new InputNode("Button 2", (uint) Keys.S, (uint) Keys.None),
            new InputNode("Button 3", (uint) Keys.Q, (uint) Keys.None),
            new InputNode("Space Bar", (uint) Keys.OemOpenBrackets, (uint) Keys.K),
            new InputNode("Enter", (uint) Keys.OemCloseBrackets, (uint) Keys.J),
            new InputNode("Down Arrow", (uint) Keys.D6, (uint) Keys.None),
            new InputNode("Right Alt", (uint) Keys.D4, (uint) Keys.L)
        };

        public Process WinUAEProcess = null;
        public Process WHDRunProcess = null;
        private bool ProcessRunning = false;

        private bool SendingDiskSwapperKeys = false;

        private int CurrentSlot = 2;
        private int NumDisks = 1;

        private System.Timers.Timer MouseTimer = null;
        private bool UseMouse = false;

        private int[] MouseSpeed = new int[4];
        private bool[] MouseDown = new bool[4];

        public WinUAE()
        {
            Global.KeyboardHook.OnGlobalKeyEvent += new KeyboardHook.GlobalKeyEventHandler(KeyboardHook_OnGlobalKeyEvent);

            MouseTimer = new System.Timers.Timer();
            MouseTimer.Elapsed += new ElapsedEventHandler(MouseTimer_Elapsed);
            MouseTimer.Interval = 50;
            MouseTimer.Enabled = true;
        }

        private void KeyboardHook_OnGlobalKeyEvent(object sender, GlobalKeyEventArgs e)
        {
            try
            {
                if (!ProcessRunning)
                    return;

                if (SendingDiskSwapperKeys)
                    return;

                if (UseMouse && Settings.Input.EnableMouseMap)
                    ProcessMouseKeys(e);

                ProcessWinUAEKeys(e);
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("KeyboardHook_OnGlobalKeyEvent", "WinUAE", ex.Message, ex.StackTrace);
            }
        }

        private void MouseTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!UseMouse || !Settings.Input.EnableMouseMap)
                return;

            if (MouseDown[0]) // Up
            {
                Point mousePos = System.Windows.Forms.Cursor.Position;
                mousePos.Y -= MouseSpeed[0];
                System.Windows.Forms.Cursor.Position = mousePos;
                MouseSpeed[0] += Settings.Input.MouseSpeed;
            }

            if (MouseDown[1]) // Down
            {
                Point mousePos = System.Windows.Forms.Cursor.Position;
                mousePos.Y += MouseSpeed[1];
                System.Windows.Forms.Cursor.Position = mousePos;
                MouseSpeed[1] += Settings.Input.MouseSpeed;
            }

            if (MouseDown[2]) // Left
            {
                Point mousePos = System.Windows.Forms.Cursor.Position;
                mousePos.X -= MouseSpeed[2];
                System.Windows.Forms.Cursor.Position = mousePos;
                MouseSpeed[2] += Settings.Input.MouseSpeed;
            }

            if (MouseDown[3]) // Right
            {
                Point mousePos = System.Windows.Forms.Cursor.Position;
                mousePos.X += MouseSpeed[3];
                System.Windows.Forms.Cursor.Position = mousePos;
                MouseSpeed[3] += Settings.Input.MouseSpeed;
            }
        }

        private void ProcessMouseKeys(GlobalKeyEventArgs e)
        {
            try
            {
                if (!UseMouse || !Settings.Input.EnableMouseMap)
                    return;

                if (e.Key == Mouse[0].NewInput) // Up
                {
                    if (e.KeyDown)
                        MouseDown[0] = true;
                    else
                    {
                        MouseDown[0] = false;
                        MouseSpeed[0] = 0;
                    }

                    e.Handled = true;
                }
                else if (e.Key == Mouse[1].NewInput) // Down
                {
                    if (e.KeyDown)
                        MouseDown[1] = true;
                    else
                    {
                        MouseDown[1] = false;
                        MouseSpeed[1] = 0;
                    }

                    e.Handled = true;
                }
                else if (e.Key == Mouse[2].NewInput) // Left
                {
                    if (e.KeyDown)
                        MouseDown[2] = true;
                    else
                    {
                        MouseDown[2] = false;
                        MouseSpeed[2] = 0;
                    }

                    e.Handled = true;
                }
                else if (e.Key == Mouse[3].NewInput) // Right
                {
                    if (e.KeyDown)
                        MouseDown[3] = true;
                    else
                    {
                        MouseDown[3] = false;
                        MouseSpeed[3] = 0;
                    }

                    e.Handled = true;
                }
              
                if (e.Key == Mouse[4].NewInput) // Button 1
                {
                    if (e.KeyDown)
                    {
                        Point mousePos = System.Windows.Forms.Cursor.Position;
                        SendMouse.SetLeftMouseButtonDown(mousePos);
                    }
                    else
                    {
                        Point mousePos = System.Windows.Forms.Cursor.Position;
                        SendMouse.SetLeftMouseButtonUp(mousePos);
                    }

                    e.Handled = true;
                }
                else if (e.Key == Mouse[5].NewInput) // Button 2
                {
                    if (e.KeyDown)
                    {
                        Point mousePos = System.Windows.Forms.Cursor.Position;
                        SendMouse.SetRightMouseButtonDown(mousePos);
                    }
                    else
                    {
                        Point mousePos = System.Windows.Forms.Cursor.Position;
                        SendMouse.SetRightMouseButtonUp(mousePos);
                    }

                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("ProcessMouseKeys", "WinUAE", ex.Message, ex.StackTrace);
            }
        }

        public void CloseWinUAEProcess()
        {
            if (WinUAEProcess != null)
            {
                if (!WinUAEProcess.HasExited)
                    WinUAEProcess.CloseMainWindow();
            }
        }

        public void CloseWHDRunProcess()
        {
            if (WHDRunProcess != null)
            {
                if (!WHDRunProcess.HasExited)
                {
                    WHDRunProcess.WaitForExit(Settings.WHDRun.ForceCloseTimeout * 1000);

                    IntPtr hWndWinUAE = FindProcess.FindWindow(null, "PCsuxRox");

                    if (hWndWinUAE != IntPtr.Zero)
                        Win32.SendMessage(hWndWinUAE, Win32.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

                    hWndWinUAE = FindProcess.FindWindow(null, "AmigaPowah");

                    if (hWndWinUAE != IntPtr.Zero)
                        Win32.SendMessage(hWndWinUAE, Win32.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

                    if (!WHDRunProcess.HasExited)
                        WHDRunProcess.CloseMainWindow();
                }
            }
        }

        private void ProcessWinUAEKeys(GlobalKeyEventArgs e)
        {
            try
            {
                if (e.Key == Settings.Input.DiskSwapperKey)
                {
                    if (!e.KeyDown)
                        PressDiskSwapperKey();

                    e.Handled = true;

                    return;
                }

                if (e.Key == Settings.Input.ExitKey)
                {
                    if (!e.KeyDown)
                    {
                        switch (Settings.General.LoaderMode)
                        {
                            case ROMType.GameBase:
                            case ROMType.SPS:
                            case ROMType.DemoBase:
                                CloseWinUAEProcess();
                                break;
                            case ROMType.WHDLoad:
                                CloseWHDRunProcess();
                                break;
                        }
                    }

                    return;
                }

                if (Settings.Input.EnableKeyboardAMap)
                {
                    for (int i = 0; i < KeyboardA.Length; i++)
                    {
                        if (KeyboardA[i].NewInput != (uint)Keys.None)
                        {
                            if (e.Key == KeyboardA[i].NewInput)
                            {
                                if (KeyboardA[i].NewInput == KeyboardA[i].Input)
                                    continue;

                                e.Key = KeyboardA[i].Input;
                                e.ReMap = true;
                                e.Handled = true;
                            }
                        }
                    }
                }

                if (Settings.Input.EnableKeyboardBMap)
                {
                    for (int i = 0; i < KeyboardB.Length; i++)
                    {
                        if (KeyboardB[i].NewInput != (uint)Keys.None)
                        {
                            if (e.Key == KeyboardB[i].NewInput)
                            {
                                if (KeyboardB[i].NewInput == KeyboardB[i].Input)
                                    continue;

                                e.Key = KeyboardB[i].Input;
                                e.ReMap = true;
                                e.Handled = true;
                            }
                        }
                    }
                }

                if (Settings.Input.EnableKeyboardCMap)
                {
                    for (int i = 0; i < KeyboardC.Length; i++)
                    {
                        if (KeyboardC[i].NewInput != (uint)Keys.None)
                        {
                            if (e.Key == KeyboardC[i].NewInput)
                            {
                                if (KeyboardC[i].NewInput == KeyboardC[i].Input)
                                    continue;

                                e.Key = KeyboardC[i].Input;
                                e.ReMap = true;
                                e.Handled = true;
                            }
                        }
                    }
                }

                if (Settings.Input.EnableXArcadeLeftMap)
                {
                    for (int i = 0; i < XArcadeLeft.Length; i++)
                    {
                        if (XArcadeLeft[i].NewInput != (uint)Keys.None)
                        {
                            if (e.Key == XArcadeLeft[i].NewInput)
                            {
                                if (XArcadeLeft[i].NewInput == XArcadeLeft[i].Input)
                                    continue;

                                e.Key = XArcadeLeft[i].Input;
                                e.ReMap = true;
                                e.Handled = true;
                            }
                        }
                    }
                }

                if (Settings.Input.EnableXArcadeRightMap)
                {
                    for (int i = 0; i < XArcadeRight.Length; i++)
                    {
                        if (XArcadeRight[i].NewInput != (uint)Keys.None &&
                            XArcadeRight[i].NewInput != XArcadeRight[i].Input)
                        {
                            if (e.Key == XArcadeRight[i].NewInput)
                            {
                                if (XArcadeRight[i].NewInput == XArcadeRight[i].Input)
                                    continue;

                                e.Key = XArcadeRight[i].Input;
                                e.ReMap = true;
                                e.Handled = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("ProcessWinUAEKeys", "WinUAE", ex.Message, ex.StackTrace);
            }
        }

        public string GetKeyValues(InputNode[] inputNode)
        {
            string keyValues = null;

            try
            {
                for (int i = 0; i < inputNode.Length; i++)
                {
                    if (i > 0)
                        keyValues += ",";
                    keyValues += inputNode[i].NewInput.ToString();
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("GetKeyValues", "WinUAE", ex.Message, ex.StackTrace);
            }

            return keyValues;
        }

        public void SetKeyValues(ref InputNode[] inputNode, string keyValues)
        {
            try
            {
                string[] keyString = keyValues.Split(new char[] { ',' });

                for (int i = 0; i < inputNode.Length && i < keyString.Length; i++)
                    inputNode[i].NewInput = (uint)Convert.StrToInt(keyString[i]);
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("SetKeyValues", "WinUAE", ex.Message, ex.StackTrace);
            }
        }

        public void LaunchWinUAE(string UAEConfig, string[] GamePathFile)
        {
            try
            {
                WinUAEProcess = new Process();
                WinUAEProcess.Exited += new EventHandler(WinUAEProcess_Exited);
                WinUAEProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName(Settings.File.WinUAEExe);
                WinUAEProcess.StartInfo.FileName = Settings.File.WinUAEExe;
                WinUAEProcess.StartInfo.Arguments = String.Format("-f \"{0}\" -0 \"{1}\" -1 \"{2}\" -2 \"{3}\" -3 \"{4}\" -s diskimage0=\"{5}\" -s diskimage1=\"{6}\" -s diskimage2=\"{7}\" -s diskimage3=\"{8}\" -s diskimage4=\"{9}\" -s diskimage5=\"{10}\" -s diskimage6=\"{11}\" -s diskimage7=\"{12}\" -s diskimage8=\"{13}\" -s diskimage9=\"{14}\" -s diskimage10=\"{15}\" -s diskimage11=\"{16}\" -s diskimage12=\"{17}\" -s diskimage13=\"{18}\" -s diskimage14=\"{19}\" -s diskimage15=\"{20}\" -s diskimage16=\"{21}\" -s diskimage17=\"{22}\" -s diskimage18=\"{23}\" -s diskimage19=\"{24}\"", UAEConfig, GamePathFile[0], GamePathFile[1], GamePathFile[2], GamePathFile[3], GamePathFile[0], GamePathFile[1], GamePathFile[2], GamePathFile[3], GamePathFile[4], GamePathFile[5], GamePathFile[6], GamePathFile[7], GamePathFile[8], GamePathFile[9], GamePathFile[10], GamePathFile[11], GamePathFile[12], GamePathFile[13], GamePathFile[14], GamePathFile[15], GamePathFile[16], GamePathFile[17], GamePathFile[18], GamePathFile[19]);

                WinUAEProcess.EnableRaisingEvents = true;
                WinUAEProcess.Start();
                ProcessRunning = true;
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("LaunchWinUAE", "WinUAE", ex.Message, ex.StackTrace);
            }
        }

        public void LaunchWinUAEGUI(string UAEConfig)
        {
            try
            {
                RegistryKey RegKey = Registry.CurrentUser.OpenSubKey("Software\\Arabuusimiehet\\WinUAE", true);
                RegKey.SetValue("ConfigFile", Path.GetFileName(UAEConfig), RegistryValueKind.String);
                RegKey.SetValue("ConfigurationPath", Path.GetDirectoryName(UAEConfig), RegistryValueKind.String);
                RegKey.Close();

                WinUAEProcess = new Process();
                WinUAEProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName(Settings.File.WinUAEExe);
                WinUAEProcess.StartInfo.FileName = Settings.File.WinUAEExe;
                WinUAEProcess.StartInfo.Arguments = String.Format("-f \"{0}\" -s use_gui=yes", UAEConfig);

                WinUAEProcess.Start();
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("LaunchWinUAEGUI", "WinUAE", ex.Message, ex.StackTrace);
            }
        }

        void WinUAEProcess_Exited(object sender, EventArgs e)
        {
            ProcessRunning = false;
        }

        public void LaunchWHDRun()
        {
            try
            {
                WHDRunProcess = new Process();
                WHDRunProcess.Exited += new EventHandler(WHDRunProcess_Exited);
                WHDRunProcess.StartInfo.WorkingDirectory = Settings.Folder.WHDRun;
                WHDRunProcess.StartInfo.FileName = Settings.WHDRun.Exe;
                WHDRunProcess.StartInfo.Arguments = String.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\"", Settings.WHDRun.GamePack, Settings.WHDRun.TempDir, Settings.WHDRun.WinUAEExe, Settings.WHDRun.UAEConfig, Settings.WHDRun.UserStartup);
                //WHDRunProcess.StartInfo.CreateNoWindow = true;
                //WHDRunProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                WHDRunProcess.EnableRaisingEvents = true;
                WHDRunProcess.Start();
                ProcessRunning = true;
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("LaunchWHDRun", "WinUAE", ex.Message, ex.StackTrace);
            }
        }

        void WHDRunProcess_Exited(object sender, EventArgs e)
        {
            ProcessRunning = false;
            DeleteTempFiles();
        }

        public void CalcDiskSpan(string folder, string firstDisk, int numDisks, ref string[] gamePathArray)
        {
            try
            {
                gamePathArray[0] = Path.Combine(folder, firstDisk);

                if (numDisks <= 1)
                    return;

                if (firstDisk.Contains("_Disk0"))
                {
                    for (int i = 1; i < numDisks; i++)
                        gamePathArray[i] = Path.Combine(folder, firstDisk.Replace("_Disk0", "_Disk" + i.ToString()));
                }
                else if (firstDisk.Contains("_Disk1"))
                {
                    for (int i = 1; i < numDisks; i++)
                        gamePathArray[i] = Path.Combine(folder, firstDisk.Replace("_Disk1", "_Disk" + (i + 1).ToString()));
                }
                else if (firstDisk.Contains("_DiskA"))
                {
                    for (int i = 1; i < numDisks; i++)
                        gamePathArray[i] = Path.Combine(folder, firstDisk.Replace("_DiskA", "_Disk" + ((char)(65 + i)).ToString()));
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("CalcDiskSpan", "WinUAE", ex.Message, ex.StackTrace);
            }
        }

        public string[] GetJoyPortSettings(bool useMouse)
        {
            string[] joyPorts = new string[2];

            UseMouse = false;

            try
            {
                switch (Settings.Input.Port0)
                {
                    case InputType.Mouse:
                        UseMouse = true;
                        joyPorts[0] = "joyport0=mouse0";
                        break;
                    case InputType.Joystick1:
                        joyPorts[0] = "joyport0=joy0";
                        break;
                    case InputType.Joystick2:
                        joyPorts[0] = "joyport0=joy1";
                        break;
                    case InputType.KeyboardA:
                        joyPorts[0] = "joyport0=kbd1";
                        break;
                    case InputType.KeyboardB:
                        joyPorts[0] = "joyport0=kbd2";
                        break;
                    case InputType.KeyboardC:
                        joyPorts[0] = "joyport0=kbd3";
                        break;
                    case InputType.XArcadeLeft:
                        joyPorts[0] = "joyport0=kbd4";
                        break;
                    case InputType.XArcadeRight:
                        joyPorts[0] = "joyport0=kbd5";
                        break;
                }

                switch (Settings.Input.Port1)
                {
                    case InputType.Mouse:
                        joyPorts[1] = "joyport1=mouse0";
                        break;
                    case InputType.Joystick1:
                        joyPorts[1] = "joyport1=joy0";
                        break;
                    case InputType.Joystick2:
                        joyPorts[1] = "joyport1=joy1";
                        break;
                    case InputType.KeyboardA:
                        joyPorts[1] = "joyport1=kbd1";
                        break;
                    case InputType.KeyboardB:
                        joyPorts[1] = "joyport1=kbd2";
                        break;
                    case InputType.KeyboardC:
                        joyPorts[1] = "joyport1=kbd3";
                        break;
                    case InputType.XArcadeLeft:
                        joyPorts[1] = "joyport1=kbd4";
                        break;
                    case InputType.XArcadeRight:
                        joyPorts[1] = "joyport1=kbd5";
                        break;
                }

                if (useMouse)
                {
                    UseMouse = true;
                    joyPorts[0] = "joyport0=mouse0";
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("GetJoyPortSettings", "WinUAE", ex.Message, ex.StackTrace);
            }

            return joyPorts;
        }

        public bool TryGetLoaderMode(string fileName, ref ROMType romType)
        {
            if (Global.GBDatabase.GameBaseArray.Count == 0)
                Global.GBDatabase.ReadGameBaseXml(Settings.File.GameBaseXml, Global.GBDatabase.GameBaseArray);

            GameBaseNode GameBaseNode = null;

            if (Global.GBDatabase.TryReadGameBaseXml(Settings.File.GameBaseXml, fileName, Settings.Folder.GameBaseROMs, Global.GBDatabase.GameBaseArray, out GameBaseNode))
            {
                romType = ROMType.GameBase;

                return true;
            }

            // ----------------------

            if (Global.GBDatabase.WHDLoadArray.Count == 0)
                Global.GBDatabase.ReadWHDLoadXml();

            WHDLoadNode WHDLoadNode = null;

            if (Global.GBDatabase.TryReadWHDLoadXml(Settings.File.WHDLoadXml, fileName, out WHDLoadNode))
            {
                romType = ROMType.WHDLoad;

                return true;
            }

            // ----------------------

            if (Global.GBDatabase.SPSArray.Count == 0)
                Global.GBDatabase.ReadSPSXml();

            SPSNode SPSNode = null;

            if (Global.GBDatabase.TryReadSPSXml(Settings.File.SPSXml, fileName, out SPSNode))
            {
                romType = ROMType.SPS;

                return true;
            }

            // ----------------------

            if (Global.GBDatabase.DemoBaseArray.Count == 0)
                Global.GBDatabase.ReadGameBaseXml(Settings.File.DemoBaseXml, Global.GBDatabase.DemoBaseArray);

            if (Global.GBDatabase.TryReadGameBaseXml(Settings.File.DemoBaseXml, fileName, Settings.Folder.DemoBaseROMs, Global.GBDatabase.DemoBaseArray, out GameBaseNode))
            {
                romType = ROMType.DemoBase;

                return true;
            }

            return false;
        }

        public bool RunROM(string fileName)
        {
            try
            {
                switch (Settings.General.LoaderMode)
                {
                    case ROMType.GameBase:
                        if (Global.GBDatabase.GameBaseArray.Count == 0)
                            Global.GBDatabase.ReadGameBaseXml(Settings.File.GameBaseXml, Global.GBDatabase.GameBaseArray);

                        return TryRunGameBaseROM(Settings.File.GameBaseXml, fileName, Settings.Folder.GameBaseROMs, Global.GBDatabase.GameBaseArray);
                    case ROMType.WHDLoad:
                        if (Global.GBDatabase.WHDLoadArray.Count == 0)
                            Global.GBDatabase.ReadWHDLoadXml();

                        return TryRunWHDLoadROM(fileName);
                    case ROMType.SPS:
                        if (Global.GBDatabase.SPSArray.Count == 0)
                            Global.GBDatabase.ReadSPSXml();

                        return TryRunSPSROM(fileName);
                    case ROMType.DemoBase:
                        if (Global.GBDatabase.DemoBaseArray.Count == 0)
                            Global.GBDatabase.ReadGameBaseXml(Settings.File.DemoBaseXml, Global.GBDatabase.DemoBaseArray);

                        return TryRunGameBaseROM(Settings.File.DemoBaseXml, fileName, Settings.Folder.DemoBaseROMs, Global.GBDatabase.DemoBaseArray);
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("RunROM", "WinUAE", ex.Message, ex.StackTrace);
            }

            return false;
        }

        private bool TryRunGameBaseROM(string xmlFileName, string fileName, string romFolder, List<GameBaseNode> GameBaseArray)
        {
            try
            {
                GameBaseNode GameBaseNode = null;

                if (!Global.GBDatabase.TryReadGameBaseXml(xmlFileName, fileName, romFolder, GameBaseArray, out GameBaseNode))
                    return false;

                List<string> uaeArray = new List<string>();

                if(Settings.Display.Enabled)
                    uaeArray.AddRange(GetDisplaySettings());

                if (GameBaseNode.PalNTSC == PalNTSCType.NTSC)
                    uaeArray.Add("ntsc=true");
                else
                    uaeArray.Add("ntsc=false");

                uaeArray.Add("use_gui=no");

                uaeArray.AddRange(GameBaseNode.UAEConfig);

                if(Settings.Input.Enabled)
                    uaeArray.AddRange(GetJoyPortSettings(GameBaseNode.UseMouse));

                if (Settings.Config.OverwriteConfigs)
                    Global.GBDatabase.WriteUAEConfig(Settings.File.UAEConfig_GameBase, uaeArray.ToArray());

                string[] GamePathFile = new string[20];

                CurrentSlot = 2;
                NumDisks = GameBaseNode.NumDisks;

                Global.WinUAE.CalcDiskSpan(romFolder, GameBaseNode.FileName, GameBaseNode.NumDisks, ref GamePathFile);
                Global.WinUAE.LaunchWinUAE(Settings.File.UAEConfig_GameBase, GamePathFile);

                return true;
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("TryRunGameBaseROM", "WinUAE", ex.Message, ex.StackTrace);
            }

            return false;
        }

        private bool TryRunWHDLoadROM(string fileName)
        {
            try
            {
                WHDLoadNode WHDLoadNode = null;

                if (!Global.GBDatabase.TryReadWHDLoadXml(Settings.File.WHDLoadXml, fileName, out WHDLoadNode))
                    return false;

                Settings.WHDRun.GamePack = Path.Combine(Settings.Folder.WHDLoadROMs, WHDLoadNode.FileName);
                Settings.WHDRun.TempDir = Path.Combine(Settings.Folder.Temp, Path.GetFileNameWithoutExtension(WHDLoadNode.FileName));
                Settings.WHDRun.WinUAEExe = Settings.File.WinUAEExe;
                Settings.WHDRun.UAEConfig = Settings.File.UAEConfig_WHDLoad;
                Settings.WHDRun.UserStartup = Settings.WHD.FileName.UserStartup;

                Global.GBDatabase.WriteUserStartup(Settings.WHD.FileName.UserStartup, WHDLoadNode.Cd, WHDLoadNode.WHDLoad);

                List<string> uaeArray = new List<string>();

                if (Settings.Display.Enabled)
                    uaeArray.AddRange(GetDisplaySettings());

                if (WHDLoadNode.PalNTSC == PalNTSCType.NTSC)
                    uaeArray.Add("ntsc=true");
                else
                    uaeArray.Add("ntsc=false");

                uaeArray.Add("use_gui=no");

                uaeArray.AddRange(WHDLoadNode.UAEConfig);

                if (Settings.Input.Enabled)
                    uaeArray.AddRange(GetJoyPortSettings(WHDLoadNode.UseMouse));

                uaeArray.Add(String.Format("filesystem2=rw,DH0:DH0:{0},0", Settings.Folder.WHD));
                uaeArray.Add(String.Format("filesystem=rw,DH0:{0}", Settings.Folder.WHD));
                uaeArray.Add(String.Format("filesystem2=rw,DH1:DH1:{0},0", Settings.WHDRun.TempDir));
                uaeArray.Add(String.Format("filesystem=rw,DH1:{0}", Settings.WHDRun.TempDir));

                if (Settings.Config.OverwriteConfigs)
                    Global.GBDatabase.WriteUAEConfig(Settings.File.UAEConfig_WHDLoad, uaeArray.ToArray());

                WriteWHDRunSettings();
                WriteWHDLoadPrefs();
                CopyWHDLoadSystemConfiguration();

                LaunchWHDRun();

                return true;
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("TryRunWHDLoadROM", "WinUAE", ex.Message, ex.StackTrace);
            }

            return false;
        }

        private bool TryRunSPSROM(string fileName)
        {
            try
            {
                SPSNode SPSNode = null;

                if (!Global.GBDatabase.TryReadSPSXml(Settings.File.SPSXml, fileName, out SPSNode))
                    return false;

                List<string> uaeArray = new List<string>();

                if (Settings.Display.Enabled)
                    uaeArray.AddRange(GetDisplaySettings());

                if (SPSNode.PalNTSC == PalNTSCType.NTSC)
                    uaeArray.Add("ntsc=true");
                else
                    uaeArray.Add("ntsc=false");

                uaeArray.Add("use_gui=no");

                uaeArray.AddRange(SPSNode.UAEConfig);

                if (Settings.Input.Enabled)
                    uaeArray.AddRange(GetJoyPortSettings(SPSNode.UseMouse));

                if (Settings.Config.OverwriteConfigs)
                    Global.GBDatabase.WriteUAEConfig(Settings.File.UAEConfig_GameBase, uaeArray.ToArray());

                string[] GamePathFile = new string[20];

                CurrentSlot = 2;
                NumDisks = SPSNode.NumDisks;

                Global.WinUAE.CalcDiskSpan(Settings.Folder.SPSROMs, SPSNode.FileName, SPSNode.NumDisks, ref GamePathFile);
                Global.WinUAE.LaunchWinUAE(Settings.File.UAEConfig_GameBase, GamePathFile);

                return true;
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("TryRunSPSROM", "WinUAE", ex.Message, ex.StackTrace);
            }

            return false;
        }

        private void WriteWHDRunSettings()
        {
            try
            {
                IniFile iniFile = new IniFile(Settings.WHDRun.IniFile);

                iniFile.WriteValue("global", "quitKey", "$" + String.Format("{0:x}", KeyCodes.VK2Amiga((int)Settings.Input.ExitKey)));
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("WriteWHDRunSettings", "WinUAE", ex.Message, ex.StackTrace);
            }
        }

        private void WriteWHDLoadPrefs()
        {
            try
            {
                string[] lines = File.ReadAllLines(Settings.WHD.FileName.WHDLoadPrefs);

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("QuitKey"))
                        lines[i] = "QuitKey=$" + String.Format("{0:x}", KeyCodes.VK2Amiga((int)Settings.Input.ExitKey));
                }

                File.WriteAllLines(Settings.WHD.FileName.WHDLoadPrefs, lines);
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("WriteWHDLoadPrefs", "WinUAE", ex.Message, ex.StackTrace);
            }
        }

        public string RefreshRateToStr(int refreshRate)
        {
            switch (refreshRate)
            {
                case 0:
                    return "Default";
                case 50:
                    return "(50Hz) PAL";
                case 60:
                    return "60Hz NTSC";
                case 100:
                    return "(100Hz) PAL";
                case 120:
                    return "(120Hz) NTSC";
                default:
                    return String.Format("{0}Hz", refreshRate);
            }
        }

        public int RefreshRateToUAE(int refreshRate)
        {
            switch (refreshRate)
            {
                case 50:
                    return -50;
                case 100:
                    return -100;
                case 120:
                    return -120;
                default:
                    return refreshRate;
            }
        }

        private string[] GetDisplaySettings()
        {
            List<string> displaySettings = new List<string>();

            try
            {
                switch (Settings.Display.VSync)
                {
                    case 0: // -
                        displaySettings.Add("gfx_vsync=false");
                        displaySettings.Add("gfx_vsyncmode=normal");
                        break;
                    case 1: // Low latency VSync
                        displaySettings.Add("gfx_vsync=true");
                        displaySettings.Add("gfx_vsyncmode=busywait");
                        break;
                    case 2: // Low latency, 50/60Hz
                        displaySettings.Add("gfx_vsync=autoswitch");
                        displaySettings.Add("gfx_vsyncmode=busywait");
                        break;
                    case 3: // Legacy VSync
                        displaySettings.Add("gfx_vsync=true");
                        displaySettings.Add("gfx_vsyncmode=normal");
                        break;
                    case 4: // Lagacy VS, 50/60Hz
                        displaySettings.Add("gfx_vsync=autoswitch");
                        displaySettings.Add("gfx_vsyncmode=normal");
                        break;
                }

                switch (Settings.Display.FullScreen)
                {
                    case 0: // Windowed
                        displaySettings.Add("gfx_fullscreen_amiga=false");
                        break;
                    case 1: // Fullscreen
                        displaySettings.Add("gfx_fullscreen_amiga=true");
                        break;
                    case 2: // Full-Window
                        displaySettings.Add("gfx_fullscreen_amiga=fullwindow");
                        break;
                }

                switch (Settings.Display.Screen)
                {
                    case 0: // 320 x 256
                        displaySettings.Add("gfx_width=320");
                        displaySettings.Add("gfx_height=256");
                        displaySettings.Add("gfx_width_windowed=320");
                        displaySettings.Add("gfx_height_windowed=256");
                        break;
                    case 1: // 640 x 512
                        displaySettings.Add("gfx_width=640");
                        displaySettings.Add("gfx_height=512");
                        displaySettings.Add("gfx_width_windowed=640");
                        displaySettings.Add("gfx_height_windowed=512");
                        break;
                    case 2: // 720 x 576
                        displaySettings.Add("gfx_width=720");
                        displaySettings.Add("gfx_height=576");
                        displaySettings.Add("gfx_width_windowed=720");
                        displaySettings.Add("gfx_height_windowed=576");
                        break;
                }

                if (Settings.Display.Screen < Global.Screen.Length)
                {
                    displaySettings.Add("gfx_width=" + Global.Screen[Settings.Display.Screen].Width);
                    displaySettings.Add("gfx_height=" + Global.Screen[Settings.Display.Screen].Height);
                    displaySettings.Add("gfx_width_fullscreen=" + Global.Screen[Settings.Display.Screen].Width);
                    displaySettings.Add("gfx_height_fullscreen=" + Global.Screen[Settings.Display.Screen].Height);
                    if (Global.Depth.Length > 0)
                        displaySettings.Add("gfx_colour_mode=" + Global.Depth[Math.Min(Settings.Display.Depth, Global.Depth.Length - 1)] + "bit");
                    displaySettings.Add("gfx_refreshrate=" + RefreshRateToUAE(Global.Refresh[Settings.Display.Refresh]));
                }

                switch(Settings.Display.Resolution)
                {
                    case 0: // Lores
                        displaySettings.Add("gfx_lores=true");
                        displaySettings.Add("gfx_resolution=lores");
                        break;
                    case 1: // Hires (normal)
                        displaySettings.Add("gfx_lores=false");
                        displaySettings.Add("gfx_resolution=hires");
                        break;
                    case 2: // SuperHires
                        displaySettings.Add("gfx_lores=false");
                        displaySettings.Add("gfx_resolution=superhires");
                        break;
                }

                switch(Settings.Display.LineMode)
                {
                    case 0: // Normal
                        displaySettings.Add("gfx_linemode=none");
                        break;
                    case 1: // Double
                        displaySettings.Add("gfx_linemode=double");
                        break;
                    case 2: // Scanlines
                        displaySettings.Add("gfx_linemode=scanlines");
                        break;
                }

                displaySettings.Add("gfx_center_horizontal=" + (Settings.Display.CenterHorizontal ? "smart" : "none"));
                displaySettings.Add("gfx_center_vertical=" + (Settings.Display.CenterVertical ? "smart" : "none"));
                displaySettings.Add("gfx_blacker_than_black=" + Settings.Display.BlackerThanBlack.ToString().ToLower());
                displaySettings.Add("gfx_flickerfixer=" + Settings.Display.RemoveInterlaceArtifacts.ToString().ToLower());
                displaySettings.Add("gfx_lores_mode=" + (Settings.Display.FilteredLowResolution ? "filtered" : "normal"));
                displaySettings.Add("gfx_autoresolution=" + Settings.Display.ResolutionAutoswitch.ToString().ToLower());
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("GetDisplaySettings", "WinUAE", ex.Message, ex.StackTrace);
            }

            return displaySettings.ToArray();
        }

        private void CopyWHDLoadSystemConfiguration()
        {
            try
            {
                switch (Settings.WHDRun.CLIHide)
                {
                    case CLIHideType.Default:
                        File.Copy(Settings.File.SystemConfigurationDefault, Path.Combine(Settings.WHD.Folder.Devs, "system-configuration"), true);
                        break;
                    case CLIHideType.Background:
                        File.Copy(Settings.File.SystemConfigurationBackground, Path.Combine(Settings.WHD.Folder.Devs, "system-configuration"), true);
                        break;
                    case CLIHideType.All:
                        File.Copy(Settings.File.SystemConfigurationAll, Path.Combine(Settings.WHD.Folder.Devs, "system-configuration"), true);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("CopyWHDLoadSystemConfiguration", "WinUAE", ex.Message, ex.StackTrace);
            }
        }

        public void DeleteTempFiles()
        {
            try
            {
                if (Settings.WHDRun.DeleteTempFiles)
                {
                    Directory.Delete(Settings.Folder.Temp, true);
                    Directory.CreateDirectory(Settings.Folder.Temp);
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("DeleteTempFiles", "WinUAE", ex.Message, ex.StackTrace);
            }
        }

        private void PressDiskSwapperKey()
        {
            try
            {
                if (Settings.General.LoaderMode == ROMType.WHDLoad)
                    return;
					
				if (NumDisks == 1)
					return;

                SendingDiskSwapperKeys = true;

                if (CurrentSlot <= 10)
                {
                    Global.SendKeys.SendKeyDown((short)Keys.End, 1, false, true);
                    Global.SendKeys.SendKeyString(((CurrentSlot == 10) ? "0" : CurrentSlot.ToString()));
                    Global.SendKeys.SendKeyUp((short)Keys.End, true);
                }
                else if (CurrentSlot <= 20)
                {
                    Global.SendKeys.SendKeyDown((short)Keys.ShiftKey, 1, false, true);
                    Global.SendKeys.SendKeyDown((short)Keys.End, 1, false, true);
                    Global.SendKeys.SendKeyString(((CurrentSlot == 20) ? "0" : CurrentSlot.ToString()));
                    Global.SendKeys.SendKeyUp((short)Keys.End, true);
                    Global.SendKeys.SendKeyUp((short)Keys.ShiftKey, true);
                }

                CurrentSlot++;

                if (CurrentSlot > NumDisks)
                    CurrentSlot = 1;

                SendingDiskSwapperKeys = false;
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("PressDiskSwapperKey", "WinUAE", ex.Message, ex.StackTrace);
            }
        }

        public List<UAENode> ReadUAEConfig(string fileName)
        {
            List<UAENode> UAEConfigArray = new List<UAENode>();

            try
            {
                string[] Lines = File.ReadAllLines(fileName);

                for (int i = 0; i < Lines.Length; i++)
                {
                    string Line = Lines[i].Trim();
                    string[] Values = Line.Split(new char[] { '=' });

                    if (Values.Length == 2)
                    {
                        bool Skip = false;

                        if(Values[0].StartsWith("floppy"))
                        {
                            for(int j=0; j<4; j++)
                                if(Values[0] == ("floppy" + j.ToString()))
                                    Skip = true;
                        }

                        if(Values[0].StartsWith("diskimage"))
                             Skip = true;

                        if(!Skip)
                            UAEConfigArray.Add(new UAENode(Values[0], Values[1]));
                    }
                }
            }
            catch
            {
            }

            return UAEConfigArray;
        }

		public void WriteUAEConfig(string outputFolder, List<UAENode> configFile, List<string> configNames)
		{
            string ConfigOutputFile = String.Empty;

            foreach (string configName in configNames)
			{
                if (configNames.IndexOf(configName) == 0)
				{
                    ConfigOutputFile = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(configName) + ".uae");

                    using (StreamWriter sw = File.CreateText(ConfigOutputFile))
                        for (int i = 0; i < configFile.Count; i++)
                            sw.WriteLine(configFile[i].Name + "=" + configFile[i].Value);

                    using(StreamWriter sw = File.AppendText(ConfigOutputFile))
                        sw.WriteLine("floppy0=" + configName);
				}
				else
				{
                    if (configNames.IndexOf(configName) < 4)
                    {
                        using (StreamWriter sw = File.AppendText(ConfigOutputFile))
                        {
                            sw.WriteLine("floppy" + (configNames.IndexOf(configName)).ToString() + "=" + configName);
                            sw.WriteLine("diskimage" + (configNames.IndexOf(configName) - 1).ToString() + "=" + configName);
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(ConfigOutputFile))
                            sw.WriteLine("diskimage" + (configNames.IndexOf(configName) - 1).ToString() + "=" + configName);
                    }
				}
			}
		}
    }
}
