// Copyright (c) 2008, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WinUAELoader
{
    public enum ArtworkType
    {
        Titlescreen,
        Screenshot1,
        Screenshot2,
        BoxArtFront,
        BoxArtBack
    }

    public enum ROMType
    {
        GameBase,
        WHDLoad,
        SPS,
        DemoBase
    }

    class Global
    {
        public static string[] RomTypeString = { "GameBase", "WHDLoad", "SPS", "DemoBase" };
        public static string[] ArtworkTypeString = { "Title Screen", "Screenshot 1", "Screenshot 2", "Box Art Front", "Box Art Back" };
        public static string Version = "v1.78";
        public static GBDatabase GBDatabase = null;

        public static frmMain MainForm = null;

        public static KeyboardHook KeyboardHook = null;
        public static SendKeys SendKeys = null;
        public static WinUAE WinUAE = null;

        //public static DisplayMode[] DisplayMode = null;
        public static Screen[] Screen = null;
        public static int[] Depth = null;
        public static int[] Refresh = null;
    }

    class Settings
    {
        public class General
        {
            public static bool FirstRun = false;
            public static ROMType LoaderMode = ROMType.GameBase;
        }

        public class Folder
        {
            public static string StartupPath = null;
            public static string Data = null;
            public static string Configs = null;
            public static string GameEx = null;
            public static string GameEx_MapFiles = null;
            public static string GameEx_Database = null;
            public static string Media = null;
            public static string Temp = null;
            public static string WHD = null;
            public static string WHDRun = null;
            public static string GameBaseROMs = null;
            public static string WHDLoadROMs = null;
            public static string SPSROMs = null;
            public static string DemoBaseROMs = null;
        }

        public class File
        {
            public static string Log = null;
            public static string Config = null;
            public static string GameBaseDatabase = null;
            public static string DemoBaseDatabase = null;
            public static string GameEx_GameBaseMap = null;
            public static string GameEx_WHDLoadMap = null;
            public static string GameEx_SPSMap = null;
            public static string GameEx_DemoBaseMap = null;
            public static string GameBaseXml = null;
            public static string WHDLoadXml = null;
            public static string SPSXml = null;
            public static string DemoBaseXml = null;
            public static string WinUAEExe = null;
            public static string UAEConfig_GameBase = null;
            public static string UAEConfig_WHDLoad = null;
            public static string GameEx_GameBaseDatabase = null;
            public static string GameEx_WHDLoadDatabase = null;
            public static string GameEx_SPSDatabase = null;
            public static string GameEx_DemoBaseDatabase = null;
            public static string SystemConfigurationDefault = null;
            public static string SystemConfigurationBackground = null;
            public static string SystemConfigurationAll = null;
            public static string Biography = null;
        }

        public class Media
        {
            public static string XArcadeWinUAE = null;
            public static string XArcadeKeys = null;
        }

        public class WHD
        {
            public class Folder
            {
                public static string C = null;
                public static string Devs = null;
                public static string Libs = null;
                public static string S = null;
            }

            public class FileName
            {
                public static string UserStartup = null;
                public static string WHDLoadPrefs = null;
            }
        }

        public class WHDRun
        {
            public static string Exe = null;
            public static string GamePack = null;
            public static string TempDir = null;
            public static string WinUAEExe = null;
            public static string UAEConfig = null;
            public static string UserStartup = null;
            public static bool EnableForceClose = true;
            public static int ForceCloseTimeout = 5;
            public static string IniFile = null;
            public static CLIHideType CLIHide = CLIHideType.Default;
            public static bool DeleteTempFiles = false;
        }

        public class Input
        {
            public static bool Enabled = true;
            public static InputType Port0 = InputType.XArcadeLeft;
            public static InputType Port1 = InputType.XArcadeRight;
            public static uint ExitKey = (uint)Keys.Escape;
            public static uint DiskSwapperKey = (uint)Keys.P;
            public static int MouseSpeed = 1;
            public static bool EnableMouseMap = false;
            public static bool EnableKeyboardAMap = false;
            public static bool EnableKeyboardBMap = false;
            public static bool EnableKeyboardCMap = false;
            public static bool EnableXArcadeLeftMap = false;
            public static bool EnableXArcadeRightMap = false;
        }

        public class Config
        {
            public static bool OverwriteConfigs = true;
            public static string AmigaConfigFile = null;
            public static string ROMFolder = null;
            public static string OutputFolder = null;
            public static bool OnlyApply = false;
            public static string OnlyApplyWord = "AGA";
        }

        public class Display
        {
            public static bool Enabled = true;
            public static int FullScreen = 0;
            public static int VSync = 0;
            public static int Screen = 0;
            public static int Depth = 0;
            public static int Refresh = 0;
            public static int Resolution = 1;
            public static int LineMode = 1;
            public static bool CenterHorizontal = false;
            public static bool CenterVertical = false;
            public static bool BlackerThanBlack = false;
            public static bool RemoveInterlaceArtifacts = false;
            public static bool FilteredLowResolution = false;
            public static bool ResolutionAutoswitch = false;
        }

        public class Tools
        {
            public static string ArtworkSource = null;
            public static string ArtworkDest = null;
            public static ArtworkType ArtworkType = ArtworkType.Screenshot1;
            public static ROMType ArtworkROMType = ROMType.GameBase;
        }

        public class GameEx
        {
            public static bool RemoveMissingGames = true;
            public static bool FilterGames = false;
            public static string FilterString = null;
        }
    }
}
